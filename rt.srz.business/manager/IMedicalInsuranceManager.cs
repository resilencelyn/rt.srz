// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMedicalInsuranceManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The MedicalInsuranceManager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System.Collections.Generic;

  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The MedicalInsuranceManager interface.
  /// </summary>
  public partial interface IMedicalInsuranceManager
  {
    #region Public Methods and Operators

    #endregion

    /// <summary>
    /// The save medical insurances.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    void SaveMedicalInsurances(Statement statement);

    void ReflectionMedicalInsured2(Statement statement, IList<MedicalInsurance> medicalInsurances, bool saveInSession);
  }
}