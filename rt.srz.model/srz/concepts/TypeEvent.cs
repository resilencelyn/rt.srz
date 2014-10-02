// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeEvent.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
    /// <summary> Создание записи </summary>
    public const int Create = 270;

    /// <summary> Изменение записи </summary>
    public const int Update = 271;

    /// <summary> Удаление записи </summary>
    public const int Remove = 272;
  }
}