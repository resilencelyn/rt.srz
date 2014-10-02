namespace rt.srz.model.logicalcontrol.exceptions.step4
{
  using System.Runtime.Serialization;

  public class FaultStep4 : LogicalControlException
  {
    public FaultStep4(ExceptionInfo info, string message)
      : base(info, message)
    {
    }

    public FaultStep4(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    protected override int Step()
    {
      return 4;
    }
  }
}
