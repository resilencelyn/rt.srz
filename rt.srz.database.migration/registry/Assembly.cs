using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace rt.srz.database.registry
{
  static internal class Assembly
  {
    // Преобразование содержимого сборки в HEX строку
    private static string GetAssemblyHEXString(string assemblyPath)
    {
      StringBuilder builder = new StringBuilder();
      builder.Append("0x");

      using (FileStream stream = new FileStream(assemblyPath, FileMode.Open, FileAccess.Read, FileShare.Read))
      {
        int currentByte = stream.ReadByte();
        while (currentByte > -1)
        {
          builder.Append(currentByte.ToString("X2", CultureInfo.InvariantCulture));
          currentByte = stream.ReadByte();
        }
      }
      return builder.ToString();
    }
    
    
    /// <summary>
    /// Скрипт настройки БД для загрузки CLR сборок
    /// </summary>
    /// <param name="databaseName"></param>
    /// <returns></returns>
    public static string AdjustDatabase(string databaseName)
    {
      return string.Format(@"EXEC sp_changedbowner 'sa'
                           ALTER DATABASE [{0}] SET TRUSTWORTHY ON", databaseName);
                                                                   
    }

    /// <summary>
    /// Деплоит CLR сборки
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string DeployDatabaseAssembly(string assemblyName)
    {
      var assemblyPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin", assemblyName + ".dll");
      return string.Format(@"CREATE ASSEMBLY [{0}]
                           FROM {1} 
                           WITH PERMISSION_SET = UNSAFE", assemblyName, GetAssemblyHEXString(assemblyPath));
    }
  }
}
