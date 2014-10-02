// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportBatchFactory.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.exchange.export
{
  #region

  using System.Linq;

  using rt.core.business.interfaces.exchange;

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
  public class ExportBatchFactory<TSerializeObject, TNode> : IExportBatchFactory<TSerializeObject, TNode>
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get exporter.
    /// </summary>
    /// <param name="type">
    /// The type. 
    /// </param>
    /// <returns>
    /// The <see>
    ///       <cref>IExportBatchTyped</cref>
    ///     </see> . 
    /// </returns>
    public IExportBatchTyped<TSerializeObject, TNode> GetExporter(ExportBatchType type)
    {
      var exportBatchTypeds = ObjectFactory.GetAllInstances<IExportBatchTyped<TSerializeObject, TNode>>();
      return exportBatchTypeds.FirstOrDefault(x => x.TypeBatch == type);
    }

    #endregion
  }
}