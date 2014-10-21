// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Version13.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.mssql
{
  using ECM7.Migrator.Framework;

  /// <summary>
  ///   The version 12.
  /// </summary>
  [Migration(13)]
  public class Version13 : Migration
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The apply.
    /// </summary>
    public override void Apply()
    {
      var tableKladr = new SchemaQualifiedObjectName { Schema = "dbo", Name = "Address" };
      if (Database.ConstraintExists(tableKladr, "FK_Address_KLADR_2"))
      {
        Database.RemoveConstraint(tableKladr, "FK_Address_KLADR_2");
      }
    }

    #endregion
  }
}