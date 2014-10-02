using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using rt.core.business.server.jobpool;

namespace rt.atl.business.quartz
{
  /// <summary>
  /// Информация для таска первичной загрузки
  /// </summary>
  public class FirstLoadingToPvpJobInfo : JobInfo
  {
    /// <summary>
    /// 
    /// </summary>
    public int Min { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Max {get; set;}
  }
}
