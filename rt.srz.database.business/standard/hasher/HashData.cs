// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashData.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The hash data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.hasher
{
  /// <summary>
  /// The hash data.
  /// </summary>
  public class HashData
  {
    #region Fields

    /// <summary>
    /// The hash.
    /// </summary>
    public byte[] hash;

    /// <summary>
    /// The id card date.
    /// </summary>
    public string idCardDate;

    /// <summary>
    /// The id card date exp.
    /// </summary>
    public string idCardDateExp;

    /// <summary>
    /// The id card number.
    /// </summary>
    public string idCardNumber;

    /// <summary>
    /// The id card org.
    /// </summary>
    public string idCardOrg;

    /// <summary>
    /// The key.
    /// </summary>
    public string key;

    /// <summary>
    /// The subtype.
    /// </summary>
    public int subtype;

    /// <summary>
    /// The type.
    /// </summary>
    public int type;

    #endregion
  }
}