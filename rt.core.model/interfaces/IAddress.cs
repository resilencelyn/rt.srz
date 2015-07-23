// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAddress.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.interfaces
{
  using System;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  /// <summary>
  ///   The Address interface.
  /// </summary>
  public interface IAddress
  {
    #region Public Properties

    /// <summary>
    /// Gets the code.
    /// </summary>
    [XmlElement(Order = 5)]
    [DataMember(Order = 5)]
    string Code { get; }

    /// <summary>
    ///   Gets the id.
    /// </summary>
    [XmlElement(Order = 1)]
    [DataMember(Order = 1)]
    Guid Id { get; }

    /// <summary>
    /// Gets the index.
    /// </summary>
    [XmlElement(Order = 6)]
    [DataMember(Order = 6)]
    int? Index { get; }

    /// <summary>
    /// Gets the level.
    /// </summary>
    [XmlElement(Order = 12)]
    [DataMember(Order = 12)]
    int? Level { get; }

    /// <summary>
    ///   Gets the name.
    /// </summary>
    [XmlElement(Order = 3)]
    [DataMember(Order = 3)]
    string Name { get; }

    /// <summary>
    /// Gets the ocatd.
    /// </summary>
    [XmlElement(Order = 9)]
    [DataMember(Order = 9)]
    string Okato { get; }

    /// <summary>
    ///   Gets the parent.
    /// </summary>
    [XmlElement(Order = 50)]
    [DataMember(Order = 50)]
    Guid? ParentId { get; }

    /// <summary>
    ///   Gets the socr.
    /// </summary>
    [XmlElement(Order = 4)]
    [DataMember(Order = 4)]
    string Socr { get; }

    #endregion

    /// <summary>
    /// The get address.
    /// </summary>
    /// <returns>
    /// The <see cref="Address"/>.
    /// </returns>
    Address GetAddress();
  }
}