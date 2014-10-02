// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionStandartId.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The version standart id.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The version standart id.
  /// </summary>
  [Serializable]
  public class VersionStandartId
  {
    #region Fields

    /// <summary>
    ///   The id.
    /// </summary>
    [XmlElement(ElementName = "VID.1", Order = 1)]
    public string Id = "2.6";

    #endregion
  }
}