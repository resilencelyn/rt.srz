// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateProxyInterceptorServer.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.aspects
{
  #region

  using System;
  using System.Diagnostics;

  using NHibernate;

  using rt.core.business.nhibernate;
  using rt.core.model;

  using StructureMap;

  #endregion

  /// <summary>
  ///   Аспект для логирования
  /// </summary>
  public class NHibernateProxyInterceptorServer : IMethodInterceptor
  {
    #region Fields

    /// <summary>
    /// The max depth.
    /// </summary>
    private readonly int maxDepth;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="NHibernateProxyInterceptorServer"/> class.
    /// </summary>
    /// <param name="maxDepth">
    /// The max depth.
    /// </param>
    public NHibernateProxyInterceptorServer(int maxDepth = 1)
    {
      this.maxDepth = maxDepth;
    }

    #endregion

    #region Public Methods and Operators

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
      // выполняем метод
      var result = invokeNext();

      // Возвращаемый объект, но если он Business то делаем ему Unproxy
      return result is Business
               ? result.UnproxyObjectTree(ObjectFactory.GetInstance<ISessionFactory>(), maxDepth)
               : result;
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
      // метод ничего не возвращает, поэтому и ничего делать не надо
      invokeNext();
    }

    #endregion
  }
}