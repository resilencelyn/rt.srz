// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShortYearConverter.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The short year converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The short year converter.
  /// </summary>
  public class ShortYearConverter : BaseTypeConverter
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
      if (dateTime.Year < 1900 || dateTime.Year > 2155)
      {
        throw new ArgumentException("«начение года даты должно быть больше 1900 и меньше 2155");
      }

      return new[] {(byte) (dateTime.Year - 1900)};
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

      if (length != 1)
      {
        throw new ArgumentException("ƒлина преобразуемого значени€ должна равн€тьс€ 1", "length");
      }

      return new DateTime(value[startIndex] + 1900, 1, 1);
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
      return 1;
    }

    #endregion
  }
}