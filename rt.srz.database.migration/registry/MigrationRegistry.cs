// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MigrationRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The core registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.registry
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Data;
  using System.Data.SqlClient;
  using System.Globalization;
  using System.Linq;
  using System.Web;
  using System.Xml.Linq;

  using ECM7.Migrator;

  using NLog;

  using rt.core.model.configuration;

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
      var migrator = new Migrator(providerTypeName, connectionString, executingAssembly, int.MaxValue);

      var migrationVersion = ConfigManager.MigratorConfiguration.MigrationVersion;
      migrator.Migrate(migrationVersion);
      var currentClassLogger = LogManager.GetCurrentClassLogger();

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
        sqlCommand.CommandText = Assembly.AdjustDatabase(sqlCommand.Connection.Database);
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


        try
        {
          var oids = GetElementsNoInDatabase("oid.xml", "oid", connection);

          // добавляем элементы которых нету в базу, но есть в файле
          using (var cmd = connection.CreateCommand())
          {
            cmd.CommandType = CommandType.Text;
            foreach (var oid in oids)
            {
              var id = oid.Element("Id");
              if (id != null)
              {
                CreateParameter(cmd, "@id", id.Value);
              }

              var fullName = oid.Element("FullName");
              if (fullName != null)
              {
                CreateParameter(cmd, "@FullName", fullName.Value);
              }

              var shortName = oid.Element("ShortName");
              if (shortName != null)
              {
                CreateParameter(cmd, "@ShortName", shortName.Value);
              }

              var defaultId = oid.Element("DefaultId");
              if (defaultId != null)
              {
                CreateParameter(cmd, "@DefaultId", defaultId.Value);
              }

              var latinName = oid.Element("LatinName");
              if (latinName != null)
              {
                CreateParameter(cmd, "@LatinName", latinName.Value);
              }

              cmd.CommandText =
                @"insert into oid (Id, FullName, ShortName, DefaultId, LatinName) values (@id, @FullName, @ShortName, @DefaultId, @LatinName)";

              cmd.ExecuteNonQuery();
            }
          }

          // синхронизируем concept
          var concepts = GetElementsNoInDatabase("concept.xml", "concept", connection);

          TurnOnIdentity(true, connection);

          // добавляем элементы которых нету в базе, но есть в файле
          using (var cmd = connection.CreateCommand())
          {
            cmd.CommandType = CommandType.Text;
            foreach (var concept in concepts)
            {
              CreateParameter(cmd, "@id", concept.Element("Id").Value, DbType.Int32);
              CreateParameter(cmd, "@Oid", concept.Element("Oid").Value);
              CreateParameter(cmd, "@Code", concept.Element("Code").Value);
              CreateParameter(cmd, "@Name", concept.Element("Name").Value);
              CreateParameter(cmd, "@Description", concept.Element("Description").Value);
              CreateParameter(cmd, "@ShortName", concept.Element("ShortName").Value);
              CreateParameter(cmd, "@RelatedCode", concept.Element("RelatedCode").Value);
              CreateParameter(cmd, "@RelatedOid", concept.Element("RelatedOid").Value);
              CreateParameter(cmd, "@RelatedType", concept.Element("RelatedType").Value);
              object datefrom = DBNull.Value;
              if (!string.IsNullOrEmpty(concept.Element("DateFrom").Value))
              {
                datefrom = DateTime.ParseExact(
                                               concept.Element("DateFrom").Value,
                                               "yyyy-MM-dd",
                                               CultureInfo.InvariantCulture);
              }

              object dateto = DBNull.Value;
              if (!string.IsNullOrEmpty(concept.Element("DateTo").Value))
              {
                dateto = DateTime.ParseExact(
                                             concept.Element("DateTo").Value,
                                             "yyyy-MM-dd",
                                             CultureInfo.InvariantCulture);
              }

              CreateParameter(cmd, "@DateFrom", datefrom, DbType.Date);
              CreateParameter(cmd, "@DateTo", dateto, DbType.Date);
              CreateParameter(
                              cmd,
                              "@Relevance",
                              string.IsNullOrEmpty(concept.Element("Relevance").Value)
                                ? "0"
                                : concept.Element("Relevance").Value,
                              DbType.Int32);

              cmd.CommandText =
                @"insert into concept (Id, Oid, Code, Name, Description, ShortName, RelatedCode, RelatedOid, RelatedType, DateFrom, DateTo, Relevance) 
                values (@Id, @Oid, @Code, @Name, @Description, @ShortName, @RelatedCode, @RelatedOid, @RelatedType, @DateFrom, @DateTo, @Relevance)";

              cmd.ExecuteNonQuery();
            }
          }

          TurnOnIdentity(false, connection);

        }
        catch (Exception e)
        {
          LogManager.GetCurrentClassLogger().Error("Ошибка выполнения синхронизации таблиц Oid, Concept", e);
          throw;
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Удаляет CLR сборку из БД
    /// </summary>
    /// <param name="command">
    /// The command.
    /// </param>
    /// <param name="name">
    /// The name.
    /// </param>
    private void DeleteAssembly(SqlCommand command, string name)
    {
      command.CommandText =
        string.Format(
                      @"IF EXISTS (SELECT * FROM sys.assemblies asms WHERE asms.name = N'{0}' and is_user_defined = 1) DROP ASSEMBLY [{0}]",
                      name);
      command.ExecuteNonQuery();
    }

    /// <summary>
    /// Удаляем функцию из БД
    /// </summary>
    /// <param name="command">
    /// The command.
    /// </param>
    /// <param name="name">
    /// The name.
    /// </param>
    private void DeleteFunction(SqlCommand command, string name)
    {
      command.CommandText =
        string.Format(
                      @"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT')) DROP FUNCTION [dbo].[{0}]",
                      name);
      command.ExecuteNonQuery();
    }

    /// <summary>
    /// Удаляет хранимку из БД
    /// </summary>
    /// <param name="command">
    /// The command.
    /// </param>
    /// <param name="name">
    /// The name.
    /// </param>
    private void DeleteStoredProcedure(SqlCommand command, string name)
    {
      command.CommandText =
        string.Format(
                      @"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'P', N'PC')) DROP PROCEDURE [dbo].[{0}]",
                      name);
      command.ExecuteNonQuery();
    }


    /// <summary>
    /// The create parameter.
    /// </summary>
    /// <param name="cmd">
    /// The cmd.
    /// </param>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="type">
    /// The type.
    /// </param>
    private void CreateParameter(IDbCommand cmd, string name, object value, DbType type = DbType.String)
    {
      var param = cmd.CreateParameter();
      param.DbType = type;
      param.Value = value;
      if (value is string && string.IsNullOrEmpty(value.ToString()))
      {
        param.Value = DBNull.Value;
      }

      param.ParameterName = name;
      cmd.Parameters.Add(param);
    }

    /// <summary>
    /// The get elements no in database.
    /// </summary>
    /// <param name="fileName">
    ///   The file name.
    /// </param>
    /// <param name="tableName">
    ///   The table name.
    /// </param>
    /// <param name="connection"></param>
    /// <param name="idName">
    ///   The id name.
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable{T}"/>.
    /// </returns>
    private IEnumerable<XElement> GetElementsNoInDatabase(string fileName, string tableName, SqlConnection connection, string idName = "Id")
    {
      // предполагаем что имена полей в базе и имена записей xml совпадают

      // синхронизируем оиды
      var oidxml = XDocument.Load(HttpContext.Current.Server.MapPath(string.Format(@"bin\data\{0}", fileName)));

      // зачитываем из базы ид оидов
      var existsids = new List<string>();
      using (var cmd = connection.CreateCommand())
      {
        cmd.CommandText = string.Format("select {0} from {1}", idName, tableName);
        cmd.CommandType = CommandType.Text;
        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            existsids.Add(reader[idName].ToString());
          }
        }
      }

      // находим элементы которых нету в базе
      return from o in oidxml.Root.Elements("row") where !existsids.Contains(o.Element(idName).Value) select o;
    }

    /// <summary>
    /// The turn on identity.
    /// </summary>
    /// <param name="value">
    ///   The value.
    /// </param>
    /// <param name="connection"></param>
    private void TurnOnIdentity(bool value, SqlConnection connection)
    {
      using (var cmd = connection.CreateCommand())
      {
        cmd.CommandText = string.Format("set IDENTITY_INSERT concept {0}", value ? "ON" : "OFF");
        cmd.CommandType = CommandType.Text;
        cmd.ExecuteNonQuery();
      }
    }


    #endregion
  }
}