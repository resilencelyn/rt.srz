// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MedCompanyDocMp.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The packet med company doc mp.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.nsi.Mo
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