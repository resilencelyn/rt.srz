// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlReplyFilesCompiler.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The xml reply files compiler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.foms.HL7.xml
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Xml.Linq;
  using System.Xml.XPath;

  using rt.foms.HL7.commons;
  using rt.foms.HL7.commons.Enumerations;
  using rt.foms.HL7.enumerations;
  using rt.foms.HL7.enumerations.resolve;
  using rt.foms.HL7.parameters;
  using rt.foms.HL7.parameters.target;
  using rt.foms.HL7.person.messages;
  using rt.foms.HL7.person.requests;
  using rt.foms.HL7.person.target;
  using rt.foms.HL7.xml.writer;

  #endregion

  /// <summary>
  ///   The xml reply files compiler.
  /// </summary>
  internal sealed class XmlReplyFilesCompiler
  {
    #region Fields

    /// <summary>
    ///   The answer errors.
    /// </summary>
    private readonly IList<MessageContainer> answerErrors = new List<MessageContainer>();

    /// <summary>
    ///   The answer messages.
    /// </summary>
    private readonly IDictionary<string, MessageContainer> answerMessages =
      new Dictionary<string, MessageContainer>(TextHelper.RetrieveComparer(HL7Helper.IdentifiersCaseInsesitive));

    /// <summary>
    ///   The parameters.
    /// </summary>
    private readonly Parameters parameters;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlReplyFilesCompiler"/> class.
    /// </summary>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    internal XmlReplyFilesCompiler(Parameters parameters)
    {
      this.parameters = parameters;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The reply error xml.
    /// </summary>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    /// <returns>
    /// The <see cref="WorkflowVerb"/>.
    /// </returns>
    internal static WorkflowVerb ReplyErrorXml(Parameters parameters)
    {
      var compiler = new XmlReplyFilesCompiler(parameters);
      return compiler.WriteErrorXml();
    }

    /// <summary>
    ///   The parse directories.
    /// </summary>
    /// <returns>
    ///   The <see cref="WorkflowVerb" />.
    /// </returns>
    internal WorkflowVerb ParseDirectories()
    {
      try
      {
        var info = new DirectoryInfo(parameters.ScratchFolder);
        if (info.Exists)
        {
          foreach (var str in Directory.GetFiles(parameters.ScratchFolder, "*.*", SearchOption.AllDirectories))
          {
            RSP_ZK2 rsp_zk2;
            RSP_ZK4 rsp_zk3;
            RSP_ZK5 rsp_zk4;
            Ack ack;
            var extension = Path.GetExtension(str);
            if (!string.IsNullOrEmpty(extension))
            {
              extension = extension.ToLower();
            }

            var str3 = extension;
            if (str3 != null)
            {
              if (!(str3 == ".rsp_zk1"))
              {
                if (str3 == ".rsp_zk2")
                {
                  goto Label_00E3;
                }

                if (str3 == ".rsp_zk4")
                {
                  goto Label_0111;
                }

                if (str3 == ".rsp_zk5")
                {
                  goto Label_013C;
                }

                if (str3 == ".ack")
                {
                  goto Label_0167;
                }
              }
              else
              {
                var rsp_zk = XmlStreamer.Deserialize<RSP_ZK1>(str);
                if (rsp_zk != null)
                {
                  answerMessages.Add(rsp_zk.Msa.ReferenceIdentificator, new MessageContainer(str));
                }
              }
            }

            goto Label_0190;
            Label_00E3:
            rsp_zk2 = XmlStreamer.Deserialize<RSP_ZK2>(str);
            if (rsp_zk2 != null)
            {
              answerMessages.Add(rsp_zk2.Msa.ReferenceIdentificator, new MessageContainer(str));
            }

            goto Label_0190;
            Label_0111:
            rsp_zk3 = XmlStreamer.Deserialize<RSP_ZK4>(str);
            if (rsp_zk3 != null)
            {
              answerMessages.Add(rsp_zk3.Msa.ReferenceIdentificator, new MessageContainer(str));
            }

            goto Label_0190;
            Label_013C:
            rsp_zk4 = XmlStreamer.Deserialize<RSP_ZK5>(str);
            if (rsp_zk4 != null)
            {
              answerMessages.Add(rsp_zk4.Msa.ReferenceIdentificator, new MessageContainer(str));
            }

            goto Label_0190;
            Label_0167:
            ack = XmlStreamer.Deserialize<Ack>(str);
            if (ack != null)
            {
              answerMessages.Add(ack.Msa.ReferenceIdentificator, new MessageContainer(str));
            }

            Label_0190:
            ;
          }
        }

        return LogicXmlAnswer();
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception);
      }

      return WorkflowVerb.Abort;
    }

    /// <summary>
    ///   The break packet.
    /// </summary>
    /// <returns>
    ///   The <see cref="WorkflowVerb" />.
    /// </returns>
    private WorkflowVerb BreakPacket()
    {
      if (parameters == null)
      {
        return WorkflowVerb.Abort;
      }

      parameters.InputDataEventType = DataEventType.BatchProcess;
      return WriteErrorXml();
    }

    /// <summary>
    ///   The compare file with structure.
    /// </summary>
    /// <returns>
    ///   The <see cref="WorkflowVerb" />.
    /// </returns>
    private WorkflowVerb CompareFileWithStructure()
    {
      try
      {
        answerErrors.Clear();
        var expression = XmlHelper.FormatXPath(parameters.Constants.XmlNamespaceResolver, new[] { "MSH", "MSH.10" });
        for (var node = XElement.Load(new StreamReader(parameters.SourceFile)).FirstNode;
             node != null;
             node = node.NextNode)
        {
          Ack ack;
          ERR err;
          string str3;
          string str4;
          RSP_ZK4 rsp_zk3;
          RSP_ZK5 rsp_zk4;
          MessageContainer container;
          var element2 = node as XElement;
          if (element2 == null)
          {
            continue;
          }

          var localName = element2.Name.LocalName;
          Func<KeyValuePair<string, XmlMessageLogicErrors>, bool> func = null;
          XElement xID;
          switch (localName)
          {
            case "BHS":
            case "BTS":
              {
                continue;
              }

            case "ADT_A01":
            case "ADT_A03":
            case "ADT_A24":
            case "ADT_A37":
            case "QBP_ZP1":
            case "QBP_ZP2":
            case "QBP_ZP4":
            case "ZPI_ZA1":
            case "ZPI_ZA7":
              {
                xID = element2.XPathSelectElement(expression, parameters.Constants.XmlNamespaceResolver);
                if ((xID != null) && !string.IsNullOrEmpty(xID.Value))
                {
                  goto Label_0643;
                }

                var str6 = localName;
                if (str6 == null)
                {
                  goto Label_0567;
                }

                if (!(str6 == "QBP_ZP1"))
                {
                  if (str6 == "QBP_ZP2")
                  {
                    break;
                  }

                  if (str6 == "QBP_ZP4")
                  {
                    goto Label_03BF;
                  }

                  if (str6 == "RSP_ZK5")
                  {
                    goto Label_049B;
                  }

                  goto Label_0567;
                }

                var rsp_zk = new RSP_ZK1();
                str4 = Guid.NewGuid().ToString();
                rsp_zk.Msh.Identificator = str4;
                rsp_zk.Msa.CodeConfirm = "CE";
                rsp_zk.Msa.ReferenceIdentificator = localName;
                var err2 = new ERR
                             {
                               ErrorCodeHl7 =
                                 {
                                   ErrorCode = "101", 
                                   ErrorDescription = "Не указан идентификатор сообщения"
                                 }, 
                               LevelSeriously = "E"
                             };
                rsp_zk.ErrList.Add(err2);
                parameters.InputDataEventType = DataEventType.QueryPersonInsurance;
                str3 = parameters.RetrieveTempFilePath(str4);
                XmlStreamer.Serialize(rsp_zk, str3, parameters.Constants.XmlNamespaceResolver);
                answerErrors.Add(new MessageContainer(str3));
                continue;
              }

            default:
              goto Label_09B1;
          }

          var rsp_zk2 = new RSP_ZK2();
          str4 = Guid.NewGuid().ToString();
          rsp_zk2.Msh.Identificator = str4;
          rsp_zk2.Msa.CodeConfirm = "CE";
          rsp_zk2.Msa.ReferenceIdentificator = localName;
          var item = new ERR
                       {
                         ErrorCodeHl7 =
                           {
                             ErrorCode = "101", 
                             ErrorDescription = "Не указан идентификатор сообщения"
                           }, 
                         LevelSeriously = "E"
                       };
          rsp_zk2.ErrList.Add(item);
          parameters.InputDataEventType = DataEventType.QueryPersonsRegistrating;
          str3 = parameters.RetrieveTempFilePath(str4);
          XmlStreamer.Serialize(rsp_zk2, str3, parameters.Constants.XmlNamespaceResolver);
          answerErrors.Add(new MessageContainer(str3));
          continue;
          Label_03BF:
          rsp_zk3 = new RSP_ZK4();
          str4 = Guid.NewGuid().ToString();
          rsp_zk3.Msh.Identificator = str4;
          rsp_zk3.Msa.CodeConfirm = "CE";
          rsp_zk3.Msa.ReferenceIdentificator = localName;
          var err4 = new ERR
                       {
                         ErrorCodeHl7 =
                           {
                             ErrorCode = "101", 
                             ErrorDescription = "Не указан идентификатор сообщения"
                           }, 
                         LevelSeriously = "E"
                       };
          rsp_zk3.ErrList.Add(err4);
          parameters.InputDataEventType = DataEventType.QueryPersonsDeadAbroad;
          str3 = parameters.RetrieveTempFilePath(str4);
          XmlStreamer.Serialize(rsp_zk3, str3, parameters.Constants.XmlNamespaceResolver);
          answerErrors.Add(new MessageContainer(str3));
          continue;
          Label_049B:
          rsp_zk4 = new RSP_ZK5();
          str4 = Guid.NewGuid().ToString();
          rsp_zk4.Msh.Identificator = str4;
          rsp_zk4.Msa.CodeConfirm = "CE";
          rsp_zk4.Msa.ReferenceIdentificator = localName;
          var err5 = new ERR
                       {
                         ErrorCodeHl7 =
                           {
                             ErrorCode = "101", 
                             ErrorDescription = "Не указан идентификатор сообщения"
                           }, 
                         LevelSeriously = "E"
                       };
          rsp_zk4.ErrList.Add(err5);
          str3 = parameters.RetrieveTempFilePath(str4);
          XmlStreamer.Serialize(rsp_zk4, str3, parameters.Constants.XmlNamespaceResolver);
          answerErrors.Add(new MessageContainer(str3));
          continue;
          Label_0567:
          ack = new Ack();
          str4 = Guid.NewGuid().ToString();
          ack.Msh.Identificator = str4;
          ack.Msa.CodeConfirm = "CE";
          ack.Msa.ReferenceIdentificator = localName;
          var err6 = new ERR
                       {
                         ErrorCodeHl7 =
                           {
                             ErrorCode = "101", 
                             ErrorDescription = "Не указан идентификатор сообщения"
                           }, 
                         LevelSeriously = "E"
                       };
          ack.ErrList.Add(err6);
          parameters.InputDataEventType = DataEventType.IssuePolicy;
          str3 = parameters.RetrieveTempFilePath(str4);
          XmlStreamer.Serialize(ack, str3, parameters.Constants.XmlNamespaceResolver);
          answerErrors.Add(new MessageContainer(str3));
          continue;
          Label_0643:
          if (answerMessages.ContainsKey(xID.Value) && answerMessages.TryGetValue(xID.Value, out container))
          {
            if ((parameters.MessageLogicErrors != null) && parameters.MessageLogicErrors.ContainsKey(xID.Value))
            {
              if (func == null)
              {
                func = errors => errors.Key == xID.Value;
              }

              var enumerable = from errors in parameters.MessageLogicErrors.Where(func) select errors.Value;
              var message = container.Deserialize();
              foreach (var errors in enumerable)
              {
                foreach (var errors2 in errors.Errors)
                {
                  foreach (var error in errors2.Errors)
                  {
                    err = new ERR();
                    if (errors2.HasXPath())
                    {
                      err.ErrorPosition.SegmentName = errors2.XPath;
                    }

                    if (errors2.HasNodeNumber())
                    {
                      err.ErrorPosition.SegmentId = errors2.NodeNumber;
                    }

                    err.ErrorCodeHl7.ErrorCode = error.HL7_Code;
                    err.ErrorCodeHl7.ErrorDescription = error.Message;
                    err.LevelSeriously = error.HL7_Severity;
                    err.ErrorCodeApp.MessageCode = error.Code;
                    err.ErrorCodeApp.MessageDescription = error.Message;
                    err.ErrorCodeApp.Oid = errors2.ErrorTypeCode;
                    message.ErrList.Add(err);
                  }
                }
              }

              container.Serialize(message);
            }
          }
          else
          {
            ack = new Ack();
            str4 = Guid.NewGuid().ToString();
            ack.Msh.Identificator = str4;
            ack.Msa.CodeConfirm = ConfirmationsHL7.ConfirmAsString(parameters.LastConfirmationHL7);
            ack.Msa.ReferenceIdentificator = xID.Value;
            if ((parameters.ProcessingErrors != null) && (parameters.ProcessingErrors.Count > 0))
            {
              foreach (var error2 in parameters.ProcessingErrors)
              {
                err = new ERR
                        {
                          ErrorCodeHl7 =
                            {
                              ErrorCode = ErrorsHL7.GetErrorCode(error2.Code), 
                              ErrorDescription = error2.Message
                            }, 
                          LevelSeriously = ErrorsHL7.GetErrorSeverityLevel(error2.Code)
                        };
                ack.ErrList.Add(err);
              }
            }

            str3 = parameters.RetrieveTempFilePath(str4);
            XmlStreamer.Serialize(ack, str3, parameters.Constants.XmlNamespaceResolver);
            answerErrors.Add(new MessageContainer(str3));
          }

          continue;
          Label_09B1:
          str4 = Guid.NewGuid().ToString();
          ack = new Ack
                  {
                    Msh = {
                             Identificator = str4 
                          }, 
                    Msa = {
                             CodeConfirm = "CE", ReferenceIdentificator = localName 
                          }
                  };
          err = new ERR
                  {
                    ErrorCodeHl7 = {
                                      ErrorCode = "201", ErrorDescription = "Сообщение не опознано обработчиком" 
                                   }, 
                    LevelSeriously = "E"
                  };
          ack.ErrList.Add(err);
          str3 = parameters.RetrieveTempFilePath(str4);
          XmlStreamer.Serialize(ack, str3, parameters.Constants.XmlNamespaceResolver);
          answerErrors.Add(new MessageContainer(str3));
        }

        if ((answerMessages.Count + answerErrors.Count) != parameters.SourceRecordsCount)
        {
          return WorkflowVerb.Abort;
        }

        return WorkflowVerb.Proceed;
      }
      catch (Exception exception)
      {
        FomsLogger.WriteError(exception, null);
        return WorkflowVerb.Abort;
      }
    }

    /// <summary>
    /// The lazy read errors.
    /// </summary>
    /// <param name="ignoreMessageErrors">
    /// The ignore message errors.
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable"/>.
    /// </returns>
    private IEnumerable<XElement> LazyReadErrors(bool ignoreMessageErrors = false)
    {
      long messagesExpected = parameters.ProcessingErrors.Count + parameters.BatchLogicErrors.Count;
      if (!ignoreMessageErrors)
      {
        messagesExpected += (parameters.MessageLogicErrors.Count + answerMessages.Count) + answerErrors.Count;
      }

      var messagesWritten = 0L;
      foreach (var iteratorVariable2 in parameters.ProcessingErrors)
      {
        if (parameters.UserTimerTripped())
        {
          UserInteractionProgress(messagesExpected, messagesWritten);
        }

        yield return
          XmlCreator.CreateProcessingError(
            parameters.Constants, 
            iteratorVariable2.Code, 
            iteratorVariable2.Message, 
            null);
        messagesWritten += 1L;
      }

      foreach (var iteratorVariable3 in parameters.BatchLogicErrors)
      {
        if (parameters.UserTimerTripped())
        {
          UserInteractionProgress(messagesExpected, messagesWritten);
        }

        foreach (var iteratorVariable4 in iteratorVariable3.Errors)
        {
          var errorTypeCode = iteratorVariable3.ErrorTypeCode;
          var code = iteratorVariable4.Code;
          var message = iteratorVariable4.Message;
          var str4 = iteratorVariable4.HL7_Severity;
          var str5 = iteratorVariable4.HL7_Code;
          var str6 = parameters.Constants.MessageByErrorCodeHL7(iteratorVariable4.HL7_Code);
          var xpath = iteratorVariable3.HasXPath() ? iteratorVariable3.XPath : null;
          var nodeNumber = iteratorVariable3.HasNodeNumber() ? iteratorVariable3.NodeNumber : null;
          yield return
            XmlCreator.CreateLogicError(errorTypeCode, code, message, str5, str6, str4, null, xpath, nodeNumber);
        }

        messagesWritten += 1L;
      }

      if (!ignoreMessageErrors)
      {
        foreach (var iteratorVariable5 in parameters.MessageLogicErrors)
        {
          if (parameters.UserTimerTripped())
          {
            UserInteractionProgress(messagesExpected, messagesWritten);
          }

          foreach (var iteratorVariable6 in iteratorVariable5.Value.Errors)
          {
            foreach (var iteratorVariable7 in iteratorVariable6.Errors)
            {
              var str9 = iteratorVariable6.ErrorTypeCode;
              var str10 = iteratorVariable7.Code;
              var str11 = iteratorVariable7.Message;
              var str12 = iteratorVariable7.HL7_Severity;
              var str13 = iteratorVariable7.HL7_Code;
              var str14 = parameters.Constants.MessageByErrorCodeHL7(iteratorVariable7.HL7_Code);
              var str15 = iteratorVariable6.HasXPath() ? iteratorVariable6.XPath : null;
              var str16 = iteratorVariable6.HasNodeNumber() ? iteratorVariable6.NodeNumber : null;
              var reference = iteratorVariable5.Key;
              yield return XmlCreator.CreateLogicError(str9, str10, str11, str13, str14, str12, reference, str15, str16)
                ;
            }
          }

          messagesWritten += 1L;
        }

        foreach (var iteratorVariable8 in answerMessages)
        {
          if (parameters.UserTimerTripped())
          {
            UserInteractionProgress(messagesExpected, messagesWritten);
          }

          var xmlNode = iteratorVariable8.Value.Load();
          if (xmlNode != null)
          {
            foreach (var iteratorVariable10 in xmlNode.SelectByLocalName("ERR"))
            {
              XmlCreator.AddErrorReference(iteratorVariable10, iteratorVariable8.Key);
              yield return iteratorVariable10;
            }
          }

          messagesWritten += 1L;
        }

        foreach (var iteratorVariable11 in answerErrors)
        {
          if (parameters.UserTimerTripped())
          {
            UserInteractionProgress(messagesExpected, messagesWritten);
          }

          var iteratorVariable12 = iteratorVariable11.Load();
          if (iteratorVariable12 != null)
          {
            foreach (var iteratorVariable13 in iteratorVariable12.SelectByLocalName("ERR"))
            {
              yield return iteratorVariable13;
            }
          }

          messagesWritten += 1L;
        }
      }
    }

    /// <summary>
    ///   The lazy read temp messages.
    /// </summary>
    /// <returns>
    ///   The <see cref="IEnumerable" />.
    /// </returns>
    private IEnumerable<XElement> LazyReadTempMessages()
    {
      long messagesExpected = answerMessages.Count + answerErrors.Count;
      var messagesWritten = 0L;
      foreach (var iteratorVariable2 in answerMessages.Values)
      {
        if (parameters.UserTimerTripped())
        {
          UserInteractionProgress(messagesExpected, messagesWritten);
        }

        yield return iteratorVariable2.Load();
        messagesWritten += 1L;
      }

      foreach (var iteratorVariable3 in answerErrors)
      {
        if (parameters.UserTimerTripped())
        {
          UserInteractionProgress(messagesExpected, messagesWritten);
        }

        yield return iteratorVariable3.Load();
        messagesWritten += 1L;
      }
    }

    /// <summary>
    ///   The logic xml answer.
    /// </summary>
    /// <returns>
    ///   The <see cref="WorkflowVerb" />.
    /// </returns>
    private WorkflowVerb LogicXmlAnswer()
    {
      try
      {
        if (parameters == null)
        {
          return WorkflowVerb.Abort;
        }

        if ((parameters.ProcessingErrors != null) && (parameters.ProcessingErrors.Count > 0))
        {
          for (var i = parameters.ProcessingErrors.Count - 1; i >= 0; i--)
          {
            if (!ErrorsHL7.IsBatchProcessible(parameters.ProcessingErrors[i].Code))
            {
              parameters.ProcessingErrors.RemoveAt(i);
            }
          }

          foreach (var error in parameters.ProcessingErrors)
          {
            if (error.Code == ErrorHL7.BatchMessagesOverload)
            {
              return BreakPacket();
            }

            if ((error.Code == ErrorHL7.BatchMessagesMissing) && (CompareFileWithStructure() != WorkflowVerb.Proceed))
            {
              return BreakPacket();
            }

            if (!ErrorsHL7.IsWorkflowEffector(error.Code))
            {
              return BreakPacket();
            }
          }
        }

        if ((parameters.BatchLogicErrors != null) && (parameters.BatchLogicErrors.Count > 0))
        {
          return BreakPacket();
        }

        if (((parameters.MessageLogicErrors != null) && (parameters.MessageLogicErrors.Count > 0))
            && (CompareFileWithStructure() == WorkflowVerb.Abort))
        {
          return BreakPacket();
        }

        return WriteXml(LazyReadTempMessages());
      }
      catch (Exception exception)
      {
        FomsLogger.WriteError(exception, null);
        return WorkflowVerb.Abort;
      }
    }

    /// <summary>
    /// The user interaction progress.
    /// </summary>
    /// <param name="messagesExpected">
    /// The messages expected.
    /// </param>
    /// <param name="messagesWritten">
    /// The messages written.
    /// </param>
    private void UserInteractionProgress(long messagesExpected, long messagesWritten)
    {
      if (messagesExpected > 0L)
      {
        parameters.TaskProgressUpdate(messagesWritten / ((double)messagesExpected));
      }

      parameters.UserTimerRestart();
    }

    /// <summary>
    ///   The user interaction start.
    /// </summary>
    private void UserInteractionStart()
    {
      parameters.TaskProgressUpdate(0.0);
      parameters.UserTimerRestart();
    }

    /// <summary>
    ///   The user interaction stop.
    /// </summary>
    private void UserInteractionStop()
    {
      parameters.UserTimerStop();
      parameters.TaskProgressUpdate(1.0);
    }

    /// <summary>
    ///   The write error xml.
    /// </summary>
    /// <returns>
    ///   The <see cref="WorkflowVerb" />.
    /// </returns>
    private WorkflowVerb WriteErrorXml()
    {
      try
      {
        var person = parameters.CreatePersonObject();
        var batchFatalReference = true;
        var filePath = person.BeginPacket.PrepareHeader(parameters, true, batchFatalReference);
        UserInteractionStart();
        try
        {
          var flag2 = true;
          XmlStreamer.SerializeWithHash(
            person, 
            new HL7MessageWriter(
              XName.Get("ACK", "urn:hl7-org:v2xml"), 
              parameters, 
              LazyReadErrors(ConfigHelper.ReadConfigValue("HL7_BatchErrorsDiscardMessageErrors", false)), 
              flag2), 
            filePath, 
            parameters.Constants.XmlNamespaceResolver);
        }
        finally
        {
          UserInteractionStop();
        }
      }
      catch (Exception exception)
      {
        FomsLogger.WriteError(exception, null);
      }

      return WorkflowVerb.Abort;
    }

    /// <summary>
    /// The write xml.
    /// </summary>
    /// <param name="xElements">
    /// The x elements.
    /// </param>
    /// <returns>
    /// The <see cref="WorkflowVerb"/>.
    /// </returns>
    private WorkflowVerb WriteXml(IEnumerable<XElement> xElements)
    {
      try
      {
        var person = parameters.CreatePersonObject();
        var filePath = person.BeginPacket.PrepareHeader(parameters, true, false);
        UserInteractionStart();
        try
        {
          if (XmlStreamer.SerializeWithHash(person, xElements, filePath, parameters.Constants.XmlNamespaceResolver))
          {
            return WorkflowVerb.Proceed;
          }
        }
        finally
        {
          UserInteractionStop();
        }
      }
      catch (Exception exception)
      {
        FomsLogger.WriteError(exception, null);
      }

      return WorkflowVerb.Abort;
    }

    #endregion

    //// [CompilerGenerated]
    //// private sealed class <LazyReadErrors>d__b : IEnumerable<XElement>, IEnumerable, IEnumerator<XElement>, IEnumerator, IDisposable
    //// {
    //// private int <>1__state;
    //// private XElement <>2__current;
    //// public bool <>3__ignoreMessageErrors;
    //// public XmlReplyFilesCompiler <>4__this;
    //// public IEnumerator<ProcessingError> <>7__wrap1a;
    //// public IEnumerator<XmlLogicErrors> <>7__wrap1c;
    //// public IEnumerator<LogicError> <>7__wrap1e;
    //// public IEnumerator<KeyValuePair<string, XmlMessageLogicErrors>> <>7__wrap20;
    //// public IEnumerator<XmlLogicErrors> <>7__wrap22;
    //// public IEnumerator<LogicError> <>7__wrap24;
    //// public IEnumerator<KeyValuePair<string, MessageContainer>> <>7__wrap26;
    //// public IEnumerator<XElement> <>7__wrap28;
    //// public IEnumerator<MessageContainer> <>7__wrap2a;
    //// public IEnumerator<XElement> <>7__wrap2c;
    //// private int <>l__initialThreadId;
    //// public MessageContainer <answerError>5__17;
    //// public KeyValuePair<string, MessageContainer> <answerMessage>5__14;
    //// public XmlLogicErrors <batchLogicErrors>5__f;
    //// public LogicError <error>5__10;
    //// public LogicError <error>5__13;
    //// public XElement <error>5__16;
    //// public XElement <error>5__19;
    //// public ProcessingError <error>5__e;
    //// public XmlLogicErrors <errors>5__12;
    //// public KeyValuePair<string, XmlMessageLogicErrors> <messageLogicErrors>5__11;
    //// public long <messagesExpected>5__c;
    //// public long <messagesWritten>5__d;
    //// public XElement <reply>5__15;
    //// public XElement <reply>5__18;
    //// public bool ignoreMessageErrors;

    //// [DebuggerHidden]
    //// public <LazyReadErrors>d__b(int <>1__state)
    //// {
    //// this.<>1__state = <>1__state;
    //// this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
    //// }

    //// private void <>m__Finally1b()
    //// {
    //// this.<>1__state = -1;
    //// if (this.<>7__wrap1a != null)
    //// {
    //// this.<>7__wrap1a.Dispose();
    //// }
    //// }

    //// private void <>m__Finally1d()
    //// {
    //// this.<>1__state = -1;
    //// if (this.<>7__wrap1c != null)
    //// {
    //// this.<>7__wrap1c.Dispose();
    //// }
    //// }

    //// private void <>m__Finally1f()
    //// {
    //// this.<>1__state = 3;
    //// if (this.<>7__wrap1e != null)
    //// {
    //// this.<>7__wrap1e.Dispose();
    //// }
    //// }

    //// private void <>m__Finally21()
    //// {
    //// this.<>1__state = -1;
    //// if (this.<>7__wrap20 != null)
    //// {
    //// this.<>7__wrap20.Dispose();
    //// }
    //// }

    //// private void <>m__Finally23()
    //// {
    //// this.<>1__state = 6;
    //// if (this.<>7__wrap22 != null)
    //// {
    //// this.<>7__wrap22.Dispose();
    //// }
    //// }

    //// private void <>m__Finally25()
    //// {
    //// this.<>1__state = 7;
    //// if (this.<>7__wrap24 != null)
    //// {
    //// this.<>7__wrap24.Dispose();
    //// }
    //// }

    //// private void <>m__Finally27()
    //// {
    //// this.<>1__state = -1;
    //// if (this.<>7__wrap26 != null)
    //// {
    //// this.<>7__wrap26.Dispose();
    //// }
    //// }

    //// private void <>m__Finally29()
    //// {
    //// this.<>1__state = 10;
    //// if (this.<>7__wrap28 != null)
    //// {
    //// this.<>7__wrap28.Dispose();
    //// }
    //// }

    //// private void <>m__Finally2b()
    //// {
    //// this.<>1__state = -1;
    //// if (this.<>7__wrap2a != null)
    //// {
    //// this.<>7__wrap2a.Dispose();
    //// }
    //// }

    //// private void <>m__Finally2d()
    //// {
    //// this.<>1__state = 13;
    //// if (this.<>7__wrap2c != null)
    //// {
    //// this.<>7__wrap2c.Dispose();
    //// }
    //// }

    //// private bool MoveNext()
    //// {
    //// try
    //// {
    //// switch (this.<>1__state)
    //// {
    //// case 9:
    //// goto Label_04F2;

    //// case 12:
    //// goto Label_0638;

    //// case 15:
    //// goto Label_073C;

    //// case 0:
    //// this.<>1__state = -1;
    //// this.<messagesExpected>5__c = this.<>4__this.parameters.ProcessingErrors.Count + this.<>4__this.parameters.BatchLogicErrors.Count;
    //// if (!this.ignoreMessageErrors)
    //// {
    //// this.<messagesExpected>5__c += (this.<>4__this.parameters.MessageLogicErrors.Count + this.<>4__this.answerMessages.Count) + this.<>4__this.answerErrors.Count;
    //// }
    //// this.<messagesWritten>5__d = 0L;
    //// this.<>7__wrap1a = this.<>4__this.parameters.ProcessingErrors.GetEnumerator();
    //// this.<>1__state = 1;
    //// while (this.<>7__wrap1a.MoveNext())
    //// {
    //// this.<error>5__e = this.<>7__wrap1a.Current;
    //// if (this.<>4__this.parameters.UserTimerTripped())
    //// {
    //// this.<>4__this.UserInteractionProgress(this.<messagesExpected>5__c, this.<messagesWritten>5__d);
    //// }
    //// this.<>2__current = XmlCreator.CreateProcessingError(this.<>4__this.parameters.Constants, this.<error>5__e.Code, this.<error>5__e.Message, null);
    //// this.<>1__state = 2;
    //// return true;
    //// Label_0178:
    //// this.<>1__state = 1;
    //// this.<messagesWritten>5__d += 1L;
    //// }
    //// this.<>m__Finally1b();
    //// this.<>7__wrap1c = this.<>4__this.parameters.BatchLogicErrors.GetEnumerator();
    //// this.<>1__state = 3;
    //// while (this.<>7__wrap1c.MoveNext())
    //// {
    //// this.<batchLogicErrors>5__f = this.<>7__wrap1c.Current;
    //// if (this.<>4__this.parameters.UserTimerTripped())
    //// {
    //// this.<>4__this.UserInteractionProgress(this.<messagesExpected>5__c, this.<messagesWritten>5__d);
    //// }
    //// this.<>7__wrap1e = this.<batchLogicErrors>5__f.Errors.GetEnumerator();
    //// this.<>1__state = 4;
    //// while (this.<>7__wrap1e.MoveNext())
    //// {
    //// this.<error>5__10 = this.<>7__wrap1e.Current;
    //// string errorTypeCode = this.<batchLogicErrors>5__f.ErrorTypeCode;
    //// string code = this.<error>5__10.Code;
    //// string message = this.<error>5__10.Message;
    //// string str4 = this.<error>5__10.HL7_Severity;
    //// string str5 = this.<error>5__10.HL7_Code;
    //// string str6 = this.<>4__this.parameters.Constants.MessageByErrorCodeHL7(this.<error>5__10.HL7_Code);
    //// string xpath = this.<batchLogicErrors>5__f.HasXPath() ? this.<batchLogicErrors>5__f.XPath : null;
    //// string nodeNumber = this.<batchLogicErrors>5__f.HasNodeNumber() ? this.<batchLogicErrors>5__f.NodeNumber : null;
    //// this.<>2__current = XmlCreator.CreateLogicError(errorTypeCode, code, message, str5, str6, str4, null, xpath, nodeNumber);
    //// this.<>1__state = 5;
    //// return true;
    //// Label_02FB:
    //// this.<>1__state = 4;
    //// }
    //// this.<>m__Finally1f();
    //// this.<messagesWritten>5__d += 1L;
    //// }
    //// this.<>m__Finally1d();
    //// if (!this.ignoreMessageErrors)
    //// {
    //// this.<>7__wrap20 = this.<>4__this.parameters.MessageLogicErrors.GetEnumerator();
    //// this.<>1__state = 6;
    //// while (this.<>7__wrap20.MoveNext())
    //// {
    //// this.<messageLogicErrors>5__11 = this.<>7__wrap20.Current;
    //// if (this.<>4__this.parameters.UserTimerTripped())
    //// {
    //// this.<>4__this.UserInteractionProgress(this.<messagesExpected>5__c, this.<messagesWritten>5__d);
    //// }
    //// this.<>7__wrap22 = this.<messageLogicErrors>5__11.Value.Errors.GetEnumerator();
    //// this.<>1__state = 7;
    //// while (this.<>7__wrap22.MoveNext())
    //// {
    //// this.<errors>5__12 = this.<>7__wrap22.Current;
    //// this.<>7__wrap24 = this.<errors>5__12.Errors.GetEnumerator();
    //// this.<>1__state = 8;
    //// while (this.<>7__wrap24.MoveNext())
    //// {
    //// this.<error>5__13 = this.<>7__wrap24.Current;
    //// string str9 = this.<errors>5__12.ErrorTypeCode;
    //// string str10 = this.<error>5__13.Code;
    //// string str11 = this.<error>5__13.Message;
    //// string str12 = this.<error>5__13.HL7_Severity;
    //// string str13 = this.<error>5__13.HL7_Code;
    //// string str14 = this.<>4__this.parameters.Constants.MessageByErrorCodeHL7(this.<error>5__13.HL7_Code);
    //// string str15 = this.<errors>5__12.HasXPath() ? this.<errors>5__12.XPath : null;
    //// string str16 = this.<errors>5__12.HasNodeNumber() ? this.<errors>5__12.NodeNumber : null;
    //// string key = this.<messageLogicErrors>5__11.Key;
    //// this.<>2__current = XmlCreator.CreateLogicError(str9, str10, str11, str13, str14, str12, key, str15, str16);
    //// this.<>1__state = 9;
    //// return true;
    //// Label_04F2:
    //// this.<>1__state = 8;
    //// }
    //// this.<>m__Finally25();
    //// }
    //// this.<>m__Finally23();
    //// this.<messagesWritten>5__d += 1L;
    //// }
    //// this.<>m__Finally21();
    //// this.<>7__wrap26 = this.<>4__this.answerMessages.GetEnumerator();
    //// this.<>1__state = 10;
    //// while (this.<>7__wrap26.MoveNext())
    //// {
    //// this.<answerMessage>5__14 = this.<>7__wrap26.Current;
    //// if (this.<>4__this.parameters.UserTimerTripped())
    //// {
    //// this.<>4__this.UserInteractionProgress(this.<messagesExpected>5__c, this.<messagesWritten>5__d);
    //// }
    //// this.<reply>5__15 = this.<answerMessage>5__14.Value.Load();
    //// if (this.<reply>5__15 != null)
    //// {
    //// this.<>7__wrap28 = this.<reply>5__15.SelectByLocalName("ERR").GetEnumerator();
    //// this.<>1__state = 11;
    //// while (this.<>7__wrap28.MoveNext())
    //// {
    //// this.<error>5__16 = this.<>7__wrap28.Current;
    //// XmlCreator.AddErrorReference(this.<error>5__16, this.<answerMessage>5__14.Key);
    //// this.<>2__current = this.<error>5__16;
    //// this.<>1__state = 12;
    //// return true;
    //// Label_0638:
    //// this.<>1__state = 11;
    //// }
    //// this.<>m__Finally29();
    //// }
    //// this.<messagesWritten>5__d += 1L;
    //// }
    //// this.<>m__Finally27();
    //// this.<>7__wrap2a = this.<>4__this.answerErrors.GetEnumerator();
    //// this.<>1__state = 13;
    //// while (this.<>7__wrap2a.MoveNext())
    //// {
    //// this.<answerError>5__17 = this.<>7__wrap2a.Current;
    //// if (this.<>4__this.parameters.UserTimerTripped())
    //// {
    //// this.<>4__this.UserInteractionProgress(this.<messagesExpected>5__c, this.<messagesWritten>5__d);
    //// }
    //// this.<reply>5__18 = this.<answerError>5__17.Load();
    //// if (this.<reply>5__18 != null)
    //// {
    //// this.<>7__wrap2c = this.<reply>5__18.SelectByLocalName("ERR").GetEnumerator();
    //// this.<>1__state = 14;
    //// while (this.<>7__wrap2c.MoveNext())
    //// {
    //// this.<error>5__19 = this.<>7__wrap2c.Current;
    //// this.<>2__current = this.<error>5__19;
    //// this.<>1__state = 15;
    //// return true;
    //// Label_073C:
    //// this.<>1__state = 14;
    //// }
    //// this.<>m__Finally2d();
    //// }
    //// this.<messagesWritten>5__d += 1L;
    //// }
    //// this.<>m__Finally2b();
    //// }
    //// break;

    //// case 2:
    //// goto Label_0178;

    //// case 5:
    //// goto Label_02FB;
    //// }
    //// return false;
    //// }
    //// fault
    //// {
    //// this.System.IDisposable.Dispose();
    //// }
    //// }

    //// [DebuggerHidden]
    //// IEnumerator<XElement> IEnumerable<XElement>.GetEnumerator()
    //// {
    //// XmlReplyFilesCompiler.<LazyReadErrors>d__b _b;
    //// if ((Thread.CurrentThread.ManagedThreadId == this.<>l__initialThreadId) && (this.<>1__state == -2))
    //// {
    //// this.<>1__state = 0;
    //// _b = this;
    //// }
    //// else
    //// {
    //// _b = new XmlReplyFilesCompiler.<LazyReadErrors>d__b(0) {
    //// <>4__this = this.<>4__this
    //// };
    //// }
    //// _b.ignoreMessageErrors = this.<>3__ignoreMessageErrors;
    //// return _b;
    //// }

    //// [DebuggerHidden]
    //// IEnumerator IEnumerable.GetEnumerator()
    //// {
    //// return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement>.GetEnumerator();
    //// }

    //// [DebuggerHidden]
    //// void IEnumerator.Reset()
    //// {
    //// throw new NotSupportedException();
    //// }

    //// void IDisposable.Dispose()
    //// {
    //// switch (this.<>1__state)
    //// {
    //// case 1:
    //// case 2:
    //// try
    //// {
    //// }
    //// finally
    //// {
    //// this.<>m__Finally1b();
    //// }
    //// break;

    //// case 3:
    //// case 4:
    //// case 5:
    //// try
    //// {
    //// switch (this.<>1__state)
    //// {
    //// case 4:
    //// case 5:
    //// try
    //// {
    //// }
    //// finally
    //// {
    //// this.<>m__Finally1f();
    //// }
    //// break;
    //// }
    //// }
    //// finally
    //// {
    //// this.<>m__Finally1d();
    //// }
    //// break;

    //// case 6:
    //// case 7:
    //// case 8:
    //// case 9:
    //// try
    //// {
    //// switch (this.<>1__state)
    //// {
    //// case 7:
    //// case 8:
    //// case 9:
    //// try
    //// {
    //// switch (this.<>1__state)
    //// {
    //// case 8:
    //// case 9:
    //// try
    //// {
    //// }
    //// finally
    //// {
    //// this.<>m__Finally25();
    //// }
    //// break;
    //// }
    //// }
    //// finally
    //// {
    //// this.<>m__Finally23();
    //// }
    //// break;
    //// }
    //// }
    //// finally
    //// {
    //// this.<>m__Finally21();
    //// }
    //// break;

    //// case 10:
    //// case 11:
    //// case 12:
    //// try
    //// {
    //// switch (this.<>1__state)
    //// {
    //// case 11:
    //// case 12:
    //// try
    //// {
    //// }
    //// finally
    //// {
    //// this.<>m__Finally29();
    //// }
    //// break;
    //// }
    //// }
    //// finally
    //// {
    //// this.<>m__Finally27();
    //// }
    //// break;
    //// }
    //// switch (this.<>1__state)
    //// {
    //// case 13:
    //// case 14:
    //// case 15:
    //// try
    //// {
    //// switch (this.<>1__state)
    //// {
    //// case 14:
    //// case 15:
    //// try
    //// {
    //// }
    //// finally
    //// {
    //// this.<>m__Finally2d();
    //// }
    //// break;
    //// }
    //// }
    //// finally
    //// {
    //// this.<>m__Finally2b();
    //// }
    //// break;

    //// default:
    //// return;
    //// }
    //// }

    //// XElement IEnumerator<XElement>.Current
    //// {
    //// [DebuggerHidden]
    //// get
    //// {
    //// return this.<>2__current;
    //// }
    //// }

    //// object IEnumerator.Current
    //// {
    //// [DebuggerHidden]
    //// get
    //// {
    //// return this.<>2__current;
    //// }
    //// }
    //// }

    //// [CompilerGenerated]
    //// private sealed class <LazyReadTempMessages>d__0 : IEnumerable<XElement>, IEnumerable, IEnumerator<XElement>, IEnumerator, IDisposable
    //// {
    //// private int <>1__state;
    //// private XElement <>2__current;
    //// public XmlReplyFilesCompiler <>4__this;
    //// public IEnumerator<MessageContainer> <>7__wrap5;
    //// public IEnumerator<MessageContainer> <>7__wrap7;
    //// private int <>l__initialThreadId;
    //// public long <messagesExpected>5__1;
    //// public long <messagesWritten>5__2;
    //// public MessageContainer <value>5__3;
    //// public MessageContainer <value>5__4;

    //// [DebuggerHidden]
    //// public <LazyReadTempMessages>d__0(int <>1__state)
    //// {
    //// this.<>1__state = <>1__state;
    //// this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
    //// }

    //// private void <>m__Finally6()
    //// {
    //// this.<>1__state = -1;
    //// if (this.<>7__wrap5 != null)
    //// {
    //// this.<>7__wrap5.Dispose();
    //// }
    //// }

    //// private void <>m__Finally8()
    //// {
    //// this.<>1__state = -1;
    //// if (this.<>7__wrap7 != null)
    //// {
    //// this.<>7__wrap7.Dispose();
    //// }
    //// }

    //// private bool MoveNext()
    //// {
    //// bool flag;
    //// try
    //// {
    //// switch (this.<>1__state)
    //// {
    //// case 0:
    //// this.<>1__state = -1;
    //// this.<messagesExpected>5__1 = this.<>4__this.answerMessages.Count + this.<>4__this.answerErrors.Count;
    //// this.<messagesWritten>5__2 = 0L;
    //// this.<>7__wrap5 = this.<>4__this.answerMessages.Values.GetEnumerator();
    //// this.<>1__state = 1;
    //// goto Label_00F1;

    //// case 2:
    //// this.<>1__state = 1;
    //// this.<messagesWritten>5__2 += 1L;
    //// goto Label_00F1;

    //// case 4:
    //// goto Label_0179;

    //// default:
    //// goto Label_01A2;
    //// }
    //// Label_0082:
    //// this.<value>5__3 = this.<>7__wrap5.Current;
    //// if (this.<>4__this.parameters.UserTimerTripped())
    //// {
    //// this.<>4__this.UserInteractionProgress(this.<messagesExpected>5__1, this.<messagesWritten>5__2);
    //// }
    //// this.<>2__current = this.<value>5__3.Load();
    //// this.<>1__state = 2;
    //// return true;
    //// Label_00F1:
    //// if (this.<>7__wrap5.MoveNext())
    //// {
    //// goto Label_0082;
    //// }
    //// this.<>m__Finally6();
    //// this.<>7__wrap7 = this.<>4__this.answerErrors.GetEnumerator();
    //// this.<>1__state = 3;
    //// while (this.<>7__wrap7.MoveNext())
    //// {
    //// this.<value>5__4 = this.<>7__wrap7.Current;
    //// if (this.<>4__this.parameters.UserTimerTripped())
    //// {
    //// this.<>4__this.UserInteractionProgress(this.<messagesExpected>5__1, this.<messagesWritten>5__2);
    //// }
    //// this.<>2__current = this.<value>5__4.Load();
    //// this.<>1__state = 4;
    //// return true;
    //// Label_0179:
    //// this.<>1__state = 3;
    //// this.<messagesWritten>5__2 += 1L;
    //// }
    //// this.<>m__Finally8();
    //// Label_01A2:
    //// flag = false;
    //// }
    //// fault
    //// {
    //// this.System.IDisposable.Dispose();
    //// }
    //// return flag;
    //// }

    //// [DebuggerHidden]
    //// IEnumerator<XElement> IEnumerable<XElement>.GetEnumerator()
    //// {
    //// if ((Thread.CurrentThread.ManagedThreadId == this.<>l__initialThreadId) && (this.<>1__state == -2))
    //// {
    //// this.<>1__state = 0;
    //// return this;
    //// }
    //// return new XmlReplyFilesCompiler.<LazyReadTempMessages>d__0(0) { <>4__this = this.<>4__this };
    //// }

    //// [DebuggerHidden]
    //// IEnumerator IEnumerable.GetEnumerator()
    //// {
    //// return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement>.GetEnumerator();
    //// }

    //// [DebuggerHidden]
    //// void IEnumerator.Reset()
    //// {
    //// throw new NotSupportedException();
    //// }

    //// void IDisposable.Dispose()
    //// {
    //// switch (this.<>1__state)
    //// {
    //// case 1:
    //// case 2:
    //// try
    //// {
    //// }
    //// finally
    //// {
    //// this.<>m__Finally6();
    //// }
    //// break;

    //// case 3:
    //// case 4:
    //// try
    //// {
    //// }
    //// finally
    //// {
    //// this.<>m__Finally8();
    //// }
    //// return;
    //// }
    //// }

    //// XElement IEnumerator<XElement>.Current
    //// {
    //// [DebuggerHidden]
    //// get
    //// {
    //// return this.<>2__current;
    //// }
    //// }

    //// object IEnumerator.Current
    //// {
    //// [DebuggerHidden]
    //// get
    //// {
    //// return this.<>2__current;
    //// }
    //// }
    //// }
  }
}