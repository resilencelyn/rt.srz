// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShortDateConverter.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The short date converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The short date converter.
  /// </summary>
  public class ShortDateConverter : BaseTypeConverter
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
      return typeof (DateTime) == type;
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

      if (value.GetType() != typeof (DateTime))
      {
        throw new ArgumentException(
          string.Format("Ќевозможно выполнить преобразование типа: {0}", value.GetType().Name), 
          "value");
      }

      var dateTime = (DateTime) value;
      if (dateTime < new DateTime(2000, 1, 1) || dateTime > new DateTime(2179, 6, 6))
      {
        throw new ArgumentException("«начение даты должно быть больше 01.01.2000 и меньше 06.06.2179");
      }

      var days = (dateTime - new DateTime(2000, 1, 1)).Days;
      return new[] {(byte) ((days & 65280) >> 8), (byte) (days & byte.MaxValue)};
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
      if (type != typeof (DateTime))
      {
        throw new ArgumentException(string.Format("Ќевозможно выполнить преобразование в тип: {0}", type.Name), "value");
      }

      if (length != 2)
      {
        throw new ArgumentException("ƒлина преобразуемого значени€ должна равн€тьс€ 2", "length");
      }

      return new DateTime(2000, 1, 1).AddDays(value[startIndex] << 8 | value[startIndex + 1]);
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
      return 2;
    }

    #endregion
  }
}