// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Poli.cs" company="РусБИТех">
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

namespace rt.atl.model.atl
{
   [DataContract] 
	 [Serializable]
   public partial class Poli : BusinessBase<int>
   {
      public Poli() { }

	    [XmlElement(Order = 1)]
      [DataMember(Order = 1)]
      public override int Id
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
      public virtual System.DateTime? Dedit { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string Q { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string Prz { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual System.DateTime? Dbeg { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual System.DateTime? Dend { get; set;}
        
      [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
      public virtual int? Poltp { get; set;}
        
      [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
      public virtual string Okato { get; set;}
        
      [XmlElement(Order =  9)]
      [DataMember(Order =  9)]
      public virtual string Spol { get; set;}
        
      [XmlElement(Order =  10)]
      [DataMember(Order =  10)]
      public virtual string Npol { get; set;}
        
      [XmlElement(Order =  11)]
      [DataMember(Order =  11)]
      public virtual string Qogrn { get; set;}
        
      [XmlElement(Order =  12)]
      [DataMember(Order =  12)]
      public virtual System.DateTime? Dstop { get; set;}
        
      [XmlElement(Order =  13)]
      [DataMember(Order =  13)]
      public virtual int? St { get; set;}
        
      [XmlElement(Order =  14)]
      [DataMember(Order =  14)]
      public virtual bool? Del { get; set;}
        
      [XmlElement(Order =  15)]
      [DataMember(Order =  15)]
      public virtual int? Rstop { get; set;}
        
      [XmlElement(Order =  16)]
      [DataMember(Order =  16)]
      public virtual string Nvs { get; set;}
        
      [XmlElement(Order =  17)]
      [DataMember(Order =  17)]
      public virtual System.DateTime? Dvs { get; set;}
        
      [XmlElement(Order =  18)]
      [DataMember(Order =  18)]
      public virtual System.DateTime? Et { get; set;}
        
      [XmlElement(Order =  19)]
      [DataMember(Order =  19)]
      public virtual System.DateTime? Dz { get; set;}
        
      [XmlElement(Order =  20)]
      [DataMember(Order =  20)]
      public virtual System.DateTime? Dp { get; set;}
        
      [XmlElement(Order =  21)]
      [DataMember(Order =  21)]
      public virtual System.DateTime? Dh { get; set;}
        
      [XmlElement(Order =  22)]
      [DataMember(Order =  22)]
      public virtual string Err { get; set;}
        
      [XmlElement(Order =  23)]
      [DataMember(Order =  23)]
      public virtual int? Polvid { get; set;}
        
      [XmlElement(Order =  24)]
      [DataMember(Order =  24)]
      public virtual int? Oldpid { get; set;}
        
      [XmlElement(Order =  25)]
      [DataMember(Order =  25)]
      public virtual System.DateTime? Sout { get; set;}
        
      [XmlElement(Order =  26)]
      [DataMember(Order =  26)]
      public virtual int? M2id { get; set;}
        
      [XmlElement(Order =  27)]
      [DataMember(Order =  27)]
      public virtual System.DateTime? DstopCs { get; set;}
        
      [XmlElement(Order =  28)]
      [DataMember(Order =  28)]
      public virtual System.DateTime? Unload { get; set;}
        
        
		  [XmlElement(Order =  29)]
      [DataMember(Order =  29)]
		  public virtual person P { get; set;}
			
      [XmlIgnore] 
      public virtual IList<Uec> Uecs { get; set;}
		
   }
}
