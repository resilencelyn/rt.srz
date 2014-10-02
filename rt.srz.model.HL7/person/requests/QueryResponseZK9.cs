// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryResponseZK9.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The query response_ z k 9.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.requests
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.person.target;

  #endregion

  /// <summary>
  ///   The query response_ z k 9.
  /// </summary>
  [Serializable]
  public class QueryResponse_ZK9
  {
    #region Fields

    /// <summary>
    ///   The pid.
    /// </summary>
    [XmlElement(ElementName = "PID", Order = 1)]
    public PID Pid = new PID();

    /// <summary>
    ///   The registration list.
    /// </summary>
    [XmlElement(ElementName = "RSP_ZK8.REGISTRATION", Order = 2)]
    public List<Registration_ZK8> RegistrationList = new List<Registration_ZK8>();

    #endregion
  }
}