// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Znd.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The znd.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The znd.
  /// </summary>
  [Serializable]
  public class Znd
  {
    #region Fields

    /// <summary>
    ///   The actual from.
    /// </summary>
    [XmlElement(ElementName = "ZND.6", Order = 6)]
    public string ActualFrom;

    /// <summary>
    ///   The actual to.
    /// </summary>
    [XmlElement(ElementName = "ZND.7", Order = 7)]
    public string ActualTo;

    /// <summary>
    ///   The application type.
    /// </summary>
    [XmlElement(ElementName = "ZND.4", Order = 4)]
    public ApplicationType ApplicationType = new ApplicationType();

    /// <summary>
    ///   The disposition and title.
    /// </summary>
    [XmlElement(ElementName = "ZND.2", Order = 2)]
    public Document DispositionAndTitle = new Document();

    /// <summary>
    ///   The document id.
    /// </summary>
    [XmlElement(ElementName = "ZND.10", Order = 10)]
    public EiStructure DocumentID = new EiStructure();

    /// <summary>
    ///   The document name.
    /// </summary>
    [XmlElement(ElementName = "ZND.9", Order = 9)]
    public string DocumentName;

    /// <summary>
    ///   The documentсontent.
    /// </summary>
    [XmlElement(ElementName = "ZND.8", Order = 8)]
    public string DocumentСontent;

    /// <summary>
    ///   The drafting date time.
    /// </summary>
    [XmlElement(ElementName = "ZND.5", Order = 5)]
    public string DraftingDateTime;

    /// <summary>
    ///   The id.
    /// </summary>
    [XmlElement(ElementName = "ZND.1", Order = 1)]
    public string Id;

    /// <summary>
    ///   The mime type.
    /// </summary>
    [XmlElement(ElementName = "ZND.3", Order = 3)]
    public CneStructure MimeType = new CneStructure();

    #endregion
  }
}