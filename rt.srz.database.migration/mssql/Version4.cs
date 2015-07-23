// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Version4.cs" company="Альянс">
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
  [Migration(4)]
  public class Version4 : Migration
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The apply.
    /// </summary>
    public override void Apply()
    {
      var tableMedIns = new SchemaQualifiedObjectName { Schema = "dbo", Name = "MedicalInsurance" };

      var tableInsuredPerson = new SchemaQualifiedObjectName { Schema = "dbo", Name = "InsuredPerson" };

      var tablePeriodIns = new SchemaQualifiedObjectName { Schema = "dbo", Name = "PeriodInsurance" };

      if (Database.ColumnExists(tableMedIns, "InsuredPersonId") && Database.TableExists(tablePeriodIns))
      {
        Database.ExecuteNonQuery(@"update MedicalInsurance
set StateDateFrom = t.StateDateFrom,
    StateDateTo = t.StateDateTo,
    InsuredPersonId = t.InsuredPersonId
from PeriodInsurance t
where MedicalInsurance.RowId = t.MedicalInsuranceId");
        Database.AddForeignKey(
                               "FK_MedicalInsurance_InsuredPerson", 
                               tableMedIns, 
                               "InsuredPersonId", 
                               tableInsuredPerson, 
                               "RowId");
      }
    }

    #endregion
  }
}