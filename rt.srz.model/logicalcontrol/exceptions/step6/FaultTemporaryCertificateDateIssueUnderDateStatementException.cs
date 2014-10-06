// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultTemporaryCertificateDateIssueUnderDateStatementException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault temporary certificate date issue exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6
{
  using System;
  using System.Runtime.Serialization;

  /// <summary>
  ///   The fault temporary certificate date issue exception.
  /// </summary>
  [Serializable]
  public class FaultTemporaryCertificateDateIssueUnderDateStatementException : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultTemporaryCertificateDateIssueUnderDateStatementException" /> class.
    /// </summary>
    public FaultTemporaryCertificateDateIssueUnderDateStatementException()
      : base(new ExceptionInfo("99"), "Дата выдачи ВС меньше даты заявления.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultTemporaryCertificateDateIssueUnderDateStatementException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultTemporaryCertificateDateIssueUnderDateStatementException(
      SerializationInfo info, 
      StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}