// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Statement.cs" company="РусБИТех">
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
   public partial class Statement : BusinessBase<System.Guid>
   {
      public Statement() { }

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
      public virtual System.DateTime? DateFiling { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual bool? HasPetition { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string NumberPolicy { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual bool? AbsentPrevPolicy { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual bool IsActive { get; set;}
        
      [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
      public virtual bool? PolicyIsIssued { get; set;}
        
      [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
      public virtual int? PrzBuffId { get; set;}
        
      [XmlElement(Order =  9)]
      [DataMember(Order =  9)]
      public virtual int? PidId { get; set;}
        
      [XmlElement(Order =  10)]
      [DataMember(Order =  10)]
      public virtual int? PolisId { get; set;}
        
      [XmlElement(Order =  11)]
      [DataMember(Order =  11)]
      public virtual bool IsExportTemp { get; set;}
        
      [XmlElement(Order =  12)]
      [DataMember(Order =  12)]
      public virtual bool IsExportPolis { get; set;}
        
      [XmlElement(Order =  13)]
      [DataMember(Order =  13)]
      public virtual int? PrzBuffPolisId { get; set;}
        
      [XmlElement(Order =  14)]
      [DataMember(Order =  14)]
      public virtual int Version { get; set;}
        
      [XmlElement(Order =  15)]
      [DataMember(Order =  15)]
      public virtual System.Guid? UserId { get; set;}
        
        
		  [XmlElement(Order =  16)]
      [DataMember(Order =  16)]
		  public virtual Organisation PointDistributionPolicy { get; set;}
			
        
		  [XmlElement(Order =  17)]
      [DataMember(Order =  17)]
		  public virtual Statement PreviousStatement { get; set;}
			
        
		  [XmlElement(Order =  18)]
      [DataMember(Order =  18)]
		  public virtual Concept Status { get; set;}
			
        
		  [XmlElement(Order =  19)]
      [DataMember(Order =  19)]
		  public virtual InsuredPersonDatum InsuredPersonData { get; set;}
			
        
		  [XmlElement(Order =  20)]
      [DataMember(Order =  20)]
		  public virtual InsuredPerson InsuredPerson { get; set;}
			
        
		  [XmlElement(Order =  21)]
      [DataMember(Order =  21)]
		  public virtual Concept CauseFiling { get; set;}
			
        
		  [XmlElement(Order =  22)]
      [DataMember(Order =  22)]
		  public virtual Concept ModeFiling { get; set;}
			
        
		  [XmlElement(Order =  23)]
      [DataMember(Order =  23)]
		  public virtual Concept FormManufacturing { get; set;}
			
        
		  [XmlElement(Order =  24)]
      [DataMember(Order =  24)]
		  public virtual Document DocumentUdl { get; set;}
			
        
		  [XmlElement(Order =  25)]
      [DataMember(Order =  25)]
		  public virtual ContactInfo ContactInfo { get; set;}
			
        
		  [XmlElement(Order =  26)]
      [DataMember(Order =  26)]
		  public virtual Representative Representative { get; set;}
			
        
		  [XmlElement(Order =  27)]
      [DataMember(Order =  27)]
		  public virtual Document ResidencyDocument { get; set;}
			
        
		  [XmlElement(Order =  28)]
      [DataMember(Order =  28)]
		  public virtual address Address { get; set;}
			
        
		  [XmlElement(Order =  29)]
      [DataMember(Order =  29)]
		  public virtual Document DocumentRegistration { get; set;}
			
        
		  [XmlElement(Order =  30)]
      [DataMember(Order =  30)]
		  public virtual address Address2 { get; set;}
			
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Error> Errors { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<MedicalInsurance> MedicalInsurances { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<MessageStatement> MessageStatements { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<SearchKey> SearchKeys { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Statement> Statements { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<StatementChangeDate> StatementChangeDates { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<UserAction> UserActions { get; set;}
      
   }
}
