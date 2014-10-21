// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Function.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The function.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.registry
{
  /// <summary>
  ///   The function.
  /// </summary>
  internal static class Function
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The calc standard search keys.
    /// </summary>
    /// <returns>
    ///   The <see cref="string" />.
    /// </returns>
    public static string CalcStandardSearchKeys()
    {
      return @"CREATE FUNCTION [dbo].[CalcStandardSearchKeys]
            (@statementXml NVARCHAR (4000), @insuredPersonDataXml NVARCHAR (4000), @documentXml NVARCHAR (4000), @okato NVARCHAR (4000))
              RETURNS 
                TABLE (
                  [KeyId] UNIQUEIDENTIFIER NULL,
                  [DocumentTypeId] INT NULL,
                  [Hash]  VARBINARY (MAX)  NULL,
                  [ErrorSqlString] nvarchar(max))
            AS
            EXTERNAL NAME [rt.srz.database.business.sqlserver].[StoredProcedures].[CalcStandardSearchKeys]";
    }

    /// <summary>
    ///   The calc standard search keys exchange.
    /// </summary>
    /// <returns>
    ///   The <see cref="string" />.
    /// </returns>
    public static string CalcStandardSearchKeysExchange()
    {
      return @"CREATE FUNCTION [dbo].[CalcStandardSearchKeysExchange]
             (@exchangeXml NVARCHAR (4000), @documentXml NVARCHAR (4000), @addressXml NVARCHAR (4000))
               RETURNS 
                TABLE (
                  [KeyId] UNIQUEIDENTIFIER NULL,
                  [DocumentTypeId] INT NULL,
                  [Hash]  VARBINARY (MAX)  NULL,
                  [ErrorSqlString] nvarchar(max))
               AS
              EXTERNAL NAME [rt.srz.database.business.sqlserver].[StoredProcedures].[CalcStandardSearchKeysExchange]";
    }

    /// <summary>
    ///   The calc user search key.
    /// </summary>
    /// <returns>
    ///   The <see cref="string" />.
    /// </returns>
    public static string CalcUserSearchKey()
    {
      return @"CREATE FUNCTION [dbo].[CalcUserSearchKey]
            (@searchKeyTypeXml NVARCHAR (4000), @insuredPersonDataXml NVARCHAR (4000), @documentXml NVARCHAR (4000), @address1Xml NVARCHAR (4000), @address2Xml NVARCHAR (4000), @medicalInsuranceXml NVARCHAR (4000), @okato NVARCHAR (4000))
              RETURNS 
                TABLE (
                  [Hash] VARBINARY (MAX) NULL)
              AS
              EXTERNAL NAME [rt.srz.database.business.sqlserver].[StoredProcedures].[CalcUserSearchKey]";
    }

    /// <summary>
    ///   The calc user search key exchange.
    /// </summary>
    /// <returns>
    ///   The <see cref="string" />.
    /// </returns>
    public static string CalcUserSearchKeyExchange()
    {
      return @"CREATE FUNCTION [dbo].[CalcUserSearchKeyExchange]
             (@searchKeyTypeXml NVARCHAR (4000), @exchangeXml NVARCHAR (4000), @documentXml NVARCHAR (4000), @addressXml NVARCHAR (4000))
               RETURNS 
                TABLE (
                  [Hash] VARBINARY (MAX) NULL)
              AS
              EXTERNAL NAME [rt.srz.database.business.sqlserver].[StoredProcedures].[CalcUserSearchKeyExchange]";
    }

    #endregion
  }
}