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
    // ������ ����� ���� �����
    // public static readonly byte �����_FullLength = 2;

    // ������ ����� ������������� ���� ����� ��
    // public static readonly byte �����_FullLength = 5;

    // �������� ������ ����� ���� �������
    // public static byte RetrieveFullLength(RegionCoding coding)
    // {
    // switch (coding)
    // {
    // case RegionCoding.�����:
    // return �����_FullLength;
    // case RegionCoding.�����:
    // return �����_FullLength;
    // }
    // return 0;
    // }

    // ������������� ����� � ������, �������� ����� ������ �� ��������� ���������� ����
    // public static string NumberToString<T>(T number, byte digits = 0) where T : IFormattable
    // {
    // return (digits > 0) ? number.ToString(string.Concat("D", digits.ToString()), formatProvider: null) : number.ToString();
    // }

    // �������� ������ � ����� �������
    // public static string RegionAsString(UInt64 region, RegionCoding coding)
    // {
    // return NumberToString(region, RetrieveFullLength(coding));
    // }

    // �������� ������ � ����� �������
    // public static string RegionAsString(UInt64? region, RegionCoding coding)
    // {
    // if (region.HasValue)
    // return RegionAsString(region.Value, coding);
    // return null;
    // }

    // ������������� ������ � ��� �������
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
        case RegionCoding.�����:
          return Convert.ToByte(s);
        case RegionCoding.�����:
          return Convert.ToUInt32(s);
      }

      throw new ArgumentException("coding");
    }

    // ������������� ������ � ���������� ��� �������
    // public static UInt64? StringAsRegionNullable(string s, RegionCoding coding)
    // {
    // if (string.IsNullOrEmpty(s))
    // return null;
    // return StringAsRegion(s, coding);
    // }

    // ������������� ������ � ��� ������� �����
    // public static Byte StringAs�����(string s)
    // {
    // return (Byte)StringAsRegion(s, RegionCoding.�����);
    // }

    // ������������� ������ � ���������� ��� ������� �����
    // public static Byte? StringAs�����Nullable(string s)
    // {
    // return (Byte?)StringAsRegionNullable(s, RegionCoding.�����);
    // }

    // ������������� ������ � ��� ������� �����
    /// <summary>
    /// The string as�����.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    public static uint StringAs�����(string s)
    {
      return (UInt32) StringAsRegion(s, RegionCoding.�����);
    }

    // ������������� ������ � ���������� ��� ������� �����
    // public static UInt32? StringAs�����Nullable(string s)
    // {
    // return (UInt32?)StringAsRegionNullable(s, RegionCoding.�����);
    // }
  }
}