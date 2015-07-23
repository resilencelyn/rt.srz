// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterHouseInterval.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.fias.business.exchange
{
  using System;
  using System.IO;
  using System.Reflection;

  using SQLXMLBULKLOADLib;

  /// <summary>
  ///   The importer house.
  /// </summary>
  public class ImporterHouseInterval : ImporterFias
  {
    #region Public Methods and Operators

    /// <summary>
    /// The applies to.
    /// </summary>
    /// <param name="file">
    /// The file.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool AppliesTo(FileInfo file)
    {
      if (file.Name.Contains("AS_HOUSEINT_"))
      {
        return true;
      }

      return false;
    }

    #endregion

    #region Methods

    /// <summary>
    ///   The delete data.
    /// </summary>
    protected override void DeleteData()
    {
      var session = GetSession();

      session.CreateSQLQuery(@"
        truncate table dbo.HouseInterval").SetTimeout(int.MaxValue).ExecuteUpdate();

      CloseSession(session);
    }

    /// <summary>
    /// The execute.
    /// </summary>
    /// <param name="objBl">
    /// The obj bl.
    /// </param>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    protected override void Execute(SQLXMLBulkLoad4Class objBl, string fileName)
    {
      var xsd = string.Format("{0}Exchange\\xsd\\AS_HOUSEINT.xsd", AppDomain.CurrentDomain.BaseDirectory);
      objBl.Execute(xsd, fileName);
    }

    #endregion
  }
}