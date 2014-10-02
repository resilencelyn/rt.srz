// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusStatement.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Статусы заявления на выдачу полиса ОМС
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace rt.srz.model.srz.concepts
{
  using System.Globalization;

  /// <summary> Статусы заявления на выдачу полиса ОМС </summary>
  public class StatusStatement : Concept
  {
    /// <summary> Новое </summary>
    public const int New = 285;

    /// <summary> Проверка правомерности </summary>
    public const int CheckingTheValidity = 286;

    /// <summary> Подлежит исполнению </summary>
    public const int Enforceable = 287;

    /// <summary> Отменено </summary>
    public const int Cancelled = 288;

    /// <summary> Отклонено </summary>
    public const int Declined = 289;

    /// <summary> Исполняется </summary>
    public const int Performed = 290;

    /// <summary> Исполнено </summary>
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