// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IJobPoolTyped.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The JobPoolTyped interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.jobpool
{
  using System;

  /// <summary>
  /// The JobPoolTyped interface.
  /// </summary>
  /// <typeparam name="TPoolObject">
  /// </typeparam>
  public interface IJobPoolTyped<TPoolObject> : IJobPool
  {
    #region Public Methods and Operators

    /// <summary>
    /// Выполняет пользовательскую операцию с блокировкой на уровне базы данных
    /// </summary>
    /// <param name="operationExpression">
    /// </param>
    void PerfomOperationWithLock(Action operationExpression);

    /// <summary>
    /// Выполняет операцию на пуле с блокировкой на уровне базы данных
    /// </summary>
    /// <param name="operationExpression">
    /// </param>
    void PerfomOperationWithLock(Action<TPoolObject> operationExpression);

    #endregion
  }
}