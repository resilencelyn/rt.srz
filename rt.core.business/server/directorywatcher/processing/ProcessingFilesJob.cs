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

      // ������ ���������� �� ������ � ���������
      lock (ProcessingPool.Instance.QueueFiles)
      {
        // ������ ������ �� �����, ����� �� ������� �������, ������� ������� � �������� �� lock
        context.Scheduler.PauseTrigger(context.Trigger.Key);
        try
        {
          // ���������� ������� ������ ��� ���������� � �������� �� � ������� � �����������
          while (ProcessingPool.Instance.QueueFiles.Any())
          {
            // ������� �������� ����������� �� ���� ������������, ��� ��� ��� �� ��������
            var file = new FileInfo(ProcessingPool.Instance.QueueFiles.Peek());
            if (!FileCanBeRead(file))
            {
              continue;
            }

            // ������ ��� �� �������
            ProcessingPool.Instance.QueueFiles.Dequeue();

            // ���������� ���������
            var priority = GetPriority(file);

            // �������� ���� �� ���������, �� ��� � �����������
            ProcessingPool.Instance.Queue.Enqueue(new BatchInfo { FileInfo = file, Priority = priority }, priority);
          }

          if (ProcessingPool.Instance.ProsessingFiles.Count >= MaxProc)
          {
            return;
          }

          // ����� �� ������� � ������������ ����
          batchInfo = ProcessingPool.Instance.Queue.Dequeue();

          if (batchInfo != null)
          {
            try
            {
              // ����������  ��� � ������ �����������
              ProcessingPool.Instance.ProsessingFiles.Add(batchInfo);

              // ���������� ���� ��� �������� ���� ��� ����������� � ����������
              context.JobDetail.JobDataMap["datails"] = batchInfo.FileInfo.Name;

              // �������� ���������, � �������� ���� �������� �� ������ :)
              importer = ObjectFactory.GetInstance<IImporterFileFactory>().GetImporterFile(batchInfo.FileInfo);
            }
            catch (Exception ex)
            {
              LogManager.GetCurrentClassLogger().ErrorException(ex.Message, ex);

              // ���� �������� ������, �� ������� ������ �� ������ ����������� ����� 
              // todo ���� �������� ��� ������������ ����� �����, ����� �� ���������� � ��������� ����� ��� �������
              if (ProcessingPool.Instance.ProsessingFiles.Contains(batchInfo))
              {
                ProcessingPool.Instance.ProsessingFiles.Remove(batchInfo);
              }
            }
          }

          // ��������
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
        // ����������� ������� ���� ��� ������, ���� �����, �� ���� ��� �� �����������
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
      // todo �� ����������� ��������� ����� ���������� �� ����� ����� ��� ����������� ������� � ListenerFolder.ExecuteImpl
      // todo �� ����� ������� ������ �������, ��� ��������� ����� ����� � ��� ���������
      return 0;
    }

    #endregion
  }
}