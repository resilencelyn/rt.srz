// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The string extension.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
    #region Static Fields

    /// <summary>
    ///   The assumptions.
    /// </summary>
    private static readonly Assumptions Assumptions = new Assumptions();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes static members of the <see cref="StringExtension" /> class.
    /// </summary>
    static StringExtension()
    {
      var formatsXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + "<�������>"
                       + "<����������� �����������������=\"yyyyMMdd\" ������������������=\"18000101\" �������������������=\"25000101\" />"
                       + "<��� �����������������=\"yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'zzz\" />"
                       + "<��� �����������������=\"yyyy'-'MM'-'dd'T'HH':'mm':'sszzz\" />"
                       + "<��� �����������������=\"yyyy'-'MM'-'dd\" />" + "<��� �����������������=\"yyyyMM\" />"
                       + "</�������>";

      // var formats = Assembly.GetExecutingAssembly().GetManifestResourceStream("rt.srz.business.database.standard.DataFormats.xml");
      var xmlReader = new XmlTextReader(new StringReader(formatsXML));
      Assumptions.LoadAssumptions(XElement.Load(xmlReader), false);
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ������������� ������ � ����, ����� ������ � ����������� ��������������
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
    /// ������������� ������ � ����
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
    /// ������������� ������ � Nullable(����)
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

    /// <summary>
    /// ������������� ������ � Nullable(����)
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
    /// ������������� ������ � ����/�����, ����� ������ � ����������� ��������������
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
    /// ������������� ������ � ����/�����
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
    /// ������������� ������ � ����/�����
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
    /// ������������� ������ � Nullable(����/�����)
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

    /// <summary>
    /// ������������� ������ � Nullable(����/�����)
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

    #endregion
  }
}