// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zah.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The zah.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The zah.
  /// </summary>
  [Serializable]
  public class Zah
  {
    #region Fields

    /// <summary>
    ///   The application id at the organization received it.
    /// </summary>
    [XmlElement(ElementName = "ZAH.8", Order = 8)]
    public EiStructure ApplicationIDAtTheOrganizationReceivedIt = new EiStructure();

    /// <summary>
    ///   The code of teritory.
    /// </summary>
    [XmlElement(ElementName = "ZAH.10", Order = 10)]
    public CneStructure CodeOfTeritory = new CneStructure();

    /// <summary>
    ///   The completion date.
    /// </summary>
    [XmlElement(ElementName = "ZAH.11", Order = 11)]
    public string CompletionDate;

    /// <summary>
    ///   The decline reason.
    /// </summary>
    [XmlElement(ElementName = "ZAH.16", Order = 16)]
    public CneStructure DeclineReason = new CneStructure();

    /// <summary>
    ///   The familiarization attribute.
    /// </summary>
    [XmlElement(ElementName = "ZAH.12", Order = 12)]
    public string FamiliarizationAttribute;

    /// <summary>
    ///   The intercessorial organization type code.
    /// </summary>
    [XmlElement(ElementName = "ZAH.6", Order = 6)]
    public CneStructure IntercessorialOrganizationTypeCode = new CneStructure();

    /// <summary>
    ///   The is representative.
    /// </summary>
    [XmlElement(ElementName = "ZAH.5", Order = 5)]
    public string IsRepresentative;

    /// <summary>
    ///   The method of application submission.
    /// </summary>
    [XmlElement(ElementName = "ZAH.7", Order = 7)]
    public CneStructure MethodOfApplicationSubmission = new CneStructure();

    /// <summary>
    ///   The policy form.
    /// </summary>
    [XmlElement(ElementName = "ZAH.4", Order = 4)]
    public CneStructure PolicyForm = new CneStructure();

    /// <summary>
    ///   The policy issue application type.
    /// </summary>
    [XmlElement(ElementName = "ZAH.2", Order = 2)]
    public CneStructure PolicyIssueApplicationType = new CneStructure();

    /// <summary>
    ///   The policy issue or change reason.
    /// </summary>
    [XmlElement(ElementName = "ZAH.3", Order = 3)]
    public CneStructure PolicyIssueOrChangeReason = new CneStructure();

    /// <summary>
    ///   The policy issuing point id.
    /// </summary>
    [XmlElement(ElementName = "ZAH.9", Order = 9)]
    public EiStructure PolicyIssuingPointID = new EiStructure();

    /// <summary>
    ///   The preference or change smo type.
    /// </summary>
    [XmlElement(ElementName = "ZAH.1", Order = 1)]
    public CneStructure PreferenceOrChangeSmoType = new CneStructure();

    /// <summary>
    ///   The processing ending date time.
    /// </summary>
    [XmlElement(ElementName = "ZAH.15", Order = 15)]
    public string ProcessingEndingDateTime;

    /// <summary>
    ///   The receiving employee full name.
    /// </summary>
    [XmlElement(ElementName = "ZAH.14", Order = 14)]
    public string ReceivingEmployeeFullName;

    /// <summary>
    ///   The recipiency date time.
    /// </summary>
    [XmlElement(ElementName = "ZAH.13", Order = 13)]
    public string RecipiencyDateTime;

    #endregion
  }
}