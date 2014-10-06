// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsuredPersonDatum.cs" company="РусБИТех">
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
   public partial class InsuredPersonDatum : BusinessBase<System.Guid>
   {
      public InsuredPersonDatum() { }

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
      public virtual System.DateTime? Birthday { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual string Birthday2 { get; set;}
        
      [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
      public virtual int? BirthdayType { get; set;}
        
      [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
      public virtual bool? IsIncorrectDate { get; set;}
        
      [XmlElement(Order =  9)]
      [DataMember(Order =  9)]
      public virtual bool? IsNotGuru { get; set;}
        
      [XmlElement(Order =  10)]
      [DataMember(Order =  10)]
      public virtual string Snils { get; set;}
        
      [XmlElement(Order =  11)]
      [DataMember(Order =  11)]
      public virtual string Birthplace { get; set;}
        
      [XmlElement(Order =  12)]
      [DataMember(Order =  12)]
      public virtual bool IsNotCitizenship { get; set;}
        
      [XmlElement(Order =  13)]
      [DataMember(Order =  13)]
      public virtual bool IsRefugee { get; set;}
        
      [XmlElement(Order =  14)]
      [DataMember(Order =  14)]
      public virtual bool IsBadSnils { get; set;}
        
      [XmlElement(Order =  15)]
      [DataMember(Order =  15)]
      public virtual int NhFirstName { get; set;}
        
      [XmlElement(Order =  16)]
      [DataMember(Order =  16)]
      public virtual int NhLastName { get; set;}
        
      [XmlElement(Order =  17)]
      [DataMember(Order =  17)]
      public virtual int NhMiddleName { get; set;}
        
        
		  [XmlElement(Order =  18)]
      [DataMember(Order =  18)]
		  public virtual Concept Citizenship { get; set;}
			
        
		  [XmlElement(Order =  19)]
      [DataMember(Order =  19)]
		  public virtual Concept Gender { get; set;}
			
        
		  [XmlElement(Order =  20)]
      [DataMember(Order =  20)]
		  public virtual Concept Category { get; set;}
			
        
		  [XmlElement(Order =  21)]
      [DataMember(Order =  21)]
		  public virtual Concept OldCountry { get; set;}
			
      [XmlIgnore] 
      public virtual IList<Content> Contents { get; set;}
		
      [XmlIgnore] 
      public virtual IList<QueryResponse> QueryResponses { get; set;}
		
      [XmlIgnore] 
      public virtual IList<Statement> Statements { get; set;}
		
   }
}
