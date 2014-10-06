// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StoredProcedures.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The stored procedures.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

using Microsoft.SqlServer.Server;

using rt.srz.database.business;
using rt.srz.database.business.standard.helpers;

/// <summary>
///   The stored procedures.
/// </summary>
public class StoredProcedures
{
  #region Public Methods and Operators

  /// <summary>
  /// Расчитывает стандартные ключи для указанных параметров
  /// </summary>
  /// <param name="statementXml">
  /// The statement Xml.
  /// </param>
  /// <param name="insuredPersonDataXml">
  /// The insured Person Data Xml.
  /// </param>
  /// <param name="documentXml">
  /// The document Xml.
  /// </param>
  /// <param name="okato">
  /// The okato.
  /// </param>
  /// <returns>
  /// The <see cref="IEnumerable"/>.
  /// </returns>
  [SqlFunction(DataAccess = DataAccessKind.Read, FillRowMethodName = "FillStandardKeyHash", 
    TableDefinition = "KeyId uniqueidentifier, DocumentTypeId int, Hash varbinary(max), ErrorSqlString nvarchar(max)")]
  public static IEnumerable CalcStandardSearchKeys(
    SqlString statementXml, 
    SqlString insuredPersonDataXml, 
    SqlString documentXml, 
    SqlString okato)
  {
    IEnumerable result;
    try
    {
      // Расчет ключей
      var calculatedHashes = ObjectFactory.GetStandardPseudonymizationManager()
                                          .CalculateHashes(
                                                           statementXml.Value, 
                                                           insuredPersonDataXml.Value, 
                                                           documentXml.Value, 
                                                           okato.Value);

      result =
        calculatedHashes.Where(x => x.hash != null)
                        .Select(
                                x =>
                                new StandardKeyHashResult(
                                  new SqlGuid(KeyCode2KeyIdConverter.ConvertKeyCode2KeyId(x.key)), 
                                  new SqlInt32(KeyCode2KeyIdConverter.ConvertKeyCode2DocumentId(x.key, x.subtype)), 
                                  new SqlBinary(x.hash), 
                                  new SqlString(string.Empty)))
                        .ToList();
    }
    catch (Exception ex)
    {
      result = new List<StandardKeyHashResult>
               {
                 new StandardKeyHashResult(
                   new SqlGuid(Guid.Empty), 
                   new SqlInt32(0), 
                   new SqlBinary(new byte[0]), 
                   new SqlString(ex.ToString()))
               };
    }

    return result;
  }

  /// <summary>
  /// Расчитывает стандартные ключи для указанных параметров
  /// </summary>
  /// <param name="exchangeXml">
  /// The exchange Xml.
  /// </param>
  /// <param name="documentXml">
  /// The document Xml.
  /// </param>
  /// <param name="addressXml">
  /// The address Xml.
  /// </param>
  /// <returns>
  /// The <see cref="IEnumerable"/>.
  /// </returns>
  [SqlFunction(DataAccess = DataAccessKind.Read, FillRowMethodName = "FillStandardKeyHash", 
    TableDefinition = "KeyId uniqueidentifier, DocumentTypeId int, Hash varbinary(max), ErrorSqlString nvarchar(max)")]
  public static IEnumerable CalcStandardSearchKeysExchange(
    SqlString exchangeXml, 
    SqlString documentXml, 
    SqlString addressXml)
  {
    IEnumerable result;
    try
    {
      // Расчет ключей
      var calculatedHashes = ObjectFactory.GetStandardPseudonymizationManager()
                                          .CalculateHashes(exchangeXml.Value, documentXml.Value, addressXml.Value);

      result =
        calculatedHashes.Where(x => x.hash != null)
                        .Select(
                                x =>
                                new StandardKeyHashResult(
                                  new SqlGuid(KeyCode2KeyIdConverter.ConvertKeyCode2KeyId(x.key)), 
                                  new SqlInt32(KeyCode2KeyIdConverter.ConvertKeyCode2DocumentId(x.key, x.subtype)), 
                                  new SqlBinary(x.hash), 
                                  new SqlString(string.Empty)))
                        .ToList();
    }
    catch (Exception ex)
    {
      result = new List<StandardKeyHashResult>
               {
                 new StandardKeyHashResult(
                   new SqlGuid(Guid.Empty), 
                   new SqlInt32(0), 
                   new SqlBinary(new byte[0]), 
                   new SqlString(ex.ToString()))
               };
    }

    return result;
  }

  /// <summary>
  /// Расчитывает пользовательский ключ для указанных параметров
  /// </summary>
  /// <param name="searchKeyTypeXml">
  /// The search Key Type Xml.
  /// </param>
  /// <param name="insuredPersonDataXml">
  /// The insured Person Data Xml.
  /// </param>
  /// <param name="documentXml">
  /// The document Xml.
  /// </param>
  /// <param name="address1Xml">
  /// The address 1 Xml.
  /// </param>
  /// <param name="address2Xml">
  /// The address 2 Xml.
  /// </param>
  /// <param name="medicalInsuranceXml">
  /// The medical Insurance Xml.
  /// </param>
  /// <param name="okato">
  /// The okato.
  /// </param>
  /// <returns>
  /// The <see cref="IEnumerable"/>.
  /// </returns>
  [SqlFunction(DataAccess = DataAccessKind.Read, FillRowMethodName = "FillUserKeyHash", 
    TableDefinition = "Hash varbinary(max)")]
  public static IEnumerable CalcUserSearchKey(
    SqlString searchKeyTypeXml, 
    SqlString insuredPersonDataXml, 
    SqlString documentXml, 
    SqlString address1Xml, 
    SqlString address2Xml, 
    SqlString medicalInsuranceXml, 
    SqlString okato)
  {
    var result = new byte[0];
    try
    {
      // Расчет ключа
      result = ObjectFactory.GetPseudonymizationManager()
                            .CalculateUserSearchKey(
                                                    searchKeyTypeXml.Value, 
                                                    insuredPersonDataXml.Value, 
                                                    documentXml.Value, 
                                                    address1Xml.Value, 
                                                    address2Xml.Value, 
                                                    medicalInsuranceXml.Value, 
                                                    okato.Value);
    }
    catch (Exception)
    {
      // TODO: логирование
    }

    return new List<UserKeyHashResult> { new UserKeyHashResult(result) };
  }

  /// <summary>
  /// Расчитывает пользовательский ключ для указанных параметров
  /// </summary>
  /// <param name="searchKeyTypeXml">
  /// The search Key Type Xml.
  /// </param>
  /// <param name="exchangeXml">
  /// The exchange Xml.
  /// </param>
  /// <param name="documentXml">
  /// The document Xml.
  /// </param>
  /// <param name="addressXml">
  /// The address Xml.
  /// </param>
  /// <returns>
  /// The <see cref="IEnumerable"/>.
  /// </returns>
  [SqlFunction(DataAccess = DataAccessKind.Read, FillRowMethodName = "FillUserKeyHash", 
    TableDefinition = "Hash varbinary(max)")]
  public static IEnumerable CalcUserSearchKeyExchange(
    SqlString searchKeyTypeXml, 
    SqlString exchangeXml, 
    SqlString documentXml, 
    SqlString addressXml)
  {
    var result = new byte[0];
    try
    {
      // Расчет ключа
      result = ObjectFactory.GetPseudonymizationManager()
                            .CalculateUserSearchKeyExchange(
                                                            searchKeyTypeXml.Value, 
                                                            exchangeXml.Value, 
                                                            documentXml.Value, 
                                                            addressXml.Value);
    }
    catch (Exception e)
    {
      // TODO: логирование
    }

    return new List<UserKeyHashResult> { new UserKeyHashResult(result) };
  }

  /// <summary>
  /// Заполняет значение для табличной функции
  /// </summary>
  /// <param name="obj">
  /// The obj.
  /// </param>
  /// <param name="keyId">
  /// The key Id.
  /// </param>
  /// <param name="documentTypeId">
  /// The document Type Id.
  /// </param>
  /// <param name="hash">
  /// The hash.
  /// </param>
  /// <param name="errorSqlString">
  /// The error Sql String.
  /// </param>
  public static void FillStandardKeyHash(
    object obj, 
    out SqlGuid keyId, 
    out SqlInt32 documentTypeId, 
    out SqlBinary hash, 
    out SqlString errorSqlString)
  {
    var hashResult = (StandardKeyHashResult)obj;
    keyId = hashResult.KeyId;
    documentTypeId = hashResult.DocumentTypeId;
    hash = hashResult.Hash;
    errorSqlString = hashResult.ErrorSqlString;
  }

  /// <summary>
  /// Заполняет значение для табаличной функции
  /// </summary>
  /// <param name="obj">
  /// The obj.
  /// </param>
  /// <param name="hash">
  /// The hash.
  /// </param>
  public static void FillUserKeyHash(object obj, out SqlBinary hash)
  {
    var hashResult = (UserKeyHashResult)obj;
    hash = hashResult.Hash;
  }

  #endregion

  /// <summary>
  ///   Класс для табличной функции
  /// </summary>
  private class StandardKeyHashResult
  {
    #region Fields

    /// <summary>
    ///   The document type id.
    /// </summary>
    public readonly SqlInt32 DocumentTypeId;

    /// <summary>
    ///   The error sql string.
    /// </summary>
    public readonly SqlString ErrorSqlString;

    /// <summary>
    ///   The hash.
    /// </summary>
    public readonly SqlBinary Hash;

    /// <summary>
    ///   The key id.
    /// </summary>
    public readonly SqlGuid KeyId;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="StandardKeyHashResult"/> class.
    /// </summary>
    /// <param name="keyId">
    /// The key id.
    /// </param>
    /// <param name="documentTypeId">
    /// The document type id.
    /// </param>
    /// <param name="hash">
    /// The hash.
    /// </param>
    /// <param name="errorSqlString">
    /// The error Sql String.
    /// </param>
    public StandardKeyHashResult(SqlGuid keyId, SqlInt32 documentTypeId, SqlBinary hash, SqlString errorSqlString)
    {
      KeyId = keyId;
      DocumentTypeId = documentTypeId;
      Hash = hash;
      ErrorSqlString = errorSqlString;
    }

    #endregion
  }

  /// <summary>
  ///   Класс для табличной функции
  /// </summary>
  private class UserKeyHashResult
  {
    #region Fields

    /// <summary>
    ///   The hash.
    /// </summary>
    public readonly SqlBinary Hash;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="UserKeyHashResult"/> class.
    /// </summary>
    /// <param name="hash">
    /// The hash.
    /// </param>
    public UserKeyHashResult(SqlBinary hash)
    {
      Hash = hash;
    }

    #endregion
  }
}