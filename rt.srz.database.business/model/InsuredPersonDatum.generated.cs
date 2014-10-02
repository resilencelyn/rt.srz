namespace rt.srz.database.business.model
{
  using System;
  using System.Xml.Serialization;

  [XmlRoot("InsuredPersonData")]
  public partial class InsuredPersonDatum
  {
    public InsuredPersonDatum() { }

    [XmlElement]
    public virtual Guid RowId { get; set; }

    [XmlElement]
    public virtual string FirstName { get; set; }

    [XmlElement]
    public virtual string LastName { get; set; }

    [XmlElement]
    public virtual string MiddleName { get; set; }

    [XmlElement]
    public virtual System.DateTime? Birthday { get; set; }

    [XmlElement]
    public virtual string Birthday2 { get; set; }

    [XmlElement]
    public virtual int? BirthdayType { get; set; }

    [XmlElement]
    public virtual bool? IsIncorrectDate { get; set; }

    [XmlElement]
    public virtual bool? IsNotGuru { get; set; }

    [XmlElement]
    public virtual string Snils { get; set; }

    [XmlElement]
    public virtual string Birthplace { get; set; }

    [XmlElement]
    public virtual bool IsNotCitizenship { get; set; }

    [XmlElement]
    public virtual bool IsRefugee { get; set; }

    [XmlElement]
    public virtual int? CitizenshipId { get; set; }

    [XmlElement]
    public virtual int? GenderId { get; set; }

    [XmlElement]
    public virtual int? CategoryId { get; set; }

    [XmlElement]
    public virtual int? OldCountryId { get; set; }
  }
}
