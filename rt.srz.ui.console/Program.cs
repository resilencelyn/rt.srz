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
  using rt.core.model.interfaces;
  using rt.core.services.client;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;
  using rt.srz.services.client.services;

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
        ////Bootstrapper.Bootstrap();
        ////var session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
        ////CurrentSessionContext.Bind(session);

        ////Logger.Info("Loggined to services...");

        ////if (!ConnectToServices.Connect("admin", "123456"))
        ////{
        ////  Logger.Fatal("Login or password failed!");
        ////  return;
        ////}

       //// var statementService = new AddressClient();

       //// var k = statementService.GetFirstLevelByTfoms("45000000000");

       ////Logger.Info("Run task...");

        var objBL = new SQLXMLBULKLOADLib.SQLXMLBulkLoad4Class();
        objBL.ConnectionString = "provider=SQLOLEDB.1; Data Source=DEVELOPER-09; Initial Catalog=rt_fias;User ID=pvp;Password=elianora";
        objBL.BulkLoad = true;
        objBL.KeepIdentity = false;
        objBL.SchemaGen = true;            //создать пустую таблицу в БД
        objBL.SGDropTables = false;         //если таблица существует, удалить её и создать заново

        ////objBL.Execute("D:\\xsd\\AS_ESTSTAT.xsd", "D:\\xsd\\AS_ESTSTAT.XML");
        ////objBL.Execute("D:\\xsd\\AS_ACTSTAT.xsd", "D:\\xsd\\AS_ACTSTAT.XML");
        objBL.Execute("D:\\xsd\\AS_ADDROBJ.xsd", "D:\\xsd\\AS_ADDROBJ.XML");
        ////objBL.Execute("D:\\xsd\\AS_CENTERST.xsd", "D:\\xsd\\AS_CENTERST.XML");
        ////objBL.Execute("D:\\xsd\\AS_CURENTST.xsd", "D:\\xsd\\AS_CURENTST.XML");
        objBL.Execute("D:\\xsd\\AS_HOUSE.xsd", "D:\\xsd\\AS_HOUSE.XML");

        objBL.Execute("D:\\xsd\\AS_HOUSEINT.xsd", "D:\\xsd\\AS_HOUSEINT.XML");
        ////objBL.Execute("D:\\xsd\\AS_HSTSTAT.xsd", "D:\\xsd\\AS_HSTSTAT.XML");
        ////objBL.Execute("D:\\xsd\\AS_INTVSTAT.xsd", "D:\\xsd\\AS_INTVSTAT.XML");
        ////objBL.Execute("D:\\xsd\\AS_LANDMARK.xsd", "D:\\xsd\\AS_LANDMARK.XML");
        ////objBL.Execute("D:\\xsd\\AS_NDOCTYPE.xsd", "D:\\xsd\\AS_NDOCTYPE.XML");
        ////objBL.Execute("D:\\xsd\\AS_NORMDOC.xsd", "D:\\xsd\\AS_NORMDOC.XML");
        ////objBL.Execute("D:\\xsd\\AS_OPERSTAT.xsd", "D:\\xsd\\AS_OPERSTAT.XML");
        ////objBL.Execute("D:\\xsd\\AS_SOCRBASE.xsd", "D:\\xsd\\AS_SOCRBASE.XML");
        ////objBL.Execute("D:\\xsd\\AS_STRSTAT.xsd", "D:\\xsd\\AS_STRSTAT.XML");
      }
      catch (Exception ex)
      {
        Logger.Fatal(ex);
      }

      Logger.Info("End press any key...");
      Console.Read();
    }

    #endregion
  }
}