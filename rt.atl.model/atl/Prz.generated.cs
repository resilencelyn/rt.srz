
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace rt.atl.model.atl
{
  using rt.core.model;

  [DataContract] 
   public partial class Prz : BusinessBase<int>
   {
      public Prz() { }

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
        public virtual System.DateTime? Dedit { get; set;}
        
        [XmlElement(Order =  3)]
        [DataMember(Order =  3)]
        public virtual string Caption { get; set;}
        
        [XmlElement(Order =  4)]
        [DataMember(Order =  4)]
        public virtual string Code { get; set;}
        
        [XmlElement(Order =  5)]
        [DataMember(Order =  5)]
        public virtual string Fullname { get; set;}
        
        [XmlElement(Order =  6)]
        [DataMember(Order =  6)]
        public virtual string Ogrn { get; set;}
        
        [XmlElement(Order =  7)]
        [DataMember(Order =  7)]
        public virtual string Bossname { get; set;}
        
        [XmlElement(Order =  8)]
        [DataMember(Order =  8)]
        public virtual string Buhname { get; set;}
        
        [XmlElement(Order =  9)]
        [DataMember(Order =  9)]
        public virtual string Email { get; set;}
        
        [XmlElement(Order =  10)]
        [DataMember(Order =  10)]
        public virtual string Tel1 { get; set;}
        
        [XmlElement(Order =  11)]
        [DataMember(Order =  11)]
        public virtual string Tel2 { get; set;}
        
        [XmlElement(Order =  12)]
        [DataMember(Order =  12)]
        public virtual string Addr { get; set;}
        
        [XmlElement(Order =  13)]
        [DataMember(Order =  13)]
        public virtual string Okato { get; set;}
        
        [XmlElement(Order =  14)]
        [DataMember(Order =  14)]
        public virtual string Extcode { get; set;}
        
        [XmlElement(Order =  15)]
        [DataMember(Order =  15)]
        public virtual bool? Main { get; set;}
        
        [DataMember(Order =  18)]
		public virtual Smo SMO { get; set;}
			
       }
}