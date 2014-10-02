// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeSertificate.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Тип сертификата
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.uec.model.enumerations
{
  #region references

  using System.Runtime.InteropServices;
  using System.Runtime.Serialization;

  #endregion

  /// <summary> Тип сертификата </summary>
  [ComVisible(true)]
  [DataContract]
  public enum TypeSertificate
  {
    /// <summary> Сертификат ОКО №1 ГОСТ</summary>
    [EnumMember]
    OKO1GOST = 490, 

    /// <summary> Сертификат ОКО №1 RSA</summary>
    [EnumMember]
    OKO1RSA = 485, 

    /// <summary> Сертификат терминала ГОСТ</summary>
    [EnumMember]
    TerminalGOST = 491, 

    /// <summary> Сертификат терминала RSA</summary>
    [EnumMember]
    TerminalRSA = 486, 

    /// <summary> Сертификат  УЦ №1 ГОСТ</summary>
    [EnumMember]
    UC1GOST = 489, 

    /// <summary> Сертификат  УЦ №1 RSA</summary>
    [EnumMember]
    UC1RSA = 484,

    /// <summary> Закрытый сертификат терминала ГОСТ</summary>
    [EnumMember]
    PrivateTerminalGOST = 528, 
  }
}