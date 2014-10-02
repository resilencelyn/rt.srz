#include "il_types.h"
#include "il_error.h"
#include "HAL_Common.h"
#include "opCtxDef.h"
#include "opCtxFunc.h"
#include "HAL_Protocol.h"

extern IL_BYTE _calculate_crc(IL_BYTE *buf, IL_WORD buf_len);


IL_FUNC IL_WORD opCtxSetDisplayTextFunc(s_opContext *p_opContext, void (*pf)(IL_CHAR*))         
{
#ifdef GUIDE
	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetDisplayTextFunc()")
	if(p_opContext->pDisplayText == NULL && pf)
		p_opContext->pDisplayText = pf;
	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetDisplayTextFunc()=%s", p_opContext->pDisplayText ? "FUNC" : "NULL")
#endif//GUIDE

	return 0;
}

IL_FUNC IL_WORD opCtxSetClean(s_opContext *p_opContext)
{
	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetClean()")
    cmnMemSet((BYTE*)p_opContext, 0, sizeof(s_opContext));
	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetClean()=0")
    
	return 0;
}

IL_FUNC IL_WORD opCtxSetCardReaderHandler(s_opContext *p_opContext, IL_CARD_HANDLE* in_phCrd)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetCardReaderHandler()")
    if (!p_opContext || !in_phCrd) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
		p_opContext->phCrd = in_phCrd;
	
	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetCardReaderHandler()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetMetaInfo(s_opContext *p_opContext, IL_BYTE *pMetaInfo, IL_WORD MetaInfoLen)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetMetaInfo()")
    if (!p_opContext || !pMetaInfo) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: MetaInfo[%u]=%s", MetaInfoLen, bin2hex(p_opContext->TmpBuf, pMetaInfo, MetaInfoLen))
		p_opContext->pMetaInfo	 = pMetaInfo;
		p_opContext->MetaInfoLen = MetaInfoLen;
		p_opContext->MataInfoCrc = _calculate_crc(pMetaInfo, MetaInfoLen);
	}
	
	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetMetaInfo()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetExtraData(s_opContext *p_opContext, IL_BYTE *pExtraData, IL_WORD ExtraDataLen)
{
	WORD RC = 0;
	
	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetExtraData()")
    if(!p_opContext || !pExtraData) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: ExtraData[%u]=%s", ExtraDataLen, bin2hex(p_opContext->TmpBuf, pExtraData, ExtraDataLen))
		p_opContext->pExtraData	  = pExtraData;
		p_opContext->ExtraDataLen = ExtraDataLen;
		p_opContext->ExtraDataCrc = _calculate_crc(pExtraData, ExtraDataLen);
	}
	
	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetExtraData()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetRequestHash(s_opContext *p_opContext, IL_BYTE *pRequestHash, IL_WORD RequestHashLen)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetRequestHash()")
    if(!p_opContext || !pRequestHash) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else 
	{
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: RequestHash[%u]=%s", RequestHashLen, bin2hex(p_opContext->TmpBuf, pRequestHash, RequestHashLen))
		p_opContext->pRequestHash   = pRequestHash;
		p_opContext->RequestHashLen = RequestHashLen;
		p_opContext->RequestHashCrc = _calculate_crc(pRequestHash, RequestHashLen);
	}
	
	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetRequestHash()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetPinBlock(s_opContext *p_opContext, IL_CHAR *pPinStr)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetPinBlock()")
    if(!p_opContext || !pPinStr || cmnStrLen(pPinStr) > 8) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX1(PROT_OPLIB2, "IN: PinLen=%u", ((IL_WORD)cmnStrLen(pPinStr)))
		str2pin(pPinStr, p_opContext->PinBlock);
	}
	
	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetPinBlock()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetNewPinStr(s_opContext *p_opContext, IL_CHAR *pNewPinStr)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetNewPinStr()")
    if(!p_opContext || !pNewPinStr || cmnStrLen(pNewPinStr) > 8) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX1(PROT_OPLIB2, "IN: PinLen=%u", ((IL_WORD)cmnStrLen(pNewPinStr)))
		p_opContext->pNewPinStr = pNewPinStr; 
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetNewPinStr()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetConfirmPinStr(s_opContext *p_opContext, IL_CHAR *pConfirmPinStr)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetConfirmPinStr()")
    if(!p_opContext || !pConfirmPinStr || cmnStrLen(pConfirmPinStr) > 8) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX1(PROT_OPLIB2, "IN: PinLen=%u", ((IL_WORD)cmnStrLen(pConfirmPinStr)))
		p_opContext->pConfirmPinStr = pConfirmPinStr; 
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetConfirmPinStr()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxGetPinTriesLeft(s_opContext *p_opContext, IL_BYTE *out_PinTriesLeft)
{
	IL_WORD RC = 0;

 	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxGetPinTriesLeft()")
	if(!p_opContext || !out_PinTriesLeft) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX1(PROT_OPLIB2, "OUT: PinTriesLeft=%u", p_opContext->PinTriesLeft)
		*out_PinTriesLeft = p_opContext->PinTriesLeft;
	}
	
	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxGetPinTriesLeft()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxGetPinNum(s_opContext *p_opContext, IL_BYTE *out_PinNum)
{
	IL_WORD RC = 0;

 	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxGetPinNum()")
    if(!p_opContext || !out_PinNum) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX1(PROT_OPLIB2, "OUT: PinNum=%u", p_opContext->PinNum)
		*out_PinNum = p_opContext->PinNum;
	}
	
	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxGetPinNum()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetPinNum(s_opContext *p_opContext, IL_BYTE in_PinNum)
{
	IL_WORD RC = 0;

 	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetPinNum()")
    if (!p_opContext) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX1(PROT_OPLIB2, "IN: PinNum=%u", in_PinNum)
		p_opContext->PinNum = in_PinNum;
	}
	
	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetPinNum()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetCardDataBuf(s_opContext *p_opContext, IL_CHAR *CardDataDescr, IL_CHAR *CardDataBuf, IL_WORD *CardDataLen)
{
	IL_WORD RC = 0;

 	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetCardDataBuf()")
    if(!p_opContext || !CardDataDescr || !CardDataBuf || !CardDataLen) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: CardDataDesc=%s CardDataBufLen=%u", CardDataDescr, *CardDataLen)
		p_opContext->pCardDataDescr = CardDataDescr;
		p_opContext->pCardDataBuf	= CardDataBuf;
		p_opContext->pCardDataLen   = CardDataLen;
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetCardDataBuf()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetAuthRequestBuf(s_opContext *p_opContext, IL_BYTE ifAuthOnline, IL_BYTE *pAuthRequestBuf, IL_WORD *pAuthRequestLen)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetAuthRequestBuf()")
    if (!p_opContext || !pAuthRequestBuf || !pAuthRequestLen) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: ifAuthOnline=%u AuthRequestBufLen=%u", ifAuthOnline, *pAuthRequestLen)
		p_opContext->ifAuthOnline    = ifAuthOnline;
		p_opContext->pAuthRequestBuf = pAuthRequestBuf;
		p_opContext->pAuthRequestLen = pAuthRequestLen;
		p_opContext->AuthRequestCrc  = 0;
	}
	
	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetAuthRequestBuf()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetAppAuthResponseData(s_opContext *p_opContext, IL_BYTE *pResponseData, IL_WORD ResponseDataLen)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetAppAuthResponseData()")
    if (!p_opContext || !pResponseData) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: ResponseData[%u]=%s", ResponseDataLen, bin2hex(p_opContext->TmpBuf, pResponseData, ResponseDataLen))
		p_opContext->pAuthResponseData = pResponseData;
		p_opContext->AuthResponseLen = ResponseDataLen;
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetAppAuthResponseData()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetOperationCode(s_opContext *p_opContext, IL_WORD in_OpCode)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetOperationCode()")
    if(!p_opContext || in_OpCode >= UEC_OP_END) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX1(PROT_OPLIB2, "IN: OpCode=%u", in_OpCode)
		p_opContext->OperationCode = in_OpCode;
	}
	
	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetOperationCode()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxGetIssuerSessionIcChallenge(s_opContext *p_opContext, IL_BYTE *IcChallenge16)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxGetIssuerSessionIcChallenge()")
    if(!p_opContext || !IcChallenge16) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		IL_WORD len = (IL_WORD)sizeof(p_opContext->sess_in.IcChallenge);
		cmnMemCopy(IcChallenge16, p_opContext->sess_in.IcChallenge, len);
		PROT_WRITE_EX2(PROT_OPLIB2, "OUT: IcChallenge[%u]=%s", len, bin2hex(p_opContext->TmpBuf, IcChallenge16, len))
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxGetIssuerSessionIcChallenge()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetIssuerSessionCryptogramm(s_opContext *p_opContext, IL_BYTE *CardCryptogramm20, IL_BYTE CardCryptogrammLength)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetIssuerSessionCryptogramm()")
    if(!p_opContext || !CardCryptogramm20) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: CardCryptogramm[%u]=%s", CardCryptogrammLength, bin2hex(p_opContext->TmpBuf, CardCryptogramm20, CardCryptogrammLength))
		cmnMemCopy((IL_BYTE*)&p_opContext->sess_out.CardCryptogramm, CardCryptogramm20, 20);
		p_opContext->sess_out.CardCryptogrammLength = CardCryptogrammLength;
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetIssuerSessionCryptogramm()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxGetCheckIssuerSessionData(s_opContext *p_opContext, IL_BYTE *HostChallenge16, IL_BYTE *CardCryptogramm4)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxGetCheckIssuerSessionData()")
    if(!p_opContext || !HostChallenge16 || !CardCryptogramm4) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		IL_WORD HostChallengeLen = (IL_WORD)sizeof(p_opContext->chk_sess_in.HostChallenge);
		IL_WORD CardCryptogrammLen = (IL_WORD)sizeof(p_opContext->chk_sess_in.CardCryptogramm);
		cmnMemCopy(HostChallenge16,   p_opContext->chk_sess_in.HostChallenge, HostChallengeLen);
		cmnMemCopy(CardCryptogramm4,  p_opContext->chk_sess_in.CardCryptogramm, CardCryptogrammLen);
		PROT_WRITE_EX4(PROT_OPLIB2, "OUT: HostChallenge[%u]=%s CardCryptogramm[%u]=%s", HostChallengeLen, bin2hex(p_opContext->TmpBuf, HostChallenge16, HostChallengeLen), CardCryptogrammLen, bin2hex(&p_opContext->TmpBuf[100], CardCryptogramm4, CardCryptogrammLen))
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxGetCheckIssuerSessionData()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetPhotoBuf(s_opContext *p_opContext, IL_BYTE *pPhotoBuf, IL_WORD *pPhotoLen)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetPhotoBuf()")
	if (!p_opContext || !pPhotoBuf || !pPhotoLen) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX1(PROT_OPLIB2, "IN: PhotoBufLen=%u", *pPhotoLen)
		p_opContext->pPhotoBuf = pPhotoBuf;
		p_opContext->pPhotoLen = pPhotoLen;
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetPhotoBuf()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetSectorExDescr(s_opContext *p_opContext, IL_WORD SectorId, IL_BYTE SectorVer, IL_CHAR *pExSectorDescr)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetSectorExDescr()")
	if(!p_opContext || !pExSectorDescr) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX3(PROT_OPLIB2, "IN: SectorId=%u SectorVer=%02X SectorDescr=%s", SectorId, SectorVer, pExSectorDescr)
		p_opContext->wTmp = SectorId;
		p_opContext->bTmp = SectorVer;	
		p_opContext->pExSectorDesr = pExSectorDescr; 
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetSectorExDescr()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxGetPassPhrase(s_opContext *p_opContext, IL_CHAR *PassPhrase)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB1, "opCtxGetPassPhrase()")
	if(!p_opContext || !PassPhrase) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{	
		if(p_opContext->ifPassPhraseUsed)
			PassPhrase[0] = 0;	// фраза контрольного приветстви€ предъ€вл€етс€ только один раз!!!
		else
		{
			cmnStrCopy(PassPhrase, p_opContext->PassPhrase);
			p_opContext->ifPassPhraseUsed = 1;
		}
		PROT_WRITE_EX1(PROT_OPLIB2, "OUT: PassPhrase=%s", PassPhrase)
	}

	PROT_WRITE_EX1(PROT_OPLIB1, "opCtxGetPassPhrase()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetPassPhrase(s_opContext *p_opContext, IL_CHAR *PassPhrase)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB1, "opCtxSetPassPhrase()")
	if(!p_opContext || !PassPhrase) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX1(PROT_OPLIB2, "IN: PassPhrase=%s", PassPhrase);
		if(cmnStrLen(PassPhrase) > PASS_PHRASE_MAX_LEN)
			PassPhrase[PASS_PHRASE_MAX_LEN] = '\0';
		cmnStrCopy(p_opContext->PassPhrase, PassPhrase);
		p_opContext->ifPassPhraseUsed = 0;
	}

	PROT_WRITE_EX1(PROT_OPLIB1, "opCtxSetPassPhrase()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetSectorsDescrBuf(s_opContext *p_opContext, IL_CHAR *pSectorsDescrBuf, IL_WORD *pSectorsDescrLen)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetSectorsDescrBuf()")
	if (!p_opContext || !pSectorsDescrBuf || !pSectorsDescrLen) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX1(PROT_OPLIB2, "IN: SectorsDescrBufLen=%u", *pSectorsDescrLen)
		p_opContext->pSectorsDescrBuf = pSectorsDescrBuf;
		p_opContext->pSectorsDescrLen = pSectorsDescrLen;
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetSectorsDescrBuf()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetBlockDataBuf(s_opContext *p_opContext, IL_CHAR *pBlockDescr, IL_CHAR *pBlockDataBuf, IL_WORD *pBlockDataLen)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetBlockDataBuf()")
	if(!p_opContext || !pBlockDescr || !pBlockDataBuf || !pBlockDataLen) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: BlockDescr=%s BlockDataBufLen=%u", pBlockDescr, *pBlockDataLen)
		p_opContext->pBlockDescr	= pBlockDescr;
		p_opContext->pBlockDataBuf	= pBlockDataBuf; 	
		p_opContext->pBlockDataLen  = pBlockDataLen;
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetBlockDataBuf()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetApduPacketBuf(s_opContext *p_opContext,
									  IL_BYTE isApduEncryptedPS,
									  IL_WORD *pApduPacketSize,
									  IL_BYTE *pApduInBuf, IL_WORD ApduInLen, 
									  IL_BYTE *pApduOutBuf, IL_WORD *pApduOutLen,
									  IL_WORD *pApduPacketResult)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetApduPacketBuf()")
	if (!p_opContext || !pApduInBuf || !pApduOutBuf || !pApduOutLen) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX1(PROT_OPLIB2, "IN: EncryptedPS=%u", isApduEncryptedPS)
        if(ApduInLen < 1024*2)
			PROT_WRITE_EX2(PROT_OPLIB2, "IN: ApduIn[%u]=%s", ApduInLen, bin2hex(p_opContext->TmpBuf, pApduInBuf, ApduInLen));
		PROT_WRITE_EX1(PROT_OPLIB2, "IN: ApduOutBufLen=%u", *pApduOutLen)
		p_opContext->isApduEncryptedPS	= isApduEncryptedPS;
		p_opContext->pApduPacketSize	= pApduPacketSize;
		p_opContext->pApduIn			= pApduInBuf;
		p_opContext->ApduInLen			= ApduInLen;
		p_opContext->pApduOut			= pApduOutBuf;
		p_opContext->ApduOutLen			= pApduOutLen;
		p_opContext->pApduPacketResult	= pApduPacketResult;
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetApduPacketBuf()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetDigitalSignatureBuf(s_opContext *p_opContext, 
											IL_BYTE *pDigitalSignBuf, IL_WORD *pDigitalSignLen,
											IL_BYTE *pDigitalSignCertChain, IL_WORD *pDigitalSignCertChainLen)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetDigitalSignatureBuf()")
	if (!p_opContext || !pDigitalSignBuf || !pDigitalSignLen || !pDigitalSignCertChain || !pDigitalSignCertChainLen) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: DigitalSignBufLen=%u DigitalSignCertChainBufLen=%u", *pDigitalSignLen, *pDigitalSignCertChainLen)
		p_opContext->pDigitalSignBuf		  = pDigitalSignBuf;
		p_opContext->pDigitalSignLen		  = pDigitalSignLen;
		p_opContext->pDigitalSignCertChain	  = pDigitalSignCertChain;
		p_opContext->pDigitalSignCertChainLen = pDigitalSignCertChainLen;
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetDigitalSignatureBuf()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetProviderSessionParams(s_opContext *p_opContext, IL_BYTE ifGostPS, IL_BYTE *pCSpId, IL_WORD CSpIdLen)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetProviderSessionParams()")
	if(!p_opContext || !pCSpId) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX3(PROT_OPLIB2, "IN: ifGostPS=%u CSpId[%u]=%s", ifGostPS, CSpIdLen, bin2hex(p_opContext->TmpBuf, pCSpId, CSpIdLen))
		p_opContext->ifGostPS	= ifGostPS;
		p_opContext->pCSpId		= pCSpId;
		p_opContext->CSpIdLen	= CSpIdLen;
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetProviderSessionParams()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxGetProviderSessionData(s_opContext *p_opContext, PROVIDER_SESSION_DATA *pProviderSessionDataOut)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxGetProviderSessionData()")
	if(!p_opContext || !pProviderSessionDataOut)
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
		cmnMemCopy((IL_BYTE*)pProviderSessionDataOut, (IL_BYTE*)&p_opContext->ProviderSessionData, sizeof(PROVIDER_SESSION_DATA));

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxGetProviderSessionData()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetProviderAuthData(s_opContext *p_opContext, IL_BYTE *Msg, IL_WORD Msg_len, IL_BYTE *S, IL_WORD S_len)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetProviderAuthData()")
	if(!p_opContext || !Msg || !S)
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: Msg[%u]=%s", Msg_len, bin2hex(p_opContext->TmpBuf, Msg, Msg_len))
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: Sign[%u]=%s", S_len, bin2hex(p_opContext->TmpBuf, S, S_len))
		p_opContext->ProviderAuthData.pMsg	 = Msg;
		p_opContext->ProviderAuthData.MsgLen = Msg_len;
		p_opContext->ProviderAuthData.pS	 = S;
		p_opContext->ProviderAuthData.SLen	 = S_len;
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetProviderAuthData()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetProviderEncrDecrBuf(s_opContext *p_opContext, 
											IL_BYTE *pClearData, IL_DWORD *pClearDataLen,
											IL_BYTE *pEncryptedData, IL_DWORD *pEncryptedDataLen)
{
	IL_WORD RC = 0;

	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetProviderEncrDecrBuf()")
	if(!p_opContext || !pClearData || !pClearDataLen || !pEncryptedData || !pEncryptedDataLen)
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: ClearData[%lu]=%s", *pClearDataLen, bin2hex(p_opContext->TmpBuf, pClearData, *pClearDataLen))
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: EncryptedData[%lu]=%s", *pEncryptedDataLen, bin2hex(p_opContext->TmpBuf, pEncryptedData, *pEncryptedDataLen))
		p_opContext->pClearData			= pClearData;
		p_opContext->pClearDataLen		= pClearDataLen;
		p_opContext->pEncryptedData		= pEncryptedData;
		p_opContext->pEncryptedDataLen	= pEncryptedDataLen;
	}

	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetProviderEncrDecrBuf()=%u", RC)
	return RC;
}

IL_FUNC IL_WORD opCtxSetSeOwnerName(s_opContext *p_opContext, IL_BYTE *pSeOwnerName, IL_WORD SeOwnerNameLen)
{
	IL_WORD RC = 0;
#ifdef SM_SUPPORT
	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxSetSeOwnerName()")
	if(!p_opContext || !pSeOwnerName)
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX2(PROT_OPLIB2, "IN: SeOwnerName[%lu]=%s", SeOwnerNameLen, bin2hex(p_opContext->TmpBuf, pSeOwnerName, SeOwnerNameLen))
		p_opContext->pSeOwnerName = pSeOwnerName;
		p_opContext->SeOwnerNameLen = SeOwnerNameLen;
	}
	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetSeOwnerName()=%u", RC)
#endif//SM_SUPPORT

	return RC;
}

IL_FUNC IL_WORD opCtxGetSeIssuerSessionIcChallenge(s_opContext *p_opContext, IL_BYTE *IcChallenge16)
{
	IL_WORD RC = 0;
#ifdef SM_SUPPORT
	PROT_WRITE_EX0(PROT_OPLIB3, "opCtxGetSeIssuerSessionIcChallenge()")
    if(!p_opContext || !IcChallenge16) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		IL_WORD len = (IL_WORD)sizeof(p_opContext->SE_SessIn.IcChallenge);
		cmnMemCopy(IcChallenge16, p_opContext->SE_SessIn.IcChallenge, sizeof(p_opContext->SE_SessIn.IcChallenge));
		PROT_WRITE_EX2(PROT_OPLIB2, "OUT: IcChallenge[%lu]=%s", len, bin2hex(p_opContext->TmpBuf, IcChallenge16, len))
	}
	PROT_WRITE_EX1(PROT_OPLIB3, "opCtxSetSeOwnerName()=%u", RC)
#endif//SM_SUPPORT
	return RC;
}

IL_FUNC IL_WORD opCtxSetSeIssuerSessionCryptogramm(s_opContext *p_opContext, IL_BYTE *CardCryptogramm, IL_BYTE CardCryptogrammLength, IL_BYTE ifGostSession)
{
	IL_WORD RC = 0;
#ifdef SM_SUPPORT
    if(!p_opContext || !CardCryptogramm) 
		RC = ILRET_OPLIB_INVALID_ARGUMENT;
	else
	{
		PROT_WRITE_EX3(PROT_OPLIB2, "IN: ifGostSession=%u, CardCryptogramm[%lu]=%s", ifGostSession, CardCryptogrammLength, bin2hex(p_opContext->TmpBuf, CardCryptogramm, CardCryptogrammLength))
		cmnMemCopy((IL_BYTE*)&p_opContext->SE_SessOut.CardCryptogramm, CardCryptogramm, CardCryptogrammLength);
		p_opContext->SE_SessOut.CardCryptogrammLength = CardCryptogrammLength;
		p_opContext->SE_IfGostIssSession = ifGostSession;
	}
#endif//SM_SUPPORT
	return RC;
}











