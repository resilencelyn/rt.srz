﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PolicyOrder.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   POLICY_ORDER
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.card.za2
{
  #region references

  using System;

  using rt.srz.model.HL7.card.target;
  using rt.srz.model.HL7.person.target;

  #endregion

  /// <summary>
  ///   POLICY_ORDER
  /// </summary>
  [Serializable]
  public class PolicyOrder
  {
    #region Public Properties

    /// <summary>
    ///   IN1
    /// </summary>
    public In1Card IN1 { get; set; }

    /// <summary>
    ///   ZQH
    /// </summary>
    public ZQH ZQH { get; set; }

    #endregion
  }
}