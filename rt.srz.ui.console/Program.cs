// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.console
{
  using System;
  using System.Linq;
  using System.Net.Mime;

  using NHibernate;
  using NHibernate.Context;

  using NLog;

  using rt.core.model;
  using rt.core.services.client;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  /// The program.
  /// </summary>
  internal class Program
  {
    /// <summary>
    ///   logger
    /// </summary>
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    #region Methods

    /// <summary>
    /// The main.
    /// </summary>
    /// <param name="args">
    /// The args.
    /// </param>
    private static void Main(string[] args)
    {
      try
      {
        Bootstrapper.Bootstrap();
        var session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
        CurrentSessionContext.Bind(session);

        Logger.Info("Loggined to services...");

        if (!ConnectToServices.Connect("admin", "123456"))
        {
          Logger.Fatal("Login or password failed!");
          return;
        }

        var statementService = ObjectFactory.GetInstance<IStatementService>();

        var statement = session.QueryOver<Statement>().Take(1).List().SingleOrDefault();

        statementService.SaveStatement(statement);

        Logger.Info("Run task...");
      }
      catch (Exception ex)
      {
        Logger.Fatal(ex);
      }

      Logger.Info("End press any key...");
      Console.ReadLine();
    }

    #endregion
  }
}