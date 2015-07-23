// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Конфигуратор начальной загрузки
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model
{
  using System;

  using NLog;

  using Quartz;

  using StructureMap;

  /// <summary>
  ///   Конфигуратор начальной загрузки
  /// </summary>
  public class Bootstrapper
  {
    #region Static Fields

    /// <summary>
    ///   Запущен ли
    /// </summary>
    private static bool hasStarted;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Начальная загрузка
    /// </summary>
    public static void Bootstrap()
    {
      if (!hasStarted)
      {
        var bootstraper = new Bootstrapper();

        if (!SiteMode.IsOnline)
        {
          return;
        }

        bootstraper.BootstrapStructureMap();
        bootstraper.BootstrapSheduler();
        hasStarted = true;
      }
      else
      {
        Reset();
      }

      LogAssemblies();
    }

    /// <summary>
    ///   Перезапуск
    /// </summary>
    public static void Reset()
    {
      if (hasStarted)
      {
        ObjectFactory.ResetDefaults();
      }
      else
      {
        Bootstrap();
        hasStarted = true;
      }
    }

    /// <summary>
    /// The restart.
    /// </summary>
    public static void Restart()
    {
      ObjectFactory.Initialize(x => { x.PullConfigurationFromAppConfig = true; });
    }

    /// <summary>
    ///   Останавливает работу сервера приложений, посылает всем зависимым потокам сигнал завершения
    /// </summary>
    public static void Stop()
    {
      if (!SiteMode.IsOnline)
      {
        return;
      }

      // todo остановить все сервисы
      // todo почистить свой темп
      // todo отключить всех пользователей
      // todo почистить фантомные записи в БД в монопльном режиме
      var bootstraper = new Bootstrapper();
      bootstraper.StopShedulers();
      bootstraper.StopWcfService();
    }

    #endregion

    #region Methods

    /// <summary>
    /// The log assemblies.
    /// </summary>
    private static void LogAssemblies()
    {
      var currentDomain = AppDomain.CurrentDomain;
      var assems = currentDomain.GetAssemblies();

      LogManager.GetCurrentClassLogger().Info("List of assemblies loaded in current appdomain:");
      foreach (var assem in assems)
      {
        LogManager.GetCurrentClassLogger().Info(assem.ToString());
      }
    }

    /// <summary>
    ///   The bootstrap sheduler.
    /// </summary>
    private void BootstrapSheduler()
    {
      var schedulerFactory = ObjectFactory.TryGetInstance<ISchedulerFactory>();
      if (schedulerFactory != null)
      {
        // Создаем шедуллер
        var scheduler = schedulerFactory.GetScheduler();

        // Стартуем шедулер
        scheduler.Start();

        scheduler.ResumeAll();
      }
    }

    /// <summary>
    ///   Инициализация SM
    /// </summary>
    private void BootstrapStructureMap()
    {
      if (!hasStarted)
      {
        ObjectFactory.Initialize(x => { x.PullConfigurationFromAppConfig = true; });
      }
    }

    /// <summary>
    ///   The stop shedulers.
    /// </summary>
    private void StopShedulers()
    {
      var schedulerFactory = ObjectFactory.TryGetInstance<ISchedulerFactory>();
      if (schedulerFactory != null)
      {
        // Получаем все шедулеры
        foreach (var scheduler in schedulerFactory.AllSchedulers)
        {
          // получаем все запущеные работы
          var runingJob = scheduler.GetCurrentlyExecutingJobs();
          foreach (var context in runingJob)
          {
            // Останавливаем работы
            scheduler.Interrupt(context.JobDetail.Key);
          }

          // Выключаем шедуллер
          scheduler.Shutdown(true);
        }
      }
    }

    /// <summary>
    ///   The stop wcf service.
    /// </summary>
    private void StopWcfService()
    {
      // Получаем все хосты
      foreach (var service in ServiceRegistryManager.ServiceHosts)
      {
        // выключаем хост
        service.Shutdown(new TimeSpan(0, 0, 5, 0));
      }
    }

    #endregion
  }
}