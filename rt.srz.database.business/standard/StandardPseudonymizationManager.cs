// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardPseudonymizationManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The pseudonymization service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Xml;
  using System.Xml.Linq;

  using rt.srz.database.business.cryptography;
  using rt.srz.database.business.model;
  using rt.srz.database.business.standard.enums;
  using rt.srz.database.business.standard.helpers;
  using rt.srz.database.business.standard.keyscompiler;
  using rt.srz.database.business.standard.keyscompiler.Fields;
  using rt.srz.database.business.standard.stream;

  // --------------------------------------------------------

  /// <summary>
  ///   The pseudonymization service.
  /// </summary>
  public class StandardPseudonymizationManager
  {
    #region Fields

    /// <summary>
    ///   The id type_ enp.
    /// </summary>
    private readonly string IdType_ENP = "19";

    /// <summary>
    ///   The common pattern dictionary.
    /// </summary>
    private readonly Dictionary<FieldTypes, string> commonPatternDictionary = new Dictionary<FieldTypes, string>();

    /// <summary>
    ///   The document pattern dictionary.
    /// </summary>
    private readonly Dictionary<int, string> documentPatternDictionary = new Dictionary<int, string>();

    /// <summary>
    ///   The keys.
    /// </summary>
    private readonly Key[] keys =
    {
      new Key
      {
        number = "H01", 
        formula =
          new[]
          {
            FieldTypes.FamilyName, FieldTypes.FirstName, FieldTypes.MiddleName, 
            FieldTypes.BirthPlace, FieldTypes.IdCardType, FieldTypes.IdCardNumber
          }
      }, 
      new Key
      {
        number = "H02", 
        formula =
          new[]
          {
            FieldTypes.FamilyName, FieldTypes.FirstName, FieldTypes.MiddleName, 
            FieldTypes.BirthDate, FieldTypes.IdCardType, FieldTypes.IdCardNumber
          }
      }, 
      new Key
      {
        number = "H03", 
        formula =
          new[]
          {
            FieldTypes.FamilyName, FieldTypes.FirstName, FieldTypes.MiddleName, 
            FieldTypes.BirthDate, FieldTypes.SNILS
          }
      }, 
      new Key
      {
        number = "H04", 
        formula =
          new[]
          {
            FieldTypes.FamilyName, FieldTypes.FirstName, FieldTypes.MiddleName, 
            FieldTypes.BirthDate, FieldTypes.InsuranceTerritory, 
            FieldTypes.PolicyType, FieldTypes.PolicyNumber
          }
      }, 
      new Key
      {
        number = "H05", 
        formula =
          new[]
          {
            FieldTypes.FirstName, FieldTypes.MiddleName, FieldTypes.BirthDate, 
            FieldTypes.BirthPlace, FieldTypes.SNILS
          }
      }
    };

    /// <summary>
    ///   The compile handler.
    /// </summary>
    private VoidHandler compileHandler;

    /// <summary>
    ///   The text rules.
    /// </summary>
    private TextRules textRules;

    #endregion

    #region Delegates

    /// <summary>
    ///   The void handler.
    /// </summary>
    /// <param name="value"> The value. </param>
    private delegate void VoidHandler(BinaryWriter writer, string value);

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The convert fields.
    /// </summary>
    /// <param name="variations">
    /// The variations.
    /// </param>
    /// <param name="initials">
    /// The initials.
    /// </param>
    /// <param name="documents">
    /// The documents.
    /// </param>
    /// <param name="newVariations">
    /// The new variations.
    /// </param>
    /// <param name="newInitials">
    /// The new initials.
    /// </param>
    /// <param name="newDocuments">
    /// The new documents.
    /// </param>
    public static void ConvertFields(
      List<FieldVariation> variations, 
      List<FIO> initials, 
      List<Document> documents, 
      out List<SearchField> newVariations, 
      out List<keyscompiler.Fields.FIO> newInitials, 
      out List<IDCard> newDocuments)
    {
      newVariations = new List<SearchField>();
      newInitials = new List<keyscompiler.Fields.FIO>();
      newDocuments = new List<IDCard>();

      foreach (var _variation in variations)
      {
        newVariations.Add(
                          new SearchField
                          {
                            fieldType = (keyscompiler.Fields.FieldTypes)((int)_variation.fieldType) - 1, 
                            value = _variation.value
                          });
      }

      foreach (var _initial in initials)
      {
        newInitials.Add(
                        new keyscompiler.Fields.FIO
                        {
                          familyName =
                            _initial.familyName != null
                              ? new SearchField
                                {
                                  fieldType =
                                    keyscompiler.Fields.FieldTypes
                                                .FamilyName, 
                                  value = _initial.familyName.value
                                }
                              : null, 
                          firstName =
                            _initial.firstName != null
                              ? new SearchField
                                {
                                  fieldType =
                                    keyscompiler.Fields.FieldTypes.FirstName, 
                                  value = _initial.firstName.value
                                }
                              : null, 
                          middleName =
                            _initial.middleName != null
                              ? new SearchField
                                {
                                  fieldType =
                                    keyscompiler.Fields.FieldTypes
                                                .MiddleName, 
                                  value = _initial.middleName.value
                                }
                              : null
                        });
      }

      foreach (var _document in documents)
      {
        newDocuments.Add(
                         new IDCard
                         {
                           idCardType =
                             _document.idCardType != null
                               ? new SearchField
                                 {
                                   fieldType = keyscompiler.Fields.FieldTypes.IdCardType, 
                                   value = _document.idCardType.value
                                 }
                               : null, 
                           idCardNumber =
                             _document.idCardNumber != null
                               ? new SearchField
                                 {
                                   fieldType = keyscompiler.Fields.FieldTypes.IdCardNumber, 
                                   value = _document.idCardNumber.value
                                 }
                               : null, 
                           
                           ////idCardDate = _document.idCardDate != null ? new GISOMS.KEYSCOMPILER.Fields.SearchField() { fieldType = GISOMS.KEYSCOMPILER.Fields.FieldTypes.IdCardDate, value = _document.idCardDate.value } : null,
                           ////idCardDateExp = _document.idCardDateExp != null ? new GISOMS.KEYSCOMPILER.Fields.SearchField() { fieldType = GISOMS.KEYSCOMPILER.Fields.FieldTypes.IdCardDateExp, value = _document.idCardDateExp.value } : null,
                           ////idCardOrg = _document.idCardOrg != null ? new GISOMS.KEYSCOMPILER.Fields.SearchField() { fieldType = GISOMS.KEYSCOMPILER.Fields.FieldTypes.IdCardOrg, value = _document.idCardOrg.value } : null
                         });
      }
    }

    /// <summary>
    /// Расчитывает ключ для указанных параметров
    /// </summary>
    /// <param name="statementXml">
    /// </param>
    /// <param name="insuredPersonDataXml">
    /// </param>
    /// <param name="documentXml">
    /// </param>
    /// <param name="okato">
    /// </param>
    /// <returns>
    /// The <see cref="List"/>.
    /// </returns>
    public List<HashDataNew> CalculateHashes(
      string statementXml, 
      string insuredPersonDataXml, 
      string documentXml, 
      string okato)
    {
      // Парсинг
      ModelAdapter model = null;
      try
      {
        model = new ModelAdapter
                {
                  Statement = Statement.FromXML(statementXml), 
                  PersonData = InsuredPersonDatum.FromXML(insuredPersonDataXml), 
                  Document = business.model.Document.FromXML(documentXml), 
                  Okato = okato
                };
      }
      catch (Exception ex)
      {
        throw new Exception("Ошибка парсинга xml", ex);
      }

      return CalculateHashes(model);
    }

    /// <summary>
    /// Расчитывает ключ для указанных параметров
    /// </summary>
    /// <param name="exchangeXml">
    /// </param>
    /// <param name="documentXml">
    /// The document Xml.
    /// </param>
    /// <param name="addressXml">
    /// The address Xml.
    /// </param>
    /// <returns>
    /// The <see cref="List"/>.
    /// </returns>
    public List<HashDataNew> CalculateHashes(string exchangeXml, string documentXml, string addressXml)
    {
      // Парсинг
      ModelAdapter model = null;
      try
      {
        model = new ModelAdapter
                {
                  Statement = new Statement(), 
                  PersonData = InsuredPersonDatum.FromXML(exchangeXml), 
                  Document = business.model.Document.FromXML(documentXml), 
                  Okato = string.Empty
                };
      }
      catch (Exception ex)
      {
        throw new Exception("Ошибка парсинга xml", ex);
      }

      return CalculateHashes(model);
    }

    /// <summary>
    /// </summary>
    /// <param name="model">
    /// </param>
    /// <returns>
    /// The <see cref="List"/>.
    /// </returns>
    public List<HashDataNew> CalculateHashes(ModelAdapter model)
    {
      // ФИО
      var initialsList = new List<FIO>();
      var fio = new FIO
                {
                  familyName =
                    new FieldVariation { fieldType = FieldTypes.FamilyName, value = model.PersonData.LastName, }, 
                  firstName =
                    new FieldVariation { fieldType = FieldTypes.FirstName, value = model.PersonData.FirstName, }, 
                  middleName =
                    new FieldVariation
                    {
                      fieldType = FieldTypes.MiddleName, 
                      value =
                        !string.IsNullOrEmpty(model.PersonData.MiddleName)
                          ? model.PersonData.MiddleName
                          : null
                    }
                };
      initialsList.Add(fio);

      // Основной документ УДЛ
      var idDocument = string.IsNullOrEmpty(model.Document.Series)
                         ? string.Format("{0}", model.Document.Number)
                         : string.Format("{0} № {1}", model.Document.Series, model.Document.Number);
      var docList = new List<Document>();
      var document = new Document
                     {
                       idCardNumber =
                         new FieldVariation { value = idDocument, fieldType = FieldTypes.IdCardNumber }, 
                       idCardType =
                         new FieldVariation
                         {
                           value = model.Document.DocumentTypeId.Value.ToString(), 
                           fieldType = FieldTypes.IdCardType
                         }
                     };
      docList.Add(document);

      var variations = new List<FieldVariation>();

      // Дата рождения
      if (model.PersonData.Birthday != null)
      {
        variations.Add(
                       new FieldVariation
                       {
                         fieldType = FieldTypes.BirthDate, 
                         value = model.PersonData.Birthday.Value.ToString("yyyyMMdd")
                       });
      }

      // Место рождения
      if (!string.IsNullOrEmpty(model.PersonData.Birthplace))
      {
        variations.Add(new FieldVariation { fieldType = FieldTypes.BirthPlace, value = model.PersonData.Birthplace });
      }

      // Территория страхования
      if (!string.IsNullOrEmpty(model.Okato))
      {
        variations.Add(new FieldVariation { fieldType = FieldTypes.InsuranceTerritory, value = model.Okato });
      }

      // Тип полиса
      if (model.Statement.AbsentPrevPolicy.HasValue && model.Statement.AbsentPrevPolicy.Value)
      {
        if (!string.IsNullOrEmpty(model.Statement.NumberTemporaryCertificate))
        {
          variations.Add(new FieldVariation { fieldType = FieldTypes.PolicyType, value = "В" });
          variations.Add(
                         new FieldVariation
                         {
                           fieldType = FieldTypes.PolicyNumber, 
                           value = model.Statement.NumberTemporaryCertificate
                         });
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(model.Statement.NumberPolicy))
        {
          variations.Add(new FieldVariation { fieldType = FieldTypes.PolicyType, value = "П" });
          variations.Add(
                         new FieldVariation
                         {
                           fieldType = FieldTypes.PolicyNumber, 
                           value = model.Statement.NumberPolicy
                         });
        }
      }

      // СНИЛС
      if (!string.IsNullOrEmpty(model.PersonData.Snils))
      {
        variations.Add(new FieldVariation { fieldType = FieldTypes.SNILS, value = model.PersonData.Snils });
      }

      // ЕНП
      if (!string.IsNullOrEmpty(model.Statement.NumberPolicy))
      {
        variations.Add(new FieldVariation { fieldType = FieldTypes.ENP, value = model.Statement.NumberPolicy });
      }

      // Считаем старые ключи
      var oldKeys = CalculateHashes(variations, initialsList, docList);

      // Считаем новые ключи
      var newKeys = CalculateNewKeys(variations, initialsList, docList);

      // Объединям два набора ключей
      newKeys.AddRange(oldKeys.Select(x => new HashDataNew { key = x.key, hash = x.hash }));

      // Проставялем id Документов
      foreach (var hashData in newKeys)
      {
        if (hashData.subtype == 0)
        {
          hashData.subtype = model.Document.DocumentTypeId.HasValue ? model.Document.DocumentTypeId.Value : 0;
        }
      }

      return newKeys;
    }

    /// <summary>
    /// The calculate hashes.
    /// </summary>
    /// <param name="variations">
    /// The variations.
    /// </param>
    /// <param name="initials">
    /// The initials.
    /// </param>
    /// <param name="documents">
    /// The documents.
    /// </param>
    /// <returns>
    /// The <see cref="List"/> .
    /// </returns>
    public List<HashData> CalculateHashes(List<FieldVariation> variations, List<FIO> initials, List<Document> documents)
    {
      var badDocument = false;
      try
      {
        ValidateFields(variations, initials, documents);
      }
      catch (DocumentPatternException)
      {
        badDocument = true;
      }

      var hashes = new List<HashData>();
      var hashAlgorithm = new HashAlgorithm();

      for (var i = 0; i < 2; i++)
      {
        var looseAlgorithm = i != 0;
        foreach (var fio in initials)
        {
          foreach (var key in keys)
          {
            try
            {
              if ((key.number == "H01" || key.number == "H02") && !badDocument)
              {
                foreach (var idCard in documents)
                {
                  CalculateHash(key, variations, fio, idCard, false, hashAlgorithm, ref hashes, looseAlgorithm);
                }

                if (variations.FirstOrDefault(variation => variation.fieldType == FieldTypes.ENP) != null)
                {
                  CalculateHash(key, variations, fio, null, true, hashAlgorithm, ref hashes, looseAlgorithm);
                }
              }
              else
              {
                CalculateHash(key, variations, fio, null, false, hashAlgorithm, ref hashes, looseAlgorithm);
              }
            }
            catch (Exception ex)
            {
              throw new EntryPointNotFoundException(
                string.Format("Calculate hash with key '{0}' is interrupted.", key.number), 
                ex);
            }
          }
        }
      }

      ////Log.Typed(this).Debug("PseudonymizationManager::CalculateHashes(return {0} hashes)", hashes.Count);
      return hashes;
    }

    /// <summary>
    /// The calculate new keys.
    /// </summary>
    /// <param name="variations">
    /// The variations.
    /// </param>
    /// <param name="initials">
    /// The initials.
    /// </param>
    /// <param name="documents">
    /// The documents.
    /// </param>
    /// <returns>
    /// The <see cref="List"/>.
    /// </returns>
    public List<HashDataNew> CalculateNewKeys(
      List<FieldVariation> variations, 
      List<FIO> initials, 
      List<Document> documents)
    {
      var badDocument = false;
      try
      {
        ValidateFields(variations, initials, documents);
      }
      catch (DocumentPatternException)
      {
        badDocument = true;
      }

      var keys = new List<HashDataNew>();
      var compiler = new KeysCompiler();

      List<SearchField> newVariations;
      List<keyscompiler.Fields.FIO> newInitials;
      List<IDCard> newDocuments;
      ConvertFields(
                    variations, 
                    initials, 
                    badDocument ? new List<Document>() : documents, 
                    out newVariations, 
                    out newInitials, 
                    out newDocuments);

      compiler.Initialize();
      compiler.PrepareFields(newVariations, newInitials, newDocuments);
      compiler.BuildKeys();
      foreach (var key in compiler.CalculateHashes())
      {
        keys.Add(new HashDataNew { hash = key.hash, key = key.key, subtype = key.subtype, type = key.type });
      }

      return keys;
    }

    /// <summary>
    /// The check hashes count.
    /// </summary>
    /// <param name="variations">
    /// The variations.
    /// </param>
    /// <param name="initials">
    /// The initials.
    /// </param>
    /// <param name="documents">
    /// The documents.
    /// </param>
    /// <returns>
    /// The <see cref="List"/> .
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// </exception>
    public List<HashData> CheckHashesCount(List<FieldVariation> variations, FIO initials, List<Document> documents)
    {
      var hashes = new List<HashData>();

      var isCheckHashesCount = true;
      var isHash = new[] { false, false, false, false, false };
      var hashCount = new[] { 0, 0, 0, 0, 0 };

      FieldTypes[] pattern =
      {
        FieldTypes.FamilyName, FieldTypes.FirstName, FieldTypes.MiddleName, FieldTypes.BirthDate, 
        FieldTypes.BirthPlace, FieldTypes.IdCardType, FieldTypes.IdCardNumber, FieldTypes.SNILS, 
        FieldTypes.InsuranceTerritory, FieldTypes.PolicyType, FieldTypes.PolicyNumber, 
        FieldTypes.ENP
      };

      var dictionary = new Dictionary<FieldTypes, bool>();

      foreach (var field in pattern)
      {
        if (initials == null || variations == null || variations.Count == 0)
        {
          isCheckHashesCount = false;
          break;
        }

        switch (field)
        {
          case FieldTypes.FamilyName:
            if (initials.familyName != null && !string.IsNullOrEmpty(initials.familyName.value))
            {
              dictionary.Add(field, true);
            }
            else
            {
              dictionary.Add(field, false);
            }

            break;
          case FieldTypes.FirstName:
            if (initials.firstName != null && !string.IsNullOrEmpty(initials.firstName.value))
            {
              dictionary.Add(field, true);
            }
            else
            {
              dictionary.Add(field, false);
            }

            break;
          case FieldTypes.MiddleName:
            if (initials.middleName != null && !string.IsNullOrEmpty(initials.middleName.value))
            {
              dictionary.Add(field, true);
            }
            else
            {
              dictionary.Add(field, false);
            }

            break;
          case FieldTypes.BirthDate:
          case FieldTypes.BirthPlace:
          case FieldTypes.SNILS:
          case FieldTypes.InsuranceTerritory:
          case FieldTypes.PolicyType:
          case FieldTypes.PolicyNumber:
          case FieldTypes.ENP:
            if (variations.FirstOrDefault(variation => variation.fieldType == field) != null)
            {
              dictionary.Add(field, true);
            }
            else
            {
              dictionary.Add(field, false);
            }

            break;
          case FieldTypes.IdCardType:
            if (documents != null && documents.Count != 0)
            {
              if (documents.FirstOrDefault().idCardType != null
                  && !string.IsNullOrEmpty(documents.FirstOrDefault().idCardType.value))
              {
                dictionary.Add(field, true);
              }
            }
            else
            {
              dictionary.Add(field, false);
            }

            break;
          case FieldTypes.IdCardNumber:
            if (documents != null && documents.Count != 0)
            {
              if (documents.FirstOrDefault().idCardNumber != null
                  && !string.IsNullOrEmpty(documents.FirstOrDefault().idCardNumber.value))
              {
                dictionary.Add(field, true);
              }
            }
            else
            {
              dictionary.Add(field, false);
            }

            break;
          default:
            dictionary.Add(field, false);
            break;
        }
      }

      if (isCheckHashesCount)
      {
        isHash[0] = (dictionary[FieldTypes.FamilyName] || dictionary[FieldTypes.FirstName]
                     || dictionary[FieldTypes.MiddleName]) && dictionary[FieldTypes.BirthPlace]
                    && ((dictionary[FieldTypes.IdCardType] && dictionary[FieldTypes.IdCardNumber])
                        || dictionary[FieldTypes.ENP]);
        isHash[1] = (dictionary[FieldTypes.FamilyName] || dictionary[FieldTypes.FirstName]
                     || dictionary[FieldTypes.MiddleName]) && dictionary[FieldTypes.BirthDate]
                    && ((dictionary[FieldTypes.IdCardType] && dictionary[FieldTypes.IdCardNumber])
                        || dictionary[FieldTypes.ENP]);
        isHash[2] = (dictionary[FieldTypes.FamilyName] || dictionary[FieldTypes.FirstName]
                     || dictionary[FieldTypes.MiddleName]) && dictionary[FieldTypes.BirthDate]
                    && dictionary[FieldTypes.SNILS];
        isHash[3] = (dictionary[FieldTypes.FamilyName] || dictionary[FieldTypes.FirstName]
                     || dictionary[FieldTypes.MiddleName]) && dictionary[FieldTypes.BirthDate]
                    && dictionary[FieldTypes.InsuranceTerritory] && dictionary[FieldTypes.PolicyType]
                    && dictionary[FieldTypes.PolicyNumber];
        isHash[4] = (dictionary[FieldTypes.FirstName] || dictionary[FieldTypes.MiddleName])
                    && dictionary[FieldTypes.BirthDate] && dictionary[FieldTypes.BirthPlace]
                    && dictionary[FieldTypes.SNILS];
      }

      for (var i = 0; i < isHash.Length; i++)
      {
        if (isHash[i])
        {
          hashCount[i] = 1;
          if (i == 0 || i == 1)
          {
            if (documents != null && documents.Count != 0)
            {
              hashCount[i] = 1 * documents.Count;
            }

            if (variations.FirstOrDefault(variation => variation.fieldType == FieldTypes.ENP) != null
                && (dictionary[FieldTypes.IdCardType] && dictionary[FieldTypes.IdCardNumber]))
            {
              hashCount[i] += 1;
            }
          }
        }
      }

      for (var i = 0; i < hashCount.Length; i++)
      {
        for (var j = 0; j < hashCount[i]; j++)
        {
          hashes.Add(new HashData { key = "H0" + (i + 1) });
        }
      }

      return hashes;
    }

    /// <summary>
    ///   The initialize pattern dictionaries.
    /// </summary>
    public void Initialize()
    {
      var textRulesXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + "<Правила>"
                         + "<Текстовые ВерхнийРегистр=\"Да\" СхлопнутьПробелы=\"Да\" УбратьБоковыеПробелы=\"Да\">"
                         + "<Заменить Символы=\"–—?\" На=\"-\" />"
                         + "<Заменить Символы=\"A\" На=\"А\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />"
                         + "<Заменить Символы=\"B\" На=\"В\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />"
                         + "<Заменить Символы=\"C\" На=\"С\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />"
                         + "<Заменить Символы=\"E\" На=\"Е\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />"
                         + "<Заменить Символы=\"H\" На=\"Н\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />"
                         + "<Заменить Символы=\"K\" На=\"К\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />"
                         + "<Заменить Символы=\"M\" На=\"М\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />"
                         + "<Заменить Символы=\"O\" На=\"О\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />"
                         + "<Заменить Символы=\"P\" На=\"Р\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />"
                         + "<Заменить Символы=\"T\" На=\"Т\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />"
                         + "<Заменить Символы=\"X\" На=\"Х\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />"
                         + "<Заменить Символы=\"Ё\" На=\"Е\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />"
                         + "<Заменить Коды=\"9,10,12,13,160\" На=\" \" ТолькоПоля=\"BirthPlace\" />"
                         + "<Заменить Символы=\"_\" На=\" \" ТолькоПоля=\"BirthPlace\" />"
                         + "<Заменить Коды=\"34,60,62\" На=\"&apos;\" ТолькоПоля=\"BirthPlace\" />"
                         + "<Заменить Символы=\"`„‹‘’“”›«»\" На=\"&apos;\" ТолькоПоля=\"BirthPlace\" />"
                         + "<Заменить Символы=\"[{\" На=\"(\" ТолькоПоля=\"BirthPlace\" />"
                         + "<Заменить Символы=\"]}\" На=\")\" ТолькоПоля=\"BirthPlace\" />"
                         + "<Заменить Символы=\"\\|¦\" На=\"/\" ТолькоПоля=\"BirthPlace\" />"
                         + "<Заменить Символы=\"‚\" На=\",\" ТолькоПоля=\"BirthPlace\" />"
                         + "<Заменить Символы=\"…\" На=\".\" ТолькоПоля=\"BirthPlace\" />"
                         + "<Заменить Символы=\"•\" На=\".\" ТолькоПоля=\"BirthPlace\" />"
                         + "<Заменить Символы=\"№\" На=\"N\" ТолькоПоля=\"BirthPlace\" />"
                         + "<Заменить Сочетание=\"V\" На=\"У\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />"
                         + "<Заменить Сочетание=\"D\" На=\"Д\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />"
                         + "<Заменить Сочетание=\"L\" На=\"Л\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />"
                         + "<Заменить Сочетание=\"111\" На=\"Ш\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />"
                         + "<Заменить Сочетание=\"11\" На=\"П\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />"
                         + "<Заменить Сочетание=\"III\" На=\"Ш\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />"
                         + "<Заменить Сочетание=\"II\" На=\"П\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />"
                         + "<Заменить Сочетание=\"I\" На=\"1\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />"
                         + "<УбратьПробелы Слева=\"Да\" Справа=\"Да\" Символы=\"-.'\" ТолькоПоля=\"FamilyName, FirstName, MiddleName\" />"
                         + "<УбратьПробелы Слева=\"Да\" Справа=\"Да\" Символы=\"-.,\" ТолькоПоля=\"BirthPlace\" />"
                         + "</Текстовые>" + "</Правила>";

      // var rules = Assembly.GetExecutingAssembly().GetManifestResourceStream("rt.srz.business.database.standard.TextRules.xml");
      var xmlReader = new XmlTextReader(new StringReader(textRulesXml));
      textRules = new TextRules(XElement.Load(xmlReader), ResolveFields);

      commonPatternDictionary.Add(FieldTypes.FamilyName, @"^(?i)[\d. а-яёa-z-']+$");
      commonPatternDictionary.Add(FieldTypes.FirstName, @"^(?i)[\d. а-яёa-z-']+$");
      commonPatternDictionary.Add(FieldTypes.MiddleName, @"^(?i)[\d. а-яёa-z-']+$");
      commonPatternDictionary.Add(FieldTypes.BirthDate, @"^\d{4}(?:0[1-9]|1[012])(?:0[1-9]|[12][0-9]|3[01])?$");
      commonPatternDictionary.Add(
                                  FieldTypes.BirthPlace, 
                                  @"^(?i)[\d,‚.:№  _\-–—/\\\|¦\(\[\{\)\]\}'""`‘“’”<‹«>›»„а-яёa-z-]+$");
      commonPatternDictionary.Add(FieldTypes.SNILS, @"^\d{11}$|^\d{3}[ -]\d{3}[ -]\d{3}[ -]\d{2}$");
      commonPatternDictionary.Add(FieldTypes.ENP, @"^\d{16}$");
      commonPatternDictionary.Add(FieldTypes.PolicyType, @"^[вВпПсСэЭ-]{1}$");
      commonPatternDictionary.Add(FieldTypes.PolicyNumber, @"(?i)^(?:[\d а-яёa-z-]+ № )?[\d а-яёa-z-]+$");
      commonPatternDictionary.Add(
                                  FieldTypes.InsuranceTerritory, 
                                  @"^(?:00000|79000|80000|81000|84000|82000|26000|83000|85000|91000|86000|87000|88000|89000|98000|90000|92000|93000|94000|95000|96000|97000|01000|03000|04000|05000|07000|08000|10000|11000|12000|14000|15000|17000|18000|19000|20000|24000|25000|27000|29000|30000|32000|33000|34000|37000|38000|41000|42000|44000|46000|47000|22000|49000|50000|52000|53000|54000|56000|57000|58000|60000|61000|36000|63000|64000|65000|66000|68000|28000|69000|70000|71000|73000|75000|76000|78000|45000|40000|99000|11100|71100|77000|71140|55000)$");

      documentPatternDictionary.Add(1, @"^[IVXLC]+-[A-ЯЁ]{2,3} № \d{6}$");
      documentPatternDictionary.Add(2, @"(?i)^(?:[\d а-яёa-z-]+ № )?\d{0,11}\d$");
      documentPatternDictionary.Add(3, @"^[IVXLC]+-[A-ЯЁ]{2,3} № \d{6}$");
      documentPatternDictionary.Add(4, @"^[A-ЯЁ]{2} № \d{7}$");
      documentPatternDictionary.Add(5, @"(?i)^(?:[\d а-яёa-z-]+ № )?\d{0,11}\d$");
      documentPatternDictionary.Add(6, @"^[A-ЯЁ]{2} № \d{6}$");
      documentPatternDictionary.Add(7, @"^[A-ЯЁ]{2} № \d{6}\d?$");
      documentPatternDictionary.Add(8, @"^\d{2} № \d{7}$");
      documentPatternDictionary.Add(9, @"(?i)^(?:[\d а-яёa-z-]+ № )?\d{0,11}\d$");
      documentPatternDictionary.Add(10, @"(?i)^(?:[\d а-яёa-z-]+ № )?\d{0,11}\d$");
      documentPatternDictionary.Add(11, @"(?i)^(?:[\d а-яёa-z-]+ № )?\d{0,11}\d$");
      documentPatternDictionary.Add(12, @"(?i)^(?:[\d а-яёa-z-]+ № )?\d{0,11}\d$");
      documentPatternDictionary.Add(13, @"(?i)^(?:[\d а-яёa-z-]+ № )?\d{0,11}\d$");
      documentPatternDictionary.Add(14, @"^\d{2} \d{2} № \d{6}\d?$");
      documentPatternDictionary.Add(15, @"^\d{2} № \d{7}$");
      documentPatternDictionary.Add(16, @"^[A-ЯЁ]{2} № \d{6}\d?$");
      documentPatternDictionary.Add(17, @"^[A-ЯЁ]{2} № \d{6}$");
      documentPatternDictionary.Add(18, @"(?i)^(?:[\d а-яёa-z-]+ № )?\d{0,11}\d$");
      documentPatternDictionary.Add(21, @"(?i)^(?:[\d а-яёa-z-]+ № )?\d{0,11}\d$");
      documentPatternDictionary.Add(22, @"(?i)^(?:[\d а-яёa-z-]+ № )?\d{0,11}\d$");
      documentPatternDictionary.Add(23, @"(?i)^(?:[\d а-яёa-z-]+ № )?\d{0,11}\d$");
      documentPatternDictionary.Add(24, @"(?i)^(?:[\d а-яёa-z-]+ № )?\d{0,11}\d$");
      documentPatternDictionary.Add(25, @"^[\dA-ЯЁ]{2} № \d{7}$");
    }

    #endregion

    #region Methods

    /// <summary>
    /// Этот метод высчитывает значения хэш-функций по формулам. На вход поступают 3 списка. Список типа FIO содержит набор
    ///   инициалов человека(тип FIO содержит поля: фамилия, имя и отчество).
    ///   Список типа Document содержит набор имеющихся документов, кроме ЕНП и СНИЛС(тип Document содержит поля: тип документа
    ///   и номер документа(серия и номер)).
    ///   Список типа FieldVariation содержит все остальные поля(тип FieldVariation содержит поля: тип поля и значение).
    /// </summary>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="variations">
    /// The variations.
    /// </param>
    /// <param name="fio">
    /// The fio.
    /// </param>
    /// <param name="document">
    /// The document.
    /// </param>
    /// <param name="isNeedENP">
    /// The is need enp.
    /// </param>
    /// <param name="hashAlgorithm">
    /// The hash algorithm.
    /// </param>
    /// <param name="hashes">
    /// The hashes.
    /// </param>
    /// <param name="looseAlgorithm">
    /// The loose algorithm.
    /// </param>
    private void CalculateHash(
      Key key, 
      List<FieldVariation> variations, 
      FIO fio, 
      Document document, 
      bool isNeedENP, 
      IHashAlgorithm hashAlgorithm, 
      ref List<HashData> hashes, 
      bool looseAlgorithm)
    {
      var isFamilyName = true;
      var isFirstName = true;
      var isMiddleName = true;
      using (var stream = new MemoryStream())
      {
        using (var currentWriter = new BinaryWriter(stream, Encoding.Unicode))
        {
          foreach (var fieldType in key.formula)
          {
            string fieldValue = null;

            if (fieldType == FieldTypes.IdCardType && isNeedENP)
            {
              fieldValue = IdType_ENP;
            }
            else if (fieldType == FieldTypes.IdCardNumber && isNeedENP)
            {
              fieldValue = variations.First(variation => variation.fieldType == FieldTypes.ENP).value;
            }
            else
            {
              FieldVariation field;
              switch (fieldType)
              {
                case FieldTypes.ENP:
                case FieldTypes.PolicyNumber:
                case FieldTypes.BirthPlace:
                case FieldTypes.PolicyType:
                  field = variations.FirstOrDefault(variation => variation.fieldType == fieldType);
                  if (field != null)
                  {
                    fieldValue = textRules.PrepareString(field.value, fieldType, false, true);
                  }

                  break;
                case FieldTypes.FamilyName:
                  if (fio.familyName != null && fio.familyName.value != null)
                  {
                    fieldValue = textRules.PrepareString(fio.familyName.value, fieldType, false, true);
                  }
                  else
                  {
                    isFamilyName = false;
                  }

                  break;
                case FieldTypes.FirstName:
                  if (fio.firstName != null && fio.firstName.value != null)
                  {
                    fieldValue = textRules.PrepareString(fio.firstName.value, fieldType, false, true);
                  }
                  else
                  {
                    isFirstName = false;
                  }

                  break;
                case FieldTypes.MiddleName:
                  if (fio.middleName != null && fio.middleName.value != null)
                  {
                    fieldValue = textRules.PrepareString(fio.middleName.value, fieldType, false, true);
                  }
                  else
                  {
                    isMiddleName = false;
                  }

                  break;
                case FieldTypes.IdCardType:
                  if (document != null && document.idCardType != null)
                  {
                    fieldValue = textRules.PrepareString(document.idCardType.value, fieldType, false, true);
                  }

                  break;
                case FieldTypes.IdCardNumber:
                  if (document != null && document.idCardNumber != null)
                  {
                    fieldValue = textRules.PrepareString(document.idCardNumber.value, fieldType, false, true);
                  }

                  break;
                case FieldTypes.SNILS:
                  field = variations.FirstOrDefault(variation => variation.fieldType == fieldType);
                  if (field != null)
                  {
                    fieldValue = PrepareSNILS(field.value);
                  }

                  break;
                case FieldTypes.BirthDate:
                  field = variations.FirstOrDefault(variation => variation.fieldType == fieldType);
                  if (field != null)
                  {
                    fieldValue = field.value;
                  }

                  break;
                default:
                  field = variations.FirstOrDefault(variation => variation.fieldType == fieldType);
                  if (field != null)
                  {
                    fieldValue = field.value;
                  }

                  break;
              }
            }

            if (fieldType != FieldTypes.FamilyName && fieldType != FieldTypes.FirstName
                && fieldType != FieldTypes.MiddleName && fieldValue == null)
            {
              var nullData = new HashData();
              nullData.key = key.number;
              if (looseAlgorithm)
              {
                nullData.key = nullData.key.Replace("H", "P");
              }

              nullData.hash = null;
              hashes.Add(nullData);
              return;
            }

            ResetCompileHandler(RetrieveWriteHandler(fieldType, looseAlgorithm));
            compileHandler(currentWriter, fieldValue);
          }

          stream.Flush();
          stream.Position = 0;
          var data = new HashData();
          if (isFamilyName || isFirstName || isMiddleName)
          {
            if (key.number == "H05" && !(isFirstName || isMiddleName))
            {
              data.hash = null;
            }
            else
            {
              data.hash = hashAlgorithm.ComputeHash(stream);
            }
          }
          else
          {
            data.hash = null;
          }

          data.key = key.number;
          if (looseAlgorithm)
          {
            data.key = data.key.Replace("H", "P");
          }

          hashes.Add(data);
        }
      }
    }

    /// <summary>
    /// получить тип документа из строки
    ///   если тип неизвестен, возвращает null
    /// </summary>
    /// <param name="IdCardType">
    /// The id card type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/> .
    /// </returns>
    private string IdCardTypeFromString(string IdCardType)
    {
      if (!string.IsNullOrEmpty(IdCardType))
      {
        // сперва пытаемся преобразовать в число, попутно убирая лидирующий ноль
        byte typeNumber;
        if (byte.TryParse(IdCardType, out typeNumber))
        {
          return typeNumber.ToString();
        }

        // теперь преобразовываем строковые идентификаторы
        switch (IdCardType)
        {
          case "NI":
            return IdType_ENP;
        }
      }

      return null;
    }

    /// <summary>
    /// The insurance type as string.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="InsuranceType?"/> .
    /// </returns>
    private InsuranceType? InsuranceTypeAsString(string type)
    {
      switch (type)
      {
        case "-":
          return InsuranceType.Unknown;
        case "С":
          return InsuranceType.Obsolete;
        case "В":
          return InsuranceType.Temporary;
        case "П":
          return InsuranceType.Effective2011;
        case "Э":
          return InsuranceType.Electronic2011;
        case "У":
          return InsuranceType.UniversalElectronicCard;
      }

      return null;
    }

    /// <summary>
    /// The prepare snils.удаляет из СНИЛС все разделители
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="string"/> .
    /// </returns>
    private string PrepareSNILS(string value)
    {
      var delSymbols = new[] { (char)32, (char)45, (char)46, (char)95, (char)150, (char)151, (char)160 };
      foreach (var symbol in delSymbols)
      {
        while (value.IndexOf(symbol) != -1)
        {
          value = value.Remove(value.IndexOf(symbol), 1);
        }
      }

      return value;
    }

    /// <summary>
    /// The reset compile handler.
    /// </summary>
    /// <param name="handler">
    /// The handler.
    /// </param>
    private void ResetCompileHandler(VoidHandler handler)
    {
      compileHandler = handler;
    }

    /// <summary>
    /// The resolve fields.
    /// </summary>
    /// <param name="fieldName">
    /// The field name.
    /// </param>
    /// <returns>
    /// The <see cref="FieldTypes"/> .
    /// </returns>
    private FieldTypes ResolveFields(string fieldName)
    {
      switch (fieldName)
      {
        case "ENP":
          return FieldTypes.ENP;
        case "PolicyNumber":
          return FieldTypes.PolicyNumber;
        case "FamilyName":
          return FieldTypes.FamilyName;
        case "FirstName":
          return FieldTypes.FirstName;
        case "MiddleName":
          return FieldTypes.MiddleName;
        case "Gender":
          return FieldTypes.Gender;
        case "BirthDate":
          return FieldTypes.BirthDate;
        case "BirthPlace":
          return FieldTypes.BirthPlace;
        case "IdCardType":
          return FieldTypes.IdCardType;
        case "IdCardNumber":
          return FieldTypes.IdCardNumber;
        case "IdCardDate":
          return FieldTypes.IdCardDate;
        case "SNILS":
          return FieldTypes.SNILS;
        case "InsuranceTerritory":
          return FieldTypes.InsuranceTerritory;
        case "InsuranceCompanyCode":
          return FieldTypes.InsuranceCompanyCode;
        case "PolicyActualFrom":
          return FieldTypes.PolicyActualFrom;
        case "PolicyActualTo":
          return FieldTypes.PolicyActualTo;
        case "Citizenry_OKSM_3":
          return FieldTypes.Citizenry_OKSM_3;
        case "DeathMark":
          return FieldTypes.DeathMark;
        case "DeathDate":
          return FieldTypes.DeathDate;
        case "RegistrationTerritory":
          return FieldTypes.RegistrationTerritory;
        case "PolicyType":
          return FieldTypes.PolicyType;
        case "InsuranceCardID":
          return FieldTypes.InsuranceCardID;
        case "InsuranceCompanyCoding":
          return FieldTypes.InsuranceCompanyCoding;
      }

      return FieldTypes.Undefined;
    }

    /// <summary>
    /// The retrieve write handler.
    /// </summary>
    /// <param name="fieldType">
    /// The field type.
    /// </param>
    /// <param name="looseAlgorithm">
    /// The loose algorithm.
    /// </param>
    /// <returns>
    /// The <see cref="VoidHandler"/> .
    /// </returns>
    private VoidHandler RetrieveWriteHandler(FieldTypes fieldType, bool looseAlgorithm)
    {
      if (looseAlgorithm)
      {
        switch (fieldType)
        {
          case FieldTypes.ENP:
            return writer_ENP;
          case FieldTypes.PolicyNumber:
            return writer_PolicyNumber;
          case FieldTypes.FamilyName:
            return writer_loose_FamilyName;
          case FieldTypes.FirstName:
            return writer_loose_FirstName;
          case FieldTypes.MiddleName:
            return writer_loose_MiddleName;
          case FieldTypes.Gender:
            return writer_Gender;
          case FieldTypes.BirthDate:
            return writer_BirthDate;
          case FieldTypes.BirthPlace:
            return writer_loose_BirthPlace;
          case FieldTypes.IdCardType:
            return writer_IdCardType;
          case FieldTypes.IdCardNumber:
            return writer_IdCardNumber;
          case FieldTypes.IdCardDate:
            return writer_IdCardDate;
          case FieldTypes.SNILS:
            return writer_SNILS;
          case FieldTypes.InsuranceTerritory:
            return writer_InsuranceTerritory;
          case FieldTypes.InsuranceCompanyCode:
            return writer_InsuranceCompanyCode;
          case FieldTypes.PolicyActualFrom:
            return writer_PolicyActualFrom;
          case FieldTypes.PolicyActualTo:
            return writer_PolicyActualTo;
          case FieldTypes.Citizenry_OKSM_3:
            return writer_Citizenry_OKSM_3;
          case FieldTypes.DeathMark:
            return writer_DeathMark;
          case FieldTypes.DeathDate:
            return writer_DeathDate;
          case FieldTypes.RegistrationTerritory:
            return writer_RegistrationTerritory;
          case FieldTypes.PolicyType:
            return writer_PolicyType;
          case FieldTypes.InsuranceCardID:
            return writer_InsuranceCardID;
          case FieldTypes.InsuranceCompanyCoding:
            return writer_InsuranceCompanyCoding;
        }
      }
      else
      {
        // -- строгие алгоритмы
        switch (fieldType)
        {
          case FieldTypes.ENP:
            return writer_ENP;
          case FieldTypes.PolicyNumber:
            return writer_PolicyNumber;
          case FieldTypes.FamilyName:
            return writer_FamilyName;
          case FieldTypes.FirstName:
            return writer_FirstName;
          case FieldTypes.MiddleName:
            return writer_MiddleName;
          case FieldTypes.Gender:
            return writer_Gender;
          case FieldTypes.BirthDate:
            return writer_BirthDate;
          case FieldTypes.BirthPlace:
            return writer_BirthPlace;
          case FieldTypes.IdCardType:
            return writer_IdCardType;
          case FieldTypes.IdCardNumber:
            return writer_IdCardNumber;
          case FieldTypes.IdCardDate:
            return writer_IdCardDate;
          case FieldTypes.SNILS:
            return writer_SNILS;
          case FieldTypes.InsuranceTerritory:
            return writer_InsuranceTerritory;
          case FieldTypes.InsuranceCompanyCode:
            return writer_InsuranceCompanyCode;
          case FieldTypes.PolicyActualFrom:
            return writer_PolicyActualFrom;
          case FieldTypes.PolicyActualTo:
            return writer_PolicyActualTo;
          case FieldTypes.Citizenry_OKSM_3:
            return writer_Citizenry_OKSM_3;
          case FieldTypes.DeathMark:
            return writer_DeathMark;
          case FieldTypes.DeathDate:
            return writer_DeathDate;
          case FieldTypes.RegistrationTerritory:
            return writer_RegistrationTerritory;
          case FieldTypes.PolicyType:
            return writer_PolicyType;
          case FieldTypes.InsuranceCardID:
            return writer_InsuranceCardID;
          case FieldTypes.InsuranceCompanyCoding:
            return writer_InsuranceCompanyCoding;
        }
      }

      // нет такого метода
      return null;
    }

    /// <summary>
    /// The validate fields.
    /// </summary>
    /// <param name="variations">
    /// The variations.
    /// </param>
    /// <param name="initials">
    /// The initials.
    /// </param>
    /// <param name="documents">
    /// The documents.
    /// </param>
    /// <exception cref="ArgumentException">
    /// </exception>
    private void ValidateFields(List<FieldVariation> variations, List<FIO> initials, List<Document> documents)
    {
      Match valueMatch;

      // if (variations == null || variations.Count == 0 || initials == null || initials.Count == 0)
      // throw new Exception("Инициалы обязательно должны быть указаны.");
      if (initials != null)
      {
        foreach (var fio in initials)
        {
          if (fio.familyName != null && !string.IsNullOrEmpty(fio.familyName.value))
          {
            valueMatch = Regex.Match(fio.familyName.value, commonPatternDictionary[fio.familyName.fieldType]);
            if (!valueMatch.Success)
            {
              throw new ArgumentException(
                string.Format("Фамилия:'{0}' не удовлетворяет шаблону.", fio.familyName.value));
            }
          }

          if (fio.firstName != null && !string.IsNullOrEmpty(fio.firstName.value))
          {
            valueMatch = Regex.Match(fio.firstName.value, commonPatternDictionary[fio.firstName.fieldType]);
            if (!valueMatch.Success)
            {
              throw new ArgumentException(string.Format("Имя:'{0}' не удовлетворяет шаблону.", fio.firstName.value));
            }
          }

          if (fio.middleName != null && !string.IsNullOrEmpty(fio.middleName.value))
          {
            valueMatch = Regex.Match(fio.middleName.value, commonPatternDictionary[fio.middleName.fieldType]);
            if (!valueMatch.Success)
            {
              throw new ArgumentException(
                string.Format("Отчество:'{0}' не удовлетворяет шаблону.", fio.middleName.value));
            }
          }
        }
      }

      if (variations != null)
      {
        foreach (var variation in variations)
        {
          if (variation != null && !string.IsNullOrEmpty(variation.value)
              && commonPatternDictionary.Keys.Contains(variation.fieldType))
          {
            valueMatch = Regex.Match(variation.value, commonPatternDictionary[variation.fieldType]);
            if (!valueMatch.Success)
            {
              throw new ArgumentException(string.Format("Поле:'{0}' не удовлетворяет шаблону.", variation.fieldType));
            }
          }
        }
      }

      if (documents != null)
      {
        foreach (var document in documents)
        {
          if (document != null)
          {
            if (document.idCardNumber != null && document.idCardType != null
                && !string.IsNullOrEmpty(document.idCardNumber.value)
                && !string.IsNullOrEmpty(document.idCardType.value))
            {
              var pattern = documentPatternDictionary[int.Parse(document.idCardType.value)];
              if (pattern != null)
              {
                valueMatch = Regex.Match(document.idCardNumber.value, pattern);
                if (!valueMatch.Success)
                {
                  throw new DocumentPatternException(
                    string.Format("Номер документа типа:'{0}' не удовлетворяет шаблону.", document.idCardType.value));
                }
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// The writer_ birth date.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_BirthDate(BinaryWriter writer, string value)
    {
      DateTime? birthDate;
      StringExtension.StringAsDate(value, out birthDate);
      DataStreamer.Write(writer, birthDate);
    }

    /// <summary>
    /// The writer_ birth place.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_BirthPlace(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ citizenry_ oks m_3.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_Citizenry_OKSM_3(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ death date.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_DeathDate(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, StringExtension.StringAsDate(value));
    }

    /// <summary>
    /// The writer_ death mark.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_DeathMark(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ enp.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_ENP(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ employment.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_Employment(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ family name.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_FamilyName(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ first name.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_FirstName(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ gender.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_Gender(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ id card date.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_IdCardDate(BinaryWriter writer, string value)
    {
      DateTime? idCardDate;
      StringExtension.StringAsDate(value, out idCardDate);
      DataStreamer.Write(writer, idCardDate);
    }

    /// <summary>
    /// The writer_ id card number.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_IdCardNumber(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ id card type.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_IdCardType(BinaryWriter writer, string value)
    {
      var idCardType = IdCardTypeFromString(value);
      DataStreamer.Write(writer, idCardType);
    }

    /// <summary>
    /// The writer_ insurance card id.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_InsuranceCardID(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ insurance company code.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_InsuranceCompanyCode(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ insurance company coding.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_InsuranceCompanyCoding(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ insurance territory.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_InsuranceTerritory(BinaryWriter writer, string value)
    {
      uint? insuranceTerritory = RegionHelper.StringAsОКАТО(value);
      DataStreamer.Write(writer, insuranceTerritory);
    }

    /// <summary>
    /// The writer_ middle name.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_MiddleName(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ policy actual from.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_PolicyActualFrom(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, StringExtension.StringAsDate(value));
    }

    /// <summary>
    /// The writer_ policy actual to.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_PolicyActualTo(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, StringExtension.StringAsDate(value));
    }

    /// <summary>
    /// The writer_ policy number.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_PolicyNumber(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, value);
    }

    /// <summary>
    /// The writer_ policy type.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_PolicyType(BinaryWriter writer, string value)
    {
      var policyType = (InsuranceType)InsuranceTypeAsString(value);
      DataStreamer.Write(writer, policyType);
    }

    /// <summary>
    /// The writer_ registration territory.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_RegistrationTerritory(BinaryWriter writer, string value)
    {
      uint? registrationTerritory = RegionHelper.StringAsОКАТО(value);
      DataStreamer.Write(writer, registrationTerritory);
    }

    /// <summary>
    /// The writer_ snils.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_SNILS(BinaryWriter writer, string value)
    {
      ulong? SNILS = Convert.ToUInt64(value);
      DataStreamer.Write(writer, SNILS);
    }

    /// <summary>
    /// The writer_loose_ birth place.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_loose_BirthPlace(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, TStringHelper.StringToNull(SoundExTransliteratedRus.GenerateLooseString(value), true));
    }

    /// <summary>
    /// The writer_loose_ family name.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_loose_FamilyName(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, TStringHelper.StringToNull(SoundExTransliteratedRus.GenerateLooseString(value), true));
    }

    /// <summary>
    /// The writer_loose_ first name.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_loose_FirstName(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, TStringHelper.StringToNull(SoundExTransliteratedRus.GenerateLooseString(value), true));
    }

    /// <summary>
    /// The writer_loose_ middle name.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void writer_loose_MiddleName(BinaryWriter writer, string value)
    {
      DataStreamer.Write(writer, TStringHelper.StringToNull(SoundExTransliteratedRus.GenerateLooseString(value), true));
    }

    #endregion
  }
}