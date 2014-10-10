// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QAK.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The qak.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The qak.
  /// </summary>
  [Serializable]
  public class QAK
  {
    #region Fields

    /// <summary>
    ///   The count result.
    /// </summary>
    [XmlElement(ElementName = "QAK.4", Order = 4)]
    public string CountResult;

    /// <summary>
    ///   The count result in onew answer.
    /// </summary>
    [XmlElement(ElementName = "QAK.5", Order = 5)]
    public string CountResultInOnewAnswer;

    /// <summary>
    ///   The count result remaining.
    /// </summary>
    [XmlElement(ElementName = "QAK.6", Order = 6)]
    public string CountResultRemaining;

    /// <summary>
    ///   The message name.
    /// </summary>
    [XmlElement(ElementName = "QAK.3", Order = 3)]
    public MessageName MessageName = new MessageName();

    /// <summary>
    ///   The state answer.
    /// </summary>
    [XmlElement(ElementName = "QAK.2", Order = 2)]
    public string StateAnswer;

    #endregion
  }
}