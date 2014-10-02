// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Surname.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The surname.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The surname.
  /// </summary>
  [Serializable]
  public class Surname
  {
    #region Fields

    /// <summary>
    ///   The name to print.
    /// </summary>
    [XmlElement(ElementName = "FN.3", Order = 3)]
    public string nameToPrint;

    /// <summary>
    ///   The surname.
    /// </summary>
    [XmlElement(ElementName = "FN.1", Order = 1)]
    public string surname;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="Surname" /> class.
    /// </summary>
    public Surname()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Surname"/> class.
    /// </summary>
    /// <param name="surname">
    /// The surname.
    /// </param>
    public Surname(string surname)
    {
      this.surname = surname;
    }

    #endregion
  }
}