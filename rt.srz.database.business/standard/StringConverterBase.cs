// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringConverterBase.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The string converter base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard
{
  using System.Text;

  using rt.srz.database.business.standard.helpers;

  // --------------------------------------------------------

  /// <summary>
  ///   The string converter base.
  /// </summary>
  public abstract class StringConverterBase
  {
    // ���� ������ �� ����, �������� ������ ��� �������� �����
    #region Fields

    /// <summary>
    ///   The allowed fields.
    /// </summary>
    public readonly PolicySearchFields AllowedFields;

    /// <summary>
    ///   The flagged only.
    /// </summary>
    public readonly bool FlaggedOnly; // �������� ������ � ������ flagged

    /// <summary>
    ///   The string matcher.
    /// </summary>
    public readonly StringMatchingBase StringMatcher;

    #endregion

    // --------------------------------------------------------

    // �������������� ������

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
      PolicySearchFieldNameResolver FieldNameResolver = null)
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

      // ����� flagged
      if (FlaggedOnly)
      {
        if (!flagged)
        {
          return s;
        }
      }

      // ����� ��� ���������� �����
      if (field != FieldTypes.Undefined)
      {
        if (!AllowedFields.EmptyOrContainsField(field))
        {
          return s;
        }
      }

      // �������� �� �������� ������ ����������� � �� ��� ��������� ���������� �������� ���������������
      StringBuilder result = null;
      for (int i = 0, len = s.Length; i < len;)
      {
        // ���� ��������� ����������
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

        // ���� ������� ����������, ����� ������ ����� ������ ����� ������
        if (i < len)
        {
          if (result == null)
          {
            result = new StringBuilder(s);
          }

          var matchLength = StringMatcher.RetrieveMatchLength();
          var matchStart = i - matchLength + 1;

          // ������������ ���������� � �������� ����� �������
          i = ProcessMatch(result, matchStart, matchLength);
          len = result.Length;
        }
      }

      // ������
      return (result != null) ? result.ToString() : s;
    }

    #endregion

    // ���������� ����������
    // <returns>���������� ��������� ������� ����� ��������� (���� �� ���� ���������, �� ��� matchStart + matchLength)</returns>
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
  ///   The string matching base.
  /// </summary>
  public abstract class StringMatchingBase
  {
    // !! ������� �� ����, �� ����� ��������� null
    #region Fields

    /// <summary>
    ///   The matcher.
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
    ///   The clear search.
    /// </summary>
    public virtual void ClearSearch()
    {
      // ...�����
    }

    /// <summary>
    ///   The retrieve match length.
    /// </summary>
    /// <returns>
    ///   The <see cref="int" />.
    /// </returns>
    public abstract int RetrieveMatchLength();

    #endregion

    // --------------------------------------------------------
  }

  // --------------------------------------------------------
}