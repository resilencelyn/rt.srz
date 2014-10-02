// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsuredPersonDatum.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  /// <summary>
  ///   The InsuredPersonDatum.
  /// </summary>
  public partial class InsuredPersonDatum
  {
  /// <summary>
    /// Gets or sets a value indicating whether check snils.
    /// </summary>
    [XmlElement(Order = 50)]
    [DataMember(Order = 50)]
    public virtual bool? NotCheckSnils { get; set; }

    /// <summary>
    /// Gets or sets the not check exists snils.
    /// </summary>
    [XmlElement(Order = 51)]
    [DataMember(Order = 51)]
    public virtual bool NotCheckExistsSnils { get; set; }
  }
}