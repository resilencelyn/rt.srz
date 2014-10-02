
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
   public partial class Template : BusinessBase<System.Guid>
   {
      public Template() { }

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
        public virtual string PosSmo { get; set;}
        
        [XmlElement(Order =  4)]
        [DataMember(Order =  4)]
        public virtual string PosAddress { get; set;}
        
        [XmlElement(Order =  5)]
        [DataMember(Order =  5)]
        public virtual string PosDay1 { get; set;}
        
        [XmlElement(Order =  6)]
        [DataMember(Order =  6)]
        public virtual string PosMonth1 { get; set;}
        
        [XmlElement(Order =  7)]
        [DataMember(Order =  7)]
        public virtual string PosYear1 { get; set;}
        
        [XmlElement(Order =  8)]
        [DataMember(Order =  8)]
        public virtual string PosBirthplace { get; set;}
        
        [XmlElement(Order =  9)]
        [DataMember(Order =  9)]
        public virtual string PosMale { get; set;}
        
        [XmlElement(Order =  10)]
        [DataMember(Order =  10)]
        public virtual string PosFemale { get; set;}
        
        [XmlElement(Order =  11)]
        [DataMember(Order =  11)]
        public virtual string PosDay2 { get; set;}
        
        [XmlElement(Order =  12)]
        [DataMember(Order =  12)]
        public virtual string PosMonth2 { get; set;}
        
        [XmlElement(Order =  13)]
        [DataMember(Order =  13)]
        public virtual string PosYear2 { get; set;}
        
        [XmlElement(Order =  14)]
        [DataMember(Order =  14)]
        public virtual string PosFio { get; set;}
        
        [XmlElement(Order =  15)]
        [DataMember(Order =  15)]
        public virtual string PosLine1 { get; set;}
        
        [XmlElement(Order =  16)]
        [DataMember(Order =  16)]
        public virtual string PosLin2 { get; set;}
        
        [XmlElement(Order =  17)]
        [DataMember(Order =  17)]
        public virtual string PosLine3 { get; set;}
        
        [XmlElement(Order =  18)]
        [DataMember(Order =  18)]
        public virtual bool? Default { get; set;}
        
        [XmlIgnore] 
        public virtual IList<RangeNumber> RangeNumbers { get; set;}
		
       }
}
