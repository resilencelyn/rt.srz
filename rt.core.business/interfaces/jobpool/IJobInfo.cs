using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rt.core.business.interfaces.jobpool
{
  public interface IJobInfo
  {
    #region Public Properties

    /// <summary>
    /// Идентификатор
    /// </summary>
    Guid Id { get; }

    #endregion
  }
}
