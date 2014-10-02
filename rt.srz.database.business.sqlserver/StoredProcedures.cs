//------------------------------------------------------------------------------
// <copyright file="CSSqlStoredProcedure.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using System.Linq;
using Microsoft.SqlServer.Server;
using rt.srz.database.business;
using rt.srz.database.business.interfaces;
using rt.srz.database.business.interfaces.pseudonymization;
using rt.srz.database.business.model;
using rt.srz.database.business.standard;
using rt.srz.database.business.standard.helpers;

public partial class StoredProcedures
{
  /// <summary>
  /// Класс для табличной функции
  /// </summary>
  private class StandardKeyHashResult
  {
    public SqlGuid KeyId;
    public SqlInt32 DocumentTypeId;
    public SqlBinary Hash;
    
    public StandardKeyHashResult(SqlGuid keyId, SqlInt32 documentTypeId, SqlBinary hash)
    {
      KeyId = keyId;
      DocumentTypeId = documentTypeId;
      Hash = hash;
    }
  }

  /// <summary>
  /// Класс для табличной функции
  /// </summary>
  private class UserKeyHashResult
  {
    public SqlBinary Hash;

    public UserKeyHashResult(SqlBinary hash)
    {
      Hash = hash;
    }
  }

  /// <summary>
  /// Заполняет значение для табличной функции
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="keyId"></param>
  /// <param name="hash"></param>
  public static void FillStandardKeyHash(object obj, out SqlGuid keyId, out SqlInt32 documentTypeId, out SqlBinary hash)
  {
    StandardKeyHashResult hashResult = (StandardKeyHashResult)obj;
    keyId = hashResult.KeyId;
    documentTypeId = hashResult.DocumentTypeId;
    hash = hashResult.Hash;
  }

  /// <summary>
  /// Заполняет значение для табаличной функции
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="keyId"></param>
  /// <param name="hash"></param>
  public static void FillUserKeyHash(object obj, out SqlBinary hash)
  {
    UserKeyHashResult hashResult = (UserKeyHashResult)obj;
    hash = hashResult.Hash;
  }

  /// <summary>
  /// Расчитывает стандартные ключи для указанных параметров
  /// </summary>
  /// <param name="searchKeyTypeXml"></param>
  /// <param name="prfExchangeXml"></param>
  /// <param name="keyValue"></param>
  /// <param name="hash"></param>
  [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.Read, FillRowMethodName = "FillStandardKeyHash", TableDefinition = "KeyId uniqueidentifier, DocumentTypeId int, Hash varbinary(max)")]
  public static IEnumerable CalcStandardSearchKeys(SqlString statementXml, SqlString insuredPersonDataXml, SqlString documentXml, SqlString okato)
  {
    IEnumerable result = new StandardKeyHashResult[0];
    try
    {
       //Расчет ключей
      List<HashDataNew> calculatedHashes = ObjectFactory.GetStandardPseudonymizationManager().CalculateHashes(statementXml.Value, insuredPersonDataXml.Value, 
        documentXml.Value, okato.Value);

      result = calculatedHashes.Where(x => x.hash != null).Select(x => new StandardKeyHashResult(new SqlGuid(KeyCode2KeyIdConverter.ConvertKeyCode2KeyId(x.key)), 
        new SqlInt32(KeyCode2KeyIdConverter.ConvertKeyCode2DocumentId(x.key, x.subtype)), new SqlBinary(x.hash))).ToList();
    }
    catch (Exception e)
    {
      //TODO: логирование
    }
    
    return result;
  }

  /// <summary>
  /// Расчитывает пользовательский ключ для указанных параметров
  /// </summary>
  /// <param name="searchKeyTypeXml"></param>
  /// <param name="insuredPersonDataXml"></param>
  /// <param name="documentXml"></param>
  /// <param name="address1Xml"></param>
  /// <param name="address2Xml"></param>
  /// <param name="medicalInsuranceXml"></param>
  /// <param name="keyValue"></param>
  /// <param name="hash"></param>
  [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.Read, FillRowMethodName = "FillUserKeyHash", TableDefinition = "Hash varbinary(max)")]
  public static IEnumerable CalcUserSearchKey(SqlString searchKeyTypeXml, SqlString insuredPersonDataXml, SqlString documentXml,
                                     SqlString address1Xml, SqlString address2Xml, SqlString medicalInsuranceXml, SqlString okato)
  {
    byte[] result = new byte[0];
    try
    {
      //Расчет ключа
      result = ObjectFactory.GetPseudonymizationManager().CalculateUserSearchKey(searchKeyTypeXml.Value, insuredPersonDataXml.Value,
        documentXml.Value, address1Xml.Value, address2Xml.Value, medicalInsuranceXml.Value, okato.Value);
    }
    catch (Exception)
    {
      //TODO: логирование
    }

    return new List<UserKeyHashResult> { new UserKeyHashResult(result) };
  }

  /// <summary>
  /// Расчитывает стандартные ключи для указанных параметров
  /// </summary>
  /// <param name="searchKeyTypeXml"></param>
  /// <param name="prfExchangeXml"></param>
  /// <param name="keyValue"></param>
  /// <param name="hash"></param>
  [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.Read, FillRowMethodName = "FillStandardKeyHash", TableDefinition = "KeyId uniqueidentifier, DocumentTypeId int, Hash varbinary(max)")]
  public static IEnumerable CalcStandardSearchKeysExchange(SqlString exchangeXml, SqlString documentXml, SqlString addressXml)
  {
    IEnumerable result = new StandardKeyHashResult[0];
    try
    {
       //Расчет ключей
      List<HashDataNew> calculatedHashes = ObjectFactory.GetStandardPseudonymizationManager().CalculateHashes(exchangeXml.Value, documentXml.Value, addressXml.Value);

      result = calculatedHashes.Where(x => x.hash != null).Select(x => new StandardKeyHashResult(new SqlGuid(KeyCode2KeyIdConverter.ConvertKeyCode2KeyId(x.key)), new SqlInt32(KeyCode2KeyIdConverter.ConvertKeyCode2DocumentId(x.key, x.subtype)),
        new SqlBinary(x.hash))).ToList();
    }
    catch (Exception e)
    {
      //TODO: логирование
    }
    
    return result;
  }
  
  /// <summary>
  /// Расчитывает пользовательский ключ для указанных параметров
  /// </summary>
  /// <param name="searchKeyTypeXml"></param>
  /// <param name="prfExchangeXml"></param>
  /// <param name="keyValue"></param>
  /// <param name="hash"></param>
  [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.Read, FillRowMethodName = "FillUserKeyHash", TableDefinition = "Hash varbinary(max)")]
  public static IEnumerable CalcUserSearchKeyExchange(SqlString searchKeyTypeXml, SqlString exchangeXml, SqlString documentXml, SqlString addressXml)
  {
    byte[] result = new byte[0];
    try
    {
      //Расчет ключа
      result = ObjectFactory.GetPseudonymizationManager().CalculateUserSearchKeyExchange(searchKeyTypeXml.Value, exchangeXml.Value, documentXml.Value, addressXml.Value);
    }
    catch(Exception e)
    {
      //TODO: логирование
    }

    return new List<UserKeyHashResult> { new UserKeyHashResult(result) };
  }
}
