// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BHS3.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The bh s 3.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The bh s 3.
  /// </summary>
  [Serializable]
  public class BHS3
  {
    #region Fields

    /// <summary>
    ///   The application.
    /// </summary>
    [XmlElement(ElementName = "HD.1", Order = 1)]
    public string Application;

    #endregion
  }
}