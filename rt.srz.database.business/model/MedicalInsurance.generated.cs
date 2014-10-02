namespace rt.srz.database.business.model
{
  using System;
  using System.Xml.Serialization;

  public partial class MedicalInsurance
  {
    public MedicalInsurance() { }

    [XmlElement]
    public virtual Guid RowId { get; set; }

    [XmlElement]
    public virtual string PolisSeria { get; set; }

    [XmlElement]
    public virtual string PolisNumber { get; set; }

    [XmlElement]
    public virtual System.DateTime DateFrom { get; set; }

    [XmlElement]
    public virtual System.DateTime DateTo { get; set; }

    [XmlElement]
    public virtual int? PolisTypeId { get; set; }
  }
}
