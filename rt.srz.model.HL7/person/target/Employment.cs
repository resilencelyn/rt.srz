// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Employment.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The employment.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The employment.
  /// </summary>
  [Serializable]
  public class Employment
  {
    #region Fields

    /// <summary>
    ///   The employment.
    /// </summary>
    [XmlElement(ElementName = "CWE.1", Order = 1)]
    public string employment;

    #endregion
  }
}