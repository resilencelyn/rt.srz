// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PolicyOrderBatch.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   POLICY_ORDER_BATCH
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.card.za2
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.card.target;

  #endregion

  /// <summary>
  ///   POLICY_ORDER_BATCH
  /// </summary>
  [Serializable]
  public class PolicyOrderBatch
  {
    #region Public Properties

    /// <summary>
    ///   SIGNED_POLICY_ORDER
    /// </summary>
    [XmlElement("ZPI_ZA2.SIGNED_POLICY_ORDER")]
    public List<SignedPolicyOrder> SignedPolicyOrder { get; set; }

    /// <summary>
    ///   ZRH
    /// </summary>
    public ZRH Zrh { get; set; }

    #endregion
  }
}