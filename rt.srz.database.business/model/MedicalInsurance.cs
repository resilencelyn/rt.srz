//-------------------------------------------------------------------------------------
// <copyright file="MedicalInsurance.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

namespace rt.srz.database.business.model
{
  using System;
  using System.Linq;
  using System.Xml.Linq;

  /// <summary>
  /// The MedicalInsurance.
  /// </summary>
  public partial class MedicalInsurance 
  {
    public static MedicalInsurance FromXML(string xml)
    {
      XDocument document = XDocument.Parse(xml);
      var insurance = from ins in document.Descendants("Dual")
                      select new MedicalInsurance
                      {
                        RowId = new Guid(ins.Element("RowId").Value),
                        PolisTypeId = ins.Element("PolisTypeId") == null? (int?)null : int.Parse(ins.Element("PolisTypeId").Value),
                        PolisSeria = ins.Element("PolisSeria") == null? null : ins.Element("PolisSeria").Value,
                        PolisNumber = ins.Element("PolisNumber") == null ? null : ins.Element("PolisNumber").Value,
                      };

      return insurance.FirstOrDefault();
    }
  }
}