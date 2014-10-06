// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Version5.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The kladr migrator v 2.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.mssql
{
  using ECM7.Migrator.Framework;

  /// <summary>
  ///   The kladr migrator v 2.
  /// </summary>
  [Migration(5)]
  public class Version5 : Migration
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The apply.
    /// </summary>
    public override void Apply()
    {
      var tableMedIns = new SchemaQualifiedObjectName { Schema = "dbo", Name = "MedicalInsurance" };

      var tablePeriodInsurance = new SchemaQualifiedObjectName { Schema = "dbo", Name = "PeriodInsurance" };

      if (!Database.ConstraintExists(tableMedIns, "IX_MedicalInsurance"))
      {
        Database.AddIndex("IX_MedicalInsurance", false, tableMedIns, "InsuredPersonId");
      }

      if (Database.TableExists(tablePeriodInsurance))
      {
        Database.RemoveTable(tablePeriodInsurance);
      }
    }

    #endregion
  }
}