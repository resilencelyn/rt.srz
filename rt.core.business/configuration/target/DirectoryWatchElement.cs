// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryWatchElement.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.core.business.configuration.target
{
  using System.Configuration;

  /// <summary>
  ///   The file name element.
  /// </summary>
  public class DirectoryWatchElement : ConfigurationElement
  {
    /// <summary>
    ///   Путь
    /// </summary>
    [ConfigurationProperty("Path", IsRequired = true)]
    public string Path
    {
      get { return this["Path"] as string; }
      ////get { return "D:\\work\\project\\gisoms\\SOURCE\\PVP\\rt.srz.ui.pvp\\WorkingExchange\\In"; }
    }

    /////// <summary>
    ///////   Фильтр
    /////// </summary>
    ////[ConfigurationProperty("Filter", IsRequired = true, DefaultValue = "")]
    ////public string Filter
    ////{
    ////  get { return this["Filter"] as string; }
    ////}
  }
}