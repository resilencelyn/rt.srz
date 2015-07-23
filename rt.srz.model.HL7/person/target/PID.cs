// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PID.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The pid.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The pid.
  /// </summary>
  [Serializable]
  public class PID
  {
    #region Fields

    /// <summary>
    ///   The dead day.
    /// </summary>
    [XmlElement(ElementName = "PID.29", Order = 29)]
    public string DeadDay;

    /// <summary>
    ///   The identificators list.
    /// </summary>
    [XmlElement(ElementName = "PID.3", Order = 3)]
    public List<Identificators> IdentificatorsList = new List<Identificators>();

    /// <summary>
    ///   The is dead.
    /// </summary>
    [XmlElement(ElementName = "PID.30", Order = 30)]
    public string IsDead;

    /// <summary>
    ///   The pid 5.
    /// </summary>
    [XmlElement(ElementName = "PID.5", Order = 5)]
    public string pid5 = string.Empty;

    /// <summary>
    ///   The pid 7.
    /// </summary>
    [XmlElement(ElementName = "PID.7", Order = 7)]
    public string pid7 = string.Empty;

    /// <summary>
    ///   The pid 8.
    /// </summary>
    [XmlElement(ElementName = "PID.8", Order = 8)]
    public string pid8 = string.Empty;

    #endregion
  }
}