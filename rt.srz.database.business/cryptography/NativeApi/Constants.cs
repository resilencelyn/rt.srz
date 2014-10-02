// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Константы для криптопровайдера.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.cryptography.NativeApi
{
  /// <summary>
  ///   Константы для криптопровайдера.
  /// </summary>
  internal static class Constants
  {
    #region Constants

    /// <summary>
    /// The cpcsp hash id.
    /// </summary>
    public const int CpcspHashId = AlgClassHash | AlgSidHashCpcsp;

    /// <summary>
    /// The crypt verifycontext.
    /// </summary>
    public const int CryptVerifycontext = -268435456;

    /// <summary>
    /// The delete key set.
    /// </summary>
    public const int DeleteKeySet = 0x00000010;

    /// <summary>
    /// The hp hash value.
    /// </summary>
    public const int HpHashValue = 0x00000002;

    /// <summary>
    /// The new key set.
    /// </summary>
    public const int NewKeySet = 0x00000008;

    /// <summary>
    /// The nte bad signature.
    /// </summary>
    public const int NteBadSignature = -2146893818;

    /// <summary>
    /// The public key blob.
    /// </summary>
    public const int PublicKeyBlob = 0x06;

    /// <summary>
    /// The silent mode.
    /// </summary>
    public const int SilentMode = 0x00000040;

    /// <summary>
    /// The alg class hash.
    /// </summary>
    private const int AlgClassHash = 4 << 13;

    /// <summary>
    /// The alg sid hash cpcsp.
    /// </summary>
    private const int AlgSidHashCpcsp = 30;

    #endregion
  }
}