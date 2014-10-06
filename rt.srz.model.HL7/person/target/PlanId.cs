// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlanId.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The plan id.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The plan id.
  /// </summary>
  [Serializable]
  public class PlanId
  {
    #region Fields

    /// <summary>
    ///   The id.
    /// </summary>
    [XmlElement(ElementName = "CWE.1", Order = 1)]
    public string Id = string.Empty;

    /// <summary>
    ///   The oid.
    /// </summary>
    [XmlElement(ElementName = "CWE.3", Order = 3)]
    public string Oid;

    #endregion
  }
}