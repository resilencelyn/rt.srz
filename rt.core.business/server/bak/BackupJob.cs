// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BackupJob.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The backup job.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.bak
{
  #region

  using System;
  using System.IO;
  using System.Threading;

  using NHibernate;
  using NHibernate.Dialect;

  using Quartz;

  using rt.core.business.quartz;
  using rt.core.model;
  using rt.core.model.configuration;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The backup job.
  /// </summary>
  public class BackupJob : JobBase
  {
    #region Static Fields

    /// <summary>
    ///   The lock object.
    /// </summary>
    private static string LockObject = "lock";

    #endregion

    #region Methods

    /// <summary>
    /// The execute impl.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    protected override void ExecuteImpl(IJobExecutionContext context)
    {
      // 4. ���� ����� ��������, ����� (��������� ���� �������������). 
      // 4.1 ��������� �� � ������ ����� �� ������� (����� ������� ��������� �������� ����� ����� � ���� ������������� �������) 
      // 4.2 ���� ��������� ��� ���������� ����� ������ 
      // 4.3 ������� ����� 
      lock (LockObject)
      {
        // ������ ������ �� �����, ����� �� ������� �������, ������� ������� � �������� �� lock
        context.Scheduler.PauseTrigger(context.Trigger.Key);
        try
        {
          if (ConfigManager.ExchangeSettings.DisconnectUsers)
          {
            // 4.1 ��������� �� � ������ ����� �� ������� (����� ������� ��������� �������� ����� ����� � ���� ������������� �������) 
            SiteMode.SetOfflineMode(context.JobRunTime.Add(new TimeSpan(0, 5, 0, 0)).ToString());
            context.JobDetail.JobDataMap["progress"] = 20;

            // 4.2 ���� ��������� ��� ���������� ����� ������ ����� ��� ������� ��������� ����� ����
            var schedulerFactory = ObjectFactory.GetInstance<ISchedulerFactory>();
            var scheduler = schedulerFactory.GetScheduler();

            // ������������� �������� (����� ����� �����)
            scheduler.PauseAll();

            while (true)
            {
              // ���� �������� ���� ������, �� ��� ���������� ������
              if (scheduler.GetCurrentlyExecutingJobs().Count == 1)
              {
                break;
              }

              Thread.Sleep(5000);
            }

            context.JobDetail.JobDataMap["progress"] = 40;
          }

          // 4.3 ������� ����� 
          var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

          var currentDataBase = session.GetSessionImplementation().Connection.Database;

          var dialect = session.GetSessionImplementation().Factory.Dialect;

          if (dialect is Oracle8iDialect)
          {
            var filePath = Path.Combine(
                                        ConfigManager.ExchangeSettings.BackupOutputFolder, 
                                        string.Format(
                                                      "{0}.{1}.dmp", 
                                                      currentDataBase, 
                                                      DateTime.UtcNow.ToString("yyyy.MM.dd HH.mm")));

            // TODO: get from config
            var userName = string.Empty;
            var password = string.Empty;
            var listener = string.Empty;
            var backupQuery =
              session.CreateSQLQuery(
                                     string.Format(
                                                   @"exp {0}/{1}@{2} file={3} consistent=y", 
                                                   userName, 
                                                   password, 
                                                   listener, 
                                                   filePath));
            backupQuery.UniqueResult();
          }
          else if (dialect is MsSql2000Dialect)
          {
            var filePath = Path.Combine(
                                        ConfigManager.ExchangeSettings.BackupOutputFolder, 
                                        string.Format(
                                                      "{0}.{1}.bak", 
                                                      currentDataBase, 
                                                      DateTime.UtcNow.ToString("yyyy.MM.dd HH.mm")));
            var sql = @"BACKUP DATABASE {0}
          TO DISK = '{1}'
          WITH FORMAT,
          MEDIANAME = 'Z_SQLServerBackups',
          NAME = 'Full Backup of {2}';";
            var backupQuery = session.CreateSQLQuery(string.Format(sql, currentDataBase, filePath, currentDataBase));
            backupQuery.UniqueResult();
          }

          context.JobDetail.JobDataMap["progress"] = 100;
          logger.Info("������� �������� ������ ������� ��������");
        }
        catch (Exception exception)
        {
          logger.FatalException("� �������� �������� ������ ��������� ������", exception);
        }
        finally
        {
          context.Scheduler.ResumeTrigger(context.Trigger.Key);

          // ������� ���� �� ��������� �������
          if (ConfigManager.ExchangeSettings.DisconnectUsers)
          {
            SiteMode.SetOnlineMode();
            var schedulerFactory = ObjectFactory.GetInstance<ISchedulerFactory>();
            var scheduler = schedulerFactory.GetScheduler();
            scheduler.ResumeAll();
          }
        }
      }
    }

    #endregion
  }
}