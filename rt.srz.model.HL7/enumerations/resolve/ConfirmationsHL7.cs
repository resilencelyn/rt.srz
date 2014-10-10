// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfirmationsHl7.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The confirmations h l 7.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.enumerations.resolve
{
  /// <summary>
  ///   The confirmations h l 7.
  /// </summary>
  public static class ConfirmationsHl7
  {
    #region Public Methods and Operators

    /// <summary>
    /// The confirm as string.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ConfirmAsString(ConfirmationHl7 type)
    {
      switch (type)
      {
        case ConfirmationHl7.None:
          return string.Empty;

        case ConfirmationHl7.CommitAccept:
          return "CA";

        case ConfirmationHl7.CommitError:
          return "CE";

        case ConfirmationHl7.CommitReject:
          return "CR";

        case ConfirmationHl7.ApplicationAccept:
          return "AA";

        case ConfirmationHl7.ApplicationError:
          return "AE";

        case ConfirmationHl7.ApplicationReject:
          return "AR";
      }

      return type.ToString();
    }

    #endregion
  }
}