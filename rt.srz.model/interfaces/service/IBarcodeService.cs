// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBarcodeService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The BarcodeService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service
{
  #region

  using rt.srz.model.barcode;

  #endregion

  /// <summary>
  ///   The BarcodeService interface.
  /// </summary>
  public interface IBarcodeService
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the schema.
    /// </summary>
    string Schema { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The compose barcode.
    /// </summary>
    /// <param name="xmlSource">
    /// The xml_source. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    byte[] ComposeBarcode(string xmlSource);

    /// <summary>
    /// The decompose barcode.
    /// </summary>
    /// <param name="barcodData">
    /// The barcod data. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    string DecomposeBarcode(byte[] barcodData);

    /// <summary>
    /// The parse barcode.
    /// </summary>
    /// <param name="strBarCode">
    /// The str bar code. 
    /// </param>
    /// <returns>
    /// The <see cref="PolicyData"/> . 
    /// </returns>
    PolicyData ParseBarcode(string strBarCode);

    #endregion
  }
}