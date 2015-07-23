// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Err.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The err.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service.uir
{
  #region

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The err.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true, Namespace = "http://uir.ffoms.ru")]
  [XmlRoot(Namespace = "http://uir.ffoms.ru", IsNullable = false)]
  public class Err
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the err code.
    /// </summary>
    [XmlElement(Order = 0)]
    public string ErrCode { get; set; }

    /// <summary>
    ///   Gets or sets the err text.
    /// </summary>
    [XmlElement(Order = 1)]
    public string ErrText { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Create a clone of this Err object
    /// </summary>
    /// <returns> The <see cref="Err" /> . </returns>
    public virtual Err Clone()
    {
      return (Err)MemberwiseClone();
    }

    #endregion
  }
}