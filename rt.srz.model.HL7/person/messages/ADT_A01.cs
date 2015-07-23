// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ADT_A01.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ad t_ a 01.
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
  ///   The ad t_ a 01.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "ADT_A01", Namespace = "urn:hl7-org:v2xml")]
  public class ADT_A01 : BaseMessageTemplate
  {
    #region Fields

    /// <summary>
    ///   The evn.
    /// </summary>
    [XmlElement(ElementName = "EVN", Order = 2)]
    public Evn Evn = new Evn();

    /// <summary>
    ///   The insurance list.
    /// </summary>
    [XmlElement(ElementName = "ADT_A01.INSURANCE", Order = 6)]
    public List<ADT_A01_INSURANCE> InsuranceList = new List<ADT_A01_INSURANCE>();

    /// <summary>
    ///   The pid.
    /// </summary>
    [XmlElement(ElementName = "PID", Order = 4)]
    public MessagePid Pid = new MessagePid();

    /// <summary>
    ///   The pv 1.
    /// </summary>
    [XmlElement(ElementName = "PV1", Order = 5)]
    public Pv1 Pv1 = new Pv1();

    /// <summary>
    ///   The zvn.
    /// </summary>
    [XmlElement(ElementName = "ZVN", Order = 3)]
    public Zvn Zvn;

    #endregion
  }
}