// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System.Configuration;
  using System.Linq;

  using NHibernate;

  using rt.core.business.security.interfaces;
  using rt.srz.business.manager.cache;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The SettingManager.
  /// </summary>
  public partial class SettingManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Добавляет в базу настройку о том что можно включать отключать проверку данного валидатора
    /// </summary>
    /// <param name="className">
    /// тип валидатора
    /// </param>
    public void AddAllowChangeSetting(string className)
    {
      var setting = new Setting();
      setting.Name = className;
      setting.ValueString = "0";
      SaveOrUpdate(setting);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
      ObjectFactory.GetInstance<ICheckCacheManager>().UpdateAllowChangeCacheRecord(setting);
    }

    /// <summary>
    /// Добавляет в базу настройку проверки о том что её не надо проверять с учётом организации
    /// </summary>
    /// <param name="className">
    /// The class Name.
    /// </param>
    public void AddSetting(string className)
    {
      var setting = new Setting { Name = className, ValueString = "0" };
      var user = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      setting.Organisation = user.GetTf();
      SaveOrUpdate(setting);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
      ObjectFactory.GetInstance<ICheckCacheManager>().UpdateCacheRecord(setting);
    }

    /// <summary>
    /// The get setting.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="Setting"/> .
    /// </returns>
    public Setting GetSetting(string name)
    {
      var curUser = ObjectFactory.GetInstance<ISecurityService>().GetCurrentUser();
      return GetBy(x => x.UserId == curUser.Id && x.Name == name).FirstOrDefault();
    }

    /// <summary>
    /// The get setting current user.
    /// </summary>
    /// <param name="nameSetting">
    /// The name setting.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string GetSettingCurrentUser(string nameSetting)
    {
      var user = ObjectFactory.GetInstance<ISecurityService>().GetCurrentUser();

      var sett =
        ObjectFactory.GetInstance<ISettingManager>()
                     .GetBy(x => x.UserId == user.Id && x.Name == nameSetting)
                     .FirstOrDefault();

      return sett != null ? sett.ValueString : ConfigurationManager.AppSettings[nameSetting];
    }

    /// <summary>
    /// Удаляет из базы настройку о том что можно включать отключать проверку данного валидатора
    /// </summary>
    /// <param name="className">
    /// тип валидатора
    /// </param>
    public void RemoveAllowChangeSetting(string className)
    {
      Delete(s => s.Name == className);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
      ObjectFactory.GetInstance<ICheckCacheManager>().UpdateAllowChangeCacheRecord(new Setting { Name = className });
    }

    /// <summary>
    /// Удаляет настройку из базы которую надо стало проверять
    /// </summary>
    /// <param name="className">
    /// The class Name.
    /// </param>
    public void RemoveSetting(string className)
    {
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      Delete(s => s.Name == className && s.Organisation.Id == currentUser.GetTf().Id);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
      ObjectFactory.GetInstance<ICheckCacheManager>()
                   .UpdateCacheRecord(new Setting { Name = className, UserId = currentUser.Id });
    }

    /// <summary>
    /// The set setting current user.
    /// </summary>
    /// <param name="nameSetting">
    /// The name setting.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    public void SetSettingCurrentUser(string nameSetting, string value)
    {
      var user = ObjectFactory.GetInstance<ISecurityService>().GetCurrentUser();

      var sett =
        ObjectFactory.GetInstance<ISettingManager>()
                     .GetBy(x => x.UserId == user.Id && x.Name == nameSetting)
                     .FirstOrDefault() ?? new Setting { Name = nameSetting, UserId = user.Id };

      sett.ValueString = value;

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      session.Save(sett);
      session.Flush();
    }

    #endregion
  }
}