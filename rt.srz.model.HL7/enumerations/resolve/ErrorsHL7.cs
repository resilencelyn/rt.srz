// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorsHl7.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The errors h l 7.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.enumerations.resolve
{
  /// <summary>
  ///   The errors h l 7.
  /// </summary>
  public static class ErrorsHl7
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
    public static string GetApplicationError(ErrorHl7 error)
    {
      switch (error)
      {
        case ErrorHl7.FileXmlFailed:
          return "40";

        case ErrorHl7.FileXsdFailed:
          return "41";

        case ErrorHl7.FileHashFailed:
          return "44";

        case ErrorHl7.BatchIdentifierFailed:
          return "42";

        case ErrorHl7.BatchMessagesMissing:
        case ErrorHl7.BatchMessagesOverload:
          return "43";
      }

      return Hl7Helper.UnspecifiedErrorAPP;
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
    public static string GetErrorCode(ErrorHl7 error)
    {
      switch (error)
      {
        case ErrorHl7.FileXmlFailed:
        case ErrorHl7.FileXsdFailed:
        case ErrorHl7.FileHashFailed:
        case ErrorHl7.BatchIdentifierFailed:
        case ErrorHl7.BatchMessagesMissing:
        case ErrorHl7.BatchMessagesOverload:
          return "207";

        case ErrorHl7.BatchException:
        case ErrorHl7.MessageException:
          return "207";

        case ErrorHl7.BatchConversionFailed:
        case ErrorHl7.BatchDataFailed:
        case ErrorHl7.MessageConversionFailed:
        case ErrorHl7.MessageDataFailed:
        case ErrorHl7.MessageIdentifierFailed:
          return "102";

        case ErrorHl7.BatchMessagesRejected:
          return "202";

        case ErrorHl7.MessageCommandUnknown:
          return "200";

        case ErrorHl7.TableDataFailed:
          return "103";

        case ErrorHl7.KeyNotFound:
          return "204";

        case ErrorHl7.KeyExistsAlready:
          return "205";

        case ErrorHl7.FlagUnauthorized:
          return "206";
      }

      return Hl7Helper.UnspecifiedErrorISO;
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
    public static string GetErrorSeverityLevel(ErrorHl7 error)
    {
      if (error == ErrorHl7.None)
      {
        return string.Empty;
      }

      if (error == ErrorHl7.BatchMessagesRejected)
      {
        return "W";
      }

      return Hl7Helper.FatalSeverityLevel;
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
    public static bool IsBatchProcessible(ErrorHl7 error)
    {
      switch (error)
      {
        case ErrorHl7.BatchMessagesRejected:
        case ErrorHl7.MessageException:
        case ErrorHl7.MessageConversionFailed:
        case ErrorHl7.MessageDataFailed:
        case ErrorHl7.MessageCommandUnknown:
        case ErrorHl7.MessageIdentifierFailed:
        case ErrorHl7.TableDataFailed:
        case ErrorHl7.None:
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
    public static bool IsErrorFatal(ErrorHl7 error)
    {
      return Hl7Helper.IsSeverityFatal(GetErrorSeverityLevel(error));
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
    public static bool IsWorkflowEffector(ErrorHl7 error)
    {
      switch (error)
      {
        case ErrorHl7.BatchMessagesMissing:
        case ErrorHl7.BatchMessagesOverload:
          return true;
      }

      return false;
    }

    #endregion
  }
}