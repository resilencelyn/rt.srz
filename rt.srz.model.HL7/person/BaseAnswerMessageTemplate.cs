// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseAnswerMessageTemplate.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The base answer message template.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   The base answer message template.
  /// </summary>
  [Serializable]
  public class BaseAnswerMessageTemplate
  {
    #region Fields

    /// <summary>
    ///   The err list.
    /// </summary>
    [XmlElement(ElementName = "ERR", Order = 3)]
    public List<ERR> ErrList = new List<ERR>();

    /// <summary>
    ///   The msa.
    /// </summary>
    [XmlElement(ElementName = "MSA", Order = 2)]
    public Msa Msa = new Msa();

    /// <summary>
    ///   The msh.
    /// </summary>
    [XmlElement(ElementName = "MSH", Order = 1)]
    public MSH Msh = new MSH();

    #endregion
  }
}