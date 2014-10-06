// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryResponseZP1.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   QueryResponseZP1
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.requests
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.person.target;

  #endregion

  /// <summary>
  ///   QueryResponseZP1
  /// </summary>
  [Serializable]
  public class QueryResponseZP1
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="QueryResponseZP1" /> class.
    /// </summary>
    public QueryResponseZP1()
    {
      Qri = new QRI();
      In1List = new List<IN1>();
      Pid = new PID();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   In
    /// </summary>
    [XmlElement(ElementName = "IN1", Order = 2)]
    public List<IN1> In1List { get; set; }

    /// <summary>
    ///   Pid
    /// </summary>
    [XmlElement(ElementName = "PID", Order = 1)]
    public PID Pid { get; set; }

    /// <summary>
    ///   Qri
    /// </summary>
    [XmlElement(ElementName = "QRI", Order = 3)]
    public QRI Qri { get; set; }

    #endregion
  }
}