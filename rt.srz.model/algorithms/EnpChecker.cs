// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnpChecker.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The enp.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.algorithms
{
  using System;
  using System.Globalization;
  using System.Text;

  using rt.srz.model.HL7.dotNetX;

  /// <summary>
  ///   The enp.
  /// </summary>
  public static class EnpChecker
  {
    #region Static Fields

    /// <summary>
    ///   The full length.
    /// </summary>
    public static readonly byte FullLength = 0x10;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The append check sum.
    /// </summary>
    /// <param name="pro_id">
    /// The pro_id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string AppendCheckSum(string pro_id)
    {
      return pro_id + CalculateCheckSumAsString(pro_id, false);
    }

    /// <summary>
    /// The append check sum.
    /// </summary>
    /// <param name="pro_id">
    /// The pro_id.
    /// </param>
    public static void AppendCheckSum(StringBuilder pro_id)
    {
      pro_id.Append(CalculateCheckSumAsString(pro_id, false));
    }

    /// <summary>
    /// The calculate check sum.
    /// </summary>
    /// <param name="pro_id">
    /// The pro_id.
    /// </param>
    /// <param name="hasCheckPosition">
    /// The has check position.
    /// </param>
    /// <returns>
    /// The <see cref="byte"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// </exception>
    public static byte CalculateCheckSum(TStringHelper.ReadonlyString pro_id, bool hasCheckPosition = false)
    {
      var length = pro_id.Length;
      if (hasCheckPosition)
      {
        length--;
      }

      if (length < 2)
      {
        throw new ArgumentException("слишком короткий идентификатор");
      }

      var builder = new StringBuilder();
      var s = new StringBuilder();
      for (var flag = true; length > 0; flag = !flag)
      {
        var ch = pro_id[--length];
        if (flag)
        {
          builder.Append(ch);
        }
        else
        {
          s.Append(ch);
        }
      }

      s.Append((Convert.ToUInt64(builder.ToString()) * 2L).ToString());
      ulong num2 = 0L;
      length = s.Length;
      while (length > 0)
      {
        num2 += NumbersHelper.GetDecimalDigitValue(s, --length);
      }

      num2 = num2 % 10L;
      if (num2 > 0L)
      {
        num2 = 10L - num2;
      }

      return (byte)num2;
    }

    /// <summary>
    /// The calculate check sum as string.
    /// </summary>
    /// <param name="pro_id">
    /// The pro_id.
    /// </param>
    /// <param name="hasCheckPosition">
    /// The has check position.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CalculateCheckSumAsString(TStringHelper.ReadonlyString pro_id, bool hasCheckPosition = false)
    {
      return CalculateCheckSum(pro_id, hasCheckPosition).ToString();
    }

    /// <summary>
    /// Check birthday and gender by ENP
    /// </summary>
    /// <param name="id">
    /// </param>
    /// <param name="birthday">
    /// </param>
    /// <param name="isMan">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CheckBirthdayAndGender(string id, DateTime birthday, bool isMan)
    {
      var faset = GetFacet(birthday, isMan);
      return id.Substring(2, faset.Length) == faset;
    }

    /// <summary>
    /// The check identifier.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CheckIdentifier(string id)
    {
      if (id == null)
      {
        return false;
      }

      var length = id.Length;
      if (length != FullLength)
      {
        return false;
      }

      var decimalDigitValue = NumbersHelper.GetDecimalDigitValue(id, length - 1);
      var hasCheckPosition = true;
      return CalculateCheckSum(id, hasCheckPosition) == decimalDigitValue;
    }

    /// <summary>
    /// The check identifier.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static bool CheckIdentifier(ulong id)
    {
      return CheckIdentifier(NumbersHelper.NumberToString(id, FullLength));
    }

    /// <summary>
    /// Возвращает фасету
    /// </summary>
    /// <param name="birthday">
    /// </param>
    /// <param name="isMan">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetFacet(DateTime birthday, bool isMan)
    {
      var m = birthday.Month;
      var y = birthday.Year;
      var d = birthday.Day;

      if (birthday.Year <= 1950)
      {
        m += 20;
      }
      else
      {
        if (birthday.Year <= 2000)
        {
          m += 40;
        }
      }

      var cd = isMan
                 ? (d + 50).ToString(CultureInfo.InvariantCulture)
                 : d.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');

      var cm = m.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
      var cy = y.ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');

      cm = (9 - byte.Parse(cm.Substring(0, 1))).ToString(CultureInfo.InvariantCulture)
           + (9 - byte.Parse(cm.Substring(1, 1))).ToString(CultureInfo.InvariantCulture);

      cd = (9 - byte.Parse(cd.Substring(0, 1))).ToString(CultureInfo.InvariantCulture)
           + (9 - byte.Parse(cd.Substring(1, 1))).ToString(CultureInfo.InvariantCulture);

      cy = (9 - byte.Parse(cy.Substring(3, 1))).ToString(CultureInfo.InvariantCulture)
           + (9 - byte.Parse(cy.Substring(2, 1))).ToString(CultureInfo.InvariantCulture)
           + (9 - byte.Parse(cy.Substring(1, 1))).ToString(CultureInfo.InvariantCulture)
           + (9 - byte.Parse(cy.Substring(0, 1))).ToString(CultureInfo.InvariantCulture);

      return cm + cy + cd;
    }

    /// <summary>
    /// The get facet.
    /// </summary>
    /// <param name="polisNumber">
    /// The polis number.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetTfFacet(string polisNumber)
    {
      if (!string.IsNullOrEmpty(polisNumber) && polisNumber.Length == 16)
      {
        return polisNumber.Substring(0, 10);
      }

      return null;
    }

    /// <summary>
    /// The reset check sum.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// </exception>
    public static string ResetCheckSum(string id)
    {
      var length = id.Length;
      if (length != FullLength)
      {
        throw new ArgumentException("некорректный идентификатор");
      }

      var hasCheckPosition = true;
      var str = CalculateCheckSumAsString(id, hasCheckPosition);
      if (id[--length] == str[0])
      {
        return id;
      }

      var builder = new StringBuilder(id);
      builder[length] = str[0];
      return builder.ToString();
    }

    /// <summary>
    /// The reset check sum.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <exception cref="ArgumentException">
    /// </exception>
    public static void ResetCheckSum(StringBuilder id)
    {
      var length = id.Length;
      if (length != FullLength)
      {
        throw new ArgumentException("некорректный идентификатор");
      }

      var hasCheckPosition = true;
      var str = CalculateCheckSumAsString(id, hasCheckPosition);
      id[length - 1] = str[0];
    }

    #endregion
  }
}