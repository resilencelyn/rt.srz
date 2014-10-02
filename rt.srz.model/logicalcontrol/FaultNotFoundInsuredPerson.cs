using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace rt.srz.model.logicalcontrol
{
  [Serializable]
  public class FaultNotFoundInsuredPerson : LogicalControlException
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultDubleException" /> class.
    /// </summary>
    public FaultNotFoundInsuredPerson()
      : base(new ExceptionInfo("99"), "Не найдена застрахованная персона")
    {
    }

    protected FaultNotFoundInsuredPerson(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    protected override int Step()
    {
      return 6;
    }
  }
}
