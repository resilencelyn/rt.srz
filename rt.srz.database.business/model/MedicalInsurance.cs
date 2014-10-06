// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MedicalInsurance.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The MedicalInsurance.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.model
{
  using System;
  using System.Linq;
  using System.Xml.Linq;

  /// <summary>
  ///   The MedicalInsurance.
  /// </summary>
  public partial class MedicalInsurance
  {
    #region Public Methods and Operators

    /// <summary>
    /// The from xml.
    /// </summary>
    /// <param name="xml">
    /// The xml.
    /// </param>
    /// <returns>
    /// The <see cref="MedicalInsurance"/>.
    /// </returns>
    public static MedicalInsurance FromXML(string xml)
    {
      var document = XDocument.Parse(xml);
      var insurance = from ins in document.Descendants("Dual")
                      select
                        new MedicalInsurance
                        {
                          RowId = new Guid(ins.Element("RowId").Value), 
                          PolisTypeId =
                            ins.Element("PolisTypeId") == null
                              ? (int?)null
                              : int.Parse(ins.Element("PolisTypeId").Value), 
                          PolisSeria =
                            ins.Element("PolisSeria") == null
                              ? null
                              : ins.Element("PolisSeria").Value, 
                          PolisNumber =
                            ins.Element("PolisNumber") == null
                              ? null
                              : ins.Element("PolisNumber").Value, 
                        };

      return insurance.FirstOrDefault();
    }

    #endregion
  }
}