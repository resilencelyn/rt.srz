// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Version7.cs" company="РусБИТех">
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
  [Migration(7)]
  public class Version7 : Migration
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The apply.
    /// </summary>
    public override void Apply()
    {
      var table = new SchemaQualifiedObjectName { Schema = "dbo", Name = "SearchKey" };

      if (Database.ConstraintExists(table, "FK_SearchKey_Statement"))
      {
        Database.RemoveConstraint(table, "FK_SearchKey_Statement");
      }

      if (Database.ConstraintExists(table, "FK_SearchKey_InsuredPerson"))
      {
        Database.RemoveConstraint(table, "FK_SearchKey_InsuredPerson");
      }

      var table1 = new SchemaQualifiedObjectName { Schema = "dbo", Name = "Contents" };

      if (Database.ConstraintExists(table1, "FK_Contents_InsuredPersonData"))
      {
        Database.RemoveConstraint(table1, "FK_Contents_InsuredPersonData");
      }
    }

    #endregion
  }
}