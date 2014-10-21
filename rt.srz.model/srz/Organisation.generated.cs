// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Organisation.cs" company="РусБИТех">
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
   public partial class Organisation : BusinessBase<System.Guid>
   {
      public Organisation() { }

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
      public virtual bool IsActive { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual bool IsOnLine { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string Code { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual string FullName { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual string ShortName { get; set;}
        
      [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
      public virtual string Inn { get; set;}
        
      [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
      public virtual string Ogrn { get; set;}
        
      [XmlElement(Order =  9)]
      [DataMember(Order =  9)]
      public virtual string Postcode { get; set;}
        
      [XmlElement(Order =  10)]
      [DataMember(Order =  10)]
      public virtual string LastName { get; set;}
        
      [XmlElement(Order =  11)]
      [DataMember(Order =  11)]
      public virtual string FirstName { get; set;}
        
      [XmlElement(Order =  12)]
      [DataMember(Order =  12)]
      public virtual string MiddleName { get; set;}
        
      [XmlElement(Order =  13)]
      [DataMember(Order =  13)]
      public virtual string Phone { get; set;}
        
      [XmlElement(Order =  14)]
      [DataMember(Order =  14)]
      public virtual string Fax { get; set;}
        
      [XmlElement(Order =  15)]
      [DataMember(Order =  15)]
      public virtual string EMail { get; set;}
        
      [XmlElement(Order =  16)]
      [DataMember(Order =  16)]
      public virtual string Website { get; set;}
        
      [XmlElement(Order =  17)]
      [DataMember(Order =  17)]
      public virtual string LicenseData { get; set;}
        
      [XmlElement(Order =  18)]
      [DataMember(Order =  18)]
      public virtual string LicenseNumber { get; set;}
        
      [XmlElement(Order =  19)]
      [DataMember(Order =  19)]
      public virtual System.DateTime? DateLicensing { get; set;}
        
      [XmlElement(Order =  20)]
      [DataMember(Order =  20)]
      public virtual System.DateTime? DateExpiryLicense { get; set;}
        
      [XmlElement(Order =  21)]
      [DataMember(Order =  21)]
      public virtual bool? IsSubordination { get; set;}
        
      [XmlElement(Order =  22)]
      [DataMember(Order =  22)]
      public virtual System.DateTime? DateIncludeRegister { get; set;}
        
      [XmlElement(Order =  23)]
      [DataMember(Order =  23)]
      public virtual System.DateTime? DateExcludeRegister { get; set;}
        
      [XmlElement(Order =  24)]
      [DataMember(Order =  24)]
      public virtual bool? HasActivePolicy { get; set;}
        
      [XmlElement(Order =  25)]
      [DataMember(Order =  25)]
      public virtual System.DateTime? DateNotification { get; set;}
        
      [XmlElement(Order =  26)]
      [DataMember(Order =  26)]
      public virtual int? CountInsured { get; set;}
        
      [XmlElement(Order =  27)]
      [DataMember(Order =  27)]
      public virtual System.DateTime? DateLastEdit { get; set;}
        
      [XmlElement(Order =  28)]
      [DataMember(Order =  28)]
      public virtual string Okato { get; set;}
        
      [XmlElement(Order =  29)]
      [DataMember(Order =  29)]
      public virtual System.DateTime? TimeRunFrom { get; set;}
        
      [XmlElement(Order =  30)]
      [DataMember(Order =  30)]
      public virtual System.DateTime? TimeRunTo { get; set;}
        
      [XmlElement(Order =  31)]
      [DataMember(Order =  31)]
      public virtual string Address { get; set;}
        
        
		  [XmlElement(Order =  32)]
      [DataMember(Order =  32)]
		  public virtual Oid Oid { get; set;}
			
        
		  [XmlElement(Order =  33)]
      [DataMember(Order =  33)]
		  public virtual Organisation Parent { get; set;}
			
        
		  [XmlElement(Order =  34)]
      [DataMember(Order =  34)]
		  public virtual Organisation ChangedRow { get; set;}
			
        
		  [XmlElement(Order =  35)]
      [DataMember(Order =  35)]
		  public virtual Concept CauseRegistration { get; set;}
			
        
		  [XmlElement(Order =  36)]
      [DataMember(Order =  36)]
		  public virtual Concept CauseExclusion { get; set;}
			
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Batch> Batches1 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Batch> Batches2 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<MedicalInsurance> MedicalInsurances { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<In1> In1s { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Organisation> Organisations1 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<RangeNumber> RangeNumbers { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<SearchKeyType> SearchKeyTypes { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<SertificateUec> SertificateUecs { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Setting> Settings { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Statement> Statements { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Workstation> Workstations { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Organisation> Organisations2 { get; set;}
      
   }
}
