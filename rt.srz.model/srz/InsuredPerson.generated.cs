
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
   public partial class InsuredPerson : BusinessBase<System.Guid>
   {
      public InsuredPerson() { }

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
        public virtual string MainPolisNumber { get; set;}
        
        
				[XmlElement(Order =  3)]
        [DataMember(Order =  3)]
		public virtual DeadInfo DeadInfo { get; set;}
			
        
				[XmlElement(Order =  4)]
        [DataMember(Order =  4)]
		public virtual Concept Status { get; set;}
			
        [XmlIgnore] 
        public virtual IList<EmploymentHistory> EmploymentHistories { get; set;}
		
        [XmlIgnore] 
        public virtual IList<MedicalInsurance> MedicalInsurances { get; set;}
		
        [XmlIgnore] 
        public virtual IList<SearchKey> SearchKeys { get; set;}
		
        [XmlIgnore] 
        public virtual IList<Twin> Twins1 { get; set;}
		
        [XmlIgnore] 
        public virtual IList<Twin> Twins2 { get; set;}
		
        [XmlIgnore] 
        public virtual IList<Statement> Statements { get; set;}
		
       }
}
