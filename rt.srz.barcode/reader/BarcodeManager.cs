// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BarcodeManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The barcode service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Text;
using rt.srz.barcode.Properties;
using rt.srz.barcode.converter;
using rt.srz.barcode.reader.delegates.args;
using rt.srz.model.barcode;

#endregion

namespace rt.srz.barcode.reader
{
  /// <summary>
  ///   The barcode service.
  /// </summary>
  public class BarcodeManager : IBarcodeManager
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="BarcodeManager" /> class.
    /// </summary>
    public BarcodeManager()
    {
      Schema = Resources.BarcodeSchema;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the schema.
    /// </summary>
    public string Schema { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The compose barcode.
    /// </summary>
    /// <param name="xmlSource">
    /// The xml source. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public byte[] ComposeBarcode(string xmlSource)
    {
      return BarcodeConverter.Convert(Schema, xmlSource);
    }

    /// <summary>
    /// The decompose barcode.
    /// </summary>
    /// <param name="barcodData">
    /// The barcod data. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    public string DecomposeBarcode(byte[] barcodData)
    {
      return BarcodeConverter.Convert(Schema, barcodData);
    }

    /// <summary>
    /// The parse barcode.
    /// </summary>
    /// <param name="strBarCode">
    /// The str bar code. 
    /// </param>
    /// <returns>
    /// The <see cref="PolicyData"/> . 
    /// </returns>
    public PolicyData ParseBarcode(string strBarCode)
    {
      var tempBytes = Encoding.GetEncoding(1251).GetBytes(strBarCode);
      var strXml = DecomposeBarcode(tempBytes);
      var drea = new DataRecievedEventArgs(strXml);
      return drea.PolicyData;
    }

    #endregion
  }
}