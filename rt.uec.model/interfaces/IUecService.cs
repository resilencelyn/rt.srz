﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUecService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The UecService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.uec.model.Interfaces
{
  #region references

  using System;
  using System.Runtime.InteropServices;
  using System.ServiceModel;
  using rt.uec.model.dto;

  #endregion

  /// <summary>
  ///   The UecService interface.
  /// </summary>
  [ComVisible(true)]
  [Guid("7B276FE3-CA0B-418D-858D-C0830414B671")]
  [ServiceContract]
  public interface IUecService
  {
    #region Public Methods and Operators

    /// <summary>
    /// Возвращает имя текущего УЭК  ридера
    /// </summary>
    /// <param name="workstationName">
    /// Имя машины в локальной сети ПВП
    /// </param>
    /// <returns>
    /// Имя текущего ридера
    /// </returns>
    [OperationContract]
    string GetCurrentReaderName(string workstationName);

    /// <summary>
    /// Возвращает имя текущего УЭК  ридера
    /// </summary>
    /// <param name="workstationName">Имя машины в локальной сети ПВП</param>
    /// <param name="pdpCode">Код ПВП</param>
    /// <returns>Имя текущего ридера</returns>
    [OperationContract(Name="GetCurrentReaderNameWithPdpCode")]
    string GetCurrentReaderName(string worstationName, string pdpCode);

    /// <summary>
    /// Сохраняет имя текущего УЭК ридера
    /// </summary>
    /// <param name="worstationName">Имя машины в локальной сети ПВП</param>
    /// <param name="pdpCode">Код ПВП</param>
    /// <param name="readerName">Имя текущего ридера</param>
    [OperationContract]
    void SaveCurrentReaderName(string worstationName, string pdpCode, string readerName);

    /// <summary>
    /// Возвращает имя текущего Смарт Карт ридера
    /// </summary>
    /// <param name="worstationName">
    /// Имя машины в локальной сети ПВП 
    /// </param>
    /// <returns>
    /// Имя текущего ридера 
    /// </returns>
    [OperationContract]
    string GetCurrentSmcReaderName(string workstationName);

    /// <summary>
    /// Возвращает имя текущего Смарт Карт ридера
    /// </summary>
    /// <param name="worstationName">
    /// Имя машины в локальной сети ПВП 
    /// </param>
    /// <returns>
    /// Имя текущего ридера 
    /// </returns>
    [OperationContract]
    string GetCurrentSmcTokenReaderName(string workstationName);

    /// <summary>
    /// Возвращает имя текущего Смарт Карт ридера
    /// </summary>
    /// <param name="worstationName">
    /// Имя машины в локальной сети ПВП 
    /// </param>
    /// <param name="pdpCode">
    /// Код ПВП
    /// </param>
    /// <returns>
    /// Имя текущего ридера 
    /// </returns>
    [OperationContract(Name = "GetCurrentSmcReaderNameWithPdpCode")]
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
    [OperationContract(Name = "GetCurrentTokenSmcReaderNameWithPdpCode")]
    string GetCurrentSmcTokenReaderName(string workstationName, string pdpCode);

    /// <summary>
    /// Сохраняет имя текущего Сарт Карт ридера
    /// </summary>
    /// <param name="worstationName">Имя машины в локальной сети ПВП</param>
    /// <param name="pdpCode">Код ПВП</param>
    /// <param name="readerName">Имя текущего ридера</param>
    [OperationContract]
    void SaveCurrentSmcReaderName(string workstationName, string pdpCode, string readerName);

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
    [OperationContract]
    byte[] GetCertificateKey(string workstationName, int version, int type);

    /// <summary>
    /// Возвращает ключ сертификата
    /// </summary>
    /// <param name="workstationName">Имя машины в локальной сети ПВП</param>
    /// <param name="pdpCode">>Код ПВП</param>
    /// <param name="version">Версия сертификата</param>
    /// <param name="type">Тип сертификата</param>
    /// <returns>Ключ сертификата</returns>
    [OperationContract(Name = "GetCertificateKeyWithPpdCode")]
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
    [OperationContract]
    int GetCurrentCryptographyType(string workstationName);

    /// <summary>
    /// Возвращает текущий тип криптографии
    /// </summary>
    /// <param name="workstationName">
    /// Имя машины в локальной сети ПВП
    /// </param>
    /// <param name="pdpCode">>
    /// Код ПВП
    /// </param>
    /// <returns>
    /// Теущий тип криптографии
    /// </returns>
    [OperationContract(Name = "GetCurrentCryptographyTypeWithPdpCode")]
    int GetCurrentCryptographyType(string workstationName, string pdpCode);

    /// <summary>
    /// Возвращает настройки протоколирования
    /// </summary>
    /// <param name="type">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    [OperationContract]
    string GetProtocolSettings(int type);

    /// <summary>
    /// Сохраняет ключ сертификата
    /// </summary>
    /// <param name="worksationId"></param>
    /// <param name="version">
    /// </param>
    /// <param name="type">
    /// </param>
    /// <param name="key">
    /// </param>
    [OperationContract]
    void SaveWorkstationSertificateKey(Guid worksationId, short version, int type, byte[] key);

    /// <summary>
    /// The save sertificate key.
    /// </summary>
    /// <param name="worksationId"></param>
    /// <param name="version">
    ///   The version.
    /// </param>
    /// <param name="type">
    ///   The type.
    /// </param>
    /// <param name="keyHex">
    ///   The key hex.
    /// </param>
    [OperationContract]
    void SaveWorkstationSertificateHexKey(Guid worksationId, short version, int type, string keyHex);

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
    [OperationContract]
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
    [OperationContract]
    void SaveSmoSertificateHexKey(Guid smoId, short version, int type, string hexKey);

    /// <summary>
    /// Возвращает настройки рабочей станции
    /// </summary>
    /// <param name="workstationName">Имя машины в локальной сети ПВП</param>
    /// <param name="pdpCode">Код ПВП</param>
    /// <returns>Настройки</returns>
    [OperationContract]
    WorkstationSettingParameter[] GetWorkstationSettings(string workstationName, string pdpCode);

    /// <summary>
    /// Сохраняет настройки рабочей станции
    /// </summary>
    /// <param name="workstationName">Имя машины в локальной сети ПВП</param>
    /// <param name="pdpCode">Код ПВП</param>
    /// <param name="?">Настройки</param>
    [OperationContract]
    void SaveWorkstationSettings(string workstationName, string pdpCode, WorkstationSettingParameter[] settings);

    /// <summary>
    /// Возвращает все ТФОМС
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    MO[] GetTFoms(string workstationName);
    
    /// <summary>
    /// Возвращает все МО для указанного ТФОМС
    /// </summary>
    /// <param name="tfomsCode"></param>
    /// <param name="workstationName"></param>
    /// <returns></returns>
    [OperationContract]
    MO[] GetMO(string tfomsCode, string workstationName);

    #endregion
  }
}