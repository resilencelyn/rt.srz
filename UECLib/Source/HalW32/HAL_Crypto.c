#include "HAL_Crypto.h"
#include "HAL_Common.h"
#include "HAL_Protocol.h"
#include "HAL_Parameter.h"
#include "tlv.h"
#include "tag.h"
#include "il_error.h"
#include <time.h>
#include "ru_cryptodsb.h"
#include "com_cryptodsb.h"
#include "rsa_helper.h"
#include "il_version.h"

#ifdef SM_SUPPORT
#include "SmLib.h"
IL_FUNC IL_RETCODE smSetTerminalToProviderSession11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Cspaid_buf, IL_WORD wCspaid_size, IL_BYTE* msg, IL_DWORD msg_len, PROVIDER_SESSION_DATA* pSessDataOut, BYTE ifGost);
IL_FUNC IL_RETCODE smDecryptTerminalToProvider11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* DecryptedMsg, IL_DWORD* pwDecryptedMsgLen, IL_BYTE ifGost);
IL_FUNC IL_RETCODE smEncryptTerminalToProvider11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* EncMsg, IL_DWORD* pwMsgLenEncrypted, IL_BYTE ifGost);
#endif

IL_FUNC IL_RETCODE GetRandom(IL_BYTE* pRandom, IL_DWORD dwLen)
{
	IL_DWORD i;
	time_t t;
	srand((IL_DWORD)time(&t));

	for(i = 0; i < dwLen; i++)
		pRandom[i] = (IL_BYTE)rand();

	return 0 ;
}

void toLog(IL_CHAR* str, IL_BYTE* buf, IL_DWORD len)
{
	char tmp1[1024];
	bin2hex(tmp1, buf, len);
	OutputDebugStringA("\n");    
	OutputDebugStringA(str);
	OutputDebugStringA(": ");
	OutputDebugStringA("\n");    
	OutputDebugStringA(tmp1);
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

void SubFromSessionSmCounter(IL_BYTE* SessionSmCounter8, IL_BYTE bNumberToSub)
{
	IL_WORD tmp;
	IL_BYTE c = bNumberToSub;
	IL_WORD i;

	for(i = 0; i < 8; i++)
	{
		tmp = SessionSmCounter8[7-i] - c;
		SessionSmCounter8[7-i] = (IL_BYTE)tmp;
		c = (SessionSmCounter8[7-i] >= c)?0:1;
	}
}

// извлечениe модуля и экспоненты закрытого ключа терминала Sifd.id.rsa 
// из внешнего файла параметров 'terminal.ini'
IL_FUNC IL_RETCODE GetSifdidrsa(IL_BYTE KeyVer, KEY_RSA* pKeySifdidrsa)
{
	IL_RETCODE ilRet; 
	IL_DWORD dwTmp; 

	if((ilRet = prmGetParameter(IL_PARAM_SIFDID_MOD, pKeySifdidrsa->mod, &dwTmp)) != 0)
		return ilRet;
	pKeySifdidrsa->mod_len = (IL_WORD)dwTmp;

	if((ilRet = prmGetParameter(IL_PARAM_SIFDID_EXP, pKeySifdidrsa->exp, &dwTmp)) != 0)
		return ilRet;
	pKeySifdidrsa->exp_len = (IL_WORD)dwTmp;

	return ilRet;
}

// создание случайной компоненты общего секрета терминала
IL_FUNC IL_RETCODE GetKifd(IL_BYTE* Kifd16)
{
	return GetRandom(Kifd16, 16);
}

#ifdef SM_SUPPORT
IL_FUNC IL_RETCODE cryptoPrepareMutualAuthDataRsa(IL_HANDLE_CRYPTO hCrypto, MUTUAL_AUTH_DATA_IN* pDataIn, MUTUAL_AUTH_DATA_OUT* pDataOut, IL_BYTE KeyVer)
{
	IL_RETCODE ilRet = 0;
	IL_BYTE in[512];
	IL_BYTE out[512];
	IL_WORD out_len;
	IL_WORD offset = 0;

	cmnMemCopy(in, pDataIn->IcChallenge, 16);
	offset += 16;
	offset += (IL_WORD)AddTag(IL_TAG_81, pDataIn->KeyPicidrsa.exp, pDataIn->KeyPicidrsa.exp_len, &in[offset]);
	offset += (IL_WORD)AddTag(IL_TAG_82, pDataIn->KeyPicidrsa.mod, pDataIn->KeyPicidrsa.mod_len, &in[offset]);

	ilRet = smAuthBegin(hCrypto, in, offset, out, &out_len, 0);
	if(ilRet)
		return ilRet;

	cmnMemCopy(pDataOut->S, out, out_len/2);
	pDataOut->S_len = out_len/2;
	cmnMemCopy(pDataOut->Y, &out[out_len/2], out_len/2);
	pDataOut->Y_len = out_len/2;

	return ilRet;
}
#else
IL_FUNC IL_RETCODE cryptoPrepareMutualAuthDataRsa(IL_HANDLE_CRYPTO hCrypto, MUTUAL_AUTH_DATA_IN* pDataIn, MUTUAL_AUTH_DATA_OUT* pDataOut, IL_BYTE KeyVer)
{
	IL_RETCODE ilRet;
	KEY_RSA keySifdidrsa = {0};
	HANDLE_CRYPTO *myhCrypto = (HANDLE_CRYPTO*)hCrypto;

	// извлекаем закрытый ключ терминала Sifd.id.rsa (модуль и экспоненту) из внешнего файла параметров терминала
	ilRet = GetSifdidrsa(KeyVer, &keySifdidrsa);
	if(ilRet)
		return ilRet;

	// шифруем случайноe числo карты pDataIn->IcChallenge 
	// на закрытом ключе терминала Sifd.id.rsa => pDataOut->S (цифровая подпись)
	MakeRsaCertificate(pDataIn->IcChallenge, pDataIn->IcChallengeLength, &keySifdidrsa, pDataOut->S, &pDataOut->S_len);

	// создадим случайную компоненту общего секрета терминала => hCrypto->Kifd
	ilRet = GetKifd(myhCrypto->Kifd);
	if(ilRet)
		return ilRet;

	// шифруем случайную компоненту общего секрета терминала hCrypto->Kifd
	// на открытом ключе карты Pic.id.rsa => pDataOut->Y
	EncryptRsa(myhCrypto->Kifd, 16, &pDataIn->KeyPicidrsa, pDataOut->Y, &pDataOut->Y_len);

	return 0;
} 
#endif

#ifndef SM_SUPPORT
// извлечениe закрытого ключа терминала Sifd.id.gost из внешнего файла параметров 'terminal.ini'
IL_FUNC IL_RETCODE GetSifdidgost(IL_BYTE KeyVer, KEY_GOST_PRIV* pKeySifdidgost)
{
	IL_RETCODE ilRet; 
	IL_DWORD dwTmp; 

	ilRet = prmGetParameter(IL_PARAM_SIFDID_GOST, pKeySifdidgost->key, &dwTmp);

	return ilRet;
}
#endif

#ifndef SM_SUPPORT
IL_FUNC IL_RETCODE cryptoPrepareMutualAuthDataGost10(IL_HANDLE_CRYPTO hCrypto, MUTUAL_AUTH_DATA_IN* pDataIn, MUTUAL_AUTH_DATA_OUT* pDataOut, IL_BYTE KeyVer)
{
	IL_RETCODE ilRet = 0;
	KEY_GOST_PRIV tmp_keySifdidgost = {0};
	KEY_GOST_PUB  tmp_keyPifdidgost = {0};
	KEY_GOST_PRIV keySifdidgost = {0};
	IL_BYTE compressed_tmp_keyPifdidgost[33] = {0};
	IL_BYTE MSG[41] = {0};
	IL_BYTE S[64] = {0};
	HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)hCrypto;

	//1. Gen temporary key pair
	ilRet = GostR3410_2001_GenKeyPair(tmp_keySifdidgost.key, tmp_keyPifdidgost.key);
	if(ilRet)
		return ilRet;

	cmnMemCopy(myhCrypto->tmpSifdgost, tmp_keySifdidgost.key, 32);    
	cmnMemCopy(myhCrypto->tmpPifdgost, tmp_keyPifdidgost.key, 64);    

	//4. Compress temporary public 
	GostR3410_2001_CompressPublicKey(tmp_keyPifdidgost.key, compressed_tmp_keyPifdidgost);

	//5. Form MSG to sign
	cmnMemCopy(MSG, pDataIn->IcChallenge, 8);
	cmnMemCopy(&MSG[8], compressed_tmp_keyPifdidgost, 33);

	//6. Sign MSG on private key Sifdidgost
	ilRet = GetSifdidgost(KeyVer, &keySifdidgost);
	if(ilRet)
		return ilRet;

	ilRet = GostR3410_2001_Sign(MSG, sizeof(MSG), keySifdidgost.key, pDataOut->S);    
	if(ilRet)
		return ilRet;

	cmnMemCopy(&pDataOut->S[64], tmp_keyPifdidgost.key, 64);    
	pDataOut->S_len = 128;    

	return 0; 
} 

IL_FUNC IL_RETCODE cryptoPrepareMutualAuthDataGost11(IL_HANDLE_CRYPTO hCrypto, MUTUAL_AUTH_DATA_IN* pDataIn, MUTUAL_AUTH_DATA_OUT* pDataOut, IL_BYTE KeyVer)
{
	IL_RETCODE ilRet = 0;
	KEY_GOST_PRIV keySifdidgost = {0};

	//!!! Урезать до 4 байт согласно спецификации
	IL_BYTE TermRandom[16];

	IL_BYTE MSG[32] = {0};
	IL_WORD MSG_len = 0;
	IL_BYTE S[64] = {0};
	HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)hCrypto;

	//1. Генерируем случайное число терминала
	GetRandom(TermRandom, sizeof(TermRandom));

	//2. Формируем MSG для подписи
	cmnMemCopy(&MSG[MSG_len], TermRandom, sizeof(TermRandom));
	MSG_len += sizeof(TermRandom);
	cmnMemCopy(&MSG[MSG_len], pDataIn->IcChallenge, pDataIn->IcChallengeLength);
	MSG_len += pDataIn->IcChallengeLength;

	//6. Sign MSG on private key Sifdidgost
	ilRet = GetSifdidgost(KeyVer, &keySifdidgost);
	if(ilRet)
		return ilRet;

	ilRet = GostR3410_2001_Sign11(MSG, MSG_len, keySifdidgost.key, pDataOut->S);    
	if(ilRet)
		return ilRet;

	cmnMemCopy(&pDataOut->S[64], TermRandom, sizeof(TermRandom));    
	pDataOut->S_len = 64 + sizeof(TermRandom);    

	return 0; 
} 
#endif

#ifdef SM_SUPPORT
IL_FUNC IL_RETCODE cryptoPrepareMutualAuthDataGost(IL_HANDLE_CRYPTO hCrypto, MUTUAL_AUTH_DATA_IN* pDataIn, MUTUAL_AUTH_DATA_OUT* pDataOut, IL_BYTE KeyVer, IL_BYTE AppVer)
{
	IL_RETCODE ilRet;

	IL_BYTE in[256];
	IL_BYTE out[256];
	IL_WORD out_len;

	cmnMemCopy(in, pDataIn->IcChallenge, 16);
	cmnMemCopy(&in[16], pDataIn->KeyPicidgost.key, 64);

	ilRet = smAuthBegin(hCrypto, in, 80, out, &out_len, 1);
	if(ilRet)
		return ilRet;

	cmnMemCopy(&pDataOut->S[0], out, out_len);    
	pDataOut->S_len = out_len;    

	return ilRet;
}

#else
IL_FUNC IL_RETCODE cryptoPrepareMutualAuthDataGost(IL_HANDLE_CRYPTO hCrypto, MUTUAL_AUTH_DATA_IN* pDataIn, MUTUAL_AUTH_DATA_OUT* pDataOut, IL_BYTE KeyVer, IL_BYTE AppVer)
{
	IL_RETCODE ilRet;

	if(AppVer == UECLIB_APP_VER_10)
	{
		ilRet = cryptoPrepareMutualAuthDataGost10(hCrypto, pDataIn, pDataOut, KeyVer);
	}
	else
	{
		ilRet = cryptoPrepareMutualAuthDataGost11(hCrypto, pDataIn, pDataOut, KeyVer);
	}

	return ilRet;
}
#endif

#ifndef SM_SUPPORT
// инициализация сессионных ключей для установления защищённого соединения между терминалом и картой
IL_FUNC IL_RETCODE cryptoPrepareSessionGOST10(IL_HANDLE_CRYPTO hCrypto, SESSION_DATA_IN* pSessionDataIn)
{
	IL_RETCODE ilRet;
	IL_BYTE SK64[64] = {0};
	IL_BYTE R1[] = {0x00,0x00,0x00,0x01};
	IL_BYTE R2[] = {0x00,0x00,0x00,0x02};
	IL_BYTE T[4] = {0};
	IL_BYTE tmpPifdgost_compressed[40] = {0};
	HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)hCrypto;

	ilRet = GostR3410_2001_KeyMatching(myhCrypto->tmpSifdgost, pSessionDataIn->Data, SK64);
	if(ilRet)
		return ilRet;

	toLog("SK64", SK64, 64);

	// выводим сессионные ключи для установления защищённого соединения
	// сессионный ключ шифрования smi.id.gost
	KDF_GOST(SK64, R1, sizeof(R1), myhCrypto->SM.SKsmiidgost);
	toLog("SKsmiidgost", myhCrypto->SM.SKsmiidgost, 32);
	// сессионный ключ аутентификации smc.id.gost
	KDF_GOST(SK64, R2, sizeof(R2), myhCrypto->SM.SKsmcidgost);
	toLog("SKsmcidgost", myhCrypto->SM.SKsmcidgost, 32);

	//!!! MICRON SPECIFIC!!!
	memcpy(tmpPifdgost_compressed, myhCrypto->SM.SKsmiidgost, 32);
	memcpy(myhCrypto->SM.SKsmiidgost, myhCrypto->SM.SKsmcidgost, 32);
	memcpy(myhCrypto->SM.SKsmcidgost, tmpPifdgost_compressed, 32);
	GostR3410_2001_CompressPublicKey(myhCrypto->tmpPifdgost, tmpPifdgost_compressed);
	toLog("P'ifdgost_compressed", tmpPifdgost_compressed, 33);
	tmpPifdgost_compressed[33] = 0x80;
	toLog("P'ifdgost_compressed and blin!!! padded", tmpPifdgost_compressed, 34);
	toLog("SKsmiidgost", myhCrypto->SM.SKsmiidgost, 32);
	Gost28147_Imit(myhCrypto->SM.SKsmiidgost, tmpPifdgost_compressed, 34, T);
	toLog("T", T, 4);

	if(cmnMemCmp(T, &pSessionDataIn->Data[64], 4))
		return ILRET_CRYPTO_CRYPTO_PREPARE_SESSION;

	// инициализируем счётчик сообщений защищённого обмена
	cmnMemCopy(myhCrypto->SM.SessionSmCounter, pSessionDataIn->InitSessionSmCounter, 8);

	//MICRON SPECIFIC!!!
	SubFromSessionSmCounter(myhCrypto->SM.SessionSmCounter, 1);

	return ilRet;
}

// инициализация сессионных ключей для установления защищённого соединения между терминалом и картой
IL_FUNC IL_RETCODE cryptoPrepareSessionGOST11(IL_HANDLE_CRYPTO hCrypto, SESSION_DATA_IN* pSessionDataIn, IL_BYTE KeyVer)
{
	IL_RETCODE ilRet;
	IL_BYTE SK32[32] = {0};
	IL_BYTE R1[] = {0x00,0x00,0x00,0x01};
	IL_BYTE R2[] = {0x00,0x00,0x00,0x02};
	IL_BYTE MSG[32] = {0};
	IL_WORD MSG_len = 0;
	IL_BYTE T[4] = {0};
	HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)hCrypto;
	KEY_GOST_PRIV keySifdidgost = {0};

	ilRet = GetSifdidgost(KeyVer, &keySifdidgost);
	if(ilRet)
		return ilRet;

	ilRet = GostR3410_2001_KeyMatching11(keySifdidgost.key, pSessionDataIn->Data, 
		pSessionDataIn->TermRandom, pSessionDataIn->TermRandom_len,
		pSessionDataIn->IcChallenge, pSessionDataIn->IcChallenge_len,
		SK32);
	if(ilRet)
		return ilRet;

	toLog("SK32", SK32, 32);

	// выводим сессионные ключи для установления защищённого соединения
	// сессионный ключ шифрования smi.id.gost
	KDF_GOST(SK32, R2, sizeof(R1), myhCrypto->SM.SKsmiidgost);
	toLog("SKsmiidgost", myhCrypto->SM.SKsmiidgost, 32);
	// сессионный ключ аутентификации smc.id.gost
	KDF_GOST(SK32, R1, sizeof(R2), myhCrypto->SM.SKsmcidgost);
	toLog("SKsmcidgost", myhCrypto->SM.SKsmcidgost, 32);

	Gost28147_Imit(myhCrypto->SM.SKsmiidgost, pSessionDataIn->TermRandom, pSessionDataIn->TermRandom_len, T);
	toLog("T", T, 4);

	if(cmnMemCmp(T, &pSessionDataIn->Data[64], 4))
		return ILRET_CRYPTO_CRYPTO_PREPARE_SESSION;

	// инициализируем счётчик сообщений защищённого обмена
	cmnMemCopy(myhCrypto->SM.SessionSmCounter, pSessionDataIn->InitSessionSmCounter, 8);

#ifndef GET_DATA_VALUE_WITH_TAG 
	SubFromSessionSmCounter(myhCrypto->SM.SessionSmCounter, 1);
#endif

	return ilRet;
}
#endif

#ifdef SM_SUPPORT
IL_FUNC IL_RETCODE cryptoPrepareSessionGOST(IL_HANDLE_CRYPTO hCrypto, SESSION_DATA_IN* pSessionDataIn, IL_BYTE KeyVer, IL_BYTE AppVer)
{
	IL_RETCODE ilRet;
	HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)hCrypto;

	ilRet = smAuthComplete(hCrypto, &pSessionDataIn->Data[64], 4, 1);
	if(ilRet)
		return ilRet;

	// инициализируем счётчик сообщений защищённого обмена
	cmnMemCopy(myhCrypto->SM.SessionSmCounter, pSessionDataIn->InitSessionSmCounter, 8);

	return ilRet;
}
#else
// инициализация сессионных ключей для установления защищённого соединения между терминалом и картой
IL_FUNC IL_RETCODE cryptoPrepareSessionGOST(IL_HANDLE_CRYPTO hCrypto, SESSION_DATA_IN* pSessionDataIn, IL_BYTE KeyVer, IL_BYTE AppVer)
{
	IL_RETCODE ilRet;

	if(AppVer == UECLIB_APP_VER_10)
	{
		ilRet = cryptoPrepareSessionGOST10(hCrypto, pSessionDataIn);
	}
	else
	{
		ilRet = cryptoPrepareSessionGOST11(hCrypto, pSessionDataIn, KeyVer);
	}

	return ilRet;
}
#endif

#ifdef SM_SUPPORT
IL_FUNC IL_RETCODE cryptoPrepareSession(IL_HANDLE_CRYPTO hCrypto, SESSION_DATA_IN* pSessionDataIn, IL_BYTE KeyVer, IL_BYTE AppVer)
{
	IL_RETCODE ilRet;
	HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)hCrypto;

	ilRet = smAuthComplete(hCrypto, pSessionDataIn->Data, pSessionDataIn->Data_len, 0);
	if(ilRet)
		return ilRet;

	// инициализируем счётчик сообщений защищённого обмена
	cmnMemCopy(myhCrypto->SM.SessionSmCounter, pSessionDataIn->InitSessionSmCounter, 8);

	return ilRet;
}
#else
// инициализация сессионных ключей для установления защищённого соединения между терминалом и картой
IL_FUNC IL_RETCODE cryptoPrepareSession(IL_HANDLE_CRYPTO hCrypto, SESSION_DATA_IN* pSessionDataIn, IL_BYTE KeyVer, IL_BYTE AppVer)
{
	IL_RETCODE ilRet;
	KEY_RSA keySifdidrsa = {0};
	IL_BYTE Y1[256];
	IL_WORD Y1_len;
	IL_WORD i;
	IL_BYTE SK[16] = {0};
	IL_BYTE R1[] = {0x00,0x00,0x00,0x01};
	IL_BYTE R2[] = {0x00,0x00,0x00,0x02};
	HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)hCrypto;

	// извлекаем закрытый ключ терминала Sifd.id.rsa (модуль и экспонента)  
	ilRet = GetSifdidrsa(KeyVer, &keySifdidrsa);
	if(ilRet)
		return ilRet;

	// восстанавливаем Kifd, расшифровывая его на закрытом ключе терминала
	ilRet = DecryptRsa(pSessionDataIn->Data, pSessionDataIn->Data_len, &keySifdidrsa, Y1, &Y1_len);
	if(ilRet)
		return ILRET_CRYPTO_CRYPTO_PREPARE_SESSION;

	if(Y1[Y1_len-32-1] != 0)
		return ILRET_CRYPTO_CRYPTO_PREPARE_SESSION;//format error

	// сравниваем восстанвленный Kifd с его исходным значением
	if(cmnMemCmp(&Y1[Y1_len-32], myhCrypto->Kifd, 16))
		return ILRET_CRYPTO_CRYPTO_PREPARE_SESSION;//Ys not match error

	toLog("Kifd", &Y1[Y1_len-32], 16);
	toLog("Kic", &Y1[Y1_len-16], 16);

	// формируем общий секрет SK
	for(i = 0; i<16; i++)
	{
		SK[i] = Y1[Y1_len-32+i] ^ Y1[Y1_len-16+i];
	}
	toLog("SK", SK, 16);


	if(AppVer == UECLIB_APP_VER_10)
	{
		// выводим сессионные ключи для установления защищённого соединения
		// сессионный ключ шифрования smi.id.des
		KDF(SK, R1, sizeof(R1), myhCrypto->SM.SKsmiiddes);
		// сессионный ключ аутентификации smc.id.des
		KDF(SK, R2, sizeof(R2), myhCrypto->SM.SKsmciddes);
	}
	else
	{
		// выводим сессионные ключи для установления защищённого соединения
		// сессионный ключ шифрования smi.id.des
		KDF(SK, R2, sizeof(R2), myhCrypto->SM.SKsmiiddes);
		// сессионный ключ аутентификации smc.id.des
		KDF(SK, R1, sizeof(R1), myhCrypto->SM.SKsmciddes);
	}

	// инициализируем счётчик сообщений защищённого обмена
	cmnMemCopy(myhCrypto->SM.SessionSmCounter, pSessionDataIn->InitSessionSmCounter, 8);

#ifndef GET_DATA_VALUE_WITH_TAG 
	if(AppVer != UECLIB_APP_VER_10)
	{
		SubFromSessionSmCounter(myhCrypto->SM.SessionSmCounter, 1);
	}
#endif

	return ilRet;
}
#endif

#ifndef SM_SUPPORT
IL_FUNC IL_RETCODE PrepareSM_RSA(SM_CONTEXT* pSM, IL_APDU* pilApdu, IL_BYTE AppVer)
{
	IL_BYTE ICV[8];
	IL_BYTE buff[512];
	IL_DWORD dwDataLen=0;
	IL_WORD offset;
	IL_BYTE MAC4[4];

	toLog("Initial SessionSmCounter", pSM->SessionSmCounter, 8);

	//Add 2 to SessionSmCounter
	AddToSessionSmCounter(pSM->SessionSmCounter, 2);

	toLog("SessionSmCounter", pSM->SessionSmCounter, 8);

	cmnMemCopy(ICV, pSM->SessionSmCounter, 8);

	toLog("SMC Key", pSM->SKsmciddes, 16);

	DES3_Encrypt(ICV, pSM->SKsmciddes);
	toLog("ICV", ICV, 8);

	if(pilApdu->LengthIn)
	{
		buff[0] = 0x01;
		cmnMemCopy(&buff[1], pilApdu->DataIn, pilApdu->LengthIn);

		toLog("Data before DES3_CBC_PAD_Encrypt", &buff[1], pilApdu->LengthIn);

		DES3_CBC_PAD_Encrypt(pSM->SKsmciddes, ICV, &buff[1], (IL_WORD)pilApdu->LengthIn, &buff[1], &dwDataLen);

		toLog("Data after DES3_CBC_PAD_Encrypt", &buff[1], dwDataLen);

		dwDataLen = (IL_WORD)AddTag(0x87, buff, dwDataLen+1, pilApdu->DataIn);

		toLog("Data tag 87", pilApdu->DataIn, dwDataLen);
	}

	if(pilApdu->LengthExpected)
	{
		pilApdu->DataIn[dwDataLen++] = 0x97;
		pilApdu->DataIn[dwDataLen++] = 0x01;
		pilApdu->DataIn[dwDataLen++] = pilApdu->LengthExpected;
	}

	//prepare to MAC calc
	cmnMemSet(buff, 0, sizeof(buff));
	offset = 0;
	cmnMemCopy(&buff[offset], pSM->SessionSmCounter, 8);
	offset += 8;
	pilApdu->Cmd[0] |= 0x0C;
	cmnMemCopy(&buff[offset], pilApdu->Cmd, 4);
	offset+=4;
	buff[offset] = 0x80;
	offset+=4;
	cmnMemCopy(&buff[offset], pilApdu->DataIn, dwDataLen);
	offset += dwDataLen;
	toLog("Data for MACing", buff, offset);

	//calc MAC
#ifndef GET_DATA_VALUE_WITH_TAG 
	DES_RetailMAC4(buff, offset, pSM->SKsmiiddes, MAC4); 
#else    
	if(AppVer == UECLIB_APP_VER_10)
		DES_RetailMAC4(buff, offset, pSM->SKsmiiddes, MAC4); 
	else
		DES_MAC4(buff, offset, pSM->SKsmiiddes, MAC4); 
#endif

	toLog("MAC", MAC4, 4);

	dwDataLen += (IL_WORD)AddTag(IL_TAG_8E, MAC4, 4, &pilApdu->DataIn[dwDataLen]);
	toLog("Data with MAC", pilApdu->DataIn, dwDataLen);

	pilApdu->LengthIn = dwDataLen;

	return 0;
}

IL_FUNC IL_RETCODE PrepareSM_GOST(SM_CONTEXT* pSM, IL_APDU* pilApdu, IL_BYTE AppVer)
{
	IL_BYTE ICV[8];
	IL_BYTE buff[512];
	IL_WORD DataLen=0;
	IL_WORD offset;
	IL_BYTE MAC4[4];
	IL_DWORD dwTmp = 0;


	//   Add 2 to SessionSmCounter
	AddToSessionSmCounter(pSM->SessionSmCounter, 2);

	toLog("SessionSmCounter", pSM->SessionSmCounter, 8);

	toLog("SMC Key GOST", pSM->SKsmcidgost, 32);

	//MICRON specific!!!!
	Gost28147_EncryptECB(pSM->SKsmcidgost, pSM->SessionSmCounter, 8, ICV);

	toLog("ICV", ICV, 8);

	if(pilApdu->LengthIn)
	{
		if(AppVer == UECLIB_APP_VER_10)
			buff[0] = 0x02;
		else
			buff[0] = 0x01;

		cmnMemCopy(&buff[1], pilApdu->DataIn, pilApdu->LengthIn);

		toLog("Data before Gost28147_Encrypt", &buff[1], pilApdu->LengthIn);

		if(AppVer == UECLIB_APP_VER_10)
		{
			Gost28147_Encrypt(pSM->SKsmcidgost, &buff[1], (IL_WORD)pilApdu->LengthIn, &buff[1], ICV);
			DataLen = (IL_WORD)pilApdu->LengthIn;
		}
		else
		{
			Gost28147_EncryptCBC(pSM->SKsmcidgost, ICV, &buff[1], (IL_WORD)pilApdu->LengthIn, &buff[1], &dwTmp);
			DataLen = dwTmp;
		}

		toLog("Data after Gost28147_Encrypt", &buff[1], DataLen);

		DataLen = (IL_WORD)AddTag(IL_TAG_87, buff, DataLen+1, pilApdu->DataIn);

		toLog("Data tag 87", pilApdu->DataIn, DataLen);
	}

	if(AppVer == UECLIB_APP_VER_10)
	{
		// uncomment HERE is MICRON error!
		//      if(pilApdu->LengthExpected)
		{
			pilApdu->DataIn[DataLen++] = 0x97;
			pilApdu->DataIn[DataLen++] = 0x01;
			pilApdu->DataIn[DataLen++] = pilApdu->LengthExpected;
		}
	}
	else
	{
		if(pilApdu->LengthExpected)
		{
			pilApdu->DataIn[DataLen++] = 0x97;
			pilApdu->DataIn[DataLen++] = 0x01;
			pilApdu->DataIn[DataLen++] = pilApdu->LengthExpected;
		}
	}

	//prepare to MAC calc
	cmnMemSet(buff, 0, sizeof(buff));
	offset = 0;
	cmnMemCopy(&buff[offset], pSM->SessionSmCounter, 8);
	offset += 8;
	pilApdu->Cmd[0] |= 0x0C;
	cmnMemCopy(&buff[offset], pilApdu->Cmd, 4);
	offset+=4;
	buff[offset] = 0x80;
	offset+=4;
	cmnMemCopy(&buff[offset], pilApdu->DataIn, DataLen);
	offset += DataLen;

	//MICRON SPECIFIC!!!
	if(AppVer == UECLIB_APP_VER_10)
	{
		buff[offset] = 0x80;
		offset += 1;
	}

	toLog("Data for MACing", buff, offset);

	//calc MAC
	Gost28147_Imit(pSM->SKsmiidgost, buff, offset, MAC4); 
	toLog("MAC", MAC4, 4);

	DataLen += (IL_WORD)AddTag(IL_TAG_8E, MAC4, 4, &pilApdu->DataIn[DataLen]);
	toLog("Data with MAC", pilApdu->DataIn, DataLen);

	pilApdu->LengthIn = DataLen;

	return 0;
}
#endif

#ifdef SM_SUPPORT
IL_FUNC IL_RETCODE cryptoPrepareSM(IL_HANDLE_CRYPTO hCrypto, IL_APDU* pilApdu, IL_BYTE ifGOST, IL_BYTE AppVer)
{  
	return smPrepareSM(hCrypto, pilApdu);
}
#else
IL_FUNC IL_RETCODE cryptoPrepareSM(IL_HANDLE_CRYPTO hCrypto, IL_APDU* pilApdu, IL_BYTE ifGOST, IL_BYTE AppVer)
{  
	HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)hCrypto;

	if(ifGOST)
		return PrepareSM_GOST(&myhCrypto->SM, pilApdu, AppVer);
	else
		return PrepareSM_RSA(&myhCrypto->SM, pilApdu, AppVer);
}
#endif

#ifndef SM_SUPPORT
IL_FUNC IL_RETCODE ProcessSM(IL_HANDLE_CRYPTO hCrypto, IL_APDU* pilApdu, IL_BYTE AppVer)
{
	IL_BYTE AuthCounter[8];
	IL_BYTE ICV[8];
	IL_BYTE buff[512];
	IL_WORD DataLen=0;
	IL_WORD offset;
	IL_BYTE MAC4[4];
	IL_BYTE* pSW;
	IL_DWORD dwSW;
	IL_BYTE* pMAC;
	IL_DWORD dwMAC;
	IL_BYTE* pEncData;
	IL_DWORD dwEncData;
	IL_DWORD dwDecData = 0;
	IL_RETCODE ilRet = 0;
	HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)hCrypto;

	cmnMemCopy(AuthCounter, myhCrypto->SM.SessionSmCounter, 8);
	toLog("SessionSmCounter", myhCrypto->SM.SessionSmCounter, 8);

	//Add 1 to SessionSmCounter
	AddToSessionSmCounter(AuthCounter, 1);

	toLog("AuthCounter", AuthCounter, 8);

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
	cmnMemSet(buff, 0, sizeof(buff));
	offset = 0;
	cmnMemCopy(&buff[offset], AuthCounter, 8);
	offset += 8;
	if(dwEncData)
	{
		cmnMemCopy(&buff[offset], pEncData, dwEncData);
		offset += (IL_WORD)dwEncData;
	}
	cmnMemCopy(&buff[offset], pSW, dwSW);
	offset += (IL_WORD)dwSW;
	toLog("Data for MACing", buff, offset);

	//calc MAC
#ifndef GET_DATA_VALUE_WITH_TAG 
	DES_RetailMAC4(buff, offset, myhCrypto->SM.SKsmiiddes, MAC4); 
#else    
	if(AppVer == UECLIB_APP_VER_10)
		DES_RetailMAC4(buff, offset, myhCrypto->SM.SKsmiiddes, MAC4); 
	else
		DES_MAC4(buff, offset, myhCrypto->SM.SKsmiiddes, MAC4); 
#endif        

	toLog("MAC", MAC4, 4);

	if(cmnMemCmp(MAC4, pMAC, 4))
		return ILRET_CRYPTO_WRONG_SM_MAC;

	//игнорируем ошибки специально
	TagFind(pilApdu->DataOut, pilApdu->LengthOut, IL_TAG_87, &dwEncData, &pEncData, 0);
	if(pEncData)
	{
		if(pEncData[0]!=0x01)
			ILRET_CRD_APDU_DATA_FORMAT_ERROR;

		toLog("SMC Key", myhCrypto->SM.SKsmciddes, 16);

		cmnMemCopy(ICV, AuthCounter, 8);
		DES3_Encrypt(ICV, myhCrypto->SM.SKsmciddes);
		toLog("ICV", ICV, 8);

		toLog("Data before DES3_CBC_PAD_Decrypt", &pEncData[1], dwEncData-1);
		ilRet = DES3_CBC_PAD_Decrypt(myhCrypto->SM.SKsmciddes, ICV, &pEncData[1], dwEncData-1, &pEncData[1], &dwDecData);
		if(ilRet)
			return ilRet;

		toLog("Data after DES3_CBC_PAD_Decrypt", &pEncData[1], dwDecData);
	}

	ilRet = TagFind(pilApdu->DataOut, pilApdu->LengthOut, IL_TAG_99, &dwSW, &pSW, 0);
	if(ilRet)
		return ilRet;

	pilApdu->SW1 = pSW[0];
	pilApdu->SW2 = pSW[1];
	pilApdu->LengthOut = dwDecData;
	if(pilApdu->LengthOut)
	{
		cmnMemMove(pilApdu->DataOut, &pEncData[1], pilApdu->LengthOut);
		//clear end of buffer
		cmnMemSet(&pilApdu->DataOut[pilApdu->LengthOut], 0, sizeof(pilApdu->DataOut) - pilApdu->LengthOut);
	}
	return 0;
}

IL_FUNC IL_RETCODE ProcessSM_GOST(SM_CONTEXT* pSM, IL_APDU* pilApdu, IL_BYTE AppVer)
{
	IL_BYTE AuthCounter[8];
	IL_BYTE ICV[8];
	IL_BYTE buff[512];
	IL_WORD DataLen=0;
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
	IL_DWORD dwTmp = 0;

	cmnMemCopy(AuthCounter, pSM->SessionSmCounter, 8);
	toLog("SessionSmCounter", pSM->SessionSmCounter, 8);

	//Add 1 to SessionSmCounter
	AddToSessionSmCounter(AuthCounter, 1);

	toLog("AuthCounter", AuthCounter, 8);

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
	cmnMemSet(buff, 0, sizeof(buff));
	offset = 0;
	cmnMemCopy(&buff[offset], AuthCounter, 8);
	offset += 8;
	if(dwEncData)
	{
		cmnMemCopy(&buff[offset], pEncData, dwEncData);
		offset += (IL_WORD)dwEncData;
	}
	cmnMemCopy(&buff[offset], pSW, dwSW);
	offset += (IL_WORD)dwSW;
	toLog("Data for MACing", buff, offset);

	if(AppVer == UECLIB_APP_VER_10)
	{
		//MICRON SPECIFIC!!!!
		buff[offset] = 0x80;
		offset++;
	}

	//calc MAC
	Gost28147_Imit(pSM->SKsmiidgost, buff, offset, MAC4); 
	toLog("MAC", MAC4, 4);

	if(cmnMemCmp(MAC4, pMAC, 4))
		return ILRET_CRYPTO_WRONG_SM_MAC;

	//игнорируем ошибки специально
	TagFind(pilApdu->DataOut, pilApdu->LengthOut, IL_TAG_87, &dwEncData, &pEncData, 0);
	if(pEncData)
	{
		if(pEncData[0]!=0x01)
			ILRET_CRD_APDU_DATA_FORMAT_ERROR;

		toLog("SMC Key", pSM->SKsmcidgost, 16);

		//MICRON SPECIFIC
		Gost28147_EncryptECB(pSM->SKsmcidgost, AuthCounter, 8, ICV);

		toLog("ICV", ICV, 8);

		toLog("Data before Gost28147_Decrypt", &pEncData[1], dwEncData-1);

		if(AppVer == UECLIB_APP_VER_10)
		{
			Gost28147_Decrypt(pSM->SKsmcidgost, &pEncData[1], (IL_WORD)(dwEncData-1), &pEncData[1], ICV);
			if(ilRet)
				return ilRet;

			wDecData = dwEncData-1;
		}
		else
		{
			Gost28147_DecryptCBC(pSM->SKsmcidgost, ICV, &pEncData[1], (IL_WORD)(dwEncData-1), &pEncData[1], &dwTmp);
			if(ilRet)
				return ilRet;

			wDecData = dwTmp;
		}   

		toLog("Data after Gost28147_Decrypt", &pEncData[1], wDecData);
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

IL_FUNC IL_RETCODE cryptoProcessSM(IL_HANDLE_CRYPTO hCrypto, IL_APDU* pilApdu, IL_BYTE ifGOST, IL_BYTE AppVer)
{
	HANDLE_CRYPTO* myhCrypto = (HANDLE_CRYPTO*)hCrypto;

	if(ifGOST)
		return ProcessSM_GOST(&myhCrypto->SM, pilApdu, AppVer);
	else
		return ProcessSM(&myhCrypto->SM, pilApdu, AppVer);
}
#else

IL_FUNC IL_RETCODE cryptoProcessSM(IL_HANDLE_CRYPTO hCrypto, IL_APDU* pilApdu, IL_BYTE ifGOST, IL_BYTE AppVer)
{
	return smProcessSM(hCrypto, pilApdu);
}

#endif

#ifndef SM_SUPPORT
IL_FUNC IL_RETCODE cryptoSetTerminalToProviderSessionRsa(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Cspaid_buf, IL_WORD wCspaid_size, IL_BYTE* msg, IL_DWORD msg_len, PROVIDER_SESSION_DATA* pSessDataOut)
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
	ilRet = GetRandom(SK, sizeof(SK));
	if(ilRet)
		return ilRet;

	//Y
	EncryptRsa(SK, sizeof(SK), &KeyPspidrsa, pSessDataOut->Y, &pSessDataOut->Y_len);

	//создаём сессионный ключ
	KDF(SK, SP_Challenge, wSP_ChallengeLen, pSM->SK_sm_id_smc_des);

	return ilRet;
}

IL_FUNC IL_RETCODE cryptoSetTerminalToProviderSessionGost(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* bPubKey64, IL_BYTE* bPrivKey32, IL_BYTE* Msg, IL_DWORD dwMsgLen, IL_BYTE* bRand, IL_WORD wRandLen)
{
	IL_RETCODE ilRet = 0;
	IL_BYTE SK32[32];
	IL_BYTE* pACC = NULL;
	IL_DWORD dwACC = 0;
	IL_BYTE* pPAN = NULL;
	IL_DWORD dwPAN = 0;
	IL_BYTE SP_Challenge[256] = {0};
	IL_WORD wSP_ChallengeLen = 0;

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

	//Получение сессионного ключа из приватного ключа провайдера услуг и публичного ключа терминала 
	ilRet = GostR3410_2001_KeyMatching11(bPrivKey32, bPubKey64, bRand, wRandLen, SP_Challenge, wSP_ChallengeLen, SK32);
	if(ilRet)
		return ilRet;

	//создаём сессионный ключ
	KDF_GOST(SK32, NULL, 0, pSM->SK_sm_id_smc_gost);

	return ilRet;
}
#endif

IL_FUNC IL_RETCODE cryptoAuthServiceProvider(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_WORD wMsgLen, IL_BYTE* S, IL_WORD wS_len, IL_BYTE* Pspaid_buf, IL_WORD wPspaid_size, IL_BYTE ifGost, IL_BYTE AppVer)
{
#ifndef SM_SUPPORT
	return cryptoCheckMessageSignature(Msg, wMsgLen, S, wS_len, Pspaid_buf, wPspaid_size, ifGost, AppVer);
#else
	return smAuthServiceProvider(pSM, Msg, wMsgLen, S, wS_len, Pspaid_buf, wPspaid_size, ifGost, AppVer);
#endif	
}

void CompressKeyGOST10(IL_BYTE* PubKey64, IL_BYTE* PubKeyCompressed33)
{
	PubKeyCompressed33[0] = (PubKey64[63] & 0x01) ? 0x03 : 0x02;
	cmnMemCopy(&PubKeyCompressed33[1], PubKey64, 32);
}

void CompressKeyGOST11(IL_BYTE* PubKey64, IL_BYTE* PubKeyCompressed33)
{
	PubKeyCompressed33[0] = (PubKey64[32] & 0x01) ? 0x03 : 0x02;
	cmnMemCopy(&PubKeyCompressed33[1], PubKey64, 32);
	swap(&PubKeyCompressed33[1], 32);
}

void CompressKeyRSA(IL_BYTE* KeyRsaMod, IL_WORD KeyRsaModLen, IL_BYTE* PubKeyCompressed33)
{
	PubKeyCompressed33[0] = KeyRsaMod[0];
	cmnMemCopy(&PubKeyCompressed33[1], &KeyRsaMod[KeyRsaModLen - 32], 32);
}

IL_FUNC IL_RETCODE cryptoPublicKeyFromCertificate(IL_BYTE* Cert, IL_WORD wCertLen, IL_BYTE* pKey, IL_WORD* pwKeyLen, IL_BYTE ifGost, IL_BYTE ifCompressed)
{
	IL_RETCODE ilRet = 0;

	if(!ifGost)
	{
		KEY_RSA KeyRSA = {0};
		IL_DWORD dwLen7F21 = 0;
		IL_BYTE* p7F21 = NULL;

		ilRet = TagFind(Cert, wCertLen, IL_TAG_7F21, &dwLen7F21, &p7F21, 0);
		if(ilRet == 0)
		{
			Cert = p7F21;
			wCertLen = (IL_WORD)dwLen7F21;
		}

		ilRet = RsaKeyFromCertificateEx(IL_TAG_7F4E, Cert, wCertLen, &KeyRSA);
		if(ilRet)
			return ilRet;

		if(!ifCompressed)
		{    
			*pwKeyLen = KeyRSA.mod_len;
			if(pKey != NULL)
			{
				cmnMemCopy(pKey, KeyRSA.mod, *pwKeyLen);
			}
		}
		else
		{
			*pwKeyLen = 33;
			if(pKey != NULL)
			{

				CompressKeyRSA(KeyRSA.mod, KeyRSA.mod_len, pKey);
				/*                {
				//ATLAS SPECIFIC!!!!
				CompressKeyGOST11(KeyRSA.mod, pKey);
				swap(&pKey[1], 32);
				}
				*/
			}
		}
	}
	else
	{
		KEY_GOST_PUB KeyGOST = {0};

		IL_DWORD dwLen7F21 = 0;
		IL_BYTE* p7F21 = NULL;

		ilRet = TagFind(Cert, wCertLen, IL_TAG_7F21, &dwLen7F21, &p7F21, 0);
		if(ilRet == 0)
		{
			Cert = p7F21;
			wCertLen = (IL_WORD)dwLen7F21;
		}
		//        ilRet = cryptoGostKeyFromCertificate(Cert, wCertLen, &KeyGOST);
		ilRet = GostKeyFromCertificateEx(IL_TAG_7F4E, Cert, wCertLen, &KeyGOST);
		if(ilRet)
			return ilRet;

		if(!ifCompressed)
		{    
			*pwKeyLen = sizeof(KEY_GOST_PUB);
			if(pKey != NULL)
			{
				cmnMemCopy(pKey, KeyGOST.key, *pwKeyLen);
			}
		}
		else
		{
			*pwKeyLen = 33;
			if(pKey != NULL)
				CompressKeyGOST11(KeyGOST.key, pKey);
		}
	}

	return ilRet;
}

#ifndef SM_SUPPORT
IL_FUNC IL_RETCODE cryptoEncryptTerminalToProvider(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* EncMsg, IL_DWORD* pwMsgLenEncrypted, IL_BYTE ifGost)
{
	IL_RETCODE ilRet = 0;

	if(!ifGost)
	{
		DES3_CBC_PAD_Encrypt(pSM->SK_sm_id_smc_des, NULL, Msg, MsgLen, EncMsg, pwMsgLenEncrypted);
	}
	else
	{
		Gost28147_Encrypt(pSM->SK_sm_id_smc_gost, Msg, MsgLen, EncMsg, NULL);
		*pwMsgLenEncrypted = MsgLen;  
	}

	return ilRet;
}

IL_FUNC IL_RETCODE cryptoDecryptTerminalToProvider(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* DecryptedMsg, IL_DWORD* pdwDecryptedMsgLen, IL_BYTE ifGost)
{
	IL_RETCODE ilRet = 0;

	if(!ifGost)
	{
		ilRet = DES3_CBC_PAD_Decrypt(pSM->SK_sm_id_smc_des, NULL, Msg, MsgLen, DecryptedMsg, pdwDecryptedMsgLen);
		memmove(DecryptedMsg, Msg, *pdwDecryptedMsgLen);
	}
	else
	{
		Gost28147_Decrypt(pSM->SK_sm_id_smc_gost, Msg, MsgLen, DecryptedMsg, NULL);
		*pdwDecryptedMsgLen = MsgLen;
	}

	return ilRet;
}
#endif

#ifndef SM_SUPPORT
IL_FUNC IL_RETCODE cryptoSetTerminalToProviderSessionRsa11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Cspaid_buf, IL_WORD wCspaid_size, IL_BYTE* msg, IL_DWORD msg_len, PROVIDER_SESSION_DATA* pSessDataOut)
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
	ilRet = GetRandom(SK, sizeof(SK));
	if(ilRet)
		return ilRet;

	//Y
	EncryptRsa(SK, sizeof(SK), &KeyPspidrsa, pSessDataOut->Y, &pSessDataOut->Y_len);

	//создаём сессионный ключ
	KDF(SK, SP_Challenge, wSP_ChallengeLen, pSM->SK_sm_id_smc_des);

	return ilRet;
}
#endif

#ifndef SM_SUPPORT
IL_FUNC IL_RETCODE cryptoSetTerminalToProviderSessionGost11_Ex(PROVIDER_SM_CONTEXT* pSM, 
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

	toLog("TermRandom", TerminalRandom, wTerminalRandomLen);
	toLog("SP_Challenge", SP_Challenge, wSP_ChallengeLen);
	toLog("PrivateKey32", PrivateKey32, 32);
	toLog("PublicKey64", PublicKey64, 64);

	//Получение сессионного ключа из приватного ключа провайдера услуг и публичного ключа терминала 
	ilRet = GostR3410_2001_KeyMatching11(PrivateKey32, PublicKey64, TerminalRandom, wTerminalRandomLen, SP_Challenge, wSP_ChallengeLen, SK32);
	if(ilRet)
		return ilRet;

	toLog("SK32", SK32, sizeof(SK32));

	//создаём сессионный ключ
	KDF_GOST(SK32, R2, sizeof(R2), pSM->SK_sm_id_smc_gost);
	toLog("SK_sm_id_smi_gost", pSM->SK_sm_id_smc_gost, sizeof(pSM->SK_sm_id_smc_gost));
	KDF_GOST(SK32, R1, sizeof(R1), pSM->SK_sm_id_smc_gost);
	toLog("SK_sm_id_smc_gost", pSM->SK_sm_id_smc_gost, sizeof(pSM->SK_sm_id_smc_gost));

	if(pSessDataOut != NULL)
	{
		pSessDataOut->Random_len = wTerminalRandomLen;
		cmnMemCopy(pSessDataOut->Random, TerminalRandom, wTerminalRandomLen);
	}

	return ilRet;
}

IL_FUNC IL_RETCODE cryptoSetTerminalToProviderSessionGost11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE KeyVer, IL_BYTE* Cspaid_buf, IL_WORD wCspaid_size, IL_BYTE* Msg, IL_DWORD dwMsgLen, PROVIDER_SESSION_DATA* pSessDataOut)
{
	IL_RETCODE ilRet = 0;
	KEY_GOST_PUB KeyPspidgost;
	KEY_GOST_PUB KeyPifdidgost;
	KEY_GOST_PRIV KeySifdidgost = {0};
	IL_BYTE TerminalRandom[16] = {0};
	IL_BYTE Cifd_buf[1024] = {0};
	IL_DWORD dwCifdLen;

	GetRandom(TerminalRandom, sizeof(TerminalRandom));

	ilRet = GetSifdidgost(KeyVer, &KeySifdidgost);
	if(ilRet)
		return ilRet;

	dwCifdLen = sizeof(Cifd_buf);    
	ilRet = prmGetParameterKeyVer(IL_PARAM_CIFDID, KeyVer, 1, Cifd_buf, &dwCifdLen);  
	if(ilRet)
		return ilRet;

	ilRet = GostKeyFromCertificateEx(IL_TAG_7F4E, Cifd_buf, dwCifdLen, &KeyPifdidgost);
	if(ilRet)
		return ilRet;

	ilRet = GostKeyFromCertificateEx(IL_TAG_7F4E, Cspaid_buf, wCspaid_size, &KeyPspidgost);
	if(ilRet)
		return ilRet;

	cmnMemCopy(pSessDataOut->Pidgost, KeyPifdidgost.key, sizeof(pSessDataOut->Pidgost)); 

	return cryptoSetTerminalToProviderSessionGost11_Ex(pSM, Msg, dwMsgLen, TerminalRandom, sizeof(TerminalRandom), KeySifdidgost.key, KeyPspidgost.key, pSessDataOut);
}
#endif

#ifdef SM_SUPPORT
IL_FUNC IL_RETCODE cryptoSetTerminalToProviderSession11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE KeyVer, IL_BYTE* Cspaid_buf, IL_WORD wCspaid_size, IL_BYTE* msg, IL_DWORD msg_len, PROVIDER_SESSION_DATA* pSessDataOut, IL_BYTE ifGost)
{
	IL_RETCODE ilRet = 0;
	IL_BYTE Cifd_buf[1024] = {0};
	IL_DWORD dwCifdLen;
	KEY_GOST_PUB KeyPifdidgost;

	dwCifdLen = sizeof(Cifd_buf);    
	ilRet = smGetCertificate(pSM, IL_PARAM_CIFDID, KeyVer, ifGost, Cifd_buf, &dwCifdLen);  
	if(ilRet)
		return ilRet;

	if(ifGost)
	{
		ilRet = GostKeyFromCertificateEx(IL_TAG_7F4E, Cifd_buf, (IL_WORD)dwCifdLen, &KeyPifdidgost);
		if(ilRet)
			return ilRet;
	}

	cmnMemCopy(pSessDataOut->Pidgost, KeyPifdidgost.key, 64); 

	ilRet = smSetTerminalToProviderSession11(pSM, Cspaid_buf, wCspaid_size, msg, msg_len, pSessDataOut, ifGost);

	return ilRet;
}
#else
IL_FUNC IL_RETCODE cryptoSetTerminalToProviderSession11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE KeyVer, IL_BYTE* Cspaid_buf, IL_WORD wCspaid_size, IL_BYTE* msg, IL_DWORD msg_len, PROVIDER_SESSION_DATA* pSessDataOut, IL_BYTE ifGost)
{
	IL_RETCODE ilRet = 0;

	if(!ifGost)
		ilRet = cryptoSetTerminalToProviderSessionRsa11(pSM, Cspaid_buf, wCspaid_size, msg, msg_len, pSessDataOut);
	else
		ilRet = cryptoSetTerminalToProviderSessionGost11(pSM, KeyVer, Cspaid_buf, wCspaid_size, msg, msg_len, pSessDataOut);

	return ilRet;
}
#endif

#ifndef SM_SUPPORT
IL_FUNC IL_RETCODE cryptoEncryptTerminalToProvider11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* EncMsg, IL_DWORD* pwMsgLenEncrypted, IL_BYTE ifGost)
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
#else
IL_FUNC IL_RETCODE cryptoEncryptTerminalToProvider11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* EncMsg, IL_DWORD* pwMsgLenEncrypted, IL_BYTE ifGost)
{
	IL_RETCODE ilRet = 0;
	
	ilRet = smEncryptTerminalToProvider11(pSM, Msg, MsgLen, EncMsg, pwMsgLenEncrypted, ifGost);
	
	return ilRet;
}
#endif

#ifndef SM_SUPPORT
IL_FUNC IL_RETCODE cryptoDecryptTerminalToProvider11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* DecryptedMsg, IL_DWORD* pwDecryptedMsgLen, IL_BYTE ifGost)
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
#else
IL_FUNC IL_RETCODE cryptoDecryptTerminalToProvider11(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* DecryptedMsg, IL_DWORD* pwDecryptedMsgLen, IL_BYTE ifGost)
{
	IL_RETCODE ilRet = 0;

	ilRet = smDecryptTerminalToProvider11(pSM, Msg, MsgLen, DecryptedMsg, pwDecryptedMsgLen, ifGost);

	return ilRet;
}
#endif

IL_FUNC IL_RETCODE cryptoCalcMessageSignature(IL_BYTE* Msg, IL_WORD wMsgLen, IL_BYTE* PrivatekeyCert, IL_WORD wPrivatekeyCertLen, IL_BYTE* S, IL_WORD* pwS_len, IL_BYTE ifGost, IL_BYTE AppVer)
{
	IL_RETCODE ilRet = 0;

	if(!ifGost)
	{
		KEY_RSA KeyRSA;

		ilRet = cryptoRsaKeyFromCertificate(PrivatekeyCert, wPrivatekeyCertLen, &KeyRSA);
		if(ilRet)
			return ilRet;

		MakeRsaCertificate(Msg, wMsgLen, &KeyRSA, S, pwS_len);
	}
	else
	{
#ifndef SM_SUPPORT //ssm+++
		KEY_GOST_PUB KeyGOST;

		ilRet = cryptoGostKeyFromCertificate(PrivatekeyCert, wPrivatekeyCertLen, &KeyGOST);
		if(ilRet)
			return ilRet;

		if(AppVer == UECLIB_APP_VER_10)
			GostR3410_2001_Sign(Msg, wMsgLen, KeyGOST.key, S);
		else
			GostR3410_2001_Sign11(Msg, wMsgLen, KeyGOST.key, S);
		*pwS_len = 64;
#endif//SM_SUPPORT
	}

	return ilRet;
}

IL_FUNC IL_RETCODE cryptoCheckMessageSignature(IL_BYTE* Msg, IL_WORD wMsgLen, IL_BYTE* S, IL_WORD wS_len, IL_BYTE* PublicKeyCert, IL_WORD wPublicKeyCertLen, IL_BYTE ifGost, IL_BYTE AppVer)
{
	IL_RETCODE ilRet = 0;

	if(!ifGost)
	{
		KEY_RSA KeyRSA;

		ilRet = RsaKeyFromCertificateEx(IL_TAG_7F4E, PublicKeyCert, wPublicKeyCertLen, &KeyRSA);
		if(ilRet)
			return ilRet;

		ilRet = cryptoVerifyRsaSignature(Msg, wMsgLen, S, wS_len, &KeyRSA);
	}
	else
	{
#ifndef SM_SUPPORT 
		KEY_GOST_PUB KeyGOST;

		ilRet = GostKeyFromCertificateEx(IL_TAG_7F4E, PublicKeyCert, wPublicKeyCertLen, &KeyGOST);
		if(ilRet)
			return ilRet;

		if(AppVer == UECLIB_APP_VER_10)
			ilRet = GostR3410_2001_Verify(Msg, wMsgLen, KeyGOST.key, S);
		else
			ilRet = GostR3410_2001_Verify11(Msg, wMsgLen, KeyGOST.key, S);
#endif//SM_SUPPORT
	}

	return ilRet;
}

IL_FUNC IL_RETCODE cryptoGetHashSnils(IL_BYTE *Snils, IL_BYTE *HashBuf, IL_WORD *HashLen, IL_BYTE ifGost)
{
	if(ifGost)
	{
		GostR3411_94_Hash(Snils, 6, HashBuf);
		*HashLen = 32;
	}
	else
	{
		SHA1(Snils, 6, HashBuf);
		*HashLen = 20;
	}

	return 0;
}
