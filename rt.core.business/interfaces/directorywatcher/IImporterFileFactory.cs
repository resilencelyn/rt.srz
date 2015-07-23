// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImporterFileFactory.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
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
    #region Public Methods and Operators

    /// <summary>
    /// Возвращает ExporterBatchTyped по указанному типу.
    /// </summary>
    /// <param name="file">
    /// </param>
    /// <returns>
    /// импортер
    /// </returns>
    IImporterFile GetImporterFile(FileInfo file);

    #endregion
  }
}