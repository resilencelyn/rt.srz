#include "FuncLib.h"
#include "CardLib.h"
#include "TLV.h"
#include "Tag.h"
#include "FileID.h"
#include "HAL_Parameter.h"
#include "HAL_Protocol.h"
#include "HAL_Common.h"
#include "HAL_Crypto.h"
#include "KeyType.h"
#include "il_error.h"
#include "HAL_Rtc.h"
#include "CertType.h"

#ifdef SM_SUPPORT
	#include "SmLib.h"
#endif

#ifndef PROT_IGNORE
IL_CHAR protBuf[1024*5];
#endif//PROT_IGNORE

//FCI tags
const IL_TAG TAG_PATH_9F08[] = {IL_TAG_6F,IL_TAG_A5,IL_TAG_9F08};
const IL_TAG TAG_PATH_E0[]   = {IL_TAG_6F,IL_TAG_A5,IL_TAG_E0};
const IL_TAG TAG_PATH_5F24[] = {IL_TAG_6F,IL_TAG_A5,IL_TAG_5F24};
const IL_TAG TAG_PATH_5F25[] = {IL_TAG_6F,IL_TAG_A5,IL_TAG_5F25};
const IL_TAG TAG_PATH_C2[]   = {IL_TAG_6F,IL_TAG_A5,IL_TAG_C2};
const IL_TAG TAG_PATH_E2[]   = {IL_TAG_6F,IL_TAG_A5,IL_TAG_E2};

//FMD tags
const IL_TAG TAG_PATH_V11_9F08[] = {IL_TAG_64,IL_TAG_9F08};
const IL_TAG TAG_PATH_V11_E0[]   = {IL_TAG_64,IL_TAG_E0};
const IL_TAG TAG_PATH_V11_5F24[] = {IL_TAG_64,IL_TAG_5F24};
const IL_TAG TAG_PATH_V11_5F25[] = {IL_TAG_64,IL_TAG_5F25};
const IL_TAG TAG_PATH_V11_C2[]   = {IL_TAG_64,IL_TAG_C2};
const IL_TAG TAG_PATH_V11_8F[]	 = {IL_TAG_64,IL_TAG_8F};
const IL_TAG TAG_PATH_V11_E2[]   = {IL_TAG_64,IL_TAG_E2};

const IL_TAG TAG_PATH_V11_7F4E[] = {IL_TAG_7F21,IL_TAG_7F4E};
const IL_TAG TAG_PATH_V11_5F37[] = {IL_TAG_7F21,IL_TAG_5F37};
const IL_TAG TAG_PATH_V11_5F20[] = {IL_TAG_7F21,IL_TAG_7F4E,IL_TAG_5F20};


IL_BYTE ID_APP_AID[] = {0xA0, 0x00, 0x00, 0x04, 0x32, 0x90, 0x00, 0x01};
IL_BYTE ISD_AID[] = {0xA0, 0x00, 0x00, 0x04, 0x32, 0x55, 0x45, 0x43, 0x49, 0x53, 0x44};


IL_FUNC IL_RETCODE flInitReader(IL_CARD_HANDLE* phCrd, IL_READER_SETTINGS ilRdrSettings)
{
	IL_WORD RC;
    
	PROT_WRITE_EX0(PROT_FUNCLIB1, "flInitReader()")
	RC = crInit(phCrd->hRdr, ilRdrSettings);
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flInitReader()=%u", RC)
	
	return RC;
}

IL_FUNC IL_RETCODE flDeinitReader(IL_CARD_HANDLE* phCrd)
{
	IL_WORD RC;

	PROT_WRITE_EX0(PROT_FUNCLIB1, "flDeinitReader()")
    RC = crDeinit(phCrd->hRdr); 
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flDeinitReader()=%u", RC)
	
	return RC;
}

static IL_FUNC IL_RETCODE _MSEReset(IL_CARD_HANDLE* phCrd)
{
	if(phCrd->ifNeedMSE)
	{
		IL_RETCODE ilRet;
	    IL_BYTE sanction_id = (phCrd->ifGostCrypto)? 0x15 : 0x16;
	    
		if((ilRet = clMSE(phCrd, 0x81, 0xB6, 0x83, 0x01, sanction_id)) != 0)
			return ilRet;

		if((ilRet = clMSE(phCrd, 0x81, 0xA4, 0x83, 0x01, sanction_id)) != 0)
			return ilRet;

		if((ilRet = clMSE(phCrd, 0x31, 0xB4, 0x83, 0x01, sanction_id)) != 0)
			return ilRet;

		if((ilRet = clMSE(phCrd, 0x31, 0xB8, 0x83, 0x01, sanction_id)) != 0)
			return ilRet;
	}

	return 0;
}

IL_FUNC IL_RETCODE flGetCertificate(IL_CARD_HANDLE *phCrd, IL_WORD ilParam, IL_BYTE KeyVer, IL_BYTE ifGostCrypto, IL_BYTE* pCertBuf, IL_DWORD* pdwCertLen)
{
	IL_WORD RC;

	PROT_WRITE_EX0(PROT_FUNCLIB1, "flGetCertificate()")

	#ifdef SM_SUPPORT
		PROT_WRITE_EX0(PROT_FUNCLIB3, "smGetCertificate()")
		PROT_WRITE_EX3(PROT_FUNCLIB2, "IN: Param=%u KeyVer=%u GostCrypto=%u", ilParam, KeyVer, ifGostCrypto)
		RC = smGetCertificate(phCrd->hCrypto, ilParam, KeyVer, ifGostCrypto, pCertBuf, pdwCertLen);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "smGetCertificate()=%u", RC)
	#else
		PROT_WRITE_EX0(PROT_FUNCLIB3, "prmGetParameterKeyVer()")
		PROT_WRITE_EX3(PROT_FUNCLIB2, "IN: ParamId=%u KeyVer=%u Gost=%u", ilParam, KeyVer, ifGostCrypto)
		RC = prmGetParameterKeyVer(ilParam, KeyVer, ifGostCrypto, pCertBuf, pdwCertLen);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "smGetCertificate()=%u", RC)
	#endif

    if(RC == 0 && pCertBuf && pdwCertLen)
		PROT_WRITE_EX2(PROT_FUNCLIB2, "OUT: Cert[%lu]=%s", *pdwCertLen, bin2hex(protBuf, pCertBuf, *pdwCertLen));
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flGetCertificate()=%u", RC)
	return RC;
}

IL_FUNC IL_RETCODE flGetTerminalInfo(IL_CARD_HANDLE *phCrd, IL_BYTE *pOut, IL_DWORD *pdwOutLen)
{
	IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_FUNCLIB1, "flGetTerminalInfo()")

	if(!phCrd || !pOut || !pdwOutLen || phCrd->AppVer < UECLIB_APP_VER_10) {
		ilRet = ILRET_SYS_INVALID_ARGUMENT; goto End;
	}

	if(phCrd->AppVer == UECLIB_APP_VER_10)
	{	// получаем из внешнего файла настроек
		IL_BYTE term_info[256];
		PROT_WRITE_EX0(PROT_FUNCLIB3, "prmGetParameter(IL_PARAM_TERMINAL_INFO)")
		ilRet = prmGetParameter(IL_PARAM_TERMINAL_INFO, term_info, pdwOutLen);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "prmGetParameter(IL_PARAM_TERMINAL_INFO)=%u", ilRet)
		if(ilRet)
			goto End;
		cmnMemCopy(pOut, term_info, (IL_WORD)(*pdwOutLen));
	}
	else
	{	// извлекаем из сертификата открытого ключа терминала
		IL_BYTE  certGost[2048];
		IL_DWORD dwCertGostLen;
		IL_BYTE  *pTermInfoGost = NULL;
		IL_DWORD dwTermInfoGostLen = 0;
		IL_BYTE  certRsa[2048];
		IL_DWORD dwCertRsaLen;
		IL_BYTE  *pTermInfoRsa = NULL;
		IL_DWORD dwTermInfoRsaLen = 0;

		// длина элемента данных TerminalInfo должна равняться IL_TERM_INFO_LEN (4 + 4 + 8 = 16 байт)
		*pdwOutLen = 16;

		// получаем сертификат открытого ключа терминала GOST
		ilRet = flGetCertificate(phCrd, IL_PARAM_CIFDID, phCrd->KeyVer, 1, certGost, &dwCertGostLen);
		if(ilRet && ilRet != ILRET_PARAM_CERTIFICATE_NOT_FOUND)
			return ilRet;
		if(!ilRet)
		{
			// инициализируем указатель на сведения терминала сертификата GOST
			PROT_WRITE_EX0(PROT_FUNCLIB3, "TagFindByPath(5F20)")
			ilRet = TagFindByPath(certGost, dwCertGostLen, TAG_PATH_V11_5F20, sizeof(TAG_PATH_V11_5F20)/sizeof(TAG_PATH_V11_5F20[0]), &dwTermInfoGostLen, &pTermInfoGost, 0);
			PROT_WRITE_EX1(PROT_FUNCLIB3, "TagFindByPath(5F20)=%u", ilRet)
			if(ilRet) {
				ilRet = ILRET_CERT_MISSING_ELEMENT; goto End;
			}
		}

		// получаем сертификат открытого ключа терминала RSA
		ilRet = flGetCertificate(phCrd, IL_PARAM_CIFDID, phCrd->KeyVer, 0, certRsa, &dwCertRsaLen);
		if(ilRet && ilRet != ILRET_PARAM_CERTIFICATE_NOT_FOUND)
			goto End;
		if(!ilRet)
		{	// инициализируем указатель на сведения терминала сертификата RSA
			PROT_WRITE_EX0(PROT_FUNCLIB3, "TagFindByPath(5F20)")
			ilRet = TagFindByPath(certRsa, dwCertRsaLen, TAG_PATH_V11_5F20, sizeof(TAG_PATH_V11_5F20)/sizeof(TAG_PATH_V11_5F20[0]), &dwTermInfoRsaLen, &pTermInfoRsa, 0);
			PROT_WRITE_EX1(PROT_FUNCLIB3, "TagFindByPath(5F20)=%u", ilRet)
			if(ilRet) {
				ilRet = ILRET_CERT_MISSING_ELEMENT; goto End;
			}
		}

		// хотя бы один сертификат должен быть получен
		if(!pTermInfoGost && !pTermInfoRsa) {
			ilRet = ILRET_PARAM_CERTIFICATE_NOT_FOUND; goto End;
		}

		// длина элемента данных TerminalInfo должна равняться (4 + 4 + 8 = 16 байт)
		if(pTermInfoGost && dwTermInfoGostLen != *pdwOutLen) {
			ilRet = ILRET_CERT_WRONG_LENGTH; goto End;
		}
		if(pTermInfoRsa && dwTermInfoRsaLen != *pdwOutLen) {
			ilRet = ILRET_CERT_WRONG_LENGTH; goto End;
		}

		// при наличии одновременно двух сертификатов значения TerminalInfo должны совпадать
		if(pTermInfoGost && pTermInfoRsa && cmnMemCmp(pTermInfoGost, pTermInfoRsa, (IL_WORD)(*pdwOutLen))) {
			ilRet = ILRET_CERT_TERMINFO_NOT_MATCH; goto End;
		}
		
		// копируем сведения о терминале в выходной буфер
		cmnMemCopy(pOut, (pTermInfoGost ? pTermInfoGost : pTermInfoRsa), (IL_WORD)(*pdwOutLen));
	}

End:
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flGetTerminalInfo()=%u", ilRet)
	return ilRet;
}

// flFillRecordsList - заполнение списка секторов и блоков карты
IL_FUNC IL_WORD flFillRecordsList(IL_BYTE AppVer, IL_BYTE *fci, IL_WORD fci_len, IL_RECORD_LIST *list, IL_BYTE ifMain)
{
	IL_WORD ilRet = 0;
    IL_BYTE num_records = 0;
    IL_BYTE* pRecListPtr;
    IL_DWORD dwRecListLen;
    IL_BYTE* pRecListLastPtr;
    IL_BYTE* pTmpPtr;
    IL_DWORD dwTmpLen;
    IL_BYTE* pPtr1;
    IL_DWORD dwLen1;
    IL_BYTE* pPtr2;
    IL_DWORD dwLen2;
    IL_BYTE* pPtr3;
    IL_DWORD dwLen3;
    IL_BYTE* pPtr4;
    IL_DWORD dwLen4;

	PROT_WRITE_EX0(PROT_FUNCLIB3, "flFillRecordsList()")

	if(!fci || !list) 
		ilRet = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		if(AppVer == UECLIB_APP_VER_10)
			TagFindByPath(fci, fci_len, ifMain?TAG_PATH_E0:TAG_PATH_E2, 3, &dwRecListLen, &pRecListPtr, 0);
		else
			TagFindByPath(fci, fci_len, ifMain?TAG_PATH_V11_E0:TAG_PATH_V11_E2, 2, &dwRecListLen, &pRecListPtr, 0);
    
		pRecListLastPtr = &pRecListPtr[dwRecListLen];
		for(pTmpPtr = pRecListPtr, dwTmpLen = dwRecListLen; pTmpPtr < pRecListLastPtr; )
		{
			TagFind(pTmpPtr, dwTmpLen, ifMain?IL_TAG_E1:IL_TAG_E3, &dwLen1, &pPtr1, 0);

			TagFind(pPtr1, dwLen1, ifMain?IL_TAG_DF20:IL_TAG_DF22, &dwLen2, &pPtr2, 0);
			list->rec[num_records].id = *pPtr2;
			TagFind(pPtr1, dwLen1, IL_TAG_51, &dwLen3, &pPtr3, 0);
			cmnMemCopy((list->rec[num_records]).fid,pPtr3,2);
			if(ifMain)
			{
				// TODO: Проверять длину тега = 1 байт
				TagFind(pPtr1, dwLen1, IL_TAG_DF21, &dwLen4, &pPtr4, 0);
				list->rec[num_records].version = *pPtr4;
			}
			pTmpPtr = pPtr1 + dwLen1;
			dwTmpLen -= dwLen1;
			num_records++;
		}
		list->num_records = num_records;
	}

	PROT_WRITE_EX1(PROT_FUNCLIB3, "flFillRecordsList()=%u", ilRet)
	return ilRet;
}

IL_FUNC IL_RETCODE flGetAppVersion(IL_CARD_HANDLE* phCrd, IL_BYTE* data, IL_WORD data_len)
{
	IL_RETCODE ilRet;
	IL_BYTE* p9F08;
	IL_DWORD dwLen9F08;

	PROT_WRITE_EX0(PROT_FUNCLIB3, "flGetAppVersion()")

	// пытаемся определить версию ИД-приложения в соответствии со спецификацией карты 1.1
	ilRet = TagFindByPath(data, data_len, TAG_PATH_V11_9F08, sizeof(TAG_PATH_V11_9F08)/sizeof(TAG_PATH_V11_9F08[0]), &dwLen9F08, &p9F08, 0);
	if(ilRet == ILRET_DATA_TAG_NOT_FOUND)
		// пытаемся определить версию ИД-приложения в соответствии со спецификацией карты 1.0 
		ilRet = TagFindByPath(data, data_len, TAG_PATH_9F08, sizeof(TAG_PATH_9F08)/sizeof(TAG_PATH_9F08[0]), &dwLen9F08, &p9F08, 0);
	if(ilRet)
		return ilRet;
 
	// проверяем допустимость использования версии ИД-приложения
	if(dwLen9F08 != 1) {
        ilRet = ILRET_DATA_CARD_FORMAT_ERROR; goto End;
	}
	if(UECLIB_APP_VER < p9F08[0]) {
        ilRet =  ILRET_APP_VER_NOT_SUPPORTED; goto End;
	}
#ifndef UECLIB_APP_VER_10_SUPPORT
	if(p9F08[0] <= UECLIB_APP_VER_10) {
		ilRet = ILRET_APP_VER_NOT_SUPPORTED; goto End;
	}
#endif//UECLIB_APP_VER_10_SUPPORT
	
	// сохраняем версию приложения в дескрипторе карты
	phCrd->AppVer = p9F08[0];
	PROT_WRITE_EX1(PROT_FUNCLIB2, "OUT: AppVer=%u", phCrd->AppVer)

End:
	PROT_WRITE_EX1(PROT_FUNCLIB3, "flGetAppVersion()=%u", ilRet)
	return ilRet;
}

IL_FUNC IL_BYTE flSignCheckCertPresent(IL_CARD_HANDLE *phCrd)
{
	IL_RETCODE ilRet;
	IL_WORD wFileId = IL_FILEID_12;
	//!!!MICRON SPECIFIC !!!!!!!!!!!!!!!!!!!!!!!!!!!!
	IL_WORD wDataId = phCrd->ifNeedMSE ? 0xFFFF : 0; 
	//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	IL_TAG  TagPath03[] = { 0x30, 0x03 };
	IL_BYTE tmp[256];
	IL_WORD tmp_len;
	IL_BYTE *BitMask;
	IL_DWORD BitMaskLen;

 	if((ilRet = flSelectContext(phCrd, 0, 0, 0)) == 0)
	{
		if((ilRet = flAppReadBlock(phCrd, wFileId, wDataId, tmp, &tmp_len)) == 0) 
		{
			if((ilRet = TagFindByPath(tmp, tmp_len, TagPath03, 2, &BitMaskLen, &BitMask, 0)) == 0)
				return (BitMaskLen == 2 && BitMask[1] == 0x03) ? 1 : 0;
		}
	}

	return 0;
}

IL_FUNC IL_RETCODE flAppSelect(IL_CARD_HANDLE* phCrd, IL_BYTE* pOut, IL_WORD* pwOutLen)
{
	IL_RETCODE ilRet;
	IL_BYTE out[256];
	IL_WORD out_len;
	IL_BYTE* pTmpPtr;
	IL_DWORD dwTmpLen;
	IL_BYTE* pTmpPtr5F24;
	IL_DWORD dwTmpLen5F24;
	IL_BYTE* pTmpPtr5F25;
	IL_DWORD dwTmpLen5F25;
	IL_BYTE* pTmpPtrC2;
	IL_DWORD dwTmpLenC2;
	IL_BYTE* pTmpPtr8F;
	IL_DWORD dwTmpLen8F;
	IL_BYTE term_info[256];
	IL_BYTE term_date[3];
	IL_DWORD term_info_size;
	IL_BYTE crypto_supported;

	PROT_WRITE_EX0(PROT_FUNCLIB1, "flAppSelect()")

	phCrd->ifSM = 0;
	phCrd->ifLongAPDU = 0;
	phCrd->currDF = 0;
	phCrd->currEF = 0;

	// селектируем приложение
	PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppSelect()")
	ilRet = clAppSelect(phCrd, 0x04, 0x00, ID_APP_AID, sizeof(ID_APP_AID), out, &out_len);
	PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppSelect()=%u", ilRet)
	if(ilRet)
		goto End;

    // определяем версию приложения
	if((ilRet = flGetAppVersion(phCrd, out, out_len)) != 0)
		goto End;

	// проверяем наличие каталога секторов
	PROT_WRITE_EX0(PROT_FUNCLIB3, "TagFindByPath(E0)")
	if(phCrd->AppVer == UECLIB_APP_VER_10)
		ilRet = TagFindByPath(out, out_len, TAG_PATH_E0, sizeof(TAG_PATH_E0)/sizeof(TAG_PATH_E0[0]), &dwTmpLen, &pTmpPtr, 0);
	else
		ilRet = TagFindByPath(out, out_len, TAG_PATH_V11_E0, sizeof(TAG_PATH_V11_E0)/sizeof(TAG_PATH_V11_E0[0]), &dwTmpLen, &pTmpPtr, 0);
	PROT_WRITE_EX1(PROT_FUNCLIB3, "TagFindByPath(E0)=%u", ilRet)
	if(ilRet)
		goto End;

    // дата окончания срока действия приложения
	PROT_WRITE_EX0(PROT_FUNCLIB3, "TagFindByPath(5F24)")
	if(phCrd->AppVer == UECLIB_APP_VER_10)
		ilRet = TagFindByPath(out, out_len, TAG_PATH_5F24, sizeof(TAG_PATH_5F24)/sizeof(TAG_PATH_5F24[0]), &dwTmpLen5F24, &pTmpPtr5F24, 0);
	else
		ilRet = TagFindByPath(out, out_len, TAG_PATH_V11_5F24, sizeof(TAG_PATH_V11_5F24)/sizeof(TAG_PATH_V11_5F24[0]), &dwTmpLen5F24, &pTmpPtr5F24, 0);
	PROT_WRITE_EX1(PROT_FUNCLIB3, "TagFindByPath(5F24)=%u", ilRet)
	if(ilRet)
		goto End;
	if(dwTmpLen5F24 != 3) {
        ilRet = ILRET_DATA_CARD_FORMAT_ERROR; goto End;
	}

    // дата начала срока действия приложения
	PROT_WRITE_EX0(PROT_FUNCLIB3, "TagFindByPath(5F25)");
	if(phCrd->AppVer == UECLIB_APP_VER_10)
		ilRet = TagFindByPath(out, out_len, TAG_PATH_5F25, sizeof(TAG_PATH_5F25)/sizeof(TAG_PATH_5F25[0]), &dwTmpLen5F25, &pTmpPtr5F25, 0);
	else
		ilRet = TagFindByPath(out, out_len, TAG_PATH_V11_5F25, sizeof(TAG_PATH_V11_5F25)/sizeof(TAG_PATH_V11_5F25[0]), &dwTmpLen5F25, &pTmpPtr5F25, 0);
	PROT_WRITE_EX1(PROT_FUNCLIB3, "TagFindByPath(5F25)=%u", ilRet);
	if(ilRet)
		goto End;;
	if(dwTmpLen5F25 != 3) {
        ilRet = ILRET_DATA_CARD_FORMAT_ERROR; goto End;
	}

    // сведения о применении приложения
	PROT_WRITE_EX0(PROT_FUNCLIB3, "TagFindByPath(C2)")
	if(phCrd->AppVer == UECLIB_APP_VER_10)
		ilRet = TagFindByPath(out, out_len, TAG_PATH_C2, sizeof(TAG_PATH_C2)/sizeof(TAG_PATH_C2[0]), &dwTmpLenC2, &pTmpPtrC2, 0);
	else
	    ilRet = TagFindByPath(out, out_len, TAG_PATH_V11_C2, sizeof(TAG_PATH_V11_C2)/sizeof(TAG_PATH_V11_C2[0]), &dwTmpLenC2, &pTmpPtrC2, 0);
	PROT_WRITE_EX1(PROT_FUNCLIB3, "TagFindByPath(C2)=%u", ilRet)
	if(ilRet)
        goto End;
	if(dwTmpLenC2 != 2) {
        ilRet = ILRET_DATA_CARD_FORMAT_ERROR; goto End;
	}
	cmnMemCopy(phCrd->AUC, pTmpPtrC2, 2);

	// версия открытого ключа УЦ ЕПСС УЭК
	phCrd->KeyVer = 0;
	if(phCrd->AppVer != UECLIB_APP_VER_10)
	{	
		PROT_WRITE_EX0(PROT_FUNCLIB3, "TagFindByPath(8F)")
		ilRet = TagFindByPath(out, out_len, TAG_PATH_V11_8F, sizeof(TAG_PATH_V11_8F)/sizeof(TAG_PATH_V11_8F[0]), &dwTmpLen8F, &pTmpPtr8F, 0);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "TagFindByPath(8F)=%u", ilRet)
		if(ilRet)
			goto End;
		if(dwTmpLen8F != 1) {
			ilRet = ILRET_DATA_CARD_FORMAT_ERROR; goto End;
		}
		phCrd->KeyVer = pTmpPtr8F[0];
	}

    // получаем текущую дату операции
	PROT_WRITE_EX0(PROT_FUNCLIB3, "rtcGetCurrentDate()")
    ilRet = rtcGetCurrentDate(term_date);
	PROT_WRITE_EX1(PROT_FUNCLIB3, "rtcGetCurrentDate()=%u", ilRet)
	if(ilRet)
        goto End;

    // проверяем дату начала срока действия приложения
	if(rtcCompareDates(term_date, pTmpPtr5F25) < 0) {
        ilRet = ILRET_APP_NOT_ACTIVE_YET; goto End;
	}

    // проверяем дату окончания срока действия приложения
	if(rtcCompareDates(term_date, pTmpPtr5F24) > 0) {
        ilRet = ILRET_APP_EXPIRED; goto End;
	}

    // получаем сведения о терминале
	if((ilRet = flGetTerminalInfo(phCrd, term_info, &term_info_size)) != 0)
        goto End;

    // выбираем тип используемого криптоалгоритма
	if(phCrd->AppVer == UECLIB_APP_VER_10)
		crypto_supported = (phCrd->AUC[0] & term_info[10]) & 0xC0;
	else
		crypto_supported = (phCrd->AUC[0] & term_info[14]) & 0xC0;
    if(crypto_supported & 0x80)
        phCrd->ifGostCrypto = 1;
    else if(crypto_supported & 0x40)
        phCrd->ifGostCrypto = 0;
	else {
        ilRet = ILRET_NO_CRYPTOALG_SUPPORTED; goto End;
	}

#ifdef GUIDE
	// Переключение ГОСТ/RSA по параметру в terminal.ini 
	// Attention!!!! Данный код содержится только в целях отладки алгоритмов. Не использовать в рабочих версиях!!!!
	{
		IL_DWORD dwTmp = 1;
		IL_BYTE ifGostCrypto;

		if( !prmGetParameter(IL_PARAM_USE_GOST, &ifGostCrypto, &dwTmp) )
		{
			phCrd->ifGostCrypto = ifGostCrypto;
		}
	}
#endif//GUIDE	

	// Принудительная установка
	//phCrd->ifGostCrypto = 0;
	//phCrd->ifGostCrypto = 1;

    // определяем поддерживается ли картой механизм электронной подписи держателя карты
    phCrd->ifSign = ((term_info[14] & 0x04 ) && (phCrd->AppVer > UECLIB_APP_VER_10) && phCrd->ifGostCrypto)? 1 : 0; 

	// устанавливаем признак поддержки картой «длинных данных»  
	if(phCrd->AUC[0] & 0x20)
		phCrd->ifLongAPDU = 1;

	// устанавливаем признак поддержки картой команды MSE
	if(phCrd->AUC[0] & 0x10)
		phCrd->ifNeedMSE = 1;

	// инициализируем список секторов карты 
	flFillRecordsList(phCrd->AppVer, out, out_len, &phCrd->sectors, 1);

	// инициализируем выходной буфер возвращенными ИД-приложением карты данными   
	if(pOut)
        cmnMemCopy(pOut, out, out_len);
    if(pwOutLen)
        *pwOutLen = out_len;

	// проверяем наличие сертификата открытого ключа ОКО 
	if((ilRet = flGetCertificate(phCrd, IL_PARAM_CAID, phCrd->KeyVer, phCrd->ifGostCrypto, NULL, NULL)) != 0)
		goto End;
	
	// проверяем наличие сертификата открытого ключа терминала
	if((ilRet = flGetCertificate(phCrd, IL_PARAM_CIFDID, phCrd->KeyVer, phCrd->ifGostCrypto, NULL, NULL)) != 0)
		goto End;

	// инициализируем MSE
	if(phCrd->ifNeedMSE)
	{
		PROT_WRITE_EX0(PROT_FUNCLIB3, "_MSEReset()")
		ilRet = _MSEReset(phCrd);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "_MSEReset()=%u", ilRet)
		if(ilRet)
			goto End;
	}

	// проверяем статус приложения
	phCrd->AppStatus = 0x77;
	if(phCrd->AppVer > UECLIB_APP_VER_10)
	{
		if((ilRet = flAppGetStatus(phCrd, &phCrd->AppStatus)) != 0)
			goto End;

		if(phCrd->AppStatus == 0x7F)
		{	// при несогласованном состоянии ИД-приложения получаем CIN
			if((ilRet = flGetCIN(phCrd, phCrd->CIN)) != 0)
				goto End;
			ilRet = ILRET_APP_INCONSISTENT_STATE;
		}
		else if(phCrd->AppStatus != 0x77)
			ilRet = ILRET_APP_UNKNOWN_STATE;
	}

End:
	if(!ilRet && pOut && pwOutLen)
		PROT_WRITE_EX2(PROT_FUNCLIB2, "OUT: Resp[%u]=%s", *pwOutLen, bin2hex(protBuf, pOut, *pwOutLen));
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flAppSelect()=%u", ilRet)
	return ilRet;
}

IL_FUNC IL_RETCODE flAppReselect(IL_CARD_HANDLE* phCrd)
{
	IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_FUNCLIB1, "flAppReselect()")

	phCrd->ifSM = 0;
	phCrd->currDF = 0;
	phCrd->currEF = 0;

	if((ilRet = clAppSelect(phCrd, 0x04, 0x0C, ID_APP_AID, sizeof(ID_APP_AID), NULL, NULL)) == 0)
		if(phCrd->ifNeedMSE)
			ilRet = _MSEReset(phCrd);

	PROT_WRITE_EX1(PROT_FUNCLIB1, "flAppReselect()=%u", ilRet)
	return ilRet;
}


IL_FUNC IL_RETCODE flFileSelect(IL_CARD_HANDLE* phCrd, IL_BYTE ifDF, IL_BYTE* pId, IL_BYTE IdLen, IL_BYTE* pOut, IL_WORD* pOutLength)
{
    IL_RETCODE ilRet = 0;

	PROT_WRITE_EX1(PROT_FUNCLIB3, "flFileSelect(%s)", bin2hex(protBuf, pId, IdLen))
    
	if((ilRet = clAppSelect(phCrd, 0x00, ifDF ? ((phCrd->AppVer == UECLIB_APP_VER_10) ? 0x00:0x08) : 0x0C, pId, IdLen, pOut, pOutLength)) == 0)
	{
		if(IdLen == 2)
		{
			IL_WORD wFileId =  (pId[0]<<8) + pId[1];

			if(ifDF)
			{
				phCrd->currDF = wFileId;
				phCrd->currEF = 0;  
			}
			else
				phCrd->currEF = wFileId;
		}
	}

	PROT_WRITE_EX2(PROT_FUNCLIB3, "flFileSelect(%s)=%u", protBuf, ilRet)
    return ilRet;
}

IL_FUNC IL_RETCODE flSelectContext(IL_CARD_HANDLE* phCrd, IL_WORD sectorId, IL_WORD blockId, IL_BYTE ifForceSelection)
{
    IL_RETCODE ilRet = 0;
	IL_WORD DFid = 0;
	IL_WORD EFid;
	IL_WORD sector = 0;
	IL_WORD block;

	PROT_WRITE_EX2(PROT_FUNCLIB1, "flSelectContext(%u-%u)", sectorId, blockId)

    if(sectorId)
	{	// ищем в списке секторов нужный сектор 
		for(sector = 0; sector < phCrd->sectors.num_records; sector++)
		{
			if(phCrd->sectors.rec[sector].id == sectorId)
			{	// определяем DF-идентификатор селектируемого прикладного сектора
				DFid = (phCrd->sectors.rec[sector].fid[0]<<8) + phCrd->sectors.rec[sector].fid[1];
				break;
			}
		}
		if(sector >= phCrd->sectors.num_records) {
			ilRet = ILRET_CRD_SELECT_SECTOR_NOT_FOUND; goto End;
		}
	}

	// проверим необходимость возвратиться в системный контекст
	if((sectorId && phCrd->currDF && phCrd->currDF != DFid) || (!sectorId  && phCrd->currDF))
	{	// возвращаемся из прикладного контекста в системный контекст!!!
		PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppSelect()")
		if(phCrd->AppVer == UECLIB_APP_VER_10) 
		{	// версия 1.0
			ilRet = clAppSelect(phCrd, 0x03, 0x0C, NULL, 0, NULL, NULL);
		}
		else
		{   // версия 1.1
			IL_BYTE ADF[] = { 0x3F, 0x3F };
			ilRet = clAppSelect(phCrd, 0x00, 0x0C, ADF, 2, NULL, NULL); 
		}
		PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppSelect()=%u", ilRet)
		phCrd->currDF = phCrd->currEF = 0;
	}

	// для явного перехода в системный контекст всё уже сделано
	if(!sectorId)
		goto End;

	// селектируем сектор и инициализируем список его блоков
	if(DFid != phCrd->currDF || ifForceSelection)
	{ 
		IL_BYTE resp[IL_APDU_BUF_SIZE];
		IL_WORD resp_len;
	    
		if((ilRet = flFileSelect(phCrd, 1, phCrd->sectors.rec[sector].fid, 2, resp, &resp_len)) != 0)
			goto End;
		// ответ карты на селектирование сектора не может быть нулевым!!!
		if(!resp_len) {
			ilRet = ILRET_CRD_SELECT_RESPONSE_ABSENT; goto End;
		}
	    flFillRecordsList(phCrd->AppVer, resp, resp_len, &phCrd->blocks, 0);
	}

	// завершаем, если селектируемый блок не указан
	if(!blockId)
	{
		phCrd->currEF = 0;
		goto End;
	}

	// ищем в списке блоков нужный блок
	for(block = 0; block < phCrd->blocks.num_records; block++)
		if(phCrd->blocks.rec[block].id == blockId)
			break;
	if(block >= phCrd->blocks.num_records) {
		ilRet = ILRET_CRD_SELECT_BLOCK_NOT_FOUND; goto End;
	}

	// селектируем блок данных
	EFid = (phCrd->blocks.rec[block].fid[0]<<8) + phCrd->blocks.rec[block].fid[1];
	if(EFid != phCrd->currEF || ifForceSelection)
		ilRet = flFileSelect(phCrd, 0, phCrd->blocks.rec[block].fid, 2, NULL, NULL);

End:
	PROT_WRITE_EX3(PROT_FUNCLIB1, "flSelectContext(%u-%u)=%u", sectorId, blockId, ilRet)
    return ilRet;
}

IL_FUNC IL_RETCODE flAppReadBlock(IL_CARD_HANDLE* phCrd, IL_WORD wFileId, IL_WORD wDataId, IL_BYTE* pDataOut, IL_WORD* pwDataOutLen)
{
    IL_RETCODE ilRet;
    IL_BYTE tmp[256];
    IL_WORD file_len;
    IL_BYTE FileId[2];

    PROT_WRITE_EX2(PROT_FUNCLIB1, "flAppReadBlock(%04X-%04X)", wFileId, wDataId)

	// селектируем указанный файл
	FileId[0] = wFileId>>8;
	FileId[1] = (IL_BYTE)wFileId;
	if((ilRet = flFileSelect(phCrd, 0, FileId, sizeof(FileId), NULL, NULL)) != 0)
		goto End;

    if(wDataId==0)
    {
        //READ BINARY (TLV header)
        //ВНИМАНИЕ!!! Потенциальная угроза: при чтении TLV файла с длиной данных меньшей 3x байт возможна ошибка
		PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppReadBinary()")
        ilRet = clAppReadBinary(phCrd, 0, 5, tmp);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppReadBinary()=%u", ilRet)
        if(ilRet)
            goto End;

        file_len = (IL_WORD)(GetTagLen(tmp) + GetLenLen(tmp) + GetDataLen(tmp));

        if(pDataOut != NULL)
        {
            //READ BINARY (TLV header)
            ilRet = flReadBinary(phCrd, 0, file_len, pDataOut);
            if(ilRet)
                goto End;
        }

        *pwDataOutLen = file_len;
    }
    else if(wDataId==0xFFFF)
    {	// Специальный случай чтения сертификата публичного ключа приложения!!!
		ilRet = flReadBinaryEx(phCrd, 0, pDataOut, *pwDataOutLen, pwDataOutLen);
    }
    else
    {
		PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppGetData()")
        ilRet = clAppGetData(phCrd, wDataId, pDataOut, pwDataOutLen);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppGetData()=%u", ilRet)
    }

End:
    PROT_WRITE_EX3(PROT_FUNCLIB1, "flAppReadBlock(%04X-%04X)=%u", wFileId, wDataId, ilRet);
    return ilRet;
}

// Процедура проверки формата сертификатов
IL_FUNC IL_RETCODE flCheckCertificateFormat(IL_BYTE* pKeyCertIn, IL_WORD wKeyCertInLen, IL_BYTE AppVer, IL_BYTE ifGost, IL_BYTE CertificateTypeToCheck)
{
    IL_RETCODE ilRet;
	IL_BYTE term_date[3];

	IL_BYTE *p7F21;
	IL_DWORD dwLen7F21;
	IL_BYTE *p7F4E;
	IL_DWORD dwLen7F4E;

	IL_BYTE *pCertVer;

	IL_BYTE *pElem;
	IL_DWORD dwElemLen;
	IL_BYTE *pSub;
	IL_DWORD dwSubLen;

	PROT_WRITE_EX0(PROT_FUNCLIB3, "flCheckCertificateFormat()")

	if((ilRet = TagFind(pKeyCertIn, wKeyCertInLen, IL_TAG_7F21, &dwLen7F21, &p7F21, 0)) == 0)
	{
		pKeyCertIn    = p7F21;
		wKeyCertInLen = (IL_WORD)dwLen7F21;
	}
	else if(ilRet != ILRET_DATA_TAG_NOT_FOUND)
		goto End;

	// инициализируем указатель на сертифицируемые данные открытого ключа
	ilRet = TagFind(pKeyCertIn, wKeyCertInLen, IL_TAG_7F4E, &dwLen7F4E, &p7F4E, 0);
	if(ilRet)
		goto End;

    // получаем текущую дату операции
    if((ilRet = rtcGetCurrentDate(term_date)) != 0)
        goto End;

	// TODO: проверяем начало срока действия сертификата
	/*---
	if((ilRet = TagFind(p7F4E, dwLen7F4E, IL_TAG_5F25, &dwElemLen, &pElem, 0)) != 0)  
		goto End;
	if(dwElemLen != 3) {
        ilRet = ILRET_CERT_WRONG_LENGTH; goto End;
	}
	if(rtcCompareDates(term_date, pElem) < 0) {
        ilRet = ILRET_CERT_NOT_ACTIVE_YET; goto End;
	}
	---*/

	// проверяем завершение срока действия сертификата
	if((ilRet = TagFind(p7F4E, dwLen7F4E, IL_TAG_5F24, &dwElemLen, &pElem, 0)) != 0) 
		goto End;
	if(dwElemLen != 3) {
        ilRet = ILRET_CERT_WRONG_LENGTH; goto End;
	}
	if(rtcCompareDates(term_date, pElem) > 0) {
        ilRet = ILRET_CERT_EXPIRED; goto End;
	}

	// проверяем версию формата сертификата
	if((ilRet = TagFind(p7F4E, dwLen7F4E, IL_TAG_5F29, &dwElemLen, &pCertVer, 0)) != 0) {
		ilRet = ILRET_CERT_MISSING_ELEMENT; goto End;
	}
	if(dwElemLen != 1) {
        ilRet = ILRET_CERT_WRONG_LENGTH; goto End;
	}
	if(AppVer == UECLIB_APP_VER_10 && *pCertVer != 0x01) {
		ilRet = ILRET_CERT_WRONG_VERSION; goto End;
	}

	/*+++
	// проверяем идентификатор выпустившей сертификат организации
	if((ilRet = TagFind(p7F4E, dwLen7F4E, IL_TAG_42, &dwElemLen, &pElem, 0))) {
		ilRet = ILRET_CERT_MISSING_ELEMENT; goto End;
	}
	if(dwElemLen != 3) {
        ilRet = ILRET_CERT_WRONG_LENGTH; goto End;
	}
    +++*/ 

	// проверяем данные выпустившей сертификат организации 
	if((ilRet = TagFind(p7F4E, dwLen7F4E, IL_TAG_65, &dwElemLen, &pElem, 0)) != 0) {
		ilRet = ILRET_CERT_MISSING_ELEMENT; goto End;
	}
	if((ilRet = TagFind(pElem, dwElemLen, IL_TAG_80, &dwSubLen, &pSub, 0)) != 0) {
		ilRet = ILRET_CERT_MISSING_ELEMENT;	goto End;
	}
	if(dwSubLen != 1) {
        ilRet = ILRET_CERT_WRONG_LENGTH; goto End;
	}
	if((ilRet = TagFind(pElem, dwElemLen, IL_TAG_81, &dwSubLen, &pSub, 0)) != 0) {
		ilRet = ILRET_CERT_MISSING_ELEMENT;	goto End;
	}
	if(dwSubLen != 1) {
        ilRet = ILRET_CERT_WRONG_LENGTH; goto End;
	}

	//// проверяем идентификатор субъекта - владельца открытого ключв
	//if((ilRet = TagFind(p7F4E, dwLen7F4E, IL_TAG_5F20, &dwElemLen, &pElem, 0))) {
	//	ilRet = ILRET_CERT_MISSING_ELEMENT; goto End;
	//} 
	//if(dwElemLen < 3) {
	//  ilRet = ILRET_CERT_WRONG_LENGTH; goto End;
	//} 

	// проверяем сведения об открытом ключе сертификата
	if((ilRet = TagFind(p7F4E, dwLen7F4E, IL_TAG_7F49, &dwElemLen, &pElem, 0)) != 0) {
		ilRet = ILRET_CERT_MISSING_ELEMENT;	goto End;
	}
	if((ilRet = TagFind(pElem, dwElemLen, IL_TAG_81, &dwSubLen, &pSub, 0)) != 0) {
		ilRet = ILRET_CERT_MISSING_ELEMENT;	goto End;
	}
	if(ifGost)
	{	
		if(dwSubLen != 64) {
			ilRet = ILRET_CERT_WRONG_LENGTH; goto End;
		}
	}
	else
	{
		if((ilRet = TagFind(pElem, dwElemLen, IL_TAG_82, &dwSubLen, &pSub, 0)) != 0) {
			ilRet = ILRET_CERT_MISSING_ELEMENT; goto End;
		}
		if(dwSubLen != 1 && dwSubLen != 3) {
			ilRet = ILRET_CERT_WRONG_LENGTH; goto End;
		}
		if(dwSubLen == 1 && *pSub != 3) {
			ilRet = ILRET_CERT_WRONG_RSA_EXP; goto End;
		}
		if(dwSubLen == 3 && (pSub[0] != 0x01 || pSub[1] != 0x00 || pSub[2] != 0x01)) {
			ilRet = ILRET_CERT_WRONG_RSA_EXP; goto End;
		}
	}

	// проверяем полномочия владельца сертификата
	if((ilRet = TagFind(p7F4E, dwLen7F4E, IL_TAG_7F4C, &dwElemLen, &pElem, 0)) != 0) {
		ilRet = ILRET_CERT_MISSING_ELEMENT;	goto End;
	}
	if((ilRet = TagFind(pElem, dwElemLen, IL_TAG_80, &dwSubLen, &pSub, 0)) != 0) {
		ilRet = ILRET_CERT_MISSING_ELEMENT; goto End;
	}
	if(*pSub == 0 || *pSub > 7) {
		ilRet = ILRET_CERT_INVALID_TYPE; goto End;
	}
	if(CertificateTypeToCheck != 0)//в случае если 0 не контролируем тип сертификата
    {
		if(*pSub != CertificateTypeToCheck) {
            ilRet = ILRET_CERT_INVALID_TYPE; goto End;
		}
    }	    
	if(*pSub == 4 || *pSub == 7)
	{
		if((ilRet = TagFind(pElem, dwElemLen, IL_TAG_81, &dwSubLen, &pSub, 0)) != 0) {
			ilRet = ILRET_CERT_MISSING_ELEMENT; goto End;
		}
	}
	if((ilRet = TagFind(pElem, dwElemLen, IL_TAG_82, &dwSubLen, &pSub, 0)) != 0) {
		ilRet = ILRET_CERT_MISSING_ELEMENT; goto End;
	}
	if((ilRet = TagFind(pElem, dwElemLen, IL_TAG_83, &dwSubLen, &pSub, 0)) != 0) {
		ilRet = ILRET_CERT_MISSING_ELEMENT; goto End;
	}

End:
	PROT_WRITE_EX1(PROT_FUNCLIB3, "flCheckCertificateFormat()=%u", ilRet)
	return ilRet;
}

// процедура проверки сертификатов
IL_FUNC IL_RETCODE flCheckCertificate(IL_CARD_HANDLE* phCrd, IL_BYTE* pKeyCertIn, IL_WORD wKeyCertInLen, BYTE CertificateTypeToCheck)
{
    IL_RETCODE ilRet = 0;
    IL_DWORD dwLen7F4E;
    IL_BYTE* p7F4E;
    IL_DWORD dwLen5F37;
    IL_BYTE* p5F37;
	IL_BYTE* p7F21;
	IL_DWORD dwLen7F21;
	IL_BYTE* _pKeyCertIn = pKeyCertIn;
	IL_WORD  _wKeyCertInLen = wKeyCertInLen;
	
    PROT_WRITE_EX1(PROT_FUNCLIB1, "flCheckCertificate(%u)", CertificateTypeToCheck)

	if(phCrd->ifLongAPDU && !phCrd->ifNeedMSE)
    {	// MICRON SPECIFIC!!!! 
		PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppPerformSecOperation(MICRON)")
        ilRet = clAppPerformSecOperation(phCrd, 0x00, pKeyCertIn, wKeyCertInLen);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppPerformSecOperation(MICRON)=%u", ilRet)
		if(ilRet)
            goto End;
    }
    else
    {	// пытаемся получить указатель на данные обрамляющего тега 7F21
		if((ilRet = TagFind(pKeyCertIn, wKeyCertInLen, IL_TAG_7F21, &dwLen7F21, &p7F21, 0)) == 0)
		{
			pKeyCertIn    = p7F21;
			wKeyCertInLen = (IL_WORD)dwLen7F21;
		}
		else if(ilRet != ILRET_DATA_TAG_NOT_FOUND)
			goto End;

		// извлекаем сертифицируемые данные из сертификата открытого ключа
		if((ilRet = TagFind(pKeyCertIn, wKeyCertInLen, IL_TAG_7F4E, &dwLen7F4E, &p7F4E, 1)) != 0)
			goto End;
		
		// извлекаем цифорвую подпись из сертификата открытого ключа
		if((ilRet = TagFind(pKeyCertIn, wKeyCertInLen, IL_TAG_5F37, &dwLen5F37, &p5F37, 1)) != 0)
			goto End;

		// проверяем сертифицируемые данные
		PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppPerformSecOperation(7F4E)")
        ilRet = clAppPerformSecOperation(phCrd, 0x10, p7F4E, (IL_WORD)dwLen7F4E);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppPerformSecOperation(7F4E)=%u", ilRet)
		if(ilRet)
			goto End;

		// проверяем цифровую подпись 
		PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppPerformSecOperation(5F37)")
        ilRet = clAppPerformSecOperation(phCrd, 0x00, p5F37, (IL_WORD)dwLen5F37);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppPerformSecOperation(5F37)=%u", ilRet)
		if(ilRet)
            goto End;
    }

	// проверяем формат сертификата
	//MICRON SPECIFIC: ДЛЯ КАРТ МИКРОН ФОРМАТ СЕРТИФИКАТОВ НЕ ПРОВЕРЯЕМ!!!!
	if(!phCrd->ifNeedMSE) 
		ilRet = flCheckCertificateFormat(_pKeyCertIn, _wKeyCertInLen, phCrd->AppVer, phCrd->ifGostCrypto, CertificateTypeToCheck);

End:
    PROT_WRITE_EX2(PROT_FUNCLIB1, "flCheckCertificate(%u)=%u", CertificateTypeToCheck, ilRet);
    return ilRet;
}

IL_FUNC IL_RETCODE flAppTerminalAuth(IL_CARD_HANDLE* phCrd)
{
    IL_RETCODE ilRet;
    IL_BYTE caid_buf[2048] = {0};
    IL_DWORD caid_size;
    IL_BYTE cifdid_buf[2048] = {0};
    IL_DWORD cifdid_size;
 
    PROT_WRITE_EX0(PROT_FUNCLIB1, "flAppTerminalAuth()");

	if((ilRet = flGetCertificate(phCrd, IL_PARAM_CAID, phCrd->KeyVer, phCrd->ifGostCrypto, caid_buf, &caid_size)) == 0)
		if((ilRet = flGetCertificate(phCrd, IL_PARAM_CIFDID, phCrd->KeyVer, phCrd->ifGostCrypto, cifdid_buf, &cifdid_size)) == 0)
			ilRet = flAppTerminalAuthEx(phCrd, caid_buf, (IL_WORD)caid_size, cifdid_buf, (IL_WORD)cifdid_size);

	//+++ уточним признак формирования ЭЦП по наличию проверочного сертификата
	if(ilRet == 0 && phCrd->ifSign)
		phCrd->ifSign = flSignCheckCertPresent(phCrd);
    
    PROT_WRITE_EX1(PROT_FUNCLIB1, "flAppTerminalAuth()=%u", ilRet);
	return ilRet;
}

IL_FUNC IL_RETCODE flAppTerminalAuthEx(IL_CARD_HANDLE* phCrd, IL_BYTE* pOpCert, IL_WORD wOpCertSize, IL_BYTE* pTermCert, IL_WORD wTermCertSize)
{
    IL_RETCODE ilRet;
    IL_WORD  wTmpLen;
    IL_BYTE tmp[256];
    IL_BYTE Picid[256];
    IL_WORD wPicid_len = 0;
    MUTUAL_AUTH_DATA_IN MutualAuthDataIn;
    MUTUAL_AUTH_DATA_OUT MutualAuthDataOut;
    SESSION_DATA_IN SessionDataIn;
	IL_WORD wFileId;

    PROT_WRITE_EX0(PROT_FUNCLIB3, "flAppTerminalAuthEx()")

	// проверка ИД-приложением сертификата открытого ключа Оператора канала обслуживания Ca.id
    if((ilRet = flCheckCertificate(phCrd, pOpCert, wOpCertSize, IL_CERTTYPE_A_ID)) != 0)
        goto End;

	// проверка ИД-приложением сертификата открытого ключа терминала Cifd.id
    if((ilRet = flCheckCertificate(phCrd, pTermCert, wTermCertSize, IL_CERTTYPE_IFD_ID)) != 0)
        goto End;

	wFileId = phCrd->ifGostCrypto?IL_FILEID_0F:IL_FILEID_10;

	if(phCrd->ifNeedMSE)
	{	//MICRON DPECIFIC!!! НА КАРТЕ МИКРОН ЗНАЧЕНИЯ КЛЮЧЕЙ ХРАНЯТСЯ НЕ В ТЕГИРОВАННОМ ВИДЕ!!!
		IL_BYTE FileId[2];
		FileId[0] = wFileId>>8;
		FileId[1] = (IL_BYTE)wFileId;
		if((ilRet = flFileSelect(phCrd, 0, FileId, sizeof(FileId), NULL, NULL)) == 0)
			ilRet = flReadBinaryEx(phCrd, 0, Picid, sizeof(Picid), &wPicid_len); 
	}
	else 
		ilRet = flAppReadBlock(phCrd, wFileId, 0, Picid, &wPicid_len); 
	if(ilRet)
        goto End;


    // получаем случайное 16-ти байтовое число карты => MutualAuthDataIn.IcChallenge
	//MICRON DPECIFIC!!! КАРТА МИКРОН ВОЗВРАЩАЕТ СЛУЧАЙНУЮ ПОСЛЕДОВАТЕЛЬНОСТЬ РАЗМЕРОМ 8 БАЙТ!!!
	MutualAuthDataIn.IcChallengeLength = phCrd->ifNeedMSE ? 8 : 16;
    PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppGetChallenge()")
	ilRet = clAppGetChallenge(phCrd, MutualAuthDataIn.IcChallengeLength, MutualAuthDataIn.IcChallenge);
    PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppGetChallenge()=%u", ilRet)
	if(ilRet)
        goto End;


	if(!phCrd->ifGostCrypto)   
	{	//пытаемся обойти "особенности" карты МИКРОН
		if(wPicid_len == 128)
		{
			cmnMemCopy(MutualAuthDataIn.KeyPicidrsa.mod, Picid, wPicid_len);
			MutualAuthDataIn.KeyPicidrsa.mod_len = wPicid_len;
			memcpy(MutualAuthDataIn.KeyPicidrsa.exp, "\x01\x00\x01", 3); 
			MutualAuthDataIn.KeyPicidrsa.exp_len = 3;
		}
		else
		{
			PROT_WRITE_EX0(PROT_FUNCLIB3, "cryptoRsaKeyFromCertificate()")
			ilRet = cryptoRsaKeyFromCertificate(Picid, wPicid_len, &MutualAuthDataIn.KeyPicidrsa);
			PROT_WRITE_EX1(PROT_FUNCLIB3, "cryptoRsaKeyFromCertificate()=%u", ilRet)
			if(ilRet)
			    goto End;
	    }
		// подготавливаем данные для динамической аутентификации терминала => MutualAuthDataOut.S, MutualAuthDataOut.Y 
		PROT_WRITE_EX0(PROT_FUNCLIB3, "cryptoPrepareMutualAuthDataRsa()")
        ilRet = cryptoPrepareMutualAuthDataRsa(phCrd->hCrypto, &MutualAuthDataIn, &MutualAuthDataOut, phCrd->KeyVer);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "cryptoPrepareMutualAuthDataRsa()=%u", ilRet)
		if(ilRet)
		    goto End;

        // выполняем динамическую аутентификацию терминала
        if(phCrd->ifLongAPDU)
        {	// аутентификация терминала для карт, поддерживающих "длинные команды"
            IL_BYTE* p = (IL_BYTE*)malloc(MutualAuthDataOut.S_len + MutualAuthDataOut.Y_len); 
			if(p == NULL) {
                ilRet = ILRET_SYS_MEM_ALLOC_ERROR; goto End;
			}

            cmnMemCopy(p, MutualAuthDataOut.S, MutualAuthDataOut.S_len);
            cmnMemCopy(&p[MutualAuthDataOut.S_len], MutualAuthDataOut.Y, MutualAuthDataOut.Y_len);
			PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppMutualAuth(0x00,0x18)")
//			ilRet = clAppMutualAuth(phCrd, 0x00, IL_KEYTYPE_16_P_CA_ID_RSA, p, (IL_WORD)(MutualAuthDataOut.S_len + MutualAuthDataOut.Y_len), SessionDataIn.Data, &SessionDataIn.Data_len);
			ilRet = clAppMutualAuth(phCrd, 0x00, 0x18/*!!MICRON SPECIFIC!!!*/, p, (IL_WORD)(MutualAuthDataOut.S_len + MutualAuthDataOut.Y_len), SessionDataIn.Data, &SessionDataIn.Data_len);
			PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppMutualAuth(0x00,0x18)=%u", ilRet)
            free(p);
            if(ilRet)
                goto End;
        }
        else
        {	// аутентификация терминала для карт, не поддерживающих "длинные команды" посредством связывания команд
			// цифровая подпись терминала MutualAuthDataOut.S
			PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppMutualAuth(0x10,0x16)")
            ilRet = clAppMutualAuth(phCrd, 0x10, IL_KEYTYPE_16_P_CA_ID_RSA, MutualAuthDataOut.S, MutualAuthDataOut.S_len, tmp, &wTmpLen);
			PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppMutualAuth(0x10,0x16)=%u", ilRet)
			if(ilRet)
                goto End;

			// зашифрованный блок данных MutualAuthDataOut.Y => SessionDataIn.Data
			PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppMutualAuth(0x00,0x16)")
            ilRet = clAppMutualAuth(phCrd, 0x00, IL_KEYTYPE_16_P_CA_ID_RSA, MutualAuthDataOut.Y, MutualAuthDataOut.Y_len, SessionDataIn.Data, &SessionDataIn.Data_len);
			PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppMutualAuth(0x00,0x16)=%u", ilRet)
			if(ilRet)
                goto End;
        }

		// 
        cmnMemCopy(SessionDataIn.InitSessionSmCounter, MutualAuthDataOut.S, 8);
		PROT_WRITE_EX0(PROT_FUNCLIB3, "cryptoPrepareSession()")
        ilRet = cryptoPrepareSession(phCrd->hCrypto, &SessionDataIn, phCrd->KeyVer, phCrd->AppVer);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "cryptoPrepareSession()=%u", ilRet)
        if(ilRet)
            goto End;
    }
    else
    {
		//пытаемся обойти "особенности" карты МИКРОН
		if(wPicid_len == 64)
		{
			cmnMemCopy(SessionDataIn.Data, Picid, wPicid_len);
		}
		else
		{
			PROT_WRITE_EX0(PROT_FUNCLIB3, "cryptoGostKeyFromCertificate()")
            ilRet = cryptoGostKeyFromCertificate(Picid, wPicid_len, &MutualAuthDataIn.KeyPicidgost);
			PROT_WRITE_EX1(PROT_FUNCLIB3, "cryptoGostKeyFromCertificate()=%u", ilRet)
			if(ilRet)
				goto End;
			cmnMemCopy(SessionDataIn.Data,MutualAuthDataIn.KeyPicidgost.key, 64);
		}

		PROT_WRITE_EX0(PROT_FUNCLIB3, "cryptoPrepareMutualAuthDataGost()")
		ilRet = cryptoPrepareMutualAuthDataGost(phCrd->hCrypto, &MutualAuthDataIn, &MutualAuthDataOut, phCrd->KeyVer, phCrd->AppVer);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "cryptoPrepareMutualAuthDataGost()=%u", ilRet)
        if(ilRet)
            goto End;
            
        if(phCrd->AppVer == UECLIB_APP_VER_10) 
		{
			PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppMutualAuth(0x00,0x17)")
    	    ilRet = clAppMutualAuth(phCrd, 0x00, IL_KEYTYPE_17_IC_ID_GOST, MutualAuthDataOut.S, MutualAuthDataOut.S_len, tmp, &wTmpLen);
			PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppMutualAuth(0x00,0x17)=%u", ilRet)
		}
    	else
		{
			PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppMutualAuth(0x00,0x15)")
    	    ilRet = clAppMutualAuth(phCrd, 0x00, IL_KEYTYPE_15_P_CA_ID_GOST, MutualAuthDataOut.S, MutualAuthDataOut.S_len, tmp, &wTmpLen);
			PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppMutualAuth(0x00,0x15)=%u", ilRet)
		}
 		if(ilRet)
			goto End;
			
		cmnMemCopy(&SessionDataIn.Data[64], tmp, 4);
		cmnMemCopy(SessionDataIn.InitSessionSmCounter, MutualAuthDataOut.S, 8);
		if(phCrd->AppVer != UECLIB_APP_VER_10)
		{
		    cmnMemCopy(SessionDataIn.IcChallenge, MutualAuthDataIn.IcChallenge, MutualAuthDataIn.IcChallengeLength);
		    SessionDataIn.IcChallenge_len = MutualAuthDataIn.IcChallengeLength;
		    cmnMemCopy(SessionDataIn.TermRandom, &MutualAuthDataOut.S[64], MutualAuthDataOut.S_len - 64);
		    SessionDataIn.TermRandom_len = MutualAuthDataOut.S_len - 64;
		}

		PROT_WRITE_EX0(PROT_FUNCLIB3, "cryptoPrepareSessionGOST()")
		ilRet = cryptoPrepareSessionGOST(phCrd->hCrypto, &SessionDataIn, phCrd->KeyVer, phCrd->AppVer);
		PROT_WRITE_EX1(PROT_FUNCLIB3, "cryptoPrepareSessionGOST()=%u", ilRet)
		if(ilRet)
			goto End;
    }

    phCrd->ifSM = 1;

End:
    PROT_WRITE_EX1(PROT_FUNCLIB3, "flAppTerminalAuthEx()=%u", ilRet)
    return ilRet;
}

IL_FUNC IL_RETCODE _flAppAuthRequestGetCryptogramm(
		IL_CARD_HANDLE* phCrd, IL_BYTE ifOnline, 
		IL_BYTE* pAuthDataIn, IL_DWORD dwAuthDataInLen,
		IL_BYTE* ia_in,  IL_WORD* ia_in_len,
		IL_BYTE* ia_out, IL_WORD* ia_out_len)
{
    IL_RETCODE ilRet;
    IL_BYTE KeyId;
    IL_WORD i;
    IL_DWORD dwTmp;
    IL_BYTE* pTmp;
    IL_TAG taglist_ver10[]	= {IL_TAG_9F15, IL_TAG_9F1C, IL_TAG_9F37, IL_TAG_9F21, IL_TAG_9F03, IL_TAG_DF02};
    IL_TAG taglist[]		= {IL_TAG_9F15, IL_TAG_9F37, IL_TAG_9F21, IL_TAG_9F03, IL_TAG_DF02};
	IL_TAG *pTagList		= phCrd->AppVer == UECLIB_APP_VER_10 ? taglist_ver10 : taglist;
	IL_WORD taglist_len		= phCrd->AppVer == UECLIB_APP_VER_10 ? sizeof(taglist_ver10)/sizeof(IL_TAG) : sizeof(taglist)/sizeof(IL_TAG);

	IL_WORD max_in_len  = *ia_in_len;

	// формируем данные для вычисления криптограммы (MSG)
    *ia_in_len = 0;
    for(i = 0; i < taglist_len; i++)
    {
        if((ilRet = TagFind(pAuthDataIn, dwAuthDataInLen, pTagList[i], &dwTmp, &pTmp, 0)) != 0)
		{
			PROT_WRITE_EX2(PROT_FUNCLIB1, "TagFind(%04X)=%u", pTagList[i], ilRet)
            return ilRet;
		}
		if(*ia_in_len + AddTag(pTagList[i], NULL, dwTmp, NULL) > max_in_len)
			return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
        *ia_in_len += (IL_WORD)AddTag(pTagList[i], pTmp, dwTmp, &ia_in[*ia_in_len]);
    }

  	// запрашиваем у ИД-приложения криптограмму аутентификации ИД-приложения
	if(ifOnline)
		KeyId = phCrd->ifGostCrypto ? IL_KEYTYPE_29_MK_IC_ID_GOST : IL_KEYTYPE_2A_MK_IC_ID_DES;
	else
		KeyId = phCrd->ifGostCrypto ? IL_KEYTYPE_17_IC_ID_GOST : IL_KEYTYPE_18_IC_ID_RSA;
	if((ilRet = clAppInternalAuth(phCrd, KeyId, ia_in, *ia_in_len, ia_out, ia_out_len)) != 0)
        return ilRet;


	// проверяем наличие необходимых данных в ответе карты
    if((ilRet = TagFind(ia_out, *ia_out_len, IL_TAG_9F36, &dwTmp, &pTmp, 1)) != 0)
        return ilRet;
    if((ilRet = TagFind(ia_out, *ia_out_len, IL_TAG_9F35, &dwTmp, &pTmp, 1)) != 0)
        return ilRet;
	if(phCrd->AppVer != UECLIB_APP_VER_10 && ((ilRet = TagFind(ia_out, *ia_out_len, IL_TAG_DF28, &dwTmp, &pTmp, 1)) != 0))
		return ilRet;
	if((ilRet = TagFind(ia_out, *ia_out_len, IL_TAG_5F74, &dwTmp, &pTmp, 1)) != 0)
        return ilRet;
	// для карты 1.0 идентификатор тега 5F74 заменяем на 9F26!!!
	if(phCrd->AppVer == UECLIB_APP_VER_10)
	{
		pTmp[0] = 0x9F; pTmp[1] = 0x26;
	}

	return 0;
}

IL_TAG tag_list_headerV10[]		= { IL_TAG_9F15, IL_TAG_9F1C, IL_TAG_9F37, IL_TAG_9F21, IL_TAG_9F03, IL_TAG_DF02 };
IL_TAG tag_list_header[]		= { IL_TAG_9F15, IL_TAG_9F37, IL_TAG_9F21, IL_TAG_9F03, IL_TAG_DF02, IL_TAG_9F1C, IL_TAG_9F1D };
IL_TAG tag_list_card_respV10[]	= { IL_TAG_9F36, IL_TAG_9F35, IL_TAG_9F26 };
IL_TAG tag_list_card_resp[]		= { IL_TAG_9F36, IL_TAG_9F35, IL_TAG_DF28, IL_TAG_5F74 };
IL_TAG tag_list_pan_snils[]		= { IL_TAG_5A, IL_TAG_DF27 }; 
IL_TAG tag_list_utility_data[]  = { IL_TAG_C2, IL_TAG_9F08, IL_TAG_9F1E, IL_TAG_DF7F };

IL_FUNC IL_RETCODE flAppAuthRequest(IL_CARD_HANDLE* phCrd, IL_BYTE ifOnline, IL_BYTE* pAuthDataIn, IL_DWORD dwAuthDataInLen, IL_BYTE* pAuthDataOut, IL_WORD* pwAuthDataOutLen)
{
    IL_RETCODE ilRet;
    IL_WORD i;
    IL_BYTE ia_in[256];
    IL_WORD ia_in_len;
    IL_BYTE ia_out[256];
    IL_WORD ia_out_len; 
	IL_TAG *pTagList;
	IL_WORD TagListLen;
    IL_WORD ofs = 0;
    IL_DWORD dwLen;
    IL_BYTE* pTmp;
	IL_WORD max_len;
	
    PROT_WRITE_EX0(PROT_FUNCLIB1, "flAppAuthRequest()")
	PROT_WRITE_EX2(PROT_FUNCLIB2, "IN: AuthDataIn[%lu]=%s", dwAuthDataInLen, bin2hex(protBuf, pAuthDataIn, dwAuthDataInLen))

	// запрашиваем у ИД-приложения криптограмму аутентификации ИД-приложения
	ia_in_len = sizeof(ia_in);
	PROT_WRITE_EX0(PROT_FUNCLIB3, "_flAppAuthRequestGetCryptogramm()")
	ilRet = _flAppAuthRequestGetCryptogramm(phCrd, ifOnline, pAuthDataIn, dwAuthDataInLen, ia_in, &ia_in_len, ia_out, &ia_out_len);
	PROT_WRITE_EX1(PROT_FUNCLIB3, "_flAppAuthRequestGetCryptogramm()=%u", ilRet);
	if(ilRet)
		goto End;

	// установим максимальный размер выходного буфера для запроса
	max_len = *pwAuthDataOutLen;

	// добавим теги заголовка запроса 
	// V10: 9F15, 9F1C, 9F37, 9F21, 9F03, DF02
	// V11: 9F15, 9F37, 9F21, 9F03, DF02, 9F1C, 9F1D
	pTagList   = phCrd->AppVer == UECLIB_APP_VER_10 ? tag_list_headerV10 : tag_list_header;
	TagListLen = phCrd->AppVer == UECLIB_APP_VER_10 ? sizeof(tag_list_headerV10)/sizeof(IL_TAG) : sizeof(tag_list_header)/sizeof(IL_TAG);
	for(i = 0; i < TagListLen; i++)
	{
		ilRet = TagFind(pAuthDataIn, dwAuthDataInLen, pTagList[i], &dwLen, &pTmp, 0);
		if(ilRet) {
			PROT_WRITE_EX2(PROT_FUNCLIB1, "TagFind(%04X)=%u", pTagList[i], ilRet)
			goto End;
		}
		if(ofs + AddTag(pTagList[i], NULL, dwLen, NULL) > max_len) {
			ilRet = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
		}
		ofs += (IL_WORD)AddTag(pTagList[i], pTmp, dwLen, &pAuthDataOut[ofs]);
	} 

	// добавим теги ответа карты на аутентификацию
	// V10: 9F36, 9F35, 9F26
	// V11: 9F36, 9F35, DF28, 5F74
	pTagList   = phCrd->AppVer == UECLIB_APP_VER_10 ? tag_list_card_respV10 : tag_list_card_resp;
	TagListLen = phCrd->AppVer == UECLIB_APP_VER_10 ? sizeof(tag_list_card_respV10)/sizeof(IL_TAG) : sizeof(tag_list_card_resp)/sizeof(IL_TAG);
	for(i = 0; i < TagListLen; i++)
	{
		ilRet = TagFind(ia_out, ia_out_len, pTagList[i], &dwLen, &pTmp, 0);
		if(ilRet) {
			PROT_WRITE_EX2(PROT_FUNCLIB1, "TagFind(%04X)=%u", pTagList[i], ilRet)
			goto End;
		}
		if(ofs + AddTag(pTagList[i], NULL, dwLen, NULL) > max_len){
			ilRet = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
		}
		ofs += (IL_WORD)AddTag(pTagList[i], pTmp, dwLen, &pAuthDataOut[ofs]);
	}

	if(ifOnline)
	{	
		if(phCrd->AppStatus == 0x7F)
		{	// для несогласованного состояния ИД-приложения добавляем тег CIN(45)
			ilRet = TagFind(pAuthDataIn, dwAuthDataInLen, IL_TAG_45, &dwLen, &pTmp, 0);
			if(ilRet) {
				PROT_WRITE_EX2(PROT_FUNCLIB1, "TagFind(%04X)=%u", IL_TAG_45, ilRet)
				goto End;
			}
			if(ofs + AddTag(IL_TAG_45, NULL, dwLen, NULL) > max_len) {
				ilRet = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
			}
			ofs += (IL_WORD)AddTag(IL_TAG_45, pTmp, dwLen, &pAuthDataOut[ofs]);
		}
		else
		{	// добавим теги 5A(PAN) и DF27(СНИЛС)
			for(i = 0; i < sizeof(tag_list_pan_snils)/sizeof(IL_TAG); i++)
			{
				ilRet = TagFind(pAuthDataIn, dwAuthDataInLen, tag_list_pan_snils[i], &dwLen, &pTmp, 0);
				if(ilRet) {
					PROT_WRITE_EX2(PROT_FUNCLIB1, "TagFind(%04X)=%u", tag_list_pan_snils[i], ilRet)
					goto End;
				}
				if(ofs + AddTag(tag_list_pan_snils[i], NULL, dwLen, NULL) > max_len) {
					ilRet = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
				}
				ofs += (IL_WORD)AddTag(tag_list_pan_snils[i], pTmp, dwLen, &pAuthDataOut[ofs]);
			}
		}
	}
	else
	{	// добавляем сертификаты открытого ключа ИД-приложения и открытого ключа эмитента ИД-приложения
		IL_BYTE cert[2048];
		IL_WORD cert_len;

		// считываем с карты и добавляем сертификат открытого ключа ИД-приложения
		if((ilRet = flGetAppPubKey(phCrd, cert, &cert_len)))
			goto End;
		if(ofs + cert_len > max_len) {
			ilRet = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
		}
		cmnMemCopy(&pAuthDataOut[ofs], cert, cert_len);
        ofs += cert_len;

		// считываем с карты и добавляем сертификат открытого ключа эмитента ИД-приложения
		ilRet = flGetIssPubKey(phCrd, cert, &cert_len);
		if(ilRet)
			goto End;
		if(ofs + cert_len > max_len) {
			ilRet = ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER; goto End;
		}
		cmnMemCopy(&pAuthDataOut[ofs], cert, cert_len);
        ofs += cert_len;
	}

	// добавим теги общих данных  
	if(phCrd->AppVer > UECLIB_APP_VER_10)
	{
		for(i = 0; i < sizeof(tag_list_utility_data)/sizeof(IL_TAG); i++)
		{
			ilRet = TagFind(pAuthDataIn, dwAuthDataInLen, tag_list_utility_data[i], &dwLen, &pTmp, 0);
			if(ilRet) {
				PROT_WRITE_EX2(PROT_FUNCLIB1, "TagFind(%04X)=%u", tag_list_utility_data[i], ilRet)
				goto End;
			}
			if(ofs + AddTag(tag_list_utility_data[i], NULL, dwLen, NULL) > max_len) {
				return ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER;
			}
			ofs += (IL_WORD)AddTag(tag_list_utility_data[i], pTmp, dwLen, &pAuthDataOut[ofs]);
		}
	}

    *pwAuthDataOutLen = ofs;

End:
	if(!ilRet)
		PROT_WRITE_EX2(PROT_FUNCLIB2, "OUT: AuthDataOut[%u]=%s", *pwAuthDataOutLen, bin2hex(protBuf, pAuthDataOut, *pwAuthDataOutLen));
    PROT_WRITE_EX1(PROT_FUNCLIB1, "flAppAuthRequest()=%u", ilRet);
    return ilRet;
}

IL_FUNC IL_RETCODE flAppAuthRequestIssSession(IL_CARD_HANDLE* phCrd, IL_BYTE* pAuthDataIn, IL_DWORD dwAuthDataInLen, IL_BYTE* pAuthDataOut, IL_WORD* pwAuthDataOutLen)
{
    IL_RETCODE ilRet;
	IL_BYTE ic_challenge[256];
	IL_WORD ic_challenge_len = phCrd->ifNeedMSE ? 8 : 16;

    PROT_WRITE_EX0(PROT_FUNCLIB1, "flAppAuthRequestIssSession()")

	// формируем основную часть запроса на аутентификацию ИД-приложения
	if((ilRet = flAppAuthRequest(phCrd, 1, pAuthDataIn, dwAuthDataInLen, pAuthDataOut, pwAuthDataOutLen)) != 0)
		goto End;
	
	if(phCrd->ifNeedMSE)
	{
	    IL_BYTE sanction_id = (phCrd->ifGostCrypto)?0x2B:0x2C;
	    
		if(!phCrd->ifGostCrypto)
		{
			PROT_WRITE_EX1(PROT_FUNCLIB3, "clMSE(31B48301%02X)", sanction_id)
			ilRet = clMSE(phCrd, 0x31, 0xB4, 0x83, 0x01, sanction_id);
			PROT_WRITE_EX2(PROT_FUNCLIB3, "clMSE(31B48301%02X)=%u", sanction_id, ilRet)
			if(ilRet)
				goto End;

			PROT_WRITE_EX1(PROT_FUNCLIB3, "clMSE(31B88301%02X)", sanction_id)
			ilRet = clMSE(phCrd, 0x31, 0xB8, 0x83, 0x01, sanction_id);
			PROT_WRITE_EX2(PROT_FUNCLIB3, "clMSE(31B88301%02X)=%u", sanction_id, ilRet)
			if(ilRet)
				goto End;
		}

		PROT_WRITE_EX1(PROT_FUNCLIB3, "clMSE(31B48301%02X)", sanction_id)
		ilRet = clMSE(phCrd, 0x31, 0xB4, 0x83, 0x01, sanction_id);
		PROT_WRITE_EX2(PROT_FUNCLIB3, "clMSE(31B48301%02X)=%u", sanction_id, ilRet)
		if(ilRet)
			goto End;

		PROT_WRITE_EX1(PROT_FUNCLIB3, "clMSE(31B88301%02X)", sanction_id)
		ilRet = clMSE(phCrd, 0x31, 0xB8, 0x83, 0x01, sanction_id);
		PROT_WRITE_EX2(PROT_FUNCLIB3, "clMSE(31B88301%02X)=%u", sanction_id, ilRet)
		if(ilRet)
			goto End;
	}

	// получаем случайное число карты и добавляем его к запросу на аутентификацию запроса для установления защищённой сессии с эмитентом	
	if((ilRet = clAppGetChallenge(phCrd, ic_challenge_len, ic_challenge)) != 0)
		goto End;
		
    *pwAuthDataOutLen += (IL_WORD)AddTag(IL_TAG_DF71, ic_challenge, ic_challenge_len, &pAuthDataOut[*pwAuthDataOutLen]);
    PROT_WRITE_EX2(PROT_FUNCLIB2, "OUT: IssAuthDataOut[%u]=", *pwAuthDataOutLen, bin2hex(protBuf, pAuthDataOut, *pwAuthDataOutLen))

End:
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flAppAuthRequestIssSession()=%lu", ilRet)
    return ilRet;
}

IL_FUNC IL_RETCODE flAppAuthCheckResponse(IL_CARD_HANDLE* phCrd, IL_BYTE* pAuthDataIn, IL_WORD wAuthDataInLen, IL_BYTE* pAuthResp, IL_WORD wAuthRespLen, IL_WORD* pAuthResult)
{
 	IL_RETCODE ilRet = 0;
	IL_INT i, j;
	IL_WORD ofs = 0;
	IL_BYTE Tmp[2048];
	IL_DWORD dwTmp;
    IL_BYTE* pTmpPtr;
	IL_BYTE ccaid_buf[2048] = {0};
    IL_DWORD ccaid_size;
    
	IL_TAG tags10[] = {IL_TAG_9F27, IL_TAG_5A, IL_TAG_9F36, IL_TAG_9F03, IL_TAG_9F35, IL_TAG_9F1C, IL_TAG_9F13};  
	IL_TAG tags_out10[] = {IL_TAG_9F27, IL_TAG_9F13};  
	IL_TAG tags11[] = {IL_TAG_9F38, IL_TAG_9F1C, IL_TAG_9F1D, IL_TAG_9F37, IL_TAG_9F15, IL_TAG_5A, IL_TAG_DF27, IL_TAG_9F13, IL_TAG_9F36, IL_TAG_9F35, IL_TAG_DF02, IL_TAG_DF74, IL_TAG_9F27};  
	IL_TAG tags_out11[] = {IL_TAG_9F38, IL_TAG_9F13, IL_TAG_DF74, IL_TAG_9F27};  
	IL_TAG* tags = (phCrd->AppVer == UECLIB_APP_VER_10)? tags10:tags11;
	IL_TAG* tags_out = (phCrd->AppVer == UECLIB_APP_VER_10)? tags_out10:tags_out11;
	WORD tags_size = (phCrd->AppVer == UECLIB_APP_VER_10)? (sizeof(tags10)/sizeof(tags10[0])):(sizeof(tags11)/sizeof(tags11[0]));
	WORD tags_out_size = (phCrd->AppVer == UECLIB_APP_VER_10)? (sizeof(tags_out10)/sizeof(tags_out10[0])):(sizeof(tags_out11)/sizeof(tags_out11[0]));

    PROT_WRITE_EX0(PROT_FUNCLIB1, "flAppAuthCheckResponse()")
	PROT_WRITE_EX2(PROT_FUNCLIB2, "IN: AuthResp[%u]=%s", wAuthRespLen, bin2hex(protBuf, pAuthResp, wAuthRespLen))

	//Получаем сертификат открытого ключа УЦ
    PROT_WRITE_EX0(PROT_FUNCLIB3, "prmGetParameterKeyVer(IL_PARAM_CCAID)")
	ilRet = prmGetParameterKeyVer(IL_PARAM_CCAID, phCrd->KeyVer, phCrd->ifGostCrypto, ccaid_buf, &ccaid_size);
    PROT_WRITE_EX1(PROT_FUNCLIB3, "prmGetParameterKeyVer(IL_PARAM_CCAID)=%u", ilRet)
	if(ilRet)
		goto End;

  	//формируем данные результата аутентификации
	for(i = 0; i < tags_size; i++)
	{
	    //ищем тэг в списке данных ответа
	    for(j = 0; j < tags_out_size; j++)
	    {  
	        if(tags[i] == tags_out[j])
	            break;
	    }
	    
	    //если не найден делаем поиск в данных запроса
	    if(j == tags_out_size)
    		ilRet = TagFind(pAuthDataIn, wAuthDataInLen, tags[i], &dwTmp, &pTmpPtr, 1);
    	else //иначе ищем в данных ответа
    		ilRet = TagFind(pAuthResp, wAuthRespLen, tags[i], &dwTmp, &pTmpPtr, 1);
		if(ilRet)
		{
			if(tags[i] == IL_TAG_5A || tags[i] == IL_TAG_DF27)
			{	//+++ ЗНАЧЕНИЯ PAN(5A) и SNILS(DF27) извлекаем из сертификата ИД-приложения
				IL_BYTE *p7F4E;
				IL_DWORD dw7F4E;
				IL_BYTE *p5F20;
				IL_DWORD dw5F20;
				IL_BYTE *p7F21;
				IL_DWORD dw7F21;
				// пытаемся получить указатель на данные обрамляющего тега 7F21
				if((ilRet = TagFind(pAuthDataIn, wAuthDataInLen, IL_TAG_7F21, &dw7F21, &p7F21, 0)) != 0) {
					PROT_WRITE_EX1(PROT_FUNCLIB3, "TagFind(7F21)=%u", ilRet)
					goto End;
				}
				// извлекаем сертифицируемые данные из сертификата открытого ключа
				if((ilRet = TagFind(p7F21, dw7F21, IL_TAG_7F4E, &dw7F4E, &p7F4E, 0)) != 0){
					PROT_WRITE_EX1(PROT_FUNCLIB3, "TagFind(7F4E)=%u", ilRet)
					goto End;
				}
				// получаем указатель на конкатенированный элемент 5F20 (PAN+SNILS)
				if((ilRet = TagFind(p7F4E, dw7F4E, IL_TAG_5F20, &dw5F20, &p5F20, 0)) != 0) {
					PROT_WRITE_EX1(PROT_FUNCLIB3, "TagFind(5F20)=%u", ilRet)
					goto End;
				}
				if(tags[i] == IL_TAG_5A) 
					// добавляем PAN(5A)
					ofs += (IL_WORD)AddTag(IL_TAG_5A, p5F20, 10, &Tmp[ofs]);
				else
					// добавляем СНИЛС(DF27)
					ofs += (IL_WORD)AddTag(IL_TAG_DF27, p5F20+10, 6, &Tmp[ofs]);
				continue;
			}
			else
			{
				PROT_WRITE_EX2(PROT_FUNCLIB3, "TagFind(%04X)=%u", tags[i], ilRet)
				goto End;
			}
		}

		cmnMemCopy(&Tmp[ofs], pTmpPtr, (IL_WORD)dwTmp);
		ofs += (IL_WORD)dwTmp;
	}

	if((ilRet = TagFind(pAuthResp, wAuthRespLen, IL_TAG_9F4B, &dwTmp, &pTmpPtr, 0)) != 0) {
		PROT_WRITE_EX1(PROT_FUNCLIB3, "TagFind(9F4B)=%u", ilRet)
        goto End;
	}

    PROT_WRITE_EX0(PROT_FUNCLIB3, "cryptoCheckMessageSignature()")
    ilRet = cryptoCheckMessageSignature(Tmp, ofs, pTmpPtr, (IL_WORD)dwTmp, ccaid_buf, (IL_WORD)ccaid_size, phCrd->ifGostCrypto, phCrd->AppVer);	
    PROT_WRITE_EX1(PROT_FUNCLIB3, "cryptoCheckMessageSignature()=%u", ilRet)
	if(ilRet)
		goto End;

	if((ilRet = TagFind(pAuthResp, wAuthRespLen, IL_TAG_9F27, &dwTmp, &pTmpPtr, 0)) != 0) {
		PROT_WRITE_EX1(PROT_FUNCLIB3, "TagFind(9F27)=%u", ilRet)
        goto End;
	}

    if(dwTmp!=2)
        return ILRET_DATA_TAG_WRONG_LENGTH;

    //return Application authentication code
    *pAuthResult = (pTmpPtr[0]<<8) + pTmpPtr[1];
	PROT_WRITE_EX1(PROT_FUNCLIB2, "AuthResult=%u", *pAuthResult)

End:
    PROT_WRITE_EX1(PROT_FUNCLIB1, "flAppAuthCheckResponse()=%u", ilRet)
    return ilRet;
}

IL_FUNC IL_RETCODE flAppCitizenVerification(IL_CARD_HANDLE* phCrd, IL_BYTE PinNum, IL_BYTE* pPinBlock8, IL_BYTE* pbTriesRemained)
{
	IL_WORD ilRet;

	PROT_WRITE_EX1(PROT_FUNCLIB1, "flAppCitizenVerification(%u)", PinNum)
    PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppVerify()")
    ilRet = clAppVerify(phCrd, PinNum, pPinBlock8, pbTriesRemained);
    PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppVerify()=%u", ilRet)
	PROT_WRITE_EX2(PROT_FUNCLIB1, "flAppCitizenVerification(%u)=%u", PinNum, ilRet)
	
	return ilRet;
}

IL_FUNC IL_RETCODE flAppChangePIN(IL_CARD_HANDLE* phCrd, IL_BYTE PinNum, IL_BYTE* pNewPinBlock8)
{
	IL_WORD ilRet;

    PROT_WRITE_EX1(PROT_FUNCLIB1, "flAppChangePIN(%u)", PinNum)
    PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppChangeRefData()")
    ilRet = clAppChangeRefData(phCrd, PinNum, pNewPinBlock8, 8);
    PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppChangeRefData()=%u", ilRet)
	PROT_WRITE_EX2(PROT_FUNCLIB1, "flAppCitizenVerification(%u)=%u", PinNum, ilRet)

	return ilRet;
}

IL_FUNC IL_RETCODE flAppChangePUK(IL_CARD_HANDLE* phCrd, IL_BYTE* pNewPukBlock8)
{
	IL_WORD ilRet;

    PROT_WRITE_EX0(PROT_FUNCLIB1, "flAppChangePUK()");
    PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppChangeRefData()")
    ilRet = clAppChangeRefData(phCrd, IL_KEYTYPE_05_PUK, pNewPukBlock8, 8);
    PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppChangeRefData()=%u", ilRet)
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flAppChangePUK()=%u", ilRet)

	return ilRet;
}

IL_FUNC IL_RETCODE flAppUnlockPIN(IL_CARD_HANDLE* phCrd, IL_BYTE PinNum)
{
 	IL_WORD ilRet;
	
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flAppUnlockPIN(%u)", PinNum);
    PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppChangeRefData()")
    ilRet = clAppResetRetryCounter(phCrd, PinNum);
    PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppChangeRefData()=%u", ilRet)
	PROT_WRITE_EX2(PROT_FUNCLIB1, "flAppUnlockPIN(%u)=%u", PinNum, ilRet)

	return ilRet;
}

IL_FUNC IL_RETCODE flReadBinary(IL_CARD_HANDLE* phCard, IL_WORD wOffset, IL_WORD wOutLength, IL_BYTE* pOut)
{
    IL_RETCODE ilRet = 0;
    IL_WORD toRead;
    IL_WORD wCurrOffset;
    IL_WORD wCurrLen;
    IL_WORD MAX_LEN = phCard->ifSM ? 224 : 248;

    PROT_WRITE_EX0(PROT_FUNCLIB1, "flReadBinary()");
	PROT_WRITE_EX2(PROT_FUNCLIB2, "IN: Offset=%u OutLen=%u", wOffset, wOutLength);

    for(toRead = wOutLength, wCurrOffset = wOffset; toRead>0; )
    {
        wCurrLen = (toRead>MAX_LEN) ? MAX_LEN : toRead;
		PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppReadBinary()")
        ilRet = clAppReadBinary(phCard, wCurrOffset, wCurrLen, &pOut[wCurrOffset - wOffset]); 
		PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppReadBinary()=%u", ilRet)
        if(ilRet)
            break;

        wCurrOffset += wCurrLen;
        toRead -= wCurrLen;
    }

	if(!ilRet && wOutLength < 2*1024)
		PROT_WRITE_EX2(PROT_FUNCLIB2, "OUT: Data[%u]=%s", wOutLength, bin2hex(protBuf, pOut, wOutLength));
    PROT_WRITE_EX1(PROT_FUNCLIB1, "flReadBinary()=%u", ilRet);
    return ilRet;
}

IL_FUNC IL_RETCODE flReadBinaryEx(IL_CARD_HANDLE* phCard, IL_WORD wOffset, IL_BYTE* pOut, IL_WORD wBufLength, IL_WORD* pwOutLength)
{
    IL_RETCODE ilRet = 0;

    IL_WORD toRead;
    IL_WORD wCurrOffset;
    IL_WORD wCurrLen;
    IL_WORD wOutLen;
    IL_WORD MAX_LEN = phCard->ifSM ? 224 : 248;

    PROT_WRITE_EX0(PROT_FUNCLIB1, "flReadBinaryEx()");
	PROT_WRITE_EX2(PROT_FUNCLIB2, "IN: Offset=%u BufLen=%u", wOffset, wBufLength)

    for(toRead = wBufLength, wCurrOffset = wOffset; toRead>0; )
    {
        wCurrLen = (toRead>MAX_LEN) ? MAX_LEN : toRead;
		PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppReadBinaryEx()")
        ilRet = clAppReadBinaryEx(phCard, wCurrOffset, wCurrLen, &pOut[wCurrOffset - wOffset], &wOutLen); 
		PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppReadBinaryEx()=%u", ilRet)
        if(ilRet)
            break;

        if(wCurrLen != wOutLen)
        {
            wCurrOffset+=wOutLen;
            toRead-=wOutLen;
            break;
        }

        wCurrOffset+=wCurrLen;
        toRead-=wCurrLen;
    }

    *pwOutLength = wCurrOffset;

	if(!ilRet)
		PROT_WRITE_EX2(PROT_FUNCLIB2, "OUT: Data[%u]=%s", *pwOutLength, bin2hex(protBuf, pOut, *pwOutLength));
    PROT_WRITE_EX1(PROT_FUNCLIB1, "flReadBinaryEx()=%u", ilRet);
    return ilRet;
}

IL_FUNC IL_RETCODE flUpdateBinary(IL_CARD_HANDLE* phCard, IL_WORD wOffset, IL_WORD wInLength, IL_BYTE* pIn)
{
    IL_RETCODE ilRet = 0;
    IL_WORD toWrite;
    IL_WORD wCurrOffset;
    IL_WORD wCurrLen;
    IL_WORD MAX_LEN = phCard->ifSM ? 224 : 248;
	IL_WORD i; 

    PROT_WRITE_EX0(PROT_FUNCLIB1, "flUpdateBinary()");
	if(wInLength < 2*1024)
		PROT_WRITE_EX3(PROT_FUNCLIB2, "IN: Offset=%u Data[%u]=%s", wOffset, wInLength, bin2hex(protBuf, pIn, wInLength));

    for(i = 0, toWrite = wInLength, wCurrOffset = wOffset; toWrite>0; i += wCurrLen)
    {
        wCurrLen = (toWrite>MAX_LEN) ? MAX_LEN : toWrite;
		PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppUpdateBinary()")
        ilRet = clAppUpdateBinary(phCard, wCurrOffset, &pIn[i], wCurrLen); 
		PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppUpdateBinary()=%u", ilRet)
        if(ilRet)
            break;

        wCurrOffset += wCurrLen;
        toWrite -= wCurrLen;
    }

    PROT_WRITE_EX1(PROT_FUNCLIB1, "flUpdateBinary()=%u", ilRet);
    return ilRet;
}

IL_FUNC IL_RETCODE flIssuerAuth(IL_CARD_HANDLE* phCrd, IL_BYTE* pDataIn, IL_WORD wDataInLen, IL_BYTE* pDataOut, IL_WORD* pwDataOutLen)
{
    IL_RETCODE ilRet = 0;
    IL_BYTE KeyType = phCrd->ifGostCrypto ? IL_KEYTYPE_2B_MK_SM_ID_GOST : IL_KEYTYPE_2C_MK_SM_ID_DES;

    PROT_WRITE_EX0(PROT_FUNCLIB1, "flIssuerAuth");
	PROT_WRITE_EX2(PROT_FUNCLIB2, "IN: Data[%u]=%s", wDataInLen, bin2hex(protBuf, pDataIn, wDataInLen))

    phCrd->ifSM = 0;
	PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppMutualAuth()")
    ilRet = clAppMutualAuth(phCrd, 0x00, KeyType, pDataIn, wDataInLen, pDataOut, pwDataOutLen);
	PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppMutualAuth()=%u", ilRet)
	phCrd->ifSM = 1;
    if(!ilRet)
		PROT_WRITE_EX2(PROT_FUNCLIB2, "OUT: Data[%u]=%s", *pwDataOutLen, bin2hex(protBuf, pDataOut, *pwDataOutLen));
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flIssuerAuth()=%u", ilRet);
    return ilRet;
}

IL_FUNC IL_RETCODE flChkApduAllowedResponse(IL_APDU_PACK_ELEM* ilApduPackElem)
{
	IL_WORD ilRet = ILRET_APDU_RES_NOT_ALLOWED;
	IL_BYTE i;

	PROT_WRITE_EX0(PROT_FUNCLIB3, "flChkApduAllowedResponse()")
	for(i = 0; i < ilApduPackElem->allowed_res_len; i++)
	{
		if(ilApduPackElem->Apdu.SW1 == ilApduPackElem->allowed_res[i*2]
				&& ilApduPackElem->Apdu.SW2 == ilApduPackElem->allowed_res[i*2+1]) {
					ilRet = 0; break;
		}
	}

	PROT_WRITE_EX1(PROT_FUNCLIB3, "flChkApduAllowedResponse()=%u", ilRet)
	return ilRet;
}

IL_FUNC IL_RETCODE flRunApdu(IL_CARD_HANDLE* phCrd, IL_BYTE SM_MODE, IL_APDU_PACK_ELEM* ilApduElem)
{
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_FUNCLIB1, "flRunApdu()")
	ilRet = clAPDU(phCrd, &ilApduElem->Apdu, SM_MODE);
	if(!ilRet)
		ilRet = flChkApduAllowedResponse(ilApduElem);

	PROT_WRITE_EX1(PROT_FUNCLIB1, "flRunApdu()=%u", ilRet)
	return ilRet;
}


IL_FUNC IL_RETCODE flGetPassPhrase(IL_CARD_HANDLE *phCrd, IL_CHAR *pPassPhraseOut)
{
	IL_RETCODE ilRet = 0;
	IL_BYTE fileId[] = {0x00, 0x0A };
	IL_BYTE tmp[256] = {0};
	IL_WORD tmp_len;
	IL_BYTE *pData;
	IL_DWORD data_len;

	PROT_WRITE_EX0(PROT_FUNCLIB1, "flGetPassPhrase()")

	*pPassPhraseOut = '\0';

	// для карт версии 1.0. механизм конорольного приветствия отсутствует!!!
	if(phCrd->AppVer == UECLIB_APP_VER_10)
		goto End;

	// переходим в глобальный контекст
	if((ilRet = flSelectContext(phCrd, 0, 0, 0)) != 0)
		goto End;

	// селектируем файл 
	if((ilRet = flFileSelect(phCrd, 0, fileId, 2, NULL, NULL)) != 0)
	{	// зафиксируем ошибку в журнале!!!
		PROT_WRITE_EX0(PROT_FUNCLIB1, "ОШИБКА СЕЛЕКТИРОВАНИЯ ФАЙЛА КОНТРОЛЬНОГО ПРИВЕТСТВИЯ!!!");
		ilRet = 0;	// игнорируем ошибку и завершаем
		goto End;
	}

	// получаем фразу контрольного приветствия 
	PROT_WRITE_EX0(PROT_FUNCLIB2, "clAppGetData()")
	ilRet = clAppGetData(phCrd, IL_TAG_DF2A, tmp, &tmp_len);
	PROT_WRITE_EX1(PROT_FUNCLIB2, "clAppGetData()=%u", ilRet)
	if(ilRet)
	{	// зафиксируем ошибку в журнале!!!
		PROT_WRITE_EX0(PROT_FUNCLIB1, "ОШИБКА ПОЛУЧЕНИЯ ФРАЗЫ КОНТРОЛЬНОГО ПРИВЕТСТВИЯ!!!");
		ilRet = 0;	// игнорируем ошибку и завершаем
		goto End;
	}
	TagFind(tmp, tmp_len, IL_TAG_DF2A, &data_len, &pData, 0);
	if(pData == NULL)
	{	// зафиксируем ошибку в журнале!!!
		PROT_WRITE_EX0(PROT_FUNCLIB1, "НЕВЕРНЫЙ ФОРМАТ ФРАЗЫ КОНТРОЛЬНОГО ПРИВЕТСТВИЯ!!!");
		ilRet = 0;	// игнорируем ошибку и завершаем
		goto End;
	}
		
	// ограничиваем максимальную длину фразы
	if(data_len > PASS_PHRASE_MAX_LEN)
		pData[PASS_PHRASE_MAX_LEN] = 0;
		
	// конвертируем фразу контрольного приветствия
	Iso8859_2_Ansi((IL_CHAR*)pData, pPassPhraseOut);

End:
	PROT_WRITE_EX1(PROT_FUNCLIB2, "OUT: PassPhrase=%s", pPassPhraseOut)
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flGetPassPhrase()=%u", ilRet)
	return ilRet;
}

//
IL_FUNC IL_RETCODE flSetPassPhrase(IL_CARD_HANDLE *phCrd, IL_CHAR *pPassPhraseIn)
{
	IL_RETCODE ilRet = 0;
	IL_BYTE fileId[] = {0x00, 0x0A };
	IL_BYTE tmp[PASS_PHRASE_MAX_LEN+1];
	IL_WORD tmp_len;

	PROT_WRITE_EX0(PROT_FUNCLIB1, "flSettPassPhrase()")
	PROT_WRITE_EX1(PROT_FUNCLIB2, "IN: PassPhrase=%s", pPassPhraseIn)

	// для карт версии 1.0. механизм конорольного приветствия отсутствует!!!
	if(phCrd->AppVer == UECLIB_APP_VER_10)
		goto End;

	// переходим в глобальный контекст
	if((ilRet = flSelectContext(phCrd, 0, 0, 0)) != 0)
		goto End;

	// селектируем файл 
	if((ilRet = flFileSelect(phCrd, 0, fileId, 2, NULL, NULL)) != 0)
	{	// зафиксируем ошибку в журнале!!!
		PROT_WRITE_EX0(PROT_FUNCLIB1, "ОШИБКА СЕЛЕКТИРОВАНИЯ ФАЙЛА КОНТРОЛЬНОГО ПРИВЕТСТВИЯ!!!");
		ilRet = 0;	// игнорируем ошибку и завершаем
		goto End;
	}

	// ограничиваем максимальную длину фразы
	tmp_len = cmnStrLen(pPassPhraseIn);
	if(tmp_len > PASS_PHRASE_MAX_LEN)
		pPassPhraseIn[PASS_PHRASE_MAX_LEN] = 0;

	// конвертируем фразу контрольного приветствия
	Ansi_2_Iso8859(pPassPhraseIn, (IL_CHAR*)tmp);

	// записыаем фразу контрольного приветствия на карту 
	PROT_WRITE_EX0(PROT_FUNCLIB2, "clAppPutData()")
	ilRet = clAppPutData(phCrd, IL_TAG_DF2A, tmp, tmp_len);
	PROT_WRITE_EX1(PROT_FUNCLIB2, "clAppPutData()=%u", ilRet)

End:
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flSettPassPhrase()=%u", ilRet)
	return ilRet;
}



IL_FUNC IL_RETCODE flAppGetStatus(IL_CARD_HANDLE *phCrd, IL_BYTE *pStatusOut)
{
	IL_RETCODE ilRet = 0;
	IL_BYTE tmp[256];
	IL_WORD tmp_len;
	IL_BYTE *pData;
	IL_DWORD data_len;

	PROT_WRITE_EX0(PROT_FUNCLIB3, "flAppGetStatus()")
	// получаем статус приложения 
	if((ilRet = clAppGetData(phCrd, IL_TAG_DF2C, tmp, &tmp_len)) != 0)
		goto End;
	TagFind(tmp, tmp_len, IL_TAG_DF2C, &data_len, &pData, 0);
	if(pData == NULL) {
		ilRet = ILRET_DATA_CARD_FORMAT_ERROR; goto End;
	}
	if(data_len != 1) {
		ilRet = ILRET_DATA_CARD_FORMAT_ERROR; goto End;
	}

	*pStatusOut = *pData;
//---
//*pStatusOut = 0x7F;
//---
End:
	PROT_WRITE_EX1(PROT_FUNCLIB3, "flAppGetStatus()=%u", ilRet)
	return ilRet;
}

//
IL_FUNC IL_RETCODE flGetAppPubKey(IL_CARD_HANDLE *phCrd, IL_BYTE *pDataOut, IL_WORD *pwDataLenOut)
{
	IL_RETCODE ilRet;
	IL_WORD wFileId = phCrd->ifGostCrypto ? IL_FILEID_0B : IL_FILEID_0D;
	//!!!MICRON SPECIFIC !!!!!!!!!!!!!!!!!!!!!!!!!!!!
	IL_WORD wDataId = phCrd->ifNeedMSE ? 0xFFFF : 0; 
	//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

	PROT_WRITE_EX0(PROT_FUNCLIB1, "flGetAppPubKey()")
	if((ilRet = flSelectContext(phCrd, 0, 0, 0)) == 0)
		ilRet = flAppReadBlock(phCrd, wFileId, wDataId, pDataOut, pwDataLenOut); 

	if(!ilRet)
		PROT_WRITE_EX2(PROT_FUNCLIB2, "OUT: AppPubKey[%u]=%s", *pwDataLenOut, bin2hex(protBuf, pDataOut, *pwDataLenOut));
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flGetAppPubKey()=%u", ilRet)
	
	return ilRet;
}

IL_FUNC IL_RETCODE flGetIssPubKey(IL_CARD_HANDLE *phCrd, IL_BYTE *pDataOut, IL_WORD *pwDataLenOut)
{
	IL_RETCODE ilRet;
	IL_WORD wFileId = phCrd->ifGostCrypto ? IL_FILEID_0C : IL_FILEID_0E;
	//!!!MICRON SPECIFIC !!!!!!!!!!!!!!!!!!!!!!!!!!!!
	IL_WORD wDataId = phCrd->ifNeedMSE ? 0xFFFF : 0; 
	//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	
	PROT_WRITE_EX0(PROT_FUNCLIB1, "flGetIssPubKey()")
 	if((ilRet = flSelectContext(phCrd, 0, 0, 0)) == 0)
		ilRet = flAppReadBlock(phCrd, wFileId, wDataId, pDataOut, pwDataLenOut); 
	if(!ilRet)
		PROT_WRITE_EX2(PROT_FUNCLIB2, "OUT: IssPubKey[%u]=%s", *pwDataLenOut, bin2hex(protBuf, pDataOut, *pwDataLenOut));
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flGetIssPubKey()=%u", ilRet)
	
	return ilRet;
}

IL_FUNC IL_RETCODE flGetAuthPubKey(IL_CARD_HANDLE *phCrd, IL_BYTE *pDataOut, IL_WORD *pwDataLenOut)
{
	IL_RETCODE ilRet;
	IL_WORD wFileId = IL_FILEID_11;
	//!!!MICRON SPECIFIC !!!!!!!!!!!!!!!!!!!!!!!!!!!!
	IL_WORD wDataId = phCrd->ifNeedMSE ? 0xFFFF : 0; 
	//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

	PROT_WRITE_EX0(PROT_FUNCLIB1, "flGetAuthPubKey()")
 	if((ilRet = flSelectContext(phCrd, 0, 0, 0)) == 0)
		ilRet = flAppReadBlock(phCrd, wFileId, wDataId, pDataOut, pwDataLenOut); 
	if(!ilRet) 
		PROT_WRITE_EX2(PROT_FUNCLIB2, "OUT: AuthPubKey[%u]=%s", *pwDataLenOut, bin2hex(protBuf, pDataOut, *pwDataLenOut));
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flGetIssPubKey()=%u", ilRet)

	return ilRet;
}

IL_FUNC IL_RETCODE flMakeDigitalSignature(IL_CARD_HANDLE *phCrd, 
										  IL_BYTE *pAuthRequest, IL_WORD wAuthRequestLen,
										  IL_BYTE *pSignOut, IL_WORD *pwSignLenOut,
										  IL_BYTE *pCertChainOut, IL_WORD *pwCertChainOutLen)
{
	IL_RETCODE ilRet;
	IL_DWORD dwTmpLen = 0;
	IL_BYTE* pTmpPtr;
    IL_BYTE tmp[4096]; 
	IL_WORD wTmpLen = 0;

	PROT_WRITE_EX0(PROT_FUNCLIB1, "flMakeDigitalSignature()")
	PROT_WRITE_EX2(PROT_FUNCLIB2, "IN: AuthRequest[%u]=%s", wAuthRequestLen, bin2hex(protBuf, pAuthRequest, wAuthRequestLen))
	
    ilRet = TagFind(pAuthRequest, wAuthRequestLen, IL_TAG_DF02, &dwTmpLen, &pTmpPtr, 0);
	if(ilRet) {
		PROT_WRITE_EX1(PROT_FUNCLIB1, "TagFind(DF02)=%u", ilRet)
        goto End;
	}

	PROT_WRITE_EX0(PROT_FUNCLIB3, "clAppComputeDigitalSignature()")
    ilRet = clAppComputeDigitalSignature(phCrd, pTmpPtr, (IL_WORD)dwTmpLen);
	PROT_WRITE_EX1(PROT_FUNCLIB3, "clAppComputeDigitalSignature()=%u", ilRet)
    if(ilRet)
        goto End;
    
    if(pwSignLenOut != NULL)
    {    
		if(dwTmpLen > *pwSignLenOut) {
            ilRet = ILRET_BUFFER_TOO_SMALL; goto End;
		}
            
        *pwSignLenOut = (IL_WORD)dwTmpLen;   
    }

    if(pSignOut != NULL)
        cmnMemCopy(pSignOut, pTmpPtr, (IL_WORD)dwTmpLen);
    
    ilRet = flGetAuthPubKey(phCrd, tmp, &wTmpLen);    
    if(ilRet)
        goto End;

    if(pwCertChainOutLen != NULL)
    {
         if(wTmpLen > *pwCertChainOutLen)
            return ILRET_BUFFER_TOO_SMALL;
            
        *pwCertChainOutLen = wTmpLen;   
       
    }
    
    if(pCertChainOut != NULL)
        cmnMemCopy(pCertChainOut, tmp, wTmpLen);

	PROT_WRITE_EX2(PROT_FUNCLIB2, "OUT: SignOut[%u]=%s", *pwSignLenOut, bin2hex(protBuf, pSignOut, *pwSignLenOut))
	PROT_WRITE_EX2(PROT_FUNCLIB2, "OUT: SignOut[%u]=%s", *pwCertChainOutLen, bin2hex(protBuf, pCertChainOut, *pwCertChainOutLen))

End:
	PROT_WRITE_EX1(PROT_FUNCLIB1, "flMakeDigitalSignature()=%u", ilRet)
	return ilRet;
}

IL_FUNC IL_RETCODE flCheckCertificateSP(IL_CARD_HANDLE* phCrd, IL_BYTE* pKeyCertIn, IL_WORD wKeyCertInLen, BYTE ifGost)
{
    IL_RETCODE ilRet;
    IL_DWORD dwLen7F4E;
    IL_BYTE* p7F4E;
    IL_DWORD dwLen5F37;
    IL_BYTE* p5F37;
	IL_BYTE* p7F21;
	IL_DWORD dwLen7F21;
	IL_BYTE* _pKeyCertIn = pKeyCertIn;
	IL_WORD  _wKeyCertInLen = wKeyCertInLen;
	IL_BYTE ccaid_buf[2048] = {0};
    IL_DWORD ccaid_size;

    PROT_WRITE_EX0(PROT_FUNCLIB1, "flCheckCertificateSP()");
	PROT_WRITE_EX1(PROT_FUNCLIB2, "IN: IfGost=%u", ifGost)
	PROT_WRITE_EX2(PROT_FUNCLIB2, "IN: CertSP[%u]=%s", wKeyCertInLen, bin2hex(protBuf, pKeyCertIn, wKeyCertInLen))

    // пытаемся получить указатель на данные обрамляющего тега 7F21
	if((ilRet = TagFind(pKeyCertIn, wKeyCertInLen, IL_TAG_7F21, &dwLen7F21, &p7F21, 0)) == 0)
	{
		pKeyCertIn    = p7F21;
		wKeyCertInLen = (IL_WORD)dwLen7F21;
	}
	else if(ilRet != ILRET_DATA_TAG_NOT_FOUND) {
		PROT_WRITE_EX1(PROT_FUNCLIB1, "TagFind(7F21)=%u", ilRet)
		goto End;
	}

	// извлекаем сертифицируемые данные из сертификата открытого ключа
	if((ilRet = TagFind(pKeyCertIn, wKeyCertInLen, IL_TAG_7F4E, &dwLen7F4E, &p7F4E, 1)) != 0) {
		PROT_WRITE_EX1(PROT_FUNCLIB1, "TagFind(7F4E)=%u", ilRet)
		goto End;
	}
	
	// извлекаем цифорвую подпись из сертификата открытого ключа
	if((ilRet = TagFind(pKeyCertIn, wKeyCertInLen, IL_TAG_5F37, &dwLen5F37, &p5F37, 0)) != 0) {
		PROT_WRITE_EX1(PROT_FUNCLIB1, "TagFind(5F37)=%u", ilRet)
		goto End;
	}
	
	// проверяем сертифицируемые данные
/*
    if((ilRet = clAppPerformSecOperation(phCrd, 0x10, p7F4E, (IL_WORD)dwLen7F4E)))
		goto End;

	// проверяем цифровую подпись 
    if((ilRet = clAppPerformSecOperation(phCrd, 0x00, p5F37, (IL_WORD)dwLen5F37)))
		goto End;

	// проверяем формат сертификата
	if((ilRet = flCheckCertificateFormat(phCrd, _pKeyCertIn, _wKeyCertInLen)))
		goto End;
*/
    //Получаем сертификат открытого ключа УЦ
	if((ilRet = prmGetParameterKeyVer(IL_PARAM_CCAID, phCrd->KeyVer, ifGost, ccaid_buf, &ccaid_size)) != 0) {
		PROT_WRITE_EX1(PROT_FUNCLIB1, "prmGetParameterKeyVer(IL_PARAM_CCAID)=%u", ilRet)
		goto End;
	}
    
    
/*    
	// Проверяем формат сертификата открытого ключа УЦ
	if((ilRet = flCheckCertificateFormat(phCrd, _pKeyCertIn, _wKeyCertInLen)))
		goto End;
		
    ilRet = cryptoAuthServiceProvider(p7F4E, dwLen7F4E, p5F37, dwLen5F37, ccaid_buf, ccaid_size, ifGost, UECLIB_APP_VER_11);
    if(ilRet)
		goto End;
*/
	// Проверяем формат сертификата открытого ключа провайдера услуг
	if((ilRet = flCheckCertificateFormat(_pKeyCertIn, _wKeyCertInLen, 1, ifGost, IL_CERTTYPE_SP_ID)) != 0)
		goto End;

    //Функция cryptoAuthServiceProvider использована здесь только из-за совпадения функционала
    PROT_WRITE_EX0(PROT_FUNCLIB3, "cryptoCheckMessageSignature()");
    ilRet = cryptoCheckMessageSignature(p7F4E, (IL_WORD)dwLen7F4E, p5F37, (IL_WORD)dwLen5F37, ccaid_buf, (IL_WORD)ccaid_size, ifGost, UECLIB_APP_VER_11);
    PROT_WRITE_EX1(PROT_FUNCLIB3, "cryptoCheckMessageSignature()=%u", ilRet);
 
End:
    PROT_WRITE_EX1(PROT_FUNCLIB1, "flCheckCertificateSP()=%u", ilRet);
    return ilRet;
}

// 
IL_FUNC IL_RETCODE flGetCIN(IL_CARD_HANDLE* phCrd, IL_BYTE* pCIN)
{
    IL_RETCODE ilRet;
    IL_WORD wLen;

	PROT_WRITE_EX0(PROT_FUNCLIB3, "flGetCIN()")
    
    if((ilRet = clAppSelect(phCrd, 0x04, 0x00, ISD_AID, sizeof(ISD_AID), NULL, NULL)) == 0)
		if((ilRet = clAppGetData(phCrd, IL_TAG_CF, pCIN, &wLen)) == 0)
			ilRet = clAppSelect(phCrd, 0x04, 0x0C, ID_APP_AID, sizeof(ID_APP_AID), NULL, NULL);

	PROT_WRITE_EX1(PROT_FUNCLIB3, "flGetCIN()=%u", ilRet)
	return ilRet;
}

// 
IL_FUNC IL_BYTE flGetSectorVersion(IL_CARD_HANDLE* phCrd, IL_BYTE SectorId)
{
	IL_BYTE i;

	for(i = 0; i < phCrd->sectors.num_records; i++)
	{
		if(phCrd->sectors.rec[i].id == SectorId)
			return phCrd->sectors.rec[i].version;
	}

	return 0;
}

//
IL_FUNC IL_RETCODE flAppReadBinTlvBlock(IL_CARD_HANDLE* phCrd, IL_WORD SectorId, IL_WORD BlockId, IL_BYTE *pDataOut, IL_WORD *pwDataOutLen)
{
    IL_RETCODE ilRet;
    IL_BYTE tmp[256];
    IL_WORD file_len;

 	// селектируем указанный блок Tlv-данных
	ilRet = flSelectContext(phCrd, SectorId, BlockId, 0);
	if(ilRet)
		return ilRet;

    // считываем TLV-заголовок 
    //ВНИМАНИЕ!!! Потенциальная угроза: при чтении TLV файла с длиной данных меньшей 3x байт возможна ошибка
    ilRet = clAppReadBinary(phCrd, 0, 5, tmp);
    if(ilRet)
		goto End;

	// вычислим общую длину считываемых бинарных данных
    file_len = (IL_WORD)(GetTagLen(tmp) + GetLenLen(tmp) + GetDataLen(tmp));

	// проверим непревышение размера выходного буфера 
	if(file_len > *pwDataOutLen)
	{
		ilRet = ILRET_OPLIB_BINTLV_BUF_IS_OVER; goto End;
	}

	// считываем бинарные данные TLV-файла
	*pwDataOutLen = 0;
    if(pDataOut != NULL)
    {   
        ilRet = flReadBinary(phCrd, 0, file_len, pDataOut);
        if(ilRet)
			goto End;

	    *pwDataOutLen = file_len;
    }

End:
    return ilRet;
}


