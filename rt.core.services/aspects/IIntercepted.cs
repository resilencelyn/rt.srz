// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIntercepted.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ��������� �������, ������� ��������� ������ �������� �������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.aspects
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   ��������� �������, ������� ��������� ������ �������� �������
  /// </summary>
  public interface IIntercepted
  {
    #region Public Methods and Operators

    /// <summary>
    /// ����� ��������
    /// </summary>
    /// <typeparam name="T">
    /// ��� ����������
    /// </typeparam>
    /// <param name="targetMethod">
    /// ����� ��� ������
    /// </param>
    /// <param name="interceptorIndex">
    /// ������ �������, � �������� ����� �������� �������
    /// </param>
    /// <returns>
    /// ��, ��� ������ �����
    /// </returns>
    T InvokeInterceptors<T>(Func<T> targetMethod, int interceptorIndex);

    #endregion
  }
}