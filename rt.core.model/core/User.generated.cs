
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using rt.core.model;

namespace rt.core.model.core
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
        public virtual System.Guid? PointDistributionPolicyId { get; set;}
        
        [XmlElement(Order =  11)]
        [DataMember(Order =  11)]
        public virtual System.Guid? UserId1 { get; set;}
        
        [XmlElement(Order =  12)]
        [DataMember(Order =  12)]
        public virtual System.Guid? UserId2 { get; set;}
        
        [XmlElement(Order =  13)]
        [DataMember(Order =  13)]
        public virtual System.Guid UserId3 { get; set;}
        
        [XmlIgnore] 
        public virtual IList<UserGroupRole> UserGroupRoles { get; set;}
		
        [XmlIgnore] 
        public virtual IList<UserGroup> UserGroups { get; set;}
		
       }
}
