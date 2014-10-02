//-------------------------------------------------------------------------------------
// <copyright file="Twin.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------
using System.Linq;

namespace rt.srz.model.srz
{
  using System.Xml.Serialization;

  /// <summary>
  /// The Twin.
  /// </summary>
  public partial class Twin
  {
    /// <summary>
    /// Gets the twin key as text.
    /// </summary>
    [XmlIgnore]
    public virtual string TwinKeyAsText
    {
      get { return string.Join("; ", TwinsKeys.Select(t => t.KeyType.Name).ToArray()); }
    }
  }
}