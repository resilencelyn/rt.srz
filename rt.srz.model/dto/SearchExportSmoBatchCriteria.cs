using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace rt.srz.model.dto
{
  using rt.core.model.dto;

  [Serializable]
  [DataContract]
  public class SearchExportSmoBatchCriteria : BaseSearchCriteria
  {
    /// <summary>
    /// Отправитель батча
    /// </summary>
    public Guid SenderId { get; set; }

    /// <summary>
    /// Получатель батча
    /// </summary>
    public Guid ReceiverId { get; set; }
    
    /// <summary>
    /// Идентификатор периода
    /// </summary>
    public Guid PeriodId { get; set; }
    
    /// <summary>
    /// Номер батча
    /// </summary>
    public int BatchNumber {get; set;}
  }
}
