#include "Host.h"
#include "HAL_Crypto.h"
#include "com_cryptodsb.h"
#include "ru_cryptodsb.h"
#include "HAL_Parameter.h"
#include "HAL_Protocol.h" //+++

IL_FUNC IL_RETCODE PrepareSM(SM_CONTEXT* pSM, IL_APDU* pilApdu);
IL_FUNC IL_RETCODE ProcessSM(SM_CONTEXT* pSM, IL_APDU* pilApdu);
IL_FUNC void AddToSessionSmCounter(IL_BYTE* SessionSmCounter8, IL_BYTE bNumberToAdd); 
IL_FUNC void SubFromSessionSmCounter(IL_BYTE* SessionSmCounter8, IL_BYTE bNumberToAdd); 

void toHostLog(IL_CHAR* str, IL_BYTE* buf, IL_DWORD len)
{
	/*
	char tmp1[1024];
	bin2hex(tmp1, buf, len);
	OutputDebugString("\n");    
	OutputDebugString(str);
	OutputDebugString(": ");
	OutputDebugString("\n");    
	OutputDebugString(tmp1);
	*/
}

IL_FUNC IL_RETCODE GetHostRandom(IL_BYTE* HostRandom16)
{
	return GetRandom(HostRandom16, 16);
}

IL_FUNC IL_RETCODE hostCheckAuthRequestOnline(IL_BYTE* pData, IL_WORD wDataLen, IL_BYTE ifGost, IL_BYTE AppVer)
{
	IL_DWORD ilRet = 0;

	IL_BYTE* pTmpPtr;
	IL_DWORD dwTmpLen;
	IL_INT i;
	IL_BYTE DiversData[256] = {0};
	IL_WORD DiversDataLen = 0;
	
	IL_TAG tags10[] = {IL_TAG_9F15, IL_TAG_9F1C, IL_TAG_9F37, IL_TAG_9F21, IL_TAG_9F03, IL_TAG_DF02, IL_TAG_9F36, IL_TAG_9F35};  
	IL_TAG tags11[] = {IL_TAG_9F15, IL_TAG_9F37, IL_TAG_9F21, IL_TAG_9F03, IL_TAG_DF02, IL_TAG_9F1C, IL_TAG_9F1D, IL_TAG_9F36, IL_TAG_9F35, IL_TAG_DF28};  
    IL_TAG* tags = (AppVer == UECLIB_APP_VER_10)? tags10 : tags11;
    IL_BYTE tag_num = (AppVer == UECLIB_APP_VER_10)?(sizeof(tags10)/sizeof(tags10[0])):(sizeof(tags11)/sizeof(tags11[0]));
    IL_TAG tag_mac = (AppVer == UECLIB_APP_VER_10)? IL_TAG_9F26 : IL_TAG_5F74;
    
	IL_BYTE* pCardMAC;
	IL_DWORD dwCardMAC;
	IL_BYTE MK_ac_id[32]; 
	IL_BYTE MAC4[4] = {0};

	IL_BYTE msg[8192] = {0};
	IL_WORD ofs = 0;
	IL_BYTE DiversFlag = 0;

	// MK_ac_id считываем из файла параметров эмулятора хоста
	if((ilRet = prmGetParameterHost(ifGost?IL_PARAM_MK_AC_ID_GOST:IL_PARAM_MK_AC_ID_RSA, MK_ac_id, &dwTmpLen)))
		return ilRet;
		
	if((ilRet = prmGetParameterHost(ifGost?IL_PARAM_MK_AC_ID_DIVERS_FLAG_GOST:IL_PARAM_MK_AC_ID_DIVERS_FLAG_RSA, &DiversFlag, &dwTmpLen)))
		return ilRet;
		
	if(DiversFlag)
	{
		IL_BYTE Pan[10] = { 0 };
		IL_BYTE Snils[6] = { 0 };
		IL_BYTE *pCIN = NULL;

		if((ilRet = TagFind(pData, wDataLen, IL_TAG_5A, &dwTmpLen, &pTmpPtr, 0)) && ilRet != ILRET_DATA_TAG_NOT_FOUND)
			return ilRet;
		if(ilRet)
		{	// извлекаем из запрса на аутентификацию значение CIN для возможного несогласованного состояния ИД-Приложения 
			if((ilRet = TagFind(pData, wDataLen, IL_TAG_45, &dwTmpLen, &pCIN, 0)))
				return ilRet;
			// извлекаем из настроек эмулятора хоста значения PAN и SNILS для указанной учётной записи карты (CIN)
			if((ilRet = prmGetParameterHostCin2PanSnils(pCIN, Pan, Snils)))
				return ilRet;
			pTmpPtr = Pan; dwTmpLen = sizeof(Pan);
		}

		memcpy(DiversData, pTmpPtr, dwTmpLen);
		DiversDataLen += dwTmpLen;	
		
		if((ilRet = TagFind(pData, wDataLen, IL_TAG_DF27, &dwTmpLen, &pTmpPtr, 0)) && !pCIN)
			return ilRet;
		if(pCIN)
		{
			pTmpPtr = Snils; dwTmpLen = sizeof(Snils);
		}

		memcpy(&DiversData[DiversDataLen], pTmpPtr, dwTmpLen);	
		DiversDataLen += dwTmpLen;	
		
	    if(!ifGost)
	    {
	        MKDF(MK_ac_id, DiversData, DiversDataLen, MK_ac_id);
	    }
	    else
	    {
	        MKDF_GOST(MK_ac_id, DiversData, DiversDataLen, MK_ac_id);
	    }	
	}

	// формируем данные для аутентификации 
	for(i=0; i<tag_num; i++)
	{
		ilRet = TagFind(pData, wDataLen, tags[i], &dwTmpLen, &pTmpPtr, 1);
		if(ilRet) {
protWriteEx(0, "hostCheckAuthRequestOnline: НЕ НАЙДЕН ТЕГ %08lX", tags[i]);  
			return ilRet;
		}
		cmnMemCopy(&msg[ofs], pTmpPtr, dwTmpLen);
		ofs += dwTmpLen;
	}

	ilRet = TagFind(pData, wDataLen, tag_mac, &dwCardMAC, &pCardMAC, 0); 
	if(ilRet) {
protWriteEx(0, "hostCheckAuthRequestOnline: НЕ НАЙДЕН ТЕГ %08lX", tag_mac);  
		return ilRet;
	}
	
	//вычисляем MK ac.id
	if(!ifGost)
	{
#ifndef GET_DATA_VALUE_WITH_TAG 
	    DES_RetailMAC4(msg, ofs, MK_ac_id, MAC4);
#else	    
	    if(AppVer == UECLIB_APP_VER_10)
		    DES_RetailMAC4(msg, ofs, MK_ac_id, MAC4);
		else 
		    DES_MAC4(msg, ofs, MK_ac_id, MAC4);
#endif		    
	}
	else
	{
        if(AppVer == UECLIB_APP_VER_10)
        {
//MICRON SPECIFIC!!! REMOVE!!!
	        TagFind(pData, wDataLen, IL_TAG_9F36, &dwTmpLen, &pTmpPtr, 1);
	        dwTmpLen = pTmpPtr - pData;
    	    
	        ofs = AddTag(IL_TAG_5F70, pData, dwTmpLen, msg);
       		
   		    ilRet = TagFind(pData, wDataLen, IL_TAG_9F36, &dwTmpLen, &pTmpPtr, 1);
		    if(ilRet)
			    return ilRet;
		    cmnMemCopy(&msg[ofs], pTmpPtr, dwTmpLen);
		    ofs += dwTmpLen;
    		
   		    ilRet = TagFind(pData, wDataLen, IL_TAG_9F35, &dwTmpLen, &pTmpPtr, 1);
		    if(ilRet)
			    return ilRet;
		    cmnMemCopy(&msg[ofs], pTmpPtr, dwTmpLen);
		    ofs += dwTmpLen;
    
	        msg[ofs] = 0x80;
	        ofs++;
//END MICRON SPECIFIC!!!
	    }
	    
		Gost28147_Imit(MK_ac_id, msg, ofs, MAC4);
	}

	if((dwCardMAC!=4) || cmnMemCmp(MAC4, pCardMAC, 4))
		ilRet = ILRET_CRYPTO_WRONG_MAC;

	return ilRet;
}



IL_FUNC IL_RETCODE hostPrepareIssuerSessionRSA(HANDLE_CRYPTO_HOST* hCrypto, IL_BYTE* pData, IL_WORD wDataLen, ISSUER_SESSION_DATA_IN* pSessionDataIn, ISSUER_SESSION_DATA_OUT* pSessionDataOut, IL_BYTE AppVer)
{
	IL_RETCODE ilRet;
	IL_WORD i;
	IL_BYTE HostChallenge[16] = {0};
	IL_BYTE SK[16] = {0};
	IL_BYTE SM_Challenge[16];
	IL_BYTE R1[] = {0x00,0x00,0x00,0x01};
	IL_BYTE R2[] = {0x00,0x00,0x00,0x02};

	IL_BYTE T[4] = {0};

	IL_BYTE MK_sm_id[16];
	IL_DWORD dwTmpLen; 
	IL_BYTE DiversFlag = 0;
 	IL_BYTE* pTmpPtr;
  	IL_BYTE DiversData[256] = {0};
	IL_WORD DiversDataLen = 0;

	//Generate Host Challenge
	ilRet = GetHostRandom(HostChallenge);
	if(ilRet)
		return ilRet;

	// MK_sm_id считываем из файла параметров эмулятора хоста!!!
	if((ilRet = prmGetParameterHost(IL_PARAM_MK_SM_ID_RSA, MK_sm_id, &dwTmpLen)))
		return ilRet;

	if((ilRet = prmGetParameterHost(IL_PARAM_MK_AC_ID_DIVERS_FLAG_RSA, &DiversFlag, &dwTmpLen)))
		return ilRet;
		
	for(i = 0; i<16; i++)
	{
		SM_Challenge[i] = HostChallenge[i] ^ pSessionDataIn->IcChallenge[i];
	}
	
    if(DiversFlag)
	{
		IL_BYTE Pan[10] = { 0 };
		IL_BYTE Snils[6] = { 0 };
		IL_BYTE *pCIN = NULL;

		if((ilRet = TagFind(pData, wDataLen, IL_TAG_5A, &dwTmpLen, &pTmpPtr, 0)) && ilRet != ILRET_DATA_TAG_NOT_FOUND)
			return ilRet;
		if(ilRet)
		{	// извлекаем из запрса на аутентификацию значение CIN для возможного несогласованного состояния ИД-Приложения 
			if((ilRet = TagFind(pData, wDataLen, IL_TAG_45, &dwTmpLen, &pCIN, 0)))
				return ilRet;
			// извлекаем из настроек эмулятора хоста значения PAN и SNILS для указанной учётной записи карты (CIN)
			if((ilRet = prmGetParameterHostCin2PanSnils(pCIN, Pan, Snils)))
				return ilRet;
			pTmpPtr = Pan; dwTmpLen = sizeof(Pan);
		}
		
		memcpy(DiversData, pTmpPtr, dwTmpLen);
		DiversDataLen += dwTmpLen;	
		
		if((ilRet = TagFind(pData, wDataLen, IL_TAG_DF27, &dwTmpLen, &pTmpPtr, 0)) && !pCIN)
			return ilRet;
		if(pCIN)
		{
			pTmpPtr = Snils; dwTmpLen = sizeof(Snils);
		}
		
		memcpy(&DiversData[DiversDataLen], pTmpPtr, dwTmpLen);	
		DiversDataLen += dwTmpLen;	
		
        MKDF(MK_sm_id, DiversData, DiversDataLen, MK_sm_id);
    }

	//Make SK
	KDF(MK_sm_id, SM_Challenge, sizeof(SM_Challenge), SK);

	if( AppVer == UECLIB_APP_VER_10)
	{
	    //Make SK smi.id.des
	    KDF(SK, R1, sizeof(R1), hCrypto->SM.SKsmiiddes);
	    //Make SK smc.id.des
	    KDF(SK, R2, sizeof(R2), hCrypto->SM.SKsmciddes);
    }
    else
    {
	    //Make SK smi.id.des
	    KDF(SK, R2, sizeof(R2), hCrypto->SM.SKsmiiddes);
	    //Make SK smc.id.des
	    KDF(SK, R1, sizeof(R1), hCrypto->SM.SKsmciddes);
    }
    
	//calc T
#ifndef GET_DATA_VALUE_WITH_TAG 
	DES_RetailMAC4(pSessionDataIn->IcChallenge, 16, hCrypto->SM.SKsmiiddes, T);
#else	
	if( AppVer == UECLIB_APP_VER_10)
	    DES_RetailMAC4(pSessionDataIn->IcChallenge, 16, hCrypto->SM.SKsmiiddes, T); 
	else
	    DES_MAC4(pSessionDataIn->IcChallenge, 16, hCrypto->SM.SKsmiiddes, T); 
#endif
	    
    pSessionDataOut->CardCryptogrammLength = 20;
	cmnMemCopy(pSessionDataOut->CardCryptogramm, T, 4);
	cmnMemCopy(&pSessionDataOut->CardCryptogramm[4], HostChallenge, 16);

	//Init session SM counter
	cmnMemCopy(hCrypto->SM.SessionSmCounter, SM_Challenge, 8);
	
#ifndef GET_DATA_VALUE_WITH_TAG 
	if( AppVer != UECLIB_APP_VER_10)
	{
        SubFromSessionSmCounter(hCrypto->SM.SessionSmCounter, 1);
    }
#endif

	return 0;
}

IL_FUNC IL_RETCODE hostPrepareIssuerSessionGost(HANDLE_CRYPTO_HOST* hCrypto, IL_BYTE* pData, IL_WORD wDataLen, ISSUER_SESSION_DATA_IN* pSessionDataIn, ISSUER_SESSION_DATA_OUT* pSessionDataOut, IL_BYTE AppVer)
{
	IL_RETCODE ilRet;
	IL_WORD i;
	IL_BYTE HostChallenge[16] = {0};
	IL_BYTE SK32[32] = {0};
	IL_BYTE R1[] = {0x00,0x00,0x00,0x01};
	IL_BYTE R2[] = {0x00,0x00,0x00,0x02};
	IL_BYTE SM_Challenge[16] = {0};

	IL_BYTE T[4] = {0};
	IL_BYTE MK_sm_id_gost[32];
	IL_DWORD dwTmpLen;
    IL_BYTE rand_len;
	IL_BYTE DiversFlag = 0;
 	IL_BYTE* pTmpPtr;
  	IL_BYTE DiversData[256] = {0};
	IL_WORD DiversDataLen = 0;
 
    if(AppVer == UECLIB_APP_VER_10)
    {
        //MICRON SPECIFIC!!!
        rand_len = 8;
    }
    else
    {
        rand_len = 16;
    }

	//Generate Host Challenge
	ilRet = GetHostRandom(HostChallenge);
	if(ilRet)
		return ilRet;

	// MK_sm_id_gost считываем из файла параметров эмулятора хоста!!!
	if((ilRet = prmGetParameterHost(IL_PARAM_MK_SM_ID_GOST, MK_sm_id_gost, &dwTmpLen)))
		return ilRet;

	if((ilRet = prmGetParameterHost(IL_PARAM_MK_AC_ID_DIVERS_FLAG_GOST, &DiversFlag, &dwTmpLen)))
		return ilRet;
		
	for(i = 0; i < rand_len; i++)
	{
		SM_Challenge[i] = HostChallenge[i] ^ pSessionDataIn->IcChallenge[i];
	}
	
	if(DiversFlag)
	{
		ilRet = TagFind(pData, wDataLen, IL_TAG_5A, &dwTmpLen, &pTmpPtr, 0);
		if(ilRet)
			return ilRet;
		
		memcpy(DiversData, pTmpPtr, dwTmpLen);
		DiversDataLen += dwTmpLen;	
		
		ilRet = TagFind(pData, wDataLen, IL_TAG_DF27, &dwTmpLen, &pTmpPtr, 0);
		if(ilRet)
			return ilRet;
		
		memcpy(&DiversData[DiversDataLen], pTmpPtr, dwTmpLen);	
		DiversDataLen += dwTmpLen;	
		
        MKDF_GOST(MK_sm_id_gost, DiversData, DiversDataLen, MK_sm_id_gost);
    }

	//Make SK
	KDF_GOST(MK_sm_id_gost, SM_Challenge, rand_len, SK32);

	
    //Make SK smi.id.des
    KDF_GOST(SK32, R1, sizeof(R1), hCrypto->SM.SKsmcidgost);
    //Make SK smc.id.des
    KDF_GOST(SK32, R2, sizeof(R2), hCrypto->SM.SKsmiidgost);
	
	//calc T
	Gost28147_Imit(hCrypto->SM.SKsmiidgost, pSessionDataIn->IcChallenge, rand_len,  T); 

    pSessionDataOut->CardCryptogrammLength = rand_len + 4;

	if(AppVer == UECLIB_APP_VER_10)
	{
	    //MICRON SPECIFIC!!!
	    cmnMemCopy(&pSessionDataOut->CardCryptogramm[0], HostChallenge, rand_len);
	    cmnMemCopy(&pSessionDataOut->CardCryptogramm[rand_len], T, 4);
    }
    else
    {
	    cmnMemCopy(&pSessionDataOut->CardCryptogramm[0], T, 4);
	    cmnMemCopy(&pSessionDataOut->CardCryptogramm[4], HostChallenge, rand_len);
    }
    
	//Init session SM counter
	cmnMemCopy(hCrypto->SM.SessionSmCounter, SM_Challenge, 8);

    if(AppVer == UECLIB_APP_VER_10)
    {
	    //MICRON SPECIFIC!!!
	    SubFromSessionSmCounter(hCrypto->SM.SessionSmCounter, 1);
    }
#ifndef GET_DATA_VALUE_WITH_TAG 
    else
    {
	    SubFromSessionSmCounter(hCrypto->SM.SessionSmCounter, 1);
    }
#endif
    	
	return 0;
}

IL_FUNC IL_RETCODE hostPrepareIssuerSession(HANDLE_CRYPTO_HOST* hCrypto, IL_BYTE* pData, IL_WORD wDataLen, ISSUER_SESSION_DATA_IN* pSessionDataIn, ISSUER_SESSION_DATA_OUT* pSessionDataOut, IL_BYTE ifGost, IL_BYTE AppVer)
{
	IL_WORD RC;

	if(ifGost)
		RC = hostPrepareIssuerSessionGost(hCrypto, pData, wDataLen, pSessionDataIn, pSessionDataOut, AppVer);
	else
		RC = hostPrepareIssuerSessionRSA(hCrypto, pData, wDataLen, pSessionDataIn, pSessionDataOut, AppVer);

	return RC;
}

IL_FUNC IL_RETCODE hostCheckIssuerSession(HANDLE_CRYPTO_HOST* hCrypto, CHECK_ISSUER_SESSION_DATA_IN* pCheckSessionDataIn, IL_BYTE ifGost, IL_BYTE AppVer)
{
	IL_BYTE Tic[8] = {0};

	if(ifGost)
	{
	    if(AppVer == UECLIB_APP_VER_10)
	        Gost28147_Imit(hCrypto->SM.SKsmiidgost, pCheckSessionDataIn->HostChallenge, 8, Tic);
	    else
	        Gost28147_Imit(hCrypto->SM.SKsmiidgost, pCheckSessionDataIn->HostChallenge, 16, Tic);
	}
	else
	{
	    //calc Tic
#ifndef GET_DATA_VALUE_WITH_TAG 
        DES_RetailMAC4(pCheckSessionDataIn->HostChallenge, 16, hCrypto->SM.SKsmiiddes, Tic);
#else        
	    if(AppVer == UECLIB_APP_VER_10)
	        DES_RetailMAC4(pCheckSessionDataIn->HostChallenge, 16, hCrypto->SM.SKsmiiddes, Tic);
	    else 
	        DES_MAC4(pCheckSessionDataIn->HostChallenge, 16, hCrypto->SM.SKsmiiddes, Tic);
#endif
    }

	if(cmnMemCmp(Tic, pCheckSessionDataIn->CardCryptogramm, 4))
		return ILRET_CRYPTO_WRONG_CHK_ISS_SESS_MAC;

	return 0;
}

IL_FUNC IL_RETCODE hostSetTerminalToProviderSessionRsa(HOST_PROVIDER_SM_CONTEXT* pSM, IL_BYTE* pPrivateProviderKeyCert, IL_WORD wPrivateProviderKeyCertLen, IL_BYTE* msg, IL_DWORD msg_len, IL_BYTE* pY, IL_WORD wY_len)
{
	IL_RETCODE ilRet = 0;
	KEY_RSA KeySspidrsa;
	IL_BYTE SK[256];
	IL_WORD wSK_len;
    IL_BYTE* pACC = NULL;
    IL_DWORD dwACC = 0;
    IL_BYTE* pPAN = NULL;
    IL_DWORD dwPAN = 0;
    IL_BYTE SP_Challenge[256] = {0};
    IL_WORD wSP_ChallengeLen = 0;

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

	ilRet = cryptoRsaKeyFromCertificate(pPrivateProviderKeyCert, wPrivateProviderKeyCertLen, &KeySspidrsa);
	if(ilRet)
		return ilRet;

	//Y
	ilRet = DecryptRsa(pY, wY_len, &KeySspidrsa, SK, &wSK_len);
	if(ilRet)
		return ilRet;

	if(SK[wSK_len-16-1] != 0)
		return ILRET_CRYPTO_CRYPTO_PREPARE_SESSION;//format error

	//создаём сессионный ключ
	KDF(&SK[wSK_len-16], SP_Challenge, wSP_ChallengeLen, pSM->SK_sm_id_smc_des);

	return ilRet;
}

//IL_FUNC IL_RETCODE hostSetTerminalToProviderSessionGost(HOST_PROVIDER_SM_CONTEXT* pSM, IL_BYTE* bPubKey64, IL_BYTE* bPrivKey32, IL_BYTE* msg, IL_DWORD msg_len, IL_BYTE* bRand, IL_WORD wRand)
//{
//    return cryptoSetTerminalToProviderSessionGost(pSM, bPubKey64, bPrivKey32, msg, msg_len, bRand, wRand);
//}

IL_FUNC IL_RETCODE hostPrepareApdus(SM_CONTEXT* pSM, IL_APDU_PACK_ELEM* ilApduSequence, IL_WORD ilApduNum, IL_BYTE ifGost, IL_BYTE AppVersion)
{
	IL_RETCODE ilRet = 0;
	IL_DWORD i;

	for(i = 0; i < ilApduNum; i++)
	{
		ilRet = cryptoPrepareSM(pSM, &ilApduSequence[i].Apdu, ifGost, AppVersion);
		if(ilRet)
			break;
	}

	return ilRet;
}

IL_FUNC IL_RETCODE hostProcessApdus(SM_CONTEXT* pSM, IL_APDU_PACK_ELEM* ilApduSequence, IL_WORD ilApduNum, IL_BYTE ifGost, IL_BYTE AppVersion)
{
	IL_RETCODE ilRet = 0;
	IL_DWORD i;

	for(i = 0; i < ilApduNum; i++)
	{
		AddToSessionSmCounter(pSM->SessionSmCounter, 2); 

		ilRet = cryptoProcessSM(pSM, &ilApduSequence[i].Apdu, ifGost, AppVersion);
		if(ilRet)
			break;
	}

	return ilRet;
}


IL_FUNC IL_RETCODE hostCheckAuthRequestOffline(IL_BYTE* pData, IL_WORD wDataLen, IL_BYTE ifGost, IL_BYTE AppVer)
{
   	IL_DWORD ilRet = 0;
	IL_INT i;
    IL_DWORD dwTmpLen;
    IL_BYTE* pTmpPtr;
    IL_DWORD dwAppPubCertLen;
    IL_BYTE* pAppPubCertPtr;

	IL_TAG tags10[] = {IL_TAG_9F15, IL_TAG_9F1C, IL_TAG_9F37, IL_TAG_9F21, IL_TAG_9F03, IL_TAG_DF02, IL_TAG_9F36, IL_TAG_9F35};  
	IL_TAG tags11[] = {IL_TAG_9F15, IL_TAG_9F37, IL_TAG_9F21, IL_TAG_9F03, IL_TAG_DF02, IL_TAG_9F1C, IL_TAG_9F1D, IL_TAG_9F36, IL_TAG_9F35, IL_TAG_DF28};  
    IL_TAG* tags = (AppVer == UECLIB_APP_VER_10)? tags10 : tags11;
    IL_BYTE tag_num = (AppVer == UECLIB_APP_VER_10)?(sizeof(tags10)/sizeof(tags10[0])):(sizeof(tags11)/sizeof(tags11[0]));
    IL_TAG tag_mac = (AppVer == UECLIB_APP_VER_10)? IL_TAG_9F26 : IL_TAG_5F74;

    IL_BYTE msg[8192] = {0};
    IL_WORD ofs = 0;

    KEY_RSA key = {0};
    KEY_GOST_PUB key_gost_pub = {0};

    //извлекаем и проверяем сертификат открытого ключа приложения
    //для упрощения считаем что сертификат приложения всегда первый
    ilRet = TagFind(pData, wDataLen, IL_TAG_7F21, &dwAppPubCertLen, &pAppPubCertPtr, 1);
    if(ilRet)
        return ilRet;
        
    if(!ifGost)
    {
        ilRet = RsaKeyFromCertificateEx(IL_TAG_7F4E, pAppPubCertPtr, dwAppPubCertLen, &key);
        if(ilRet)
            return ilRet;
    }
    else
    {
	    ilRet = GostKeyFromCertificateEx(IL_TAG_7F4E, pAppPubCertPtr, dwAppPubCertLen, &key_gost_pub);
	    if(ilRet)
		    return ilRet;
    }
/*  демонстрация полного функционала хоста выходит за рамки работ по ИБТ  
    if((pCertPtr + dwCertLen) > (pData + wDataLen))
        return ILRET_DATA_TAG_NOT_FOUND;
        
    //извлекаем и проверяем сертификат эмитента
    ilRet = TagFind(pCertPtr + dwCertLen, (pData + wDataLen) - (pCertPtr + dwCertLen), IL_TAG_7F21, &dwTmpLen, &pTmpPtr, 1);
    if(ilRet)
        return ilRet;
        
    //здесь должны были быть проверка цепочки сертификатов
    
    //здесь должны были быть сверка PANа посылки и PANа из сертификата приложения
*/
    //формируем данные для аутентификации 
	for(i = 0; i < tag_num; i++)
    {
		ilRet = TagFind(pData, wDataLen, tags[i], &dwTmpLen, &pTmpPtr, 1);
        if(ilRet)
            return ilRet;

		cmnMemCopy(&msg[ofs], pTmpPtr, dwTmpLen);
		ofs += dwTmpLen;
	}
    
    ilRet = TagFind(pData, wDataLen, tag_mac, &dwTmpLen, &pTmpPtr, 0); 
    if(ilRet)
        return ilRet;

    if(ifGost)
    {
        if(dwTmpLen != 64)
        {
            ilRet = ILRET_CRYPTO_WRONG_DATA_LENGTH;
            return ilRet;
        }
        
        if(AppVer == UECLIB_APP_VER_10)
        {
   	        //MICRON SPECIFIC!!! REMOVE!!!
	        TagFind(pData, wDataLen, IL_TAG_9F36, &dwTmpLen, &pTmpPtr, 1);
	        dwTmpLen = pTmpPtr - pData;
    	    
	        ofs = AddTag(IL_TAG_5F70, pData, dwTmpLen, msg);
       		
   		    ilRet = TagFind(pData, wDataLen, IL_TAG_9F36, &dwTmpLen, &pTmpPtr, 1);
		    if(ilRet)
			    return ilRet;
		    cmnMemCopy(&msg[ofs], pTmpPtr, dwTmpLen);
		    ofs += dwTmpLen;
    		
   		    ilRet = TagFind(pData, wDataLen, IL_TAG_9F35, &dwTmpLen, &pTmpPtr, 1);
		    if(ilRet)
			    return ilRet;
    			
		    cmnMemCopy(&msg[ofs], pTmpPtr, dwTmpLen);
		    ofs += dwTmpLen;
   	        //MICRON SPECIFIC!!! REMOVE!!!
   	        
            ilRet = GostR3410_2001_Verify(msg, ofs, key_gost_pub.key, pTmpPtr);
		}
        else
        {
            ilRet = GostR3410_2001_Verify11(msg, ofs, key_gost_pub.key, pTmpPtr);
        }
    }
    else
	    ilRet = cryptoVerifyRsaSignature(msg, ofs, pTmpPtr, (IL_WORD)dwTmpLen, &key);

    return ilRet;
}

IL_FUNC IL_RETCODE hostSetTerminalToProviderSessionGost11_Ex(HOST_PROVIDER_SM_CONTEXT* pSM, 
															   IL_BYTE* Msg, IL_DWORD dwMsgLen, 
															   IL_BYTE* TerminalRandom, IL_WORD wTerminalRandomLen, 
															   IL_BYTE* PrivateKey32,
															   IL_BYTE* PublicKey64, 
															   PROVIDER_SESSION_DATA* pSessDataOut)
{
	IL_RETCODE ilRet = 0;
	IL_BYTE SK32[32];
	IL_BYTE* pACC = NULL;
	IL_DWORD dwACC = 0;
	IL_BYTE* pPAN = NULL;
	IL_DWORD dwPAN = 0;
	IL_BYTE SP_Challenge[256] = {0};
	IL_WORD wSP_ChallengeLen = 0;
	IL_BYTE R1[] = {0x00,0x00,0x00,0x01};
	IL_BYTE R2[] = {0x00,0x00,0x00,0x02};
/*
	//TODO REMOVE!!!
	IL_BYTE test_data[8] = {0xC7,0x7D,0x5E,0xFC,0x9C,0xA6,0x0D,0x6A};
	IL_BYTE test_enc_data[8] = {0};
	IL_BYTE test_key[32] = {0xCD,0x10,0xF7,0x58,0xBC,0xCF,0x8C,0x38,0x3D,0x74,0x31,0xB5,0x02,0x17,0x54,0xDE,0x98,0xCE,0x9D,0xAC,0x76,0x27,0xBF,0x7E,0x56,0xF5,0x65,0xF9,0xF7,0x27,0x8C,0x2B};
	Gost28147_EncryptECB(test_key,test_data,8,test_enc_data);
*/
	//поиск PAN    
	ilRet = TagFind(Msg, dwMsgLen, IL_TAG_5A, &dwPAN, &pPAN, 0);
	if(ilRet)
		return ilRet;

	//поиск ACC
	ilRet = TagFind(Msg, dwMsgLen, IL_TAG_9F36, &dwACC, &pACC, 0);
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
/*
	//TODO REMOVE!!!!!
	//C1D003AF5C3C2461107E3DFD4491F6086FE28ACA3D7BB92BC31FEC5EF1BA3B00A55C782DC42FBF9C7D57E91DB2B08640F7A2B9070AEC3EC0257A712F8B4067D1
	hex2bin("79243246A094599FA6D868916E7FABAA", TerminalRandom, &dwACC);
	wTerminalRandomLen = dwACC;
	hex2bin("C4899228B6CA9DFED392E95AC1A1DDE842A2E45ACA092A31071DA8283605C6D0", PrivateKey32, &dwACC);
	//SP_Challenge[10] = 0x00;
	//SP_Challenge[11] = 0x0A;
*/
	toHostLog("TermRandom", TerminalRandom, wTerminalRandomLen);
	toHostLog("SP_Challenge", SP_Challenge, wSP_ChallengeLen);
	toHostLog("PrivateKey32", PrivateKey32, 32);
	toHostLog("PublicKey64", PublicKey64, 64);

	//Получение сессионного ключа из приватного ключа провайдера услуг и публичного ключа терминала 
	ilRet = GostR3410_2001_KeyMatching11(PrivateKey32, PublicKey64, TerminalRandom, wTerminalRandomLen, SP_Challenge, wSP_ChallengeLen, SK32);
	if(ilRet)
		return ilRet;

	toHostLog("SK32", SK32, sizeof(SK32));

	//создаём сессионный ключ
	KDF_GOST(SK32, R2, sizeof(R2), pSM->SK_sm_id_smc_gost);
	toHostLog("SK_sm_id_smi_gost", pSM->SK_sm_id_smc_gost, sizeof(pSM->SK_sm_id_smc_gost));
	KDF_GOST(SK32, R1, sizeof(R1), pSM->SK_sm_id_smc_gost);
	toHostLog("SK_sm_id_smc_gost", pSM->SK_sm_id_smc_gost, sizeof(pSM->SK_sm_id_smc_gost));

	if(pSessDataOut != NULL)
	{
		pSessDataOut->Random_len = wTerminalRandomLen;
		cmnMemCopy(pSessDataOut->Random, TerminalRandom, wTerminalRandomLen);
	}

	return ilRet;
}

IL_FUNC IL_RETCODE hostPrepareServiceProviderSession(HOST_PROVIDER_SM_CONTEXT* pSM, IL_BYTE* msg, IL_DWORD msg_len, PROVIDER_SESSION_DATA* sess_data, IL_BYTE ifGost)
{
	IL_DWORD ilRet = 0;
	IL_BYTE PrivateProviderKeyCert[1024] = {0};
	IL_DWORD dwPrivateProviderKeyCertLen = 0;

	ilRet = prmGetParameterHost(ifGost ? IL_PARAM_S_SP_ID_GOST : IL_PARAM_S_SP_ID_RSA, PrivateProviderKeyCert, &dwPrivateProviderKeyCertLen);
	if(ilRet)
		return ilRet;
	
	if(!ifGost)
		ilRet = hostSetTerminalToProviderSessionRsa(pSM, PrivateProviderKeyCert, dwPrivateProviderKeyCertLen, msg, msg_len, sess_data->Y, sess_data->Y_len);
	else
	{
	    KEY_GOST_PUB KeyGOST = {0};
	    
	    ilRet = cryptoGostKeyFromCertificate(PrivateProviderKeyCert, dwPrivateProviderKeyCertLen, &KeyGOST);
        if(ilRet)
	        return ilRet;
	        
		ilRet = hostSetTerminalToProviderSessionGost11_Ex(pSM, msg, msg_len, sess_data->Random, sess_data->Random_len, KeyGOST.key, sess_data->Pidgost, sess_data);
    }
	return ilRet;
}

IL_FUNC IL_RETCODE hostAuthServiceProvider(IL_BYTE* Msg, IL_WORD wMsgLen, IL_BYTE* S, IL_WORD* pwS_len, IL_BYTE ifGost, IL_BYTE AppVer)
{
	IL_RETCODE ilRet = 0;
	IL_BYTE PrivateProviderKeyCert[1024] = {0};
	IL_DWORD dwPrivateProviderKeyCertLen = 0;
	
	ilRet = prmGetParameterHost((!ifGost) ? IL_PARAM_S_SP_ID_RSA : IL_PARAM_S_SP_ID_GOST, PrivateProviderKeyCert, &dwPrivateProviderKeyCertLen);
	if(ilRet)
		return ilRet;

	ilRet = cryptoCalcMessageSignature(Msg, wMsgLen, PrivateProviderKeyCert, dwPrivateProviderKeyCertLen, S, pwS_len, ifGost, AppVer);

	return ilRet;
}

//part 3, пункт 3.5.1, стр. 37
IL_FUNC IL_RETCODE hostPrepareAuthResponse(IL_BYTE* pInData, IL_WORD wInDataLen, IL_BYTE* pOutData, IL_WORD* pwOutDataLen, IL_BYTE ifGost, IL_BYTE AppVer)
{
	IL_RETCODE ilRet = 0;
	IL_INT i, j;
	IL_WORD ofs = 0;
	IL_BYTE Tmp[2048];
	IL_DWORD dwTmp;
	
    IL_BYTE* pTmpPtr;
    IL_BYTE PrivateCaKeyCert[1024] = {0};
	IL_DWORD dwPrivateCaKeyCertLen = 0;
	IL_BYTE S[1024];
	IL_WORD wS_len;
	
	IL_TAG tags10[] = {IL_TAG_9F27, IL_TAG_5A, IL_TAG_9F36, IL_TAG_9F03, IL_TAG_9F35, IL_TAG_9F1C, IL_TAG_9F13};  
	IL_TAG tags_out10[] = {IL_TAG_9F27, IL_TAG_9F13};  
	IL_TAG tags11[] = {IL_TAG_9F38, IL_TAG_9F1C, IL_TAG_9F1D, IL_TAG_9F37, IL_TAG_9F15, IL_TAG_5A, IL_TAG_DF27, IL_TAG_9F13, IL_TAG_9F36, IL_TAG_9F35, IL_TAG_DF02, IL_TAG_DF74, IL_TAG_9F27};  
	IL_TAG tags_out11[] = {IL_TAG_9F38, IL_TAG_9F13, IL_TAG_DF74, IL_TAG_9F27};  
	IL_TAG* tags = (AppVer == UECLIB_APP_VER_10)? tags10:tags11;
	IL_TAG* tags_out = (AppVer == UECLIB_APP_VER_10)? tags_out10:tags_out11;
	WORD tags_size = (AppVer == UECLIB_APP_VER_10)? (sizeof(tags10)/sizeof(tags10[0])):(sizeof(tags11)/sizeof(tags11[0]));
	WORD tags_out_size = (AppVer == UECLIB_APP_VER_10)? (sizeof(tags_out10)/sizeof(tags_out10[0])):(sizeof(tags_out11)/sizeof(tags_out11[0]));
	IL_TAG param_id_out10[] = {IL_PARAM_AAC, IL_PARAM_IDENT_OP_ID}; 
	IL_TAG param_id_out11[] = {IL_PARAM_MEMBER_ID, IL_PARAM_IDENT_OP_ID, IL_PARAM_PAYMENT_INFO, IL_PARAM_AAC}; 
	IL_TAG* param_id_out = (AppVer == UECLIB_APP_VER_10)? param_id_out10:param_id_out11;
    
  	//формируем данные ответа
    *pwOutDataLen = 0;
    
    for(i = 0; i < tags_out_size; i++)
    {
        //получаем данные ответа, в нашем случае они берутся из ini файла
	    ilRet = prmGetParameterHost(param_id_out[i], Tmp, &dwTmp);
	    if(ilRet)
	        return ilRet;
	        
	    //добавляем их в выходные TLV данные    
	    dwTmp = AddTag(tags_out[i],  Tmp, dwTmp, &pOutData[ofs]);
	    ofs += dwTmp;
    }
    *pwOutDataLen = ofs;
    
  	//формируем данные результата аутентификации
  	ofs = 0;
	for(i = 0; i < tags_size; i++)
	{
	    //ищем тэг в списке выходных данных
	    for(j = 0; j < tags_out_size; j++)
	    {  
	        if(tags[i] == tags_out[j])
	            break;
	    }
	    
	    //если не найден делаем поиск во входных данных
	    if(j == tags_out_size)
    		ilRet = TagFind(pInData, wInDataLen, tags[i], &dwTmp, &pTmpPtr, 1);
    	else //иначе ищем в выходных данных
    		ilRet = TagFind(pOutData, *pwOutDataLen, tags[i], &dwTmp, &pTmpPtr, 1);
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
				if((ilRet = TagFind(pInData, wInDataLen, IL_TAG_7F21, &dw7F21, &p7F21, 0)))
					return ilRet;
				// извлекаем сертифицируемые данные из сертификата открытого ключа
				if((ilRet = TagFind(p7F21, dw7F21, IL_TAG_7F4E, &dw7F4E, &p7F4E, 0)))
					return ilRet;
				// получаем указатель на конкатенированный элемент 5F20 (PAN+SNILS)
				if((ilRet = TagFind(p7F4E, dw7F4E, IL_TAG_5F20, &dw5F20, &p5F20, 0))) 
					return ilRet;
				if(tags[i] == IL_TAG_5A) 
					// добавляем PAN(5A)
					ofs += AddTag(IL_TAG_5A, p5F20, 10, &Tmp[ofs]);
				else
					// добавляем СНИЛС(DF27)
					ofs += AddTag(IL_TAG_DF27, p5F20+10, 6, &Tmp[ofs]);
				continue;
			}
			else
			{
				return ilRet;
			}
		}
			
		cmnMemCopy(&Tmp[ofs], pTmpPtr, dwTmp);
		ofs += dwTmp;
	}
    
	ilRet = prmGetParameterHost(ifGost ? IL_PARAM_S_CA_ID_GOST : IL_PARAM_S_CA_ID_RSA, PrivateCaKeyCert, &dwPrivateCaKeyCertLen);
	if(ilRet)
		return ilRet;

    ilRet = cryptoCalcMessageSignature(Tmp, ofs, PrivateCaKeyCert, dwPrivateCaKeyCertLen, S, &wS_len, ifGost, AppVer);	
	if(ilRet)
		return ilRet;

    dwTmp = AddTag( IL_TAG_9F4B, S, wS_len, &pOutData[*pwOutDataLen]);
    *pwOutDataLen += dwTmp;
	
	return ilRet;
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// ДЛЯ ТЕСТИРОВАНИЯ ЭМУЛЯТОРА СЕРВИСА ФУО
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
IL_WORD _apdu2bin(IL_APDU_PACK_ELEM *apdu, IL_BYTE *bin, IL_WORD *bin_size, IL_WORD max_size)
{
	// конвертируем Cmd (4 байта)
	if(*bin_size + 4 > max_size)
		return ILRET_OPLIB_APDUPACKET_BUF_IS_OVER;
	cmnMemCopy(&bin[*bin_size], apdu->Apdu.Cmd, 4);
	*bin_size += 4;

	// конвертируем LengthIn (4 байта)
	if(*bin_size + sizeof(apdu->Apdu.LengthIn) > max_size)
		return ILRET_OPLIB_APDUPACKET_BUF_IS_OVER;
	dw2bin(apdu->Apdu.LengthIn, &bin[*bin_size]);
	*bin_size += sizeof(apdu->Apdu.LengthIn);

	// конвертируем LengthExpected (4 байта)
	if(*bin_size + sizeof(apdu->Apdu.LengthExpected) > max_size)
		return ILRET_OPLIB_APDUPACKET_BUF_IS_OVER;
	dw2bin(apdu->Apdu.LengthExpected, &bin[*bin_size]);
	*bin_size += sizeof(apdu->Apdu.LengthExpected);

	// конвертируем входные данные
	if(*bin_size + apdu->Apdu.LengthIn > max_size)
		return ILRET_OPLIB_APDUPACKET_BUF_IS_OVER;
	cmnMemCopy(&bin[*bin_size], apdu->Apdu.DataIn, apdu->Apdu.LengthIn);
	*bin_size += apdu->Apdu.LengthIn;

	// конвертируем длину допустимых ответов
	if(*bin_size + sizeof(apdu->allowed_res_len) > max_size)
		return ILRET_OPLIB_APDUPACKET_BUF_IS_OVER;
	bin[*bin_size] = apdu->allowed_res_len;
	*bin_size += sizeof(apdu->allowed_res_len);

	// конвертируем массив допустимых ответов карты
	if(*bin_size + apdu->allowed_res_len*2 > max_size)
		return ILRET_OPLIB_APDUPACKET_BUF_IS_OVER;
	cmnMemCopy(&bin[*bin_size], apdu->allowed_res, apdu->allowed_res_len*2);
	*bin_size += (apdu->allowed_res_len*2);

	return 0;
}

IL_WORD Apdus2Bin(IL_APDU_PACK_ELEM *apdus, IL_WORD pack_size, IL_BYTE *bin, IL_WORD *bin_size)
{
	IL_WORD RC;
	IL_WORD i;
	IL_WORD bin_max;

	if(!apdus || !bin || !bin_size)
		return ILRET_OPLIB_INVALID_ARGUMENT;

	bin_max = *bin_size;

	for(*bin_size = i = 0; i < pack_size; i++)
	{
		if((RC = _apdu2bin(&apdus[i], bin, bin_size, bin_max)))
			return RC;
	}

	return 0;
}

IL_BYTE* _bin2apduout(IL_BYTE *bin, IL_APDU_PACK_ELEM *apdu)
{
	// конвертируем 4 байта Cmd
	cmnMemCopy(apdu->Apdu.Cmd, bin, 4);
	bin += 4;

	// конвертируем SW1 (1 байт)
	cmnMemCopy(&apdu->Apdu.SW1, bin, 1);
	bin += 1;

	// конвертируем SW2 (1 байт)
	cmnMemCopy(&apdu->Apdu.SW2, bin, 1);
	bin += 1;

	// конвертируем LengthOut (4 байта)
	bin2dw(bin, &apdu->Apdu.LengthOut);
	bin += 4;

	// конвертируем выходные данные
	cmnMemCopy(apdu->Apdu.DataOut, bin, apdu->Apdu.LengthOut);
	bin += apdu->Apdu.LengthOut;

	return bin;
}

IL_WORD Bin2Apdus(IL_BYTE *bin, IL_APDU_PACK_ELEM *apdus, IL_WORD pack_size)
{
	IL_WORD i;
	IL_BYTE *pB;
	
	for(pB = bin, i = 0; i < pack_size; i++)
		pB = _bin2apduout(pB, &apdus[i]);

	return 0;
}


#define INS_CHANGE_DATA	0x24
#define INS_RST_CNTR	0x2C
// Формирование бинарного массива зашифрованного пакета APDU-команд для разблокировкм КРП
IL_FUNC IL_RETCODE hostPrepareUnlockPukApdus(HANDLE_CRYPTO_HOST* hCrypto, 
											 IL_BYTE *pAuthRequest, IL_WORD wAuthRequestLen,
											 IL_BYTE ifGostCrypto, IL_BYTE AppVer,
											 IL_BYTE *out_ApduIn, IL_WORD *out_ApduInLen)
{
	IL_WORD ilRet;
	IL_BYTE *p9F03;
    IL_DWORD dwLen9F03; 
	IL_APDU_PACK_ELEM ApduPacket[3] = { 0 };
	IL_BYTE ApduPacketSize = 0;
	IL_BYTE allowed_res_INS_RST_CNTR[] = { 0x90,0x00, 0x67,0x00, 0x69,0x87, 0x69,0x88, 0x6A,0x86, 0x6A,0x88 };
	IL_BYTE allowed_res_INS_CHANGE_DATA[] = { 0x90,0x00, 0x67,0x00, 0x69,0x82, 0x69,0x87, 0x69,0x88, 0x6A,0x86, 0x6A,0x88 };


	// настроим указатель на ПИН-блок с временным значение КРП
	if((ilRet = TagFind(pAuthRequest, (DWORD)wAuthRequestLen, IL_TAG_9F03, &dwLen9F03, &p9F03, 0)))
		return ilRet;

	// формируем пакет APDU-команд на разблокировку КРП
	// 1) Change PUK
	cmnMemSet((IL_BYTE*)&ApduPacket[ApduPacketSize], 0, sizeof(IL_APDU_PACK_ELEM));
	ApduPacket[ApduPacketSize].Apdu.Cmd[1] = INS_CHANGE_DATA;
	ApduPacket[ApduPacketSize].Apdu.Cmd[2] = 0x01;
	ApduPacket[ApduPacketSize].Apdu.Cmd[3] = 0x05; //PUK
	ApduPacket[ApduPacketSize].Apdu.LengthIn = (IL_WORD)dwLen9F03; // TmpPUK
	cmnMemCopy(ApduPacket[ApduPacketSize].Apdu.DataIn, p9F03, dwLen9F03);
	// возможные ответы
	if((ApduPacket[ApduPacketSize].allowed_res_len = sizeof(allowed_res_INS_CHANGE_DATA) / 2) > ALLOWED_RES_MAX)
		return ILRET_APDU_ALLOWED_RES_IS_OVER;
	cmnMemCopy(ApduPacket[ApduPacketSize++].allowed_res, allowed_res_INS_CHANGE_DATA, sizeof(allowed_res_INS_CHANGE_DATA)); 

	// 2) Unlock PUK
	cmnMemSet((IL_BYTE*)&ApduPacket[ApduPacketSize], 0, sizeof(IL_APDU_PACK_ELEM));
	ApduPacket[ApduPacketSize].Apdu.Cmd[1] = INS_RST_CNTR;
	ApduPacket[ApduPacketSize].Apdu.Cmd[2] = 0x03;
	ApduPacket[ApduPacketSize].Apdu.Cmd[3] = 0x05; //PUK
	// возможные ответы
	if((ApduPacket[ApduPacketSize].allowed_res_len = sizeof(allowed_res_INS_RST_CNTR) / 2) > ALLOWED_RES_MAX)
		return ILRET_APDU_ALLOWED_RES_IS_OVER;
	cmnMemCopy(ApduPacket[ApduPacketSize++].allowed_res, allowed_res_INS_RST_CNTR, sizeof(allowed_res_INS_RST_CNTR)); 

	// подготавливаем шифрованный пакет APDU-комманд
	if((ilRet = hostPrepareApdus(&hCrypto->SM, ApduPacket, ApduPacketSize, ifGostCrypto, AppVer)))
		return ilRet;

	// преобразуем зашифрованный пакет APDU-комманд в выходной бинарный массив
	if((ilRet = Apdus2Bin(ApduPacket, ApduPacketSize, out_ApduIn, out_ApduInLen))) 
		return ilRet; 

	return 0;
}


typedef struct
{
	IL_DWORD	TagId;		   
	IL_WORD		Type;		
	IL_WORD		MaxLen;		
} _DATA_DESCR;

#define INS_SELECT		0xA4
#define INS_PUT_DATA	0xDA

IL_FUNC IL_RETCODE hostPrepareEditPrivateDataApdus(IL_TAG TagId, IL_CHAR *Data, IL_BYTE *out_ApduIn, IL_WORD *out_ApduInLen)
{
	IL_WORD ilRet = 0;
	IL_APDU_PACK_ELEM ApduPacket[3] = { 0 };
	IL_BYTE sec1Id[] = { 0xDF, 0x01 };
	IL_BYTE block4Id[] = { 0xEF, 0x04 };
	IL_BYTE allowed_res_SELECT[]  = { 0x90,0x00, 0x62,0x83, 0x67,0x00, 0x6A,0x82, 0x6A,0x86 };
	IL_BYTE allowed_res_PUTDATA[] = { 0x90,0x00, 0x62,0x83, 0x67,0x00, 0x69,0x81, 0x69,0x82, 0x69,0x86, 0x6A,0x84 };
	IL_BYTE ApduPacketSize = 0;
	_DATA_DESCR *pDataDescr;
	static _DATA_DESCR DataDescr13[] =
	{
		{ 0x5F42, 0, 42 },	// Адрес
		{ 0xDF25, 0, 14 },	// Телефон
		{ 0xDF26, 0, 15 },	// E-mail
		{ 0, 0, 0 }
	};
	IL_BYTE Bin[1024];
	IL_WORD wTag;


	if(!Data || !out_ApduIn || !out_ApduInLen) 
		return ILRET_OPLIB_INVALID_ARGUMENT;

	// найдём описатель данного
	for(pDataDescr = DataDescr13; pDataDescr->TagId != 0x00; pDataDescr++)
		if(pDataDescr->TagId == TagId)
			break;
	if(pDataDescr->TagId == 0)
		return ILRET_OPLIB_DATA_DESCR_NOT_FOUND;

	// 1) формируем команду - SELECT для выбора сектора 1
	cmnMemSet((IL_BYTE*)&ApduPacket[ApduPacketSize], 0, sizeof(IL_APDU_PACK_ELEM));	
	ApduPacket[ApduPacketSize].Apdu.Cmd[1] = INS_SELECT;
	ApduPacket[ApduPacketSize].Apdu.Cmd[2] = 0x00;
	ApduPacket[ApduPacketSize].Apdu.Cmd[3] = 0x08; 
	ApduPacket[ApduPacketSize].Apdu.LengthIn = sizeof(sec1Id); 
	cmnMemCopy(ApduPacket[ApduPacketSize].Apdu.DataIn, sec1Id, sizeof(sec1Id));
	// возможные ответы
	if((ApduPacket[ApduPacketSize].allowed_res_len = sizeof(allowed_res_SELECT) / 2) > ALLOWED_RES_MAX)
		return ILRET_APDU_ALLOWED_RES_IS_OVER;
	cmnMemCopy(ApduPacket[ApduPacketSize++].allowed_res, allowed_res_SELECT, sizeof(allowed_res_SELECT)); 

	// 2) формируем команду - SELECT для выбора блока 2 в секторе 1
	cmnMemSet((IL_BYTE*)&ApduPacket[ApduPacketSize], 0, sizeof(IL_APDU_PACK_ELEM));
	ApduPacket[ApduPacketSize].Apdu.Cmd[1] = INS_SELECT;
	ApduPacket[ApduPacketSize].Apdu.Cmd[2] = 0x00;
	ApduPacket[ApduPacketSize].Apdu.Cmd[3] = 0x0C; 
	ApduPacket[ApduPacketSize].Apdu.LengthIn = sizeof(block4Id); 
	cmnMemCopy(ApduPacket[ApduPacketSize].Apdu.DataIn, block4Id, sizeof(block4Id));
	// возможные ответы
	if((ApduPacket[ApduPacketSize].allowed_res_len = sizeof(allowed_res_SELECT) / 2) > ALLOWED_RES_MAX)
		return ILRET_APDU_ALLOWED_RES_IS_OVER;
	cmnMemCopy(ApduPacket[ApduPacketSize++].allowed_res, allowed_res_SELECT, sizeof(allowed_res_SELECT)); 

	// подготавливаем устанавливаемое значение тегированного данного
	if(pDataDescr->MaxLen > sizeof(Bin))
		return ILRET_OPLIB_WRITE_DATA_LEN_IS_OVER;
	// конвертируем строку данных в бинарные данные
	cmnMemClr(Bin, sizeof(Bin));
	if(pDataDescr->Type == 1/*DATA_NUMERIC*/)
		str2bcdF(Data, Bin, pDataDescr->MaxLen);
	if(pDataDescr->Type == 4/*DATA_NUMERIC2*/)
		str2bcd0(Data, Bin, pDataDescr->MaxLen);
	else if(pDataDescr->Type == 0/*DATA_ASCII*/)
		Ansi_2_Iso8859(Data, Bin);
	else if(pDataDescr->Type == 3/*DATA_ISO5218*/)
		Ansi_2_Iso5218(Data, Bin);
	else
		return ILRET_OPLIB_ILLEGAL_DATA_TYPE;

	// формируем команду - PUTDATA для установки значения тегированного элемента данных 
	wTag = (IL_WORD)pDataDescr->TagId;
	cmnMemSet((IL_BYTE*)&ApduPacket[ApduPacketSize], 0, sizeof(IL_APDU_PACK_ELEM));
	ApduPacket[ApduPacketSize].Apdu.Cmd[1] = INS_PUT_DATA;
	ApduPacket[ApduPacketSize].Apdu.Cmd[2] = wTag>>8;
	ApduPacket[ApduPacketSize].Apdu.Cmd[3] = (IL_BYTE)wTag; 
	ApduPacket[ApduPacketSize].Apdu.LengthIn = pDataDescr->MaxLen; 
	cmnMemCopy(ApduPacket[ApduPacketSize].Apdu.DataIn, Bin, pDataDescr->MaxLen);
	// возможные ответы
	if((ApduPacket[ApduPacketSize].allowed_res_len = sizeof(allowed_res_PUTDATA) / 2) > ALLOWED_RES_MAX)
		return ILRET_APDU_ALLOWED_RES_IS_OVER;
	cmnMemCopy(ApduPacket[ApduPacketSize++].allowed_res, allowed_res_PUTDATA, sizeof(allowed_res_PUTDATA)); 

	// преобразуем зашифрованный пакет APDU-комманд в выходной бинарный массив
	if((ilRet = Apdus2Bin(ApduPacket, ApduPacketSize, out_ApduIn, out_ApduInLen))) 
		return ilRet; 

	return 0;

}
IL_FUNC IL_RETCODE hostPrepareEditIdDataApdus(HANDLE_CRYPTO_HOST* hCrypto, 
											  IL_TAG TagId, IL_CHAR *Data,
											  IL_BYTE ifGostCrypto,
											  IL_BYTE *out_ApduIn, IL_WORD *out_ApduInLen)
{
	IL_WORD ilRet = 0;
	IL_APDU_PACK_ELEM ApduPacket[3] = { 0 };
	IL_BYTE sec1Id[] = { 0xDF, 0x01 };
	IL_BYTE block2Id[] = { 0xEF, 0x02 };
	IL_BYTE allowed_res_SELECT[]  = { 0x90,0x00, 0x62,0x83, 0x67,0x00, 0x6A,0x82, 0x6A,0x86 };
	IL_BYTE allowed_res_PUTDATA[] = { 0x90,0x00, 0x62,0x83, 0x67,0x00, 0x69,0x81, 0x69,0x82, 0x69,0x86, 0x6A,0x84 };
	IL_BYTE ApduPacketSize = 0;
	_DATA_DESCR *pDataDescr;
	static _DATA_DESCR DataDescr21[] =
	{
		{ 0xDF27, 1, 6 },	// СНИЛС
		{ 0xDF2B, 4, 8 },	// Номер полиса
		{ 0x5F20, 0, 26 },	// ФИО
		{ 0xDF23, 0, 80 },	// Адрес эмитента
		{ 0x5F2B, 4, 4 },	// Дата рождения
		{ 0xDF24, 0, 100 }, // Место рождения
		{ 0x5F35, 3, 1 },	// Пол
		{ 0xDF2D, 0, 40 },  // Фамилия
		{ 0xDF2E, 0, 40 },  // Имя
		{ 0xDF2F, 0, 40 },  // Отчество
		{ 0, 0, 0 }
	};
	IL_BYTE Bin[1024];
	IL_WORD wTag;


	if(!hCrypto || !Data || !out_ApduIn || !out_ApduInLen) 
		return ILRET_OPLIB_INVALID_ARGUMENT;

	// найдём описатель данного
	for(pDataDescr = DataDescr21; pDataDescr->TagId != 0x00; pDataDescr++)
		if(pDataDescr->TagId == TagId)
			break;
	if(pDataDescr->TagId == 0)
		return ILRET_OPLIB_DATA_DESCR_NOT_FOUND;

	// 1) формируем команду - SELECT для выбора сектора 1
	cmnMemSet((IL_BYTE*)&ApduPacket[ApduPacketSize], 0, sizeof(IL_APDU_PACK_ELEM));	
	ApduPacket[ApduPacketSize].Apdu.Cmd[1] = INS_SELECT;
	ApduPacket[ApduPacketSize].Apdu.Cmd[2] = 0x00;
	ApduPacket[ApduPacketSize].Apdu.Cmd[3] = 0x08; 
	ApduPacket[ApduPacketSize].Apdu.LengthIn = sizeof(sec1Id); 
	cmnMemCopy(ApduPacket[ApduPacketSize].Apdu.DataIn, sec1Id, sizeof(sec1Id));
	// возможные ответы
	if((ApduPacket[ApduPacketSize].allowed_res_len = sizeof(allowed_res_SELECT) / 2) > ALLOWED_RES_MAX)
		return ILRET_APDU_ALLOWED_RES_IS_OVER;
	cmnMemCopy(ApduPacket[ApduPacketSize++].allowed_res, allowed_res_SELECT, sizeof(allowed_res_SELECT)); 

	// 2) формируем команду - SELECT для выбора блока 2 в секторе 1
	cmnMemSet((IL_BYTE*)&ApduPacket[ApduPacketSize], 0, sizeof(IL_APDU_PACK_ELEM));
	ApduPacket[ApduPacketSize].Apdu.Cmd[1] = INS_SELECT;
	ApduPacket[ApduPacketSize].Apdu.Cmd[2] = 0x00;
	ApduPacket[ApduPacketSize].Apdu.Cmd[3] = 0x0C; 
	ApduPacket[ApduPacketSize].Apdu.LengthIn = sizeof(block2Id); 
	cmnMemCopy(ApduPacket[ApduPacketSize].Apdu.DataIn, block2Id, sizeof(block2Id));
	// возможные ответы
	if((ApduPacket[ApduPacketSize].allowed_res_len = sizeof(allowed_res_SELECT) / 2) > ALLOWED_RES_MAX)
		return ILRET_APDU_ALLOWED_RES_IS_OVER;
	cmnMemCopy(ApduPacket[ApduPacketSize++].allowed_res, allowed_res_SELECT, sizeof(allowed_res_SELECT)); 

	// подготавливаем устанавливаемое значение тегированного данного
	if(pDataDescr->MaxLen > sizeof(Bin))
		return ILRET_OPLIB_WRITE_DATA_LEN_IS_OVER;
	// конвертируем строку данных в бинарные данные
	cmnMemClr(Bin, sizeof(Bin));
	if(pDataDescr->Type == 1/*DATA_NUMERIC*/)
		str2bcdF(Data, Bin, pDataDescr->MaxLen);
	if(pDataDescr->Type == 4/*DATA_NUMERIC2*/)
		str2bcd0(Data, Bin, pDataDescr->MaxLen);
	else if(pDataDescr->Type == 0/*DATA_ASCII*/)
		Ansi_2_Iso8859(Data, Bin);
	else if(pDataDescr->Type == 3/*DATA_ISO5218*/)
		Ansi_2_Iso5218(Data, Bin);
	else
		return ILRET_OPLIB_ILLEGAL_DATA_TYPE;

	// формируем команду - PUTDATA для установки значения тегированного элемента данных 
	wTag = (IL_WORD)pDataDescr->TagId;
	cmnMemSet((IL_BYTE*)&ApduPacket[ApduPacketSize], 0, sizeof(IL_APDU_PACK_ELEM));
	ApduPacket[ApduPacketSize].Apdu.Cmd[1] = INS_PUT_DATA;
	ApduPacket[ApduPacketSize].Apdu.Cmd[2] = wTag>>8;
	ApduPacket[ApduPacketSize].Apdu.Cmd[3] = (IL_BYTE)wTag; 
	ApduPacket[ApduPacketSize].Apdu.LengthIn = pDataDescr->MaxLen; 
	cmnMemCopy(ApduPacket[ApduPacketSize].Apdu.DataIn, Bin, pDataDescr->MaxLen);
	// возможные ответы
	if((ApduPacket[ApduPacketSize].allowed_res_len = sizeof(allowed_res_PUTDATA) / 2) > ALLOWED_RES_MAX)
		return ILRET_APDU_ALLOWED_RES_IS_OVER;
	cmnMemCopy(ApduPacket[ApduPacketSize++].allowed_res, allowed_res_PUTDATA, sizeof(allowed_res_PUTDATA)); 

	// подготавливаем шифрованный пакет APDU-комманд
	if((ilRet = hostPrepareApdus(&hCrypto->SM, ApduPacket, ApduPacketSize, ifGostCrypto, 0x11/*AppVer*/)))
		return ilRet;

	// преобразуем зашифрованный пакет APDU-комманд в выходной бинарный массив
	if((ilRet = Apdus2Bin(ApduPacket, ApduPacketSize, out_ApduIn, out_ApduInLen))) 
		return ilRet; 

	return 0;
}

IL_FUNC IL_RETCODE hostPrepareSmSessionRSA(HANDLE_CRYPTO_HOST* hCrypto, IL_BYTE* pData, IL_WORD wDataLen, SM_SESSION_DATA_IN* pSessionDataIn, SM_SESSION_DATA_OUT* pSessionDataOut)
{
	IL_RETCODE ilRet;
	IL_WORD i;
	IL_BYTE HostChallenge[16] = {0};
	IL_BYTE SK[16] = {0};
	IL_BYTE SM_Challenge[16];
	IL_BYTE R1[] = {0x00,0x00,0x00,0x01};
	IL_BYTE R2[] = {0x00,0x00,0x00,0x02};

	IL_BYTE T[4] = {0};

	IL_BYTE MK_sm_id[16];
	IL_DWORD dwTmpLen; 

	//Generate Host Challenge
	ilRet = GetHostRandom(HostChallenge);
	if(ilRet)
		return ilRet;

	// MK_sm_id считываем из файла параметров эмулятора хоста!!!
	if((ilRet = prmGetParameterHost(IL_PARAM_SE_SM_ID_RSA, MK_sm_id, &dwTmpLen)))
		return ilRet;

	for(i = 0; i<16; i++)
	{
		SM_Challenge[i] = HostChallenge[i] ^ pSessionDataIn->IcChallenge[i];
	}

	//Make SK
	KDF(MK_sm_id, SM_Challenge, sizeof(SM_Challenge), SK);

    //Make SK smi.id.des
    KDF(SK, R2, sizeof(R2), hCrypto->SM.SKsmiiddes);
    //Make SK smc.id.des
    KDF(SK, R1, sizeof(R1), hCrypto->SM.SKsmciddes);
    
	//calc T
	DES_RetailMAC4(pSessionDataIn->IcChallenge, 16, hCrypto->SM.SKsmiiddes, T);
	    
    pSessionDataOut->CardCryptogrammLength = 20;
	cmnMemCopy(pSessionDataOut->CardCryptogramm, T, 4);
	cmnMemCopy(&pSessionDataOut->CardCryptogramm[4], HostChallenge, 16);

	//Init session SM counter
	cmnMemCopy(hCrypto->SM.SessionSmCounter, SM_Challenge, 8);
	
    SubFromSessionSmCounter(hCrypto->SM.SessionSmCounter, 1);

	return 0;
}

IL_FUNC IL_RETCODE hostPrepareSmSessionGost(HANDLE_CRYPTO_HOST* hCrypto, IL_BYTE* pData, IL_WORD wDataLen, SM_SESSION_DATA_IN* pSessionDataIn, SM_SESSION_DATA_OUT* pSessionDataOut)
{
	IL_RETCODE ilRet;
	IL_WORD i;
	IL_BYTE HostChallenge[16] = {0};
	IL_BYTE SK32[32] = {0};
	IL_BYTE R1[] = {0x00,0x00,0x00,0x01};
	IL_BYTE R2[] = {0x00,0x00,0x00,0x02};
	IL_BYTE SM_Challenge[16] = {0};

	IL_BYTE T[4] = {0};
	IL_BYTE MK_sm_id_gost[32];
	IL_DWORD dwTmpLen;
    IL_BYTE rand_len = 16;
	IL_BYTE DiversFlag = 0;
// 	IL_BYTE* pTmpPtr;
  	IL_BYTE DiversData[256] = {0};
	IL_WORD DiversDataLen = 0;
 
	//Generate Host Challenge
	ilRet = GetHostRandom(HostChallenge);
	if(ilRet)
		return ilRet;

	// MK_sm_id_gost считываем из файла параметров эмулятора хоста!!!
	if((ilRet = prmGetParameterHost(IL_PARAM_SE_SM_ID_GOST, MK_sm_id_gost, &dwTmpLen)))
		return ilRet;

	for(i = 0; i < rand_len; i++)
	{
		SM_Challenge[i] = HostChallenge[i] ^ pSessionDataIn->IcChallenge[i];
	}

	//Make SK
	KDF_GOST(MK_sm_id_gost, SM_Challenge, rand_len, SK32);

	
    //Make SK smi.id.des
    KDF_GOST(SK32, R1, sizeof(R1), hCrypto->SM.SKsmcidgost);
    //Make SK smc.id.des
    KDF_GOST(SK32, R2, sizeof(R2), hCrypto->SM.SKsmiidgost);
	
	//calc T
	Gost28147_Imit(hCrypto->SM.SKsmiidgost, pSessionDataIn->IcChallenge, rand_len,  T); 

    pSessionDataOut->CardCryptogrammLength = rand_len + 4;

    cmnMemCopy(&pSessionDataOut->CardCryptogramm[0], T, 4);
    cmnMemCopy(&pSessionDataOut->CardCryptogramm[4], HostChallenge, rand_len);
    
	//Init session SM counter
	cmnMemCopy(hCrypto->SM.SessionSmCounter, SM_Challenge, 8);

    SubFromSessionSmCounter(hCrypto->SM.SessionSmCounter, 1);
    	
	return 0;
}

IL_FUNC IL_RETCODE hostPrepareSmIssuerSession(HANDLE_CRYPTO_HOST* hCrypto, SM_SESSION_DATA_IN* pSessionDataIn, SM_SESSION_DATA_OUT* pSessionDataOut, IL_BYTE ifGost)
{
	IL_WORD RC;

	if(ifGost)
		RC = hostPrepareSmSessionGost(hCrypto, NULL, 0, pSessionDataIn, pSessionDataOut);
	else
		RC = hostPrepareSmSessionRSA(hCrypto, NULL, 0, pSessionDataIn, pSessionDataOut);

	return RC;
}

IL_FUNC IL_RETCODE hostEncryptServiceProviderData(HOST_PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* EncMsg, IL_DWORD* pwMsgLenEncrypted, IL_BYTE ifGost)
{
	IL_RETCODE ilRet = 0;

	if(!ifGost)
	{
		DES3_CBC_PAD_Encrypt(pSM->SK_sm_id_smc_des, NULL, Msg, MsgLen, EncMsg, pwMsgLenEncrypted);
	}
	else
	{
		Gost28147_EncryptCBC(pSM->SK_sm_id_smc_gost, NULL, Msg, MsgLen, EncMsg, pwMsgLenEncrypted);
	}

	return ilRet;
}

IL_FUNC IL_RETCODE hostDecryptServiceProviderData(HOST_PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* DecryptedMsg, IL_DWORD* pwDecryptedMsgLen, IL_BYTE ifGost)
{
	IL_RETCODE ilRet = 0;

	if(!ifGost)
	{
		ilRet = DES3_CBC_PAD_Decrypt(pSM->SK_sm_id_smc_des, NULL, Msg, MsgLen, DecryptedMsg, pwDecryptedMsgLen);
	}
	else
	{
		ilRet = Gost28147_DecryptCBC(pSM->SK_sm_id_smc_gost, NULL, Msg, MsgLen, DecryptedMsg, pwDecryptedMsgLen);
	}

	return ilRet;
}




