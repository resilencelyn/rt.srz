// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UIRResponse.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The uir response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service.uir
{
  #region

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The uir response.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true, Namespace = "http://uir.ffoms.ru")]
  [XmlRoot(Namespace = "http://uir.ffoms.ru", IsNullable = false)]
  public class UIRResponse
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the ack.
    /// </summary>
    [XmlElement(Order = 0)]
    public string Ack { get; set; }

    /// <summary>
    ///   Gets or sets the err.
    /// </summary>
    [XmlElement("Err", Order = 1)]
    public Err[] Err { get; set; }

    /// <summary>
    ///   Gets or sets the uir query response.
    /// </summary>
    [XmlElement(Order = 2)]
    public UIRResponseUIRQueryResponse[] UIRQueryResponse { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Create a clone of this UIRResponse object
    /// </summary>
    /// <returns> The <see cref="UIRResponse" /> . </returns>
    public virtual UIRResponse Clone()
    {
      return (UIRResponse)MemberwiseClone();
    }

    #endregion
  }
}