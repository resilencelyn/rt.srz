//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.helpers
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Конвертер кода стандартного ключа в id
  /// </summary>
  public static class KeyCode2KeyIdConverter
  {
    /// <summary>
    /// Пары тип ключа - Id ключа
    /// </summary>
    private static readonly IDictionary<string, Guid> key2IdDictionary = new Dictionary<string, Guid>();

    /// <summary>
    /// Пары тип документа - Id документа
    /// </summary>
    private static readonly IDictionary<string, int> document2IdDictionary = new Dictionary<string, int>();

    /// <summary>
    /// Конструктор
    /// </summary>
    static KeyCode2KeyIdConverter()
    {
      key2IdDictionary.Add("H01", new Guid("0ACD5D9D-CC16-4823-979E-DE2983A1DB94"));
      key2IdDictionary.Add("H02", new Guid("75676415-1C0D-4AA9-9C89-C38D2CB47EF4"));
      key2IdDictionary.Add("H03", new Guid("77CEA0E5-5778-4EE7-AC11-50583F00C30D"));
      key2IdDictionary.Add("H04", new Guid("56DAE482-B9F7-4616-AC70-2D1C8BEDD8FF"));
      key2IdDictionary.Add("H05", new Guid("57082AB3-2B72-46E0-866A-237D3485A16C"));
      key2IdDictionary.Add("H11", new Guid("9C371EB8-0549-475E-8458-88C2816F567D"));
      key2IdDictionary.Add("H12", new Guid("CEA228A3-7785-424C-AD5C-D0C80B73B710"));
      key2IdDictionary.Add("H14", new Guid("02A6EFD6-4B1B-49B8-B20B-AAF4AA91B582"));
      key2IdDictionary.Add("H15", new Guid("2F634436-22A3-4450-BACF-920710352CB4"));
      key2IdDictionary.Add("H16", new Guid("CC18C59C-7267-4452-8C76-5015CA387CC8"));
      key2IdDictionary.Add("H17", new Guid("20D7B3DA-A750-4B36-8F0E-8F51A594754A"));
      key2IdDictionary.Add("H18", new Guid("4AC3FDFB-B11D-4306-9856-F56489895867"));
      
      key2IdDictionary.Add("P01", new Guid("678A0ECB-9931-4BCE-87B2-E56F99046463"));
      key2IdDictionary.Add("P02", new Guid("29DE0771-F5DC-4D96-8B09-4FC5C7EE21B7"));
      key2IdDictionary.Add("P03", new Guid("642A6E90-D4DE-4513-A242-640486B61431"));
      key2IdDictionary.Add("P04", new Guid("72FD1E68-F5BE-4BD2-974D-9DF4A804701C"));
      key2IdDictionary.Add("P05", new Guid("62B969C9-C0B2-4C23-9707-0CAF7A9C2324"));
      key2IdDictionary.Add("P11", new Guid("66F21350-4575-49C3-B2AC-AB39004D32E3"));
      key2IdDictionary.Add("P12", new Guid("48BECA1C-36AE-491A-ACFD-B2F486C737C7"));
      key2IdDictionary.Add("P14", new Guid("A29ADB1F-84D1-4104-99DB-CC5C1DBCAD95"));
      key2IdDictionary.Add("P15", new Guid("87970C63-B1FA-4670-BFE8-553712929A85"));
      key2IdDictionary.Add("P16", new Guid("48DF3B58-32F0-490E-B4E7-0B05B7CA44F8"));
      key2IdDictionary.Add("P17", new Guid("033CB982-A22A-4A93-808A-4623E150FCA3"));
      key2IdDictionary.Add("P18", new Guid("D150DF2F-42A6-4D26-81BC-58F5B7DA41AD"));

      document2IdDictionary.Add("1",  1);
      document2IdDictionary.Add("2",  2);
      document2IdDictionary.Add("3",  3);
      document2IdDictionary.Add("4",  4);
      document2IdDictionary.Add("5",  5);
      document2IdDictionary.Add("6",  6);
      document2IdDictionary.Add("7",  7);
      document2IdDictionary.Add("8",  8);
      document2IdDictionary.Add("9",  9);
      document2IdDictionary.Add("10", 10);
      document2IdDictionary.Add("11", 11);
      document2IdDictionary.Add("12", 12);
      document2IdDictionary.Add("13", 13);
      document2IdDictionary.Add("14", 14);
      document2IdDictionary.Add("15", 15);
      document2IdDictionary.Add("16", 16);
      document2IdDictionary.Add("17", 17);
      document2IdDictionary.Add("18", 18);
      document2IdDictionary.Add("20", 391);
      document2IdDictionary.Add("21", 392);
      document2IdDictionary.Add("22", 393);
      document2IdDictionary.Add("23", 394);
      document2IdDictionary.Add("24", 629);
      document2IdDictionary.Add("25", 630);
      
      // Временное свидетельство
      document2IdDictionary.Add("В", 439);

      // Полис ОМС в составе универсальной электронной карты
      document2IdDictionary.Add("К", 322);

      // Бумажный полис ОМС единого образц
      document2IdDictionary.Add("П", 321);

      // Полис ОМС старого образ-ца
      document2IdDictionary.Add("С", 438);

      // Электронный полис ОМС единого образца
      document2IdDictionary.Add("Э", 440);

      // СНИЛС
      document2IdDictionary.Add("PEN", 639);
      
      // ЕНП
      document2IdDictionary.Add("NI", 640);

      // УЭК
      document2IdDictionary.Add("CZ", 641);
    }

    /// <summary>
    /// Преобразует код стандартного ключа в id
    /// </summary>
    /// <param name="keyCode"></param>
    /// <returns></returns>
    public static Guid ConvertKeyCode2KeyId(string keyCode)
    {
      string[] splittedKeyCode = keyCode.Split('.');
      if (splittedKeyCode.Length == 2)
        return key2IdDictionary[splittedKeyCode[0]];

      return key2IdDictionary[keyCode];
    }


    /// <summary>
    /// Преобразует код стандартного ключа нового образца в идентификатор документа
    /// </summary>
    /// <param name="keyCode"></param>
    /// <returns></returns>
    public static int ConvertKeyCode2DocumentId(string keyCode, int subType)
    {
      string[] splittedKeyCode = keyCode.Split('.');
      if (splittedKeyCode.Length == 2)
        return document2IdDictionary[splittedKeyCode[1]];

      return subType;
    }
  }
}
