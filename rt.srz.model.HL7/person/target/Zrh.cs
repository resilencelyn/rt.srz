// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zrh.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The zrh.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The zrh.
  /// </summary>
  [Serializable]
  public class Zrh
  {
    #region Fields

    /// <summary>
    ///   The count.
    /// </summary>
    [XmlElement(ElementName = "ZRH.3", Order = 3)]
    public string Count;

    /// <summary>
    ///   The customers request block id.
    /// </summary>
    [XmlElement(ElementName = "ZRH.1", Order = 1)]
    public EiStructure CustomersRequestBlockID = new EiStructure();

    /// <summary>
    ///   The execution date.
    /// </summary>
    [XmlElement(ElementName = "ZRH.5", Order = 5)]
    public string ExecutionDate;

    /// <summary>
    ///   The executors request rlock id.
    /// </summary>
    [XmlElement(ElementName = "ZRH.2", Order = 2)]
    public EiStructure ExecutorsRequestRlockID = new EiStructure();

    /// <summary>
    ///   The formation date.
    /// </summary>
    [XmlElement(ElementName = "ZRH.4", Order = 4)]
    public string FormationDate;

    #endregion
  }
}