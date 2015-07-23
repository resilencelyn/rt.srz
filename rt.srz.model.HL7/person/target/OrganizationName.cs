// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrganizationName.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The organization name.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The organization name.
  /// </summary>
  [Serializable]
  public class OrganizationName
  {
    #region Fields

    /// <summary>
    ///   The name.
    /// </summary>
    [XmlElement(ElementName = "HD.1", Order = 1)]
    public string Name;

    #endregion
  }
}