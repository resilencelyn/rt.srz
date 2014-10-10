// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INSURANCE.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   INSURANCE
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.card.za3
{
  #region references

  using System;

  using rt.srz.model.Hl7.card.target;
  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   INSURANCE
  /// </summary>
  [Serializable]
  public class INSURANCE
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