// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Address.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.interfaces
{
  using System;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  /// <summary>
  ///   The address.
  /// </summary>
  public class Address : IAddress
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the code.
    /// </summary>
    public virtual string Code { get; set; }

    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    [XmlElement(Order = 1)]
    [DataMember(Order = 1)]
    public virtual Guid Id { get; set; }

    /// <summary>
    ///   Gets or sets the index.
    /// </summary>
    public int? Index { get; set; }

    /// <summary>
    ///   Gets or sets the level.
    /// </summary>
    public int? Level { get; set; }

    /// <summary>
    ///   Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///   Gets or sets the okato.
    /// </summary>
    public string Okato { get; set; }

    /// <summary>
    ///   Gets or sets the parent.
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    ///   Gets or sets the socr.
    /// </summary>
    public string Socr { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The get address.
    /// </summary>
    /// <returns>
    ///   The <see cref="Address" />.
    /// </returns>
    public Address GetAddress()
    {
      return this;
    }

    #endregion
  }
}