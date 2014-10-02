// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumberConverter.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The number converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The number converter.
  /// </summary>
  public class NumberConverter : BaseTypeConverter
  {
    #region Public Methods and Operators

    /// <summary>
    /// The can convert.
    /// </summary>
    /// <param name="type">
    /// The type. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public override bool CanConvert(Type type)
    {
      return typeof (byte) == type || typeof (short) == type || (typeof (int) == type || typeof (ushort) == type)
             || (typeof (uint) == type || typeof (long) == type) || typeof (ulong) == type;
    }

    /// <summary>
    /// The convert from.
    /// </summary>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public override byte[] ConvertFrom(object value)
    {
      if (value == null)
      {
        throw new ArgumentNullException();
      }

      if (value is byte)
      {
        return new[] {(byte) ((byte) value & (uint) byte.MaxValue)};
      }

      if (value is short)
      {
        var num = (short) value;
        return new[] {(byte) ((num & 65280) >> 8), (byte) ((uint) num & byte.MaxValue)};
      }

      if (value is ushort)
      {
        var num = (ushort) value;
        return new[] {(byte) ((num & 65280) >> 8), (byte) (num & (uint) byte.MaxValue)};
      }

      if (value is int)
      {
        var num = (int) value;
        return new[]
                 {
                   (byte) ((num & 4278190080L) >> 24), (byte) ((num & 16711680) >> 16), (byte) ((num & 65280) >> 8), 
                   (byte) (num & byte.MaxValue)
                 };
      }

      if (value is uint)
      {
        var num = (uint) value;
        return new[]
                 {
                   (byte) ((num & 4278190080U) >> 24), (byte) ((num & 16711680U) >> 16), (byte) ((num & 65280U) >> 8), 
                   (byte) (num & byte.MaxValue)
                 };
      }

      if (value is ulong)
      {
        var num = (ulong) value;
        return new[]
                 {
                   (byte) ((num & 18374686479671623680UL) >> 56), (byte) ((num & 71776119061217280UL) >> 48), 
                   (byte) ((num & 280375465082880UL) >> 40), (byte) ((num & 1095216660480UL) >> 32), 
                   (byte) ((num & 4278190080UL) >> 24), (byte) ((num & 16711680UL) >> 16), (byte) ((num & 65280UL) >> 8)
                   , 
                   (byte) (num & byte.MaxValue)
                 };
      }
      else
      {
        if (!(value is long))
        {
          throw new ArgumentException(
            string.Format("Невозможно выполнить преобразование типа: {0}", value.GetType().Name), 
            "value");
        }

        var num = (long) value;
        return new[]
                 {
                   (byte) ((ulong) (num & -72057594037927936L) >> 56), (byte) ((num & 71776119061217280L) >> 48), 
                   (byte) ((num & 280375465082880L) >> 40), (byte) ((num & 1095216660480L) >> 32), 
                   (byte) ((num & 4278190080L) >> 24), (byte) ((num & 16711680L) >> 16), (byte) ((num & 65280L) >> 8), 
                   (byte) ((ulong) num & byte.MaxValue)
                 };
      }
    }

    /// <summary>
    /// The convert to.
    /// </summary>
    /// <param name="type">
    /// The type. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <param name="startIndex">
    /// The start index. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <returns>
    /// The <see cref="object"/> . 
    /// </returns>
    public override object ConvertTo(Type type, byte[] value, int startIndex, int length)
    {
      base.ConvertTo(type, value, startIndex, length);
      if (type == typeof (byte))
      {
        return value[startIndex];
      }

      if (type == typeof (short))
      {
        return (short) (value[startIndex] << 8 | value[startIndex + 1]);
      }

      if (type == typeof (ushort))
      {
        return (ushort) ((uint) value[startIndex] << 8 | value[startIndex + 1]);
      }

      if (type == typeof (int))
      {
        return value[startIndex] << 24 | value[startIndex + 1] << 16 | value[startIndex + 2] << 8
               | value[startIndex + 3];
      }

      if (type == typeof (uint))
      {
        return
          (uint)
          (value[startIndex] << 24 | value[startIndex + 1] << 16 | value[startIndex + 2] << 8 | value[startIndex + 3]);
      }

      if (type == typeof (ulong))
      {
        return
          (ulong)
          ((long) value[startIndex] << 56 | (long) value[startIndex + 1] << 48 | (long) value[startIndex + 2] << 40
           | (long) value[startIndex + 3] << 32 | (long) value[startIndex + 4] << 24 |
           (long) value[startIndex + 5] << 16
           | (long) value[startIndex + 6] << 8 | value[startIndex + 7]);
      }

      if (type == typeof (long))
      {
        return (long) value[startIndex] << 56 | (long) value[startIndex + 1] << 48 | (long) value[startIndex + 2] << 40
               | (long) value[startIndex + 3] << 32 | (long) value[startIndex + 4] << 24
               | (long) value[startIndex + 5] << 16 | (long) value[startIndex + 6] << 8 | value[startIndex + 7];
      }

      throw new ArgumentException(string.Format("Невозможно выполнить преобразование в тип: {0}", type.Name), "value");
    }

    /// <summary>
    /// The get length.
    /// </summary>
    /// <param name="type">
    /// The type. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public override int GetLength(Type type)
    {
      if (type == null)
      {
        throw new ArgumentNullException();
      }

      if (type == typeof (byte))
      {
        return 1;
      }

      if (type == typeof (short) || type == typeof (ushort))
      {
        return 2;
      }

      if (type == typeof (int) || type == typeof (uint))
      {
        return 4;
      }

      if (type == typeof (ulong) || type == typeof (long))
      {
        return 8;
      }

      throw new ArgumentException("Тип не поддерживается");
    }

    #endregion
  }
}