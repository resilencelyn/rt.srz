using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using rt.srz.model.Properties;

namespace rt.srz.model.logicalcontrol.exceptions.step6.issue
{
  public class FaultPoliceCertificateDateFromMoreThenDateEnd : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    public FaultPoliceCertificateDateFromMoreThenDateEnd()
      : base(new ExceptionInfo(Resource.FaultPoliceCertificateDateFromMoreThenDateEndExceptionCode), Resource.FaultPoliceCertificateDateFromMoreThenDateEndMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPoliceCertificateDateFromMoreThenDateEnd(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

   #endregion
  }
}
