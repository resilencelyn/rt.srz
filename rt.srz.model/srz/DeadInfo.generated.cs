
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
   public partial class DeadInfo : BusinessBase<System.Guid>
   {
      public DeadInfo() { }

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
        public virtual System.DateTime DateDead { get; set;}
        
        [XmlElement(Order =  3)]
        [DataMember(Order =  3)]
        public virtual System.DateTime? ActRecordDate { get; set;}
        
        [XmlElement(Order =  4)]
        [DataMember(Order =  4)]
        public virtual int? ActRecordNumber { get; set;}
        
        [XmlIgnore] 
        public virtual IList<InsuredPerson> InsuredPeople { get; set;}
		
        [XmlIgnore] 
        public virtual IList<QueryResponse> QueryResponses { get; set;}
		
       }
}
