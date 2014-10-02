//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace rt.srz.database.business.pseudonymization
{
  using System;
  using System.IO;
  using System.Text;
  using System.Security.Cryptography;
  using System.Linq;
  using System.Linq.Expressions;

  using rt.srz.database.business.interfaces.pseudonymization;
  using rt.srz.database.business.model;

  public class PseudonymizationManager : IPseudonymizationManager
  {
    #region Constants
    /// <summary>
    /// The default policy hasher name.
    /// </summary>
    private const string DefaultPolicyHasherName = "GOST3411";
    #endregion

    #region Fields

    /// <summary>
    /// Писатели полей в поток
    /// </summary>
    private readonly IWriteField[] writeFields;

    #endregion

    #region Constructor

    public PseudonymizationManager(IWriteField[] writeFields)
    {
      this.writeFields = writeFields;
    }

    #endregion

    #region Private Methods
    /// <summary>
    /// Записывает значение поля в поток
    /// </summary>
    /// <param name="fieldValue">Значение поля</fieldValue>
    /// <param name="expression">Выражение</param>
    /// <param name="binaryWriter">Бинарный сериализатор</param>
    private void WriteField(ModelAdapter model, Expression<Func<ModelAdapter, object>> expression, BinaryWriter binaryWriter)
    {
      var wf = writeFields.Where(x => x.Expression != null && x.Expression.ToString() == expression.ToString()).FirstOrDefault();
      if (wf != null)
      {
        //Запись значения
        wf.WriteField(model, binaryWriter);
        return;
      }

      throw new Exception("WriteField, Не найден обработчик для выражения: " + expression.ToString());
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private byte[] CalculateUserSearchKey(ModelAdapter model)
    {
      //создание внешнего hash алгоритма
      var hashAlgorithm = new cryptography.HashAlgorithm();
      
      byte[] hash = null;
      using (var stream = new MemoryStream())
      {
        using (var currentWriter = new BinaryWriter(stream, Encoding.Unicode))
        {
          //Персональные данные
          WriteField(model, x => x.PersonData.LastName, currentWriter);
          WriteField(model, x => x.PersonData.FirstName, currentWriter);
          WriteField(model, x => x.PersonData.MiddleName, currentWriter);
          WriteField(model, x => x.PersonData.Birthday, currentWriter);
          WriteField(model, x => x.PersonData.Birthplace, currentWriter);
          WriteField(model, x => x.PersonData.Snils, currentWriter);

          //Документ УДЛ
          WriteField(model, x => x.Document.DocumentTypeId, currentWriter);
          WriteField(model, x => x.Document.Series, currentWriter);
          WriteField(model, x => x.Document.Number, currentWriter);

          //Медстраховка
          WriteField(model, x => x.MedicalInsurance.PolisTypeId, currentWriter);
          WriteField(model, x => x.MedicalInsurance.PolisSeria, currentWriter);
          WriteField(model, x => x.MedicalInsurance.PolisNumber, currentWriter);

          //Адрес регистрации
          WriteField(model, x => x.Address1.Street, currentWriter);
          WriteField(model, x => x.Address1.House, currentWriter);
          WriteField(model, x => x.Address1.Room, currentWriter);

          //Адрес проживания
          WriteField(model, x => x.Address2.Street, currentWriter);
          WriteField(model, x => x.Address2.House, currentWriter);
          WriteField(model, x => x.Address2.Room, currentWriter);

          //ОКАТО ТФОМС
          WriteField(model, x => x.Okato, currentWriter);

          //расчет ключа с помощью внешнего алгоритма
          stream.Flush();
          stream.Position = 0;
          hash = hashAlgorithm.ComputeHash(stream);
        }
      }

      return hash;
    }
    
    #endregion

    #region IPseudonymizationManager implementation

    /// <summary>
    /// Расчитывает ключ для указанных параметров
    /// </summary>
    /// <param name="searchKeyTypeXml"></param>
    /// <param name="insuredPersonDataXml"></param>
    /// <param name="documentXml"></param>
    /// <param name="address1Xml"></param>
    /// <param name="address2Xml"></param>
    /// <param name="medicalInsuranceXml"></param>
    /// <param name="okato"></param>
    /// <returns></returns>
    public byte[] CalculateUserSearchKey(string searchKeyTypeXml, string insuredPersonDataXml, string documentXml, 
      string address1Xml, string address2Xml, string medicalInsuranceXml, string okato)
    {
      //Парсинг
      ModelAdapter model = null;
      try
      {
        model = new ModelAdapter()
        {
          SearchKeyType = SearchKeyType.FromXML(searchKeyTypeXml),
          PersonData = InsuredPersonDatum.FromXML(insuredPersonDataXml),
          Document = Document.FromXML(documentXml),
          Address1 = address.FromXML(address1Xml),
          Address2 = address.FromXML(address2Xml),
          MedicalInsurance = MedicalInsurance.FromXML(medicalInsuranceXml),
          Okato = okato
        };
      }
      catch (Exception ex)
      {
        throw new Exception("Ошибка парсинга xml", ex);
      }

      return CalculateUserSearchKey(model);  
    }

    /// <summary>
    /// Расчитывает ключ для указанных параметров
    /// </summary>
    /// <param name="keyType"></param>
    /// <param name="personData"></param>
    /// <param name="document"></param>
    /// <param name="address1"></param>
    /// <param name="address2"></param>
    /// <param name="medicalInsurance"></param>
    /// <param name="okato"></param>
    /// <returns></returns>
    public byte[] CalculateUserSearchKey(SearchKeyType keyType, InsuredPersonDatum personData, Document document,
      address address1, address address2, MedicalInsurance medicalInsurance, string okato)
    {
      ModelAdapter model = new ModelAdapter()
      {
        SearchKeyType = keyType,
        PersonData = personData,
        Document = document,
        Address1 = address1,
        Address2 = address2,
        MedicalInsurance = medicalInsurance,
        Okato = okato
      };
      
      return CalculateUserSearchKey(model);  
    }
    
    /// <summary>
    /// Расчитывает ключ для указанных параметров
    /// </summary>
    /// <param name="searchKeyTypeXml"></param>
    /// <param name="pfrExchangeXml"></param>
    /// <returns></returns>
    public byte[] CalculateUserSearchKeyExchange(string searchKeyTypeXml, string exchangeXml, string documentXml, string addressXml)
    {
      //Парсинг
      ModelAdapter model = null;
      try
      {
        model = new ModelAdapter()
        {
          SearchKeyType = SearchKeyType.FromXML(searchKeyTypeXml),
          PersonData = InsuredPersonDatum.FromXML(exchangeXml),
          Document = Document.FromXML(documentXml),
          Address1 = address.FromXML(addressXml)
        };
      }
      catch (Exception ex)
      {
        throw new Exception("Ошибка парсинга xml", ex);
      }

      //создание внешнего hash алгоритма
      var hashAlgorithm = new cryptography.HashAlgorithm();
      
      byte[] hash = null;
      using (var stream = new MemoryStream())
      {
        using (var currentWriter = new BinaryWriter(stream, Encoding.Unicode))
        {
          //Персональные данные
          WriteField(model, x => x.PersonData.LastName, currentWriter);
          WriteField(model, x => x.PersonData.FirstName, currentWriter);
          WriteField(model, x => x.PersonData.MiddleName, currentWriter);
          WriteField(model, x => x.PersonData.Birthday, currentWriter);
          WriteField(model, x => x.PersonData.Birthplace, currentWriter);
          WriteField(model, x => x.PersonData.Snils, currentWriter);

          //Документ УДЛ
          WriteField(model, x => x.Document.DocumentTypeId, currentWriter);
          WriteField(model, x => x.Document.Series, currentWriter);
          WriteField(model, x => x.Document.Number, currentWriter);

          //расчет ключа с помощью внешнего алгоритма
          stream.Flush();
          stream.Position = 0;
          hash = hashAlgorithm.ComputeHash(stream);
        }
      }
      return hash;
    }
    #endregion
  }
}
