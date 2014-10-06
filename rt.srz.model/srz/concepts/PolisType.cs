// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PolisType.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Форма изготовления полиса
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary> Форма изготовления полиса </summary>
  public class PolisType : Concept
  {
    #region Constants

    /// <summary> Временное свидетельство </summary>
    public const int В = 439;

    /// <summary> Полис ОМС в составе универсальной электронной карты </summary>
    public const int К = 322;

    /// <summary> Бумажный полис ОМС единого образца </summary>
    public const int П = 321;

    /// <summary> Полис ОМС старого образ-ца </summary>
    public const int С = 438;

    /// <summary> Электронный полис ОМС единого образца </summary>
    public const int Э = 440;

    #endregion
  }
}