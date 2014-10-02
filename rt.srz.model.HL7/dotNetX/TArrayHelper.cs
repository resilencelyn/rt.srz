// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TArrayHelper.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The t array helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.dotNetX
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   The t array helper.
  /// </summary>
  public static class TArrayHelper
  {
    #region Public Methods and Operators

    /// <summary>
    /// The compare arrays.
    /// </summary>
    /// <param name="arr1">
    /// The arr 1.
    /// </param>
    /// <param name="arr2">
    /// The arr 2.
    /// </param>
    /// <typeparam name="LeftType">
    /// </typeparam>
    /// <typeparam name="RightType">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CompareArrays<LeftType, RightType>(LeftType[] arr1, RightType[] arr2)
      where LeftType : IEquatable<RightType>
    {
      return CompareArrays(arr1, arr2, 0L);
    }

    /// <summary>
    /// The compare arrays.
    /// </summary>
    /// <param name="arr1">
    /// The arr 1.
    /// </param>
    /// <param name="arr2">
    /// The arr 2.
    /// </param>
    /// <param name="index">
    /// The index.
    /// </param>
    /// <typeparam name="LeftType">
    /// </typeparam>
    /// <typeparam name="RightType">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CompareArrays<LeftType, RightType>(LeftType[] arr1, RightType[] arr2, long index)
      where LeftType : IEquatable<RightType>
    {
      return CompareArrays(arr1, arr2, index, -1L);
    }

    /// <summary>
    /// The compare arrays.
    /// </summary>
    /// <param name="arr1">
    /// The arr 1.
    /// </param>
    /// <param name="index1">
    /// The index 1.
    /// </param>
    /// <param name="arr2">
    /// The arr 2.
    /// </param>
    /// <param name="index2">
    /// The index 2.
    /// </param>
    /// <typeparam name="LeftType">
    /// </typeparam>
    /// <typeparam name="RightType">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CompareArrays<LeftType, RightType>(LeftType[] arr1, long index1, RightType[] arr2, long index2)
      where LeftType : IEquatable<RightType>
    {
      return CompareArrays(arr1, index1, arr2, index2, -1L);
    }

    /// <summary>
    /// The compare arrays.
    /// </summary>
    /// <param name="arr1">
    /// The arr 1.
    /// </param>
    /// <param name="arr2">
    /// The arr 2.
    /// </param>
    /// <param name="index">
    /// The index.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <typeparam name="LeftType">
    /// </typeparam>
    /// <typeparam name="RightType">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CompareArrays<LeftType, RightType>(LeftType[] arr1, RightType[] arr2, long index, long count)
      where LeftType : IEquatable<RightType>
    {
      return CompareArrays(arr1, index, arr2, index, count);
    }

    /// <summary>
    /// The compare arrays.
    /// </summary>
    /// <param name="arr1">
    /// The arr 1.
    /// </param>
    /// <param name="index1">
    /// The index 1.
    /// </param>
    /// <param name="arr2">
    /// The arr 2.
    /// </param>
    /// <param name="index2">
    /// The index 2.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <typeparam name="LeftType">
    /// </typeparam>
    /// <typeparam name="RightType">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CompareArrays<LeftType, RightType>(
      LeftType[] arr1, 
      long index1, 
      RightType[] arr2, 
      long index2, 
      long count) where LeftType : IEquatable<RightType>
    {
      // if (arr1 == arr2)
      // {
      // return true;
      // }
      if ((arr1 != null) && (arr2 != null))
      {
        if (count >= 0L)
        {
          goto Label_0060;
        }

        count = arr1.LongLength - index1;
        if ((arr2.LongLength - index2) == count)
        {
          goto Label_0060;
        }
      }

      return false;
      Label_0060:
      while (count > 0L)
      {
        if (!arr1[(int)((IntPtr)index1)].Equals(arr2[(int)((IntPtr)index2)]))
        {
          return false;
        }

        count -= 1L;
        index1 += 1L;
        index2 += 1L;
      }

      return true;
    }

    /// <summary>
    /// The create array from range.
    /// </summary>
    /// <param name="from">
    /// The from.
    /// </param>
    /// <param name="index">
    /// The index.
    /// </param>
    /// <typeparam name="ObjectType">
    /// </typeparam>
    /// <returns>
    /// The <see cref="ObjectType[]"/>.
    /// </returns>
    public static ObjectType[] CreateArrayFromRange<ObjectType>(this ObjectType[] from, long index)
    {
      return from.CreateArrayFromRange(index, from.Length - index);
    }

    /// <summary>
    /// The create array from range.
    /// </summary>
    /// <param name="from">
    /// The from.
    /// </param>
    /// <param name="index">
    /// The index.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <typeparam name="ObjectType">
    /// </typeparam>
    /// <returns>
    /// The <see cref="ObjectType[]"/>.
    /// </returns>
    public static ObjectType[] CreateArrayFromRange<ObjectType>(this ObjectType[] from, long index, long count)
    {
      var destinationArray = new ObjectType[count];
      Array.Copy(from, index, destinationArray, 0L, count);
      return destinationArray;
    }

    #endregion
  }
}