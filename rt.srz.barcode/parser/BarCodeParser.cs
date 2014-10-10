// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BarCodeParser.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The bar code parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#endregion

namespace rt.srz.barcode.parser
{
  /// <summary>
  ///   The bar code parser.
  /// </summary>
  public class BarCodeParser
  {
    #region Fields

    /// <summary>
    ///   The symbol codes.
    /// </summary>
    private readonly char[] symbolCodes;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="BarCodeParser" /> class.
    /// </summary>
    public BarCodeParser()
    {
      symbolCodes = new[]
                      {
                        ' ', '.', '-', '‘', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'А', 'Б', 'В', 'Г', 'Д', 
                        'E', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 
                        'Ч', 'Ш', 'Щ', 'Ь', 'Ъ', 'Ы', 'Э', 'Ю', 'Я', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', 
                        '*', '*', '*', '*', '*', '*', '|'
                      };
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Распарсивает массив байт плоученный со штрих кода,
    /// </summary>
    /// <param name="data">
    /// Массив байт плоученный со штрих кода 
    /// </param>
    /// <param name="result">
    /// строка содержащая XML с распарсенными данными 
    /// </param>
    /// <returns>
    /// true если распарсилось, false иначе 
    /// </returns>
    public bool ParseBytes(byte[] data, out string result)
    {
      var res = string.Empty;
      var counter = 0;
      ulong policyNumber = 0;
      string fio;
      ulong ogrn = 0;
      var dateStart = new DateTime(1900, 1, 1);

      // Код типа штрих-кода
      var barcodeType = data[counter++];

      ////var temqwep = 7851000840000199;

      // Номер полиса
      for (var i = 0; i < 8; i++, counter++)
      {
        policyNumber = (policyNumber*256) + data[counter];
      }

      //////Номер полиса
      ////PolicyNumber = 0;
      ////Counter -= 8;
      ////for (int i = 0; i < 8; i++, Counter++)
      ////{
      ////    //PolicyNumber = PolicyNumber + Data[8 - Counter] * (ulong)Math.Pow(256, i);
      ////    PolicyNumber = PolicyNumber * 256 + Data[8-Counter];
      ////}

      // Фамилия, Имя, Отчество
      var temp = new byte[42];
      Array.Copy(data, counter, temp, 0, 42);

      //// fio = Pc.Shared.Utils.Extensions.BinaryOperations.ToString(temp);
      counter += 42;
      var barCodeParser = new BarCodeParser();
      if (!barCodeParser.TryGetStringFromUncompressedCodes(temp, out fio))
      {
        // если попали сюда значит была ошибка
        result = null;
        return false;
      }

      // Пол
      var sex = data[counter++].ToString(CultureInfo.InvariantCulture);

      // Дата рождения  
      int birthDayInterval = data[counter++];
      birthDayInterval = (birthDayInterval*256) + data[counter++];
      var timeSpan = new TimeSpan(birthDayInterval, 0, 0, 0);
      var birthDate = dateStart + timeSpan;

      // Срок действия полиса
      int expireDateInterval = data[counter++];
      expireDateInterval = (expireDateInterval*256) + data[counter++];
      timeSpan = new TimeSpan(expireDateInterval, 0, 0, 0);
      var expireDate = dateStart + timeSpan;

      // ОГРН страховой медицинской организации
      for (var i = 0; i < 6; i++, counter++)
      {
        ogrn = (ogrn*256) + data[counter];
      }

      // ОКАТО субъекта РФ, на территории которого застрахован гражданин
      ulong okato = data[counter++];
      okato = (okato*256) + data[counter++];
      okato = (okato*256) + data[counter];

      fio = fio.Trim();
      var fioArray = fio.Split(new[] {'|'}, 3);

      result =
        string.Format(
          "<BarcodeData> <BarcodeType>{0}</BarcodeType> <PolicyNumber>{1}</PolicyNumber> <FirstName>{2}</FirstName> <LastName>{3}</LastName> <Patronymic>{4}</Patronymic> <Sex>{5}</Sex> <BirthDate>{6}</BirthDate> <ExpireDate>{7}</ExpireDate> <Ogrn>{8}</Ogrn> <Okato>{9}</Okato>  <EDS>{8}</EDS> </BarcodeData>", 
          barcodeType, 
          policyNumber.ToString("D16"), 
          fioArray[0], 
          fioArray[1], 
          fioArray[2], 
          sex, 
          birthDate, 
          expireDate, 
          ogrn, 
          okato);
      return true;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The symbol 6 bit code to char.
    /// </summary>
    /// <param name="symbol">
    /// The symbol. 
    /// </param>
    /// <returns>
    /// The <see cref="char"/> . 
    /// </returns>
    private char Symbol6BitCodeToChar(byte symbol)
    {
      return symbolCodes[symbol];
    }

    /// <summary>
    /// The symbol 6 bit code to string.
    /// </summary>
    /// <param name="symbol">
    /// The symbol. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    private string Symbol6BitCodeToString(byte symbol)
    {
      return Symbol6BitCodeToChar(symbol).ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// The try get string from compressed codes.
    /// </summary>
    /// <param name="codes">
    /// The codes. 
    /// </param>
    /// <param name="result">
    /// The result. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    private bool TryGetStringFromCompressedCodes(IEnumerable<byte> codes, out string result)
    {
      result = codes.Select(Symbol6BitCodeToString)
        .Where(symbol => symbol != "*")
        .Aggregate(string.Empty, (current, symbol) => current + symbol);

      return true;
    }

    /// <summary>
    /// Пробует преобразовать массив байт в котором каждый симвло представлен 6 битами
    ///   в строку.
    /// </summary>
    /// <param name="source">
    /// Исходный массив байт 
    /// </param>
    /// <param name="dest">
    /// Преобразованная строка 
    /// </param>
    /// <returns>
    /// Если упешно преобразование вернет true иначе else 
    /// </returns>
    
    /// Порядок хранения символов такой : первые 3 байта = хранят первые 4 символа и т.д.
    ///   Причем для получения кода символа необходимо взять соответствующие 6 бит, и
    ///   обратить их порядок.
    
    private bool TryGetStringFromUncompressedCodes(byte[] source, out string dest)
    {
      var uncompressedCodes = UncompressCodes(source);
      string result;
      if (TryGetStringFromCompressedCodes(uncompressedCodes, out result))
      {
        dest = result;
        return true;
      }

      dest = null;
      return false;
    }

    /// <summary>
    /// The uncompress codes.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    private byte[] UncompressCodes(byte[] source)
    {
      // TODO здесь нужно задвать входной массив опред-й длинны!!! или 8/6 получет не ту длинну
      var dest = new byte[(source.Count()*8)/6];

      var bitArray = new BitArray(source);

      ////т.к. биты записываются справа налево перевернем их зеркально
      ////for (int i = 0; i < source.Count(); i++)
      ////{
      ////    for (int j = 0; j < 4; j++)
      ////    {
      ////        bool temp = bitArray[j + i * 8];
      ////        bitArray[j + i * 8] = bitArray[7 - j + i * 8];
      ////        bitArray[7 - j + i * 8] = temp;
      ////    }
      ////}

      // собираем в распакованные массив байт
      // счетчик показывает на какком бите мы сейчас находимся в массиве bitArray
      var bitIndex = 0;
      for (var i = 0; i < dest.Count(); i++)
      {
        dest[i] = 0;
        for (var j = 5; j >= 0; j--)
        {
          // значение bitArray[bitIndex++] приведенное к byte;
          var bitValue = (byte) (bitArray[bitIndex++] ? 1 : 0);
          var shiftedbitValue = (byte) (bitValue << (5 - j));
          dest[i] = (byte) (dest[i] | shiftedbitValue);
        }
      }

      return dest;
    }

    #endregion
  }
}