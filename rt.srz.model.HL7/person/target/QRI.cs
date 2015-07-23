// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QRI.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The qri.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The qri.
  /// </summary>
  [Serializable]
  public class QRI
  {
    #region Fields

    /// <summary>
    ///   The code compare list.
    /// </summary>
    [XmlElement(ElementName = "QRI.2", Order = 2)]
    public List<string> CodeCompareList;

    /// <summary>
    ///   The confidence.
    /// </summary>
    [XmlElement(ElementName = "QRI.1", Order = 1)]
    public string Confidence;

    #endregion
  }
}