using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rt.srz.model.logicalcontrol
{
  [Serializable]
  public class SearchTimeoutException : Exception
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="message"></param>
    /// <param name="e"></param>
    public SearchTimeoutException(string message, Exception e) : base(message)
    { 
    }
  }
}
