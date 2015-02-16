// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageTest.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.integration.test
{
  using System;
  using System.IO;

  using NHibernate;
  using NHibernate.Context;

  using NUnit.Framework;

  using rt.core.business.server.exchange.export;
  using rt.core.model;
  using rt.cs.business.request;
  using rt.srz.business.exchange.export;
  using rt.srz.business.manager;
  using rt.srz.integration.test;
  using rt.srz.model.Hl7.person;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  ///   The fias gate test.
  /// </summary>
  [TestFixture]
  public class MessageTest
  {
    #region Fields

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
    /// The set up.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
      Bootstrapper.Bootstrap();
      session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
      CurrentSessionContext.Bind(session);
      transaction = session.BeginTransaction();
    }

    /// <summary>
    ///   The tear down.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
      transaction.Rollback();
    }

    /// <summary>
    ///   The test Name.
    /// </summary>
    [Test]
    public void TestName()
    {
      // arrange
      var exporter = ObjectFactory.GetInstance<IExporterBatchFactory<PersonErp, BaseMessageTemplate>>().GetExporter(Exporters.ErpExporter);
      var messageFactory = ObjectFactory.GetInstance<IMessageFactory>();
      var messageExporter = messageFactory.GetExporter(ReasonType.П01);
      exporter.PeriodId = ObjectFactory.GetInstance<IPeriodManager>().GetPeriodByMonth(DateTime.Now).Id;
      exporter.ReceiverId = new Guid("11111111-0000-0000-0000-000000000000");
      exporter.SenderId = new Guid("24000000-0000-0000-0000-000000000000");
      var statement = GoodStatement.CreateGoodStatement();

      // action
      exporter.BeginBatch();
      var message = messageExporter.GetMessageTemplate(Guid.NewGuid(), statement, statement.MedicalInsurances[0]);
      exporter.AddNode(message);
      var file = exporter.GetFileNameFull();
      exporter.CommitBatch();

      // assert
      WaitFile(file);
    }

    private void WaitFile(string file)
    {
      // Ждем обработку шлюза
      var timeout = DateTime.Now + new TimeSpan(0, 0, 2, 0);
      while (File.Exists(file) && DateTime.Now <= timeout)
      {
        System.Threading.Thread.Sleep(100);
      }

      // Ждем обработку ЦС
      var fileInfo = new FileInfo(file);
      var f = Path.Combine(fileInfo.Directory.Parent.Parent.Parent.FullName, "DatInput", fileInfo.Name.Substring(0, fileInfo.Name.Length - 4) + ".dat");

      while (File.Exists(f) && DateTime.Now <= timeout)
      {
        System.Threading.Thread.Sleep(100);
      }
    }

    #endregion
  }
}