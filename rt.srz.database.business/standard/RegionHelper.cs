// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegionHelper.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The region helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.srz.database.business.standard
{
  using System;

  using rt.srz.database.business.standard.enums;

  // [CLSCompliant(false)]
  /// <summary>
  /// The region helper.
  /// </summary>
  public static class RegionHelper
  {
    // полная длина кода ТФОМС
    // public static readonly byte ТФОМС_FullLength = 2;

    // полная длина регионального кода ОКАТО ТС
    // public static readonly byte ОКАТО_FullLength = 5;

    // получить полную длину кода региона
    // public static byte RetrieveFullLength(RegionCoding coding)
    // {
    // switch (coding)
    // {
    // case RegionCoding.ТФОМС:
    // return ТФОМС_FullLength;
    // case RegionCoding.ОКАТО:
    // return ОКАТО_FullLength;
    // }
    // return 0;
    // }

    // преобразовать число в строку, дополнив слева нулями до заданного количества цифр
    // public static string NumberToString<T>(T number, byte digits = 0) where T : IFormattable
    // {
    // return (digits > 0) ? number.ToString(string.Concat("D", digits.ToString()), formatProvider: null) : number.ToString();
    // }

    // получить строку с кодом региона
    // public static string RegionAsString(UInt64 region, RegionCoding coding)
    // {
    // return NumberToString(region, RetrieveFullLength(coding));
    // }

    // получить строку с кодом региона
    // public static string RegionAsString(UInt64? region, RegionCoding coding)
    // {
    // if (region.HasValue)
    // return RegionAsString(region.Value, coding);
    // return null;
    // }

    // преобразовать строку в код региона
    /// <summary>
    /// The string as region.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="coding">
    /// The coding.
    /// </param>
    /// <returns>
    /// The <see cref="ulong"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// </exception>
    public static ulong StringAsRegion(string s, RegionCoding coding)
    {
      switch (coding)
      {
        case RegionCoding.ТФОМС:
          return Convert.ToByte(s);
        case RegionCoding.ОКАТО:
          return Convert.ToUInt32(s);
      }

      throw new ArgumentException("coding");
    }

    // преобразовать строку в обнуляемый код региона
    // public static UInt64? StringAsRegionNullable(string s, RegionCoding coding)
    // {
    // if (string.IsNullOrEmpty(s))
    // return null;
    // return StringAsRegion(s, coding);
    // }

    // преобразовать строку в код региона ТФОМС
    // public static Byte StringAsТФОМС(string s)
    // {
    // return (Byte)StringAsRegion(s, RegionCoding.ТФОМС);
    // }

    // преобразовать строку в обнуляемый код региона ТФОМС
    // public static Byte? StringAsТФОМСNullable(string s)
    // {
    // return (Byte?)StringAsRegionNullable(s, RegionCoding.ТФОМС);
    // }

    // преобразовать строку в код региона ОКАТО
    /// <summary>
    /// The string asокато.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    public static uint StringAsОКАТО(string s)
    {
      return (UInt32) StringAsRegion(s, RegionCoding.ОКАТО);
    }

    // преобразовать строку в обнуляемый код региона ОКАТО
    // public static UInt32? StringAsОКАТОNullable(string s)
    // {
    // return (UInt32?)StringAsRegionNullable(s, RegionCoding.ОКАТО);
    // }
  }
}