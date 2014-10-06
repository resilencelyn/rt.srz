// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultDateFillingLessThenLastStatement.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault date filling less then last statement.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step1
{
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  /// <summary>
  /// The fault date filling less then last statement.
  /// </summary>
  public class FaultDateFillingLessThenLastStatement : FaultStep1
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultDateFillingLessThenLastStatement" /> class.
    /// </summary>
    public FaultDateFillingLessThenLastStatement()
      : base(
        new ExceptionInfo(Resource.FaultDateFillingLessThenLastStatementExceptionCode), 
        Resource.FaultDateFillingLessThenLastStatementMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDateFillingLessThenLastStatement"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDateFillingLessThenLastStatement(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}