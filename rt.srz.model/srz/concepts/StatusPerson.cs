// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusPerson.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Статус застрахованного
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary> Статус застрахованного </summary>
  public class StatusPerson : Concept
  {
    #region Constants

    /// <summary> Застрахован </summary>
    public const int Active = 465;

    /// <summary> Аннулирован </summary>
    public const int Annuled = 466;

    /// <summary> Умерший </summary>
    public const int Dead = 467;

    #endregion
  }
}