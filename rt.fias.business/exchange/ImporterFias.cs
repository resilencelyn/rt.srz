// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterFias.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.fias.business.exchange
{
  using System;
  using System.IO;

  using NHibernate;

  using Quartz;

  using rt.core.business.nhibernate;
  using rt.core.business.server.exchange.import;

  using SQLXMLBULKLOADLib;

  using StructureMap;

  /// <summary>
  ///   The importer fias.
  /// </summary>
  public abstract class ImporterFias : ImporterFile
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ImporterFias" /> class.
    /// </summary>
    public ImporterFias()
      : base(255)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The processing.
    /// </summary>
    /// <param name="file">
    /// The file.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool Processing(FileInfo file, IJobExecutionContext context)
    {
      var objBl = new SQLXMLBulkLoad4Class();
      objBl.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NHibernateCfgFias.xml"].ConnectionString;
      objBl.BulkLoad = true;
      objBl.KeepIdentity = false;
      objBl.SchemaGen = true; // создать пустую таблицу в БД
      objBl.SGDropTables = false; // если таблица существует, удалить её и создать заново

      DeleteData();
      Execute(objBl, file.FullName);

      return true;
    }

    /// <summary>
    /// The undo batches.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    public override void UndoBatches(string fileName)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// The close s ession.
    /// </summary>
    /// <param name="session">
    /// The session.
    /// </param>
    protected void CloseSession(ISession session)
    {
      var sessionFactory = ObjectFactory.GetInstance<IManagerSessionFactorys>()
                                        .GetFactoryByName("NHibernateCfgFias.xml");
      if (sessionFactory != null)
      {
        session.Close();
        session.Dispose();
      }
    }

    /// <summary>
    /// The delete data.
    /// </summary>
    protected abstract void DeleteData();

    /// <summary>
    /// The execute.
    /// </summary>
    /// <param name="objBl">
    /// The obj bl.
    /// </param>
    /// <param name="fileName">
    /// The file Name.
    /// </param>
    protected abstract void Execute(SQLXMLBulkLoad4Class objBl, string fileName);

    /// <summary>
    ///   The get session.
    /// </summary>
    /// <returns>
    ///   The <see cref="ISession" />.
    /// </returns>
    protected ISession GetSession()
    {
      var sessionFactory = ObjectFactory.GetInstance<IManagerSessionFactorys>()
                                        .GetFactoryByName("NHibernateCfgFias.xml");
      ISession session = null;
      if (sessionFactory != null)
      {
        session = sessionFactory.OpenSession();
      }

      if (session == null)
      {
        session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      }

      return session;
    }

    /// <summary>
    /// The undo batch.
    /// </summary>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    protected override bool UndoBatch(Guid batch)
    {
      return true;
    }

    #endregion
  }
}