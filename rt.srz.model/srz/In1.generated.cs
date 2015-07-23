// --------------------------------------------------------------------------------------------------------------------
// <copyright file="In1.cs" company="Альянс">
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
   public partial class In1 : BusinessBase<System.Guid>
   {
      public In1() { }

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
      public virtual short Number { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string PolisSeria { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string PolisNumber { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual System.DateTime DateFrom { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual System.DateTime? DateTo { get; set;}
        
      [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
      public virtual System.DateTime? DateStop { get; set;}
        
        
		  [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
		  public virtual QueryResponse QueryResponse { get; set;}
			
        
		  [XmlElement(Order =  9)]
      [DataMember(Order =  9)]
		  public virtual Concept PolisType { get; set;}
			
        
		  [XmlElement(Order =  10)]
      [DataMember(Order =  10)]
		  public virtual Organisation Smo { get; set;}
			
   }
}
