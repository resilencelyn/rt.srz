// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsuredPersonDataExchange.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The insured person data exchange.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.model
{
  using System;
  using System.Linq;
  using System.Xml.Linq;

  /// <summary>
  /// The insured person data exchange.
  /// </summary>
  public class InsuredPersonDataExchange
  {
    #region Public Methods and Operators

    /// <summary>
    /// The from xm l 2 document.
    /// </summary>
    /// <param name="xml">
    /// The xml.
    /// </param>
    /// <returns>
    /// The <see cref="Document"/>.
    /// </returns>
    public static Document FromXML2Document(string xml)
    {
      var document = XDocument.Parse(xml);
      var doc = from d in document.Descendants("Dual")
                select
                  new Document
                  {
                    RowId = new Guid(d.Element("RowId").Value), 
                    DocumentTypeId =
                      d.Element("DocumentTypeId") == null
                        ? (int?)null
                        : int.Parse(d.Element("DocumentTypeId").Value), 
                    Series = d.Element("DocumentSeries") == null ? null : d.Element("DocumentSeries").Value, 
                    Number = d.Element("DocumentNumber") == null ? null : d.Element("DocumentNumber").Value, 
                  };

      return doc.FirstOrDefault();
    }

    /// <summary>
    /// The from xm l 2 insured person data.
    /// </summary>
    /// <param name="xml">
    /// The xml.
    /// </param>
    /// <returns>
    /// The <see cref="InsuredPersonDatum"/>.
    /// </returns>
    public static InsuredPersonDatum FromXML2InsuredPersonData(string xml)
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