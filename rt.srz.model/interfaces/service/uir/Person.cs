// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Person.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The person.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service.uir
{
  #region

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The person.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true, Namespace = "http://uir.ffoms.ru")]
  [XmlRoot(Namespace = "http://uir.ffoms.ru", IsNullable = false)]
  public class Person
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the main enp.
    /// </summary>
    [XmlElement(Order = 0)]
    public string MainENP { get; set; }

    /// <summary>
    ///   Gets or sets the regional enp.
    /// </summary>
    [XmlElement(Order = 1)]
    public string RegionalENP { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Create a clone of this Person object
    /// </summary>
    /// <returns> The <see cref="Person" /> . </returns>
    public virtual Person Clone()
    {
      return (Person)MemberwiseClone();
    }

    #endregion
  }
}