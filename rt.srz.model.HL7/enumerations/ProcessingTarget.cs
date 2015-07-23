// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessingTarget.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The processing target.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.enumerations
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   The processing target.
  /// </summary>
  [CLSCompliant(false)]
  [Flags]
  public enum ProcessingTarget : ulong
  {
    /// <summary>
    ///   The ack.
    /// </summary>
    Ack = 1L, 

    /// <summary>
    ///   The ad t_ a 01.
    /// </summary>
    ADT_A01 = 0x1000000L, 

    /// <summary>
    ///   The ad t_ a 03.
    /// </summary>
    ADT_A03 = 0x2000000L, 

    /// <summary>
    ///   The ad t_ a 24.
    /// </summary>
    ADT_A24 = 0x4000000L, 

    /// <summary>
    ///   The ad t_ a 37.
    /// </summary>
    ADT_A37 = 0x8000000L, 

    /// <summary>
    ///   The all targets.
    /// </summary>
    AllTargets = 18446744073709551615L, 

    /// <summary>
    ///   The bhs.
    /// </summary>
    BHS = 0x10000000000L, 

    /// <summary>
    ///   The bts.
    /// </summary>
    BTS = 0x20000000000L, 

    /// <summary>
    ///   The center targets.
    /// </summary>
    CenterTargets = 0x100030000000000L, 

    /// <summary>
    ///   The erp center targets.
    /// </summary>
    ErpCenterTargets = 0x100030000000000L, 

    /// <summary>
    ///   The erp region targets.
    /// </summary>
    ErpRegionTargets = 0x13070f000f01L, 

    /// <summary>
    ///   The erp targets.
    /// </summary>
    ErpTargets = 0x10013070f000f01L, 

    /// <summary>
    ///   The kptu region targets.
    /// </summary>
    KptuRegionTargets = 0x230000030001L, 

    /// <summary>
    ///   The kptu targets.
    /// </summary>
    KptuTargets = 0x230000030001L, 

    /// <summary>
    ///   The no targets.
    /// </summary>
    NoTargets = 0L, 

    /// <summary>
    ///   The person card.
    /// </summary>
    PersonCard = 0x200000000000L, 

    /// <summary>
    ///   The person erp.
    /// </summary>
    PersonErp = 0x100000000000L, 

    /// <summary>
    ///   The qb p_ z p 1.
    /// </summary>
    QBP_ZP1 = 0x100000000L, 

    /// <summary>
    ///   The qb p_ z p 2.
    /// </summary>
    QBP_ZP2 = 0x200000000L, 

    /// <summary>
    ///   The qb p_ z p 4.
    /// </summary>
    QBP_ZP4 = 0x400000000L, 

    /// <summary>
    ///   The region targets.
    /// </summary>
    RegionTargets = 0x33070f030f01L, 

    /// <summary>
    ///   The rs p_ z k 1.
    /// </summary>
    RSP_ZK1 = 0x100L, 

    /// <summary>
    ///   The rs p_ z k 2.
    /// </summary>
    RSP_ZK2 = 0x200L, 

    /// <summary>
    ///   The rs p_ z k 4.
    /// </summary>
    RSP_ZK4 = 0x400L, 

    /// <summary>
    ///   The rs p_ z k 5.
    /// </summary>
    RSP_ZK5 = 0x800L, 

    /// <summary>
    ///   The x element.
    /// </summary>
    XElement = 0x100000000000000L, 

    /// <summary>
    ///   The zp i_ z a 1.
    /// </summary>
    ZPI_ZA1 = 0x10000L, 

    /// <summary>
    ///   The zp i_ z a 7.
    /// </summary>
    ZPI_ZA7 = 0x20000L
  }
}