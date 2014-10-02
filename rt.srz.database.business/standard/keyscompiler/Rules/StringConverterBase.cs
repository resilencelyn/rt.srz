namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  using System.Text;

  using rt.srz.database.business.standard.keyscompiler.Fields;

  // --------------------------------------------------------

    public abstract class StringConverterBase
    {
        public readonly StringMatchingBase StringMatcher;
        public readonly bool FlaggedOnly; // работать только в режиме flagged
        // если список не пуст, работает только для заданных полей
        public readonly PolicySearchFields AllowedFields;

        // --------------------------------------------------------

        // конвертировать строку
        public string Convert(string s, FieldTypes field = FieldTypes.Undefined, bool flagged = false)
        {
            if (string.IsNullOrEmpty(s) || StringMatcher == null)
                return s;

            // режим flagged
            if (FlaggedOnly)
            {
                if (!flagged)
                    return s;
            }

            // режим для конкретных полей
            if (field != FieldTypes.Undefined)
            {
                if (!AllowedFields.EmptyOrContainsField(field))
                    return s;
            }

            // проходим по заданной строке посимвольно и на все найденные совпадения вызываем преобразователь
            StringBuilder result = null;
            for (int i = 0, len = s.Length; i < len; )
            {
                // ищем очередное совпадение
                StringMatcher.ClearSearch();
                if (result != null)
                {
                    do
                    {
                        if (StringMatcher.CheckCharMatch(result[i]))
                            break;
                    }
                    while (++i < len);
                }
                else
                {
                    do
                    {
                        if (StringMatcher.CheckCharMatch(s[i]))
                            break;
                    }
                    while (++i < len);
                }

                // если найдено совпадение, тогда индекс будет меньше длины строки
                if (i < len)
                {
                    if (result == null)
                        result = new StringBuilder(s);

                    int matchLength = StringMatcher.RetrieveMatchLength();
                    int matchStart = i - matchLength + 1;

                    // отрабатываем совпадение и получаем новую позицию
                    i = ProcessMatch(result, matchStart, matchLength);
                    len = result.Length;
                }
            }

            // готово
            return (result != null) ? result.ToString() : s;
        }

        // --------------------------------------------------------

        protected StringConverterBase(StringMatchingBase StringMatcher, bool FlaggedOnly = false, SearchFieldNameResolver FieldNameResolver = null)
        {
            this.StringMatcher = StringMatcher;
            this.FlaggedOnly = FlaggedOnly;
            this.AllowedFields = new PolicySearchFields(FieldNameResolver);
        }

        // отработать совпадение
        // <returns>возвращает следующую позицию после изменения (если не было изменений, то это matchStart + matchLength)</returns>
        protected abstract int ProcessMatch(StringBuilder s, int matchStart, int matchLength);

    }

    // --------------------------------------------------------

    public abstract class StringMatchingBase
    {
        // !! никогда не пуст, но может равняться null
        readonly public string matcher;

        // --------------------------------------------------------

        public virtual void ClearSearch()
        {
            //...пусто
        }

        public abstract bool CheckCharMatch(char currChar);
        public abstract int RetrieveMatchLength();

        // --------------------------------------------------------

        protected StringMatchingBase(string matcher)
        {
            this.matcher = TStringHelper.StringToNull(matcher);
        }

    }

    // --------------------------------------------------------
}
