// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
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
   public partial class Message : BusinessBase<System.Guid>
   {
      public Message() { }

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
      public virtual bool IsCommit { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual bool IsError { get; set;}
        
        
		  [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
		  public virtual Batch Batch { get; set;}
			
        
		  [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
		  public virtual Message DependsOnMessage { get; set;}
			
        
		  [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
		  public virtual Concept Reason { get; set;}
			
        
		  [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
		  public virtual Concept Type { get; set;}
			
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Error> Errors { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<QueryResponse> QueryResponses { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Message> Messages { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<MessageStatement> MessageStatements { get; set;}
      
   }
}
