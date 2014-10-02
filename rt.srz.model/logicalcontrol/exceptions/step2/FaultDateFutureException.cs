namespace rt.srz.model.logicalcontrol.exceptions.step2
{
  using System;
  using System.Runtime.Serialization;

  using Resource = rt.srz.model.barcode.Properties.Resource;

  /// <summary>
  ///   The fault snils exception.
  /// </summary>
  [Serializable]
  public class FaultDateFutureException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDateFutureException"/> class.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    public FaultDateFutureException(string name)
      : base(new ExceptionInfo(Resource.FaultDateFutureExceptionCode), string.Format(Resource.FaultDateFutureExceptionMessage, name))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDateFutureException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDateFutureException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

 #endregion
  }
}