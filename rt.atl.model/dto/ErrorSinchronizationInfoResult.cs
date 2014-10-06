// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorSinchronizationInfoResult.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The error sinchronization info result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.model.dto
{
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  using rt.atl.model.atl;

  /// <summary>
  /// The error sinchronization info result.
  /// </summary>
  public class ErrorSinchronizationInfoResult : Przbuf
  {
    // [XmlElement]
    // [DataMember]
    // public virtual Przbuf Przbuf { get; set; }
    #region Public Properties

    /// <summary>
    /// Gets or sets the error.
    /// </summary>
    [XmlElement]
    [DataMember]
    public virtual string Error { get; set; }

    #endregion
  }
}