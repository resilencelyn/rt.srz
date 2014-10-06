// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMethodInterceptor.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   �����������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.aspects
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   �����������
  /// </summary>
  public interface IMethodInterceptor
  {
    #region Public Methods and Operators

    /// <summary>
    /// �������
    /// </summary>
    /// <typeparam name="T">
    /// T
    /// </typeparam>
    /// <param name="invokeNext">
    /// ����� ������.
    /// </param>
    /// <param name="metod">
    /// ����� �������� �����.
    /// </param>
    /// <returns>
    /// ��������� ����������.
    /// </returns>
    T InvokeMethod<T>(Func<T> invokeNext, Func<T> metod);

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="invokeNext">
    /// ����� ������.
    /// </param>
    /// <param name="metod">
    /// ����� �������� �����.
    /// </param>
    void InvokeMethod(Action invokeNext, Action metod);

    #endregion
  }
}