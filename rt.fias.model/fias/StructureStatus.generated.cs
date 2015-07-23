// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructureStatus.cs" company="Альянс">
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
   public partial class StructureStatus : BusinessBase<int>
   {
      public StructureStatus() { }

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
      public virtual string Name { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string Shortname { get; set;}
        
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<House> Houses { get; set;}
      
   }
}
