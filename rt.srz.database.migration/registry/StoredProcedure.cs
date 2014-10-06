// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StoredProcedure.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The stored procedure.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.registry
{
  /// <summary>
  ///   The stored procedure.
  /// </summary>
  internal static class StoredProcedure
  {
    #region Public Methods and Operators

    /// <summary>
    /// The calculate enp numbers.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CalculateEnpNumbers()
    {
      return @"
CREATE PROCEDURE [dbo].[srz_CalculateEnpNumbers] 
	@BeginRecordNumber int,
	@EndRecordNumber int
AS
BEGIN
MERGE NumberPolicyCounter AS Dst
	USING
	(
	select  t.Facet,
        max(t.CurrentNumber) as CurrentNumber
	from 
	(
		select  
			  SUBSTRING(S.NumberPolicy, 1, 10) as Facet, 	   
			  CONVERT(int, SUBSTRING(S.NumberPolicy, 11, 5)) AS CurrentNumber 
		from  Statement S
		where len(S.NumberPolicy) = 16 ) t
	group by t.Facet
	) AS Src(Facet, CurrentNumber)
	ON  (Dst.Id  = Src.Facet)
	WHEN MATCHED AND (Src.CurrentNumber + 1) > Dst.CurrentNumber THEN
		 UPDATE SET Dst.CurrentNumber = Src.CurrentNumber + 1
	WHEN NOT MATCHED THEN 
		 INSERT VALUES (Src.Facet, Src.CurrentNumber + 1); 
END";
    }

    /// <summary>
    /// The calculate kladr level and parrent id.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CalculateKladrLevelAndParrentId()
    {
      return @"
CREATE PROCEDURE [srz_CalculateKladrLevelAndParrentId]
AS
BEGIN

DELETE [KLADR]
WHERE Id IN
(SELECT k.id FROM [KLADR] AS k
 INNER JOIN [KLADR] AS k2 
  ON k.Code=k2.CODE
	WHERE k.id > k2.id)
--субъекты
Update [KLADR]
set level = t.RecursiveDepth,
	KLADR_PARENT_ID = t.ParentID 
from
[KLADR] AS k
inner join
(SELECT ID, CAST(NULL AS uniqueidentifier) AS ParentID, 1 AS RecursiveDepth
  FROM [KLADR]
  WHERE LEN(CODE) = 13 AND Cast(SUBSTRING(CODE,3,11) AS bigint)=0) AS t ON t.ID = k.ID
  
--районы
Update [KLADR]
set level = t.RecursiveDepth,
	KLADR_PARENT_ID = t.ParentID 
from
[KLADR] AS k
inner join
(SELECT t1.ID, t2.ID AS ParentID, 2 AS RecursiveDepth
	FROM [KLADR] t1
	INNER JOIN [KLADR] AS t2 ON 
	SUBSTRING(t1.Code, 1,2) = SUBSTRING(t2.Code, 1,2)
	AND CAST(SUBSTRING(t2.Code, 3,9) AS int)=0
	AND CAST(SUBSTRING(t1.Code, 3,3) AS int)<>0
	AND CAST(SUBSTRING(t1.Code, 6,6) AS int)=0
	AND LEN(t1.CODE)<=13 AND LEN(t2.CODE)<=13) AS t ON t.ID = k.ID 


--города
Update [KLADR]
set level = t.RecursiveDepth,
	KLADR_PARENT_ID = t.ParentID 
from
[KLADR] AS k
inner join
(SELECT t1.ID, t2.ID AS ParentID, 3 AS RecursiveDepth
	FROM [KLADR] t1
	INNER JOIN [KLADR] AS t2 ON 
	SUBSTRING(t1.Code, 1,5) = SUBSTRING(t2.Code, 1,5)
	AND CAST(SUBSTRING(t2.Code, 6,6) AS int)=0
	AND CAST(SUBSTRING(t1.Code, 6,3) AS int)<>0
	AND CAST(SUBSTRING(t1.Code, 9,3) AS int)=0
	AND LEN(t1.CODE)<=13 AND LEN(t2.CODE)<=13) AS t ON t.ID = k.ID

--населённые пункты
Update [KLADR]
set level = t.RecursiveDepth,
	KLADR_PARENT_ID = t.ParentID 
from [KLADR] AS k inner join
(SELECT t1.ID, t2.ID AS ParentID, 4 AS RecursiveDepth
	FROM [KLADR] t1
	INNER JOIN [KLADR] AS t2 ON 
	SUBSTRING(t1.Code, 1,8) = SUBSTRING(t2.Code, 1,8)
	AND CAST(SUBSTRING(t2.Code, 9,3) AS int)=0
	AND CAST(SUBSTRING(t1.Code, 9,3) AS int)<>0
	AND LEN(t1.CODE)<=13 AND LEN(t2.CODE)<=13) AS t ON t.ID = k.ID

--улицы
UPDATE [KLADR]
SET level = t.RecursiveDepth,
	KLADR_PARENT_ID = t.ParentID 
FROM [KLADR] AS k INNER JOIN
(SELECT t1.ID, t2.ID AS ParentID, 5 AS RecursiveDepth
	FROM [KLADR] t1
	INNER JOIN [KLADR] AS t2 ON
	SUBSTRING(t1.Code, 1,11) = SUBSTRING(t2.Code, 1,11)
	AND LEN(t1.CODE)>13	AND LEN(t2.CODE)<=13 ) AS t ON t.ID = k.ID

-- проставляем код ОКАТО
update KLADR
set OCATD = (select OCATD from KLADR k where KLADR.KLADR_PARENT_ID = k.ID )
where OCATD is null 
  or OCATD = ''
end";
    }

    /// <summary>
    /// The calculate standard search keys.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CalculateStandardSearchKeys()
    {
      return @"
CREATE PROCEDURE [dbo].[srz_CalculateStandardSearchKeys]
	@BeginRecordNumber int,
	@EndRecordNumber int
AS
BEGIN

	WITH StatementImp(RowId, InsuredPersonId, StatementXml, InsuredPersonDataXml, DocumentXml, DocumentTypeId, Okato) 
	AS
	(
	  SELECT 
			 Statement.RowId, 
			 Statement.InsuredPersonId, 
			   (SELECT Statement.* FROM Dual FOR XML AUTO, ELEMENTS) as StatementXml, 
			   (SELECT InsuredPersonData.* FROM Dual FOR XML AUTO, ELEMENTS) as InsuredPersonDataXml,  
			   (SELECT Document.* FROM Dual FOR XML AUTO, ELEMENTS) as DocumentXml, 
			   Document.DocumentTypeId,
			   Tfoms.Okato
	  FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY Statement.RowID) AS Quartile from Statement) Statement
		INNER JOIN InsuredPersonData on Statement.InsuredPersonDataId = InsuredPersonData.RowId
		INNER JOIN Document on Statement.DocumentUdlId = Document.RowId
		INNER JOIN Address a on Statement.AddressId = a.RowId
		LEFT JOIN Address a2 on Statement.Address2Id = a2.RowId
		INNER JOIN Organisation PointDistributionPolicy on PointDistributionPolicy.RowId = Statement.PointDistributionPolicyId
		INNER JOIN Organisation Smo on Smo.RowId = PointDistributionPolicy.ParentId
		INNER JOIN Organisation Tfoms on Tfoms.RowId = Smo.ParentId
	  WHERE Statement.Quartile between @BeginRecordNumber and @EndRecordNumber
	)

    INSERT INTO [SearchKey]
		   (
		    [RowId]
		   ,[DocumentUdlTypeId]
		   ,[InsuredPersonId]
		   ,[StatementId] 
		   ,[KeyValue]
		   ,[KeyTypeId])
	(	
		SELECT NEWID(),
				t1.DocumentTypeId,
			    t1.InsuredPersonId, 
				t1.RowId,
				t1.KeyValue,
				t1.KeyId
	    FROM (
				SELECT  t.DocumentTypeId,
						t.InsuredPersonId,
						t.RowId,
						k.KeyId as KeyId,
						k.[Hash] as KeyValue
				FROM StatementImp t CROSS APPLY [dbo].[CalcStandardSearchKeys] (t.StatementXml, t.InsuredPersonDataXml, t.DocumentXml, t.Okato) k
			) t1
	)
END";
    }

    /// <summary>
    /// The calculate standard search keys exchange.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CalculateStandardSearchKeysExchange()
    {
      return @"
CREATE PROCEDURE [dbo].[srz_CalculateStandardSearchKeysExchange]
	@BatchId uniqueidentifier,
	@BeginRecordNumber int,
	@EndRecordNumber int
AS
BEGIN
    WITH QueryResponseImp(RowId, InsuredPersonDataXml, DocumentXml, AddressXml) 
	AS
	(
	    SELECT QR.RowId
			  ,(SELECT InsuredPersonData.* FROM Dual FOR XML AUTO, ELEMENTS) AS InsuredPersonDataXml 
			  ,(SELECT Document.* FROM Dual FOR XML AUTO, ELEMENTS) AS DocumentXml 
			  ,(SELECT Address.* FROM Dual FOR XML AUTO, ELEMENTS) AS AddressXml 
		FROM (SELECT qr.*, ROW_NUMBER() OVER (ORDER BY qr.RowID) AS Quartile 
		      FROM QueryResponse qr
			    inner join Message m on qr.MessageId = m.RowId and BatchId=@BatchId) QR
             left join InsuredPersonData on  InsuredPersonData.RowId = qr.InsuredPersonDataId
             left join Document  on Document.RowId = qr.DocumentUdlId
			 left join Address on Address.RowId = qr.AddressId
		WHERE QR.Quartile BETWEEN @BeginRecordNumber and @EndRecordNumber
	)

    INSERT INTO [SearchKey]
			   (
			    [RowId]
			   ,[DocumentUdlTypeId]
			   ,[QueryResponseId]
			   ,[KeyValue]
			   ,[KeyTypeId])
	(	
		 SELECT NEWID()
			   ,t1.DocumentTypeId
			   ,t1.RowId
			   ,t1.[KeyValue]
			   ,t1.[KeyId]
		 FROM (
				SELECT  t.RowId
					   ,k.[DocumentTypeId] as [DocumentTypeId]
					   ,k.[Hash] as KeyValue
					   ,k.[KeyId] as KeyId
				FROM QueryResponseImp t CROSS APPLY [dbo].[CalcStandardSearchKeysExchange] (t.InsuredPersonDataXml, t.DocumentXml, t.AddressXml) k
			  ) t1
	)
END";
    }

    /// <summary>
    /// The calculate user search keys.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CalculateUserSearchKeys()
    {
      return @"
CREATE PROCEDURE [dbo].[srz_CalculateUserSearchKeys]
	@SearchKeyTypeId uniqueidentifier,
	@BeginRecordNumber int,
	@EndRecordNumber int
AS
BEGIN
   	   
	--Получение xml для типа ключа
	DECLARE @SearchKeyTypeXml AS nvarchar(4000)
	SET @SearchKeyTypeXml = (SELECT * FROM SearchKeyType WHERE RowId= @SearchKeyTypeId FOR XML AUTO, ELEMENTS)
	
	--Получение идентификатора ТФОМС
	DECLARE @TfomsId AS uniqueidentifier
	DECLARE @Okato AS nvarchar(4000)
	SELECT @TfomsId = SearchKeyType.TfomsId FROM SearchKeyType WHERE RowId = @SearchKeyTypeId  
	SELECT @Okato = Organisation.Okato FROM Organisation WHERE RowId = @TfomsId  ;

	WITH StatementImp(RowId, InsuredPersonId, InsuredPersonDataXml, DocumentXml, AddressXml, AddressXml2, MedicalInsuranceXml, DocumentTypeId) 
	AS
	(
	    SELECT 
			Statement.RowId, 
			Statement.InsuredPersonId, 
			(SELECT InsuredPersonData.* FROM Dual FOR XML AUTO, ELEMENTS) as InsuredPersonDataXml,  
			(SELECT Document.* FROM Dual FOR XML AUTO, ELEMENTS) as DocumentXml, 
			(SELECT Addr.* FROM Dual FOR XML AUTO, ELEMENTS) as AddressXml, 
			(SELECT Addr2.* FROM Dual FOR XML AUTO, ELEMENTS) as AddressXml2, 
			(SELECT MedicalInsurance.* FROM Dual FOR XML AUTO, ELEMENTS) as MedicalInsuranceXml,
			Document.DocumentTypeId
		FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY Statement.RowID) AS Quartile from Statement) Statement
		INNER JOIN InsuredPersonData on Statement.InsuredPersonDataId = InsuredPersonData.RowId
		INNER JOIN Document on Statement.DocumentUdlId = Document.RowId
		INNER JOIN Address Addr on Statement.AddressId = Addr.RowId
		LEFT JOIN Address Addr2 on Statement.Address2Id = Addr2.RowId
		INNER JOIN MedicalInsurance on StatementId=Statement.RowId and MedicalInsurance.IsActive =1
		INNER JOIN Organisation PointDistributionPolicy on PointDistributionPolicy.RowId = Statement.PointDistributionPolicyId
		INNER JOIN Organisation Smo on Smo.RowId = PointDistributionPolicy.ParentId
		WHERE Smo.ParentId = @TfomsId AND Statement.Quartile BETWEEN @BeginRecordNumber and @EndRecordNumber
	)


	INSERT INTO [SearchKey]
			   (
				[RowId]
			   ,[DocumentUdlTypeId]
			   ,[InsuredPersonId]
			   ,[StatementId]
			   ,[KeyValue]
			   ,[KeyTypeId])
	(	
		 SELECT NEWID(),
			   t1.DocumentTypeId,
			   t1.InsuredPersonId, 
			   t1.RowId, 
			   t1.[Hash],
			   @SearchKeyTypeId
	     FROM (
				SELECT  t.DocumentTypeId,
						t.InsuredPersonId,
						t.RowId,
						k.[Hash]
				FROM StatementImp t CROSS APPLY 
				       [dbo].[CalcUserSearchkey] (@SearchKeyTypeXml, t.InsuredPersonDataXml, t.DocumentXml, t.AddressXml, t.AddressXml2, t.MedicalInsuranceXml, @Okato) k
			   ) t1
	)
END";
    }

    /// <summary>
    /// The calculate user search keys exchange.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CalculateUserSearchKeysExchange()
    {
      return @"
CREATE PROCEDURE [dbo].[srz_CalculateUserSearchKeysExchange]
	@SearchKeyTypeId uniqueidentifier,
	@BatchId uniqueidentifier,
	@BeginRecordNumber int,
	@EndRecordNumber int
AS
BEGIN
	          
  --Получение xml для типа ключа
	DECLARE @SearchKeyTypeXml AS nvarchar(4000)
	SET @SearchKeyTypeXml = (SELECT * FROM SearchKeyType WHERE RowId= @SearchKeyTypeId FOR XML AUTO, ELEMENTS) ;
	
	WITH QueryResponseImp(RowId, DocumentTypeId, InsuredPersonDataXml, DocumentXml, AddressXml) 
	AS
	(
	    SELECT QR.RowId
			  ,Document.DocumentTypeId 
			  ,(SELECT InsuredPersonData.* FROM Dual FOR XML AUTO, ELEMENTS) AS InsuredPersonDataXml 
			  ,(SELECT Document.* FROM Dual FOR XML AUTO, ELEMENTS) AS DocumentXml 
			  ,(SELECT Address.* FROM Dual FOR XML AUTO, ELEMENTS) AS AddressXml 
		FROM (SELECT qr.*, ROW_NUMBER() OVER (ORDER BY qr.RowID) AS Quartile 
		      FROM QueryResponse qr
			    inner join Message m on qr.MessageId = m.RowId and BatchId=@BatchId) QR
             left join InsuredPersonData on  InsuredPersonData.RowId = qr.InsuredPersonDataId
             left join Document  on Document.RowId = qr.DocumentUdlId
			 left join Address on Address.RowId = qr.AddressId
		WHERE QR.Quartile BETWEEN @BeginRecordNumber and @EndRecordNumber
	)
	
	INSERT INTO [SearchKey]
			   (
			    [RowId]
			   ,[DocumentUdlTypeId]
			   ,[QueryResponseId]
			   ,[KeyValue]
			   ,[KeyTypeId])
	(	
		 SELECT NEWID()
			   ,t1.DocumentTypeId
			   ,t1.RowId
			   ,t1.[Hash],
			   @SearchKeyTypeId
	     FROM (
				SELECT  t.RowId,
						t.DocumentTypeId,
						k.[Hash]
				FROM QueryResponseImp t CROSS APPLY [dbo].[CalcUserSearchKeyExchange] (@SearchKeyTypeXml, t.InsuredPersonDataXml, t.DocumentXml, t.AddressXml) k
			  ) t1
	)
END
";
    }

    /// <summary>
    /// The create export smo batches.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CreateExportSmoBatches()
    {
      return @"
CREATE PROCEDURE [dbo].[srz_CreateExportSmoBatches]
	@PeriodId uniqueidentifier,
	@MaxMessageCountInBatch int
AS
BEGIN
	DECLARE @PdpId uniqueidentifier

	-- Получаем список ПВП, для которых имеются не выгруженные заявления и создаем курсор
	DECLARE Pdp_Cursor CURSOR FOR
	SELECT st.PointDistributionPolicyId
	FROM Statement st
	LEFT JOIN MessageStatement ms on ms.StatementId = st.RowId and ms.Version = st.Version 
	WHERE ms.RowId IS NULL
	GROUP BY st.PointDistributionPolicyId

	-- Открываем курсор
	OPEN Pdp_Cursor 
	FETCH NEXT FROM Pdp_Cursor INTO @PdpId

	--Проходим курсором по результатам запроса
	WHILE @@FETCH_STATUS = 0  
	BEGIN 
	  if (@PdpId is not null) 
	  begin
		SELECT @PdpId
		
	    -- Получаем код ПВП
	    DECLARE @PdpCode varchar(20)
	    SELECT @PdpCode = Code
	    FROM Organisation
	    WHERE RowId=@PdpId 
	  
	    --Получаем идентификатор СМО к которому относится ПВП
	    DECLARE @SmoId uniqueidentifier
	    DECLARE @SmoCode varchar(20)
	    SELECT @SmoId = Smo.RowId, @SmoCode = Smo.Code 
	    FROM Organisation Smo
	    JOIN Organisation Pdp ON Smo.RowId=Pdp.ParentId
	    WHERE Pdp.RowId=@PdpId
	  
	    --Получаем ТФОМС к которому относится СМО
	    DECLARE @TfomsId uniqueidentifier
	    DECLARE @TfomsCode varchar(20)
	    SELECT @TfomsId = Tfoms.RowId, @TfomsCode = Tfoms.Code 
	    FROM Organisation Tfoms
	    JOIN Organisation Smo ON Tfoms.RowId=Smo.ParentId
	    JOIN Organisation Pdp ON Smo.RowId=Pdp.ParentId
	    WHERE Pdp.RowId=@PdpId
	    
	    --Запрашиваем все не выгруженные заявления для ПВП во временную таблицу
		DECLARE @UnloadedStatements AS StatementWithVersion
		INSERT INTO @UnloadedStatements(StatementId, StatementVersion)
		SELECT st.RowId as StatementId, st.Version as StatementVersion
		FROM Statement st
		LEFT JOIN MessageStatement ms on ms.StatementId = st.RowId and ms.Version = st.Version 
		WHERE ms.RowId IS NULL
		AND st.PointDistributionPolicyId = @PdpId
		
		--Вызываем процедуру формирования батчей выгрузки RecList
		EXEC [dbo].[srz_CreateExportSmoBatchesForPdp]
		@UnloadedStatements = @UnloadedStatements,
		@BatchSubjectId = 626, -- СМО
		@BatchTypeId = 628, -- Файл корректировки данных от ТФОМС в СМО
		@MessageTypeId = 623, -- Файлы корректировки данных от ТФОМС по отдельным записям или группам записей
		@FilePrefix = 'k', -- Файлы корректировки данных от ТФОМС по отдельным записям или группам записей
		@PeriodId = @PeriodId,
		@SenderId = @TfomsId, -- Отправитель ТФОМС
		@ReceiverId = @SmoId, -- Получатель СМО
		@PdpId = @PdpId,
		@PdpCode = @PdpCode,
		@SmoCode = @SmoCode,
		@MaxMessageCountInBatch = @MaxMessageCountInBatch
		
		----Вызываем процедуру формирования батчей выгрузки OpList
		--EXEC [dbo].[srz_CreateExportSmoBatchesForPdp]
		--@UnloadedStatements = @UnloadedStatements,
		--@BatchSubjectId = 646, -- ТФОМС
		--@BatchTypeId = 645, -- Файл с изменениями от СМО в ТФОМС
		--@MessageTypeId = 620, -- Файлы с изменениями от СМО
		--@FilePrefix = 'i', -- Для файлов с изменениями от СМО
		--@PeriodId = @PeriodId,
		--@SenderId = @SmoId, -- Отправитель СМО
		--@ReceiverId = @TfomsId, -- Получатель ТФОМС
		--@PdpId = @PdpId,
		--@PdpCode = @PdpCode,
		--@SmoCode = @SmoCode,
		--@MaxMessageCountInBatch = @MaxMessageCountInBatch
		
		--Удаляем все записи во временной таблице
		DELETE FROM @UnloadedStatements
	  end	
		FETCH NEXT FROM Pdp_Cursor INTO @PdpId
	END  	
END
";
    }

    /// <summary>
    /// The create export smo batches for pdp.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CreateExportSmoBatchesForPdp()
    {
      return @"
CREATE PROCEDURE [dbo].[srz_CreateExportSmoBatchesForPdp]
	@UnloadedStatements StatementWithVersion READONLY,
	@BatchSubjectId int,
	@BatchTypeId int,
	@MessageTypeId int,
	@FilePrefix char,
	@PeriodId uniqueidentifier,
	@SenderId uniqueidentifier,
	@ReceiverId uniqueidentifier,
	@PdpId uniqueidentifier,
	@PdpCode varchar(20),
	@SmoCode varchar(20),
	@MaxMessageCountInBatch int
AS
BEGIN
	--Расчитываем количество батчей
	DECLARE @BatchCount int
	SELECT @BatchCount = COUNT(*) / @MaxMessageCountInBatch + 1
	FROM @UnloadedStatements
	
	--Нумеруем заявления
	SELECT ROW_NUMBER() OVER (ORDER BY StatementId) as StatementNumber, *
	INTO #NumberedStatements
	FROM @UnloadedStatements
	
	DECLARE @BatchNumber int
	SET @BatchNumber=1

	DECLARE @StatementCounter int
	SET @StatementCounter=1 

	WHILE (@BatchNumber <= @BatchCount)
	BEGIN 
	  --Считаем следующий номер батча
	  DECLARE @BatchNextNumber int
	  SET @BatchNextNumber = 1
	  DECLARE @BatchRecordCount int
	  SELECT @BatchNextNumber = ISNULL(MAX(Number), 0) + 1
	  FROM Batch
	  WHERE SubjectId = @BatchSubjectId 
	  AND TypeId = @BatchTypeId
	  AND SenderId = @SenderId
	  AND ReceiverId = @ReceiverId 
	  AND PeriodId = @PeriodId
	  
	  --Достаем месяц и год из периода
	  DECLARE @Month varchar(2)
	  DECLARE @Year varchar(2)
	  SELECT @Month = CONVERT(varchar(2), C.Code), @Year=SUBSTRING(CONVERT(varchar(4), YEAR(P.Year)), 3, 2) 
	  FROM Period P
	  JOIN Concept C ON P.CodeId=C.Id
	  WHERE P.RowId=@PeriodId
	  
	  IF (LEN(@Month) = 1)
		SET @Month = '0' + @Month; 
	  
	  --Формируем имя файла
	  DECLARE @FileName varchar(250)
	  SET @FileName = 
		 @FilePrefix
	   + @SmoCode
	   + '_'  
	   + @PdpCode
	   + '_'
	   + @Month
	   + @Year
	   + CONVERT(varchar(10), @BatchNextNumber)
	   
	  --Создаем новый батч
	  DECLARE @BatchId uniqueidentifier
	  SET @BatchId = NEWID()
	  INSERT INTO [Batch]
           ([RowId]
           ,[SubjectId]
           ,[TypeId]
           ,[FileName]
           ,[Number]
           ,[CodeConfirmId]
           ,[PeriodId]
           ,[SenderId]
           ,[ReceiverId])
      VALUES
           (@BatchId
           ,@BatchSubjectId
           ,@BatchTypeId
           ,@FileName
           ,@BatchNextNumber
           ,596 -- Подтверждение при-ёма: принято
           ,@PeriodId
           ,@SenderId
           ,@ReceiverId)
	
	  SELECT TOP (@MaxMessageCountInBatch)
            NEWID() as RowID
           ,StatementId as StatementId
           ,StatementVersion as Version
           ,635 as TypeId
      INTO #PartNumberedStatement
      FROM #NumberedStatements
      WHERE StatementNumber >= @StatementCounter
	    order by StatementNumber

	  -- Вставляем записи в MessageStatement
	  INSERT INTO [MessageStatement]
           ([RowId]
           ,[StatementId]
           ,[Version]
           ,[TypeId])
      SELECT * FROM #PartNumberedStatement
      
      --Вставляем записи в Message
	  INSERT INTO [Message]
           ([RowId]
           ,[BatchId]
           ,[TypeId]
           ,[IsCommit]
           ,[IsError]
           )
      SELECT 
           #PartNumberedStatement.RowId
           ,@BatchId
           ,@MessageTypeId
           ,'True'
           ,'False'
      FROM #PartNumberedStatement
      
      --Апдэейтим связку MessageStatement с Message
      DECLARE @TempId uniqueidentifier
      UPDATE MessageStatement
	  SET @TempId = RowId,
		  MessageId = @TempId
	  WHERE MessageId IS NULL
	
	  SET @BatchNumber = @BatchNumber + 1
	  SET @StatementCounter = @StatementCounter + @MaxMessageCountInBatch
		DROP TABLE #PartNumberedStatement
	END

	--Удаляем временные таблицы
	DROP TABLE #NumberedStatements
END
";
    }

    /// <summary>
    /// The create statement with version.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CreateStatementWithVersion()
    {
      return
        @"IF  NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'StatementWithVersion' AND ss.name = N'dbo')
               CREATE TYPE [dbo].[StatementWithVersion] 
               AS TABLE(
	              [StatementId] [uniqueidentifier] NOT NULL,
	              [StatementVersion] [int] NOT NULL
              )";
    }

    /// <summary>
    /// The find twins.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FindTwins()
    {
      return @"
CREATE PROCEDURE [dbo].[srz_FindTwins]
AS
BEGIN
	--Селектируем равные значения ключей во временную таблицу
	SELECT  k1.InsuredPersonId AS FirstInsuredPersonId, 
			k2.InsuredPersonId AS SecondInsuredPersonId, 
			k1.KeyTypeId 
	INTO #ttt
	FROM SearchKey k1  
	 inner join SearchKeyType kt1 on k1.KeyTypeId = kt1.RowId and kt1.IsActive = 1
	 inner join SearchKey k2 ON k1.KeyValue = k2.KeyValue AND k1.KeyTypeId = k2.KeyTypeId AND k1.InsuredPersonId != k2.InsuredPersonId
	 inner join SearchKeyType kt2 on k2.KeyTypeId = kt2.RowId and kt2.IsActive = 1
	WHERE k1.RowId < k2.RowId
	group by k1.InsuredPersonId, k2.InsuredPersonId, k1.KeyTypeId 

	--Убираем данные, которые уже есть в твинсах
	SELECT RES.* 
	INTO #tt
	FROM #ttt RES
	 LEFT JOIN [Twins] T ON T.FirstInsuredPersonId = RES.FirstInsuredPersonId AND T.SecondInsuredPersonId = RES.SecondInsuredPersonId
	 LEFT JOIN [TwinsKey] TK ON TK.TwinId = T.RowId AND TK.KeyTypeId = RES.KeyTypeId
    WHERE T.RowId IS NULL

	--Убираем данные, которые уже есть в твинсах РЕВЕРС ПАРЫ
	SELECT RES.* 
	INTO #t
	FROM #tt RES
	 LEFT JOIN [Twins] T ON T.FirstInsuredPersonId = RES.SecondInsuredPersonId AND T.SecondInsuredPersonId = RES.FirstInsuredPersonId
	 LEFT JOIN [TwinsKey] TK ON TK.TwinId = T.RowId AND TK.KeyTypeId = RES.KeyTypeId
    WHERE T.RowId IS NULL
	
	--Вставляем значения в таблицу Twins
	INSERT INTO [Twins]
           ([RowId]
           ,[FirstInsuredPersonId]
           ,[SecondInsuredPersonId]
           ,[TwinTypeId])
     SELECT NEWID()
			,FirstInsuredPersonId
			,SecondInsuredPersonId
			,538 --Кадидат в дубликаты
	 FROM #t
	 GROUP BY FirstInsuredPersonId, SecondInsuredPersonId
	 
	 --Вставляем значение в таблицу TwinsKey
	 INSERT INTO [TwinsKey]
           ([RowId]
           ,[TwinId]
           ,[KeyTypeId])
     SELECT
           NEWID()
           ,Twins.RowId
           ,#t.KeyTypeId
     FROM #t
     JOIN Twins ON #t.FirstInsuredPersonId=Twins.FirstInsuredPersonId AND 
                   #t.SecondInsuredPersonId=Twins.SecondInsuredPersonId AND 
                   Twins.TwinTypeId=538
           
END";
    }

    /// <summary>
    /// The process pfr.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ProcessPfr()
    {
      return @"
CREATE PROCEDURE [dbo].[srz_ProcessPfr] 
	@MessageId uniqueidentifier, 
	@PeriodId uniqueidentifier
AS
BEGIN

select s2.InsuredPersonId,
	   qr.RowId,
	   -- новый снилс	
       qr.snils,
	   qr.[Employment]
into #serchResult       
from QueryResponse qr
 inner join SearchKey s1 on s1.QueryResponseId = qr.RowId -- все входящие ключи 
 inner join SearchKey s2 on s1.KeyTypeId = s2.KeyTypeId 
                        and s1.KeyValue = s2.KeyValue 
						and s1.RowId != s2.RowId 
						and s2.InsuredPersonId is not null
where qr.MessageId = @MessageId
group by s2.InsuredPersonId, qr.RowId, qr.snils, qr.[Employment]


-- Вставляем информацию о занятости
insert into EmploymentHistory (PeriodId, QueryResponseId, InsuredPersonId, SourceTypeId, Employment)
(
   select @periodId, RowId, InsuredPersonId, 594, [Employment] from #serchResult
)

-- обновляем снилс
select st.RowId as StatementId,
       pd.RowId as InsuredPersonDataId,
	   sr.Snils as SnilsNew,
	   pd.Snils as SnilsOld,
	   sr.InsuredPersonId, 
	   st.Version
into #change	      
from #serchResult sr
 inner join Statement st on st.InsuredPersonId = sr.InsuredPersonId 
						and st.IsActive = 1 
 inner join InsuredPersonData pd on pd.RowId = st.InsuredPersonDataId
                        and pd.Snils != sr.Snils						
                                  
-- Пишем историю (И эти изменения отправятся в СМО)
update Statement
set [Version] = Version + 1
where RowId in (select StatementId from #change)

INSERT INTO [StatementChangeDate]
           ([RowId]
           ,[StatementId]
           ,[Version]
           ,[FieldId]
           ,[Datum])
     (
	 select NewId(),
	        StatementId,
			[Version],
			612,
			SnilsOld
	 from #change
	 )

-- Изменяем снилс
update InsuredPersonData
  set Snils = ch.SnilsNew 
  from #change ch
  where ch.InsuredPersonDataId = InsuredPersonDataId

-- Если есть испорченные СНИЛСЫ надо пометить что снилсы испорчены
update InsuredPersonData
set IsBadSnils = 1
where RowId in (
		select pd.RowId 
		from #change ch
		 inner join InsuredPersonData pd on pd.Snils = ch.SnilsNew
		 inner join Statement st on pd.RowId = st.InsuredPersonDataId
							   and ch.InsuredPersonId != st.InsuredPersonId
							   and st.StatusId not in (288, 289))

END
";
    }

    /// <summary>
    /// The process snils pfr.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ProcessSnilsPfr()
    {
      return @"
CREATE PROCEDURE [dbo].[srz_ProcessSnilsPfr]
	@MessageId uniqueidentifier, 
	@PeriodId uniqueidentifier
AS
BEGIN
		
	insert into EmploymentHistory (PeriodId, QueryResponseId, InsuredPersonId, SourceTypeId, Employment)
	(select @periodId,
			qr.RowId, 
			s.InsuredPersonId,
			594,
			1
	from  QueryResponse qr 
		inner join InsuredPersonData d on d.Snils = qr.Snils 
		inner join [Statement] s on s.InsuredPersonDataId = d.RowId and s.IsActive = 1 	        
  where qr.MessageId = @MessageId
	)
END";
    }

    /// <summary>
    /// The process zags.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ProcessZags()
    {
      return @"
create PROCEDURE [dbo].[srz_ProcessZags] 
	@BatchId uniqueidentifier
AS
BEGIN

	DECLARE @DeadInfoVar table(
		RowId uniqueidentifier,
		InsuredPersonId uniqueidentifier  
	);
  
	WITH #searchResult(InsuredPersonId, DeathDate) 
	AS
	(
		select distinct s2.InsuredPersonId, -- ид InsuredPersonId у которой надо проставаить статус что умер
			pe.DeathDate	
		from InsuredPersonDataExchange pe
		inner join SearchKey s1 on s1.InsuredPersonDataExchangeId = pe.RowId -- все входящие ключи 
		inner join SearchKey s2 on s1.KeyTypeId = s2.KeyTypeId and s1.KeyValue = s2.KeyValue and s1.RowId != s2.RowId and s2.InsuredPersonId is not null
		where pe.BatchId = @BatchId
	)

	--добавляем инфу о смерти в таблицу 
	MERGE DeadInfo g
	USING 
	(  
	SELECT s.DeathDate, s.InsuredPersonId
	FROM #searchResult s
	)src(DeathDate, InsuredPersonId) ON (1=0)
	WHEN NOT MATCHED THEN INSERT ( DateDead )
	VALUES (src.DeathDate)
	OUTPUT INSERTED.RowId, src.InsuredPersonId
	INTO @DeadInfoVar;   

	--проставляем что умерший и ссылку на саму инфу по дате смерти 
	update InsuredPerson
	set DeadInfoId = d.RowId, StatusId = 467
	from @DeadInfoVar d
	where InsuredPerson.RowId = d.InsuredPersonId

END
";
    }

    #endregion
  }
}