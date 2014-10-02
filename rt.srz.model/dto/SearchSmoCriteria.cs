using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace rt.srz.model.dto
{
  using rt.core.model.dto;

  [Serializable]
  [DataContract]
  public class SearchSmoCriteria : BaseSearchCriteria
  {
    [XmlElement]
    [DataMember]
    public string ShortName { get; set; }

    [XmlElement]
    [DataMember]
    public string Oid { get; set; }
  }
}
