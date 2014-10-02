// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImporterFile.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Интерфейс импортера пакетов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.directorywatcher
{
  using Quartz;
using System.IO;

  /// <summary>
  ///   Интерфейс импортера пакетов
  /// </summary>
  public interface IImporterFile
  {
    /// <summary>
    ///   Обработка
    /// </summary>
    /// <param name="file">
    /// Путь к файлу загрузки
    /// </param>
    /// <param name="context"> </param>
    /// <returns> был ли обработан пакет </returns>
    bool Processing(FileInfo file, IJobExecutionContext context);

    /// <summary>
    ///   Получает папку куда перемещать файлы после загрузки
    /// </summary>
    /// <returns> папка </returns>
    string GetDirectoryToMove();

    /// <summary>
    ///   Получает папку куда перемещать файлы в результате загрузки которых произошли ошибки
    /// </summary>
    /// <returns> папка </returns>
    string GetFailedDirectoryToMove();

    /// <summary>
    /// Применим ли импортер для данного типа сообщений?
    /// </summary>
    /// <param name="file"> </param>
    /// <returns>
    /// true, если применим, иначе false 
    /// </returns>
    bool AppliesTo(FileInfo file);

    /// <summary>
    /// The undo batches.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    void UndoBatches(string fileName);
  }
}