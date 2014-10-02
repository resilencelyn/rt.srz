//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace rt.srz.database.business.model
{
  using System;
  using System.Linq;
  using System.Xml.Linq;

  public partial class InsuredPersonDataExchange
  {
    public static InsuredPersonDatum FromXML2InsuredPersonData(string xml)
    {
      XDocument document = XDocument.Parse(xml);
      var personData = from pd in document.Descendants("Dual")
                       select new InsuredPersonDatum
                       {
                         RowId = new Guid(pd.Element("RowId").Value),
                         LastName = pd.Element("LastName") == null ? null : pd.Element("LastName").Value,
                         FirstName = pd.Element("FirstName") == null ? null : pd.Element("FirstName").Value,
                         MiddleName = pd.Element("MiddleName") == null ? null : pd.Element("MiddleName").Value,
                         Birthday = pd.Element("Birthday") == null ? (DateTime?)null : DateTime.Parse(pd.Element("Birthday").Value),
                         Birthplace = pd.Element("Birthplace") == null ? null : pd.Element("Birthplace").Value,
                         Snils = pd.Element("Snils") == null ? null : pd.Element("Snils").Value,
                       };

      return personData.FirstOrDefault();
    }

    public static Document FromXML2Document(string xml)
    {
      XDocument document = XDocument.Parse(xml);
      var doc = from d in document.Descendants("Dual")
                select new Document
                {
                  RowId = new Guid(d.Element("RowId").Value),
                  DocumentTypeId = d.Element("DocumentTypeId") == null ? (int?)null : int.Parse(d.Element("DocumentTypeId").Value),
                  Series = d.Element("DocumentSeries") == null ? null : d.Element("DocumentSeries").Value,
                  Number = d.Element("DocumentNumber") == null ? null : d.Element("DocumentNumber").Value,
                };

      return doc.FirstOrDefault();
    }
  }
}
