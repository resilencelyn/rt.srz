// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateWcfContextInitializer.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Инициализация сессий хибернейта
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
  ///   Инициализация сессий хибернейта
  /// </summary>
  public sealed class NHibernateWcfContextInitializer : IDispatchMessageInspector
  {
    #region Public Methods and Operators

    /// <summary>
    /// Начало обработки запроса
    /// </summary>
    /// <param name="request">
    /// Запрос
    /// </param>
    /// <param name="channel">
    /// Канал
    /// </param>
    /// <param name="instanceContext">
    /// Контекст
    /// </param>
    /// <returns>
    /// Ретурн
    /// </returns>
    public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();

      // session.BeginTransaction(IsolationLevel.ReadCommitted);
      CurrentSessionContext.Bind(session);
      return null;
    }

    /// <summary>
    /// Конец обработки запроса
    /// </summary>
    /// <param name="reply">
    /// Ответ
    /// </param>
    /// <param name="correlationState">
    /// Статус
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