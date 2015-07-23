// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WatchersJob.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The watchers job.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.directorywatcher.watch
{
  #region

  using System.Collections.Generic;
  using System.IO;
  using System.Linq;

  using Quartz;

  using rt.core.model.configuration;
  using rt.core.model.configuration.target;

  #endregion

  /// <summary>
  ///   The watchers job.
  /// </summary>
  public class WatchersJob : IJob
  {
    #region Static Fields

    /// <summary>
    ///   The _started watchers.
    /// </summary>
    private static readonly Dictionary<string, Watcher> StartedWatchers = new Dictionary<string, Watcher>();

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The execute.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public void Execute(IJobExecutionContext context)
    {
      var exchangeSettings = ConfigManager.ExchangeSettings;
      if (exchangeSettings == null || exchangeSettings.DirectoryWatchCollection == null
          || exchangeSettings.DirectoryWatchCollection.Count == 0)
      {
        context.Scheduler.PauseTrigger(context.Trigger.Key);
        return;
      }

      lock (StartedWatchers)
      {
        var directoryWatchElements = exchangeSettings.DirectoryWatchCollection.Cast<DirectoryWatchElement>().ToList();

        // находим разность множеств того что в конфиге, и того что уже запущено
        var except = directoryWatchElements.Select(dir => dir.Path)
                                           .Select(Path.GetFullPath)
                                           .Except(StartedWatchers.Keys);

        // Если все прослушиватели запущены, то останавливаем триггер
        if (!except.Any())
        {
          context.Scheduler.PauseTrigger(context.Trigger.Key);
        }

        foreach (var path in directoryWatchElements.Select(dir => dir.Path).Select(Path.GetFullPath))
        {
          Watcher watcher;
          if (StartedWatchers.TryGetValue(path, out watcher) || !Directory.Exists(path))
          {
            continue;
          }

          watcher = new Watcher(path);
          StartedWatchers.Add(path, watcher);
          watcher.EnableRaisingEvents = true;
        }
      }
    }

    #endregion
  }
}