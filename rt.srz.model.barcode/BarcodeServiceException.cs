// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BarcodeServiceException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The barcode service exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace rt.srz.model.barcode
{
  #region references

  

  #endregion

  /// <summary>The barcode service exception.</summary>
  public class BarcodeServiceException : Exception
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="BarcodeServiceException" /> class.
    /// </summary>
    public BarcodeServiceException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeServiceException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    public BarcodeServiceException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeServiceException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="innerException">
    /// The inner exception.
    /// </param>
    public BarcodeServiceException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeServiceException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected BarcodeServiceException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}