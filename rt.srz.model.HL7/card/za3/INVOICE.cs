// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INVOICE.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   INVOICE
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.card.za3
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   INVOICE
  /// </summary>
  [Serializable]
  public class INVOICE
  {
    #region Public Properties

    /// <summary>
    ///   INSURANCE
    /// </summary>
    [XmlElement("ZPI_ZA3.INSURANCE")]
    public List<INSURANCE> INSURANCE { get; set; }

    #endregion
  }
}