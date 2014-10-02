namespace rt.srz.model.logicalcontrol.exceptions.step6
{
  using System.Runtime.Serialization;

  public class FaultStep6 : LogicalControlException
  {
    public FaultStep6(ExceptionInfo info, string message)
      : base(info, message)
    {
    }

    public FaultStep6(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    protected override int Step()
    {
      return 6;
    }
  }
}
