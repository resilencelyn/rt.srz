// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Przlog.cs" company="РусБИТех">
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
   public partial class Przlog : BusinessBase<int>
   {
      public Przlog() { }

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
      public virtual string Filename { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string Q { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string Prz { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual int? Mm { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual int? Gg { get; set;}
        
      [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
      public virtual int? Zz { get; set;}
        
      [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
      public virtual System.DateTime? Dtin { get; set;}
        
      [XmlElement(Order =  9)]
      [DataMember(Order =  9)]
      public virtual System.DateTime? Dtout { get; set;}
        
      [XmlElement(Order =  10)]
      [DataMember(Order =  10)]
      public virtual int? Reccount { get; set;}
        
      [XmlElement(Order =  11)]
      [DataMember(Order =  11)]
      public virtual string Tpfile { get; set;}
        
      [XmlElement(Order =  12)]
      [DataMember(Order =  12)]
      public virtual int? Nerr { get; set;}
        
      [XmlElement(Order =  13)]
      [DataMember(Order =  13)]
      public virtual int? Nz { get; set;}
        
      [XmlElement(Order =  14)]
      [DataMember(Order =  14)]
      public virtual string Errfile { get; set;}
        
      [XmlElement(Order =  15)]
      [DataMember(Order =  15)]
      public virtual int? St { get; set;}
        
      [XmlElement(Order =  16)]
      [DataMember(Order =  16)]
      public virtual string Vers { get; set;}
        
      [XmlIgnore] 
      public virtual IList<Przbuf> Przbufs { get; set;}
		
   }
}
