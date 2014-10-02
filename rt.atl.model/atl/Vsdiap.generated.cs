
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace rt.atl.model.atl
{
  using rt.core.model;

  [DataContract] 
   public partial class Vsdiap : BusinessBase<int>
   {
      public Vsdiap() { }

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
        public virtual int? Lo { get; set;}
        
        [XmlElement(Order =  4)]
        [DataMember(Order =  4)]
        public virtual int? Hi { get; set;}
        
        
        [DataMember(Order =  5)]
		public virtual Smo SMO { get; set;}
			
       }
}
