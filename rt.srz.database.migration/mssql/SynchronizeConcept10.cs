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
  using ECM7.Migrator.Framework;
  using System.Collections.Generic;
  using System.Xml.Linq;
  using System.Linq;
  using System.Web;
  using System.Data;
  using System;
  using System.Globalization;

  /// <summary>
  /// Синхронизация таблицы Concept
  /// </summary>
  [Migration(10)]
  public class SynchronizeConcept : Migration
  {
    private void CreateParameter(IDbCommand cmd, string name, object value, DbType type = DbType.String)
    {
      var param = cmd.CreateParameter();
      param.DbType = type;
      param.Value = value;
      if (value is string && string.IsNullOrEmpty(value.ToString()))
      {
        param.Value = DBNull.Value;
      }
      param.ParameterName = name;
      cmd.Parameters.Add(param);
    }

    private IEnumerable<XElement> GetElementsNoInDatabase(string fileName, string tableName, string idName = "Id")
    {
      //предполагаем что имена полей в базе и имена записей xml совпадают

      //синхронизируем оиды
      XDocument oidxml = XDocument.Load(HttpContext.Current.Server.MapPath(string.Format(@"bin\data\{0}", fileName)));

      //зачитываем из базы ид оидов
      List<string> existsids = new List<string>();
      var connection = Database.Connection;
      using (var cmd = connection.CreateCommand())
      {
        cmd.CommandText = string.Format("select {0} from {1}", idName, tableName);
        cmd.CommandType = System.Data.CommandType.Text;
        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            existsids.Add(reader[idName].ToString());
          }
        }
      }

      //находим элементы которых нету в базе
      return from o in oidxml.Root.Elements("row")
             where !existsids.Contains(o.Element(idName).Value)
             select o;
    }

    private void TurnOnIdentity(bool value)
    {
      using (var cmd = Database.Connection.CreateCommand())
      {
        cmd.CommandText = string.Format("set IDENTITY_INSERT concept {0}", value ? "ON" : "OFF");
        cmd.CommandType = CommandType.Text;
        cmd.ExecuteNonQuery();
      }
    }

    /// <summary>
    /// The apply.
    /// </summary>
    public override void Apply()
    {
      try
      {
      #region oid synchronize

      IEnumerable<XElement> oids = GetElementsNoInDatabase("oid.xml", "oid");

      //добавляем элементы которых нету в базу, но есть в файле
      using (var cmd = Database.Connection.CreateCommand())
      {
        cmd.CommandType = System.Data.CommandType.Text;
        foreach (var oid in oids)
        {
          CreateParameter(cmd, "@id", oid.Element("Id").Value);
          CreateParameter(cmd, "@FullName", oid.Element("FullName").Value);
          CreateParameter(cmd, "@ShortName", oid.Element("ShortName").Value);
          CreateParameter(cmd, "@DefaultId", oid.Element("DefaultId").Value);
          CreateParameter(cmd, "@LatinName", oid.Element("LatinName").Value);

          cmd.CommandText = @"insert into oid (Id, FullName, ShortName, DefaultId, LatinName) values (@id, @FullName, @ShortName, @DefaultId, @LatinName)";

          cmd.ExecuteNonQuery();
        }
      }

      #endregion


      #region concept synchronize

      //синхронизируем concept
      IEnumerable<XElement> concepts = GetElementsNoInDatabase("concept.xml", "concept");

      TurnOnIdentity(true);

      //добавляем элементы которых нету в базе, но есть в файле
      using (var cmd = Database.Connection.CreateCommand())
      {
        cmd.CommandType = System.Data.CommandType.Text;
        foreach (var concept in concepts)
        {
          CreateParameter(cmd, "@id", concept.Element("Id").Value, DbType.Int32);
          CreateParameter(cmd, "@Oid", concept.Element("Oid").Value);
          CreateParameter(cmd, "@Code", concept.Element("Code").Value);
          CreateParameter(cmd, "@Name", concept.Element("Name").Value);
          CreateParameter(cmd, "@Description", concept.Element("Description").Value);
          CreateParameter(cmd, "@ShortName", concept.Element("ShortName").Value);
          CreateParameter(cmd, "@RelatedCode", concept.Element("RelatedCode").Value);
          CreateParameter(cmd, "@RelatedOid", concept.Element("RelatedOid").Value);
          CreateParameter(cmd, "@RelatedType", concept.Element("RelatedType").Value);
          object datefrom = DBNull.Value;
          if (!string.IsNullOrEmpty(concept.Element("DateFrom").Value))
          {
            datefrom = DateTime.ParseExact(concept.Element("DateFrom").Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
          }
          object dateto = DBNull.Value;
          if (!string.IsNullOrEmpty(concept.Element("DateTo").Value))
          {
            dateto = DateTime.ParseExact(concept.Element("DateTo").Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
          }
          CreateParameter(cmd, "@DateFrom", datefrom, DbType.Date);
          CreateParameter(cmd, "@DateTo", dateto, DbType.Date);
          CreateParameter(cmd, "@Relevance", string.IsNullOrEmpty(concept.Element("Relevance").Value) ? "0" : concept.Element("Relevance").Value, DbType.Int32);

          cmd.CommandText = @"insert into concept (Id, Oid, Code, Name, Description, ShortName, RelatedCode, RelatedOid, RelatedType, DateFrom, DateTo, Relevance) 
          values (@Id, @Oid, @Code, @Name, @Description, @ShortName, @RelatedCode, @RelatedOid, @RelatedType, @DateFrom, @DateTo, @Relevance)";

          cmd.ExecuteNonQuery();
        }
      }

      TurnOnIdentity(false);

      #endregion
      }
      catch(Exception e)
      {
        NLog.LogManager.GetCurrentClassLogger().ErrorException("Ошибка выполнения синхронизации таблиц Oid, Concept", e);
        throw;
      }
    }
  }
}
