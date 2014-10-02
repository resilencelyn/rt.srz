using rt.srz.model.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace rt.srz.model.logicalcontrol.exceptions.step3
{
  [Serializable]
  public class FaultAddressNotComplete : FaultStep3
  {
    #region Constructors and Destructors

    public FaultAddressNotComplete()
      : base(new ExceptionInfo(Resource.FaultAddressNotCompleteExceptionCode), Resource.FaultAddressNotCompleteExceptionMessage)
    {
    }

    protected FaultAddressNotComplete(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}
