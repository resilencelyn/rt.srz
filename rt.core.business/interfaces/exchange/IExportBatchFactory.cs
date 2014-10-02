// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExportBatchFactory.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.exchange
{
  #region

  using rt.core.business.server.exchange.export;

  #endregion

  /// <summary>
  /// The ExportBatchFactory interface.
  /// </summary>
  /// <typeparam name="TSerializeObject">
  /// </typeparam>
  /// <typeparam name="TNode">
  /// </typeparam>
  public interface IExportBatchFactory<TSerializeObject, TNode>
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get exporter.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="IExportBatchTyped{TSerializeObject,TNode}"/> . 
    /// </returns>
    IExportBatchTyped<TSerializeObject, TNode> GetExporter(ExportBatchType type);

    #endregion
  }
}