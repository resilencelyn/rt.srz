// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CryptographyType.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The cryptography type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.uec.model.enumerations
{
  #region references

  using System.Runtime.InteropServices;
  using System.Runtime.Serialization;

  #endregion

  /// <summary>
  ///   The cryptography type.
  /// </summary>
  [ComVisible(true)]
  [DataContract]
  public enum CryptographyType
  {
    /// <summary>
    ///   The rsa.
    /// </summary>
    [EnumMember]
    RSA = 1, 

    /// <summary>
    ///   The gost.
    /// </summary>
    [EnumMember]
    GOST = 2
  }
}