// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ERR.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The err.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The err.
  /// </summary>
  [Serializable]
  public class ERR
  {
    #region Fields

    /// <summary>
    ///   The error code app.
    /// </summary>
    [XmlElement(ElementName = "ERR.5", Order = 5)]
    public ERR5 ErrorCodeApp;

    /// <summary>
    ///   The error code hl 7.
    /// </summary>
    [XmlElement(ElementName = "ERR.3", Order = 3)]
    public ERR3 ErrorCodeHl7;

    /// <summary>
    ///   The error position.
    /// </summary>
    [XmlElement(ElementName = "ERR.2", Order = 2)]
    public ERR2 ErrorPosition;

    /// <summary>
    ///   The errors.
    /// </summary>
    [XmlElement(ElementName = "ERR.6", Order = 6)]
    public List<string> Errors;

    /// <summary>
    ///   The level seriously.
    /// </summary>
    [XmlElement(ElementName = "ERR.4", Order = 4)]
    public string LevelSeriously;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ERR" /> class.
    /// </summary>
    public ERR()
    {
      ErrorPosition = new ERR2();
      ErrorCodeHl7 = new ERR3();
      ErrorCodeApp = new ERR5();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ERR"/> class.
    /// </summary>
    /// <param name="errorPosition">
    /// The error position.
    /// </param>
    /// <param name="errorCodeHl7">
    /// The error code hl 7.
    /// </param>
    /// <param name="levelSeriously">
    /// The level seriously.
    /// </param>
    /// <param name="errorCodeApp">
    /// The error code app.
    /// </param>
    public ERR(ERR2 errorPosition, ERR3 errorCodeHl7, string levelSeriously, ERR5 errorCodeApp)
    {
      ErrorPosition = new ERR2();
      ErrorCodeHl7 = new ERR3();
      ErrorCodeApp = new ERR5();
      ErrorPosition = errorPosition;
      ErrorCodeHl7 = errorCodeHl7;
      LevelSeriously = levelSeriously;
      ErrorCodeApp = errorCodeApp;
    }

    #endregion
  }
}