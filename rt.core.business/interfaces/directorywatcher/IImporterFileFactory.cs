// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImporterFileFactory.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Фабрика для импортеров пакетов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.directorywatcher
{
  using System.IO;

  /// <summary>
  ///   Фабрика для импортеров пакетов
  /// </summary>
  public interface IImporterFileFactory
  {
    /// <summary>
    /// Возвращает ExportBatchTyped по указанному типу.
    /// </summary>
    /// <param name="file"> </param>
    /// <returns>
    /// импортер 
    /// </returns>
    IImporterFile GetImporterFile(FileInfo file);
  }
}