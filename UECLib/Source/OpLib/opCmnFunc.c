#ifdef GUIDE
#include <windows.h>
#endif//GUIDE
#include <stdio.h>
#include "opCmnFunc.h"
#include "opCtxFunc.h"
#include "FileId.h"	
#include "opApi.h"
#include "CertType.h"
#include "KeyType.h"
#include "FuncLib.h"

#ifdef GUIDE
IL_FUNC void opCmnDisplayString(s_opContext *p_opContext, IL_CHAR *str)
{
	if(p_opContext && p_opContext->pDisplayText && p_opContext && str)
	{
		p_opContext->pDisplayText((IL_CHAR*)str);	
	}
}

#define PREFIX_COL_MAX	60
IL_FUNC void opCmnDisplayStringPrefix(s_opContext *p_opContext, IL_CHAR *prefix, IL_CHAR *filler)
{
	IL_WORD i;

	if(p_opContext && p_opContext->pDisplayText && prefix)
	{
		p_opContext->pDisplayText((IL_CHAR*)prefix);	
		for(i = cmnStrLen(prefix); i < PREFIX_COL_MAX; i++)
			p_opContext->pDisplayText((filler ? filler : " "));
		p_opContext->pDisplayText(" ");
	}
}

IL_FUNC void opCmnDisplayLine(s_opContext *p_opContext, IL_CHAR *str)
{
	if(p_opContext && str && p_opContext->pDisplayText)
	{
		p_opContext->pDisplayText((IL_CHAR*)str);	
		p_opContext->pDisplayText("\n");	
	}
}
#endif//GUIDE

IL_FUNC IL_WORD opCmnAppVerifyCitizen(s_opContext *p_opContext)
{
	IL_WORD RC;

	// верифицируем гражданина на предустановленном в контексте пароле указанного номера
	RC = flAppCitizenVerification(p_opContext->phCrd, p_opContext->PinNum, p_opContext->PinBlock, &p_opContext->PinTriesLeft);
	return RC;
}

IL_FUNC IL_WORD opCmnRestoreCryptoSession(s_opContext *p_opContext)
{
	IL_WORD RC; 

	// селектируем приложение заново без возврата информации
	if((RC = flAppReselect(p_opContext->phCrd)) != 0)		
		return RC;

	// аутентифицируем терминал 
	if((RC = flAppTerminalAuth(p_opContext->phCrd)) != 0)
		return RC;
		 
	return 0;
}

#ifdef GUIDE
IL_CHAR* GetParamFilename(IL_CHAR* pszFullParamFileName, IL_CHAR* pszParamFileName, IL_CHAR* pszParamFileExt)
{
    static IL_CHAR tmpFileName[_MAX_PATH];
    IL_CHAR  drive[_MAX_DRIVE], dir[_MAX_DIR], name[_MAX_FNAME], old_ext[_MAX_EXT];
    GetModuleFileNameA(0,tmpFileName,sizeof(tmpFileName));

    _splitpath(tmpFileName, drive, dir, name, old_ext);

    _makepath(tmpFileName, drive, dir, pszParamFileName, pszParamFileExt);

    if(pszFullParamFileName)
        cmnStrCopy(pszFullParamFileName, tmpFileName);

    return pszFullParamFileName;
}
#endif//GUIDE

IL_FUNC IL_WORD opGetDataByTagId(s_opContext *p_opContext, IL_WORD sectorId, IL_WORD blockId, IL_DWORD TagId, IL_CHAR *out_str)
{
	IL_WORD RC;
	IL_WORD MaxLen; 
	DATA_DESCR *pDataDescr;

	if((RC = opDescrGetDataByTagId(p_opContext, sectorId, blockId, TagId, &pDataDescr)) != 0)
		return RC;

	RC = opCmnGetDataByDataDescr(p_opContext, pDataDescr, out_str, &MaxLen);
	return RC;
}

IL_FUNC IL_WORD opCmnGetDataByDataDescr(s_opContext *p_opContext, DATA_DESCR *pDataDescr, IL_CHAR *out_str, IL_WORD *MaxLen)
{
	IL_WORD RC;
	BLOCK_DESCR *pBlockDescr;
	IL_BYTE out[UEC_DATA_OUT_MAX_LEN];
	IL_WORD out_len;
	IL_DWORD data_len;
	IL_BYTE *pData;

	// специальный случай - чтение корневых данных карты 
	if(pDataDescr->SectorId == 0)
	{	// извлекаем уже считанные в контекст системные данные
		switch(pDataDescr->TagId)
		{
			case 0x9F08:
				cmnStrCopy(out_str, p_opContext->AppVersion); break;
			case 0x5F24:
				cmnStrCopy(out_str, p_opContext->ExpirationDate); break;
			case 0x5F25:
				cmnStrCopy(out_str, p_opContext->EffectiveDate); break;
			case 0x5A:
				cmnStrCopy(out_str, p_opContext->PAN); break;
			default:
				return ILRET_OPLIB_INVALID_ARGUMENT;
		}
		return 0;
	}

	// получим описатель блока данных 
	if((RC = opDescrGetBlock(p_opContext, pDataDescr->SectorId, pDataDescr->BlockId, &pBlockDescr)) != 0)
		return RC;

	// извлекаем сырые данные в зависимости от типа доступа к данным блока
	cmnMemSet(out, 0, sizeof(out));
	out_len = pDataDescr->MaxLen > UEC_DATA_OUT_MAX_LEN ? UEC_DATA_OUT_MAX_LEN : pDataDescr->MaxLen;
	if((RC = opCmnReadCardDataRaw(p_opContext, pBlockDescr->SectorId, pDataDescr->BlockId, pBlockDescr->FileType, (IL_WORD)pDataDescr->TagId, &out_len, out)) != 0)
	{
		//if(pBlockDescr->FileType == BLOCK_DATA_BINTLV && RC == ILRET_DATA_TAG_NOT_FOUND && !pDataDescr->isMust)
		//{	//+++ для ненайденных необязательных BINTLV-данных возвращаем пустую строку
		//	out[0]  = 0;
		//	out_len = 0;
		//}
		//else
		{	// иначе возвращаем ошибку!!!
			return RC;
		}
	}
	
	pData    = out;
	data_len = out_len;

	// за максимальную длину TLV-данных принимается фактическая длина считвнных данных
	if(MaxLen)
		*MaxLen = pBlockDescr->FileType == BLOCK_DATA_TLV ? out_len : pDataDescr->MaxLen;

	// конвертируем сырые данные
	if(pDataDescr->Type == DATA_ASCII)
		Iso8859_2_Ansi((IL_CHAR*)pData, out_str);
	else if(pDataDescr->Type == DATA_NUMERIC || pDataDescr->Type == DATA_NUMERIC2)
		bcd2str(pData, (IL_WORD)data_len, out_str);
	else if(pDataDescr->Type == DATA_ISO5218)
		Iso5218_2_Ansi(pData, out_str);
	else if(pDataDescr->Type == DATA_BYTE)
		bin2hex(out_str, pData, data_len);
	else
		return ILRET_OPLIB_ILLEGAL_DATA_TYPE;

	return 0;
}

//+++
IL_WORD _opCmnCashBinTlvArray(s_opContext *p_opContext, IL_WORD sectorId, IL_WORD blockId)
{
	IL_WORD RC;
	IL_BYTE tmp[10];
	BLOCK_DESCR *pBlockDescr;

	// получим описатель блока BinTlv-данных
	RC = opDescrGetBlock(p_opContext, (IL_WORD)sectorId, (IL_WORD)blockId, &pBlockDescr);
	if(RC)
		return RC;

	// считываем только не кешированный массив бинарных TLV-данных 
	if(sectorId == p_opContext->BinTlvDescr.SectorId 
		&& blockId == p_opContext->BinTlvDescr.BlockId 
		&& GetTag(p_opContext->BinTlvDescr.pData) == (IL_TAG)pBlockDescr->RootTag)
		return 0;

 	// селектируем указанный блок Tlv-данных
	RC = flSelectContext(p_opContext->phCrd, sectorId, blockId, 0);
	if(RC)
		return RC;

    // считываем TLV-заголовок 
    //ВНИМАНИЕ!!! Потенциальная угроза: при чтении TLV файла с длиной данных меньшей 3x байт возможна ошибка
    RC = clAppReadBinary(p_opContext->phCrd, 0, 5, tmp);
    if(RC)
		return RC;

	// вычислим общую длину считываемых бинарных данных
	cmnMemClr((IL_BYTE*)&p_opContext->BinTlvDescr, sizeof(BINTLV_DESCR));
    p_opContext->BinTlvDescr.DataLen = (IL_WORD)(GetTagLen(tmp) + GetLenLen(tmp) + GetDataLen(tmp));

	// освободим выделенную ранее память
	if(p_opContext->BinTlvDescr.pData)
		cmnMemFree(p_opContext->BinTlvDescr.pData);

	// выделим память под массив для кешируемых DinTlv-данных
	p_opContext->BinTlvDescr.pData = cmnMemAlloc(p_opContext->BinTlvDescr.DataLen);
	if(!p_opContext->BinTlvDescr.pData)
		return ILRET_SYS_MEM_ALLOC_ERROR;

	// считываем массив бинарных TLV-данных
	RC = flAppReadBinTlvBlock(p_opContext->phCrd, sectorId, blockId, p_opContext->BinTlvDescr.pData, &p_opContext->BinTlvDescr.DataLen);
	if(RC)
	{
		cmnMemFree(p_opContext->BinTlvDescr.pData);
		cmnMemClr((IL_BYTE*)&p_opContext->BinTlvDescr, sizeof(BINTLV_DESCR));
		return RC;
	}

	// инициализируем идентификаторы сектора и блока закешированных BinTlv-данных
	p_opContext->BinTlvDescr.SectorId = sectorId;
	p_opContext->BinTlvDescr.BlockId  = blockId;

	return 0;
}

//+++
extern void _GetTagPath(IL_TAG RootTag, DATA_DESCR *pDataDescr, IL_TAG *TagPath, IL_WORD *TagNums);
IL_FUNC IL_WORD _opCmnGetBinTlvData(s_opContext *p_opContext, IL_WORD sectorId, IL_WORD blockId, IL_WORD TagId, IL_WORD *inout_pDataLen, IL_BYTE *out_pData)
{
	IL_WORD RC;
	BLOCK_DESCR *pBlockDescr;
	DATA_DESCR *pDataDescr;
	IL_TAG TagPath[TPATH_MAX_LEN+2] = { 0 };
	IL_WORD NumTags = 0;
	IL_BYTE *pData;
	IL_DWORD data_len;

	// настроим указатель на описатель блока прикладных данных
	RC = opDescrGetBlock(p_opContext, (IL_WORD)sectorId, (IL_WORD)blockId, &pBlockDescr);
	if(RC)
		return RC;

	// настроим указатель на описактель элемента прикладных данных
	RC = opDescrGetDataByTagId(p_opContext, (IL_WORD)sectorId, (IL_WORD)blockId, TagId, &pDataDescr);
	if(RC)
		return RC;

	// конструируем полный путь до искомого элемента данных
	_GetTagPath(pBlockDescr->RootTag, pDataDescr, TagPath, &NumTags);

	// осуществляем поиск TLV-элемента данного 
	RC = TagFindByPath(p_opContext->BinTlvDescr.pData, p_opContext->BinTlvDescr.DataLen, TagPath, NumTags, &data_len, &pData, 0);
	if(RC)
	{
		if(RC != ILRET_DATA_TAG_NOT_FOUND || (p_opContext->OperationCode == UEC_OP_PROVIDE_SERVICE && pDataDescr->isMust)) 
			return RC;	
		// игнорируем ошибку и возвращаем пустую строку для редактирования
		*inout_pDataLen = 0; 
		*out_pData = 0;
	}
	else
	{
		cmnMemCopy(out_pData, pData, data_len); 
		*inout_pDataLen = data_len;
	}

	return 0;
}

IL_FUNC IL_WORD opCmnReadCardDataRaw(s_opContext *p_opContext, IL_WORD sectorId, IL_WORD blockId, IL_BYTE FileType,
									 IL_WORD TagId, IL_WORD *inout_pDataLen, IL_BYTE *out_pData)
{
	IL_WORD RC;

	// селектируем прикладной контекст
	if((RC = flSelectContext(p_opContext->phCrd, sectorId, blockId, 0)) != 0)
		return RC;

	// способ считывния данных зависит от типа файла 
	if(FileType == BLOCK_DATA_TLV)
	{
		IL_BYTE out[UEC_DATA_OUT_MAX_LEN];
		IL_WORD out_len;
		IL_BYTE *pData;
		IL_DWORD data_len;

		if((RC = clAppGetData(p_opContext->phCrd, TagId, out, &out_len)) != 0)
			return RC;
		TagFind(out, out_len, TagId, &data_len, &pData, 0);
		if(pData == NULL)
			return ILRET_OPLIB_CORRUPTED_TLV_DATA;
		cmnMemCopy(out_pData, pData, (IL_WORD)data_len);
		*inout_pDataLen = (IL_WORD)data_len;
	}
	else if(FileType == BLOCK_DATA_BIN)
	{	
 		if((RC = flReadBinaryEx(p_opContext->phCrd, TagId, out_pData, *inout_pDataLen, inout_pDataLen)) != 0)
			return RC;
		*inout_pDataLen -= TagId;
	}
	else if(FileType == BLOCK_DATA_RECORD)
	{
		RC = clAppReadRecord(p_opContext->phCrd, (IL_BYTE)TagId, 0, out_pData, inout_pDataLen);
		if(RC && RC == ILRET_CRD_READREC_RECORD_NOT_FOUND) 
		{	// если сектор с указанным номером не найден, то ошибку не фиксируем
			// и возвратим нулевую длину считанных данных!!!
			*inout_pDataLen = 0;
		}
		else
			return RC;
	}
	else if(FileType == BLOCK_DATA_BINTLV)
	{	// кешируем в контекст ИБТ массив BinTlv-данных 
		RC = _opCmnCashBinTlvArray(p_opContext, sectorId, blockId);
		if(RC)
			return RC;
		
		// извлекаем значение элемента BinTlv-данного  
		RC = _opCmnGetBinTlvData(p_opContext, sectorId, blockId, TagId, inout_pDataLen, out_pData);
		if(RC)
			return RC;
	}
	else
		return ILRET_OPLIB_INVALID_FILE_TYPE;

	return 0;
}

// Сохранение в контексте статических параметров запроса на аутентификацию ИД-Приложения для установления защищённой сессии с эмитентом
// Данные запроса формируются на основе данных основного запроса на оказание услуги 
IL_FUNC IL_WORD _opCmnSaveAppAuthRequestIssSession(s_opContext *p_opContext)
{
	IL_WORD RC = 0;
    IL_WORD ofs = 0;
    IL_DWORD dwLen;
    IL_BYTE* pTmp;
	IL_BYTE Empty[32];
	IL_WORD i;
    IL_BYTE OperationTimeStamp[6]; 
    IL_BYTE TerminalUnpredictableNumber[4]; 
	IL_TAG utility_data_list[] = { IL_TAG_C2, IL_TAG_9F08, IL_TAG_9F1E, IL_TAG_DF7F };

	//+++ ЗАПЛАТКА
	cmnMemClr(Empty, sizeof(Empty));

	// идентификатор типа услуги (9F15) - ПУСТОЕ ЗНАЧЕНИЕ!!!
	if(ofs + AddTag(IL_TAG_9F15, NULL, 10, NULL) > UEC_AUTHREQISSSESBUF_LEN)
		return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
	ofs += (IL_WORD)AddTag(IL_TAG_9F15, Empty, 10, &p_opContext->AuthRequestIssSessionBuf[ofs]);

	// случайное число терминала (9F37)
	GetRandom(TerminalUnpredictableNumber, 4);
	dwLen = sizeof(TerminalUnpredictableNumber);
	if(ofs + AddTag(IL_TAG_9F37, NULL, dwLen, NULL) > UEC_AUTHREQISSSESBUF_LEN)
		return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
    ofs += (IL_WORD)AddTag(IL_TAG_9F37, TerminalUnpredictableNumber, dwLen, &p_opContext->AuthRequestIssSessionBuf[ofs]);

	// штамп времени выполнения операции (9F21) 
	rtcGetTimeStamp(OperationTimeStamp);
	dwLen = sizeof(OperationTimeStamp);
	if(ofs + AddTag(IL_TAG_9F21, NULL, dwLen, NULL) > UEC_AUTHREQISSSESBUF_LEN)
		return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
    ofs += (IL_WORD)AddTag(IL_TAG_9F21, OperationTimeStamp, dwLen, &p_opContext->AuthRequestIssSessionBuf[ofs]);

	// дополнительные данные услуги (9F03) 
	if(p_opContext->OperationCode == UEC_OP_UNLOCK_PUK)
	{	// копируем дополнительные данные (временное значение блока КРП) из предыдущего запроса
		if((RC = TagFind(p_opContext->pAuthRequestBuf, (DWORD)(*p_opContext->pAuthRequestLen), IL_TAG_9F03, &dwLen, &pTmp, 1)) != 0)
			return RC;
		if(dwLen > UEC_AUTHREQISSSESBUF_LEN)
			return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
		cmnMemCopy(&p_opContext->AuthRequestIssSessionBuf[ofs], pTmp, (IL_WORD)dwLen);
		ofs += (IL_WORD)dwLen;
	}
	else
	{	// ПУСТОЕ ЗНАЧЕНИЕ!!!
		if(ofs + AddTag(IL_TAG_9F03, NULL, 0, NULL) > UEC_AUTHREQISSSESBUF_LEN)
			return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
		ofs += (IL_WORD)AddTag(IL_TAG_9F03, Empty, 0, &p_opContext->AuthRequestIssSessionBuf[ofs]);
	}

	// хэш запроса (DF02) - ПУСТОЕ ЗНАЧЕНИЕ!!!
	dwLen = p_opContext->phCrd->ifGostCrypto ? 32 : 20;
	if(ofs + AddTag(IL_TAG_DF02, NULL, dwLen, NULL) > UEC_AUTHREQISSSESBUF_LEN)
		return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
	ofs += (IL_WORD)AddTag(IL_TAG_DF02, Empty, dwLen, &p_opContext->AuthRequestIssSessionBuf[ofs]);

	// информация о терминале (9F1C) - копируем из предыдущего запроса
	if((RC = TagFind(p_opContext->pAuthRequestBuf, (DWORD)(*p_opContext->pAuthRequestLen), IL_TAG_9F1C, &dwLen, &pTmp, 1)) != 0)
		return RC;
	if(dwLen > UEC_AUTHREQISSSESBUF_LEN)
		return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
	cmnMemCopy(&p_opContext->AuthRequestIssSessionBuf[ofs], pTmp, (IL_WORD)dwLen);
	ofs += (IL_WORD)dwLen;

	// сжатое значение открытого ключа терминала (9F1D) - копируем из предыдущего запроса
	if(p_opContext->phCrd->AppVer > UECLIB_APP_VER_10)
	{
		if((RC = TagFind(p_opContext->pAuthRequestBuf, (DWORD)(*p_opContext->pAuthRequestLen), IL_TAG_9F1D, &dwLen, &pTmp, 1)) != 0)
			return RC;
		if(dwLen > UEC_AUTHREQISSSESBUF_LEN)
			return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
		cmnMemCopy(&p_opContext->AuthRequestIssSessionBuf[ofs], pTmp, (IL_WORD)dwLen);
		ofs += (IL_WORD)dwLen;
	}

	if(p_opContext->phCrd->AppStatus == 0x7F)
	{	// при несогласованном состоянии ИД-приложения вместо PAN и СНИЛС добавляем значение CIN(45)
		if((RC = TagFind(p_opContext->pAuthRequestBuf, (DWORD)(*p_opContext->pAuthRequestLen), IL_TAG_45, &dwLen, &pTmp, 0)) != 0)
			return RC;
		if(ofs + AddTag(IL_TAG_45, NULL, dwLen, NULL) > UEC_AUTHREQISSSESBUF_LEN)
			return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
		ofs += (IL_WORD)AddTag(IL_TAG_45, pTmp, dwLen, &p_opContext->AuthRequestIssSessionBuf[ofs]);
	}
	else if(p_opContext->ifAuthOnline)
	{	// PAN(5A) и SNILS(DF27) копируем из предыдущего запроса
		IL_TAG pan_snils_data[] = { IL_TAG_5A,  IL_TAG_DF27 };
		for(i = 0; i < sizeof(pan_snils_data)/sizeof(IL_TAG); i++)
		{
			if((RC = TagFind(p_opContext->pAuthRequestBuf, (DWORD)(*p_opContext->pAuthRequestLen), pan_snils_data[i], &dwLen, &pTmp, 0)) != 0)
				return RC;
			if(ofs + AddTag(pan_snils_data[i], NULL, dwLen, NULL) > UEC_AUTHREQISSSESBUF_LEN)
				return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
			ofs += (IL_WORD)AddTag(pan_snils_data[i], pTmp, dwLen, &p_opContext->AuthRequestIssSessionBuf[ofs]);
		}
	}
	else
	{	// PAN(5A) и SNILS(DF27) добавляем из альтернативного источника
		IL_BYTE pan[10];
		IL_BYTE snils[6];
		IL_WORD data_len;
		// добавляем PAN(5A)
		str2bcdF(p_opContext->PAN, pan, sizeof(pan));
		if(ofs + AddTag(IL_TAG_5A, NULL, 10, NULL) > UEC_AUTHREQISSSESBUF_LEN)
			return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
		ofs += (IL_WORD)AddTag(IL_TAG_5A, pan, 10, &p_opContext->AuthRequestIssSessionBuf[ofs]);
		// добавляем SNILS(DF27)
		if((RC = opCmnReadCardDataRaw(p_opContext, 1, 2, BLOCK_DATA_TLV, 0xDF27, &data_len, snils)) != 0)
			return RC;
		if(ofs + AddTag(IL_TAG_DF27, NULL, data_len, NULL) > UEC_AUTHREQISSSESBUF_LEN)
			return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
		ofs += (IL_WORD)AddTag(IL_TAG_DF27, snils, data_len, &p_opContext->AuthRequestIssSessionBuf[ofs]);
	}

	if(p_opContext->phCrd->AppVer > UECLIB_APP_VER_10)
	{	// общие данные (Utility_Data) - - копируем из предыдущего запроса
		for(i = 0; i < sizeof(utility_data_list)/sizeof(IL_TAG); i++)
		{
			if((RC = TagFind(p_opContext->pAuthRequestBuf, (DWORD)(*p_opContext->pAuthRequestLen), utility_data_list[i], &dwLen, &pTmp, 0)) != 0)
				return RC;
			if(ofs + AddTag(utility_data_list[i], NULL, dwLen, NULL) > UEC_AUTHREQISSSESBUF_LEN)
				return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
			ofs += (IL_WORD)AddTag(utility_data_list[i], pTmp, dwLen, &p_opContext->AuthRequestIssSessionBuf[ofs]);
		}
    }

	// инициализируем размер сформированного буфера параметров 
	p_opContext->AuthRequestIssSessionLen = ofs;
	
	return 0;
}

IL_FUNC IL_WORD opCmnPrepareAppAuthRequest(s_opContext *p_opContext)
{
	IL_WORD RC = 0;
	IL_BYTE AuthRequest[1024*5];
    IL_WORD ofs = 0;
    IL_BYTE OperationTimeStamp[6]; 
    IL_BYTE TerminalUnpredictableNumber[4]; 
	IL_BYTE *pData;
	IL_DWORD dwLen;
	IL_WORD max_len;

	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "Формирование запроса на аутентификацию ИД приложения", ".")

	// проверим валидность входных параметров
	if(!p_opContext) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	// проверим установку в контексте указателей на внешние буфера
	if(!p_opContext->pAuthRequestBuf || !p_opContext->pAuthRequestLen)
	{
		RC = ILRET_OPLIB_EXTERNAL_BUF_NOT_SET;
		goto End;
	}

	// Для операции 'Удалённое управление контентом карты' буфер запроса на аутентификацию ИД приложения 
	// инициализируем значением PAN (для контактного интерфейса) или CIN (для бесконтактного интерфейса) 
	// для инициализации запроса на получение APDU-команд
	if(p_opContext->OperationCode == UEC_OP_REM_MANAGE_CARD_DATA)
	{
		flAppReselect(p_opContext->phCrd);
		RC = flAppReadBlock(p_opContext->phCrd, 0x0009, IL_TAG_5A, AuthRequest, &ofs);
		if(!RC)
		{	// обрамим тегом '5A'
			*p_opContext->pAuthRequestLen = (IL_WORD)AddTag(IL_TAG_5A, AuthRequest, ofs, p_opContext->pAuthRequestBuf);
		}
		else if(RC == ILRET_CRD_GETDATA_WRONG_SEC_CONDITIONS)
		{	// эта ошибка означает, что мы работаем по бесконтактному интерфейсу!
			// в этом случае в запросе на аутентификацию ИД-приложения возвращаем учётный номер карты (CIN)
			RC = flGetCIN(p_opContext->phCrd, /*CIN*/AuthRequest);
			if(!RC)
			{	// обрамим тегом 'CF'
				*p_opContext->pAuthRequestLen = (IL_WORD)AddTag(IL_TAG_CF, AuthRequest, 10, p_opContext->pAuthRequestBuf);
			}
		}
		goto End;
	}

	if(!p_opContext->pRequestHash || !p_opContext->pMetaInfo || 
			(!p_opContext->pExtraData && p_opContext->OperationCode != UEC_OP_UNLOCK_PUK && p_opContext->OperationCode != UEC_OP_UNLOCK_PUK))
	{
		RC = ILRET_OPLIB_EXTERNAL_BUF_NOT_SET;
		goto End;
	}

	// инициализируем максимальный размер выходного буфера запроса на аутентификацию ИД-приложения
	max_len = sizeof(AuthRequest);
	ofs = 0;
	
	// хэш запроса (DF02)
	if(p_opContext->RequestHashCrc != _calculate_crc(p_opContext->pRequestHash, p_opContext->RequestHashLen)) {
		RC = ILRET_OPLIB_INVALID_BUF_CRC_VALUE; goto End;
	}
	if(ofs + AddTag(IL_TAG_DF02, NULL, p_opContext->RequestHashLen, NULL) > max_len) {
		RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
	}
	ofs += (IL_WORD)AddTag(IL_TAG_DF02, p_opContext->pRequestHash, p_opContext->RequestHashLen, &AuthRequest[ofs]);
	
	// идентификатор типа услуги (9F15)
	if(p_opContext->MataInfoCrc != _calculate_crc(p_opContext->pMetaInfo, p_opContext->MetaInfoLen)) {
		RC = ILRET_OPLIB_INVALID_BUF_CRC_VALUE; goto End;
	}
	if((RC = TagFind(p_opContext->pMetaInfo, p_opContext->MetaInfoLen, IL_TAG_9F15, &dwLen, &pData, 0)) != 0)
		goto End;
	if(ofs + AddTag(IL_TAG_9F15, NULL, dwLen, NULL) > max_len) {
		RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
	}
	ofs += (IL_WORD)AddTag(IL_TAG_9F15, pData, dwLen, &AuthRequest[ofs]);
	
	// дополнительные данные услуги (9F03)
	if(p_opContext->OperationCode == UEC_OP_UNLOCK_PUK)
	{	// для разблокировки КРП создаём временное значение блока КРП!!!
		_opCmnCreateTmpPukBlock(p_opContext->PinBlock);
		opCtxSetExtraData(p_opContext, p_opContext->PinBlock, sizeof(p_opContext->PinBlock));
	}
	if(p_opContext->ExtraDataCrc != _calculate_crc(p_opContext->pExtraData, p_opContext->ExtraDataLen)) {
		RC = ILRET_OPLIB_INVALID_BUF_CRC_VALUE; goto End;
	}
	if(ofs + AddTag(IL_TAG_9F03, NULL, p_opContext->ExtraDataLen, NULL) > max_len) {
		RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
	}
	ofs += (IL_WORD)AddTag(IL_TAG_9F03, p_opContext->pExtraData, p_opContext->ExtraDataLen, &AuthRequest[ofs]);

	// штамп времени выполнения операции (9F21)
	rtcGetTimeStamp(OperationTimeStamp);
	dwLen = sizeof(OperationTimeStamp);
	if(ofs + AddTag(IL_TAG_9F21, NULL, dwLen, NULL) > max_len) {
		RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
	}
    ofs += (IL_WORD)AddTag(IL_TAG_9F21, OperationTimeStamp, dwLen, &AuthRequest[ofs]);
	
	// случайное число терминала (9F37)
	GetRandom(TerminalUnpredictableNumber, 4);
	dwLen = sizeof(TerminalUnpredictableNumber);
	if(ofs + AddTag(IL_TAG_9F37, NULL, dwLen, NULL) > max_len) {
		RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
	}
    ofs += (IL_WORD)AddTag(IL_TAG_9F37, TerminalUnpredictableNumber, dwLen, &AuthRequest[ofs]);
	
	// информация о терминале (9F1C)
	if(p_opContext->phCrd->AppVer == UECLIB_APP_VER_10)
	{	// считываем из файла параметров терминала
		IL_BYTE TerminalInfo[32]; 
		PROT_WRITE_EX0(PROT_OPLIB3, "prmGetParameter(IL_PARAM_TERMINAL_INFO)")
		RC = prmGetParameter(IL_PARAM_TERMINAL_INFO, TerminalInfo, &dwLen);
		PROT_WRITE_EX1(PROT_OPLIB3, "prmGetParameter(IL_PARAM_TERMINAL_INFO)=%u", RC)
		if(RC)
			goto End;
		if(ofs + AddTag(IL_TAG_9F1C, NULL, dwLen, NULL) > max_len) {
			RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
		}
		ofs += (IL_WORD)AddTag(IL_TAG_9F1C, TerminalInfo, sizeof(TerminalInfo), &AuthRequest[ofs]);
	}
	else
	{	// извлекаем из сертификата терминала
		IL_BYTE TCert[2048];
		IL_DWORD dwTCertLen;
		IL_BYTE Key[512];
		IL_WORD wKeyLen;
		IL_BYTE buf_9F1C[32] = {0};
		IL_DWORD dw9F1C;
		
		// считаем сертификат открытого ключа терминала (9F1C)
		PROT_WRITE_EX0(PROT_OPLIB3, "flGetCertificate()")
	    RC = flGetCertificate(p_opContext->phCrd, IL_PARAM_CIFDID, p_opContext->phCrd->KeyVer, p_opContext->phCrd->ifGostCrypto, TCert, &dwTCertLen);
		PROT_WRITE_EX1(PROT_OPLIB3, "flGetCertificate()=%u", RC)
		if(RC)
		    goto End;
		if(p_opContext->phCrd->AppStatus != 0x7F)
		{
			PROT_WRITE_EX0(PROT_OPLIB3, "flGetTerminalInfo()");
		    RC = flGetTerminalInfo(p_opContext->phCrd, buf_9F1C, &dw9F1C); 
			PROT_WRITE_EX1(PROT_OPLIB3, "flGetTerminalInfo()=%u", RC);
			if(RC)
			    goto End;
		    if(ofs + AddTag(IL_TAG_9F1C, NULL, dw9F1C, NULL) > max_len) 
		    {
			    RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
		    }
            ofs += (IL_WORD)AddTag(IL_TAG_9F1C, buf_9F1C, dw9F1C, &AuthRequest[ofs]);
		    // добавляем сжатое значение открытого ключа терминала (9F1D)
			PROT_WRITE_EX0(PROT_OPLIB3, "cryptoPublicKeyFromCertificate()");
		    RC = cryptoPublicKeyFromCertificate(TCert, (IL_WORD)dwTCertLen, Key, &wKeyLen, p_opContext->phCrd->ifGostCrypto, 1);
			PROT_WRITE_EX1(PROT_OPLIB3, "cryptoPublicKeyFromCertificate()=%u", RC);
			if(RC)
			    goto End;
		    if(ofs + AddTag(IL_TAG_9F1D, NULL, (IL_DWORD)wKeyLen, NULL) > max_len) 
		    {
			    RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
		    }
            ofs += (IL_WORD)AddTag(IL_TAG_9F1D, Key, (IL_DWORD)wKeyLen, &AuthRequest[ofs]);
        }
        else
        {
            ofs += (IL_WORD)AddTag(IL_TAG_9F1C, buf_9F1C, 0, &AuthRequest[ofs]);
            ofs += (IL_WORD)AddTag(IL_TAG_9F1D, buf_9F1C, 0, &AuthRequest[ofs]);
        }

		// добавляем сведения об идентификационном приложении (C2)
		if(ofs + AddTag(IL_TAG_C2, NULL, sizeof(p_opContext->phCrd->AUC), NULL) > max_len) {
			RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
		}
        ofs += (IL_WORD)AddTag(IL_TAG_C2, p_opContext->phCrd->AUC, sizeof(p_opContext->phCrd->AUC), &AuthRequest[ofs]);
		// добавляем номер версии идентификационного приложения (9F08)
		if(ofs + AddTag(IL_TAG_9F08, NULL, 1, NULL) > max_len) {
			RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
		}
        ofs += (IL_WORD)AddTag(IL_TAG_9F08, &p_opContext->phCrd->AppVer, 1, &AuthRequest[ofs]);
		// добавляем открытый ключ терминала (9F1E)
		if((RC = cryptoPublicKeyFromCertificate(TCert, (IL_WORD)dwTCertLen, Key, &wKeyLen, p_opContext->phCrd->ifGostCrypto, 0)) != 0)
			goto End;
		if(ofs + AddTag(IL_TAG_9F1E, NULL, (IL_DWORD)wKeyLen, NULL) > max_len) {
			RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
		}
        ofs += (IL_WORD)AddTag(IL_TAG_9F1E, Key, (IL_DWORD)wKeyLen, &AuthRequest[ofs]);
		// добавляем Шифрованный блок данных - общий секрета Y для настройки защищенного канала с Поставщиком услуги (DF7F)
		if(ofs + AddTag(IL_TAG_DF7F, NULL, (IL_DWORD)p_opContext->ProviderSessionData.Y_len, NULL) > max_len) {
			RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
		}
        ofs += (IL_WORD)AddTag(IL_TAG_DF7F, p_opContext->ProviderSessionData.Y, (IL_DWORD)p_opContext->ProviderSessionData.Y_len, &AuthRequest[ofs]);
	}

	// Добавляем PAN и СНИЛС 
	if(p_opContext->ifAuthOnline)
	{	
		if(p_opContext->phCrd->AppStatus == 0x7F)
		{	// при несогласованном состоянии ИД-приложения вместо значений PAN(5A) и СНИЛС(DF27) добавляем значение CIN(45)
			if(ofs + AddTag(IL_TAG_45, NULL, 10, NULL) > max_len) {
				RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
			}
			ofs += (IL_WORD)AddTag(IL_TAG_45, p_opContext->phCrd->CIN, 10, &AuthRequest[ofs]);
		}
		else if(p_opContext->phCrd->ifNeedMSE)
		{	//!!! НА КАРТЕ МИКРОН НЕВЕРНЫЙ ФОРМАТ СЕРТИФИКАТА ОТКРЫТОГО КЛЮЧА ИД-ПРИЛОЖЕНИЯ 
			//!!! ПОЭТОМУ ЗНАЧЕНИЯ ПАН ИЗВЛКАЕМ ИЗ КОНТЕКСТА, А СНИЛС ПОКА ОПРЕДЕЛЯЕМ КОНСТАНТОЙ
			IL_BYTE Pan[10];
			IL_BYTE Snils[] = { 0x00,0x28,0x26,0x24,0x61,0x5F };
			str2bcdF(p_opContext->PAN, Pan, 10);
			if(ofs + AddTag(IL_TAG_5A, NULL, 10, NULL) > max_len) {
				RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
			}
			ofs += (IL_WORD)AddTag(IL_TAG_5A, Pan, 10, &AuthRequest[ofs]);
			if(ofs + AddTag(IL_TAG_DF27, NULL, 6, NULL) > max_len) {
				RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
			}
			ofs += (IL_WORD)AddTag(IL_TAG_DF27, Snils, 6, &AuthRequest[ofs]);
		}
		else if(p_opContext->OperationCode == UEC_OP_UNLOCK_PUK)
		{	// для операции 'Разблокировка КРП' значения PAN и SNULS извлекаем из сертификата открытого ключа ИД-приложения
			IL_BYTE *p7F4E;
			IL_DWORD dw7F4E;
			IL_BYTE *p5F20;
			IL_DWORD dw5F20;
			IL_BYTE Cert[2048];
			IL_WORD wCertLen;
			IL_BYTE *p7F21;
			IL_DWORD dw7F21;
			IL_BYTE *pCert = Cert;
			// считываем с карты сертификат ИД-приложения
			PROT_WRITE_EX0(PROT_OPLIB3, "flGetAppPubKey()");
			RC = flGetAppPubKey(p_opContext->phCrd, Cert, &wCertLen);
			PROT_WRITE_EX1(PROT_OPLIB3, "flGetAppPubKey()=%u", RC);
			if(RC)
				goto End;
			// пытаемся получить указатель на данные обрамляющего тега 7F21
			if((RC = TagFind(pCert, wCertLen, IL_TAG_7F21, &dw7F21, &p7F21, 0)) == 0)
			{
				pCert     = p7F21;
				wCertLen = (IL_WORD)dw7F21;
			}
			else if(RC != ILRET_DATA_TAG_NOT_FOUND)
				goto End;
			// извлекаем сертифицируемые данные из сертификата открытого ключа
			if((RC = TagFind(pCert, wCertLen, IL_TAG_7F4E, &dw7F4E, &p7F4E, 0)) != 0)
				goto End;
			// получаем указатель на конкатенированный элемент 5F20 (PAN+SNILS)
			if((RC = TagFind(p7F4E, dw7F4E, IL_TAG_5F20, &dw5F20, &p5F20, 0)) != 0) 
				goto End;
			// добавляем PAN(5A) 
			if(ofs + AddTag(IL_TAG_5A, NULL, 10, NULL) > max_len) {
				RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
			}
			ofs += (IL_WORD)AddTag(IL_TAG_5A, p5F20, 10, &AuthRequest[ofs]);
			// добавляем СНИЛС(DF27)
			if(ofs + AddTag(IL_TAG_DF27, NULL, 6, NULL) > max_len) {
				RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
			}
			ofs += (IL_WORD)AddTag(IL_TAG_DF27, p5F20+10, 6, &AuthRequest[ofs]);
		} 
		else
		{	// PAN и SNILS добавляем из альтернативного источника
			IL_BYTE pan[10];
			IL_BYTE snils[6];
			IL_WORD data_len;
			// добавляем PAN (5A)
			str2bcdF(p_opContext->PAN, pan, sizeof(pan));
			if(ofs + AddTag(IL_TAG_5A, NULL, 10, NULL) > max_len) {
				RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
			}
			ofs += (IL_WORD)AddTag(IL_TAG_5A, pan, 10, &AuthRequest[ofs]);
			// добавляем SNILS (DF27)
			PROT_WRITE_EX0(PROT_OPLIB3, "opCmnReadCardDataRaw()")
			RC = opCmnReadCardDataRaw(p_opContext, 1, 2, BLOCK_DATA_TLV, 0xDF27, &data_len, snils);
			PROT_WRITE_EX1(PROT_OPLIB3, "opCmnReadCardDataRaw()=%u", RC)
			if(RC)
				goto End;
			if(ofs + AddTag(IL_TAG_DF27, NULL, data_len, NULL) > max_len) {
				RC = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
			}
			ofs += (IL_WORD)AddTag(IL_TAG_DF27, snils, data_len, &AuthRequest[ofs]);
		}
	}

	if(p_opContext->OperationCode == UEC_OP_PROVIDE_SERVICE) 
	{	// полноформатный запрос на аутентификацию ИД-приложения для оказания услуги формируем в буфере AuthRequest контекста операции
		PROT_WRITE_EX0(PROT_OPLIB3, "flAppAuthRequest()")
		RC = flAppAuthRequest(p_opContext->phCrd, p_opContext->ifAuthOnline, AuthRequest, ofs, p_opContext->pAuthRequestBuf, p_opContext->pAuthRequestLen);
		PROT_WRITE_EX1(PROT_OPLIB3, "flAppAuthRequest()=%u", RC)
	}
	else
		// копируем сформированный запрос в буфер AuthRequest контекста операции для установления защищённой сессии с эмитентом
		cmnMemCopy(p_opContext->pAuthRequestBuf, AuthRequest, ofs);
	if(!RC)
	{	// подсчитаем контрольную сумму данных запроса на аутентификацию ИД-приложения
		p_opContext->AuthRequestCrc = _calculate_crc(p_opContext->pAuthRequestBuf, *p_opContext->pAuthRequestLen);
		// сформируем в контексте статические параметры возможного Online-запроса на аутентификацию ИД-приложения для установки защищённой сессии с эмитентом
		PROT_WRITE_EX0(PROT_OPLIB3, "_opCmnSaveAppAuthRequestIssSession()")
		RC = _opCmnSaveAppAuthRequestIssSession(p_opContext);
		PROT_WRITE_EX1(PROT_OPLIB3, "_opCmnSaveAppAuthRequestIssSession()=%u", RC)
	}

End:
	OP_CMN_DISPLAY_LINE(p_opContext, (RC) ? "ОШИБКА" : "OK")
	if(!RC && p_opContext->OperationCode == UEC_OP_PROVIDE_SERVICE)
		PROT_WRITE_EX2(PROT_OPLIB2, "OUT: AuthRequest[%u]=%s", *p_opContext->pAuthRequestLen, bin2hex((IL_CHAR*)p_opContext->TmpBuf, p_opContext->pAuthRequestBuf, *p_opContext->pAuthRequestLen));
	PROT_WRITE_EX1(PROT_OPLIB1, "opCmnPrepareAppAuthRequest()=%u", RC)

	return RC;
}

IL_FUNC IL_WORD opCmnCheckAppAuthResponse(s_opContext *p_opContext, IL_WORD *pAuthResult)
{
	IL_WORD RC;

	OP_CMN_DISPLAY_LINE(p_opContext, "                      ")
	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "Обработка результатов аутентификации ИД-приложения", ".")

	if(!p_opContext || !pAuthResult || !p_opContext->pAuthResponseData)
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: AuthResponse[%u]=%s", p_opContext->AuthResponseLen, bin2hex(p_opContext->TmpBuf, p_opContext->pAuthResponseData, p_opContext->AuthResponseLen));
		PROT_WRITE_EX0(PROT_OPLIB3, "flAppAuthCheckResponse()")
		// проверим результаты аутентификации ИД-приложения
		RC = flAppAuthCheckResponse(p_opContext->phCrd, 
								p_opContext->pAuthRequestBuf, *p_opContext->pAuthRequestLen, 
								p_opContext->pAuthResponseData, p_opContext->AuthResponseLen, pAuthResult);
		PROT_WRITE_EX1(PROT_OPLIB3, "flAppAuthCheckResponse()=%u", RC)
	}

	if(RC)
		sprintf((IL_CHAR*)p_opContext->TmpBuf, "ОШИБКА");
	else
		sprintf((IL_CHAR*)p_opContext->TmpBuf, "AuthResult=%u", *pAuthResult);
	OP_CMN_DISPLAY_LINE(p_opContext, ((IL_CHAR*)p_opContext->TmpBuf))

	// сохраним код ответа на аутентификацию ИД-приложения в контексте ИБТ
	if(!RC) 
	{
		p_opContext->AuthResult = *pAuthResult;	
		PROT_WRITE_EX1(PROT_OPLIB2, "OUT: AuthResult=%u", *pAuthResult)
		
		if(p_opContext->AuthResult == 400 && p_opContext->OperationCode == UEC_OP_PROVIDE_SERVICE)
		{   // Перед переходом в режим удалённого управления контентом карты необходимо провести повторное селектирование!!! 
			RC = flAppReselect(p_opContext->phCrd);
		}
	}

	return RC;
}


IL_FUNC IL_WORD opCmnChangePinPuk(s_opContext *p_opContext)
{
	IL_WORD RC = 0;
	IL_BYTE NewPinBlock[8];

	PROT_WRITE_EX0(PROT_OPLIB3, "opCmnChangePinPuk()")
	
	// проверим валидность входных параметров
	if(!p_opContext || !p_opContext->pNewPinStr || !p_opContext->pConfirmPinStr) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	// проверим подтверждающий пароль
	if((p_opContext->pNewPinStr != p_opContext->pConfirmPinStr) && (cmnStrCmp(p_opContext->pNewPinStr, p_opContext->pConfirmPinStr))) {
		RC = ILRET_OPLIB_INVALID_CONFIRM_PIN; goto End;
	}

	// изменим пароль
	str2pin(p_opContext->pNewPinStr, NewPinBlock);
	if(p_opContext->OperationCode == UEC_OP_CHANGE_PIN || p_opContext->OperationCode == UEC_OP_UNLOCK_PIN)
	{	// смена|разблокировка PIN
		if(p_opContext->OperationCode == UEC_OP_UNLOCK_PIN && p_opContext->phCrd->ifNeedMSE)
		{	//!!!MICRON SPECIFIC!!!
			PROT_WRITE_EX0(PROT_OPLIB3, "flAppUnlockPIN()")
			RC = flAppUnlockPIN(p_opContext->phCrd, p_opContext->PinNum);
			PROT_WRITE_EX1(PROT_OPLIB3, "flAppUnlockPIN()=%u", RC)
			if(RC)
				return RC;
		}
		PROT_WRITE_EX0(PROT_OPLIB3, "flAppChangePIN()")
		RC = flAppChangePIN(p_opContext->phCrd, p_opContext->PinNum, NewPinBlock);
		PROT_WRITE_EX1(PROT_OPLIB3, "flAppChangePIN()=%u", RC)
	}
	else
	{	// смена КРП
		PROT_WRITE_EX0(PROT_OPLIB3, "flAppChangePUK()")
		RC = flAppChangePUK(p_opContext->phCrd, NewPinBlock);
		PROT_WRITE_EX1(PROT_OPLIB3, "flAppChangePUK()=%u", RC)
	}

End:
	PROT_WRITE_EX1(PROT_OPLIB3, "opCmnChangePinPuk()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCmnGetPAN(s_opContext *p_opContext, IL_CHAR *out_str20)
{
	IL_WORD RC;
	IL_BYTE out[30];
	IL_WORD out_len;
	IL_DWORD data_len;
	IL_BYTE *pPAN;
	
	if(!p_opContext || !out_str20) return ILRET_OPLIB_INVALID_ARGUMENT;

	// считываем с карты PAN
	if((RC = flAppReadBlock(p_opContext->phCrd, 0x0009, IL_TAG_5A, out, &out_len)) != 0) 
		return RC;
	TagFind(out, out_len, IL_TAG_5A, &data_len, &pPAN, 0);
	if(pPAN == NULL)
		return ILRET_OPLIB_CORRUPTED_TLV_DATA;
	
	// конвертируем в символьную строку
	bcd2str(pPAN, (IL_WORD)data_len, out_str20);

	return 0;
}

IL_FUNC IL_WORD _opCmnGetAppSystemInfoTagId(s_opContext *p_opContext, IL_BYTE *resp, IL_WORD resp_len, 
											IL_WORD TagId, IL_CHAR *out_str)
{
	IL_WORD RC;
	IL_DWORD data_len;
	IL_BYTE *pData;
	IL_DWORD NumTags;
	const IL_TAG TAG_PATH_5F24[] = {IL_TAG_6F,IL_TAG_A5,IL_TAG_5F24};
	const IL_TAG TAG_PATH_5F25[] = {IL_TAG_6F,IL_TAG_A5,IL_TAG_5F25};
	const IL_TAG TAG_PATH_9F08[] = {IL_TAG_6F,IL_TAG_A5,IL_TAG_9F08};

	const IL_TAG TAG_PATH_V11_5F24[] = {IL_TAG_64,IL_TAG_5F24};
	const IL_TAG TAG_PATH_V11_5F25[] = {IL_TAG_64,IL_TAG_5F25};
	const IL_TAG TAG_PATH_V11_9F08[] = {IL_TAG_64,IL_TAG_9F08};

	
	const IL_TAG *pTagPath;

	if(!p_opContext || !resp || !out_str) return ILRET_OPLIB_INVALID_ARGUMENT;

	if(TagId == 0x9F08)
		pTagPath = p_opContext->phCrd->AppVer == UECLIB_APP_VER_10 ? TAG_PATH_9F08 : TAG_PATH_V11_9F08;
	else if(TagId == 0x5F25)
		pTagPath = p_opContext->phCrd->AppVer == UECLIB_APP_VER_10 ? TAG_PATH_5F25 : TAG_PATH_V11_5F25;
	else if(TagId == 0x5F24)
		pTagPath = p_opContext->phCrd->AppVer == UECLIB_APP_VER_10 ? TAG_PATH_5F24 : TAG_PATH_V11_5F24;
	else
		return ILRET_OPLIB_INVALID_ARGUMENT;

	NumTags = p_opContext->phCrd->AppVer == UECLIB_APP_VER_10 ? 3 : 2;
	if((RC = TagFindByPath(resp, resp_len, pTagPath, NumTags, &data_len, &pData, 0)) != 0)
		return RC;
	if(pData == NULL)
		return ILRET_OPLIB_CORRUPTED_TLV_DATA;

	// конвертируем в символьную строку
	bcd2str(pData, (IL_WORD)data_len, out_str);

	return 0;
}

IL_FUNC IL_WORD _opCmnGetAppSystemInfo(s_opContext *p_opContext, IL_BYTE *resp, IL_WORD resp_len)
{
	IL_WORD RC;

	if((RC = _opCmnGetAppSystemInfoTagId(p_opContext, resp, resp_len, IL_TAG_9F08, p_opContext->AppVersion)) != 0)   
		return RC;
	if((RC = _opCmnGetAppSystemInfoTagId(p_opContext, resp, resp_len, IL_TAG_5F25, p_opContext->EffectiveDate)) != 0) 
		return RC;
	if((RC = _opCmnGetAppSystemInfoTagId(p_opContext, resp, resp_len, IL_TAG_5F24, p_opContext->ExpirationDate)) != 0)	
		return RC;

	return 0;
}

IL_FUNC IL_WORD _opCmnGetIcChallenge(s_opContext *p_opContext)
{
	IL_WORD RC = 0;
    IL_DWORD dwLen = 0;
    IL_BYTE *pData = NULL;

	if(p_opContext->AuthRequestCrc != _calculate_crc(p_opContext->pAuthRequestBuf, *p_opContext->pAuthRequestLen))
		RC = ILRET_OPLIB_INVALID_BUF_CRC_VALUE;
	else
	{
		TagFind(p_opContext->pAuthRequestBuf, *p_opContext->pAuthRequestLen, IL_TAG_DF71, &dwLen, &pData, 0);
		if(pData == NULL)
			RC = ILRET_DATA_TAG_NOT_FOUND;
		else
			cmnMemCopy(p_opContext->sess_in.IcChallenge, pData, dwLen);
	}

	if(!RC)
		PROT_WRITE_EX2(PROT_OPLIB2, "OUT: IcChallenge[%lu]=%s", dwLen, bin2hex(p_opContext->TmpBuf, pData, dwLen));

	return RC;
}

//
IL_FUNC IL_WORD opCmnUnlockTmpPUK(s_opContext *p_opContext)
{
	IL_WORD RC = 0;

	// изменим КРП на временное значение и разблокируем КРП посредством выполнения сформированных хостом пакета APDU-комманд  
	if((RC = opApiRunApduPacket(p_opContext)) != 0)
		goto End;
	
	// восстанавливаем защищённое соединение заново
	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "Восстановление защищённого соединения", ".")
	// селектируем приложение заново и аутентифицируем терминал 
	PROT_WRITE_EX0(PROT_OPLIB3, "opCmnRestoreCryptoSession()");
	RC = opCmnRestoreCryptoSession(p_opContext);
	PROT_WRITE_EX1(PROT_OPLIB3, "opCmnRestoreCryptoSession()=%u", RC);
	if(RC)
		goto End;
	
	// верифицируем гражданина на временном КРП
	PROT_WRITE_EX0(PROT_OPLIB3, "flAppCitizenVerification()");
	RC = flAppCitizenVerification(p_opContext->phCrd, IL_KEYTYPE_05_PUK, p_opContext->PinBlock, &p_opContext->PinTriesLeft);
	PROT_WRITE_EX1(PROT_OPLIB3, "flAppCitizenVerification()=%u", RC);

End:
	OP_CMN_DISPLAY_LINE(p_opContext, RC ? "ОШИБКА" : "OK")
	return RC;
}

IL_FUNC IL_WORD _opCmnCreateTmpPukBlock(IL_BYTE *out_PukBlock8)
{
	IL_BYTE i, b1, b2;

	GetRandom(out_PukBlock8, 8);

	out_PukBlock8[0] = 0x2C;
	out_PukBlock8[7] = 0xFF;

	for(i = 1; i < 7; i++)
	{
		if((b1 = out_PukBlock8[i] >> 4) > 0x09)
			b1 -= 0x09;
		if((b2 = out_PukBlock8[i] & 0x0F) > 0x09)
			b2 -= 0x09;
		out_PukBlock8[i] = (IL_BYTE)((b1 << 4) | b2);
	}

	return 0;
}

IL_FUNC IL_WORD opCmnReadCardData(s_opContext *p_opContext, IL_CHAR *in_Str, IL_CHAR *out_Str,IL_WORD *out_StrLen)
{
	IL_WORD RC = 0;
	IL_CHAR *pStr;
	IL_INT sector, block, tag, num_to_read;
	IL_CHAR	out[UEC_DATA_OUT_MAX_LEN*2+15];
	IL_BYTE RawMode;
	IL_BYTE data[UEC_DATA_OUT_MAX_LEN];
	IL_WORD data_len;
	IL_WORD max_len;
	IL_CHAR header[15];
	BLOCK_DESCR *pBlockDescr; 

	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "Чтение прикладных данных карты", ".")

	// проверим валидность входных аргументов
	if(!p_opContext || !in_Str || !out_Str || !out_StrLen || *out_StrLen == 0) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	// размер возвращаемой строки ограничен 
	max_len = *out_StrLen - 1;

	for(pStr = in_Str, *out_Str = 0, *out_StrLen = 0; ; )
	{
		// определим необходимость чтения данных в "сыром" формате
		RawMode = *pStr == 'x';
		if(RawMode)
			pStr++;

		// определим тип файла\блока данных
		sscanf(pStr, "%u-%u", &sector, &block);
		if((RC = opDescrGetBlock(p_opContext, (IL_WORD)sector, (IL_WORD)block, &pBlockDescr)) != 0)
			break;

		// определим сектор, блок и тэг элемента считываемых данных
		num_to_read = 0;
		tag = 0;
		if(pBlockDescr->FileType == BLOCK_DATA_TLV || pBlockDescr->FileType == BLOCK_DATA_BINTLV)
		{
			sscanf(pStr, "%u-%u-%X", &sector, &block, &tag);
			PROT_WRITE_EX3(PROT_OPLIB2, "ReadCardData(%d-%d-%04X)", sector, block, tag)
		}
		else if(pBlockDescr->FileType == BLOCK_DATA_BIN || pBlockDescr->FileType == BLOCK_DATA_RECORD)
		{
			sscanf(pStr, "%u-%u-%u-%u", &sector, &block, &tag, &num_to_read);
			PROT_WRITE_EX4(PROT_OPLIB2, "ReadCardData(%d-%d-%d-%d)", sector, block, tag, num_to_read)
		}
		else 
		{
			RC = ILRET_OPLIB_INVALID_FILE_TYPE; break;
		}

		// специальный случай чтения фотографии
		if(sector == 1 && block == 3 && !RawMode)
		{	
			if(p_opContext->pPhotoBuf && p_opContext->pPhotoLen && *p_opContext->pPhotoLen > 0)
			{	// считываем данные  
				if((RC = opApiReadPhoto(p_opContext, NULL, NULL)) != 0)
					break;
			}
			goto Next;
		}

		// считываем значение элемент данного
		if(!RawMode)
		{	// по дескриптору с конвертацией данных
			if((RC = opGetDataByTagId(p_opContext, (IL_WORD)sector, (IL_WORD)block, (IL_WORD)tag, out)) != 0)
				break;
		}
		else
		{	// сырые данные без конвертации	
			data_len = (IL_WORD)num_to_read;
			if((RC = opCmnReadCardDataRaw(p_opContext, (IL_WORD)sector, (IL_WORD)block, pBlockDescr->FileType, (IL_WORD)tag, &data_len, data)) != 0)
				break;
			// считанные данные преобразуем в hex-строку
			bin2hex(out, data, data_len);
		}

		// форматируем в выходную строку
		if(pBlockDescr->FileType == BLOCK_DATA_TLV || pBlockDescr->FileType == BLOCK_DATA_BINTLV)
			sprintf(header, "%s%d-%d-%04X=", (RawMode ? "x" : ""), sector, block, tag);
		else if(RawMode)
			sprintf(header, "x%d-%d-%u-%u=", sector, block, tag, num_to_read);
		else
			sprintf(header, "%d-%d-%u=", sector, block, tag);

		// контролируем возможное превышение максимального размера выходного буфера 
		if(*out_StrLen + strlen(header) + strlen(out) + 1 > max_len) {
			RC = ILRET_OPLIB_CARDDATA_BUF_IS_OVER; break;
		}

		sprintf(&out_Str[*out_StrLen], "%s%s\n", header, out);

		*out_StrLen += (IL_WORD)(strlen(header) + strlen(out) + 1);

Next:	// позиционируем указатель на следующую подстроку
		pStr = strchr(pStr, ';');
		
		if(!pStr++ || *pStr == 0)
			break;
	}

End:
	OP_CMN_DISPLAY_LINE(p_opContext, RC ? "ОШИБКА" : "OK")
	return RC;
}

IL_FUNC IL_BYTE* _bin2apduin(IL_BYTE* bin, IL_APDU_PACK_ELEM *pApduElem)
{
	IL_BYTE *pB;

	// проверим входные аргументы
	if(!bin || !pApduElem) return NULL;

	// обнулим команду
	cmnMemClr((IL_BYTE*)pApduElem, sizeof(IL_APDU_PACK_ELEM));

	// настроим указатель на входной буфер
	pB = bin;

	// копируем заголовка команды (4 байта)  
	cmnMemCopy(pApduElem->Apdu.Cmd, pB, 4);
	pB += 4;

	// конвертируем LengthIn (4 байта)
	pB = bin2dw(pB, &pApduElem->Apdu.LengthIn); 

	// конвертируем LengthExpected (4 байта)
	pB = bin2dw(pB, &pApduElem->Apdu.LengthExpected);

	// копируем DataIn
	cmnMemCopy(pApduElem->Apdu.DataIn, pB, (IL_WORD)pApduElem->Apdu.LengthIn);
	pB += pApduElem->Apdu.LengthIn;

	// конвертируем длину массива допустимых ответов (1 байт)
	pApduElem->allowed_res_len = *pB;
	pB += 1;

	// проверяем допустимость 
	if(pApduElem->allowed_res_len > ALLOWED_RES_MAX)
		return NULL;

	// копируем допустимые ответы
	cmnMemCopy(pApduElem->allowed_res, pB, pApduElem->allowed_res_len*2);
	pB += (pApduElem->allowed_res_len*2);

	return pB;
}

//
#define MAX_LOCK_PIN_TRIES	5
IL_WORD _LockPin(s_opContext *p_opContext, IL_BYTE PinNum)
{
	IL_WORD RC, tries;

	for(RC = tries = 0; RC != ILRET_CRD_VERIFY_PASSWORD_BLOCKED && tries < MAX_LOCK_PIN_TRIES;  tries++)
	{
		if((RC = flAppCitizenVerification(p_opContext->phCrd, PinNum, p_opContext->PinBlock, &p_opContext->PinTriesLeft)) != 0)
		{
			if(!RC)
			{	// изменим ПИН-блок
				p_opContext->PinBlock[1] = ((p_opContext->PinBlock[1] & 0x0F) < 9) ?  p_opContext->PinBlock[1]+1 : 0x11;
			}
			else if(RC == ILRET_CRD_VERIFY_WRONG_PASSWORD_PRESENTED)
			{	// ЗАПЛАТКА!!! ПОСКОЛЬКУ ПРИ ЛЮБОЙ ОШИБКЕ НА КАРТЕ ВЕРСИИ 1.0 СБРАСЫВАЕТСЯ КРИПТОСЕССИЯ
				// перед повторным предъявлением ПИН необходимо заново селектировать приложение и провести аутентификацию терминала 
				if(p_opContext->phCrd->AppVer == UECLIB_APP_VER_10 && (RC = opCmnRestoreCryptoSession(p_opContext)) != 0)
					return RC;
			}
			else if(RC != ILRET_CRD_VERIFY_PASSWORD_BLOCKED)
			{	// возвратим ошибку
				return RC;
			}
		}
	}

	if(tries >= MAX_LOCK_PIN_TRIES)
		return ILRET_OPLIB_CARD_LOCK_ERROR;

	return 0;
}

IL_FUNC IL_BYTE _calculate_crc(IL_BYTE *buf, IL_WORD buf_len)
{
	IL_WORD i;
	IL_BYTE crc = 0;

	for(i = 0; i < buf_len; i++)
		crc ^= buf[i];

	return crc;
}

IL_FUNC IL_WORD _apduout2bin(IL_APDU_PACK_ELEM *apdu, IL_BYTE *bin, IL_WORD *bin_len, IL_WORD max_len)
{
	// проверим валидность входных параметров
	if(!apdu || !bin || !bin_len) 		
		return ILRET_OPLIB_INVALID_ARGUMENT;

	// конвертируем 4 байта Cmd
	if(*bin_len + 4 > max_len)
		return ILRET_OPLIB_APDUPACKET_BUF_IS_OVER;
	cmnMemCopy(&bin[*bin_len], apdu->Apdu.Cmd, 4);
	*bin_len += 4;

	// конвертируем SW1 (1 байт)
	if(*bin_len + 1 > max_len)
		return ILRET_OPLIB_APDUPACKET_BUF_IS_OVER;
	bin[*bin_len] = apdu->Apdu.SW1;
	*bin_len += 1;

	// конвертируем SW2 (1 байт)
	if(*bin_len + 1 > max_len)
		return ILRET_OPLIB_APDUPACKET_BUF_IS_OVER;
	bin[*bin_len] = apdu->Apdu.SW2;
	*bin_len += 1;

	// конвертируем LengthOut (4 байта)
	if(*bin_len + sizeof(apdu->Apdu.LengthOut) > max_len)
		return ILRET_OPLIB_APDUPACKET_BUF_IS_OVER;
	dw2bin(apdu->Apdu.LengthOut, &bin[*bin_len]);
	*bin_len += sizeof(apdu->Apdu.LengthOut);

	// конвертируем выходные данные
	if(apdu->Apdu.LengthOut)
	{
		if(*bin_len + apdu->Apdu.LengthOut > max_len)
			return ILRET_OPLIB_APDUPACKET_BUF_IS_OVER;
		cmnMemCopy(&bin[*bin_len], apdu->Apdu.DataOut, (IL_WORD)apdu->Apdu.LengthOut);
		*bin_len += (IL_WORD)apdu->Apdu.LengthOut;
	}

	return 0;
}








