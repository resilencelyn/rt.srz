// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsuredPersonDatum.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The InsuredPersonDatum.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.model
{
  using System;
  using System.Linq;
  using System.Xml.Linq;

  /// <summary>
  ///   The InsuredPersonDatum.
  /// </summary>
  public partial class InsuredPersonDatum
  {
    #region Public Methods and Operators

    /// <summary>
    /// The from xml.
    /// </summary>
    /// <param name="xml">
    /// The xml.
    /// </param>
    /// <returns>
    /// The <see cref="InsuredPersonDatum"/>.
    /// </returns>
    public static InsuredPersonDatum FromXML(string xml)
    {
      var document = XDocument.Parse(xml);
      var personData = from pd in document.Descendants("Dual")
                       select
                         new InsuredPersonDatum
                         {
                           RowId = new Guid(pd.Element("RowId").Value), 
                           LastName =
                             pd.Element("LastName") == null ? null : pd.Element("LastName").Value, 
                           FirstName =
                             pd.Element("FirstName") == null
                               ? null
                               : pd.Element("FirstName").Value, 
                           MiddleName =
                             pd.Element("MiddleName") == null
                               ? null
                               : pd.Element("MiddleName").Value, 
                           Birthday =
                             pd.Element("Birthday") == null
                               ? (DateTime?)null
                               : DateTime.Parse(pd.Element("Birthday").Value), 
                           Birthplace =
                             pd.Element("Birthplace") == null
                               ? null
                               : pd.Element("Birthplace").Value, 
                           Snils = pd.Element("Snils") == null ? null : pd.Element("Snils").Value, 
                         };

      return personData.FirstOrDefault();
    }

    #endregion
  }
}