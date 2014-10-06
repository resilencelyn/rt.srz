// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportSmoJob.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The export smo job.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.server
{
  using System;

  using Quartz;

  using rt.core.business.interfaces.exchange;
  using rt.core.business.quartz;
  using rt.core.business.server.exchange.export;
  using rt.srz.business.manager;
  using rt.srz.model.HL7.smo;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  /// The export smo job.
  /// </summary>
  public class ExportSmoJob : JobBase
  {
    #region Constants

    /// <summary>
    ///   Максимальное количество работ
    /// </summary>
    private const int MaxCountJob = 5;

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

      ExportSmoJobInfo jobInfo = null;
      lock (ExportSmoPool.LockObject)
      {
        // Если очередь пуста, то выходим
        if (ExportSmoPool.Instance.Queue.Count == 0)
        {
          return;
        }

        // Проверка максимально допустимого количества исполняемых работ
        if (ExportSmoPool.Instance.ExecutingList.Count >= MaxCountJob)
        {
          return;
        }

        jobInfo = ExportSmoPool.Instance.Queue.Dequeue();

        // помещаем задачу в лист исполнения
        ExportSmoPool.Instance.ExecutingList.Add(jobInfo);
      }

      try
      {
        // Получаем батч
        var batch = ObjectFactory.GetInstance<IBatchManager>().GetById(jobInfo.BatchId);
        switch (batch.Type.Id)
        {
          case TypeFile.Rec:
          {
            // Выгрузка для СМО(RecList)
            // Получаем экспортер
            var eb =
              ObjectFactory.GetInstance<IExportBatchFactory<RECListType, RECType>>().GetExporter(ExportBatchType.SmoRec);

            // Вызываем экспортер
            eb.BulkCreateAndExport(context, jobInfo.BatchId);
          }

            break;

            // case TypeFile.Op: // Выгрузка для ТФОМС(OpList)
            // {
            // // Получаем экспортер
            // var eb = ObjectFactory.GetInstance<IExportBatchFactory<OPListType, OPType>>().GetExporter(ExportBatchType.SmoOp);

            // // Вызываем экспортер
            // eb.BulkCreateAndExport(context);
            // }
            // break;
        }
      }
      catch (Exception ex)
      {
        logger.Fatal("Процедура экспорта завершилась завершилась аварийно", ex);
      }
      finally
      {
        // извлекаем задачу из листа исполнения
        lock (ExportSmoPool.LockObject)
        {
          ExportSmoPool.Instance.ExecutingList.Remove(jobInfo);
        }
      }
    }

    #endregion
  }
}