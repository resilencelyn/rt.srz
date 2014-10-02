namespace rt.srz.model.logicalcontrol.exceptions.step5
{
  using System.Runtime.Serialization;

  public class FaultStep5 : LogicalControlException
  {
    public FaultStep5(ExceptionInfo info, string message)
      : base(info, message)
    {
    }

    public FaultStep5(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    protected override int Step()
    {
      return 5;
    }
  }
}
