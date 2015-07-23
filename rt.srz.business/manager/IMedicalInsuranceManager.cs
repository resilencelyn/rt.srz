// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMedicalInsuranceManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The MedicalInsuranceManager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using System.Collections.Generic;

  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The MedicalInsuranceManager interface.
  /// </summary>
  public partial interface IMedicalInsuranceManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The calculate en period working day.
    /// </summary>
    /// <param name="dateFrom">
    /// The date from.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    DateTime CalculateEndPeriodWorkingDay(DateTime dateFrom, int count);

    /// <summary>
    /// The reflection medical insured 2.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="medicalInsurances">
    /// The medical insurances.
    /// </param>
    /// <param name="saveInSession">
    /// The save in session.
    /// </param>
    void ReflectionMedicalInsured2(Statement statement, IList<MedicalInsurance> medicalInsurances, bool saveInSession);

    /// <summary>
    /// The save medical insurances.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    void SaveMedicalInsurances(Statement statement);

    #endregion
  }
}