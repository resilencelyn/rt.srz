// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultTemporaryCertificateNumberExists.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault temporary certificate number exists.
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
  ///   The fault temporary certificate number exists.
  /// </summary>
  [Serializable]
  public class FaultTemporaryCertificateNumberExists : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultTemporaryCertificateNumberExists" /> class.
    /// </summary>
    public FaultTemporaryCertificateNumberExists()
      : base(
        new ExceptionInfo(Resource.FaultTemporaryCertificateNumberExistsCode), 
        Resource.FaultTemporaryCertificateNumberExistsMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultTemporaryCertificateNumberExists"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultTemporaryCertificateNumberExists(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}