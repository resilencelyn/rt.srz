using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rt.srz.model.HL7.enumerations
{
  public enum HL7Node
  {
    /// <summary>
    ///   The root.
    /// </summary>
    Root,

    /// <summary>
    ///   The header.
    /// </summary>
    Header,

    /// <summary>
    ///   The trailer.
    /// </summary>
    Trailer,

    /// <summary>
    ///   The message.
    /// </summary>
    Message
  }
}
