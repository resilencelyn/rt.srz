// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISertificateUecManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface SertificateUecManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;

  /// <summary>
  ///   The interface SertificateUecManager.
  /// </summary>
  public partial interface ISertificateUecManager
  {
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
    byte[] GetCertificateKey(string workstationName, int version, int type);

    /// <summary>
    /// The save sertificate key.
    /// </summary>
    /// <param name="workstationId"></param>
    /// <param name="version">
    ///   The version.
    /// </param>
    /// <param name="type">
    ///   The type.
    /// </param>
    /// <param name="key">
    ///   The key.
    /// </param>
    void SaveWorkstationSertificateKey(Guid workstationId, short version, int type, byte[] key);

    #endregion

    void SaveWorkstationSertificateKey(Guid workstationId, short version, int type, string hexKey);

    /// <summary>
    /// The save smo sertificate key.
    /// </summary>
    /// <param name="smoId">
    ///   The smo id. 
    /// </param>
    /// <param name="version">
    ///   The version. 
    /// </param>
    /// <param name="type">
    ///   The type. 
    /// </param>
    /// <param name="key">
    ///   The key. 
    /// </param>
    void SaveSmoSertificateKey(Guid smoId, short version, int type, byte[] key);

    /// <summary>
    /// The save smo sertificate key.
    /// </summary>
    /// <param name="smoId">
    ///   The smo id. 
    /// </param>
    /// <param name="version">
    ///   The version. 
    /// </param>
    /// <param name="type">
    ///   The type. 
    /// </param>
    /// <param name="hexKey">
    ///   The hex key. 
    /// </param>
    void SaveSmoSertificateKey(Guid smoId, short version, int type, string hexKey);
  }
}