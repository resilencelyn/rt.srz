
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
   public partial class TwinsKey : BusinessBase<System.Guid>
   {
      public TwinsKey() { }

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
		public virtual SearchKeyType KeyType { get; set;}
			
        
				[XmlElement(Order =  3)]
        [DataMember(Order =  3)]
		public virtual Twin Twin { get; set;}
			
       }
}
