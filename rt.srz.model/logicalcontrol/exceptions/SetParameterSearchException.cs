namespace rt.srz.model.logicalcontrol.exceptions
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  using Resource = rt.srz.model.barcode.Properties.Resource;

  [Serializable]
  public class SetParameterSearchException: LogicalControlException
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SetParameterSearchException"/> class.
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

    /// <summary>
    ///   Gets or sets the info.
    /// </summary>
    [DataMember]
    public ExceptionInfo Info { get; set; }

    /// <summary>
    /// The step.
    /// </summary>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    protected override int Step()
    {
      return 1;
    }
  }
}