#include "FuncLib.h"
#include "HAL_Crypto.h"
#include "SmLib.h"
#include "sm_error.h"
#include "CardLib.h"
#include "TLV.h"
#include "Tag.h"
#include "FileID.h"
#include "HAL_Parameter.h"
#include "HAL_Protocol.h"
#include "HAL_Common.h"
#include "KeyType.h"
#include "il_error.h"
#include "HAL_Rtc.h"
#include "CertType.h"
#include "CertType.h"
#include "com_cryptodsb.h"


//FMD tags
static const IL_TAG TAG_PATH_V11_9F08[] = {IL_TAG_64,IL_TAG_9F08};
static const IL_TAG TAG_PATH_V11_E0[]   = {IL_TAG_64,IL_TAG_E0};
static const IL_TAG TAG_PATH_V11_E2[]   = {IL_TAG_64,IL_TAG_E2};
static const IL_TAG TAG_PATH_V11_5F24[] = {IL_TAG_64,IL_TAG_5F24};
static const IL_TAG TAG_PATH_V11_5F25[] = {IL_TAG_64,IL_TAG_5F25};
static const IL_TAG TAG_PATH_V11_9F1C[] = {IL_TAG_64,IL_TAG_9F1C};

static const IL_TAG TAG_PATH_V11_5F20[] = {IL_TAG_7F21,IL_TAG_7F4E,IL_TAG_5F20};
IL_BYTE SM_APP_AID[] = {0xA0, 0x00, 0x00, 0x04, 0x32, 0x80, 0x80};

IL_FUNC IL_RETCODE smCardError(IL_APDU* pApdu)
{
    IL_RETCODE ilRet = 0;

    switch(pApdu->Cmd[1])
    {
    case INS_SELECT:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6700:
            ilRet = ILRET_SM_SELECT_WRONG_LENGTH;
            break;
        case 0x6A82:
            ilRet = ILRET_SM_SELECT_FILE_NOT_FOUND;
            break;
        case 0x6A86:
            ilRet = ILRET_SM_SELECT_WRONG_PARAMETERS;
            break;
        default:
            ilRet = ILRET_SM_SELECT_ERROR;
        }
        break;
    case INS_MUT_AUTH:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6700:
            ilRet = ILRET_SM_MUTAUTH_WRONG_LENGTH;
            break;
        case 0x6982:
            ilRet = ILRET_SM_MUTAUTH_WRONG_CRYPTO;
            break;
        case 0x6985:
            ilRet = ILRET_SM_MUTAUTH_CONDITIONS;
            break;
        case 0x6A86:
            ilRet = ILRET_SM_MUTAUTH_WRONG_PARAMETERS;
            break;
        case 0x6A88:
            ilRet = ILRET_SM_MUTAUTH_KEY_NOT_FOUND;
            break;
        default:
            ilRet = ILRET_SM_MUTAUTH_ERROR;
        }
        break;
    case INS_GET_CHAL:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6700:
            ilRet = ILRET_SM_GETCHAL_WRONG_LENGTH;
            break;
        case 0x6A86:
            ilRet = ILRET_SM_GETCHAL_WRONG_PARAMETERS;
            break;
        default:
            ilRet = ILRET_SM_GETCHAL_ERROR;
        }
        break;
    case INS_CHANGE_DATA:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6700:
            ilRet = ILRET_SM_CHDATA_WRONG_LENGTH;
            break;
        case 0x6982:
            ilRet = ILRET_SM_CHDATA_WRONG_CRYPTO;
            break;
        case 0x6983:
            ilRet = ILRET_SM_CHDATA_KEY_BLOCKED;
            break;
        case 0x6985:
            ilRet = ILRET_SM_CHDATA_OP_NOT_COMPATIBLE_KEY_STATE;
            break;
        case 0x6A86:
            ilRet = ILRET_SM_CHDATA_WRONG_PARAMETERS;
            break;
        case 0x6A87:
            ilRet = ILRET_SM_CHDATA_LENGTH_NOT_COMPATIBLE_MODE;
            break;
        case 0x6A88:
            ilRet = ILRET_SM_CHDATA_KEY_NOT_FOUND;
            break;
        case 0x9602:
            ilRet = ILRET_SM_CHDATA_WRONG_FORMAT;
            break;
        default:
            if(pApdu->SW1 == 0x63)
                ilRet = ILRET_SM_CHDATA_WRONG_PIN_LENGTH;
            else
                ilRet = ILRET_SM_CHDATA_ERROR;
        }
        break;
    case INS_VERIFY:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6700:
            ilRet = ILRET_SM_VERIFY_WRONG_LENGTH;
            break;
        case 0x6982:
            ilRet = ILRET_SM_VERIFY_SECURITY_STATUS_NOT_SATISFIED;
            break;
        case 0x6983:
            ilRet = ILRET_SM_VERIFY_PASSWORD_BLOCKED;
            break;
        case 0x6A86:
            ilRet = ILRET_SM_VERIFY_WRONG_PARAMETERS;
            break;
        case 0x6A88:
            ilRet = ILRET_SM_VERIFY_PASSWORD_NOT_FOUND;
            break;
        default:
            if(pApdu->SW1 == 0x63)
                ilRet = ILRET_SM_VERIFY_WRONG_PASSWORD_PRESENTED;
            else
                ilRet = ILRET_SM_VERIFY_ERROR;
        }
        break;
    case INS_PERF_SEC_OP:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6700:
            ilRet = ILRET_SM_PERFSECOP_WRONG_LENGTH;
            break;
        case 0x6883:
            ilRet = ILRET_SM_PERFSECOP_BINDING_CMD_MISSED;
            break;
        case 0x6884:
            ilRet = ILRET_SM_PERFSECOP_BINDING_NOT_SUPPORTED;
            break;
        case 0x6982:
            ilRet = ILRET_SM_PERFSECOP_WRONG_CERT;
            break;
        case 0x6A80:
            ilRet = ILRET_SM_PERFSECOP_WRONG_DATA_STRUCT;
            break;
        case 0x6A86:
            ilRet = ILRET_SM_PERFSECOP_WRONG_PARAMETERS;
            break;
        case 0x6A88:
            ilRet = ILRET_SM_PERFSECOP_SIGN_KEY_ABSENT;
            break;
        default:
            ilRet = ILRET_SM_PERFSECOP_ERROR;
        }
        break;
    case INS_READ_BIN:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6700:
            ilRet = ILRET_SM_READBIN_WRONG_LENGTH;
            break;
        case 0x6981:
            ilRet = ILRET_SM_READBIN_WRONG_FILE_TYPE;
            break;
        case 0x6982:
            ilRet = ILRET_SM_READBIN_WRONG_SEC_CONDITIONS;
            break;
        case 0x6986:
            ilRet = ILRET_SM_READBIN_EF_NOT_SELECTED;
            break;
        case 0x6A86:
            ilRet = ILRET_SM_READBIN_WRONG_PARAMETERS;
            break;
        case 0x6B00:
            ilRet = ILRET_SM_READBIN_WRONG_OFFSET;
            break;
        default:
            ilRet = ILRET_SM_READBIN_ERROR;
        }
        break;
    case INS_UPDATE_BIN:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6700:
            ilRet = ILRET_SM_UPDBIN_WRONG_LENGTH;
            break;
        case 0x6981:
            ilRet = ILRET_SM_UPDBIN_WRONG_FILE_TYPE;
            break;
        case 0x6982:
            ilRet = ILRET_SM_UPDBIN_WRONG_SEC_CONDITIONS;
            break;
        case 0x6986:
            ilRet = ILRET_SM_UPDBIN_WRONG_FILE;
            break;
        case 0x6A86:
            ilRet = ILRET_SM_UPDBIN_WRONG_PARAMETERS;
            break;
        default:
            ilRet = ILRET_SM_UPDBIN_ERROR;
        }
        break;
    case INS_GET_DATA:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6700:
            ilRet = ILRET_SM_GETDATA_WRONG_LENGTH;
            break;
        case 0x6982:
            ilRet = ILRET_SM_GETDATA_WRONG_SEC_CONDITIONS;
            break;
        case 0x6986:
            ilRet = ILRET_SM_GETDATA_WRONG_PARAMETERS;
            break;
        case 0x6A88:
            ilRet = ILRET_SM_GETDATA_TAG_NOT_FOUND;
            break;
        default:
            ilRet = ILRET_SM_GETDATA_ERROR;
        }
        break;
    case INS_PUT_DATA:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6700:
            ilRet = ILRET_SM_PUTDATA_WRONG_LENGTH;
            break;
        case 0x6982:
            ilRet = ILRET_SM_PUTDATA_WRONG_SEC_CONDITIONS;
            break;
        case 0x6986:
            ilRet = ILRET_SM_PUTDATA_WRONG_PARAMETERS;
            break;
        case 0x6A88:
            ilRet = ILRET_SM_PUTDATA_TAG_NOT_FOUND;
            break;
        default:
            ilRet = ILRET_SM_PUTDATA_ERROR;
        }
        break;
    case INS_MSE:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6700:
            ilRet = ILRET_SM_MSE_WRONG_LENGTH;
            break;
        case 0x6984:
            ilRet = ILRET_SM_MSE_CANT_SET_CONTEXT;
            break;
        case 0x6A86:
            ilRet = ILRET_SM_MSE_WRONG_PARAMETERS;
            break;
        default:
            ilRet = ILRET_SM_MSE_ERROR;
        }
        break;
    case INS_AUTH_BEGIN:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6A80:
            ilRet = ILRET_SM_AUTH_BEGIN_WRONG_DATA;
            break;
        case 0x6A88:
            ilRet = ILRET_SM_AUTH_BEGIN_REF_DATA_ERROR;
            break;
		case 0x6D00:
			ilRet = ILRET_SM_NOT_ACTIVATED;
			break;
        default:
            ilRet = ILRET_SM_AUTH_BEGIN_ERROR;
        }
        break;
    case INS_AUTH_COMPLETE:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6A80:
            ilRet = ILRET_SM_AUTH_COMPLETE_WRONG_DATA;
            break;
        case 0x6A88:
            ilRet = ILRET_SM_AUTH_COMPLETE_REF_DATA_ERROR;
            break;
        default:
            ilRet = ILRET_SM_AUTH_COMPLETE_ERROR;
        }
        break;
    case INS_SP_SESS_INIT:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6700:
            ilRet = ILRET_SM_SP_SESS_WRONG_LENGTH;
            break;
        case 0x6985:
            ilRet = ILRET_SM_SP_SESS_CONDITIONS_NOT_SATISFIED;
            break;
        case 0x6A86:
            ilRet = ILRET_SM_SP_SESS_WRONG_PARAMETERS;
            break;
        default:
            ilRet = ILRET_SM_SP_SESS_ERROR;
        }
        break;
    case INS_SE_ACTIVATION:
        switch(SW(pApdu))
        {
        case 0x9000:
            ilRet = 0;
            break;
        case 0x6A80:
            ilRet = ILRET_SM_SE_ACTIVATION_WRONG_DATA;
            break;
        default:
            ilRet = ILRET_SM_SE_ACTIVATION_ERROR;
        }
        break;
    default:
        ilRet = ILRET_SM_ERROR;
    }
    return ilRet;
}

IL_FUNC IL_WORD smFillRecordsList(IL_BYTE *fci, IL_WORD fci_len, IL_RECORD_LIST *list, IL_BYTE ifMain)
{
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

    if(!fci || !list) 
        return ILRET_OPLIB_INVALID_ARGUMENT;

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
            TagFind(pPtr1, dwLen1, IL_TAG_DF21, &dwLen4, &pPtr4, 0);
            list->rec[num_records].version = *pPtr4;
        }
        pTmpPtr = pPtr1 + dwLen1;
        dwTmpLen -= dwLen1;
        num_records++;
    }
    list->num_records = num_records;

    return 0;
}

void toSmLog(IL_CHAR* str, IL_BYTE* buf, IL_DWORD len)
{
    char tmp1[1024];
    bin2hex(tmp1, buf, len);
    OutputDebugString("\n");    
    OutputDebugString(str);
    OutputDebugString(": ");
    OutputDebugString("\n");    
    OutputDebugString(tmp1);
}

void AddToSessionSmCounter(IL_BYTE* SessionSmCounter8, IL_BYTE bNumberToAdd)
{
    IL_WORD tmp;
    IL_BYTE c = bNumberToAdd;
    IL_WORD i;

    for(i = 0; i<8; i++)
    {
        tmp = SessionSmCounter8[7-i] + c;
        SessionSmCounter8[7-i] = (IL_BYTE)tmp;
        c = tmp>>8;
    }
}

IL_FUNC IL_RETCODE smRdrInit(IL_HANDLE_CRYPTO* phCrypto, IL_READER_SETTINGS ilRdrSettings)
{
    HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)phCrypto;

    protWrite(PROT_SMLIB, "smInit");

    return crInit(myhCrypto->hSm.hRdr, ilRdrSettings); 
}

IL_FUNC IL_RETCODE smRdrDeinit(IL_HANDLE_CRYPTO* phCrypto)
{
    HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)phCrypto;

    protWrite(PROT_SMLIB, "smRdrDeinit");

    return crDeinit(myhCrypto->hSm.hRdr); 
}

IL_FUNC IL_RETCODE smGetAppVersion(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* data, IL_WORD data_len)
{
    IL_RETCODE ilRet;
    IL_BYTE* p9F08;
    IL_DWORD dwLen9F08;

    protWrite(PROT_SMLIB, "smGetAppVersion");

    // пытаемся определить версию ИД-приложения в соответствии со спецификацией карты 1.1
    ilRet = TagFindByPath(data, data_len, TAG_PATH_V11_9F08, sizeof(TAG_PATH_V11_9F08)/sizeof(TAG_PATH_V11_9F08[0]), &dwLen9F08, &p9F08, 0);
    if(ilRet)
        return ilRet;

    if(dwLen9F08 != 1)
        return ILRET_DATA_CARD_FORMAT_ERROR;
   //проверяем что данная версия МВ поддерживается терминалом
    if(UECLIB_SM_VER < p9F08[0])
        return ILRET_APP_VER_NOT_SUPPORTED;

	return 0;
}

IL_FUNC IL_RETCODE smInit(IL_HANDLE_CRYPTO* phCrypto, IL_READER_SETTINGS ilRdrSettings)
{
    IL_RETCODE ilRet = 0;
    HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)phCrypto;
    IL_BYTE out[256];
    WORD wLen;
    IL_BYTE term_date[3];

    protWrite(PROT_SMLIB, "smInit");

    ilRet = smRdrInit(phCrypto, ilRdrSettings);
    if(ilRet)
        return ilRet;

    ilRet = smOpen(phCrypto);
    if(ilRet)
        return ilRet;

    ilRet = smAppSelect(phCrypto, out, &wLen);
    if(ilRet)
        return ilRet;

    // определяем версию приложения
    if((ilRet = smGetAppVersion(phCrypto, out, wLen)))
        return ilRet;

	// получаем текущую дату операции
    if((ilRet = rtcGetCurrentDate(term_date)))
        return ilRet;

    ilRet = smGetData(phCrypto, IL_TAG_5F24, out, &wLen);
    if(ilRet)
		return ilRet;
    if(wLen != 3)
        return ILRET_DATA_CARD_FORMAT_ERROR;

    // проверяем дату окончания срока действия приложения
    if(rtcCompareDates(term_date, out) > 0)
        return ILRET_APP_EXPIRED;

    ilRet = smGetData(phCrypto, IL_TAG_5F25, out, &wLen);
    if(ilRet)
    return ilRet;
    if(wLen != 3)
        return ILRET_DATA_CARD_FORMAT_ERROR;

    // проверяем дату начала срока действия приложения
    if(rtcCompareDates(term_date, out) < 0)
        return ILRET_APP_NOT_ACTIVE_YET;

    ilRet = smGetData(phCrypto, IL_TAG_9F1C, out, &wLen);
    if(ilRet)
		return ilRet;

	ilRet = smReadCertificates(phCrypto);
    if(ilRet)
        return ilRet;

    return ilRet; 
}

IL_FUNC IL_RETCODE smDeinit(IL_HANDLE_CRYPTO* phCrypto)
{
    HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)phCrypto;

    protWrite(PROT_SMLIB, "smDeinit");

    return crDeinit(myhCrypto->hSm.hRdr); 
}

IL_FUNC IL_RETCODE smOpen(IL_HANDLE_CRYPTO* phCrypto)
{
    HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)phCrypto;

    protWrite(PROT_SMLIB, "smOpen");

    return crOpenSession(myhCrypto->hSm.hRdr);
}

IL_FUNC IL_RETCODE smClose(IL_HANDLE_CRYPTO* phCrypto)
{
    HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)phCrypto;

    protWrite(PROT_SMLIB, "smOpen");

    return crCloseSession(myhCrypto->hSm.hRdr);
}

IL_FUNC IL_RETCODE smAPDU(IL_HANDLE_CRYPTO* phCrypto, IL_APDU* pilApdu)
{
    IL_RETCODE ilRet;
    IL_CHAR str[IL_APDU_BUF_SIZE*2+100] = {0};
    IL_CHAR tmp_1[IL_APDU_BUF_SIZE*2+100] = {0};
    IL_CHAR tmp_2[IL_APDU_BUF_SIZE*2+100] = {0};
    HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)phCrypto;

    wsprintf(str, "APDU=[%s] Lc=[%04X] Le=[%04X] DataIn=[%s]", bin2hex(tmp_1, pilApdu->Cmd, 4), pilApdu->LengthIn, pilApdu->LengthExpected, bin2hex(tmp_2, pilApdu->DataIn, pilApdu->LengthIn));
    protWrite(PROT_SMLIB, str);
    //protWriteEx("***APDU=[%s] Lc=[%04X] Le=[%04X] DataIn=[%s]", bin2hex(tmp_1, pilApdu->Cmd, 4), pilApdu->LengthIn, pilApdu->LengthExpected, bin2hex(tmp_2, pilApdu->DataIn, pilApdu->LengthIn));

    ilRet = crAPDU(myhCrypto->hSm.hRdr, pilApdu);
    if(ilRet)
        return ilRet;

    if(SW(pilApdu) != 0x9000) 
    {
        ilRet =  smCardError(pilApdu);
        return ilRet;
    }

    wsprintf(str, "SW=[%02X%02X] Lo=[%04X] DataOut=[%s]", pilApdu->SW1, pilApdu->SW2, pilApdu->LengthOut, bin2hex(tmp_1, pilApdu->DataOut, pilApdu->LengthOut));
    protWrite(PROT_SMLIB, str);

    if(SW(pilApdu) != 0x9000) 
        ilRet =  smCardError(pilApdu);

    return ilRet;
}

IL_FUNC IL_RETCODE smSendAPDU(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* In, IL_WORD wInLen, IL_BYTE* Out, IL_WORD* pwOutLen)
{
    IL_RETCODE ilRet;
	IL_APDU ilApdu = {0};

    protWrite(PROT_SMLIB, "smSendAPDU");

	if(wInLen >= 4)
		cmnMemCopy(ilApdu.Cmd, In, 4);

	if(wInLen >= 5)
	{
		ilApdu.LengthIn = In[4];
		if(wInLen >= 5+ilApdu.LengthIn)
		{
			cmnMemCopy(ilApdu.DataIn, &In[5], ilApdu.LengthIn);
			if(wInLen >= 6+ilApdu.LengthIn)
				ilApdu.LengthExpected = In[5+ilApdu.LengthIn];
		}
	}

	ilRet =  smAPDU(phCrypto, &ilApdu);
    if(ilRet)
        return ilRet;

	if(ilApdu.LengthOut && (Out != NULL))
	{
		cmnMemCopy(Out, ilApdu.DataOut, ilApdu.LengthOut);
	}

	if(pwOutLen != NULL)
	{
		*pwOutLen = ilApdu.LengthOut;
	}

    return ilRet;
}

IL_FUNC IL_RETCODE smSelect(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE P1, IL_BYTE P2, IL_BYTE* pId, IL_BYTE IdLen, IL_BYTE* pOut, IL_WORD* pOutLength)
{
    IL_RETCODE ilRet;
    IL_APDU ilApdu;

    protWrite(PROT_SMLIB, "smSelect");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_SELECT;
    ilApdu.Cmd[2] = P1;
    ilApdu.Cmd[3] = P2;
    if(!pId) 
        ilApdu.LengthIn = 0;
    else
    {
        cmnMemCopy(ilApdu.DataIn, pId, IdLen);
        ilApdu.LengthIn = IdLen;
    }
    if((ilRet = smAPDU(phCrypto, &ilApdu)))
        return ilRet;

    //+++
    if(pOut && pOutLength)
    {
        *pOutLength = ilApdu.LengthOut;
        cmnMemCopy(pOut, ilApdu.DataOut, ilApdu.LengthOut);
    }

    return 0;
}

IL_FUNC IL_RETCODE smVerify(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE P2, IL_BYTE* pDataIn8, IL_BYTE* pbTriesRemained)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

    protWrite(PROT_SMLIB, "smVerify");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_VERIFY;
    ilApdu.Cmd[3] = P2;//01 - PIN or 05 - PUK
    ilApdu.LengthIn = 8;
    cmnMemCopy(ilApdu.DataIn, pDataIn8, 8);

    ilRet = smAPDU(phCrypto, &ilApdu);

    if(ilApdu.SW1 == 0x63)
        *pbTriesRemained = ilApdu.SW2 & 0x0F;
    else
        *pbTriesRemained = 0xFF;

    return ilRet;
}


IL_FUNC IL_RETCODE smChangeRefData(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE P2, IL_BYTE* pDataIn, IL_WORD wDataInLen)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

    protWrite(PROT_SMLIB, "smChangeRefData");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_CHANGE_DATA;
    ilApdu.Cmd[2] = 0x01;
    ilApdu.Cmd[3] = P2;//Key Id
    ilApdu.LengthIn = wDataInLen;
    cmnMemCopy(ilApdu.DataIn, pDataIn, wDataInLen);

    ilRet = smAPDU(phCrypto, &ilApdu);

    return ilRet;
}

IL_FUNC IL_RETCODE smAppSelect(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* pOut, IL_WORD* pwOutLen)
{
    IL_RETCODE ilRet;
    IL_BYTE out[256];
    IL_WORD out_len;

    protWrite(PROT_SMLIB, "smAppSelect");

    // селектируем приложение
    ilRet = smSelect(phCrypto, 0x04, 0x00, SM_APP_AID, sizeof(SM_APP_AID), out, &out_len);
    if(ilRet)
        return ilRet;

    // инициализируем выходной буфер возвращенными ИД-приложением карты данными   
    if(pOut)
        cmnMemCopy(pOut, out, out_len);
    if(pwOutLen)
        *pwOutLen = out_len;

    return 0;
}

IL_FUNC IL_RETCODE smGetData(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wTag, IL_BYTE* pOut, IL_WORD* pwOutLength)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;
    HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)phCrypto;

    protWrite(PROT_SMLIB, "smGetData");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_GET_DATA;
    ilApdu.Cmd[2] = wTag>>8;
    ilApdu.Cmd[3] = (IL_BYTE)wTag;
    ilRet = smAPDU(phCrypto, &ilApdu);
    if(ilRet)
        return ilRet;

    cmnMemCopy(pOut, ilApdu.DataOut, ilApdu.LengthOut);
    *pwOutLength = ilApdu.LengthOut;

    return 0;
}

IL_FUNC IL_RETCODE smGetChallenge(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wOutLength, IL_BYTE* pOut)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

    protWrite(PROT_SMLIB, "smGetChallenge");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_GET_CHAL;
    ilApdu.LengthExpected = wOutLength;
    ilRet = smAPDU(phCrypto, &ilApdu);
    if(ilRet)
        return ilRet;

    if(ilApdu.LengthOut!=wOutLength)
        return ILRET_CRD_LENGTH_NOT_MATCH;

    cmnMemCopy(pOut, ilApdu.DataOut, ilApdu.LengthOut);

    return 0;
}

IL_FUNC IL_RETCODE smMutualAuth(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE CLA, IL_BYTE P2, IL_BYTE* pDataIn, IL_WORD wDataInLen, IL_BYTE* pDataOut, IL_WORD* pDataOutLen)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

    protWrite(PROT_SMLIB, "smMutualAuth");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[0] = CLA;
    ilApdu.Cmd[1] = INS_MUT_AUTH;
    ilApdu.Cmd[3] = P2;//Key Id
    ilApdu.LengthIn = wDataInLen;
    cmnMemCopy(ilApdu.DataIn, pDataIn, wDataInLen);
    ilRet = smAPDU(phCrypto, &ilApdu);
    if(ilRet)
        return ilRet;

    *pDataOutLen = ilApdu.LengthOut;
    cmnMemCopy(pDataOut, ilApdu.DataOut, ilApdu.LengthOut);

    return 0;
}

IL_FUNC IL_RETCODE smActivation(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE CLA, IL_BYTE IfDeactivation)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

    protWrite(PROT_SMLIB, "smActivation");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[0] = CLA;
    ilApdu.Cmd[1] = INS_SE_ACTIVATION;
    ilApdu.Cmd[3] = IfDeactivation?0x7F:0x77;
    ilRet = smAPDU(phCrypto, &ilApdu);
    if(ilRet)
        return ilRet;

    return ilRet;
}

IL_FUNC IL_RETCODE smSpSessionInit(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* pDataIn14)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

    protWrite(PROT_SMLIB, "smMutualAuth");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[0] = 0x80;
    ilApdu.Cmd[1] = INS_SP_SESS_INIT;
    ilApdu.LengthIn = 14;
    cmnMemCopy(ilApdu.DataIn, pDataIn14, 14);
    ilRet = smAPDU(phCrypto, &ilApdu);
    if(ilRet)
        return ilRet;

    return ilRet;
}

IL_FUNC IL_RETCODE smReadBinary(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wOffset, IL_WORD wBufLength, IL_BYTE* pOut, IL_WORD* pwOutLen)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;
    HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)phCrypto;

    protWrite(PROT_SMLIB, "smReadBinary");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_READ_BIN;
    ilApdu.Cmd[2] = wOffset>>8;
    ilApdu.Cmd[3] = (IL_BYTE)wOffset;
    ilApdu.LengthExpected = wBufLength;
    ilRet = smAPDU(phCrypto, &ilApdu);
    if(ilRet)
        return ilRet;

    if(pwOutLen != NULL)
        *pwOutLen = ilApdu.LengthOut;
    cmnMemCopy(pOut, ilApdu.DataOut, ilApdu.LengthOut);

    return 0;
}

IL_FUNC IL_RETCODE smReadBinaryEx(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wOffset, IL_BYTE* pOut, IL_WORD wBufLength, IL_WORD* pwOutLength)
{
    IL_RETCODE ilRet = 0;

    IL_WORD toRead;
    IL_WORD wCurrOffset;
    IL_WORD wCurrLen;
    IL_WORD wOutLen;
    IL_WORD MAX_LEN = 248;

    protWrite(PROT_SMLIB, "smReadBinaryEx");

    for(toRead = wBufLength, wCurrOffset = wOffset; toRead>0; )
    {
        wCurrLen = (toRead>MAX_LEN)?MAX_LEN:toRead;
        ilRet = smReadBinary(phCrypto, wCurrOffset, wCurrLen, &pOut[wCurrOffset - wOffset], &wOutLen); 
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

    return ilRet;
}


IL_FUNC IL_RETCODE smReadFile(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wFileId, IL_BYTE* FileData, IL_WORD* pwFileDataLen)
{
    IL_RETCODE ilRet;
    IL_BYTE FileId[2];
    IL_WORD wTmp = 0;
    IL_BYTE Tmp[512] = {0};

    protWrite(PROT_SMLIB, "smReadFile");

    FileId[0] = wFileId>>8;
    FileId[1] = (IL_BYTE)wFileId;
    ilRet = smSelect(phCrypto, 0x00, 0x04, FileId, sizeof(FileId), Tmp, &wTmp);
    if(ilRet)
        return ilRet;

    ilRet = smReadBinaryEx(phCrypto, 0, FileData, *pwFileDataLen, pwFileDataLen);

    return ilRet;
}

IL_FUNC IL_RETCODE smReadBlock(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wFileId, IL_WORD wDataId, IL_BYTE* pDataOut, IL_WORD* pwDataOutLen)
{
    IL_RETCODE ilRet;
    IL_BYTE tmp[256];
    IL_WORD file_len;
    IL_BYTE FileId[2];

    protWrite(PROT_SMLIB, "smReadBlock");

    // селектируем указанный файл
    FileId[0] = wFileId>>8;
    FileId[1] = (IL_BYTE)wFileId;
    ilRet = smSelect(phCrypto, 0x00, 0x04, FileId, sizeof(FileId), NULL, NULL);
    if(ilRet)
        return ilRet;

    if(wDataId==0)
    {
        //READ BINARY (TLV header)
        //ВНИМАНИЕ!!! Потенциальная угроза: при чтении TLV файла с длиной данных меньшей 3x байт возможна ошибка
        ilRet = smReadBinary(phCrypto, 0, 5, tmp, NULL);
        if(ilRet)
            return ilRet;

        file_len = GetTagLen(tmp) + GetLenLen(tmp) + GetDataLen(tmp);

        if(pDataOut != NULL)
        {
            //READ BINARY (TLV header)
            ilRet = smReadBinaryEx(phCrypto, 0, pDataOut, file_len, pwDataOutLen);
            if(ilRet)
                return ilRet;
        }

        *pwDataOutLen = file_len;
    }
    else if(wDataId==0xFFFF)
    {	// Специальный случай чтения сертификата публичного ключа приложения!!!
        ilRet = smReadBinaryEx(phCrypto, 0, pDataOut, *pwDataOutLen, pwDataOutLen);
    }
    else
    {
        ilRet = smGetData(phCrypto, wDataId, pDataOut, pwDataOutLen);
    }
    return ilRet;
}

IL_FUNC IL_RETCODE _smAuthBegin(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE Cla, IL_BYTE* pIn, IL_WORD InLen, IL_BYTE* pOut, IL_WORD* pOutLen, IL_BYTE ifGost)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

    protWrite(PROT_SMLIB, "_smAuthBegin");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[0] = Cla;
    ilApdu.Cmd[1] = INS_AUTH_BEGIN;
    ilApdu.Cmd[3] = ifGost?0x21:0x22;
    ilApdu.LengthIn = InLen;
    cmnMemCopy(ilApdu.DataIn, pIn, InLen);
    ilRet = smAPDU(phCrypto, &ilApdu);
    if(ilRet)
        return ilRet;

    *pOutLen = ilApdu.LengthOut;
    cmnMemCopy(pOut, ilApdu.DataOut, ilApdu.LengthOut);

    return ilRet;
}

IL_FUNC IL_RETCODE smAuthBegin(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* pIn, IL_WORD InLen, IL_BYTE* pOut, IL_WORD* pOutLen, IL_BYTE ifGost)
{
    IL_RETCODE ilRet;
    IL_WORD wTmp;

    protWrite(PROT_SMLIB, "smAuthBegin");
    if(!ifGost)
    {
        ilRet = _smAuthBegin(phCrypto, 0x90, pIn, 16, pOut, &wTmp, 0);
        if(ilRet)
            return ilRet;

        *pOutLen = wTmp;

        ilRet = _smAuthBegin(phCrypto, 0x80, &pIn[16], InLen - 16, &pOut[*pOutLen], &wTmp, 0);
        if(ilRet)
            return ilRet;

        *pOutLen += wTmp;
    }
    else
    {
        ilRet = _smAuthBegin(phCrypto, 0x80, pIn, InLen, pOut, pOutLen, 1);
    }

    return ilRet;
}
IL_FUNC IL_RETCODE smAuthComplete(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* pIn, IL_WORD InLen, BYTE ifGost)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

    protWrite(PROT_SMLIB, "smAuthComplete");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[0] = 0x80;
    ilApdu.Cmd[1] = INS_AUTH_COMPLETE;
    //TODO -error in docs about P2
    //    ilApdu.Cmd[3] = ifGost?0x21:0x22;
    ilApdu.Cmd[3] = 0x00;
    ilApdu.LengthIn = InLen;
    cmnMemCopy(ilApdu.DataIn, pIn, InLen);
    ilRet = smAPDU(phCrypto, &ilApdu);
    if(ilRet)
        return ilRet;

    return ilRet;
}

IL_FUNC IL_RETCODE smPSO(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE CLA, IL_BYTE bP1, IL_BYTE bP2, IL_BYTE* pIn, IL_WORD wIn, IL_BYTE* pOut, IL_WORD* pwOut)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

    protWrite(PROT_SMLIB, "smPSO");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[0] = CLA;
    ilApdu.Cmd[1] = 0x2A;
    ilApdu.Cmd[2] = bP1;
    ilApdu.Cmd[3] = bP2;
    ilApdu.LengthIn = wIn;
    if(pwOut != NULL)
        ilApdu.LengthExpected = *pwOut;
    cmnMemCopy(ilApdu.DataIn, pIn, wIn);
    ilRet = smAPDU(phCrypto, &ilApdu);
    if(ilRet)
        return ilRet;

    if(pwOut != NULL)
        *pwOut = ilApdu.LengthOut;
    if(pOut != NULL)    
        cmnMemCopy(pOut, ilApdu.DataOut, ilApdu.LengthOut);

    return ilRet;
}

IL_FUNC IL_RETCODE smMSE(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE bP1, IL_BYTE bP2, IL_BYTE* pIn, IL_WORD wIn)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

    protWrite(PROT_SMLIB, "smMSE");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[0] = 0x00;
    ilApdu.Cmd[1] = 0x22;
    ilApdu.Cmd[2] = bP1;
    ilApdu.Cmd[3] = bP2;
    ilApdu.LengthIn = wIn;
    if(pIn!=NULL)
        cmnMemCopy(ilApdu.DataIn, pIn, wIn);
    ilRet = smAPDU(phCrypto, &ilApdu);
    if(ilRet)
        return ilRet;

    return ilRet;
}

IL_WORD Pad(IL_BYTE* buf, IL_WORD len)
{
    WORD blocks = len/8 + 1;
    cmnMemSet(&buf[len], 0, blocks*8 - len);
    buf[len] = 0x80;

    return blocks*8;
}

IL_FUNC IL_RETCODE smPrepareSM(IL_HANDLE_CRYPTO* phCrypto, IL_APDU* pilApdu)
{
    IL_RETCODE ilRet;
    IL_BYTE ICV[8];
    IL_BYTE buf[512];
    IL_WORD wLen=0;
    IL_WORD wTmp=0;
    IL_WORD offset;
    IL_BYTE MAC4[4];
    HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)phCrypto;

    protWrite(PROT_SMLIB, "smPrepareSM");

    toSmLog("Initial SessionSmCounter", myhCrypto->SM.SessionSmCounter, 8);

    //Add 2 to SessionSmCounter
    //    AddToSessionSmCounter(myhCrypto->SM.SessionSmCounter, 2);
    AddToSessionSmCounter(myhCrypto->SM.SessionSmCounter, 1);

    toSmLog("SessionSmCounter", myhCrypto->SM.SessionSmCounter, 8);

    ilRet = smMSE(phCrypto, 0xF3, 0x01, NULL, 0);
    if(ilRet)
        return ilRet;

    ilRet = smPSO(phCrypto, 0x00, 0x82, 0x81, myhCrypto->SM.SessionSmCounter, 8, ICV, &wTmp);
    if(ilRet)
        return ilRet;

    toSmLog("ICV", ICV, 8);

    if(pilApdu->LengthIn)
    {
        buf[0] = 0x87;
        buf[1] = 0x08;
        cmnMemCopy(&buf[2], ICV, 8);
        ilRet = smMSE(phCrypto, 0x21, 0xB4, buf, 10);
        if(ilRet)
            return ilRet;

        buf[0] = 0x01;
        cmnMemCopy(&buf[1], pilApdu->DataIn, pilApdu->LengthIn);
        pilApdu->LengthIn = Pad(&buf[1], pilApdu->LengthIn);

        toSmLog("Data before SM_Encrypt", &buf[1], pilApdu->LengthIn);

        ilRet = smPSO(phCrypto, 0x00, 0x82, 0x82, &buf[1], pilApdu->LengthIn, &buf[1], &wLen);
        if(ilRet)
            return ilRet;

        toSmLog("Data after SM_Encrypt", &buf[1], wLen);

        wLen = (IL_WORD)AddTag(0x87, buf, wLen+1, pilApdu->DataIn);

        toSmLog("Data tag 87", pilApdu->DataIn, wLen);
    }

    if(pilApdu->LengthExpected)
    {
        pilApdu->DataIn[wLen++] = 0x97;
        pilApdu->DataIn[wLen++] = 0x01;
        pilApdu->DataIn[wLen++] = pilApdu->LengthExpected;
    }

    //TODO уточнить источник идеи
    buf[0] = 0x87;
    buf[1] = 0x08;
    cmnMemSet(&buf[2], 0, 8);
    ilRet = smMSE(phCrypto, 0x21, 0xB4, buf, 10);
    if(ilRet)
        return ilRet;

    //prepare to MAC calc
    cmnMemSet(buf, 0, sizeof(buf));
    offset = 0;
    cmnMemCopy(&buf[offset], myhCrypto->SM.SessionSmCounter, 8);
    offset += 8;
    pilApdu->Cmd[0] |= 0x0C;
    cmnMemCopy(&buf[offset], pilApdu->Cmd, 4);
    offset += 4;
    buf[offset] = 0x80;
    offset += 4;
    cmnMemCopy(&buf[offset], pilApdu->DataIn, wLen);
    offset += wLen;
    toSmLog("Data for MACing", buf, offset);

    //calc MAC
    ilRet = smPSO(phCrypto, 0x00, 0x8E, 0x80, buf, offset, MAC4, &wTmp);
    if(ilRet)
        return ilRet;

    toSmLog("MAC", MAC4, 4);

    wLen += (IL_WORD)AddTag(IL_TAG_8E, MAC4, 4, &pilApdu->DataIn[wLen]);
    toSmLog("Data with MAC", pilApdu->DataIn, wLen);

    pilApdu->LengthIn = wLen;

    return 0;
}

IL_FUNC IL_RETCODE smProcessSM(IL_HANDLE_CRYPTO* phCrypto, IL_APDU* pilApdu)
{
    //    IL_BYTE AuthCounter[8];
    IL_BYTE ICV[8];
    IL_BYTE buf[512];
    IL_WORD wLen=0;
    IL_WORD offset;
    IL_BYTE MAC4[4];
    IL_BYTE* pSW;
    IL_DWORD dwSW;
    IL_BYTE* pMAC;
    IL_DWORD dwMAC;
    IL_BYTE* pEncData;
    IL_DWORD dwEncData;
    IL_WORD wDecData = 0;
    IL_RETCODE ilRet = 0;
    int i;
    HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)phCrypto;

    protWrite(PROT_SMLIB, "smProcessSM");

    //    cmnMemCopy(AuthCounter, myhCrypto->SM.SessionSmCounter, 8);
    toSmLog("Initial SessionSmCounter", myhCrypto->SM.SessionSmCounter, 8);

    //Add 1 to SessionSmCounter
    //    AddToSessionSmCounter(AuthCounter, 1);
    AddToSessionSmCounter(myhCrypto->SM.SessionSmCounter, 1);

    //    toSmLog("AuthCounter", AuthCounter, 8);
    toSmLog("SessionSmCounter", myhCrypto->SM.SessionSmCounter, 8);

    ilRet = TagFind(pilApdu->DataOut, pilApdu->LengthOut, IL_TAG_99, &dwSW, &pSW, 0);
    if(ilRet)
        return ilRet;
    if(dwSW != 2)
        return ILRET_CRD_APDU_TAG_LEN_ERROR;

    ilRet = TagFind(pilApdu->DataOut, pilApdu->LengthOut, IL_TAG_99, &dwSW, &pSW, 1);
    if(ilRet)
        return ilRet;

    ilRet = TagFind(pilApdu->DataOut, pilApdu->LengthOut, IL_TAG_8E, &dwMAC, &pMAC, 0);
    if(ilRet)
        return ilRet;
    if(dwMAC != 4)
        return ILRET_CRD_APDU_TAG_LEN_ERROR;

    //игнорируем ошибки специально
    TagFind(pilApdu->DataOut, pilApdu->LengthOut, IL_TAG_87, &dwEncData, &pEncData, 1);

    //prepare to MAC calc
    cmnMemSet(buf, 0, sizeof(buf));
    offset = 0;
    //    cmnMemCopy(&buf[offset], AuthCounter, 8);
    cmnMemCopy(&buf[offset], myhCrypto->SM.SessionSmCounter, 8);
    offset += 8;
    if(dwEncData)
    {
        cmnMemCopy(&buf[offset], pEncData, dwEncData);
        offset += (IL_WORD)dwEncData;
    }
    cmnMemCopy(&buf[offset], pSW, dwSW);
    offset += (IL_WORD)dwSW;
    toSmLog("Data for MACing", buf, offset);

    //calc MAC
    ilRet = smPSO(phCrypto, 0x00, 0x8E, 0x80, buf, offset, MAC4, &wLen);
    if(ilRet)
        return ilRet;

    toSmLog("MAC", MAC4, 4);

    if(cmnMemCmp(MAC4, pMAC, 4))
        return ILRET_CRYPTO_WRONG_SM_MAC;

    //игнорируем ошибки специально
    TagFind(pilApdu->DataOut, pilApdu->LengthOut, IL_TAG_87, &dwEncData, &pEncData, 0);
    if(pEncData)
    {
        if(pEncData[0]!=0x01)
            ILRET_CRD_APDU_DATA_FORMAT_ERROR;

        ilRet = smPSO(phCrypto, 0x00, 0x82, 0x81, myhCrypto->SM.SessionSmCounter, 8, ICV, &wLen);
        if(ilRet)
            return ilRet;

        toSmLog("ICV", ICV, 8);

        buf[0] = 0x87;
        buf[1] = 0x08;
        cmnMemCopy(&buf[2], ICV, 8);
        ilRet = smMSE(phCrypto, 0x21, 0xB4, buf, 10);
        if(ilRet)
            return ilRet;

        toSmLog("Data before SM_Decrypt", &pEncData[1], dwEncData-1);

        ilRet = smPSO(phCrypto, 0x00, 0x80, 0x82, &pEncData[1], dwEncData-1, &pEncData[1], &wDecData);
        if(ilRet)
            return ilRet;

        if(wDecData < 8)
            return ILRET_CRYPTO_WRONG_DATA_FORMAT;

        for(i = 0; i < 8; i++)
        {
            IL_BYTE b = pEncData[wDecData - i];
            if(b == 0x80)
            {
                wDecData -= (i+1);
                break;
            }
            else if(b == 0x00)
            {
                continue;
            }
            else
                return ILRET_CRYPTO_WRONG_DATA_FORMAT;
        }

        if(i == 8)
            return ILRET_CRYPTO_WRONG_DATA_FORMAT;

        toSmLog("Data after SM_Decrypt", &pEncData[1], wDecData);
    }

    ilRet = TagFind(pilApdu->DataOut, pilApdu->LengthOut, IL_TAG_99, &dwSW, &pSW, 0);
    if(ilRet)
        return ilRet;

    pilApdu->SW1 = pSW[0];
    pilApdu->SW2 = pSW[1];
    pilApdu->LengthOut = wDecData;
    if(pilApdu->LengthOut)
    {
        cmnMemMove(pilApdu->DataOut, &pEncData[1], pilApdu->LengthOut);
        //clear end of buffer
        cmnMemSet(&pilApdu->DataOut[pilApdu->LengthOut], 0, sizeof(pilApdu->DataOut) - pilApdu->LengthOut);
    }

    return 0;
}

IL_FUNC IL_RETCODE smReadCertificates(IL_HANDLE_CRYPTO* phCrypto)
{
    IL_RETCODE ilRet = 0;
    IL_WORD wFileId, i;
    HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)phCrypto;

    protWrite(PROT_SMLIB, "smReadCertificates");

    for(wFileId = 5, i = 0; wFileId <= 8; wFileId++, i++)
    {
        ilRet = smReadBlock(phCrypto, wFileId, 0, myhCrypto->hSm.Certificates[i], &myhCrypto->hSm.wCertificatesLen[i]);
        if(ilRet)
            return ilRet;
    }

    return ilRet;
}

IL_FUNC IL_RETCODE smGetCertificate(IL_HANDLE_CRYPTO* phCrypto, IL_WORD ilParamType, IL_BYTE KeyVer, IL_BYTE ifGost, IL_BYTE* certGost, IL_DWORD* pdwCertGostLen)
{		
    IL_RETCODE ilRet = 0;
    IL_DWORD dwLen;
    IL_BYTE* p5F29;
    IL_BYTE* p65_80;
    IL_BYTE* p7F4C_80;
    IL_TAG TAG_PATH_7F21_7F4E_5F29[] = {IL_TAG_7F21, IL_TAG_7F4E, IL_TAG_5F29};//cert profile 02 - cert format ver
    IL_TAG TAG_PATH_7F21_7F4E_65_80[] = {IL_TAG_7F21, IL_TAG_7F4E, IL_TAG_65, IL_TAG_80};//15 or 16 - key Id 
    IL_TAG TAG_PATH_7F21_7F4E_7F4C_80[] = {IL_TAG_7F21, IL_TAG_7F4E, IL_TAG_7F4C, IL_TAG_80};//04 or 07 - cert type
    IL_WORD i;
    HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)phCrypto;

    protWrite(PROT_SMLIB, "smGetCertificate");

    for(i = 0; i < 4; i++)
    {
        ilRet = TagFindByPath(myhCrypto->hSm.Certificates[i], myhCrypto->hSm.wCertificatesLen[i], TAG_PATH_7F21_7F4E_5F29, 
            sizeof(TAG_PATH_7F21_7F4E_5F29)/sizeof(TAG_PATH_7F21_7F4E_5F29[0]),&dwLen, &p5F29, 0);
        if(ilRet)
            return ilRet;

        //проверяем формат сертификата, должен быть 2
        if((dwLen != 1) || (*p5F29 != 2))
            continue;

        ilRet = TagFindByPath(myhCrypto->hSm.Certificates[i], myhCrypto->hSm.wCertificatesLen[i], TAG_PATH_7F21_7F4E_65_80, 
            sizeof(TAG_PATH_7F21_7F4E_65_80)/sizeof(TAG_PATH_7F21_7F4E_65_80[0]),&dwLen, &p65_80, 0);
        if(ilRet)
            return ilRet;

        if(dwLen != 1)
            continue;
        //проверяем тип ключа сертификата, должен быть 15 для ГОСТ и 16 для RSA
        if(ifGost)
        {
            if(*p65_80 != 0x15)
                continue;
        }
        else
        {
            if(*p65_80 != 0x16)
                continue;
        }

        ilRet = TagFindByPath(myhCrypto->hSm.Certificates[i], myhCrypto->hSm.wCertificatesLen[i], TAG_PATH_7F21_7F4E_7F4C_80, 
            sizeof(TAG_PATH_7F21_7F4E_7F4C_80)/sizeof(TAG_PATH_7F21_7F4E_7F4C_80[0]),&dwLen, &p7F4C_80, 0);
        if(ilRet)
            return ilRet;

        if(dwLen != 1)
            continue;
        //проверяем тип сертификата, должен быть 7 для терминала и 4 для ОКО
        if(((ilParamType == IL_PARAM_CIFDID)&&(*p7F4C_80 == 7)) ||
            ((ilParamType == IL_PARAM_CAID)&&(*p7F4C_80 == 4)))
        {
            if(certGost != NULL)
                memcpy(certGost, myhCrypto->hSm.Certificates[i], myhCrypto->hSm.wCertificatesLen[i]);
            if(pdwCertGostLen != NULL)
                *pdwCertGostLen = myhCrypto->hSm.wCertificatesLen[i];
            return 0;
        }
    }

    return ILRET_PARAM_NOT_FOUND;
}

IL_FUNC IL_RETCODE smUpdateBinary(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wOffset, IL_BYTE* pDataIn, IL_WORD wDataInLen)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

    protWrite(PROT_SMLIB, "smUpdateBinary");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_UPDATE_BIN;
    ilApdu.Cmd[2] = (IL_BYTE)(wOffset>>8);
    ilApdu.Cmd[3] = (IL_BYTE)wOffset;
    ilApdu.LengthIn = wDataInLen;
    cmnMemCopy(ilApdu.DataIn, pDataIn, wDataInLen);

    ilRet = smAPDU(phCrypto, &ilApdu);

    return ilRet;
}

IL_FUNC IL_RETCODE smActivationStart(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* ifStateActivated, IL_BYTE* pTag86Data, IL_DWORD* pdwTag86Len)
{		
    IL_RETCODE ilRet = 0;
    IL_BYTE buf[256]; 
    IL_WORD wLen = 0;
    IL_BYTE FileId0003[] = {0x00, 0x03};
	IL_TAG TAG_PATH_62_86[] = {IL_TAG_62, IL_TAG_86};
	IL_TAG TAG_PATH_A0_AF[] = {IL_TAG_A0, IL_TAG_AF};
	IL_DWORD dw86 = 0;
	IL_BYTE* p86 = NULL;
	IL_DWORD dwTmp = 0;
	IL_BYTE* pTmp = NULL;

    protWrite(PROT_SMLIB, "smActivationStart");
    
    ilRet = smGetData(phCrypto, IL_TAG_DF2C, buf, &wLen);
    if(ilRet)
        return ilRet;

    //TODO проверить    
    *ifStateActivated = (buf[0] == 0x77) ? 1 : 0;

    ilRet = smSelect(phCrypto, 0, 0x04, FileId0003, 2, buf, &wLen);
    if(ilRet)
        return ilRet;

	ilRet = TagFindByPath(buf, wLen, TAG_PATH_62_86, sizeof(TAG_PATH_62_86)/sizeof(TAG_PATH_62_86[0]), &dw86, &p86, 0);
	if(ilRet)
		return ilRet;

	while(dw86 > 0)
	{
		ilRet = TagFind(p86, dw86, IL_TAG_80, &dwTmp, &pTmp, 0);
		if(ilRet)
			return ilRet;

		if(pTmp - p86 + dwTmp > dw86)
			return ILRET_SM_WRONG_DATA;

		dw86 -= (pTmp - p86 + dwTmp);
		p86 = pTmp + dwTmp;

		if(pTmp[0] == 2)
		{
			if(pdwTag86Len!= NULL)
				*pdwTag86Len = 0;

			while(dw86 > 0)
			{
				ilRet = TagFindByPath(p86, dw86, TAG_PATH_A0_AF, sizeof(TAG_PATH_A0_AF)/sizeof(TAG_PATH_62_86[0]), &dwTmp, &pTmp, 0);
				if(ilRet)
					return ilRet;

				cmnMemCopy(&pTag86Data[*pdwTag86Len], pTmp, dwTmp);
				*pdwTag86Len += dwTmp;

				if(pTmp - p86 + dwTmp > dw86)
					return ILRET_SM_WRONG_DATA;

				dw86 -= (pTmp - p86 + dwTmp);
				p86 = pTmp + dwTmp;
			}
			break;
		}
	}

	if(pTag86Data != NULL)
		cmnMemCopy(pTag86Data, p86, dw86);

	if(pdwTag86Len != NULL)
		*pdwTag86Len = dw86;

	return ilRet;
}

IL_FUNC IL_RETCODE smOfflineActivationFinish(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE ifGost, BYTE ifDeactivate, IL_BYTE* pSmOwnerName, IL_WORD wSmOwnerNameLen)
{		
    IL_RETCODE ilRet = 0;
    IL_BYTE buf[256]; 
    IL_WORD wLen = 0;
    IL_BYTE FileId0003[] = {0x00, 0x03};

    protWrite(PROT_SMLIB, "smOfflineActivationFinish");

	ilRet = smActivation(phCrypto, 0x80, ifDeactivate);
    if(ilRet)
        return ilRet;

    ilRet = smSelect(phCrypto, 0, 4, FileId0003, 2, buf, &wLen);
    if(ilRet)
        return ilRet;

    if(pSmOwnerName == NULL)
        return ilRet;


    ilRet = smUpdateBinary(phCrypto, 0, pSmOwnerName, wSmOwnerNameLen);
    if(ilRet)
        return ilRet;

    return ilRet;
}

IL_FUNC IL_RETCODE smSetTerminalToProviderSessionRsa11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Cspaid_buf, IL_WORD wCspaid_size, IL_BYTE* msg, IL_DWORD msg_len, PROVIDER_SESSION_DATA* pSessDataOut)
{
    IL_RETCODE ilRet = 0;
    KEY_RSA KeyPspidrsa;
    IL_BYTE* pACC = NULL;
    IL_DWORD dwACC = 0;
    IL_BYTE* pPAN = NULL;
    IL_DWORD dwPAN = 0;
    IL_BYTE SK[16];
    IL_BYTE SP_Challenge[256] = {0};
    IL_WORD wSP_ChallengeLen = 0;
    HANDLE_CRYPTO* pProvSM = (HANDLE_CRYPTO*)pSM;

    protWrite(PROT_SMLIB, "smSetTerminalToProviderSessionRsa11");

    //поиск PAN    
    ilRet = TagFind(msg, msg_len, IL_TAG_5A, &dwPAN, &pPAN, 0);
    if(ilRet)
        return ilRet;

    //поиск ACC
    ilRet = TagFind(msg, msg_len, IL_TAG_9F36, &dwACC, &pACC, 0);
    if(ilRet)
        return ilRet;

    if((pACC != NULL) & (dwACC <= sizeof(SP_Challenge)))
    {
        cmnMemCopy(&SP_Challenge[wSP_ChallengeLen], pPAN, dwPAN);
        wSP_ChallengeLen += dwPAN;

        if((pPAN != NULL) & (dwPAN <= (sizeof(SP_Challenge) - dwPAN)))
        {
            cmnMemCopy(&SP_Challenge[wSP_ChallengeLen], pACC, dwACC);
            wSP_ChallengeLen += dwACC;
        }
    }    

    //извлекаем сертификат открытого ключа провайдера услуг
    //сертификат должен быть проверен к этому моменту!
    ilRet = RsaKeyFromCertificateEx(IL_TAG_7F4E, Cspaid_buf, wCspaid_size, &KeyPspidrsa);
    if(ilRet)
        return ilRet;

    //создаём случайный общий секрет SK
    ilRet = smGetChallenge(pSM, sizeof(SK), SK);
    if(ilRet)
        return ilRet;

    //Y
    EncryptRsa(SK, sizeof(SK), &KeyPspidrsa, pSessDataOut->Y, &pSessDataOut->Y_len);

    //создаём сессионный ключ
    KDF(SK, SP_Challenge, wSP_ChallengeLen, pProvSM->SK_sm_id_smc_des);

    return ilRet;
}

IL_FUNC IL_RETCODE smSetTerminalToProviderSessionGost11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Cspaid_buf, IL_WORD wCspaid_size, IL_BYTE* msg, IL_DWORD msg_len, PROVIDER_SESSION_DATA* pSessDataOut)
{
    IL_RETCODE ilRet = 0;
    IL_BYTE* pACC = NULL;
    IL_DWORD dwACC = 0;
    IL_BYTE* pPAN = NULL;
    IL_DWORD dwPAN = 0;
    IL_BYTE SP_Challenge[256] = {0};
    IL_WORD wSP_ChallengeLen = 0;
    IL_BYTE buf[256] = {0};

    protWrite(PROT_SMLIB, "smSetTerminalToProviderSessionGost11");

    //поиск PAN    
    ilRet = TagFind(msg, msg_len, IL_TAG_5A, &dwPAN, &pPAN, 0);
    if(ilRet)
        return ilRet;

    //поиск ACC
    ilRet = TagFind(msg, msg_len, IL_TAG_9F36, &dwACC, &pACC, 0);
    if(ilRet)
        return ilRet;

    if((pACC != NULL) & (dwACC <= sizeof(SP_Challenge)))
    {
        cmnMemCopy(&SP_Challenge[wSP_ChallengeLen], pPAN, dwPAN);
        wSP_ChallengeLen += dwPAN;

        if((pPAN != NULL) & (dwPAN <= (sizeof(SP_Challenge) - dwPAN)))
        {
            cmnMemCopy(&SP_Challenge[wSP_ChallengeLen], pACC, dwACC);
            wSP_ChallengeLen += dwACC;
        }
    }    

    ilRet = smPSO(pSM, 0, 0x00, 0xBE, Cspaid_buf, wCspaid_size, NULL, NULL);
    if(ilRet)
        return ilRet;

    //создаём случайный общий секрет SK
    ilRet = smGetChallenge(pSM, 16, pSessDataOut->Random);
    if(ilRet)
        return ilRet;

    cmnMemCopy(buf, SP_Challenge, wSP_ChallengeLen);
    cmnMemCopy(&buf[wSP_ChallengeLen], pACC, dwACC);

    ilRet = smSpSessionInit(pSM, buf);
    if(ilRet)
        return ilRet;

    return ilRet;
}

IL_FUNC IL_RETCODE smSetTerminalToProviderSession11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Cspaid_buf, IL_WORD wCspaid_size, IL_BYTE* msg, IL_DWORD msg_len, PROVIDER_SESSION_DATA* pSessDataOut, BYTE ifGost)
{
    IL_RETCODE ilRet = 0;

    protWrite(PROT_SMLIB, "smSetTerminalToProviderSession11");
    
    if(!ifGost)
        ilRet = smSetTerminalToProviderSessionRsa11(pSM, Cspaid_buf, wCspaid_size, msg, msg_len, pSessDataOut);
    else
        ilRet = smSetTerminalToProviderSessionGost11(pSM, Cspaid_buf, wCspaid_size, msg, msg_len, pSessDataOut);

    return ilRet;
}

IL_FUNC IL_RETCODE smEncrypt(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* OutMsg, IL_DWORD* pwOutMsgLen)
{
    IL_RETCODE ilRet = 0;
	IL_WORD wInPortionLen = 0;
    IL_WORD wOutPortionLen;
	IL_DWORD dwInOffset = 0;
	IL_DWORD dwOutOffset = 0;
	IL_DWORD dwBytesToEncrypt = (MsgLen/8 + 1)*8;
	IL_BYTE bPadLen = dwBytesToEncrypt - MsgLen; 
	IL_DWORD MAX_PORTION_LEN = 0xE0;
	IL_BYTE buf[256] = {0};

    protWrite(PROT_SMLIB, "smEncrypt");
    
	//настраиваем контекcт SM на работу с провайдером 
    ilRet = smMSE(pSM, 0xF3, 0x02, NULL, 0);
    if(ilRet)
        return ilRet;

	//TODO уточнить источник идеи
    buf[0] = 0x87;
    buf[1] = 0x08;
    cmnMemSet(&buf[2], 0, 8);
    ilRet = smMSE(pSM, 0x21, 0xB4, buf, 10);
    if(ilRet)
        return ilRet;

    for( ;dwBytesToEncrypt > 0; )
	{
		IL_BYTE ifLast = (dwBytesToEncrypt <= MAX_PORTION_LEN);
		wInPortionLen = ifLast?dwBytesToEncrypt:MAX_PORTION_LEN;
		if(ifLast)
		{
			cmnMemCopy(buf, &Msg[dwInOffset], wInPortionLen-bPadLen);
			cmnMemSet(&buf[wInPortionLen - bPadLen], 0, bPadLen);
			buf[wInPortionLen - bPadLen] = 0x80;//padding
		}
		else
			cmnMemCopy(buf, &Msg[dwInOffset], wInPortionLen);

		ilRet = smPSO(pSM, ifLast?0x00:0x10, 0x82, 0x82, buf, wInPortionLen, &OutMsg[dwOutOffset], &wOutPortionLen);
		if(ilRet)
			return ilRet;

		dwInOffset += wInPortionLen;
		dwBytesToEncrypt -= wInPortionLen;
		dwOutOffset += wOutPortionLen;
	}

    *pwOutMsgLen = dwOutOffset;
    
    return ilRet;
}

IL_FUNC IL_RETCODE smDecrypt(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* OutMsg, IL_DWORD* pwOutMsgLen)
{
    IL_RETCODE ilRet = 0;
	IL_WORD wInPortionLen = 0;
    IL_WORD wOutPortionLen;
	IL_DWORD dwInOffset = 0;
	IL_DWORD dwOutOffset = 0;
	IL_DWORD dwBytesToDecrypt = MsgLen;
	IL_DWORD MAX_PORTION_LEN = 0xE0;
	IL_BYTE buf[256] = {0};

    protWrite(PROT_SMLIB, "smDecrypt");

	if(MsgLen%8 != 0)
		return ILRET_CRYPTO_WRONG_DATA_LENGTH;

	ilRet = smMSE(pSM, 0xF3, 0x02, NULL, 0);
	if(ilRet)
		return ilRet;

	//TODO уточнить источник идеи
    buf[0] = 0x87;
    buf[1] = 0x08;
    cmnMemSet(&buf[2], 0, 8);
    ilRet = smMSE(pSM, 0x21, 0xB4, buf, 10);
    if(ilRet)
        return ilRet;

	for( ;dwBytesToDecrypt > 0; )
	{
		IL_BYTE ifLast = (dwBytesToDecrypt <= MAX_PORTION_LEN);
		wInPortionLen = ifLast?dwBytesToDecrypt:MAX_PORTION_LEN;

		ilRet = smPSO(pSM, ifLast?0x00:0x10, 0x80, 0x82, &Msg[dwInOffset], wInPortionLen, buf, &wOutPortionLen);
		if(ilRet)
			return ilRet;

		if(ifLast)
		{
			IL_INT i;
			for(i = 7; i >= 0; i--)
			{
				if(buf[wOutPortionLen-8+i]==0x00)
					continue;
				else if(buf[wOutPortionLen-8+i]==0x80)
					break;
				else
					return ILRET_CRYPTO_WRONG_DATA_FORMAT;
			}

			if(i < 0)
				return ILRET_CRYPTO_WRONG_DATA_FORMAT;

			wOutPortionLen -= (8-i);
		}

		cmnMemCopy(&OutMsg[dwOutOffset], buf, wOutPortionLen);

		dwInOffset += wInPortionLen;
		dwBytesToDecrypt -= wInPortionLen;
		dwOutOffset += wOutPortionLen;
	}

    *pwOutMsgLen = dwOutOffset;
    
    return ilRet;
}

IL_FUNC IL_RETCODE smDecryptTerminalToProvider11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* DecryptedMsg, IL_DWORD* pwDecryptedMsgLen, IL_BYTE ifGost)
{
    IL_RETCODE ilRet = 0;
    HANDLE_CRYPTO* pProvSM = (HANDLE_CRYPTO*)pSM;

    protWrite(PROT_SMLIB, "smDecryptTerminalToProvider11");

    if(!ifGost)
    {
        ilRet = DES3_CBC_PAD_Decrypt(pProvSM->SK_sm_id_smc_des, NULL, Msg, MsgLen, DecryptedMsg, pwDecryptedMsgLen);
    }
    else
    {
        ilRet = smDecrypt(pSM, Msg, MsgLen, DecryptedMsg, pwDecryptedMsgLen);
    }

    return ilRet;
}

IL_FUNC IL_RETCODE smEncryptTerminalToProvider11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* EncMsg, IL_DWORD* pwMsgLenEncrypted, IL_BYTE ifGost)
{
    IL_RETCODE ilRet = 0;
    HANDLE_CRYPTO* pProvSM = (HANDLE_CRYPTO*)pSM;

    protWrite(PROT_SMLIB, "smDecryptTerminalToProvider11");

    if(!ifGost)
    {
        DES3_CBC_PAD_Encrypt(pProvSM->SK_sm_id_smc_des, NULL, Msg, MsgLen, EncMsg, pwMsgLenEncrypted);
    }
    else
    {
        ilRet = smEncrypt(pSM, Msg, MsgLen, EncMsg, pwMsgLenEncrypted);
    }

    return ilRet;
}

IL_FUNC IL_RETCODE smAuthServiceProviderGost(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_WORD wMsgLen, IL_BYTE* S, IL_WORD wS_len, IL_BYTE* pCspaid, IL_WORD wCspaidLen)
{
    IL_RETCODE ilRet = 0;
	IL_DWORD dwInOffset = 0;
	IL_DWORD MAX_PORTION_LEN = 0xE0;
	IL_DWORD dwBytesToSign = wMsgLen;
	IL_WORD wInPortionLen = 0;
	IL_BYTE buf[256] = {0};
	IL_WORD wTmpLen = 0;
	
    protWrite(PROT_SMLIB, "smAuthServiceProviderGost");

    ilRet = smPSO(pSM, 0, 0x00, 0xBE, pCspaid, wCspaidLen, NULL, NULL);
    if(ilRet)
        return ilRet;
    
    for( ;dwBytesToSign > 0; )
	{
		IL_BYTE ifLast = (dwBytesToSign <= MAX_PORTION_LEN);
		wInPortionLen = ifLast?dwBytesToSign:MAX_PORTION_LEN;

		ilRet = smPSO(pSM, ifLast?0x00:0x10, 0x90, 0x80, &Msg[dwInOffset], wInPortionLen, buf, &wTmpLen);
		if(ilRet)
			return ilRet;

		dwInOffset += wInPortionLen;
		dwBytesToSign -= wInPortionLen;
	}
	
    cmnMemCopy(&buf[wTmpLen], S, wS_len);
    wTmpLen += wS_len;
    ilRet = smPSO(pSM, 0, 0, 0xA8, buf, wTmpLen, NULL, NULL);
    if(ilRet)
        return ilRet;
        
    return ilRet;
}

IL_FUNC IL_RETCODE smAuthServiceProviderRsa(IL_BYTE* Msg, IL_WORD wMsgLen, IL_BYTE* S, IL_WORD wS_len, IL_BYTE* pCspaid, IL_WORD wCspaidLen)
{
	IL_RETCODE ilRet = 0;

	KEY_RSA KeyRSA;

    protWrite(PROT_SMLIB, "smAuthServiceProviderRsa");
    
	ilRet = RsaKeyFromCertificateEx(IL_TAG_7F4E, pCspaid, wCspaidLen, &KeyRSA);
	if(ilRet)
		return ilRet;

	ilRet = cryptoVerifyRsaSignature(Msg, wMsgLen, S, wS_len, &KeyRSA);

	return ilRet;
}

IL_FUNC IL_RETCODE smAuthServiceProvider(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* Msg, IL_WORD wMsgLen, IL_BYTE* S, IL_WORD wS_len, IL_BYTE* pCspaid, IL_WORD wCspaidLen, IL_BYTE ifGost, IL_BYTE AppVer)
{
    IL_RETCODE ilRet = 0;

    protWrite(PROT_SMLIB, "smAuthServiceProvider");
    
    if(!ifGost)
    {
        ilRet = smAuthServiceProviderRsa(Msg, wMsgLen, S, wS_len,  pCspaid, wCspaidLen);
    }
    else
    {
        ilRet = smAuthServiceProviderGost(phCrypto, Msg, wMsgLen, S, wS_len, pCspaid, wCspaidLen);
    }

    return ilRet;
}

IL_FUNC IL_RETCODE smChkApduAllowedResponse(IL_APDU_PACK_ELEM* ilApduPackElem)
{
	IL_BYTE i;

	for(i = 0; i < ilApduPackElem->allowed_res_len; i++)
	{
		if(ilApduPackElem->Apdu.SW1 == ilApduPackElem->allowed_res[i*2]
				&& ilApduPackElem->Apdu.SW2 == ilApduPackElem->allowed_res[i*2+1])
					return 0;

	}

	return ILRET_APDU_RES_NOT_ALLOWED;
}

// smRunApdu - выполняет APDU-команду пакета
IL_FUNC IL_RETCODE smRunApdu(IL_HANDLE_CRYPTO* phCrypto, IL_APDU_PACK_ELEM* ilApduElem)
{
    IL_RETCODE ilRet;
    
	protWrite(PROT_SMLIB, "smRunApdu");

	if(!(ilRet = smAPDU(phCrypto, &ilApduElem->Apdu)))
		ilRet = smChkApduAllowedResponse(ilApduElem);

	return ilRet;
}

/*---
IL_FUNC IL_RETCODE smGetOwnerName(IL_HANDLE_CRYPTO* phCrypto, IL_CHAR *pOwnerNameOut)
{
    IL_RETCODE ilRet = 0;
    IL_BYTE FileId0003[] = {0x00, 0x03};
	IL_BYTE buf[256] = {0};
    IL_WORD wLen = 0;

	*pOwnerNameOut = '\0';

	// селектируем файл с именем владельца МБ 
    if((ilRet = smSelect(phCrypto, 0, 4, FileId0003, 2, buf, &wLen)))
        return ilRet;

	// считаем его содержимое
    if((ilRet = smReadBinary(phCrypto, 0, sizeof(buf), buf, &wLen)))
        return ilRet;

	// конвертируем имя владельца
	Iso8859_2_Ansi((IL_CHAR*)buf, pOwnerNameOut);

	return 0;
}
---*/