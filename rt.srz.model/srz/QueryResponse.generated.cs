// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryResponse.cs" company="РусБИТех">
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
   public partial class QueryResponse : BusinessBase<System.Guid>
   {
      public QueryResponse() { }

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
      public virtual short? Number { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string PolisNumber { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string MainPolisNumber { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual string Snils { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual bool IsActive { get; set;}
        
      [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
      public virtual bool Employment { get; set;}
        
        
		  [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
		  public virtual address Address { get; set;}
			
        
		  [XmlElement(Order =  9)]
      [DataMember(Order =  9)]
		  public virtual Concept Feature { get; set;}
			
        
		  [XmlElement(Order =  10)]
      [DataMember(Order =  10)]
		  public virtual DeadInfo DeadInfo { get; set;}
			
        
		  [XmlElement(Order =  11)]
      [DataMember(Order =  11)]
		  public virtual Document DocumentUdl { get; set;}
			
        
		  [XmlElement(Order =  12)]
      [DataMember(Order =  12)]
		  public virtual InsuredPersonDatum InsuredPersonData { get; set;}
			
        
		  [XmlElement(Order =  13)]
      [DataMember(Order =  13)]
		  public virtual Message Message { get; set;}
			
        
		  [XmlElement(Order =  14)]
      [DataMember(Order =  14)]
		  public virtual Concept TrustLevel { get; set;}
			
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<EmploymentHistory> EmploymentHistories { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<In1> In1s { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<QueryResponseItemize> QueryResponseItemizes { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<SearchKey> SearchKeys { get; set;}
      
   }
}
