// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Twin.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The Twin.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System.Linq;
  using System.Xml.Serialization;

  /// <summary>
  ///   The Twin.
  /// </summary>
  public partial class Twin
  {
    #region Public Properties

    /// <summary>
    ///   Gets the twin key as text.
    /// </summary>
    [XmlIgnore]
    public virtual string TwinKeyAsText
    {
      get
      {
        return string.Join("; ", TwinsKeys.Select(t => t.KeyType.Name).ToArray());
      }
    }

    #endregion
  }
}