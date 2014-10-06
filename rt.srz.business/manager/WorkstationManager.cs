// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkstationManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The WorkstationManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Linq;

  using NHibernate;

  using rt.core.business.security.interfaces;
  using rt.core.model.interfaces;
  using rt.srz.model.srz;
  using rt.uec.model.dto;
  using rt.uec.model.enumerations;

  using StructureMap;

  /// <summary>
  ///   The WorkstationManager.
  /// </summary>
  public partial class WorkstationManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The delete.
    /// </summary>
    /// <param name="entity">
    /// The entity.
    /// </param>
    public override void Delete(Workstation entity)
    {
      ObjectFactory.GetInstance<ISettingManager>().Delete(x => x.Workstation.Id == entity.Id);
      base.Delete(entity);
    }

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
      var user = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      var sertificateUecManager = ObjectFactory.GetInstance<ISertificateUecManager>();
      var organisationManager = ObjectFactory.GetInstance<IOrganisationManager>();

      // Ищем сертификаты уровня рабочей станции
      var workstation =
        GetBy(x => x.Name == workstationName && x.PointDistributionPolicy.Id == user.PointDistributionPolicyId)
          .FirstOrDefault();

      SertificateUec sert = null;
      if (workstation != null)
      {
        sert =
          sertificateUecManager.GetBy(
                                      x =>
                                      x.IsActive && x.Version == version && x.Workstation.Id == workstation.Id
                                      && x.Type.Id == type).FirstOrDefault();
        if (sert != null)
        {
          return sert.Key;
        }
      }

      // Ищем сертификаты уровня СМО
      if (user.PointDistributionPolicyId != null)
      {
        var smo = organisationManager.GetById(user.PointDistributionPolicyId.Value);
        sert =
          sertificateUecManager.GetBy(
                                      x => x.IsActive && x.Version == version && x.Smo.Id == smo.Id && x.Type.Id == type)
                               .FirstOrDefault();
      }

      return sert != null ? sert.Key : null;
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
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var sertificateUecManager = ObjectFactory.GetInstance<ISertificateUecManager>();
      var level2 = ObjectFactory.GetInstance<IOrganisationManager>().GetBy(x => x.Code == pdpCode).FirstOrDefault();
      if (level2 != null)
      {
        var workstation =
          session.QueryOver<Workstation>()
                 .Where(x => x.Name == workstationName && x.PointDistributionPolicy.Id == level2.Id)
                 .List()
                 .FirstOrDefault();

        SertificateUec sert;
        if (workstation != null)
        {
          sert =
            sertificateUecManager.GetBy(
                                        x =>
                                        x.IsActive && x.Version == version && x.Workstation.Id == workstation.Id
                                        && x.Type.Id == type).FirstOrDefault();
          if (sert != null)
          {
            return sert.Key;
          }
        }

        if (level2.Parent != null)
        {
          sert =
            sertificateUecManager.GetBy(
                                        x =>
                                        x.IsActive && x.Version == version && x.Smo.Id == level2.Parent.Id
                                        && x.Type.Id == type).FirstOrDefault();
          return sert != null ? sert.Key : null;
        }
      }

      return null;
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
      var user = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      var workstation =
        GetBy(x => x.Name == workstationName && x.PointDistributionPolicy.Id == user.PointDistributionPolicyId)
          .FirstOrDefault();
      return
        (workstation != null && workstation.UecCerticateType != null
           ? (CryptographyType)workstation.UecCerticateType.Value
           : CryptographyType.GOST).GetHashCode();
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
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Organisation pdp = null;
      var workstation =
        session.QueryOver<Workstation>()
               .JoinAlias(x => x.PointDistributionPolicy, () => pdp)
               .Where(x => x.Name == workstationName && pdp.Code == pdpCode)
               .List()
               .FirstOrDefault();
      return
        (workstation != null && workstation.UecCerticateType != null
           ? (CryptographyType)workstation.UecCerticateType.Value
           : CryptographyType.GOST).GetHashCode();
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
    public string GetCurrentReaderName(string worstationName)
    {
      var user = ObjectFactory.GetInstance<ISecurityService>().GetCurrentUser();
      var workstation =
        GetBy(x => x.Name == worstationName && x.PointDistributionPolicy.Id == user.PointDistributionPolicyId)
          .FirstOrDefault();
      return workstation != null && !string.IsNullOrEmpty(workstation.UecReaderName)
               ? workstation.UecReaderName
               : string.Empty;
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
    public string GetCurrentReaderName(string worstationName, string pdpCode)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Organisation pdp = null;
      var workstation =
        session.QueryOver<Workstation>()
               .JoinAlias(x => x.PointDistributionPolicy, () => pdp)
               .Where(x => x.Name == worstationName && pdp.Code == pdpCode)
               .List()
               .FirstOrDefault();
      return workstation != null && !string.IsNullOrEmpty(workstation.UecReaderName)
               ? workstation.UecReaderName
               : string.Empty;
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
    public string GetCurrentSmcReaderName(string worstationName)
    {
      var user = ObjectFactory.GetInstance<ISecurityService>().GetCurrentUser();
      var workstation =
        GetBy(x => x.Name == worstationName && x.PointDistributionPolicy.Id == user.PointDistributionPolicyId)
          .FirstOrDefault();
      return workstation != null && !string.IsNullOrEmpty(workstation.SmardCardReaderName)
               ? workstation.SmardCardReaderName
               : string.Empty;
    }

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
    public string GetCurrentSmcReaderName(string workstationName, string pdpCode)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Organisation pdp = null;
      var workstation =
        session.QueryOver<Workstation>()
               .JoinAlias(x => x.PointDistributionPolicy, () => pdp)
               .Where(x => x.Name == workstationName && pdp.Code == pdpCode)
               .List()
               .FirstOrDefault();
      return workstation != null && !string.IsNullOrEmpty(workstation.SmardCardReaderName)
               ? workstation.SmardCardReaderName
               : string.Empty;
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
    public string GetCurrentSmcTokenReaderName(string worstationName)
    {
      var user = ObjectFactory.GetInstance<ISecurityService>().GetCurrentUser();
      var workstation =
        GetBy(x => x.Name == worstationName && x.PointDistributionPolicy.Id == user.PointDistributionPolicyId)
          .FirstOrDefault();
      return workstation != null && !string.IsNullOrEmpty(workstation.SmardCardTokenReaderName)
               ? workstation.SmardCardTokenReaderName
               : string.Empty;
    }

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
    public string GetCurrentSmcTokenReaderName(string workstationName, string pdpCode)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Organisation pdp = null;
      var workstation =
        session.QueryOver<Workstation>()
               .JoinAlias(x => x.PointDistributionPolicy, () => pdp)
               .Where(x => x.Name == workstationName && pdp.Code == pdpCode)
               .List()
               .FirstOrDefault();
      return workstation != null && !string.IsNullOrEmpty(workstation.SmardCardTokenReaderName)
               ? workstation.SmardCardTokenReaderName
               : string.Empty;
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
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Workstation workstation = null;
      Organisation pdp = null;
      var settings =
        session.QueryOver<Setting>()
               .JoinAlias(x => x.Workstation, () => workstation)
               .JoinAlias(() => workstation.PointDistributionPolicy, () => pdp)
               .Where(() => workstation.Name == workstationName && pdp.Code == pdpCode)
               .List();

      return settings.Select(s => new WorkstationSettingParameter { Name = s.Name, Value = s.ValueString }).ToArray();
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
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Organisation pdp = null;
      var workstation =
        session.QueryOver<Workstation>()
               .JoinAlias(x => x.PointDistributionPolicy, () => pdp)
               .Where(x => x.Name == workstationName && pdp.Code == pdpCode)
               .List()
               .FirstOrDefault();
      if (workstation != null)
      {
        workstation.UecReaderName = readerName;
      }

      session.SaveOrUpdate(workstation);
      session.Flush();
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
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Organisation pdp = null;
      var workstation =
        session.QueryOver<Workstation>()
               .JoinAlias(x => x.PointDistributionPolicy, () => pdp)
               .Where(x => x.Name == workstationName && pdp.Code == pdpCode)
               .List()
               .FirstOrDefault();
      if (workstation != null)
      {
        workstation.SmardCardReaderName = readerName;
      }

      session.SaveOrUpdate(workstation);
      session.Flush();
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
    public void SaveWorkstationSettings(
      string workstationName, 
      string pdpCode, 
      WorkstationSettingParameter[] settingsArr)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // Получаем рабочую станцию 
      Organisation pdp = null;
      var workstation =
        session.QueryOver<Workstation>()
               .JoinAlias(x => x.PointDistributionPolicy, () => pdp)
               .Where(x => x.Name == workstationName && pdp.Code == pdpCode)
               .List()
               .FirstOrDefault();

      if (workstation == null)
      {
        return;
      }

      var transaction = session.BeginTransaction();
      try
      {
        foreach (var dtoSetting in settingsArr)
        {
          // Проверяем наличие указанного параметра в таблице
          var setting =
            session.QueryOver<Setting>()
                   .Where(x => x.Name == dtoSetting.Name && x.Workstation != null && x.Workstation.Id == workstation.Id)
                   .List()
                   .FirstOrDefault();
          if (setting == null)
          {
            setting = new Setting(); // создаем новую запись
            setting.Workstation = workstation;
          }

          setting.Name = dtoSetting.Name;
          setting.ValueString = dtoSetting.Value;
          session.SaveOrUpdate(setting);
        }

        session.Flush();
        transaction.Commit();
      }
      catch (Exception)
      {
        transaction.Dispose();
        session.Clear();
        throw;
      }
    }

    #endregion
  }
}