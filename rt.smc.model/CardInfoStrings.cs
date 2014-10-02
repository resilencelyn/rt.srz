//-----------------------------------------------------------------------
// <copyright file="CardInfoStrings.cs" company="Rintech" author="Syurov">
//     Copyright (c) 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace rt.smc.model
{
  /// <summary>The card info strings.</summary>
  [ComVisible(true)]
  public class CardInfoStrings
  {
    /// <summary>
    /// Номер карты
    /// </summary>
    public string CardHexNum { get; set; }

    /// <summary>
    /// Тип карты
    /// </summary>
    public byte CardType { get; set; }

    /// <summary>
    /// Версия карты
    /// </summary>
    public short Vers { get; set; }

    /// <summary>
    /// Идентификатор учреждения
    /// </summary>
    public string InstitId { get; set; }

    /// <summary>
    /// Дополнительная информация
    /// </summary>
    public string AddInfo { get; set; }

    /// <summary>
    /// Код производителя чипа карты
    /// </summary>
    public byte IssuerCode { get; set; }

    /// <summary>
    /// Данные о производителе чипа карты
    /// </summary>
    public string IssuerData { get; set; }
  }
}