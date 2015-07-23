// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextRules.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The policy search field name resolver.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard
{
  using System;
  using System.Collections.ObjectModel;
  using System.Text;
  using System.Xml.Linq;
  using System.Xml.XPath;

  using rt.srz.database.business.standard.enums;
  using rt.srz.database.business.standard.helpers;

  // ��������� ���� �� ��� �����
  // [CLSCompliant(false)]
  /// <summary>
  ///   The policy search field name resolver.
  /// </summary>
  /// <param name="fieldName">
  ///   The field name.
  /// </param>
  public delegate FieldTypes PolicySearchFieldNameResolver(string fieldName);

  // ����� ������ ��� �������
  // [CLSCompliant(false)]
  /// <summary>
  ///   The text rules.
  /// </summary>
  public sealed class TextRules
  {
    // ������� ������� �� �����

    // ������� ����� �������� � ����
    #region Fields

    /// <summary>
    ///   The field name resolver.
    /// </summary>
    public readonly PolicySearchFieldNameResolver FieldNameResolver;

    /// <summary>
    ///   The global compact.
    /// </summary>
    public bool GlobalCompact = false;

    /// <summary>
    ///   The global trim.
    /// </summary>
    public bool GlobalTrim = false;

    // ��������� � �������� ��������
    /// <summary>
    ///   The global upper.
    /// </summary>
    public bool GlobalUpper = false;

    // �����, ����������� ��� ����
    // !! ����� ���� null

    /// <summary>
    ///   The converters.
    /// </summary>
    private Collection<StringConverterBase> converters;

    #endregion

    // --------------------------------------------------------

    // �������� �����������
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TextRules"/> class.
    /// </summary>
    /// <param name="rootRulesLoader">
    /// The root rules loader.
    /// </param>
    /// <param name="FieldNameResolver">
    /// The field name resolver.
    /// </param>
    public TextRules(XElement rootRulesLoader = null, PolicySearchFieldNameResolver FieldNameResolver = null)
    {
      this.FieldNameResolver = FieldNameResolver;

      if (rootRulesLoader != null)
      {
        LoadRules(rootRulesLoader);
      }
    }

    #endregion

    // ��������� ����� ������ �� xml-������������

    // �������� ��������� ������
    #region Public Methods and Operators

    /// <summary>
    /// The add string converter.
    /// </summary>
    /// <param name="converter">
    /// The converter.
    /// </param>
    public void AddStringConverter(StringConverterBase converter)
    {
      if (converters == null)
      {
        converters = new Collection<StringConverterBase>();
      }

      converters.Add(converter);
    }

    /// <summary>
    ///   The clear rules.
    /// </summary>
    public void ClearRules()
    {
      if (converters != null)
      {
        converters.Clear();
      }

      GlobalTrim = GlobalCompact = GlobalUpper = false;
    }

    /// <summary>
    /// The load rules.
    /// </summary>
    /// <param name="rootRulesLoader">
    /// The root rules loader.
    /// </param>
    /// <param name="clearExistingRules">
    /// The clear existing rules.
    /// </param>
    public void LoadRules(XElement rootRulesLoader, bool clearExistingRules = true)
    {
      if (clearExistingRules)
      {
        ClearRules();
      }

      foreach (var xmlGlobals in rootRulesLoader.XPathSelectElements("/���������"))
      {
        // ������ ���������� ���������
        GlobalTrim = xmlGlobals.RetrieveBool("��������������������", GlobalTrim);
        GlobalCompact = xmlGlobals.RetrieveBool("����������������", GlobalCompact);
        GlobalUpper = xmlGlobals.RetrieveBool("��������������", GlobalUpper);

        // �������� ��������
        foreach (var xmlRule in xmlGlobals.XPathSelectElements("�������������"))
        {
          var converter = new StringParamTrimmer(
            RetrieveStringMatching(xmlRule), 
            RetrieveSearchDirection(xmlRule), 
            RetrieveFlaggedOnly(xmlRule), 
            FieldNameResolver);
          AddAllowedFields(converter, xmlRule);
          AddStringConverter(converter);
        }

        // ������ �����
        foreach (var xmlRule in xmlGlobals.XPathSelectElements("��������"))
        {
          var converter = new StringParamReplacer(
            RetrieveStringMatching(xmlRule), 
            RetrieveReplacer(xmlRule), 
            RetrieveFlaggedOnly(xmlRule), 
            FieldNameResolver);
          AddAllowedFields(converter, xmlRule);
          AddStringConverter(converter);
        }
      }
    }

    // ��������� ��� ������ �� ����� �������������� �����
    // public void ResolveAllowedFields(PolicySearchFieldNameResolver resolver)
    // {
    // if (converters != null)
    // {
    // foreach (var converter in converters)
    // converter.AllowedFields.ResolveFields(resolver);
    // }
    // }

    // ��������� ��� ������ �� ����� �������������� �����
    // public void ResolveAllowedFields()
    // {
    // ResolveAllowedFields(FieldNameResolver);
    // }

    // �������� ���������� ������, ����� ������ ���������
    /// <summary>
    /// The prepare string.
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
    /// <param name="emptyToNull">
    /// The empty to null.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    /// <exception cref="InvalidCastException">
    /// </exception>
    public string PrepareString(
      string s, 
      FieldTypes field = FieldTypes.Undefined, 
      bool flagged = false, 
      bool emptyToNull = false)
    {
      if (s != null)
      {
        /*
                 * ������� ���������, ��� ����� �������������� �����-�� ����� ����� ���������, � �����-�� ����������
                 * � ���������� ����� ��������� ����� ������������������ ��������, ������� ���� �� ���� �������� ��������������
                 * ���� ����: �������� ����� "��������" ���������, �� ���� �����, ������� ��� �� ����� ��������, ���� � ���� ����� � ����� ��������� ��� �� �� ��������������
                 * 
                 * ��� ����� ����� ��������� �������������� � ����� �� ��� ���, ���� ���� ���-�� � ������ �������� (����������, � ������� �� ������������)
                 * �������� �������������� ������� ��� ������� ��������, �� ����� ��������, ��� � ���� ��� ������� ������, ������ ��� ������� ����� �������� ����� 2-3% ������� �� ��������� �����
                 */
        ushort cycles = 0;
        var original_s = s;
        do
        {
          // ������� �������� ������
          var saved_s = s;

          // �������������� � �������� ��������
          if (GlobalUpper)
          {
            s = s.ToUpper();
          }

          // ��������� ������������� ��������������
          if (converters != null)
          {
            foreach (var converter in converters)
            {
              s = converter.Convert(s, field, flagged);
            }
          }

          // ������ ������� �������
          if (GlobalTrim)
          {
            s = s.Trim();
          }

          // ������ ������ �������
          if (GlobalCompact)
          {
            s = TStringHelper.CompactString(s);
          }

          // ��������� ����, ����� ������� ��������� � ������ ������ ���
          if (string.Compare(s, saved_s, StringComparison.Ordinal) == 0)
          {
            break;
          }

          // ������ �� ������������
          if (++cycles > 10000)
          {
            throw new InvalidCastException(
              string.Format(
                            "TextRules.PrepareString: ������������! field: {0}, flagged: {1}, string: {2}", 
                            field, 
                            flagged, 
                            original_s));
          }
        }
        while (true);

        // �������� ������ ������ � null
        if (emptyToNull)
        {
          s = TStringHelper.StringToNull(s);
        }
      }

      return s;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The add allowed fields.
    /// </summary>
    /// <param name="converter">
    /// The converter.
    /// </param>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    private static void AddAllowedFields(StringConverterBase converter, XElement xmlNode)
    {
      converter.AllowedFields.AddFields(xmlNode.RetrieveAttribute("����������", true));
    }

    /// <summary>
    /// The retrieve flagged only.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool RetrieveFlaggedOnly(XElement xmlNode)
    {
      return xmlNode.RetrieveBool("���������������", false);
    }

    /// <summary>
    /// The retrieve replacer.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// </exception>
    private static string RetrieveReplacer(XElement xmlNode)
    {
      var replacer = xmlNode.RetrieveAttribute("��", trimToNull: false);
      if (replacer == null)
      {
        throw new ArgumentException("���������� ������ ��� �������� ������ '��'");
      }

      return replacer;
    }

    // �������� ���������� ������
    // public string PrepareString(string s, bool emptyToNull)
    // {
    // return PrepareString(s, FieldTypes.Undefined, flagged: false, emptyToNull: emptyToNull);
    // }

    // --------------------------------------------------------

    /// <summary>
    /// The retrieve search direction.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <returns>
    /// The <see cref="WardUnidimensional"/>.
    /// </returns>
    private static WardUnidimensional RetrieveSearchDirection(XElement xmlNode)
    {
      var direction = WardUnidimensional.None;
      if (xmlNode.RetrieveBool("�����", false))
      {
        direction |= WardUnidimensional.Back;
      }

      if (xmlNode.RetrieveBool("������", false))
      {
        direction |= WardUnidimensional.Forward;
      }

      return direction;
    }

    // --------------------------------------------------------

    /// <summary>
    /// The retrieve string matching.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <returns>
    /// The <see cref="StringMatchingBase"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// </exception>
    private static StringMatchingBase RetrieveStringMatching(XElement xmlNode)
    {
      var matcher = xmlNode.RetrieveAttribute("�������", true);
      if (matcher != null)
      {
        return new AnyCharacterMatching(matcher);
      }

      matcher = xmlNode.RetrieveAttribute("����", true);
      if (matcher != null)
      {
        var codes = matcher.Split(',');
        if (codes != null)
        {
          var res = new StringBuilder();
          var len = codes.Length;
          for (var i = 0; i < len; ++i)
          {
            var code = codes[i].Trim();
            if (code.Length > 0)
            {
              var ch = (char)int.Parse(code);
              res.Append(ch);
            }
          }

          if (res.Length > 0)
          {
            return new AnyCharacterMatching(res.ToString());
          }
        }
      }

      matcher = xmlNode.RetrieveAttribute("���������", true);
      if (matcher != null)
      {
        return new SubstringMatching(matcher);
      }

      throw new ArgumentException("���������� ��������� ������� ��� �������� ������");
    }

    #endregion

    // --------------------------------------------------------

    // --------------------------------------------------------
  }

  // --------------------------------------------------------

  /// <summary>
  ///   The substring matching.
  /// </summary>
  public sealed class SubstringMatching : StringMatchingBase
  {
    #region Fields

    /// <summary>
    ///   The matcher last pos.
    /// </summary>
    private readonly int matcherLastPos;

    /// <summary>
    ///   The matcher length.
    /// </summary>
    private readonly int matcherLength;

    /// <summary>
    ///   The search position.
    /// </summary>
    private int searchPosition = -1;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SubstringMatching"/> class.
    /// </summary>
    /// <param name="matcher">
    /// The matcher.
    /// </param>
    public SubstringMatching(string matcher)
      : base(matcher)
    {
      matcherLength = matcher.Length;
      matcherLastPos = matcherLength - 1;
    }

    #endregion

    // public override void ClearSearch()
    // {
    // searchPosition = -1;
    // }
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
    public override bool CheckCharMatch(char currChar)
    {
      if (matcher != null)
      {
        if (matcher[++searchPosition] == currChar)
        {
          if (searchPosition == matcherLastPos)
          {
            return true;
          }

          return false;
        }

        ClearSearch();
      }

      return false;
    }

    /// <summary>
    ///   The retrieve match length.
    /// </summary>
    /// <returns>
    ///   The <see cref="int" />.
    /// </returns>
    public override int RetrieveMatchLength()
    {
      return matcherLength;
    }

    #endregion
  }

  // ������� ������� ������ ������� ��� ��������� ��������
  // [CLSCompliant(false)]
  /// <summary>
  ///   The string param trimmer.
  /// </summary>
  public sealed class StringParamTrimmer : StringConverterBase
  {
    #region Fields

    /// <summary>
    ///   The direction.
    /// </summary>
    public readonly WardUnidimensional Direction;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="StringParamTrimmer"/> class.
    /// </summary>
    /// <param name="StringMatcher">
    /// The string matcher.
    /// </param>
    /// <param name="Direction">
    /// The direction.
    /// </param>
    /// <param name="FlaggedOnly">
    /// The flagged only.
    /// </param>
    /// <param name="FieldNameResolver">
    /// The field name resolver.
    /// </param>
    public StringParamTrimmer(
      StringMatchingBase StringMatcher, 
      WardUnidimensional Direction, 
      bool FlaggedOnly = false, 
      PolicySearchFieldNameResolver FieldNameResolver = null)
      : base(StringMatcher, FlaggedOnly, FieldNameResolver)
    {
      this.Direction = Direction;
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
    protected override int ProcessMatch(StringBuilder s, int matchStart, int matchLength)
    {
      int i;

      // ������� ������� �����
      if ((Direction & WardUnidimensional.Back) == WardUnidimensional.Back)
      {
        for (i = matchStart - 1; i >= 0; --i)
        {
          if (!char.IsWhiteSpace(s[i]))
          {
            break;
          }
        }

        var spaceLength = matchStart - (++i);
        if (spaceLength > 0)
        {
          s.Remove(i, spaceLength);
          matchStart = i;
        }
      }

      // ������� ������� ������
      var nextPos = matchStart + matchLength;
      if ((Direction & WardUnidimensional.Forward) == WardUnidimensional.Forward)
      {
        i = nextPos;
        for (var len = s.Length; i < len; ++i)
        {
          if (!char.IsWhiteSpace(s[i]))
          {
            break;
          }
        }

        var spaceLength = i - nextPos;
        if (spaceLength > 0)
        {
          s.Remove(nextPos, spaceLength);
        }
      }

      return nextPos;
    }

    #endregion
  }

  // �������� ������ ��� ��������� �������� �� ���������
  // [CLSCompliant(false)]
  /// <summary>
  ///   The string param replacer.
  /// </summary>
  public sealed class StringParamReplacer : StringConverterBase
  {
    #region Fields

    /// <summary>
    ///   The replacer.
    /// </summary>
    public readonly string Replacer;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="StringParamReplacer"/> class.
    /// </summary>
    /// <param name="StringMatcher">
    /// The string matcher.
    /// </param>
    /// <param name="Replacer">
    /// The replacer.
    /// </param>
    /// <param name="FlaggedOnly">
    /// The flagged only.
    /// </param>
    /// <param name="FieldNameResolver">
    /// The field name resolver.
    /// </param>
    public StringParamReplacer(
      StringMatchingBase StringMatcher, 
      string Replacer, 
      bool FlaggedOnly = false, 
      PolicySearchFieldNameResolver FieldNameResolver = null)
      : base(StringMatcher, FlaggedOnly, FieldNameResolver)
    {
      this.Replacer = Replacer;
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
    protected override int ProcessMatch(StringBuilder s, int matchStart, int matchLength)
    {
      s.Remove(matchStart, matchLength).Insert(matchStart, Replacer);
      return matchStart + Replacer.Length;
    }

    #endregion
  }

  // ���� �������
  // public enum QuotationMark
  // {
  // None,
  // Apostrophe,
  // DoubleQuotes,
  // }

  /// <summary>
  ///   The any character matching.
  /// </summary>
  public sealed class AnyCharacterMatching : StringMatchingBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AnyCharacterMatching"/> class.
    /// </summary>
    /// <param name="matcher">
    /// The matcher.
    /// </param>
    public AnyCharacterMatching(string matcher)
      : base(matcher)
    {
    }

    #endregion

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
    public override bool CheckCharMatch(char currChar)
    {
      return matcher.IndexOf(currChar) >= 0;
    }

    /// <summary>
    ///   The retrieve match length.
    /// </summary>
    /// <returns>
    ///   The <see cref="int" />.
    /// </returns>
    public override int RetrieveMatchLength()
    {
      return 1; // !! ������ ���� ������
    }

    #endregion
  }
}