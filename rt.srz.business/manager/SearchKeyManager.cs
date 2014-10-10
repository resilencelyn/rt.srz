// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchKeyManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The SearchKeyManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;

  using rt.srz.business.manager.cache;
  using rt.srz.database.business.standard;
  using rt.srz.model.srz;

  using StructureMap;

  using Document = rt.srz.model.srz.Document;

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
    /// The <see cref="IList{SearchKey}"/>.
    /// </returns>
    public IList<SearchKey> CalculateStandardKeys(Statement statement)
    {
      var data = statement.InsuredPersonData;
      var documentManager = ObjectFactory.GetInstance<IDocumentManager>();
      var initials = new List<FIO>();
      var variations = new List<FieldVariation>();
      var documents = new List<database.business.standard.Document>();
      var fio = new FIO
                {
                  familyName = new FieldVariation { fieldType = FieldTypes.FamilyName, value = data.LastName, },
                  firstName = new FieldVariation { fieldType = FieldTypes.FirstName, value = data.FirstName, },
                  middleName =
                    new FieldVariation
                    {
                      fieldType = FieldTypes.MiddleName,
                      value = !string.IsNullOrEmpty(data.MiddleName) ? data.MiddleName : null
                    }
                };
      initials.Add(fio);

      // Дата рождения
      if (data.Birthday != null)
      {
        variations.Add(
                       new FieldVariation
                       {
                         fieldType = FieldTypes.BirthDate,
                         value = data.Birthday.Value.ToString("yyyyMMdd")
                       });
      }

      // Место рождения
      variations.Add(new FieldVariation { value = data.Birthplace, fieldType = FieldTypes.BirthPlace });

      // Территория страхования
      if (statement.PointDistributionPolicy != null)
      {
        variations.Add(
                       new FieldVariation
                       {
                         value = statement.PointDistributionPolicy.Parent.Parent.Okato,
                         fieldType = FieldTypes.InsuranceTerritory
                       });

        var temp = statement.TempMedicalInsurance;
        if (temp != null)
        {
          variations.Add(new FieldVariation { value = "В", fieldType = FieldTypes.PolicyType });
          variations.Add(new FieldVariation { value = temp.SeriesNumber, fieldType = FieldTypes.PolicyNumber });
        }

        var polis = statement.PolisMedicalInsurance;
        if (polis != null)
        {
          variations.Add(new FieldVariation { value = "П", fieldType = FieldTypes.PolicyType });
          variations.Add(new FieldVariation { value = polis.SeriesNumber, fieldType = FieldTypes.PolicyNumber });
        }
      }

      // Документы удостоверяющие личность
      if (!string.IsNullOrEmpty(data.Snils))
      {
        variations.Add(new FieldVariation { value = data.Snils, fieldType = FieldTypes.SNILS });
      }

      if (!string.IsNullOrEmpty(statement.NumberPolicy))
      {
        variations.Add(new FieldVariation { value = statement.NumberPolicy, fieldType = FieldTypes.ENP });
      }

      if (statement.DocumentUdl != null && statement.DocumentUdl.DocumentType != null)
      {
        documents.Add(
                      new database.business.standard.Document
                      {
                        idCardNumber =
                          new FieldVariation
                          {
                            value =
                              documentManager
                              .GetSerNumDocument(
                                                 statement
                                                   .DocumentUdl),
                            fieldType =
                              FieldTypes.IdCardNumber
                          },
                        idCardType =
                          new FieldVariation
                          {
                            value =
                              statement.DocumentUdl
                                       .DocumentType.Code,
                            fieldType = FieldTypes.IdCardType
                          }
                      });
      }

      // Считаем старые ключи
      var oldKeys = database.business.ObjectFactory.GetStandardPseudonymizationManager()
                            .CalculateHashes(variations, initials, documents);

      // Считаем новые ключи
      var newKeys = database.business.ObjectFactory.GetStandardPseudonymizationManager()
                            .CalculateNewKeys(variations, initials, documents);

      // Объединяем два набора ключей
      newKeys.AddRange(oldKeys.Select(x => new HashDataNew { key = x.key, hash = x.hash }));
      var searchKeyTypeCacheManager = ObjectFactory.GetInstance<ISearchKeyTypeCacheManager>();

      var defaultDocumentType = statement.DocumentUdl != null ? statement.DocumentUdl.DocumentType : new Concept { Id = 0 };
      return
        newKeys.Where(x => x.hash != null)
               .Select(
                       x =>
                       new SearchKey
                                     {
                                       KeyValue = x.hash,
                                       KeyType = searchKeyTypeCacheManager.Single(y => y.Code == GetKeyCodeByFullKeyCode(x.key)),
                                       DocumentUdlType = GetDocumentType(x.key, defaultDocumentType)
                                     })
               .ToList();
    }

    /// <summary>
    /// Расчитывает и сохраняет пользовательские ключи для заявления
    /// </summary>
    /// <param name="keyTypeList">
    /// The key Type List.
    /// </param>
    /// <param name="pd">
    /// The pd.
    /// </param>
    /// <param name="doc">
    /// The doc.
    /// </param>
    /// <param name="addr1">
    /// The addr 1.
    /// </param>
    /// <param name="addr2">
    /// The addr 2.
    /// </param>
    /// <param name="medicalInsurances">
    /// The medical Insurances.
    /// </param>
    /// <param name="okato">
    /// The okato.
    /// </param>
    /// <returns>
    /// The <see cref="IList{SearchKey}"/>.
    /// </returns>
    public IList<SearchKey> CalculateUserKeys(
      IList<SearchKeyType> keyTypeList,
      InsuredPersonDatum pd,
      Document doc,
      address addr1,
      address addr2,
      IList<MedicalInsurance> medicalInsurances,
      string okato)
    {
      var result = new List<SearchKey>();
      foreach (var kt in keyTypeList)
      {
        // Перекодирование типа ключа
        var keyType = new database.business.model.SearchKeyType
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
        var personData = new database.business.model.InsuredPersonDatum
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
          document = new database.business.model.Document
                     {
                       RowId = doc.Id,
                       DocumentTypeId = doc.DocumentType.Id,
                       Series = doc.Series,
                       Number = doc.Number,
                     };
        }

        // Перекодировка адреса регистрации
        var address1 = new database.business.model.address
                       {
                         RowId = addr1.Id,
                         Street = addr1.Street,
                         House = addr1.House,
                         Room = addr1.Room,
                         Okato = addr1.Okato,
                       };

        // Перекодировка адреса проживания
        database.business.model.address address2 = null;
        if (addr2 != null)
        {
          address2 = new database.business.model.address
                     {
                       RowId = addr2.Id,
                       Street = addr2.Street,
                       House = addr2.House,
                       Room = addr2.Room,
                       Okato = addr2.Okato,
                     };
        }

        // Перекодировка страховок
        foreach (var medicalInsurance in medicalInsurances)
        {
          var insurance = new database.business.model.MedicalInsurance
                          {
                            RowId = medicalInsurance.Id,
                            PolisTypeId = medicalInsurance.PolisType.Id,
                            PolisSeria = medicalInsurance.PolisSeria,
                            PolisNumber = medicalInsurance.PolisNumber,
                          };

          // Расчет ключа
          var keyValue = database.business.ObjectFactory.GetPseudonymizationManager()
                                 .CalculateUserSearchKey(
                                                         keyType,
                                                         personData,
                                                         document,
                                                         address1,
                                                         address2,
                                                         insurance,
                                                         okato);

          var conceptCacheManager = ObjectFactory.GetInstance<IConceptCacheManager>();
          var searchKey = new SearchKey
                          {
                            KeyValue = keyValue,
                            KeyType = kt,
                            DocumentUdlType =
                              doc != null && doc.DocumentType != null
                                ? conceptCacheManager.GetById(doc.DocumentType.Id)
                                : null
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

    #region Methods

    /// <summary>
    /// Возвращает тип документа по строчке, возвращаемой алгоритмом расчета новых ключей
    /// </summary>
    /// <param name="fullKeyCode">
    /// The full Key Code.
    /// </param>
    /// <param name="defaultDocumentType">
    /// The default Document Type.
    /// </param>
    /// <returns>
    /// The <see cref="Concept"/>.
    /// </returns>
    private Concept GetDocumentType(string fullKeyCode, Concept defaultDocumentType)
    {
      // Старый ключ
      var splittedKeyCode = fullKeyCode.Split('.');
      if (splittedKeyCode.Length != 2)
      {
        return defaultDocumentType;
      }

      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      // Стандартный код документа от 1 до 25, Oid='1.2.643.2.40.5.100.203.1'
      var document = conceptManager.SingleOrDefault(x => x.Oid.Id == Oid.ДокументУдл && x.Code == splittedKeyCode[1]);
      if (document != null)
      {
        return document;
      }

      // Форма изготовления полиса Oid='1.2.643.2.40.5.100.86'
      document =
        conceptManager.SingleOrDefault(x => x.Oid.Id == Oid.Формаизготовленияполиса && x.Code == splittedKeyCode[1]);
      if (document != null)
      {
        return document;
      }

      // Коды типа идентификатора Oid='1.2.643.2.40.5.100.203'
      document = conceptManager.SingleOrDefault(x => x.Oid.Id == Oid.TypeIdentificator && x.Code == splittedKeyCode[1]);
      if (document != null)
      {
        return document;
      }

      return defaultDocumentType;
    }

    /// <summary>
    /// Возвращает код типа ключа по строчке, возвращаемой алгоритмом расчета новых ключей
    /// </summary>
    /// <param name="fullKeyCode">
    /// The full Key Code.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetKeyCodeByFullKeyCode(string fullKeyCode)
    {
      var splittedKeyCode = fullKeyCode.Split('.');
      if (splittedKeyCode.Any())
      {
        return splittedKeyCode[0];
      }

      return fullKeyCode;
    }

    #endregion
  }
}