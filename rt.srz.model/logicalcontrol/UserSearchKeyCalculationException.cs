using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace rt.srz.model.logicalcontrol
{
  public class UserSearchKeyCalculationException : LogicalControlException
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultDubleException" /> class.
    /// </summary>
    public UserSearchKeyCalculationException()
      : base(new ExceptionInfo("99"), "Ошибка расчета пользовательских ключей")
    {
    }

    protected UserSearchKeyCalculationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    protected override int Step()
    {
      return 6;
    }
  }
}
