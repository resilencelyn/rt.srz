// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Document.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The document.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service.uir
{
  #region

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The document.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true, Namespace = "http://uir.ffoms.ru")]
  [XmlRoot(Namespace = "http://uir.ffoms.ru", IsNullable = false)]
  public class Document
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the doc ident.
    /// </summary>
    [XmlElement(Order = 1)]
    public string DocIdent { get; set; }

    /// <summary>
    ///   Gets or sets the doc type.
    /// </summary>
    [XmlElement(Order = 0)]
    public int DocType { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Create a clone of this Document object
    /// </summary>
    /// <returns> The <see cref="Document" /> . </returns>
    public virtual Document Clone()
    {
      return (Document)MemberwiseClone();
    }

    #endregion
  }
}