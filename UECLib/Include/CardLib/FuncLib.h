#ifndef _FUNCLIB_H_
#define _FUNCLIB_H_

#include "FuncLibEx.h"
#include "il_version.h" 

#define PASS_PHRASE_MAX_LEN		40

//flFileSelect - селекция DF или EF с сохранением в контексте состояния селектированных DF и EF
IL_FUNC IL_RETCODE flFileSelect(IL_CARD_HANDLE* phCrd, IL_BYTE ifDF, IL_BYTE* pId, IL_BYTE IdLen, IL_BYTE* pOut, IL_WORD* pOutLength);

//flAppTerminalAuth - аутентификация терминала
IL_FUNC IL_RETCODE flAppTerminalAuthEx(IL_CARD_HANDLE* phCrd, IL_BYTE* pOpCert, IL_WORD wOpCertSize, IL_BYTE* pTermCert, IL_WORD wTermCertSize);

//flAppGetStatus - получение состояния приложения
IL_FUNC IL_RETCODE flAppGetStatus(IL_CARD_HANDLE *phCrd, IL_BYTE *pStatusOut);

//  Description:
//		Считывает из главного домена безопасности учётный номер карты CIN.
//	See Also:
//  Arguments:
//		phCard		 - Указатель на описатель карты.
//		out_pCIN	 - Указатель на буфер для возвращаемых данных длиной 10 байт.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//		Получение учётного номера карты CIN. 
IL_FUNC IL_RETCODE flGetCIN(IL_CARD_HANDLE* phCrd, IL_BYTE *out_pCIN);

//flCheckCertificate - проверка сертификата открытого ключа картой
IL_FUNC IL_RETCODE flCheckCertificate(IL_CARD_HANDLE* phCrd, IL_BYTE* pKeyCertIn, IL_WORD wKeyCertInLen, IL_BYTE CertificateTypeToCheck);

//  Description:
//		Считывает с карты значение открытого ключа электронной подписи держателя карты ИД-приложения.
//	See Also:
//  Arguments:
//		phCard		 - Указатель на описатель карты.
//		out_pData	 - Указатель на буфер для возвращаемых данных.
//		out_pDataLen - Указатель на длину возвращаемых данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//		Получение открытого ключа электронной подписи держателя карты ИД-приложения. 
IL_FUNC IL_RETCODE flGetAuthPubKey(IL_CARD_HANDLE *phCrd, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

//  Description:
//		Считывает с карты значение открытого ключа эмитента ИД-приложения.
//	See Also:
//  Arguments:
//		phCard		 - Указатель на описатель карты.
//		out_pData	 - Указатель на буфер для возвращаемых данных.
//		out_pDataLen - Указатель на длину возвращаемых данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//		Получение открытого ключа эмитента ИД-приложения. 
IL_FUNC IL_RETCODE flGetIssPubKey(IL_CARD_HANDLE *phCrd, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

#endif