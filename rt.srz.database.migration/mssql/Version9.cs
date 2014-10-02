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
  using System;
  using System.Data;
  using ECM7.Migrator.Framework;

  /// <summary>
  /// The kladr migrator v 2.
  /// </summary>
  [Migration(9)]
  public class Version9 : Migration
  {
    /// <summary>
    /// The apply.
    /// </summary>
    public override void Apply()
    {
      var table = new SchemaQualifiedObjectName
      {
        Schema = "dbo",
        Name = "Workstation"
      };

      if (!Database.ColumnExists(table, "SmardCardTokenReaderName"))
      {
        Database.AddColumn(table, new Column("SmardCardTokenReaderName", new ColumnType(DbType.AnsiString, 100), ColumnProperty.Null));
      }
    }
  }
}
