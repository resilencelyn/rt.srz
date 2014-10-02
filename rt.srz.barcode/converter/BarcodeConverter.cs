// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BarcodeConverter.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The barcode converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;
using rt.srz.barcode.Properties;
using rt.srz.model.barcode;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The barcode converter.
  /// </summary>
  public static class BarcodeConverter
  {
    #region Static Fields

    /// <summary>
    ///   The cnvs.
    /// </summary>
    private static readonly Dictionary<Type, ITypeConverter> Cnvs = new Dictionary<Type, ITypeConverter>
                                                                      {
                                                                        {
                                                                          typeof (
                                                                          Oms5EncodingStringConverter
                                                                          ), 
                                                                          new Oms5EncodingStringConverter
                                                                          ()
                                                                        }, 
                                                                        {
                                                                          typeof (
                                                                          Oms6EncodingStringConverter
                                                                          ), 
                                                                          new Oms6EncodingStringConverter
                                                                          ()
                                                                        }, 
                                                                        {
                                                                          typeof (
                                                                          Oms62EncodingStringConverter
                                                                          ), 
                                                                          new Oms62EncodingStringConverter
                                                                          ()
                                                                        }, 
                                                                        {
                                                                          typeof (
                                                                          NumberConverter
                                                                          ), 
                                                                          new NumberConverter
                                                                          ()
                                                                        }, 
                                                                        {
                                                                          typeof (
                                                                          Int24Converter
                                                                          ), 
                                                                          new Int24Converter
                                                                          ()
                                                                        }, 
                                                                        {
                                                                          typeof (
                                                                          ShortDateConverter
                                                                          ), 
                                                                          new ShortDateConverter
                                                                          ()
                                                                        }, 
                                                                        {
                                                                          typeof (
                                                                          ShortYearConverter
                                                                          ), 
                                                                          new ShortYearConverter
                                                                          ()
                                                                        }, 
                                                                        {
                                                                          typeof (
                                                                          ShortBirthDateConverter
                                                                          ), 
                                                                          new ShortBirthDateConverter
                                                                          ()
                                                                        }
                                                                      };

    /// <summary>
    ///   The bc_input_transform.
    /// </summary>
    private static readonly XslCompiledTransform InputTransform;

    /// <summary>
    ///   The bc output transform.
    /// </summary>
    private static readonly XslCompiledTransform OutputTransform;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes static members of the <see cref="BarcodeConverter" /> class.
    /// </summary>
    static BarcodeConverter()
    {
      var settings1 = new XsltSettings { EnableScript = true };
      InputTransform = new XslCompiledTransform();

      ////using (XmlReader stylesheet = XmlReader.Create((TextReader)new StringReader(Resources.BarcodeNormilizer)))
      using (var stylesheet = XmlReader.Create(new StringReader(Resources.BarcodeNormilizer)))
      {
        InputTransform.Load(stylesheet, settings1, new XmlUrlResolver());
      }

      var settings2 = new XsltSettings { EnableScript = true };

      OutputTransform = new XslCompiledTransform();

      ////using (XmlReader stylesheet = XmlReader.Create((TextReader) new StringReader(Resources.BarcodeDenormilizer))
      using (var stylesheet = XmlReader.Create(new StringReader(Resources.BarcodeDenormilizer)))
      {
        OutputTransform.Load(stylesheet, settings2, new XmlUrlResolver());
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The convert.
    /// </summary>
    /// <param name="schema">
    /// The schema. 
    /// </param>
    /// <param name="barcodData">
    /// The barcod data. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    public static string Convert(string schema, byte[] barcodData)
    {
      if (barcodData == null)
      {
        throw new BarcodeConverterException("Параметр не может быть равен <null>. Параметр: <barcodData>.");
      }

      if (string.IsNullOrEmpty(schema))
      {
        throw new BarcodeConverterException("Параметр не может быть равен <null>. Параметр: <schema>.");
      }

      if (!BarcodeVersions.IsValidBarcode(barcodData))
      {
        throw new BarcodeConverterException("Неизвестная версия штрих-кода. Параметр: <barcodData>.");
      }

      var barcodeVersion = BarcodeVersions.GetBarcodeVersion(barcodData);
      if (barcodeVersion.Version != 1 && barcodeVersion.Version != 2)
      {
        return null;
      }

      switch (barcodeVersion.Version)
      {
        case 1:
          using (var dataTable = new DataTable { TableName = barcodeVersion.RootTagName })
          {
            ////dataTable.ReadXmlSchema((TextReader)new StringReader(Resources.NormalizedBarcode));
            dataTable.ReadXmlSchema(new StringReader(Resources.NormalizedBarcode));
            var offset = 0;
            var values = new[]
                           {
                             GetObject(barcodData, Cnvs[typeof (NumberConverter)], typeof (byte), ref offset), 
                             GetObject(barcodData, Cnvs[typeof (NumberConverter)], typeof (ulong), ref offset), 
                             GetObject(
                               barcodData, 
                               Cnvs[typeof (Oms6EncodingStringConverter)], 
                               typeof (string), 
                               ref offset, 
                               42), 
                             GetObject(barcodData, Cnvs[typeof (NumberConverter)], typeof (byte), ref offset), 
                             GetObject(barcodData, Cnvs[typeof (ShortBirthDateConverter)], typeof (DateTime), ref offset)
                             , 
                             GetObject(barcodData, Cnvs[typeof (ShortBirthDateConverter)], typeof (DateTime), ref offset)
                             , 
                             barcodData.ToList().GetRange(offset, 6).ToArray(), 
                             barcodData.ToList().GetRange(offset + 6, 3).ToArray(), 
                             barcodData.ToList().GetRange(offset + 9, barcodeVersion.Length - (offset + 9)).ToArray()
                           };
            dataTable.LoadDataRow(values, LoadOption.OverwriteChanges);
            string s;
            using (var stringWriter = new StringWriter())
            {
              dataTable.WriteXml(stringWriter);
              s = XElement.Parse(stringWriter.ToString()).FirstNode.ToString();
            }

            var xpathDocument = new XPathDocument(new StringReader(s));
            var settings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            var output = new StringBuilder();
            using (var results = XmlWriter.Create(output, settings))
            {
              OutputTransform.Transform(xpathDocument, results);
              results.Close();
            }

            return output.ToString();
          }

        case 2:
          {
            using (var dataTable = new DataTable { TableName = barcodeVersion.RootTagName })
            {
              ////dataTable.ReadXmlSchema((TextReader)new StringReader(Resources.NormalizedBarcode));
              dataTable.ReadXmlSchema(new StringReader(Resources.NormalizedBarcode));
              var offset = 0;
              var values = new[]
                             {
                               GetObject(barcodData, Cnvs[typeof (NumberConverter)], typeof (byte), ref offset), 
                               GetObject(barcodData, Cnvs[typeof (NumberConverter)], typeof (ulong), ref offset), 
                               GetObject(
                                 barcodData, 
                                 Cnvs[typeof (Oms62EncodingStringConverter)], 
                                 typeof (string), 
                                 ref offset, 
                                 0x33), 
                               GetObject(barcodData, Cnvs[typeof (NumberConverter)], typeof (byte), ref offset), 
                               GetObject(
                                 barcodData, 
                                 Cnvs[typeof (ShortBirthDateConverter)], 
                                 typeof (DateTime), 
                                 ref offset), 
                               GetObject(
                                 barcodData, 
                                 Cnvs[typeof (ShortBirthDateConverter)], 
                                 typeof (DateTime), 
                                 ref offset), 
                               barcodData.ToList().GetRange(offset, barcodeVersion.Length - offset).ToArray()
                             };

              ////dataTable.LoadDataRow(values, LoadOption.OverwriteChanges);
              ////string s = (string) null;
              ////using (StringWriter stringWriter = new StringWriter())
              ////{
              ////    dataTable.WriteXml((TextWriter) stringWriter);
              ////    s = ((object) XElement.Parse(stringWriter.ToString()).FirstNode).ToString();
              ////}
              ////XPathDocument xpathDocument = new XPathDocument((TextReader) new StringReader(s));
              ////XmlWriterSettings settings = new XmlWriterSettings();
              ////settings.OmitXmlDeclaration = true;
              ////settings.Indent = true;
              ////StringBuilder output = new StringBuilder();
              ////using (XmlWriter results = XmlWriter.Create(output, settings))
              ////{
              ////    BarcodeConverter.bc_output_transform.Transform((IXPathNavigable) xpathDocument, results);
              ////    results.Close();
              ////}
              ////return ((object) output).ToString();

              ////dataTable.LoadDataRow(values, LoadOption.OverwriteChanges);
              ////string s = (string)null;
              ////using (StringWriter stringWriter = new StringWriter())
              ////{
              ////    dataTable.WriteXml((TextWriter)stringWriter);
              ////    s = ((object)XElement.Parse(stringWriter.ToString()).FirstNode).ToString();
              ////}
              ////XPathDocument xpathDocument = new XPathDocument((TextReader)new StringReader(s));
              ////XmlWriterSettings settings = new XmlWriterSettings();
              ////settings.OmitXmlDeclaration = true;
              ////settings.Indent = true;
              ////StringBuilder output = new StringBuilder();
              ////using (XmlWriter results = XmlWriter.Create(output, settings))
              ////{
              ////    BarcodeConverter.bc_output_transform.Transform((IXPathNavigable)xpathDocument, results);
              ////    results.Close();
              ////}
              ////return ((object)output).ToString();
              var fio = values[2].ToString().Split('|');

              ////string eds = System.Text.Encoding.GetEncoding(28591).GetString(((byte[])values[6]));
              ////string eds = BitConverter.ToString(((byte[])values[6])).Replace("-","");// = System.Text.Encoding.GetEncoding(28591).GetString(((byte[])values[6]));
              var eds = System.Convert.ToBase64String((byte[])values[6]);
              var tempStr = new string[10];
              tempStr[0] = ((byte)values[0]).ToString(CultureInfo.InvariantCulture);
              tempStr[1] = ((ulong)values[1]).ToString(CultureInfo.InvariantCulture);
              tempStr[2] = fio[1].Trim();
              tempStr[3] = fio[0].Trim();
              tempStr[4] = fio[2].Trim();
              tempStr[5] = ((byte)values[3]).ToString(CultureInfo.InvariantCulture);
              tempStr[6] = ((DateTime)values[4]).ToString("s");
              tempStr[7] = ((DateTime)values[5]).ToString("s");
              tempStr[8] = eds;
              var resultXml =
                string.Format(
                  "<BarcodeData> <BarcodeType>{0}</BarcodeType> <PolicyNumber>{1}</PolicyNumber>  <FirstName>{2}</FirstName> <LastName>{3}</LastName> <Patronymic>{4}</Patronymic> <Sex>{5}</Sex> <BirthDate>{6}</BirthDate> <ExpireDate>{7}</ExpireDate>  <EDS>{8}</EDS> </BarcodeData>",
                  tempStr[0],
                  tempStr[1],
                  tempStr[2],
                  tempStr[3],
                  tempStr[4],
                  tempStr[5],
                  tempStr[6],
                  tempStr[7],
                  tempStr[8]);
              return resultXml;
            }
          }
      }

      return string.Empty;
    }

    /// <summary>
    /// The convert.
    /// </summary>
    /// <param name="schema">
    /// The schema. 
    /// </param>
    /// <param name="xmlData">
    /// The xml_data. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public static byte[] Convert(string schema, string xmlData)
    {
      if (string.IsNullOrEmpty(xmlData))
      {
        throw new BarcodeConverterException("Параметр не может быть равен <null>. Параметр: <xml_data>.");
      }

      if (string.IsNullOrEmpty(schema))
      {
        throw new BarcodeConverterException("Параметр не может быть равен <null>. Параметр: <schema>.");
      }

      ValidateSchema(schema, xmlData);
      var xpathDocument = new XPathDocument(new StringReader(xmlData));
      var settings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
      var output = new StringBuilder();
      using (var results = XmlWriter.Create(output, settings))
      {
        InputTransform.Transform(xpathDocument, results);
        results.Close();
      }

      xmlData = output.ToString();

      ////BarcodeConverter.ValidateSchema(Resources.NormalizedBarcode, xml_data);
      ValidateSchema(Resources.NormalizedBarcode, xmlData);
      var xdocument = XDocument.Parse(xmlData);
      if (xdocument.Root != null)
      {
        using (var dataTable = new DataTable { TableName = xdocument.Root.Name.LocalName })
        {
          ////dataTable.ReadXmlSchema((TextReader)new StringReader(Resources.NormalizedBarcode));
          dataTable.ReadXmlSchema(new StringReader(Resources.NormalizedBarcode));
          var xmlReaderSettings = new XmlReaderSettings { IgnoreWhitespace = false };
          var element = new XElement("DataSet", XElement.Parse(xmlData, LoadOptions.PreserveWhitespace));
          var reader = XmlReader.Create(new StringReader(element.ToString()), xmlReaderSettings);
          var num = (int)dataTable.ReadXml(reader);
          if (dataTable.Rows.Count > 0)
          {
            var data = new byte[130];
            var offset = 0;
            dataTable.Rows[0].ItemArray.ToList().ForEach(
              item =>
              {
                if (item is byte || item is short || (item is int || item is uint)
                    || (item is ushort || item is ulong) || item is long)
                {
                  CopyBytes(typeof(NumberConverter), item, data, ref offset);
                }

                if (item is string)
                {
                  CopyBytes(typeof(Oms6EncodingStringConverter), item, data, ref offset);
                }

                if (item is DateTime)
                {
                  CopyBytes(typeof(ShortBirthDateConverter), item, data, ref offset);
                }

                if (!(item is byte[]))
                {
                  return;
                }

                Array.Copy((Array)item, 0, data, offset, ((byte[])item).Length);
                offset += ((byte[])item).Length;
              });
            return data.ToList().GetRange(0, offset).ToArray();
          }
        }
      }

      return null;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The copy bytes.
    /// </summary>
    /// <param name="converterType">
    /// The converter type. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <param name="destination">
    /// The destination. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    private static void CopyBytes(Type converterType, object value, byte[] destination, ref int offset)
    {
      var numArray = Cnvs[converterType].ConvertFrom(value);
      Array.Copy(numArray, 0, destination, offset, numArray.Length);
      offset += numArray.Length;
    }

    /// <summary>
    /// The get object.
    /// </summary>
    /// <param name="data">
    /// The data. 
    /// </param>
    /// <param name="converter">
    /// The converter. 
    /// </param>
    /// <param name="type">
    /// The type. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <returns>
    /// The <see cref="object"/> . 
    /// </returns>
    private static object GetObject(byte[] data, ITypeConverter converter, Type type, ref int offset)
    {
      return GetObject(data, converter, type, ref offset, converter.GetLength(type));
    }

    /// <summary>
    /// The get object.
    /// </summary>
    /// <param name="data">
    /// The data. 
    /// </param>
    /// <param name="converter">
    /// The converter. 
    /// </param>
    /// <param name="type">
    /// The type. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <returns>
    /// The <see cref="object"/> . 
    /// </returns>
    private static object GetObject(byte[] data, ITypeConverter converter, Type type, ref int offset, int length)
    {
      var obj = converter.ConvertTo(type, data, offset, length);
      offset += length;
      return obj;
    }

    /// <summary>
    /// The validate schema.
    /// </summary>
    /// <param name="schema">
    /// The schema. 
    /// </param>
    /// <param name="xmlData">
    /// The xml_data. 
    /// </param>
    private static void ValidateSchema(string schema, string xmlData)
    {
      try
      {
        var xmlTextReader = new XmlTextReader(new StringReader(schema));
        var xmlSchemaSet = new XmlSchemaSet();
        xmlSchemaSet.Add(null, xmlTextReader);
        var xmlReaderSettings = new XmlReaderSettings
                                  {
                                    Schemas = xmlSchemaSet,
                                    ValidationFlags = XmlSchemaValidationFlags.AllowXmlAttributes,
                                    ValidationType = ValidationType.Schema
                                  };
        var xmlReader = XmlReader.Create(new StringReader(xmlData), xmlReaderSettings);
        do
        {
        }
        while (xmlReader.Read());
        xmlReader.Close();
      }
      catch (Exception ex)
      {
        throw new BarcodeConverterException("Ошибка проверки документа по схеме XSD", ex);
      }
    }

    #endregion
  }
}