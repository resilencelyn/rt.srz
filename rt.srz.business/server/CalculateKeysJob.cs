// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalculateKeysJob.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The calculating job.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.server
{
  #region

  using System;
  using System.Linq;
  using System.Threading;

  using NHibernate;
  using NHibernate.Context;

  using Quartz;

  using rt.core.business.quartz;
  using rt.srz.business.manager;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The calculating job.
  /// </summary>
  public class CalculateKeysJob : JobBase
  {
    #region Constants

    /// <summary>
    ///   The max count job.
    /// </summary>
    private const int MaxCountJob = 10;

    #endregion

    #region Fields

    /// <summary>
    ///   The interrupt event.
    /// </summary>
    private readonly ManualResetEvent interruptEvent = new ManualResetEvent(false);

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Запрос на прерывание работы
    /// </summary>
    public override void Interrupt()
    {
      // выставляем событие - запрос на прерывание работы
      interruptEvent.Set();
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
      var executeStoredManager = ObjectFactory.GetInstance<IExecuteStoredManager>();
      CalculateKeysJobInfo jobInfo;

      // Не срабатываем в рабочее время ТФ
      if (ObjectFactory.GetInstance<IOrganisationManager>().OffHours())
      {
        return;
      }

      lock (CalculateKeysPool.LockObject)
      {
        // Ставим тригер на паузу, чтобы не плодить потоков, которые выпадут в ожидание по lock
        context.Scheduler.PauseTrigger(context.Trigger.Key);
        try
        {
          // Если очередь пуста, то выходим
          if (CalculateKeysPool.Instance.Queue.Count == 0)
          {
            return;
          }

          // удаляем старые ключи
          try
          {
            DeleteKeysPrecalculate();
          }
          catch (Exception exception)
          {
            logger.FatalException(
                                  "Произошла ошибка удаления ключей поиска перед их пересчетом. Вычисление ключей не возможно!", 
                                  exception);
            CalculateKeysPool.Instance.Queue.Clear();
            return;
          }

          // Проверка максимально допустимого количества исполняемых работ
          if (CalculateKeysPool.Instance.ExecutingList.Count >= MaxCountJob)
          {
            return;
          }

          jobInfo = CalculateKeysPool.Instance.Queue.Dequeue();

          // Если по данному типу произошла ошибка, то пропускаем данный тип
          if (jobInfo != null && jobInfo.IsError)
          {
            return;
          }

          // помещаем задачу в лист исполнения
          CalculateKeysPool.Instance.ExecutingList.Add(jobInfo);
        }
        finally
        {
          context.Scheduler.ResumeTrigger(context.Trigger.Key);
        }
      }

      if (jobInfo == null)
      {
        return;
      }

      // асинхронный пересчет
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Action storedManagerCallAction = () =>
                                       {
                                         CurrentSessionContext.Bind(session);
                                         try
                                         {
                                           context.JobDetail.JobDataMap["progress"] = 20;
                                           context.JobDetail.JobDataMap["Имя ключа"] = jobInfo.IsStandard
                                                                                         ? "Стандартные"
                                                                                         : jobInfo.SearchKeyType.Code;
                                           context.JobDetail.JobDataMap["Интервал"] = string.Format(
                                                                                                    "{0} - {1}", 
                                                                                                    jobInfo.From, 
                                                                                                    jobInfo.To);

                                           // вызов хранимых процедур
                                           if (jobInfo.IsStandard)
                                           {
                                             executeStoredManager.CalculateStandardSearchKeys(jobInfo.From, jobInfo.To);
                                           }
                                           else
                                           {
                                             executeStoredManager.CalculateUserSearchKeys(
                                                                                          jobInfo.SearchKeyType.Id, 
                                                                                          jobInfo.From, 
                                                                                          jobInfo.To);
                                           }

                                           lock (CalculateKeysPool.LockObject)
                                           {
                                             context.Scheduler.PauseTrigger(context.Trigger.Key);
                                             session.CreateSQLQuery("DBCC SHRINKFILE (N'srz_log' , 0, TRUNCATEONLY)")
                                                    .SetTimeout(int.MaxValue)
                                                    .UniqueResult();
                                             context.Scheduler.ResumeTrigger(context.Trigger.Key);
                                           }

                                           context.JobDetail.JobDataMap["progress"] = 100;
                                         }
                                         catch (Exception exception)
                                         {
                                           if (exception is ADOException)
                                           {
                                             // обработка запроса на прерывание работы
                                             if (interruptEvent.WaitOne(0))
                                             {
                                               logger.FatalException(
                                                                     "Получен запрос на прерывание процедуры расчета ключей.", 
                                                                     exception);
                                               lock (CalculateKeysPool.LockObject)
                                               {
                                                 CalculateKeysPool.Instance.ExecutingList.Remove(jobInfo);
                                                 return;
                                               }
                                             }
                                           }

                                           if (!jobInfo.IsError)
                                           {
                                             logger.FatalException(
                                                                   "Произошла ошибка вызова процедуры расчета ключей. Задача будет помещена в очередь еще один раз.", 
                                                                   exception);
                                             lock (CalculateKeysPool.LockObject)
                                             {
                                               jobInfo.IsError = true;
                                               CalculateKeysPool.Instance.ExecutingList.Remove(jobInfo);
                                               CalculateKeysPool.Instance.Queue.Enqueue(jobInfo);
                                               return;
                                             }
                                           }

                                           logger.FatalException(
                                                                 "Произошла ошибка вызова процедуры расчета ключей. Расчет ключа данного типа прекращен. Все ключи данного типа будут удалены из базы данных.", 
                                                                 exception);
                                           lock (CalculateKeysPool.LockObject)
                                           {
                                             jobInfo.IsError = true;

                                             // Помечаем остальные типы данного типа как выполнение с ошибкой
                                             foreach (var info in
                                               CalculateKeysPool.Instance.ExecutingList.Where(
                                                                                              x =>
                                                                                              x.SearchKeyType.Id
                                                                                              == jobInfo.SearchKeyType
                                                                                                        .Id))
                                             {
                                               info.IsError = true;
                                             }

                                             foreach (var info in
                                               CalculateKeysPool.Instance.Queue.Where(
                                                                                      x =>
                                                                                      x.SearchKeyType.Id
                                                                                      == jobInfo.SearchKeyType.Id))
                                             {
                                               info.IsError = true;
                                             }
                                           }
                                         }
                                       };

      // асинхронный вызов процедуры пересчета ключей
      var asyncRes = storedManagerCallAction.BeginInvoke(null, null);

      // ожидание окончания процедуры пересчета
      while (!asyncRes.IsCompleted)
      {
        // запрос на прерывание расчета
        if (interruptEvent.WaitOne(0))
        {
          // закрываем соединение для окончания работы процедур пересчета
          session.CancelQuery();

          // session.Connection.Close();
        }

        // спим
        Thread.Sleep(1000);
      }

      // пересчет завершился
      lock (CalculateKeysPool.LockObject)
      {
        CalculateKeysPool.Instance.ExecutingList.Remove(jobInfo);
        if (jobInfo.IsError)
        {
          DeleteKeys(jobInfo.IsStandard ? null : (Guid?)jobInfo.SearchKeyType.Id);
        }
        else
        {
          // Если задание последнее то взводим флаг пересчитанности ключей
          SetRecalculated(jobInfo);
        }
      }
    }

    /// <summary>
    /// The delete keys.
    /// </summary>
    /// <param name="typeId">
    /// The type id.
    /// </param>
    private void DeleteKeys(Guid? typeId = null)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      if (typeId.HasValue)
      {
        var query = session.CreateSQLQuery("delete from [SearchKey] where KeyTypeId = :KeyType");
        query.SetTimeout(int.MaxValue).SetParameter("KeyType", typeId).UniqueResult();
      }
      else
      {
        var query =
          session.CreateSQLQuery(
                                 "delete from [SearchKey] where KeyTypeId in (select RowId from [SearchKeyType] where TfomsId is null)");
        query.SetTimeout(int.MaxValue).UniqueResult();
      }
    }

    /// <summary>
    ///   The delete keys precalculate.
    /// </summary>
    private void DeleteKeysPrecalculate()
    {
      // Удалить предыдущие стандартные ключи
      if (CalculateKeysPool.Instance.Queue.Any(x => x.IsStandard && !x.IsDeleted))
      {
        DeleteKeys();
        foreach (var info in CalculateKeysPool.Instance.Queue.Where(x => x.IsStandard && !x.IsDeleted))
        {
          info.IsDeleted = true;
        }
      }

      // Удалить пользовательские ключи
      if (CalculateKeysPool.Instance.Queue.Any(x => !x.IsStandard && !x.IsDeleted))
      {
        foreach (var info in
          CalculateKeysPool.Instance.Queue.Where(x => !x.IsStandard && !x.IsDeleted).GroupBy(x => x.SearchKeyType))
        {
          DeleteKeys(info.Key.Id);
        }

        foreach (var info in CalculateKeysPool.Instance.Queue.Where(x => !x.IsStandard && !x.IsDeleted))
        {
          info.IsDeleted = true;
        }
      }
    }

    /// <summary>
    /// The set recalculated.
    /// </summary>
    /// <param name="calculateJobInfo">
    /// The job info.
    /// </param>
    private void SetRecalculated(CalculateKeysJobInfo calculateJobInfo)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      if (calculateJobInfo.IsStandard)
      {
        if (CalculateKeysPool.Instance.ExecutingList.All(x => !x.IsStandard)
            && CalculateKeysPool.Instance.Queue.All(x => !x.IsStandard))
        {
          var searchKeyTypes = ObjectFactory.GetInstance<ISearchKeyTypeManager>().GetAll().Where(x => x.Tfoms == null);
          foreach (var type in searchKeyTypes)
          {
            type.Recalculated = true;
            session.SaveOrUpdate(type);
          }

          session.Flush();
        }
      }
      else
      {
        if (CalculateKeysPool.Instance.ExecutingList.All(x => x.SearchKeyType.Id != calculateJobInfo.SearchKeyType.Id)
            && CalculateKeysPool.Instance.Queue.All(x => x.SearchKeyType.Id != calculateJobInfo.SearchKeyType.Id))
        {
          calculateJobInfo.SearchKeyType.Recalculated = true;
          session.SaveOrUpdate(calculateJobInfo.SearchKeyType);
          session.Flush();
        }
      }
    }

    #endregion
  }
}