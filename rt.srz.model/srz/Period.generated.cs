// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Period.cs" company="РусБИТех">
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
   public partial class Period : BusinessBase<System.Guid>
   {
      public Period() { }

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
      public virtual System.DateTime Year { get; set;}
        
        
		  [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
		  public virtual Concept Code { get; set;}
			
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Batch> Batches { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<EmploymentHistory> EmploymentHistories { get; set;}
      
   }
}
