// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateProxyInterceptorServer.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
    ///   ������ ��� �����������
    /// </summary>
    public class NHibernateProxyInterceptorServer : IMethodInterceptor
    {
        #region Public Methods and Operators

        /// <summary>
        /// �������
        /// </summary>
        /// <typeparam name="T">
        /// T 
        /// </typeparam>
        /// <param name="invokeNext">
        /// ����� ������ 
        /// </param>
        /// <param name="metod">
        /// ����� �������� ����� 
        /// </param>
        /// <returns>
        /// ��������� ���������� 
        /// </returns>
        public virtual T InvokeMethod<T>(Func<T> invokeNext, Func<T> metod)
        {
            // ��������� �����
            var result = invokeNext();

            // ������������ ������, �� ���� �� Business �� ������ ��� Unproxy
            return result is Business
                       ? result.UnproxyObjectTree(ObjectFactory.GetInstance<ISessionFactory>(), 1)
                       : result;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="invokeNext">
        /// ����� ������. 
        /// </param>
        /// <param name="metod">
        /// ����� �������� �����. 
        /// </param>
        [DebuggerStepThrough]
        public virtual void InvokeMethod(Action invokeNext, Action metod)
        {
            // ����� ������ �� ����������, ������� � ������ ������ �� ����
            invokeNext();
        }

        #endregion
    }
}