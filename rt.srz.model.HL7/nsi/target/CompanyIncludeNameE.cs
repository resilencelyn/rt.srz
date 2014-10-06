// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompanyIncludeNameE.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The packet med company med include name_e.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.nsi.target
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  ///   The packet med company med include name_e.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true)]
  public class CompanyIncludeNameE
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