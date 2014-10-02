using rt.atl.model.atl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace rt.atl.model.dto
{
  public class StatisticInitialLoading: person
  {
    [XmlElement]
    [DataMember]
    public virtual int Count { get; set; }

  }
}
