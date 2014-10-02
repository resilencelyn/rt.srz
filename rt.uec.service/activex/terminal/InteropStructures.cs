// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InteropStructures.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The private data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace rt.uec.service.activex.terminal
{
  #region references

  

  #endregion

  /// <summary>
  /// The private data.
  /// </summary>
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct PrivateData
  {
    /// <summary>
    /// The uec max string length.
    /// </summary>
    private const int UECMaxStringLength = 1024 + 1;

    /// <summary>
    /// The snils.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string SNILS; // СНИЛС

    /// <summary>
    /// The policy number oms.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string PolicyNumberOMS; // Номер полиса ОМС

    /// <summary>
    /// The issuer address.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string IssuerAddress; // Адрес эмитента

    /// <summary>
    /// The birth date.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string BirthDate; // Дата рождения

    /// <summary>
    /// The birth place.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string BirthPlace; // Место рождения

    /// <summary>
    /// The gender.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Gender; // Пол

    /// <summary>
    /// The last name.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string LastName; // Фамилия

    /// <summary>
    /// The first name.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string FirstName; // Имя

    /// <summary>
    /// The middle name.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string MiddleName; // Отчество

    /// <summary>
    /// The address.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Address; // Адрес

    /// <summary>
    /// The tel number.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string TelNumber; // Телефон

    /// <summary>
    /// The email.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Email; // E-Mail

    /// <summary>
    /// The inn.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string INN; // ИНН

    /// <summary>
    /// The drive license number.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DriveLicenseNumber; // Номер водительского удостоверения

    /// <summary>
    /// The transport number.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string TransportNumber; // Номер регистрации транспорта

    /// <summary>
    /// The document type.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentType; // Тип документа УДЛ

    /// <summary>
    /// The document series.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentSeries; // Серия документа

    /// <summary>
    /// The document number.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentNumber; // Номер документа

    /// <summary>
    /// The document issue date.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentIssueDate; // Дата выдачи документа

    /// <summary>
    /// The document end date.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentEndDate; // Дата окончания действия документа

    /// <summary>
    /// The document issue authority.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentIssueAuthority; // Кем выдан

    /// <summary>
    /// The document department code.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentDepartmentCode; // Код подразделения

    /// <summary>
    /// The document additional data.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentAdditionalData; // Дополнительные сведения

    /// <summary>
    /// The init.
    /// </summary>
    public void Init()
    {
      SNILS = new string('\0', UECMaxStringLength); // СНИЛС
      PolicyNumberOMS = new string('\0', UECMaxStringLength); // Номер полиса ОМС
      IssuerAddress = new string('\0', UECMaxStringLength); // Адрес эмитента
      BirthDate = new string('\0', UECMaxStringLength); // Дата рождения
      BirthPlace = new string('\0', UECMaxStringLength); // Место рождения
      Gender = new string('\0', UECMaxStringLength); // Пол
      LastName = new string('\0', UECMaxStringLength); // Фамилия
      FirstName = new string('\0', UECMaxStringLength); // Имя
      MiddleName = new string('\0', UECMaxStringLength); // Отчество
      Address = new string('\0', UECMaxStringLength); // Адрес
      TelNumber = new string('\0', UECMaxStringLength); // Телефон
      Email = new string('\0', UECMaxStringLength); // E-Mail
      INN = new string('\0', UECMaxStringLength); // ИНН
      DriveLicenseNumber = new string('\0', UECMaxStringLength); // Номер водительского удостоверения
      TransportNumber = new string('\0', UECMaxStringLength); // Номер регистрации транспорта
      DocumentType = new string('\0', UECMaxStringLength); // Тип документа УДЛ
      DocumentSeries = new string('\0', UECMaxStringLength); // Серия документа
      DocumentNumber = new string('\0', UECMaxStringLength); // Номер документа
      DocumentIssueDate = new string('\0', UECMaxStringLength); // Дата выдачи документа
      DocumentEndDate = new string('\0', UECMaxStringLength); // Дата окончания действия документа
      DocumentIssueAuthority = new string('\0', UECMaxStringLength); // Кем выдан
      DocumentDepartmentCode = new string('\0', UECMaxStringLength); // Код подразделения
      DocumentAdditionalData = new string('\0', UECMaxStringLength); // Дополнительные сведения
    }
  }

  /// <summary>
  /// The oms data.
  /// </summary>
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct OMSData
  {
    /// <summary>
    /// The uec max string length.
    /// </summary>
    private const int UECMaxStringLength = 1024 + 1;

    /// <summary>
    /// The last name.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string LastName; // Фамилия
    
    /// <summary>
    /// The first name.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string FirstName; // Имя
    
    /// <summary>
    /// The middle name.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string MiddleName; // Отчество
    
    /// <summary>
    /// The birth date.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string BirthDate; // Дата рождения
    
    /// <summary>
    /// The ogrn.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string OGRN; // ОГРН

    /// <summary>
    /// The okato.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string OKATO; // OKATO

    /// <summary>
    /// The insurance date.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string InsuranceDate; // Дата страхования

    /// <summary>
    /// The insurance end date.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string InsuranceEndDate; // Дата окончания страхования

    /// <summary>
    /// The init.
    /// </summary>
    public void Init()
    {
      LastName = new string('\0', UECMaxStringLength); // Фамилия
      FirstName = new string('\0', UECMaxStringLength); // Имя
      MiddleName = new string('\0', UECMaxStringLength); // Отчество
      BirthDate = new string('\0', UECMaxStringLength); // Дата рождения
      OGRN = new string('\0', UECMaxStringLength); // ОГРН
      OKATO = new string('\0', UECMaxStringLength); // OKATO
      InsuranceDate = new string('\0', UECMaxStringLength); // Дата страхования
      InsuranceEndDate = new string('\0', UECMaxStringLength); // Дата окончания страхования
    }
  }

  /// <summary>
  /// The card reader info.
  /// </summary>
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct CardReaderInfo
  {
    /// <summary>
    /// The uec max string length.
    /// </summary>
    private const int UECMaxStringLength = 1024 + 1;

    /// <summary>
    /// The reader name.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string ReaderName; // Имя устройства

    /// <summary>
    /// The init.
    /// </summary>
    public void Init()
    {
      ReaderName = new string('\0', UECMaxStringLength); // Имя устройства
    }
  }
}