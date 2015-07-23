// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeEvent.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Тип События
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary> Тип События </summary>
  public class TypeEvent : Concept
  {
    #region Constants

    /// <summary> Создание записи </summary>
    public const int Create = 270;

    /// <summary> Удаление записи </summary>
    public const int Remove = 272;

    /// <summary> Изменение записи </summary>
    public const int Update = 271;

    #endregion
  }
}