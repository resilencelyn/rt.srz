using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace rt.atl.model.dto
{
  using rt.core.model.dto;

  [Serializable]
  [DataContract]
  public class SearchErrorSinchronizationCriteria : BaseSearchCriteria
  {
    /// <summary>
    ///   Дата c
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// Дата по
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime? DateTo { get; set; }

  }
}
