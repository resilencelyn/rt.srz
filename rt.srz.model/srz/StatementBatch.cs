namespace rt.srz.model.srz
{
  using System;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  /// <summary>
  ///   The statement maximum.
  /// </summary>
  public class StatementBatch : Statement
  {
    [XmlElement(Order = 40)]
    [DataMember(Order = 40)]
    public virtual Guid BatchId { get; set; }

    [XmlElement(Order = 41)]
    [DataMember(Order = 41)]
    public virtual int VersionExport { get; set; }

    [XmlElement(Order = 42)]
    [DataMember(Order = 42)]
    public virtual string Kladr { get; set; }

    [XmlElement(Order = 43)]
    [DataMember(Order = 43)]
    public virtual string Kladr2 { get; set; }
  }
}