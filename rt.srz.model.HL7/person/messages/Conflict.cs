// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Conflict.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Конфликт
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.messages
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.person.target;

  #endregion

  /// <summary>
  ///   Конфликт
  /// </summary>
  [Serializable]
  public class Conflict : BaseMessageTemplate
  {
    #region Fields

    /// <summary>
    ///   Евн
    /// </summary>
    [XmlElement(ElementName = "EVN", Order = 2)]
    public Evn Evn;

    /// <summary>
    ///   Список пид
    /// </summary>
    [XmlElement(ElementName = "PID", Order = 3)]
    public List<MessagePid> PidList;

    #endregion
  }
}