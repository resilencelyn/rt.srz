//-------------------------------------------------------------------------------------
// <copyright file="MedicalInsurance.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System.Xml.Serialization;

  /// <summary>
  /// The MedicalInsurance.
  /// </summary>
  public partial class MedicalInsurance
  {
    /// <summary>
    /// Gets the series number.
    /// </summary>
    [XmlIgnore]
    public virtual string SeriesNumber
    {
      get
      {
        return string.IsNullOrEmpty(PolisSeria) ? PolisNumber : string.Format("{0} ¹ {1}", PolisNumber, PolisSeria);
      }
    }
  }
}