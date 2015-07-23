// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchKeyType.cs" company="Альянс">
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
   public partial class SearchKeyType : BusinessBase<System.Guid>
   {
      public SearchKeyType() { }

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
      public virtual string Code { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string Name { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual bool IsActive { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual bool FirstName { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual bool LastName { get; set;}
        
      [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
      public virtual bool MiddleName { get; set;}
        
      [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
      public virtual bool Birthday { get; set;}
        
      [XmlElement(Order =  9)]
      [DataMember(Order =  9)]
      public virtual bool Birthplace { get; set;}
        
      [XmlElement(Order =  10)]
      [DataMember(Order =  10)]
      public virtual bool Snils { get; set;}
        
      [XmlElement(Order =  11)]
      [DataMember(Order =  11)]
      public virtual bool DocumentType { get; set;}
        
      [XmlElement(Order =  12)]
      [DataMember(Order =  12)]
      public virtual bool DocumentSeries { get; set;}
        
      [XmlElement(Order =  13)]
      [DataMember(Order =  13)]
      public virtual bool DocumentNumber { get; set;}
        
      [XmlElement(Order =  14)]
      [DataMember(Order =  14)]
      public virtual bool Okato { get; set;}
        
      [XmlElement(Order =  15)]
      [DataMember(Order =  15)]
      public virtual bool PolisType { get; set;}
        
      [XmlElement(Order =  16)]
      [DataMember(Order =  16)]
      public virtual bool PolisSeria { get; set;}
        
      [XmlElement(Order =  17)]
      [DataMember(Order =  17)]
      public virtual bool PolisNumber { get; set;}
        
      [XmlElement(Order =  18)]
      [DataMember(Order =  18)]
      public virtual short FirstNameLength { get; set;}
        
      [XmlElement(Order =  19)]
      [DataMember(Order =  19)]
      public virtual short LastNameLength { get; set;}
        
      [XmlElement(Order =  20)]
      [DataMember(Order =  20)]
      public virtual short MiddleNameLength { get; set;}
        
      [XmlElement(Order =  21)]
      [DataMember(Order =  21)]
      public virtual short BirthdayLength { get; set;}
        
      [XmlElement(Order =  22)]
      [DataMember(Order =  22)]
      public virtual bool AddressStreet { get; set;}
        
      [XmlElement(Order =  23)]
      [DataMember(Order =  23)]
      public virtual short AddressStreetLength { get; set;}
        
      [XmlElement(Order =  24)]
      [DataMember(Order =  24)]
      public virtual bool AddressHouse { get; set;}
        
      [XmlElement(Order =  25)]
      [DataMember(Order =  25)]
      public virtual bool AddressRoom { get; set;}
        
      [XmlElement(Order =  26)]
      [DataMember(Order =  26)]
      public virtual bool AddressStreet2 { get; set;}
        
      [XmlElement(Order =  27)]
      [DataMember(Order =  27)]
      public virtual short AddressStreetLength2 { get; set;}
        
      [XmlElement(Order =  28)]
      [DataMember(Order =  28)]
      public virtual bool AddressHouse2 { get; set;}
        
      [XmlElement(Order =  29)]
      [DataMember(Order =  29)]
      public virtual bool AddressRoom2 { get; set;}
        
      [XmlElement(Order =  30)]
      [DataMember(Order =  30)]
      public virtual bool DeleteTwinChar { get; set;}
        
      [XmlElement(Order =  31)]
      [DataMember(Order =  31)]
      public virtual string IdenticalLetters { get; set;}
        
      [XmlElement(Order =  32)]
      [DataMember(Order =  32)]
      public virtual bool Recalculated { get; set;}
        
      [XmlElement(Order =  33)]
      [DataMember(Order =  33)]
      public virtual bool Enp { get; set;}
        
      [XmlElement(Order =  34)]
      [DataMember(Order =  34)]
      public virtual bool MainEnp { get; set;}
        
      [XmlElement(Order =  35)]
      [DataMember(Order =  35)]
      public virtual int Weight { get; set;}
        
      [XmlElement(Order =  36)]
      [DataMember(Order =  36)]
      public virtual bool Insertion { get; set;}
        
        
		  [XmlElement(Order =  37)]
      [DataMember(Order =  37)]
		  public virtual Concept OperationKey { get; set;}
			
        
		  [XmlElement(Order =  38)]
      [DataMember(Order =  38)]
		  public virtual Organisation Tfoms { get; set;}
			
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<QueryResponseItemize> QueryResponseItemizes { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<SearchKey> SearchKeys { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<TwinsKey> TwinsKeys { get; set;}
      
   }
}
