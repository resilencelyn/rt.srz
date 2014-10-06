// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExchange.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The Exprot interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.exchange.interfaces
{
  using Quartz;

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
    /// The run.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    void Run(IJobExecutionContext context);

    #endregion
  }
}