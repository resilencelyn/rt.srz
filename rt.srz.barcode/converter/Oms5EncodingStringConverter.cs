// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Oms5EncodingStringConverter.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The oms 5 encoding string converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The oms 5 encoding string converter.
  /// </summary>
  public class Oms5EncodingStringConverter : BaseTypeConverter
  {
    #region Fields

    /// <summary>
    ///   The _encoding bytes.
    /// </summary>
    private readonly Hashtable encodingBytes = new Hashtable
                                                 {
                                                   {0, ' '}, 
                                                   {1, '-'}, 
                                                   {2, 'А'}, 
                                                   {3, 'Б'}, 
                                                   {4, 'В'}, 
                                                   {5, 'Г'}, 
                                                   {6, 'Д'}, 
                                                   {7, 'Е'}, 
                                                   {8, 'Ж'}, 
                                                   {9, 'З'}, 
                                                   {10, 'И'}, 
                                                   {11, 'К'}, 
                                                   {12, 'Л'}, 
                                                   {13, 'М'}, 
                                                   {14, 'Н'}, 
                                                   {15, 'О'}, 
                                                   {16, 'П'}, 
                                                   {17, 'Р'}, 
                                                   {18, 'С'}, 
                                                   {19, 'Т'}, 
                                                   {20, 'У'}, 
                                                   {21, 'Ф'}, 
                                                   {22, 'Х'}, 
                                                   {23, 'Ц'}, 
                                                   {24, 'Ч'}, 
                                                   {25, 'Ш'}, 
                                                   {26, 'Щ'}, 
                                                   {27, 'Ь'}, 
                                                   {28, 'Ы'}, 
                                                   {29, 'Э'}, 
                                                   {30, 'Ю'}, 
                                                   {31, 'Я'}
                                                 };

    /// <summary>
    ///   The _encoding chars.
    /// </summary>
    private readonly Hashtable encodingChars = new Hashtable
                                                 {
                                                   {' ', 0}, 
                                                   {'-', 1}, 
                                                   {'А', 2}, 
                                                   {'Б', 3}, 
                                                   {'В', 4}, 
                                                   {'Г', 5}, 
                                                   {'Д', 6}, 
                                                   {'Е', 7}, 
                                                   {'Ё', 7}, 
                                                   {'Ж', 8}, 
                                                   {'З', 9}, 
                                                   {'И', 10}, 
                                                   {'Й', 10}, 
                                                   {'К', 11}, 
                                                   {'Л', 12}, 
                                                   {'М', 13}, 
                                                   {'Н', 14}, 
                                                   {'О', 15}, 
                                                   {'П', 16}, 
                                                   {'Р', 17}, 
                                                   {'С', 18}, 
                                                   {'Т', 19}, 
                                                   {'У', 20}, 
                                                   {'Ф', 21}, 
                                                   {'Х', 22}, 
                                                   {'Ц', 23}, 
                                                   {'Ч', 24}, 
                                                   {'Ш', 25}, 
                                                   {'Щ', 26}, 
                                                   {'Ь', 27}, 
                                                   {'Ъ', 27}, 
                                                   {'Ы', 28}, 
                                                   {'Э', 29}, 
                                                   {'Ю', 30}, 
                                                   {'Я', 31}
                                                 };

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
      var bitArray = new BitArray(str.Length*5);
      var index1 = 0;
      var index2 = 0;
      for (; index1 < str.Length; ++index1)
      {
        var ch = str[index1];
        if (!encodingChars.ContainsKey(ch))
        {
          ch = ' ';
        }

        var num1 = (byte) encodingChars[ch];
        bitArray.Set(index2, (num1 & 1) == 1);
        int num2;
        bitArray.Set(num2 = index2 + 1, (num1 & 2) == 2);
        int num3;
        bitArray.Set(num3 = num2 + 1, (num1 & 4) == 4);
        int num4;
        bitArray.Set(num4 = num3 + 1, (num1 & 8) == 8);
        int num5;
        bitArray.Set(num5 = num4 + 1, (num1 & 16) == 16);
        index2 = num5 + 1;
      }

      var i = ((str.Length*5)/8) + ((str.Length*5)%8 == 0 ? 0 : 1);
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
      var i = (length*8)/5;
      var array = new char[i];
      var index1 = 0;
      var index2 = 0;
      for (; index1 < array.Length; ++index1)
      {
        int num1;
        int num2;
        int num3;
        int num4;
        var num5 =
          (byte)
          ((byte)
           ((byte)
            ((byte)
             ((byte) (0U | ToByte(bitArray.Get(index2)))
              | (uint) (byte) ((uint) ToByte(bitArray.Get(num1 = index2 + 1)) << 1))
             | (uint) (byte) ((uint) ToByte(bitArray.Get(num2 = num1 + 1)) << 2))
            | (uint) (byte) ((uint) ToByte(bitArray.Get(num3 = num2 + 1)) << 3))
           | (uint) (byte) ((uint) ToByte(bitArray.Get(num4 = num3 + 1)) << 4));
        index2 = num4 + 1;
        array[index1] = (char) encodingBytes[num5];
      }

      return new string(array, 0, array.Length);
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