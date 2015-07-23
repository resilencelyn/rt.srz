// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmploymentHistory.cs" company="Альянс">
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
   public partial class EmploymentHistory : BusinessBase<System.Guid>
   {
      public EmploymentHistory() { }

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
      public virtual bool Employment { get; set;}
        
        
		  [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
		  public virtual Concept SourceType { get; set;}
			
        
		  [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
		  public virtual InsuredPerson InsuredPerson { get; set;}
			
        
		  [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
		  public virtual Period Period { get; set;}
			
        
		  [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
		  public virtual QueryResponse QueryResponse { get; set;}
			
   }
}
