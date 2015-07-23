// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationToPvpJob.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The synchronization job.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.quartz
{
  #region

  using System;
  using System.Globalization;
  using System.Linq;
  using System.Threading;

  using NHibernate;

  using Quartz;

  using rt.atl.model.configuration;
  using rt.atl.model.interfaces.Service;
  using rt.core.business.quartz;
  using rt.core.model;
  using rt.srz.business.manager;
  using rt.srz.model.srz;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The synchronization job.
  /// </summary>
  public class SynchronizationToPvpJob : JobBase
  {
    #region Static Fields

    /// <summary>
    ///   The lock object.
    /// </summary>
    private static string lockObject = "lock";

    /// <summary>
    /// Gets the lock object.
    /// </summary>
    public static string LockObject 
    {
      get
      {
        return lockObject;
      }
    }
    
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
      lock (LockObject)
      {
        try
        {
          // Останавливаем все триггеры
          context.Scheduler.PauseAll();

          // Ждем окончания всех работ
          var waitInterupt = false;
          while (!waitInterupt)
          {
            Thread.Sleep(1000);
            var executingJobs = context.Scheduler.GetCurrentlyExecutingJobs();
            if (executingJobs.Count == 1 && executingJobs.First().JobInstance == this)
            {
              waitInterupt = true;
            }
          }

          // Вырубаем сайт
          SiteMode.SetOfflineMode(CalculateOnlineDateTime());

          // Сохраянем время начала синхронизации
          SaveSyncStartOrFinishTime("_Start");

          var atlServices = ObjectFactory.GetInstance<IAtlService>();
          if (ConfigManager.SynchronizationSettings.SynchronizationNsi)
          {
            atlServices.RunSinhronizeNsi(context);
          }

          if (ConfigManager.SynchronizationSettings.SynchronizationToPvp)
          {
            atlServices.RunExportToPvp(context);
          }

          // Сохраянем время окончания синхронизации
          SaveSyncStartOrFinishTime("_Finish");
        }
        finally
        {
          // Запускаем снова все триггеры
          context.Scheduler.ResumeAll();

          // Переводим сайт в online
          SiteMode.SetOnlineMode();
        }
      }
    }

    /// <summary>
    ///   The calculate online date time.
    /// </summary>
    /// <returns>
    ///   The <see cref="string" />.
    /// </returns>
    private string CalculateOnlineDateTime()
    {
      var settingManager = ObjectFactory.GetInstance<ISettingManager>();

      var beginSyncTime = DateTime.Now;
      var syncTime2SrzBegin = settingManager.GetBy(x => x.Name == "ExporterToPvp_Start").FirstOrDefault();
      if (syncTime2SrzBegin != null)
      {
        DateTime.TryParse(syncTime2SrzBegin.ValueString, out beginSyncTime);
      }

      var endSyncTime = beginSyncTime.AddHours(2);
      var syncTime2SrzEnd = settingManager.GetBy(x => x.Name == "ExporterToPvp_Finish").FirstOrDefault();
      if (syncTime2SrzEnd != null)
      {
        DateTime.TryParse(syncTime2SrzEnd.ValueString, out endSyncTime);
      }

      var onlineDateTime = DateTime.Now + (endSyncTime - beginSyncTime);
      return string.Format(
                           "Дата: {0}, Время: {1}", 
                           onlineDateTime.ToShortDateString(), 
                           onlineDateTime.ToShortTimeString());
    }

    /// <summary>
    /// The save sync start or finish time.
    /// </summary>
    /// <param name="postfix">
    /// The postfix.
    /// </param>
    private void SaveSyncStartOrFinishTime(string postfix)
    {
      var syncTime = DateTime.Now;
      var settingManager = ObjectFactory.GetInstance<ISettingManager>();

      // Пишем информацию о времени завершения синхронизации из ПВП в СРЗ
      var syncTime2Srz = settingManager.GetBy(x => x.Name == "ExporterToSrz" + postfix).FirstOrDefault();
      if (syncTime2Srz == null)
      {
        syncTime2Srz = new Setting();
        syncTime2Srz.Name = "ExporterToSrz" + postfix;
      }

      syncTime2Srz.ValueString = syncTime.ToString();
      settingManager.SaveOrUpdate(syncTime2Srz);

      // Пишем информацию о времени завершения синхронизации из СРЗ в ПВП
      var syncTime2Pvp = settingManager.GetBy(x => x.Name == "ExporterToPvp" + postfix).FirstOrDefault();
      if (syncTime2Pvp == null)
      {
        syncTime2Pvp = new Setting();
        syncTime2Pvp.Name = "ExporterToPvp" + postfix;
      }

      syncTime2Pvp.ValueString = syncTime.ToString();
      settingManager.SaveOrUpdate(syncTime2Pvp);

      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    #endregion
  }
}