namespace CrystalQuartz.Core.Domain
{
  using System;
  using System.Collections.Generic;

  public class SchedulerData
  {
    public string Name { get; set; }

    public string Title { get; set; }

    public string InstanceId { get; set; }

    public bool IsStarted
    {
      get
      {
        return Status == SchedulerStatus.Started;
      }
    }

    public bool CanStart
    {
      get
      {
        return false;
      }
    }

    public bool CanShutdown
    {
      get
      {
        return false;
      }
    }

    public IList<JobGroupData> JobGroups { get; set; }

    public IList<TriggerGroupData> TriggerGroups { get; set; }

    public SchedulerStatus Status { get; set; }

    public string StatusDecription
    {
      get
      {
        switch (Status)
        {
          case SchedulerStatus.Empty:
            return "Пустой";
          case SchedulerStatus.Ready:
            return "Готов";
          case SchedulerStatus.Shutdown:
            return "Завершен";
          case SchedulerStatus.Started:
            return "Запущен";
          default:
            return null;
        }
      }
    }

    public int JobsTotal { get; set; }

    public int JobsExecuted { get; set; }

    public bool IsRemote { get; set; }

    public Type SchedulerType { get; set; }

    public DateTime? RunningSince { get; set; }
  }
}