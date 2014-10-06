// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ADT_A01_INSURANCE.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ad t_ a 01_ insurance.
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
  ///   The ad t_ a 01_ insurance.
  /// </summary>
  [Serializable]
  public class ADT_A01_INSURANCE
  {
    #region Fields

    /// <summary>
    ///   The in 1.
    /// </summary>
    [XmlElement(ElementName = "IN1")]
    public IN1 In1 = new IN1();

    #endregion
  }
}