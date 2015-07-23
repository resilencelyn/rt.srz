// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AObject.cs" company="Альянс">
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
   public partial class AObject : BusinessBase<System.Guid>
   {
      public AObject() { }

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
      public virtual System.Guid Aoguid { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string Formalname { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string Regioncode { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual string Autocode { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual string Areacode { get; set;}
        
      [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
      public virtual string Citycode { get; set;}
        
      [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
      public virtual string Ctarcode { get; set;}
        
      [XmlElement(Order =  9)]
      [DataMember(Order =  9)]
      public virtual string Placecode { get; set;}
        
      [XmlElement(Order =  10)]
      [DataMember(Order =  10)]
      public virtual string Streetcode { get; set;}
        
      [XmlElement(Order =  11)]
      [DataMember(Order =  11)]
      public virtual string Extrcode { get; set;}
        
      [XmlElement(Order =  12)]
      [DataMember(Order =  12)]
      public virtual string Sextcode { get; set;}
        
      [XmlElement(Order =  13)]
      [DataMember(Order =  13)]
      public virtual string Offname { get; set;}
        
      [XmlElement(Order =  14)]
      [DataMember(Order =  14)]
      public virtual string Postalcode { get; set;}
        
      [XmlElement(Order =  15)]
      [DataMember(Order =  15)]
      public virtual string Ifnsfl { get; set;}
        
      [XmlElement(Order =  16)]
      [DataMember(Order =  16)]
      public virtual string Terrifnsfl { get; set;}
        
      [XmlElement(Order =  17)]
      [DataMember(Order =  17)]
      public virtual string Ifnsul { get; set;}
        
      [XmlElement(Order =  18)]
      [DataMember(Order =  18)]
      public virtual string Terrifnsul { get; set;}
        
      [XmlElement(Order =  19)]
      [DataMember(Order =  19)]
      public virtual string Okato { get; set;}
        
      [XmlElement(Order =  20)]
      [DataMember(Order =  20)]
      public virtual string Oktmo { get; set;}
        
      [XmlElement(Order =  21)]
      [DataMember(Order =  21)]
      public virtual System.DateTime Updatedate { get; set;}
        
      [XmlElement(Order =  22)]
      [DataMember(Order =  22)]
      public virtual string Shortname { get; set;}
        
      [XmlElement(Order =  23)]
      [DataMember(Order =  23)]
      public virtual int Aolevel { get; set;}
        
      [XmlElement(Order =  24)]
      [DataMember(Order =  24)]
      public virtual System.Guid? Parentguid { get; set;}
        
      [XmlElement(Order =  25)]
      [DataMember(Order =  25)]
      public virtual string Code { get; set;}
        
      [XmlElement(Order =  26)]
      [DataMember(Order =  26)]
      public virtual string Plaincode { get; set;}
        
      [XmlElement(Order =  27)]
      [DataMember(Order =  27)]
      public virtual System.DateTime Startdate { get; set;}
        
      [XmlElement(Order =  28)]
      [DataMember(Order =  28)]
      public virtual System.DateTime Enddate { get; set;}
        
      [XmlElement(Order =  29)]
      [DataMember(Order =  29)]
      public virtual System.Guid? Normdoc { get; set;}
        
      [XmlElement(Order =  30)]
      [DataMember(Order =  30)]
      public virtual string Livestatus { get; set;}
        
        
		  [XmlElement(Order =  31)]
      [DataMember(Order =  31)]
		  public virtual ActualStatus ACTSTATUS { get; set;}
			
        
		  [XmlElement(Order =  32)]
      [DataMember(Order =  32)]
		  public virtual CenterStatus CENTSTATUS { get; set;}
			
        
		  [XmlElement(Order =  33)]
      [DataMember(Order =  33)]
		  public virtual CurrentStatus CURRSTATUS { get; set;}
			
        
		  [XmlElement(Order =  34)]
      [DataMember(Order =  34)]
		  public virtual AObject NEXT { get; set;}
			
        
		  [XmlElement(Order =  35)]
      [DataMember(Order =  35)]
		  public virtual AObject PREV { get; set;}
			
        
		  [XmlElement(Order =  36)]
      [DataMember(Order =  36)]
		  public virtual OperationStatus OPERSTATUS { get; set;}
			
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<AObject> AObjects1 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<AObject> AObjects2 { get; set;}
      
   }
}
