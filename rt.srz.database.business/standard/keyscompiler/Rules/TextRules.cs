namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  using System;
  using System.Collections.ObjectModel;
  using System.Text;
  using System.Xml.Linq;
  using System.Xml.XPath;

  using rt.srz.database.business.standard.keyscompiler.Fields;

  // набор правил для текстов
    //[CLSCompliant(false)]
    public sealed class TextRules
    {
        

        // убирать пробелы по бокам
        public bool GlobalTrim = false;

        // сжимать много пробелов в один
        public bool GlobalCompact = false;

        // приводить к верхнему регистру
        public bool GlobalUpper = false;

        // метод, разрешающий имя поля
        // !! может быть null
        public readonly SearchFieldNameResolver FieldNameResolver;

        Collection<StringConverterBase> converters = null;

        // --------------------------------------------------------

        // основной конструктор
        public TextRules(XElement rootRulesLoader = null, SearchFieldNameResolver FieldNameResolver = null)
        {
            this.FieldNameResolver = FieldNameResolver;

            if (rootRulesLoader != null)
                LoadRules(rootRulesLoader);
        }

        // загрузить набор правил из xml-конфигурации
        public void LoadRules(XElement rootRulesLoader, bool clearExistingRules = true)
        {
            if (clearExistingRules)
                ClearRules();

            foreach (var xmlGlobals in rootRulesLoader.XPathSelectElements("/Текстовые"))
            {
                // грузим глобальные настройки
                GlobalTrim = XmlHelper.RetrieveBool(xmlGlobals, "УбратьБоковыеПробелы", GlobalTrim);
                GlobalCompact = XmlHelper.RetrieveBool(xmlGlobals, "СхлопнутьПробелы", GlobalCompact);
                GlobalUpper = XmlHelper.RetrieveBool(xmlGlobals, "ВерхнийРегистр", GlobalUpper);
                // триммеры пробелов
                foreach (var xmlRule in xmlGlobals.XPathSelectElements("УбратьПробелы"))
                {
                    var converter = new StringParamTrimmer(RetrieveStringMatching(xmlRule), RetrieveSearchDirection(xmlRule), RetrieveFlaggedOnly(xmlRule), FieldNameResolver);
                    AddAllowedFields(converter, xmlRule);
                    AddStringConverter(converter);
                }
                // замена строк
                foreach (var xmlRule in xmlGlobals.XPathSelectElements("Заменить"))
                {
                    var converter = new StringParamReplacer(RetrieveStringMatching(xmlRule), RetrieveReplacer(xmlRule), RetrieveFlaggedOnly(xmlRule), FieldNameResolver);
                               AddAllowedFields(converter, xmlRule);
                                AddStringConverter(converter);
                }
            }
        }

        // очистить набор правил
        public void ClearRules()
        {
            if (converters != null)
                converters.Clear();
            GlobalTrim = GlobalCompact = GlobalUpper = false;
        }

        // добавить конвертер строки
        public void AddStringConverter(StringConverterBase converter)
        {
            if (converters == null)
                converters = new Collection<StringConverterBase>();
            converters.Add(converter);
        }

      

        // провести подготовку строки, задав тонкие настройки
        public string PrepareString(string s, FieldTypes field = FieldTypes.Undefined, bool flagged = false, bool emptyToNull = false)
        {
            if (s != null)
            {
                /*
                 * следует учитывать, что после преобразований какие-то буквы могут исчезнуть, а какие-то добавиться
                 * в результате может сложиться новая последовательность символов, которая сама по себе подлежит преобразованию
                 * наша цель: получить некий "конечный" результат, то есть такой, который уже не будет меняться, если к нему снова и снова применить все те же преобразования
                 * 
                 * для этого будем повторять преобразования в цикле до тех пор, пока хоть что-то в строке меняется (разумеется, с защитой от зацикливания)
                 * поначалу предполагалось сделать кеш готовых значений, но тесты показали, что в этом нет особого смысла, потому что наличие цикла добавило всего 2-3% времени на обработку файла
                 */
                ushort cycles = 0;
                string original_s = s;
                do
                {
                    // текущее значение строки
                    string saved_s = s;
                    // преобразование к верхнему регистру
                    if (GlobalUpper)
                        s = s.ToUpper();
                    // применить настраиваемые преобразования
                    if (converters != null)
                    {
                        foreach (var converter in converters)
                            s = converter.Convert(s, field, flagged);
                    }
                    // убрать боковые пробелы
                    if (GlobalTrim)
                        s = s.Trim();
                    // убрать лишние пробелы
                    if (GlobalCompact)
                        s = TStringHelper.CompactString(s);
                    // прерываем цикл, когда никаких изменений в строке больше нет
                    if (string.Compare(s, saved_s, StringComparison.Ordinal) == 0)
                        break;
                    // защита от зацикливания
                    if (++cycles > 10000)
                        throw new InvalidCastException(string.Format("TextRules.PrepareString: зацикливание! field: {0}, flagged: {1}, string: {2}", field, flagged, original_s));
                }
                while (true);
                // привести пустую строку к null
                if (emptyToNull)
                    s = TStringHelper.StringToNull(s);
            }
            return s;
        }

        // провести подготовку строки
        //public string PrepareString(string s, bool emptyToNull)
        //{
        //    return PrepareString(s, FieldTypes.Undefined, flagged: false, emptyToNull: emptyToNull);
        //}

        // --------------------------------------------------------

        static WardUnidimensional RetrieveSearchDirection(XElement xmlNode)
        {
            WardUnidimensional direction = WardUnidimensional.None;
            if (XmlHelper.RetrieveBool(xmlNode, "Слева", false))
                direction |= WardUnidimensional.Back;
            if (XmlHelper.RetrieveBool(xmlNode, "Справа", false))
                direction |= WardUnidimensional.Forward;
            return direction;
        }

        // --------------------------------------------------------

        static StringMatchingBase RetrieveStringMatching(XElement xmlNode)
        {
            string matcher = XmlHelper.RetrieveAttribute(xmlNode, "Символы", trimToNull: true);
            if (matcher != null)
                return new AnyCharacterMatching(matcher);
            matcher = XmlHelper.RetrieveAttribute(xmlNode, "Коды", trimToNull: true);
            if (matcher != null)
            {
                string[] codes = matcher.Split(',');
                if (codes != null)
                {
                    StringBuilder res = new StringBuilder();
                    int len = codes.Length;
                    for (int i = 0; i < len; ++i)
                    {
                        string code = codes[i].Trim();
                        if (code.Length > 0)
                        {
                            char ch = (char)int.Parse(code);
                            res.Append(ch);
                        }
                    }
                    if (res.Length > 0)
                        return new AnyCharacterMatching(res.ToString());
                }
            }
            matcher = XmlHelper.RetrieveAttribute(xmlNode, "Сочетание", trimToNull: true);
            if (matcher != null)
                return new SubstringMatching(matcher);
            throw new ArgumentException("Обнаружено текстовое правило без атрибута поиска");
        }

        // --------------------------------------------------------

        static string RetrieveReplacer(XElement xmlNode)
        {
            string replacer = XmlHelper.RetrieveAttribute(xmlNode, "На", trimToNull: false);
            if (replacer == null)
                throw new ArgumentException("Обнаружена Замена без атрибута замены 'На'");
            return replacer;
        }

        // --------------------------------------------------------

        static bool RetrieveFlaggedOnly(XElement xmlNode)
        {
            return XmlHelper.RetrieveBool(xmlNode, "ТолькоНестрогий", false);
        }

        // --------------------------------------------------------

        static void AddAllowedFields(StringConverterBase converter, XElement xmlNode)
        {
            converter.AllowedFields.AddFields(XmlHelper.RetrieveAttribute(xmlNode, "ТолькоПоля", trimToNull: true));
        }

        // --------------------------------------------------------
    }

    // --------------------------------------------------------

    public sealed class SubstringMatching : StringMatchingBase
    {
        readonly int matcherLength;
        readonly int matcherLastPos;
        int searchPosition = -1;

        public SubstringMatching(string matcher)
            : base(matcher)
        {
            matcherLength = matcher.Length;
            matcherLastPos = matcherLength - 1;
        }

        //public override void ClearSearch()
        //{
        //    searchPosition = -1;
        //}

        public override bool CheckCharMatch(char currChar)
        {
            if (matcher != null)
            {
                if (matcher[++searchPosition] == currChar)
                {
                    if (searchPosition == matcherLastPos)
                        return true;
                    return false;
                }
                ClearSearch();
            }
            return false;
        }

        public override int RetrieveMatchLength()
        {
            return matcherLength;
        }
    }

    // убирает пробелы вокруг символа или сочетания символов
    //[CLSCompliant(false)]
    public sealed class StringParamTrimmer : StringConverterBase
    {
        public readonly WardUnidimensional Direction;

        public StringParamTrimmer(StringMatchingBase StringMatcher, WardUnidimensional Direction, bool FlaggedOnly = false, SearchFieldNameResolver FieldNameResolver = null)
            : base(StringMatcher, FlaggedOnly, FieldNameResolver)
        {
            this.Direction = Direction;
        }

        // отработать совпадение
        // <returns>возвращает следующую позицию после изменения (если не было изменений, то это matchStart + matchLength)</returns>
        protected override int ProcessMatch(StringBuilder s, int matchStart, int matchLength)
        {
            int i;
            // убираем пробелы слева
            if ((Direction & WardUnidimensional.Back) == WardUnidimensional.Back)
            {
                for (i = matchStart - 1; i >= 0; --i)
                {
                    if (!char.IsWhiteSpace(s[i]))
                        break;
                }
                int spaceLength = matchStart - (++i);
                if (spaceLength > 0)
                {
                    s.Remove(i, spaceLength);
                    matchStart = i;
                }
            }
            // убираем пробелы справа
            int nextPos = matchStart + matchLength;
            if ((Direction & WardUnidimensional.Forward) == WardUnidimensional.Forward)
            {
                i = nextPos;
                for (int len = s.Length; i < len; ++i)
                {
                    if (!char.IsWhiteSpace(s[i]))
                        break;
                }
                int spaceLength = i - nextPos;
                if (spaceLength > 0)
                    s.Remove(nextPos, spaceLength);
            }
            return nextPos;
        }
    }

    // заменяет символ или сочетание символов на подстроку
    //[CLSCompliant(false)]
    public sealed class StringParamReplacer : StringConverterBase
    {
        public readonly string Replacer;

        public StringParamReplacer(StringMatchingBase StringMatcher, string Replacer, bool FlaggedOnly = false, SearchFieldNameResolver FieldNameResolver = null)
            : base(StringMatcher, FlaggedOnly, FieldNameResolver)
        {
            this.Replacer = Replacer;
        }

        // отработать совпадение
        // <returns>возвращает следующую позицию после изменения (если не было изменений, то это matchStart + matchLength)</returns>
        protected override int ProcessMatch(StringBuilder s, int matchStart, int matchLength)
        {
            s.Remove(matchStart, matchLength).Insert(matchStart, Replacer);
            return matchStart + Replacer.Length;
        }
    }

    // виды кавычек
    //public enum QuotationMark
    //{
    //    None,
    //    Apostrophe,
    //    DoubleQuotes,
    //}

    public sealed class AnyCharacterMatching : StringMatchingBase
    {
        public AnyCharacterMatching(string matcher)
            : base(matcher)
        {
        }

        public override bool CheckCharMatch(char currChar)
        {
            return (matcher.IndexOf(currChar) >= 0);
        }

        public override int RetrieveMatchLength()
        {
            return 1; // !! всегда один символ
        }
    }
}
