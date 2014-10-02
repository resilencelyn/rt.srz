
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
   public partial class User : BusinessBase<System.Guid>
   {
      public User() { }

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
        public virtual string Login { get; set;}
        
        [XmlElement(Order =  3)]
        [DataMember(Order =  3)]
        public virtual string Password { get; set;}
        
        [XmlElement(Order =  4)]
        [DataMember(Order =  4)]
        public virtual string Email { get; set;}
        
        [XmlElement(Order =  5)]
        [DataMember(Order =  5)]
        public virtual string Salt { get; set;}
        
        [XmlElement(Order =  6)]
        [DataMember(Order =  6)]
        public virtual System.DateTime CreationDate { get; set;}
        
        [XmlElement(Order =  7)]
        [DataMember(Order =  7)]
        public virtual System.DateTime LastLoginDate { get; set;}
        
        [XmlElement(Order =  8)]
        [DataMember(Order =  8)]
        public virtual bool IsApproved { get; set;}
        
        [XmlElement(Order =  9)]
        [DataMember(Order =  9)]
        public virtual string Fio { get; set;}
        
        
				[XmlElement(Order =  10)]
        [DataMember(Order =  10)]
		public virtual Organisation PointDistributionPolicy { get; set;}
			
        [XmlIgnore] 
        public virtual IList<Setting> Settings { get; set;}
		
        [XmlIgnore] 
        public virtual IList<Statement> Statements { get; set;}
		
        [XmlIgnore] 
        public virtual IList<UserAction> UserActions { get; set;}
		
        [XmlIgnore] 
        public virtual IList<UserGroupRole> UserGroupRoles { get; set;}
		
        [XmlIgnore] 
        public virtual IList<UserGroup> UserGroups { get; set;}
		
       }
}
