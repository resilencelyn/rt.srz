// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalEvent.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The global event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons.Enumerations
{
  /// <summary>
  ///   The global event.
  /// </summary>
  public enum GlobalEvent
  {
    /// <summary>
    ///   The live state changed.
    /// </summary>
    LiveStateChanged, 

    /// <summary>
    ///   The system time changed.
    /// </summary>
    SystemTimeChanged, 

    /// <summary>
    ///   The config read event.
    /// </summary>
    ConfigReadEvent, 

    /// <summary>
    ///   The fatal error handle.
    /// </summary>
    FatalErrorHandle
  }
}