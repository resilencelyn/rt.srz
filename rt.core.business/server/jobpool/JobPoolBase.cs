using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rt.core.business.interfaces.jobpool;

namespace rt.core.business.server.jobpool
{
  public abstract class JobPoolBase : IJobPool
  {
    protected JobPoolBase(JobPoolType typePool)
    {
      TypePool = typePool;
    }
    
    #region Public Properties

    /// <summary>
    /// Тип пула
    /// </summary>
    public JobPoolType TypePool { get; protected set; }
    
    #endregion
  }
}
