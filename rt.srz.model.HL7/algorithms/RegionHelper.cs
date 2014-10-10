// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegionHelper.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The region helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.algorithms
{
  #region references

  using System;

  using rt.srz.model.Hl7.dotNetX;

  #endregion

  /// <summary>
  ///   The region helper.
  /// </summary>
  [CLSCompliant(false)]
  public static class RegionHelper
  {
    #region Static Fields

    /// <summary>
    ///   The �����_ full length.
    /// </summary>
    public static readonly byte �����_FullLength = 5;

    /// <summary>
    ///   The �����_ full length.
    /// </summary>
    public static readonly byte �����_FullLength = 2;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The region as string.
    /// </summary>
    /// <param name="region">
    /// The region.
    /// </param>
    /// <param name="coding">
    /// The coding.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RegionAsString(ulong? region, RegionCoding coding)
    {
      if (region.HasValue)
      {
        return RegionAsString(region.Value, coding);
      }

      return null;
    }

    /// <summary>
    /// The region as string.
    /// </summary>
    /// <param name="region">
    /// The region.
    /// </param>
    /// <param name="coding">
    /// The coding.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RegionAsString(ulong region, RegionCoding coding)
    {
      return NumbersHelper.NumberToString(region, RetrieveFullLength(coding));
    }

    /// <summary>
    /// The retrieve full length.
    /// </summary>
    /// <param name="coding">
    /// The coding.
    /// </param>
    /// <returns>
    /// The <see cref="byte"/>.
    /// </returns>
    public static byte RetrieveFullLength(RegionCoding coding)
    {
      switch (coding)
      {
        case RegionCoding.�����:
          return �����_FullLength;

        case RegionCoding.�����:
          return �����_FullLength;
      }

      return 0;
    }

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

    /// <summary>
    /// The string as region nullable.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="coding">
    /// The coding.
    /// </param>
    /// <returns>
    /// The <see cref="ulong?"/>.
    /// </returns>
    public static ulong? StringAsRegionNullable(string s, RegionCoding coding)
    {
      if (string.IsNullOrEmpty(s))
      {
        return null;
      }

      return StringAsRegion(s, coding);
    }

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
      return (uint)StringAsRegion(s, RegionCoding.�����);
    }

    /// <summary>
    /// The string as����� nullable.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <returns>
    /// The <see cref="uint?"/>.
    /// </returns>
    public static uint? StringAs�����Nullable(string s)
    {
      var nullable = StringAsRegionNullable(s, RegionCoding.�����);
      if (!nullable.HasValue)
      {
        return null;
      }

      return (uint)nullable.GetValueOrDefault();
    }

    /// <summary>
    /// The string as�����.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <returns>
    /// The <see cref="byte"/>.
    /// </returns>
    public static byte StringAs�����(string s)
    {
      return (byte)StringAsRegion(s, RegionCoding.�����);
    }

    /// <summary>
    /// The string as����� nullable.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <returns>
    /// The <see cref="byte?"/>.
    /// </returns>
    public static byte? StringAs�����Nullable(string s)
    {
      var nullable = StringAsRegionNullable(s, RegionCoding.�����);
      if (!nullable.HasValue)
      {
        return null;
      }

      return (byte)nullable.GetValueOrDefault();
    }

    #endregion
  }
}