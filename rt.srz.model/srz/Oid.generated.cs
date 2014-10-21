// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Oid.cs" company="РусБИТех">
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
   public partial class Oid : BusinessBase<string>
   {
      public Oid() { }

	    [XmlElement(Order = 1)]
      [DataMember(Order = 1)]
      public override string Id
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
      public virtual string FullName { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string ShortName { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string LatinName { get; set;}
        
        
		  [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
		  public virtual Concept Default { get; set;}
			
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Organisation> Organisations { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Concept> Concepts { get; set;}
      
   }
}
