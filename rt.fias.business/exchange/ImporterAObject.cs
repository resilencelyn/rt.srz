// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterAObject.cs" company="РусБИТех">
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
  /// The importer a object.
  /// </summary>
  public class ImporterAObject : ImporterFias
  {
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
      if (file.Name.Contains("AS_ADDROBJ"))
      {
        return true;
      }

      return false;
    }

    /// <summary>
    /// The execute.
    /// </summary>
    /// <param name="objBl">
    /// The obj bl.
    /// </param>
    /// <param name="fileName">
    /// The file Name.
    /// </param>
    protected override void Execute(SQLXMLBulkLoad4Class objBl, string fileName)
    {
      var xsd = string.Format("{0}Exchange\\xsd\\AS_ADDROBJ.xsd", AppDomain.CurrentDomain.BaseDirectory);
      objBl.Execute(xsd, fileName);
    }

    /// <summary>
    /// The delete data.
    /// </summary>
    protected override void DeleteData()
    {
      var session = GetSession();

       session.CreateSQLQuery(@"
        truncate table dbo.AObject")
                                .SetTimeout(int.MaxValue)
                                .ExecuteUpdate();

      CloseSession(session);
    }
  }
}