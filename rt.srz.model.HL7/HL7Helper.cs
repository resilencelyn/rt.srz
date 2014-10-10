// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Hl7Helper.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The h l 7 helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text;
  using System.Threading;
  using System.Xml.Linq;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.commons;
  using rt.srz.model.Hl7.commons.Enumerations;
  using rt.srz.model.Hl7.enumerations;
  using rt.srz.model.Hl7.person;
  using rt.srz.model.Hl7.person.messages;
  using rt.srz.model.Hl7.person.requests;
  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   The h l 7 helper.
  /// </summary>
  public static class Hl7Helper
  {
    #region Constants

    /// <summary>
    ///   The h l 7 namespace.
    /// </summary>
    public const string Hl7Namespace = "urn:Hl7-org:v2xml";

    /// <summary>
    ///   The iso.
    /// </summary>
    public const string ISO = "ISO";

    /// <summary>
    ///   The node name_ ad t_ a 01.
    /// </summary>
    public const string NodeName_ADT_A01 = "ADT_A01";

    /// <summary>
    ///   The node name_ ad t_ a 03.
    /// </summary>
    public const string NodeName_ADT_A03 = "ADT_A03";

    /// <summary>
    ///   The node name_ ad t_ a 24.
    /// </summary>
    public const string NodeName_ADT_A24 = "ADT_A24";

    /// <summary>
    ///   The node name_ ad t_ a 37.
    /// </summary>
    public const string NodeName_ADT_A37 = "ADT_A37";

    /// <summary>
    ///   The node name_ batch file hash.
    /// </summary>
    public const string NodeName_BatchFileHash = "BTS.3";

    /// <summary>
    ///   The node name_ batch header.
    /// </summary>
    public const string NodeName_BatchHeader = "BHS";

    /// <summary>
    ///   The node name_ batch messages count.
    /// </summary>
    public const string NodeName_BatchMessagesCount = "BTS.1";

    /// <summary>
    ///   The node name_ batch processing mode.
    /// </summary>
    public const string NodeName_BatchProcessingMode = "BHS.9";

    /// <summary>
    ///   The node name_ batch trailer.
    /// </summary>
    public const string NodeName_BatchTrailer = "BTS";

    /// <summary>
    ///   The node name_ error.
    /// </summary>
    public const string NodeName_Error = "ERR";

    /// <summary>
    ///   The node name_ error app.
    /// </summary>
    public const string NodeName_ErrorAPP = "ERR.5";

    /// <summary>
    ///   The node name_ error iso.
    /// </summary>
    public const string NodeName_ErrorISO = "ERR.3";

    /// <summary>
    ///   The node name_ insurance node.
    /// </summary>
    public const string NodeName_InsuranceNode = "IN1";

    /// <summary>
    ///   The node name_ insurance node number.
    /// </summary>
    public const string NodeName_InsuranceNodeNumber = "IN1.1";

    /// <summary>
    ///   The node name_ message header.
    /// </summary>
    public const string NodeName_MessageHeader = "MSH";

    /// <summary>
    ///   The node name_ message identifier.
    /// </summary>
    public const string NodeName_MessageIdentifier = "MSH.10";

    /// <summary>
    ///   The node name_ qb p_ z p 1.
    /// </summary>
    public const string NodeName_QBP_ZP1 = "QBP_ZP1";

    /// <summary>
    ///   The node name_ qb p_ z p 2.
    /// </summary>
    public const string NodeName_QBP_ZP2 = "QBP_ZP2";

    /// <summary>
    ///   The node name_ qb p_ z p 4.
    /// </summary>
    public const string NodeName_QBP_ZP4 = "QBP_ZP4";

    /// <summary>
    ///   The node name_ zp i_ z a 1.
    /// </summary>
    public const string NodeName_ZPI_ZA1 = "ZPI_ZA1";

    /// <summary>
    ///   The node name_ zp i_ z a 7.
    /// </summary>
    public const string NodeName_ZPI_ZA7 = "ZPI_ZA7";

    #endregion

    #region Static Fields

    /// <summary>
    ///   The bh s_ code symbols.
    /// </summary>
    public static readonly string BHS_CodeSymbols = @"^~\&";

    /// <summary>
    ///   The bh s_ delimiter.
    /// </summary>
    public static readonly string BHS_Delimiter = "|";

    /// <summary>
    ///   The fatal severity level.
    /// </summary>
    public static readonly string FatalSeverityLevel = "E";

    /// <summary>
    ///   The foms log prefix.
    /// </summary>
    public static readonly string FomsLogPrefix = "Hl7: ";

    /// <summary>
    ///   The identifiers case insesitive.
    /// </summary>
    public static readonly bool IdentifiersCaseInsesitive = false;

    /// <summary>
    ///   The node name_ batch root_ erp.
    /// </summary>
    public static readonly string NodeName_BatchRoot_ERP = "UPRMessageBatch";

    /// <summary>
    ///   The node name_ batch root_ kptu.
    /// </summary>
    public static readonly string NodeName_BatchRoot_KPTU = "ZPIMessageBatch";

    /// <summary>
    ///   The node tag 1251_ application name_begin.
    /// </summary>
    public static readonly byte[] NodeTag1251_ApplicationName_begin = Encoding.Default.GetBytes("<BHS.3>");

    /// <summary>
    ///   The node tag 1251_ application name_end.
    /// </summary>
    public static readonly byte[] NodeTag1251_ApplicationName_end = Encoding.Default.GetBytes("</BHS.3>");

    /// <summary>
    ///   The node tag 1251_ batch checksum_begin.
    /// </summary>
    public static readonly byte[] NodeTag1251_BatchChecksum_begin = Encoding.Default.GetBytes("<BTS.3>");

    /// <summary>
    ///   The node tag 1251_ batch checksum_end.
    /// </summary>
    public static readonly byte[] NodeTag1251_BatchChecksum_end = Encoding.Default.GetBytes("</BTS.3>");

    /// <summary>
    ///   The node tag 1251_ batch header_begin.
    /// </summary>
    public static readonly byte[] NodeTag1251_BatchHeader_begin = Encoding.Default.GetBytes("<BHS>");

    /// <summary>
    ///   The node tag 1251_ batch header_end.
    /// </summary>
    public static readonly byte[] NodeTag1251_BatchHeader_end = Encoding.Default.GetBytes("</BHS>");

    /// <summary>
    ///   The node tag 1251_ batch identifier_begin.
    /// </summary>
    public static readonly byte[] NodeTag1251_BatchIdentifier_begin = Encoding.Default.GetBytes("<BHS.11>");

    /// <summary>
    ///   The node tag 1251_ batch identifier_end.
    /// </summary>
    /// public
    public static readonly byte[] NodeTag1251_BatchIdentifier_end = Encoding.Default.GetBytes("</BHS.11>");

    /// <summary>
    ///   The node tag 1251_ batch trailer_begin.
    /// </summary>
    public static readonly byte[] NodeTag1251_BatchTrailer_begin = Encoding.Default.GetBytes("<BTS>");

    /// <summary>
    ///   The node tag 1251_ batch trailer_end.
    /// </summary>
    public static readonly byte[] NodeTag1251_BatchTrailer_end = Encoding.Default.GetBytes("</BTS>");

    /// <summary>
    ///   The node tag 1251_ h d_begin.
    /// </summary>
    public static readonly byte[] NodeTag1251_HD_begin = Encoding.Default.GetBytes("<HD.1>");

    /// <summary>
    ///   The node tag 1251_ h d_end.
    /// </summary>
    public static readonly byte[] NodeTag1251_HD_end = Encoding.Default.GetBytes("</HD.1>");

    /// <summary>
    ///   The node tag 1251_ organization name_begin.
    /// </summary>
    public static readonly byte[] NodeTag1251_OrganizationName_begin = Encoding.Default.GetBytes("<BHS.4>");

    /// <summary>
    ///   The node tag 1251_ organization name_end.
    /// </summary>
    public static readonly byte[] NodeTag1251_OrganizationName_end = Encoding.Default.GetBytes("</BHS.4>");

    /// <summary>
    ///   The type code_ center error.
    /// </summary>
    public static readonly string TypeCode_CenterError = "1.2.643.2.40.1.13.8.2";

    /// <summary>
    ///   The type code_ region 2 code.
    /// </summary>
    public static readonly string TypeCode_Region2Code = "1.2.643.2.40.3.3.1.0";

    /// <summary>
    ///   The type code_ region 5 code.
    /// </summary>
    public static readonly string TypeCode_Region5Code = "1.2.643.2.40.3.3.1";

    /// <summary>
    ///   The type code_ region error.
    /// </summary>
    public static readonly string TypeCode_RegionError = "1.2.643.2.40.1.13.8.1";

    /// <summary>
    ///   The type code_ xml logic error.
    /// </summary>
    public static readonly string TypeCode_XmlLogicError = "1.2.643.2.40.1.13.8.3";

    /// <summary>
    ///   The unspecified error app.
    /// </summary>
    public static readonly string UnspecifiedErrorAPP = "99";

    /// <summary>
    ///   The unspecified error iso.
    /// </summary>
    public static readonly string UnspecifiedErrorISO = "207";

    /// <summary>
    ///   The pre cache threads.
    /// </summary>
    private static readonly HashSet<Thread> preCacheThreads = new HashSet<Thread>();

    /// <summary>
    ///   The date time format.
    /// </summary>
    private static string dateFormat;

    /// <summary>
    ///   The date time format.
    /// </summary>
    private static string dateTimeFormat;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the date format.
    /// </summary>
    public static string DateFormat
    {
      get
      {
        if (dateFormat == null)
        {
          lock (typeof(Hl7Helper))
          {
            dateFormat = ConfigHelper.ReadConfigValue("Hl7_DateFormat", "yyyy-MM-dd");
          }
        }

        return dateFormat;
      }
    }

    /// <summary>
    ///   Gets the date time format.
    /// </summary>
    public static string DateTimeFormat
    {
      get
      {
        if (dateTimeFormat == null)
        {
          lock (typeof(Hl7Helper))
          {
            dateTimeFormat = ConfigHelper.ReadConfigValue("Hl7_DateTimeFormat", "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'zzz");
          }
        }

        return dateTimeFormat;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The add message reference.
    /// </summary>
    /// <param name="text">
    /// The text.
    /// </param>
    /// <param name="reference">
    /// The reference.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string AddMessageReference(string text, string reference)
    {
      if (string.IsNullOrEmpty(text))
      {
        return reference;
      }

      if (string.IsNullOrEmpty(reference))
      {
        return text;
      }

      return "{MSH.10=" + reference + "} " + text;
    }

    /// <summary>
    /// The find insurance segment.
    /// </summary>
    /// <param name="segments">
    /// The segments.
    /// </param>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <param name="allowNull">
    /// The allow null.
    /// </param>
    /// <returns>
    /// The <see cref="ADT_A01_INSURANCE"/>.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// </exception>
    public static ADT_A01_INSURANCE FindInsuranceSegment(
      IEnumerable<ADT_A01_INSURANCE> segments, 
      string id, 
      bool allowNull = false)
    {
      if (segments != null)
      {
        foreach (var adt_a_insurance in segments)
        {
          if (adt_a_insurance.In1.Id == id)
          {
            return adt_a_insurance;
          }
        }
      }

      if (!allowNull)
      {
        throw new InvalidDataException(string.Format("Не найден сегмент IN1 с порядковым номером {0}", id));
      }

      return null;
    }

    /// <summary>
    ///   The format current date time.
    /// </summary>
    /// <returns>
    ///   The <see cref="string" />.
    /// </returns>
    public static string FormatCurrentDateTime()
    {
      return FormatDateTime(DateTime.Now);
    }

    /// <summary>
    /// The format date.
    /// </summary>
    /// <param name="dt">
    /// The dt.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatDate(DateTime dt)
    {
      return dt.ToString(DateFormat);
    }

    /// <summary>
    /// The format date.
    /// </summary>
    /// <param name="dt">
    /// The dt.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatDate(DateTime? dt)
    {
      if (!dt.HasValue)
      {
        return null;
      }

      return FormatDate(dt.Value);
    }

    /// <summary>
    /// The format date time.
    /// </summary>
    /// <param name="dt">
    /// The dt.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatDateTime(DateTime dt)
    {
      return dt.ToString(DateTimeFormat);
    }

    /// <summary>
    /// The format date time.
    /// </summary>
    /// <param name="dt">
    /// The dt.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatDateTime(DateTime? dt)
    {
      if (!dt.HasValue)
      {
        return null;
      }

      return FormatDateTime(dt.Value);
    }

    /// <summary>
    /// The is severity fatal.
    /// </summary>
    /// <param name="severity">
    /// The severity.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsSeverityFatal(string severity)
    {
      return string.Compare(severity, FatalSeverityLevel, StringComparison.Ordinal) == 0;
    }

    /// <summary>
    ///   The pre cache serializers abort.
    /// </summary>
    public static void PreCacheSerializersAbort()
    {
      lock (preCacheThreads)
      {
        foreach (var thread in preCacheThreads)
        {
          thread.Abort();
        }
      }
    }

    /// <summary>
    /// The pre cache serializers start.
    /// </summary>
    /// <param name="targets">
    /// The targets.
    /// </param>
    [CLSCompliant(false)]
    public static void PreCacheSerializersStart(ProcessingTarget targets)
    {
      ThreadStart start = null;
      if (targets != ProcessingTarget.NoTargets)
      {
        if (start == null)
        {
          start = delegate
                  {
                    var fomsLogPrefix = FomsLogPrefix;
                    FomsLogger.WriteLog(
                                        LogType.Local, 
                                        string.Format("Предварительное кеширование классов сериализации: {0}", targets), 
                                        fomsLogPrefix, 
                                        LogErrorType.None);
                    try
                    {
                      if ((targets & ProcessingTarget.XElement) == ProcessingTarget.XElement)
                      {
                        new XmlSerializer(typeof(XElement));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.Ack))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.Ack))
                      {
                        new XmlSerializer(typeof(Ack));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.RSP_ZK1))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.RSP_ZK1))
                      {
                        new XmlSerializer(typeof(RSP_ZK1));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.RSP_ZK2))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.RSP_ZK2))
                      {
                        new XmlSerializer(typeof(RSP_ZK2));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.RSP_ZK4))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.RSP_ZK4))
                      {
                        new XmlSerializer(typeof(RSP_ZK4));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.RSP_ZK5))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.RSP_ZK5))
                      {
                        new XmlSerializer(typeof(RSP_ZK5));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.ZPI_ZA1))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.ZPI_ZA1))
                      {
                        new XmlSerializer(typeof(ZPI_ZA1));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.ZPI_ZA7))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.ZPI_ZA7))
                      {
                        new XmlSerializer(typeof(ZPI_ZA7));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.ADT_A01))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.ADT_A01))
                      {
                        new XmlSerializer(typeof(ADT_A01));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.ADT_A03))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.ADT_A03))
                      {
                        new XmlSerializer(typeof(ADT_A03));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.ADT_A24))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.ADT_A24))
                      {
                        new XmlSerializer(typeof(ADT_A24));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.ADT_A37))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.ADT_A37))
                      {
                        new XmlSerializer(typeof(ADT_A37));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.QBP_ZP1))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.QBP_ZP1))
                      {
                        new XmlSerializer(typeof(QBP_ZP1));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.QBP_ZP2))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.QBP_ZP2))
                      {
                        new XmlSerializer(typeof(QBP_ZP2));
                      }

                      if ((targets & (ProcessingTarget.NoTargets | ProcessingTarget.QBP_ZP4))
                          == (ProcessingTarget.NoTargets | ProcessingTarget.QBP_ZP4))
                      {
                        new XmlSerializer(typeof(QBP_ZP4));
                      }

                      if ((targets & ProcessingTarget.BHS) == ProcessingTarget.BHS)
                      {
                        new XmlSerializer(typeof(BHS));
                      }

                      if ((targets & ProcessingTarget.BTS) == ProcessingTarget.BTS)
                      {
                        new XmlSerializer(typeof(BTS));
                      }

                      if ((targets & ProcessingTarget.PersonErp) == ProcessingTarget.PersonErp)
                      {
                        new XmlSerializer(typeof(PersonErp));
                      }

                      if ((targets & ProcessingTarget.PersonCard) == ProcessingTarget.PersonCard)
                      {
                        new XmlSerializer(typeof(PersonCard));
                      }

                      var prefix = FomsLogPrefix;
                      FomsLogger.WriteLog(
                                          LogType.Local, 
                                          string.Format("Завершено кеширование классов сериализации: {0}", targets), 
                                          prefix, 
                                          LogErrorType.None);
                    }
                    catch (Exception exception)
                    {
                      var str3 = FomsLogPrefix;
                      FomsLogger.WriteError(
                                            LogType.Local, 
                                            string.Format(
                                                          "Исключение при кешировании классов сериализации: {0}", 
                                                          exception.Message), 
                                            str3);
                      throw;
                    }
                    finally
                    {
                      lock (preCacheThreads)
                      {
                        preCacheThreads.Remove(Thread.CurrentThread);
                      }
                    }
                  };
        }

        var item = new Thread(start);
        lock (preCacheThreads)
        {
          preCacheThreads.Add(item);
        }

        item.Start();
      }
    }

    #endregion
  }
}