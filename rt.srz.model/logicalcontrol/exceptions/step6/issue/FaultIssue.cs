namespace rt.srz.model.logicalcontrol.exceptions.step6
{
  using System.Runtime.Serialization;

  public class FaultIssue : FaultStep6
  {
    public FaultIssue(ExceptionInfo info, string message)
      : base(info, message)
    {
    }

    public FaultIssue(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    protected override int Step()
    {
      return 6;
    }
  }
}