// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Document.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The Document.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  /// <summary>
  ///   The Document.
  /// </summary>
  public partial class Document
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether valid document.
    /// </summary>
    [XmlElement(Order = 50)]
    [DataMember(Order = 50)]
    public virtual bool ExistDocument { get; set; }

    /// <summary>
    ///   Gets the series number.
    /// </summary>
    [XmlIgnore]
    public virtual string SeriesNumber
    {
      get
      {
        return string.IsNullOrWhiteSpace(Series) ? Number : string.Format("{0} ¹ {1}", Series, Number);
      }
    }

    #endregion
  }
}