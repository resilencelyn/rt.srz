// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWorkstationManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface WorkstationManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using rt.uec.model.dto;

  /// <summary>
  ///   The interface WorkstationManager.
  /// </summary>
  public partial interface IWorkstationManager
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
    /// Возвращает ключ сертификата
    /// </summary>
    /// <param name="workstationName">
    /// Имя машины в локальной сети ПВП
    /// </param>
    /// <param name="pdpCode">
    /// &gt; Код ПВП
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
    byte[] GetCertificateKey(string workstationName, string pdpCode, int version, int type);

    /// <summary>
    /// Возвращает текущий тип криптографии
    /// </summary>
    /// <param name="workstationName">
    /// Имя машины в локальной сети ПВП
    /// </param>
    /// <returns>
    /// Теущий тип криптографии
    /// </returns>
    int GetCurrentCryptographyType(string workstationName);

    /// <summary>
    /// Возвращает текущий тип криптографии
    /// </summary>
    /// <param name="workstationName">
    /// Имя машины в локальной сети ПВП
    /// </param>
    /// <param name="pdpCode">
    /// &gt; Код ПВП
    /// </param>
    /// <returns>
    /// Теущий тип криптографии
    /// </returns>
    int GetCurrentCryptographyType(string workstationName, string pdpCode);

    /// <summary>
    /// Возвращает имя текущего УЭК ридера
    /// </summary>
    /// <param name="worstationName">
    /// Имя машины в локальной сети ПВП
    /// </param>
    /// <returns>
    /// Имя текущего ридера
    /// </returns>
    string GetCurrentReaderName(string worstationName);

    /// <summary>
    /// Возвращает имя текущего УЭК  ридера
    /// </summary>
    /// <param name="worstationName">
    /// The worstation Name.
    /// </param>
    /// <param name="pdpCode">
    /// Код ПВП
    /// </param>
    /// <returns>
    /// Имя текущего ридера
    /// </returns>
    string GetCurrentReaderName(string worstationName, string pdpCode);

    /// <summary>
    /// Возвращает имя текущего Смарт Карт ридера
    /// </summary>
    /// <param name="worstationName">
    /// Имя машины в локальной сети ПВП
    /// </param>
    /// <returns>
    /// Имя текущего ридера
    /// </returns>
    string GetCurrentSmcReaderName(string worstationName);

    /// <summary>
    /// Возвращает имя текущего Смарт Карт ридера
    /// </summary>
    /// <param name="workstationName">
    /// The workstation Name.
    /// </param>
    /// <param name="pdpCode">
    /// Код ПВП
    /// </param>
    /// <returns>
    /// Имя текущего ридера
    /// </returns>
    string GetCurrentSmcReaderName(string workstationName, string pdpCode);

    /// <summary>
    /// Возвращает имя текущего Смарт Карт ридера
    /// </summary>
    /// <param name="worstationName">
    /// Имя машины в локальной сети ПВП
    /// </param>
    /// <returns>
    /// Имя текущего ридера
    /// </returns>
    string GetCurrentSmcTokenReaderName(string worstationName);

    /// <summary>
    /// Возвращает имя текущего Смарт Карт ридера
    /// </summary>
    /// <param name="workstationName">
    /// The workstation Name.
    /// </param>
    /// <param name="pdpCode">
    /// The pdp Code.
    /// </param>
    /// <returns>
    /// Имя текущего ридера
    /// </returns>
    string GetCurrentSmcTokenReaderName(string workstationName, string pdpCode);

    /// <summary>
    /// Возвращает настройки рабочей станции
    /// </summary>
    /// <param name="workstationName">
    /// Имя машины в локальной сети ПВП
    /// </param>
    /// <param name="pdpCode">
    /// Код ПВП
    /// </param>
    /// <returns>
    /// Настройки
    /// </returns>
    WorkstationSettingParameter[] GetWorkstationSettings(string workstationName, string pdpCode);

    /// <summary>
    /// Сохраняет имя текущего УЭК ридера
    /// </summary>
    /// <param name="workstationName">
    /// The workstation Name.
    /// </param>
    /// <param name="pdpCode">
    /// Код ПВП
    /// </param>
    /// <param name="readerName">
    /// Имя текущего ридера
    /// </param>
    void SaveCurrentReaderName(string workstationName, string pdpCode, string readerName);

    /// <summary>
    /// Сохраняет имя текущего Сарт Карт ридера
    /// </summary>
    /// <param name="workstationName">
    /// The workstation Name.
    /// </param>
    /// <param name="pdpCode">
    /// Код ПВП
    /// </param>
    /// <param name="readerName">
    /// Имя текущего ридера
    /// </param>
    void SaveCurrentSmcReaderName(string workstationName, string pdpCode, string readerName);

    /// <summary>
    /// Сохраняет настройки рабочей станции
    /// </summary>
    /// <param name="workstationName">
    /// Имя машины в локальной сети ПВП
    /// </param>
    /// <param name="pdpCode">
    /// Код ПВП
    /// </param>
    /// <param name="settingsArr">
    /// The settings Arr.
    /// </param>
    void SaveWorkstationSettings(string workstationName, string pdpCode, WorkstationSettingParameter[] settingsArr);

    #endregion
  }
}