// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MedCompanyDocMp.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The packet med company doc mp.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.nsi.Mo
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  ///   The packet med company doc mp.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true)]
  public class MedCompanyDocMp
  {
    #region Public Properties

    /// <summary>
    ///   The value.
    /// </summary>
    [XmlText]
    public string Value { get; set; }

    #endregion
  }
}