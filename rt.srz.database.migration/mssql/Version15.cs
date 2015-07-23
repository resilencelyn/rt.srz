// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Version15.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.mssql
{
  using ECM7.Migrator.Framework;

  /// <summary>
  ///   The version 14.
  /// </summary>
  [Migration(15)]
  public class Version15 : Migration
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The apply.
    /// </summary>
    public override void Apply()
    {
      var table = new SchemaQualifiedObjectName { Schema = "dbo", Name = "KLADR" };
      if (Database.TableExists(table))
      {
        Database.RemoveTable(table);
      }

      table = new SchemaQualifiedObjectName { Schema = "dbo", Name = "JobLockObject" };
      if (Database.TableExists(table))
      {
        Database.RemoveTable(table);
      }

      table = new SchemaQualifiedObjectName { Schema = "dbo", Name = "JobPool" };
      if (Database.TableExists(table))
      {
        Database.RemoveTable(table);
      }
    }

    #endregion
  }
}