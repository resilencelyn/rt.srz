// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusStatement.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   ������� ��������� �� ������ ������ ���
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace rt.srz.model.srz.concepts
{
  using System.Globalization;

  /// <summary> ������� ��������� �� ������ ������ ��� </summary>
  public class StatusStatement : Concept
  {
    /// <summary> ����� </summary>
    public const int New = 285;

    /// <summary> �������� ������������� </summary>
    public const int CheckingTheValidity = 286;

    /// <summary> �������� ���������� </summary>
    public const int Enforceable = 287;

    /// <summary> �������� </summary>
    public const int Cancelled = 288;

    /// <summary> ��������� </summary>
    public const int Declined = 289;

    /// <summary> ����������� </summary>
    public const int Performed = 290;

    /// <summary> ��������� </summary>
    public const int Exercised = 291;

    /// <summary>
    /// The can canceled.
    /// </summary>
    /// <param name="statusId">
    /// The status id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CanCanceled(int statusId)
    {
      if (statusId == New)
      {
        return true;
      }

      if (statusId == Declined)
      {
        return true;
      }

      return false;
    }

    /// <summary>
    /// The is annuled.
    /// </summary>
    /// <param name="statusId">
    /// The status id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsAnnuled(int statusId)
    {
      if (statusId == Cancelled)
      {
        return true;
      }

      if (statusId == Declined)
      {
        return true;
      }

      return false;
    }
  }
}