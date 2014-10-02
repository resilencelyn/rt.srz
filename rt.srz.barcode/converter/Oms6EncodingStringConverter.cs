// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Oms6EncodingStringConverter.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The oms 6 encoding string converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The oms 6 encoding string converter.
  /// </summary>
  public class Oms6EncodingStringConverter : BaseTypeConverter
  {
    #region Constants

    /// <summary>
    ///   The reserved.
    /// </summary>
    public const char Reserved = '*';

    #endregion

    #region Fields

    /// <summary>
    ///   The _chars raw table.
    /// </summary>
    private readonly List<string> charsRawTable = new List<string>
                                                    {
                                                      " .-‘0123456789АБ", 
                                                      "ВГДЕЁЖЗИЙКЛМНОПР", 
                                                      "СТУФХЦЧШЩЬЪЫЭЮЯ*", 
                                                      "***************|"
                                                    };

    /// <summary>
    ///   The _encoding bytes.
    /// </summary>
    private readonly Dictionary<byte, char> encodingBytes = new Dictionary<byte, char>();

    /// <summary>
    ///   The _encoding chars.
    /// </summary>
    private readonly Dictionary<char, byte> encodingChars = new Dictionary<char, byte>();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="Oms6EncodingStringConverter" /> class.
    /// </summary>
    internal Oms6EncodingStringConverter()
    {
      for (var index1 = 0; index1 < charsRawTable.Count; ++index1)
      {
        var charArray = charsRawTable[index1].ToCharArray();
        for (var index2 = 0; index2 < 16; ++index2)
        {
          if (charArray[index2] != 42)
          {
            encodingChars[charArray[index2]] = (byte) (index1 << 4 | index2);
            encodingBytes[(byte) (index1 << 4 | index2)] = charArray[index2];
          }
        }
      }
    }

    #endregion

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
      return type == typeof (string);
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
      base.ConvertFrom(value);
      if (value.GetType() != typeof (string))
      {
        throw new ArgumentException(
          string.Format("Невозможно выполнить преобразование типа: {0}", value.GetType().Name), 
          "value");
      }

      var str = ((string) value).ToUpper();
      var bitArray = new BitArray(str.Length*6);
      var index1 = 0;
      var index2 = 0;
      for (; index1 < str.Length; ++index1)
      {
        var key = str[index1];
        if (!encodingChars.ContainsKey(key))
        {
          key = ' ';
        }

        var num1 = encodingChars[key];
        bitArray.Set(index2, (num1 & 1) == 1);
        int num2;
        bitArray.Set(num2 = index2 + 1, (num1 & 2) == 2);
        int num3;
        bitArray.Set(num3 = num2 + 1, (num1 & 4) == 4);
        int num4;
        bitArray.Set(num4 = num3 + 1, (num1 & 8) == 8);
        int num5;
        bitArray.Set(num5 = num4 + 1, (num1 & 16) == 16);
        int num6;
        bitArray.Set(num6 = num5 + 1, (num1 & 32) == 32);
        index2 = num6 + 1;
      }

      var i = ((str.Length*6)/8) + ((str.Length*6)%8 == 0 ? 0 : 1);
      var numArray = new byte[i];
      bitArray.CopyTo(numArray, 0);
      return numArray;
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
      if (type != typeof (string))
      {
        throw new ArgumentException(string.Format("Невозможно выполнить преобразование в тип: {0}", type.Name), "value");
      }

      var bytes = new byte[length];
      Array.Copy(value, startIndex, bytes, 0, length);
      var bitArray = new BitArray(bytes);
      var i = length*8/6;
      var charArray = new char[i];
      var index1 = 0;
      var index2 = 0;
      for (; index1 < charArray.Length; ++index1)
      {
        int num1;
        int num2;
        int num3;
        int num4;
        int num5;
        var index3 =
          (byte)
          ((byte)
           ((byte)
            ((byte)
             ((byte)
              ((byte) (0U | ToByte(bitArray.Get(index2)))
               | (uint) (byte) ((uint) ToByte(bitArray.Get(num1 = index2 + 1)) << 1))
              | (uint) (byte) ((uint) ToByte(bitArray.Get(num2 = num1 + 1)) << 2))
             | (uint) (byte) ((uint) ToByte(bitArray.Get(num3 = num2 + 1)) << 3))
            | (uint) (byte) ((uint) ToByte(bitArray.Get(num4 = num3 + 1)) << 4))
           | (uint) (byte) ((uint) ToByte(bitArray.Get(num5 = num4 + 1)) << 5));
        index2 = num5 + 1;
        try
        {
          // chArray[index1] = this._encodingBytes[index3];
          var x = (byte) (index3 >> 4);
          var y = (byte) (index3 << 4);
          y = (byte) (y >> 4);
          charArray[index1] = charsRawTable[x][y];
        }
        catch
        {
          ////chArray[index1] = this._encodingBytes[0x3f];
        }
      }

      return new string(charArray, 0, charArray.Length);
    }

    #endregion

    #region Methods

    /// <summary>
    /// The to byte.
    /// </summary>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    private byte ToByte(bool value)
    {
      return value ? (byte) 1 : (byte) 0;
    }

    #endregion
  }
}