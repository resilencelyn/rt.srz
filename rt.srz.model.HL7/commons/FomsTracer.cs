// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FomsTracer.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The foms tracer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.commons
{
  #region references

  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Reflection;

  #endregion

  /// <summary>
  ///   The foms tracer.
  /// </summary>
  public sealed class FomsTracer : TraceListener
  {
    #region Static Fields

    /// <summary>
    ///   The defaul file reduction min.
    /// </summary>
    public static readonly long DefaulFileReductionMin;

    /// <summary>
    ///   The default file growth max.
    /// </summary>
    public static readonly long DefaultFileGrowthMax;

    #endregion

    #region Fields

    /// <summary>
    ///   The allow create folder.
    /// </summary>
    private readonly bool allowCreateFolder;

    /// <summary>
    ///   The trace file path.
    /// </summary>
    private readonly string traceFilePath;

    /// <summary>
    ///   The trace folder path.
    /// </summary>
    private readonly string traceFolderPath;

    /// <summary>
    ///   The new line prefix.
    /// </summary>
    private string newLinePrefix;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes static members of the <see cref="FomsTracer" /> class.
    /// </summary>
    static FomsTracer()
    {
      var min = 0L;
      var max = 0x1e8480L;
      DefaultFileGrowthMax = 0x400L
                             * ConfigHelper.ReadConfigValue("System_LogFileGrowth", 0xc00L, min, max, new long[0]);
      var num3 = 0L;
      var num4 = 0x1e8480L;
      DefaulFileReductionMin = 0x400L
                               * ConfigHelper.ReadConfigValue("System_LogFileShrink", 0x800L, num3, num4, new long[0]);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FomsTracer"/> class.
    /// </summary>
    /// <param name="traceFilePath">
    /// The trace file path.
    /// </param>
    /// <param name="allowCreateFolder">
    /// The allow create folder.
    /// </param>
    public FomsTracer(string traceFilePath, bool allowCreateFolder = false)
      : base("Default")
    {
      FileGrowthMax = DefaultFileGrowthMax;
      FileReductionMin = DefaulFileReductionMin;
      newLinePrefix = ConversionHelper.DateTimeFormat + "' '";
      this.traceFilePath = traceFilePath;
      traceFolderPath = Path.GetDirectoryName(traceFilePath);
      this.allowCreateFolder = allowCreateFolder;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the file growth max.
    /// </summary>
    public long FileGrowthMax { get; set; }

    /// <summary>
    ///   Gets or sets the file reduction min.
    /// </summary>
    public long FileReductionMin { get; set; }

    /// <summary>
    ///   Gets or sets the new line prefix.
    /// </summary>
    public string NewLinePrefix
    {
      get
      {
        return newLinePrefix;
      }

      set
      {
        newLinePrefix = value;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The get assembly info.
    /// </summary>
    /// <param name="assembly">
    /// The assembly.
    /// </param>
    /// <param name="lineSeparator">
    /// The line separator.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetAssemblyInfo(Assembly assembly, string lineSeparator = "")
    {
      if (assembly == null)
      {
        return Environment.CommandLine + lineSeparator + "<EntryAssembly = null>";
      }

      if (lineSeparator == null)
      {
        lineSeparator = Environment.NewLine;
      }

      return Environment.CommandLine + lineSeparator + assembly.FullName + lineSeparator + "ImageRuntimeVersion: "
             + assembly.ImageRuntimeVersion;
    }

    // private Stream OpenLogStream()
    // {
    // Stream stream;
    // try
    // {
    // if (!Directory.Exists(this.traceFolderPath))
    // {
    // if (!this.allowCreateFolder)
    // {
    // return null;
    // }
    // Directory.CreateDirectory(this.traceFolderPath);
    // }
    // stream = LogFileStream.MakeLogFileStream(this.traceFilePath, 2, this.FileGrowthMax, this.FileReductionMin, SeekOrigin.End);
    // }
    // catch
    // {
    // }
    // return stream;
    // }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="o">
    /// The o.
    /// </param>
    public override void Write(object o)
    {
      Write(o.ToString());
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    public override void Write(string message)
    {
      // bool writeLine = false;
      // this.WriteLog(message, writeLine);
    }

    /// <summary>
    /// The write assembly info.
    /// </summary>
    /// <param name="assembly">
    /// The assembly.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    public void WriteAssemblyInfo(Assembly assembly, string format = "")
    {
      var newLine = Environment.NewLine;
      if (format == null)
      {
        format = newLine + newLine + "-->-" + newLine + "{0}" + newLine + "-<--" + newLine + newLine;
      }

      Write(string.Format(format, GetAssemblyInfo(assembly, newLine)));
    }

    /// <summary>
    /// The write line.
    /// </summary>
    /// <param name="o">
    /// The o.
    /// </param>
    public override void WriteLine(object o)
    {
      WriteLine(o.ToString());
    }

    /// <summary>
    /// The write line.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    public override void WriteLine(string message)
    {
      // bool writeLine = true;
      // this.WriteLog(message, writeLine);
    }

    #endregion

    #region Methods

    /// <summary>
    ///   The write indent.
    /// </summary>
    protected override void WriteIndent()
    {
    }

    /// <summary>
    /// The convert utc to offset.
    /// </summary>
    /// <param name="DateTime">
    /// The date time.
    /// </param>
    /// <returns>
    /// The <see cref="DateTimeOffset"/>.
    /// </returns>
    private static DateTimeOffset ConvertUtcToOffset(DateTime DateTime)
    {
      return new DateTimeOffset(DateTime);
    }

    /// <summary>
    /// The prepare message.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="dateTimeOffset">
    /// The date time offset.
    /// </param>
    /// <param name="writeLine">
    /// The write line.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string PrepareMessage(string message, DateTimeOffset dateTimeOffset, bool writeLine)
    {
      if (writeLine)
      {
        message = dateTimeOffset.ToString(newLinePrefix) + message + Environment.NewLine;
      }

      return message;
    }

    #endregion

    // private void WriteLog(string message, bool writeLine)
    // {
    // DateTime utcNow = DateTime.UtcNow;
    // Stream stream = this.OpenLogStream();
    // if (stream != null)
    // {
    // try
    // {
    // byte[] bytes = Encoding.Unicode.GetBytes(this.PrepareMessage(message, ConvertUtcToOffset(utcNow), writeLine));
    // stream.Write(bytes, 0, bytes.Length);
    // }
    // finally
    // {
    // stream.Dispose();
    // }
    // }
    // }
  }
}