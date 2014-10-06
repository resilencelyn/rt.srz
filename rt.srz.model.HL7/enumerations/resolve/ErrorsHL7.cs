// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorsHL7.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The errors h l 7.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.enumerations.resolve
{
  /// <summary>
  ///   The errors h l 7.
  /// </summary>
  public static class ErrorsHL7
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get application error.
    /// </summary>
    /// <param name="error">
    /// The error.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetApplicationError(ErrorHL7 error)
    {
      switch (error)
      {
        case ErrorHL7.FileXmlFailed:
          return "40";

        case ErrorHL7.FileXsdFailed:
          return "41";

        case ErrorHL7.FileHashFailed:
          return "44";

        case ErrorHL7.BatchIdentifierFailed:
          return "42";

        case ErrorHL7.BatchMessagesMissing:
        case ErrorHL7.BatchMessagesOverload:
          return "43";
      }

      return HL7Helper.UnspecifiedErrorAPP;
    }

    /// <summary>
    /// The get error code.
    /// </summary>
    /// <param name="error">
    /// The error.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetErrorCode(ErrorHL7 error)
    {
      switch (error)
      {
        case ErrorHL7.FileXmlFailed:
        case ErrorHL7.FileXsdFailed:
        case ErrorHL7.FileHashFailed:
        case ErrorHL7.BatchIdentifierFailed:
        case ErrorHL7.BatchMessagesMissing:
        case ErrorHL7.BatchMessagesOverload:
          return "207";

        case ErrorHL7.BatchException:
        case ErrorHL7.MessageException:
          return "207";

        case ErrorHL7.BatchConversionFailed:
        case ErrorHL7.BatchDataFailed:
        case ErrorHL7.MessageConversionFailed:
        case ErrorHL7.MessageDataFailed:
        case ErrorHL7.MessageIdentifierFailed:
          return "102";

        case ErrorHL7.BatchMessagesRejected:
          return "202";

        case ErrorHL7.MessageCommandUnknown:
          return "200";

        case ErrorHL7.TableDataFailed:
          return "103";

        case ErrorHL7.KeyNotFound:
          return "204";

        case ErrorHL7.KeyExistsAlready:
          return "205";

        case ErrorHL7.FlagUnauthorized:
          return "206";
      }

      return HL7Helper.UnspecifiedErrorISO;
    }

    /// <summary>
    /// The get error severity level.
    /// </summary>
    /// <param name="error">
    /// The error.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetErrorSeverityLevel(ErrorHL7 error)
    {
      if (error == ErrorHL7.None)
      {
        return string.Empty;
      }

      if (error == ErrorHL7.BatchMessagesRejected)
      {
        return "W";
      }

      return HL7Helper.FatalSeverityLevel;
    }

    /// <summary>
    /// The is batch processible.
    /// </summary>
    /// <param name="error">
    /// The error.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsBatchProcessible(ErrorHL7 error)
    {
      switch (error)
      {
        case ErrorHL7.BatchMessagesRejected:
        case ErrorHL7.MessageException:
        case ErrorHL7.MessageConversionFailed:
        case ErrorHL7.MessageDataFailed:
        case ErrorHL7.MessageCommandUnknown:
        case ErrorHL7.MessageIdentifierFailed:
        case ErrorHL7.TableDataFailed:
        case ErrorHL7.None:
          return false;
      }

      return true;
    }

    /// <summary>
    /// The is error fatal.
    /// </summary>
    /// <param name="error">
    /// The error.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsErrorFatal(ErrorHL7 error)
    {
      return HL7Helper.IsSeverityFatal(GetErrorSeverityLevel(error));
    }

    /// <summary>
    /// The is workflow effector.
    /// </summary>
    /// <param name="error">
    /// The error.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsWorkflowEffector(ErrorHL7 error)
    {
      switch (error)
      {
        case ErrorHL7.BatchMessagesMissing:
        case ErrorHL7.BatchMessagesOverload:
          return true;
      }

      return false;
    }

    #endregion
  }
}