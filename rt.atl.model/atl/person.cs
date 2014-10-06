// --------------------------------------------------------------------------------------------------------------------
// <copyright file="person.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The person.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.model.atl
{
  using System.Runtime.Serialization;

  /// <summary>
  ///   The person.
  /// </summary>
  public partial class person
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the export error.
    /// </summary>
    [DataMember(Order = 160)]
    public virtual string ExportError { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether is exported.
    /// </summary>
    [DataMember(Order = 159)]
    public virtual bool IsExported { get; set; }

    #endregion
  }
}