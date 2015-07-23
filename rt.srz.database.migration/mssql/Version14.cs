// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Version13.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.mssql
{
  using ECM7.Migrator.Framework;

  /// <summary>
  ///   The version 14.
  /// </summary>
  [Migration(14)]
  public class Version14 : Migration
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The apply.
    /// </summary>
    public override void Apply()
    {
      var tableKladr = new SchemaQualifiedObjectName { Schema = "dbo", Name = "Address" };
      if (Database.ColumnExists(tableKladr, "TypeAddress"))
      {
        Database.RemoveColumn(tableKladr, "TypeAddress");
      }
    }

    #endregion
  }
}