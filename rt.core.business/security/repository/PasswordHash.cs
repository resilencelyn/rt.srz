// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordHash.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The password hash.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.security.repository
{
  #region references

  using System;
  using System.Security.Cryptography;

  #endregion

  /// <summary>
  ///   The password hash.
  /// </summary>
  public sealed class PasswordHash
  {
    #region Constants

    /// <summary>
    ///   The hash iter.
    /// </summary>
    private const int HashIter = 10000;

    /// <summary>
    ///   The hash size.
    /// </summary>
    private const int HashSize = 20;

    /// <summary>
    ///   The salt size.
    /// </summary>
    private const int SaltSize = 16;

    #endregion

    #region Fields

    /// <summary>
    ///   The _hash.
    /// </summary>
    private readonly byte[] _hash;

    /// <summary>
    ///   The _salt.
    /// </summary>
    private readonly byte[] _salt;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordHash"/> class.
    /// </summary>
    /// <param name="password">
    /// The password.
    /// </param>
    public PasswordHash(string password)
    {
      new RNGCryptoServiceProvider().GetBytes(_salt = new byte[SaltSize]);
      _hash = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordHash"/> class.
    /// </summary>
    /// <param name="salt">
    /// The salt.
    /// </param>
    /// <param name="hash">
    /// The hash.
    /// </param>
    public PasswordHash(byte[] salt, byte[] hash)
    {
      Array.Copy(salt, 0, _salt = new byte[SaltSize], 0, SaltSize);
      Array.Copy(hash, 0, _hash = new byte[HashSize], 0, HashSize);
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the hash.
    /// </summary>
    public byte[] Hash
    {
      get
      {
        return (byte[])_hash.Clone();
      }
    }

    /// <summary>
    ///   Gets the salt.
    /// </summary>
    public byte[] Salt
    {
      get
      {
        return (byte[])_salt.Clone();
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The verify.
    /// </summary>
    /// <param name="password">
    /// The password.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool Verify(string password)
    {
      var test = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);

      for (var i = 0; i < HashSize; i++)
      {
        if (test[i] != _hash[i])
        {
          return false;
        }
      }

      return true;
    }

    #endregion
  }
}