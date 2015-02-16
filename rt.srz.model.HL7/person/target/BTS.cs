// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BTS.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The bts.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The bts.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "BTS", Namespace = "urn:hl7-org:v2xml")]
  public class BTS
  {
    #region Fields

    /// <summary>
    ///   The count messages.
    /// </summary>
    [XmlElement(ElementName = "BTS.1", Order = 1)]
    public string CountMessages = string.Empty;

    /// <summary>
    ///   The hash.
    /// </summary>
    [XmlElement(ElementName = "BTS.3", Order = 3)]
    public string Hash = "????????";

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The retrieve short info.
    /// </summary>
    /// <returns>
    ///   The <see cref="string" />.
    /// </returns>
    public string RetrieveShortInfo()
    {
      return string.Format("Messages: {0}", CountMessages);
    }

    #endregion
  }
}