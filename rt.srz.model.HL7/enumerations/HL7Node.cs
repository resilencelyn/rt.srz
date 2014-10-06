// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HL7Node.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The h l 7 node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.enumerations
{
  /// <summary>
  /// The h l 7 node.
  /// </summary>
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