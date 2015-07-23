// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UIRResponseUIRQueryResponse.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The uir response uir query response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service.uir
{
  #region

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The uir response uir query response.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true, Namespace = "http://uir.ffoms.ru")]
  public class UIRResponseUIRQueryResponse
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the insurance.
    /// </summary>
    [XmlElement(Order = 1)]
    public Insurance Insurance { get; set; }

    /// <summary>
    ///   Gets or sets the person.
    /// </summary>
    [XmlElement(Order = 0)]
    public Person Person { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Create a clone of this UIRResponseUIRQueryResponse object
    /// </summary>
    /// <returns> The <see cref="UIRResponseUIRQueryResponse" /> . </returns>
    public virtual UIRResponseUIRQueryResponse Clone()
    {
      return (UIRResponseUIRQueryResponse)MemberwiseClone();
    }

    #endregion
  }
}