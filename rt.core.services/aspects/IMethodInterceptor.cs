// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMethodInterceptor.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Интерцептов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.aspects
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   Интерцептов
  /// </summary>
  public interface IMethodInterceptor
  {
    #region Public Methods and Operators

    /// <summary>
    /// Инвокер
    /// </summary>
    /// <typeparam name="T">
    /// T
    /// </typeparam>
    /// <param name="invokeNext">
    /// Метод вызова.
    /// </param>
    /// <param name="metod">
    /// Самый конечный метод.
    /// </param>
    /// <returns>
    /// Результат выполнения.
    /// </returns>
    T InvokeMethod<T>(Func<T> invokeNext, Func<T> metod);

    /// <summary>
    /// Инвокер
    /// </summary>
    /// <param name="invokeNext">
    /// Метод вызова.
    /// </param>
    /// <param name="metod">
    /// Самый конечный метод.
    /// </param>
    void InvokeMethod(Action invokeNext, Action metod);

    #endregion
  }
}