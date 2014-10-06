// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ack.cs" company="ÐóñÁÈÒåõ">
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
    /// <remarks>
    /// </remarks>
    AA, 

    /// <summary>
    ///   The ae.
    /// </summary>
    /// <remarks>
    /// </remarks>
    AE, 

    /// <summary>
    ///   The ar.
    /// </summary>
    /// <remarks>
    /// </remarks>
    AR, 

    /// <summary>
    ///   The ce.
    /// </summary>
    /// <remarks>
    /// </remarks>
    CE, 

    /// <summary>
    ///   The cr.
    /// </summary>
    /// <remarks>
    /// </remarks>
    CR, 

    /// <summary>
    ///   The ca.
    /// </summary>
    /// <remarks>
    /// </remarks>
    CA, 
  }
}