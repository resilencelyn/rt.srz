// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KladrLevel.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Cодержит номер уровня классификации адресных объектов.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.interfaces
{
  /// <summary>
  ///   Cодержит номер уровня классификации адресных объектов.
  /// </summary>
  public enum KladrLevel
  {
    /// <summary>
    ///  Уровень региона
    /// </summary>
    Subject = 1, 

    /// <summary>
    ///    Уровень района
    /// </summary>
    Area = 2, 

    /// <summary>
    ///  Уровень города
    /// </summary>
    City = 3,

    /// <summary>
    ///  Уровень населенного пункта
    /// </summary>
    Town = 4, 

    /// <summary>
    ///   Уровень улицы
    /// </summary>
    Street = 5 
  }
}