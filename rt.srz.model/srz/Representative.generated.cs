// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Representative.cs" company="РусБИТех">
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
   public partial class Representative : BusinessBase<System.Guid>
   {
      public Representative() { }

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
      public virtual string FirstName { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string LastName { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string MiddleName { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual string HomePhone { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual string WorkPhone { get; set;}
        
        
		  [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
		  public virtual Document Document { get; set;}
			
        
		  [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
		  public virtual Concept RelationType { get; set;}
			
      [XmlIgnore] 
      public virtual IList<Statement> Statements { get; set;}
		
   }
}
