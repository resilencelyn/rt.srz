// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParseDbf.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.kladr
{
  using System;
  using System.Collections;
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.Data;
  using System.IO;
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
    ///   *Thanks to wu.qingman on code project for fixing a bug in this for me
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
      var number_count = 0;
      var point_count = 0;
      var space_count = 0;

      foreach (var number in numbers)
      {
        if (number >= 48 && number <= 57)
        {
          number_count += 1;
        }
        else if (number == 46)
        {
          point_count += 1;
        }
        else if (number == 32)
        {
          space_count += 1;
        }
        else
        {
          return false;
        }
      }

      return number_count > 0 && point_count < 2;
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
      string year;
      string month;
      string day;
      long lTime;
      int fieldIndex;

      // If there isn't even a file, just return an empty DataTable
      if (false == File.Exists(dbfFile))
      {
        return -1;
      }

      BinaryReader br = null;
      try
      {
        // Read the header into a buffer
        br = new BinaryReader(File.OpenRead(dbfFile));
        var buffer = br.ReadBytes(Marshal.SizeOf(typeof(DBFHeader)));

        // Marshall the header into a DBFHeader structure
        var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        var header = (DBFHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(DBFHeader));
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



        var fieldsNames = new Dictionary<string, string>();
        foreach (FieldDescriptor field in fields)
        {
          fieldsNames.Add(
            field.fieldName,
            field.fieldName.Substring(0, 1).ToUpper() + field.fieldName.Substring(1).ToLower());
        }



        // Read in the first row of records, we need this to help determine column types below
        br.BaseStream.Seek(header.headerLen + 1, SeekOrigin.Begin);
        buffer = br.ReadBytes(header.recordLen);
        var recReader = new BinaryReader(new MemoryStream(buffer));

        // Create the columns in our new DataTable
        #region create columns

        DataColumn col = null;
        string number;
        foreach (FieldDescriptor field in fields)
        {
          number = Encoding.GetEncoding(866).GetString(recReader.ReadBytes(field.fieldLen));
          switch (field.fieldType)
          {
            case 'N':
              if (number.IndexOf(".") > -1)
              {
                col = new DataColumn(field.fieldName, typeof(decimal));
              }
              else
              {
                col = new DataColumn(field.fieldName, typeof(int));
              }

              break;
            case 'C':
              col = new DataColumn(field.fieldName, typeof(string));
              break;
            case 'T':

              // You can uncomment this to see the time component in the grid
              // col = new DataColumn(field.fieldName, typeof(string));
              col = new DataColumn(field.fieldName, typeof(DateTime));
              break;
            case 'D':
              col = new DataColumn(field.fieldName, typeof(DateTime));
              break;
            case 'L':
              col = new DataColumn(field.fieldName, typeof(bool));
              break;
            case 'F':
              col = new DataColumn(field.fieldName, typeof(Double));
              break;
          }
        }

        #endregion

        // Skip past the end of the header. 
        br.BaseStream.Seek(header.headerLen, SeekOrigin.Begin);

        // Read in all the records
        for (var counter = 0; counter <= header.numRecords - 1; counter++)
        {
          // First we'll read the entire record into a buffer and then read each field from the buffer
          // This helps account for any extra space at the end of each record and probably performs better
          buffer = br.ReadBytes(header.recordLen);
          recReader = new BinaryReader(new MemoryStream(buffer));

          // All dbf field records begin with a deleted flag field. Deleted - 0x2A (asterisk) else 0x20 (space)
          if (recReader.ReadChar() == '*')
          {
            continue;
          }

          // Loop through each field in a record
          fieldIndex = 0;

          // New XML
          var document = new XmlDocument();
          var element = document.CreateElement("Kladr"); // Path.GetFileNameWithoutExtension(dbfFile).ToUpper());
          document.AppendChild(element);

          foreach (FieldDescriptor field in fields)
          {
            string s;
            fieldsNames.TryGetValue(field.fieldName, out s);
            var element2 = document.CreateElement(s);
            element.AppendChild(element2);
            XmlText text;

            #region switch field type

            switch (field.fieldType)
            {
              case 'N': // Number

                // If you port this to .NET 2.0, use the Decimal.TryParse method
                number = Encoding.GetEncoding(866).GetString(recReader.ReadBytes(field.fieldLen));
                text = IsNumber(number) ? document.CreateTextNode(number) : document.CreateTextNode("0");

                break;

              case 'C': // String                       
                text =
                  document.CreateTextNode(
                    Encoding.GetEncoding(866).GetString(recReader.ReadBytes(field.fieldLen)).TrimEnd(' '));
                element2.AppendChild(text);
                if (text.Length < 3)
                {
                  var attr = document.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                  attr.Value = "true";
                  element2.SetAttributeNode(attr);
                }

                break;

              case 'D': // Date (YYYYMMDD)
                year = Encoding.GetEncoding(866).GetString(recReader.ReadBytes(4));
                month = Encoding.GetEncoding(866).GetString(recReader.ReadBytes(2));
                day = Encoding.GetEncoding(866).GetString(recReader.ReadBytes(2));
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
                long lDate = recReader.ReadInt32();
                lTime = recReader.ReadInt32() * 10000L;
                text = document.CreateTextNode(JulianToDateTime(lDate).AddTicks(lTime).ToString());
                break;

              case 'L': // Boolean (Y/N)
                text = 'Y' == recReader.ReadByte() ? document.CreateTextNode("true") : document.CreateTextNode("false");

                break;

              case 'F':
                number = Encoding.GetEncoding(866).GetString(recReader.ReadBytes(field.fieldLen));
                text = IsNumber(number) ? document.CreateTextNode(number) : document.CreateTextNode("0.0F");

                break;
            }

            #endregion

            fieldIndex++;
          }

          var stream = new MemoryStream();
          document.Save(stream);
          stream.Position = 0;
          kladrCq.Enqueue(Kladr.Deserializating(document));
          recReader.Close();
        }

        return header.numRecords;
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
    /// <param name="lJDN">
    /// Julian Date to convert (days since 01/01/4713 BC)
    /// </param>
    /// <returns>
    /// DateTime
    /// </returns>
    private static DateTime JulianToDateTime(long lJDN)
    {
      var p = Convert.ToDouble(lJDN);
      var s1 = p + 68569;
      var n = Math.Floor(4 * s1 / 146097);
      var s2 = s1 - Math.Floor((146097 * n + 3) / 4);
      var i = Math.Floor(4000 * (s2 + 1) / 1461001);
      var s3 = s2 - Math.Floor(1461 * i / 4) + 31;
      var q = Math.Floor(80 * s3 / 2447);
      var d = s3 - Math.Floor(2447 * q / 80);
      var s4 = Math.Floor(q / 11);
      var m = q + 2 - 12 * s4;
      var j = 100 * (n - 49) + i + s4;
      return new DateTime(Convert.ToInt32(j), Convert.ToInt32(m), Convert.ToInt32(d));
    }

    #endregion

    /// <summary>
    ///   This is the file header for a DBF. We do this special layout with everything
    ///   packed so we can read straight from disk into the structure to populate it
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    private struct DBFHeader
    {
      /// <summary>
      ///   The version.
      /// </summary>
      public readonly byte version;

      /// <summary>
      ///   The update year.
      /// </summary>
      public readonly byte updateYear;

      /// <summary>
      ///   The update month.
      /// </summary>
      public readonly byte updateMonth;

      /// <summary>
      ///   The update day.
      /// </summary>
      public readonly byte updateDay;

      /// <summary>
      ///   The num records.
      /// </summary>
      public readonly int numRecords;

      /// <summary>
      ///   The header len.
      /// </summary>
      public readonly short headerLen;

      /// <summary>
      ///   The record len.
      /// </summary>
      public readonly short recordLen;

      /// <summary>
      ///   The reserved 1.
      /// </summary>
      public readonly short reserved1;

      /// <summary>
      ///   The incomplete trans.
      /// </summary>
      public readonly byte incompleteTrans;

      /// <summary>
      ///   The encryption flag.
      /// </summary>
      public readonly byte encryptionFlag;

      /// <summary>
      ///   The reserved 2.
      /// </summary>
      public readonly int reserved2;

      /// <summary>
      ///   The reserved 3.
      /// </summary>
      public readonly long reserved3;

      /// <summary>
      ///   The mdx.
      /// </summary>
      public readonly byte MDX;

      /// <summary>
      ///   The language.
      /// </summary>
      public readonly byte language;

      /// <summary>
      ///   The reserved 4.
      /// </summary>
      public readonly short reserved4;
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
      public readonly string fieldName;

      /// <summary>
      ///   The field type.
      /// </summary>
      public readonly char fieldType;

      /// <summary>
      ///   The address.
      /// </summary>
      public readonly int address;

      /// <summary>
      ///   The field len.
      /// </summary>
      public readonly byte fieldLen;

      /// <summary>
      ///   The count.
      /// </summary>
      public readonly byte count;

      /// <summary>
      ///   The reserved 1.
      /// </summary>
      public readonly short reserved1;

      /// <summary>
      ///   The work area.
      /// </summary>
      public readonly byte workArea;

      /// <summary>
      ///   The reserved 2.
      /// </summary>
      public readonly short reserved2;

      /// <summary>
      ///   The flag.
      /// </summary>
      public readonly byte flag;

      /// <summary>
      ///   The reserved 3.
      /// </summary>
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
      public readonly byte[] reserved3;

      /// <summary>
      ///   The index flag.
      /// </summary>
      public readonly byte indexFlag;
    }
  }
}