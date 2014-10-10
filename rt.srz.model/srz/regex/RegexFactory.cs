// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegexFactory.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The regex factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.regex
{
  using System.Text.RegularExpressions;

  using rt.srz.model.Properties;

  /// <summary>
  /// The regex factory.
  /// </summary>
  public class RegexFactory
  {
    #region Static Fields

    /// <summary>
    /// The regex birthplace.
    /// </summary>
    private static Regex regexBirthplace;

    /// <summary>
    /// The regex docoment number blok.
    /// </summary>
    private static Regex regexDocomentNumberBlok;

    /// <summary>
    /// The regex docoment seria blok.
    /// </summary>
    private static Regex regexDocomentSeriaBlok;

    /// <summary>
    /// The regex fio.
    /// </summary>
    private static Regex regexFio;

    /// <summary>
    /// The regex issue by.
    /// </summary>
    private static Regex regexIssueBy;

    /// <summary>
    /// The regex replace double symb.
    /// </summary>
    private static Regex regexReplaceDoubleSymb;

    /// <summary>
    /// The regex replace spaces.
    /// </summary>
    private static Regex regexReplaceSpaces;

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the regex birthplace.
    /// </summary>
    public static Regex RegexBirthplace
    {
      get
      {
        return regexBirthplace ?? (regexBirthplace = new Regex(Resource.RegexBirthplace));
      }
    }

    /// <summary>
    /// Gets the regex docoment number blok.
    /// </summary>
    public static Regex RegexDocomentNumberBlok
    {
      get
      {
        return regexDocomentNumberBlok ?? (regexDocomentNumberBlok = new Regex(Resource.RegexDocomentNumberBlok));
      }
    }

    /// <summary>
    /// Gets the regex docoment seria blok.
    /// </summary>
    public static Regex RegexDocomentSeriaBlok
    {
      get
      {
        return regexDocomentSeriaBlok ?? (regexDocomentSeriaBlok = new Regex(Resource.RegexDocomentSeriaBlok));
      }
    }

    /// <summary>
    /// Gets the regex fio.
    /// </summary>
    public static Regex RegexFio
    {
      get
      {
        return regexFio ?? (regexFio = new Regex(Resource.RegexFio));
      }
    }

    /// <summary>
    /// Gets the regex issuing authority.
    /// </summary>
    public static Regex RegexIssuingAuthority
    {
      get
      {
        return regexIssueBy ?? (regexIssueBy = new Regex(Resource.RegexIssuingAuthority));
      }
    }

    /// <summary>
    /// Gets the regex replace double symb.
    /// </summary>
    public static Regex RegexReplaceDoubleSymb
    {
      get
      {
        return regexReplaceDoubleSymb ?? (regexReplaceDoubleSymb = new Regex(Resource.RegexReplaceDoubleSymb));
      }
    }

    /// <summary>
    /// Gets the regex replace spaces.
    /// </summary>
    public static Regex RegexReplaceSpaces
    {
      get
      {
        return regexReplaceSpaces ?? (regexReplaceSpaces = new Regex(Resource.RegexReplaceSpaces));
      }
    }

    #endregion
  }
}