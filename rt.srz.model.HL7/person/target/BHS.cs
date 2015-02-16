// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BHS.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The bhs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Text;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The bhs.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "BHS", Namespace = "urn:hl7-org:v2xml")]
  public class BHS
  {
    #region Fields

    /// <summary>
    ///   The application name.
    /// </summary>
    [XmlElement(ElementName = "BHS.5", Order = 5)]
    public BHS5 ApplicationName = new BHS5();

    /// <summary>
    ///   The date time now.
    /// </summary>
    [XmlElement(ElementName = "BHS.7", Order = 7)]
    public string DateTimeNow = Hl7Helper.FormatCurrentDateTime();

    /// <summary>
    ///   The field separator.
    /// </summary>
    [XmlElement(ElementName = "BHS.1", Order = 1)]
    public string FieldSeparator = Hl7Helper.BHS_Delimiter;

    /// <summary>
    ///   The identificator.
    /// </summary>
    [XmlElement(ElementName = "BHS.11", Order = 11)]
    public string Identificator;

    /// <summary>
    ///   The organization name.
    /// </summary>
    [XmlElement(ElementName = "BHS.6", Order = 6)]
    public BHS6 OrganizationName = new BHS6();

    /// <summary>
    ///   The origin application name.
    /// </summary>
    [XmlElement(ElementName = "BHS.3", Order = 3)]
    public BHS3 OriginApplicationName = new BHS3();

    /// <summary>
    ///   The origin organization name.
    /// </summary>
    [XmlElement(ElementName = "BHS.4", Order = 4)]
    public BHS4 OriginOrganizationName = new BHS4();

    /// <summary>
    ///   The reference identificator.
    /// </summary>
    [XmlElement(ElementName = "BHS.12", Order = 12)]
    public string ReferenceIdentificator;

    /// <summary>
    ///   The special symbol.
    /// </summary>
    [XmlElement(ElementName = "BHS.2", Order = 2)]
    public string SpecialSymbol = Hl7Helper.BHS_CodeSymbols;

    /// <summary>
    ///   The type work.
    /// </summary>
    [XmlElement(ElementName = "BHS.9", Order = 9)]
    public string TypeWork;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The retrieve short info.
    /// </summary>
    /// <returns>
    ///   The <see cref="string" />.
    /// </returns>
    public string RetrieveShortInfo()
    {
      var builder = new StringBuilder();
      builder.AppendFormat("BatchID: {0}", Identificator);
      if (!string.IsNullOrEmpty(ReferenceIdentificator))
      {
        builder.AppendFormat(", SourceID: {0}", ReferenceIdentificator);
      }

      return builder.ToString();
    }

    #endregion
  }
}