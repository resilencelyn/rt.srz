// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MedicalInsurance.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The MedicalInsurance.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System.Xml.Serialization;

  /// <summary>
  ///   The MedicalInsurance.
  /// </summary>
  public partial class MedicalInsurance
  {
    #region Public Properties

    /// <summary>
    ///   Gets the series number.
    /// </summary>
    [XmlIgnore]
    public virtual string SeriesNumber
    {
      get
      {
        return string.IsNullOrEmpty(PolisSeria) ? PolisNumber : string.Format("{0} ¹ {1}", PolisNumber, PolisSeria);
      }
    }

    #endregion
  }
}