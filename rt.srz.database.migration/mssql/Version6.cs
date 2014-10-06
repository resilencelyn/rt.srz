// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Version6.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
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
  ///   The kladr migrator v 2.
  /// </summary>
  [Migration(6)]
  public class Version6 : Migration
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The apply.
    /// </summary>
    public override void Apply()
    {
      var table = new SchemaQualifiedObjectName { Schema = "dbo", Name = "MedicalInsurance" };
      var columnType = new ColumnType(DbType.Guid);

      Database.ChangeColumn(table, "InsuredPersonId", columnType, false);
    }

    #endregion
  }
}