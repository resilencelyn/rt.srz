// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Insurance.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The insurance.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service.uir
{
  #region

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The insurance.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true, Namespace = "http://uir.ffoms.ru")]
  [XmlRoot(Namespace = "http://uir.ffoms.ru", IsNullable = false)]
  public class Insurance
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the end date.
    /// </summary>
    [XmlElement(DataType = "date", Order = 3)]
    public DateTime EndDate { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether end date specified.
    /// </summary>
    [XmlIgnore]
    public bool EndDateSpecified { get; set; }

    /// <summary>
    ///   Gets or sets the ins id.
    /// </summary>
    [XmlElement(Order = 5)]
    public string InsId { get; set; }

    /// <summary>
    ///   Gets or sets the ins region.
    /// </summary>
    [XmlElement(Order = 1)]
    public string InsRegion { get; set; }

    /// <summary>
    ///   Gets or sets the ins type.
    /// </summary>
    [XmlElement(Order = 4)]
    public string InsType { get; set; }

    /// <summary>
    ///   Gets or sets the med ins company id.
    /// </summary>
    [XmlElement(Order = 0)]
    public string MedInsCompanyId { get; set; }

    /// <summary>
    ///   Gets or sets the start date.
    /// </summary>
    [XmlElement(DataType = "date", Order = 2)]
    public DateTime StartDate { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether start date specified.
    /// </summary>
    [XmlIgnore]
    public bool StartDateSpecified { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Create a clone of this Insurance object
    /// </summary>
    /// <returns> The <see cref="Insurance" /> . </returns>
    public virtual Insurance Clone()
    {
      return (Insurance)MemberwiseClone();
    }

    #endregion
  }
}