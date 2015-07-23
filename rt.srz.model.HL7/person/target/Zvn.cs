// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zvn.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The zvn.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The zvn.
  /// </summary>
  [Serializable]
  public class Zvn
  {
    #region Fields

    /// <summary>
    ///   The id discrepancies.
    /// </summary>
    [XmlElement(ElementName = "ZVN.2", Order = 2)]
    public string IdDiscrepancies = null;

    #endregion
  }
}