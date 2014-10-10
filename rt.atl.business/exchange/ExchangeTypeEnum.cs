// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExchangeTypeEnum.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The exporterType enum.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.exchange
{
  /// <summary>
  ///   The exporterType enum.
  /// </summary>
  public enum ExchangeTypeEnum
  {
    /// <summary>
    ///   The export to srz.
    /// </summary>
    ExportToSrz = 1, 

    /// <summary>
    ///   The sinhronize nsi.
    /// </summary>
    SinhronizeNsi = 2, 

    /// <summary>
    ///   The export to pvp.
    /// </summary>
    ExportToPvp = 3, 

    /// <summary>
    ///   The first loading to pvp
    /// </summary>
    FirstLoadingToPvp = 4,

    /// <summary>
    /// The export to pvp uec.
    /// </summary>
    ExportToPvpUec = 5
  }
}