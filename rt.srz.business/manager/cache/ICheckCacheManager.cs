namespace rt.srz.business.manager.cache
{
  using rt.core.business.nhibernate;

  using System;

  using rt.srz.model.srz;

  public interface ICheckCacheManager : IManagerCacheBaseT<Setting, Guid>
  {
    /// <summary>
    /// Возвращает настройку по имени класса (ValidatorName = тип валидатора), которая записывается в поле name. 
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    Setting GetByClassName(string className);

    /// <summary>
    /// Возвращает настройку по имени класса (ValidatorName = тип валидатора), которая записывается в поле name. 
    /// без учёта принадлежности текущего пользователя территориальному фонду пользователя для которого эта настройка есть в базе
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    Setting GetByClassNameOnly(string className);

    /// <summary>
    /// Обновляет объект в кеше зачитав его из базы данных
    /// </summary>
    /// <param name="setting"></param>
    void UpdateCacheRecord(Setting setting);

    /// <summary>
    /// Обновляет объект в кеше зачитав его из базы без учёта организации
    /// </summary>
    /// <param name="setting"></param>
    void UpdateAllowChangeCacheRecord(Setting setting);

  }
}
