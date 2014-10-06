// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateWcfContextInitializer.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ������������� ������ ����������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.nhibernate
{
  #region references

  using System.ServiceModel;
  using System.ServiceModel.Channels;
  using System.ServiceModel.Dispatcher;

  using NHibernate;
  using NHibernate.Context;

  using StructureMap;

  #endregion

  /// <summary>
  ///   ������������� ������ ����������
  /// </summary>
  public sealed class NHibernateWcfContextInitializer : IDispatchMessageInspector
  {
    #region Public Methods and Operators

    /// <summary>
    /// ������ ��������� �������
    /// </summary>
    /// <param name="request">
    /// ������
    /// </param>
    /// <param name="channel">
    /// �����
    /// </param>
    /// <param name="instanceContext">
    /// ��������
    /// </param>
    /// <returns>
    /// ������
    /// </returns>
    public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();

      // session.BeginTransaction(IsolationLevel.ReadCommitted);
      CurrentSessionContext.Bind(session);
      return null;
    }

    /// <summary>
    /// ����� ��������� �������
    /// </summary>
    /// <param name="reply">
    /// �����
    /// </param>
    /// <param name="correlationState">
    /// ������
    /// </param>
    public void BeforeSendReply(ref Message reply, object correlationState)
    {
      var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
      var session = CurrentSessionContext.Unbind(sessionFactory);
      session.Flush();

      ////if (session.Transaction.IsActive)
      ////{
      ////  if (reply.IsFault)
      ////  {
      ////    session.Transaction.Rollback();
      ////  }
      ////  else
      ////  {
      ////    session.Transaction.Commit();
      ////  }
      ////}
      session.Dispose();
    }

    #endregion
  }
}