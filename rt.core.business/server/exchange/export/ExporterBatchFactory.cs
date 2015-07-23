// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExporterBatchFactory.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The export batch factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.exchange.export
{
  #region

  using System;
  using System.Linq;

  using StructureMap;

  #endregion

  /// <summary>
  /// The export batch factory.
  /// </summary>
  /// <typeparam name="TSerializeObject">
  /// Тип сериализуемого объекта
  /// </typeparam>
  /// <typeparam name="TNode">
  /// Тип ноды
  /// </typeparam>
  public class ExporterBatchFactory<TSerializeObject, TNode> : IExporterBatchFactory<TSerializeObject, TNode>
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get exporter.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IExporterBatchTyped</cref>
    ///   </see>
    ///   .
    /// </returns>
    public IExporterBatchTyped<TSerializeObject, TNode> GetExporter(Guid type)
    {
      var exportBatchTypeds = ObjectFactory.GetAllInstances<IExporterBatchTyped<TSerializeObject, TNode>>();
      return exportBatchTypeds.FirstOrDefault(x => x.TypeBatch == type);
    }

    #endregion
  }
}