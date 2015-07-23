// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructureAddress.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.interfaces
{
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  /// <summary>
  /// The structure address.
  /// </summary>
  public class StructureAddress
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the area.
    /// </summary>
    [XmlElement(Order = 5)]
    [DataMember(Order = 5)]
    public virtual string Area { get; set; }

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    [XmlElement(Order = 6)]
    [DataMember(Order = 6)]
    public virtual string City { get; set; }

    /// <summary>
    /// Gets or sets the street.
    /// </summary>
    [XmlElement(Order = 8)]
    [DataMember(Order = 8)]
    public virtual string Street { get; set; }

    /// <summary>
    /// Gets or sets the code.
    /// </summary>
    [XmlElement(Order = 25)]
    [DataMember(Order = 25)]
    public virtual string Code { get; set; }

    /// <summary>
    /// Gets or sets the code.
    /// </summary>
    [XmlElement(Order = 50)]
    [DataMember(Order = 50)]
    public virtual string OkatoRn { get; set; }

    /// <summary>
    /// Gets or sets the subject.
    /// </summary>
    [XmlElement(Order = 4)]
    [DataMember(Order = 4)]
    public virtual string Subject { get; set; }

    /// <summary>
    /// Gets or sets the town.
    /// </summary>
    [XmlElement(Order = 7)]
    [DataMember(Order = 7)]
    public virtual string Town { get; set; }

    #endregion
  }
}