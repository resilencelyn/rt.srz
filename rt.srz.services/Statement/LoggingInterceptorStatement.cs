// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingInterceptorStatement.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The logging interceptor statement.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Statement
{
  #region

  using System;

  using rt.core.services.aspects;
  using rt.srz.model.logicalcontrol;

  #endregion

  /// <summary>
  ///   The logging interceptor statement.
  /// </summary>
  public class LoggingInterceptorStatement : LoggingInterceptor
  {
    #region Methods

    /// <summary>
    /// The on error.
    /// </summary>
    /// <param name="target">
    /// The target.
    /// </param>
    /// <param name="sessionId">
    /// The session id.
    /// </param>
    /// <param name="ex">
    /// The ex.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    protected override void OnError<T>(Func<T> target, string sessionId, Exception ex)
    {
      if (ex is LogicalControlException)
      {
        logger.Info(ex.Message, ex);
      }
      else
      {
        base.OnError(target, sessionId, ex);
      }
    }

    /// <summary>
    /// The on error.
    /// </summary>
    /// <param name="target">
    /// The target.
    /// </param>
    /// <param name="sessionId">
    /// The session id.
    /// </param>
    /// <param name="ex">
    /// The ex.
    /// </param>
    protected override void OnError(Action target, string sessionId, Exception ex)
    {
      if (ex is LogicalControlException)
      {
        logger.Info(ex.Message, ex);
      }
      else
      {
        base.OnError(target, sessionId, ex);
      }
    }

    #endregion
  }
}