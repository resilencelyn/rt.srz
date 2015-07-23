// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUirManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface UirManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using rt.srz.model.interfaces.service.uir;

  /// <summary>
  ///   The interface UirManager.
  /// </summary>
  public interface IUirManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get med ins state.
    /// </summary>
    /// <param name="request">
    /// The request.
    /// </param>
    /// <returns>
    /// The <see cref="Response"/> .
    /// </returns>
    Response GetMedInsState(Request request);

    /// <summary>
    /// The get med ins state 2.
    /// </summary>
    /// <param name="request">
    /// The request.
    /// </param>
    /// <returns>
    /// The <see cref="Response"/> .
    /// </returns>
    Response GetMedInsState2(Request2 request);

    #endregion
  }
}