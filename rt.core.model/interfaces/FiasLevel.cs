// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiasLevel.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.interfaces
{
  /// <summary>
  ///   Cодержит номер уровня классификации адресных объектов.
  /// </summary>
  public enum FiasLevel
  {
    /// <summary>
    ///  Уровень региона 
    /// </summary>
    Subject = 1,

    /// <summary>
    /// уровень автономного округа
    /// </summary>
    AutonomousOkrug = 2,

    /// <summary>
    ///  Уровень района
    /// </summary>
    Area = 3,

    /// <summary>
    ///   Уровень города
    /// </summary>
    City = 4,

    /// <summary>
    /// уровень внутригородской территории
    /// </summary>
    InCity = 5,

    /// <summary>
    ///   Уровень населенного пункта
    /// </summary>
    Town = 6,

    /// <summary>
    ///   Уровень улицы
    /// </summary>
    Street = 7
  }
}