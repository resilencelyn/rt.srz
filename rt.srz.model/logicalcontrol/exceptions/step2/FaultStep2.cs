namespace rt.srz.model.logicalcontrol.exceptions.step2
{
  using System.Runtime.Serialization;

  public class FaultStep2 : LogicalControlException
  {
    public FaultStep2(ExceptionInfo info, string message)
      : base(info, message)
    {
    }

    public FaultStep2(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    protected override int Step()
    {
      return 2;
    }
  }
}
