
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
   public partial class Role : BusinessBase<System.Guid>
   {
      public Role() { }

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
        public virtual int? Code { get; set;}
        
        [XmlIgnore] 
        public virtual IList<PermissionRole> PermissionRoles { get; set;}
		
        [XmlIgnore] 
        public virtual IList<UserGroupRole> UserGroupRoles { get; set;}
		
       }
}
