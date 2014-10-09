// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Concept.cs" company="РусБИТех">
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
   public partial class Concept : BusinessBase<int>
   {
      public Concept() { }

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
      public virtual string Code { get; set;}
        
      [XmlElement(Order =  3)]
      [DataMember(Order =  3)]
      public virtual string Name { get; set;}
        
      [XmlElement(Order =  4)]
      [DataMember(Order =  4)]
      public virtual string Description { get; set;}
        
      [XmlElement(Order =  5)]
      [DataMember(Order =  5)]
      public virtual string ShortName { get; set;}
        
      [XmlElement(Order =  6)]
      [DataMember(Order =  6)]
      public virtual string RelatedCode { get; set;}
        
      [XmlElement(Order =  7)]
      [DataMember(Order =  7)]
      public virtual string RelatedOid { get; set;}
        
      [XmlElement(Order =  8)]
      [DataMember(Order =  8)]
      public virtual string RelatedType { get; set;}
        
      [XmlElement(Order =  9)]
      [DataMember(Order =  9)]
      public virtual System.DateTime? DateFrom { get; set;}
        
      [XmlElement(Order =  10)]
      [DataMember(Order =  10)]
      public virtual System.DateTime? DateTo { get; set;}
        
      [XmlElement(Order =  11)]
      [DataMember(Order =  11)]
      public virtual int Relevance { get; set;}
        
        
		  [XmlElement(Order =  12)]
      [DataMember(Order =  12)]
		  public virtual Oid Oid { get; set;}
			
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<AutoComplete> AutoCompletes1 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<AutoComplete> AutoCompletes2 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Batch> Batches1 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Batch> Batches2 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Batch> Batches3 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Content> Contents { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<EmploymentHistory> EmploymentHistories { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Error> Errors { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<InsuredPerson> InsuredPeople { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<QueryResponse> QueryResponses1 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<QueryResponse> QueryResponses2 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<MedicalInsurance> MedicalInsurances { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<In1> In1s { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Message> Messages1 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Message> Messages2 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<MessageStatement> MessageStatements { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Oid> Oids { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Period> Periods { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<SearchKey> SearchKeys { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<SearchKeyType> SearchKeyTypes { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<SertificateUec> SertificateUecs { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Statement> Statements1 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<StatementChangeDate> StatementChangeDates { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Twin> Twins { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<UserAction> UserActions { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Organisation> Organisations1 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Organisation> Organisations2 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Statement> Statements2 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Statement> Statements3 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Statement> Statements4 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<InsuredPersonDatum> InsuredPersonData1 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Document> Documents { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<InsuredPersonDatum> InsuredPersonData2 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<InsuredPersonDatum> InsuredPersonData3 { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<Representative> Representatives { get; set;}
      
      [XmlIgnore]
      [IgnoreDataMember]
      public virtual IList<InsuredPersonDatum> InsuredPersonData4 { get; set;}
      
   }
}
