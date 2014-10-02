namespace rt.srz.database.business.model
{
  using System;
  using System.Xml.Serialization;

  [XmlRoot("Address")]
  public partial class address
  {
    public address() { }

    [XmlElement]
    public virtual Guid RowId { get; set; }

    [XmlElement]
    public virtual bool? IsHomeless { get; set; }

    [XmlElement]
    public virtual string Postcode { get; set; }

    [XmlElement]
    public virtual string Subject { get; set; }

    [XmlElement]
    public virtual string Area { get; set; }

    [XmlElement]
    public virtual string City { get; set; }

    [XmlElement]
    public virtual string Town { get; set; }

    [XmlElement]
    public virtual string Street { get; set; }

    [XmlElement]
    public virtual string House { get; set; }

    [XmlElement]
    public virtual string Housing { get; set; }

    [XmlElement]
    public virtual short? Room { get; set; }

    [XmlElement]
    public virtual System.DateTime? DateRegistration { get; set; }
    
    [XmlElement]
    public virtual bool? IsNotStructureAddress { get; set; }

    [XmlElement]
    public virtual string Okato { get; set; }
  }
}
