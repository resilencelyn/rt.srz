// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Assumptions.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The assumptions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.srz.database.business.standard
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Xml.Linq;
  using System.Xml.XPath;

  using rt.srz.database.business.standard.helpers;

  /// <summary>
  /// The assumptions.
  /// </summary>
  public sealed class Assumptions
  {
    /// <summary>
    /// The date time formats.
    /// </summary>
    private readonly List<string> dateTimeFormats = new List<string>(); // !! ������ �������� ������� ���� ������

    /// <summary>
    /// The date time max.
    /// </summary>
    private DateTime dateTimeMax;

    /// <summary>
    /// The date time min.
    /// </summary>
    private DateTime dateTimeMin;

    /// <summary>
    /// Initializes a new instance of the <see cref="Assumptions"/> class.
    /// </summary>
    /// <param name="rootRulesLoader">
    /// The root rules loader.
    /// </param>
    public Assumptions(XElement rootRulesLoader = null)
    {
      ClearAssumptions();
      if (rootRulesLoader != null)
        LoadAssumptions(rootRulesLoader, clearExistingRules: false);
    }

    // ��� �������� ����������
    // public static string CurrentApplicationName = RetrieveApplicationName();
    // ��� �������� �������� �����
    // public static string CurrentWorkplaceName = null;
    // ��� �������� ���������
    // public static string CurrentOperatorName = null;

    // --------------------------------------------------------

    // �������� ������ ����/�������
    // public string DateTimeFormat
    // {
    // get { return dateTimeFormats[0]; }
    // }

    // ��� ������� ����/�������
    // public IList<string> DateTimeFormats
    // {
    // get { return dateTimeFormats; }
    // }

    /// <summary>
    /// ���������� ������� ����/�������
    /// </summary>
    public DateTime DateTimeMin
    {
      get { return dateTimeMin; }
    }

    /// <summary>
    /// ���������� �������� ����/�������
    /// </summary>
    public DateTime DateTimeMax
    {
      get { return dateTimeMax; }
    }


    /// <summary>
    /// ��������� ������ �� xml-������������
    /// </summary>
    /// <param name="rootRulesLoader">
    /// The root rules loader.
    /// </param>
    /// <param name="clearExistingRules">
    /// The clear existing rules.
    /// </param>
    public void LoadAssumptions(XElement rootRulesLoader, bool clearExistingRules = true)
    {
      if (clearExistingRules)
        ClearAssumptions();

      if (rootRulesLoader != null)
      {
        foreach (var xmlGlobals in rootRulesLoader.XPathSelectElements("/�����������"))
        {
          // ������ ������ �������� ������ ����/�������
          var attr = xmlGlobals.RetrieveAttribute("�����������������", trimToNull: true);
          if (attr != null)
            dateTimeFormats[0] = attr;

          // ������ ������ ��� ������������
          foreach (var xmlRule in xmlGlobals.XPathSelectElements("���"))
          {
            attr = xmlRule.RetrieveAttribute("�����������������", trimToNull: true);
            if (attr != null)
              AddDateTimeFormat(attr);
          }

          // ������ ����������� ����/�������
          ReadDateTimeRule(xmlGlobals, "������������������", ref dateTimeMin);
          ReadDateTimeRule(xmlGlobals, "�������������������", ref dateTimeMax);
        }
      }
    }

    // --------------------------------------------------------
    // �������� ��� ������

    /// <summary>
    /// The clear assumptions.
    /// </summary>
    public void ClearAssumptions()
    {
      dateTimeFormats.Clear();
      dateTimeFormats.Add(helpers.ConversionHelper.DateTimeFormat);

      dateTimeMin = DateTime.MinValue;
      dateTimeMax = DateTime.MaxValue;
    }

    // --------------------------------------------------------
    // �������� ��� ���� ������ ����/�������

    /// <summary>
    /// The add date time format.
    /// </summary>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool AddDateTimeFormat(string format)
    {
      foreach (var f in dateTimeFormats)
      {
        if (string.CompareOrdinal(f, format) == 0)
          return false;
      }

      dateTimeFormats.Add(format);
      return true;
    }

    // --------------------------------------------------------
    // ��������� ��������� ����/������� � ���������� ��������

    /// <summary>
    /// The check date time.
    /// </summary>
    /// <param name="date">
    /// The date.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool CheckDateTime(DateTime date)
    {
      return date >= DateTimeMin && date <= DateTimeMax;
    }

    // --------------------------------------------------------
    // ������������� ������ � ����/�����

    /// <summary>
    /// The string as date time.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public DateTime StringAsDateTime(string s)
    {
      return StringAsDateTime(s, formatRule: "������������������", constraintsRule: "�������������");
    }

    // --------------------------------------------------------
    // ������������� ������ � ����/�����, ������ ������ � ������� ��������������

    /// <summary>
    /// The string as date time.
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
    /// The <see cref="DateTime"/>.
    /// </returns>
    public DateTime StringAsDateTime(string str, string formatRule, string constraintsRule)
    {
      DateTime result;
      if (ParseDateTime(str, out result))
      {
        if (!CheckDateTime(result))
        {
        }

        // errorsHolder.ThrowException(constraintsRule, result, DateTimeMin, DateTimeMax);
      }

      return result;
    }

    // --------------------------------------------------------
    // ������������� ����/����� � ������

    // public string DateTimeAsString(DateTime? dt, int formatIndex = 0)
    // {
    // if (dt.HasValue)
    // return dt.Value.ToString(dateTimeFormats[formatIndex]);
    // return null;
    // }

    // --------------------------------------------------------

    // static string RetrieveApplicationName()
    // {
    // var process = Process.GetCurrentProcess();
    // if (process != null)
    // {
    // var module = process.MainModule;
    // if (module != null)
    // {
    // var file = module.FileName;
    // if (!string.IsNullOrEmpty(file))
    // return Path.GetFileNameWithoutExtension(file);
    // }
    // }
    // return null;
    // }

    // --------------------------------------------------------

    /// <summary>
    /// The parse date time.
    /// </summary>
    /// <param name="text">
    /// The text.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <param name="result">
    /// The result.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool ParseDateTime(string text, string format, out DateTime result)
    {
      if (string.IsNullOrEmpty(format))
      {
        result = helpers.ConversionHelper.DateTimeZero;
        return false;
      }

      return DateTime.TryParseExact(text, format, /*IFormatProvider*/ null,
                                    DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result);
    }

    // --------------------------------------------------------

    /// <summary>
    /// The parse date time.
    /// </summary>
    /// <param name="text">
    /// The text.
    /// </param>
    /// <param name="result">
    /// The result.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private bool ParseDateTime(string text, out DateTime result)
    {
      var i = 0;
      do
      {
        if (ParseDateTime(text, dateTimeFormats[i], out result))
          return true;
      }
      while (++i < dateTimeFormats.Count);

      return false;
    }

    // --------------------------------------------------------

    /// <summary>
    /// The read date time rule.
    /// </summary>
    /// <param name="xmlRules">
    /// The xml rules.
    /// </param>
    /// <param name="attribute">
    /// The attribute.
    /// </param>
    /// <param name="dateTimeRule">
    /// The date time rule.
    /// </param>
    private void ReadDateTimeRule(XElement xmlRules, string attribute, ref DateTime dateTimeRule)
    {
      attribute = xmlRules.RetrieveAttribute(attribute, trimToNull: true);
      if (attribute != null)
      {
        DateTime rule;
        if (!ParseDateTime(attribute, out rule))
          XmlHelper.ThrowCastException(attribute, "����/�����");
        dateTimeRule = rule;
      }
    }

    // --------------------------------------------------------
  }
}