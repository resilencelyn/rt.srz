// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPoliceCertificateDateFromMoreThenDateEnd.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault police certificate date from more then date end.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6.issue
{
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  /// <summary>
  /// The fault police certificate date from more then date end.
  /// </summary>
  public class FaultPoliceCertificateDateFromMoreThenDateEnd : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPoliceCertificateDateFromMoreThenDateEnd"/> class. 
    ///   Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    public FaultPoliceCertificateDateFromMoreThenDateEnd()
      : base(
        new ExceptionInfo(Resource.FaultPoliceCertificateDateFromMoreThenDateEndExceptionCode), 
        Resource.FaultPoliceCertificateDateFromMoreThenDateEndMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPoliceCertificateDateFromMoreThenDateEnd"/> class. 
    /// Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPoliceCertificateDateFromMoreThenDateEnd(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}