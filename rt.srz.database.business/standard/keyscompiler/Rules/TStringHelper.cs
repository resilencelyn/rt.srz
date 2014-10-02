namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  using System;
  using System.Text;

  public static class TStringHelper
    {
     
        //---------------------------------------------------------------------------------------------------------------

        #region IsNullOrEmpty

        // IsNullOrEmpty: универсальная перегрузка (для String, StringBuilder...)
        public static bool IsNullOrEmpty(String str)
        {
            return (str == null || str.Length < 1);
        }

        #endregion

        //---------------------------------------------------------------------------------------------------------------

        #region empty strings

        // привести строку к заданному виду, если она ПУСТАЯ
        // !! непустые строки возвращаются как-есть, с наложением условия bTrimFirst
        public static string StringShrink(string String, TEmptyStringTypes EmptyType, bool bTrimFirst)
        {
            if (bTrimFirst && String != null)
                String = String.Trim();
            if (string.IsNullOrEmpty(String))
            {
                switch (EmptyType)
                {
                    case TEmptyStringTypes.Empty:
                        return string.Empty;
                    case TEmptyStringTypes.Null:
                        return null;
                    case TEmptyStringTypes.Signature:
                        return "null";
                }
            }
            return String;
        }

       
        public static string StringToNull(string String, bool bTrimFirst)
        {
            return StringShrink(String, TEmptyStringTypes.Null, bTrimFirst);
        }

        // упрощенная версия StringToNull()
        // !! bTrimFirst == false
        public static string StringToNull(string String)
        {
            return StringToNull(String, /*bTrimFirst*/ false);
        }

        // возвратить string.Empty, если на входе пустая строка (в т.ч. null)
        // !! непустые строки возвращаются как-есть, с наложением условия bTrimFirst
        public static string StringToEmpty(string String, bool bTrimFirst)
        {
            return StringShrink(String, TEmptyStringTypes.Empty, bTrimFirst);
        }

       

        #endregion

    
        //---------------------------------------------------------------------------------------------------------------

        #region string shrink
        

        // уплотнить строку, заменяя все множественные пробелы/табуляции/и т.п. на заданный символ
        // !! можно задать Space = '\0', чтобы схлопнуть "в ноль"
        public static string CompactString(string String, char Space)
        {
            if (!IsNullOrEmpty(String))
            {
                StringBuilder result = null;
                int start = 0, space = -1, len = String.Length;
                for (int i = 0; i < len; ++i)
                {
                    if (char.IsWhiteSpace(String, i))
                    {
                        if (space < 0)
                            space = i;
                    }
                    else
                    {
                        if (space >= 0)
                        {
                            // оптимизация: если пробел только один и он равен заданному Space-символу, ничего делать не надо
                            if (i - space == 1 && String[space] == Space)
                            {
                                space = -1;
                                continue;
                            }
                            if (result == null)
                                result = new StringBuilder();
                            result.Append(String.Substring(start, space - start));
                            if (Space != '\0')
                                result.Append(Space);
                            start = i;
                            space = -1;
                        }
                    }
                }
                // если что-то заменяли, возвращаем полученную строку
                if (result != null)
                {
                    if (start < len)
                        result.Append(String.Substring(start));
                    return result.ToString();
                }
            }
            return String;
        }

        // упрощенный вызов CompactString()
        // !! Space == ' '
        public static string CompactString(string String)
        {
            return CompactString(String, /*Space*/ ' ');
        }

        #endregion

    }
}
