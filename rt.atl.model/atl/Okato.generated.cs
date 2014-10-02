
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
   public partial class Okato : BusinessBase<int>
   {
      public Okato() { }

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
        public virtual System.DateTime? Dedit { get; set;}
        
        [XmlElement(Order =  3)]
        [DataMember(Order =  3)]
        public virtual string Caption { get; set;}
        
        [XmlElement(Order =  4)]
        [DataMember(Order =  4)]
        public virtual string Code { get; set;}
        
        [XmlElement(Order =  5)]
        [DataMember(Order =  5)]
        public virtual int? Parentid { get; set;}
        
        [XmlElement(Order =  6)]
        [DataMember(Order =  6)]
        public virtual string Centrum { get; set;}
        
        [XmlElement(Order =  7)]
        [DataMember(Order =  7)]
        public virtual int? Okatoid { get; set;}
        
       }
}
