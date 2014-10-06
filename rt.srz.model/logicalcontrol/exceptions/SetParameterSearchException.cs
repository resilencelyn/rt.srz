// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetParameterSearchException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The set parameter search exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  /// <summary>
  /// The set parameter search exception.
  /// </summary>
  [Serializable]
  public class SetParameterSearchException : LogicalControlException
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SetParameterSearchException" /> class.
    /// </summary>
    public SetParameterSearchException()
      : base(new ExceptionInfo(Resource.SetParameterSearchExceptionCode), Resource.SetParameterSearchExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SetParameterSearchException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    public SetParameterSearchException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the info.
    /// </summary>
    [DataMember]
    public ExceptionInfo Info { get; set; }

    #endregion

    #region Methods

    /// <summary>
    ///   The step.
    /// </summary>
    /// <returns>
    ///   The <see cref="int" />.
    /// </returns>
    protected override int Step()
    {
      return 1;
    }

    #endregion
  }
}