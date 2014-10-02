// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportBatchTyped.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.exchange.export
{
  #region

  using System;

  using Quartz;

  using rt.core.business.interfaces.exchange;

  #endregion

  /// <summary>
  /// Экспортер пакета
  /// </summary>
  /// <typeparam name="TSerializeObject">
  /// Тип сериализуемого объекта
  /// </typeparam>
  /// <typeparam name="TNode">Тип ноды</typeparam>
  public abstract class ExportBatchTyped<TSerializeObject, TNode> : ExportBatch, IExportBatchTyped<TSerializeObject, TNode>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ExportBatchTyped{TSerializeObject}"/> class.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    protected ExportBatchTyped(ExportBatchType type)
      : base(type)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Объект текущего пакета
    /// </summary>
    public TSerializeObject SerializeObject { get; protected set; }

    #endregion

    /// <summary>
    /// The add node.
    /// </summary>
    /// <param name="node">
    /// The node. 
    /// </param>
    public virtual void AddNode(TNode node)
    {
      //Начинаем новый батч, в случае если к-во обработанных записей превысило допустимое значение
      if (Count >= MaxCountMessageInBatchSession)
        BeginBatch();
    }

    /// <summary>
    /// The bulk create and export.
    /// </summary>
    public virtual void BulkCreateAndExport(IJobExecutionContext context, Guid batchId)
    {
    }
  }
}