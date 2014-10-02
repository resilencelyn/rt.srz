#ifndef _CARDLIB_H_
#define _CARDLIB_H_

#include "CardLibEx.h"
#include "il_version.h"
#include "HAL_SCReader.h"
#include "HAL_Crypto.h"

#define PATCH_9F36	//+++ ЗАПЛАТКА ДЛЯ ТЕГА '9F36' СОГЛАСНО ПИСЬМУ УЭК ОТ 01.01.2013

// Команда 'VERIFY' - верификация держателя карты
#define INS_VERIFY		0x20
// Команда 'MANAGE SECURE ENVIRONMENT' - переключение контекстов безопасности приложения карты
#define INS_MSE 		0x22
// Команда 'CHANGE REFERENCE DATA' - установка (изменение) значения критических данных пароля
#define INS_CHANGE_DATA	0x24
// Команда 'PERFORM SECURITY OPERATION' - выполнение криптографических операций
#define INS_PERF_SEC_OP	0x2A
// Команда 'RESET RETRY COUNTER' - разблокировка паролей и установка счётчика оставшихся попыток его предъявления в максимальное значение
#define INS_RST_CNTR	0x2C
// Команда 'MUTUAL (EXTERNAL) AUTHENTICATE' - аутентификации внешнего субъекта со стороны ИД-приложения. 
#define INS_MUT_AUTH	0x82
// Команда 'GET CHALLENGE' - получение от ИД-приложения случайного  числа
#define INS_GET_CHAL	0x84
// Команда 'INTERNAL AUTHENTICATE' - аутентификация ИД-приложения со стороны ФУО
#define INS_INT_AUTH	0x88
// Команда 'SELECT' - выбор приложения или файла
#define INS_SELECT		0xA4
// Команда 'READ BINARY' - чтение данных из бинарного  файла
#define INS_READ_BIN	0xB0
// Команда 'READ RECORD' - считывание записи из файла записей линейной структуры
#define INS_READ_REC	0xB2
// Команда 'GET RESPONSE' - получение ответа от карты
#define INS_GET_RESP	0xC0
// Команда 'GET DATA' - получение значений элементов данных идентификационного приложения по тэгу 
#define INS_GET_DATA	0xCA
// Команда 'UPDATE BINARY' - запись данных в бинарный файл
#define INS_UPDATE_BIN	0xD6
// Команда 'UPDATE RECORD' - обновление содержимого записи файла записей линейной  структуры
#define INS_UPDATE_REC	0xDC
// Команда 'PUT DATA' - установка значений элементов данных ИД-приложения по тэгу
#define INS_PUT_DATA	0xDA
// Команда 'APPEND RECORD' - добавление записи в конец файла записей линейной структуры 
#define INS_APPEND_REC	0xE2

// Режим передачи APDU-команды по открытому каналу
#define SM_MODE_NONE        0
// Режим передачи APDU-команды по закрытому каналу при условии его установки
#define SM_MODE_IF_SESSION  1
// Режим передачи APDU-команды по закрытому каналу
#define SM_MODE_ALWAYS      2

//  Description:
//      Выполняет APDU-команду карты УЭК.
//  See Also:
//  Arguments:
//      phCard		- Указатель на описатель карты.
//      in_pilApdu	- Указатель на описатель APDU-команды.
//      SM_MODE		- Режим передачи APDU-команды:
//						- SM_MODE_NONE - по открытому каналу.
//						- SM_MODE_IF_SESSION - по закрытому каналу при условии его установки.
//						- SM_MODE_ALWAYS - по закрытому каналу.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение APDU-команды.
IL_FUNC IL_RETCODE clAPDU(IL_CARD_HANDLE *phCard, IL_APDU *in_pilApdu, IL_BYTE SM_MODE);

/* Description
   Выполняет APDU-команду 'PERFORM SECURITY OPERATION'
   криптографической операции.<p />
   Используется для верификации цепочки сертификатов при
   проверки открытого ключа и электронной подписи держателя
   карты.
   See Also
   Parameters
   phCard :      Указатель на описатель карты.
   CLA :         Класс команды\:
                 * '00' – при передаче команды по открытому
                   каналу.
                 * '0C' – при передаче команды по защищённому
                   каналу (Secure messaging). 
   in_pDataIn :  Входные данные. В зависимости от контекста
                 применения или сертификат проверяемого открытого
                 ключа, или хэш\-значение исходного сообщения.
   DataLen :     Длина входных данных.
   Returns
   IL_RETCODE - Код ошибки.
   Summary
   Выполнение команды криптографической операции.                 */
IL_FUNC IL_RETCODE clAppPerformSecOperation(IL_CARD_HANDLE *phCrd, IL_BYTE CLA, IL_BYTE *in_pDataIn, IL_WORD DataLen);

//  Description:
//      Выполняет APDU-команду 'READ BINARY' для чтения данных из бинарного файла по указанному смещению.
//  See Also:
//  Arguments:
//      phCard		- Указатель на описатель карты.
//      Offset		- Смещение в байтах от начала файла до считываемых данных.
//      DataLen		- Длина считываемых данных.
//		out_pData	- Указатель на буфер для считываемых данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение команды чтения данных из бинарного файла.
IL_FUNC IL_RETCODE clAppReadBinary(IL_CARD_HANDLE *phCrd, IL_WORD Offset, IL_WORD Length, IL_BYTE *out_pData);

//  Description:
//      Выполняет APDU-команду 'GET CHALLENGE' для получения от ИД-приложения случайного числа.
//  See Also:
//  Arguments:
//      phCard			- Указатель на описатель карты.
//      Length			- Требуемая длина случайного числа.
//		out_pChallenge	- Указатель на буфер для возвращаемого случайного числа.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение команды получения случайного числа ИД-приложения.
IL_FUNC IL_RETCODE clAppGetChallenge(IL_CARD_HANDLE *phCrd, IL_WORD Length, IL_BYTE *out_pChallenge);

/* Description
   Выполняет APDU-команду 'MUTUAL (EXTERNAL) AUTHENTICATE' для
   аутентификации внешнего субъекта со стороны ИД-приложения.
   See Also
   Parameters
   phCard :        Указатель на описатель карты.
   CLA :           Класс команды. '00' при передаче команды по
                   открытому каналу.
   P2 :            Идентификатор используемого ключа.
   in_pDataIn :    Входные данные для аутентификации.
   DataInLen :     Длина входных данных.
   out_pData :     Указатель на буфер для возвращаемых данных
                   криптограммы или компонентов общего секрета.
   out_pDataLen :  Указатель на переменную для длины возвращаемых
                   данных.
   Returns
   IL_RETCODE - Код ошибки.
   Summary
   Выполнение команды аутентификации ИД-приложением внешнего
   субъекта.                                                      */
IL_FUNC IL_RETCODE clAppMutualAuth(IL_CARD_HANDLE *phCrd, IL_BYTE CLA, IL_BYTE P2, IL_BYTE *in_pDataIn, IL_WORD DataInLen, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

//  Description:
//      Выполняет APDU-команду 'INTERNAL AUTHENTICATE' для аутентификации ИД-приложения со стороны ФУО.
//  See Also:
//  Arguments:
//      phCard		 - Указатель на описатель карты.
//		P2			 - Идентификатор используемого ключа. 
//      in_pDataIn	 - Входные данные для аутентификации. 
//		DataInLen	 - Длина входных данных.
//		out_pData	 - Указатель на буфер для возвращаемых данных криптограммы, подтверждающей аутентичность ИД-приложения и параметров оказываемой услуги.
//		out_pDataLen - Указатель на переменную для длины возвращаемых данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение команды аутентификации ИД-приложения со стороны ФУО.
IL_FUNC IL_RETCODE clAppInternalAuth(IL_CARD_HANDLE *phCrd, IL_BYTE P2, IL_BYTE *in_pDataIn, IL_WORD DataInLen, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

/* Description
   Выполняет APDU-команду 'VERIFY' для верификации держателя
   карты.
   See Also
   Parameters
   phCard :              Указатель на описатель карты.
   P2 :                  Идентификатор используемого пароля.
   in_pData8 :           Указатель на ПИН\-блок со значением
                         пароля в формате ISO 9564\-3\:2002
                         (Format 2).
   out_pTriesRemained :  Указатель на переменную для
                         возвращаемого значения количества
                         оставшихся презентаций пароля.
   Returns
   IL_RETCODE - Код ошибки.
   Summary
   Выполнение команды верификации держателя карты.           */
IL_FUNC IL_RETCODE clAppVerify(IL_CARD_HANDLE *phCrd, IL_BYTE P2, IL_BYTE *in_pData8, IL_BYTE *out_pTriesRemained);

//  Description:
//      Выполняет APDU-команду 'CHANGE REFERENCE DATA' для установки или изменения значения критических данных пароля.
//  See Also:
//  Arguments:
//      phCard		- Указатель на описатель карты.
//		P2			- Идентификатор изменяемых критических данных. 
//      in_pData	- Указатель на буфер с новым значением критических данных. 
//		DataLen		- Длина устанавливаемых данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение команды установки или изменения пароля.
IL_FUNC IL_RETCODE clAppChangeRefData(IL_CARD_HANDLE *phCrd, IL_BYTE P2, IL_BYTE *in_pData, IL_WORD DataLen);

//  Description:
//      Выполняет APDU-команду 'RESET RETRY COUNTER' для разблокировки паролей и установки счётчика оставшихся попыток его предъявления в максимальное значение.
//  See Also:
//  Arguments:
//      phCard		- Указатель на описатель карты.
//		P2			- Идентификатор изменяемых критических данных. 
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение команды разблокировки пароля и установки счётчика оставшихся попыток его предъявления.
IL_FUNC IL_RETCODE clAppResetRetryCounter(IL_CARD_HANDLE *phCrd, IL_BYTE P2);

//  Description:
//      Выполняет APDU-команду 'PERFORM SECURITY OPERATION' криптографической операции для проверки электронной подписи держателя карты.
//  See Also:
//  Arguments:
//      phCard		- Указатель на описатель карты.
//      in_pData	- Указатель на буфер с хэш-значением исходного сообщения. 
//		DataLen		- Длина исходного сообщения.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение команды проверки электронной подписи держателя карты.
IL_FUNC IL_RETCODE clAppComputeDigitalSignature(IL_CARD_HANDLE *phCard, IL_BYTE *in_pData, IL_WORD DataLen);

IL_FUNC IL_RETCODE clAppReadBinaryEx(IL_CARD_HANDLE* phCard, IL_WORD wOffset, IL_WORD wBufLength, IL_BYTE* pOut, IL_WORD* pwOutLen);


#endif