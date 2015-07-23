// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ack.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ack.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service.uir
{
  #region

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The ack.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true, Namespace = "http://uir.ffoms.ru")]
  [XmlRoot(Namespace = "http://uir.ffoms.ru", IsNullable = false)]
  public enum Ack
  {
    /// <summary>
    ///   The aa.
    /// </summary>
    AA, 

    /// <summary>
    ///   The ae.
    /// </summary>
    AE, 

    /// <summary>
    ///   The ar.
    /// </summary>
    AR, 

    /// <summary>
    ///   The ce.
    /// </summary>
    CE, 

    /// <summary>
    ///   The cr.
    /// </summary>
    CR, 

    /// <summary>
    ///   The ca.
    /// </summary>
    CA, 
  }
}