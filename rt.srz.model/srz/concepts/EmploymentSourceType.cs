// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmploymentSourceType.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Источник информации о занятости
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary> Источник информации о занятости </summary>
  public class EmploymentSourceType : Concept
  {
    #region Constants

    /// <summary> ОПФР </summary>
    public const int EmploymentSourceType1 = 594;

    /// <summary> Завяление застрахованного лица </summary>
    public const int EmploymentSourceType2 = 595;

    #endregion
  }
}