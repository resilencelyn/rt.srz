// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParseDbf.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   парсер dbf
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.kladr
{
  using System;
  using System.Collections;
  using System.Collections.Concurrent;
  using System.IO;
  using System.Linq;
  using System.Runtime.InteropServices;
  using System.Text;
  using System.Xml;

  using rt.srz.model.srz;

  /// <summary>
  ///   парсер dbf
  /// </summary>
  public class ParseDbf
  {
    #region Public Methods and Operators

    /// <summary>
    /// Simple function to test is a string can be parsed. There may be a better way, but this works
    ///   If you port this to .NET 2.0, use the new TryParse methods instead of this
    /// </summary>
    /// <param name="numberString">
    /// The number String.
    /// </param>
    /// <returns>
    /// true if string can be parsed
    /// </returns>
    public static bool IsNumber(string numberString)
    {
      var numbers = numberString.ToCharArray();
      var numberCount = 0;
      var pointCount = 0;

      foreach (var number in numbers)
      {
        if (number >= 48 && number <= 57)
        {
          numberCount += 1;
        }
        else if (number == 46)
        {
          pointCount += 1;
        }
        else if (number == 32)
        {
        }
        else
        {
          return false;
        }
      }

      return numberCount > 0 && pointCount < 2;
    }

    /// <summary>
    /// Read an entire standard DBF file through tasks
    /// </summary>
    /// <param name="dbfFile">
    /// full file name
    /// </param>
    /// <param name="kladrCq">
    /// The Kladr CQ.
    /// </param>
    /// <returns>
    /// collection of Kladr objects
    /// </returns>
    public static int ReadDbf(string dbfFile, ConcurrentQueue<Kladr> kladrCq)
    {
      // If there isn't even a file, just return an empty DataTable
      if (false == File.Exists(dbfFile))
      {
        return -1;
      }

      BinaryReader br = null;
      var fieldIndex = 0;
      try
      {
        // Read the header into a buffer
        br = new BinaryReader(File.OpenRead(dbfFile));
        var buffer = br.ReadBytes(Marshal.SizeOf(typeof(DbfHeader)));

        // Marshall the header into a DbfHeader structure
        var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        var header = (DbfHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(DbfHeader));
        handle.Free();

        // Read in all the field descriptors. Per the spec, 13 (0D) marks the end of the field descriptors
        var fields = new ArrayList();
        while (13 != br.PeekChar())
        {
          buffer = br.ReadBytes(Marshal.SizeOf(typeof(FieldDescriptor)));
          handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
          fields.Add((FieldDescriptor)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(FieldDescriptor)));
          handle.Free();
        }

        var fieldsNames = fields.Cast<FieldDescriptor>()
                                .ToDictionary(
                                              field => field.FieldName, 
                                              field =>
                                              field.FieldName.Substring(0, 1).ToUpper()
                                              + field.FieldName.Substring(1).ToLower());

        // Read in the first row of records, we need this to help determine column types below
        br.BaseStream.Seek(header.HeaderLen + 1, SeekOrigin.Begin);
        br.ReadBytes(header.RecordLen);

        // Skip past the end of the header. 
        br.BaseStream.Seek(header.HeaderLen, SeekOrigin.Begin);

        // Read in all the records
        for (var counter = 0; counter <= header.NumRecords - 1; counter++)
        {
          // First we'll read the entire record into a buffer and then read each field from the buffer
          // This helps account for any extra space at the end of each record and probably performs better
          buffer = br.ReadBytes(header.RecordLen);
          var recReader = new BinaryReader(new MemoryStream(buffer));

          // All dbf field records begin with a deleted Flag field. Deleted - 0x2A (asterisk) else 0x20 (space)
          if (recReader.ReadChar() == '*')
          {
            continue;
          }

          // Loop through each field in a record
          // New XML
          var document = new XmlDocument();
          var element = document.CreateElement("Kladr"); // Path.GetFileNameWithoutExtension(dbfFile).ToUpper());
          document.AppendChild(element);

          foreach (FieldDescriptor field in fields)
          {
            string s;
            fieldsNames.TryGetValue(field.FieldName, out s);
            var element2 = document.CreateElement(s);
            element.AppendChild(element2);
            XmlText text;

            string number;
            switch (field.FieldType)
            {
              case 'N': // Number

                // If you port this to .NET 2.0, use the Decimal.TryParse method
                number = Encoding.GetEncoding(866).GetString(recReader.ReadBytes(field.FieldLen));
                text = IsNumber(number) ? document.CreateTextNode(number) : document.CreateTextNode("0");

                break;

              case 'C': // String                       
                text =
                  document.CreateTextNode(
                                          Encoding.GetEncoding(866)
                                                  .GetString(recReader.ReadBytes(field.FieldLen))
                                                  .TrimEnd(' '));
                element2.AppendChild(text);
                if (text.Length < 3)
                {
                  var attr = document.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                  attr.Value = "true";
                  element2.SetAttributeNode(attr);
                }

                break;

              case 'D': // Date (YYYYMMDD)
                var year = Encoding.GetEncoding(866).GetString(recReader.ReadBytes(4));
                var month = Encoding.GetEncoding(866).GetString(recReader.ReadBytes(2));
                var day = Encoding.GetEncoding(866).GetString(recReader.ReadBytes(2));
                try
                {
                  if (IsNumber(year) && IsNumber(month) && IsNumber(day) && int.Parse(year) > 1900)
                  {
                    var dt = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                    text = document.CreateTextNode(dt.ToString());
                  }
                }
                catch
                {
                }

                break;

              case 'T': // Timestamp, 8 bytes - two integers, first for date, second for time

                // Date is the number of days since 01/01/4713 BC (Julian Days)
                // Time is hours * 3600000L + minutes * 60000L + Seconds * 1000L (Milliseconds since midnight)
                var date = recReader.ReadInt32();
                var time = recReader.ReadInt32() * 10000L;
                text = document.CreateTextNode(JulianToDateTime(date).AddTicks(time).ToString());
                break;

              case 'L': // Boolean (Y/N)
                text = 'Y' == recReader.ReadByte() ? document.CreateTextNode("true") : document.CreateTextNode("false");

                break;

              case 'F':
                number = Encoding.GetEncoding(866).GetString(recReader.ReadBytes(field.FieldLen));
                text = IsNumber(number) ? document.CreateTextNode(number) : document.CreateTextNode("0.0F");

                break;
            }

            fieldIndex++;
          }

          var stream = new MemoryStream();
          document.Save(stream);
          stream.Position = 0;
          var kladr = Kladr.Deserializating(document);
          
          kladrCq.Enqueue(kladr);
          recReader.Close();
        }

        return header.NumRecords;
      }
      finally
      {
        if (null != br)
        {
          br.Close();
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Convert a Julian Date to a .NET DateTime structure
    ///   Implemented from pseudo code at http://en.wikipedia.org/wiki/Julian_day
    /// </summary>
    /// <param name="ljdn">
    /// Julian Date to convert (days since 01/01/4713 BC)
    /// </param>
    /// <returns>
    /// DateTime
    /// </returns>
    private static DateTime JulianToDateTime(long ljdn)
    {
      var p = Convert.ToDouble(ljdn);
      var s1 = p + 68569;
      var n = Math.Floor(4 * s1 / 146097);
      var s2 = s1 - Math.Floor(((146097 * n) + 3) / 4);
      var i = Math.Floor(4000 * (s2 + 1) / 1461001);
      var s3 = s2 - Math.Floor(1461 * i / 4) + 31;
      var q = Math.Floor(80 * s3 / 2447);
      var d = s3 - Math.Floor(2447 * q / 80);
      var s4 = Math.Floor(q / 11);
      var m = q + 2 - (12 * s4);
      var j = (100 * (n - 49)) + i + s4;
      return new DateTime(Convert.ToInt32(j), Convert.ToInt32(m), Convert.ToInt32(d));
    }

    #endregion

    /// <summary>
    ///   This is the file header for a DBF. We do this special layout with everything
    ///   packed so we can read straight from disk into the structure to populate it
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    internal struct DbfHeader
    {
      /// <summary>
      ///   The Version.
      /// </summary>
      internal readonly byte Version;

      /// <summary>
      ///   The update year.
      /// </summary>
      internal readonly byte UpdateYear;

      /// <summary>
      ///   The update month.
      /// </summary>
      internal readonly byte UpdateMonth;

      /// <summary>
      ///   The update day.
      /// </summary>
      internal readonly byte UpdateDay;

      /// <summary>
      ///   The num records.
      /// </summary>
      internal readonly int NumRecords;

      /// <summary>
      ///   The header len.
      /// </summary>
      internal readonly short HeaderLen;

      /// <summary>
      ///   The record len.
      /// </summary>
      internal readonly short RecordLen;

      /// <summary>
      ///   The reserved 1.
      /// </summary>
      internal readonly short Reserved1;

      /// <summary>
      ///   The incomplete trans.
      /// </summary>
      internal readonly byte IncompleteTrans;

      /// <summary>
      ///   The encryption Flag.
      /// </summary>
      internal readonly byte EncryptionFlag;

      /// <summary>
      ///   The reserved 2.
      /// </summary>
      internal readonly int Reserved2;

      /// <summary>
      ///   The reserved 3.
      /// </summary>
      internal readonly long Reserved3;

      /// <summary>
      ///   The mdx.
      /// </summary>
      internal readonly byte Mdx;

      /// <summary>
      ///   The Language.
      /// </summary>
      internal readonly byte Language;

      /// <summary>
      ///   The reserved 4.
      /// </summary>
      internal readonly short Reserved4;
    }

    /// <summary>
    ///   This is the field descriptor structure. There will be one of these for each column in the table.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    private struct FieldDescriptor
    {
      /// <summary>
      ///   The field name.
      /// </summary>
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
      public readonly string FieldName;

      /// <summary>
      ///   The field type.
      /// </summary>
      public readonly char FieldType;

      /// <summary>
      ///   The Address.
      /// </summary>
      public readonly int Address;

      /// <summary>
      ///   The field len.
      /// </summary>
      public readonly byte FieldLen;

      /// <summary>
      ///   The Count.
      /// </summary>
      public readonly byte Count;

      /// <summary>
      ///   The reserved 1.
      /// </summary>
      public readonly short Reserved1;

      /// <summary>
      ///   The work area.
      /// </summary>
      public readonly byte WorkArea;

      /// <summary>
      ///   The reserved 2.
      /// </summary>
      public readonly short Reserved2;

      /// <summary>
      ///   The Flag.
      /// </summary>
      public readonly byte Flag;

      /// <summary>
      ///   The reserved 3.
      /// </summary>
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
      public readonly byte[] Reserved3;

      /// <summary>
      ///   The index Flag.
      /// </summary>
      public readonly byte IndexFlag;
    }
  }
}