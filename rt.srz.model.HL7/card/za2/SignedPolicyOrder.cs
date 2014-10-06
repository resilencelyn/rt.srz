// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignedPolicyOrder.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   SIGNED_POLICY_ORDER
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.card.za2
{
  #region references

  using System;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.card.target;

  #endregion

  /// <summary>
  ///   SIGNED_POLICY_ORDER
  /// </summary>
  [Serializable]
  public class SignedPolicyOrder
  {
    #region Public Properties

    /// <summary>
    ///   POLICY_ORDER
    /// </summary>
    [XmlElement("ZPI_ZA2.POLICY_ORDER")]
    public PolicyOrder POLICY_ORDER { get; set; }

    /// <summary>
    ///   ZSG
    /// </summary>
    public ZSG ZSG { get; set; }

    #endregion
  }
}