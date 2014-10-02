
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
   public partial class Group : BusinessBase<System.Guid>
   {
      public Group() { }

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
        
        [XmlIgnore] 
        public virtual IList<UserGroupRole> UserGroupRoles { get; set;}
		
        [XmlIgnore] 
        public virtual IList<UserGroup> UserGroups { get; set;}
		
       }
}
