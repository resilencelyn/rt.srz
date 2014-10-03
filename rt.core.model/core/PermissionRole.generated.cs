
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using rt.core.model;

namespace rt.core.model.core
{
   [DataContract] 
	 [Serializable]
   public partial class PermissionRole : BusinessBase<System.Guid>
   {
      public PermissionRole() { }

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
        public virtual int? FakeField { get; set;}
        
        
				[XmlElement(Order =  3)]
        [DataMember(Order =  3)]
		public virtual Permission Permission { get; set;}
			
        
				[XmlElement(Order =  4)]
        [DataMember(Order =  4)]
		public virtual Role Role { get; set;}
			
       }
}
