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
    /// Открывает соединение к УЭК сервису
    /// </summary>
    /// <param name="token">
    ///   Авторизационный токен
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool OpenConnection(string token);

    /// <summary>
    /// Закрывает соединение
    /// </summary>
    void CloseConnection();

    /// <summary>
    /// Возвращает ключ сертификата
    /// </summary>
    /// <param name="pcName">
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
    long GetCertificateKey(string pcName, int version, int type, ref byte[] key);

    /// <summary>
    /// Возвращает текущий тип криптографии
    /// </summary>
    /// <param name="pcName">
    /// Имя машины в локальной сети ПВП
    /// </param>
    /// <returns>
    /// Теущий тип криптографии
    /// </returns>
    long GetCurrentCryptographyType(string pcName, ref CryptographyType type);

    /// <summary>
    /// Возвращает имя текущего УЭК  ридера
    /// </summary>
    /// <param name="pcName">
    /// Имя машины в локальной сети ПВП
    /// </param>
    /// <returns>
    /// Имя текущего ридера
    /// </returns>
    long GetCurrentReaderName(string pcName, ref string name);

    /// <summary>
    /// Возвращает настройки протоколирования
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    long GetProtocolSettings(ProtocolSettingsEnum type, ref string value);

    #endregion
  }
}