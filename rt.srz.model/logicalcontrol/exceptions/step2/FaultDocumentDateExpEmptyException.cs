namespace rt.srz.model.logicalcontrol.exceptions.step2
{
  using System;
  using System.Runtime.Serialization;

  using Resource = rt.srz.model.barcode.Properties.Resource;

  /// <summary>
  ///   The fault snils exception.
  /// </summary>
  [Serializable]
  public class FaultDocumentDateExpEmptyException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultDocumentDateExpEmptyException" /> class.
    /// </summary>
    public FaultDocumentDateExpEmptyException()
      : base(
        new ExceptionInfo(Resource.FaultDocumentDateExpEmptyExceptionCode), 
        Resource.FaultDocumentDateExpEmptyExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDocumentDateExpEmptyException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDocumentDateExpEmptyException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

   #endregion
  }
}