#include "opApi.h"
#include "opCtxFunc.h"
#include "KeyType.h"
#include "CertType.h" 
#include "com_cryptodsb.h"	
#include "ru_cryptodsb.h"	

#ifdef SM_SUPPORT
#include "sm_error.h"
#endif//SM_SUPPORT

extern IL_WORD _opCmnCashBinTlvArray(s_opContext *p_opContext, IL_WORD sectorId, IL_WORD blockId);

IL_FUNC IL_WORD _opApiSelectApplication(s_opContext *p_opContext)
{
	IL_WORD RC;
    IL_BYTE resp[UEC_CARD_RESP_MAX_LEN];
    IL_WORD resp_len;

	// �������� ���������� ����������
	if(!p_opContext) return ILRET_OPLIB_INVALID_ARGUMENT;

	// ����������� ��-����������
	if((RC = flAppSelect(p_opContext->phCrd, resp, &resp_len)) != 0)
		return RC;

	// �������������� ��������� ������ ��-���������� � ��������
	if((RC = _opCmnGetAppSystemInfo(p_opContext, resp, resp_len)) != 0)
		return RC;

	return 0;
}

IL_FUNC IL_WORD opApiInitOperation(s_opContext *p_opContext, IL_CARD_HANDLE *phCrd, IL_BYTE opCode,
									  IL_BYTE *pMetaInfo, IL_WORD MetaInfoLen,
									  IL_CHAR *PAN, IL_CHAR *appVer, IL_CHAR *effDate, IL_CHAR *expDate,
									  IL_CHAR *passPhrase, IL_BYTE *ifGostCrypto)
{
	IL_WORD RC;

	// �������������� ������ ���������
#ifndef PROT_IGNORE
	//if(protInit())
	//	return ILRET_OPLIB_INIT_PROTOCOL_ERROR;
#endif//PROT_IGNORE

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiInitOperation()")
	PROT_WRITE_EX1(PROT_OPLIB2, "IN: OpCode=%u", opCode);

	// �������� ���������� ����������
	if(!p_opContext || !phCrd) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	// ������ �������� ��������
	RC = opCtxSetClean(p_opContext);
	if(RC)
		goto End; 

	// ��������� � �������� ��������� �� ���������� ������
	RC = opCtxSetCardReaderHandler(p_opContext, phCrd);
	if(RC)
		goto End; 

	// ��������� � �������� ��� ����������� ��������
	RC = opCtxSetOperationCode(p_opContext, opCode);
	if(RC)
		goto End; 

	// ����������� ������ ��� �������� "���˨���� ���������� ��������� �����!!!
	if(opCode == UEC_OP_REM_MANAGE_CARD_DATA)
	{	
		// �������������� �������� ����������
		if(PAN)
			*PAN = 0;
		if(appVer)
			*appVer = 0;
		if(effDate)
			*effDate = 0;
		if(expDate)
			*expDate = 0;
		if(passPhrase)
			*passPhrase = 0;
		if(ifGostCrypto)
			*ifGostCrypto = 0;

		// ��������� ��������� �������������� ����������
		PROT_WRITE_EX0(PROT_OPLIB3, "flAppReselect()")
		RC = flAppReselect(p_opContext->phCrd);
		PROT_WRITE_EX1(PROT_OPLIB3,"flAppReselect()=%u", RC)

		goto End;
	}

	// ��������� � �������� �������������� 
	if(MetaInfoLen > 0 && pMetaInfo)
	{
		RC = opCtxSetMetaInfo(p_opContext, pMetaInfo, MetaInfoLen);
		if(RC)
			goto End;
	}

	// ����������� ����������
	PROT_WRITE_EX0(PROT_OPLIB3, "_opApiSelectApplication()")
	RC = _opApiSelectApplication(p_opContext);
	PROT_WRITE_EX1(PROT_OPLIB3,"_opApiSelectApplication()=%u", RC)
	if(RC)
		goto End; 

	// ��������� ����� ������� ��� �������������� 
	if(p_opContext->SectorIdAuth == 0)
		p_opContext->SectorIdAuth = 1;

	// ��������������� ��������
	PROT_WRITE_EX0(PROT_OPLIB3, "flAppTerminalAuth()")
	RC = flAppTerminalAuth(p_opContext->phCrd);
	PROT_WRITE_EX1(PROT_OPLIB3,"flAppTerminalAuth()=%u", RC)
	if(RC)
		goto End; 

	// �������������� � �������� �������� PAN
	PROT_WRITE_EX0(PROT_OPLIB3, "opCmnGetPAN()")
	RC = opCmnGetPAN(p_opContext, p_opContext->PAN);
	PROT_WRITE_EX1(PROT_OPLIB3,"opCmnGetPAN()=%u", RC)
	if(RC)
		goto End; 

	// �������������� � ���������� ����� ����� ������������ �����������
	PROT_WRITE_EX0(PROT_OPLIB3, "flGetPassPhrase()")
	RC = flGetPassPhrase(p_opContext->phCrd, p_opContext->PassPhrase); 
	PROT_WRITE_EX1(PROT_OPLIB3,"flGetPassPhrase()=%u", RC)
	if(RC)
		goto End; 

	// �������������� �������� ��������� ������ ��-���������� 
	if(PAN)		
		cmnStrCopy(PAN, p_opContext->PAN);
	if(appVer)	
		cmnStrCopy(appVer, p_opContext->AppVersion);
	if(effDate) 
		cmnStrCopy(effDate, p_opContext->EffectiveDate);
	if(expDate) 
		cmnStrCopy(expDate, p_opContext->ExpirationDate);
	if(passPhrase)
		cmnStrCopy(passPhrase, p_opContext->PassPhrase); 
	if(ifGostCrypto)
		*ifGostCrypto = p_opContext->phCrd->ifGostCrypto;

End:
	if(!RC)
		PROT_WRITE_EX6(PROT_OPLIB2, 
			"OUT:\nPAN=%s\nAppVer=%s\nEffDate=%s\nExpDate=%s\nPassPhrase=%s\nIfGostCrypto=%u", 
			(PAN ? PAN : ""), (appVer ? appVer : ""), (effDate ? effDate : ""), (expDate ? expDate : ""), (passPhrase ? passPhrase : ""), (p_opContext->phCrd->ifGostCrypto));
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiInitOperation()=%u", RC)

	return RC;
}

IL_FUNC IL_WORD opApiVerifyCitizen(s_opContext *p_opContext, IL_BYTE PinNum, IL_CHAR *strPin)
{
	IL_WORD RC;

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiVerifyCitizen()")
	
	// �������� ���������� ����������
	if(!p_opContext || !strPin) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}
	
	PROT_WRITE_EX2(PROT_OPLIB2, "IN: PinNum=%u PinLen=%d", PinNum, (cmnStrLen(strPin)))
	// ��� �������� "������������� ���" ���-���� ���������� ��� ��� �������������� � �������� 
	if(p_opContext->OperationCode != UEC_OP_UNLOCK_PUK)
	{	// ��������� � �������� �������� �������������� ��� ����������� ���������� ���-�����
		RC = opCtxSetPinBlock(p_opContext, strPin);
		if(RC)
			goto End;
		// ��������� � �������� ����� �������������� ���
		p_opContext->PinNum = PinNum;
	}

	// ������������ ���������� 
	PROT_WRITE_EX0(PROT_OPLIB3, "opCmnAppVerifyCitizen()")
	RC = opCmnAppVerifyCitizen(p_opContext);
	PROT_WRITE_EX1(PROT_OPLIB3, "opCmnAppVerifyCitizen()=%u", RC)
	if(RC)
	{	
		if(RC == ILRET_CRD_VERIFY_WRONG_PASSWORD_PRESENTED && p_opContext->PinTriesLeft > 0)
		{
			if(p_opContext->phCrd->AppVer == UECLIB_APP_VER_10)
			{	// ��������!!! ��������� ��� ����� ������ �� ����� ������ 1.0 ������������ ������������
				// ����� ��������� ������������� ��� ���������� ������ ������������� ���������� � �������� �������������� ��������� 
				PROT_WRITE_EX0(PROT_OPLIB3, "opCmnRestoreCryptoSession()")
				RC = opCmnRestoreCryptoSession(p_opContext);
				PROT_WRITE_EX1(PROT_OPLIB3, "opCmnRestoreCryptoSession()=%u", RC)
				if(RC) 
					goto End;
			}
			// ���������� ������ � ����������� �� ���������� ������� ������������ ���
			if(p_opContext->PinTriesLeft > PIN_TRIES_MAX) {
				RC = ILRET_CRD_VERIFY_WRONG_PASSWORD_PRESENTED; goto End;
			}
			RC = ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_1 + p_opContext->PinTriesLeft - 1;
		}
	}

End:
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiVerifyCitizen()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opApiReadCardData(s_opContext *p_opContext, IL_CHAR *in_Str, IL_CHAR *out_Str, IL_WORD *out_StrLen)
{
	IL_WORD RC;

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiReadCardData()")
	PROT_WRITE_EX1(PROT_OPLIB2, "IN: %s", in_Str ? in_Str : "");

	PROT_WRITE_EX0(PROT_OPLIB3, "opCmnReadCardData()")
	RC = opCmnReadCardData(p_opContext, in_Str, out_Str, out_StrLen);
	PROT_WRITE_EX1(PROT_OPLIB3, "opCmnReadCardData()=%u", RC)
	
	if(!RC)
		PROT_WRITE_EX2(PROT_OPLIB2, "OUT[%u]:\n%s", *out_StrLen, out_Str);
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiReadCardData()=%u", RC)

	return RC;
}

IL_FUNC IL_WORD opApiPrepareAppAuthRequest(s_opContext *p_opContext)
{
	IL_WORD RC;

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiPrepareAppAuthRequest()")
	PROT_WRITE_EX0(PROT_OPLIB3, "opCmnPrepareAppAuthRequest()")
	RC = opCmnPrepareAppAuthRequest(p_opContext);
	PROT_WRITE_EX1(PROT_OPLIB3, "opCmnPrepareAppAuthRequest()=%u", RC)
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiPrepareAppAuthRequest()=%u", RC)

	return RC;
}

IL_FUNC IL_WORD opApiCheckAppAuthResponse(s_opContext *p_opContext, IL_BYTE *pAuthResponseData, IL_WORD AuthResponseDataLen, IL_WORD *pAuthResult)
{
	IL_WORD RC;

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiCheckAppAuthResponse()")

	// �������� ���������� ����������
	if(!p_opContext || !pAuthResponseData || !AuthResponseDataLen) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	// ��������� � �������� ��� ��������� �� ����� � ������� ������ �� ������ �������������� ��-����������
	opCtxSetAppAuthResponseData(p_opContext, pAuthResponseData, AuthResponseDataLen);

	// �������� ���������� �������������� ��-����������
	PROT_WRITE_EX0(PROT_OPLIB3, "opCmnCheckAppAuthResponse()")
	RC = opCmnCheckAppAuthResponse(p_opContext, pAuthResult);
	PROT_WRITE_EX1(PROT_OPLIB3, "opCmnCheckAppAuthResponse()=%u", RC)

End:
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiCheckAppAuthResponse()=%u", RC)
	return 0;
}

IL_FUNC IL_WORD opApiPrepareAppAuthRequestIssSession(s_opContext *p_opContext)
{
	IL_WORD RC;

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiPrepareAppAuthRequestIssSession()")
	PROT_WRITE_EX0(PROT_OPLIB3, "flAppAuthRequestIssSession()")
	RC = flAppAuthRequestIssSession(p_opContext->phCrd, 
					p_opContext->AuthRequestIssSessionBuf, p_opContext->AuthRequestIssSessionLen, 
					p_opContext->pAuthRequestBuf, p_opContext->pAuthRequestLen);
	PROT_WRITE_EX1(PROT_OPLIB3, "flAppAuthRequestIssSession()=%u", RC);
	if(!RC)
		PROT_WRITE_EX2(PROT_OPLIB2, "OUT: AuthRequestIssSession[%u]=%s", *p_opContext->pAuthRequestLen, (bin2hex(p_opContext->TmpBuf, p_opContext->pAuthRequestBuf, *p_opContext->pAuthRequestLen)));
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiPrepareAppAuthRequestIssSession()=%u", RC)

	return RC;
}


IL_FUNC IL_WORD opApiSetIssuerCryptoSession(s_opContext *p_opContext, 
											IL_BYTE *in_pCardCryptogramm20, IL_BYTE in_CardCryptogrammLength,
											IL_BYTE *out_pHostChallenge16, IL_BYTE *out_pCardCryptogramm4)
{
	IL_WORD RC;

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiSetIssuerCryptoSession()")

	// �������� ���������� ����������
	if(!p_opContext || !in_pCardCryptogramm20 || !out_pHostChallenge16 || !out_pCardCryptogramm4)
		return ILRET_OPLIB_INVALID_ARGUMENT;

	PROT_WRITE_EX2(PROT_OPLIB2, "IN: CardCryptogramm[%u]=%s", in_CardCryptogrammLength, (bin2hex(p_opContext->TmpBuf, in_pCardCryptogramm20, in_CardCryptogrammLength)))  

	// ������ ���������� ������ ������ ����� ������ � ��������� ��-����������
	PROT_WRITE_EX0(PROT_OPLIB3, "flIssuerAuth()")
	RC = flIssuerAuth(p_opContext->phCrd, in_pCardCryptogramm20, in_CardCryptogrammLength, p_opContext->TmpBuf, &p_opContext->wTmp);
	PROT_WRITE_EX1(PROT_OPLIB3, "flIssuerAuth()=%u", RC)

	if(!RC)
	{	// ���������� ������ ��� �������� ������������� ���������� ������ �� ������� ����� 
		IL_BYTE len = p_opContext->phCrd->ifNeedMSE ? 8 : 16;
		cmnMemCopy(out_pCardCryptogramm4, p_opContext->TmpBuf, p_opContext->wTmp/*4*/);
		if(p_opContext->phCrd->ifNeedMSE)
			cmnMemCopy(out_pHostChallenge16,  &in_pCardCryptogramm20[0], 8);
		else
			cmnMemCopy(out_pHostChallenge16,  &in_pCardCryptogramm20[4], 16);
		PROT_WRITE_EX3(PROT_OPLIB2, "OUT: HostChallenge[%u]=%s CardCryptogramm[4]=%s", 
					   len, (bin2hex(p_opContext->TmpBuf, out_pHostChallenge16, len)), (bin2hex(&p_opContext->TmpBuf[100], out_pCardCryptogramm4, 4)))
	}

	PROT_WRITE_EX1(PROT_OPLIB1, "opApiSetIssuerCryptoSession()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opApiDeinitOperation(s_opContext *p_opContext)
{
	IL_WORD RC = 0;
	IL_WORD RC1 = 0;

	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "���������� ��������", ".")
	PROT_WRITE_EX0(PROT_OPLIB1, "opApiDeinitOperation()")

	if(p_opContext) 
	{
		// ��������� �����
		if(p_opContext->phCrd)
		{
			PROT_WRITE_EX0(PROT_OPLIB3, "clCardClose()")
			RC = clCardClose(p_opContext->phCrd);
			PROT_WRITE_EX1(PROT_OPLIB3, "clCardClose()=%u", RC)
		}
		// ����������� �����
		if(p_opContext->phCrd)
		{
			PROT_WRITE_EX0(PROT_OPLIB3, "flDeinitReader()")
			RC1 = flDeinitReader(p_opContext->phCrd);
			PROT_WRITE_EX1(PROT_OPLIB3, "flDeinitReader()=%u", RC1)
		}
		// ��������� ��� ��������
		if(!RC) RC = RC1;
	}
	else
		RC = ILRET_OPLIB_INVALID_ARGUMENT;

	OP_CMN_DISPLAY_LINE(p_opContext, RC ? "������" : "OK");
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiDeinitOperation()=%u", RC)

	// ���������� ����� ����������������
	PROT_DEINIT

	return RC;
}

IL_FUNC IL_WORD opApiRunApduPacket(s_opContext *p_opContext)
{
	IL_WORD RC = 0;
	IL_BYTE *pIn, *pOut;
	IL_BYTE *pInMax;
	IL_WORD max_out_len;
	IL_BYTE SM_MODE;
	IL_APDU_PACK_ELEM ApduElem;

	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "���������� ��������������� ������ APDU-�������", ".")
	PROT_WRITE_EX0(PROT_OPLIB1, "opApiRunApduPacket()")

	// �������� ���������� ����������
	if(!p_opContext || !p_opContext->pApduIn || !p_opContext->ApduInLen || !p_opContext->pApduOut || !p_opContext->ApduOutLen) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	if(p_opContext->ApduInLen < 2*1024)
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: ApduIn[%u]=%s", p_opContext->ApduInLen, (bin2hex(p_opContext->TmpBuf, p_opContext->pApduIn, p_opContext->ApduInLen))); 

	// ��������� ������������ ������ ��������� ������ ��������� ���������� ������ � ���������� APDU-������ � ������
	max_out_len = *p_opContext->ApduOutLen;

	// ������� ������ ��������� ������ � ��������� ���������� ������
	*p_opContext->ApduOutLen		= 0;
	*p_opContext->pApduPacketResult = 0;

	// �������������� ��������� �� ������� � �������� ������ ������
	pIn  = p_opContext->pApduIn;
	pOut = p_opContext->pApduOut;

	// �������� APDU-������� ������
	for(*p_opContext->pApduPacketSize = 0, pInMax = pIn + p_opContext->ApduInLen;
		RC == 0 && *p_opContext->pApduPacketResult == 0 && pIn < pInMax; 
		(*p_opContext->pApduPacketSize)++)
	{
		// ������������ �������� ������ � ������� APDU-�������
		pIn = _bin2apduin(pIn, &ApduElem);
		// �������� APDU-�������
		SM_MODE = SM_MODE_NONE; //====p_opContext->AuthResult == 400 ? SM_MODE_IF_SESSION : SM_MODE_NONE;
		PROT_WRITE_EX0(PROT_OPLIB2, "flRunApdu()")
		*p_opContext->pApduPacketResult = flRunApdu(p_opContext->phCrd, SM_MODE, &ApduElem);
		PROT_WRITE_EX1(PROT_OPLIB2, "flRunApdu()=%u", *p_opContext->pApduPacketResult)
		// �o���������� ����� APDU-������� � �������� �������� ������
		PROT_WRITE_EX0(PROT_OPLIB2, "_apduout2bin()")
		RC = _apduout2bin(&ApduElem, pOut, p_opContext->ApduOutLen, max_out_len);
		PROT_WRITE_EX1(PROT_OPLIB2, "_apduout2bin()=%u", RC)
	}

End:
	if(!RC)
		RC = *p_opContext->pApduPacketResult;
	
	OP_CMN_DISPLAY_LINE(p_opContext, RC ? "������" : "OK")
	PROT_WRITE_EX1(PROT_OPLIB2, "OUT: ApduPacketResult=%u", *p_opContext->pApduPacketResult)
	PROT_WRITE_EX1(PROT_OPLIB2, "OUT: ApduPacketSize=%u", *p_opContext->pApduPacketSize)
	if(*p_opContext->ApduOutLen < 2*1024)
		PROT_WRITE_EX2(PROT_OPLIB2, "OUT: ApduOut[%u]=%s", *p_opContext->ApduOutLen, bin2hex(p_opContext->TmpBuf, p_opContext->pApduOut, *p_opContext->ApduOutLen));
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiRunApduPacket()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opApiManagePinPuk(s_opContext *p_opContext, IL_BYTE PinNum, IL_CHAR *newPin)
{
	IL_WORD RC;

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiManagePinPuk()")

	// �������� ���������� ����������
	if(!p_opContext || !newPin) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	// ������� ����� ���� ������� ������ ��� ���������������� ������ ��������
	if(!(p_opContext->OperationCode == UEC_OP_CHANGE_PIN 
			|| p_opContext->OperationCode == UEC_OP_UNLOCK_PIN 
			|| p_opContext->OperationCode == UEC_OP_CHANGE_PUK
			|| p_opContext->OperationCode == UEC_OP_UNLOCK_PUK)) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	PROT_WRITE_EX2(PROT_OPLIB2, "PinNum=%u PinLen=%u", PinNum, (cmnStrLen(newPin)))

	// ��������� � �������� ����� ���
	p_opContext->PinNum = PinNum; //????

	// �������� � ��������� ����� �������� ���/���
	opCtxSetNewPinStr(p_opContext, newPin);
	
	// �������� � ��������� �������������� �������� ���/���, �����������, ��� �� ��� �������� �����!!!
	opCtxSetConfirmPinStr(p_opContext, newPin);

	// ��� �������� "������������� ���" ���������� ������������ ���������� ���������� ����� ������ � ����������
	if(p_opContext->OperationCode == UEC_OP_UNLOCK_PUK)
	{
		// ����������� ������ ���������� 
		PROT_WRITE_EX0(PROT_OPLIB3, "flAppReselect()")
		RC = flAppReselect(p_opContext->phCrd);
		PROT_WRITE_EX1(PROT_OPLIB3, "flAppReselect()=%u", RC)
		if(RC)
			return RC;
		// � ��������������� ��������
		PROT_WRITE_EX0(PROT_OPLIB3, "flAppTerminalAuth()")
		RC = flAppTerminalAuth(p_opContext->phCrd);
		PROT_WRITE_EX1(PROT_OPLIB3, "flAppTerminalAuth()=%u", RC)
		if(RC)
			return RC;
		// ������������ ���������� �� ��������� ���
		PROT_WRITE_EX0(PROT_OPLIB3, "opApiVerifyCitizen()")
		RC = opApiVerifyCitizen(p_opContext, IL_KEYTYPE_05_PUK, "");
		PROT_WRITE_EX1(PROT_OPLIB3, "opApiVerifyCitizen()=%u", RC)
		if(RC)
			return RC;
	}

	// �������� ������� �������� ���/��� �� �����
	PROT_WRITE_EX0(PROT_OPLIB3, "opCmnChangePinPuk()")
	RC = opCmnChangePinPuk(p_opContext);
	PROT_WRITE_EX1(PROT_OPLIB3, "opCmnChangePinPuk()=%u", RC)

End:
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiManagePinPuk()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opApiGetCardBlockDataDescr(s_opContext *p_opContext, IL_CHAR *strBlockId, IL_CHAR *strBlockDataDescr, IL_WORD *BlockDataDescrLen)
{
	IL_WORD RC;
	IL_INT sectorId, blockId;
	IL_INT i;
	DATA_DESCR *pDataDescr;
	IL_CHAR strData[512];
	BLOCK_DESCR *pBlockDescr;
	//TODO: �������������� ������ ������� ����������� 
	IL_CHAR *format[] = { 
		"%u-%u-%X|%X,%X,%X|%u|%u|%s|%s|\n", 
		"%u-%u-%u|%X,%X,%X|%u|%u|%s|%s|\n", 
		"%u-%u-%u|%X,%X,%X|%u|%u|%s|%s|\n", 
		"%u-%u-%X|%X,%X,%X|%u|%u|%s|%s|\n" 
	};
	IL_WORD MaxLen; 
	IL_WORD max_buf_len;
	IL_CHAR str[512+15];
	IL_WORD str_len;
	IL_CHAR *pS = NULL;
	IL_TAG TagId[MAX_TAG_IDS_DESCR] = { 0 };

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiGetCardBlockDataDescr()")

	// �������� ���������� ����������
	if(!p_opContext || !strBlockId || !strBlockDataDescr || !BlockDataDescrLen || *BlockDataDescrLen == 0) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	PROT_WRITE_EX1(PROT_OPLIB2, "BlockId=%s", strBlockId)

	// ��������� ������������ ������ ��������� ������
	max_buf_len = *BlockDataDescrLen - 1;

	// ��������� �������������� ������� � ����� ������������� ������ 
	sectorId = blockId = 0;
	sscanf(strBlockId, "%u-%u", &sectorId, &blockId);

	// ������� ��������� �� ��������� �����
	PROT_WRITE_EX2(PROT_OPLIB3, "opDescrGetBlock(%d-%d)", sectorId, blockId)
	RC = opDescrGetBlock(p_opContext, (IL_WORD)sectorId, (IL_WORD)blockId, &pBlockDescr);
	PROT_WRITE_EX3(PROT_OPLIB3, "opDescrGetBlock(%d-%d)=%u", sectorId, blockId, RC)
	if(RC)
		goto End;

	// �������� ���������� ���� �����
	if(pBlockDescr->FileType > BLOCK_DATA_TYPE_MAX) {
		RC = ILRET_OPLIB_INVALID_FILE_TYPE; goto End;
	}

	// �������������� ������ ��������������� ���������� �������������� �����
	if((pS = strstr(strBlockId, "-")))
		pS = strstr(pS+1, "-");
	if(pS)
	{
		IL_INT tagId;
		for(i = 0; pS && i < MAX_TAG_IDS_DESCR-1; pS = strstr(pS, ","))
		{
			tagId = 0;
			sscanf(++pS, pBlockDescr->FileType == BLOCK_DATA_TLV || pBlockDescr->FileType == BLOCK_DATA_BINTLV ? "%X" : "%u", &tagId);
			if(!tagId)
				break;
			TagId[i++] = (IL_TAG)tagId;
		}
	}

	// ������� ��������� �� ��������� ������� �������� ������ �����
	PROT_WRITE_EX0(PROT_OPLIB3, "opDescrGetFirstDataInBlock()")
	RC = opDescrGetFirstDataInBlock(p_opContext, (IL_WORD)sectorId, (IL_WORD)blockId, &pDataDescr);
	PROT_WRITE_EX1(PROT_OPLIB3, "opDescrGetFirstDataInBlock()=%u", RC)
	if(RC)
		return RC;

	// ����������� �������� ������ � ����������� ���� ��������� ������ �����
	for(*BlockDataDescrLen = 0, strBlockDataDescr[0] = 0; 
		pDataDescr->SectorId == sectorId && pDataDescr->BlockId == blockId; 
		pDataDescr++)
	{
		// �������� ������������� ��������� � ������� �������������� ��������� 
		for(i = 0; TagId[i]; i++)
			if(TagId[i] == pDataDescr->TagId)
				break;
		if(*TagId && !TagId[i])
			continue; // ����������...  

		// ��������� �������� �������� ������
#ifndef PROT_IGNORE
		sprintf((IL_CHAR*)p_opContext->TmpBuf, 
				pBlockDescr->FileType == BLOCK_DATA_TLV || pBlockDescr->FileType == BLOCK_DATA_BINTLV 
				? "%u-%u-%X" : "%u-%u-%u", pDataDescr->SectorId, pDataDescr->BlockId, pDataDescr->TagId);
#endif//PROT_IGNORE
		PROT_WRITE_EX1(PROT_OPLIB3, "opCmnGetDataByDataDescr(%s)", ((IL_CHAR*)p_opContext->TmpBuf))
		RC = opCmnGetDataByDataDescr(p_opContext, pDataDescr, strData, &MaxLen);
		PROT_WRITE_EX2(PROT_OPLIB3, "opCmnGetDataByDataDescr(%s)=%u", ((IL_CHAR*)p_opContext->TmpBuf), RC)
		if(RC)
			break;

		// ���� ������ ������ ������ � ��� ����� ������ - ������, �� ���������!!!
		if(!strData[0] && pBlockDescr->FileType == BLOCK_DATA_RECORD)
			break;
		
		// ����������� ��������� ��� �������� �������� ������
		sprintf(str, format[pBlockDescr->FileType], 
			         pDataDescr->SectorId, pDataDescr->BlockId, pDataDescr->TagId,
					 pDataDescr->TPath[0], pDataDescr->TPath[1], pDataDescr->TPath[2],	
					 //---pDataDescr->Type, MaxLen, pDataDescr->Name,	
					 pDataDescr->Type, MaxLen > pDataDescr->MaxLen ? MaxLen : pDataDescr->MaxLen, pDataDescr->Name,
					 strData);

		// �������� ������������ ������
		str_len = (IL_WORD)strlen(str);
		if(*BlockDataDescrLen + str_len > max_buf_len) {
			RC = ILRET_OPLIB_BLOCKDATADESCR_BUF_IS_OVER; break;
		}

		// ������� ������-��������� �������� ������ � �������� �����
		sprintf(&strBlockDataDescr[*BlockDataDescrLen], "%s", str);
		*BlockDataDescrLen += str_len;
	}

End:
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiGetCardBlockDataDescr()=%u", RC)
	return RC;
}

IL_WORD _get_data_len(IL_CHAR *str)
{
	IL_WORD len;
	
	for(len = 0; *str && *str != '\n';len++, str++) ;

	return len;
}

void _GetTagPath(IL_TAG RootTag, DATA_DESCR *pDataDescr, IL_TAG *TagPath, IL_WORD *TagNums)
{
	IL_BYTE i;

	*TagNums = 0;
	if(RootTag)
		TagPath[(*TagNums)++] =  RootTag;
	for(i = 0; pDataDescr->TPath[i] && i < TPATH_MAX_LEN; i++)
		TagPath[(*TagNums)++] = (IL_TAG)pDataDescr->TPath[i];
	TagPath[(*TagNums)++] = (IL_TAG)pDataDescr->TagId; 
}

typedef struct
{
	IL_TAG  TagId;		// ������������� ������������ ���������� TLV-���������
	IL_BYTE *pData;		// ��������� �� ��������� TLV-���������
	IL_DWORD DataLen;	// ����� ���������� TLV-���������
} TLV_DATA;

#define TLV_UNMOD_LEN	10
#define TLV_EXTRA_LEN   15 
IL_WORD _opApiCompileBinTlvArray(s_opContext *p_opContext, BLOCK_DESCR *pBlockDescr, DATA_DESCR *pUpdateDescr, IL_BYTE *Data, IL_WORD DataLen, IL_BYTE mode)
{
	IL_WORD RC = 0;
	int i, j;
	TLV_DATA TlvTmp[TPATH_MAX_LEN] = { 0 };
	TLV_DATA TlvRoot = { 0 };
	IL_TAG TagPath[TPATH_MAX_LEN+3]; 
	IL_WORD TagNums;
	IL_BYTE *curData;
	IL_DWORD curDataLen;
	DATA_DESCR *pFDataDescr;
	DATA_DESCR *pDataDescr;
	IL_BYTE *pBinTlvData = NULL;
	IL_WORD BinTlvDataLen = 0;
	IL_BYTE isCompile = 0;
	IL_WORD addLen = 0;
	IL_WORD TlvUnm[TLV_UNMOD_LEN] = { 0 };

//protWriteEx(0, "BinTlvCur[%u]=%s", p_opContext->BinTlvDescr.DataLen, bin2hex(p_opContext->TmpBuf, p_opContext->BinTlvDescr.pData, p_opContext->BinTlvDescr.DataLen));

	// �������� ��������� �� ������ ������� ������ �����
	if((RC = opDescrGetFirstDataInBlock(p_opContext, pUpdateDescr->SectorId, pUpdateDescr->BlockId, &pFDataDescr)))
		goto End;

Precompile: 
	// �������������� ����������� �������� Tlv-������ � ���������� ��������������� ��������
	for(pDataDescr = pFDataDescr; pDataDescr->SectorId == pUpdateDescr->SectorId && pDataDescr->BlockId == pUpdateDescr->BlockId; pDataDescr++)
	{
		if(!pDataDescr->TPath[0])
		{	// �������� ������� ������
			if(pDataDescr == pUpdateDescr)
			{	// �������������� TLV-�������
				curDataLen = (mode == BINTLV_DATA_DELETE) ? 0 : DataLen;
				curData    = Data; 
			}
			else
			{	// ���������������� �������� TLV-�������
				_GetTagPath(pBlockDescr->RootTag, pDataDescr, TagPath, &TagNums);  
				if(TagFindByPath(p_opContext->BinTlvDescr.pData, p_opContext->BinTlvDescr.DataLen, TagPath, TagNums, &curDataLen, &curData, 0))
					curDataLen = 0;
			}
			if(curDataLen)
				TlvRoot.DataLen += AddTag((IL_TAG)pDataDescr->TagId, 
										  isCompile ? curData : NULL, 
										  curDataLen, 
										  isCompile ? &TlvRoot.pData[TlvRoot.DataLen] : NULL);
		}
		else 
		{	// ��������� ������� ���������� TLV-������ 
			if(!isCompile)
			{	// �������� ������ ��� ���������� ����������� TLV-������ 	
				if(pDataDescr == pUpdateDescr)
				{	// ��������� �������������� ������� ������
					// �������� �������� �� ������� ���������� ��������� ������ ���������� ������ ��� ��������������  ����������� TLV-������
					addLen = 0;
					if(mode == BINTLV_DATA_ADD)
						addLen = DataLen + TLV_EXTRA_LEN;
					else if(mode == BINTLV_DATA_DELETE)
						addLen = 0;
					else
					{	 
						_GetTagPath((IL_TAG)pBlockDescr->RootTag, pUpdateDescr, TagPath, &TagNums); 
						if((RC = TagFindByPath(p_opContext->BinTlvDescr.pData, p_opContext->BinTlvDescr.DataLen, TagPath, TagNums, &curDataLen, &curData, 0)))
							goto End;
						addLen = DataLen > curDataLen ? DataLen - curDataLen + TLV_EXTRA_LEN : 0;
					}

					// �������� ������ ��� ���������� ����������� TLV-������
					for(i = 0, TagPath[0] = pBlockDescr->RootTag; pDataDescr->TPath[i] && i < TPATH_MAX_LEN; i++)
					{
						TagPath[i+1] = pDataDescr->TPath[i];
						if(TagFindByPath(p_opContext->BinTlvDescr.pData, p_opContext->BinTlvDescr.DataLen, TagPath, i+1, &curDataLen, &curData, 0))
							curDataLen = 0;
						TlvTmp[i].TagId   = pDataDescr->TPath[i];
						TlvTmp[i].DataLen = curDataLen + addLen;   // ����������� ��� �������� ��������� �����������!!! 
//protWriteEx(0, "*** TlvTmp[%d]: MemAlloc(%u)", i, TlvTmp[i].DataLen);
						TlvTmp[i].pData   = cmnMemAlloc(TlvTmp[i].DataLen);
						if(!TlvTmp[i].pData) {
							RC = ILRET_SYS_MEM_ALLOC_ERROR; goto End;
						}
						TlvTmp[i].DataLen = 0;
					}
				}
			}
			else
			{	// ��������� ������� � ��������� 
				// j - ������ �������������� ���� ����������
				for(j = 0; pDataDescr->TPath[j] && j < TPATH_MAX_LEN; j++) ;
				for(i = 0; TlvTmp[i].TagId != pDataDescr->TPath[j-1] && i < sizeof(TlvTmp); i++) ;
				if(i < sizeof(TlvTmp))
				{	// ��������� ������� ������ � ���������
					_GetTagPath((IL_TAG)pBlockDescr->RootTag, pDataDescr, TagPath, &TagNums); 
					if(TagFindByPath(p_opContext->BinTlvDescr.pData, p_opContext->BinTlvDescr.DataLen, TagPath, TagNums, &curDataLen, &curData, 0))
						curDataLen = 0;
					if(pDataDescr == pUpdateDescr)
					{
						curDataLen = (mode == BINTLV_DATA_DELETE) ? 0 : DataLen;
						curData    = Data;
					}
					else if(mode == BINTLV_DATA_UPDATE_EX && pDataDescr->TPath[0] == 0x7F7F)
						curDataLen = 0; //++++ ����������� ������ ��������� ���� ��������� ������� 7F7F!!!
					if(curDataLen)
						TlvTmp[i].DataLen += AddTag((IL_TAG)pDataDescr->TagId, curData, curDataLen, &TlvTmp[i].pData[TlvTmp[i].DataLen]);
				}
			}
		}
	}

	if(!isCompile)
	{
		if(TlvRoot.DataLen)
		{	// ������� ������ ��� �������������� ������� �������� TLV-������
			TlvRoot.pData   = cmnMemAlloc(TlvRoot.DataLen);
//protWriteEx(0, "*** TlvRoot: MemAlloc(%u)", TlvRoot.DataLen);
			if(!TlvRoot.pData) {
				RC = ILRET_SYS_MEM_ALLOC_ERROR; goto End;
			}
			TlvRoot.DataLen = 0;
		}

		// ������������ ���������� �������� ������ � ��������� �����������
		isCompile = 1;
		goto Precompile;
	}

	isCompile = 0;
	
	// ����������� ������ BinTlv-������
Compile:
//if(!isCompile)
//{
//protWriteEx(0, "--- BEFORE COMPILE --------");
//protWriteEx(0, "TlvRoot[%u]=%s", TlvRoot.DataLen, bin2hex(p_opContext->TmpBuf, TlvRoot.pData, TlvRoot.DataLen));
//for(i = 0; i < TPATH_MAX_LEN; i++)
//protWriteEx(0, "TlvTmp%d[%u]=%s", i, TlvTmp[i].DataLen, bin2hex(p_opContext->TmpBuf, TlvTmp[i].pData, TlvTmp[i].DataLen));	
//protWriteEx(0, "---------------------------");
//}

	BinTlvDataLen = 0;
	// ����������� �������� TLV-��������
	if(TlvRoot.DataLen)
	{	
		if(isCompile)
		{
//protWriteEx(0, "*** Compile TlvRoot: Off=%u Len=%u", BinTlvDataLen, TlvRoot.DataLen);
//protWriteEx(0, "%s", bin2hex(p_opContext->TmpBuf, TlvRoot.pData, TlvRoot.DataLen));
			cmnMemCopy(&pBinTlvData[BinTlvDataLen], TlvRoot.pData, TlvRoot.DataLen); 
			cmnMemFree(TlvRoot.pData);
			TlvRoot.pData = NULL;
		}
		BinTlvDataLen += TlvRoot.DataLen; 
	}

	// ����������� ��������� �������������� ���������� TLV-��������� � TlvTmp[0]
	if(!isCompile)
	{
		for(i = TPATH_MAX_LEN-1; i > 0; i--)
		{
			if(TlvTmp[i].DataLen)
			{	//TODO: ��������� ������� ��������� � ��������� �������� ������
//protWriteEx(0, "TlvTmp%d[%u]=%s", i, TlvTmp[i].DataLen, bin2hex(p_opContext->TmpBuf, TlvTmp[i].pData, TlvTmp[i].DataLen));
//protWriteEx(0, "TlvTmp%d]: AddTag(%lX) Off=%u Len=%u", i-1, TlvTmp[i].TagId, TlvTmp[i-1].DataLen, TlvTmp[i].DataLen); 
				TlvTmp[i-1].DataLen += AddTag(TlvTmp[i].TagId, TlvTmp[i].pData, TlvTmp[i].DataLen, &TlvTmp[i-1].pData[TlvTmp[i-1].DataLen]);
//protWriteEx(0, "TlvTmp%d[%d]=%s", i-1, TlvTmp[i].DataLen, bin2hex(p_opContext->TmpBuf, TlvTmp[i-1].pData, TlvTmp[i-1].DataLen)); 
				cmnMemFree(TlvTmp[i].pData);
				TlvTmp[i].pData = NULL;
			}
		}
	}
	if(TlvTmp[0].DataLen)
	{	// ����������� �������� ���������������� ���������
//if(isCompile)
//{
//protWriteEx(0, "*** Compile TlvTmp[0]: AddTag(%X) Off=%u  Len=%u", TlvTmp[0].TagId, BinTlvDataLen, TlvTmp[0].DataLen);
//protWriteEx(0, "%s", bin2hex(p_opContext->TmpBuf, TlvTmp[0].pData, TlvTmp[0].DataLen));
//}
		BinTlvDataLen += AddTag(TlvTmp[0].TagId, 
								isCompile ? TlvTmp[0].pData : NULL, 
								TlvTmp[0].DataLen, 
								isCompile ? &pBinTlvData[BinTlvDataLen] : NULL);
		if(isCompile)
		{
//protWriteEx(0, "*** pBinTlvData[%u]: %s", BinTlvDataLen, bin2hex(p_opContext->TmpBuf, pBinTlvData, BinTlvDataLen));
			cmnMemFree(TlvTmp[0].pData);
			TlvTmp[0].pData = NULL;
		}
	}

	// ����������� �������� ������������������ ����������
	for(pDataDescr = pFDataDescr; pDataDescr->SectorId == pUpdateDescr->SectorId && pDataDescr->BlockId == pUpdateDescr->BlockId; pDataDescr++)
	{
		if(!pDataDescr->TPath[0])  
			continue; // �������� ������� �������
		if(pDataDescr->TPath[0] == TlvTmp[0].TagId)
			continue; // ���������������� ���������
		for(i = 0; TlvUnm[i] && i < TLV_UNMOD_LEN; i++)
			if(TlvUnm[i] == pDataDescr->TPath[0])
				break;
		if(i >= TLV_UNMOD_LEN || TlvUnm[i])
			continue; // ��� ������������
		TlvUnm[i] = pDataDescr->TPath[0];
		_GetTagPath((IL_TAG)pBlockDescr->RootTag, pDataDescr, TagPath, &TagNums); 
		if(!TagFindByPath(p_opContext->BinTlvDescr.pData, p_opContext->BinTlvDescr.DataLen, TagPath, TagNums, &curDataLen, &curData, 1))
		{
			if(isCompile)
				cmnMemCopy(&pBinTlvData[BinTlvDataLen], curData, curDataLen); 
			BinTlvDataLen += curDataLen;
		}
	}

	if(!isCompile)
	{
		BinTlvDataLen = TlvRoot.DataLen; 
		if(TlvTmp[0].DataLen)
			BinTlvDataLen += AddTag(TlvTmp[0].TagId, NULL, TlvTmp[0].DataLen, NULL);
//protWriteEx(0, "*** pBinTlvData: MemAlloc(%u)", BinTlvDataLen+10);
		pBinTlvData = cmnMemAlloc(BinTlvDataLen+10);
		cmnMemClr((IL_BYTE*)TlvUnm, sizeof(TlvUnm));
		isCompile = 1;
		goto Compile;
	}

	// ����������� ���������� ������ 
	if(p_opContext->BinTlvDescr.pData)
	{
		cmnMemFree(p_opContext->BinTlvDescr.pData);
		p_opContext->BinTlvDescr.pData = NULL;
	}

	// ��������� ����������� ����� �������������� TLV-������� 
	p_opContext->BinTlvDescr.DataLen = AddTag((IL_TAG)pBlockDescr->RootTag, NULL, BinTlvDataLen, NULL);

	// �������� ������ ��� �������������� TLV-�������
	p_opContext->BinTlvDescr.pData = cmnMemAlloc(p_opContext->BinTlvDescr.DataLen);
	if(!p_opContext->BinTlvDescr.pData)
	{
		RC = ILRET_SYS_MEM_ALLOC_ERROR;
		goto End;
	}

	// ����������� ���������������� TLv-������ � �������� ���
	AddTag((IL_TAG)pBlockDescr->RootTag, pBinTlvData, BinTlvDataLen, p_opContext->BinTlvDescr.pData);
//protWriteEx(0, "BinTlvNew[%u]=%s", p_opContext->BinTlvDescr.DataLen, bin2hex(p_opContext->TmpBuf, p_opContext->BinTlvDescr.pData, p_opContext->BinTlvDescr.DataLen));
	
	// � ���������� ������ �� �����
	RC = flUpdateBinary(p_opContext->phCrd, 0, p_opContext->BinTlvDescr.DataLen, p_opContext->BinTlvDescr.pData); 
	if(RC)
	{
		cmnMemFree(p_opContext->BinTlvDescr.pData);
		cmnMemClr((IL_BYTE*)&p_opContext->BinTlvDescr, sizeof(BINTLV_DESCR));
	}

//{
//IL_TAG PATH_75[1]   = {  IL_TAG_75 };
//IL_TAG PATH_7F7F[2] = { IL_TAG_75, IL_TAG_7F7F };
//IL_TAG PATH_5F40[3] = { IL_TAG_75, IL_TAG_7F7F, IL_TAG_5F40 }; 
//IL_TAG PATH_76[3]   = { IL_TAG_75, IL_TAG_7F7F, IL_TAG_76 }; 
//IL_TAG PATH_DF23[4] = { IL_TAG_75, IL_TAG_7F7F, IL_TAG_76, IL_TAG_DF23 };
//
//
//RC = TagFindByPath(p_opContext->BinTlvData, p_opContext->BinTlvDataLen, PATH_75, 1, &curDataLen, &curData, 0);
//RC = TagFindByPath(p_opContext->BinTlvData, p_opContext->BinTlvDataLen, PATH_7F7F, 2, &curDataLen, &curData, 0);
//RC = TagFindByPath(p_opContext->BinTlvData, p_opContext->BinTlvDataLen, PATH_76, 3, &curDataLen, &curData, 0);
//RC = TagFind(curData, curDataLen, IL_TAG_DF23, &curDataLen, &curData, 0);
//RC = TagFindByPath(p_opContext->BinTlvData, p_opContext->BinTlvDataLen, PATH_DF23, 4, &curDataLen, &curData, 0);
//}

End:
	// ��������� ���������� ������
	if(pBinTlvData)
		cmnMemFree(pBinTlvData);
	if(TlvRoot.pData)
		cmnMemFree(TlvRoot.pData);
	for(i = 0; i < sizeof(TlvTmp)/sizeof(TLV_DATA); i++)
		if(TlvTmp[i].pData)
			cmnMemFree(TlvTmp[i].pData);

	return RC;
}

//+++
IL_WORD _opApiUpdateBinTlvData(s_opContext *p_opContext, BLOCK_DESCR *pBlockDescr, DATA_DESCR *pDataDescr, IL_BYTE *Data, IL_WORD DataLen)
{
	IL_WORD RC;
	IL_TAG TagPath[TPATH_MAX_LEN+2];
	IL_WORD TagsNums;
	IL_BYTE *pData;
	IL_DWORD data_len;

	// ������������ ������ ���� �� �������� �������� ������
	_GetTagPath(pBlockDescr->RootTag, pDataDescr, TagPath, &TagsNums);

	// ������������ ����� �������� ������� 
	RC = TagFindByPath(p_opContext->BinTlvDescr.pData, p_opContext->BinTlvDescr.DataLen, TagPath, TagsNums, &data_len, &pData, 0);  
	if(RC)
	{
		if(RC == ILRET_DATA_TAG_NOT_FOUND)
		{
			if(DataLen == 0 || (pDataDescr->Type == DATA_ASCII && Data[0] == 0))
				// ������ ������ - ������ �� ������!
				RC = 0;	
			else
				// ����������� ������ BinTlv ������ - ��������� TLV-������� 
				RC = _opApiCompileBinTlvArray(p_opContext, pBlockDescr, pDataDescr, Data, DataLen, BINTLV_DATA_ADD);
		}
	}
	else
	{
		if(DataLen == 0 || (pDataDescr->Type == DATA_ASCII && Data[0] == 0))
		{	// ������� TLV-�������
			RC = _opApiCompileBinTlvArray(p_opContext, pBlockDescr, pDataDescr, Data, DataLen, BINTLV_DATA_DELETE);
		}
		else if(DataLen == data_len)
		{
			if(pDataDescr->TagId == 0x9F7F)
			{	// ����������� ������ ��������� ���� ���������
				RC = _opApiCompileBinTlvArray(p_opContext, pBlockDescr, pDataDescr, Data, DataLen, BINTLV_DATA_UPDATE_EX);
			}
			else
			{	// �������� ������� �������� ������ �� ����� 
				IL_WORD offset = pData - p_opContext->BinTlvDescr.pData;
				cmnMemCopy(&p_opContext->BinTlvDescr.pData[offset], Data, DataLen);
				RC = clAppUpdateBinary(p_opContext->phCrd, offset, Data, DataLen);
			}
		}
		else
		{	// ��������� TLV-������� - ��������������� ������ BinTlv ������ 
			RC = _opApiCompileBinTlvArray(p_opContext, pBlockDescr, pDataDescr, Data, DataLen, BINTLV_DATA_UPDATE);
		}
	}

	return RC;
}

#define DATA_BUF_WRITE_MAX_LEN	1024
IL_FUNC IL_WORD opApiWriteCardData(s_opContext *p_opContext, IL_CHAR *in_DataDescr)
{
	IL_WORD RC = 0;
	IL_INT sectorId, blockId, tagId; 
	BLOCK_DESCR *pBlockDescr;
	DATA_DESCR *pDataDescr;
	IL_CHAR *pStr;
	IL_BYTE Data[DATA_BUF_WRITE_MAX_LEN]; 
	IL_DWORD DataLen;
	IL_BYTE RawMode;
	IL_WORD DataType;
	IL_WORD max_len;
	IL_CHAR _str[DATA_BUF_WRITE_MAX_LEN*2+1];
	IL_CHAR *format[] = { "%u-%u-%X", "%u-%u-%u", "%u-%u-%u", "%u-%u-%X" };
	IL_CHAR *pMaxLen; 

	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "������ ��������� ������ �� �����", ".")
	PROT_WRITE_EX0(PROT_OPLIB1, "opApiWriteCardData()")

	// �������� ���������� ����������
	if(!p_opContext || !in_DataDescr) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	PROT_WRITE_EX1(PROT_OPLIB2, "DataDescr=%s", in_DataDescr)

	for(pStr = in_DataDescr; ; )
	{
		// ��������� ������ ������ ������ 
		RawMode = *pStr == 'x';
		if(RawMode)
			pStr++;

		// ������� ��������� ����� ������������� �������� ������� 
		sscanf(pStr, "%u-%u", &sectorId, &blockId);
		PROT_WRITE_EX2(PROT_OPLIB3, "opDescrGetBlock(%u-%u)", ((IL_WORD)sectorId), ((IL_WORD)blockId))
		RC = opDescrGetBlock(p_opContext, (IL_WORD)sectorId, (IL_WORD)blockId, &pBlockDescr);
		PROT_WRITE_EX3(PROT_OPLIB3, "opDescrGetBlock(%u-%u)=%u", ((IL_WORD)sectorId), ((IL_WORD)blockId), RC)
		if(RC)
			break;

		// ��������� ������, ���� � ��� ������������� �� ����� �������� ������
		if(pBlockDescr->FileType > BLOCK_DATA_TYPE_MAX) 
		{
			RC = ILRET_OPLIB_INVALID_FILE_TYPE; break;
		}
		sscanf(pStr, format[pBlockDescr->FileType], &sectorId, &blockId, &tagId);

		// ����������� ������ � ���� ���������� ������
		PROT_WRITE_EX2(PROT_OPLIB3, "flSelectContext(%u-%u)", ((IL_WORD)sectorId), ((IL_WORD)blockId))
		RC = flSelectContext(p_opContext->phCrd, (IL_WORD)sectorId, (IL_WORD)blockId, 0);
		PROT_WRITE_EX3(PROT_OPLIB3, "flSelectContext(%u-%u)=%u", ((IL_WORD)sectorId), ((IL_WORD)blockId), RC)
		if(RC)
			break;

		// �������� ��������� �� ��������� �� ��������� ������������ ����� ������� (���� ����������!)
		pMaxLen = strchr(in_DataDescr, '|');

		// �������� ��������� �� ������ �� ��������� �������� ������
		pStr = strchr(in_DataDescr, '=');

		if(!pStr++ || *pStr == 0) 
		{	// ������ ������ ������������ ������
			DataLen = 0; 
		}
		else 
		{	// ��������� ����������� ����� ������ ������������ ������ 
			DataLen = _get_data_len(pStr);
			cmnMemCopy((IL_BYTE*)_str, (IL_BYTE*)pStr, (IL_WORD)DataLen);
		} 
		_str[DataLen] = 0;

		// ��������� ��� � ����������� ��������� ����� ������
		if(RawMode)
		{	// "�����" ������ ������ DATA_BYTE
			DataType = DATA_BYTE;
			max_len  = (IL_WORD)DataLen;
		}
		else
		{	// �������������� ��������� �� ��������� �������
			IL_INT _tag = pBlockDescr->FileType == BLOCK_DATA_RECORD ? 1 : tagId;
#ifndef PROT_IGNORE
			sprintf(p_opContext->TmpBuf, 
				pBlockDescr->FileType == BLOCK_DATA_TLV || pBlockDescr->FileType == BLOCK_DATA_BINTLV 
				? "%u-%u-%04X" : "%u-%u-%u", 
				(IL_WORD)sectorId, (IL_WORD)blockId, (IL_WORD)_tag); 
#endif//PROT_IGNORE
			// ������� ��������� ������� �� ��� ��������������
			PROT_WRITE_EX1(PROT_OPLIB3, "opDescrGetDataByTagId(%s)", ((IL_CHAR*)p_opContext->TmpBuf))
			RC = opDescrGetDataByTagId(p_opContext, (IL_WORD)sectorId, (IL_WORD)blockId, _tag, &pDataDescr);
			PROT_WRITE_EX2(PROT_OPLIB3, "opDescrGetDataByTagId(%s)=%u", ((IL_CHAR*)p_opContext->TmpBuf), RC)
			if(RC)
				break;
			DataType = pDataDescr->Type;
			max_len  = pDataDescr->MaxLen;
			if(pMaxLen)
			{	// ������������ ����� ����������� �� ������� ������!!!!
				IL_INT MaxLen = max_len;
				sscanf(++pMaxLen, "%u", &MaxLen);
				max_len = (IL_WORD)MaxLen;
			}
		}

		// ����������� ������ ������ �������� �������!!!
		if(!DataLen && DataType != DATA_ASCII) {	
			RC = ILRET_OPLIB_DATA_FOR_WRITE_NOT_FOUND; break;
		}

		// ������������ ������ �������� ������� � ��������� ������
		cmnMemClr(Data, sizeof(Data));
		if(DataType == DATA_NUMERIC)
		{
			if(DataLen/2 > DATA_BUF_WRITE_MAX_LEN || max_len > DATA_BUF_WRITE_MAX_LEN || DataLen/2 > max_len) {
				RC = ILRET_OPLIB_WRITE_DATA_LEN_IS_OVER; break;
			}
			str2bcdF(_str, Data, pDataDescr->MaxLen);
		}
		else if(DataType == DATA_NUMERIC2)
		{
			if(DataLen/2 > DATA_BUF_WRITE_MAX_LEN || max_len > DATA_BUF_WRITE_MAX_LEN || DataLen/2 > max_len) {
				RC = ILRET_OPLIB_WRITE_DATA_LEN_IS_OVER; break;
			}
			str2bcd0(_str, Data, pDataDescr->MaxLen);
		}
		else if(DataType == DATA_ASCII)
		{
			if(DataLen+1 > DATA_BUF_WRITE_MAX_LEN || max_len > DATA_BUF_WRITE_MAX_LEN || DataLen > max_len) {
				RC = ILRET_OPLIB_WRITE_DATA_LEN_IS_OVER; break;
			}
			Ansi_2_Iso8859(_str, (IL_CHAR*)Data);
		}
		else if(DataType == DATA_BYTE)
		{
			if(DataLen/2 > DATA_BUF_WRITE_MAX_LEN || max_len > DATA_BUF_WRITE_MAX_LEN || DataLen/2 > max_len) {
				RC = ILRET_OPLIB_WRITE_DATA_LEN_IS_OVER; break;
			}
			if((RC = hex2bin(_str, Data, &DataLen)) != 0)
				break;
		}
		else 
		{
			RC = ILRET_OPLIB_INVALID_FILE_TYPE;
			break;
		}

		// ���������� ������ �� �����
		DataLen = RawMode ? DataLen : max_len;
		if(pBlockDescr->FileType == BLOCK_DATA_TLV)
		{	// TLV-������
			PROT_WRITE_EX0(PROT_OPLIB3, "clAppPutData")
			RC = clAppPutData(p_opContext->phCrd, (IL_WORD)tagId, Data, (IL_WORD)DataLen);
			PROT_WRITE_EX1(PROT_OPLIB3, "clAppPutData=%u", RC)
			if(RC)
				break;
		}
		else  if(pBlockDescr->FileType == BLOCK_DATA_BIN)
		{	// �������� ������ 
			PROT_WRITE_EX0(PROT_OPLIB3, "clAppUpdateBinary")
			RC = flUpdateBinary(p_opContext->phCrd, (IL_WORD)tagId, (IL_WORD)DataLen, Data); 
			PROT_WRITE_EX1(PROT_OPLIB3, "clAppUpdateBinary=%u", RC)
			if(RC)
				break;
		}
		else if(pBlockDescr->FileType == BLOCK_DATA_RECORD)
		{	// RECORD-������
			if(!tagId)
			{
				PROT_WRITE_EX0(PROT_OPLIB3, "clAppAppendRecord")
				RC = clAppAppendRecord(p_opContext->phCrd, Data, (IL_WORD)DataLen);
				PROT_WRITE_EX1(PROT_OPLIB3, "clAppAppendRecord=%u", RC)
			}
			else
			{
				PROT_WRITE_EX0(PROT_OPLIB3, "clAppUpdateRecord")
				RC = clAppUpdateRecord(p_opContext->phCrd, (IL_BYTE)tagId, Data, (IL_WORD)DataLen);
				PROT_WRITE_EX1(PROT_OPLIB3, "clAppUpdateRecord=%u", RC)
			}
			if(RC)
				break;
		}
		else if(pBlockDescr->FileType == BLOCK_DATA_BINTLV)
		{	// �������� � �������� ��� ������ BinTlv-������ 
			RC = _opCmnCashBinTlvArray(p_opContext, sectorId, blockId);
			if(!RC)
				RC = _opApiUpdateBinTlvData(p_opContext, pBlockDescr, pDataDescr, Data, DataLen);
			if(RC)
				break;
		}
		else
		{
			RC = ILRET_OPLIB_INVALID_WRITE_DATA_TYPE;
			break;
		}

		// ������������� ��������� �� ��������� ��������� ������
		pStr = strchr(pStr, '\n');
		
		// ��������� ������� ���������� 
		if(!pStr++ || *pStr == 0)
			break;
	}

End:
	OP_CMN_DISPLAY_LINE(p_opContext, RC ? "������" : "OK");
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiWriteCardData()=%u", RC)

	return RC;
}

IL_FUNC IL_WORD opApiGetCardSectorsDescr(s_opContext *p_opContext, IL_CHAR *out_SectorsDescr, IL_WORD *out_SectorsDescrLen)
{
	IL_WORD RC = 0;
	IL_WORD sector;
	IL_WORD max_len;
	IL_CHAR str[256];
	SECTOR_DESCR *pSectorDescr;

	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "��������� ���������� �������� ������ �����", ".")
	PROT_WRITE_EX0(PROT_OPLIB1, "opApiGetCardSectorsDescr()")
	
	// �������� ���������� ����������
	if(!p_opContext || !out_SectorsDescr || !out_SectorsDescrLen || *out_SectorsDescrLen == 0) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	// ��������� ������������ ������ ��������� ������
	max_len = *out_SectorsDescrLen - 1;

	// ���������� ������ ���� ������������� � ������ �������� ��������
	if(p_opContext->phCrd->sectors.num_records == 0) {
		RC = ILRET_OPLIB_SECTOR_LIST_IS_EMPTY; goto End;
	}

	// ��������� �������� ������ ��������� ��������
	for(*out_SectorsDescrLen = 0, sector = 0; sector < p_opContext->phCrd->sectors.num_records; sector++)
	{
		PROT_WRITE_EX1(PROT_OPLIB3, "opDescrGetSector(%u)", p_opContext->phCrd->sectors.rec[sector].id)
		RC = opDescrGetSector(p_opContext, p_opContext->phCrd->sectors.rec[sector].id, &pSectorDescr);
		PROT_WRITE_EX2(PROT_OPLIB3, "opDescrGetSector(%u)=%u", p_opContext->phCrd->sectors.rec[sector].id, RC)
		if(RC)
		{
			if(RC == ILRET_OPLIB_SECTOR_DESCR_NOT_FOUND)
			{
				RC = 0;
				continue; // �� ��������� ������, ������ �� �������� � ������ ��������� ��������!!!
			}
			break;
		}

		// ����������� ������-��������� �������� "1|����������������� ������|\n"
		sprintf(str, "%u|%s|\n", p_opContext->phCrd->sectors.rec[sector].id, pSectorDescr->Icon);

		// �������� ��������� ���������� ����� ��������� ������
		if(*out_SectorsDescrLen + strlen(str) > max_len) {
			RC = ILRET_OPLIB_SECTORSDESCR_BUF_IS_OVER; break;
		}
		
		// ��������� ��������� �������� ������� � �������� �����
		sprintf(&out_SectorsDescr[*out_SectorsDescrLen], "%s", str);

		*out_SectorsDescrLen += (IL_WORD)strlen(str);
	}

End:
	OP_CMN_DISPLAY_LINE(p_opContext, RC ? "������" : "OK")
	if(!RC)
		PROT_WRITE_EX1(PROT_OPLIB2, "OUT: SectorsDescr=\n%s", out_SectorsDescr);
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiGetCardSectorsDescr()=%u", RC)

	return RC;
}

IL_FUNC IL_WORD opApiCanCaptureCard(s_opContext *p_opContext, IL_BYTE *CanCapture)
{
	IL_WORD RC = 0;
	IL_DWORD dwLen;
	IL_BYTE TerminalInfo[32];
	IL_BYTE CaptureMode = 0;

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiCanCaptureCard()")

	// �������� ���������� ����������
	if(!p_opContext || !CanCapture) 
		return ILRET_OPLIB_INVALID_ARGUMENT;

	// ��������� ������� ������������ ��� ������� �����
	//---if(CanCapture)
	{	// �������� ���������� � ���������
		PROT_WRITE_EX0(PROT_OPLIB3, "flGetTerminalInfo()")
		RC = flGetTerminalInfo(p_opContext->phCrd, TerminalInfo, &dwLen);
		PROT_WRITE_EX1(PROT_OPLIB3, "flGetTerminalInfo()=%u", RC)
		if(RC != 0)
			goto End;
		// ��������� ���������� ����������� �� ������� �����
		if(p_opContext->phCrd->AppVer != UECLIB_APP_VER_10)
			CaptureMode = TerminalInfo[13] & 0x01;
		else
			CaptureMode = TerminalInfo[9] & 0x01;
	}

/*---
	// ��� ���������� ������������ ��� ������� ����� ��� �������������� ������������ ����������� �
	if(!CanCapture || !CaptureMode)
	{
		OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "���������� �����", ".")
		RC = opApiLockCard(p_opContext);
		OP_CMN_DISPLAY_LINE(p_opContext, RC ? "������" : "OK")
	}
---*/

End:
	// ��������� ������� ������� ������������ ��� ������� �����
	//---if(CanCapture)
		*CanCapture = CaptureMode;
	
	if(!RC)
		PROT_WRITE_EX1(PROT_OPLIB2, "OUT: CanCapture=%u", *CanCapture);
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiCanCaptureCard()=%u", RC)

	return RC;
}

IL_FUNC IL_WORD opApiReadPhoto(s_opContext *p_opContext, IL_BYTE *PhotoBuf, IL_WORD *PhotoBufLen)
{
	IL_WORD RC;
	IL_WORD max_len;

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiReadPhoto()");

	// �������� ���������� ����������
	if(!p_opContext) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	// ����������� ���� ���������� ���������� (������=1, ����=3)
	PROT_WRITE_EX0(PROT_OPLIB3, "flSelectContext()")
	RC = flSelectContext(p_opContext->phCrd, 1, 3, 0);
	PROT_WRITE_EX1(PROT_OPLIB3, "flSelectContext()=%u", RC)
	if(RC != 0)
		goto End;

	// ���� ������ �������� ����� ��� �������� ���������� � ����� ������ - ������������� �� � ��������
	if(PhotoBuf && PhotoBufLen && *PhotoBufLen > 0)
		opCtxSetPhotoBuf(p_opContext, PhotoBuf, PhotoBufLen);

	// �������� ���������� ���������� ����������
	if(!p_opContext->pPhotoBuf || !p_opContext->pPhotoLen || *p_opContext->pPhotoLen == 0)
		return ILRET_OPLIB_INVALID_ARGUMENT;

	// �������������� ����������� ����� ��������� ������ ����������
	max_len = *p_opContext->pPhotoLen;
	*p_opContext->pPhotoLen = 0;

	if(p_opContext->phCrd->AppVer == UECLIB_APP_VER_10)
	{	// ��� ���� ������ 1.0 ���������� �������� ��� ������� �������� ������
		PROT_WRITE_EX0(PROT_OPLIB3, "flReadBinaryEx()")
		RC = flReadBinaryEx(p_opContext->phCrd, 0, p_opContext->pPhotoBuf, max_len, p_opContext->pPhotoLen);
		PROT_WRITE_EX1(PROT_OPLIB3, "flReadBinaryEx()=%u", RC)
	}
	else
	{	// ��� ���� ������ 1.1 � ���� ���������� �������� ��� TLV-�������� ������
		IL_BYTE header[10];
		IL_BYTE *pData;
		IL_DWORD data_len;
		IL_WORD offset;

        // ������� ��������� TLV-�����
		PROT_WRITE_EX0(PROT_OPLIB3, "clAppReadBinary()")
		RC = clAppReadBinary(p_opContext->phCrd, 0, 10, header);
		PROT_WRITE_EX1(PROT_OPLIB3, "clAppReadBinary()=%u", RC)
		if(RC != 0)
            goto End;

		// �������������� ��������� �� ������ ���������� � ������� �� ����������� �����
		TagFind(header, 10, IL_TAG_5F40, &data_len, &pData, 0);
		if(pData == NULL) {
			RC = ILRET_OPLIB_CORRUPTED_TLV_DATA; goto End;
		}

		// �������� ������ ��������� ������ ��� ����������
		if(data_len > max_len) {
			RC = ILRET_OPLIB_BINTLV_BUF_IS_OVER; goto End;
		}

		// c�������� ���������� � �������� ����� 
		offset = (IL_WORD)(GetTagLen(header) + GetLenLen(header));
		*p_opContext->pPhotoLen = (IL_WORD)data_len;
		PROT_WRITE_EX0(PROT_OPLIB3, "flReadBinary()")
		RC = flReadBinary(p_opContext->phCrd, offset, *p_opContext->pPhotoLen, p_opContext->pPhotoBuf);
		PROT_WRITE_EX1(PROT_OPLIB3, "flReadBinary()=%u", RC)
 	}

End:
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiReadPhoto()=%u", RC)
	return RC;
}

IL_FUNC void opApiGetVersion(IL_CHAR *LibVer, IL_CHAR *AppVer)
{
	PROT_WRITE_EX0(PROT_OPLIB1, "opApiGetVersion()");
#ifndef SM_SUPPORT
	if(LibVer)
		sprintf(LibVer, "%s", UECLIB_VER);
	if(AppVer)
		sprintf(AppVer, "%u.%u", UECLIB_APP_VER >> 4, UECLIB_APP_VER & 0x0F);
#else
	if(LibVer)
		sprintf(LibVer, "%s SE", UECLIB_VER);
	if(AppVer)
		sprintf(AppVer, "%u.%u SE", UECLIB_APP_VER >> 4, UECLIB_APP_VER & 0x0F);
#endif//SM_SUPPORT
	PROT_WRITE_EX2(PROT_OPLIB1, "OUT: LibVer=%s AppVer=%s", LibVer ? LibVer : "", AppVer ? AppVer : "")
}

IL_FUNC IL_WORD opApiWriteSectorExDescr(IL_WORD SectorId, IL_BYTE SectorVer, IL_CHAR *SectorExDescr)
{
	IL_WORD RC;
	PROT_WRITE_EX0(PROT_OPLIB1, "opApiWriteSectorExDescr()")
	PROT_WRITE_EX3(PROT_OPLIB2, "IN: SectorId=%u SectorVer=%02X SectorExDescr=%s", SectorId, SectorVer, SectorExDescr)
	PROT_WRITE_EX0(PROT_OPLIB3, "prmWriteSectorExDescr()")
	RC = prmWriteSectorExDescr(SectorId, SectorVer, SectorExDescr);
	PROT_WRITE_EX1(PROT_OPLIB3, "prmWriteSectorExDescr()=%u", RC)
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiWriteSectorExDescr()=%u", RC)

	return RC;
}

//
typedef struct 
{
	IL_WORD RC;
	IL_CHAR *Text;
} UECERROR_DESCR;

#ifndef ENGLISH
static UECERROR_DESCR ErrDescr[] =
{
	{ 0,									"�������" },
	{ ILRET_SYS_ERROR,						"��������� ������" },
	{ ILRET_SYS_MEM_ALLOC_ERROR,			"������ ������������� ��������� ������" },
	{ ILRET_SYS_INVALID_ARGUMENT,			"�������� �������� ��� ������ �������" },
	
	{ ILRET_SCR_ERROR,						"������ ����-������" },
	{ ILRET_SCR_UNPOWERED_CARD,				"����������� ������� �� �����" },
	{ ILRET_SCR_REMOVED_CARD,				"����� ����������� � ����-������" },
	{ ILRET_SCR_RESET_CARD,					"��������� reset �����" },
	{ ILRET_SCR_UNRESPONSIVE_CARD,			"����� �� �������� �� reset" },
	{ ILRET_SCR_PROTOCOL_ERROR,				"������ ��������� �����" },
	{ ILRET_SCR_SHARING_VIOLATION,			"������ ������������ ������� � ����-������" },
	{ ILRET_SCR_UNKNOWN_READER,				"����������� ����-�����" },
	{ ILRET_SCR_NOT_READY,					"����-����� �� �����" },
	{ ILRET_SCR_PROTO_MISMATCH,				"������ �������� APDU ������ �� ��������������" },
	{ ILRET_SCR_UNSUPPORTED_CARD,			"����-����� �� ������������ ������������ � ������" },
	{ ILRET_SCR_INVALID_ATR,				"�������� ATR �����" },
	{ ILRET_SCR_INVALID_HANDLE,				"������������ ������������� ���������� ����-������" },
	{ ILRET_SCR_INVALID_DEVICE,				"�������� �����" },
	{ ILRET_SCR_TIMEOUT,					"��������� ����� �������� ������ ����-������" },
	{ ILRET_SCR_READER_UNAVAILABLE,			"����-����� �� ��������" },
	
	{ ILRET_CRD_ERROR,						"������ �����" },
	{ ILRET_CRD_LENGTH_NOT_MATCH,			"�������� ����� ������ �����" },
	{ ILRET_CRD_APDU_TAG_NOT_FOUND,			"�� ������ ��� � ������������ � ������ ����� ������" },
	{ ILRET_CRD_APDU_TAG_LEN_ERROR,			"�������� ����� ���� ������������ � ������ ����� ������" },
	{ ILRET_CRD_APDU_DATA_FORMAT_ERROR,		"�������� ������ ������ APDU-�������" },
	
	{ ILRET_CRD_VERIFY_ERROR,				"������ ����������� ����������" },
	{ ILRET_CRD_VERIFY_WRONG_LENGTH,		"������������ ����� �������� ������ ����������� ����������" },
	{ ILRET_CRD_VERIFY_WRONG_PARAMETERS,	"������������ �������� ���������� P1/P2 ������� ����������� ����������" },
	{ ILRET_CRD_VERIFY_PASSWORD_BLOCKED,	"������ ������������" },
	{ ILRET_CRD_VERIFY_PASSWORD_NOT_FOUND,  "��������� � ������� ����������� ���������� ���� �� ������" },
	{ ILRET_CRD_VERIFY_WRONG_PASSWORD_PRESENTED,   "�������� �������� ������" },
	
	{ ILRET_CRD_SELECT_ERROR,				"������ �������������� ����������/�����" },
	{ ILRET_CRD_SELECT_WRONG_LENGTH,		"������������ ����� ������� ������ ������� ��������������" },
	{ ILRET_CRD_SELECT_WRONG_PARAMETERS,	"������������ �������� ���������� P1/P2 ������� ��������������" },
	{ ILRET_CRD_SELECT_OBJECT_BLOCKED,		"��������� ������ ����������" },
	{ ILRET_CRD_SELECT_WRONG_CMD_DATA,		"������� ������ ������� �������������� ����� ������������ ��������" },
	{ ILRET_CRD_SELECT_FILE_NOT_FOUND,		"������������� ������ �� ������" },
	{ ILRET_CRD_SELECT_RESPONSE_ABSENT,	    "����������� ���������� � ��������������� �������" },
	{ ILRET_CRD_SELECT_SECTOR_NOT_FOUND,	"������ � ��������� ��������������� ����������� � ������ ��������" },
	{ ILRET_CRD_SELECT_BLOCK_NOT_FOUND,		"���� � ��������� ��������������� ����������� � ������ ������ �������" },
	
	{ ILRET_CRD_INTAUTH_ERROR,				"������ �������������� ��-����������" },
	{ ILRET_CRD_INTAUTH_WRONG_LENGTH,		"������������ ����� ������� ������ ������� �������������� ��-����������" },
	{ ILRET_CRD_INTAUTH_WRONG_PARAMETERS,   "������������ �������� ���������� P1/P2 ������� �������������� ��-����������" },
	{ ILRET_CRD_INTAUTH_WRONG_CMD_DATA,		"������� ������ ������� �������������� ��-���������� ����� ������������ ��������" },
	{ ILRET_CRD_INTAUTH_KEY_NOT_FOUND,		"��������� � ������� �������������� ��-���������� ���� �� ������" },
	
	{ ILRET_CRD_MUTAUTH_ERROR,				"������ �������������� �������� ��������" },
	{ ILRET_CRD_MUTAUTH_WRONG_LENGTH,		"������������ ����� ������� ������ ������� �������������� �������� ��������" },
	{ ILRET_CRD_MUTAUTH_WRONG_PARAMETERS,   "������������ �������� ���������� P1/P2 ������� �������������� �������� ��������" },
	{ ILRET_CRD_MUTAUTH_WRONG_CRYPTO,		"�������� �������� ������������ �������������� �������� ��������" },
	{ ILRET_CRD_MUTAUTH_KEY_NOT_FOUND,		"��������� � ������� �������������� �������� �������� ���� �� ������" },
	{ ILRET_CRD_MUTAUTH_CONDITIONS,			"���������� �� ������������ � ��������� ������� �������������� �������� ��������" },
	
	{ ILRET_CRD_GETCHAL_ERROR,				"������ ��������� ���������� ����� �����" },
	{ ILRET_CRD_GETCHAL_WRONG_LENGTH,		"������������ ����� ������� ������ ������� ��������� ���������� ����� �����" },
	{ ILRET_CRD_GETCHAL_WRONG_PARAMETERS,   "������������ �������� ���������� P1/P2 ������� ��������� ���������� ����� �����" },
	
	{ ILRET_CRD_CHDATA_ERROR,				"������ ���������/��������� �������� ����������� ������" },
	{ ILRET_CRD_CHDATA_WRONG_LENGTH,		"������������ ����� ������� ������ ������� ���������/��������� �������� ����������� ������" },
	{ ILRET_CRD_CHDATA_WRONG_CRYPTO,		"�� ��������� ������� ������������ ��� ������� ���������/��������� �������� ����������� ������" },
	{ ILRET_CRD_CHDATA_WRONG_DATA_STRUCT,   "�������� ��������� ������ ������� ���������/��������� �������� ����������� ������" },
	{ ILRET_CRD_CHDATA_WRONG_SM_TAG,		"�������� �������� ������������ ������� ���������/��������� �������� ����������� ������" },
	{ ILRET_CRD_CHDATA_WRONG_PARAMETERS,    "������������ �������� ���������� P1/P2 ������� ���������/��������� �������� ����������� ������" },
	{ ILRET_CRD_CHDATA_KEY_NOT_FOUND,		"��������� � ������� ���������/��������� �������� ����������� ������ ���� �� ������" },
	{ ILRET_CRD_CHDATA_DATA_LEN_TOO_SHORT,	"����� ���������������� ������ ������ ����������" },
	
	{ ILRET_CRD_RSTCNTR_ERROR,				"������ ������������� ������" },
	{ ILRET_CRD_RSTCNTR_WRONG_LENGTH,		"������������ ����� ������� ������ ������� ������������� ������" },
	{ ILRET_CRD_RSTCNTR_WRONG_DATA_STRUCT,  "�������� ��������� ������ ������� ������������� ������" },
	{ ILRET_CRD_RSTCNTR_WRONG_SM_TAG,		"�������� �������� ������ �� ����� SM ������� ������������� ������" },
	{ ILRET_CRD_RSTCNTR_WRONG_PARAMETERS,   "������������ �������� ���������� P1/P2 ������� ������������� ������" },
	{ ILRET_CRD_RSTCNTR_KEY_NOT_FOUND,		"��������� � ������� ������������� ������ ���� �� ������" },
	
	{ ILRET_CRD_PERFSECOP_ERROR,			"������ ���������� ����������������� ��������" },
	{ ILRET_CRD_PERFSECOP_WRONG_LENGTH,		"������������ ����� ������� ������ ������� ���������� ����������������� ��������" },
	{ ILRET_CRD_PERFSECOP_BINDING_CMD_MISSED, "������ �������� ��������� ��������� ������� ���������� ����������������� ��������" },
	{ ILRET_CRD_PERFSECOP_BINDING_NOT_SUPPORTED, "������� ����������������� �������� �� ������������ ����������" },
	{ ILRET_CRD_PERFSECOP_WRONG_CERT,		"�������� ���������� ��������� ����� ������� ���������� ����������������� ��������" },
	{ ILRET_CRD_PERFSECOP_WRONG_DATA_STRUCT, "�������� ��������� ������ ������� ���������� ����������������� ��������" },
	{ ILRET_CRD_PERFSECOP_WRONG_PARAMETERS, "������������ �������� ���������� P1/P2 ������� ���������� ����������������� ��������" },
	
	{ ILRET_CRD_READBIN_ERROR,				"������ ������ ������ �� ��������� �����" },
	{ ILRET_CRD_READBIN_WRONG_LENGTH,		"������������ ����� ������� ������ ������� ������ ��������� �����" },
	{ ILRET_CRD_READBIN_WRONG_FILE_TYPE,    "������� ������ ������ �� �� ��������� �����" },
	{ ILRET_CRD_READBIN_WRONG_SEC_CONDITIONS, "�� ��������� ������� ������� ��� ������ ������ �� ��������� �����" },
	{ ILRET_CRD_READBIN_EF_NOT_SELECTED,    "������� ������ �������� ������ �� ������������������ �����" },
	{ ILRET_CRD_READBIN_WRONG_PARAMETERS,   "������������ �������� ���������� P1/P2 ������� ������ ������ �� ��������� �����" },
	{ ILRET_CRD_READBIN_WRONG_OFFSET,		"��������� ��� ������ ������ �� ��������� ����� �������� ��������� ������ �����" },
	
	{ ILRET_CRD_UPDBIN_ERROR,				"������ ������ ������ � �������� ����" },
	{ ILRET_CRD_UPDBIN_WRONG_LENGTH,		"������������ ����� ������� ������ ������� ������ � �������� ����" },
	{ ILRET_CRD_UPDBIN_WRONG_SEC_CONDITIONS, "�� ��������� ������� ������� ��� ������ ������ � �������� ����" },
	{ ILRET_CRD_UPDBIN_WRONG_FILE,			"������� ������ �������� ������ � ����������������� ����" },
	{ ILRET_CRD_UPDBIN_WRONG_PARAMETERS,    "������������ �������� ���������� P1/P2 ������� ������ ������ � �������� ����" },
	{ ILRET_CRD_UPDBIN_WRONG_OFFSET,		"�������� �������� ��� ������� ������ ������ � �������� ����" },
	
	{ ILRET_CRD_GETDATA_ERROR,				"������ ������ �������� ������ �� TLV-�����" },
	{ ILRET_CRD_GETDATA_WRONG_LENGTH,		"������������ ����� �������/�������� ������ ��� ������ �� TLV-�����" },
	{ ILRET_CRD_GETDATA_WRONG_PARAMETERS,   "������������ �������� ���������� P1/P2 ������� ������ �������� ������ �� TLV-�����" },
	{ ILRET_CRD_GETDATA_TAG_NOT_FOUND,      "�� ������ ������� ������ � ��������� ����� ��� ������ �� TLV-�����" },
	{ ILRET_CRD_GETDATA_WRONG_SEC_CONDITIONS, "�� ��������� ������� ������� ��� ������ �������� ������ �� TLV-�����" },
	
	{ ILRET_CRD_PUTDATA_ERROR,				"������ ��������� �������� ������ � TLV-����" },
	{ ILRET_CRD_PUTDATA_WRONG_LENGTH,       "������������ ����� ������� ������ ������� ��������� �������� ������ � TLV-����" },
	{ ILRET_CRD_PUTDATA_WRONG_SEC_CONDITIONS, "�� ��������� ������� ������� ��� ��������� �������� ������ � TLV-����" },
	{ ILRET_CRD_PUTDATA_WRONG_PARAMETERS,   "������������ �������� ���������� P1/P2 ������� ��������� �������� ������ � TLV-����" },
	{ ILRET_CRD_PUTDATA_TAG_NOT_FOUND,		"����������� ��� ��� ��������� �������� ������ � TLV-����" },

	{ ILRET_CRD_READREC_ERROR,				"������ ������ ������ �� ����� ������� �������� ���������" },
	{ ILRET_CRD_READREC_FILE_BLOCKED,		"���� ������� �������� ��������� ����������" },
	{ ILRET_CRD_READREC_WRONG_LENGTH,		"����������� ����� �������� ������ ����� ������� �������� ���������" },
	{ ILRET_CRD_READREC_WRONG_FILE_TYPE,	"���� �� �������� ������ ������� �������� ���������" },
	{ ILRET_CRD_READREC_WRONG_SEC_CONDITIONS, "�� ��������� ������� ������� � ����� ������� �������� ���������" },
	{ ILRET_CRD_READREC_WRONG_PARAMETERS,	"������������ �������� ���������� ������� ������ ����� ������� �������� ���������" },
	{ ILRET_CRD_READREC_RECORD_NOT_FOUND,	"�� ������� ������ ��� ������ ����� ������� �������� ���������" },
	{ ILRET_CRD_APPREC_WRONG_PARAMETERS_P1P2, "������������ �������� ���������� P1/P2 ��� ������ ����� ������� �������� ���������" },
	
	{ ILRET_CRD_UPDREC_ERROR,				"������ ���������� ������ ����� ������� �������� ���������" },
	{ ILRET_CRD_UPDREC_FILE_BLOCKED,		"���� ������� �������� ��������� ����������" },
	{ ILRET_CRD_UPDREC_WRONG_LENGTH,		"����������� ����� ������ ��� ���������� ����� ������� �������� ���������" },
	{ ILRET_CRD_UPDREC_WRONG_FILE_TYPE,		"���� �� �������� ������ ������� �������� ���������" },
	{ ILRET_CRD_UPDREC_WRONG_SEC_CONDITIONS, "�� ��������� ������� ������� � ����� ������� �������� ���������" },
	{ ILRET_CRD_UPDREC_WRONG_PARAMETERS,	"������������ �������� ���������� ������� ���������� ����� ������� �������� ���������" },
	{ ILRET_CRD_UPDREC_RECORD_NOT_FOUND,	"�� ������� ������ ��� ���������� ����� ������� �������� ���������" },
	{ ILRET_CRD_UPDREC_WRONG_PARAMETERS_P1P2, "������������ �������� ���������� P1/P2 ��� ���������� ����� ������� �������� ���������" },
	
	{ ILRET_CRD_APPREC_ERROR,				"������ ���������� ������ � ���� ������� �������� ���������" },
	{ ILRET_CRD_APPREC_FILE_BLOCKED,		"���� ������� �������� ��������� ����������" },
	{ ILRET_CRD_APPREC_WRONG_LENGTH,		"����������� ����� ������ ��� ���������� � ����� ������� �������� ���������" },
	{ ILRET_CRD_APPREC_WRONG_FILE_TYPE,		"���� �� �������� ������ ������� �������� ���������" },
	{ ILRET_CRD_APPREC_WRONG_SEC_CONDITIONS, "�� ��������� ������� ������� � ����� ������� �������� ���������" },
	{ ILRET_CRD_APPREC_WRONG_PARAMETERS,	"������������ �������� ���������� ������� ���������� � ���� ������� �������� ���������" },
	{ ILRET_CRD_APPREC_NOT_ENOUGH_MEMORY,	"������������ ����� � ����� ������� �������� ��������� ��� ����������" },
	{ ILRET_CRD_APPREC_WRONG_PARAMETERS_P1P2, "������������ �������� ���������� P1/P2 ��� ���������� � ����� ������� �������� ���������" },

	{ ILRET_PARAM_ERROR,					"������ ��������� ��������� �� �������� ����� ��������" },
	{ ILRET_PARAM_NOT_FOUND,				"�� ������ ������������� �������� �������� ����� ��������" },
	{ ILRET_PARAM_WRONG_FORMAT,				"�������� ������ ��������� �������� ����� ��������" },
	{ ILRET_PARAM_WRONG_LENGTH,				"�������� ����� ���������� �� �������� ����� �������� ���������" },
	{ ILRET_PARAM_FORMAT_UNKNOWN,			"����������� ������ ��������� �������� ����� ��������" },
	{ ILRET_PARAM_DESCR_NOT_FOUND,			"�� ������ ��������� ��������� �������� ����� ��������" },
	{ ILRET_PARAM_WRITE_SECTOR_EX_ERROR,	"������ ������ ������ �������� ��������� �������" },
	{ ILRET_PARAM_SECTOR_EX_WRONG_FORMAT,	"�������� ������ ������ �������� ��������� �������" },
	{ ILRET_PARAM_CERTIFICATE_NOT_FOUND,	"�� ������ ���������� �� ������� ����� ��������" },
	
	{ ILRET_PROT_ERROR,						"������ ����������������" },
	{ ILRET_PROT_LOGFILE_OPEN_ERROR,		"������ �������� ����� ����������������" },
	{ ILRET_PROT_LOGEILE_WRITE_ERROR,		"������ ������ � ���� ����������������" },
	
	{ ILRET_DATA_ERROR,						"������ ������� ������ ���������� �������" },
	{ ILRET_DATA_TAG_NOT_FOUND,				"�� ������ ������� ������ � ��������� �����" },
	{ ILRET_DATA_TAG_WRONG_FORMAT,			"�������� ������ �������� ������" },
	{ ILRET_DATA_TAG_WRONG_LENGTH,			"�������� ����� �������� ������" },
	{ ILRET_DATA_CARD_FORMAT_ERROR,			"�������� ������ ���������� � ����� �������� TLV-������" },
	
	{ ILRET_APP_VER_NOT_SUPPORTED,			"�������� �� ������������ ������ � ������ ������ ������" },
	{ ILRET_NO_CRYPTOALG_SUPPORTED,			"�������� �� ������������ ��������������� ������ �����" },
	{ ILRET_APP_NOT_ACTIVE_YET,				"���� ������ �������� ����� �� ��������" },
	{ ILRET_APP_EXPIRED,					"���� �������� ����� ����" },
	{ ILRET_INVALID_HEX_STRING_FORMAT,		"�������� ������ Hex-������" },
	{ ILRET_APDU_RES_NOT_ALLOWED,			"������������ ����� APDU-������� ������" },
	{ ILRET_APDU_ALLOWED_RES_IS_OVER,		"��������� ���������� ���������� ������� APDU-������� ������" },
	{ ILRET_APP_INCONSISTENT_STATE,			"���������� ��������� � ��������������� ���������" },
	{ ILRET_APP_UNKNOWN_STATE,				"���������� ��������� � ����������� ���������" },
	{ ILRET_BUFFER_TOO_SMALL,				"������������� ������ ������" },

	{ ILRET_CERT_ERROR,						"������ �������� ������� �����������" },
	{ ILRET_CERT_MISSING_ELEMENT,			"����������� ������������ ������� ������ �����������" },
	{ ILRET_CERT_WRONG_LENGTH,				"�������� ����� ������ �������� �����������" },
	{ ILRET_CERT_NOT_ACTIVE_YET,			"���� ������ �������� ����������� �� ��������" },
	{ ILRET_CERT_EXPIRED,					"���� �������� ����������� ����" },
	{ ILRET_CERT_WRONG_VERSION,				"�������� ������ �����������" },
	{ ILRET_CERT_WRONG_RSA_EXP,				"�������� ���������� ��������� ����� � �����������" },
	{ ILRET_CERT_INVALID_TYPE,				"�������� ��� ����������� ��������� �����" },
	{ ILRET_CERT_TERMINFO_NOT_MATCH,		"�� ��������� �������� � ��������� � ������������ GOST � RSA" },

	{ ILRET_CRYPTO_ERROR,					"������ ����������������" },
	{ ILRET_CRYPTO_RSA_FORMAT,				"������ ������� RSA" },
	{ ILRET_CRYPTO_CRYPTO_PREPARE_SESSION,  "������ ��� ���������� ������������" },
	{ ILRET_CRYPTO_WRONG_SM_MAC,			"�������� �������� ����� MAC ������������" },
	{ ILRET_CRYPTO_WRONG_DATA_LENGTH,		"�������� ����� ����������������� ������" },
	{ ILRET_CRYPTO_WRONG_DATA_FORMAT,		"�������� ������ ����������������� ������" },
	{ ILRET_CRYPTO_WRONG_CERT,				"�������� ����������" },
	{ ILRET_CRYPTO_WRONG_MAC,				"�������� �������� ����� MAC ��� �������� ������� �� �������������� ��-����������" },
	{ ILRET_CRYPTO_WRONG_CHK_ISS_SESS_MAC,  "�������� �������� ����� MAC ��� �������� ������������� ���������� ������ � ���������" },
	{ ILRET_CRYPTO_ERROR_GENKEYPAIR,		"������ ��������� �������� ����" },
	{ ILRET_CRYPTO_ERROR_FILLPARAM,			"������ �������� ����������� �������� ���������" },
	{ ILRET_CRYPTO_ERROR_SIGN,				"������ ������������ �������" },
	{ ILRET_CRYPTO_ERROR_CHECKSIGN,			"������ �������� �������" },
	{ ILRET_CRYPTO_ERROR_KEYMATCHING,		"������������ ���������� ������" },

#ifdef SM_SUPPORT
	{ ILRET_SM_SELECT_ERROR,				"��: ������ �������������� ����������/�����" },
	{ ILRET_SM_SELECT_WRONG_LENGTH,			"��: ������������ ����� ������� ������ ������� ��������������" },
	{ ILRET_SM_SELECT_WRONG_PARAMETERS,		"��: ������������ �������� ���������� P1/P2 ������� ��������������" },
	{ ILRET_SM_SELECT_FILE_NOT_FOUND,		"��: ������������� ������ �� ������" },

	{ ILRET_SM_MUTAUTH_ERROR ,				"��: ������ �������������� �������� ��������" },
	{ ILRET_SM_MUTAUTH_WRONG_LENGTH ,		"��: ������������ ����� ������� ������ ������� �������������� �������� ��������" },
	{ ILRET_SM_MUTAUTH_WRONG_PARAMETERS,	"��: ������������ �������� ���������� P1/P2 ������� �������������� �������� ��������" },
	{ ILRET_SM_MUTAUTH_WRONG_CRYPTO,		"��: �������� �������� ������������ �������������� �������� ��������" },
	{ ILRET_SM_MUTAUTH_KEY_NOT_FOUND,		"��: ��������� � ������� �������������� �������� �������� ���� �� ������" },
	{ ILRET_SM_MUTAUTH_CONDITIONS,			"��: ���������� �� ������������ � ��������� ������� �������������� �������� ��������" },

	{ ILRET_SM_NOT_ACTIVATED,				"��: ������ ������������ �� �����������" },

	{ ILRET_SM_GETCHAL_ERROR,				"��: ������ ��������� ���������� ����� �����" },
	{ ILRET_SM_GETCHAL_WRONG_LENGTH,		"��: ������������ ����� ������� ������ ������� ��������� ���������� ����� �����" },
	{ ILRET_SM_GETCHAL_WRONG_PARAMETERS,	"��: ������������ �������� ���������� P1/P2 ������� ��������� ���������� ����� �����" },
	
	{ ILRET_SM_CHDATA_ERROR,				"��: ������ ���������/��������� �������� ����������� ������" },
	{ ILRET_SM_CHDATA_WRONG_LENGTH,			"��: ������������ ����� ������� ������ ������� ���������/��������� �������� ����������� ������" },
	{ ILRET_SM_CHDATA_WRONG_CRYPTO,			"��: �� ��������� ������� ������������ ��� ������� ���������/��������� �������� ����������� ������" },
	{ ILRET_SM_CHDATA_WRONG_PARAMETERS,		"��: ������������ �������� ���������� P1/P2 ������� ���������/��������� �������� ����������� ������" },
	{ ILRET_SM_CHDATA_KEY_NOT_FOUND,		"��: ��������� � ������� ���������/��������� �������� ����������� ������ ���� �� ������" },
	{ ILRET_SM_CHDATA_KEY_BLOCKED,			"��: ���� ����������" },
	{ ILRET_SM_CHDATA_OP_NOT_COMPATIBLE_KEY_STATE, "��: �������� �� ���������� � �������� ���������� �����" },
	{ ILRET_SM_CHDATA_LENGTH_NOT_COMPATIBLE_MODE, "��: ����� ������ ������� �� ���������� � ������� �������" },
	{ ILRET_SM_CHDATA_WRONG_FORMAT,			"��: �������� ������ ������ �������" },
	{ ILRET_SM_CHDATA_WRONG_PIN_LENGTH,		"��: ����� ���������������� ������ ������ ���������� ����������� ��������" },

	{ ILRET_SM_VERIFY_ERROR,				"��: ������ ����������� ���������" },
	{ ILRET_SM_VERIFY_WRONG_LENGTH,			"��: ������������ ����� �������� ������ ����������� ���������" },
	{ ILRET_SM_VERIFY_WRONG_PARAMETERS,		"��: ������������ �������� ���������� P1/P2 ������� ����������� ���������" },
	{ ILRET_SM_VERIFY_PASSWORD_BLOCKED,		"��: ������ ������������" },
	{ ILRET_SM_VERIFY_PASSWORD_NOT_FOUND,	"��: ��������� � ������� ����������� ��������� ���� �� ������" },
	{ ILRET_SM_VERIFY_WRONG_PASSWORD_PRESENTED, "��: �������� �������� ������" },
	{ ILRET_SM_VERIFY_SECURITY_STATUS_NOT_SATISFIED, "��: �� ��������� ������� ������������ ��� ����������� ���������" },

	{ ILRET_SM_PERFSECOP_ERROR,				"��: ������ ���������� ����������������� ��������" },
	{ ILRET_SM_PERFSECOP_WRONG_LENGTH,		"��: ������������ ����� ������� ������ ������� ���������� ����������������� ��������" },
	{ ILRET_SM_PERFSECOP_BINDING_CMD_MISSED, "��: ������ �������� ��������� ��������� ������� ���������� ����������������� ��������" },
	{ ILRET_SM_PERFSECOP_BINDING_NOT_SUPPORTED, "��: ������� ����������������� �������� �� ������������ ����������" },
	{ ILRET_SM_PERFSECOP_WRONG_CERT,		"��: �������� ���������� ��������� ����� ������� ���������� ����������������� ��������" },
	{ ILRET_SM_PERFSECOP_WRONG_DATA_STRUCT, "��: �������� ��������� ������ ������� ���������� ����������������� ��������" },
	{ ILRET_SM_PERFSECOP_WRONG_PARAMETERS,	"��: ������������ �������� ���������� P1/P2 ������� ���������� ����������������� ��������" },
	{ ILRET_SM_PERFSECOP_SIGN_KEY_ABSENT,	"��: ���� ����������� ������� �����������" },

	{ ILRET_SM_READBIN_ERROR,				"��: ������ ������ ������ �� ��������� �����" },
	{ ILRET_SM_READBIN_WRONG_LENGTH,		"��: ������������ ����� ������� ������ ������� ������ ��������� �����" },
	{ ILRET_SM_READBIN_WRONG_FILE_TYPE,		"��: ������� ������ ������ �� �� ��������� �����" },
	{ ILRET_SM_READBIN_WRONG_SEC_CONDITIONS, "��: �� ��������� ������� ������� ��� ������ ������ �� ��������� �����" },
	{ ILRET_SM_READBIN_EF_NOT_SELECTED,		"��: ������� ������ �������� ������ �� ������������������ �����" },
	{ ILRET_SM_READBIN_WRONG_PARAMETERS,	"��: ������������ �������� ���������� P1/P2 ������� ������ ������ �� ��������� �����" },
	{ ILRET_SM_READBIN_WRONG_OFFSET,		"��: ��������� ��� ������ ������ �� ��������� ����� �������� ��������� ������ �����" },

	{ ILRET_SM_UPDBIN_ERROR,				"��: ������ ������ ������ � �������� ����" },
	{ ILRET_SM_UPDBIN_WRONG_LENGTH,			"��: ������������ ����� ������� ������ ������� ������ � �������� ����" },
	{ ILRET_SM_UPDBIN_WRONG_SEC_CONDITIONS, "��: �� ��������� ������� ������� ��� ������ ������ � �������� ����" },
	{ ILRET_SM_UPDBIN_WRONG_FILE,			"��: ������� ������ �������� ������ � ����������������� ����" },
	{ ILRET_SM_UPDBIN_WRONG_FILE_TYPE,		"��: ������� ������ �� � �������� ����" },
	{ ILRET_SM_UPDBIN_WRONG_PARAMETERS,		"��: ������������ �������� ���������� P1/P2 ������� ������ ������ � �������� ����" },

	{ ILRET_SM_GETDATA_ERROR,				"��: ������ ������ �������� ������ �� TLV-�����" },
	{ ILRET_SM_GETDATA_WRONG_LENGTH,		"��: ������������ ����� �������/�������� ������ ��� ������ �� TLV-�����" },
	{ ILRET_SM_GETDATA_WRONG_PARAMETERS,	"��: ������������ �������� ���������� P1/P2 ������� ������ �������� ������ �� TLV-�����" },
	{ ILRET_SM_GETDATA_TAG_NOT_FOUND,		"��: �� ������ ������� ������ � ��������� ����� ��� ������ �� TLV-�����" },
	{ ILRET_SM_GETDATA_WRONG_SEC_CONDITIONS,"��: �� ��������� ������� ������� ��� ������ �������� ������ �� TLV-�����" },

	{ ILRET_SM_PUTDATA_ERROR,				"��: ������ ��������� �������� ������ � TLV-����" },
	{ ILRET_SM_PUTDATA_WRONG_LENGTH,		"��: ������������ ����� ������� ������ ������� ��������� �������� ������ � TLV-����" },
	{ ILRET_SM_PUTDATA_WRONG_SEC_CONDITIONS,"��: �� ��������� ������� ������� ��� ��������� �������� ������ � TLV-����" },
	{ ILRET_SM_PUTDATA_WRONG_PARAMETERS,	"��: ������������ �������� ���������� P1/P2 ������� ��������� �������� ������ � TLV-����" },
	{ ILRET_SM_PUTDATA_TAG_NOT_FOUND,		"��: ����������� ��� ��� ��������� �������� ������ � TLV-����" },

	{ ILRET_SM_MSE_ERROR,					"��: ������ ���������� ������� MSE" },
	{ ILRET_SM_MSE_WRONG_LENGTH,			"��: ������������ ����� ������� ������ ������� MSE" },
	{ ILRET_SM_MSE_CANT_SET_CONTEXT,		"��: ��������� ��������� ������������ ������� MSE �� ����� ���� ������������ " },
	{ ILRET_SM_MSE_WRONG_PARAMETERS,		"��: ������������ �������� ���������� P1/P2 ������� MSE" },

	{ ILRET_SM_AUTH_BEGIN_ERROR,			"��: ������ ���������� ������� ������ ��������������" },
	{ ILRET_SM_AUTH_BEGIN_WRONG_DATA,		"��: ������������ �������� ������ ������� ������ ��������������" },
	{ ILRET_SM_AUTH_BEGIN_REF_DATA_ERROR,	"��: ������ �������� ������ ������� ������ ��������������" },

	{ ILRET_SM_AUTH_COMPLETE_ERROR,			"��: ������ ���������� ������� ���������� ��������������" },
	{ ILRET_SM_AUTH_COMPLETE_WRONG_DATA,	"��: ������������ �������� ������ ������� ���������� ��������������" },
	{ ILRET_SM_AUTH_COMPLETE_REF_DATA_ERROR,"��: ������ �������� ������ ������� ���������� ��������������" },

	{ ILRET_SM_SP_SESS_ERROR,				"��: ������ ���������� ������� ��������� ������ � ����������� ������" },
	{ ILRET_SM_SP_SESS_WRONG_LENGTH,		"��: ������������ ����� ������� ������ ������� ��������� ������ � ����������� ������" },
	{ ILRET_SM_SP_SESS_CONDITIONS_NOT_SATISFIED, "��: ���������� �� ������������ ��� ���������� ������� ��������� ������ � ����������� ������" },
	{ ILRET_SM_SP_SESS_WRONG_PARAMETERS,	"��: ������������ �������� ���������� P1/P2 ������� ��������� ������ � ����������� ������" },

	{ ILRET_SM_SE_ACTIVATION_ERROR,			"��: ������ ���������" },
	{ ILRET_SM_SE_ACTIVATION_WRONG_DATA,	"��: ������������ �������� ������ ������� ���������" },

	{ ILRET_SM_SE_ALREADY_ACTIVATED,		"��: ������ ������������ ��� � �������������� ���������" },
	{ ILRET_SM_SE_ALREADY_DEACTIVATED,		"��: ������ ������������ ��� � ���������������� ���������" },
	{ ILRET_SM_SE_ILLEGAL_ACTIVATION_MODE,	"��: ����������� ����� ���������/����������� �����" },
#endif//SM_SUPPORT
	
	{ ILRET_OPLIB_ESCAPE,					"����� ������������ �� ���������� ��������" },
	{ ILRET_OPLIB_INVALID_ARGUMENT,			"�������� �������� �������" },
	{ ILRET_OPLIB_METAINFO_WRONG_LEN,		"�������� ����� �������������� �� ������" },
	{ ILRET_OPLIB_EXTRA_DATA_IS_OVER,		"��������� ������������ ����� �������������� �������� �� ������" },
	{ ILRET_OPLIB_SECTOR_NOT_FOUND_IN_LIST, "������ � ��������� ��������������� ����������� � ������ ��������" },
	{ ILRET_OPLIB_DATA_DESCR_NOT_FOUND,		"�� ������ ��������� �������� ������" },
	{ ILRET_OPLIB_BLOCK_DESCR_NOT_FOUND,    "�� ������ ��������� ����� ������" },
	{ ILRET_OPLIB_SECTOR_DESCR_NOT_FOUND,   "�� ������ ��������� ������� ������" },
	{ ILRET_OPLIB_BLOCK_NOT_FOUND,			"�� ������ ���� ������" },
	{ ILRET_OPLIB_ILLEGAL_DATA_TYPE,		"�������� ��� ������" },
	{ ILRET_OPLIB_CORRUPTED_TLV_DATA,		"������ ������� ������ TLV-��������" },
	{ ILRET_OPLIB_INVALID_CONFIRM_PIN,		"�������� ������ �������������" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_1, "�������� ������! �������� �������: 1" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_2, "�������� ������! �������� �������: 2" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_3, "�������� ������! �������� �������: 3" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_4, "�������� ������! �������� �������: 4" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_5, "�������� ������! �������� �������: 5" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_6, "�������� ������! �������� �������: 6" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_7, "�������� ������! �������� �������: 7" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_8, "�������� ������! �������� �������: 8" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_9, "�������� ������! �������� �������: 9" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_10,"�������� ������! �������� �������: 10" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_11,"�������� ������! �������� �������: 11" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_12,"�������� ������! �������� �������: 12" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_13,"�������� ������! �������� �������: 13" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_14,"�������� ������! �������� �������: 14" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_15,"�������� ������! �������� �������: 15" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_16,"�������� ������! �������� �������: 16" },
	{ ILRET_OPLIB_APDU_PACKET_ABSENT,		"����������� ����� APDU-������" },
	{ ILRET_OPLIB_INIT_PROTOCOL_ERROR,		"������ ������������� ������� ����������������" },
	{ ILRET_OPLIB_DATA_FOR_WRITE_NOT_FOUND, "����������� ������ ��� ������ �� �����" },
	{ ILRET_OPLIB_INVALID_WRITE_DATA_TYPE,  "�������� ��� ������" },
	{ ILRET_OPLIB_INVALID_EDIT_DATA_INDEX,  "�������� ������ ������������� ������" },
	{ ILRET_OPLIB_SECTOR_LIST_IS_EMPTY,		"�� ����� ����������� ������� ������" },
	{ ILRET_OPLIB_APP_AUTH_WRONG_CRYPTOGRAM, "������ �������� ���������� ����� ������������ ��������������" },
	{ ILRET_OPLIB_INVALID_FILE_TYPE,		"����������� ��� �����" },
	{ ILRET_OPLIB_WRITE_DATA_LEN_IS_OVER,	"�������� ������ ����������� ������ ������ ��� ����������������� ����� �������" },
	{ ILRET_OPLIB_CARD_LOCK_ERROR,			"������ ���������� �����" },
	{ ILRET_OPLIB_CARD_LOCKED,				"����� �������������!\n���������� � ����" },
	{ ILRET_OPLIB_CARD_CAPTURED,			"����� ������!\n���������� � ����" },
	{ ILRET_OPLIB_CTX_TMP_BUF_IS_OVER,		"�������� ������ ���������� ������ ���������" },
	{ ILRET_OPLIB_INVALID_OPERATION,		"������� �� ������������ ��� ���������� ������ ��������" },
	{ ILRET_OPLIB_SECTORS_EX_DESCR_IS_OVER, "�������� ������ ������ ������� ���������� ��������" },
	{ ILRET_OPLIB_BLOCKS_EX_DESCR_IS_OVER,	"�������� ������ ������ ������� ���������� ������" },
	{ ILRET_OPLIB_DATAS_EX_DESCR_IS_OVER,	"�������� ������ ������ ������� ���������� ��������� ������" },
	{ ILRET_OPLIB_INVALID_BUF_CRC_VALUE,	"�������� �������� ����������� ����� �������� ������" },
	{ ILRET_OPLIB_EXTERNAL_BUF_NOT_SET,		"�� ���������� ��������� �� ������� �����" },
	{ ILRET_OPLIB_BINTLV_BUF_IS_OVER,		"�������� ������ �������� ������ ��� ������ �������� TLV-������" },
	{ ILRET_OPLIB_CARDDATA_BUF_IS_OVER,		"�������� ������ �������� ������ ��� ����������� � ����� ������" },
	{ ILRET_OPLIB_SECTORSDESCR_BUF_IS_OVER,	"�������� ������ �������� ������ ��������� ��������" },
	{ ILRET_OPLIB_BLOCKDATADESCR_BUF_IS_OVER, "�������� ������ �������� ������ ��� ��������� ������ �����" },
	{ ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER,	"�������� ������ �������� ������ ������� �� �������������� ��-����������" },
	{ ILRET_OPLIB_APDUPACKET_BUF_IS_OVER,	"�������� ������ �������� ������ ������ APDU-������" },
	{ ILRET_OPLIB_ENCRYPT_DECRYPT_ERROR,	"����������/������������� ������ �� ���������" },
	{ ILRET_OPLIB_PROVIDER_SESSION_NOT_SET, "�� ����������� ������ � ����������� ������" },
	{ ILRET_OPLIB_ILLEGAL_HASH_LEN,			"�������� ����� ���-�������� ������� �� �������� ������" },

	{ ILRET_TEST_ERROR,						"������ ���������� ��������������� ������������" },
	{ ILRET_TEST_PARAM_NOT_FOUND,			"�� ������ �������� � ����� ��������� �������" },
	{ ILRET_TEST_PARAM_READER_NAME_NOT_FOUND, "� �������� ������� �� ������ �������� ������������ ������" },
	{ ILRET_TEST_PARAM_TESTS_NOT_FOUND,		"� �������� ������� �� ������ �������� ������������������ ����������� ������" },
	{ ILRET_TEST_PARAM_OPER_ID_NOT_FOUND,	"� �������� ������� �� ������ �������� �������������� ��������" },
	{ ILRET_TEST_PARAM_ILLEGAL_OPER_ID,		"�������� �������� �������������� �������� � �������� �������" },
	{ ILRET_TEST_PARAM_ILLEGAL_FORMAT,		"�������� ������ ��������� � �������� �������" }

};
#else
static UECERROR_DESCR ErrDescr[] =
{
	{ 0,									"SUCCESS" },
	{ ILRET_SYS_ERROR,						"System error" },
	{ ILRET_SYS_MEM_ALLOC_ERROR,			"Dynomic allocation memmory failed" },
	{ ILRET_SYS_INVALID_ARGUMENT,			"Invalid input parameter" },
	
	{ ILRET_SCR_ERROR,						"Card reader error" },
	{ ILRET_SCR_UNPOWERED_CARD,				"Card is unpowered" },
	{ ILRET_SCR_REMOVED_CARD,				"Card is removed" },
	{ ILRET_SCR_RESET_CARD,					"Card is reset" },
	{ ILRET_SCR_UNRESPONSIVE_CARD,			"Card is unresponsive" },
	{ ILRET_SCR_PROTOCOL_ERROR,				"Card protocol error" },
	{ ILRET_SCR_SHARING_VIOLATION,			"Card reader sharing violation" },
	{ ILRET_SCR_UNKNOWN_READER,				"Card reader is unknown" },
	{ ILRET_SCR_NOT_READY,					"Card reader is not ready" },
	{ ILRET_SCR_PROTO_MISMATCH,				"APDU protocol is not supported" },
	{ ILRET_SCR_UNSUPPORTED_CARD,			"Unsupported card" },
	{ ILRET_SCR_INVALID_ATR,				"Invalid ATR" },
	{ ILRET_SCR_INVALID_HANDLE,				"Card reader handle is invalid" },
	{ ILRET_SCR_INVALID_DEVICE,				"Invalid device" },
	{ ILRET_SCR_TIMEOUT,					"Card reader timeout is elapsed" },
	{ ILRET_SCR_READER_UNAVAILABLE,			"Card reader is unavailable" },
	
	{ ILRET_CRD_ERROR,						"Card error" },
	{ ILRET_CRD_LENGTH_NOT_MATCH,			"Card response length is not match" },
	
	{ ILRET_CRD_APDU_TAG_NOT_FOUND,			"Tag is not found in returned card data" },
	{ ILRET_CRD_APDU_TAG_LEN_ERROR,			"Illegal tag length in returned card data" },
	{ ILRET_CRD_APDU_DATA_FORMAT_ERROR,		"Illegal format of APDU-coomand data" },
	
	{ ILRET_CRD_VERIFY_ERROR,				"Citezen verification error" },
	{ ILRET_CRD_VERIFY_WRONG_LENGTH,		"Illegal length of citizen verification output data" },
	{ ILRET_CRD_VERIFY_WRONG_PARAMETERS,	"Illegal P1/P2 citizen verification parameters" },
	{ ILRET_CRD_VERIFY_PASSWORD_BLOCKED,	"Password is locked" },
	{ ILRET_CRD_VERIFY_PASSWORD_NOT_FOUND,  "Citizen verfication key is not found" },
	{ ILRET_CRD_VERIFY_WRONG_PASSWORD_PRESENTED,   "Illegal password" },
	
	{ ILRET_CRD_SELECT_ERROR,				"Application or file selection error" },
	{ ILRET_CRD_SELECT_WRONG_LENGTH,		"Illegal length of input selection instruction data" },
	{ ILRET_CRD_SELECT_WRONG_PARAMETERS,	"Illegal P1/P2 values of selection instruction" },
	{ ILRET_CRD_SELECT_OBJECT_BLOCKED,		"Selected object is locked" },
	{ ILRET_CRD_SELECT_WRONG_CMD_DATA,		"Illegal input selection data" },
	{ ILRET_CRD_SELECT_FILE_NOT_FOUND,		"Selecting object is not found" },
	{ ILRET_CRD_SELECT_RESPONSE_ABSENT,	    "Selecting object information is missing" },
	{ ILRET_CRD_SELECT_SECTOR_NOT_FOUND,	"Selecting sector identification is not found" },
	{ ILRET_CRD_SELECT_BLOCK_NOT_FOUND,		"Selecting block identification is not found" },
	
	{ ILRET_CRD_INTAUTH_ERROR,				"Internal authentication error" },
	{ ILRET_CRD_INTAUTH_WRONG_LENGTH,		"Illegal input data length of internal authentication" },
	{ ILRET_CRD_INTAUTH_WRONG_PARAMETERS,   "Illegal P1/P2 values of internal authentication instruction" },
	{ ILRET_CRD_INTAUTH_WRONG_CMD_DATA,		"Illegal input data of internal authentication instruction" },
	{ ILRET_CRD_INTAUTH_KEY_NOT_FOUND,		"Internal authentication instruction key is not found" },
	
	{ ILRET_CRD_MUTAUTH_ERROR,				"External subject authentication error" },
	{ ILRET_CRD_MUTAUTH_WRONG_LENGTH,		"Illegal input data length of external subject authentication" },
	{ ILRET_CRD_MUTAUTH_WRONG_PARAMETERS,   "Illegal P1/P2 values of external subject authentication" },
	{ ILRET_CRD_MUTAUTH_WRONG_CRYPTO,		"Wrong cryptogram of external subject authentication" },
	{ ILRET_CRD_MUTAUTH_KEY_NOT_FOUND,		"External authentication instruction key is not found" },
	{ ILRET_CRD_MUTAUTH_CONDITIONS,			"Wrong application conditions for external subject authentication" },
	
	{ ILRET_CRD_GETCHAL_ERROR,				"Card challange error" },
	{ ILRET_CRD_GETCHAL_WRONG_LENGTH,		"Illegal input data length of Card challange instruction" },
	{ ILRET_CRD_GETCHAL_WRONG_PARAMETERS,   "Illegal P1/P2 values of Card challange instruction" },
	
	{ ILRET_CRD_CHDATA_ERROR,				"Critical data change/set error" },
	{ ILRET_CRD_CHDATA_WRONG_LENGTH,		"Illegal input data length of critical data change/set instruction" },
	{ ILRET_CRD_CHDATA_WRONG_CRYPTO,		"Wrong security conditions for critical data change/set instruction" },
	{ ILRET_CRD_CHDATA_WRONG_DATA_STRUCT,   "Illegal data structure for critical data change/set instruction" },
	{ ILRET_CRD_CHDATA_WRONG_SM_TAG,		"Wrong MAC of critical data change/set instruction" },
	{ ILRET_CRD_CHDATA_WRONG_PARAMETERS,    "Illegal P1/P2 values of critical data change/set instruction" },
	{ ILRET_CRD_CHDATA_KEY_NOT_FOUND,		"Critical data change/set instruction key is not found" },
	{ ILRET_CRD_CHDATA_DATA_LEN_TOO_SHORT,	"Password length too small" },
	
	{ ILRET_CRD_RSTCNTR_ERROR,				"Unlock PIN error" },
	{ ILRET_CRD_RSTCNTR_WRONG_LENGTH,		"Illegal input data length of Unlock PIN instruction" },
	{ ILRET_CRD_RSTCNTR_WRONG_DATA_STRUCT,  "Illegal data structure of Unlock PIN instruction" },
	{ ILRET_CRD_RSTCNTR_WRONG_SM_TAG,		"Wrong tag of SM-insruction when unlockong Pin" },
	{ ILRET_CRD_RSTCNTR_WRONG_PARAMETERS,   "Illegal P1/P2 values of Unlock Pin instruction" },
	{ ILRET_CRD_RSTCNTR_KEY_NOT_FOUND,		"Unlock PIN instruction key is not found" },
	
	{ ILRET_CRD_PERFSECOP_ERROR,			"Crypto operation error" },
	{ ILRET_CRD_PERFSECOP_WRONG_LENGTH,		"Illegal input data length of crypto operation" },
	{ ILRET_CRD_PERFSECOP_BINDING_CMD_MISSED, "Crypto binding command is missed" },
	{ ILRET_CRD_PERFSECOP_BINDING_NOT_SUPPORTED, "Crypto binding is not supported" },
	{ ILRET_CRD_PERFSECOP_WRONG_CERT,		"Illegal public key certificate in Crypto operation" },
	{ ILRET_CRD_PERFSECOP_WRONG_DATA_STRUCT, "Illegal data structure of Crypto operation" },
	{ ILRET_CRD_PERFSECOP_WRONG_PARAMETERS, "Illegal P1/P2 values of Crypto operation" },
	
	{ ILRET_CRD_READBIN_ERROR,				"Binary file data read error" },
	{ ILRET_CRD_READBIN_WRONG_LENGTH,		"Illegal input data length of binary file data read instruction" },
	{ ILRET_CRD_READBIN_WRONG_FILE_TYPE,    "Not binary file for reading data" },
	{ ILRET_CRD_READBIN_WRONG_SEC_CONDITIONS, "Wrong security conditions when reading data from binary file" },
	{ ILRET_CRD_READBIN_EF_NOT_SELECTED,    "Binary file is not selected for reading" },
	{ ILRET_CRD_READBIN_WRONG_PARAMETERS,   "Illegal P1/P2 values of binary file data read instruction" },
	{ ILRET_CRD_READBIN_WRONG_OFFSET,		"Wrong offset when reading from binary file" },
	
	{ ILRET_CRD_UPDBIN_ERROR,				"Binary file data write error" },
	{ ILRET_CRD_UPDBIN_WRONG_LENGTH,		"Illegal input data length of binary file data write instruction" },
	{ ILRET_CRD_UPDBIN_WRONG_SEC_CONDITIONS, "Wrong security conditions when writing data into binary file" },
	{ ILRET_CRD_UPDBIN_WRONG_FILE,			"Binary file is not selected for writing" },
	{ ILRET_CRD_UPDBIN_WRONG_PARAMETERS,    "Illegal P1/P2 values of binary file data write instruction" },
	{ ILRET_CRD_UPDBIN_WRONG_OFFSET,		"Wrong offset when writing into binary file" },
	
	{ ILRET_CRD_GETDATA_ERROR,				"GetDat error" },
	{ ILRET_CRD_GETDATA_WRONG_LENGTH,		"Illegal input/output GetData length" },
	{ ILRET_CRD_GETDATA_WRONG_PARAMETERS,   "Illegal P1/P2 values of GetData instruction" },
	{ ILRET_CRD_GETDATA_TAG_NOT_FOUND,      "GetData tag is not found" },
	{ ILRET_CRD_GETDATA_WRONG_SEC_CONDITIONS, "Wrong security conditions for GetData instruction" },
	{ ILRET_CRD_PUTDATA_ERROR,				"PutData error" },
	{ ILRET_CRD_PUTDATA_WRONG_LENGTH,       "Illegal input GetData length" },
	{ ILRET_CRD_PUTDATA_WRONG_SEC_CONDITIONS, "Wrong security conditions for PutData instruction" },
	{ ILRET_CRD_PUTDATA_WRONG_PARAMETERS,   "Illegal P1/P2 values of PutData instruction" },
	{ ILRET_CRD_PUTDATA_TAG_NOT_FOUND,		"PutData tag is not found" },

	{ ILRET_CRD_READREC_ERROR,				"Record file read error" },
	{ ILRET_CRD_READREC_FILE_BLOCKED,		"Record file is blocked" },
	{ ILRET_CRD_READREC_WRONG_LENGTH,		"Wrong output data length when reading record file" },
	{ ILRET_CRD_READREC_WRONG_FILE_TYPE,	"Not record type file" },
	{ ILRET_CRD_READREC_WRONG_SEC_CONDITIONS, "Wrong security conditions when reading record file" },
	{ ILRET_CRD_READREC_WRONG_PARAMETERS,	"Wrong parameters when reading record file" },
	{ ILRET_CRD_READREC_RECORD_NOT_FOUND,	"Record is not found" },
	{ ILRET_CRD_READREC_WRONG_PARAMETERS_P1P2, "Wrong P1/P2 parameters when reading record file" },
	{ ILRET_CRD_UPDREC_ERROR,				"Record file update error" },
	{ ILRET_CRD_UPDREC_FILE_BLOCKED,		"Record file is blocked" },
	{ ILRET_CRD_UPDREC_WRONG_LENGTH,		"Wrong input data length when updating record file" },
	{ ILRET_CRD_UPDREC_WRONG_FILE_TYPE,		"Not record type file" },
	{ ILRET_CRD_UPDREC_WRONG_SEC_CONDITIONS, "Wrong security conditions when updating record file" },
	{ ILRET_CRD_UPDREC_WRONG_PARAMETERS,	"Wrong parameters when updating record file" },
	{ ILRET_CRD_UPDREC_RECORD_NOT_FOUND,	"Updating record is not found" },
	{ ILRET_CRD_UPDREC_WRONG_PARAMETERS_P1P2, "Wrong P1/P2 parameters when updating record file" },
	{ ILRET_CRD_APPREC_ERROR,				"Record file append error" },
	{ ILRET_CRD_APPREC_FILE_BLOCKED,		"Record file is blocked" },
	{ ILRET_CRD_APPREC_WRONG_LENGTH,		"Wrong input data length when appending record file" },
	{ ILRET_CRD_APPREC_WRONG_FILE_TYPE,		"Not record type file" },
	{ ILRET_CRD_APPREC_WRONG_SEC_CONDITIONS, "Wrong security conditions when appending record file" },
	{ ILRET_CRD_APPREC_WRONG_PARAMETERS,	"Wrong parameters when appending record file" },
	{ ILRET_CRD_APPREC_NOT_ENOUGH_MEMORY,	"No room when appending record file" },
	{ ILRET_CRD_APPREC_WRONG_PARAMETERS_P1P2, "Wrong P1/P2 parameters when appending record file" },

	{ ILRET_PARAM_ERROR,					"Log file error" },
	{ ILRET_PARAM_NOT_FOUND,				"Configurational file parameter is not found" },
	{ ILRET_PARAM_WRONG_FORMAT,				"Wrong format of configurational file parameter" },
	{ ILRET_PARAM_WRONG_LENGTH,				"Illegal length of configurational file parameter" },
	{ ILRET_PARAM_FORMAT_UNKNOWN,			"Configurational file parameter format is unknown" },
	{ ILRET_PARAM_DESCR_NOT_FOUND,			"External descriptor of configurational file parameter is not found" },
	{ ILRET_PARAM_WRITE_SECTOR_EX_ERROR,	"Exteral sector descriptions section writing failed" },
	{ ILRET_PARAM_SECTOR_EX_WRONG_FORMAT,	"Illegal format of exteral sector descriptions" },
	{ ILRET_PARAM_CERTIFICATE_NOT_FOUND,	"Certificate is not found in configurational file" },
	{ ILRET_PROT_ERROR,						"Log-file error" },
	{ ILRET_PROT_LOGFILE_OPEN_ERROR,		"Log-file open error" },
	{ ILRET_PROT_LOGEILE_WRITE_ERROR,		"Log-file write error" },
	{ ILRET_DATA_ERROR,						"Illegal input data of callinc function" },
	{ ILRET_DATA_TAG_NOT_FOUND,				"Data tag is not found" },
	{ ILRET_DATA_TAG_WRONG_FORMAT,			"Wrong format of data tag" },
	{ ILRET_DATA_TAG_WRONG_LENGTH,			"Wrong length of data tag element" },
	{ ILRET_DATA_CARD_FORMAT_ERROR,			"Wrong format of data tag element" },
	{ ILRET_APP_VER_NOT_SUPPORTED,			"Illegal card version" },
	{ ILRET_NO_CRYPTOALG_SUPPORTED,			"Terminal does not support selected crypto algorithm" },
	{ ILRET_APP_NOT_ACTIVE_YET,				"Card is not active yet" },
	{ ILRET_APP_EXPIRED,					"Card is expired" },
	{ ILRET_INVALID_HEX_STRING_FORMAT,		"Wrong format of Hex-string" },
	{ ILRET_APDU_RES_NOT_ALLOWED,			"Not allowed answer in APDU-packet returned" },
	{ ILRET_APDU_ALLOWED_RES_IS_OVER,		"Too many APDU-packet command allowed answers" },
	{ ILRET_APP_INCONSISTENT_STATE,			"Application is in inconsistent state" },
	{ ILRET_APP_UNKNOWN_STATE,				"Application is in unknown state" },
	
	{ ILRET_CERT_MISSING_ELEMENT,			"Mandatory certificate element is missing" },
	{ ILRET_CERT_WRONG_LENGTH,				"Illegal data length of certificate element" },
	{ ILRET_CERT_NOT_ACTIVE_YET,			"Certificate is not active yet" },
	{ ILRET_CERT_EXPIRED,					"Certificate is expired" },
	{ ILRET_CERT_WRONG_VERSION,				"Illegal version of certificate" },
	{ ILRET_CERT_WRONG_RSA_EXP,				"Illegal public key exponent value in certificate" },
	{ ILRET_CERT_INVALID_TYPE,				"Illegal type of certificate" },
	{ ILRET_CERT_TERMINFO_NOT_MATCH,		"Terminal information is not equal in  GOST and RSA certificates" },

	{ ILRET_CRYPTO_ERROR,					"Cryptoprovider error" },
	{ ILRET_CRYPTO_RSA_FORMAT,				"Wrong RSA format" },
	{ ILRET_CRYPTO_CRYPTO_PREPARE_SESSION,  "Crypto prepare session error" },
	{ ILRET_CRYPTO_WRONG_SM_MAC,			"Wrong cryposession MAC" },
	{ ILRET_CRYPTO_WRONG_DATA_LENGTH,		"Illegal cryptographic data length" },
	{ ILRET_CRYPTO_WRONG_DATA_FORMAT,		"Illegal cryptographic data format" },
	{ ILRET_CRYPTO_WRONG_CERT,				"Wrong certificate" },
	{ ILRET_CRYPTO_WRONG_MAC,				"Wrong MAC key value when checking application authentication request" },
	{ ILRET_CRYPTO_WRONG_CHK_ISS_SESS_MAC,  "Wrong MAC key value when checking issuer crypto session" },
	
	{ ILRET_OPLIB_ESCAPE,					"Operation is canceled by user" },
	{ ILRET_OPLIB_INVALID_ARGUMENT,			"Invalid input argument" },
	{ ILRET_OPLIB_METAINFO_WRONG_LEN,		"Illegal length of metainformation" },
	{ ILRET_OPLIB_EXTRA_DATA_IS_OVER,		"External data length is over" },
	{ ILRET_OPLIB_SECTOR_NOT_FOUND_IN_LIST, "Secor whith the given identification is not found" },
	{ ILRET_OPLIB_DATA_DESCR_NOT_FOUND,		"Data element description is not found" },
	{ ILRET_OPLIB_BLOCK_DESCR_NOT_FOUND,    "Block description is not found" },
	{ ILRET_OPLIB_SECTOR_DESCR_NOT_FOUND,   "Sector description is not found" },
	{ ILRET_OPLIB_BLOCK_NOT_FOUND,			"Block is not found" },
	{ ILRET_OPLIB_ILLEGAL_DATA_TYPE,		"Illegal data type" },
	{ ILRET_OPLIB_CORRUPTED_TLV_DATA,		"TLV-data is corrupted" },
	{ ILRET_OPLIB_INVALID_CONFIRM_PIN,		"Invalid confirmation PIN-value" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_1, "Illegal password! Tries left: 1" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_2, "Illegal password! Tries left: 2" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_3, "Illegal password! Tries left: 3" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_4, "Illegal password! Tries left: 4" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_5, "Illegal password! Tries left: 5" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_6, "Illegal password! Tries left: 6" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_7, "Illegal password! Tries left: 7" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_8, "Illegal password! Tries left: 8" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_9, "Illegal password! Tries left: 9" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_10,"Illegal password! Tries left: 10" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_11,"Illegal password! Tries left: 11" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_12,"Illegal password! Tries left: 12" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_13,"Illegal password! Tries left: 13" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_14,"Illegal password! Tries left: 14" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_15,"Illegal password! Tries left: 15" },
	{ ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_16,"Illegal password! Tries left: 16" },
	{ ILRET_OPLIB_APDU_PACKET_ABSENT,		"APDU-packet is absent" },
	{ ILRET_OPLIB_INIT_PROTOCOL_ERROR,		"Log-file initialization error" },
	{ ILRET_OPLIB_DATA_FOR_WRITE_NOT_FOUND, "Data for writing on card is not found" },
	{ ILRET_OPLIB_INVALID_WRITE_DATA_TYPE,  "Illegal data type for writing" },
	{ ILRET_OPLIB_INVALID_EDIT_DATA_INDEX,  "Invalid editting data index" },
	{ ILRET_OPLIB_SECTOR_LIST_IS_EMPTY,		"Sectors list is empty" },
	{ ILRET_OPLIB_APP_AUTH_WRONG_CRYPTOGRAM, "Wrong authentication cryptopgram when checking on host" },
	{ ILRET_OPLIB_INVALID_FILE_TYPE,		"Invalid type of file" },
	{ ILRET_OPLIB_WRITE_DATA_LEN_IS_OVER,	"Buffer for data writing iis over" },
	{ ILRET_OPLIB_CARD_LOCK_ERROR,			"Card locking error" },
	{ ILRET_OPLIB_CARD_LOCKED,				"CARD IS LOCKED!!!" },
	{ ILRET_OPLIB_CARD_CAPTURED,			"CARD IS CAPTURED!!!" },
	{ ILRET_OPLIB_CTX_TMP_BUF_IS_OVER,		"Temporary context buffer length is over" },
	{ ILRET_OPLIB_INVALID_OPERATION,		"Operation is not supported" },
	{ ILRET_OPLIB_SECTORS_EX_DESCR_IS_OVER, "Buffer of external sector descriptors is over" },
	{ ILRET_OPLIB_BLOCKS_EX_DESCR_IS_OVER,	"Buffer of external block descriptors is over" },
	{ ILRET_OPLIB_DATAS_EX_DESCR_IS_OVER,	"Buffer of external data descriptors is over" },
	{ ILRET_OPLIB_INVALID_BUF_CRC_VALUE,	"Invalid checksum of external buffer" },
	{ ILRET_OPLIB_EXTERNAL_BUF_NOT_SET,		"Pointer to �ne of external buffers is not set in context" },
	{ ILRET_OPLIB_BINTLV_BUF_IS_OVER,		"Maximum size of external buffer for BinTlv data is over" },
	{ ILRET_OPLIB_CARDDATA_BUF_IS_OVER,		"Maximum size of external buffer for reading card data is over" },
	{ ILRET_OPLIB_SECTORSDESCR_BUF_IS_OVER,	"Maximum size of external buffer for secors description is over" },
	{ ILRET_OPLIB_BLOCKDATADESCR_BUF_IS_OVER, "Maximum size of external buffer for blocks description is over" },
	{ ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER,	"Maximum size of external buffer for application auth request is over" },
	{ ILRET_OPLIB_APDUPACKET_BUF_IS_OVER,	"Maximum size of external buffer for APDU-packets is over },
	{ ILRET_OPLIB_ENCRYPT_DECRYPT_ERROR,	"Encrypted\Decrypted data is not matching" },
	{ ILRET_OPLIB_PROVIDER_SESSION_NOT_SET, "Service Provider session is not established" },
	{ ILRET_OPLIB_ILLEGAL_HASH_LEN,			"Illegal hash length of service request" },

	{ ILRET_TEST_ERROR,						"Autotest error" },
	{ ILRET_TEST_PARAM_NOT_FOUND,			"Autotest parameter is not found in script" },
	{ ILRET_TEST_PARAM_READER_NAME_NOT_FOUND, "<ReaderName> autotest parameter is not found in script" },
	{ ILRET_TEST_PARAM_TESTS_NOT_FOUND,		"<Tests> autotest parameter is not found in script" },
	{ ILRET_TEST_PARAM_OPER_ID_NOT_FOUND,	"<OperId> autotest parameter is not found in script" },
	{ ILRET_TEST_PARAM_ILLEGAL_OPER_ID,		"Current <OperId> autotest parameter is illegal" },
	{ ILRET_TEST_PARAM_ILLEGAL_FORMAT,		"Illegal autotest parameter" }

};
#endif//ENGLISH

IL_FUNC IL_CHAR* opApiGetErrorDescr(IL_WORD ErrorCode)
{
	IL_WORD i;

	for(i = 0; i < sizeof(ErrDescr); i++)
	{
		if(ErrorCode == ErrDescr[i].RC)
			return ErrDescr[i].Text;
	}

	return "������������������� ������";
}

IL_FUNC IL_WORD opApiGetIqFrontParam(IL_CHAR *SectionName, IL_CHAR *ParamName, IL_CHAR *out_ParamValue)
{
	IL_WORD RC;

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiGetIqFrontParam()")
	PROT_WRITE_EX2(PROT_OPLIB2, "IN: SectionName=%s ParamName=%s", SectionName, ParamName)
	PROT_WRITE_EX0(PROT_OPLIB3, "prmGetParameterIqFront()")
	RC = prmGetParameterIqFront(SectionName, ParamName, out_ParamValue);
	PROT_WRITE_EX1(PROT_OPLIB3, "prmGetParameterIqFront()=%u", RC)
	PROT_WRITE_EX1(PROT_OPLIB2, "OUT: ParamValue=%s", out_ParamValue)
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiGetIqFrontParam()=%u", RC)

	return RC;
}

IL_FUNC IL_WORD opApiReset(s_opContext *p_opContext)
{
	IL_WORD RC;

	PROT_WRITE_EX0(PROT_OPLIB1, "crDeinit()")
	RC = crDeinit(p_opContext->phCrd->hRdr);
	PROT_WRITE_EX1(PROT_OPLIB1, "crDeinit()=%u", RC)
	
	return RC;
}


IL_FUNC IL_WORD opApiLockCard(s_opContext *p_opContext)
{
	/*+++
	IL_WORD RC;

	// �������� ��������� ������ 
	_opCmnCreateTmpPukBlock(p_opContext->PinBlock);

	//??? ����������� ������������
	//p_opContext->SectorIdSelected = 0;
	if((RC = opCmnRestoreCryptoSession(p_opContext)))
		return ILRET_OPLIB_CARD_LOCK_ERROR;

	// ��������� ���
	if((RC = _LockPin(p_opContext, 0)))
		return ILRET_OPLIB_CARD_LOCK_ERROR;

	//??? ����������� ������������
	//p_opContext->SectorIdSelected = 0;
	if((RC = opCmnRestoreCryptoSession(p_opContext)))
		return ILRET_OPLIB_CARD_LOCK_ERROR;

	// ��������� ���
	if((RC = _LockPin(p_opContext, 1)))
		return ILRET_OPLIB_CARD_LOCK_ERROR;

	//??? ����������� ������������
	//p_opContext->SectorIdSelected = 0;
	if((RC = opCmnRestoreCryptoSession(p_opContext)))
		return ILRET_OPLIB_CARD_LOCK_ERROR;
	+++*/

	return 0;
}

IL_FUNC IL_WORD opApiMakeDigitalSignature(s_opContext *p_opContext)
{
	IL_WORD RC;

	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "������������ ����������� ������� ��������� �����", ".")
	PROT_WRITE_EX0(PROT_OPLIB1, "opApiMakeDigitalSignature()")

	// �������� ������������� ������� �������
	if(!p_opContext || !p_opContext->phCrd->ifSign 
			|| !p_opContext->pAuthRequestBuf || !p_opContext->pAuthRequestLen
			|| !p_opContext->pDigitalSignBuf || !p_opContext->pDigitalSignLen 
			|| !p_opContext->pDigitalSignCertChain || !p_opContext->pDigitalSignCertChainLen) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT; 
	else
	{	// ���������� ��� ��������� �����
		PROT_WRITE_EX0(PROT_OPLIB1, "flMakeDigitalSignature()")
		RC = flMakeDigitalSignature(p_opContext->phCrd, 
								p_opContext->pAuthRequestBuf, *p_opContext->pAuthRequestLen,
								p_opContext->pDigitalSignBuf, p_opContext->pDigitalSignLen,
								p_opContext->pDigitalSignCertChain, p_opContext->pDigitalSignCertChainLen);
		PROT_WRITE_EX1(PROT_OPLIB1, "flMakeDigitalSignature()=%u", RC)
	}

	OP_CMN_DISPLAY_LINE(p_opContext, RC ? "������" : "OK");
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiMakeDigitalSignature()=%u", RC)

	return RC;
}

IL_FUNC IL_WORD opApiSetProviderCryptoSession(s_opContext *p_opContext)
{
	IL_WORD RC;
	IL_BYTE msg[256];
	IL_WORD msg_len = 0;
	IL_BYTE pan[10];
	IL_BYTE *pTmpPtr;
	IL_BYTE tmp[10];
	IL_DWORD dwLen;
	IL_WORD wLen;
	IL_WORD wTmp;

	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "��������� ���������� ������ � ����������� ������", ".")
	PROT_WRITE_EX0(PROT_OPLIB1, "opApiSetProviderCryptoSession()")

	// �������� ���������� �������� ���������
	if(!p_opContext) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	// �������� ������������� ��������� �� ���������� ���������� ������
	if(!p_opContext->pCSpId || !p_opContext->CSpIdLen) 	{
		RC = ILRET_OPLIB_EXTERNAL_BUF_NOT_SET; goto End;
	}

	// �������� ���������� ���������� ������
	PROT_WRITE_EX0(PROT_OPLIB1, "flCheckCertificateSP()")
	RC = flCheckCertificateSP(p_opContext->phCrd, p_opContext->pCSpId, p_opContext->CSpIdLen, p_opContext->ifGostPS);
	PROT_WRITE_EX1(PROT_OPLIB1, "flCheckCertificateSP()=%u", RC)
	if(RC)
		return RC;

	// ��������� ��������������� ����� SP Challenge = PAN || ACC
	str2bcdF(p_opContext->PAN, pan, sizeof(pan));
	msg_len += (IL_WORD)AddTag(IL_TAG_5A, pan, 10, msg);
	// ������� �������� �������� ����������� �������������� AAC ��������� � �����
	if((RC = clAppGetData(p_opContext->phCrd, IL_TAG_9F36, tmp, &wLen)) != 0) 
        goto End;
    if((RC = TagFind(tmp, wLen, IL_TAG_9F36, &dwLen, &pTmpPtr, 0)) != 0)
		goto End;
	msg_len += (IL_WORD)AddTag(IL_TAG_9F36, pTmpPtr, dwLen, &msg[msg_len]);

	// ����������� ������� �������� �������� AAC �� 1 
	// ��������� ��� ��������� ������������ �������������� ������ ��� �������� ����� ��������� �� 1 �������������
	wTmp = ((msg[msg_len-2] << 8) + msg[msg_len-1]); 
	wTmp++;
	msg[msg_len-1] = (IL_BYTE)(wTmp & 0x00FF);
	msg[msg_len-2] = (IL_BYTE)((wTmp >> 8) & 0x00FF);

	// ������������� ������ � ����������� ������
	PROT_WRITE_EX0(PROT_OPLIB3, "opApiSetProviderCryptoSession()")
#ifndef SM_SUPPORT
	RC = cryptoSetTerminalToProviderSession11(&p_opContext->PSMContext, 
	                                          p_opContext->phCrd->KeyVer,
											  p_opContext->pCSpId, p_opContext->CSpIdLen, 
											  msg, msg_len, 
											  &p_opContext->ProviderSessionData, 
											  p_opContext->ifGostPS);
#else											  
	RC = cryptoSetTerminalToProviderSession11(p_opContext->phCrd->hCrypto, 
	                                          p_opContext->phCrd->KeyVer,
											  p_opContext->pCSpId, p_opContext->CSpIdLen, 
											  msg, msg_len, 
											  &p_opContext->ProviderSessionData, 
											  p_opContext->ifGostPS);
#endif
	PROT_WRITE_EX1(PROT_OPLIB3, "opApiSetProviderCryptoSession()=%u", RC)
	if(!RC)
		// ������ ���� �������� ��������� ���������� ������ � ����������� ������
		p_opContext->isProviderSession = 1;

End:
	OP_CMN_DISPLAY_LINE(p_opContext, RC ? "������" : "OK");
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiSetProviderCryptoSession()=%u", RC)
	
	return RC;
}

IL_FUNC IL_WORD opApiAuthServiceProvider(s_opContext *p_opContext)
{
	IL_WORD RC;

	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "�������������� ���������� ������", ".")
	PROT_WRITE_EX0(PROT_OPLIB1, "opApiAuthServiceProvider()")

	// �������� ���������� �������� ���������
	if(!p_opContext) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	// �������� ��������� � ��������� ��������� �� ���������� ���������� ������  
	if(!p_opContext->pCSpId || !p_opContext->CSpIdLen) {
		RC = ILRET_OPLIB_EXTERNAL_BUF_NOT_SET; goto End;
	}

	// �������� ���������� ���������� ������
	PROT_WRITE_EX0(PROT_OPLIB3, "flCheckCertificateSP()")
	RC = flCheckCertificateSP(p_opContext->phCrd, p_opContext->pCSpId, p_opContext->CSpIdLen, p_opContext->ifGostPS);
	PROT_WRITE_EX1(PROT_OPLIB3, "flCheckCertificateSP()=%u", RC)
	if(RC)
		goto End;

	// �������� ������������� ������ ��� �������������� ���������� ������
	if(!p_opContext->ProviderAuthData.pMsg || !p_opContext->ProviderAuthData.pS) {
		RC = ILRET_OPLIB_EXTERNAL_BUF_NOT_SET; goto End;
	}

	// ��������������� ���������� ������
	PROT_WRITE_EX0(PROT_OPLIB3, "cryptoAuthServiceProvider()")
#ifndef SM_SUPPORT
	RC = cryptoAuthServiceProvider(&p_opContext->PSMContext, p_opContext->ProviderAuthData.pMsg, p_opContext->ProviderAuthData.MsgLen, 
									 p_opContext->ProviderAuthData.pS,  p_opContext->ProviderAuthData.SLen, 
									 p_opContext->pCSpId, p_opContext->CSpIdLen, p_opContext->ifGostPS, UECLIB_APP_VER_11);
#else
	RC = cryptoAuthServiceProvider(p_opContext->phCrd->hCrypto, p_opContext->ProviderAuthData.pMsg, p_opContext->ProviderAuthData.MsgLen, 
									 p_opContext->ProviderAuthData.pS,  p_opContext->ProviderAuthData.SLen, 
									 p_opContext->pCSpId, p_opContext->CSpIdLen, p_opContext->ifGostPS, UECLIB_APP_VER_11);
#endif
	PROT_WRITE_EX1(PROT_OPLIB3, "cryptoAuthServiceProvider()=%u", RC)

End:
	OP_CMN_DISPLAY_LINE(p_opContext, RC ? "������" : "OK")
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiAuthServiceProvider()=%u", RC)

	return RC;
}

IL_FUNC IL_WORD opApiEncryptProviderToTerminal(s_opContext *p_opContext)
{
	IL_WORD RC;

	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "���������� ������ �� ���������� ����� ���������� ������", ".")
	PROT_WRITE_EX0(PROT_OPLIB1, "opApiEncryptProviderToTerminal()")

	// �������� ���������� �������� ���������
	if(!p_opContext) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	// �������� ��������� � ��������� ��� ������� ������� ��� ���������� ������  
	if(!p_opContext->pClearData || !p_opContext->pClearDataLen || !p_opContext->pEncryptedData || !p_opContext->pEncryptedDataLen) {
		RC = ILRET_OPLIB_EXTERNAL_BUF_NOT_SET; goto End;
	}

	// �������� ��������� � ��������� ��� ������� ������� ��� ���������� ������  
	if(!p_opContext->pClearData || !p_opContext->pClearDataLen || !p_opContext->pEncryptedData || !p_opContext->pEncryptedDataLen) {
		RC = ILRET_OPLIB_EXTERNAL_BUF_NOT_SET; goto End;
	}

	// ������� ���� ������
	PROT_WRITE_EX0(PROT_OPLIB3, "cryptoEncryptTerminalToProvider11()")
#ifndef SM_SUPPORT
	RC = cryptoEncryptTerminalToProvider11(&p_opContext->PSMContext, 
										   p_opContext->pClearData, *p_opContext->pClearDataLen,
										   p_opContext->pEncryptedData, p_opContext->pEncryptedDataLen,
										   p_opContext->ifGostPS);
#else
	RC = cryptoEncryptTerminalToProvider11(p_opContext->phCrd->hCrypto, 
										   p_opContext->pClearData, *p_opContext->pClearDataLen,
										   p_opContext->pEncryptedData, p_opContext->pEncryptedDataLen,
										   p_opContext->ifGostPS);
#endif
	PROT_WRITE_EX1(PROT_OPLIB3, "cryptoEncryptTerminalToProvider11()=%u", RC)
										   
End:
	OP_CMN_DISPLAY_LINE(p_opContext, RC ? "������" : "OK")
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiEncryptProviderToTerminal()=%u", RC)
	
	return RC;
}

IL_FUNC IL_WORD opApiDecryptProviderToTerminal(s_opContext *p_opContext)
{
	IL_WORD RC;

	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "������������� ������ �� ���������� ����� ���������� ������", ".")
	PROT_WRITE_EX0(PROT_OPLIB1, "opApiDecryptProviderToTerminal()")

	// �������� ���������� �������� ���������
	if(!p_opContext) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	// �������� ��������� ������ � ����������� ������
	if(!p_opContext->isProviderSession) {
		RC = ILRET_OPLIB_PROVIDER_SESSION_NOT_SET; goto End;
	}


	// �������� ��������� � ��������� ��� ������� ������� ��� ������������� ������  
	if(!p_opContext->pClearData || !p_opContext->pClearDataLen || !p_opContext->pEncryptedData || !p_opContext->pEncryptedDataLen) {
		RC = ILRET_OPLIB_EXTERNAL_BUF_NOT_SET; goto End;
	}

	// ���������� ���� ������
	PROT_WRITE_EX0(PROT_OPLIB3, "cryptoDecryptTerminalToProvider11()")
#ifndef SM_SUPPORT
	RC = cryptoDecryptTerminalToProvider11(&p_opContext->PSMContext, 
										   p_opContext->pEncryptedData, *p_opContext->pEncryptedDataLen,
										   p_opContext->pClearData, p_opContext->pClearDataLen,
										   p_opContext->ifGostPS);
#else
	RC = cryptoDecryptTerminalToProvider11(p_opContext->phCrd->hCrypto, 
										   p_opContext->pEncryptedData, *p_opContext->pEncryptedDataLen,
										   p_opContext->pClearData, p_opContext->pClearDataLen,
										   p_opContext->ifGostPS);
#endif
	PROT_WRITE_EX1(PROT_OPLIB3, "cryptoDecryptTerminalToProvider11()=%u", RC)

End:
	OP_CMN_DISPLAY_LINE(p_opContext, RC ? "������" : "OK")
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiDecryptProviderToTerminal()=%u", RC)

	return RC;
}

IL_FUNC IL_WORD opApiRunApduPacketSE(s_opContext *p_opContext)
{
	IL_WORD RC = 0;
#ifdef SM_SUPPORT
	IL_BYTE *pIn, *pOut;
	IL_BYTE *pInMax;
	IL_WORD max_out_len;
	IL_APDU_PACK_ELEM ApduElem;

	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "���������� ��������������� ������ APDU-�������", ".")
	PROT_WRITE_EX0(PROT_OPLIB1, "opApiRunApduPacketSE()")

	// �������� ���������� ����������
	if(!p_opContext || !p_opContext->pApduIn || !p_opContext->ApduInLen || !p_opContext->pApduOut || !p_opContext->ApduOutLen) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	PROT_WRITE_EX2(PROT_OPLIB2, "ApduIn[%u]=%s", p_opContext->ApduInLen, (bin2hex(p_opContext->TmpBuf, p_opContext->pApduIn, p_opContext->ApduInLen)))

	// ��������� ������������ ������ ��������� ������ ��������� ���������� ������ � ���������� APDU-������ � ������
	max_out_len = *p_opContext->ApduOutLen;

	// ������� ������ ��������� ������ � ��������� ���������� ������
	*p_opContext->ApduOutLen		= 0;
	*p_opContext->pApduPacketResult = 0;

	// �������������� ��������� �� ������� � �������� ������ ������
	pIn  = p_opContext->pApduIn;
	pOut = p_opContext->pApduOut;

	// �������� APDU-������� ������
	for(*p_opContext->pApduPacketSize = 0, pInMax = pIn + p_opContext->ApduInLen;
		RC == 0 && *p_opContext->pApduPacketResult == 0 && pIn < pInMax; 
		(*p_opContext->pApduPacketSize)++)
	{
		// ������������ �������� ������ � ������� APDU-�������
		pIn = _bin2apduin(pIn, &ApduElem);
		// �������� APDU-�������
		PROT_WRITE_EX0(PROT_OPLIB3, "smRunApdu()")
		*p_opContext->pApduPacketResult = smRunApdu(p_opContext->phCrd->hCrypto, &ApduElem);
		PROT_WRITE_EX1(PROT_OPLIB3, "smRunApdu()=%u", *p_opContext->pApduPacketResult)
		// �o���������� ����� APDU-������� � �������� �������� ������
		PROT_WRITE_EX0(PROT_OPLIB3, "_apduout2bin()")
		RC = _apduout2bin(&ApduElem, pOut, p_opContext->ApduOutLen, max_out_len);
		PROT_WRITE_EX1(PROT_OPLIB3, "_apduout2bin()=%u", RC)
	}

End:
	if(!RC)
		RC = *p_opContext->pApduPacketResult;
	
	OP_CMN_DISPLAY_LINE(p_opContext, RC ? "������" : "OK")
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiRunApduPacketSE()=%u", RC)
#endif//SM_SUPPORT 

	return RC;
}

IL_FUNC IL_WORD opApiGetHashSnils(s_opContext *p_opContext, IL_BYTE *pHashBuf, IL_WORD *pHashLen)
{
	IL_WORD RC = 0;
	IL_BYTE snils[20];
	IL_WORD data_len = sizeof(snils);

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiGetHashSnils()")

	// �������� ���������� ������� ����������
	if(!p_opContext || !pHashBuf || !pHashLen) {
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	// ������� "�����" �������� ����� �� ������������������ ������� ��-����������
	PROT_WRITE_EX0(PROT_OPLIB3, "opCmnReadCardDataRaw()")
	RC = opCmnReadCardDataRaw(p_opContext, 1, 2, BLOCK_DATA_TLV, 0xDF27, &data_len, snils);
	PROT_WRITE_EX1(PROT_OPLIB3, "opCmnReadCardDataRaw()=%u", RC)
	if(RC)
		goto End;

	// �������� ���������� ����� ������������� �������� �����
	if(data_len != 6) {
		RC = ILRET_DATA_CARD_FORMAT_ERROR; goto End;
	}

	// �������� �������� ����� � ����������� �� ������������� ���������������
	PROT_WRITE_EX0(PROT_OPLIB3, "cryptoGetHashSnils()")
	RC = cryptoGetHashSnils(snils, pHashBuf, pHashLen, p_opContext->phCrd->ifGostCrypto);
	PROT_WRITE_EX1(PROT_OPLIB3, "cryptoGetHashSnils()=%u", RC)

End:
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiGetHashSnils()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opApiCheckUecCard(IL_CARD_HANDLE* phCrd)
{
	IL_WORD RC = flAppReselect(phCrd);

	return RC;
}

IL_FUNC IL_WORD opApiWritePassPhrase(s_opContext *p_opContext)
{
	IL_WORD RC;

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiWritePassPhrase()")
	OP_CMN_DISPLAY_STRING_PREFIX(p_opContext, "������ ������������ ����������� �� �����", ".")

	if(!p_opContext)
		RC = ILRET_OPLIB_INVALID_ARGUMENT; 
	else
	{
		PROT_WRITE_EX0(PROT_OPLIB3, "flSetPassPhrase()")
		RC = flSetPassPhrase(p_opContext->phCrd, p_opContext->PassPhrase);
		PROT_WRITE_EX1(PROT_OPLIB3, "flSetPassPhrase()=%u", RC)
	}

	OP_CMN_DISPLAY_LINE(p_opContext, RC ? "������" : "OK")
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiWritePassPhrase()=%u", RC)
	
	return RC;
}

IL_FUNC IL_WORD opApiGetBlockPatternDescr(s_opContext *p_opContext, IL_CHAR *BlockId, IL_CHAR *PatternDescr, IL_WORD *PatternDescrLen)
{
	IL_WORD RC = 0;
	IL_WORD max_buf_len;
	int item, _sectorId, _blockId;
	IL_CHAR ParamName[20];
	

	PROT_WRITE_EX0(PROT_OPLIB1, "opApiGetBlockPatternDescr()")

	// �������� ���������� ����������
	if(!p_opContext || !BlockId || !PatternDescr || !PatternDescrLen || *PatternDescrLen == 0) 
	{
		RC = ILRET_OPLIB_INVALID_ARGUMENT; goto End;
	}

	PROT_WRITE_EX1(PROT_OPLIB2, "BlockId=%s", BlockId)

	// ��������� ������������ ������ ��������� ������
	PatternDescr[0] = '\0';
	max_buf_len = *PatternDescrLen - 1;

	// ��������� �������������� ������� � ����� ������������� ������ 
	_sectorId = _blockId = 0;
	sscanf(BlockId, "%u-%u", &_sectorId, &_blockId);

	for(item = 1, *PatternDescrLen = 0;  ; item++)
	{
		sprintf(ParamName, "Pattern%d%d%d", _sectorId, _blockId, item); 
		RC = prmGetParameterPattern(ParamName, &PatternDescr[*PatternDescrLen], (max_buf_len - *PatternDescrLen + 1));
		if(RC)
		{
			if(RC == ILRET_PARAM_NOT_FOUND) 
				RC = 0; 
			goto End;
		}

		*PatternDescrLen += cmnStrLen(&PatternDescr[*PatternDescrLen]);
		PatternDescr[(*PatternDescrLen)++] = '\n';
		PatternDescr[*PatternDescrLen] = '\0';
	}

End:
	if(!RC)
		PROT_WRITE_EX1(PROT_OPLIB2, "OUT: opApiGetBlockPatternDescr=\n%s", PatternDescr);
	PROT_WRITE_EX1(PROT_OPLIB1, "opApiGetBlockPatternDescr()=%u", RC)

	return RC;
}