// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Role.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The role.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The role.
  /// </summary>
  [Serializable]
  public class Role
  {
    #region Fields

    /// <summary>
    ///   The code.
    /// </summary>
    [XmlElement(ElementName = "CWE.1", Order = 1)]
    public string Code;

    /// <summary>
    ///   The description.
    /// </summary>
    [XmlElement(ElementName = "CWE.2", Order = 2)]
    public string Description;

    /// <summary>
    ///   The oid.
    /// </summary>
    [XmlElement(ElementName = "CWE.3", Order = 3)]
    public string Oid = "1.2.643.2.40.5.100.131";

    #endregion
  }
}