// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BarcodeVersion.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The barcode version.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.barcode
{
  /// <summary>The barcode version.</summary>
  public class BarcodeVersion
  {
    #region Public Properties

    /// <summary>Gets or sets the data length.</summary>
    public int DataLength { get; set; }

    /// <summary>Gets or sets the length.</summary>
    public int Length { get; set; }

    /// <summary>Gets or sets the root tag name.</summary>
    public string RootTagName { get; set; }

    /// <summary>Gets or sets the schema.</summary>
    public string Schema { get; set; }

    /// <summary>Gets or sets the signature length.</summary>
    public int SignatureLength { get; set; }

    /// <summary>Gets or sets the version.</summary>
    public int Version { get; set; }

    #endregion
  }
}