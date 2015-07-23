// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringConverterBase.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The string converter base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  using System.Text;

  using rt.srz.database.business.standard.keyscompiler.Fields;

  // --------------------------------------------------------

  /// <summary>
  /// The string converter base.
  /// </summary>
  public abstract class StringConverterBase
  {
    // если список не пуст, работает только для заданных полей
    #region Fields

    /// <summary>
    /// The allowed fields.
    /// </summary>
    public readonly PolicySearchFields AllowedFields;

    /// <summary>
    /// The flagged only.
    /// </summary>
    public readonly bool FlaggedOnly; // работать только в режиме flagged

    /// <summary>
    /// The string matcher.
    /// </summary>
    public readonly StringMatchingBase StringMatcher;

    #endregion

    // --------------------------------------------------------

    // конвертировать строку

    // --------------------------------------------------------
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="StringConverterBase"/> class.
    /// </summary>
    /// <param name="StringMatcher">
    /// The string matcher.
    /// </param>
    /// <param name="FlaggedOnly">
    /// The flagged only.
    /// </param>
    /// <param name="FieldNameResolver">
    /// The field name resolver.
    /// </param>
    protected StringConverterBase(
      StringMatchingBase StringMatcher, 
      bool FlaggedOnly = false, 
      SearchFieldNameResolver FieldNameResolver = null)
    {
      this.StringMatcher = StringMatcher;
      this.FlaggedOnly = FlaggedOnly;
      AllowedFields = new PolicySearchFields(FieldNameResolver);
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The convert.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="field">
    /// The field.
    /// </param>
    /// <param name="flagged">
    /// The flagged.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string Convert(string s, FieldTypes field = FieldTypes.Undefined, bool flagged = false)
    {
      if (string.IsNullOrEmpty(s) || StringMatcher == null)
      {
        return s;
      }

      // режим flagged
      if (FlaggedOnly)
      {
        if (!flagged)
        {
          return s;
        }
      }

      // режим для конкретных полей
      if (field != FieldTypes.Undefined)
      {
        if (!AllowedFields.EmptyOrContainsField(field))
        {
          return s;
        }
      }

      // проходим по заданной строке посимвольно и на все найденные совпадения вызываем преобразователь
      StringBuilder result = null;
      for (int i = 0, len = s.Length; i < len;)
      {
        // ищем очередное совпадение
        StringMatcher.ClearSearch();
        if (result != null)
        {
          do
          {
            if (StringMatcher.CheckCharMatch(result[i]))
            {
              break;
            }
          }
          while (++i < len);
        }
        else
        {
          do
          {
            if (StringMatcher.CheckCharMatch(s[i]))
            {
              break;
            }
          }
          while (++i < len);
        }

        // если найдено совпадение, тогда индекс будет меньше длины строки
        if (i < len)
        {
          if (result == null)
          {
            result = new StringBuilder(s);
          }

          var matchLength = StringMatcher.RetrieveMatchLength();
          var matchStart = i - matchLength + 1;

          // отрабатываем совпадение и получаем новую позицию
          i = ProcessMatch(result, matchStart, matchLength);
          len = result.Length;
        }
      }

      // готово
      return (result != null) ? result.ToString() : s;
    }

    #endregion

    // отработать совпадение
    // <returns>возвращает следующую позицию после изменения (если не было изменений, то это matchStart + matchLength)</returns>
    #region Methods

    /// <summary>
    /// The process match.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="matchStart">
    /// The match start.
    /// </param>
    /// <param name="matchLength">
    /// The match length.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    protected abstract int ProcessMatch(StringBuilder s, int matchStart, int matchLength);

    #endregion
  }

  // --------------------------------------------------------

  /// <summary>
  /// The string matching base.
  /// </summary>
  public abstract class StringMatchingBase
  {
    // !! никогда не пуст, но может равняться null
    #region Fields

    /// <summary>
    /// The matcher.
    /// </summary>
    public readonly string matcher;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="StringMatchingBase"/> class.
    /// </summary>
    /// <param name="matcher">
    /// The matcher.
    /// </param>
    protected StringMatchingBase(string matcher)
    {
      this.matcher = TStringHelper.StringToNull(matcher);
    }

    #endregion

    // --------------------------------------------------------
    #region Public Methods and Operators

    /// <summary>
    /// The check char match.
    /// </summary>
    /// <param name="currChar">
    /// The curr char.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public abstract bool CheckCharMatch(char currChar);

    /// <summary>
    /// The clear search.
    /// </summary>
    public virtual void ClearSearch()
    {
      // ...пусто
    }

    /// <summary>
    /// The retrieve match length.
    /// </summary>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public abstract int RetrieveMatchLength();

    #endregion

    // --------------------------------------------------------
  }

  // --------------------------------------------------------
}