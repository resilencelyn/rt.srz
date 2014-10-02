// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationToPvpJob.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.quartz
{
  #region
  using System.Threading;
  using System.Linq;
  using Quartz;

  using rt.core.model;

  using StructureMap;
  using rt.atl.business.configuration;
  using rt.atl.model.interfaces.Service;
  using rt.core.business.quartz;
  using rt.srz.business.manager;
  using System;
  using rt.atl.business.exchange.impl;
  using rt.srz.model.srz;
  using NHibernate;
  

  #endregion

  /// <summary>
  /// The synchronization job.
  /// </summary>
  public class SynchronizationToPvpJob : JobBase
  {
    /// <summary>
    /// The lock object.
    /// </summary>
    public static string LockObject = "lock";

    #region Private Methods
    private void SaveSyncStartOrFinishTime(string postfix)
    {
      DateTime syncTime = DateTime.Now;
      var settingManager = ObjectFactory.GetInstance<ISettingManager>();

      // Пишем информацию о времени завершения синхронизации из ПВП в СРЗ
      var syncTime2Srz = settingManager.GetBy(x => x.Name == typeof(ExporterToSrz).FullName + postfix).FirstOrDefault();
      if (syncTime2Srz == null)
      {
        syncTime2Srz = new Setting();
        syncTime2Srz.Name = typeof(ExporterToSrz).FullName + postfix;
      }
      syncTime2Srz.ValueString = syncTime.ToString();
      settingManager.SaveOrUpdate(syncTime2Srz);

      // Пишем информацию о времени завершения синхронизации из СРЗ в ПВП
      var syncTime2Pvp = settingManager.GetBy(x => x.Name == typeof(ExporterToPvp).FullName + postfix).FirstOrDefault();
      if (syncTime2Pvp == null)
      {
        syncTime2Pvp = new Setting();
        syncTime2Pvp.Name = typeof(ExporterToPvp).FullName + postfix;
      }
      syncTime2Pvp.ValueString = syncTime.ToString();
      settingManager.SaveOrUpdate(syncTime2Pvp);

      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    private string CalculateOnlineDateTime()
    {
      var settingManager = ObjectFactory.GetInstance<ISettingManager>();

      DateTime beginSyncTime = DateTime.Now;
      var syncTime2SrzBegin = settingManager.GetBy(x => x.Name == typeof(ExporterToPvp).FullName + "_Start").FirstOrDefault();
      if (syncTime2SrzBegin != null)
        DateTime.TryParse(syncTime2SrzBegin.ValueString, out beginSyncTime);

      DateTime endSyncTime = beginSyncTime.AddHours(2);
      var syncTime2SrzEnd = settingManager.GetBy(x => x.Name == typeof(ExporterToPvp).FullName + "_Finish").FirstOrDefault();
      if (syncTime2SrzEnd != null)
        DateTime.TryParse(syncTime2SrzEnd.ValueString, out endSyncTime);

      DateTime onlineDateTime = DateTime.Now + (endSyncTime - beginSyncTime);
      return string.Format("Дата: {0}, Время: {1}", onlineDateTime.ToShortDateString(),
        onlineDateTime.ToShortTimeString());
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
          bool waitInterupt = false;
          while (!waitInterupt)
          {
            Thread.Sleep(1000);
            var executingJobs = context.Scheduler.GetCurrentlyExecutingJobs();
            if (executingJobs.Count == 1 && executingJobs.First().JobInstance == this)
              waitInterupt = true;
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

    #endregion
  }
}