// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Evn.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The evn.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The evn.
  /// </summary>
  [Serializable]
  public class Evn
  {
    #region Fields

    /// <summary>
    ///   The code reason event.
    /// </summary>
    [XmlElement(ElementName = "EVN.4", Order = 4)]
    public string CodeReasonEvent;

    /// <summary>
    ///   The date registration event.
    /// </summary>
    [XmlElement(ElementName = "EVN.2", Order = 2)]
    public string DateRegistrationEvent;

    #endregion
  }
}