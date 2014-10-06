// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExporterToPvp.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The exporter to pvp.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.exchange.impl
{
  #region references

  using System;
  using System.IO;
  using System.Linq;

  using NHibernate;

  using NLog;

  using Quartz;

  using rt.atl.business.exchange.interfaces;
  using rt.atl.business.scripts;
  using rt.core.business.interfaces.exchange;
  using rt.core.business.nhibernate;
  using rt.core.business.server.exchange.export;
  using rt.core.model.configuration;
  using rt.srz.business.manager;
  using rt.srz.business.server;
  using rt.srz.model.HL7.smo;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The exporter to pvp.
  /// </summary>
  public class ExporterToPvp : ExchangeBase
  {
    #region Constants

    /// <summary>
    ///   Максимальное к-во сообщений в одном батче
    /// </summary>
    private const int MaxCountMessageInBatch = 5000;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ExporterToPvp"/> class.
    /// </summary>
    /// <param name="sessionFactoryPvp">
    /// The session factory pvp.
    /// </param>
    /// <param name="managerSessionFactorys">
    /// The manager Session Factorys.
    /// </param>
    public ExporterToPvp(ISessionFactory sessionFactoryPvp, IManagerSessionFactorys managerSessionFactorys)
      : base(ExchangeTypeEnum.ExportToPvp)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The run.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public override void Run(IJobExecutionContext context)
    {
      // Создаем бэкап 
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      try
      {
        var filePath = Path.Combine(
                                    ConfigManager.ExchangeSettings.BackupOutputFolder, 
                                    string.Format(
                                                  "{0}.{1}.bak", 
                                                  session.Connection.Database, 
                                                  DateTime.UtcNow.ToString("yyyy.MM.dd HH.mm")));
        var sql = @"BACKUP DATABASE {0}
                    TO DISK = '{1}'
                    WITH FORMAT,
                    MEDIANAME = 'Z_SQLServerBackups',
                    NAME = 'Full Backup of {2}';";
        var backupQuery =
          session.CreateSQLQuery(string.Format(sql, session.Connection.Database, filePath, session.Connection.Database));
        backupQuery.UniqueResult();
      }
      catch (Exception exception)
      {
        // logger.FatalException("В процессе создания бэкапа произошла ошибка", exception);
      }

      try
      {
        // 1. Синхронизируем в Атлантику
        var exporter = ObjectFactory.GetInstance<IExchangeFactory>().GetExporter(ExchangeTypeEnum.ExportToSrz).First();
        exporter.Run(context);

        // 2. Выгружаем операционый день
        // Создаем батчи в базе
        var currentDate = DateTime.Now;
        var period = ObjectFactory.GetInstance<IPeriodManager>().GetPeriodByMonth(currentDate);
        session.Flush();
        ObjectFactory.GetInstance<IExecuteStoredManager>().CreateExportSmoBatches(period.Id, MaxCountMessageInBatch);

        // Строим очередь выгрузки RecList
        var recBatchList =
          session.QueryOver<Batch>()
                 .Where(
                        x =>
                        x.Subject.Id == TypeSubject.Smo && x.Type.Id == TypeFile.Rec
                        && x.CodeConfirm.Id == CodeConfirm.CA)
                 .OrderBy(x => x.Sender)
                 .Asc.ThenBy(x => x.Receiver)
                 .Asc.ThenBy(x => x.Period)
                 .Asc.ThenBy(x => x.Number)
                 .Asc.List();

        // Запускаем выгрузки в паралеле, пусть система определяет сколько надо потоков на эту задачу
        foreach (var batch in recBatchList.AsParallel())
        {
          // Копируем в локальную переменную
          var batch1 = batch;

          // Получаем экспортер
          var eb =
            ObjectFactory.GetInstance<IExportBatchFactory<RECListType, RECType>>().GetExporter(ExportBatchType.SmoRec);

          // Запускаем выгрузку (теоретически это длжно работать в паралельном режиме в зависимости от возможностей системы)
          eb.BulkCreateAndExport(context, batch1.Id);
        }

        // Получаем имя БД Атлатники
        var atlDatabase = string.Empty;
        using (
          var sessionAtl =
            ObjectFactory.GetInstance<IManagerSessionFactorys>().GetFactoryByName("NHibernateCfgAtl.xml").OpenSession())
        {
          atlDatabase = sessionAtl.Connection.Database;

          // Включаем индексы
          var scriptIndexOn = string.Format("ALTER DATABASE [{0}] SET ARITHABORT ON", atlDatabase);
          sessionAtl.CreateSQLQuery(scriptIndexOn).UniqueResult();
          scriptIndexOn =
            @"IF not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PRZBUFT]') AND name = N'PK_PRZBUFT')
                  CREATE UNIQUE CLUSTERED INDEX [PK_PRZBUFT] ON [dbo].[PRZBUFT] ([ID] ASC) 
                  WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]";
          sessionAtl.CreateSQLQuery(scriptIndexOn).UniqueResult();
        }

        session.CreateSQLQuery(
                               string.Format(
                                             "ALTER DATABASE [{0}] SET RECOVERY SIMPLE WITH NO_WAIT", 
                                             session.Connection.Database)).UniqueResult();

        // Открываем транзакцию
        using (var pvpTransaction = session.BeginTransaction())
        {
          try
          {
            // Делаем очистку БД
            var scriptClear =
              ScriptResource.ClearSrz.Replace("rt_srz_empty", session.Connection.Database)
                            .Replace("atl_srz_empty", atlDatabase);
            session.CreateSQLQuery(scriptClear).SetTimeout(int.MaxValue).UniqueResult();

            // Запускаем скрипт
            var scriptInit = ScriptResource.Initialization.Replace("atl_srz_empty", atlDatabase);
            session.CreateSQLQuery(scriptInit).SetTimeout(int.MaxValue).UniqueResult();

            pvpTransaction.Commit();
          }
          catch (Exception ex)
          {
            pvpTransaction.Rollback();
            throw ex;
          }
          finally
          {
            // режем БД
            session.CreateSQLQuery("DBCC SHRINKFILE (N'srz_log', 0, TRUNCATEONLY)")
                   .SetTimeout(int.MaxValue)
                   .ExecuteUpdate();

            ////session.CreateSQLQuery("DBCC SHRINKDATABASE(N'rt_srz_empty')".Replace("rt_srz_empty", session.Connection.Database)).SetTimeout(int.MaxValue).ExecuteUpdate();
            using (
              var sessionAtl =
                ObjectFactory.GetInstance<IManagerSessionFactorys>()
                             .GetFactoryByName("NHibernateCfgAtl.xml")
                             .OpenSession())
            {
              // Отключаем индексы
              var scriptIndexOff = string.Format(
                                                 "ALTER DATABASE [{0}] SET ARITHABORT OFF", 
                                                 sessionAtl.Connection.Database);
              sessionAtl.CreateSQLQuery(scriptIndexOff).UniqueResult();
              scriptIndexOff = string.Format("DROP INDEX [PK_PRZBUFT] ON [dbo].[PRZBUFT] WITH ( ONLINE = OFF )");
              sessionAtl.CreateSQLQuery(scriptIndexOff).UniqueResult();
            }
          }
        }

        // Запускаем пересчет ключей, если требуется
        CalculateKeysPool.ReInit();
      }
      catch (Exception ex)
      {
        LogManager.GetCurrentClassLogger().FatalException("Ошибка синхронизации с АТЛ", ex);
      }
    }

    #endregion
  }
}