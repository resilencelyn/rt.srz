namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Xml.Linq;
  using System.Xml.XPath;

  // набор констант и представлений
    public sealed class Assumptions
    {
        List<string> dateTimeFormats = new List<string>(); // !! всегда содержит минимум один формат
        DateTime dateTimeMin, dateTimeMax;

        // имя текущего приложения
        //public static string CurrentApplicationName = RetrieveApplicationName();
        // имя текущего рабочего места
        //public static string CurrentWorkplaceName = null;
        // имя текущего оператора
        //public static string CurrentOperatorName = null;

        // --------------------------------------------------------

        // основной формат даты/времени
        //public string DateTimeFormat
        //{
        //    get { return dateTimeFormats[0]; }
        //}

        // все форматы даты/времени
        //public IList<string> DateTimeFormats
        //{
        //    get { return dateTimeFormats; }
        //}

        // допустимый минимум даты/времени
        public DateTime DateTimeMin
        {
            get { return dateTimeMin; }
        }

        // допустимый максимум даты/времени
        public DateTime DateTimeMax
        {
            get { return dateTimeMax; }
        }

        // --------------------------------------------------------
        // основной конструктор

        public Assumptions(XElement rootRulesLoader = null)
        {
            ClearAssumptions();
            if (rootRulesLoader != null)
                LoadAssumptions(rootRulesLoader, clearExistingRules: false);
        }

        // --------------------------------------------------------
        // загрузить данные из xml-конфигурации

        public void LoadAssumptions(XElement rootRulesLoader, bool clearExistingRules = true)
        {
            if (clearExistingRules)
                ClearAssumptions();

            foreach (var xmlGlobals in rootRulesLoader.XPathSelectElements("/Ограничения"))
            {
                // сперва читаем основной формат даты/времени
                string attr = xmlGlobals.RetrieveAttribute("ФорматДатыВремени", trimToNull: true);
                if (attr != null)
                    dateTimeFormats[0] = attr;

                // теперь читаем все альтернативы
                foreach (var xmlRule in xmlGlobals.XPathSelectElements("Или"))
                {
                    attr = xmlRule.RetrieveAttribute("ФорматДатыВремени", trimToNull: true);
                    if (attr != null)
                        AddDateTimeFormat(attr);
                }

                // читаем ограничения даты/времени
                ReadDateTimeRule(xmlGlobals, "МинимумДатыВремени", ref dateTimeMin);
                ReadDateTimeRule(xmlGlobals, "МаксимумДатыВремени", ref dateTimeMax);
            }
        }

        // --------------------------------------------------------
        // очистить все данные

        public void ClearAssumptions()
        {
            dateTimeFormats.Clear();
            dateTimeFormats.Add(ConversionHelper.DateTimeFormat);

            dateTimeMin = DateTime.MinValue;
            dateTimeMax = DateTime.MaxValue;
        }

        // --------------------------------------------------------
        // добавить еще один формат даты/времени

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
        // проверить попадание даты/времени в допустимый диапазон

        public bool CheckDateTime(DateTime date)
        {
            return (date >= DateTimeMin && date <= DateTimeMax);
        }

        // --------------------------------------------------------
        // преобразовать строку в дату/время

        public DateTime StringAsDateTime(string s)
        {
            return StringAsDateTime(s, formatRule: "ДатаПреобразование", constraintsRule: "ДатаДопустима");
        }

        // --------------------------------------------------------
        // преобразовать строку в дату/время, указав формат и правила преобразования

        public DateTime StringAsDateTime(string str, string formatRule, string constraintsRule)
        {
            DateTime result;
            if (ParseDateTime(str, out result))
            {
                if (!CheckDateTime(result))
                {
                }
                //errorsHolder.ThrowException(constraintsRule, result, DateTimeMin, DateTimeMax);
            }
            return result;
        }

        // --------------------------------------------------------
        // преобразовать дату/время в строку

        //public string DateTimeAsString(DateTime? dt, int formatIndex = 0)
        //{
        //    if (dt.HasValue)
        //        return dt.Value.ToString(dateTimeFormats[formatIndex]);
        //    return null;
        //}

        // --------------------------------------------------------

        //static string RetrieveApplicationName()
        //{
        //    var process = Process.GetCurrentProcess();
        //    if (process != null)
        //    {
        //        var module = process.MainModule;
        //        if (module != null)
        //        {
        //            var file = module.FileName;
        //            if (!string.IsNullOrEmpty(file))
        //                return Path.GetFileNameWithoutExtension(file);
        //        }
        //    }
        //    return null;
        //}

        // --------------------------------------------------------

        static bool ParseDateTime(string text, string format, out DateTime result)
        {
            if (string.IsNullOrEmpty(format))
            {
                result = ConversionHelper.DateTimeZero;
                return false;
            }
            return DateTime.TryParseExact(text, format, /*IFormatProvider*/ null,
                DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result);
        }

        // --------------------------------------------------------

        bool ParseDateTime(string text, out DateTime result)
        {
            int i = 0;
            do
            {
                if (ParseDateTime(text, dateTimeFormats[i], out result))
                    return true;
            }
            while (++i < dateTimeFormats.Count);

            return false;
        }

        // --------------------------------------------------------

        void ReadDateTimeRule(XElement xmlRules, string attribute, ref DateTime dateTimeRule)
        {
            attribute = xmlRules.RetrieveAttribute(attribute, trimToNull: true);
            if (attribute != null)
            {
                DateTime rule;
                if (!ParseDateTime(attribute, out rule))
                    XmlHelper.ThrowCastException(attribute, "Дата/Время");
                dateTimeRule = rule;
            }
        }

        // --------------------------------------------------------
    }
}
