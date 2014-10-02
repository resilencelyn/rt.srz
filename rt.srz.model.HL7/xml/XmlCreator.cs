// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlCreator.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The xml creator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.foms.HL7.xml
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Xml.Linq;

  using rt.foms.HL7.commons;
  using rt.foms.HL7.commons.Enumerations;
  using rt.foms.HL7.enumerations;
  using rt.foms.HL7.enumerations.resolve;
  using rt.foms.HL7.parameters;
  using rt.foms.HL7.xmlreply.target;

  #endregion

  /// <summary>
  ///   The xml creator.
  /// </summary>
  internal static class XmlCreator
  {
    #region Methods

    /// <summary>
    /// The add error reference.
    /// </summary>
    /// <param name="error">
    /// The error.
    /// </param>
    /// <param name="reference">
    /// The reference.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    internal static bool AddErrorReference(XElement error, string reference)
    {
      if ((error != null) && !string.IsNullOrEmpty(reference))
      {
        foreach (var element in error.SelectByLocalName("ERR.5"))
        {
          foreach (var element2 in element.SelectByLocalName("CWE.2"))
          {
            element2.SetValue(HL7Helper.AddMessageReference(element2.Value, reference));
            return true;
          }
        }
      }

      return false;
    }

    /// <summary>
    /// The create logic error.
    /// </summary>
    /// <param name="errorTypeCode">
    /// The error type code.
    /// </param>
    /// <param name="code">
    /// The code.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="codeHL7">
    /// The code h l 7.
    /// </param>
    /// <param name="messageHL7">
    /// The message h l 7.
    /// </param>
    /// <param name="severityHL7">
    /// The severity h l 7.
    /// </param>
    /// <param name="reference">
    /// The reference.
    /// </param>
    /// <param name="xpath">
    /// The xpath.
    /// </param>
    /// <param name="nodeNumber">
    /// The node number.
    /// </param>
    /// <returns>
    /// The <see cref="XElement"/>.
    /// </returns>
    internal static XElement CreateLogicError(
      string errorTypeCode, 
      string code, 
      string message, 
      string codeHL7, 
      string messageHL7, 
      string severityHL7, 
      string reference, 
      string xpath, 
      string nodeNumber)
    {
      return new XElement(
        XName.Get("ERR", "urn:hl7-org:v2xml"), 
        new object[]
          {
            new XElement(
              XName.Get("ERR.2", "urn:hl7-org:v2xml"), 
              new object[]
                {
                  new XElement(
                    XName.Get("ERL.1", "urn:hl7-org:v2xml"), 
                    xpath.ReplaceInvalidXmlCharacters(XmlVersion.Version_1_0, " ", false)), 
                  new XElement(
                    XName.Get("ERL.2", "urn:hl7-org:v2xml"), 
                    nodeNumber.ReplaceInvalidXmlCharacters(XmlVersion.Version_1_0, " ", false))
                }), 
            new XElement(
              XName.Get("ERR.3", "urn:hl7-org:v2xml"), 
              new object[]
                {
                  new XElement(
                    XName.Get("CWE.1", "urn:hl7-org:v2xml"), 
                    codeHL7.ReplaceInvalidXmlCharacters(XmlVersion.Version_1_0, " ", false)), 
                  new XElement(
                    XName.Get("CWE.2", "urn:hl7-org:v2xml"), 
                    messageHL7.ReplaceInvalidXmlCharacters(XmlVersion.Version_1_0, " ", false)), 
                  new XElement(XName.Get("CWE.3", "urn:hl7-org:v2xml"), "1.2.643.2.40.5.100.357")
                }), 
            new XElement(
              XName.Get("ERR.4", "urn:hl7-org:v2xml"), 
              severityHL7.ReplaceInvalidXmlCharacters(XmlVersion.Version_1_0, " ", false)), 
            new XElement(
              XName.Get("ERR.5", "urn:hl7-org:v2xml"), 
              new object[]
                {
                  new XElement(
                    XName.Get("CWE.1", "urn:hl7-org:v2xml"), 
                    code.ReplaceInvalidXmlCharacters(XmlVersion.Version_1_0, " ", false)), 
                  new XElement(
                    XName.Get("CWE.2", "urn:hl7-org:v2xml"), 
                    HL7Helper.AddMessageReference(message, reference)
                    .ReplaceInvalidXmlCharacters(XmlVersion.Version_1_0, " ", false)), 
                  new XElement(
                    XName.Get("CWE.3", "urn:hl7-org:v2xml"), 
                    errorTypeCode.ReplaceInvalidXmlCharacters(XmlVersion.Version_1_0, " ", false))
                })
          });
    }

    /// <summary>
    /// The create msa.
    /// </summary>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    /// <param name="batchFatalReference">
    /// The batch fatal reference.
    /// </param>
    /// <returns>
    /// The <see cref="XElement"/>.
    /// </returns>
    internal static XElement CreateMsa(Parameters parameters, bool batchFatalReference = false)
    {
      return new XElement(
        XName.Get("MSA", "urn:hl7-org:v2xml"), 
        new object[]
          {
            new XElement(
              XName.Get("MSA.1", "urn:hl7-org:v2xml"), 
              ConfirmationsHL7.ConfirmAsString(parameters.LastConfirmationHL7)), 
            new XElement(XName.Get("MSA.2", "urn:hl7-org:v2xml"), parameters.RetrieveReference(batchFatalReference))
          });
    }

    /// <summary>
    /// The create msh.
    /// </summary>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    /// <returns>
    /// The <see cref="XElement"/>.
    /// </returns>
    internal static XElement CreateMsh(Parameters parameters)
    {
      return new XElement(
        XName.Get("MSH", "urn:hl7-org:v2xml"), 
        new object[]
          {
            new XElement(XName.Get("MSH.1", "urn:hl7-org:v2xml"), HL7Helper.BHS_Delimiter), 
            new XElement(XName.Get("MSH.2", "urn:hl7-org:v2xml"), HL7Helper.BHS_CodeSymbols), 
            new XElement(
              XName.Get("MSH.3", "urn:hl7-org:v2xml"), 
              new XElement(XName.Get("HD.1", "urn:hl7-org:v2xml"), parameters.Constants.ApplicationName)), 
            new XElement(
              XName.Get("MSH.4", "urn:hl7-org:v2xml"), 
              new object[]
                {
                  new XElement(XName.Get("HD.1", "urn:hl7-org:v2xml"), parameters.Constants.OrganizationName), 
                  new XElement(XName.Get("HD.2", "urn:hl7-org:v2xml"), HL7Helper.TypeCode_Region2Code), 
                  new XElement(XName.Get("HD.3", "urn:hl7-org:v2xml"), "ISO")
                }), 
            new XElement(
              XName.Get("MSH.5", "urn:hl7-org:v2xml"), 
              new XElement(XName.Get("HD.1", "urn:hl7-org:v2xml"), parameters.OriginApplicationName)), 
            new XElement(
              XName.Get("MSH.6", "urn:hl7-org:v2xml"), 
              new object[]
                {
                  new XElement(XName.Get("HD.1", "urn:hl7-org:v2xml"), parameters.OriginOrganizationName), 
                  new XElement(XName.Get("HD.2", "urn:hl7-org:v2xml"), HL7Helper.TypeCode_Region2Code), 
                  new XElement(XName.Get("HD.3", "urn:hl7-org:v2xml"), "ISO")
                }), 
            new XElement(XName.Get("MSH.7", "urn:hl7-org:v2xml"), HL7Helper.FormatCurrentDateTime()), 
            new XElement(
              XName.Get("MSH.9", "urn:hl7-org:v2xml"), 
              new object[]
                {
                  new XElement(
                    XName.Get("MSG.1", "urn:hl7-org:v2xml"), 
                    DataEventTypes.RetrieveReplyCode(parameters.InputDataEventType)), 
                  new XElement(
                    XName.Get("MSG.2", "urn:hl7-org:v2xml"), 
                    DataEventTypes.RetrieveTransactionReply(parameters.InputDataEventType)), 
                  new XElement(
                    XName.Get("MSG.3", "urn:hl7-org:v2xml"), 
                    DataEventTypes.RetrieveReplyStruct(parameters.InputDataEventType))
                }), 
            new XElement(XName.Get("MSH.10", "urn:hl7-org:v2xml"), Guid.NewGuid().ToString()), 
            new XElement(
              XName.Get("MSH.11", "urn:hl7-org:v2xml"), 
              new XElement(XName.Get("PT.1", "urn:hl7-org:v2xml"), parameters.ProcessingMode)), 
            new XElement(
              XName.Get("MSH.12", "urn:hl7-org:v2xml"), 
              new XElement(XName.Get("VID.1", "urn:hl7-org:v2xml"), "2.6")), 
            new XElement(XName.Get("MSH.15", "urn:hl7-org:v2xml"), "AL"), 
            new XElement(XName.Get("MSH.16", "urn:hl7-org:v2xml"), "AL")
          });
    }

    /// <summary>
    /// The create processing error.
    /// </summary>
    /// <param name="constants">
    /// The constants.
    /// </param>
    /// <param name="code">
    /// The code.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="reference">
    /// The reference.
    /// </param>
    /// <returns>
    /// The <see cref="XElement"/>.
    /// </returns>
    internal static XElement CreateProcessingError(Constants constants, ErrorHL7 code, string message, string reference)
    {
      var errorCode = ErrorsHL7.GetErrorCode(code);
      var str2 = constants.MessageByErrorCodeHL7(errorCode);
      return CreateLogicError(
        constants.ErrorTypeCode, 
        ErrorsHL7.GetApplicationError(code), 
        message, 
        errorCode, 
        str2, 
        ErrorsHL7.GetErrorSeverityLevel(code), 
        reference, 
        null, 
        null);
    }

    /// <summary>
    /// The create processing errors list.
    /// </summary>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    internal static IList<XElement> CreateProcessingErrorsList(Parameters parameters)
    {
      IList<XElement> list = new List<XElement>();
      if (parameters.ProcessingErrors != null)
      {
        foreach (var error in parameters.ProcessingErrors)
        {
          list.Add(CreateProcessingError(parameters.Constants, error.Code, error.Message, null));
        }
      }

      return list;
    }

    /// <summary>
    /// The create reply pid.
    /// </summary>
    /// <param name="pid">
    /// The pid.
    /// </param>
    /// <returns>
    /// The <see cref="XElement"/>.
    /// </returns>
    internal static XElement CreateReplyPid(PID pid)
    {
      return new XElement(
        XName.Get("PID", "urn:hl7-org:v2xml"), 
        new object[]
          {
            new XElement(
              XName.Get("PID.3", "urn:hl7-org:v2xml"), 
              new object[]
                {
                  new XElement(XName.Get("CX.1", "urn:hl7-org:v2xml"), pid.MainEnp), 
                  new XElement(XName.Get("CX.5", "urn:hl7-org:v2xml"), "NI")
                }), 
            from pidSegment in pid.PidSegmentList
            select
              new XElement(
              XName.Get(
                "PID.3", 
                "urn:hl7-org:v2xml"), 
              new object[]
                {
                  new XElement(
                    XName.Get(
                      "CX.1", 
                      "urn:hl7-org:v2xml"), 
                    pidSegment.Enp), 
                  new XElement(
                    XName.Get(
                      "CX.4", 
                      "urn:hl7-org:v2xml"), 
                    new object[]
                      {
                        new XElement(
                          XName.Get(
                            "HD.1", 
                            "urn:hl7-org:v2xml"), 
                          pidSegment.Region), 
                        new XElement(
                          XName.Get(
                            "HD.2", 
                            "urn:hl7-org:v2xml"), 
                          HL7Helper
                          .TypeCode_Region5Code), 
                        new XElement(
                          XName.Get(
                            "HD.3", 
                            "urn:hl7-org:v2xml"), 
                          "ISO")
                      }), 
                  new XElement(
                    XName.Get(
                      "CX.5", 
                      "urn:hl7-org:v2xml"), 
                    "NI")
                }), 
            new XElement(XName.Get("PID.5", "urn:hl7-org:v2xml"), string.Empty), 
            new XElement(XName.Get("PID.7", "urn:hl7-org:v2xml"), string.Empty), 
            new XElement(XName.Get("PID.8", "urn:hl7-org:v2xml"), string.Empty)
          });
    }

    #endregion
  }
}