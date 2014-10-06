// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobPoolBase.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The job pool base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.jobpool
{
  using rt.core.business.interfaces.jobpool;

  /// <summary>
  /// The job pool base.
  /// </summary>
  public abstract class JobPoolBase : IJobPool
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="JobPoolBase"/> class.
    /// </summary>
    /// <param name="typePool">
    /// The type pool.
    /// </param>
    protected JobPoolBase(JobPoolType typePool)
    {
      TypePool = typePool;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Тип пула
    /// </summary>
    public JobPoolType TypePool { get; protected set; }

    #endregion
  }
}