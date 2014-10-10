// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryResponse.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Базовый класс для списков
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.requests
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   Базовый класс для списков
  /// </summary>
  [Serializable]
  public class QueryResponse
  {
    #region Public Properties

    /// <summary>
    ///   In
    /// </summary>
    [XmlElement(ElementName = "IN1", Order = 2)]
    public List<IN1> In1List { get; set; }

    /// <summary>
    ///   список идентификаторов
    /// </summary>
    [XmlElement(ElementName = "PID", Order = 1)]
    public List<PID> PidList { get; set; }

    /// <summary>
    ///   Алгоритмы поиска
    /// </summary>
    [XmlElement(ElementName = "QRI", Order = 3)]
    public QRI Qri { get; set; }

    #endregion
  }
}