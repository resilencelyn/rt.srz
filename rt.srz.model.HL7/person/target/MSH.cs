// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MSH.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The msh.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The msh.
  /// </summary>
  [Serializable]
  public class MSH
  {
    #region Fields

    /// <summary>
    ///   The application name.
    /// </summary>
    [XmlElement(ElementName = "MSH.5", Order = 5)]
    public BHS5 ApplicationName = new BHS5();

    /// <summary>
    ///   The confirmation type foms.
    /// </summary>
    [XmlElement(ElementName = "MSH.16", Order = 16)]
    public string ConfirmationTypeFoms = "AL";

    /// <summary>
    ///   The confirmation type gate way.
    /// </summary>
    [XmlElement(ElementName = "MSH.15", Order = 15)]
    public string ConfirmationTypeGateWay = "AL";

    /// <summary>
    ///   The date time creation.
    /// </summary>
    [XmlElement(ElementName = "MSH.7", Order = 7)]
    public string DateTimeCreation;

    /// <summary>
    ///   The field divider.
    /// </summary>
    [XmlElement(ElementName = "MSH.1", Order = 1)]
    public string FieldDivider = Hl7Helper.BHS_Delimiter;

    /// <summary>
    ///   The identificator.
    /// </summary>
    [XmlElement(ElementName = "MSH.10", Order = 10)]
    public string Identificator;

    /// <summary>
    ///   The message type.
    /// </summary>
    [XmlElement(ElementName = "MSH.9", Order = 9)]
    public MessageType MessageType = new MessageType();

    /// <summary>
    ///   The organization name.
    /// </summary>
    [XmlElement(ElementName = "MSH.6", Order = 6)]
    public BHS6 OrganizationName = new BHS6();

    /// <summary>
    ///   The origin application name.
    /// </summary>
    [XmlElement(ElementName = "MSH.3", Order = 3)]
    public BHS3 OriginApplicationName = new BHS3();

    /// <summary>
    ///   The origin organization name.
    /// </summary>
    [XmlElement(ElementName = "MSH.4", Order = 4)]
    public BHS4 OriginOrganizationName = new BHS4();

    /// <summary>
    ///   The special symbol.
    /// </summary>
    [XmlElement(ElementName = "MSH.2", Order = 2)]
    public string SpecialSymbol = Hl7Helper.BHS_CodeSymbols;

    /// <summary>
    ///   The type work.
    /// </summary>
    [XmlElement(ElementName = "MSH.11", Order = 11)]
    public TypeWork TypeWork = new TypeWork();

    /// <summary>
    ///   The version standart id.
    /// </summary>
    [XmlElement(ElementName = "MSH.12", Order = 12)]
    public VersionStandartId VersionStandartId = new VersionStandartId();

    #endregion
  }
}