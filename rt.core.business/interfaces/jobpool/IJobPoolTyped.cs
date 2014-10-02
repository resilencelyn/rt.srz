using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace rt.core.business.interfaces.jobpool
{
  public interface IJobPoolTyped<TPoolObject> : IJobPool
  {
    #region Public Methods

    /// <summary>
    /// Выполняет пользовательскую операцию с блокировкой на уровне базы данных
    /// </summary>
    /// <param name="operationExpression"></param>
    void PerfomOperationWithLock(Action operationExpression);

    /// <summary>
    /// Выполняет операцию на пуле с блокировкой на уровне базы данных 
    /// </summary>
    /// <param name="operationExpression"></param>
    void PerfomOperationWithLock(Action<TPoolObject> operationExpression);
    
    #endregion

  }
}
