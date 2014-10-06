// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfirmationsHL7.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The confirmations h l 7.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.enumerations.resolve
{
  /// <summary>
  ///   The confirmations h l 7.
  /// </summary>
  public static class ConfirmationsHL7
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
    public static string ConfirmAsString(ConfirmationHL7 type)
    {
      switch (type)
      {
        case ConfirmationHL7.None:
          return string.Empty;

        case ConfirmationHL7.CommitAccept:
          return "CA";

        case ConfirmationHL7.CommitError:
          return "CE";

        case ConfirmationHL7.CommitReject:
          return "CR";

        case ConfirmationHL7.ApplicationAccept:
          return "AA";

        case ConfirmationHL7.ApplicationError:
          return "AE";

        case ConfirmationHL7.ApplicationReject:
          return "AR";
      }

      return type.ToString();
    }

    #endregion
  }
}