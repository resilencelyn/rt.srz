using rt.srz.model.enumerations;
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
  public class SearchTwinCriteria : BaseSearchCriteria
  {
    [XmlElement]
    [DataMember]
    public TwinKeyType KeyType { get; set; }

    [XmlElement]
    [DataMember]
    public Guid? KeyId { get; set; }
  }
}
