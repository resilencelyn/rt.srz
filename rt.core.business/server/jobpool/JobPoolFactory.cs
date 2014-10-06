// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobPoolFactory.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The job pool factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.jobpool
{
  using rt.core.business.interfaces.jobpool;

  /// <summary>
  /// The job pool factory.
  /// </summary>
  /// <typeparam name="TPoolObject">
  /// </typeparam>
  public class JobPoolFactory<TPoolObject> : IJobPoolFactory<TPoolObject>
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
    public IJobPoolTyped<TPoolObject> GetJobPool(JobPoolType type)
    {
      // var jobPools = ObjectFactory.GetAllInstances<IJobPoolTyped<TPoolObject, TJobInfo>>();
      // return jobPools.FirstOrDefault(x => x.TypePool == type);
      return null;
    }

    #endregion
  }
}