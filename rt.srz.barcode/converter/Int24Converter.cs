// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Int24Converter.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The int 24 converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The int 24 converter.
  /// </summary>
  public class Int24Converter : BaseTypeConverter
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
      return typeof (byte) == type || typeof (short) == type || typeof (int) == type;
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

      if (!(value is int))
      {
        throw new ArgumentException(
          string.Format("Ќевозможно выполнить преобразование типа: {0}", value.GetType().Name), 
          "value");
      }

      var num = (int) value;
      return new[] {(byte) ((num & 16711680) >> 16), (byte) ((num & 65280) >> 8), (byte) (num & byte.MaxValue)};
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
      if (type == typeof (int))
      {
        return value[startIndex] << 16 | value[startIndex + 1] << 8 | value[startIndex + 2];
      }

      throw new ArgumentException(string.Format("Ќевозможно выполнить преобразование в тип: {0}", type.Name), "value");
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
      return 3;
    }

    #endregion
  }
}