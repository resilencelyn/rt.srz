// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchKey.cs" company="РусБИТех">
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
   public partial class SearchKey : BusinessBase<System.Guid>
   {
      public SearchKey() { }

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
      public virtual byte[] KeyValue { get; set;}
        
        
		  [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
		  public virtual Concept DocumentUdlType { get; set;}
			
        
		  [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
		  public virtual InsuredPerson InsuredPerson { get; set;}
			
        
		  [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
		  public virtual QueryResponse QueryResponse { get; set;}
			
        
		  [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
		  public virtual SearchKeyType KeyType { get; set;}
			
        
		  [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
		  public virtual Statement Statement { get; set;}
			
   }
}
