// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfirmationHl7.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The confirmation h l 7.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.enumerations
{
  /// <summary>
  ///   The confirmation h l 7.
  /// </summary>
  public enum ConfirmationHl7
  {
    /// <summary>
    ///   The none.
    /// </summary>
    None, 

    /// <summary>
    ///   The commit accept.
    /// </summary>
    CommitAccept, 

    /// <summary>
    ///   The commit error.
    /// </summary>
    CommitError, 

    /// <summary>
    ///   The commit reject.
    /// </summary>
    CommitReject, 

    /// <summary>
    ///   The application accept.
    /// </summary>
    ApplicationAccept, 

    /// <summary>
    ///   The application error.
    /// </summary>
    ApplicationError, 

    /// <summary>
    ///   The application reject.
    /// </summary>
    ApplicationReject
  }
}