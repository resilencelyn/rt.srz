// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FullName.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The full name.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service.uir
{
  #region

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The full name.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true, Namespace = "http://uir.ffoms.ru")]
  [XmlRoot(Namespace = "http://uir.ffoms.ru", IsNullable = false)]
  public class FullName
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the family name.
    /// </summary>
    [XmlElement(Order = 0)]
    public string FamilyName { get; set; }

    /// <summary>
    ///   Gets or sets the first name.
    /// </summary>
    [XmlElement(Order = 1)]
    public string FirstName { get; set; }

    /// <summary>
    ///   Gets or sets the middle name.
    /// </summary>
    [XmlElement(Order = 2)]
    public string MiddleName { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Create a clone of this FullName object
    /// </summary>
    /// <returns> The <see cref="FullName" /> . </returns>
    public virtual FullName Clone()
    {
      return (FullName)MemberwiseClone();
    }

    #endregion
  }
}