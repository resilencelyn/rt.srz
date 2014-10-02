// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Birth.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The birth.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service.uir
{
  #region

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The birth.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true, Namespace = "http://uir.ffoms.ru")]
  [XmlRoot(Namespace = "http://uir.ffoms.ru", IsNullable = false)]
  public class Birth
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the item.
    /// </summary>
    [XmlElement("BirthDate", typeof(DateTime), DataType = "date", Order = 0)]
    public DateTime BirthDate { get; set; }

    [XmlElement("BirthPlace", typeof(string), Order = 1)]
    public string BirthPlace { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Create a clone of this Birth object
    /// </summary>
    /// <returns> The <see cref="Birth" /> . </returns>
    public virtual Birth Clone()
    {
      return (Birth)MemberwiseClone();
    }

    #endregion
  }
}