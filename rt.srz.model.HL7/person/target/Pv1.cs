// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pv1.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The pv 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The pv 1.
  /// </summary>
  [Serializable]
  public class Pv1
  {
    #region Fields

    /// <summary>
    ///   The insured type.
    /// </summary>
    [XmlElement(ElementName = "PV1.2", Order = 2)]
    public string InsuredType = "1";

    #endregion
  }
}