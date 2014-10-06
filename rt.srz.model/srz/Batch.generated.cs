// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Batch.cs" company="РусБИТех">
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
   public partial class Batch : BusinessBase<System.Guid>
   {
      public Batch() { }

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
      public virtual string FileName { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual short Number { get; set;}
        
        
		  [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
		  public virtual Concept CodeConfirm { get; set;}
			
        
		  [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
		  public virtual Concept Subject { get; set;}
			
        
		  [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
		  public virtual Concept Type { get; set;}
			
        
		  [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
		  public virtual Period Period { get; set;}
			
        
		  [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
		  public virtual Organisation Receiver { get; set;}
			
        
		  [XmlElement(Order =  9)]
      [DataMember(Order =  9)]
		  public virtual Organisation Sender { get; set;}
			
      [XmlIgnore] 
      public virtual IList<Message> Messages { get; set;}
		
   }
}
