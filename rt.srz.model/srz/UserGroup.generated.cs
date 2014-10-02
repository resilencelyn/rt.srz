﻿
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
   public partial class UserGroup : BusinessBase<System.Guid>
   {
      public UserGroup() { }

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
		public virtual Group Group { get; set;}
			
        
				[XmlElement(Order =  4)]
        [DataMember(Order =  4)]
		public virtual User User { get; set;}
			
       }
}