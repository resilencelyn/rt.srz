// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultMedicalInsuranceDateNotEquals30Exception.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The fault medical insurance date not equals 30 exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6
{
  #region

  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  #endregion

  /// <summary>
  /// The fault medical insurance date not equals 30 exception.
  /// </summary>
  [Serializable]
  public class FaultMedicalInsuranceDateNotEquals30Exception : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultMedicalInsuranceDateNotEquals30Exception"/> class.
    /// </summary>
    public FaultMedicalInsuranceDateNotEquals30Exception()
      : base(
        new ExceptionInfo(Resource.FaultMedicalInsuranceDateNotEquals30Code), 
        Resource.FaultMedicalInsuranceDateNotEquals30Message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultMedicalInsuranceDateNotEquals30Exception"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultMedicalInsuranceDateNotEquals30Exception(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}