// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZPI_ZA1_APPLICATION.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The zp i_ z a 1_ application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.messages
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   The zp i_ z a 1_ application.
  /// </summary>
  [Serializable]
  public class ZPI_ZA1_APPLICATION
  {
    #region Fields

    /// <summary>
    ///   The in 1 list.
    /// </summary>
    [XmlElement(ElementName = "IN1", Order = 4)]
    public List<In1Card> In1List = new List<In1Card>();

    /// <summary>
    ///   The nk 1 list.
    /// </summary>
    [XmlElement(ElementName = "NK1", Order = 3)]
    public List<Nk1> Nk1List = new List<Nk1>();

    /// <summary>
    ///   The pid.
    /// </summary>
    [XmlElement(ElementName = "PID", Order = 2)]
    public PidCard Pid = new PidCard();

    /// <summary>
    ///   The zah.
    /// </summary>
    [XmlElement(ElementName = "ZAH", Order = 1)]
    public Zah Zah = new Zah();

    /// <summary>
    ///   The znd.
    /// </summary>
    [XmlElement(ElementName = "ZND", Order = 5)]
    public Znd Znd = new Znd();

    #endregion
  }
}