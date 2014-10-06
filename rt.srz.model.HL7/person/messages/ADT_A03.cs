// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ADT_A03.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ad t_ a 03.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.messages
{
  #region references

  using System;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.person.target;

  #endregion

  /// <summary>
  ///   The ad t_ a 03.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "ADT_A03", Namespace = "urn:hl7-org:v2xml")]
  public class ADT_A03 : BaseMessageTemplate
  {
    #region Fields

    /// <summary>
    ///   The evn.
    /// </summary>
    [XmlElement(ElementName = "EVN", Order = 2)]
    public Evn Evn;

    /// <summary>
    ///   The pid.
    /// </summary>
    [XmlElement(ElementName = "PID", Order = 4)]
    public MessagePid Pid;

    /// <summary>
    ///   The pv 1.
    /// </summary>
    [XmlElement(ElementName = "PV1", Order = 3)]
    public Pv1 Pv1;

    #endregion
  }
}