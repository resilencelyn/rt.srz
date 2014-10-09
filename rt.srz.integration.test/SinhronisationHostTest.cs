// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SinhronisationHostTest.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client.test
{
  using System;
  using System.Linq;

  using NHibernate;
  using NHibernate.Context;

  using NUnit.Framework;

  using rt.core.model;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  ///   The sinhronisation host test.
  /// </summary>
  [TestFixture]
  public class SinhronisationHostTest : UnitTestbase
  {
    #region Fields

    /// <summary>
    /// The statement service.
    /// </summary>
    private IStatementService statementService;

    /// <summary>
    /// The transaction.
    /// </summary>
    private ITransaction transaction;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the session.
    /// </summary>
    protected ISession session { get; set; }

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
      statementService = ObjectFactory.GetInstance<IStatementService>();
      transaction = session.BeginTransaction();
    }

    /// <summary>
    ///   The tear down.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
      if (transaction != null)
      {
        transaction.Rollback();
      }
    }

    /// <summary>
    /// The send statement.
    /// </summary>
    [Test]
    public void SendStatement()
    {
      try
      {
        var statement = session.QueryOver<Statement>().Take(1).List().SingleOrDefault();

        statementService.SaveStatement(statement);
      }
      catch (Exception ex)
      {
        Assert.Fail(ex.ToString());
      }
    }


    #endregion
  }
}