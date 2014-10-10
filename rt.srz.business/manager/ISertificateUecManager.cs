// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISertificateUecManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
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
    /// Возвращает ключ сертификата
    /// </summary>
    /// <param name="workstationName">
    /// Имя машины в локальной сети ПВП
    /// </param>
    /// <param name="version">
    /// Версия сертификата
    /// </param>
    /// <param name="type">
    /// Тип сертификата
    /// </param>
    /// <returns>
    /// Ключ сертификата
    /// </returns>
    byte[] GetCertificateKey(string workstationName, int version, int type);

    /// <summary>
    /// The save smo sertificate key.
    /// </summary>
    /// <param name="smoId">
    /// The smo id.
    /// </param>
    /// <param name="version">
    /// The Version.
    /// </param>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="key">
    /// The key.
    /// </param>
    void SaveSmoSertificateKey(Guid smoId, short version, int type, byte[] key);

    /// <summary>
    /// The save smo sertificate key.
    /// </summary>
    /// <param name="smoId">
    /// The smo id.
    /// </param>
    /// <param name="version">
    /// The Version.
    /// </param>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="hexKey">
    /// The hex key.
    /// </param>
    void SaveSmoSertificateKey(Guid smoId, short version, int type, string hexKey);

    /// <summary>
    /// The save sertificate key.
    /// </summary>
    /// <param name="workstationId">
    /// The workstation Id.
    /// </param>
    /// <param name="version">
    /// The Version.
    /// </param>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="key">
    /// The key.
    /// </param>
    void SaveWorkstationSertificateKey(Guid workstationId, short version, int type, byte[] key);

    /// <summary>
    /// The save workstation sertificate key.
    /// </summary>
    /// <param name="workstationId">
    /// The workstation id.
    /// </param>
    /// <param name="version">
    /// The Version.
    /// </param>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="hexKey">
    /// The hex key.
    /// </param>
    void SaveWorkstationSertificateKey(Guid workstationId, short version, int type, string hexKey);

    #endregion
  }
}