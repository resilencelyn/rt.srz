// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateSession.cs" company="–усЅ»“ех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The n hibernate session.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.nhibernate
{
  #region references

  using System;

  using NHibernate;
  using NHibernate.Context;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The n hibernate session.
  /// </summary>
  public class NHibernateSession : INHibernateSession
  {
    #region Fields

    /// <summary>
    ///   The i session.
    /// </summary>
    protected ISession iSession = null;

    /// <summary>
    ///   The transaction.
    /// </summary>
    protected ITransaction transaction = null;

    /// <summary>
    ///   The _auto close session.
    /// </summary>
    private bool _autoCloseSession = true;

    /// <summary>
    ///   The _is disposed.
    /// </summary>
    private bool _isDisposed;

    /// <summary>
    ///   The _ref count.
    /// </summary>
    private int _refCount;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Finalizes an instance of the <see cref="NHibernateSession" /> class.
    /// </summary>
    ~NHibernateSession()
    {
      Dispose(true);
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether auto close session.
    /// </summary>
    public bool AutoCloseSession
    {
      get
      {
        return _autoCloseSession;
      }

      set
      {
        _autoCloseSession = value;
        if (_refCount == 0 && _autoCloseSession)
        {
          Close();
        }
      }
    }

    /// <summary>
    ///   Gets a value indicating whether has open transaction.
    /// </summary>
    public bool HasOpenTransaction
    {
      get
      {
        return transaction != null;
      }
    }

    /// <summary>
    ///   Gets a value indicating whether is open.
    /// </summary>
    public bool IsOpen
    {
      get
      {
        return iSession != null && iSession.IsOpen;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The begin transaction.
    /// </summary>
    public void BeginTransaction()
    {
      transaction = GetISession().BeginTransaction();
    }

    /// <summary>
    ///   The close.
    /// </summary>
    public void Close()
    {
      if (iSession == null)
      {
        return;
      }

      if (HasOpenTransaction)
      {
        RollbackTransaction();
      }

      if (iSession.IsOpen)
      {
        iSession.Close();
      }

      iSession.Dispose();
      iSession = null;
    }

    /// <summary>
    ///   The commit changes.
    /// </summary>
    public void CommitChanges()
    {
      if (HasOpenTransaction)
      {
        CommitTransaction();
      }
      else
      {
        iSession.Flush();
      }
    }

    /// <summary>
    ///   The commit transaction.
    /// </summary>
    public void CommitTransaction()
    {
      if (transaction == null)
      {
        return;
      }

      try
      {
        transaction.Commit();
        transaction.Dispose();
        transaction = null;
      }
      catch (HibernateException)
      {
        RollbackTransaction();
        throw;
      }
    }

    /// <summary>
    ///   The decrement ref count.
    /// </summary>
    public void DecrementRefCount()
    {
      _refCount--;
      if (_refCount == 0 && AutoCloseSession)
      {
        Close();
      }
    }

    /// <summary>
    ///   The dispose.
    /// </summary>
    public void Dispose()
    {
      Dispose(false);
    }

    /// <summary>
    ///   The get i session.
    /// </summary>
    /// <returns>
    ///   The <see cref="ISession" />.
    /// </returns>
    public ISession GetISession()
    {
      var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
      try
      {
        iSession = sessionFactory.GetCurrentSession();
        if (iSession.IsOpen)
        {
          return iSession;
        }

        ReopenSession();
        return iSession;
      }
      catch (Exception ex)
      {
        NLog.LogManager.GetCurrentClassLogger().Error(ex);
      }

      return null;
    }

    /// <summary>
    ///   The increment ref count.
    /// </summary>
    public void IncrementRefCount()
    {
      _refCount++;
    }

    /// <summary>
    /// The reopen session.
    /// </summary>
    public void ReopenSession()
    {
      var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
      iSession.Close();
      CurrentSessionContext.Unbind(sessionFactory);
      iSession.Dispose();

      // ќткрываем новую сессию
      iSession = sessionFactory.OpenSession();
      CurrentSessionContext.Bind(iSession);
    }

    /// <summary>
    ///   The rollback transaction.
    /// </summary>
    public void RollbackTransaction()
    {
      if (transaction == null)
      {
        return;
      }

      transaction.Rollback();
      transaction.Dispose();
      transaction = null;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The dispose.
    /// </summary>
    /// <param name="finalizing">
    /// The finalizing.
    /// </param>
    private void Dispose(bool finalizing)
    {
      if (!_isDisposed)
      {
        Close();

        if (!finalizing)
        {
          GC.SuppressFinalize(this);
        }

        _isDisposed = true;
      }
    }

    #endregion
  }
}