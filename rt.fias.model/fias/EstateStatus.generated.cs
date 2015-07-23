// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EstateStatus.cs" company="Альянс">
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
   public partial class EstateStatus : BusinessBase<int>
   {
      public EstateStatus() { }

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
        
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<House> Houses { get; set;}
      
   }
}
