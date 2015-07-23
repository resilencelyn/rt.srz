// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MedicalInsurance.cs" company="Альянс">
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
   public partial class MedicalInsurance : BusinessBase<System.Guid>
   {
      public MedicalInsurance() { }

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
      public virtual string PolisSeria { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string PolisNumber { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual System.DateTime DateFrom { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual System.DateTime DateTo { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual bool IsActive { get; set;}
        
      [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
      public virtual System.DateTime? DateStop { get; set;}
        
      [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
      public virtual string Enp { get; set;}
        
      [XmlElement(Order =  9)]
      [DataMember(Order =  9)]
      public virtual System.DateTime StateDateFrom { get; set;}
        
      [XmlElement(Order =  10)]
      [DataMember(Order =  10)]
      public virtual System.DateTime StateDateTo { get; set;}
        
        
		  [XmlElement(Order =  11)]
      [DataMember(Order =  11)]
		  public virtual InsuredPerson InsuredPerson { get; set;}
			
        
		  [XmlElement(Order =  12)]
      [DataMember(Order =  12)]
		  public virtual Concept PolisType { get; set;}
			
        
		  [XmlElement(Order =  13)]
      [DataMember(Order =  13)]
		  public virtual Organisation Smo { get; set;}
			
        
		  [XmlElement(Order =  14)]
      [DataMember(Order =  14)]
		  public virtual Statement Statement { get; set;}
			
   }
}
