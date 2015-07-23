// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Version15.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.mssql
{
  using System.Data;

  using ECM7.Migrator.Framework;

  /// <summary>
  ///   The version 14.
  /// </summary>
  [Migration(16)]
  public class Version16 : Migration
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The apply.
    /// </summary>
    public override void Apply()
    {
      var table = new SchemaQualifiedObjectName { Schema = "dbo", Name = "Address" };
      if (!Database.ColumnExists(table, "OkatoRn"))
      {
        Database.AddColumn(table, new Column("OkatoRn", new ColumnType(DbType.String, 20), ColumnProperty.Null));
      }

      if (!Database.ColumnExists(table, "Code"))
      {
        Database.AddColumn(table, new Column("Code", new ColumnType(DbType.String, 20), ColumnProperty.Null));
      }
    }

    #endregion
  }
}