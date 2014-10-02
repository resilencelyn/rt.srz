//-------------------------------------------------------------------------------------
// <copyright file="RegexFactory.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

using System.Text.RegularExpressions;
using rt.srz.model.Properties;

namespace rt.srz.model.regex
{
  public class RegexFactory
  {
    private static Regex regexBirthplace;
    private static Regex regexFio;
    private static Regex regexReplaceDoubleSymb;
    private static Regex regexReplaceSpaces;
    private static Regex regexDocomentSeriaBlok;
    private static Regex regexDocomentNumberBlok;
    private static Regex regexIssueBy;

    public static Regex RegexBirthplace
    {
      get
      {
        return regexBirthplace ?? (regexBirthplace = new Regex(Resource.RegexBirthplace));
      }
    }

    public static Regex RegexFio
    {
      get
      {
        return regexFio ?? (regexFio = new Regex(Resource.RegexFio));
      }
    }

    public static Regex RegexReplaceDoubleSymb
    {
      get
      {
        return regexReplaceDoubleSymb ?? (regexReplaceDoubleSymb = new Regex(Resource.RegexReplaceDoubleSymb));
      }
    }

    public static Regex RegexReplaceSpaces
    {
      get
      {
        return regexReplaceSpaces ?? (regexReplaceSpaces = new Regex(Resource.RegexReplaceSpaces));
      }
    }

    public static Regex RegexDocomentSeriaBlok
    {
      get
      {
        return regexDocomentSeriaBlok ?? (regexDocomentSeriaBlok = new Regex(Resource.RegexDocomentSeriaBlok));
      }
    }

    public static Regex RegexDocomentNumberBlok
    {
      get
      {
        return regexDocomentNumberBlok ?? (regexDocomentNumberBlok = new Regex(Resource.RegexDocomentNumberBlok));
      }
    }

    public static Regex RegexIssuingAuthority
    {
      get { return regexIssueBy ?? (regexIssueBy = new Regex(Resource.RegexIssuingAuthority)); }
    }

  }
}
