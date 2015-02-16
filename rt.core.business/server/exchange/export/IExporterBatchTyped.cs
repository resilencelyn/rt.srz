// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExporterBatchTyped.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ExporterBatchTyped interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.exchange.export
{
  using System;

  using Quartz;

  /// <summary>
  /// The ExporterBatchTyped interface.
  /// </summary>
  /// <typeparam name="TSerializeObject">
  /// </typeparam>
  /// <typeparam name="TNode">
  /// </typeparam>
  public interface IExporterBatchTyped<TSerializeObject, TNode> : IExporterBatch
  {
    #region Public Properties

    /// <summary>
    ///   Объект текущего пакета
    /// </summary>
    TSerializeObject SerializeObject { get; }

    #endregion

    #region Public Methods and Operators

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
    /// <param name="context">
    /// The context.
    /// </param>
    /// <param name="batchId">
    /// The batch Id.
    /// </param>
    void BulkCreateAndExport(IJobExecutionContext context, Guid batchId);

    #endregion
  }
}