// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Token.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The token.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.client
{
  using System;
  using System.Runtime.Serialization;

  /// <summary>
  ///   The token.
  /// </summary>
  [DataContract]
  public class Token
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the exp time.
    /// </summary>
    [DataMember]
    public DateTime ExpTime { get; set; }

    /// <summary>
    ///   Gets or sets the id.
    /// </summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>
    ///   Gets or sets the signature.
    /// </summary>
    [DataMember]
    public string Signature { get; set; }

    #endregion
  }
}