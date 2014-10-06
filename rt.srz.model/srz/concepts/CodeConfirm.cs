// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeConfirm.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Таблица Б.59 Код подтверждения
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary> Таблица Б.59 Код подтверждения </summary>
  public class CodeConfirm : Concept
  {
    #region Constants

    /// <summary> Прикладное под-тверждение: прием-лемо </summary>
    public const int AA = 599;

    /// <summary> Прикладное подтверждение: ошибка </summary>
    public const int AE = 600;

    /// <summary> Прикладное подтверждение: отвергнуто </summary>
    public const int AR = 601;

    /// <summary> Подтверждение при-ёма: принято </summary>
    public const int CA = 596;

    /// <summary> Подтверждение при-ёма: ошибка </summary>
    public const int CE = 597;

    /// <summary> Подтверждение при-ёма: отвергнуто </summary>
    public const int CR = 598;

    /// <summary>
    ///   Не выгруженное сообщение
    /// </summary>
    public const int CodeConfirm0 = 638;

    #endregion
  }
}