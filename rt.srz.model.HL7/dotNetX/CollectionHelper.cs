// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionHelper.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The collection helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.dotNetX
{
  #region references

  using System.Collections;
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.Text;

  #endregion

  /// <summary>
  ///   The collection helper.
  /// </summary>
  public static class CollectionHelper
  {
    #region Public Methods and Operators

    /// <summary>
    /// The collection values add unique.
    /// </summary>
    /// <param name="Collection">
    /// The collection.
    /// </param>
    /// <param name="Values">
    /// The values.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CollectionValuesAddUnique<T>(this ICollection<T> Collection, params T[] Values)
    {
      return Collection.CollectionValuesAddUnique((IEnumerable<T>)Values);
    }

    /// <summary>
    /// The collection values add unique.
    /// </summary>
    /// <param name="Collection">
    /// The collection.
    /// </param>
    /// <param name="Values">
    /// The values.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CollectionValuesAddUnique<T>(this ICollection<T> Collection, IEnumerable<T> Values)
    {
      var flag = false;
      if ((Collection != null) && (Values != null))
      {
        foreach (var local in Values)
        {
          if (!Collection.Contains(local))
          {
            Collection.Add(local);
            flag = true;
          }
        }
      }

      return flag;
    }

    /// <summary>
    /// The collection values remove.
    /// </summary>
    /// <param name="Collection">
    /// The collection.
    /// </param>
    /// <param name="Values">
    /// The values.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CollectionValuesRemove<T>(this ICollection<T> Collection, params T[] Values)
    {
      return Collection.CollectionValuesRemove((IEnumerable<T>)Values);
    }

    /// <summary>
    /// The collection values remove.
    /// </summary>
    /// <param name="Collection">
    /// The collection.
    /// </param>
    /// <param name="Values">
    /// The values.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CollectionValuesRemove<T>(this ICollection<T> Collection, IEnumerable<T> Values)
    {
      var flag = false;
      if ((Collection != null) && (Values != null))
      {
        foreach (var local in Values)
        {
          if (Collection.Remove(local))
          {
            flag = true;
          }
        }
      }

      return flag;
    }

    // public static bool IsNullOrEmpty(IEnumerable Collection)
    // {
    // if (Collection != null)
    // {
    // using ( IEnumerator enumerator = Collection.GetEnumerator())
    // {
    // while (enumerator.MoveNext())
    // {
    // object current = enumerator.Current;
    // return false;
    // }
    // }
    // }
    // return true;
    // }

    /// <summary>
    /// The list clear values.
    /// </summary>
    /// <param name="list">
    /// The list.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    public static void ListClearValues<T>(List<T> list)
    {
      if (list != null)
      {
        list.Clear();
      }
    }

    /// <summary>
    /// The list to string.
    /// </summary>
    /// <param name="list">
    /// The list.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ListToString<T>(this IEnumerable<T> list)
    {
      return list.ListToString(",");
    }

    /// <summary>
    /// The list to string.
    /// </summary>
    /// <param name="list">
    /// The list.
    /// </param>
    /// <param name="delimiter">
    /// The delimiter.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ListToString<T>(this IEnumerable<T> list, string delimiter)
    {
      if (list == null)
      {
        return null;
      }

      StringBuilder builder = null;
      foreach (var local in list)
      {
        builder = TStringHelper.CombineStrings(builder, (local != null) ? local.ToString() : null, delimiter);
      }

      return TStringHelper.StringToEmpty(builder);
    }

    /// <summary>
    /// The list write value.
    /// </summary>
    /// <param name="list">
    /// The list.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    public static void ListWriteValue<T>(ref List<T> list, T value)
    {
      if (list == null)
      {
        list = new List<T>();
      }

      list.Add(value);
    }

    /// <summary>
    /// The lists equal.
    /// </summary>
    /// <param name="list1">
    /// The list 1.
    /// </param>
    /// <param name="list2">
    /// The list 2.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool ListsEqual<T>(IList<T> list1, IList<T> list2)
    {
      switch (CompareHelper.CheckEqualityByReference(list1, list2))
      {
        case CompareHelper.Equality.Equal:
          return true;

        case CompareHelper.Equality.Different:
          return false;
      }

      var count = list1.Count;
      if (count == list2.Count)
      {
        while (count > 0)
        {
          count--;
          if (!CompareHelper.CheckEqualityForced(list1[count], list2[count]))
          {
            return false;
          }
        }

        return true;
      }

      return false;
    }

    /// <summary>
    /// The lists equal.
    /// </summary>
    /// <param name="list1">
    /// The list 1.
    /// </param>
    /// <param name="list2">
    /// The list 2.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool ListsEqual(IList list1, IList list2)
    {
      switch (CompareHelper.CheckEqualityByReference(list1, list2))
      {
        case CompareHelper.Equality.Equal:
          return true;

        case CompareHelper.Equality.Different:
          return false;
      }

      var count = list1.Count;
      if (count == list2.Count)
      {
        while (count > 0)
        {
          count--;
          if (!CompareHelper.CheckEqualityForced(list1[count], list2[count]))
          {
            return false;
          }
        }

        return true;
      }

      return false;
    }

    /// <summary>
    /// The try add to dictionary.
    /// </summary>
    /// <param name="dic">
    /// The dic.
    /// </param>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <typeparam name="TKey">
    /// </typeparam>
    /// <typeparam name="TValue">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool TryAddToDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue value)
    {
      if ((dic != null) && !dic.ContainsKey(key))
      {
        dic.Add(key, value);
        return true;
      }

      return false;
    }

    /// <summary>
    /// The try add to dictionary.
    /// </summary>
    /// <param name="dic">
    /// The dic.
    /// </param>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool TryAddToDictionary(this StringDictionary dic, string key, string value)
    {
      if ((dic != null) && !dic.ContainsKey(key))
      {
        dic.Add(key, value);
        return true;
      }

      return false;
    }

    /// <summary>
    /// The upsert to dictionary.
    /// </summary>
    /// <param name="dic">
    /// The dic.
    /// </param>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <typeparam name="TKey">
    /// </typeparam>
    /// <typeparam name="TValue">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool UpsertToDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue value)
    {
      if (dic != null)
      {
        if (!dic.ContainsKey(key))
        {
          dic.Add(key, value);
          return true;
        }

        dic[key] = value;
      }

      return false;
    }

    /// <summary>
    /// The upsert to dictionary.
    /// </summary>
    /// <param name="dic">
    /// The dic.
    /// </param>
    /// <param name="key">
    /// The key.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool UpsertToDictionary(this StringDictionary dic, string key, string value)
    {
      if (dic != null)
      {
        if (!dic.ContainsKey(key))
        {
          dic.Add(key, value);
          return true;
        }

        dic[key] = value;
      }

      return false;
    }

    #endregion
  }
}