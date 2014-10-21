// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Assembly.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The assembly.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.registry
{
  using System.Globalization;
  using System.IO;
  using System.Text;
  using System.Web.Hosting;

  /// <summary>
  ///   The assembly.
  /// </summary>
  internal static class Assembly
  {
    // Преобразование содержимого сборки в HEX строку
    #region Public Methods and Operators

    /// <summary>
    /// Скрипт настройки БД для загрузки CLR сборок
    /// </summary>
    /// <param name="databaseName">
    /// The database Name.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string AdjustDatabase(string databaseName)
    {
      return string.Format(@"EXEC sp_changedbowner 'sa' ALTER DATABASE [{0}] SET TRUSTWORTHY ON", databaseName);
    }

    /// <summary>
    /// Деплоит CLR сборки
    /// </summary>
    /// <param name="assemblyName">
    /// The assembly Name.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string DeployDatabaseAssembly(string assemblyName)
    {
      var assemblyPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin", assemblyName + ".dll");
      const string Sql = @"CREATE ASSEMBLY [{0}]
                           FROM {1} 
                           WITH PERMISSION_SET = UNSAFE";
      return string.Format(Sql, assemblyName, GetAssemblyHexString(assemblyPath));
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get assembly hex string.
    /// </summary>
    /// <param name="assemblyPath">
    /// The assembly path.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string GetAssemblyHexString(string assemblyPath)
    {
      var builder = new StringBuilder();
      builder.Append("0x");

      using (var stream = new FileStream(assemblyPath, FileMode.Open, FileAccess.Read, FileShare.Read))
      {
        var currentByte = stream.ReadByte();
        while (currentByte > -1)
        {
          builder.Append(currentByte.ToString("X2", CultureInfo.InvariantCulture));
          currentByte = stream.ReadByte();
        }
      }

      return builder.ToString();
    }

    #endregion
  }
}