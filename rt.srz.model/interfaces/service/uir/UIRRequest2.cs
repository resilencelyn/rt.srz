// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UIRRequest2.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The uir request 2.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service.uir
{
  #region

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The uir request 2.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true, Namespace = "http://uir.ffoms.ru")]
  [XmlRoot(Namespace = "http://uir.ffoms.ru", IsNullable = false)]
  public class UIRRequest2
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the birth.
    /// </summary>
    [XmlElement(Order = 4)]
    public Birth Birth { get; set; }

    /// <summary>
    ///   Gets or sets the full name.
    /// </summary>
    [XmlElement(Order = 0)]
    public FullName FullName { get; set; }

    /// <summary>
    ///   Gets or sets the ins date.
    /// </summary>
    [XmlElement(DataType = "date", IsNullable = true, Order = 5)]
    public DateTime? InsDate { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether ins date specified.
    /// </summary>
    [XmlIgnore]
    public bool InsDateSpecified { get; set; }

    /// <summary>
    ///   Gets or sets the ins region.
    /// </summary>
    [XmlElement(Order = 3)]
    public string InsRegion { get; set; }

    /// <summary>
    ///   Gets or sets the policy number.
    /// </summary>
    [XmlElement(Order = 2)]
    public string PolicyNumber { get; set; }

    /// <summary>
    ///   Gets or sets the policy type.
    /// </summary>
    [XmlElement(Order = 1)]
    public string PolicyType { get; set; }

    #endregion

    #region Public Methods and Operators

    #endregion
  }
}