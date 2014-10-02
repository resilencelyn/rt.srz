// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INVOICE.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   INVOICE
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.card.za3
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