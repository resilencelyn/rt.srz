// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ERR2.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The er r 2.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The er r 2.
  /// </summary>
  [Serializable]
  public class ERR2
  {
    #region Fields

    /// <summary>
    ///   The component id.
    /// </summary>
    [XmlElement(ElementName = "ERL.5", Order = 5)]
    public string ComponentId;

    /// <summary>
    ///   The segment id.
    /// </summary>
    [XmlElement(ElementName = "ERL.2", Order = 2)]
    public string SegmentId;

    /// <summary>
    ///   The segment name.
    /// </summary>
    [XmlElement(ElementName = "ERL.1", Order = 1)]
    public string SegmentName;

    /// <summary>
    ///   The segment row id.
    /// </summary>
    [XmlElement(ElementName = "ERL.3", Order = 3)]
    public string SegmentRowId;

    /// <summary>
    ///   The segment row id repeat.
    /// </summary>
    [XmlElement(ElementName = "ERL.4", Order = 4)]
    public string SegmentRowIdRepeat;

    /// <summary>
    ///   The sub component id.
    /// </summary>
    [XmlElement(ElementName = "ERL.6", Order = 6)]
    public string SubComponentId;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ERR2" /> class.
    /// </summary>
    public ERR2()
    {
      SegmentName = string.Empty;
      SegmentId = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ERR2"/> class.
    /// </summary>
    /// <param name="segmentName">
    /// The segment name.
    /// </param>
    /// <param name="segmentId">
    /// The segment id.
    /// </param>
    /// <param name="segmentRowId">
    /// The segment row id.
    /// </param>
    /// <param name="segmentRowIdRepeat">
    /// The segment row id repeat.
    /// </param>
    /// <param name="componentId">
    /// The component id.
    /// </param>
    /// <param name="subComponentId">
    /// The sub component id.
    /// </param>
    public ERR2(
      string segmentName, 
      string segmentId, 
      string segmentRowId, 
      string segmentRowIdRepeat, 
      string componentId, 
      string subComponentId)
    {
      SegmentName = string.Empty;
      SegmentId = string.Empty;
      SegmentName = segmentName;
      SegmentId = segmentId;
      SegmentRowId = segmentRowId;
      SegmentRowIdRepeat = segmentRowIdRepeat;
      ComponentId = componentId;
      SubComponentId = subComponentId;
    }

    #endregion
  }
}