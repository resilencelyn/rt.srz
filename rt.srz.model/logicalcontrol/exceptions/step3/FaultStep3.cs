namespace rt.srz.model.logicalcontrol.exceptions.step3
{
  using System.Runtime.Serialization;

  public class FaultStep3 : LogicalControlException
  {
    public FaultStep3(ExceptionInfo info, string message)
      : base(info, message)
    {
    }

    public FaultStep3(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    protected override int Step()
    {
      return 3;
    }
  }
}
