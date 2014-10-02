// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchKeyManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The SearchKeyManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using NHibernate;
  using rt.srz.business.manager.cache;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;
  using StructureMap;

  #endregion

  /// <summary>
  ///   The SearchKeyManager.
  /// </summary>
  public partial class SearchKeyManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The calculate keys.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<SearchKey> CalculateStandardKeys(Statement statement)
    {
      var data = statement.InsuredPersonData;
      var documentManager = ObjectFactory.GetInstance<IDocumentManager>();
      var initials = new List<rt.srz.database.business.standard.FIO>();
      var variations = new List<rt.srz.database.business.standard.FieldVariation>();
      var documents = new List<rt.srz.database.business.standard.Document>();
      var fio = new rt.srz.database.business.standard.FIO()
      {
        familyName = new rt.srz.database.business.standard.FieldVariation { fieldType = rt.srz.database.business.standard.FieldTypes.FamilyName, value = data.LastName, },
        firstName = new rt.srz.database.business.standard.FieldVariation { fieldType = rt.srz.database.business.standard.FieldTypes.FirstName, value = data.FirstName, },
        middleName =
          new rt.srz.database.business.standard.FieldVariation
          {
            fieldType = rt.srz.database.business.standard.FieldTypes.MiddleName,
            value = !string.IsNullOrEmpty(data.MiddleName) ? data.MiddleName : null
          }
      };
      initials.Add(fio);

      // Дата рождения
      if (data.Birthday != null)
      {
        variations.Add(
          new rt.srz.database.business.standard.FieldVariation { fieldType = rt.srz.database.business.standard.FieldTypes.BirthDate, value = data.Birthday.Value.ToString("yyyyMMdd") });
      }

      // Место рождения
      variations.Add(new rt.srz.database.business.standard.FieldVariation { value = data.Birthplace, fieldType = rt.srz.database.business.standard.FieldTypes.BirthPlace });

      // Территория страхования
      if (statement.PointDistributionPolicy != null)
      {
        variations.Add(
          new rt.srz.database.business.standard.FieldVariation
          {
            value = statement.PointDistributionPolicy.Parent.Parent.Okato,
            fieldType = rt.srz.database.business.standard.FieldTypes.InsuranceTerritory
          });

        var temp = statement.TempMedicalInsurance;
        if (temp != null)
        {
          variations.Add(new rt.srz.database.business.standard.FieldVariation { value = "В", fieldType = rt.srz.database.business.standard.FieldTypes.PolicyType });
          variations.Add(
             new rt.srz.database.business.standard.FieldVariation { value = temp.SeriesNumber, fieldType = rt.srz.database.business.standard.FieldTypes.PolicyNumber });

        }

        var polis = statement.PolisMedicalInsurance;
        if (polis != null)
        {
          variations.Add(new rt.srz.database.business.standard.FieldVariation { value = "П", fieldType = rt.srz.database.business.standard.FieldTypes.PolicyType });
          variations.Add(new rt.srz.database.business.standard.FieldVariation { value = polis.SeriesNumber, fieldType = rt.srz.database.business.standard.FieldTypes.PolicyNumber });
        }
      }

      // Документы удостоверяющие личность
      if (!string.IsNullOrEmpty(data.Snils))
      {
        variations.Add(new rt.srz.database.business.standard.FieldVariation { value = data.Snils, fieldType = rt.srz.database.business.standard.FieldTypes.SNILS });
      }

      if (!string.IsNullOrEmpty(statement.NumberPolicy))
      {
        variations.Add(new rt.srz.database.business.standard.FieldVariation { value = statement.NumberPolicy, fieldType = rt.srz.database.business.standard.FieldTypes.ENP });
      }

      if (statement.DocumentUdl != null && statement.DocumentUdl.DocumentType != null)
      {
        documents.Add(
          new rt.srz.database.business.standard.Document
          {
            idCardNumber =
              new rt.srz.database.business.standard.FieldVariation
              {
                value = documentManager.GetSerNumDocument(statement.DocumentUdl),
                fieldType = rt.srz.database.business.standard.FieldTypes.IdCardNumber
              },
            idCardType =
              new rt.srz.database.business.standard.FieldVariation
              {
                value = statement.DocumentUdl.DocumentType.Code,
                fieldType = rt.srz.database.business.standard.FieldTypes.IdCardType
              }
          });
      }

      // Считаем старые ключи
      var oldKeys = rt.srz.database.business.ObjectFactory.GetStandardPseudonymizationManager().CalculateHashes(variations, initials, documents);

      // Считаем новые ключи
      var newKeys = rt.srz.database.business.ObjectFactory.GetStandardPseudonymizationManager().CalculateNewKeys(variations, initials, documents);

      // Объединяем два набора ключей
      newKeys.AddRange(oldKeys.Select(x => new rt.srz.database.business.standard.HashDataNew { key = x.key, hash = x.hash }));

      return
        newKeys
          .Where(x => x.hash != null)
          .Select(
            x =>
            new SearchKey
            {
              KeyValue = x.hash,
              KeyType = ObjectFactory.GetInstance<ISearchKeyTypeCacheManager>().Single(y => y.Code == GetKeyCodeByFullKeyCode(x.key)),
              DocumentUdlType = GetDocumentType(x.key, statement.DocumentUdl != null ? statement.DocumentUdl.DocumentType : new Concept { Id = 0 })
            })
          .ToList();
    }

    /// <summary>
    /// Расчитывает и сохраняет пользовательские ключи для заявления
    /// </summary>
    /// <param name="statement"></param>
    public IList<SearchKey> CalculateUserKeys(IList<rt.srz.model.srz.SearchKeyType> keyTypeList, rt.srz.model.srz.InsuredPersonDatum pd, rt.srz.model.srz.Document doc,
      rt.srz.model.srz.address addr1, rt.srz.model.srz.address addr2, IList<rt.srz.model.srz.MedicalInsurance> medicalInsurances, string okato)
    {
      List<SearchKey> result = new List<SearchKey>();
      foreach (SearchKeyType kt in keyTypeList)
      {
        //Перекодирование типа ключа
        rt.srz.database.business.model.SearchKeyType keyType = new rt.srz.database.business.model.SearchKeyType()
        {
          RowId = kt.Id,
          FirstName = kt.FirstName,
          LastName = kt.LastName,
          MiddleName = kt.MiddleName,
          Birthday = kt.Birthday,
          Birthplace = kt.Birthplace,
          Snils = kt.Snils,
          DocumentType = kt.DocumentType,
          DocumentSeries = kt.DocumentSeries,
          DocumentNumber = kt.DocumentNumber,
          Okato = kt.Okato,
          PolisType = kt.PolisType,
          PolisSeria = kt.PolisSeria,
          PolisNumber = kt.PolisNumber,
          FirstNameLength = kt.FirstNameLength,
          LastNameLength = kt.LastNameLength,
          MiddleNameLength = kt.MiddleNameLength,
          BirthdayLength = kt.BirthdayLength,
          AddressStreet = kt.AddressStreet,
          AddressStreetLength = kt.AddressStreetLength,
          AddressHouse = kt.AddressHouse,
          AddressRoom = kt.AddressRoom,
          AddressStreet2 = kt.AddressStreet2,
          AddressStreetLength2 = kt.AddressStreetLength2,
          AddressHouse2 = kt.AddressHouse2,
          AddressRoom2 = kt.AddressRoom2,
          IdenticalLetters = kt.IdenticalLetters
        };

        // Перекодировка персональных данных
        rt.srz.database.business.model.InsuredPersonDatum personData = new rt.srz.database.business.model.InsuredPersonDatum()
        {
          RowId = pd.Id,
          LastName = pd.LastName,
          FirstName = pd.FirstName,
          MiddleName = pd.MiddleName,
          Birthday = pd.Birthday,
          Birthplace = pd.Birthplace,
          Snils = pd.Snils,
        };

        // Перекодировка документа
        database.business.model.Document document = null;
        if (doc != null && doc.DocumentType != null)
        {

          document = new rt.srz.database.business.model.Document()
                       {
                         RowId = doc.Id,
                         DocumentTypeId = doc.DocumentType.Id,
                         Series = doc.Series,
                         Number = doc.Number,
                       };
        }

        // Перекодировка адреса регистрации
        rt.srz.database.business.model.address address1 = new rt.srz.database.business.model.address()
        {
          RowId = addr1.Id,
          Street = addr1.Street,
          House = addr1.House,
          Room = addr1.Room,
          Okato = addr1.Okato,
        };

        // Перекодировка адреса проживания
        rt.srz.database.business.model.address address2 = null;
        if (addr2 != null)
        {
          address2 = new rt.srz.database.business.model.address()
          {
            RowId = addr2.Id,
            Street = addr2.Street,
            House = addr2.House,
            Room = addr2.Room,
            Okato = addr2.Okato,
          };
        }

        // Перекодировка страховок
        foreach (MedicalInsurance stInsurance in medicalInsurances)
        {
          rt.srz.database.business.model.MedicalInsurance insurance = new rt.srz.database.business.model.MedicalInsurance()
          {
            RowId = stInsurance.Id,
            PolisTypeId = stInsurance.PolisType.Id,
            PolisSeria = stInsurance.PolisSeria,
            PolisNumber = stInsurance.PolisNumber,
          };

          //Расчет ключа
          byte[] keyValue = rt.srz.database.business.ObjectFactory.GetPseudonymizationManager().CalculateUserSearchKey(keyType, personData,
            document, address1, address2, insurance, okato);

          var conceptCacheManager = ObjectFactory.GetInstance<IConceptCacheManager>();
          var searchKey = new SearchKey
          {
            KeyValue = keyValue,
            KeyType = kt,
            DocumentUdlType = doc != null && doc.DocumentType != null ? conceptCacheManager.GetById(doc.DocumentType.Id) : null
          };

          result.Add(searchKey);
        }
      }

      return result;
    }

    /// <summary>
    /// The save search keys.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="keys">
    /// The keys.
    /// </param>
    public void SaveSearchKeys(Statement statement, IEnumerable<SearchKey> keys)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // Удаляем ключи этого же заявления
      Delete(x => x.Statement.Id == statement.Id);

      // запись новых ключей
      foreach (var searchKey in keys)
      {
        searchKey.Statement = statement;
        searchKey.InsuredPerson = statement.InsuredPerson;
        session.Save(searchKey);
      }
    }

    #endregion

    #region Private Methods
    /// <summary>
    /// Возвращает код типа ключа по строчке, возвращаемой алгоритмом расчета новых ключей
    /// </summary>
    /// <param name="fullKeyCode"></param>
    /// <returns></returns>
    private string GetKeyCodeByFullKeyCode(string fullKeyCode)
    {
      string[] splittedKeyCode = fullKeyCode.Split('.');
      if (splittedKeyCode.Any())
        return splittedKeyCode[0];

      return fullKeyCode;
    }
    #endregion

    /// <summary>
    /// Возвращает тип документа по строчке, возвращаемой алгоритмом расчета новых ключей
    /// </summary>
    /// <param name="fullKeyCode"></param>
    /// <returns></returns>
    private Concept GetDocumentType(string fullKeyCode, Concept defaultDocumentType)
    {
      // Старый ключ
      string[] splittedKeyCode = fullKeyCode.Split('.');
      if (splittedKeyCode.Length != 2)
        return defaultDocumentType;

      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      // Стандартный код документа от 1 до 25, Oid='1.2.643.2.40.5.100.203.1'
      var document = conceptManager.SingleOrDefault(x => x.Oid.Id == Oid.ДокументУдл && x.Code == splittedKeyCode[1]);
      if (document != null)
        return document;

      // Форма изготовления полиса Oid='1.2.643.2.40.5.100.86'
      document = conceptManager.SingleOrDefault(x => x.Oid.Id == Oid.Формаизготовленияполиса && x.Code == splittedKeyCode[1]);
      if (document != null)
        return document;

      // Коды типа идентификатора Oid='1.2.643.2.40.5.100.203'
      document = conceptManager.SingleOrDefault(x => x.Oid.Id == Oid.TypeIdentificator && x.Code == splittedKeyCode[1]);
      if (document != null)
        return document;

      return defaultDocumentType;
    }
  }
}