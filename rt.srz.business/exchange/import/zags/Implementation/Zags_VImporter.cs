// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zags_VImporter.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The zags_ v importer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.zags.Implementation
{
  using rt.srz.model.Hl7.zags;

  /// <summary>
  ///   The zags_ v importer.
  /// </summary>
  public class Zags_VImporter : ZagsImporter<Zags_VNov>
  {
    #region Properties

    /// <summary>
    ///   Gets the xsd resource name.
    /// </summary>
    protected override string XsdResourceName
    {
      get
      {
        return "Zags_V.xsd";
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The convert.
    /// </summary>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <returns>
    /// The <see cref="Zags_VNov"/>.
    /// </returns>
    protected override Zags_VNov Convert(Zags_VNov data)
    {
      return data;
    }

    #endregion
  }
}