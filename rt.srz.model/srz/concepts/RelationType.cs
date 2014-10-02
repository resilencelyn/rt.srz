// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelationType.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
    /// <summary> Мать </summary>
    public const int Mother = 318;

    /// <summary> Отец </summary>
    public const int Father = 319;

    /// <summary> Иное </summary>
    public const int Other = 320;
  }
}