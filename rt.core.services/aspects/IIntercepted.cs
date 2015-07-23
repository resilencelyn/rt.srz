// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIntercepted.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Интерфейс объекта, который позволяет делать перехват вызовов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.aspects
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   Интерфейс объекта, который позволяет делать перехват вызовов
  /// </summary>
  public interface IIntercepted
  {
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
    /// Индекс аспекта, с которого нужно начинать цепочку
    /// </param>
    /// <returns>
    /// То, что вернул метод
    /// </returns>
    T InvokeInterceptors<T>(Func<T> targetMethod, int interceptorIndex);

    #endregion
  }
}