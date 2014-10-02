// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementSearchMenuItem.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls
{
  /// <summary>
  ///   Идентификаторы пунктов меню
  /// </summary>
  public enum StatementSearchMenuItem
  {
    /// <summary>
    ///   The create.
    /// </summary>
    Reinsuranse = 0, 

    /// <summary>
    ///   The create from example.
    /// </summary>
    Reneval = 1,

    /// <summary>
    ///   The open.
    /// </summary>
    Edit = 2,

    /// <summary>
    ///   The issue.
    /// </summary>
    Issue = 3,

    /// <summary>
    ///   The delete.
    /// </summary>
    Delete = 4, 

    /// <summary>
    ///   The read UEC.
    /// </summary>
    ReadUEC = 5, 

    /// <summary>
    ///   The write UEC.
    /// </summary>
    WriteUEC = 6,

    /// <summary>
    /// The read smard card.
    /// </summary>
    ReadSmardCard = 7,

    /// <summary>
    /// Разделение
    /// </summary>
    Separate = 8,

    /// <summary>
    /// История страхования
    /// </summary>
    InsuranceHistory = 9,

    /// <summary>
    /// Импорт данных из СРЗ
    /// </summary>
    SRZ = 10,

    /// <summary>
    /// Удаление инфы о смерти
    /// </summary>
    DeleteDeathInfo = 11
  }
}