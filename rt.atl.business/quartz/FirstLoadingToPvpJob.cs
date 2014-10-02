using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using StructureMap;
using rt.atl.model.interfaces.Service;
using rt.core.business.quartz;


namespace rt.atl.business.quartz
{
  public class FirstLoadingToPvpJob : JobBase
  {
    #region Constants

    /// <summary>
    /// Максимальное количество работ
    /// </summary>
    private static int maxCountJob = 20;

    #endregion

    #region Properties
    public static int MaxCountJob
    {
      get
      {
        return maxCountJob;
      }
      set
      {
        maxCountJob = value;
      }
    }
    #endregion


    /// <summary>
    /// The execute impl.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    protected override void ExecuteImpl(IJobExecutionContext context)
    {
      FirstLoadingToPvpJobInfo jobInfo = null;
      lock (FirstLoadingToPvpJobPool.LockObject)
      {
        // Если очередь пуста, то выходим
        if (FirstLoadingToPvpJobPool.Instance.Queue.Count == 0)
          return;

        //Проверка максимально допустимого количества исполняемых работ
        if (FirstLoadingToPvpJobPool.Instance.ExecutingList.Count >= MaxCountJob)
          return;

        jobInfo = FirstLoadingToPvpJobPool.Instance.Queue.Dequeue();

        // помещаем задачу в лист исполнения
        FirstLoadingToPvpJobPool.Instance.ExecutingList.Add(jobInfo);
      }

      //запускаем задачу
      try
      {
        context.JobDetail.JobDataMap["Min"] = jobInfo.Min;
        context.JobDetail.JobDataMap["Max"] = jobInfo.Max;
        ObjectFactory.GetInstance<IAtlService>().RunFirstLoadingToPvp(context);
      }
      catch (Exception ex)
      {
        logger.FatalException("Процедура первичной загрузки завершилась аварийно", ex);
      }
      finally
      {
        // извлекаем задачу из листа исполнения
        lock (FirstLoadingToPvpJobPool.LockObject)
        {
          FirstLoadingToPvpJobPool.Instance.ExecutingList.Remove(jobInfo);
        } 
      }
    }
  }
}
