namespace rt.srz.model.logicalcontrol.exceptions.step6
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  using Resource = rt.srz.model.barcode.Properties.Resource;

  [Serializable]
  public class FaultPoliceCertificateDateNotNeyPolisException : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    public FaultPoliceCertificateDateNotNeyPolisException()
      : base(new ExceptionInfo(Resource.FaultPoliceCertificateDateNotNeyPolisExceptionCode), Resource.FaultPoliceCertificateDateNotNeyPolisExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPoliceCertificateDateNotNeyPolisException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}