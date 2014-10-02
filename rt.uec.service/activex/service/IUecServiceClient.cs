// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUecServiceClient.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The UecServiceServiceClient interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace rt.uec.service.activex.service
{
  


  #region references

  using rt.uec.model.enumerations;

  #endregion

  /// <summary>
  /// The UecServiceServiceClient interface.
  /// </summary>
  [ComVisible(true)]
  [Guid("2523BACE-AEE2-4C09-8127-313D4FFFBE2F")]
  public interface IUecServiceClient
  {
    #region Public Methods and Operators

    /// <summary>
    /// ��������� ���������� � ��� �������
    /// </summary>
    /// <param name="token">
    ///   ��������������� �����
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool OpenConnection(string token);

    /// <summary>
    /// ��������� ����������
    /// </summary>
    void CloseConnection();

    /// <summary>
    /// ���������� ���� �����������
    /// </summary>
    /// <param name="pcName">
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
    long GetCertificateKey(string pcName, int version, int type, ref byte[] key);

    /// <summary>
    /// ���������� ������� ��� ������������
    /// </summary>
    /// <param name="pcName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <returns>
    /// ������ ��� ������������
    /// </returns>
    long GetCurrentCryptographyType(string pcName, ref CryptographyType type);

    /// <summary>
    /// ���������� ��� �������� ���  ������
    /// </summary>
    /// <param name="pcName">
    /// ��� ������ � ��������� ���� ���
    /// </param>
    /// <returns>
    /// ��� �������� ������
    /// </returns>
    long GetCurrentReaderName(string pcName, ref string name);

    /// <summary>
    /// ���������� ��������� ����������������
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    long GetProtocolSettings(ProtocolSettingsEnum type, ref string value);

    #endregion
  }
}