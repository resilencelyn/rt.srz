// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeContent.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Типы вложений, использующихся при обменах
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace rt.srz.model.srz.concepts
{
  /// <summary> Типы вложений, использующихся при обменах </summary>
  public class TypeContent : Concept
  {
    /// <summary> Заявление </summary>
    public const int TypeContent1 = 381;

    /// <summary> Фотография </summary>
    public const int Foto = 382;

    /// <summary> Иное </summary>
    public const int TypeContent20 = 388;

    /// <summary> Собственноручная подпись </summary>
    public const int Signature = 383;
  }
}