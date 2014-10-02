// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UecService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The uec service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Uec
{
  #region
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Linq;
  using NHibernate;
  using StructureMap;
  using rt.srz.business.manager;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;
  using rt.uec.model.dto;
  using rt.uec.model.Interfaces;
  using rt.uec.model.protocol;
  #endregion

  /// <summary>
  ///   The uec service.
  /// </summary>
  public class UecService : IUecService
  {
    #region Fields

    /// <summary>
    ///   The protocol settings.
    /// </summary>
    private IEnumerable<ProtocolSettingsElement> protocolSettings;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the protocol settings.
    /// </summary>
    public IEnumerable<ProtocolSettingsElement> ProtocolSettings
    {
      get
      {
        return protocolSettings
               ??
               (protocolSettings =
                ((ProtocolSettingsSection)ConfigurationManager.GetSection("ProtocolSettingsSection")).ProtocolSettings.
                  Cast<ProtocolSettingsElement>());
      }
    }

    #endregion

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
    public byte[] GetCertificateKey(string workstationName, int version, int type)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetCertificateKey(workstationName, version, type);
    }

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
    public byte[] GetCertificateKey(string workstationName, string pdpCode, int version, int type)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetCertificateKey(workstationName, version, type);
    }

    /// <summary>
    /// Возвращает текущий тип криптографии
    /// </summary>
    /// <param name="workstationName">
    /// Имя машины в локальной сети ПВП 
    /// </param>
    /// <returns>
    /// Теущий тип криптографии 
    /// </returns>
    public int GetCurrentCryptographyType(string workstationName)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetCurrentCryptographyType(workstationName);
    }

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
    public int GetCurrentCryptographyType(string workstationName, string pdpCode)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetCurrentCryptographyType(workstationName, pdpCode);
    }

    /// <summary>
    /// Возвращает имя текущего УЭК ридера
    /// </summary>
    /// <param name="worstationName">
    /// Имя машины в локальной сети ПВП 
    /// </param>
    /// <returns>
    /// Имя текущего ридера 
    /// </returns>
    public string GetCurrentReaderName(string workstationName)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetCurrentReaderName(workstationName);
    }

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
    public string GetCurrentReaderName(string workstationName, string pdpCode)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetCurrentReaderName(workstationName, pdpCode);
    }

    /// <summary>
    /// Возвращает имя текущего Смарт Карт ридера
    /// </summary>
    /// <param name="worstationName">
    /// Имя машины в локальной сети ПВП 
    /// </param>
    /// <returns>
    /// Имя текущего ридера 
    /// </returns>
    public string GetCurrentSmcReaderName(string workstationName)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetCurrentSmcReaderName(workstationName);
    }

    /// <summary>
    /// Возвращает имя текущего Смарт Карт ридера
    /// </summary>
    /// <param name="worstationName">
    /// Имя машины в локальной сети ПВП 
    /// </param>
    /// <returns>
    /// Имя текущего ридера 
    /// </returns>
    public string GetCurrentSmcTokenReaderName(string workstationName)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetCurrentSmcTokenReaderName(workstationName);
    }

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
    public string GetCurrentSmcReaderName(string workstationName, string pdpCode)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetCurrentSmcReaderName(workstationName, pdpCode);
    }

    /// <summary>
    /// Возвращает имя текущего Смарт Карт ридера
    /// </summary>
    /// <param name="worstationName">
    /// Имя машины в локальной сети ПВП 
    /// </param>
    /// <returns>
    /// Имя текущего ридера 
    /// </returns>
    public string GetCurrentSmcTokenReaderName(string workstationName, string pdpCode)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetCurrentSmcTokenReaderName(workstationName, pdpCode);
    }

    /// <summary>
    /// Возвращает все МО для указанного ТФОМС
    /// </summary>
    /// <param name="tfomsCode">
    /// </param>
    /// <param name="workstationName">
    /// </param>
    /// <returns>
    /// The <see cref="MO[]"/> . 
    /// </returns>
    public MO[] GetMO(string tfomsCode, string workstationName)
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetMO(workstationName, workstationName); 
    }

    /// <summary>
    /// Возвращает все ТФОМС
    /// </summary>
    /// <param name="workstationName">
    /// The workstation Name. 
    /// </param>
    /// <returns>
    /// The <see cref="MO[]"/> . 
    /// </returns>
    public MO[] GetTFoms(string workstationName)
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetTFoms(workstationName);
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
      var res = ProtocolSettings.SingleOrDefault(x => x.Type.GetHashCode() == type);
      return res != null ? res.Value : string.Empty;
    }

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
    public WorkstationSettingParameter[] GetWorkstationSettings(string workstationName, string pdpCode)
    {
      return ObjectFactory.GetInstance<IWorkstationManager>().GetWorkstationSettings(workstationName, pdpCode);
    }

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
    public void SaveCurrentReaderName(string workstationName, string pdpCode, string readerName)
    {
      ObjectFactory.GetInstance<IWorkstationManager>().SaveCurrentReaderName(workstationName, pdpCode, readerName);
    }

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
    public void SaveCurrentSmcReaderName(string workstationName, string pdpCode, string readerName)
    {
      ObjectFactory.GetInstance<IWorkstationManager>().SaveCurrentSmcReaderName(workstationName, pdpCode, readerName);
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
      ObjectFactory.GetInstance<ISertificateUecManager>().SaveSmoSertificateKey(smoId, version, type, hexKey);
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
      ObjectFactory.GetInstance<ISertificateUecManager>().SaveSmoSertificateKey(smoId, version, type, key);
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
    /// <param name="keyHex">
    /// The key hex. 
    /// </param>
    public void SaveWorkstationSertificateHexKey(Guid worksationId, short version, int type, string keyHex)
    {
      ObjectFactory.GetInstance<ISertificateUecManager>().SaveWorkstationSertificateKey(
        worksationId, version, type, keyHex);
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
      ObjectFactory.GetInstance<ISertificateUecManager>().SaveWorkstationSertificateKey(
        worksationId, version, type, key);
    }

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
    public void SaveWorkstationSettings(string workstationName, string pdpCode, WorkstationSettingParameter[] settingsArr)
    {
      ObjectFactory.GetInstance<IWorkstationManager>().SaveWorkstationSettings(workstationName, pdpCode, settingsArr);
    }

    #endregion
  }
}