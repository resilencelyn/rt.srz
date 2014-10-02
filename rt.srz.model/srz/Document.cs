// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Document.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
    /// <summary>
    /// Gets the series number.
    /// </summary>
     [XmlIgnore] 
    public virtual string SeriesNumber
    {
      get 
      {
        return string.IsNullOrEmpty(Series) ? Number : string.Format("{0} ¹ {1}", Series, Number);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether valid document.
    /// </summary>
     [XmlElement(Order = 50)]
     [DataMember(Order = 50)]
     public virtual bool ExistDocument { get; set; }
  }
}