using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace rt.srz.model.logicalcontrol
{
  [Serializable]
  public class StandardSearchKeyCalculationException : LogicalControlException
  {
     /// <summary>
    ///   Initializes a new instance of the <see cref="FaultDubleException" /> class.
    /// </summary>
    public StandardSearchKeyCalculationException()
      : base(new ExceptionInfo("99"), "Ошибка расчета стандартных ключей")
    {
    }

    protected StandardSearchKeyCalculationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    protected override int Step()
    {
      return 6;
    }
  }
}
