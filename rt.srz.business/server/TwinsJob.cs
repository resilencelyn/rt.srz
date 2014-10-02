// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerJob.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.server
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;

  using Quartz;

  using rt.core.business.quartz;
  using rt.srz.business.manager;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The calculating job.
  /// </summary>
  public class TwinsJob : JobBase
  {
    /// <summary>
    /// The lock object.
    /// </summary>
    private static string LockObject = "lock";

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
        // Ставим тригер на паузу, чтобы не плодить потоков, которые выпадут в ожидание по lock
        context.Scheduler.PauseTrigger(context.Trigger.Key);
        try
        {
          var executeStoredManager = ObjectFactory.GetInstance<IExecuteStoredManager>();
          executeStoredManager.FindTwins();  
        }
        finally
        {
          context.Scheduler.ResumeTrigger(context.Trigger.Key);
        }
      }
    }
  }
}