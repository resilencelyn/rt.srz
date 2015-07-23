// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UIRRequest.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The uir request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service.uir
{
  #region

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The uir request.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true, Namespace = "http://uir.ffoms.ru")]
  [XmlRoot(Namespace = "http://uir.ffoms.ru", IsNullable = false)]
  public class UIRRequest
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the birth.
    /// </summary>
    [XmlElement(Order = 2)]
    public Birth Birth { get; set; }

    /// <summary>
    ///   Gets or sets the document.
    /// </summary>
    [XmlElement("Document", Order = 1)]
    public Document Document { get; set; }

    /// <summary>
    ///   Gets or sets the full name.
    /// </summary>
    [XmlElement(Order = 0)]
    public FullName FullName { get; set; }

    /// <summary>
    ///   Gets or sets the ins date.
    /// </summary>
    [XmlElement(DataType = "date", IsNullable = true, Order = 3)]
    public DateTime? InsDate { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether ins date specified.
    /// </summary>
    [XmlIgnore]
    public bool InsDateSpecified { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Create a clone of this UIRRequest object
    /// </summary>
    /// <returns> The <see cref="UIRRequest" /> . </returns>
    public virtual UIRRequest Clone()
    {
      return (UIRRequest)MemberwiseClone();
    }

    #endregion
  }
}