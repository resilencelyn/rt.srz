// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestSegmentHl7.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The request segment h l 7.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.enumerations
{
  /// <summary>
  ///   The request segment h l 7.
  /// </summary>
  public enum RequestSegmentHl7
  {
    /// <summary>
    ///   The a 01.
    /// </summary>
    A01 = 0xc9, 

    /// <summary>
    ///   The a 03.
    /// </summary>
    A03 = 0xcb, 

    /// <summary>
    ///   The a 24.
    /// </summary>
    A24 = 0xe0, 

    /// <summary>
    ///   The a 37.
    /// </summary>
    A37 = 0xed, 

    /// <summary>
    ///   The undefined.
    /// </summary>
    Undefined = 0, 

    /// <summary>
    ///   The z a 1.
    /// </summary>
    ZA1 = 0x12d, 

    /// <summary>
    ///   The z a 7.
    /// </summary>
    ZA7 = 0x133, 

    /// <summary>
    ///   The z p 1.
    /// </summary>
    ZP1 = 0x65, 

    /// <summary>
    ///   The z p 2.
    /// </summary>
    ZP2 = 0x66, 

    /// <summary>
    ///   The z p 4.
    /// </summary>
    ZP4 = 0x68
  }
}