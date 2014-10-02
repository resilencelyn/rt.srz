// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckCacheManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.cache
{
  #region

  using System;
  using System.Linq;

  using rt.core.business.nhibernate;
  using rt.core.business.security.interfaces;
  using rt.srz.model.srz;

  using StructureMap;

  #endregion

  /// <summary>
  /// The check cache manager.
  /// </summary>
  public class CheckCacheManager : ManagerCacheBaseT<Setting, Guid>, ICheckCacheManager
  {
    private Guid currentGuid;

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckCacheManager"/> class.
    /// </summary>
    /// <param name="repository">
    /// The repository. 
    /// </param>
    public CheckCacheManager(ISettingManager repository)
      : base(repository)
    {
      TimeSpan = new TimeSpan(1, 0, 0, 0);
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      currentGuid = currentUser.GetTf().Id;
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Возвращает настройку по имени класса (ValidatorName = тип валидатора), которая записывается в поле name.
    /// </summary>
    /// <param name="className">
    /// The class Name. 
    /// </param>
    /// <returns>
    /// The <see cref="Setting"/> . 
    /// </returns>
    public Setting GetByClassName(string className)
    {
      // в таблице Settings в поле Name записывается имя класса (например ValidatorPolisCertificate наследованного от Check)
      // если у пользователя который изменял настройку и у текущего пользователя совпадают тер фонды то это та настройка что нужна
      return Cache.SingleOrDefault(s => s.Organisation != null && (s.Name == className && s.Organisation.Id == currentGuid));
    }

    /// <summary>
    /// Возвращает настройку по имени класса (ValidatorName = тип валидатора), которая записывается в поле name. 
    ///   без учёта принадлежности текущего пользователя территориальному фонду пользователя для которого эта настройка есть в базе
    /// </summary>
    /// <param name="className">
    /// The class Name. 
    /// </param>
    /// <returns>
    /// The <see cref="Setting"/> . 
    /// </returns>
    public Setting GetByClassNameOnly(string className)
    {
      // в таблице Settings в поле Name записывается имя класса (например ValidatorPolisCertificate наследованного от Check)
      return GetBy(s => s.Name == className).SingleOrDefault();
    }

    /// <summary>
    /// Обновляет объект в кеше зачитав его из базы без учёта организации
    /// </summary>
    /// <param name="setting">
    /// The setting.
    /// </param>
    public void UpdateAllowChangeCacheRecord(Setting setting)
    {
      var newRecord = Repository.GetBy(s => s.Name == setting.Name).FirstOrDefault();
      var oldRecord = Cache.SingleOrDefault(s => s.Name == setting.Name);
      if (oldRecord == null)
      {
        if (newRecord != null)
        {
          Cache.Add(newRecord);
        }
      }
      else
      {
        var indx = Cache.IndexOf(oldRecord);
        if (newRecord != null)
        {
          Cache[indx] = newRecord;
        }
        else
        {
          Cache.RemoveAt(indx);
        }
      }
    }

    /// <summary>
    /// Обновляет объект в кеше зачитав его из базы данных
    /// </summary>
    /// <param name="setting">
    /// The setting.
    /// </param>
    public void UpdateCacheRecord(Setting setting)
    {
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      var newRecord = Repository.GetBy(s => s.Organisation.Id == currentUser.PointDistributionPolicy.Parent.Parent.Id && s.Name == setting.Name)
        .SingleOrDefault();
      var oldRecord = Cache.SingleOrDefault(s => s.Organisation != null && s.Organisation.Id == currentUser.GetTf().Id && s.Name == setting.Name);
      if (oldRecord == null)
      {
        if (newRecord != null)
        {
          Cache.Add(newRecord);
        }
      }
      else
      {
        var indx = Cache.IndexOf(oldRecord);
        if (newRecord != null)
        {
          Cache[indx] = newRecord;
        }
        else
        {
          Cache.RemoveAt(indx);
        }
      }
    }

    #endregion
  }
}