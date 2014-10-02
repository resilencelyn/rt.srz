namespace rt.core.services.aspects
{
  using System;
  using System.Diagnostics;
  using System.Linq;

  using NHibernate;

  using rt.core.model;

  using StructureMap;

  using rt.core.business.nhibernate;

  /// <summary>
  ///   Аспект для логирования
  /// </summary>
  public class NHibernateProxyInterceptorClient : IMethodInterceptor
  {
    /// <summary>
    /// Инвокер
    /// </summary>
    /// <typeparam name="T">
    /// T 
    /// </typeparam>
    /// <param name="invokeNext">
    /// Метод вызова 
    /// </param>
    /// <param name="metod">
    /// Самый конечный метод 
    /// </param>
    /// <returns>
    /// Результат выполнения 
    /// </returns>
    public virtual T InvokeMethod<T>(Func<T> invokeNext, Func<T> metod)
    {
      var sessionFactory = ObjectFactory.TryGetInstance<ISessionFactory>();
      if (sessionFactory != null)
      {
        var session = sessionFactory.GetCurrentSession();

        // Делаем unproxy для параметров
        var fieldInfos = metod.Target.GetType().GetFields();
        foreach (var parameter in fieldInfos.Select(fieldInfo => fieldInfo.GetValue(metod.Target)).OfType<Business>().Where(session.Contains))
        {
          parameter.UnproxyObjectTree(sessionFactory, 1);
          ////session.Evict(parameter);
        }
      }

      // выполняем метод
      var result = invokeNext();

      return result;
    }

    /// <summary>
    /// Инвокер
    /// </summary>
    /// <param name="invokeNext">
    /// Метод вызова. 
    /// </param>
    /// <param name="metod">
    /// Самый конечный метод. 
    /// </param>
    [DebuggerStepThrough]
    public virtual void InvokeMethod(Action invokeNext, Action metod)
    {
      var sessionFactory = ObjectFactory.TryGetInstance<ISessionFactory>();
      if (sessionFactory != null)
      {
        var session = sessionFactory.GetCurrentSession();

        // Делаем unproxy для параметров
        var fieldInfos = metod.Target.GetType().GetFields();
        foreach (var parameter in fieldInfos.Select(fieldInfo => fieldInfo.GetValue(metod.Target)).OfType<Business>().Where(session.Contains))
        {
          parameter.UnproxyObjectTree(sessionFactory, 1);
          ////session.Evict(parameter);
        }
      }

      invokeNext();
    }
  }
}