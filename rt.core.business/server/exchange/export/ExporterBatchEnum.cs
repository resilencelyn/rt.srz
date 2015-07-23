// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExporterBatchEnum.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The export batch type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.exchange.export
{
  /// <summary>
  ///   The export batch type.
  /// </summary>
  public enum ExporterBatchEnum
  {
    /// <summary>
    ///   The pfr.
    /// </summary>
    PfrExporter = 1, 

    /// <summary>
    ///   The SmoRecExporter.
    /// </summary>
    SmoRecExporter = 2, 

    /// <summary>
    ///   The SmoOpExporter.
    /// </summary>
    SmoOpExporter = 3, 

    /// <summary>
    ///   The SmoRepExporter.
    /// </summary>
    SmoRepExporter = 4, 

    /// <summary>
    ///   The SmoFlkExporter.
    /// </summary>
    SmoFlkExporter = 5,

    /// <summary>
    /// The erp.
    /// </summary>
    ErpExporter = 6,
  }
}