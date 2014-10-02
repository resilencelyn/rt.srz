// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolSettingsEnum.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The protocol settings enum.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.uec.model.enumerations
{
  #region references

  using System.Runtime.InteropServices;
  using System.Runtime.Serialization;

  #endregion

  /// <summary>
  ///   The protocol settings enum.
  /// </summary>
  [ComVisible(true)]
  [DataContract]
  public enum ProtocolSettingsEnum
  {
    /// <summary>
    ///   The output.
    /// </summary>
    [EnumMember]
    Output, 

    /// <summary>
    ///   The log file.
    /// </summary>
    [EnumMember]
    LogFile, 

    /// <summary>
    ///   The log name.
    /// </summary>
    [EnumMember]
    LogName, 

    /// <summary>
    ///   The reader.
    /// </summary>
    [EnumMember]
    Reader, 

    /// <summary>
    ///   The card.
    /// </summary>
    [EnumMember]
    Card, 

    /// <summary>
    ///   The cardlib.
    /// </summary>
    [EnumMember]
    Cardlib, 

    /// <summary>
    ///   The funclib.
    /// </summary>
    [EnumMember]
    Funclib, 

    /// <summary>
    ///   The oplib.
    /// </summary>
    [EnumMember]
    Oplib, 

    /// <summary>
    ///   The tell me.
    /// </summary>
    [EnumMember]
    TellMe, 

    /// <summary>
    ///   The extern.
    /// </summary>
    [EnumMember]
    Extern, 
  }
}