namespace rt.srz.model.logicalcontrol.exceptions.step1
{
  using System.Runtime.Serialization;

  public class FaultStep1 : LogicalControlException
  {
    public FaultStep1(ExceptionInfo info, string message)
      : base(info, message)
    {
    }

    public FaultStep1(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    protected override int Step()
    {
      return 1;
    }
  }
}
