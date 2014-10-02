using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using rt.srz.model.Properties;

namespace rt.srz.model.logicalcontrol.exceptions.step1
{
  public class FaultDateFillingLessThenLastStatement : FaultStep1
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDateFillingLessThenLastStatement"/> class. 
    /// </summary>
    public FaultDateFillingLessThenLastStatement()
      : base(
        new ExceptionInfo(Resource.FaultDateFillingLessThenLastStatementExceptionCode),
          Resource.FaultDateFillingLessThenLastStatementMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDateFillingLessThenLastStatement"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDateFillingLessThenLastStatement(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}
