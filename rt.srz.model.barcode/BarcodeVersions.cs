// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BarcodeVersions.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The barcode versions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using rt.srz.model.barcode.Properties;

namespace rt.srz.model.barcode
{
  #region references

  

  #endregion

  /// <summary>The barcode versions.</summary>
  public static class BarcodeVersions
  {
    #region Static Fields

    /// <summary>The verion_1.</summary>
    private static readonly BarcodeVersion Verion1 = new BarcodeVersion
                                                       {
                                                         Version = 1, 
                                                         Length = 130, 
                                                         DataLength = 65, 
                                                         SignatureLength = 65, 
                                                         RootTagName = "BarcodeData",
                                                         Schema = Resource.BarcodeSchema
                                                       };

    /// <summary>The verion_2.</summary>
    private static readonly BarcodeVersion Verion2 = new BarcodeVersion
                                                       {
                                                         Version = 2, 
                                                         Length = 130, 
                                                         DataLength = 65, 
                                                         SignatureLength = 65, 
                                                         RootTagName = "BarcodeData_V2", 
                                                         Schema = Resource.BarcodeSchema
                                                       };

    /// <summary>The verion_3.</summary>
    private static readonly BarcodeVersion Verion3 = new BarcodeVersion
                                                       {
                                                         Version = 1, 
                                                         Length = 131, 
                                                         DataLength = 66, 
                                                         SignatureLength = 65, 
                                                         RootTagName = "BarcodeData", 
                                                         Schema = Resource.BarcodeSchema
                                                       };

    /// <summary>The verion 4.</summary>
    private static readonly BarcodeVersion Verion4 = new BarcodeVersion
                                                       {
                                                         Version = 2, 
                                                         Length = 132, 
                                                         DataLength = 67, 
                                                         SignatureLength = 65, 
                                                         RootTagName = "BarcodeData_V2", 
                                                         Schema = Resource.BarcodeSchema
                                                       };

    /// <summary>The verion 5.</summary>
    private static readonly BarcodeVersion Verion5 = new BarcodeVersion
                                                       {
                                                         Version = 1, 
                                                         Length = 132, 
                                                         DataLength = 67, 
                                                         SignatureLength = 65, 
                                                         RootTagName = "BarcodeData", 
                                                         Schema = Resource.BarcodeSchema
                                                       };

    /// <summary>The versions.</summary>
    private static readonly List<BarcodeVersion> Versions = new List<BarcodeVersion>
                                                              {
                                                                Verion1, 
                                                                Verion2, 
                                                                Verion3, 
                                                                Verion4, 
                                                                Verion5, 
                                                              };

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The get barcode version.
    /// </summary>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <returns>
    /// The <see cref="BarcodeVersion"/>.
    /// </returns>
    public static BarcodeVersion GetBarcodeVersion(byte[] data)
    {
      var source = Versions.Where(v => v.Version == (int)data[0] //&& data.Length == v.Length
        );
      return source.Any() ? source.First() : null;
    }

    /// <summary>
    /// The is valid barcode.
    /// </summary>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsValidBarcode(byte[] data)
    {
      var result = data != null && data.Length > 0;
      if (result)
      {
        var versionCount = Versions.Count(v => v.Version == (int)data[0] //&& data.Length == v.Length
          );
        result = versionCount > 0;
      }

      return result;
    }

    #endregion
  }
}