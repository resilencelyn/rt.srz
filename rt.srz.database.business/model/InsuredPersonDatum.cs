// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsuredPersonDatum.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
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
    public static InsuredPersonDatum FromXML(string xml)
    {
      XDocument document = XDocument.Parse(xml);
      var personData = from pd in document.Descendants("Dual")
                select new InsuredPersonDatum
                {
                  RowId = new Guid(pd.Element("RowId").Value),
                  LastName = pd.Element("LastName") == null? null : pd.Element("LastName").Value,
                  FirstName = pd.Element("FirstName") == null ? null : pd.Element("FirstName").Value,
                  MiddleName = pd.Element("MiddleName") == null ? null : pd.Element("MiddleName").Value,
                  Birthday = pd.Element("Birthday") == null ? (DateTime?)null : DateTime.Parse(pd.Element("Birthday").Value),
                  Birthplace = pd.Element("Birthplace") == null ? null : pd.Element("Birthplace").Value,
                  Snils = pd.Element("Snils") == null ? null : pd.Element("Snils").Value,
                };

      return personData.FirstOrDefault();
    }
  }
}