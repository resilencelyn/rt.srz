// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IZagsImporter.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ZagsImporter interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.zags
{
  using rt.srz.model.Hl7.zags;

  /// <summary>
  ///   The ZagsImporter interface.
  /// </summary>
  public interface IZagsImporter
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get import data.
    /// </summary>
    /// <param name="xmlFilePath">
    /// The xml file path.
    /// </param>
    /// <returns>
    /// The <see cref="Zags_VNov"/>.
    /// </returns>
    Zags_VNov GetImportData(string xmlFilePath);

    #endregion
  }
}