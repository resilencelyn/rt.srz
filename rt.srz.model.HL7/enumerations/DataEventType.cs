// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataEventType.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The data event type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.enumerations
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   The data event type.
  /// </summary>
  [CLSCompliant(false)]
  public enum DataEventType : short
  {
    /// <summary>
    ///   The batch process.
    /// </summary>
    BatchProcess = 10, 

    /// <summary>
    ///   The change policy complete.
    /// </summary>
    ChangePolicyComplete = 0x4ec, 

    /// <summary>
    ///   The change policy employment.
    /// </summary>
    ChangePolicyEmployment = 0x4e2, 

    /// <summary>
    ///   The change policy partial.
    /// </summary>
    ChangePolicyPartial = 0x4d8, 

    /// <summary>
    ///   The decommit policy enp.
    /// </summary>
    DecommitPolicyENP = 0x51e, 

    /// <summary>
    ///   The deregister policy.
    /// </summary>
    DeregisterPolicy = 0x4c4, 

    /// <summary>
    ///   The issue or reissuance policy announce.
    /// </summary>
    IssueOrReissuancePolicyAnnounce = 0xc1c, 

    /// <summary>
    ///   The issue policy.
    /// </summary>
    IssuePolicy = 0x4ba, 

    /// <summary>
    ///   The notification of receive announce.
    /// </summary>
    NotificationOfReceiveAnnounce = 0xc80, 

    /// <summary>
    ///   The query dublicates.
    /// </summary>
    QueryDublicates = 0x9ce, 

    /// <summary>
    ///   The query person insurance.
    /// </summary>
    QueryPersonInsurance = 0x83e, 

    /// <summary>
    ///   The query persons dead abroad.
    /// </summary>
    QueryPersonsDeadAbroad = 0x96a, 

    /// <summary>
    ///   The query persons deregistrating.
    /// </summary>
    QueryPersonsDeregistrating = 0x906, 

    /// <summary>
    ///   The query persons registrating.
    /// </summary>
    QueryPersonsRegistrating = 0x8a2, 

    /// <summary>
    ///   The recommit policy enp.
    /// </summary>
    RecommitPolicyENP = 0x582, 

    /// <summary>
    ///   The register policy.
    /// </summary>
    RegisterPolicy = 0x4ce, 

    /// <summary>
    ///   The resolve policy collisions.
    /// </summary>
    ResolvePolicyCollisions = 0x64a, 

    /// <summary>
    ///   The resolve policy dublicates.
    /// </summary>
    ResolvePolicyDublicates = 0x5e6, 

    /// <summary>
    ///   The undefined.
    /// </summary>
    Undefined = 0
  }
}