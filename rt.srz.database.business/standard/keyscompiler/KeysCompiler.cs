namespace rt.srz.database.business.standard.keyscompiler
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;
  using System.Security.Cryptography;
  using System.Text;
  using System.Xml;
  using System.Xml.Linq;
  using System.IO;

  using rt.srz.database.business.standard.hasher;
  using rt.srz.database.business.standard.keyscompiler.Fields;

  // получение поля по его имени
  public delegate FieldTypes SearchFieldNameResolver(string fieldName);

  public delegate SearchField PrepareHandler(SearchField field);

  public class KeysCompiler
  {
    const string hashAlgorithmName = "GOST3411";
    const string configFileName = "KeysCompilerConfig.xml";
    const string rules = "TextRules.xml";
    const string formats = "DataFormats.xml";
    const string fieldsSeparator = ";";

    // Dictionary<string, int> policyTypes;

    IHashAlgorithm hashAlgorithm;
    List<LoadedKey> loadedKeys;
    List<PreparedKey> preparedKeys;
    Dictionary<string, string> subtypes;

    Rules.TextRules textRules;
    Assumptions assumptions;

    List<FieldTypes> permittedFields;

    List<SearchField> preparedFields;
    List<FIO> preparedInitials;
    List<IDCard> preparedDocuments;

    public static PrepareHandler prepareHandler;

    public void Initialize()
    {
      try
      {
        var asm = Assembly.GetExecutingAssembly();
        hashAlgorithm = new cryptography.HashAlgorithm();

        // Конфиг
        string configXml =
          "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
          "<configuration>" +
            "<keys>" +
              "<key name=\"H11{Doc}\"  type=\"111\" value=\"FamilyName + FirstName + MiddleName + BirthPlace + RealIdentifier\"/>" +
              "<key name=\"H12{Doc}\"  type=\"121\" value=\"FamilyName + FirstName + MiddleName + BirthDate + RealIdentifier\"/>" +
              "<key name=\"H14{Pol}\"  type=\"141\" value=\"FamilyName + FirstName + MiddleName + BirthDate + PolicyNumber\"/>" +
              "<key name=\"H15.PEN\"   type=\"151\" value=\"FirstName + MiddleName + BirthPlace + SNILS\"/>" +
              "<key name=\"H16.PEN\"   type=\"161\" value=\"FirstName + MiddleName + BirthDate + SNILS\"/>" +
              "<key name=\"H17\"       type=\"171\" subtype=\"0\" value=\"FamilyName + FirstName + MiddleName + BirthPlace\"/>" +
              "<key name=\"H18\"       type=\"181\" subtype=\"0\" value=\"FamilyName + FirstName + MiddleName + BirthDate\"/>" +
              "<oldkey name=\"H01\"    type=\"51\"/>" +
              "<oldkey name=\"H02\"    type=\"11\"/>" +
              "<oldkey name=\"H03\"    type=\"21\"/>" +
              "<oldkey name=\"H04\"    type=\"31\"/>" +
              "<oldkey name=\"H05\"    type=\"41\"/>" +
            "</keys>" +
            "<subtypes>" +
              "<subtype name=\"NI\"  value=\"19\"/>" +
              "<subtype name=\"PEN\" value=\"20\"/>" +
              "<subtype name=\"CZ\"  value=\"25\"/>" +
              "<subtype name=\"С\"   value=\"101\"/>" +
              "<subtype name=\"В\"   value=\"105\"/>" +
              "<subtype name=\"П\"   value=\"111\"/>" +
              "<subtype name=\"Э\"   value=\"112\"/>" +
              "<subtype name=\"К\"   value=\"113\"/>" +
            "</subtypes>" +
          "</configuration>";

        using (MemoryStream stream = new MemoryStream())
        {
          StreamWriter writer = new StreamWriter(stream);
          writer.Write(configXml);
          writer.Flush();
          stream.Position = 0;
          loadedKeys = KeysLoader.LoadKeys(stream, out subtypes, new SearchFieldNameResolver(ResolveFields));
        }

        string textRulesXml =
          "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
            "<Правила>" +
              "<Текстовые ВерхнийРегистр=\"Да\" СхлопнутьПробелы=\"Да\" УбратьБоковыеПробелы=\"Да\">" +
                "<Заменить Символы=\"–—?\" На=\"-\" />" +

            "<Заменить Символы=\"A\" На=\"А\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />" +
            "<Заменить Символы=\"B\" На=\"В\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />" +
            "<Заменить Символы=\"C\" На=\"С\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />" +
            "<Заменить Символы=\"E\" На=\"Е\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />" +
            "<Заменить Символы=\"H\" На=\"Н\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />" +
            "<Заменить Символы=\"K\" На=\"К\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />" +
            "<Заменить Символы=\"M\" На=\"М\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />" +
            "<Заменить Символы=\"O\" На=\"О\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />" +
            "<Заменить Символы=\"P\" На=\"Р\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />" +
            "<Заменить Символы=\"T\" На=\"Т\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />" +
            "<Заменить Символы=\"X\" На=\"Х\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />" +
            "<Заменить Символы=\"Ё\" На=\"Е\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" />" +    

            "<Заменить Коды=\"9,10,12,13,160\" На=\" \" ТолькоПоля=\"BirthPlace\" />" +
            "<Заменить Символы=\"_\" На=\" \" ТолькоПоля=\"BirthPlace\" />" +
            "<Заменить Коды=\"34,60,62\" На=\"&apos;\" ТолькоПоля=\"BirthPlace\" />" +
            "<Заменить Символы=\"`„‹‘’“”›«»\" На=\"&apos;\" ТолькоПоля=\"BirthPlace\" />" +
            "<Заменить Символы=\"[{\" На=\"(\" ТолькоПоля=\"BirthPlace\" />" +
            "<Заменить Символы=\"]}\" На=\")\" ТолькоПоля=\"BirthPlace\" />" +
            "<Заменить Символы=\"\\|¦\" На=\"/\" ТолькоПоля=\"BirthPlace\" />" +
            "<Заменить Символы=\"‚\" На=\",\" ТолькоПоля=\"BirthPlace\" />" +
            "<Заменить Символы=\"…\" На=\".\" ТолькоПоля=\"BirthPlace\" />" +
            "<Заменить Символы=\"•\" На=\".\" ТолькоПоля=\"BirthPlace\" />" +
            "<Заменить Символы=\"№\" На=\"N\" ТолькоПоля=\"BirthPlace\" />" +

            "<Заменить Сочетание=\"V\" На=\"У\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />" +
            "<Заменить Сочетание=\"D\" На=\"Д\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />" +
            "<Заменить Сочетание=\"L\" На=\"Л\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />" +
            "<Заменить Сочетание=\"111\" На=\"Ш\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />" +
            "<Заменить Сочетание=\"11\" На=\"П\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />" +
            "<Заменить Сочетание=\"III\" На=\"Ш\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />" +
            "<Заменить Сочетание=\"II\" На=\"П\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />" +
            "<Заменить Сочетание=\"I\" На=\"1\" ТолькоПоля=\"FamilyName, FirstName, MiddleName, BirthPlace\" ТолькоНестрогий=\"Да\" />" +


            "<УбратьПробелы Слева=\"Да\" Справа=\"Да\" Символы=\"-.'\" ТолькоПоля=\"FamilyName, FirstName, MiddleName\" />" +
            "<УбратьПробелы Слева=\"Да\" Справа=\"Да\" Символы=\"-.,\" ТолькоПоля=\"BirthPlace\" />" +
          "</Текстовые>" +
        "</Правила>";

        var xmlReader = new XmlTextReader(new StringReader(textRulesXml));
        textRules = new Rules.TextRules(XElement.Load(xmlReader), ResolveFields);

        //var name = string.Format("{0}.{1}", asm.GetName().Name, formats);
        //var stm = asm.GetManifestResourceStream(name);
        //assumptions = new Assumptions();
        //assumptions.LoadAssumptions(XElement.Load(XmlReader.Create(stm)), clearExistingRules: false);
      }
      catch (Exception ex)
      {
        throw new Exception("Ошибка при инициализации: ", ex);
      }
    }

    public void Clear()
    {
      permittedFields.Clear();
      preparedFields.Clear();
      preparedInitials.Clear();
      preparedDocuments.Clear();
      preparedKeys.Clear();

    }

    public void PrepareFields(List<SearchField> fields, List<FIO> initials = null, List<IDCard> documents = null)
    {
      permittedFields = new List<FieldTypes>();
      preparedKeys = new List<PreparedKey>();
      preparedFields = new List<SearchField>();
      preparedInitials = new List<FIO>();
      preparedDocuments = new List<IDCard>();

      try
      {
        permittedFields = GetPermittedFields();

        if (fields != null)
          foreach (var field in fields)
            if (permittedFields.Contains(field.fieldType) && !string.IsNullOrEmpty(field.value))
            {
              prepareHandler = RetrieveHandler(field.fieldType);
              preparedFields.Add(prepareHandler(field));
            }

        if (initials != null)
          foreach (var fio in initials)
          {
            FieldTypes type = FieldTypes.FamilyName;
            if (permittedFields.Contains(type) && fio.familyName != null && !string.IsNullOrEmpty(fio.familyName.value))
            {
              prepareHandler = RetrieveHandler(type);
              fio.familyName = prepareHandler(fio.familyName);
            }
            type = FieldTypes.FirstName;
            if (permittedFields.Contains(type) && fio.firstName != null && !string.IsNullOrEmpty(fio.firstName.value))
            {
              prepareHandler = RetrieveHandler(type);
              fio.firstName = prepareHandler(fio.firstName);
            }
            type = FieldTypes.MiddleName;
            if (permittedFields.Contains(type) && fio.middleName != null && !string.IsNullOrEmpty(fio.middleName.value))
            {
              prepareHandler = RetrieveHandler(type);
              fio.middleName = prepareHandler(fio.middleName);
            }
            preparedInitials.Add(fio);
          }

        if (documents != null)
          foreach (var document in documents)
          {
            FieldTypes type = FieldTypes.IdCardType;
            if (permittedFields.Contains(type) && document.idCardType != null && !string.IsNullOrEmpty(document.idCardType.value))
            {
              prepareHandler = RetrieveHandler(type);
              document.idCardType = prepareHandler(document.idCardType);
            }
            type = FieldTypes.IdCardNumber;
            if (permittedFields.Contains(type) && document.idCardNumber != null && !string.IsNullOrEmpty(document.idCardNumber.value))
            {
              prepareHandler = RetrieveHandler(type);
              document.idCardNumber = prepareHandler(document.idCardNumber);
            }
            type = FieldTypes.IdCardDate;
            if (permittedFields.Contains(type) && document.idCardDate != null && !string.IsNullOrEmpty(document.idCardDate.value))
            {
              prepareHandler = RetrieveHandler(type);
              document.idCardDate = prepareHandler(document.idCardDate);
            }
            type = FieldTypes.IdCardDateExp;
            if (permittedFields.Contains(type) && document.idCardDateExp != null && !string.IsNullOrEmpty(document.idCardDateExp.value))
            {
              prepareHandler = RetrieveHandler(type);
              document.idCardDateExp = prepareHandler(document.idCardDateExp);
            }
            type = FieldTypes.IdCardOrg;
            if (permittedFields.Contains(type) && document.idCardOrg != null && !string.IsNullOrEmpty(document.idCardOrg.value))
            {
              prepareHandler = RetrieveHandler(type);
              document.idCardOrg = prepareHandler(document.idCardOrg);
            }
            preparedDocuments.Add(document);
          }
      }
      catch (Exception ex)
      {
        throw new Exception("Ошибка при подготовке полей: ", ex);
      }
    }

    public void BuildKeys()
    {
      try
      {
        foreach (var fio in preparedInitials)
          if ((fio.familyName != null &&
              !string.IsNullOrEmpty(fio.familyName.value)) ||
              (fio.firstName != null &&
              !string.IsNullOrEmpty(fio.firstName.value)) ||
              (fio.middleName != null &&
              !string.IsNullOrEmpty(fio.middleName.value)))
            foreach (var key in loadedKeys)
            {
              if (key.name.Contains("Doc"))
                BuildKeysByDoc(key, fio);
              else
                if (key.name.Contains("Pol"))
                  BuildKeysByPol(key, fio);
                else
                {
                  StringBuilder _preparedString = new StringBuilder();
                  foreach (var keyField in key.formula)
                  {
                    string currentField = GetFieldValue(keyField, fio);
                    if (currentField != null)
                    {
                      _preparedString.Append(currentField);
                      _preparedString.Append(fieldsSeparator);
                      if ((new List<FieldTypes> { FieldTypes.ENP, FieldTypes.SNILS, FieldTypes.UEC, FieldTypes.IdCardNumber, FieldTypes.PolicyNumber }).Contains(keyField))
                        key.idCardNumber = currentField;
                    }
                    else
                    {
                      if (keyField != FieldTypes.FamilyName && keyField != FieldTypes.FirstName && keyField != FieldTypes.MiddleName)
                      {
                        _preparedString = null;
                        break;
                      }
                    }
                  }
                  if (_preparedString != null)
                    preparedKeys.Add(new PreparedKey() { fullName = GetKeyName(key.name), type = key.type, subtype = GetKeySubType(key.name, subtypes, key.subtype.ToString()), preparedString = _preparedString.ToString().Trim(';'), idCardNumber = key.idCardNumber });
                }
            }
      }
      catch (Exception ex)
      {
        throw new Exception("Ошибка при группировании полей в поисковые ключи: ", ex);
      }
    }

    private void BuildKeysByDoc(LoadedKey key, FIO fio)
    {
      StringBuilder _preparedString = new StringBuilder();
      foreach (var document in preparedDocuments)
      {
        if (document.idCardType == null)
          continue;

        foreach (var keyField in key.formula)
        {
          string currentField = GetFieldValue(keyField, fio, document);
          if (currentField != null)
          {
            _preparedString.Append(currentField);
            _preparedString.Append(fieldsSeparator);
          }
          else
            if (keyField != FieldTypes.FamilyName && keyField != FieldTypes.FirstName && keyField != FieldTypes.MiddleName)
              return;
        };
        preparedKeys.Add(new PreparedKey() { fullName = GetKeyName(key.name, document.idCardType.value), type = key.type, subtype = GetKeySubType(key.name, subtypes, document.idCardType.value), preparedString = _preparedString.ToString().Trim(';'), idCardDate = document.idCardDate != null ? document.idCardDate.value : null, idCardDateExp = document.idCardDateExp != null ? document.idCardDateExp.value : null, idCardOrg = document.idCardOrg != null ? document.idCardOrg.value : null, idCardNumber = document.idCardNumber != null ? document.idCardNumber.value : null });
        _preparedString.Remove(0, _preparedString.Length);
      }
      _preparedString.Remove(0, _preparedString.Length);
      foreach (var keyField in key.formula)
      {
        string currentField = GetFieldValue(keyField, fio, realIdentifier: FieldTypes.ENP);
        if (currentField != null)
        {
          _preparedString.Append(currentField);
          _preparedString.Append(fieldsSeparator);
        }
        else
          if (keyField != FieldTypes.FamilyName && keyField != FieldTypes.FirstName && keyField != FieldTypes.MiddleName)
            return;
      };
      preparedKeys.Add(new PreparedKey() { fullName = GetKeyName(key.name, "NI"), type = key.type, subtype = GetKeySubType(key.name, subtypes, "NI"), preparedString = _preparedString.ToString().Trim(';'), idCardNumber = GetFieldValue(FieldTypes.ENP) });
      _preparedString.Remove(0, _preparedString.Length);
      foreach (var keyField in key.formula)
      {
        string currentField = GetFieldValue(keyField, fio, realIdentifier: FieldTypes.SNILS);
        if (currentField != null)
        {
          _preparedString.Append(currentField);
          _preparedString.Append(fieldsSeparator);
        }
        else
          if (keyField != FieldTypes.FamilyName && keyField != FieldTypes.FirstName && keyField != FieldTypes.MiddleName)
            return;
      };
      preparedKeys.Add(new PreparedKey() { fullName = GetKeyName(key.name, "PEN"), type = key.type, subtype = GetKeySubType(key.name, subtypes, "PEN"), preparedString = _preparedString.ToString().Trim(';'), idCardNumber = GetFieldValue(FieldTypes.SNILS) });
      _preparedString.Remove(0, _preparedString.Length);
      foreach (var keyField in key.formula)
      {
        string currentField = GetFieldValue(keyField, fio, realIdentifier: FieldTypes.UEC);
        if (currentField != null)
        {
          _preparedString.Append(currentField);
          _preparedString.Append(fieldsSeparator);
        }
        else
          if (keyField != FieldTypes.FamilyName && keyField != FieldTypes.FirstName && keyField != FieldTypes.MiddleName)
            return;
      };
      preparedKeys.Add(new PreparedKey() { fullName = GetKeyName(key.name, "CZ"), type = key.type, subtype = GetKeySubType(key.name, subtypes, "CZ"), preparedString = _preparedString.ToString().Trim(';'), idCardNumber = GetFieldValue(FieldTypes.UEC) });
    }

    private void BuildKeysByPol(LoadedKey key, FIO fio)
    {
      if (preparedFields.FirstOrDefault(field => field.fieldType == FieldTypes.PolicyType) == null)
        return;
      StringBuilder _preparedString = new StringBuilder();
      foreach (var keyField in key.formula)
      {
        string currentField = GetFieldValue(keyField, fio);
        if (currentField != null)
        {
          _preparedString.Append(currentField);
          _preparedString.Append(fieldsSeparator);
        }
        else
          if (keyField != FieldTypes.FamilyName && keyField != FieldTypes.FirstName && keyField != FieldTypes.MiddleName)
            return;
      };
      preparedKeys.Add(new PreparedKey() { fullName = GetKeyName(key.name), type = key.type, subtype = GetKeySubType(key.name, subtypes, preparedFields.FirstOrDefault(field => field.fieldType == FieldTypes.PolicyType).value), preparedString = _preparedString.ToString().Trim(';'), idCardNumber = GetFieldValue(FieldTypes.PolicyNumber), idCardDate = GetFieldValue(FieldTypes.PolicyActualFrom), idCardDateExp = GetFieldValue(FieldTypes.PolicyActualTo) });
    }


    private string GetFieldValue(FieldTypes type, FIO fio = null, IDCard document = null, FieldTypes realIdentifier = FieldTypes.Undefined)
    {
      string value = null;
      switch (type)
      {
        case FieldTypes.FamilyName:
          if (fio != null && fio.familyName != null)
            value = fio.familyName.value;
          break;
        case FieldTypes.FirstName:
          if (fio != null && fio.firstName != null)
            value = fio.firstName.value;
          break;
        case FieldTypes.MiddleName:
          if (fio != null && fio.middleName != null)
            value = fio.middleName.value;
          break;
        case FieldTypes.BirthPlace:
        case FieldTypes.BirthDate:
        case FieldTypes.ENP:
        case FieldTypes.PolicyType:
        case FieldTypes.PolicyNumber:
        case FieldTypes.PolicyActualFrom:
        case FieldTypes.PolicyActualTo:
        case FieldTypes.SNILS:
        case FieldTypes.InsuranceTerritory:
          SearchField field = preparedFields.FirstOrDefault(_field => _field.fieldType == type);
          if (field != null)
            value = field.value;
          break;
        case FieldTypes.RealIdentifier:
          if (realIdentifier == FieldTypes.Undefined)
          {
            if (document != null && document.idCardNumber != null)
              value = document.idCardNumber.value;
          }
          else
            if (realIdentifier == FieldTypes.SNILS)
              return GetFieldValue(FieldTypes.SNILS);
            else
              if (realIdentifier == FieldTypes.ENP)
                return GetFieldValue(FieldTypes.ENP);

          break;

        default:
          return null;
      }
      return value;
    }

    private string GetKeyName(string keyShortName, string keyType = null)
    {
      string _keyType = keyType;

      if (keyType != null)
        switch (keyType)
        {
          case "19":
          case "NI":
            _keyType = "NI";
            break;
          case "20":
          case "PEN":
            _keyType = "PEN";
            break;
          case "25":
          case "CZ":
            _keyType = "CZ";
            break;
        }

      if (keyShortName.Contains("{Doc}"))
        return keyShortName.Replace("{Doc}", "." + _keyType);
      else
        if (keyShortName.Contains("{Pol}"))
          return keyShortName.Replace("{Pol}", "." + (preparedFields.FirstOrDefault(field => field.fieldType == FieldTypes.PolicyType)).value);
        else
          return keyShortName;
    }

    private int GetKeySubType(string keyShortName, Dictionary<string, string> subTypes, string keySubType = null)
    {
      int subtype = 0;
      if (keySubType != null)
      {
        if (subtypes.ContainsKey(keySubType))
          int.TryParse(subtypes[keySubType], out subtype);
        else if (keyShortName.Contains("."))
        {
          string _subtypeName = keyShortName.Substring(keyShortName.LastIndexOf(".") + 1, keyShortName.Length - keyShortName.LastIndexOf(".") - 1);
          if (subtypes.ContainsKey(_subtypeName))
            int.TryParse(subtypes[_subtypeName], out subtype);
        }
        else
          int.TryParse(keySubType, out subtype);
      }

      return subtype;
    }



    private PrepareHandler RetrieveHandler(FieldTypes fieldType)
    {
      switch (fieldType)
      {
        case FieldTypes.BirthDate:
          return PrepareDate;
        case FieldTypes.FamilyName:
        case FieldTypes.FirstName:
        case FieldTypes.MiddleName:
          return PrepareText;
        case FieldTypes.BirthPlace:
        case FieldTypes.ENP:
        case FieldTypes.PolicyType:
        case FieldTypes.PolicyNumber:
        case FieldTypes.IdCardNumber:
          return PrepareText;
        case FieldTypes.SNILS:
          return PrepareSNILS;
        case FieldTypes.IdCardType:
        case FieldTypes.InsuranceTerritory:
        default:
          return PrepareDefault;
      }
    }

    private List<FieldTypes> GetPermittedFields()
    {
      List<FieldTypes> _permittedFields = new List<FieldTypes>();
      foreach (var key in loadedKeys)
        foreach (var field in key.formula)
          if (field == FieldTypes.RealIdentifier)
          {
            if (!_permittedFields.Contains(FieldTypes.SNILS))
              _permittedFields.Add(FieldTypes.SNILS);
            if (!_permittedFields.Contains(FieldTypes.ENP))
              _permittedFields.Add(FieldTypes.ENP);
            if (!_permittedFields.Contains(FieldTypes.IdCardNumber))
              _permittedFields.Add(FieldTypes.IdCardNumber);

          }
          else
            if (field == FieldTypes.PolicyNumber)
            {
              if (!_permittedFields.Contains(field))
                _permittedFields.Add(field);
              if (!_permittedFields.Contains(FieldTypes.PolicyType))
                _permittedFields.Add(FieldTypes.PolicyType);
              if (!_permittedFields.Contains(FieldTypes.PolicyActualFrom))
                _permittedFields.Add(FieldTypes.PolicyActualFrom);
              if (!_permittedFields.Contains(FieldTypes.PolicyActualTo))
                _permittedFields.Add(FieldTypes.PolicyActualTo);
            }
            else
            {
              if (!_permittedFields.Contains(field))
                _permittedFields.Add(field);
            }
      return _permittedFields;
    }

    private SearchField PrepareText(SearchField field)
    {
      if (field != null && field.value != null)
      {
        field.value = ReplaceSymbols(field.value);
        field.value = textRules.PrepareString(field.value, field.fieldType, flagged: false, emptyToNull: true);
      }
      return field;
    }

    private SearchField PrepareDate(SearchField field)
    {
      field.value = ReplaceSymbols(field.value);
      return field;
    }

    private SearchField PrepareDefault(SearchField field)
    {
      field.value = ReplaceSymbols(field.value);
      return field;
    }

    private SearchField PrepareSNILS(SearchField field)
    {
      if (field != null && field.value != null)
      {
        field.value = ReplaceSymbols(field.value);
        char[] delSymbols = new char[] { (char)32, (char)45, (char)95, (char)46, (char)150, (char)151, (char)160 };
        foreach (var symbol in delSymbols)
          while (field.value.IndexOf(symbol) != -1)
            field.value = field.value.Remove(field.value.IndexOf(symbol), 1);
      }
      return field;
    }

    private string ReplaceSymbols(string value)
    {
      return value.Replace(((char)92).ToString(), ((char)92).ToString() + ((char)92).ToString()).Replace(((char)34).ToString(), ((char)92).ToString() + ((char)34).ToString()).Replace(((char)59).ToString(), ((char)92).ToString() + ((char)59).ToString());
    }


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
        case "IdCardDateExp":
          return FieldTypes.IdCardDateExp;
        case "IdCardOrg":
          return FieldTypes.IdCardOrg;
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
        case "DeathMark":
          return FieldTypes.DeathMark;
        case "DeathDate":
          return FieldTypes.DeathDate;
        case "RegistrationTerritory":
          return FieldTypes.RegistrationTerritory;
        /*
        case FieldTypes.OrganizationType:
            return writer_OrganizationType;
        case FieldTypes.PvpId:
            return writer_PvpId;
        case FieldTypes.InsuranceTempNumber:
            return writer_InsuranceTempNumber;
        case FieldTypes.Region:
            return writer_Region;
        */
        case "PolicyType":
          return FieldTypes.PolicyType;
        case "InsuranceCardID":
          return FieldTypes.InsuranceCardID;
        case "InsuranceCompanyCoding":
          return FieldTypes.InsuranceCompanyCoding;

        case "RealIdentifier":
          return FieldTypes.RealIdentifier;
        /*
        case FieldTypes.Citizenry:
            return writer_Citizenry;
        case FieldTypes.Citizenry_OKIN_2:
            return writer_Citizenry_OKIN_2;
        case FieldTypes.IdCardType_IdCardNumber:
            return writer_IdCardType_IdCardNumber;
        case FieldTypes.PolicyType_PolicyNumber:
            return writer_PolicyType_PolicyNumber;
        case FieldTypes.InsuranceCompanyCode_InsuranceCompanyCoding:
            return writer_InsuranceCompanyCode_InsuranceCompanyCoding;
        */
      }
      return FieldTypes.Undefined;
    }

    public List<HashData> CalculateHashes()
    {
      List<HashData> hashes = new List<HashData>();
      try
      {
        HashData hash;
        foreach (var preparedKey in preparedKeys)
        {
          hash = new HashData() { key = preparedKey.fullName, type = preparedKey.type, subtype = preparedKey.subtype, hash = Hasher.CalculateHashFromString(preparedKey.preparedString, hashAlgorithm), idCardDate = preparedKey.idCardDate, idCardDateExp = preparedKey.idCardDateExp, idCardOrg = preparedKey.idCardOrg, idCardNumber = preparedKey.idCardNumber };
          if (hashes.FirstOrDefault(_hash => _hash.type == hash.type && _hash.subtype == hash.subtype && _hash.hash == hash.hash) == null)
            hashes.Add(hash);
        }
        return hashes;
      }
      catch (Exception ex)
      {
        throw new Exception("Ошибка при вычислении хэша из подготовленной строки: ", ex);
      }
    }
  }
}
