// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BarcodeConverterException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The barcode converter exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Runtime.Serialization;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The barcode converter exception.
  /// </summary>
  public class BarcodeConverterException : Exception
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="BarcodeConverterException" /> class.
    /// </summary>
    public BarcodeConverterException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeConverterException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message. 
    /// </param>
    public BarcodeConverterException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeConverterException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message. 
    /// </param>
    /// <param name="innerException">
    /// The inner exception. 
    /// </param>
    public BarcodeConverterException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeConverterException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info. 
    /// </param>
    /// <param name="context">
    /// The context. 
    /// </param>
    protected BarcodeConverterException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}