// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ERR3.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The er r 3.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The er r 3.
  /// </summary>
  [Serializable]
  public class ERR3
  {
    #region Fields

    /// <summary>
    ///   The error code.
    /// </summary>
    [XmlElement(ElementName = "CWE.1", Order = 1)]
    public string ErrorCode;

    /// <summary>
    ///   The error description.
    /// </summary>
    [XmlElement(ElementName = "CWE.2", Order = 2)]
    public string ErrorDescription;

    /// <summary>
    ///   The table code.
    /// </summary>
    [XmlElement(ElementName = "CWE.3", Order = 3)]
    public string TableCode;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ERR3" /> class.
    /// </summary>
    public ERR3()
    {
      TableCode = "1.2.643.2.40.5.100.357";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ERR3"/> class.
    /// </summary>
    /// <param name="errorCode">
    /// The error code.
    /// </param>
    /// <param name="errorDescription">
    /// The error description.
    /// </param>
    public ERR3(string errorCode, string errorDescription)
    {
      TableCode = "1.2.643.2.40.5.100.357";
      ErrorCode = errorCode;
      ErrorDescription = errorDescription;
    }

    #endregion
  }
}