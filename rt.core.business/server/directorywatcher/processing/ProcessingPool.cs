// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessingPool.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The processing Pool.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.directorywatcher.processing
{
  using System.Collections.Generic;

  using rt.core.business.server.directorywatcher.processing.target;

  /// <summary>
  ///   The processing Pool.
  /// </summary>
  public class ProcessingPool
  {
    #region Static Fields

    /// <summary>
    ///   The processing Pool.
    /// </summary>
    private static ProcessingPool processingPool;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ProcessingPool" /> class.
    /// </summary>
    protected ProcessingPool()
    {
      Queue = new PriorityQueue<BatchInfo>();
      QueueFiles = new Queue<string>();
      ProsessingFiles = new List<BatchInfo>();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the instance.
    /// </summary>
    public static ProcessingPool Instance
    {
      get
      {
        return processingPool ?? (processingPool = new ProcessingPool());
      }
    }

    /// <summary>
    ///   Gets or sets the prosessing files.
    /// </summary>
    public List<BatchInfo> ProsessingFiles { get; protected set; }

    /// <summary>
    ///   Gets or sets the queue.
    /// </summary>
    public PriorityQueue<BatchInfo> Queue { get; protected set; }

    /// <summary>
    ///   Gets the queue files.
    /// </summary>
    public Queue<string> QueueFiles { get; protected set; }

    #endregion
  }
}