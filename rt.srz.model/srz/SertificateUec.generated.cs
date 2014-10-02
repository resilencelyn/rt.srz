
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
   public partial class SertificateUec : BusinessBase<System.Guid>
   {
      public SertificateUec() { }

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
        public virtual byte[] Key { get; set;}
        
        [XmlElement(Order =  3)]
        [DataMember(Order =  3)]
        public virtual short Version { get; set;}
        
        [XmlElement(Order =  4)]
        [DataMember(Order =  4)]
        public virtual bool IsActive { get; set;}
        
        [XmlElement(Order =  5)]
        [DataMember(Order =  5)]
        public virtual System.DateTime InstallDate { get; set;}
        
        
				[XmlElement(Order =  6)]
        [DataMember(Order =  6)]
		public virtual Concept Type { get; set;}
			
        
				[XmlElement(Order =  7)]
        [DataMember(Order =  7)]
		public virtual Organisation Smo { get; set;}
			
        
				[XmlElement(Order =  8)]
        [DataMember(Order =  8)]
		public virtual Workstation Workstation { get; set;}
			
       }
}
