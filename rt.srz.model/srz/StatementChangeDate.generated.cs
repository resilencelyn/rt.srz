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
   public partial class StatementChangeDate : BusinessBase<System.Guid>
   {
      public StatementChangeDate() { }

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
        public virtual int Version { get; set;}
        
        [XmlElement(Order =  3)]
        [DataMember(Order =  3)]
        public virtual string Datum { get; set;}
        
        
				[XmlElement(Order =  4)]
        [DataMember(Order =  4)]
		public virtual Concept Field { get; set;}
			
        
				[XmlElement(Order =  5)]
        [DataMember(Order =  5)]
		public virtual Statement Statement { get; set;}
			
       }
}