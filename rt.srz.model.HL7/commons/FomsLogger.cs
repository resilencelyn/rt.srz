// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FomsLogger.cs" company="–усЅ»“ех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The foms logger.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.commons
{
  #region references

  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Text;

  using rt.srz.model.Hl7.commons.Enumerations;
  using rt.srz.model.Hl7.dotNetX;

  #endregion

  /// <summary>
  ///   The foms logger.
  /// </summary>
  public class FomsLogger
  {
    #region Static Fields

    /// <summary>
    ///   The default log type.
    /// </summary>
    public static LogType DefaultLogType = LogType.All;

    /// <summary>
    ///   The default params prefix.
    /// </summary>
    public static string DefaultParamsPrefix = "[config] ";

    /// <summary>
    ///   The file log.
    /// </summary>
    public static FileStream FileLog = null;

    /// <summary>
    ///   The os log name.
    /// </summary>
    public static string OsLogName = "Application";

    /// <summary>
    ///   The os log source name.
    /// </summary>
    public static string OsLogSourceName = null;

    /// <summary>
    ///   The current.
    /// </summary>
    private static readonly FomsLogger current = null;

    #endregion

    #region Fields

    /// <summary>
    ///   The tracer.
    /// </summary>
    private TraceListener tracer;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FomsLogger"/> class.
    /// </summary>
    /// <param name="traceLogPath">
    /// The trace log path.
    /// </param>
    /// <param name="traceLogFolderCreate">
    /// The trace log folder create.
    /// </param>
    protected FomsLogger(string traceLogPath = "", bool traceLogFolderCreate = false)
    {
      if (!string.IsNullOrEmpty(traceLogPath))
      {
        ChangeTracer(new FomsTracer(traceLogPath, traceLogFolderCreate));
      }
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the current.
    /// </summary>
    public static FomsLogger Current
    {
      get
      {
        return current;
      }
    }

    /// <summary>
    ///   Gets the current tracer.
    /// </summary>
    public static TraceListener CurrentTracer
    {
      get
      {
        if (current == null)
        {
          return null;
        }

        return current.tracer;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The join prefix.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string JoinPrefix(string message, string prefix)
    {
      string delimiter = null;
      return TStringHelper.CombineStrings(prefix, message, delimiter);
    }

    /// <summary>
    /// The log config param.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="writeNull">
    /// The write null.
    /// </param>
    public static void LogConfigParam(string name, bool writeNull = true)
    {
      LogConfigParam(name, DefaultParamsPrefix, writeNull);
    }

    /// <summary>
    /// The log config param.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="writeNull">
    /// The write null.
    /// </param>
    public static void LogConfigParam(string name, string prefix, bool writeNull = true)
    {
      string str = null;
      if (!ConfigHelper.ReadConfigValue(name, ref str) && writeNull)
      {
        str = "<absent>";
      }

      if (!string.IsNullOrEmpty(str) || writeNull)
      {
        LogParam(name, str, prefix, writeNull);
      }
    }

    /// <summary>
    /// The log param.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="writeNull">
    /// The write null.
    /// </param>
    public static void LogParam(string name, object value, bool writeNull = true)
    {
      LogParam(name, value, DefaultParamsPrefix, writeNull);
    }

    /// <summary>
    /// The log param.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="writeNull">
    /// The write null.
    /// </param>
    public static void LogParam(string name, object value, string prefix, bool writeNull = true)
    {
      if ((value != null) || writeNull)
      {
        WriteLog(
                 LogType.Local, 
                 string.Format("{0}: {1}", name, (value != null) ? value.ToString() : "<NULL>"), 
                 prefix, 
                 LogErrorType.None);
      }
    }

    // protected void MakeCurrent(Assembly traceExecutingAssemblyInfo = System.ref)
    // {
    // current = this;
    // if (traceExecutingAssemblyInfo != null)
    // {
    // FomsTracer tracer = this.tracer as FomsTracer;
    // if (tracer != null)
    // {
    // tracer.WriteAssemblyInfo(traceExecutingAssemblyInfo, null);
    // }
    // }
    // }

    /// <summary>
    /// The write error.
    /// </summary>
    /// <param name="x">
    /// The x.
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    public static void WriteError(Exception x, string prefix = "")
    {
      WriteError(DefaultLogType, x, prefix);
    }

    /// <summary>
    /// The write error.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    public static void WriteError(string message, string prefix = "")
    {
      WriteError(DefaultLogType, message, prefix);
    }

    /// <summary>
    /// The write error.
    /// </summary>
    /// <param name="logType">
    /// The log type.
    /// </param>
    /// <param name="x">
    /// The x.
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    public static void WriteError(LogType logType, Exception x, string prefix = "")
    {
      var message = x.ToString();
      var str2 = prefix;
      var exception = LogErrorType.Exception;
      WriteLog(logType, message, str2, exception);
    }

    /// <summary>
    /// The write error.
    /// </summary>
    /// <param name="logType">
    /// The log type.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    public static void WriteError(LogType logType, string message, string prefix = "")
    {
      WriteLog(logType, message, prefix, LogErrorType.Error);
    }

    /// <summary>
    /// The write log.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="errorType">
    /// The error type.
    /// </param>
    public static void WriteLog(string message, LogErrorType errorType = 0)
    {
      string prefix = null;
      var type = errorType;
      WriteLog(message, prefix, type);
    }

    /// <summary>
    /// The write log.
    /// </summary>
    /// <param name="logType">
    /// The log type.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="errorType">
    /// The error type.
    /// </param>
    public static void WriteLog(LogType logType, string message, LogErrorType errorType = 0)
    {
      string prefix = null;
      var type = errorType;
      WriteLog(logType, message, prefix, type);
    }

    /// <summary>
    /// The write log.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="errorType">
    /// The error type.
    /// </param>
    public static void WriteLog(string message, string prefix, LogErrorType errorType = 0)
    {
      WriteLog(DefaultLogType, message, prefix, errorType);
    }

    /// <summary>
    /// The write log.
    /// </summary>
    /// <param name="logType">
    /// The log type.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="errorType">
    /// The error type.
    /// </param>
    public static void WriteLog(LogType logType, string message, string prefix, LogErrorType errorType = 0)
    {
      if (current != null)
      {
        current.LogMessage(logType, message, prefix, errorType);
      }
    }

    /// <summary>
    /// The change tracer.
    /// </summary>
    /// <param name="tracer">
    /// The tracer.
    /// </param>
    public void ChangeTracer(TraceListener tracer)
    {
      if (!Equals(this.tracer, tracer))
      {
        if (this.tracer != null)
        {
          Trace.Listeners.Remove(this.tracer);
          this.tracer = null;
        }

        if (tracer != null)
        {
          if (!string.IsNullOrEmpty(tracer.Name))
          {
            Trace.Listeners.Remove(tracer.Name);
          }

          Trace.Listeners.Add(tracer);
          this.tracer = tracer;
        }
      }
    }

    /// <summary>
    /// The log message.
    /// </summary>
    /// <param name="logType">
    /// The log type.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="errorType">
    /// The error type.
    /// </param>
    public void LogMessage(LogType logType, string message, string prefix = "", LogErrorType errorType = 0)
    {
      message = JoinPrefix(message, prefix);
      if (!string.IsNullOrEmpty(message))
      {
        if ((logType & LogType.Console) == LogType.Console)
        {
          LogMessage_Console(message, errorType);
        }

        if ((logType & LogType.Trace) == LogType.Trace)
        {
          LogMessage_Trace(message, errorType);
        }

        if ((logType & LogType.OsLog) == LogType.OsLog)
        {
          LogMessage_OsLog(message, errorType);
        }

        if ((logType & LogType.Memo) == LogType.Memo)
        {
          LogMessage_Memo(message, errorType);
        }

        if ((logType & LogType.File) == LogType.File)
        {
          LogMessage_File(message, errorType);
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The join error prefix.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="errorType">
    /// The error type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    protected string JoinErrorPrefix(string message, LogErrorType errorType)
    {
      switch (errorType)
      {
        case LogErrorType.Error:
        {
          var prefix = "[Error] ";
          message = JoinPrefix(message, prefix);
          return message;
        }

        case LogErrorType.Exception:
        {
          var str2 = "[Exception] ";
          message = JoinPrefix(message, str2);
          return message;
        }
      }

      return message;
    }

    /// <summary>
    /// The log message_ console.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="errorType">
    /// The error type.
    /// </param>
    protected virtual void LogMessage_Console(string message, LogErrorType errorType)
    {
    }

    /// <summary>
    /// The log message_ file.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="errorType">
    /// The error type.
    /// </param>
    protected virtual void LogMessage_File(string message, LogErrorType errorType)
    {
      try
      {
        if (FileLog != null)
        {
          var bytes = Encoding.Unicode.GetBytes(JoinErrorPrefix(message, errorType) + Environment.NewLine);
          FileLog.Write(bytes, 0, bytes.Length);
          FileLog.Flush();
        }
      }
      catch (Exception exception)
      {
        WriteError(LogType.Memo | LogType.Console, exception, "»сключение при попытке записи в журнальный файл");
      }
    }

    /// <summary>
    /// The log message_ memo.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="errorType">
    /// The error type.
    /// </param>
    protected virtual void LogMessage_Memo(string message, LogErrorType errorType)
    {
    }

    /// <summary>
    /// The log message_ os log.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="errorType">
    /// The error type.
    /// </param>
    protected virtual void LogMessage_OsLog(string message, LogErrorType errorType)
    {
      if (!string.IsNullOrEmpty(OsLogName) && !string.IsNullOrEmpty(OsLogSourceName))
      {
        try
        {
          EventLogEntryType error;
          if (!EventLog.SourceExists(OsLogSourceName))
          {
            EventLog.CreateEventSource(OsLogSourceName, OsLogName);
          }

          switch (errorType)
          {
            case LogErrorType.Error:
            case LogErrorType.Exception:
              error = EventLogEntryType.Error;
              break;

            default:
              error = EventLogEntryType.Information;
              break;
          }

          EventLog.WriteEntry(OsLogSourceName, message, error);
        }
        catch (Exception exception)
        {
          WriteError(LogType.Local, exception, "»сключение при попытке записи в системный журнал");
        }
      }
    }

    /// <summary>
    /// The log message_ trace.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="errorType">
    /// The error type.
    /// </param>
    protected virtual void LogMessage_Trace(string message, LogErrorType errorType)
    {
      try
      {
        Trace.WriteLine(JoinErrorPrefix(message, errorType));
      }
      catch (Exception exception)
      {
        WriteError(
                   LogType.File | LogType.Memo | LogType.Console, 
                   exception, 
                   "»сключение при попытке записи в трассировщик");
      }
    }

    #endregion
  }
}