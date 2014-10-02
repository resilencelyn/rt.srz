
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using rt.core.model;

namespace rt.srz.model.srz
{
   [DataContract] 
	 [Serializable]
   public partial class AutoComplete : BusinessBase<System.Guid>
   {
      public AutoComplete() { }

	  [XmlElement(Order = 1)]
      [DataMember(Order = 1)]
      public override System.Guid Id
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
        public virtual string Name { get; set;}
        
        [XmlElement(Order =  3)]
        [DataMember(Order =  3)]
        public virtual int Relevance { get; set;}
        
        
				[XmlElement(Order =  4)]
        [DataMember(Order =  4)]
		public virtual Concept Gender { get; set;}
			
        
				[XmlElement(Order =  5)]
        [DataMember(Order =  5)]
		public virtual Concept Type { get; set;}
			
       }
}
