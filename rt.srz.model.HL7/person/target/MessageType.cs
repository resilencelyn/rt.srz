// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageType.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The message type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The message type.
  /// </summary>
  [Serializable]
  public class MessageType
  {
    #region Fields

    /// <summary>
    ///   The mess type.
    /// </summary>
    [XmlElement(ElementName = "MSG.1", Order = 1)]
    public string MessType;

    /// <summary>
    ///   The structure type.
    /// </summary>
    [XmlElement(ElementName = "MSG.3", Order = 3)]
    public string StructureType;

    /// <summary>
    ///   The transaction code.
    /// </summary>
    [XmlElement(ElementName = "MSG.2", Order = 2)]
    public string TransactionCode;

    #endregion
  }
}