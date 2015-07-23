// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessagePid.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The message pid.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The message pid.
  /// </summary>
  [Serializable]
  public class MessagePid
  {
    #region Fields

    /// <summary>
    ///   The address list.
    /// </summary>
    [XmlElement(ElementName = "PID.11", Order = 11)]
    public List<Address> AddressList = new List<Address>();

    /// <summary>
    ///   The birth day.
    /// </summary>
    [XmlElement(ElementName = "PID.7", Order = 7)]
    public string BirthDay;

    /// <summary>
    ///   The dead day.
    /// </summary>
    [XmlElement(ElementName = "PID.29", Order = 29)]
    public string DeadDay;

    /// <summary>
    ///   The fio list.
    /// </summary>
    [XmlElement(ElementName = "PID.5", Order = 5)]
    public List<Fio> FioList = new List<Fio>();

    /// <summary>
    ///   The identificators list.
    /// </summary>
    [XmlElement(ElementName = "PID.3", Order = 3)]
    public List<Identificators> IdentificatorsList = new List<Identificators>();

    /// <summary>
    ///   The is dead.
    /// </summary>
    [XmlElement(ElementName = "PID.30", Order = 30)]
    public string IsDead;

    /// <summary>
    ///   The nationality.
    /// </summary>
    [XmlElement(ElementName = "PID.26", Order = 26)]
    public National Nationality = new National();

    /// <summary>
    ///   The place of birth.
    /// </summary>
    [XmlElement(ElementName = "PID.23", Order = 23)]
    public string PlaceOfBirth;

    /// <summary>
    ///   The reliability id list.
    /// </summary>
    [XmlElement(ElementName = "PID.32", Order = 32)]
    public List<string> ReliabilityIdList = new List<string>();

    /// <summary>
    ///   The sex.
    /// </summary>
    [XmlElement(ElementName = "PID.8", Order = 8)]
    public string Sex;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The has dead flag.
    /// </summary>
    /// <returns>
    ///   The <see cref="bool" />.
    /// </returns>
    public bool HasDeadFlag()
    {
      return !string.IsNullOrEmpty(DeadDay) && !string.IsNullOrEmpty(IsDead);
    }

    #endregion
  }
}