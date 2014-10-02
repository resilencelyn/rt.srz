// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessingFilesJob.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.directorywatcher.processing
{
  #region

  using System;
  using System.IO;
  using System.Linq;

  using NLog;

  using Quartz;

  using rt.core.business.configuration;
  using rt.core.business.interfaces.directorywatcher;
  using rt.core.business.quartz;
  using rt.core.business.server.directorywatcher.processing.target;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The processing files.
  /// </summary>
  public class ProcessingFilesJob : JobBase
  {

    private int MaxProc = 20;

    #region Methods

    /// <summary>
    /// The execute.
    /// </summary>
    /// <param name="context">
    /// The context. 
    /// </param>
    protected override void ExecuteImpl(IJobExecutionContext context)
    {
      IImporterFile importer = null;
      BatchInfo batchInfo;

      // Ставим блокировку на работу с очередями
      lock (ProcessingPool.Instance.QueueFiles)
      {
        // Ставим тригер на паузу, чтобы не плодить потоков, которые выпадут в ожидание по lock
        context.Scheduler.PauseTrigger(context.Trigger.Key);
        try
        {
          // Обработаем очередь файлов без приоритета и поставим их в очередь с приоритетом
          while (ProcessingPool.Instance.QueueFiles.Any())
          {
            // Сначала проверим сформирован ли файл окончательно, или его еще не записали
            var file = new FileInfo(ProcessingPool.Instance.QueueFiles.Peek());
            if (!FileCanBeRead(file))
            {
              continue;
            }

            // Удалим его из очереди
            ProcessingPool.Instance.QueueFiles.Dequeue();

            // Определяем приоритет
            var priority = GetPriority(file);

            // Помещаем файл на обработку, но уже с приоритетом
            ProcessingPool.Instance.Queue.Enqueue(new BatchInfo { FileInfo = file, Priority = priority }, priority);
          }

          if (ProcessingPool.Instance.ProsessingFiles.Count >= MaxProc)
          {
            return;
          }

          // Берем из очереди с приоритетами файл
          batchInfo = ProcessingPool.Instance.Queue.Dequeue();

          if (batchInfo != null)
          {
            try
            {
              // Перемещаем  его в список выполняемых
              ProcessingPool.Instance.ProsessingFiles.Add(batchInfo);

              // Записываем инфу про фходящий файл для отображения в интерфейсе
              context.JobDetail.JobDataMap["datails"] = batchInfo.FileInfo.Name;

              // Получаем импортера, у которого есть лицензия на импорт :)
              importer = ObjectFactory.GetInstance<IImporterFileFactory>().GetImporterFile(batchInfo.FileInfo);
            }
            catch (Exception ex)
            {
              LogManager.GetCurrentClassLogger().ErrorException(ex.Message, ex);

              // Если возникла ошибка, то убираем импорт из списка выполняемых задач 
              // todo надо подумать как обрабатывать такие файлы, может их перемещать в отдельную папку для анализа
              if (ProcessingPool.Instance.ProsessingFiles.Contains(batchInfo))
              {
                ProcessingPool.Instance.ProsessingFiles.Remove(batchInfo);
              }
            }
          }

          // Получаем
          if (batchInfo == null || importer == null || !ProcessingPool.Instance.ProsessingFiles.Contains(batchInfo))
          {
            return;
          }

          importer.UndoBatches(batchInfo.FileInfo.Name);

        }
        finally
        {
          context.Scheduler.ResumeTrigger(context.Trigger.Key);
        }
      }

      try
      {
        if (importer.Processing(batchInfo.FileInfo, context))
        {
          var moveToPath = Path.Combine(ConfigManager.ExchangeSettings.ProcessedPath, importer.GetDirectoryToMove());
          if (!Directory.Exists(moveToPath))
          {
            Directory.CreateDirectory(moveToPath);
          }

          var destFileName = Path.Combine(moveToPath, batchInfo.FileInfo.Name);
          if (File.Exists(destFileName))
          {
            File.Delete(destFileName);
          }

          File.Move(batchInfo.FileInfo.FullName, destFileName);
        }
      }
      catch (Exception ex)
      {
        LogManager.GetCurrentClassLogger().ErrorException(ex.Message, ex);
        var moveToPath = Path.Combine(ConfigManager.ExchangeSettings.FailedPath, importer.GetFailedDirectoryToMove());
        if (!Directory.Exists(moveToPath))
        {
          Directory.CreateDirectory(moveToPath);
        }

        File.Move(batchInfo.FileInfo.FullName, Path.Combine(moveToPath, batchInfo.FileInfo.Name));
      }
      finally
      {
        ProcessingPool.Instance.ProsessingFiles.Remove(batchInfo);
      }
    }

    /// <summary>
    /// The file can be read.
    /// </summary>
    /// <param name="file">
    /// The file. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    private bool FileCanBeRead(FileInfo file)
    {
      try
      {
        // if (file.Attributes.HasFlag())
        // Попробовали открыть файл для записи, если упали, то файл еще не сформирован
        using (var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
        {
          stream.Close();
        }

        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// The get priority.
    /// </summary>
    /// <param name="file">
    /// The file. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    private int GetPriority(FileInfo file)
    {
      // todo по возможности приоритет нужно определять по имени файла как определялся порядок в ListenerFolder.ExecuteImpl
      // todo но лучше сделать секцию конфига, где указывать маску файла и его приоритет
      return 0;
    }

    #endregion
  }
}