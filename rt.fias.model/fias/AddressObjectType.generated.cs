// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressObjectType.cs" company="РусБИТех">
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
   public partial class AddressObjectType : BusinessBase<string>
   {
      public AddressObjectType() { }

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
      public virtual int Level { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string Scname { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string Socrname { get; set;}
        
   }
}
