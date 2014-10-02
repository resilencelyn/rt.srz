// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportBatchSrz.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.export
{
  #region

  using System.IO;

  using rt.core.business.server.exchange.export;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  /// The export batch typed with begin batch impl.
  /// </summary>
  /// <typeparam name="TSerializeObject">
  /// Пакет
  /// </typeparam>
  /// <typeparam name="TNode">
  /// Нода
  /// </typeparam>
  public abstract class ExportBatchSrz<TSerializeObject, TNode> : ExportBatchTyped<TSerializeObject, TNode>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ExportBatchSrz{TSerializeObject,TNode}"/> class.
    /// </summary>
    /// <param name="type">
    /// The type. 
    /// </param>
    /// <param name="typeSubjectId">
    /// The type Subject Id. 
    /// </param>
    /// <param name="typeFileId">
    /// The type File Id. 
    /// </param>
    protected ExportBatchSrz(ExportBatchType type, int typeSubjectId, int typeFileId)
      : base(type)
    {
      TypeFileId = typeSubjectId;
      TypeSubjectId = typeFileId;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the out directory.
    /// </summary>
    public override string OutDirectory
    {
      get
      {
        return Path.Combine("Out", Batch.Receiver.Oid.Id, Batch.Receiver.Code);
      }

      set
      {
        base.OutDirectory = value;
      }
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the batch.
    /// </summary>
    protected Batch Batch { get; set; }

    /// <summary>
    ///   Идентификатор типа файла
    /// </summary>
    protected int TypeFileId { get; private set; }

    /// <summary>
    ///   Идентификатор типа получателя батча
    /// </summary>
    protected int TypeSubjectId { get; private set; }

    #endregion
  }
}