using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rt.atl.model.exceptions
{
  public class Atl2PvpMappingException : Exception
  {
    public Atl2PvpMappingException(Exception innerException = null)
      : base("Ошибка мапинга заявления из Атлантики в ПВП ", innerException)
    { 
    
    }
  }
}
