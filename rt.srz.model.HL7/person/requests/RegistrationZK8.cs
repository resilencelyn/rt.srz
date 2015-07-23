// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistrationZK8.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The registration_ z k 8.
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
  ///   The registration_ z k 8.
  /// </summary>
  [Serializable]
  public class Registration_ZK8
  {
    #region Fields

    /// <summary>
    ///   The in 1 list.
    /// </summary>
    [XmlElement(ElementName = "IN1")]
    public List<IN1> In1List = new List<IN1>();

    #endregion
  }
}