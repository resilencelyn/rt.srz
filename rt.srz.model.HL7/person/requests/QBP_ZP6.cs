// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QBP_ZP6.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The qb p_ z p 6.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.requests
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The qb p_ z p 6.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "QBP_ZP6", Namespace = "urn:Hl7-org:v2xml")]
  public class QBP_ZP6 : BaseMessageTemplate
  {
    #region Fields

    /// <summary>
    ///   The qpd.
    /// </summary>
    [XmlElement(ElementName = "QPD", Order = 2)]
    public QPD_ZP6 Qpd = new QPD_ZP6();

    #endregion
  }
}