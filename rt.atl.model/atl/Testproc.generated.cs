
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace rt.atl.model.atl
{
  using rt.core.model;

  [DataContract] 
   public partial class Testproc : BusinessBase<int>
   {
      public Testproc() { }

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
        public virtual bool? Strong { get; set;}
        
        [XmlElement(Order =  6)]
        [DataMember(Order =  6)]
        public virtual bool? Act { get; set;}
        
        [XmlElement(Order =  7)]
        [DataMember(Order =  7)]
        public virtual bool? Srv { get; set;}
        
        [XmlElement(Order =  8)]
        [DataMember(Order =  8)]
        public virtual string Procname { get; set;}
        
        [XmlElement(Order =  9)]
        [DataMember(Order =  9)]
        public virtual bool? Nosrop { get; set;}
        
        [XmlElement(Order =  10)]
        [DataMember(Order =  10)]
        public virtual string Flds { get; set;}
        
       }
}
