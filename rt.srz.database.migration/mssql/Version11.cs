// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Version11.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the Version11 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.mssql
{
  using System.Data;

  using ECM7.Migrator.Framework;

  /// <summary>
  /// The version 11.
  /// </summary>
  [Migration(11)]
  public class Version11 : Migration
  {
    /// <summary>
    /// The apply.
    /// </summary>
    public override void Apply()
    {
      var tableKladr = new SchemaQualifiedObjectName { Schema = "dbo", Name = "KLADR" };

      if (!Database.ColumnExists(tableKladr, "TypeAddress"))
      {
        Database.AddColumn(
                           tableKladr,
                           new Column("TypeAddress", new ColumnType(DbType.Int16), ColumnProperty.NotNull, 1));
      }

      if (!Database.ConstraintExists(tableKladr, "IX_KLADR"))
      {
        Database.AddUniqueConstraint("IX_KLADR", tableKladr, "Id", "TypeAddress");
      }

      var tableAddress = new SchemaQualifiedObjectName { Schema = "dbo", Name = "Address" };
      if (!Database.ColumnExists(tableAddress, "TypeAddress"))
      {
        Database.AddColumn(tableAddress, new Column("TypeAddress", new ColumnType(DbType.Int16), ColumnProperty.Null));
        Database.ExecuteNonQuery("update dbo.Address set TypeAddress = 1");
      }

      if (Database.ConstraintExists(tableAddress, "FK_Address_KLADR"))
      {
        Database.RemoveConstraint(tableAddress, "FK_Address_KLADR");
        Database.AddForeignKey(
                               "FK_Address_KLADR_2",
                               tableAddress,
                               new[] { "KladrId", "TypeAddress" },
                               tableKladr,
                               new[] { "Id", "TypeAddress" });

        Database.ExecuteNonQuery("EXEC sp_rename 'dbo.Address.KladrId', 'RegulatoryId', 'COLUMN'");
      }
    }
  }
}
