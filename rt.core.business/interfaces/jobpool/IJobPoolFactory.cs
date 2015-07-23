// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IJobPoolFactory.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The JobPoolFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.jobpool
{
  using rt.core.business.server.jobpool;

  /// <summary>
  /// The JobPoolFactory interface.
  /// </summary>
  /// <typeparam name="TPoolObject">
  /// </typeparam>
  public interface IJobPoolFactory<TPoolObject>
  {
    #region Public Methods and Operators

    /// <summary>
    /// Возвращает реализацию конкретного типа пула
    /// </summary>
    /// <param name="type">
    /// </param>
    /// <returns>
    /// The <see cref="IJobPoolTyped"/>.
    /// </returns>
    IJobPoolTyped<TPoolObject> GetJobPool(JobPoolType type);

    #endregion
  }
}