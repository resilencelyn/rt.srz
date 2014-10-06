// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultDateRegistration.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault date registration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step3
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  /// <summary>
  /// The fault date registration.
  /// </summary>
  [Serializable]
  public class FaultDateRegistration : FaultStep3
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDateRegistration"/> class.
    /// </summary>
    public FaultDateRegistration()
      : base(new ExceptionInfo(Resource.FaultDateRegistrationCode), Resource.FaultDateRegistrationMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDateRegistration"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDateRegistration(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}