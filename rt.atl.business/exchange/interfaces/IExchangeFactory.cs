// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExchangeFactory.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ExchangeFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.exchange.interfaces
{
  #region references

  using System.Collections.Generic;

  #endregion

  /// <summary>
  ///   The ExchangeFactory interface.
  /// </summary>
  public interface IExchangeFactory
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get exporter.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IEnumerable</cref>
    ///   </see>
    ///   .
    /// </returns>
    IEnumerable<IExchange> GetExporter(ExchangeTypeEnum type);

    #endregion
  }
}