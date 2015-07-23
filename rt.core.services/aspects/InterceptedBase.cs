// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InterceptedBase.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Базовый класс, реализующий механизм вызова перехватчиков
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.aspects
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   Базовый класс, реализующий механизм вызова перехватчиков
  /// </summary>
  public abstract class InterceptedBase : IIntercepted
  {
    #region Fields

    /// <summary>
    ///   Интерцепторы
    /// </summary>
    private readonly List<IMethodInterceptor> interceptors = new List<IMethodInterceptor> { new LoggingInterceptor() };

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the interceptors.
    /// </summary>
    [XmlIgnore]
    public List<IMethodInterceptor> Interceptors
    {
      get
      {
        return interceptors;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Вызов аспектов
    /// </summary>
    /// <typeparam name="T">
    /// Тип результата
    /// </typeparam>
    /// <param name="targetMethod">
    /// Метод для вызова
    /// </param>
    /// <param name="interceptorIndex">
    /// Индекс аспекта
    /// </param>
    /// <returns>
    /// То, что вернул метод
    /// </returns>
    public T InvokeInterceptors<T>(Func<T> targetMethod, int interceptorIndex = 0)
    {
      if (interceptorIndex >= Interceptors.Count)
      {
        return targetMethod();
      }

      var methodInterceptor = Interceptors[interceptorIndex];
      var index = interceptorIndex + 1;
      return methodInterceptor.InvokeMethod(() => InvokeInterceptors(targetMethod, index), targetMethod);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Вызов аспектов
    /// </summary>
    /// <param name="targetMethod">
    /// Метод для вызова
    /// </param>
    /// <param name="interceptorIndex">
    /// Индекс аспекта
    /// </param>
    protected void InvokeInterceptors(Action targetMethod, int interceptorIndex = 0)
    {
      if (interceptorIndex >= Interceptors.Count)
      {
        targetMethod();
        return;
      }

      var methodInterceptor = Interceptors[interceptorIndex];
      var index = interceptorIndex + 1;
      methodInterceptor.InvokeMethod(() => InvokeInterceptors(targetMethod, index), targetMethod);
    }

    #endregion
  }
}