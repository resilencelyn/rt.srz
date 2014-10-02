using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rt.core.business.interfaces.jobpool;

namespace rt.core.business.server.jobpool
{
  /// <summary>
  /// Базовый класс для описателя работ
  /// </summary>
  [Serializable]
  public abstract class JobInfo : IJobInfo
  {
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    protected JobInfo()
    { 
      Id = Guid.NewGuid();
    }
  }
}
