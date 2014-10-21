// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterFias.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.fias.business.exchange
{
  using System;
  using System.IO;

  using Quartz;

  using rt.core.business.server.exchange.import;

  /// <summary>
  ///   The importer fias.
  /// </summary>
  public class ImporterFias : ImporterFile
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ImporterFias" /> class.
    /// </summary>
    public ImporterFias()
      : base(255)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The applies to.
    /// </summary>
    /// <param name="file">
    /// The file.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool AppliesTo(FileInfo file)
    {
      ////if (file.Name.Contains())
      return false;
    }

    /// <summary>
    /// The processing.
    /// </summary>
    /// <param name="file">
    /// The file.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool Processing(FileInfo file, IJobExecutionContext context)
    {
      return false;
    }

    /// <summary>
    /// The undo batches.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    public override void UndoBatches(string fileName)
    {
    }

    #endregion

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
    protected override bool UndoBatch(Guid batch)
    {
      return true;
    }

    #endregion
  }
}