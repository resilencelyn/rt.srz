using rt.atl.model.atl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace rt.atl.model.dto
{
  public class ErrorSinchronizationInfoResult: Przbuf
  {
    //[XmlElement]
    //[DataMember]
    //public virtual Przbuf Przbuf { get; set; }

    [XmlElement]
    [DataMember]
    public virtual string Error { get; set; }
  }
}
