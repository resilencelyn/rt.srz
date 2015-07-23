// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusinessTestsBase.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The scheck statement tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.tests
{
  #region references

  using NHibernate;
  using NHibernate.Context;

  using NUnit.Framework;

  using rt.core.model;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The scheck statement tests.
  /// </summary>
  [TestFixture]
  public class BusinessTestsBase
  {
    #region Fields

    /// <summary>
    ///   The session.
    /// </summary>
    private ISession session;

    #endregion

    #region Properties

    /// <summary>
    ///   Gets a value indicating whether is transaction.
    /// </summary>
    protected virtual bool IsTransaction
    {
      get
      {
        return true;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The set up.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
      Bootstrapper.Bootstrap();
      session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
      CurrentSessionContext.Bind(session);
      if (IsTransaction)
      {
        session.BeginTransaction();
      }
    }

    /// <summary>
    ///   The tear down.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
      if (IsTransaction)
      {
        session.Transaction.Rollback();
      }

      session.Close();
      session.Dispose();
    }

    #endregion
  }
}