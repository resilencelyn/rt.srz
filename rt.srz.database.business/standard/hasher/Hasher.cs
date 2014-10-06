// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Hasher.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The hasher.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.hasher
{
  using System.IO;
  using System.Text;

  /// <summary>
  /// The hasher.
  /// </summary>
  public class Hasher
  {
    #region Public Methods and Operators

    /// <summary>
    /// The calculate hash from string.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="algorithm">
    /// The algorithm.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    public static byte[] CalculateHashFromString(string value, IHashAlgorithm algorithm)
    {
      var buf = Encoding.GetEncoding("Windows-1251").GetBytes(value);
      Stream stream = new MemoryStream(buf);
      return algorithm.ComputeHash(stream);
    }

    #endregion
  }
}