// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExchange.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The Exprot interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Quartz;
namespace rt.atl.business.exchange.interfaces
{
  /// <summary>
  ///   The Exprot interface.
  /// </summary>
  public interface IExchange
  {
    #region Public Methods and Operators

    /// <summary>
    /// The type to.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool AppliesTo(ExchangeTypeEnum type);

    /// <summary>
    ///   The run.
    /// </summary>
    void Run(IJobExecutionContext context);

    #endregion
  }
}