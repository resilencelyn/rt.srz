// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationToPvpJob.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace rt.atl.business.quartz
{
  #region

  using System;
  using System.Linq;
  using Quartz;
  using NHibernate;
  using StructureMap;
  using rt.atl.business.configuration;
  using rt.atl.model.interfaces.Service;
  using rt.core.business.quartz;
  using rt.atl.business.exchange.impl;
  using rt.srz.model.srz;
  using rt.srz.business.manager;
  

  #endregion

  /// <summary>
  /// The synchronization job.
  /// </summary>
  public class SynchronizationToSrzJob : JobBase
  {
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
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
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
      lock (SynchronizationToPvpJob.LockObject)
      {
        // Ставим тригер на паузу, чтобы не плодить потоков, которые выпадут в ожидание по lock
        context.Scheduler.PauseTrigger(context.Trigger.Key);
        try
        {
          // Сохраянем время начала синхронизации
          SaveSyncStartOrFinishTime("_Start");
          
          var atlServices = ObjectFactory.GetInstance<IAtlService>();
          if (ConfigManager.SynchronizationSettings.SynchronizationNsi)
          {
            atlServices.RunSinhronizeNsi(context);
          }

          if (ConfigManager.SynchronizationSettings.SynchronizationToSrz)
          {
            atlServices.RunExportToSrz(context);
          }

          // Сохраянем время начала синхронизации
          SaveSyncStartOrFinishTime("_Finish");
        }
        finally
        {
          context.Scheduler.ResumeTrigger(context.Trigger.Key);
        }
      }
    }

    #endregion
  }
}