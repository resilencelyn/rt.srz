// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Assumptions.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The assumptions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Xml.Linq;
  using System.Xml.XPath;

  // набор констант и представлений
  /// <summary>
  /// The assumptions.
  /// </summary>
  public sealed class Assumptions
  {
    #region Fields

    /// <summary>
    /// The date time formats.
    /// </summary>
    private readonly List<string> dateTimeFormats = new List<string>(); // !! всегда содержит минимум один формат

    /// <summary>
    /// The date time max.
    /// </summary>
    private DateTime dateTimeMax;

    /// <summary>
    /// The date time min.
    /// </summary>
    private DateTime dateTimeMin;

    #endregion

    // имя текущего приложения
    // public static string CurrentApplicationName = RetrieveApplicationName();
    // имя текущего рабочего места
    // public static string CurrentWorkplaceName = null;
    // имя текущего оператора
    // public static string CurrentOperatorName = null;

    // --------------------------------------------------------

    // основной формат даты/времени
    // public string DateTimeFormat
    // {
    // get { return dateTimeFormats[0]; }
    // }

    // все форматы даты/времени
    // public IList<string> DateTimeFormats
    // {
    // get { return dateTimeFormats; }
    // }

    // допустимый минимум даты/времени

    // --------------------------------------------------------
    // основной конструктор
    #region Constructors and Destructors

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
      {
        LoadAssumptions(rootRulesLoader, false);
      }
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the date time max.
    /// </summary>
    public DateTime DateTimeMax
    {
      get
      {
        return dateTimeMax;
      }
    }

    /// <summary>
    /// Gets the date time min.
    /// </summary>
    public DateTime DateTimeMin
    {
      get
      {
        return dateTimeMin;
      }
    }

    #endregion

    // --------------------------------------------------------
    // загрузить данные из xml-конфигурации

    // --------------------------------------------------------
    // добавить еще один формат даты/времени
    #region Public Methods and Operators

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
        {
          return false;
        }
      }

      dateTimeFormats.Add(format);
      return true;
    }

    // --------------------------------------------------------
    // проверить попадание даты/времени в допустимый диапазон

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

    /// <summary>
    /// The clear assumptions.
    /// </summary>
    public void ClearAssumptions()
    {
      dateTimeFormats.Clear();
      dateTimeFormats.Add(ConversionHelper.DateTimeFormat);

      dateTimeMin = DateTime.MinValue;
      dateTimeMax = DateTime.MaxValue;
    }

    /// <summary>
    /// The load assumptions.
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
      {
        ClearAssumptions();
      }

      foreach (var xmlGlobals in rootRulesLoader.XPathSelectElements("/Ограничения"))
      {
        // сперва читаем основной формат даты/времени
        var attr = xmlGlobals.RetrieveAttribute("ФорматДатыВремени", true);
        if (attr != null)
        {
          dateTimeFormats[0] = attr;
        }

        // теперь читаем все альтернативы
        foreach (var xmlRule in xmlGlobals.XPathSelectElements("Или"))
        {
          attr = xmlRule.RetrieveAttribute("ФорматДатыВремени", true);
          if (attr != null)
          {
            AddDateTimeFormat(attr);
          }
        }

        // читаем ограничения даты/времени
        ReadDateTimeRule(xmlGlobals, "МинимумДатыВремени", ref dateTimeMin);
        ReadDateTimeRule(xmlGlobals, "МаксимумДатыВремени", ref dateTimeMax);
      }
    }

    // --------------------------------------------------------
    // преобразовать строку в дату/время

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
      return StringAsDateTime(s, "ДатаПреобразование", "ДатаДопустима");
    }

    // --------------------------------------------------------
    // преобразовать строку в дату/время, указав формат и правила преобразования

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

    #endregion

    // --------------------------------------------------------
    // преобразовать дату/время в строку

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
    #region Methods

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
        result = ConversionHelper.DateTimeZero;
        return false;
      }

      return DateTime.TryParseExact(
                                    text, 
                                    format, 
                                    /*IFormatProvider*/ null, 
                                    DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, 
                                    out result);
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
        {
          return true;
        }
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
      attribute = xmlRules.RetrieveAttribute(attribute, true);
      if (attribute != null)
      {
        DateTime rule;
        if (!ParseDateTime(attribute, out rule))
        {
          XmlHelper.ThrowCastException(attribute, "Дата/Время");
        }

        dateTimeRule = rule;
      }
    }

    #endregion

    // --------------------------------------------------------
  }
}