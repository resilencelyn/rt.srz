// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QPD_ZP9.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The qp d_ z p 9.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.requests
{
  #region references

  using System;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.person.target;

  #endregion

  /// <summary>
  ///   The qp d_ z p 9.
  /// </summary>
  [Serializable]
  public class QPD_ZP9
  {
    #region Fields

    /// <summary>
    ///   The date request.
    /// </summary>
    [XmlElement(ElementName = "QPD.4", Order = 4)]
    public string DateRequest;

    /// <summary>
    ///   The insurance ser num.
    /// </summary>
    [XmlElement(ElementName = "QPD.11", Order = 11)]
    public string InsuranceSerNum;

    /// <summary>
    ///   The insurance type.
    /// </summary>
    [XmlElement(ElementName = "QPD.10", Order = 10)]
    public string InsuranceType;

    /// <summary>
    ///   The insured id.
    /// </summary>
    [XmlElement(ElementName = "QPD.5", Order = 5)]
    public Identificators InsuredId = new Identificators();

    /// <summary>
    ///   The message name.
    /// </summary>
    [XmlElement(ElementName = "QPD.1", Order = 1)]
    public MessageName MessageName = new MessageName();

    #endregion
  }
}