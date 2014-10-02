
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
   public partial class Uechiststatus : BusinessBase<int>
   {
      public Uechiststatus() { }

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
        public virtual System.DateTime? Dt { get; set;}
        
        [XmlElement(Order =  3)]
        [DataMember(Order =  3)]
        public virtual string Uecstatus { get; set;}
        
        [XmlElement(Order =  4)]
        [DataMember(Order =  4)]
        public virtual int? Ufile { get; set;}
        
        
				[XmlElement(Order =  5)]
        [DataMember(Order =  5)]
		public virtual Uec UEC { get; set;}
			
        [XmlIgnore] 
        public virtual IList<ExchangePvp> ExchangePvps { get; set;}
		
        [XmlIgnore] 
        public virtual IList<Uec> Uecs { get; set;}
		
       }
}
