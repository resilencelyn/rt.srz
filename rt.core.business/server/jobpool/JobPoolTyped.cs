using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using rt.core.business.interfaces.jobpool;

namespace rt.core.business.server.jobpool
{
  public abstract class JobPoolTyped<TPoolObject> : JobPoolBase, IJobPoolTyped<TPoolObject>
  {
    /// <summary>
    ///   Gets the lock object.
    /// </summary>
    protected object LockObject { get; private set; }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="typePool"></param>
    protected JobPoolTyped(JobPoolType typePool) : base(typePool)
    {
      LockObject = typePool.ToString();
    }

    /// <summary>
    /// Выполняет пользовательскую операцию с блокировкой на уровне базы данных
    /// </summary>
    /// <param name="operationExpression"></param>
    public abstract void PerfomOperationWithLock(Action operationExpression);

    /// <summary>
    /// Выполняет операцию на пуле с блокировкой на уровне базы данных 
    /// </summary>
    /// <param name="operationExpression"></param>
    public abstract void PerfomOperationWithLock(Action<TPoolObject> operationExpression);
  }
}
