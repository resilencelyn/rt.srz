// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashAlgorithm.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The hash algoritm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.cryptography
{
  using System;
  using System.IO;

  /// <summary>
  ///   The hash algoritm.
  /// </summary>
  public class HashAlgorithm : IHashAlgorithm
  {
    #region Fields

    /// <summary>
    ///   The default policy hasher name.
    /// </summary>
    private readonly string DefaultPolicyHasherName = "GOST3411";

    /// <summary>
    ///   The hash algorithm.
    /// </summary>
    private readonly System.Security.Cryptography.HashAlgorithm hashAlgorithm;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="HashAlgorithm" /> class.
    /// </summary>
    public HashAlgorithm()
    {
      hashAlgorithm = System.Security.Cryptography.HashAlgorithm.Create(DefaultPolicyHasherName);
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The compute hash.
    /// </summary>
    /// <param name="inputStream">
    /// The imput stream.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    public byte[] ComputeHash(Stream inputStream)
    {
      try
      {
        return hashAlgorithm != null ? hashAlgorithm.ComputeHash(inputStream) : KeyContainer.ComputeHash(inputStream);
      }
      catch (Exception ex)
      {
        throw new Exception("Не удалось создать хэшер!!!!. " + ex.Message);
      }
    }

    #endregion
  }
}