// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterFileFactory.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The watcher.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.exchange.import
{
  using System.IO;
  using System.Linq;

  using rt.core.business.interfaces.directorywatcher;

  /// <summary>
  ///   The watcher.
  /// </summary>
  public class ImporterFileFactory : IImporterFileFactory
  {
    #region Fields

    /// <summary>
    ///   импортеры
    /// </summary>
    private readonly IImporterFile[] importerFiles;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ImporterFileFactory"/> class.
    ///   Инициализация нового экземпляра типа <see cref="ImporterFileFactory"/>.
    /// </summary>
    /// <param name="importerFiles">
    /// The import batches.
    /// </param>
    public ImporterFileFactory(IImporterFile[] importerFiles)
    {
      this.importerFiles = importerFiles;
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Возвращает ExporterBatchTyped по указанному типу.
    /// </summary>
    /// <param name="file">
    /// The file.
    /// </param>
    /// <returns>
    /// импортер
    /// </returns>
    public IImporterFile GetImporterFile(FileInfo file)
    {
      return importerFiles != null ? importerFiles.FirstOrDefault(x => x.AppliesTo(file)) : null;
    }

    #endregion
  }
}