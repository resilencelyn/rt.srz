// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileNameElement.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The file name element.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.nhibernate.target
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