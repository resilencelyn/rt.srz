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
    public string SNILS; // �����

    /// <summary>
    /// The policy number oms.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string PolicyNumberOMS; // ����� ������ ���

    /// <summary>
    /// The issuer address.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string IssuerAddress; // ����� ��������

    /// <summary>
    /// The birth date.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string BirthDate; // ���� ��������

    /// <summary>
    /// The birth place.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string BirthPlace; // ����� ��������

    /// <summary>
    /// The gender.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Gender; // ���

    /// <summary>
    /// The last name.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string LastName; // �������

    /// <summary>
    /// The first name.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string FirstName; // ���

    /// <summary>
    /// The middle name.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string MiddleName; // ��������

    /// <summary>
    /// The address.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Address; // �����

    /// <summary>
    /// The tel number.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string TelNumber; // �������

    /// <summary>
    /// The email.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Email; // E-Mail

    /// <summary>
    /// The inn.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string INN; // ���

    /// <summary>
    /// The drive license number.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DriveLicenseNumber; // ����� ������������� �������������

    /// <summary>
    /// The transport number.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string TransportNumber; // ����� ����������� ����������

    /// <summary>
    /// The document type.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentType; // ��� ��������� ���

    /// <summary>
    /// The document series.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentSeries; // ����� ���������

    /// <summary>
    /// The document number.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentNumber; // ����� ���������

    /// <summary>
    /// The document issue date.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentIssueDate; // ���� ������ ���������

    /// <summary>
    /// The document end date.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentEndDate; // ���� ��������� �������� ���������

    /// <summary>
    /// The document issue authority.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentIssueAuthority; // ��� �����

    /// <summary>
    /// The document department code.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentDepartmentCode; // ��� �������������

    /// <summary>
    /// The document additional data.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DocumentAdditionalData; // �������������� ��������

    /// <summary>
    /// The init.
    /// </summary>
    public void Init()
    {
      SNILS = new string('\0', UECMaxStringLength); // �����
      PolicyNumberOMS = new string('\0', UECMaxStringLength); // ����� ������ ���
      IssuerAddress = new string('\0', UECMaxStringLength); // ����� ��������
      BirthDate = new string('\0', UECMaxStringLength); // ���� ��������
      BirthPlace = new string('\0', UECMaxStringLength); // ����� ��������
      Gender = new string('\0', UECMaxStringLength); // ���
      LastName = new string('\0', UECMaxStringLength); // �������
      FirstName = new string('\0', UECMaxStringLength); // ���
      MiddleName = new string('\0', UECMaxStringLength); // ��������
      Address = new string('\0', UECMaxStringLength); // �����
      TelNumber = new string('\0', UECMaxStringLength); // �������
      Email = new string('\0', UECMaxStringLength); // E-Mail
      INN = new string('\0', UECMaxStringLength); // ���
      DriveLicenseNumber = new string('\0', UECMaxStringLength); // ����� ������������� �������������
      TransportNumber = new string('\0', UECMaxStringLength); // ����� ����������� ����������
      DocumentType = new string('\0', UECMaxStringLength); // ��� ��������� ���
      DocumentSeries = new string('\0', UECMaxStringLength); // ����� ���������
      DocumentNumber = new string('\0', UECMaxStringLength); // ����� ���������
      DocumentIssueDate = new string('\0', UECMaxStringLength); // ���� ������ ���������
      DocumentEndDate = new string('\0', UECMaxStringLength); // ���� ��������� �������� ���������
      DocumentIssueAuthority = new string('\0', UECMaxStringLength); // ��� �����
      DocumentDepartmentCode = new string('\0', UECMaxStringLength); // ��� �������������
      DocumentAdditionalData = new string('\0', UECMaxStringLength); // �������������� ��������
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
    public string LastName; // �������
    
    /// <summary>
    /// The first name.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string FirstName; // ���
    
    /// <summary>
    /// The middle name.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string MiddleName; // ��������
    
    /// <summary>
    /// The birth date.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string BirthDate; // ���� ��������
    
    /// <summary>
    /// The ogrn.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string OGRN; // ����

    /// <summary>
    /// The okato.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string OKATO; // OKATO

    /// <summary>
    /// The insurance date.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string InsuranceDate; // ���� �����������

    /// <summary>
    /// The insurance end date.
    /// </summary>
    [MarshalAs(UnmanagedType.LPWStr)]
    public string InsuranceEndDate; // ���� ��������� �����������

    /// <summary>
    /// The init.
    /// </summary>
    public void Init()
    {
      LastName = new string('\0', UECMaxStringLength); // �������
      FirstName = new string('\0', UECMaxStringLength); // ���
      MiddleName = new string('\0', UECMaxStringLength); // ��������
      BirthDate = new string('\0', UECMaxStringLength); // ���� ��������
      OGRN = new string('\0', UECMaxStringLength); // ����
      OKATO = new string('\0', UECMaxStringLength); // OKATO
      InsuranceDate = new string('\0', UECMaxStringLength); // ���� �����������
      InsuranceEndDate = new string('\0', UECMaxStringLength); // ���� ��������� �����������
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
    public string ReaderName; // ��� ����������

    /// <summary>
    /// The init.
    /// </summary>
    public void Init()
    {
      ReaderName = new string('\0', UECMaxStringLength); // ��� ����������
    }
  }
}