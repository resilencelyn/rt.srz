// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWorkstationManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface WorkstationManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.uec.model.dto;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The interface WorkstationManager.
  /// </summary>
  public partial interface IWorkstationManager
  {
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
    byte[] GetCertificateKey(string workstationName, int version, int type);

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
    byte[] GetCertificateKey(string workstationName, string pdpCode, int version, int type);

    /// <summary>
    /// ���������� ������� ��� ������������
    /// </summary>
    /// <param name="workstationName">
    /// ��� ������ � ��������� ���� ��� 
    /// </param>
    /// <returns>
    /// ������ ��� ������������ 
    /// </returns>
    int GetCurrentCryptographyType(string workstationName);

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
    int GetCurrentCryptographyType(string workstationName, string pdpCode);

     /// <summary>
    /// ���������� ��� �������� ��� ������
    /// </summary>
    /// <param name="worstationName">
    /// ��� ������ � ��������� ���� ��� 
    /// </param>
    /// <returns>
    /// ��� �������� ������ 
    /// </returns>
    string GetCurrentReaderName(string worstationName);

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
    string GetCurrentReaderName(string worstationName, string pdpCode);

     /// <summary>
    /// ���������� ��� �������� ����� ���� ������
    /// </summary>
    /// <param name="worstationName">
    /// ��� ������ � ��������� ���� ��� 
    /// </param>
    /// <returns>
    /// ��� �������� ������ 
    /// </returns>
    string GetCurrentSmcReaderName(string worstationName);

    /// <summary>
    /// ���������� ��� �������� ����� ���� ������
    /// </summary>
    /// <param name="worstationName">
    /// ��� ������ � ��������� ���� ��� 
    /// </param>
    /// <returns>
    /// ��� �������� ������ 
    /// </returns>
    string GetCurrentSmcTokenReaderName(string worstationName);

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
    string GetCurrentSmcReaderName(string workstationName, string pdpCode);

     /// <summary>
    /// ���������� ��� �������� ����� ���� ������
    /// </summary>
    /// <param name="worstationName">
    /// ��� ������ � ��������� ���� ��� 
    /// </param>
    /// <returns>
    /// ��� �������� ������ 
    /// </returns>
    string GetCurrentSmcTokenReaderName(string workstationName, string pdpCode);

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
    WorkstationSettingParameter[] GetWorkstationSettings(string workstationName, string pdpCode);

    /// <summary>
    /// ��������� ��� �������� ��� ������
    /// </summary>
    /// <param name="workstationName">
    /// The workstation Name. 
    /// </param>
    /// <param name="pdpCode">
    /// ��� ��� 
    /// </param>
    /// <param name="readerName">
    /// ��� �������� ������ 
    /// </param>
    void SaveCurrentReaderName(string workstationName, string pdpCode, string readerName);

    /// <summary>
    /// ��������� ��� �������� ���� ���� ������
    /// </summary>
    /// <param name="workstationName">
    /// The workstation Name. 
    /// </param>
    /// <param name="pdpCode">
    /// ��� ��� 
    /// </param>
    /// <param name="readerName">
    /// ��� �������� ������ 
    /// </param>
    void SaveCurrentSmcReaderName(string workstationName, string pdpCode, string readerName);

     /// <summary>
    /// ��������� ��������� ������� �������
    /// </summary>
    /// <param name="workstationName">
    /// ��� ������ � ��������� ���� ��� 
    /// </param>
    /// <param name="pdpCode">
    /// ��� ��� 
    /// </param>
    /// <param name="settingsArr">
    /// The settings Arr. 
    /// </param>
    void SaveWorkstationSettings(string workstationName, string pdpCode, WorkstationSettingParameter[] settingsArr);
  }
}