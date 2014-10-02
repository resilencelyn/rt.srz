// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseTypeConverter.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The base type converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The base type converter.
  /// </summary>
  public class BaseTypeConverter : ITypeConverter
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
    public virtual bool CanConvert(Type type)
    {
      return false;
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
    public virtual byte[] ConvertFrom(object value)
    {
      if (value == null)
      {
        throw new ArgumentNullException();
      }

      return null;
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
    public virtual object ConvertTo(Type type, byte[] value, int startIndex, int length)
    {
      if (value == null || type == null)
      {
        throw new ArgumentNullException();
      }

      if (startIndex > value.Length - 1)
      {
        throw new ArgumentOutOfRangeException("startIndex");
      }

      if (startIndex + length > value.Length)
      {
        throw new ArgumentOutOfRangeException("length");
      }

      return null;
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
    public virtual int GetLength(Type type)
    {
      return -1;
    }

    #endregion
  }
}