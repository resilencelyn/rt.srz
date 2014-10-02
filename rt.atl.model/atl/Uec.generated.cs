
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
   public partial class Uec : BusinessBase<int>
   {
      public Uec() { }

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
        public virtual string Ncard { get; set;}
        
        [XmlElement(Order =  3)]
        [DataMember(Order =  3)]
        public virtual int? Ufile { get; set;}
        
        
				[XmlElement(Order =  4)]
        [DataMember(Order =  4)]
		public virtual person P { get; set;}
			
        
				[XmlElement(Order =  5)]
        [DataMember(Order =  5)]
		public virtual Poli POLIS { get; set;}
			
        
				[XmlElement(Order =  6)]
        [DataMember(Order =  6)]
		public virtual Uechiststatus UECLASTSTATUS { get; set;}
			
        [XmlIgnore] 
        public virtual IList<Uechiststatus> Uechiststatuses { get; set;}
		
       }
}
