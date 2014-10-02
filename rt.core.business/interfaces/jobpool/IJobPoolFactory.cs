using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rt.core.business.server.jobpool;

namespace rt.core.business.interfaces.jobpool
{
  public interface IJobPoolFactory<TPoolObject>
  {
    #region Public Methods and Operators

    /// <summary>
    /// Возвращает реализацию конкретного типа пула
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IJobPoolTyped<TPoolObject> GetJobPool(JobPoolType type);

    #endregion
  }
}
