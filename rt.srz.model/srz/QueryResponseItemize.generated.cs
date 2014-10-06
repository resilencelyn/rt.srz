// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryResponseItemize.cs" company="РусБИТех">
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

namespace rt.srz.model.srz
{
   [DataContract] 
	 [Serializable]
   public partial class QueryResponseItemize : BusinessBase<System.Guid>
   {
      public QueryResponseItemize() { }

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
		  public virtual QueryResponse QueryResponse { get; set;}
			
        
		  [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
		  public virtual SearchKeyType KeyType { get; set;}
			
   }
}
