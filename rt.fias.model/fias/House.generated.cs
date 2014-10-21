// --------------------------------------------------------------------------------------------------------------------
// <copyright file="House.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using rt.core.model;

namespace rt.fias.model.fias
{
   [DataContract] 
	 [Serializable]
   public partial class House : BusinessBase<System.Guid>
   {
      public House() { }

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
      public virtual string Postalcode { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string Ifnsfl { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string Terrifnsfl { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual string Ifnsul { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual string Terrifnsul { get; set;}
        
      [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
      public virtual string Okato { get; set;}
        
      [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
      public virtual string Oktmo { get; set;}
        
      [XmlElement(Order =  9)]
      [DataMember(Order =  9)]
      public virtual System.DateTime Updatedate { get; set;}
        
      [XmlElement(Order =  10)]
      [DataMember(Order =  10)]
      public virtual string Housenum { get; set;}
        
      [XmlElement(Order =  11)]
      [DataMember(Order =  11)]
      public virtual string Buildnum { get; set;}
        
      [XmlElement(Order =  12)]
      [DataMember(Order =  12)]
      public virtual string Strucnum { get; set;}
        
      [XmlElement(Order =  13)]
      [DataMember(Order =  13)]
      public virtual System.Guid Houseguid { get; set;}
        
      [XmlElement(Order =  14)]
      [DataMember(Order =  14)]
      public virtual System.Guid Aoguid { get; set;}
        
      [XmlElement(Order =  15)]
      [DataMember(Order =  15)]
      public virtual System.DateTime Startdate { get; set;}
        
      [XmlElement(Order =  16)]
      [DataMember(Order =  16)]
      public virtual System.DateTime Enddate { get; set;}
        
      [XmlElement(Order =  17)]
      [DataMember(Order =  17)]
      public virtual System.Guid? Normdoc { get; set;}
        
      [XmlElement(Order =  18)]
      [DataMember(Order =  18)]
      public virtual int Counter { get; set;}
        
        
		  [XmlElement(Order =  19)]
      [DataMember(Order =  19)]
		  public virtual EstateStatus ESTSTATUS { get; set;}
			
        
		  [XmlElement(Order =  20)]
      [DataMember(Order =  20)]
		  public virtual HouseStateStatus STATSTATUS { get; set;}
			
        
		  [XmlElement(Order =  21)]
      [DataMember(Order =  21)]
		  public virtual StructureStatus STRSTATUS { get; set;}
			
   }
}
