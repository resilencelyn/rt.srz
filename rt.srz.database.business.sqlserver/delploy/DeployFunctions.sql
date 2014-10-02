GO
PRINT N'Creating [dbo].[CalcStandardSearchKeys]...';


GO
CREATE FUNCTION [dbo].[CalcStandardSearchKeys]
(@statementXml NVARCHAR (4000), @insuredPersonDataXml NVARCHAR (4000), @documentXml NVARCHAR (4000), @okato NVARCHAR (4000))
RETURNS 
     TABLE (
        [KeyId] UNIQUEIDENTIFIER NULL,
        [DocumentTypeId] INT NULL,
        [Hash]  VARBINARY (MAX)  NULL)
AS
 EXTERNAL NAME [rt.srz.business.database.sqlserver].[StoredProcedures].[CalcStandardSearchKeys]


GO
PRINT N'Creating [dbo].[CalcUserSearchKey]...';


GO
CREATE FUNCTION [dbo].[CalcUserSearchKey]
(@searchKeyTypeXml NVARCHAR (4000), @insuredPersonDataXml NVARCHAR (4000), @documentXml NVARCHAR (4000), @address1Xml NVARCHAR (4000), @address2Xml NVARCHAR (4000), @medicalInsuranceXml NVARCHAR (4000), @okato NVARCHAR (4000))
RETURNS 
     TABLE (
        [Hash] VARBINARY (MAX) NULL)
AS
 EXTERNAL NAME [rt.srz.business.database.sqlserver].[StoredProcedures].[CalcUserSearchKey]


GO
PRINT N'Creating [dbo].[CalcStandardSearchKeysExchange]...';


GO
CREATE FUNCTION [dbo].[CalcStandardSearchKeysExchange]
(@exchangeXml NVARCHAR (4000), @documentXml NVARCHAR (4000), @addressXml NVARCHAR (4000))
RETURNS 
     TABLE (
        [KeyId] UNIQUEIDENTIFIER NULL,
        [DocumentTypeId] INT NULL,
        [Hash]  VARBINARY (MAX)  NULL)
AS
 EXTERNAL NAME [rt.srz.business.database.sqlserver].[StoredProcedures].[CalcStandardSearchKeysExchange]


GO
PRINT N'Creating [dbo].[CalcUserSearchKeyExchange]...';


GO
CREATE FUNCTION [dbo].[CalcUserSearchKeyExchange]
(@searchKeyTypeXml NVARCHAR (4000), @exchangeXml NVARCHAR (4000), @documentXml NVARCHAR (4000), @addressXml NVARCHAR (4000))
RETURNS 
     TABLE (
        [Hash] VARBINARY (MAX) NULL)
AS
 EXTERNAL NAME [rt.srz.business.database.sqlserver].[StoredProcedures].[CalcUserSearchKeyExchange]


GO
