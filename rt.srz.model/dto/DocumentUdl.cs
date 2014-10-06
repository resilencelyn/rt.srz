// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentUdl.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The document udl.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.dto
{
  /// <summary>
  /// The document udl.
  /// </summary>
  public class DocumentUdl
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the document issue date.
    /// </summary>
    public string DocumentIssueDate { get; set; }

    /// <summary>
    /// Gets or sets the document issuer.
    /// </summary>
    public string DocumentIssuer { get; set; }

    /// <summary>
    /// Gets or sets the document number.
    /// </summary>
    public string DocumentNumber { get; set; }

    /// <summary>
    /// Gets or sets the document series.
    /// </summary>
    public string DocumentSeries { get; set; }

    /// <summary>
    /// Gets or sets the document type.
    /// </summary>
    public string DocumentType { get; set; }

    #endregion
  }
}