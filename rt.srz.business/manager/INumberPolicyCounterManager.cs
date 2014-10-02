// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMedicalInsuranceManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The INumberPolicyCounterManager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The INumberPolicyCounterManager interface.
  /// </summary>
  public partial interface INumberPolicyCounterManager
  {
    /// <summary>
    /// The get next enp number facets.
    /// </summary>
    /// <param name="tfomsId">
    /// The tfoms id.
    /// </param>
    /// <param name="genderId">
    /// The gender id.
    /// </param>
    /// <param name="birthday">
    /// The birthday.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    string GetNextEnpNumber(Guid tfomsId, int genderId, DateTime birthday);

    /// <summary>
    /// The recalculate number policy counter.
    /// </summary>
    /// <param name="numberPolicy">
    /// The number policy.
    /// </param>
    void RecalculateNumberPolicyCounter(string numberPolicy);
  }
}
