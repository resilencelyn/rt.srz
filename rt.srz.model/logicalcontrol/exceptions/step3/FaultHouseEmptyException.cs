using rt.srz.model.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace rt.srz.model.logicalcontrol.exceptions.step3
{
  [Serializable]
  public class FaultHouseEmptyException : FaultStep3
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPostcodeException"/> class.
    /// </summary>
    public FaultHouseEmptyException()
      : base(new ExceptionInfo(Resource.FaultHouseEmptyExceptionCode), Resource.FaultHouseEmptyExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPostcodeException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultHouseEmptyException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}
