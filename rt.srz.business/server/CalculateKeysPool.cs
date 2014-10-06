// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalculateKeysPool.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The task calculating Pool.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.server
{
  #region

  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;

  using Quartz;

  using rt.core.business.interfaces.quartz;
  using rt.srz.business.manager;
  using rt.srz.business.quartz;
  using rt.srz.model.srz;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The task calculating Pool.
  /// </summary>
  public class CalculateKeysPool
  {
    #region Constants

    /// <summary>
    ///   The count in job.
    /// </summary>
    public const int CountInJob = 25000;

    #endregion

    #region Static Fields

    /// <summary>
    ///   The instance.
    /// </summary>
    private static CalculateKeysPool instance;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes static members of the <see cref="CalculateKeysPool" /> class.
    /// </summary>
    static CalculateKeysPool()
    {
      LockObject = "CalculatingKeysPool";
    }

    /// <summary>
    ///   Prevents a default instance of the <see cref="CalculateKeysPool" /> class from being created.
    /// </summary>
    private CalculateKeysPool()
    {
      Queue = new Queue<CalculateKeysJobInfo>();
      ExecutingList = new List<CalculateKeysJobInfo>();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the instance.
    /// </summary>
    public static CalculateKeysPool Instance
    {
      get
      {
        return instance ?? Init();
      }
    }

    /// <summary>
    ///   Gets the lock object.
    /// </summary>
    public static object LockObject { get; private set; }

    /// <summary>
    ///   Gets or sets the executing list.
    /// </summary>
    public List<CalculateKeysJobInfo> ExecutingList { get; private set; }

    /// <summary>
    ///   Gets the queue.
    /// </summary>
    public Queue<CalculateKeysJobInfo> Queue { get; private set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The re init.
    /// </summary>
    public static void ReInit()
    {
      instance = null;
    }

    /// <summary>
    /// Добавляет в очередь расчет пользователького ключа
    /// </summary>
    /// <param name="keyType">
    /// </param>
    public void AddJobForUserKey(SearchKeyType keyType)
    {
      // Получаем фабрику
      var schedulerFactory = ObjectFactory.TryGetInstance<ISchedulerFactory>();
      if (schedulerFactory == null)
      {
        return;
      }

      // Получаем шедулер
      var scheduler = schedulerFactory.AllSchedulers.FirstOrDefault();
      if (scheduler == null)
      {
        return;
      }

      // Считаем сколько надо посчитать ключей
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var count = session.QueryOver<Statement>().RowCount();
      var countJob = (count / CountInJob) + 1;

      lock (LockObject)
      {
        // Ставим на паузу тригер пересчета ключей
        var key = ObjectFactory.GetInstance<IQuartzInfoProvider>()
                               .GetTriggerKey(JobGroupNames.Service, JobNames.CalculatingKeys);
        if (key != null)
        {
          scheduler.PauseTrigger(key);
        }

        try
        {
          // очищаем очередь задач на расчет ключей этого типа
          var list = instance.Queue.Where(x => x.SearchKeyType.Id != keyType.Id).ToList();
          instance.Queue.Clear();
          foreach (var calculateJobInfo in list)
          {
            instance.Queue.Enqueue(calculateJobInfo);
          }

          // останавливаем все работы, выполняемые для указанного ключа
          var executingJobs = scheduler.GetCurrentlyExecutingJobs();
          foreach (
            var executionContext in
              executingJobs.Where(
                                  x =>
                                  x.JobDetail.JobDataMap.Contains(
                                                                  new KeyValuePair<string, object>(
                                                                    "Имя ключа", 
                                                                    keyType.Code))))
          {
            var res = scheduler.Interrupt(executionContext.FireInstanceId);
          }

          for (var i = 0; i < countJob; i++)
          {
            var jobInfo = new CalculateKeysJobInfo
                          {
                            IsStandard = false, 
                            From = (i * CountInJob) + 1, 
                            To = (i + 1) * CountInJob, 
                            SearchKeyType = keyType, 
                            IsDeleted = false
                          };
            instance.Queue.Enqueue(jobInfo);
          }
        }
        finally
        {
          scheduler.ResumeTrigger(key);
        }
      }
    }

    /// <summary>
    /// The add jobs for all keys.
    /// </summary>
    public void AddJobsForAllKeys()
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var keyManager = ObjectFactory.GetInstance<ISearchKeyTypeManager>();

      var count = session.QueryOver<Statement>().RowCount();
      var countJob = (count / CountInJob) + 1;

      // задачи для стандартных ключей
      if (keyManager.GetBy(x => !x.Recalculated && x.IsActive).Any(x => x.Tfoms == null))
      {
        for (var i = 0; i < countJob; i++)
        {
          var jobInfo = new CalculateKeysJobInfo
                        {
                          IsStandard = true, 
                          From = (i * CountInJob) + 1, 
                          To = (i + 1) * CountInJob, 
                          IsDeleted = false
                        };

          Queue.Enqueue(jobInfo);
        }
      }

      // задачи для пользовательских ключей
      var keyTypes = keyManager.GetBy(x => !x.Recalculated && x.IsActive).Where(x => x.Tfoms != null);
      countJob = (count / CountInJob) + 1;
      foreach (var keyType in keyTypes)
      {
        for (var i = 0; i < countJob; i++)
        {
          var jobInfo = new CalculateKeysJobInfo
                        {
                          IsStandard = false, 
                          From = (i * CountInJob) + 1, 
                          To = (i + 1) * CountInJob, 
                          SearchKeyType = keyType, 
                          IsDeleted = false
                        };

          Queue.Enqueue(jobInfo);
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    ///   The init.
    /// </summary>
    /// <returns> The <see cref="CalculateKeysPool" /> . </returns>
    private static CalculateKeysPool Init()
    {
      instance = new CalculateKeysPool();
      instance.AddJobsForAllKeys();
      return instance;
    }

    #endregion
  }
}