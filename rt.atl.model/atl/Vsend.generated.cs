
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace rt.atl.model.atl
{
  using rt.core.model;

  [DataContract] 
	 [Serializable]
   public partial class Vsend : BusinessBase<int>
   {
      public Vsend() { }

	  [XmlElement(Order = 1)]
      [DataMember(Order = 1)]
      public override int Id
      {
        get
        {
          return base.Id;
        }
        set
        {
          base.Id = value;
        }
      } 


        [XmlElement(Order =  2)]
        [DataMember(Order =  2)]
        public virtual System.DateTime? Db { get; set;}
        
        [XmlElement(Order =  3)]
        [DataMember(Order =  3)]
        public virtual System.DateTime? De { get; set;}
        
       }
}
