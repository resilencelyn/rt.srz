// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelationType.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Отношение к застрахованному лицу
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary> Отношение к застрахованному лицу </summary>
  public class RelationType : Concept
  {
    #region Constants

    /// <summary> Отец </summary>
    public const int Father = 319;

    /// <summary> Мать </summary>
    public const int Mother = 318;

    /// <summary> Иное </summary>
    public const int Other = 320;

    #endregion
  }
}