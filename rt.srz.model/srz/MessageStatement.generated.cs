// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageStatement.cs" company="РусБИТех">
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
   public partial class MessageStatement : BusinessBase<System.Guid>
   {
      public MessageStatement() { }

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
      public virtual int Version { get; set;}
        
        
		  [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
		  public virtual Concept Type { get; set;}
			
        
		  [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
		  public virtual Message Message { get; set;}
			
        
		  [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
		  public virtual Statement Statement { get; set;}
			
   }
}
