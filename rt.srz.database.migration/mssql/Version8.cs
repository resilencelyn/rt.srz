// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Version2.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The kladr migrator v 2.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.mssql
{
  using System.Data;

  using ECM7.Migrator.Framework;

  /// <summary>
  /// The kladr migrator v 2.
  /// </summary>
  [Migration(8)]
  public class Version8 : Migration
  {
    /// <summary>
    /// The apply.
    /// </summary>
    public override void Apply()
    {
      var table = new SchemaQualifiedObjectName
      {
        Schema = "dbo",
        Name = "Errors"
      };

      if (!Database.ColumnExists(table, "Repl"))
      {
        Database.AddColumn(table, new Column("Repl", new ColumnType(DbType.AnsiString, 250), ColumnProperty.Null));
      }

      table = new SchemaQualifiedObjectName
      {
        Schema = "dbo",
        Name = "InsuredPersonData"
      };

      if (!Database.ColumnExists(table, "NhMiddleName"))
      {
        Database.AddColumn(table, new Column("NhMiddleName", new ColumnType(DbType.Int32), ColumnProperty.NotNull, 0));
      }

      if (!Database.ColumnExists(table, "NhFirstName"))
      {
        Database.AddColumn(table, new Column("NhFirstName", new ColumnType(DbType.Int32), ColumnProperty.NotNull, 0));
      }

      if (!Database.ColumnExists(table, "NhLastName"))
      {
        Database.AddColumn(table, new Column("NhLastName", new ColumnType(DbType.Int32), ColumnProperty.NotNull, 0));
      }

      table = new SchemaQualifiedObjectName
      {
        Schema = "dbo",
        Name = "RangeNumber"
      };

      if (!Database.ColumnExists(table, "ParentId"))
      {
        Database.AddColumn(table, new Column("ParentId", new ColumnType(DbType.Guid), ColumnProperty.Null));
        Database.AddForeignKey("FK_RangeNumber_RangeNumber", table, "ParentId", table, "RowId");
      }

      if (!Database.ColumnExists(table, "TemplateId"))
      {
        Database.AddColumn(table, new Column("TemplateId", new ColumnType(DbType.Guid), ColumnProperty.Null));
        var tablefk = new SchemaQualifiedObjectName
        {
          Schema = "dbo",
          Name = "Template"
        };

        Database.AddForeignKey("FK_RangeNumber_Template", table, "TemplateId", tablefk, "RowId");
      }
    }
  }
}
