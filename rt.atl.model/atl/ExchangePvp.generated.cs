// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExchangePvp.cs" company="РусБИТех">
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
   public partial class ExchangePvp : BusinessBase<int>
   {
      public ExchangePvp() { }

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
      public virtual string StatementId { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual bool IsExport { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string Error { get; set;}
        
        
		  [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
		  public virtual Przbuf PrzBuff { get; set;}
			
   }
}
