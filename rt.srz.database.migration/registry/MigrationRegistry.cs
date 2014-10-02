// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MigrationRegistry.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.registry
{
  using System.Configuration;
  using System.Data.SqlClient;
  using System.IO;
  using System.Reflection;
  using rt.core.business.configuration;
  using StructureMap.Configuration.DSL;

  /// <summary>
  ///   The core registry.
  /// </summary>
  public class MigrationRegistry : Registry
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="MigrationRegistry" /> class.
    /// </summary>
    public MigrationRegistry()
    {
      var providerTypeName = ConfigManager.MigratorConfiguration.ProviderName;
      var connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
      var executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
      var migrator = new ECM7.Migrator.Migrator(providerTypeName, connectionString, executingAssembly, int.MaxValue);

      var migrationVersion = ConfigManager.MigratorConfiguration.MigrationVersion;  
      migrator.Migrate(migrationVersion);
      var currentClassLogger = NLog.LogManager.GetCurrentClassLogger();

      // Апдэйтим все хранимки и функции
      using (var connection = new SqlConnection(connectionString))
      {
        connection.Open();

        var sqlCommand = connection.CreateCommand();

        sqlCommand.CommandText = StoredProcedure.CreateStatementWithVersion();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("Recreate CreateStatementWithVersion ...");

        DeleteStoredProcedure(sqlCommand, "srz_CalculateEnpNumbers");
        sqlCommand.CommandText = StoredProcedure.CalculateEnpNumbers();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("DeleteStoredProcedure srz_CalculateEnpNumbers ...");

        DeleteStoredProcedure(sqlCommand, "srz_CalculateKladrLevelAndParrentId");
        sqlCommand.CommandText = StoredProcedure.CalculateKladrLevelAndParrentId();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("DeleteStoredProcedure srz_CalculateKladrLevelAndParrentId ...");

        DeleteStoredProcedure(sqlCommand, "srz_CalculateStandardSearchKeys");
        sqlCommand.CommandText = StoredProcedure.CalculateStandardSearchKeys();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("DeleteStoredProcedure  srz_CalculateStandardSearchKeys...");

        DeleteStoredProcedure(sqlCommand, "srz_CalculateStandardSearchKeysExchange");
        sqlCommand.CommandText = StoredProcedure.CalculateStandardSearchKeysExchange();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("DeleteStoredProcedure srz_CalculateStandardSearchKeysExchange ...");

        DeleteStoredProcedure(sqlCommand, "srz_CalculateUserSearchKeys");
        sqlCommand.CommandText = StoredProcedure.CalculateUserSearchKeys();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("DeleteStoredProcedure  srz_CalculateUserSearchKeys...");

        DeleteStoredProcedure(sqlCommand, "srz_CalculateUserSearchKeysExchange");
        sqlCommand.CommandText = StoredProcedure.CalculateUserSearchKeysExchange();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("DeleteStoredProcedure srz_CalculateUserSearchKeysExchange ...");

        DeleteStoredProcedure(sqlCommand, "srz_CreateExportSmoBatches");
        sqlCommand.CommandText = StoredProcedure.CreateExportSmoBatches();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("DeleteStoredProcedure srz_CreateExportSmoBatches ...");

        DeleteStoredProcedure(sqlCommand, "srz_CreateExportSmoBatchesForPdp");
        sqlCommand.CommandText = StoredProcedure.CreateExportSmoBatchesForPdp();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("DeleteStoredProcedure srz_CreateExportSmoBatchesForPdp ...");

        DeleteStoredProcedure(sqlCommand, "srz_FindTwins");
        sqlCommand.CommandText = StoredProcedure.FindTwins();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("DeleteStoredProcedure  srz_FindTwins...");

        DeleteStoredProcedure(sqlCommand, "srz_ProcessPfr");
        sqlCommand.CommandText = StoredProcedure.ProcessPfr();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("DeleteStoredProcedure srz_ProcessPfr ...");

        DeleteStoredProcedure(sqlCommand, "srz_ProcessSnilsPfr");
        sqlCommand.CommandText = StoredProcedure.ProcessSnilsPfr();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("DeleteStoredProcedure srz_ProcessSnilsPfr ...");

        DeleteStoredProcedure(sqlCommand, "srz_ProcessZags");
        sqlCommand.CommandText = StoredProcedure.ProcessZags();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("DeleteStoredProcedure srz_ProcessZags ...");

        // Настраиваем БД для загрузки CLR сборок
        sqlCommand.CommandText = rt.srz.database.registry.Assembly.AdjustDatabase(sqlCommand.Connection.Database);
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("DeleteStoredProcedure AdjustDatabase ...");

        // Удаляем все что связано с CLR сборками
        DeleteFunction(sqlCommand, "CalcStandardSearchKeys");
        currentClassLogger.Info("DeleteFunction  CalcStandardSearchKeys...");
        DeleteFunction(sqlCommand, "CalcUserSearchKey");
        currentClassLogger.Info("DeleteFunction  CalcUserSearchKey...");
        DeleteFunction(sqlCommand, "CalcStandardSearchKeysExchange");
        currentClassLogger.Info("DeleteFunction CalcStandardSearchKeysExchange ...");
        DeleteFunction(sqlCommand, "CalcUserSearchKeyExchange");
        currentClassLogger.Info("DeleteFunction CalcUserSearchKeyExchange ...");
        DeleteAssembly(sqlCommand, "rt.srz.database.business.sqlserver");
        currentClassLogger.Info("Delete  rt.srz.database.business.sqlserver...");
        DeleteAssembly(sqlCommand, "rt.srz.database.business");
        currentClassLogger.Info("Delete rt.srz.database.business...");
        
        // Деплоим rt.srz.database.business
        sqlCommand.CommandText = Assembly.DeployDatabaseAssembly("rt.srz.database.business");
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("Recreate rt.srz.database.business ...");

        // Деплоим rt.srz.database.business.sqlserver
        sqlCommand.CommandText = Assembly.DeployDatabaseAssembly("rt.srz.database.business.sqlserver");
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("Recreate rt.srz.database.business.sqlserver ...");

        sqlCommand.CommandText = Function.CalcStandardSearchKeys();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("Recreate CalcStandardSearchKeys ...");

        sqlCommand.CommandText = Function.CalcUserSearchKey();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("Recreate CalcUserSearchKey ...");

        sqlCommand.CommandText = Function.CalcStandardSearchKeysExchange();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("Recreate CalcStandardSearchKeysExchange ...");

        sqlCommand.CommandText = Function.CalcUserSearchKeyExchange();
        sqlCommand.ExecuteNonQuery();
        currentClassLogger.Info("Recreate CalcUserSearchKeyExchange ...");
      }
    }

    /// <summary>
    ///  Удаляет хранимку из БД
    /// </summary>
    /// <param name="command"></param>
    /// <param name="name"></param>
    private void DeleteStoredProcedure(SqlCommand command, string name)
    {
      command.CommandText =
        string.Format(@"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'P', N'PC')) DROP PROCEDURE [dbo].[{0}]", name);
      command.ExecuteNonQuery();
    }

    /// <summary>
    /// Удаляем функцию из БД
    /// </summary>
    /// <param name="command"></param>
    /// <param name="name"></param>
    private void DeleteFunction(SqlCommand command, string name)
    { 
      command.CommandText =
        string.Format(@"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT')) DROP FUNCTION [dbo].[{0}]", name);
      command.ExecuteNonQuery();
    }

    /// <summary>
    /// Удаляет CLR сборку из БД
    /// </summary>
    /// <param name="command"></param>
    /// <param name="name"></param>
    private void DeleteAssembly(SqlCommand command, string name)
    {
      command.CommandText =
        string.Format(@"IF EXISTS (SELECT * FROM sys.assemblies asms WHERE asms.name = N'{0}' and is_user_defined = 1) DROP ASSEMBLY [{0}]", name);
      command.ExecuteNonQuery();    
    }

    #endregion
  }
}