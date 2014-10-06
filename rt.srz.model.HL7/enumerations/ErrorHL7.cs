// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorHL7.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The error h l 7.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.enumerations
{
  /// <summary>
  ///   The error h l 7.
  /// </summary>
  public enum ErrorHL7
  {
    /// <summary>
    ///   The none.
    /// </summary>
    None, 

    /// <summary>
    ///   The file xml failed.
    /// </summary>
    FileXmlFailed, 

    /// <summary>
    ///   The file xsd failed.
    /// </summary>
    FileXsdFailed, 

    /// <summary>
    ///   The file hash failed.
    /// </summary>
    FileHashFailed, 

    /// <summary>
    ///   The batch identifier failed.
    /// </summary>
    BatchIdentifierFailed, 

    /// <summary>
    ///   The batch exception.
    /// </summary>
    BatchException, 

    /// <summary>
    ///   The batch conversion failed.
    /// </summary>
    BatchConversionFailed, 

    /// <summary>
    ///   The batch data failed.
    /// </summary>
    BatchDataFailed, 

    /// <summary>
    ///   The batch messages missing.
    /// </summary>
    BatchMessagesMissing, 

    /// <summary>
    ///   The batch messages overload.
    /// </summary>
    BatchMessagesOverload, 

    /// <summary>
    ///   The batch messages rejected.
    /// </summary>
    BatchMessagesRejected, 

    /// <summary>
    ///   The message exception.
    /// </summary>
    MessageException, 

    /// <summary>
    ///   The message conversion failed.
    /// </summary>
    MessageConversionFailed, 

    /// <summary>
    ///   The message data failed.
    /// </summary>
    MessageDataFailed, 

    /// <summary>
    ///   The message command unknown.
    /// </summary>
    MessageCommandUnknown, 

    /// <summary>
    ///   The message identifier failed.
    /// </summary>
    MessageIdentifierFailed, 

    /// <summary>
    ///   The table data failed.
    /// </summary>
    TableDataFailed, 

    /// <summary>
    ///   The key not found.
    /// </summary>
    KeyNotFound, 

    /// <summary>
    ///   The key exists already.
    /// </summary>
    KeyExistsAlready, 

    /// <summary>
    ///   The flag unauthorized.
    /// </summary>
    FlagUnauthorized
  }
}