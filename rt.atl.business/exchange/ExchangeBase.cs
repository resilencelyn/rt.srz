// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExchangeBase.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The exprot.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.exchange
{
  #region references

  using System.Collections.Generic;

  using Quartz;

  using rt.atl.business.exchange.interfaces;

  #endregion

  /// <summary>
  ///   The exprot.
  /// </summary>
  public abstract class ExchangeBase : IExchange
  {
    #region Fields

    /// <summary>
    ///   The type to.
    /// </summary>
    private readonly List<ExchangeTypeEnum> appliesTo;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ExchangeBase"/> class.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    protected ExchangeBase(ExchangeTypeEnum type)
    {
      appliesTo = new List<ExchangeTypeEnum> { type };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExchangeBase"/> class.
    /// </summary>
    /// <param name="types">
    /// The type.
    /// </param>
    protected ExchangeBase(IEnumerable<ExchangeTypeEnum> types)
    {
      appliesTo = new List<ExchangeTypeEnum>(types);
    }

    #endregion

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
    public bool AppliesTo(ExchangeTypeEnum type)
    {
      return appliesTo.Contains(type);
    }

    /// <summary>
    /// The run.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public abstract void Run(IJobExecutionContext context);

    #endregion
  }
}