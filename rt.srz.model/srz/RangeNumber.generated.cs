
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
   public partial class RangeNumber : BusinessBase<System.Guid>
   {
      public RangeNumber() { }

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
        public virtual int RangelFrom { get; set;}
        
        [XmlElement(Order =  3)]
        [DataMember(Order =  3)]
        public virtual int RangelTo { get; set;}
        
        [XmlElement(Order =  4)]
        [DataMember(Order =  4)]
        public virtual System.DateTime? ChangeDate { get; set;}
        
        
				[XmlElement(Order =  5)]
        [DataMember(Order =  5)]
		public virtual RangeNumber Parent { get; set;}
			
        
				[XmlElement(Order =  6)]
        [DataMember(Order =  6)]
		public virtual Organisation Smo { get; set;}
			
        
				[XmlElement(Order =  7)]
        [DataMember(Order =  7)]
		public virtual Template Template { get; set;}
			
        [XmlIgnore] 
        public virtual IList<RangeNumber> RangeNumbers { get; set;}
		
       }
}
