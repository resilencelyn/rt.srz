// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InterceptedBase.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ������� �����, ����������� �������� ������ �������������
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
  ///   ������� �����, ����������� �������� ������ �������������
  /// </summary>
  public abstract class InterceptedBase : IIntercepted
  {
    #region Fields

    /// <summary>
    ///   ������������
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
    /// ����� ��������
    /// </summary>
    /// <typeparam name="T">
    /// ��� ����������
    /// </typeparam>
    /// <param name="targetMethod">
    /// ����� ��� ������
    /// </param>
    /// <param name="interceptorIndex">
    /// ������ �������
    /// </param>
    /// <returns>
    /// ��, ��� ������ �����
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
    /// ����� ��������
    /// </summary>
    /// <param name="targetMethod">
    /// ����� ��� ������
    /// </param>
    /// <param name="interceptorIndex">
    /// ������ �������
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