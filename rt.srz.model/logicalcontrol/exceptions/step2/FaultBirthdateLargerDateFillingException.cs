namespace rt.srz.model.logicalcontrol.exceptions.step2
{
  using System;
  using System.Runtime.Serialization;

  using Resource = rt.srz.model.barcode.Properties.Resource;

  /// <summary>
  /// The fault birthdate larger document date issue exception.
  /// </summary>
  [Serializable]
  public class FaultBirthdateLargerDateFillingException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultBirthdateLargerDateFillingException"/> class.
    /// </summary>
    public FaultBirthdateLargerDateFillingException()
      : base(
        new ExceptionInfo(Resource.FaultBirthdateLargerDateFillingExceptionCode), 
        Resource.FaultBirthdateLargerDateFillingExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultBirthdateLargerDateFillingException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultBirthdateLargerDateFillingException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

   #endregion
  }
}