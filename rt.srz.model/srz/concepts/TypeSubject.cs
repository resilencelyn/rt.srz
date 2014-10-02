// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeSubject.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary>
  ///   Субъект взаимодействия
  /// </summary>
  public class TypeSubject : Concept
  {
    /// <summary> Центральный сегмент ЕРЗ </summary>
    public const int Erz = 550;

    /// <summary> ОПФР </summary>
    public const int Pfr = 551;

    /// <summary> загс /// </summary>
    public const int Zags = 603;

    /// <summary> The kladr /// </summary>
    public const int Kladr = 625;

    /// <summary> СМО </summary>
    public const int Smo = 626;

    /// <summary> ТФОМС </summary>
    public const int Tfoms = 646;

    /// <summary> The undefined /// </summary>
    public const int Undefined = 574;
  }
}