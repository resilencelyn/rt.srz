namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  using System;

  public static class ConversionHelper
    {
        // формат вывода даты/времени
        public static readonly string DateTimeFormat = @"yyyy'/'MM'/'dd' 'HH':'mm':'ss'.'fff";
        // нулевые дата/время
        public static readonly DateTime DateTimeZero = new DateTime();

        // --------------------------------------------------------

        #region publics

        public static BooleanFlag StringToBool(string strValue)
        {
            if (strValue != null)
            {
                switch (strValue.ToUpper())
                {
                    case "1":
                    case "TRUE":
                    case "ДА":
                        return BooleanFlag.True;
                    case "FALSE":
                    case "НЕТ":
                    case "0":
                        return BooleanFlag.False;
                }
            }
            return BooleanFlag.Unknown;
        }

       
        #endregion

    }
}
