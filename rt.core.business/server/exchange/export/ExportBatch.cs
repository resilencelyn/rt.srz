// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportBatch.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The export batch.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.exchange.export
{
  #region

  using System;
  using System.IO;

  using NHibernate;

  using rt.core.business.interfaces.exchange;
  using rt.core.model.configuration;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The export batch.
  /// </summary>
  public abstract class ExportBatch : IExportBatch
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ExportBatch"/> class. 
    /// Конструктор
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    protected ExportBatch(ExportBatchType type)
    {
      TypeBatch = type;
      MaxCountMessageInBatchSession = 3000;
      MaxCountBatchSession = 10;
      SessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
      Status = StatusExportBatch.Opened;
      OutDirectory = "Out";
      CopyDirectory = "History";
      RootDirectory = ConfigManager.ExchangeSettings.WorkingFolderExchange;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Идентификатор пакета
    /// </summary>
    public Guid BatchId { get; protected set; }

    /// <summary>
    ///   Директория для сохранения истории
    /// </summary>
    public string CopyDirectory { get; set; }

    /// <summary>
    ///   Количество выгруженных сообщений за текущий сеанс
    /// </summary>
    public virtual int Count
    {
      get
      {
        return 0;
      }
    }

    /// <summary>
    ///   Gets or sets the file name.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    ///   Максимальное количество пакетов
    /// </summary>
    public int MaxCountBatchSession { get; set; }

    /// <summary>
    ///   Максимальное количество сообщений в пакете
    /// </summary>
    public int MaxCountMessageInBatchSession { get; set; }

    /// <summary>
    ///   Номер
    /// </summary>
    public short Number { get; set; }

    /// <summary>
    ///   Директория для выгрузки
    /// </summary>
    public virtual string OutDirectory { get; set; }

    /// <summary>
    ///   Период
    /// </summary>
    public Guid PeriodId { get; set; }

    /// <summary>
    ///   Получатель пакета
    /// </summary>
    public Guid ReceiverId { get; set; }

    /// <summary>
    ///   Gets or sets the root directory.
    /// </summary>
    public string RootDirectory { get; set; }

    /// <summary>
    ///   Получатель пакета
    /// </summary>
    public Guid SenderId { get; set; }

    /// <summary>
    ///   Сессия
    /// </summary>
    public ISessionFactory SessionFactory { get; protected set; }

    /// <summary>
    ///   Статус
    /// </summary>
    public StatusExportBatch Status { get; protected set; }

    /// <summary>
    ///   Gets the type batch.
    /// </summary>
    public ExportBatchType TypeBatch { get; protected set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Создание нового пакета
    /// </summary>
    public virtual void BeginBatch()
    {
      // Комитим если есть записи
      if (Status == StatusExportBatch.Opened && Count > 0)
      {
        CommitBatch();
      }

      Status = StatusExportBatch.Opened;
    }

    /// <summary>
    ///   Закрытие пакета, и его сохранение в (файле или памяти)
    /// </summary>
    /// <returns> был ли закрыт пакет </returns>
    public virtual bool CommitBatch()
    {
      if (Status == StatusExportBatch.Opened)
      {
        SessionFactory.GetCurrentSession().Flush();
        if (Count > 0)
        {
          SerializePersonCurrent();
          Status = StatusExportBatch.Commited;
          return true;
        }

        Status = StatusExportBatch.Commited;
      }

      return false;
    }

    /// <summary>
    ///   Откат текущего пакета
    /// </summary>
    public virtual void UndoBatch()
    {
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Возвращает имя файла для текущего пакета
    /// </summary>
    /// <returns> Полное имя файла </returns>
    protected string GetFileNameFull()
    {
      return string.Format("{0}\\{1}\\{2}.xml", RootDirectory, OutDirectory, FileName);
    }

    /// <summary>
    ///   Сериализует текущий объект пакета
    /// </summary>
    protected virtual void SerializePersonCurrent()
    {
      var fileInfo = new FileInfo(GetFileNameFull());
      if (fileInfo.Exists)
      {
        var dir = string.Format(
                                @"{0}\{1}\{2}", 
                                ConfigManager.ExchangeSettings.WorkingFolderExchange, 
                                OutDirectory, 
                                CopyDirectory);
        if (!Directory.Exists(dir))
        {
          Directory.CreateDirectory(dir);
        }

        var newFile = string.Format(@"{0}\{1}", dir, fileInfo.Name);
        fileInfo.CopyTo(newFile, true);
      }
    }

    #endregion
  }
}