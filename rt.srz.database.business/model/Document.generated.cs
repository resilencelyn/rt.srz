namespace rt.srz.database.business.model
{
  using System;
  using System.Xml.Serialization;

  public partial class Document
  {
    public Document() { }

    [XmlElement]
    public virtual Guid RowId { get; set; }

    [XmlElement]
    public virtual string Series { get; set; }

    [XmlElement]
    public virtual string Number { get; set; }

    [XmlElement]
    public virtual string IssuingAuthority { get; set; }

    [XmlElement]
    public virtual System.DateTime? DateIssue { get; set; }

    [XmlElement]
    public virtual int? DocumentTypeId { get; set; }
  }
}
