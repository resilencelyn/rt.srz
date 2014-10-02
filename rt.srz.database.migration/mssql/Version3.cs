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
  [Migration(3)]
  public class Version3 : Migration
  {
    /// <summary>
    /// The apply.
    /// </summary>
    public override void Apply()
    {
      

      var tableMedIns = new SchemaQualifiedObjectName
      {
        Schema = "dbo",
        Name = "MedicalInsurance"
      };

      var tableInsuredPerson = new SchemaQualifiedObjectName
      {
        Schema = "dbo",
        Name = "InsuredPerson"
      };


      if (!Database.ColumnExists(tableMedIns, "StateDateFrom"))
      {
        var col = new Column("StateDateFrom", new ColumnType(DbType.DateTime), ColumnProperty.NotNull, "Getdate()");
        Database.AddColumn(tableMedIns, col);
      }

      if (!Database.ColumnExists(tableMedIns, "StateDateTo"))
      {
        var col = new Column("StateDateTo", new ColumnType(DbType.DateTime), ColumnProperty.NotNull, "Getdate()");
        Database.AddColumn(tableMedIns, col);
      }

      if (!Database.ColumnExists(tableMedIns, "InsuredPersonId"))
      {
        var col = new Column("InsuredPersonId", new ColumnType(DbType.Guid), ColumnProperty.NotNull, "'00000000-0000-0000-0000-000000000000'");
        Database.AddColumn(tableMedIns, col);
        ////Database.AddForeignKey("FK_MedicalInsurance_InsuredPerson", tableMedIns,"InsuredPersonId" , tableInsuredPerson, "RowId");
      }
    }
  }
}
