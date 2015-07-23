// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementBatch.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The statement maximum.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  /// <summary>
  ///   The statement maximum.
  /// </summary>
  public class StatementBatch : Statement
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the batch id.
    /// </summary>
    [XmlElement(Order = 40)]
    [DataMember(Order = 40)]
    public virtual Guid BatchId { get; set; }

    /// <summary>
    /// Gets or sets the kladr.
    /// </summary>
    [XmlElement(Order = 42)]
    [DataMember(Order = 42)]
    public virtual string Kladr { get; set; }

    /// <summary>
    /// Gets or sets the kladr 2.
    /// </summary>
    [XmlElement(Order = 43)]
    [DataMember(Order = 43)]
    public virtual string Kladr2 { get; set; }

    /// <summary>
    /// Gets or sets the version export.
    /// </summary>
    [XmlElement(Order = 41)]
    [DataMember(Order = 41)]
    public virtual int VersionExport { get; set; }

    #endregion
  }
}