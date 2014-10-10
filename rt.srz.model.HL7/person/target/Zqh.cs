// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zqh.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The zqh.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The zqh.
  /// </summary>
  [Serializable]
  public class Zqh
  {
    #region Fields

    /// <summary>
    ///   The application id at the organization received it.
    /// </summary>
    [XmlElement(ElementName = "ZQH.1", Order = 1)]
    public EiStructure ApplicationIDAtTheOrganizationReceivedIt = new EiStructure();

    /// <summary>
    ///   The customers request id.
    /// </summary>
    [XmlElement(ElementName = "ZQH.3", Order = 3)]
    public EiStructure CustomersRequestID = new EiStructure();

    /// <summary>
    ///   The execution date.
    /// </summary>
    [XmlElement(ElementName = "ZQH.6", Order = 6)]
    public string ExecutionDate;

    /// <summary>
    ///   The executors request id.
    /// </summary>
    [XmlElement(ElementName = "ZQH.4", Order = 4)]
    public EiStructure ExecutorsRequestID = new EiStructure();

    /// <summary>
    ///   The formation date.
    /// </summary>
    [XmlElement(ElementName = "ZQH.5", Order = 5)]
    public string FormationDate;

    /// <summary>
    ///   The request reason.
    /// </summary>
    [XmlElement(ElementName = "ZQH.2", Order = 2)]
    public CneStructure RequestReason = new CneStructure();

    #endregion
  }
}