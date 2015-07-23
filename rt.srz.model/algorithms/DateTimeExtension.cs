// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtension.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The date time extension.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.algorithms
{
  #region references

  using System;
  using System.Globalization;

  #endregion

  /// <summary>
  ///   The date time extension.
  /// </summary>
  public static class DateTimeExtension
  {
    #region Public Methods and Operators

    /// <summary>
    /// ���������� ��� ���� � ��������� �� ���
    /// </summary>
    /// <param name="date1">
    /// The date1.
    /// </param>
    /// <param name="date2">
    /// The date2.
    /// </param>
    /// <returns>
    /// true ���� ���� ������ ���, ����� false
    /// </returns>
    public static bool DayEquals(this DateTime date1, DateTime date2)
    {
      return (date1.Date - date2.Date).Days == 0;
    }

    /// <summary>
    /// �������� ������������ ���� �� �������� ��-���������
    /// </summary>
    /// <param name="dt">
    /// ����
    /// </param>
    /// <param name="format">
    /// ������
    /// </param>
    /// <returns>
    /// ��������� ������
    /// </returns>
    public static string GetDefaultForIncorrectDate(this DateTime? dt, string format)
    {
      var result = dt.GetValueOrDefault(new DateTime(1900, 1, 1));
      return result.GetDefaultForIncorrectDate(format);
    }

    /// <summary>
    /// �������� ������������ ���� �� �������� ��-���������
    /// </summary>
    /// <param name="dt">
    /// ����
    /// </param>
    /// <param name="format">
    /// ������
    /// </param>
    /// <returns>
    /// ��������� ������
    /// </returns>
    public static string GetDefaultForIncorrectDate(this DateTime dt, string format)
    {
      var r = dt;

      if (r < new DateTime(1900, 1, 1))
      {
        r = new DateTime(1900, 1, 1);
      }
      else if (r > new DateTime(2200, 1, 1))
      {
        r = new DateTime(2200, 1, 1);
      }

      if (format == "s")
      {
        format = "yyyy-MM-ddThh:mm:ss";
      }

      format = format.Replace("yyyy", "{0}");
      format = format.Replace("MM", "{1}");
      format = format.Replace("dd", "{2}");
      format = format.Replace("hh", "{3}");
      format = format.Replace("mm", "{4}");
      format = format.Replace("ss", "{5}");

      return string.Format(
                           format, 
                           r.Year, 
                           AddZero(r.Month), 
                           AddZero(r.Day), 
                           AddZero(r.Hour), 
                           AddZero(r.Minute), 
                           AddZero(r.Second));
    }

    #endregion

    #region Methods

    /// <summary>
    /// ��������� ���� � ������ �����
    /// </summary>
    /// <param name="i">
    /// �����
    /// </param>
    /// <returns>
    /// ��������������� �����
    /// </returns>
    private static string AddZero(int i)
    {
      var str = i.ToString(CultureInfo.InvariantCulture);
      return str.Length == 1 ? string.Format("0{0}", i) : str;
    }

    #endregion
  }
}