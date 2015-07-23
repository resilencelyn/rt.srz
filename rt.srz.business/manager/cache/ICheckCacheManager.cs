// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICheckCacheManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The CheckCacheManager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.cache
{
  using System;

  using rt.core.business.nhibernate;
  using rt.srz.model.srz;

  /// <summary>
  ///   The CheckCacheManager interface.
  /// </summary>
  public interface ICheckCacheManager : IManagerCacheBaseT<Setting, Guid>
  {
    #region Public Methods and Operators

    /// <summary>
    /// Возвращает настройку по имени класса (ValidatorName = тип валидатора), которая записывается в поле name.
    /// </summary>
    /// <param name="className">
    /// The class Name.
    /// </param>
    /// <returns>
    /// The <see cref="Setting"/>.
    /// </returns>
    Setting GetByClassName(string className);

    /// <summary>
    /// Возвращает настройку по имени класса (ValidatorName = тип валидатора), которая записывается в поле name.
    ///   без учёта принадлежности текущего пользователя территориальному фонду пользователя для которого эта настройка есть в
    ///   базе
    /// </summary>
    /// <param name="className">
    /// The class Name.
    /// </param>
    /// <returns>
    /// The <see cref="Setting"/>.
    /// </returns>
    Setting GetByClassNameOnly(string className);

    /// <summary>
    /// Обновляет объект в кеше зачитав его из базы без учёта организации
    /// </summary>
    /// <param name="setting">
    /// The setting.
    /// </param>
    void UpdateAllowChangeCacheRecord(Setting setting);

    /// <summary>
    /// Обновляет объект в кеше зачитав его из базы данных
    /// </summary>
    /// <param name="setting">
    /// The setting.
    /// </param>
    void UpdateCacheRecord(Setting setting);

    #endregion
  }
}