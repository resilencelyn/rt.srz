// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumberPolicyCounter.cs" company="Альянс">
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
   public partial class NumberPolicyCounter : BusinessBase<string>
   {
      public NumberPolicyCounter() { }

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
      public virtual int CurrentNumber { get; set;}
        
   }
}
