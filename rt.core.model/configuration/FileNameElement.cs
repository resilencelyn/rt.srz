// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileNameElement.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The file name element.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.configuration
{
  #region references

  using System.Configuration;

  #endregion

  /// <summary>
  ///   The file name element.
  /// </summary>
  public class FileNameElement : ConfigurationElement
  {
    #region Public Properties

    /// <summary>
    ///   Заголовок
    /// </summary>
    [ConfigurationProperty("Name", IsRequired = true)]
    public string FileNameConfiguration
    {
      get
      {
        return this["Name"] as string;
      }
    }

    #endregion
  }
}