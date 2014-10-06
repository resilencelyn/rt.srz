// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbTypesConverter.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The db types converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.dotNetX
{
  #region references

  using System;
  using System.Collections;
  using System.Data.Common;
  using System.Data.Linq;
  using System.Text;

  #endregion

  /// <summary>
  ///   The db types converter.
  /// </summary>
  public static class DbTypesConverter
  {
    #region Enums

    /// <summary>
    ///   The t db providers.
    /// </summary>
    public enum TDbProviders
    {
      /// <summary>
      ///   The unknown.
      /// </summary>
      Unknown, 

      /// <summary>
      ///   The mssql.
      /// </summary>
      MSSQL, 

      /// <summary>
      ///   The oracle.
      /// </summary>
      Oracle
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The convert_ byte array to database image.
    /// </summary>
    /// <param name="ByteArray">
    /// The byte array.
    /// </param>
    /// <returns>
    /// The <see cref="Binary"/>.
    /// </returns>
    public static Binary Convert_ByteArrayToDatabaseImage(byte[] ByteArray)
    {
      if ((ByteArray != null) && (ByteArray.Length > 0))
      {
        return new Binary(ByteArray);
      }

      return null;
    }

    /// <summary>
    /// The convert_ database image to byte array.
    /// </summary>
    /// <param name="Image">
    /// The image.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    public static byte[] Convert_DatabaseImageToByteArray(Binary Image)
    {
      if (Image != null)
      {
        var buffer = Image.ToArray();
        if ((buffer != null) && (buffer.Length > 0))
        {
          return buffer;
        }
      }

      return null;
    }

    /// <summary>
    /// The get array for sql.
    /// </summary>
    /// <param name="Array">
    /// The array.
    /// </param>
    /// <typeparam name="Type">
    /// </typeparam>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetArrayForSql<Type>(Type Array) where Type : IEnumerable
    {
      if (Array != null)
      {
        var builder = new StringBuilder(string.Empty);
        foreach (var obj2 in Array)
        {
          if (builder.Length > 0)
          {
            builder.AppendFormat(",", new object[0]);
          }

          builder.AppendFormat((obj2 != null) ? obj2.ToString() : "NULL", new object[0]);
        }

        if (builder.Length > 0)
        {
          return builder.ToString();
        }
      }

      return "NULL";
    }

    /// <summary>
    /// The get binary from db object.
    /// </summary>
    /// <param name="DbObject">
    /// The db object.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    public static byte[] GetBinaryFromDbObject(object DbObject)
    {
      return Convert.IsDBNull(DbObject) ? null : ((byte[])DbObject);
    }

    /// <summary>
    /// The get binary from db reader.
    /// </summary>
    /// <param name="Reader">
    /// The reader.
    /// </param>
    /// <param name="ParamIndex">
    /// The param index.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    public static byte[] GetBinaryFromDbReader(DbDataReader Reader, int ParamIndex)
    {
      return GetBinaryFromDbObject(Reader.GetValue(ParamIndex));
    }

    /// <summary>
    /// The get db param text for sql assignment.
    /// </summary>
    /// <param name="DbParamValue">
    /// The db param value.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetDbParamTextForSqlAssignment(object DbParamValue)
    {
      return GetDbParamTextForSqlAssignment(DbParamValue, TDbProviders.Unknown);
    }

    /// <summary>
    /// The get db param text for sql assignment.
    /// </summary>
    /// <param name="DbParamValue">
    /// The db param value.
    /// </param>
    /// <param name="StringFormat">
    /// The string format.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetDbParamTextForSqlAssignment(object DbParamValue, TDbProviders StringFormat)
    {
      string dbString;
      if (DbParamValue == null)
      {
        dbString = "NULL";
      }
      else
      {
        dbString = DbParamValue.ToString();
        if (DbParamValue is string)
        {
          dbString = GetDbString(dbString, StringFormat);
        }
      }

      return "=" + dbString;
    }

    /// <summary>
    /// The get db param text for sql condition.
    /// </summary>
    /// <param name="DbParamValue">
    /// The db param value.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetDbParamTextForSqlCondition(object DbParamValue)
    {
      return GetDbParamTextForSqlCondition(DbParamValue, TDbProviders.Unknown);
    }

    /// <summary>
    /// The get db param text for sql condition.
    /// </summary>
    /// <param name="DbParamValue">
    /// The db param value.
    /// </param>
    /// <param name="StringFormat">
    /// The string format.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetDbParamTextForSqlCondition(object DbParamValue, TDbProviders StringFormat)
    {
      if (DbParamValue == null)
      {
        return "IS NULL";
      }

      var dbString = DbParamValue.ToString();
      if (DbParamValue is string)
      {
        dbString = GetDbString(dbString, StringFormat);
      }

      return "=" + dbString;
    }

    /// <summary>
    /// The get db string.
    /// </summary>
    /// <param name="str">
    /// The string.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetDbString(string str)
    {
      return GetDbString(str, TDbProviders.Unknown);
    }

    /// <summary>
    /// The get db string.
    /// </summary>
    /// <param name="str">
    /// The string.
    /// </param>
    /// <param name="StringFormat">
    /// The string format.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetDbString(string str, TDbProviders StringFormat)
    {
      if (str == null)
      {
        return "NULL";
      }

      str = str.Replace("'", "''");
      if (StringFormat == TDbProviders.MSSQL)
      {
        return "N'" + str + "'";
      }

      return "'" + str + "'";
    }

    /// <summary>
    /// The get int 16 from db object.
    /// </summary>
    /// <param name="DbObject">
    /// The db object.
    /// </param>
    /// <returns>
    /// The <see cref="short?"/>.
    /// </returns>
    public static short? GetInt16FromDbObject(object DbObject)
    {
      return Convert.IsDBNull(DbObject) ? null : ((short?)DbObject);
    }

    /// <summary>
    /// The get int 16 from db reader.
    /// </summary>
    /// <param name="Reader">
    /// The reader.
    /// </param>
    /// <param name="ParamIndex">
    /// The param index.
    /// </param>
    /// <returns>
    /// The <see cref="short?"/>.
    /// </returns>
    public static short? GetInt16FromDbReader(DbDataReader Reader, int ParamIndex)
    {
      if (!Reader.IsDBNull(ParamIndex))
      {
        return Reader.GetInt16(ParamIndex);
      }

      return null;
    }

    /// <summary>
    /// The get int 32 from db object.
    /// </summary>
    /// <param name="DbObject">
    /// The db object.
    /// </param>
    /// <returns>
    /// The <see cref="int?"/>.
    /// </returns>
    public static int? GetInt32FromDbObject(object DbObject)
    {
      return Convert.IsDBNull(DbObject) ? null : ((int?)DbObject);
    }

    /// <summary>
    /// The get int 32 from db reader.
    /// </summary>
    /// <param name="Reader">
    /// The reader.
    /// </param>
    /// <param name="ParamIndex">
    /// The param index.
    /// </param>
    /// <returns>
    /// The <see cref="int?"/>.
    /// </returns>
    public static int? GetInt32FromDbReader(DbDataReader Reader, int ParamIndex)
    {
      if (!Reader.IsDBNull(ParamIndex))
      {
        return Reader.GetInt32(ParamIndex);
      }

      return null;
    }

    /// <summary>
    /// The get int 64 from db object.
    /// </summary>
    /// <param name="DbObject">
    /// The db object.
    /// </param>
    /// <returns>
    /// The <see cref="long?"/>.
    /// </returns>
    public static long? GetInt64FromDbObject(object DbObject)
    {
      return Convert.IsDBNull(DbObject) ? null : ((long?)DbObject);
    }

    /// <summary>
    /// The get int 64 from db reader.
    /// </summary>
    /// <param name="Reader">
    /// The reader.
    /// </param>
    /// <param name="ParamIndex">
    /// The param index.
    /// </param>
    /// <returns>
    /// The <see cref="long?"/>.
    /// </returns>
    public static long? GetInt64FromDbReader(DbDataReader Reader, int ParamIndex)
    {
      if (!Reader.IsDBNull(ParamIndex))
      {
        return Reader.GetInt64(ParamIndex);
      }

      return null;
    }

    /// <summary>
    /// The get string from db object.
    /// </summary>
    /// <param name="DbObject">
    /// The db object.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetStringFromDbObject(object DbObject)
    {
      return Convert.IsDBNull(DbObject) ? null : ((string)DbObject);
    }

    /// <summary>
    /// The get string from db reader.
    /// </summary>
    /// <param name="Reader">
    /// The reader.
    /// </param>
    /// <param name="ParamIndex">
    /// The param index.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetStringFromDbReader(DbDataReader Reader, int ParamIndex)
    {
      if (!Reader.IsDBNull(ParamIndex))
      {
        return Reader.GetString(ParamIndex);
      }

      return null;
    }

    /// <summary>
    /// The get string from sql.
    /// </summary>
    /// <param name="SqlString">
    /// The sql string.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetStringFromSql(object SqlString)
    {
      if ((SqlString != null) && (SqlString != DBNull.Value))
      {
        return (string)SqlString;
      }

      return null;
    }

    #endregion
  }
}