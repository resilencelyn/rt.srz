// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterFile.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.exchange.import
{
  #region

  using System;
  using System.IO;

  using NHibernate;

  using Quartz;

  using rt.core.business.interfaces.directorywatcher;

  using StructureMap;

  #endregion

  /// <summary>
  ///   Универсальный загрузчик пакетов
  /// </summary>
  public abstract class ImporterFile : IImporterFile
  {
    #region Fields

    /// <summary>
    ///   Фабрика
    /// </summary>
    protected readonly ISessionFactory SessionFactory;

    /// <summary>
    ///   Субъект
    /// </summary>
    protected readonly int Subject;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ImporterFile"/> class. 
    ///   Конструктор
    /// </summary>
    /// <param name="subject">
    /// The subject. 
    /// </param>
    protected ImporterFile(int subject)
    {
      Subject = subject;
      SessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Применим ли импортер для данного типа сообщений?
    /// </summary>
    /// <param name="file">
    /// </param>
    /// <returns>
    /// true, если применим, иначе false 
    /// </returns>
    public abstract bool AppliesTo(FileInfo file);

    /// <summary>
    ///   Получает папку куда перемещать файлы после загрузки
    /// </summary>
    /// <returns> папка </returns>
    public virtual string GetDirectoryToMove()
    {
      return string.Empty;
    }

    /// <summary>
    ///   Получает папку куда перемещать файлы в результате загрузки которых произошли ошибки
    /// </summary>
    /// <returns> папка </returns>
    public virtual string GetFailedDirectoryToMove()
    {
      return string.Empty;
    }

    /// <summary>
    /// Обработка
    /// </summary>
    /// <param name="file">
    /// Путь к файлу загрузки 
    /// </param>
    /// <param name="context">
    /// </param>
    /// <returns>
    /// был ли обработан пакет 
    /// </returns>
    public abstract bool Processing(FileInfo file, IJobExecutionContext context);

    /// <summary>
    /// The undo batches.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    public abstract void UndoBatches(string fileName);

    #endregion

    // Отмена загрузки пакета
    #region Methods

    /// <summary>
    /// The undo batch.
    /// </summary>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    protected abstract bool UndoBatch(Guid batch);

    #endregion
  }
}