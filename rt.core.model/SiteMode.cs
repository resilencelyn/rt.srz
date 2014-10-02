// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SiteMode.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model
{
  #region

  using System;
  using System.Configuration;
  using System.Globalization;
  using System.Web.Configuration;

  #endregion

  /// <summary>
  /// The site mode.
  /// </summary>
  public static class SiteMode
  {
    #region Public Properties

    /// <summary>
    /// Gets a value indicating whether is online.
    /// </summary>
    public static bool IsOnline
    {
      get
      {
        return string.IsNullOrEmpty(ConfigurationManager.AppSettings["IsOnline"]) || bool.Parse(ConfigurationManager.AppSettings["IsOnline"]);
      }
    }

    /// <summary>
    /// Gets a value indicating whether is installed.
    /// </summary>
    public static bool BeingInstalled
    {
      get
      {
        return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["BeingInstalled"]) && bool.Parse(ConfigurationManager.AppSettings["BeingInstalled"]);
      }
    }

    /// <summary>
    /// Gets the on line date time.
    /// </summary>
    public static string OnLineDateTime
    {
      get
      {
        return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["TimeToOnline"])
                 ? ConfigurationManager.AppSettings["TimeToOnline"]
                 : DateTime.Now.AddDays(1).ToString(CultureInfo.InvariantCulture);
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The complete the installation.
    /// </summary>
    public static void CompleteTheInstallation()
    {
      var conf = WebConfigurationManager.OpenWebConfiguration("~");
      var keyValueConfigurationElement = conf.AppSettings.Settings["BeingInstalled"];
      if (keyValueConfigurationElement != null)
      {
        keyValueConfigurationElement.Value = "false";
        conf.Save(); 
      }
    }

    /// <summary>
    /// The set offline mode.
    /// </summary>
    /// <param name="onlineDateTime">
    /// The online date time.
    /// </param>
    public static void SetOfflineMode(string onlineDateTime)
    {
      // .Server.MapPath("~")
      // т.к. нету ещё контекста когда стартуют задачи кварца определяем путь библиотеки и считаем что в папке выше лежит конфиг
      // Configuration conf = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

      // использую такой странный способ так как свойство Location иногда на верменных папках даёт неверный результат
      // string codeBase = Assembly.GetExecutingAssembly().CodeBase;
      // UriBuilder uri = new UriBuilder(codeBase);
      // string path = Uri.UnescapeDataString(uri.Path);
      // var dirInfo = Directory.GetParent(Path.GetDirectoryName(path));
      // path = Path.Combine(dirInfo.FullName, "Web.config");
      var conf = WebConfigurationManager.OpenWebConfiguration("~");
      conf.AppSettings.Settings["IsOnline"].Value = "false";
      conf.AppSettings.Settings["TimeToOnline"].Value = onlineDateTime;
      conf.Save();
    }

    /// <summary>
    /// The set online mode.
    /// </summary>
    public static void SetOnlineMode()
    {
      var conf = WebConfigurationManager.OpenWebConfiguration("~");
      conf.AppSettings.Settings["IsOnline"].Value = "true";
      conf.Save();
    }

    #endregion
  }
}