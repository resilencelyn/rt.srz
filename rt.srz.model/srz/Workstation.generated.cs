// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Workstation.cs" company="РусБИТех">
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
   public partial class Workstation : BusinessBase<System.Guid>
   {
      public Workstation() { }

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
      public virtual string Name { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string UecReaderName { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual byte? UecCerticateType { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual string SmardCardReaderName { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual string SmardCardTokenReaderName { get; set;}
        
        
		  [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
		  public virtual Organisation PointDistributionPolicy { get; set;}
			
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<SertificateUec> SertificateUecs { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Setting> Settings { get; set;}
      
   }
}
