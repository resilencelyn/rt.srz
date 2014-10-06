// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Watcher.cs" company="–усЅ»“ех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The watcher.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.directorywatcher.watch
{
  using System.IO;

  using rt.core.business.interfaces.directorywatcher;
  using rt.core.business.server.directorywatcher.processing;

  /// <summary>
  ///   The watcher.
  /// </summary>
  public class Watcher : FileSystemWatcher, IWatcher
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Watcher"/> class.
    ///   »нициализаци€ нового экземпл€ра типа <see cref="Watcher"/>.
    /// </summary>
    /// <param name="path">
    /// The path.
    /// </param>
    public Watcher(string path)
      : base(path, "*.*")
    {
      // помещаем в очередь
      lock (ProcessingPool.Instance)
      {
        foreach (var file in Directory.GetFiles(path))
        {
          ProcessingPool.Instance.QueueFiles.Enqueue(file);
        }
      }

      Created += WatcherCreated;
      Changed += WatcherCreated;
      NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName
                     | NotifyFilters.DirectoryName;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The watcher created.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void WatcherCreated(object sender, FileSystemEventArgs e)
    {
      // ‘айл был создан
      if (e.ChangeType != WatcherChangeTypes.Created)
      {
        return;
      }

      // помещаем в очередь
      lock (ProcessingPool.Instance)
      {
        ProcessingPool.Instance.QueueFiles.Enqueue(e.FullPath);
      }
    }

    #endregion
  }
}