﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Version2.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The kladr migrator v 2.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.mssql
{
  using ECM7.Migrator.Framework;

  /// <summary>
  /// The kladr migrator v 2.
  /// </summary>
  [Migration(2)]
  public class Version2 : Migration
  {
    /// <summary>
    /// The apply.
    /// </summary>
    public override void Apply()
    {
      var table = new SchemaQualifiedObjectName
      {
        Schema = "dbo",
        Name = "Kladr"
      };

      if (Database.ColumnExists(table, "LEVEL1"))
      {
        Database.RemoveColumn(table, "LEVEL1");
      }

      if (Database.ColumnExists(table, "KLADR_PARENT_ID1"))
      {
        Database.RemoveColumn(table, "KLADR_PARENT_ID1");
      }
    }
  }
}