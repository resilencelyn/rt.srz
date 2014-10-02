﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HL7Writer.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The h l 7 writer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.configuration.algorithms.serialization
{
  #region references

  using System;
  using System.Collections;
  using System.IO;
  using System.Text;
  using System.Xml;
  using System.Xml.Linq;
  using System.Xml.Serialization;

  using rt.srz.model.HL7;
  using rt.srz.model.HL7.algorithms.DamienG;
  using rt.srz.model.HL7.commons;
  using rt.srz.model.HL7.commons.Enumerations;
  using rt.srz.model.HL7.commons.Interfaces;
  using rt.srz.model.HL7.enumerations;
  using rt.srz.model.HL7.person;

  #endregion

  /// <summary>
  ///   The h l 7 writer.
  /// </summary>
  public sealed class HL7Writer : IDisposable
  {
    #region Fields

    /// <summary>
    ///   The target file path.
    /// </summary>
    public readonly string TargetFilePath;

    /// <summary>
    ///   The batch file hash.
    /// </summary>
    private XElement batchFileHash;

    /// <summary>
    ///   The batch messages count.
    /// </summary>
    private XElement batchMessagesCount;

    /// <summary>
    ///   The batch trailer.
    /// </summary>
    private XElement batchTrailer;

    /// <summary>
    ///   The input stream.
    /// </summary>
    private Stream inputStream;

    /// <summary>
    ///   The is disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    ///   The messages written.
    /// </summary>
    private long messagesWritten;

    /// <summary>
    ///   The output stream.
    /// </summary>
    private FileStreamFoms outputStream;

    /// <summary>
    ///   The reader.
    /// </summary>
    private XmlReader reader;

    /// <summary>
    ///   The writer.
    /// </summary>
    private XmlWriter writer;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="HL7Writer"/> class.
    /// </summary>
    /// <param name="TargetFilePath">
    /// The target file path.
    /// </param>
    /// <param name="personObject">
    /// The person object.
    /// </param>
    public HL7Writer(string TargetFilePath, XElement personObject)
      : this(TargetFilePath)
    {
      reader = personObject.CreateReader();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HL7Writer"/> class.
    /// </summary>
    /// <param name="TargetFilePath">
    /// The target file path.
    /// </param>
    /// <param name="personObject">
    /// The person object.
    /// </param>
    /// <param name="resolver">
    /// The resolver.
    /// </param>
    public HL7Writer(string TargetFilePath, BasePersonTemplate personObject, IXmlNamespaceResolver resolver)
      : this(TargetFilePath)
    {
      inputStream = new MemoryStream();

      // XmlHelper.SerializeObject(this.inputStream, personObject, resolver);
      inputStream.Position = 0L;
      var reader = new XmlTextReader(inputStream) { WhitespaceHandling = WhitespaceHandling.None };
      this.reader = reader;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HL7Writer"/> class.
    /// </summary>
    /// <param name="TargerFilePath">
    /// The targer file path.
    /// </param>
    private HL7Writer(string TargerFilePath)
    {
      TargetFilePath = TargerFilePath;
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The write h l 7.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="segment">
    /// The segment.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool WriteHL7(XmlWriter writer, object segment)
    {
      if ((writer == null) || (segment == null))
      {
        return false;
      }

      var xmlNode = segment as XElement;
      if (xmlNode != null)
      {
        xmlNode.RemoveNamespaceAttributes();
        xmlNode.WriteTo(writer);
        return true;
      }

      var writer2 = segment as ISelfWriter<XmlWriter>;
      if (writer2 != null)
      {
        writer2.WriteTo(writer);
        return true;
      }

      new XmlSerializer(segment.GetType()).Serialize(writer, segment);
      return true;
    }

    /// <summary>
    ///   The dispose.
    /// </summary>
    public void Dispose()
    {
      var isFinalization = false;
      DoDispose(isFinalization);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    ///   The finalize write.
    /// </summary>
    public void FinalizeWrite()
    {
      EnsureWrite();
      DoClose();
    }

    /// <summary>
    /// The write message.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    public void WriteMessage(object message)
    {
      EnsureWrite();
      DoWriteMessage(message);
    }

    /// <summary>
    /// The write messages.
    /// </summary>
    /// <param name="messages">
    /// The messages.
    /// </param>
    public void WriteMessages(IEnumerable messages)
    {
      EnsureWrite();
      if (messages != null)
      {
        foreach (var obj2 in messages)
        {
          DoWriteMessage(obj2);
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    ///   The do close.
    /// </summary>
    private void DoClose()
    {
      try
      {
        DoFinalizeWrite();
      }
      catch
      {
      }

      DoCloseInput();
      DoCloseOutput();
    }

    /// <summary>
    ///   The do close input.
    /// </summary>
    private void DoCloseInput()
    {
      if (reader != null)
      {
        try
        {
          reader.Close();
        }
        catch
        {
        }

        reader = null;
      }

      if (inputStream != null)
      {
        try
        {
          inputStream.Close();
        }
        catch
        {
        }

        inputStream = null;
      }
    }

    /// <summary>
    ///   The do close output.
    /// </summary>
    private void DoCloseOutput()
    {
      if (writer != null)
      {
        try
        {
          writer.Close();
        }
        catch
        {
        }

        writer = null;
      }

      if (outputStream != null)
      {
        try
        {
          outputStream.Close();
        }
        catch
        {
        }

        outputStream = null;
      }
    }

    /// <summary>
    /// The do dispose.
    /// </summary>
    /// <param name="isFinalization">
    /// The is finalization.
    /// </param>
    private void DoDispose(bool isFinalization)
    {
      if (!isDisposed)
      {
        if (!isFinalization)
        {
          DoClose();
        }

        isDisposed = true;
      }
    }

    /// <summary>
    ///   The do finalize write.
    /// </summary>
    private void DoFinalizeWrite()
    {
      if ((writer != null) && (batchTrailer != null))
      {
        if (batchMessagesCount != null)
        {
          batchMessagesCount.Value = messagesWritten.ToString();
          batchMessagesCount = null;
        }

        if (batchFileHash != null)
        {
          writer.Flush();
          batchFileHash.Value = ConversionHelper.BytesToHexString(outputStream.DrawOutWriteHash());
          batchFileHash = null;
        }

        batchTrailer.WriteTo(writer);
        batchTrailer = null;
      }
    }

    /// <summary>
    /// The do write message.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    private void DoWriteMessage(object message)
    {
      try
      {
        if (WriteHL7(writer, message))
        {
          messagesWritten += 1L;
        }
      }
      catch (Exception exception)
      {
        var fomsLogPrefix = HL7Helper.FomsLogPrefix;
        FomsLogger.WriteError(LogType.Local, exception, fomsLogPrefix);
        throw;
      }
    }

    /// <summary>
    ///   The ensure write.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// </exception>
    private void EnsureWrite()
    {
      if (writer == null)
      {
        if (reader == null)
        {
          throw new InvalidOperationException("Недоступен базовый объект сериализации");
        }

        if (reader.MoveToContent() != XmlNodeType.Element)
        {
          throw new InvalidOperationException("Ошибка чтения базового объекта сериализации");
        }

        outputStream = new FileStreamFoms(TargetFilePath, FileMode.Create, FileAccess.Write, FileShare.Read);
        var settings = new XmlWriterSettings
                         {
                           Encoding = Encoding.Default, 
                           Indent = false, 
                           NewLineHandling = NewLineHandling.Replace, 
                           NewLineChars = string.Empty, 
                           OmitXmlDeclaration = false
                         };
        writer = XmlWriter.Create(outputStream, settings);
        writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
        if (reader.MoveToFirstAttribute())
        {
          do
          {
            if (string.Compare(reader.Name, "xmlns", StringComparison.Ordinal) != 0)
            {
              writer.WriteAttributeString(reader.Prefix, reader.LocalName, reader.NamespaceURI, reader.Value);
            }
          }
          while (reader.MoveToNextAttribute());
        }

        if (LookNearNode() != HL7Node.Header)
        {
          DoClose();
          throw new InvalidOperationException("Ошибка чтения корневого узла HL7");
        }

        var defattr = false;
        writer.WriteNode(reader, defattr);
        writer.Flush();
        outputStream.ResetHashWriteCalculator(new Crc32());
        while (LookNearNode() == HL7Node.Message)
        {
          var flag2 = false;
          writer.WriteNode(reader, flag2);
          messagesWritten += 1L;
        }

        if ((LookNearNode() != HL7Node.Trailer) || !LoadBatchTrailer())
        {
          DoClose();
          throw new InvalidOperationException("Ошибка чтения завершающего узла HL7");
        }

        DoCloseInput();
      }
    }

    /// <summary>
    ///   The load batch trailer.
    /// </summary>
    /// <returns>
    ///   The <see cref="bool" />.
    /// </returns>
    private bool LoadBatchTrailer()
    {
      if (reader != null)
      {
        batchTrailer = XNode.ReadFrom(reader) as XElement;
        if (batchTrailer != null)
        {
          for (var node = batchTrailer.FirstNode; node != null; node = node.NextNode)
          {
            var element = node as XElement;
            if (element != null)
            {
              if (string.Compare(element.Name.LocalName, "BTS.1", StringComparison.Ordinal) == 0)
              {
                batchMessagesCount = element;
              }
              else if (string.Compare(element.Name.LocalName, "BTS.3", StringComparison.Ordinal) == 0)
              {
                batchFileHash = element;
              }

              if ((batchMessagesCount != null) && (batchFileHash != null))
              {
                return true;
              }
            }
          }
        }
      }

      return false;
    }

    /// <summary>
    ///   The look near node.
    /// </summary>
    /// <returns>
    ///   The <see cref="HL7Node" />.
    /// </returns>
    private HL7Node LookNearNode()
    {
      if (reader != null)
      {
        do
        {
          if (reader.NodeType == XmlNodeType.Element)
          {
            return HL7NodeFromString(reader.LocalName);
          }
        }
        while (reader.Read());
        DoClose();
      }

      return HL7Node.Root;
    }

    public static HL7Node HL7NodeFromString(string node)
    {
      if (string.Compare(node, "BHS", StringComparison.Ordinal) == 0)
      {
        return HL7Node.Header;
      }

      if (string.Compare(node, "BTS", StringComparison.Ordinal) == 0)
      {
        return HL7Node.Trailer;
      }

      return HL7Node.Message;
    }

    #endregion
  }
}