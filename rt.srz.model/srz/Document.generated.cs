// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Document.cs" company="РусБИТех">
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
   public partial class Document : BusinessBase<System.Guid>
   {
      public Document() { }

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
      public virtual string Series { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string Number { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string IssuingAuthority { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual System.DateTime? DateIssue { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual System.DateTime? DateExp { get; set;}
        
      [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
      public virtual bool IsBad { get; set;}
        
        
		  [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
		  public virtual Concept DocumentType { get; set;}
			
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<QueryResponse> QueryResponses { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Representative> Representatives { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Statement> Statements1 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Statement> Statements2 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Statement> Statements3 { get; set;}
      
   }
}
