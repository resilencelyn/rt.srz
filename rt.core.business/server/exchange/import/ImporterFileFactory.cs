// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterFileFactory.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

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
    /// <summary>
    ///   импортеры
    /// </summary>
    private readonly IImporterFile[] importerFiles;

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

    /// <summary>
    /// Возвращает ExportBatchTyped по указанному типу.
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
  }
}