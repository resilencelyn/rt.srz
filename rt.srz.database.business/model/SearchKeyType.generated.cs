namespace rt.srz.database.business.model
{
  using System;
  using System.Xml.Serialization;

  public partial class SearchKeyType
  {
    public SearchKeyType() { }

    [XmlElement]
    public virtual Guid RowId { get; set; }
    
    [XmlElement]
    public virtual string Code { get; set; }

    [XmlElement]
    public virtual string Name { get; set; }

    [XmlElement]
    public virtual bool IsActive { get; set; }

    [XmlElement]
    public virtual bool SaveAndMoveToTwin { get; set; }

    [XmlElement]
    public virtual bool FirstName { get; set; }

    [XmlElement]
    public virtual bool LastName { get; set; }

    [XmlElement]
    public virtual bool MiddleName { get; set; }

    [XmlElement]
    public virtual bool Birthday { get; set; }

    [XmlElement]
    public virtual bool Birthplace { get; set; }
    
    [XmlElement]
    public virtual bool Snils { get; set; }

    [XmlElement]
    public virtual bool DocumentType { get; set; }

    [XmlElement]
    public virtual bool DocumentSeries { get; set; }

    [XmlElement]
    public virtual bool DocumentNumber { get; set; }

    [XmlElement]
    public virtual bool Okato { get; set; }

    [XmlElement]
    public virtual bool PolisType { get; set; }

    [XmlElement]
    public virtual bool PolisSeria { get; set; }

    [XmlElement]
    public virtual bool PolisNumber { get; set; }

    [XmlElement]
    public virtual short FirstNameLength { get; set; }

    [XmlElement]
    public virtual short LastNameLength { get; set; }

    [XmlElement]
    public virtual short MiddleNameLength { get; set; }

    [XmlElement]
    public virtual short BirthdayLength { get; set; }

    [XmlElement]
    public virtual bool AddressStreet { get; set; }

    [XmlElement]
    public virtual short AddressStreetLength { get; set; }

    [XmlElement]
    public virtual bool AddressHouse { get; set; }

    [XmlElement]
    public virtual bool AddressRoom { get; set; }

    [XmlElement]
    public virtual bool AddressStreet2 { get; set; }

    [XmlElement]
    public virtual short AddressStreetLength2 { get; set; }

    [XmlElement]
    public virtual bool AddressHouse2 { get; set; }

    [XmlElement]
    public virtual bool AddressRoom2 { get; set; }

    [XmlElement]
    public virtual bool DeleteTwinChar { get; set; }

    [XmlElement]
    public virtual string IdenticalLetters { get; set; }

    [XmlElement]
    public virtual int OperationKeyId { get; set; }
  }
}
