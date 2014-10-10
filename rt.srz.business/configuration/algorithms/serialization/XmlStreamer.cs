// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlStreamer.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The xml streamer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.configuration.algorithms.serialization
{
  #region references

  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.IO;
  using System.Security.Cryptography;
  using System.Text;
  using System.Xml;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7;
  using rt.srz.model.Hl7.algorithms.DamienG;
  using rt.srz.model.Hl7.commons;
  using rt.srz.model.Hl7.commons.Enumerations;
  using rt.srz.model.Hl7.dotNetX;
  using rt.srz.model.Hl7.person;

  #endregion

  /// <summary>
  ///   The xml streamer.
  /// </summary>
  public static class XmlStreamer
  {
    #region Public Methods and Operators

    /// <summary>
    /// The calculate and check hash.
    /// </summary>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CalculateAndCheckHash(string filePath)
    {
      string str;
      string str2;
      return CalculateAndCheckHash(filePath, out str, out str2);
    }

    /// <summary>
    /// The calculate and check hash.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <param name="resetPosition">
    /// The reset position.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CalculateAndCheckHash(Stream stream, bool resetPosition = true)
    {
      string str;
      string str2;
      return CalculateAndCheckHash(stream, out str, out str2, resetPosition);
    }

    /// <summary>
    /// The calculate and check hash.
    /// </summary>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <param name="savedHash">
    /// The saved hash.
    /// </param>
    /// <param name="expectedHash">
    /// The expected hash.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CalculateAndCheckHash(string filePath, out string savedHash, out string expectedHash)
    {
      using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
      {
        var flag = DoCalculateAndCheckHash(stream, out savedHash, out expectedHash);
        savedHash = TStringHelper.StringToEmpty(savedHash).ToUpper();
        expectedHash = TStringHelper.StringToEmpty(expectedHash).ToUpper();
        return flag;
      }
    }

    /// <summary>
    /// The calculate and check hash.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <param name="savedHash">
    /// The saved hash.
    /// </param>
    /// <param name="expectedHash">
    /// The expected hash.
    /// </param>
    /// <param name="resetPosition">
    /// The reset position.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CalculateAndCheckHash(
      Stream stream,
      out string savedHash,
      out string expectedHash,
      bool resetPosition = true)
    {
      expectedHash = savedHash = null;
      var flag = DoResetPosition(stream, resetPosition)
                 && DoCalculateAndCheckHash(stream, out savedHash, out expectedHash);
      savedHash = TStringHelper.StringToEmpty(savedHash).ToUpper();
      expectedHash = TStringHelper.StringToEmpty(expectedHash).ToUpper();
      return flag;
    }

    /// <summary>
    /// The calculate and save hash.
    /// </summary>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CalculateAndSaveHash(string filePath)
    {
      string str;
      return CalculateAndSaveHash(filePath, out str);
    }

    /// <summary>
    /// The calculate and save hash.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <param name="resetPosition">
    /// The reset position.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CalculateAndSaveHash(Stream stream, bool resetPosition = true)
    {
      string str;
      return CalculateAndSaveHash(stream, out str, resetPosition);
    }

    /// <summary>
    /// The calculate and save hash.
    /// </summary>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <param name="hash">
    /// The hash.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CalculateAndSaveHash(string filePath, out string hash)
    {
      FileSystemPhysical.FileMakeWritable(filePath);
      using (var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
      {
        var flag = DoCalculateAndSaveHash(stream, out hash);
        hash = TStringHelper.StringToEmpty(hash).ToUpper();
        return flag;
      }
    }

    /// <summary>
    /// The calculate and save hash.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <param name="hash">
    /// The hash.
    /// </param>
    /// <param name="resetPosition">
    /// The reset position.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CalculateAndSaveHash(Stream stream, out string hash, bool resetPosition = true)
    {
      hash = string.Empty;
      var flag = DoResetPosition(stream, resetPosition) && DoCalculateAndSaveHash(stream, out hash);
      hash = hash.ToUpper();
      return flag;
    }

    /// <summary>
    /// The deserialize.
    /// </summary>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <typeparam name="T">
    /// Тип пакета
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T Deserialize<T>(string filePath) where T : class
    {
      T local;
      try
      {
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
          var serializer = new XmlSerializer(typeof(T));
          local = (T)serializer.Deserialize(stream);
        }
      }
      catch (Exception exception)
      {
        FomsLogger.WriteError(exception, null);
        local = default(T);
      }

      return local;
    }

    /// <summary>
    /// The deserialize list.
    /// </summary>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <typeparam name="T">
    /// Тип пакета
    /// </typeparam>
    /// <returns>
    /// The <see cref="List{T}"/>.
    /// </returns>
    public static List<T> DeserializeList<T>(string filePath)
    {
      return Deserialize<List<T>>(filePath);
    }

    /// <summary>
    /// The deserialize with hash.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <returns>
    /// The <see cref="BasePersonTemplate"/>.
    /// </returns>
    public static BasePersonTemplate DeserializeWithHash(Type type, string filePath)
    {
      try
      {
        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
          if (CalculateAndCheckHash(stream))
          {
            stream.Position = 0L;
            var serializer = new XmlSerializer(type);
            return (BasePersonTemplate)serializer.Deserialize(stream);
          }
        }
      }
      catch (Exception exception)
      {
        FomsLogger.WriteError(exception, null);
      }

      return null;
    }

    /// <summary>
    /// The scan identifier.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <param name="resetPosition">
    /// The reset position.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ScanIdentifier(Stream stream, bool resetPosition = true)
    {
      if (DoResetPosition(stream, resetPosition))
      {
        return DoScanIdentifier(stream);
      }

      return null;
    }

    /// <summary>
    /// The serialize.
    /// </summary>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <param name="xmlNamespaceResolver">
    /// The xml namespace resolver.
    /// </param>
    public static void Serialize(object obj, string filePath, IXmlNamespaceResolver xmlNamespaceResolver)
    {
      try
      {
        if ((obj != null) && !string.IsNullOrEmpty(filePath))
        {
          FileSystemPhysical.RemoveFile(filePath);
          using (var stream = new FileStream(filePath, FileMode.Create))
          {
            using (var writer = new XmlTextWriter(stream, Encoding.Default))
            {
              //// XmlHelper.SerializeObject(writer, obj, xmlNamespaceResolver);
            }
          }
        }
      }
      catch (Exception exception)
      {
        FomsLogger.WriteError(exception, null);
      }
    }

    /// <summary>
    /// The serialize list.
    /// </summary>
    /// <param name="list">
    /// The list.
    /// </param>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <param name="xmlNamespaceResolver">
    /// The xml namespace resolver.
    /// </param>
    /// <typeparam name="T">
    /// Тип пакета
    /// </typeparam>
    public static void SerializeList<T>(IList<T> list, string filePath, IXmlNamespaceResolver xmlNamespaceResolver)
    {
      Serialize(list, filePath, xmlNamespaceResolver);
    }

    /// <summary>
    /// The serialize with hash.
    /// </summary>
    /// <param name="person">
    /// The person.
    /// </param>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <param name="xmlNamespaceResolver">
    /// The xml namespace resolver.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool SerializeWithHash(object person, string filePath, IXmlNamespaceResolver xmlNamespaceResolver)
    {
      try
      {
        FileSystemPhysical.RemoveFile(filePath);
        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
        {
          using (var writer = new XmlTextWriter(stream, Encoding.Default))
          {
            // XmlHelper.SerializeObject(writer, person, xmlNamespaceResolver);
            if (!CalculateAndSaveHash(stream, true))
            {
              throw new InvalidOperationException(string.Format("Не удалось прописать контрольную сумму: ", filePath));
            }
          }
        }

        return true;
      }
      catch (Exception exception)
      {
        FomsLogger.WriteError(exception, null);
        return false;
      }
    }

    /// <summary>
    /// The serialize with hash.
    /// </summary>
    /// <param name="person">
    /// The person.
    /// </param>
    /// <param name="messages">
    /// The messages.
    /// </param>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <param name="xmlNamespaceResolver">
    /// The xml namespace resolver.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool SerializeWithHash(
      BasePersonTemplate person,
      IEnumerable messages,
      string filePath,
      IXmlNamespaceResolver xmlNamespaceResolver)
    {
      try
      {
        var fomsLogPrefix = Hl7Helper.FomsLogPrefix;
        FomsLogger.WriteLog(
                            LogType.Local,
                            "Сериализация пакета Hl7. " + person.BeginPacket.RetrieveShortInfo(),
                            fomsLogPrefix,
                            LogErrorType.None);
        FileSystemPhysical.RemoveFile(filePath);
        using (var writer = new Hl7Writer(filePath, person, xmlNamespaceResolver))
        {
          writer.WriteMessages(messages);
          writer.FinalizeWrite();
        }

        return true;
      }
      catch (Exception exception)
      {
        FomsLogger.WriteError(exception, null);
        return false;
      }
    }

    /// <summary>
    /// The serialize with hash.
    /// </summary>
    /// <param name="person">
    /// The person.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <param name="xmlNamespaceResolver">
    /// The xml namespace resolver.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool SerializeWithHash(
      BasePersonTemplate person,
      object message,
      string filePath,
      IXmlNamespaceResolver xmlNamespaceResolver)
    {
      return SerializeWithHash(person, new[] { message }, filePath, xmlNamespaceResolver);
    }

    #endregion

    #region Methods

    /// <summary>
    /// The deserialize.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <returns>
    /// The <see cref="BasePersonTemplate"/>.
    /// </returns>
    internal static BasePersonTemplate Deserialize(Type type, string filePath)
    {
      using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
      {
        var serializer = new XmlSerializer(type);
        return (BasePersonTemplate)serializer.Deserialize(stream);
      }
    }

    /// <summary>
    /// The do calculate and check hash.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    internal static bool DoCalculateAndCheckHash(Stream stream)
    {
      string str;
      string str2;
      return DoCalculateAndCheckHash(stream, out str, out str2);
    }

    /// <summary>
    /// The do calculate and check hash.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <param name="savedHash">
    /// The saved hash.
    /// </param>
    /// <param name="expectedHash">
    /// The expected hash.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    internal static bool DoCalculateAndCheckHash(Stream stream, out string savedHash, out string expectedHash)
    {
      expectedHash = DoScanHash(stream);
      if (expectedHash != null)
      {
        var builder = new StringBuilder();
        var textStorage = builder;
        if (DoLocateSequence(stream, Hl7Helper.NodeTag1251_BatchChecksum_end, null, textStorage))
        {
          savedHash = builder.ToString();
          return string.Compare(expectedHash, savedHash, StringComparison.OrdinalIgnoreCase) == 0;
        }
      }

      savedHash = null;
      return false;
    }

    /// <summary>
    /// The do calculate and save hash.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    internal static bool DoCalculateAndSaveHash(Stream stream)
    {
      string str;
      return DoCalculateAndSaveHash(stream, out str);
    }

    /// <summary>
    /// The do calculate and save hash.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <param name="hash">
    /// The hash.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    internal static bool DoCalculateAndSaveHash(Stream stream, out string hash)
    {
      hash = DoScanHash(stream);
      if (hash == null)
      {
        return false;
      }

      var bytes = Encoding.Default.GetBytes(hash);
      if (stream.CanSeek)
      {
        var position = stream.Position;
        if (!DoLocateSequence(stream, Hl7Helper.NodeTag1251_BatchChecksum_end, null, null))
        {
          return false;
        }

        var num2 = (stream.Position - position) - Hl7Helper.NodeTag1251_BatchChecksum_end.LongLength;
        if (num2 != bytes.LongLength)
        {
          var fomsLogPrefix = Hl7Helper.FomsLogPrefix;
          FomsLogger.WriteError(
                                LogType.Local,
                                string.Format("Под контрольную сумму выделено {0} байт; ожидается {1} байт.", num2, bytes.LongLength),
                                fomsLogPrefix);
          return false;
        }

        stream.Position = position;
      }

      stream.Write(bytes, 0, bytes.Length);
      return true;
    }

    /// <summary>
    /// The do scan application name.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    internal static string DoScanApplicationName(Stream stream)
    {
      return DoScanTaggedText(
                              stream,
                              Hl7Helper.NodeTag1251_ApplicationName_begin,
                              Hl7Helper.NodeTag1251_ApplicationName_end,
                              Hl7Helper.NodeTag1251_HD_begin,
                              Hl7Helper.NodeTag1251_HD_end);
    }

    /// <summary>
    /// The do scan identifier.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    internal static string DoScanIdentifier(Stream stream)
    {
      return DoScanTaggedText(
                              stream,
                              Hl7Helper.NodeTag1251_BatchIdentifier_begin,
                              Hl7Helper.NodeTag1251_BatchIdentifier_end);
    }

    /// <summary>
    /// The do scan organization name.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    internal static string DoScanOrganizationName(Stream stream)
    {
      return DoScanTaggedText(
                              stream,
                              Hl7Helper.NodeTag1251_OrganizationName_begin,
                              Hl7Helper.NodeTag1251_OrganizationName_end,
                              Hl7Helper.NodeTag1251_HD_begin,
                              Hl7Helper.NodeTag1251_HD_end);
    }

    /// <summary>
    /// The do locate sequence.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <param name="sequence">
    /// The sequence.
    /// </param>
    /// <param name="hashCalculator">
    /// The hash calculator.
    /// </param>
    /// <param name="textStorage">
    /// The text storage.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool DoLocateSequence(
      Stream stream,
      byte[] sequence,
      HashAlgorithm hashCalculator,
      StringBuilder textStorage)
    {
      if (stream == null)
      {
        return false;
      }

      var position = stream.Position;
      if (position >= stream.Length)
      {
        return false;
      }

      var length = sequence.Length;
      if (length < 1)
      {
        return false;
      }

      var inputBuffer = (hashCalculator != null) ? new byte[1] : null;
      var bytes = (textStorage != null) ? new byte[1] : null;
      var index = 0;
      while (true)
      {
        var num4 = stream.ReadByte();
        if (num4 < 0)
        {
          if (stream.CanSeek)
          {
            stream.Position = position;
          }

          return false;
        }

        var num5 = (byte)num4;
        if (num5 == sequence[index])
        {
          if (++index >= length)
          {
            if (hashCalculator != null)
            {
              hashCalculator.TransformFinalBlock(inputBuffer, 0, 0);
            }

            return true;
          }
        }
        else
        {
          if (index > 0)
          {
            if (hashCalculator != null)
            {
              hashCalculator.TransformBlock(sequence, 0, index, sequence, 0);
            }

            if (textStorage != null)
            {
              textStorage.Append(Encoding.Default.GetString(sequence, 0, index));
            }

            index = 0;
          }

          if (hashCalculator != null)
          {
            inputBuffer[0] = num5;
            hashCalculator.TransformBlock(inputBuffer, 0, 1, inputBuffer, 0);
          }

          if (textStorage != null)
          {
            bytes[0] = num5;
            textStorage.Append(Encoding.Default.GetString(bytes));
          }
        }
      }
    }

    /// <summary>
    /// The do reset position.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <param name="resetPosition">
    /// The reset position.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool DoResetPosition(Stream stream, bool resetPosition)
    {
      if (!resetPosition)
      {
        return true;
      }

      if ((stream != null) && stream.CanSeek)
      {
        stream.Position = 0L;
        return true;
      }

      return false;
    }

    /// <summary>
    /// The do scan hash.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string DoScanHash(Stream stream)
    {
      if (((stream != null) && stream.CanRead)
          && DoLocateSequence(stream, Hl7Helper.NodeTag1251_BatchHeader_end, null, null))
      {
        HashAlgorithm algorithm = new Crc32();
        var hashCalculator = algorithm;
        if (DoLocateSequence(stream, Hl7Helper.NodeTag1251_BatchTrailer_begin, hashCalculator, null)
            && DoLocateSequence(stream, Hl7Helper.NodeTag1251_BatchChecksum_begin, null, null))
        {
          return ConversionHelper.BytesToHexString(algorithm.Hash);
        }
      }

      return null;
    }

    /// <summary>
    /// The do scan tagged text.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <param name="beginTag">
    /// The begin tag.
    /// </param>
    /// <param name="endTag">
    /// The end tag.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string DoScanTaggedText(Stream stream, byte[] beginTag, byte[] endTag)
    {
      byte[] beginSubTag = null;
      byte[] endSubTag = null;
      return DoScanTaggedText(stream, beginTag, endTag, beginSubTag, endSubTag);
    }

    /// <summary>
    /// The do scan tagged text.
    /// </summary>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <param name="beginTag">
    /// The begin tag.
    /// </param>
    /// <param name="endTag">
    /// The end tag.
    /// </param>
    /// <param name="beginSubTag">
    /// The begin sub tag.
    /// </param>
    /// <param name="endSubTag">
    /// The end sub tag.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string DoScanTaggedText(
      Stream stream,
      byte[] beginTag,
      byte[] endTag,
      byte[] beginSubTag,
      byte[] endSubTag)
    {
      if ((((stream != null) && stream.CanRead) && DoLocateSequence(stream, beginTag, null, null))
          && ((beginSubTag == null) || DoLocateSequence(stream, beginSubTag, null, null)))
      {
        var builder = new StringBuilder();
        var textStorage = builder;
        if (DoLocateSequence(stream, (endSubTag != null) ? endSubTag : endTag, null, textStorage)
            && ((endSubTag == null) || DoLocateSequence(stream, endTag, null, null)))
        {
          return builder.ToString();
        }
      }

      return null;
    }

    #endregion
  }
}