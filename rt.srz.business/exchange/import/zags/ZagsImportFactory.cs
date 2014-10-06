// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZagsImportFactory.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The zags import factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.zags
{
  using System;

  using rt.srz.model.HL7.zags;

  /// <summary>
  /// The zags import factory.
  /// </summary>
  public class ZagsImportFactory : IZagsImportFactory
  {
    #region Fields

    /// <summary>
    /// The _importer zags.
    /// </summary>
    private readonly IZagsImporter[] _importerZags;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ZagsImportFactory"/> class.
    /// </summary>
    /// <param name="importerZags">
    /// The importer zags.
    /// </param>
    public ZagsImportFactory(IZagsImporter[] importerZags)
    {
      _importerZags = importerZags;
    }

    #endregion

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
    /// <exception cref="ApplicationException">
    /// </exception>
    public Zags_VNov GetImportData(string xmlFilePath)
    {
      Zags_VNov result = null;
      foreach (var importer in _importerZags)
      {
        try
        {
          result = importer.GetImportData(xmlFilePath);
          if (result != null)
          {
            return result;
          }
        }
        catch
        {
        }
      }

      throw new ApplicationException("Не найдены данные, соответствующие какой либо их схем xsd");
    }

    #endregion
  }
}