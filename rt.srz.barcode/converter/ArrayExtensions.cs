// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArrayExtensions.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The array extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The array extensions.
  /// </summary>
  public static class ArrayExtensions
  {
    #region Public Methods and Operators

    /// <summary>
    /// The add range.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <typeparam name="T">
    /// T 
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/> . 
    /// </returns>
    public static T[] AddRange<T>(this T[] instance, T[] value)
    {
      var list = instance.ToList();
      list.AddRange(value);
      return list.ToArray();
    }

    /// <summary>
    /// The get range.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <typeparam name="T">
    /// T 
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/> . 
    /// </returns>
    public static T[] GetRange<T>(this T[] instance, int offset, int length)
    {
      return instance.ToList().GetRange(offset, length).ToArray();
    }

    /// <summary>
    /// The pad left.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public static byte[] PadLeft(this byte[] instance, int length)
    {
      var list = new List<byte>();
      list.AddRange(instance);
      if (instance.Length < length)
      {
        list.InsertRange(0, new byte[length - instance.Length]);
        instance = list.ToArray();
        return instance;
      }

      instance = instance.GetRange(0, length);
      return instance;
    }

    /// <summary>
    /// The pad right.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public static byte[] PadRight(this byte[] instance, int length)
    {
      var list = new List<byte>();
      list.AddRange(instance);
      if (instance.Length < length)
      {
        list.AddRange(new byte[length - instance.Length]);
        instance = list.ToArray();
        return instance;
      }

      instance = instance.GetRange(0, length);
      return instance;
    }

    /// <summary>
    /// The set range.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <typeparam name="T">
    /// T 
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/> . 
    /// </returns>
    public static T[] SetRange<T>(this T[] instance, int offset, T[] value)
    {
      for (var i = offset; (i < instance.Length) && (i < (offset + value.Length)); i++)
      {
        instance[i] = value[i - offset];
      }

      return instance;
    }

    /// <summary>
    /// The set value ex.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <typeparam name="T">
    /// T 
    /// </typeparam>
    public static void SetValueEx<T>(this T[] instance, int index, int length, T value)
    {
      for (var i = index; (i < instance.Length) && (i < (index + length)); i++)
      {
        instance[i] = value;
      }
    }

    #endregion

    ////public static byte[] ToArray(this object instance)
    ////{
    ////    if ((((((instance.GetType() != typeof(byte)) && (instance.GetType() != typeof(short))) && ((instance.GetType() != typeof(int)) && (instance.GetType() != typeof(long)))) && (((instance.GetType() != typeof(ushort)) && (instance.GetType() != typeof(uint))) && ((instance.GetType() != typeof(ulong)) && (instance.GetType() != typeof(byte[]))))) && ((((instance.GetType() != typeof(byte?)) && (instance.GetType() != typeof(short?))) && ((instance.GetType() != typeof(int?)) && (instance.GetType() != typeof(long?)))) && ((instance.GetType() != typeof(ushort?)) && (instance.GetType() != typeof(uint?))))) && (instance.GetType() != typeof(ulong?)))
    ////    {
    ////        throw new ArgumentException(string.Format("Type '{0}' is not valid.", instance.GetType().ToString()));
    ////    }
    ////    if ((instance.GetType() == typeof(byte)) || (instance.GetType() == typeof(byte?)))
    ////    {
    ////        return new byte[] { ((byte)instance) };
    ////    }
    ////    int bitLength = Convert.ToUInt64(instance).GetBitLength();
    ////    bitLength += (bitLength > 8) ? (((0x10 - (bitLength % 0x10)) > 0) ? (bitLength % 0x10) : 0x10) : 0;
    ////    byte[] buffer = new byte[bitLength / 8];
    ////    int from = 0;
    ////    for (int i = buffer.Length - 1; i >= 0; i--)
    ////    {
    ////        buffer[i] = (byte)Convert.ToUInt64(instance).GetBitRange(from, (from + 8));
    ////        from += 8;
    ////    }
    ////    return buffer;
    ////}
  }
}