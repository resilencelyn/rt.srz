// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Token.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Defines the Token type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.client
{
  using System;
  using System.Runtime.Serialization;

  /// <summary>
  /// The token.
  /// </summary>
   [DataContract]
  public class Token
  {
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the signature.
    /// </summary>
    [DataMember]
    public string Signature { get; set; }

    /// <summary>
    /// Gets or sets the exp time.
    /// </summary>
    [DataMember]
    public DateTime ExpTime { get; set; }
  }
}
