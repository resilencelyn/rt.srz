#include "CardLib.h"
#include "il_error.h"
#include "HAL_Common.h"
#include "HAL_Protocol.h"
#include "tlv.h"

#ifndef PROT_IGNORE
IL_CHAR protBuf[1024*5];
#endif//PROT_IGNORE

IL_FUNC IL_RETCODE clCardError(IL_APDU* pApdu)
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
            case 0x6283:
                ilRet = ILRET_CRD_SELECT_OBJECT_BLOCKED;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_SELECT_WRONG_LENGTH;
                break;
            case 0x6984:
                ilRet = ILRET_CRD_SELECT_WRONG_CMD_DATA;
                break;
            case 0x6A82:
                ilRet = ILRET_CRD_SELECT_FILE_NOT_FOUND;
                break;
            case 0x6A86:
                ilRet = ILRET_CRD_SELECT_WRONG_PARAMETERS;
                break;
            default:
                ilRet = ILRET_CRD_SELECT_ERROR;
            }
        break;
        case INS_INT_AUTH:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_INTAUTH_WRONG_LENGTH;
                break;
            case 0x6984:
                ilRet = ILRET_CRD_INTAUTH_WRONG_CMD_DATA;
                break;
            case 0x6A86:
                ilRet = ILRET_CRD_INTAUTH_WRONG_PARAMETERS;
                break;
            case 0x6A88:
                ilRet = ILRET_CRD_INTAUTH_KEY_NOT_FOUND;
                break;
            default:
                ilRet = ILRET_CRD_INTAUTH_ERROR;
            }
        break;
        case INS_MUT_AUTH:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_MUTAUTH_WRONG_LENGTH;
                break;
            case 0x6982:
                ilRet = ILRET_CRD_MUTAUTH_WRONG_CRYPTO;
                break;
            case 0x6985:
                ilRet = ILRET_CRD_MUTAUTH_CONDITIONS;
                break;
            case 0x6A86:
                ilRet = ILRET_CRD_MUTAUTH_WRONG_PARAMETERS;
                break;
            case 0x6A88:
                ilRet = ILRET_CRD_MUTAUTH_KEY_NOT_FOUND;
                break;
            default:
                ilRet = ILRET_CRD_MUTAUTH_ERROR;
            }
        break;
        case INS_GET_CHAL:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_GETCHAL_WRONG_LENGTH;
                break;
            case 0x6A86:
                ilRet = ILRET_CRD_GETCHAL_WRONG_PARAMETERS;
                break;
            default:
                ilRet = ILRET_CRD_GETCHAL_ERROR;
            }
        break;
        case INS_CHANGE_DATA:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6A87: //---0x6700:
                ilRet = ILRET_CRD_CHDATA_WRONG_LENGTH;
                break;
            case 0x6982:
                ilRet = ILRET_CRD_CHDATA_WRONG_CRYPTO;
                break;
            //---case 0x6987:
			case 0x9602: //+++
			case 0x6A80: //+++
                ilRet = ILRET_CRD_CHDATA_WRONG_DATA_STRUCT;
                break;
            case 0x6988:
                ilRet = ILRET_CRD_CHDATA_WRONG_SM_TAG;
                break;
            case 0x6A86:
                ilRet = ILRET_CRD_CHDATA_WRONG_PARAMETERS;
                break;
            case 0x6A88:
                ilRet = ILRET_CRD_CHDATA_KEY_NOT_FOUND;
                break;
            default:
                if((pApdu->SW1 == 0x63) && ((pApdu->SW2 & 0xF0) == 0xC0))
					ilRet = ILRET_CRD_CHDATA_DATA_LEN_TOO_SHORT;
				else
					ilRet = ILRET_CRD_CHDATA_ERROR;
            }
        break;
        case INS_RST_CNTR:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_RSTCNTR_WRONG_LENGTH;
                break;
            case 0x6987:
                ilRet = ILRET_CRD_RSTCNTR_WRONG_DATA_STRUCT;
                break;
            case 0x6988:
                ilRet = ILRET_CRD_RSTCNTR_WRONG_SM_TAG;
                break;
            case 0x6A86:
                ilRet = ILRET_CRD_RSTCNTR_WRONG_PARAMETERS;
                break;
            case 0x6A88:
                ilRet = ILRET_CRD_RSTCNTR_KEY_NOT_FOUND;
                break;
            default:
                ilRet = ILRET_CRD_RSTCNTR_ERROR;
            }
        break;
        case INS_VERIFY:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_VERIFY_WRONG_LENGTH;
                break;
            case 0x6983:
                ilRet = ILRET_CRD_VERIFY_PASSWORD_BLOCKED;
                break;
            case 0x6A86:
                ilRet = ILRET_CRD_VERIFY_WRONG_PARAMETERS;
                break;
            case 0x6A88:
                ilRet = ILRET_CRD_VERIFY_PASSWORD_NOT_FOUND;
                break;
            default:
                if(pApdu->SW1 == 0x63)
                    ilRet = ILRET_CRD_VERIFY_WRONG_PASSWORD_PRESENTED;
                else
                    ilRet = ILRET_CRD_VERIFY_ERROR;
            }
        break;
        case INS_PERF_SEC_OP:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_PERFSECOP_WRONG_LENGTH;
                break;
            case 0x6883:
                ilRet = ILRET_CRD_PERFSECOP_BINDING_CMD_MISSED;
                break;
            case 0x6884:
                ilRet = ILRET_CRD_PERFSECOP_BINDING_NOT_SUPPORTED;
                break;
            case 0x6982:
                ilRet = ILRET_CRD_PERFSECOP_WRONG_CERT;
                break;
            case 0x6A80:
                ilRet = ILRET_CRD_PERFSECOP_WRONG_DATA_STRUCT;
                break;
            case 0x6A86:
                ilRet = ILRET_CRD_PERFSECOP_WRONG_PARAMETERS;
                break;
            default:
                ilRet = ILRET_CRD_PERFSECOP_ERROR;
            }
        break;
        case INS_READ_BIN:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_READBIN_WRONG_LENGTH;
                break;
            case 0x6981:
                ilRet = ILRET_CRD_READBIN_WRONG_FILE_TYPE;
                break;
            case 0x6982:
                ilRet = ILRET_CRD_READBIN_WRONG_SEC_CONDITIONS;
                break;
            case 0x6986:
                ilRet = ILRET_CRD_READBIN_EF_NOT_SELECTED;
                break;
            case 0x6A86:
                ilRet = ILRET_CRD_READBIN_WRONG_PARAMETERS;
                break;
            case 0x6B00:
                ilRet = ILRET_CRD_READBIN_WRONG_OFFSET;
                break;
            default:
                ilRet = ILRET_CRD_READBIN_ERROR;
            }
        break;
        case INS_UPDATE_BIN:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_UPDBIN_WRONG_LENGTH;
                break;
            case 0x6982:
                ilRet = ILRET_CRD_UPDBIN_WRONG_SEC_CONDITIONS;
                break;
            case 0x6986:
                ilRet = ILRET_CRD_UPDBIN_WRONG_FILE;
                break;
            case 0x6A86:
                ilRet = ILRET_CRD_UPDBIN_WRONG_PARAMETERS;
                break;
            default:
                ilRet = ILRET_CRD_UPDBIN_ERROR;
            }
        break;
        case INS_GET_DATA:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_GETDATA_WRONG_LENGTH;
                break;
            case 0x6982:
                ilRet = ILRET_CRD_GETDATA_WRONG_SEC_CONDITIONS;
                break;
            case 0x6986:
                ilRet = ILRET_CRD_GETDATA_WRONG_PARAMETERS;
                break;
            case 0x6A88:
                ilRet = ILRET_CRD_GETDATA_TAG_NOT_FOUND;
                break;
            default:
                ilRet = ILRET_CRD_GETDATA_ERROR;
            }
        break;
       case INS_PUT_DATA:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_PUTDATA_WRONG_LENGTH;
                break;
            case 0x6982:
                ilRet = ILRET_CRD_PUTDATA_WRONG_SEC_CONDITIONS;
                break;
            case 0x6986:
                ilRet = ILRET_CRD_PUTDATA_WRONG_PARAMETERS;
                break;
            case 0x6A88:
                ilRet = ILRET_CRD_PUTDATA_TAG_NOT_FOUND;
                break;
            default:
                ilRet = ILRET_CRD_PUTDATA_ERROR;
            }
        break;
        case INS_READ_REC:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6283:
                ilRet = ILRET_CRD_READREC_FILE_BLOCKED;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_READREC_WRONG_LENGTH;
                break;
            case 0x6981:
                ilRet = ILRET_CRD_READREC_WRONG_FILE_TYPE;
                break;
            case 0x6982:
                ilRet = ILRET_CRD_READREC_WRONG_SEC_CONDITIONS;
                break;
            case 0x6986:
                ilRet = ILRET_CRD_READREC_WRONG_PARAMETERS;
                break;
            case 0x6A83:
                ilRet = ILRET_CRD_READREC_RECORD_NOT_FOUND;
                break;
            case 0x6A86: //---0x6B00:
                ilRet = ILRET_CRD_READREC_WRONG_PARAMETERS_P1P2;
                break;
            default:
                ilRet = ILRET_CRD_READREC_ERROR;
            }
        break;
        case INS_UPDATE_REC:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6283:
                ilRet = ILRET_CRD_UPDREC_FILE_BLOCKED;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_UPDREC_WRONG_LENGTH;
                break;
            case 0x6981:
                ilRet = ILRET_CRD_UPDREC_WRONG_FILE_TYPE;
                break;
            case 0x6982:
                ilRet = ILRET_CRD_UPDREC_WRONG_SEC_CONDITIONS;
                break;
            case 0x6986:
                ilRet = ILRET_CRD_UPDREC_WRONG_PARAMETERS;
                break;
            case 0x6A83:
                ilRet = ILRET_CRD_UPDREC_RECORD_NOT_FOUND;
                break;
            case 0x6A86: //---0x6B00:
                ilRet = ILRET_CRD_UPDREC_WRONG_PARAMETERS_P1P2;
                break;
            default:
                ilRet = ILRET_CRD_UPDREC_ERROR;
            }
        break;
        case INS_APPEND_REC:
            switch(SW(pApdu))
            {
            case 0x9000:
                ilRet = 0;
                break;
            case 0x6283:
                ilRet = ILRET_CRD_APPREC_FILE_BLOCKED;
                break;
            case 0x6700:
                ilRet = ILRET_CRD_APPREC_WRONG_LENGTH;
                break;
            case 0x6981:
                ilRet = ILRET_CRD_APPREC_WRONG_FILE_TYPE;
                break;
            case 0x6982:
                ilRet = ILRET_CRD_APPREC_WRONG_SEC_CONDITIONS;
                break;
            case 0x6986:
                ilRet = ILRET_CRD_APPREC_WRONG_PARAMETERS;
                break;
            case 0x6A84:
                ilRet = ILRET_CRD_APPREC_NOT_ENOUGH_MEMORY;
                break;
			case 0x6A86: //---0x6B00:
                ilRet = ILRET_CRD_APPREC_WRONG_PARAMETERS_P1P2;
                break;
            default:
                ilRet = ILRET_CRD_APPREC_ERROR;
            }
       break;
       default:
            ilRet = ILRET_CRD_ERROR;
    }
    return ilRet;
}

IL_FUNC IL_RETCODE clCardOpen(IL_CARD_HANDLE* phCard)
{
	IL_WORD RC;
	
	PROT_WRITE_EX0(PROT_CARDLIB1, "clCardOpen()");

    phCard->ifSM = 0;
    phCard->ifGostCrypto = 0;
    phCard->ifLongAPDU = 0;
    phCard->ifNeedMSE = 0;
	
	PROT_WRITE_EX0(PROT_CARDLIB3, "crOpenSession");
    RC = crOpenSession(phCard->hRdr);
	PROT_WRITE_EX1(PROT_CARDLIB1, "crOpenSession()=%u", RC);
	
	PROT_WRITE_EX1(PROT_CARDLIB1, "clCardOpen()=%u", RC);
	return RC;
}

IL_FUNC IL_RETCODE clAPDU(IL_CARD_HANDLE* phCard, IL_APDU* pilApdu, IL_BYTE bSM_MODE)
{
    IL_RETCODE ilRet;
    IL_BYTE ifSM;

	PROT_WRITE_EX1(PROT_CARDLIB1, "clAPDU(%s)", bin2hex(protBuf, pilApdu->Cmd, 4));

    if(bSM_MODE == SM_MODE_NONE)
        ifSM = 0;
    else if(bSM_MODE == SM_MODE_ALWAYS)
        ifSM = 1;
    else
        ifSM = phCard->ifSM;


	PROT_WRITE_EX4(PROT_CARDLIB2, "IN: Cmd=%s Le=%lu DataIn[%lu]=%s", bin2hex(protBuf, pilApdu->Cmd, 4), pilApdu->LengthExpected, pilApdu->LengthIn, bin2hex(&protBuf[10], pilApdu->DataIn, pilApdu->LengthIn));
    if(ifSM)
    {
		PROT_WRITE_EX0(PROT_CARDLIB3, "cryptoPrepareSM()");
        ilRet = cryptoPrepareSM(phCard->hCrypto, pilApdu, phCard->ifGostCrypto, phCard->AppVer);
		PROT_WRITE_EX1(PROT_CARDLIB3, "cryptoPrepareSM()=%u", ilRet);
        if(ilRet)
            goto End;
    }

	PROT_WRITE_EX0(PROT_CARDLIB3, "crAPDU()");
    ilRet = crAPDU(phCard->hRdr, pilApdu);
	PROT_WRITE_EX1(PROT_CARDLIB3, "crAPDU()=%u", ilRet);
    if(ilRet)
        goto End;

    if(SW(pilApdu) != 0x9000) 
    {
        ilRet =  clCardError(pilApdu);
        goto End;
    }

    if(ifSM)
    {
		PROT_WRITE_EX0(PROT_CARDLIB3, "cryptoProcessSM()");
        ilRet = cryptoProcessSM(phCard->hCrypto, pilApdu, phCard->ifGostCrypto, phCard->AppVer);
		PROT_WRITE_EX1(PROT_CARDLIB3, "cryptoProcessSM()=%u", ilRet);
        if(ilRet)
            goto End;
    }

	PROT_WRITE_EX4(PROT_CARDLIB2, "OUT:SW=%02X%02X DataOut[%lu]=%s", pilApdu->SW1, pilApdu->SW2, pilApdu->LengthOut, bin2hex(protBuf, pilApdu->DataOut, pilApdu->LengthOut));

    if(SW(pilApdu) != 0x9000) 
        ilRet =  clCardError(pilApdu);

End:

	PROT_WRITE_EX2(PROT_CARDLIB1, "clAPDU(%s)=%u", bin2hex(protBuf, pilApdu->Cmd, 4), ilRet);
    return ilRet;
}

IL_FUNC IL_RETCODE clCardClose(IL_CARD_HANDLE* phCard)
{
	IL_WORD ilRet;
	
	PROT_WRITE_EX0(PROT_CARDLIB1, "clCardClose");
	PROT_WRITE_EX0(PROT_CARDLIB3, "crCloseSession");
    ilRet = crCloseSession(phCard->hRdr);
	PROT_WRITE_EX1(PROT_CARDLIB3, "crCloseSession()=%u", ilRet);
	PROT_WRITE_EX1(PROT_CARDLIB1, "clCardClose()=%u", ilRet);

	return ilRet;
}

IL_FUNC IL_RETCODE clAppSelect(IL_CARD_HANDLE* phCard, IL_BYTE P1, IL_BYTE P2, IL_BYTE* pId, IL_BYTE IdLen, IL_BYTE* pOut, IL_WORD* pOutLength)
{
    IL_RETCODE ilRet;
    IL_APDU ilApdu;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppSelect()");

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
    if((ilRet = clAPDU(phCard, &ilApdu, SM_MODE_IF_SESSION)) != 0)
        return ilRet;

	//+++
	if(pOut && pOutLength)
	{
		//MICRON SPECIFIC!!!
		if(IdLen > 2 && ilApdu.LengthOut == 0)
			hex2bin("6F5D8001438102169C820138830210008408A0000004329000018A0105A5405F24031603145F25031103159F080110C202F000E02AE10CDF20010151020001DF210110E10CDF20010251020002DF210110E10CDF20010351020003DF210110",ilApdu.DataOut,&ilApdu.LengthOut);
		//!!!!!!!!!!!!!!!!!!
		*pOutLength = (IL_WORD)ilApdu.LengthOut;
		cmnMemCopy(pOut, ilApdu.DataOut, (IL_WORD)ilApdu.LengthOut);
	}

    return 0;
}

IL_FUNC IL_RETCODE clAppPerformSecOperation(IL_CARD_HANDLE* phCard, IL_BYTE CLA, IL_BYTE* pDataIn, IL_WORD wDataInLen)
{
    IL_RETCODE ilRet;
    IL_APDU ilApdu;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppPerformSecOperation()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[0] = CLA;
    ilApdu.Cmd[1] = INS_PERF_SEC_OP;
	if((phCard->AppVer != UECLIB_APP_VER_10) || phCard->ifNeedMSE)
        ilApdu.Cmd[3] = 0xBE;		
    else
        ilApdu.Cmd[3] = 0xAE;
    cmnMemCopy(ilApdu.DataIn, pDataIn, wDataInLen);
    ilApdu.LengthIn = wDataInLen;
    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_IF_SESSION);

    return ilRet;
}

IL_FUNC IL_RETCODE clAppReadBinaryEx(IL_CARD_HANDLE* phCard, IL_WORD wOffset, IL_WORD wBufLength, IL_BYTE* pOut, IL_WORD* pwOutLen)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppReadBinaryEx()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_READ_BIN;
    ilApdu.Cmd[2] = wOffset>>8;
    ilApdu.Cmd[3] = (IL_BYTE)wOffset;
    ilApdu.LengthExpected = wBufLength;
    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_IF_SESSION);
    if(ilRet == 0)
	{
		*pwOutLen = (IL_WORD)ilApdu.LengthOut;
		cmnMemCopy(pOut, ilApdu.DataOut, (IL_WORD)ilApdu.LengthOut);
	}
	
    return ilRet;
}

IL_FUNC IL_RETCODE clAppReadBinary(IL_CARD_HANDLE* phCard, IL_WORD wOffset, IL_WORD wOutLength, IL_BYTE* pOut)
{
    IL_RETCODE ilRet;
    IL_RETCODE wApduOutLength = 0;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppReadBinary()");
    ilRet = clAppReadBinaryEx(phCard, wOffset, wOutLength, pOut, &wApduOutLength);
    if(ilRet)
        return ilRet;

    if(wApduOutLength != wOutLength)
	{
		PROT_WRITE_EX1(PROT_CARDLIB1, "clAppReadBinaryEx()=%u", ILRET_CRD_LENGTH_NOT_MATCH);
        return ILRET_CRD_LENGTH_NOT_MATCH;
	}

    return 0;
}

IL_FUNC IL_RETCODE clAppGetChallenge(IL_CARD_HANDLE* phCard, IL_WORD wOutLength, IL_BYTE* pOut)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppGetChallenge()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_GET_CHAL;
    ilApdu.LengthExpected = wOutLength;
    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_NONE);
    if(ilRet)
        return ilRet;

    if(ilApdu.LengthOut!=wOutLength)
	{
		PROT_WRITE_EX1(PROT_CARDLIB1, "clAppGetChallenge()=%u", ILRET_CRD_LENGTH_NOT_MATCH);
        return ILRET_CRD_LENGTH_NOT_MATCH;
	}

    cmnMemCopy(pOut, ilApdu.DataOut, (IL_WORD)ilApdu.LengthOut);

    return 0;
}

IL_FUNC IL_RETCODE clAppMutualAuth(IL_CARD_HANDLE* phCard, IL_BYTE CLA, IL_BYTE P2, IL_BYTE* pDataIn, IL_WORD wDataInLen, IL_BYTE* pDataOut, IL_WORD* pDataOutLen)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppMutualAuth()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[0] = CLA;
    ilApdu.Cmd[1] = INS_MUT_AUTH;
    ilApdu.Cmd[3] = P2;//Key Id
    ilApdu.LengthIn = wDataInLen;
    cmnMemCopy(ilApdu.DataIn, pDataIn, wDataInLen);
    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_NONE);
    if(ilRet)
        return ilRet;

    *pDataOutLen = (IL_WORD)ilApdu.LengthOut;
    cmnMemCopy(pDataOut, ilApdu.DataOut, (IL_WORD)ilApdu.LengthOut);

    return 0;
}

IL_FUNC IL_RETCODE clAppInternalAuth(IL_CARD_HANDLE* phCard, IL_BYTE P2, IL_BYTE* pDataIn, IL_WORD wDataInLen, IL_BYTE* pDataOut, IL_WORD* pDataOutLen)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppInternalAuth()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    
    if(phCard->AppVer == UECLIB_APP_VER_10)
        ilApdu.Cmd[0] = 0x80;
    else
        ilApdu.Cmd[0] = 0x00;
            
    ilApdu.Cmd[1] = INS_INT_AUTH;
    ilApdu.Cmd[3] = P2;//Key Id
    ilApdu.LengthIn = wDataInLen;
    cmnMemCopy(ilApdu.DataIn, pDataIn, wDataInLen);

    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_IF_SESSION);
    if(ilRet)
        return ilRet;

    *pDataOutLen = (IL_WORD)ilApdu.LengthOut;
    cmnMemCopy(pDataOut, ilApdu.DataOut, (IL_WORD)ilApdu.LengthOut);

    return 0;
}

IL_FUNC IL_RETCODE clAppVerify(IL_CARD_HANDLE* phCard, IL_BYTE P2, IL_BYTE* pDataIn8, IL_BYTE* pbTriesRemained)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppVerify()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_VERIFY;
    ilApdu.Cmd[3] = P2;//01 - PIN or 05 - PUK
    ilApdu.LengthIn = 8;
    cmnMemCopy(ilApdu.DataIn, pDataIn8, 8);

    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_ALWAYS);

    if(ilApdu.SW1 == 0x63)
        *pbTriesRemained = ilApdu.SW2 & 0x0F;
    else
        *pbTriesRemained = 0xFF;

    return ilRet;
}

IL_FUNC IL_RETCODE clAppChangeRefData(IL_CARD_HANDLE* phCard, IL_BYTE P2, IL_BYTE* pDataIn, IL_WORD wDataInLen)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppChangeRefData()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_CHANGE_DATA;
    ilApdu.Cmd[2] = 0x01;
    ilApdu.Cmd[3] = P2;//Key Id
    ilApdu.LengthIn = wDataInLen;
    cmnMemCopy(ilApdu.DataIn, pDataIn, wDataInLen);

    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_ALWAYS);

    return ilRet;
}

IL_FUNC IL_RETCODE clAppResetRetryCounter(IL_CARD_HANDLE* phCard, IL_BYTE P2)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;
	
	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppResetRetryCounter()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_RST_CNTR;
    ilApdu.Cmd[2] = 0x03;
    ilApdu.Cmd[3] = P2;//Key Id

    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_ALWAYS);

    return ilRet;
}

IL_FUNC IL_RETCODE clAppUpdateBinary(IL_CARD_HANDLE* phCard, IL_WORD wOffset, IL_BYTE* pDataIn, IL_WORD wDataInLen)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppUpdateBinary()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_UPDATE_BIN;
    ilApdu.Cmd[2] = (IL_BYTE)(wOffset>>8);
    ilApdu.Cmd[3] = (IL_BYTE)wOffset;
    ilApdu.LengthIn = wDataInLen;
    cmnMemCopy(ilApdu.DataIn, pDataIn, wDataInLen);

    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_IF_SESSION);

    return ilRet;
}

IL_FUNC IL_RETCODE clAppGetData(IL_CARD_HANDLE* phCard, IL_WORD wTag, IL_BYTE* pOut, IL_WORD* pwOutLength)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppGetData()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_GET_DATA;
    ilApdu.Cmd[2] = wTag>>8;
    ilApdu.Cmd[3] = (IL_BYTE)wTag;
//!!! ÇÀÏËÀÒÊÀ ÄËß ÁÅÑÊÎÍÒÀÊÒÍÎÃÎ ÈÍÒÅÐÔÅÉÑÀ ÊÀÐÒÛ ÀÒËÀÑ 1.1 !!!!!
	if(wTag == 0xDF2C)
		ilApdu.LengthExpected = 1;
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_IF_SESSION);
    if(ilRet)
        return ilRet;

    //MICRON SPECIFIC
#ifdef GET_DATA_VALUE_WITH_TAG 
    if(phCard->ifNeedMSE)
#else    
    if(phCard->ifNeedMSE || (phCard->AppVer > UECLIB_APP_VER_10))
#endif
    {
#ifdef PATCH_9F36	//!!! ÇÀÏËÀÒÊÀ !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		if(!(!phCard->ifNeedMSE && wTag==0x9F36 && ilApdu.LengthOut != 2))
#endif//PATCH_9F36  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			//äëÿ ñîñòàâíûõ òýãîâ ýìóëÿöèÿ ñòàðîãî ïîâåäåíèÿ GET DATA íå ïðîèçâîäèòñÿ ïî óêàçàíèþ ÓÝÊ
			if(!((((wTag>>8) == 0)&&(wTag & 0x20)) || (((wTag>>8) != 0)&&(wTag & 0x2000))))
				ilApdu.LengthOut = AddTag(wTag, ilApdu.DataOut, ilApdu.LengthOut, ilApdu.DataOut);
    }
    
    cmnMemCopy(pOut, ilApdu.DataOut, (IL_WORD)ilApdu.LengthOut);
    *pwOutLength = (IL_WORD)ilApdu.LengthOut;

    return 0;
}

IL_FUNC IL_RETCODE clAppPutData(IL_CARD_HANDLE* phCard, IL_WORD wTag, IL_BYTE* pDataIn, IL_WORD wDataInLen)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppPutData()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_PUT_DATA;
    ilApdu.Cmd[2] = wTag>>8;
    ilApdu.Cmd[3] = (IL_BYTE)wTag;
    ilApdu.LengthIn = wDataInLen;
    cmnMemCopy(ilApdu.DataIn, pDataIn, wDataInLen);

    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_ALWAYS);

    return ilRet;
}

IL_FUNC IL_RETCODE clMSE(IL_CARD_HANDLE* phCard, IL_BYTE P1, IL_BYTE P2, IL_BYTE P3, IL_BYTE P4, IL_BYTE P5)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clMSE()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_MSE;
    ilApdu.Cmd[2] = P1;
    ilApdu.Cmd[3] = P2;
    ilApdu.LengthIn = 3;
    ilApdu.DataIn[0] = P3;
    ilApdu.DataIn[1] = P4;
    ilApdu.DataIn[2] = P5;

    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_NONE);

    return ilRet;
}

IL_FUNC IL_RETCODE clAppComputeDigitalSignature(IL_CARD_HANDLE* phCard, IL_BYTE* pDataIn, IL_WORD wDataInLen)
{
    IL_RETCODE ilRet;
    IL_APDU ilApdu;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppComputeDigitalSignature()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_PERF_SEC_OP;
    ilApdu.Cmd[2] = 0x9E;
    ilApdu.Cmd[3] = 0x9A;
    cmnMemCopy(ilApdu.DataIn, pDataIn, wDataInLen);
    ilApdu.LengthIn = wDataInLen;
    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_IF_SESSION);

    return ilRet;
}

IL_FUNC IL_RETCODE clAppReadRecord(IL_CARD_HANDLE* phCard, IL_BYTE wRecNumber, IL_BYTE bLen, IL_BYTE* pOut, IL_WORD* pwOutLen)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppReadRecord()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_READ_REC;
    ilApdu.Cmd[2] = wRecNumber;
    ilApdu.Cmd[3] = 0x04;
    ilApdu.LengthExpected = bLen;
    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_IF_SESSION);
    if(ilRet)
        return ilRet;

    *pwOutLen = (IL_WORD)ilApdu.LengthOut;
    cmnMemCopy(pOut, ilApdu.DataOut, (IL_WORD)ilApdu.LengthOut);

    return 0;
}

IL_FUNC IL_RETCODE clAppUpdateRecord(IL_CARD_HANDLE* phCard, IL_BYTE wRecNumber, IL_BYTE* pDataIn, IL_WORD wDataInLen)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppUpdateRecord()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_UPDATE_REC;
    ilApdu.Cmd[2] = wRecNumber;
    ilApdu.Cmd[3] = 0x04;
    ilApdu.LengthIn = wDataInLen;
    cmnMemCopy(ilApdu.DataIn, pDataIn, wDataInLen);

    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_IF_SESSION);

    return ilRet;
}

IL_FUNC IL_RETCODE clAppAppendRecord(IL_CARD_HANDLE* phCard, IL_BYTE* pDataIn, IL_WORD wDataInLen)
{
    IL_APDU ilApdu;
    IL_RETCODE ilRet;

	PROT_WRITE_EX0(PROT_CARDLIB1, "clAppAppendRecord()");

    cmnMemSet((IL_BYTE*)&ilApdu, 0, sizeof(IL_APDU));
    ilApdu.Cmd[1] = INS_APPEND_REC;
    ilApdu.LengthIn = wDataInLen;
    cmnMemCopy(ilApdu.DataIn, pDataIn, wDataInLen);

    ilRet = clAPDU(phCard, &ilApdu, SM_MODE_IF_SESSION);

    return ilRet;
}

