// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHashAlgorithm.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The HashAlgorithm interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business
{
  using System.IO;

  /// <summary>
  ///   The HashAlgorithm interface.
  /// </summary>
  public interface IHashAlgorithm
  {
    #region Public Methods and Operators

    /// <summary>
    /// The compute hash.
    /// </summary>
    /// <param name="imputStream">
    /// The imput stream.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    byte[] ComputeHash(Stream imputStream);

    #endregion
  }
}