// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContactInfo.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The addressManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
   public partial class ContactInfo : BusinessBase<System.Guid>
   {
      public ContactInfo() { }

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
      public virtual string HomePhone { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string WorkPhone { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string Email { get; set;}
        
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Statement> Statements { get; set;}
      
   }
}
