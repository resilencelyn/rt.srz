using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rt.core.business.server.jobpool;

namespace rt.core.business.interfaces.jobpool
{
  /// <summary>
  /// 
  /// </summary>
  public interface IJobPool
  {
    #region Public Properties

    /// <summary>
    /// Тип пула
    /// </summary>
    JobPoolType TypePool { get; }
    
    #endregion
  }
}
