// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExchangeFactory.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The exporter factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.exchange
{
  #region references

  using System.Collections.Generic;
  using System.Linq;

  using rt.atl.business.exchange.interfaces;

  #endregion

  /// <summary>
  ///   The exporter factory.
  /// </summary>
  public class ExchangeFactory : IExchangeFactory
  {
    #region Fields

    /// <summary>
    ///   The exporters.
    /// </summary>
    private readonly IExchange[] exporters;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ExchangeFactory"/> class.
    /// </summary>
    /// <param name="exporters">
    /// The exporters.
    /// </param>
    public ExchangeFactory(IExchange[] exporters)
    {
      this.exporters = exporters;
    }

    #endregion

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
    public IEnumerable<IExchange> GetExporter(ExchangeTypeEnum type)
    {
      return exporters.Where(x => x.AppliesTo(type));
    }

    #endregion
  }
}