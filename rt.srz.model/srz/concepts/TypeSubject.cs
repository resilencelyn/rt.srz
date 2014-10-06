// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeSubject.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Субъект взаимодействия
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary>
  ///   Субъект взаимодействия
  /// </summary>
  public class TypeSubject : Concept
  {
    #region Constants

    /// <summary> Центральный сегмент ЕРЗ </summary>
    public const int Erz = 550;

    /// <summary> The kladr /// </summary>
    public const int Kladr = 625;

    /// <summary> ОПФР </summary>
    public const int Pfr = 551;

    /// <summary> СМО </summary>
    public const int Smo = 626;

    /// <summary> ТФОМС </summary>
    public const int Tfoms = 646;

    /// <summary> The undefined /// </summary>
    public const int Undefined = 574;

    /// <summary> загс /// </summary>
    public const int Zags = 603;

    #endregion
  }
}