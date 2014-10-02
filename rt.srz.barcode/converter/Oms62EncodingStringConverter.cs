// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Oms62EncodingStringConverter.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The om s 62 encoding string converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The om s 62 encoding string converter.
  /// </summary>
  internal class Oms62EncodingStringConverter : BaseTypeConverter
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
    ///   Initializes a new instance of the <see cref="Oms62EncodingStringConverter" /> class.
    /// </summary>
    internal Oms62EncodingStringConverter()
    {
      for (var i = 0; i < charsRawTable.Count; i++)
      {
        var charArray = charsRawTable[i].ToCharArray();
        for (var j = 0; j < 0x10; j++)
        {
          if (charArray[j] != '*')
          {
            encodingChars[charArray[j]] = (byte) ((i << 4) | j);
            encodingBytes[(byte) ((i << 4) | j)] = charArray[j];
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
      var array = new CBitArray();
      var num = 0;
      var index = 0;
      while (num < str.Length)
      {
        var key = str[num];
        if (!encodingChars.ContainsKey(key))
        {
          key = ' ';
        }

        var num3 = encodingChars[key];
        array.Set(index, (num3 & 0x20) == 0x20);
        array.Set(++index, (num3 & 0x10) == 0x10);
        array.Set(++index, (num3 & 8) == 8);
        array.Set(++index, (num3 & 4) == 4);
        array.Set(++index, (num3 & 2) == 2);
        array.Set(++index, (num3 & 1) == 1);
        index++;
        num++;
      }

      return array.ToArray();
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

      var destinationArray = new byte[length];
      Array.Copy(value, startIndex, destinationArray, 0, length);
      var array = new CBitArray(destinationArray);
      var i = (length*8)/6;
      var charArray = new char[i];
      var index = 0;
      var num2 = 0;
      while (index < charArray.Length)
      {
        byte num3 = 0;
        num3 = (byte) (num3 | ((byte) (ToByte(array.Get(num2)) << 5)));
        num3 = (byte) (num3 | ((byte) (ToByte(array.Get(++num2)) << 4)));
        num3 = (byte) (num3 | ((byte) (ToByte(array.Get(++num2)) << 3)));
        num3 = (byte) (num3 | ((byte) (ToByte(array.Get(++num2)) << 2)));
        num3 = (byte) (num3 | ((byte) (ToByte(array.Get(++num2)) << 1)));
        num3 = (byte) (num3 | ToByte(array.Get(++num2)));
        num2++;
        charArray[index] = encodingBytes[num3];
        index++;
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
      return value ? ((byte) 1) : ((byte) 0);
    }

    #endregion
  }
}