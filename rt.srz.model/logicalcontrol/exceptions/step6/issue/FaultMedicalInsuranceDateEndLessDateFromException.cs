// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultMedicalInsuranceDateEndLessDateFromException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault medical insurance date end less date from exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6.issue
{
  #region

  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  #endregion

  /// <summary>
  ///   The fault medical insurance date end less date from exception.
  /// </summary>
  [Serializable]
  public class FaultMedicalInsuranceDateEndLessDateFromException : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultMedicalInsuranceDateEndLessDateFromException" /> class.
    /// </summary>
    public FaultMedicalInsuranceDateEndLessDateFromException()
      : base(
        new ExceptionInfo(Resource.FaultMedicalInsuranceDateEndLessDateFromCode), 
        Resource.FaultMedicalInsuranceDateEndLessDateFromMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultMedicalInsuranceDateEndLessDateFromException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultMedicalInsuranceDateEndLessDateFromException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}