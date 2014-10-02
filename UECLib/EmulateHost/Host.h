#ifndef _HAL_HOST_H_
#define _HAL_HOST_H_

#include "il_types.h"
#include "HAL_Common.h"
#include "HAL_Crypto.h"
#include "Tlv.h"
#include "TAG.h"
#include "il_error.h"
#include "FuncLib.h"

// Криптоконтекст защищённой сессии между ИД-приложением и эмитентом ИД-приложения на стороне эмулятора хоста
typedef struct 
{
	SM_CONTEXT SM;
} HANDLE_CRYPTO_HOST;

// Криптоконтекст защищённой сессии между Поставщиком услуги и Терминалом на стороне эмулятора хоста
typedef struct
{
    IL_BYTE SK_sm_id_smc_des[16];	// сессионный ключ шифрования RSA
    IL_BYTE SK_sm_id_smc_gost[32];	// сессионный ключ шифрования ГОСТ
} HOST_PROVIDER_SM_CONTEXT;

//  Description:
//      Формирует ЭЦП ответа Поставщика Услуги на запрос оказания услуги для последующей аутентификации на стороне терминала.<p/>
//		Механизм аутентификации Поставщика Улуги обеспечивает контроль терминалом достоверности данных, полученных в ответе на запрос оказания услуги. 
//		Аутентификация Поставщика Улуги осуществляется только в том случае, если в метаинформации по услуге присутствует соответствующий признак.
//  See Also:
//  Arguments:
//		in_pMsg		 - Указатель на подписываемое сообщение.
//		MsgLen		 - Длина подписываемого сообщения.
//      out_pSign    - Указатель на выходного буфера для формируемой цифровой подписи.
//		out_pSignLen - Длина сформированной цифровой подписи.
//		ifGost		 - Признак формирования подписи с использованием криптоалгоритма ГОСТ. Иначе используется RSA.
//		AppVer		 - Версия ИД-приложения установленной в ридер карты.
//  Return Value:
//      IL_RETCODE	 - Код ошибки.
//  Summary:
//      Формирование Поставщиком Услуги ЭЦП ответа на запрос оказания услуги.
IL_FUNC IL_RETCODE hostAuthServiceProvider(IL_BYTE *in_pMsg, IL_WORD MsgLen, IL_BYTE *out_pSign, IL_WORD *out_pSignLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      Проверяет сформированный терминалом запрос на аутентификацию ИД-приложения в режиме Online.<p/>
//		Значение и тип используемого ключа для вычисления криптограммы ИД-приложения на строне хоста задаётся в секции 'Keys' конфигурационного файла эмулятора хоста 'host.ini'.  
//  See Also:
//  Arguments:
//		in_pAuthReqData	- Указатель на входной буфер с TLV-данными запроса на аутентификацию ИД-приложения в режиме Online.
//		AuthReqDataLen	- Длина сформированного запроса.
//		ifGost	- Признак формирования криптограммы аутентификации с использованием криптоалгоритма ГОСТ. Иначе используется RSA.
//		AppVer	- Версия ИД-приложения установленной в ридер карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Проверка запроса на аутентификацию ИД-приложения в режиме Online.
IL_FUNC IL_RETCODE hostCheckAuthRequestOnline(IL_BYTE *in_pAuthReqData, IL_WORD AuthReqDataLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      Проверяет сформированный терминалом запрос на аутентификацию ИД-приложения в режиме Offine.<p/>
//		Значение и тип ключа 'MkAcId' для вычисления криптограммы ИД-приложения на строне эмулятора хоста задаётся в секции 'Keys' конфигурационного файла 'host.ini'.  
//  See Also:
//  Arguments:
//		in_pAuthReqData	- Указатель на входной буфер с TLV-данными запроса на аутентификацию ИД-приложения в режиме Online.
//		AuthReqDataLen	- Длина сформированного запроса.
//		ifGost	- Признак формирования криптограммы аутентификации с использованием криптоалгоритма ГОСТ. Иначе используется RSA.
//		AppVer	- Версия ИД-приложения установленной в ридер карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Проверка запроса на аутентификацию ИД-приложения в режиме Offline.
IL_FUNC IL_RETCODE hostCheckAuthRequestOffline(IL_BYTE *in_pAuthReqData, IL_WORD AuthReqDataLen, IL_BYTE ifGost, IL_BYTE AppVer); 

//  Description:
//      Подготавливает защищённую сессию между ИД-приложением карты УЭК и Эмитентом ИД-приложения на стороне эмулятора хоста.<p/>
//		Значение и тип ключа 'MkSmId' для обеспечения обмена через защищённый канал на строне эмулятора хоста задаётся в секции 'Keys' конфигурационного файла 'host.ini'.<p/>
//		После успешной установки защищённого канала терминал осуществляет приём зашифрованных пакетов APDU-команд, передаёт их на карту для выполнения и транслирует ответы для их обработки на стороне хоста.
//  See Also:
//  Arguments:
//		hCryptoHost		 - Указатель на дескритор для устанавливаемой сессии на стороне хоста.
//		in_pAuthReqData	 - Указатель на входной буфер с TLV-данными запроса на аутентификацию ИД-приложения в режиме Online.
//		AuthReqDataLen	 - Длина сформированного запроса.
//		in_pSessionData  - Указатель на описатель входных данных со случайным числом, сгенерированным картой. 
//		out_pSessionData - Указатель на описатель выходных данных с конкатенированными данными криптограммы аутентификации хоста и случайного числа хоста.
//		ifGost	- Признак установки защищённой сессии с использованием криптоалгоритма ГОСТ. Иначе используется RSA.
//		AppVer	- Версия ИД-приложения установленной в ридер карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Установка защищённой сессии между картой и эмитентом ИД-приложения.
IL_FUNC IL_RETCODE hostPrepareIssuerSession(HANDLE_CRYPTO_HOST *hCryptoHost, IL_BYTE *in_pAuthReqData, IL_WORD AuthReqDataLen, ISSUER_SESSION_DATA_IN *in_pSessionData, ISSUER_SESSION_DATA_OUT *out_pSessionData, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      Проверяет установленную защищённую сессию между ИД-приложением карты УЭК и Эмитентом ИД-приложения на стороне эмулятора хоста.<p/>
//		После успешной установки защищённого канала терминал осуществляет приём зашифрованных пакетов APDU-команд, передаёт их на карту для выполнения и транслирует ответы для их обработки на стороне хоста.
//  See Also:
//  Arguments:
//		hCryptoHost			 - Указатель на дескритор для устанавливаемой сессии на стороне хоста.
//		in_pCheckSessionData - Указатель на описатель входных данных со случайным числом хоста и проверочной криптограммой карты.
//		ifGost				 - Признак установки защищённой сессии с использованием криптоалгоритма ГОСТ. Иначе используется RSA.
//		AppVer				 - Версия ИД-приложения установленной в ридер карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Проверка установленной защищённой сессии между картой и эмитентом ИД-приложения.
IL_FUNC IL_RETCODE hostCheckIssuerSession(HANDLE_CRYPTO_HOST *hCryptoHost, CHECK_ISSUER_SESSION_DATA_IN *in_pCheckSessionData, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      Шифрует блок данных в рамках установленной ранее защищённой сессии между Поставщиком услуги и Терминалом для обеспечения конфиденциальности обмена данными.<p/>
//  See Also:
//		opCtxSetProviderEncrDecrBuf
//		hostEncryptServiceProviderData
//		hostPrepareServiceProviderSession
//  Arguments:
//		pSM				- Указатель на криптоконтекст эмулятора хоста для сессии Поставщика услуги.
//		in_pClearData	- Указатель на блок открытых данных для шифрования.
//		ClearDataLen	- Длина шифруемых данных.
//		out_pEncData	- Указатель на выходной буфер для зашифрованных данных.
//		out_pEncDataLen - Указатель на длину зашифрованных данных.
//		ifGost			- Признак защищённой сессии с использованием криптоалгоритма ГОСТ. Иначе используется RSA.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Шифрование блока данных Поставщиком Услуги.
IL_FUNC IL_RETCODE hostEncryptServiceProviderData(HOST_PROVIDER_SM_CONTEXT *pSM, IL_BYTE *in_pClearData, IL_DWORD ClearDataLen, IL_BYTE *in_pEncData, IL_DWORD *pEncDataLen, IL_BYTE ifGost);

//  Description:
//      Расшифровывает блок данных в рамках установленной ранее защищённой сессии между Поставщиком услуги и Терминалом для обеспечения конфиденциальности обмена данными.<p/>
//  See Also:
//		opCtxSetProviderEncrDecrBuf
//		hostEncryptServiceProviderData
//		hostPrepareServiceProviderSession
//  Arguments:
//		pSM					  - Указатель на криптоконтекст эмулятора хоста для установленной сессии.
//		in_pEncryptedData	  - Указатель на блок зашифрованных данных для расшифрования.
//		ClearDataLen		  - Длина зашифрованных данных.
//		out_pDecryptedData	  - Указатель на выходной буфер для расшифрованных данных.
//		out_pDecryptedDataLen - Указатель на длину расшифрованных данных.
//		ifGost				  - Признак защищённой сессии с использованием криптоалгоритма ГОСТ. Иначе используется RSA.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Расшифрование блока данных Поставщиком Услуги.
IL_FUNC IL_RETCODE hostDecryptServiceProviderData(HOST_PROVIDER_SM_CONTEXT *pSM, IL_BYTE *in_pEncryptedData, IL_DWORD EncryptedDataLen, IL_BYTE *out_pDecryptedData, IL_DWORD *out_pDecryptedDataLen, IL_BYTE ifGost);

//  Description:
//      Подготавливает шифрованныый пакета APDU-команд в рамках установленной ранее защищённой сессии между ИД-приложением и эмитентом ИД-приложения.
//  See Also:
//		opApiRunApduPacket
//  Arguments:
//		pSM				  - Указатель на криптоконтекст эмулятора хоста для установленной сессии.
//		in_pApduSequence  - Указатель на сформированный эмитентом пакет APDU-команд в открытом виде.
//		ApduNum			  - Количество APDU-команд в пакете.
//		ifGost			  - Признак защищённой сессии с использованием криптоалгоритма ГОСТ. Иначе используется RSA.
//		AppVer			  - Версия ИД-приложения установленной в ридер карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Подготовка шифрованного пакета APDU-команд.
IL_FUNC IL_RETCODE hostPrepareApdus(SM_CONTEXT *pSM, IL_APDU_PACK_ELEM *in_pApduSequence, IL_WORD ApduNum, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      Расшифровывает ответы пакета APDU-команд выполненых в рамках установленной ранее защищённой сессии между ИД-приложением и эмитентом ИД-приложения.
//  See Also:
//		opApiRunApduPacket
//  Arguments:
//		pSM				  - Указатель на криптоконтекст эмулятора хоста для установленной сессии.
//		in_pApduSequence  - Указатель на сформированный терминалом пакет APDU-команд с шифрованными ответами и выходными данными.
//		ApduNum			  - Количество APDU-команд в пакете.
//		ifGost			  - Признак защищённой сессии с использованием криптоалгоритма ГОСТ. Иначе используется RSA.
//		AppVer			  - Версия ИД-приложения установленной в ридер карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Расшифровка ответов выполненного пакета APDU-команд.
IL_FUNC IL_RETCODE hostProcessApdus(SM_CONTEXT *pSM, IL_APDU_PACK_ELEM *in_pApduSequence, IL_WORD ApduNum, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      Подготавливает данные ответа на запрос аутентификации ИД-приложения для последующей обработки ответа терминалом.<p/>
//		Значения данных ответа 'MemberId', 'IdentOpId', 'PaymentInfo' и 'AAC' формируются соответственно параметам секции 'Data' конфигурационного файла 'host.ini'.   
//  See Also:
//		opApiCheckAppAuthResponse	
//  Arguments:
//		in_pAuthReqData		 - Указатель на входной буфер с TLV-данными запроса на аутентификацию ИД-приложения в режиме Online.
//		AuthReqDataLen		 - Длина сформированного запроса.
//		out_pAuthRespData	 - Указатель на выходной буфер для формируемого ответа на запрос аутентификации ИД-приложения.
//		out_pAuthRespDataLen - Указатель на длину сформированного ответа.
//		ifGost				 - Признак использования криптоалгоритма ГОСТ. Иначе используется RSA.
//		AppVer				 - Версия ИД-приложения установленной в ридер карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Подготовка ответа на запрос аутентификации ИД-приложения.
IL_FUNC IL_RETCODE hostPrepareAuthResponse(IL_BYTE *in_pAuthReqData, IL_WORD AuthReqDataLen, IL_BYTE *out_pAuthRespData, IL_WORD *out_pAuthRespDataLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      Подготавливает на стороне эмулятора хоста защищённую сессию между Поставщиком услуги и Терминалом.<p/>
//		При подготовке сессии используется значение открытого ключа сертификата Поставщика услуги 'CSpId' конфигурационного файла 'host.ini', 
//		В рамках установленной сессии обеспечивается конфиденциальность обмена данными запрос/ответ при оказании услуги.<p/>
//		Защищённое соединение с Поставщиком услуги устанавливается только в том случае, если такое ребование определено в метаинформации об услуге.
//		
//  See Also:
//		opCtxGetProviderSessionData
//		hostEncryptServiceProviderData
//		hostDecryptServiceProviderData
//  Arguments:
//		pSM					     - Указатель на криптоконтекст эмулятора хоста для установленной сессии.
//		in_SPChallehge		     - Указатель на блок данных c псевдослучайным числом, формируемым Постовщиком усдуги (PAN||ACC).
//		ClearDataLen		     - Длина блока данных псевдослучайного числа.
//		in_pProviderSessionDara -  Указатель на входные параметры с данными установленной сессии.
//		ifGost				     - Признак использованием криптоалгоритма ГОСТ для подготавливаемой сессии. Иначе используется RSA.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Подготовка защищённой сессии между Поставщиком Услуги и Терминалом.
IL_FUNC IL_RETCODE hostPrepareServiceProviderSession(HOST_PROVIDER_SM_CONTEXT *pSM, IL_BYTE *in_SPChallenge, IL_DWORD SPChallengeLen, PROVIDER_SESSION_DATA *in_pProviderSessionDara, IL_BYTE ifGost);

//  Description:
//      Подготавливает защищённую сессию между модулем безопасности (МБ УЭК) и Эмитентом ИД-приложения на стороне эмулятора хоста.<p/>
//		После успешной установки защищённого канала терминал осуществляет приём зашифрованных пакетов APDU-команд, передаёт их на МБ для выполнения и транслирует ответы для их обработки на стороне хоста.
//  See Also:
//		opCtxGetSeIssuerSessionIcChallenge
//		opCtxSetSeIssuerSessionCryptogramm
//  Arguments:
//		hCryptoHost		 - Указатель на дескритор для устанавливаемой сессии на стороне хоста.
//		in_pSessionData  - Указатель на описатель входных данных со случайным числом, сгенерированным МБ. 
//		out_pSessionData - Указатель на описатель выходных данных с конкатенированными данными криптограммы аутентификации хоста и случайного числа хоста.
//		ifGost			 - Признак установки защищённой сессии с использованием криптоалгоритма ГОСТ. Иначе используется RSA.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Установка защищённой сессии между модулем безопасности и эмитентом ИД-приложения.
IL_FUNC IL_RETCODE hostPrepareSmIssuerSession(HANDLE_CRYPTO_HOST *hCryptoHost, SM_SESSION_DATA_IN *in_pSessionData, SM_SESSION_DATA_OUT *out_pSessionData, IL_BYTE ifGost);

IL_FUNC IL_RETCODE hostSetTerminalToProviderSessionRsa(HOST_PROVIDER_SM_CONTEXT* pSM, IL_BYTE* pPrivateProviderKeyCert, IL_WORD wPrivateProviderKeyCertLen, IL_BYTE* msg, IL_DWORD msg_len, IL_BYTE* pY, IL_WORD wY_len);
IL_FUNC IL_RETCODE hostSetTerminalToProviderSessionGost(HOST_PROVIDER_SM_CONTEXT* pSM, IL_BYTE* bPubKey64, IL_BYTE* bPrivKey32, IL_BYTE* msg, IL_DWORD msg_len, IL_BYTE* bRand, IL_WORD wRand);
IL_FUNC IL_RETCODE hostPrepareUnlockPukApdus(HANDLE_CRYPTO_HOST* hCrypto, IL_BYTE *in_pAuthRequest, IL_WORD in_wAuthRequestLen, IL_BYTE ifGostCrypto, IL_BYTE AppVer, IL_BYTE *out_ApduIn, IL_WORD *out_ApduInLen);
IL_FUNC IL_RETCODE hostPrepareEditIdDataApdus(HANDLE_CRYPTO_HOST* hCrypto, IL_TAG TagId, IL_CHAR *Data, IL_BYTE ifGostCrypto, IL_BYTE *out_ApduIn, IL_WORD *out_ApduInLen);
IL_FUNC IL_RETCODE hostPrepareEditPrivateDataApdus(IL_TAG TagId, IL_CHAR *Data, IL_BYTE *out_ApduIn, IL_WORD *out_ApduInLen);


#endif//_HAL_HOST_H_