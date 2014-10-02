// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KLADRObjectLevel.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Cодержит номер уровня классификации адресных объектов.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace rt.srz.model.enumerations
{
  /// <summary>
  ///   Cодержит номер уровня классификации адресных объектов.
  /// </summary>
  public enum KLADRLevel
  {
    /// <summary>
    ///   The subject.
    /// </summary>
    Subject = 1, // Уровень региона
    /// <summary>
    ///   The area.
    /// </summary>
    Area = 2, // Уровень района
    /// <summary>
    ///   The city.
    /// </summary>
    City = 3, // Уровень города
    /// <summary>
    ///   The town.
    /// </summary>
    Town = 4, // Уровень населенного пункта
    /// <summary>
    ///   The street.
    /// </summary>
    Street = 5 // Уровень улицы
  }
}