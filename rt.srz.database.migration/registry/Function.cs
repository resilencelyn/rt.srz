using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rt.srz.database.registry
{
  static internal class Function
  {
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
  }
}
