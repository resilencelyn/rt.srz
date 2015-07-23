// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobPoolTyped.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The job pool typed.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.jobpool
{
  using System;

  using rt.core.business.interfaces.jobpool;

  /// <summary>
  /// The job pool typed.
  /// </summary>
  /// <typeparam name="TPoolObject">
  /// </typeparam>
  public abstract class JobPoolTyped<TPoolObject> : JobPoolBase, IJobPoolTyped<TPoolObject>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="JobPoolTyped{TPoolObject}"/> class. 
    /// Конструктор
    /// </summary>
    /// <param name="typePool">
    /// </param>
    protected JobPoolTyped(JobPoolType typePool)
      : base(typePool)
    {
      LockObject = typePool.ToString();
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the lock object.
    /// </summary>
    protected object LockObject { get; private set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Выполняет пользовательскую операцию с блокировкой на уровне базы данных
    /// </summary>
    /// <param name="operationExpression">
    /// </param>
    public abstract void PerfomOperationWithLock(Action operationExpression);

    /// <summary>
    /// Выполняет операцию на пуле с блокировкой на уровне базы данных
    /// </summary>
    /// <param name="operationExpression">
    /// </param>
    public abstract void PerfomOperationWithLock(Action<TPoolObject> operationExpression);

    #endregion
  }
}