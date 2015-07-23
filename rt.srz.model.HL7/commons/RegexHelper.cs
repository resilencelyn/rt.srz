// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegexHelper.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The regex helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.commons
{
  #region references

  using System.Collections.Generic;
  using System.Text.RegularExpressions;

  #endregion

  /// <summary>
  ///   The regex helper.
  /// </summary>
  public static class RegexHelper
  {
    #region Public Methods and Operators

    /// <summary>
    /// The enumerate matches.
    /// </summary>
    /// <param name="matchCollection">
    /// The match collection.
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable"/>.
    /// </returns>
    public static IEnumerable<Match> EnumerateMatches(this MatchCollection matchCollection)
    {
      var count = matchCollection.Count;
      var iteratorVariable1 = 0;
      while (true)
      {
        if (iteratorVariable1 >= count)
        {
          yield break;
        }

        yield return matchCollection[iteratorVariable1];
        iteratorVariable1++;
      }
    }

    /// <summary>
    /// The retrieve regex options.
    /// </summary>
    /// <param name="ignoreCase">
    /// The ignore case.
    /// </param>
    /// <returns>
    /// The <see cref="RegexOptions"/>.
    /// </returns>
    public static RegexOptions RetrieveRegexOptions(bool ignoreCase = false)
    {
      var compiled = RegexOptions.Compiled;
      if (ignoreCase)
      {
        compiled |= RegexOptions.IgnoreCase;
      }

      return compiled;
    }

    /// <summary>
    /// The retrieve regex options ic.
    /// </summary>
    /// <param name="ignoreCase">
    /// The ignore case.
    /// </param>
    /// <returns>
    /// The <see cref="RegexOptions"/>.
    /// </returns>
    public static RegexOptions RetrieveRegexOptionsIC(bool ignoreCase = false)
    {
      var flag = true;
      return RetrieveRegexOptions(flag);
    }

    /// <summary>
    /// The try create regex.
    /// </summary>
    /// <param name="regexPattern">
    /// The regex pattern.
    /// </param>
    /// <param name="ignoreCase">
    /// The ignore case.
    /// </param>
    /// <returns>
    /// The <see cref="Regex"/>.
    /// </returns>
    public static Regex TryCreateRegex(string regexPattern, bool ignoreCase = false)
    {
      return TryCreateRegex(regexPattern, RetrieveRegexOptions(ignoreCase));
    }

    /// <summary>
    /// The try create regex.
    /// </summary>
    /// <param name="regexPattern">
    /// The regex pattern.
    /// </param>
    /// <param name="regexOptions">
    /// The regex options.
    /// </param>
    /// <returns>
    /// The <see cref="Regex"/>.
    /// </returns>
    public static Regex TryCreateRegex(string regexPattern, RegexOptions regexOptions)
    {
      if (string.IsNullOrEmpty(regexPattern))
      {
        return null;
      }

      return new Regex(regexPattern, regexOptions);
    }

    #endregion

    // [CompilerGenerated]
    // private sealed class <EnumerateMatches>d__0 : IEnumerable<Match>, IEnumerable, IEnumerator<Match>, IEnumerator, IDisposable
    // {
    // private int <>1__state;
    // private Match <>2__current;
    // public MatchCollection <>3__matchCollection;
    // private int <>l__initialThreadId;
    // public int <i>5__2;
    // public int <matchesCount>5__1;
    // public MatchCollection matchCollection;

    // [DebuggerHidden]
    // public <EnumerateMatches>d__0(int <>1__state)
    // {
    // this.<>1__state = <>1__state;
    // this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
    // }

    // private bool MoveNext()
    // {
    // switch (this.<>1__state)
    // {
    // case 0:
    // this.<>1__state = -1;
    // this.<matchesCount>5__1 = this.matchCollection.Count;
    // this.<i>5__2 = 0;
    // break;

    // case 1:
    // this.<>1__state = -1;
    // this.<i>5__2++;
    // break;

    // default:
    // goto Label_007B;
    // }
    // if (this.<i>5__2 < this.<matchesCount>5__1)
    // {
    // this.<>2__current = this.matchCollection[this.<i>5__2];
    // this.<>1__state = 1;
    // return true;
    // }
    // Label_007B:
    // return false;
    // }

    // [DebuggerHidden]
    // IEnumerator<Match> IEnumerable<Match>.GetEnumerator()
    // {
    // RegexHelper.<EnumerateMatches>d__0 d__;
    // if ((Thread.CurrentThread.ManagedThreadId == this.<>l__initialThreadId) && (this.<>1__state == -2))
    // {
    // this.<>1__state = 0;
    // d__ = this;
    // }
    // else
    // {
    // d__ = new RegexHelper.<EnumerateMatches>d__0(0);
    // }
    // d__.matchCollection = this.<>3__matchCollection;
    // return d__;
    // }

    // [DebuggerHidden]
    // IEnumerator IEnumerable.GetEnumerator()
    // {
    // return this.System.Collections.Generic.IEnumerable<System.Text.RegularExpressions.Match>.GetEnumerator();
    // }

    // [DebuggerHidden]
    // void IEnumerator.Reset()
    // {
    // throw new NotSupportedException();
    // }

    // void IDisposable.Dispose()
    // {
    // }

    // Match IEnumerator<Match>.Current
    // {
    // [DebuggerHidden]
    // get
    // {
    // return this.<>2__current;
    // }
    // }

    // object IEnumerator.Current
    // {
    // [DebuggerHidden]
    // get
    // {
    // return this.<>2__current;
    // }
    // }
    // }
  }
}