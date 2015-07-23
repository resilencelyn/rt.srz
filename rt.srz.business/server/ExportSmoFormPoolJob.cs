// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportSmoFormPoolJob.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The export smo form pool job.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.server
{
  using System;
  using System.Linq;

  using NHibernate;

  using Quartz;

  using rt.core.business.quartz;
  using rt.srz.business.manager;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  ///   The export smo form pool job.
  /// </summary>
  public class ExportSmoFormPoolJob : JobBase
  {
    #region Constants

    /// <summary>
    ///   Максимальное к-во сообщений в одном батче
    /// </summary>
    private const int MaxCountMessageInBatch = 5000;

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
      // Не срабатываем в рабочее время ТФ
      if (ObjectFactory.GetInstance<IOrganisationManager>().OffHours())
      {
        return;
      }

      // Ставим тригер на паузу, чтобы не плодить потоков, которые выпадут в ожидание по lock
      context.Scheduler.PauseTrigger(context.Trigger.Key);

      try
      {
        lock (ExportSmoPool.LockObject)
        {
          if (ExportSmoPool.Instance.Queue.Any() || ExportSmoPool.Instance.ExecutingList.Any())
          {
            return;
          }
        }

        // Создаем батчи в базе
        var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        var currentDate = DateTime.Now;
        var period = ObjectFactory.GetInstance<IPeriodManager>().GetPeriodByMonth(currentDate);
        session.Flush();
        ObjectFactory.GetInstance<IExecuteStoredManager>().CreateExportSmoBatches(period.Id, MaxCountMessageInBatch);

        // Строим очередь выгрузки RecList
        var recBatchList =
          session.QueryOver<Batch>()
                 .Where(
                        x =>
                        x.Subject.Id == ExchangeSubjectType.Smo && x.Type.Id == ExchangeFileType.Rec
                        && x.CodeConfirm.Id == CodeConfirm.CA)
                 .OrderBy(x => x.Sender)
                 .Asc.ThenBy(x => x.Receiver)
                 .Asc.ThenBy(x => x.Period)
                 .Asc.ThenBy(x => x.Number)
                 .Asc.List();

        lock (ExportSmoPool.LockObject)
        {
          foreach (var batch in recBatchList)
          {
            ExportSmoPool.Instance.Queue.Enqueue(new ExportSmoJobInfo { BatchId = batch.Id });
          }
        }

        // Строим очередь выгрузки OpList
        var operBatchList =
          session.QueryOver<Batch>()
                 .Where(
                        x =>
                        x.Subject.Id == ExchangeSubjectType.Tfoms && x.Type.Id == ExchangeFileType.Op
                        && x.CodeConfirm.Id == CodeConfirm.CA)
                 .OrderBy(x => x.Sender)
                 .Asc.ThenBy(x => x.Receiver)
                 .Asc.ThenBy(x => x.Period)
                 .Asc.ThenBy(x => x.Number)
                 .Asc.List();

        lock (ExportSmoPool.LockObject)
        {
          foreach (var batch in operBatchList)
          {
            ExportSmoPool.Instance.Queue.Enqueue(new ExportSmoJobInfo { BatchId = batch.Id });
          }
        }
      }
      catch (Exception ex)
      {
        logger.Fatal("Произошла ошибка расчета батчей для экспорта в СМО", ex);
        throw;
      }
      finally
      {
        context.Scheduler.ResumeTrigger(context.Trigger.Key);
      }
    }

    #endregion
  }
}