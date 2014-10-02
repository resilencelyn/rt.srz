// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfirmationHL7.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The confirmation h l 7.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.enumerations
{
  /// <summary>
  ///   The confirmation h l 7.
  /// </summary>
  public enum ConfirmationHL7
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