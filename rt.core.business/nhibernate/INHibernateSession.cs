// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INHibernateSession.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The NHibernateSession interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.nhibernate
{
  #region references

  using System;

  using NHibernate;

  #endregion

  /// <summary>
  ///   The NHibernateSession interface.
  /// </summary>
  public interface INHibernateSession : IDisposable
  {
    // Methods
    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether auto close session.
    /// </summary>
    bool AutoCloseSession { get; set; }

    /// <summary>
    ///   Gets a value indicating whether has open transaction.
    /// </summary>
    bool HasOpenTransaction { get; }

    /// <summary>
    ///   Gets a value indicating whether is open.
    /// </summary>
    bool IsOpen { get; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The begin transaction.
    /// </summary>
    void BeginTransaction();

    /// <summary>
    ///   The close.
    /// </summary>
    void Close();

    /// <summary>
    ///   The commit changes.
    /// </summary>
    void CommitChanges();

    /// <summary>
    ///   The commit transaction.
    /// </summary>
    void CommitTransaction();

    /// <summary>
    ///   The decrement ref count.
    /// </summary>
    void DecrementRefCount();

    /// <summary>
    ///   The get i session.
    /// </summary>
    /// <returns>
    ///   The <see cref="ISession" />.
    /// </returns>
    ISession GetISession();

    /// <summary>
    ///   The increment ref count.
    /// </summary>
    void IncrementRefCount();

    // Properties
    /// <summary>
    /// The reopen session.
    /// </summary>
    void ReopenSession();

    /// <summary>
    ///   The rollback transaction.
    /// </summary>
    void RollbackTransaction();

    #endregion
  }
}