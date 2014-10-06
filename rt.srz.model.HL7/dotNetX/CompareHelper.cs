// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompareHelper.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The compare helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.dotNetX
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   The compare helper.
  /// </summary>
  public static class CompareHelper
  {
    #region Enums

    /// <summary>
    ///   The combination.
    /// </summary>
    public enum Combination
    {
      /// <summary>
      ///   The or.
      /// </summary>
      Or, 

      /// <summary>
      ///   The and.
      /// </summary>
      And
    }

    /// <summary>
    ///   The effect.
    /// </summary>
    public enum Effect
    {
      /// <summary>
      ///   The assert.
      /// </summary>
      Assert, 

      /// <summary>
      ///   The deny.
      /// </summary>
      Deny
    }

    /// <summary>
    ///   The equality.
    /// </summary>
    public enum Equality
    {
      /// <summary>
      ///   The equal.
      /// </summary>
      Equal, 

      /// <summary>
      ///   The different.
      /// </summary>
      Different, 

      /// <summary>
      ///   The unknown.
      /// </summary>
      Unknown
    }

    /// <summary>
    ///   The relation.
    /// </summary>
    public enum Relation
    {
      /// <summary>
      ///   The equal.
      /// </summary>
      Equal, 

      /// <summary>
      ///   The non equal.
      /// </summary>
      NonEqual, 

      /// <summary>
      ///   The less.
      /// </summary>
      Less, 

      /// <summary>
      ///   The greater.
      /// </summary>
      Greater
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check equality.
    /// </summary>
    /// <param name="o1">
    /// The o 1.
    /// </param>
    /// <param name="o2">
    /// The o 2.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="Equality"/>.
    /// </returns>
    public static Equality CheckEquality<T>(T o1, T o2)
    {
      var equatable = o1 as IEquatable<T>;
      if (equatable != null)
      {
        if (equatable.Equals(o2))
        {
          return Equality.Equal;
        }

        return Equality.Different;
      }

      equatable = o2 as IEquatable<T>;
      if (equatable != null)
      {
        if (equatable.Equals(o1))
        {
          return Equality.Equal;
        }

        return Equality.Different;
      }

      var comparable = o1 as IComparable<T>;
      if (comparable != null)
      {
        if (comparable.CompareTo(o2) == 0)
        {
          return Equality.Equal;
        }

        return Equality.Different;
      }

      comparable = o2 as IComparable<T>;
      if (comparable == null)
      {
        return CheckEquality(o1, o2);
      }

      if (comparable.CompareTo(o1) == 0)
      {
        return Equality.Equal;
      }

      return Equality.Different;
    }

    // public static Equality CheckEquality(object o1, object o2)
    // {
    // Equality different;
    // Equality equality = CheckEqualityByReference(o1, o2);
    // switch (equality)
    // {
    // case Equality.Equal:
    // case Equality.Different:
    // return equality;
    // }
    // IComparable comparable = o1 as IComparable;
    // if (comparable != null)
    // {
    // try
    // {
    // if (comparable.CompareTo(o2) == 0)
    // {
    // return Equality.Equal;
    // }
    // different = Equality.Different;
    // }
    // catch
    // {
    // }
    // return different;
    // }
    // comparable = o2 as IComparable;
    // if (comparable != null)
    // {
    // try
    // {
    // if (comparable.CompareTo(o1) == 0)
    // {
    // return Equality.Equal;
    // }
    // different = Equality.Different;
    // }
    // catch
    // {
    // }
    // return different;
    // }
    // IList list = o1 as IList;
    // if (list != null)
    // {
    // IList list2 = o2 as IList;
    // if (o2 != null)
    // {
    // if (CollectionHelper.ListsEqual(list, list2))
    // {
    // return Equality.Equal;
    // }
    // return Equality.Different;
    // }
    // }
    // return Equality.Unknown;
    // }

    /// <summary>
    /// The check equality by reference.
    /// </summary>
    /// <param name="o1">
    /// The o 1.
    /// </param>
    /// <param name="o2">
    /// The o 2.
    /// </param>
    /// <returns>
    /// The <see cref="Equality"/>.
    /// </returns>
    public static Equality CheckEqualityByReference(object o1, object o2)
    {
      if (ReferenceEquals(o1, o2))
      {
        return Equality.Equal;
      }

      if ((o1 != null) && (o2 != null))
      {
        return Equality.Unknown;
      }

      return Equality.Different;
    }

    /// <summary>
    /// The check equality forced.
    /// </summary>
    /// <param name="o1">
    /// The o 1.
    /// </param>
    /// <param name="o2">
    /// The o 2.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CheckEqualityForced(object o1, object o2)
    {
      switch (CheckEquality(o1, o2))
      {
        case Equality.Equal:
          return true;

        case Equality.Different:
          return false;
      }

      return o1.Equals(o2);
    }

    /// <summary>
    /// The check equality forced.
    /// </summary>
    /// <param name="o1">
    /// The o 1.
    /// </param>
    /// <param name="o2">
    /// The o 2.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CheckEqualityForced<T>(T o1, T o2)
    {
      switch (CheckEquality(o1, o2))
      {
        case Equality.Equal:
          return true;

        case Equality.Different:
          return false;
      }

      return o1.Equals(o2);
    }

    #endregion
  }
}