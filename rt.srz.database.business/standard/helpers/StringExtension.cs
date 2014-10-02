// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.srz.database.business.standard.helpers
{
  using System;
  using System.IO;
  using System.Xml;
  using System.Xml.Linq;

  /// <summary>
  ///   The string extension.
  /// </summary>
  public static class StringExtension
  {
    /// <summary>
    ///   The assumptions.
    /// </summary>
    private static readonly Assumptions Assumptions = new Assumptions();

    #region StringAsDateTime

    /// <summary>
    /// Initializes static members of the <see cref="StringExtension"/> class.
    /// </summary>
    static StringExtension()
    {
      string formatsXML =
        "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
          "<ѕравила>"+
            "<ќграничени€ ‘орматƒаты¬ремени=\"yyyyMMdd\" ћинимумƒаты¬ремени=\"18000101\" ћаксимумƒаты¬ремени=\"25000101\" />"+
            "<»ли ‘орматƒаты¬ремени=\"yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'zzz\" />" +
            "<»ли ‘орматƒаты¬ремени=\"yyyy'-'MM'-'dd'T'HH':'mm':'sszzz\" />" +
            "<»ли ‘орматƒаты¬ремени=\"yyyy'-'MM'-'dd\" />" +
            "<»ли ‘орматƒаты¬ремени=\"yyyyMM\" />" +
          "</ѕравила>";
      
      //var formats = Assembly.GetExecutingAssembly().GetManifestResourceStream("rt.srz.business.database.standard.DataFormats.xml");
      var xmlReader = new XmlTextReader(new StringReader(formatsXML));
      Assumptions.LoadAssumptions(XElement.Load(xmlReader), clearExistingRules: false);
    }

    /// <summary>
    /// преобразовать строку в дату/врем€, задав формат и ограничени€ преобразований
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <param name="formatRule">
    /// The format rule. 
    /// </param>
    /// <param name="constraintsRule">
    /// The constraints rule. 
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/> . 
    /// </returns>
    public static DateTime StringAsDateTime(string str, string formatRule, string constraintsRule)
    {
      return Assumptions.StringAsDateTime(str, formatRule, constraintsRule);
    }

    /// <summary>
    /// преобразовать строку в дату/врем€
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/> . 
    /// </returns>
    public static DateTime StringAsDateTime(string str)
    {
      return Assumptions.StringAsDateTime(str);
    }

    /// <summary>
    /// преобразовать строку в Nullable(дату/врем€)
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/> . 
    /// </returns>
    public static DateTime? StringAsDateTimeNullable(string str)
    {
      return string.IsNullOrEmpty(str) ? (DateTime?)null : StringAsDateTime(str);
    }

    /// <summary>
    /// преобразовать строку в дату/врем€
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    public static void StringAsDateTime(string str, out DateTime value)
    {
      value = StringAsDateTime(str);
    }

    /// <summary>
    /// преобразовать строку в Nullable(дату/врем€)
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    public static void StringAsDateTime(string str, out DateTime? value)
    {
      value = StringAsDateTimeNullable(str);
    }

    #endregion

    #region StringAsDate

    /// <summary>
    /// преобразовать строку в дату, задав формат и ограничени€ преобразований
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <param name="formatRule">
    /// The format rule. 
    /// </param>
    /// <param name="constraintsRule">
    /// The constraints rule. 
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/> . 
    /// </returns>
    public static DateTime StringAsDate(string str, string formatRule, string constraintsRule)
    {
      return StringAsDateTime(str, formatRule, constraintsRule).Date;
    }

    /// <summary>
    /// The string as date.
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/> . 
    /// </returns>
    public static DateTime StringAsDate(string str)
    {
      return StringAsDateTime(str).Date;
    }

    /// <summary>
    /// преобразовать строку в Nullable(дату)
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/> . 
    /// </returns>
    public static DateTime? StringAsDateNullable(string str)
    {
      var value = StringAsDateTimeNullable(str);
      return value.HasValue ? (DateTime?)value.Value.Date : null;
    }

    /// <summary>
    /// преобразовать строку в дату
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    public static void StringAsDate(string str, out DateTime value)
    {
      value = StringAsDate(str);
    }

    /// <summary>
    /// преобразовать строку в Nullable(дату)
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    public static void StringAsDate(string str, out DateTime? value)
    {
      value = StringAsDateNullable(str);
    }

    #endregion
  }
}