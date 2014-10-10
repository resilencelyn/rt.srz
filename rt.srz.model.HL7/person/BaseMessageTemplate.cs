// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseMessageTemplate.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The base message template.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person
{
  #region references

  using System;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   The base message template.
  /// </summary>
  [Serializable]
  public class BaseMessageTemplate
  {
    #region Fields

    /// <summary>
    ///   The msh.
    /// </summary>
    [XmlElement(ElementName = "MSH", Order = 1)]
    public MSH Msh = new MSH();

    #endregion
  }
}