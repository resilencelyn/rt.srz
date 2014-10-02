// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessingPool.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.core.business.server.directorywatcher.processing
{
  using System.Collections.Generic;

  using rt.core.business.server.directorywatcher.processing.target;

  /// <summary>
  ///   The processing Pool.
  /// </summary>
  public class ProcessingPool
  {
    /// <summary>
    /// The processing Pool.
    /// </summary>
    private static ProcessingPool processingPool;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessingPool"/> class.
    /// </summary>
    protected ProcessingPool()
    {
      Queue = new PriorityQueue<BatchInfo>();
      QueueFiles = new Queue<string>();
      ProsessingFiles = new List<BatchInfo>();
    }

    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static ProcessingPool Instance
    {
      get { return processingPool ?? (processingPool = new ProcessingPool()); }
    }

    /// <summary>
    ///   Gets the queue files.
    /// </summary>
    public Queue<string> QueueFiles { get; protected set; }

    /// <summary>
    ///   Gets or sets the queue.
    /// </summary>
    public PriorityQueue<BatchInfo> Queue { get; protected set; }

    /// <summary>
    ///   Gets or sets the prosessing files.
    /// </summary>
    public List<BatchInfo> ProsessingFiles { get; protected set; }
  }
}