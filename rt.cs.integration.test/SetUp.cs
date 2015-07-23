// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetUp.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.integration.test
{
  using System;
  using System.Configuration;
  using System.Diagnostics;
  using System.IO;

  using NUnit.Framework;

  using rt.core.model.configuration;

  /// <summary>
  ///   The set up.
  /// </summary>
  [SetUpFixture]
  public class SetUp
  {
    private const string ApplicationNameGate = "foms.srz\\FOMS_gateway.exe";

    /// <summary>
    /// The application name cs.
    /// </summary>
    private const string ApplicationNameCs = "foms.cs\\FOMS_gateway.exe";

    #region Fields

    private Process processGate;

    private Process processCs;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The run after any tests.
    /// </summary>
    [TearDown]
    public void RunAfterAnyTests()
    {
      if (processGate.HasExited == false)
      {
        processGate.Kill();
      }

      if (processCs.HasExited == false)
      {
        processCs.Kill();
      }

      // востановление бэкапа
      var connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["foms_cs"].ConnectionString);
      connection.Open();
      var sqlCommand = connection.CreateCommand();
      sqlCommand.CommandText = @"EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'foms_cs'";
      sqlCommand.ExecuteNonQuery();
      sqlCommand.CommandText = @"DROP DATABASE [foms_cs]";
      sqlCommand.ExecuteNonQuery();
      sqlCommand.CommandText = @"RESTORE DATABASE [foms_cs] FROM  DISK = N'E:\sql\db\backups\foms_cs.bak' WITH  FILE = 1,  MOVE N'FC_developer' TO N'D:\sql\db\foms_cs.mdf',  MOVE N'PolicyEvent' TO N'D:\sql\db\foms_cs_1.ndf',  MOVE N'PolicyTable' TO N'D:\sql\db\foms_cs_2.ndf',  MOVE N'PolicyIndex' TO N'D:\sql\db\foms_cs_3.ndf',  MOVE N'OpenDataTable' TO N'D:\sql\db\foms_cs_4.ndf',  MOVE N'OpenDataIndex' TO N'D:\sql\db\foms_cs_5.ndf',  MOVE N'SearchKeyTable' TO N'D:\sql\db\foms_cs_6.ndf',  MOVE N'SearchKeyIndex' TO N'D:\sql\db\foms_cs_7.ndf',  MOVE N'Employment' TO N'D:\sql\db\foms_cs_8.ndf',  MOVE N'AttachmentMO' TO N'D:\sql\db\foms_cs_9.ndf',  MOVE N'Main' TO N'D:\sql\db\foms_cs_10.ndf',  MOVE N'FC_developer_log' TO N'E:\sql\db\foms_cs_11.ldf',  NOUNLOAD,  STATS = 10";
      sqlCommand.ExecuteNonQuery();
    }

    /// <summary>
    ///   The run before any tests.
    /// </summary>
    [SetUp]
    public void RunBeforeAnyTests()
    {
      // удаляем предыдущие файлы
      var directories = Directory.GetDirectories(ConfigManager.ExchangeSettings.WorkingFolderExchange);
      foreach (var dir in directories)
      {
        Directory.Delete(dir, true);
      }

      // запускаем шлюз
      var applicationPath = GetApplicationPath(ApplicationNameGate);
      processGate = new Process { StartInfo = { FileName = applicationPath, Arguments = " -exe" } };
      processGate.Start();

      // запускаем шлюз
      applicationPath = GetApplicationPath(ApplicationNameCs);
      processCs = new Process { StartInfo = { FileName = applicationPath, Arguments = " -exe" } };
      processCs.Start();
    }

    /// <summary>
    /// The get application path.
    /// </summary>
    /// <param name="applicationName">
    /// The application name.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetApplicationPath(string applicationName)
    {
      var tmpDirName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
      var solutionFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(tmpDirName)));
      var result = Path.Combine(solutionFolder, applicationName);
      return result;
    }

    #endregion
  }
}