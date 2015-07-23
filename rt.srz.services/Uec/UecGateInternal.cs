// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UecGateInternal.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The uec gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Uec
{
  #region

  using System;

  using rt.core.services.aspects;
  using rt.uec.model.dto;
  using rt.uec.model.Interfaces;

  #endregion

  /// <summary>
  ///   The uec gate.
  /// </summary>
  public class UecGateInternal : InterceptedBase, IUecService
  {
    #region Fields

    /// <summary>
    ///   The service.
    /// </summary>
    protected readonly IUecService Service = new UecService();

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ���������� ���� �����������
    /// </summary>
    /// <param name="workstationName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <param name="version">
    /// ������ �����������
    /// </param>
    /// <param name="type">
    /// ��� �����������
    /// </param>
    /// <returns>
    /// ���� �����������
    /// </returns>
    public byte[] GetCertificateKey(string workstationName, int version, int type)
    {
      return InvokeInterceptors(() => Service.GetCertificateKey(workstationName, version, type));
    }

    /// <summary>
    /// ���������� ���� �����������
    /// </summary>
    /// <param name="workstationName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <param name="pdpCode">
    /// &gt; ��� ���
    /// </param>
    /// <param name="version">
    /// ������ �����������
    /// </param>
    /// <param name="type">
    /// ��� �����������
    /// </param>
    /// <returns>
    /// ���� �����������
    /// </returns>
    public byte[] GetCertificateKey(string workstationName, string pdpCode, int version, int type)
    {
      return InvokeInterceptors(() => Service.GetCertificateKey(workstationName, pdpCode, version, type));
    }

    /// <summary>
    /// ���������� ������� ��� ������������
    /// </summary>
    /// <param name="workstationName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <returns>
    /// ������� ��� ������������
    /// </returns>
    public int GetCurrentCryptographyType(string workstationName)
    {
      return InvokeInterceptors(() => Service.GetCurrentCryptographyType(workstationName));
    }

    /// <summary>
    /// ���������� ������� ��� ������������
    /// </summary>
    /// <param name="workstationName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <param name="pdpCode">
    /// &gt; ��� ���
    /// </param>
    /// <returns>
    /// ������ ��� ������������
    /// </returns>
    public int GetCurrentCryptographyType(string workstationName, string pdpCode)
    {
      return InvokeInterceptors(() => Service.GetCurrentCryptographyType(workstationName, pdpCode));
    }

    /// <summary>
    /// ���������� ��� �������� ���  ������
    /// </summary>
    /// <param name="worstationName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <returns>
    /// ��� �������� ������
    /// </returns>
    public string GetCurrentReaderName(string worstationName)
    {
      return InvokeInterceptors(() => Service.GetCurrentReaderName(worstationName));
    }

    /// <summary>
    /// ���������� ��� �������� ���  ������
    /// </summary>
    /// <param name="worstationName">
    /// The worstation Name.
    /// </param>
    /// <param name="pdpCode">
    /// ��� ���
    /// </param>
    /// <returns>
    /// ��� �������� ������
    /// </returns>
    public string GetCurrentReaderName(string worstationName, string pdpCode)
    {
      return InvokeInterceptors(() => Service.GetCurrentReaderName(worstationName, pdpCode));
    }

    /// <summary>
    /// The get current smc reader name.
    /// </summary>
    /// <param name="worstationName">
    /// The worstation name.
    /// </param>
    /// <returns>
    /// The <see cref="string"/> .
    /// </returns>
    public string GetCurrentSmcReaderName(string worstationName)
    {
      return InvokeInterceptors(() => Service.GetCurrentSmcReaderName(worstationName));
    }

    /// <summary>
    /// ���������� ��� �������� ����� ���� ������
    /// </summary>
    /// <param name="worstationName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <param name="pdpCode">
    /// ��� ���
    /// </param>
    /// <returns>
    /// ��� �������� ������
    /// </returns>
    public string GetCurrentSmcReaderName(string worstationName, string pdpCode)
    {
      return InvokeInterceptors(() => Service.GetCurrentSmcReaderName(worstationName, pdpCode));
    }

    /// <summary>
    /// ���������� ��� �������� ����� ���� ������
    /// </summary>
    /// <param name="workstationName">
    /// The workstation Name.
    /// </param>
    /// <returns>
    /// ��� �������� ������
    /// </returns>
    public string GetCurrentSmcTokenReaderName(string workstationName)
    {
      return InvokeInterceptors(() => Service.GetCurrentSmcTokenReaderName(workstationName));
    }

    /// <summary>
    /// ���������� ��� �������� ����� ���� ������
    /// </summary>
    /// <param name="workstationName">
    /// The workstation Name.
    /// </param>
    /// <param name="pdpCode">
    /// The pdp Code.
    /// </param>
    /// <returns>
    /// ��� �������� ������
    /// </returns>
    public string GetCurrentSmcTokenReaderName(string workstationName, string pdpCode)
    {
      return InvokeInterceptors(() => Service.GetCurrentSmcTokenReaderName(workstationName, pdpCode));
    }

    /// <summary>
    /// ���������� ��� ��� ��� ���������� �����
    /// </summary>
    /// <param name="tfomsCode">
    /// The tfoms Code.
    /// </param>
    /// <param name="workstationName">
    /// The workstation Name.
    /// </param>
    /// <returns>
    /// The <see>
    ///     <cref>MO[]</cref>
    ///   </see>
    ///   .
    /// </returns>
    public MO[] GetMO(string tfomsCode, string workstationName)
    {
      return InvokeInterceptors(() => Service.GetMO(tfomsCode, workstationName));
    }

    /// <summary>
    /// The get protocol settings.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/> .
    /// </returns>
    public string GetProtocolSettings(int type)
    {
      return InvokeInterceptors(() => Service.GetProtocolSettings(type));
    }

    /// <summary>
    /// ���������� ��� �����
    /// </summary>
    /// <param name="workstationName">
    /// The workstation Name.
    /// </param>
    /// <returns>
    /// The <see>
    ///     <cref>MO[]</cref>
    ///   </see>
    ///   .
    /// </returns>
    public MO[] GetTFoms(string workstationName)
    {
      return InvokeInterceptors(() => Service.GetTFoms(workstationName));
    }

    /// <summary>
    /// ���������� ��������� ������� �������
    /// </summary>
    /// <param name="workstationName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <param name="pdpCode">
    /// ��� ���
    /// </param>
    /// <returns>
    /// ���������
    /// </returns>
    public WorkstationSettingParameter[] GetWorkstationSettings(string workstationName, string pdpCode)
    {
      return InvokeInterceptors(() => Service.GetWorkstationSettings(workstationName, pdpCode));
    }

    /// <summary>
    /// ��������� ��� �������� ��� ������
    /// </summary>
    /// <param name="worstationName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <param name="pdpCode">
    /// ��� ���
    /// </param>
    /// <param name="readerName">
    /// ��� �������� ������
    /// </param>
    public void SaveCurrentReaderName(string worstationName, string pdpCode, string readerName)
    {
      InvokeInterceptors(() => Service.SaveCurrentReaderName(worstationName, pdpCode, readerName));
    }

    /// <summary>
    /// ��������� ��� �������� ���� ���� ������
    /// </summary>
    /// <param name="worstationName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <param name="pdpCode">
    /// ��� ���
    /// </param>
    /// <param name="readerName">
    /// ��� �������� ������
    /// </param>
    public void SaveCurrentSmcReaderName(string worstationName, string pdpCode, string readerName)
    {
      InvokeInterceptors(() => Service.SaveCurrentSmcReaderName(worstationName, pdpCode, readerName));
    }

    /// <summary>
    /// The save smo sertificate key.
    /// </summary>
    /// <param name="smoId">
    /// The smo id.
    /// </param>
    /// <param name="version">
    /// The version.
    /// </param>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="hexKey">
    /// The hex key.
    /// </param>
    public void SaveSmoSertificateHexKey(Guid smoId, short version, int type, string hexKey)
    {
      InvokeInterceptors(() => Service.SaveSmoSertificateHexKey(smoId, version, type, hexKey));
    }

    /// <summary>
    /// The save smo sertificate key.
    /// </summary>
    /// <param name="smoId">
    /// The smo id.
    /// </param>
    /// <param name="version">
    /// The version.
    /// </param>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="key">
    /// The key.
    /// </param>
    public void SaveSmoSertificateKey(Guid smoId, short version, int type, byte[] key)
    {
      InvokeInterceptors(() => Service.SaveSmoSertificateKey(smoId, version, type, key));
    }

    /// <summary>
    /// The save workstation sertificate key.
    /// </summary>
    /// <param name="worksationId">
    /// The worksation id.
    /// </param>
    /// <param name="version">
    /// The version.
    /// </param>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="keyHex">
    /// The key hex.
    /// </param>
    public void SaveWorkstationSertificateHexKey(Guid worksationId, short version, int type, string keyHex)
    {
      InvokeInterceptors(() => Service.SaveWorkstationSertificateHexKey(worksationId, version, type, keyHex));
    }

    /// <summary>
    /// The save sertificate key.
    /// </summary>
    /// <param name="worksationId">
    /// The worksation Id.
    /// </param>
    /// <param name="version">
    /// The version.
    /// </param>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="key">
    /// The key.
    /// </param>
    public void SaveWorkstationSertificateKey(Guid worksationId, short version, int type, byte[] key)
    {
      InvokeInterceptors(() => Service.SaveWorkstationSertificateKey(worksationId, version, type, key));
    }

    /// <summary>
    /// ��������� ��������� ������� �������
    /// </summary>
    /// <param name="workstationName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <param name="pdpCode">
    /// ��� ���
    /// </param>
    /// <param name="settings">
    /// The settings.
    /// </param>
    public void SaveWorkstationSettings(string workstationName, string pdpCode, WorkstationSettingParameter[] settings)
    {
      InvokeInterceptors(() => Service.SaveWorkstationSettings(workstationName, pdpCode, settings));
    }

    #endregion
  }
}