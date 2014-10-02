
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
   public partial class Error : BusinessBase<System.Guid>
   {
      public Error() { }

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
        public virtual string Message1 { get; set;}
        
        [XmlElement(Order =  3)]
        [DataMember(Order =  3)]
        public virtual string Code { get; set;}
        
        [XmlElement(Order =  4)]
        [DataMember(Order =  4)]
        public virtual string Repl { get; set;}
        
        
				[XmlElement(Order =  5)]
        [DataMember(Order =  5)]
		public virtual Concept Application { get; set;}
			
        
				[XmlElement(Order =  6)]
        [DataMember(Order =  6)]
		public virtual Message Message { get; set;}
			
        
				[XmlElement(Order =  7)]
        [DataMember(Order =  7)]
		public virtual Statement Statement { get; set;}
			
       }
}
