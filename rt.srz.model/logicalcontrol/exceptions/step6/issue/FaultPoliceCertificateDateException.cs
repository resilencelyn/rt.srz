using rt.srz.model.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace rt.srz.model.logicalcontrol.exceptions.step6
{
  [Serializable]
  public class FaultPoliceCertificateDateException : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    public FaultPoliceCertificateDateException()
      : base(new ExceptionInfo(Resource.FaultPoliceCertificateDateExceptionCode), Resource.FaultPoliceCertificateDateExceptionMessage)
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
    protected FaultPoliceCertificateDateException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

   #endregion
  }
}
