// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IZagsImportFactory.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ZagsImportFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.zags
{
  using rt.srz.model.Hl7.zags;

  /// <summary>
  ///   The ZagsImportFactory interface.
  /// </summary>
  public interface IZagsImportFactory
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