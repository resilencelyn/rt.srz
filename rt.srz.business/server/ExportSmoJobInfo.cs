using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rt.core.business.server.jobpool;

namespace rt.srz.business.server
{
  [Serializable]
  public class ExportSmoJobInfo
  {
    /// <summary>
    /// Идентификатор Батча
    /// </summary>
    public Guid BatchId { get; set; }
  }
}
