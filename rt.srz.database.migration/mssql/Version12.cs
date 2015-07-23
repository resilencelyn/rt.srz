// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Version12.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.mssql
{
  using ECM7.Migrator.Framework;

  /// <summary>
  ///   The version 12.
  /// </summary>
  [Migration(12)]
  public class Version12 : Migration
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The apply.
    /// </summary>
    public override void Apply()
    {
      var tableKladr = new SchemaQualifiedObjectName { Schema = "dbo", Name = "KLADR" };
      if (Database.ColumnExists(tableKladr, "OCATD"))
      {
        Database.ExecuteNonQuery("EXEC sp_rename 'dbo.KLADR.OCATD', 'OKATO', 'COLUMN'");
      }
    }

    #endregion
  }
}