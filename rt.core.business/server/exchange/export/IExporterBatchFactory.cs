// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExporterBatchFactory.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ExporterBatchFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.exchange.export
{
  


  #region

  using System;

  #endregion

  /// <summary>
  /// The ExporterBatchFactory interface.
  /// </summary>
  /// <typeparam name="TSerializeObject">
  /// </typeparam>
  /// <typeparam name="TNode">
  /// </typeparam>
  public interface IExporterBatchFactory<TSerializeObject, TNode>
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get exporter.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="IExporterBatchTyped{TSerializeObject,TNode}"/> .
    /// </returns>
    IExporterBatchTyped<TSerializeObject, TNode> GetExporter(Guid type);

    #endregion
  }
}