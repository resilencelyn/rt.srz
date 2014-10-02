// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileNameElementColection.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The file name element colection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.nhibernate.target
{
  #region references

  using System.Configuration;

  #endregion

  /// <summary>
  ///   The file name element colection.
  /// </summary>
  public class FileNameElementColection : ConfigurationElementCollection
  {
    #region Methods

    /// <summary>
    ///   The create new element.
    /// </summary>
    /// <returns>
    ///   The <see cref="ConfigurationElement" />.
    /// </returns>
    protected override ConfigurationElement CreateNewElement()
    {
      return new FileNameElement();
    }

    /// <summary>
    /// The get element key.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    protected override object GetElementKey(ConfigurationElement element)
    {
      var filenameElement = element as FileNameElement;
      return filenameElement != null ? filenameElement.FileNameConfiguration : element.ToString();
    }

    #endregion
  }
}