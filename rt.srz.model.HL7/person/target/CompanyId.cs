// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompanyId.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The company id.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The company id.
  /// </summary>
  [Serializable]
  public class CompanyId
  {
    #region Fields

    /// <summary>
    ///   The company id type.
    /// </summary>
    [XmlElement(ElementName = "CX.5", Order = 5)]
    public string CompanyIdType = string.Empty;

    /// <summary>
    ///   The id.
    /// </summary>
    [XmlElement(ElementName = "CX.1", Order = 1)]
    public string Id = string.Empty;

    #endregion
  }
}