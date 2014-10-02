﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeWork.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The type work.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The type work.
  /// </summary>
  [Serializable]
  public class TypeWork
  {
    #region Fields

    /// <summary>
    ///   The type.
    /// </summary>
    [XmlElement(ElementName = "PT.1", Order = 1)]
    public string Type;

    /// <summary>
    ///   The view.
    /// </summary>
    [XmlElement(ElementName = "PT.2", Order = 2)]
    public string View;

    #endregion
  }
}