#ifndef _HAL_CRYPTO_H_
#define _HAL_CRYPTO_H_

#include "il_types.h"
#include "HAL_CryptoHandle.h"
#include "HAL_SCReader.h"

// Максимальная длина модуля ключа RSA
#define MAX_RSA_MOD_LEN 2048/8

// Описатель ключа RSA
typedef struct
{
	IL_BYTE mod[MAX_RSA_MOD_LEN];	// Модуль ключа
	IL_WORD mod_len;				// Длина модуля ключа
	IL_BYTE exp[MAX_RSA_MOD_LEN];	// Экспонента ключа
	IL_WORD exp_len;				// Длина экспоненты
} KEY_RSA;

// Описатель открытого ключа ГОСТ
typedef struct
{
    IL_BYTE key[64];	// Значение открытого ключа ГОСТ
} KEY_GOST_PUB;

// Описатель закрытого ключа ГОСТ
typedef struct
{
    IL_BYTE key[32];	// Значение закрытого ключа ГОСТ	
} KEY_GOST_PRIV;

//Описатель входных данных для функций cryptoPrepareMutualAuthDataGost и cryptoPrepareMutualAuthDataRsa 
typedef struct 
{
	KEY_RSA KeyPicidrsa;		// Значение открытого ключа RSA
    KEY_GOST_PUB KeyPicidgost;	// Значение открытого ключа ГОСТ
	IL_BYTE IcChallenge[16];	// Случайное число карты
	IL_WORD IcChallengeLength;	// Длина случайного числа карты
} MUTUAL_AUTH_DATA_IN;

//Описатель выходных данных для функций cryptoPrepareMutualAuthDataGost и cryptoPrepareMutualAuthDataRsa 
typedef struct
{
	IL_BYTE S[256];	// Цифровая подпись для сформированного сообщения с использованием закрытого ключа терминала ГОСТ
	IL_WORD S_len;	// Длина цифровой подписи
	IL_BYTE Y[256];	// Блок зашифрованных на открытом ключе терминала данных RSA
	IL_WORD Y_len;	// Длина зашифрованного блока данных RSA
} MUTUAL_AUTH_DATA_OUT;

//Описатель входных данных для функции cryptoPrepareSession
typedef struct
{
	IL_BYTE Data[256];					// Блок данных для выработки сессионного ключа 
	IL_WORD Data_len;					// Длина блока данных для выработки сессионного ключа 
	IL_BYTE InitSessionSmCounter[8];	// Начальное значение счётчика сообщений
	IL_BYTE TermRandom[16];		// Случайное число терминала
	IL_WORD TermRandom_len;		// Длина случайного числа терминала
	IL_BYTE IcChallenge[16];	// Случайное число карты	
	IL_WORD IcChallenge_len;	// Длина случайного числа карты
} SESSION_DATA_IN;

// Описатель входных данных для функции hostPrepareIssuerSession
typedef struct
{
	IL_BYTE IcChallenge[16];		// Случайное число карты
} ISSUER_SESSION_DATA_IN;

// Описатель выходных данных для функции hostPrepareIssuerSession
typedef struct
{
    IL_BYTE CardCryptogramm[20];	// Конкатенация данных криптограммы аутентификации хоста (4 байта) и случайного числа хоста (16байт)
	IL_BYTE CardCryptogrammLength;  // Длина конкатенированных данных
} ISSUER_SESSION_DATA_OUT;

// Описатель входных данных для функции hostCheckIssuerSession
typedef struct
{
    IL_BYTE HostChallenge[16];		// Случайное число хоста
    IL_BYTE CardCryptogramm[4];		// Проверочная криптограмма карты (Tic)
} CHECK_ISSUER_SESSION_DATA_IN;

// Описатель параметров сессии с Поставщиком услуги
typedef struct
{
    IL_BYTE Y[256];
    IL_WORD Y_len;
    
    IL_BYTE Random[256];
    IL_WORD Random_len;
    IL_BYTE Pidgost[64];
} PROVIDER_SESSION_DATA;

#ifndef SM_SUPPORT
// Описатель контекста сессии с Поставщиком услуги 
typedef struct
{
    IL_BYTE SK_sm_id_smc_des[16];	// Сессионные ключи DES
    IL_BYTE SK_sm_id_smc_gost[32];	// Сессионные ключи ГОСТ
} PROVIDER_SM_CONTEXT;
#else
typedef	IL_HANDLE_CRYPTO PROVIDER_SM_CONTEXT;
#endif

// Описатель входных данных для функции hostPrepareSmIssuerSession
typedef struct
{
	IL_BYTE IcChallenge[16];		// Cлучайное число карты (SM)
} SM_SESSION_DATA_IN;

// Описатель выходных данных для функции hostPrepareSmIssuerSession
typedef struct
{
    IL_BYTE CardCryptogramm[20];	// Конкатенация данных криптограммы аутентификации хоста (4 байта) и случайного числа хоста (16байт)
	IL_BYTE CardCryptogrammLength;  // Длина конкатенированных данных
} SM_SESSION_DATA_OUT;

typedef struct
{
    IL_BYTE HostChallenge[16];		// Случайное число хоста
    IL_BYTE CardCryptogramm[4];		// Проверочная криптограмма SM (Tic)
} CHECK_SM_SESSION_DATA_IN;



//  Description:
//		Подготавливает сообщение-запрос в рамках установленной защищённой сессии обмена сообщениями.
//  See Also:
//		cryptoProcessSM
//  Arguments:
//      hCrypto		 - Указатель на нетипизированный описатель установленной защищённой сессии обмена сообщениями. 
//		in_pilApdu	 - Указатель на подготавливаемое сообщение-запрос (APDU-команду).
//		ifGOST		 - Тип исполльзуемого криптоалгоритма ГОСТ/RSA.
//		AppVer		 - Версия ИД-приложения карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Подготовка сообщения-запроса в рамках установленной защищённой сессии обмена сообщения.
IL_FUNC IL_RETCODE cryptoPrepareSM(IL_HANDLE_CRYPTO hCrypto, IL_APDU *in_pilApdu, IL_BYTE ifGOST, IL_BYTE AppVer);

//  Description:
//		Обрабатывает сообщение-запрос (APDU-команду) в рамках установленной защищённой сессии обмена сообщениями.
//  See Also:
//		cryptoPrepareSM
//  Arguments:
//      hCrypto		 - Указатель на нетипизированный описатель установленной защищённой сессии обмена сообщениями. 
//		in_pilApdu	 - Указатель на защищённое сообщение-запрос (APDU-команду).
//		ifGOST		 - Тип исполльзуемого криптоалгоритма ГОСТ/RSA.
//		AppVer		 - Версия ИД-приложения карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Обработка сообщения-запроса в рамках установленной защищённой сессии обмена сообщения.
IL_FUNC IL_RETCODE cryptoProcessSM(IL_HANDLE_CRYPTO hCrypto, IL_APDU* pilApdu, IL_BYTE ifGOST, IL_BYTE AppVer);

//  Description:
//		Устанавливает защищённую сессию RSA для последующего обмена сообщениями между картой и внешней строной (терминалом или ИС ФУО).
//  See Also:
//		cryptoPrepareSM
//		cryptoProcessSM
//  Arguments:
//      hCrypto			  - Указатель на нетипизированный описатель защищённой сессии обмена сообщениями. 
//		in_pSessionDataIn - Указатель на входные параметры устанавливаемой сессии.
//		KeyVer			  - Версия ключа Удостоверяющего Центра (УЦ).
//		AppVer			  - Версия ИД-приложения карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Установка защищённой сессии RSA для обмена сообщениями-запросами между картой и внешней строной.
IL_FUNC IL_RETCODE cryptoPrepareSession(IL_HANDLE_CRYPTO hCrypto, SESSION_DATA_IN *in_pSessionDataIn, IL_BYTE KeyVer, IL_BYTE AppVer);

//  Description:
//		Устанавливает защищённую сессию ГОСТ для последующего обмена сообщениями между картой и внешней строной (терминалом или ИС ФУО).
//  See Also:
//		cryptoPrepareSM
//		cryptoProcessSM
//  Arguments:
//      hCrypto			  - Указатель на нетипизированный описатель защищённой сессии обмена сообщениями. 
//		in_pSessionDataIn - Указатель на входные параметры устанавливаемой сессии.
//		KeyVer			  - Версия ключа Удостоверяющего Центра (УЦ).
//		AppVer			  - Версия ИД-приложения карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Установка защищённой сессии ГОСТ для обмена сообщениями-запросами между картой и внешней строной.
IL_FUNC IL_RETCODE cryptoPrepareSessionGOST(IL_HANDLE_CRYPTO hCrypto, SESSION_DATA_IN* pSessionDataIn, IL_BYTE KeyVer, IL_BYTE AppVer);

//  Description:
//		Пордготавливает данные для последующей аутентификации внешнего объекта с использованием криптоалгоритма ГОСТ.
//  See Also:
//		cryptoPrepareMutualAuthDataRsa
//  Arguments:
//      hCrypto		 - Указатель на нетипизированный описатель защищённой сессии обмена сообщениями. 
//		in_pDataIn	 - Указатель на входные параметры.
//		out_pDataOut - Указатель на подготовленные выходные данные для аутентификации.
//		KeyVer		 - Версия ключа Удостоверяющего Центра (УЦ).
//		AppVer		 - Версия ИД-приложения карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Подготовка данных для аутентификации ГОСТ.
IL_FUNC IL_RETCODE cryptoPrepareMutualAuthDataGost(IL_HANDLE_CRYPTO hCrypto, MUTUAL_AUTH_DATA_IN *in_pDataIn, MUTUAL_AUTH_DATA_OUT *out_pDataOut, IL_BYTE KeyVer, IL_BYTE AppVer);

//  Description:
//		Пордготавливает данные для последующей аутентификации внешнего объекта с использованием криптоалгоритма RSA.
//  See Also:
//		cryptoPrepareMutualAuthDataRsa
//  Arguments:
//      hCrypto		 - Указатель на нетипизированный описатель защищённой сессии обмена сообщениями. 
//		in_pDataIn	 - Указатель на входные параметры.
//		out_pDataOut - Указатель на подготовленные выходные данные для аутентификации.
//		KeyVer		 - Версия ключа Удостоверяющего Центра (УЦ).
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Подготовка данных для аутентификации RSA.
IL_FUNC IL_RETCODE cryptoPrepareMutualAuthDataRsa(IL_HANDLE_CRYPTO hCrypto, MUTUAL_AUTH_DATA_IN *in_pDataIn, MUTUAL_AUTH_DATA_OUT *out_pDataOut, IL_BYTE KeyVer);

//  Description:
//		Устанавливает защищённую сессию с Поставщиком Услуги для обеспечения конфиденциальности данных обмена между терминалом и Поставщиком Услуги в соответствии со спецификацией карты 1.1.
//  See Also:
//  Arguments:
//      in_pSM			- Указатель на криптоконтекст сессии с Поставщиком Услуги. 
//		KeyVer			- Версия ключа Удостоверяющего Центра (УЦ).
//		in_pCspaid		- Указатель на буфер сертификата открытого ключа Поставщика услуги.
//		CspaidLen		- Длина сертификата.
//		in_pSPChallenge - Указатель на буфер с псевдослучайным числом Поставщика Услуги (Application PAN || ACC).	
//		SPChallengeLen  - Длина псевдослучайного числа.
//		out_pSessData	- Указатель на выходной буфер с параметрами установленной сессии.
//		ifGost			- Тип используемого криптоалгоритма сессии ГОСТ/RSA.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Установка защищённой сессии с Поставщиком Услуги.
IL_FUNC IL_RETCODE cryptoSetTerminalToProviderSession11(PROVIDER_SM_CONTEXT *in_pSM, IL_BYTE KeyVer, IL_BYTE *in_pCspaid, IL_WORD CspaidLen, IL_BYTE *in_pSPChallenge, IL_DWORD SPChallengeLen, PROVIDER_SESSION_DATA *out_pSessData, IL_BYTE ifGost);

//  Description:
//		Выполняет аутентификацию Поставщика Услуги.
//  See Also:
//		opApiAuthServiceProvider
//  Arguments:
//      in_pSM		- Указатель на криптоконтекст сессии с Поставщиком Услуги. 
//		in_pMsg		- Указатель на данные ответа на запрос оказания услуг.
//		MsgLen		- Длина данных ответа.
//		in_pMsgSign	- Указатель на буфер c ЭЦП, сформированной Поставщиком услуги для данных ответа на запрос оказания услугии.
//		MsgSignLen	- Длина сформированной ЭЦП.
//		in_pCSpId	- Указатель на буфер сертификата открытого ключа Поставщика услуги.
//		CSpIdLen	- Длина сертификата.
//		ifGost		- Тип используемого криптоалгоритма сессии ГОСТ/RSA.
//		AppVer		- Версия ИД-приложения.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Аутентификация Поставщика Услуги.
IL_FUNC IL_RETCODE cryptoAuthServiceProvider(PROVIDER_SM_CONTEXT *in_pSM, IL_BYTE *in_pMsg, IL_WORD MsgLen, IL_BYTE *in_pMsgSign, IL_WORD MsgSignLen, IL_BYTE *in_pCSpId, IL_WORD CSpIdLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//		Зашифровывает критические данные запроса оказания услуги в рамках установленной в соответствии со спецификацией карты 1.1 сессии с Поставщиком Услуги.
//  See Also:
//		opApiEncryptProviderToTerminal
//  Arguments:
//      in_pSM		   - Указатель на криптоконтекст сессии с Поставщиком Услуги. 
//		in_pMsg		   - Указатель на буфер с данными для шифрования.
//		MsgLen		   - Длина данных для шифрования.
//		out_pEncMsg	   - Указатель на выходной буфер c зашифрованными данными.
//		out_pEncMsgLen - Указатель на переменную, инициализируемую длиной возвращаемого блока зашифрованных данных.
//		ifGost		   - Тип используемого криптоалгоритма сессии ГОСТ/RSA.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Шифрование данных в рамках установленной сессии с Поставщиком Услуги.
IL_FUNC IL_RETCODE cryptoEncryptTerminalToProvider11(PROVIDER_SM_CONTEXT *in_pSM, IL_BYTE *in_pMsg, IL_DWORD MsgLen, IL_BYTE *out_pEncMsg, IL_DWORD *out_pEncMsgLen, IL_BYTE ifGost);

//  Description:
//		Расшифровывает данные в рамках установленной в соответствии со спецификацией карты 1.1 сессии с Поставщиком Услуги.
//  See Also:
//		opApiDecryptProviderToTerminal
//  Arguments:
//      in_pSM		    - Указатель на криптоконтекст сессии с Поставщиком Услуги. 
//		in_pMsg		    - Указатель на буфер с данными для расшифрования.
//		MsgLen		    - Длина данных для расшифрования.
//		out_pDecrMsg    - Указатель на выходной буфер c расшифрованными данными.
//		out_pDecrMsgLen - Указатель на переменную, инициализируемую длиной возвращаемого блока расшифрованных данных.
//		ifGost		    - Тип используемого криптоалгоритма сессии ГОСТ/RSA.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Расшифрование данных в рамках установленной сессии с Поставщиком Услуги.
IL_FUNC IL_RETCODE cryptoDecryptTerminalToProvider11(PROVIDER_SM_CONTEXT *in_pSM, IL_BYTE *in_pMsg, IL_DWORD MsgLen, IL_BYTE *out_pDecrMsg, IL_DWORD *out_pDecrMsgLen, IL_BYTE ifGost);

//  Description:
//		Извлекает значение модуля и экспоненты открытого RSA-ключа из данных сертификата.
//  See Also:
//  Arguments:
//      in_pKeyCert	- Указатель на RSA-сертификат открытого ключа. 
//		KeyCertLen  - Длина сертификата.
//		out_pKeyRSA - Указатель на выходной буфер cо значением RSA-компонент извлекаемого ключа.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Извлечение значения открытого ключа RSA из сертификата.
IL_FUNC IL_RETCODE cryptoRsaKeyFromCertificate(IL_BYTE *in_pKeyCert, IL_WORD KeyCertLen, KEY_RSA *out_pKeyRSA);

//  Description:
//		Извлекает значение открытого ключа ГОСТ из данных сертификата.
//  See Also:
//  Arguments:
//      in_pKeyCert		- Указатель на ГОСТ-сертификат открытого ключа. 
//		KeyCertLen		- Длина сертификата.
//		out_pKeyGostPub - Указатель на выходной буфер cо значением извлекаемого ключа ГОСТ.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Извлечение значения открытого ключа ГОСТ из сертификата.
IL_FUNC IL_RETCODE cryptoGostKeyFromCertificate(IL_BYTE *in_pKeyCert, IL_WORD KeyCertLen, KEY_GOST_PUB *out_pKeyGostPub);

//  Description:
//		Проверяет подпись на указанном RSA-ключе.
//  See Also:
//  Arguments:
//		in_pMsg		- Указатель на подписываемое сообщение.
//      MsgLen		- Длина подписываемого сообщения. 
//		in_pSign	- Указатель на проверяемую подпись
//		SignLen		- Длина проверяемой подписи.
//		in_pKeyRSA	- Указатель на подписывающий RSA ключ.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Проверка RSA-подписи.
IL_FUNC IL_RETCODE cryptoVerifyRsaSignature(IL_BYTE *in_pMsg, IL_WORD MsgLen, IL_BYTE *in_pSign, IL_WORD SignLen, KEY_RSA *in_pKeyRSA);

//  Description:
//		Извлекает значение открытого ключа из данных сертификата.
//  See Also:
//  Arguments:
//      in_pCert	 - Указатель на сертификат открытого ключа. 
//		CertLen		 - Длина сертификата.
//		out_pKey	 - Указатель на выходной буфер cо значением извлекаемого ключа.
//		ifGost		 - Тип используемого криптоалгоритма ГОСТ/RSA.
//		ifCompressed - Признак возврата сжатого значения ключа.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Извлечение значения открытого ключа из сертификата.
IL_FUNC IL_RETCODE cryptoPublicKeyFromCertificate(IL_BYTE *in_pCert, IL_WORD CertLen, IL_BYTE *out_pKey, IL_WORD *out_pKeyLen, IL_BYTE ifGost, IL_BYTE ifCompressed);

//  Description:
//		Вычисляет цифровую подпись для указанного сообщения.
//  See Also:
//		cryptoCheckMessageSignature
//  Arguments:
//      in_pMsg		 - Указатель на подписываемое сообщение. 
//		MsgLen		 - Длина подписывемого сообщения.
//		in_pPrivKey	 - Указатель на закрытый ключ, используемый для вычисления подписи.
//		PrivKeyLen	 - Длина закрытого ключа.
//		out_pSign	 - Указатель на выходной буфер для вычесляемой подписи.
//		out_pSignLen - Указатель на переменную, инициализируемую значением длины подписи.
//		ifGost		 - Тип используемого криптоалгоритма ГОСТ/RSA.
//		AppVer		 - Версия ИД-приложения.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Вычисление цифровой подписи.
IL_FUNC IL_RETCODE cryptoCalcMessageSignature(IL_BYTE *in_pMsg, IL_WORD MsgLen, IL_BYTE *in_pPrivKey, IL_WORD PrivKeyLen, IL_BYTE *out_pSign, IL_WORD *out_pSignLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//		Проверяет аутентичность цифровой подписи для указанного сообщения.
//  See Also:
//		cryptoCalcMessageSignature
//  Arguments:
//      in_pMsg		 - Указатель на сообщение. 
//		MsgLen		 - Длина сообщения.
//		in_pSign	 - Указатель на проверяемую цифровую подпись сообщения.
//		SignLen		 - Длина подписи.
//		in_pPubKey	 - Указатель на открытый ключ, используемый для проверки подписи.
//		PubKeyLen	 - Длина открытого ключа.
//		ifGost		 - Тип используемого криптоалгоритма ГОСТ/RSA.
//		AppVer		 - Версия ИД-приложения.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Проверка аутентичности цифровой подписи.
IL_FUNC IL_RETCODE cryptoCheckMessageSignature(IL_BYTE *in_pMsg, IL_WORD MsgLen, IL_BYTE *in_pSign, IL_WORD SignLen, IL_BYTE *in_pPubKey, IL_WORD PubKeyLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//		Генерирует случайное число терминала указанной длины.
//  See Also:
//  Arguments:
//      out_pRandom	 - Указатель на выходной буфер для случайного числа. 
//		RandomLen	 - Длина генерируемого случайного числа.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Генерация случайного числа терминала.
IL_FUNC IL_RETCODE GetRandom(IL_BYTE *out_pRandom, IL_DWORD RandomLen);

//  Description:
//		Вычисляет хэш-значение СНИЛС.
//  See Also:
//  Arguments:
//      in_pSnils	 - Указатель на входной буфер размером 6 байт с "сырым" значением СНИЛС. 
//		out_pHashBuf - Указатель на выходной буфер с вычисленным хэш-значением СНИЛС.
//		out_pHashLen - Указатель на возвращаемое значение длины вычисленнгог хэш-значения СНИЛС.
//		ifGost		 - Тип используемого криптоалгоритма ГОСТ/RSA.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Вычисление хэш-значения СНИЛС.
IL_FUNC IL_RETCODE cryptoGetHashSnils(IL_BYTE *in_pSnils, IL_BYTE *out_pHashBuf, IL_WORD *out_pHashLen, IL_BYTE ifGost);



IL_FUNC IL_RETCODE RsaKeyFromCertificateEx(IL_TAG rootTag, IL_BYTE* pKeyCert, IL_WORD wKeyCertLen, KEY_RSA* pKeyRSA);
IL_FUNC IL_RETCODE GostKeyFromCertificateEx(IL_TAG rootTag, IL_BYTE* pKeyCert, IL_WORD wKeyCertLen, KEY_GOST_PUB* pKeyGostPub);
IL_FUNC void MakeRsaCertificate(IL_BYTE* msg, IL_WORD msg_len, KEY_RSA* pKeyRSA, IL_BYTE* cert, IL_WORD* cert_len);
IL_FUNC void EncryptRsa(IL_BYTE* data, IL_WORD data_len, KEY_RSA* pKeyRSA, IL_BYTE* enc_data, IL_WORD* enc_data_len);
IL_FUNC IL_RETCODE DecryptRsa(IL_BYTE* enc_data, IL_WORD enc_data_len, KEY_RSA* pKeyRSA, IL_BYTE* data, IL_WORD* data_len);
IL_FUNC void KDF(IL_BYTE* MSK16, IL_BYTE* R, IL_WORD R_len, IL_BYTE* SMK16);
IL_FUNC void KDF_GOST(IL_BYTE* MSK32, IL_BYTE* R, IL_WORD R_len, IL_BYTE* SMK32);
IL_FUNC void MKDF(IL_BYTE* IMK16, IL_BYTE* DD, IL_WORD DD_len, IL_BYTE* MK16);
IL_FUNC void MKDF_GOST(IL_BYTE* IMK32, IL_BYTE* DD, IL_WORD DD_len, IL_BYTE* MK32);
IL_FUNC IL_RETCODE cryptoSetTerminalToProviderSessionRsa(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Cspaid_buf, IL_WORD wCspaid_size, IL_BYTE* msg, IL_DWORD msg_len, PROVIDER_SESSION_DATA* pSessDataOut);
IL_FUNC IL_RETCODE cryptoSetTerminalToProviderSessionGost(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* bPubKey64, IL_BYTE* bPrivKey32, IL_BYTE* Msg, IL_DWORD Msg_len, IL_BYTE* bRand, IL_WORD wRandLen);
IL_FUNC IL_RETCODE cryptoEncryptTerminalToProvider(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* EncMsg, IL_DWORD* pdwMsgLenEncrypted, IL_BYTE ifGost);
IL_FUNC IL_RETCODE cryptoDecryptTerminalToProvider(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* DecryptedMsg, IL_DWORD* pdwDecryptedMsgLen, IL_BYTE ifGost);

#endif