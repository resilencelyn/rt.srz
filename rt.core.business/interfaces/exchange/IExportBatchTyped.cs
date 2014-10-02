// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExportBatchTyped.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.exchange
{
  using System;

  using Quartz;

  /// <summary>
  /// The ExportBatchTyped interface.
  /// </summary>
  /// <typeparam name="TSerializeObject">
  /// </typeparam>
  public interface IExportBatchTyped<TSerializeObject, TNode> : IExportBatch
  {
    #region Public Properties

    /// <summary>
    ///   Объект текущего пакета
    /// </summary>
    TSerializeObject SerializeObject { get; }

    #endregion

    /// <summary>
    /// The add node.
    /// </summary>
    /// <param name="node">
    /// The node. 
    /// </param>
    void AddNode(TNode node);

    /// <summary>
    /// The bulk create and export.
    /// </summary>
    void BulkCreateAndExport(IJobExecutionContext context, Guid batchId);
  }
}