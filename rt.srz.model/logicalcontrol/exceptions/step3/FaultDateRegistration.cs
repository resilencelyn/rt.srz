namespace rt.srz.model.logicalcontrol.exceptions.step3
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  using Resource = rt.srz.model.barcode.Properties.Resource;

  [Serializable]
  public class FaultDateRegistration : FaultStep3
  {
    #region Constructors and Destructors

    public FaultDateRegistration()
      : base(new ExceptionInfo(Resource.FaultDateRegistrationCode), Resource.FaultDateRegistrationMessage)
    {
    }

    protected FaultDateRegistration(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}