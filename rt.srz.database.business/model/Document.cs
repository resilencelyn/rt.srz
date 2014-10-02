// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Document.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The Document.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.model
{
  using System;
  using System.Linq;
  using System.Xml.Linq;

  /// <summary>
  ///   The Document.
  /// </summary>
  public partial class Document
  {
    public static Document FromXML(string xml)
    {
      XDocument document = XDocument.Parse(xml);
      var doc = from d in document.Descendants("Dual")
                select new Document
                {
                  RowId = new Guid(d.Element("RowId").Value),
                  DocumentTypeId = d.Element("DocumentTypeId") == null ? (int?)null : int.Parse(d.Element("DocumentTypeId").Value),
                  Series = d.Element("Series") == null ? null : d.Element("Series").Value,
                  Number = d.Element("Number") == null ? null : d.Element("Number").Value,
                };

      return doc.FirstOrDefault();
    }
  }
}