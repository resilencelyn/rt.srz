#ifndef _SMLIB_H_
#define _SMLIB_H_

#include "il_types.h"
#include "il_version.h"
#include "HAL_SCReader.h"
#include "CardLibEx.h"
#include "FuncLibEx.h"
#include "HAL_CryptoHandle.h"

#define UECLIB_SM_VER	  0x10

#define INS_AUTH_BEGIN    0x16
#define INS_AUTH_COMPLETE 0x18
#define INS_SP_SESS_INIT  0x1A
#define INS_SE_ACTIVATION 0x1C

// Описатель Модуля Безопасности
typedef struct 
{
	IL_HANDLE_READER hRdr;			// дескриптор смарт-карт ридера
	IL_BYTE AppVer;					// версия карты
    IL_RECORD_LIST sectors;			// список секторов
    IL_RECORD_LIST blocks;			// список блоков
	IL_BYTE Certificates[4][2048];	// массив сертификатов открытых ключей
	IL_WORD wCertificatesLen[4];	// массив длин сертификатов
}IL_SM_HANDLE;

//  Description:
//      Инициализация модуля безопасности.
//  See Also:
//      smDeinit
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//      ilSmRdrSettings - Указатель на параметры настройки ридера модуля безопасности
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Инициализация модуля безопасности.
IL_FUNC IL_RETCODE smInit(IL_HANDLE_CRYPTO* phCrypto, IL_READER_SETTINGS ilSmRdrSettings);

//  Description:
//      Деинициализация модуля безопасности. 
//  See Also:
//      smInit
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Денициализация модуля безопасности.
IL_FUNC IL_RETCODE smDeinit(IL_HANDLE_CRYPTO* phCrypto);

//  Description:
//      Начало работы с модулем безопасности.
//  See Also:
//      smClose
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Начало работы с модулем безопасности.
IL_FUNC IL_RETCODE smOpen(IL_HANDLE_CRYPTO* phCrypto);

//  Description:
//      Завершение работы с модулем безопасности.
//  See Also:
//      smOpen
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Завершение работы с модулем безопасности.
IL_FUNC IL_RETCODE smClose(IL_HANDLE_CRYPTO* phCrypto);

//  Description:
//      Выбор приложения модуля безопасности.
//  See Also:
//      
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//      pOut			- Указатель на возвращаемые  данные.
//      pwOutLen		- Указатель на длину возвращаемых данных.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Выбор приложения модуля безопасности.
IL_FUNC IL_RETCODE smAppSelect(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* pOut, IL_WORD* pwOutLen);

//  Description:
//      Выбор приложения или файла данных модуля безопасности.
//  See Also:
//      
//  Arguments:
//      phCrypto	- Указатель на контекст модуля безопасности.
//      P1			- Выбираемый объект:
//						- '00' – Файл данных (EF).
//						- '04' – Приложение по AID приложения.
//      P2			- Режим возврата данных:
//						- '00' – вернуть данные выбора приложения.
//						- '04' – вернуть File Control Parameters (FCP), доступно только для EF.
//						- '08' – вернуть File Management Data (FMD), доступно только для ADF.
//						- '0C' – не возвращать информацию о выбранном объекте.
//		pId			- Указатель на входные данные идентификатора селектируемого файла или приложения.
//		IdLen		- Длина входных данных.
//		pOut		- Указатель на возвращаемые  данные.
//		pOutLength	- Указатель на длину возвращаемых данных.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Выбор приложения или файла данных модуля безопасности.
IL_FUNC IL_RETCODE smSelect(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE P1, IL_BYTE P2, IL_BYTE* pId, IL_BYTE IdLen, IL_BYTE* pOut, IL_WORD* pOutLength);

//  Description:
//      Получение значений элементов данных приложения МБ по тэгу.
//  See Also:
//      
//  Arguments:
//      phCrypto	- Указатель на контекст модуля безопасности.
//		wTag		- Идентификатор тега получаемого элемента данных.
//		pOut		- Указатель на возвращаемые данные со значением элемента данного.
//		pOutLength	- Указатель на длину возвращаемых данных.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Получение значений элементов данных приложения МБ по тэгу.
IL_FUNC IL_RETCODE smGetData(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wTag, IL_BYTE* pOut, IL_WORD* pwOutLength);

//  Description:
//      Чтение содержимого бинарного файла МБ.
//  See Also:
//      
//  Arguments:
//      phCrypto	- Указатель на контекст модуля безопасности.
//		wFileId		- Идентификатор считываемого файла данных.
//		pOut		- Указатель на возвращаемые данные.
//		pOutLength	- Указатель на длину возвращаемых данных.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Чтение содержимого бинарного файла МБ.
IL_FUNC IL_RETCODE smReadFile(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wFileId, IL_BYTE* FileData, IL_WORD* pwFileDataLen);

//  Description:
//      Чтение блока данных из модуля безопасности функциями GET DATA или READ BINARY.
//  See Also:
//      
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//      wFileId			- Идентификатор файла, ноль в случае если данные читаются не из файловой области
//      wDataId			- Идентификатор (тэг) данных, ноль в случае если данные читаются из бинарного файла
//      pDataOut		- Указатель на возвращаемые  данные.
//      pwDataOutLen	- Указатель на длину возвращаемых данных.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Чтение блока данных из модуля безопасности функциями GET DATA или READ BINARY.
IL_FUNC IL_RETCODE smReadBlock(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wFileId, IL_WORD wDataId, IL_BYTE* pDataOut, IL_WORD* pwDataOutLen);

//  Description:
//      Предъявление пароля модуля безопасности.
//  See Also:
//      
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//      P2				- Параметр P2 APDU VERIFY.
//      pDataIn8		- Указатель на ПИН-блок длиной 8 байт.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Предъявление пароля модуля безопасности.
IL_FUNC IL_RETCODE smVerify(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE P2, IL_BYTE* pDataIn8, IL_BYTE* pbTriesRemained);

//  Description:
//      Смена пароля модуля безопасности.
//  See Also:
//      
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//      P2				- Параметр P2 APDU CHANGE REF DATA.
//      pDataIn8		- Указатель на ПИН-блок длиной 8 байт.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Смена пароля модуля безопасности.
IL_FUNC IL_RETCODE smChangeRefData(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE P2, IL_BYTE* pDataIn8, IL_WORD wDataInLen);

//  Description:
//      Чтение файлов сертификатов модуля безопасности. 
//		Считывает сертификаты модуля безопасности из файлов 5,6,7,8 модуля безопасности.
//		Сохраняет считанные сертификаты в контексте модуля безопасности.
//  See Also:
//      
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Чтение файлов сертификатов модуля безопасности.
IL_FUNC IL_RETCODE smReadCertificates(IL_HANDLE_CRYPTO* phCrypto);

//  Description:
//      Получение сертификата публичного ключа с заданными параметрами, считанного из модуля безопасности. 
//  See Also:
//      smGetCertificate
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//      ilParamType		- Указатель на контекст модуля безопасности.
//      KeyVer			- Версия корневого ключа сертификата.
//      ifGost			- Тип ключа сертификата: 0 - RSA, 1 - ГОСТ.
//      certGost		- Указатель на буфер для возвращаемого сертификата.
//      pdwCertGostLen	- Указатель на длину возвращаемого сертификата.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Получение сертификата публичного ключа с заданными параметрами.
IL_FUNC IL_RETCODE smGetCertificate(IL_HANDLE_CRYPTO* phCrypto, IL_WORD ilParamType, IL_BYTE KeyVer, IL_BYTE ifGost, IL_BYTE* certGost, IL_DWORD* pdwCertGostLen);

//  Description:
//      Команда вырабатывает данные для установления сессии с терминалом (1я фаза).
//  See Also:
//      smReadCertificates
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//      pIn				- Указатель на входные данные для установления сессии с терминалом.
//      InLen			- Длина входных данных.
//      pOut			- Указатель на выходные данные для установления сессии с терминалом.
//      pOutLen			- Указатель на длину выходных данных.
//      ifGost			- Тип устанавливаемой сессии: 0 - RSA, 1 - ГОСТ.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Команда вырабатывает данные для установления сессии с терминалом (1я фаза).
IL_FUNC IL_RETCODE smAuthBegin(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* pIn, IL_WORD InLen, IL_BYTE* pOut, IL_WORD* pOutLen, IL_BYTE ifGost);

//  Description:
//      Команда вырабатывает данные для установления сессии с терминалом (2я фаза).
//  See Also:
//      smAuthComplete
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//      pIn				- Указатель на входные данные для установления сессии с терминалом.
//      InLen			- Длина входных данных.
//      ifGost			- Тип устанавливаемой сессии: 0 - RSA, 1 - ГОСТ.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Чтение файлов сертификатов модуля безопасности.
IL_FUNC IL_RETCODE smAuthComplete(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* pIn, IL_WORD InLen, BYTE ifGost);

//  Description:
//		Подготовкa APDU в режиме ЗОС.
//      Команда выполняет действия по подготовке APDU в режиме ЗОС.
//  See Also:
//      smProcessSM
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//      pilApdu			- Указатель на структуру данных с подготовливаемой APDU командой.
///  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Подготовкa APDU в режиме ЗОС.
IL_FUNC IL_RETCODE smPrepareSM(IL_HANDLE_CRYPTO* phCrypto, IL_APDU* pilApdu);

//  Description:
//      Команда выполняет действия по проверке и расшифровке APDU в режиме ЗОС.
//  See Also:
//      smProcessSM
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//      pilApdu			- Указатель на структуру данных с проверяемой APDU командой.
///  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Чтение файлов сертификатов модуля безопасности.
IL_FUNC IL_RETCODE smProcessSM(IL_HANDLE_CRYPTO* phCrypto, IL_APDU* pilApdu);

//  Description:
//      Получение от модуля безопасности случайного числа. 
//  See Also:
//      
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//		wOutLength		- Длина случайного числа.
//		pOut			- Указатель на выходной буфер.  
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Получение от модуля безопасности случайного числа.
IL_FUNC IL_RETCODE smGetChallenge(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wOutLength, IL_BYTE* pOut);

//  Description:
//      Команда предназначена для аутентификации внешнего объекта со стороны модуля безопасности. 
//  See Also:
//      
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//		CLA				- Класс команды.
//		P2				- Иденификатор используемого ключа.
//		pDataIn			- Указатель на данные аутентификации.
//		wDataInLen		- Длина данных аутентификации.
//		pDataOut		- Указатель на буфер для данных сообщения-ответа команды.
//		pDataOutLen		- Указатель на длину возвращаемых дааных.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Аутентификация внешнего объекта со стороны модуля безопасности.
IL_FUNC IL_RETCODE smMutualAuth(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE CLA, IL_BYTE P2, IL_BYTE* pDataIn, IL_WORD wDataInLen, IL_BYTE* pDataOut, IL_WORD* pDataOutLen);

//  Description:
//      Команда выполняет действия по активации\деактивации модуля безопасности.
//  See Also:
//      smActivationStart
//		smOfflineActivationFinish		
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//      CLA				- Класс команды.
//		IfDeactivation	- Признак выполнения деактивации модуля безопасности
///  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Активация\Деактивация модуля безопасности.
IL_FUNC IL_RETCODE smActivation(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE CLA, IL_BYTE IfDeactivation);

//  Description:
//      Команда выполняет действия по инициализации защищённой сессии с Поставщиком Услуги.
//  See Also:
//				
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//      pDataIn14		- Входные данный для установки сессии с Поставщиком Услуги.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Инициализация сессии с Поставщиком Услуги.
IL_FUNC IL_RETCODE smSpSessionInit(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* pDataIn14);

//  Description:
//      Команда выполняет первоначальные действия процедуры активации\деактивации модуля безопасности.
//  See Also:
//				
//  Arguments:
//      phCrypto		 - Указатель на контекст модуля безопасности.
//      ifStateActivated - Указатель на возвращаемый признак активированного состояния модуля активации.
//		pAC0003			 - Указатель на возвращаемые данные с правами доступа к файлу 3 для определения режима активации.
//		pdwAC0003		 - Указатель на длина возвращаемых данных.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Инициализация процедуры активации\деактивации модуля безопасности.
IL_FUNC IL_RETCODE smActivationStart(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* ifStateActivated, IL_BYTE* pAC0003, IL_DWORD* pdwAC0003);

//  Description:
//      Команда выполняет завершающие действия процедуры активации\деактивации модуля безопасности.
//  See Also:
//				
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//      ifGost			- Признак использования криптоалгоритма ГОСТ.
//		ifDeactivate	- Признак проведения процедуры деактивации модуля безопасности.
//		pSmOwnerName    - Указатель на входные данные наименования владельца модуля безопасности.
//		wSmOwnerNameLen - Длина наименования владельца модуля безопасности.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Завершение процедуры активации\деактивации модуля безопасности.
IL_FUNC IL_RETCODE smOfflineActivationFinish(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE ifGost, BYTE ifDeactivate, IL_BYTE* pSmOwnerName, IL_WORD wSmOwnerNameLen);

//  Description:
//      Выполняет аутентификацию модулем безопасности Поставщика Услуги.
//  See Also:
//				
//  Arguments:
//      phCrypto			- Указатель на контекст модуля безопасности.
//      Msg					- Указатель на аутентифицируемые данные.
//		wMsgLen				- Длина аутентифицируемых данных.
//		S					- Указатель на сформированную Поставщиком услуги ЭЦП для аутентифицируемых данных.
//		wS_len				- Длина сформированной ЭЦП.
//		PublicKeyCert		- Указатель на сертификат открытого ключа Поставщика услуги.
//		wPublicKeyCertLen	- Длина сертификата
//		ifGost				- Признак аутентификации по криптоалгоритму ГОСТ.
//		AppVer				- Версия ИД-приложения (карты).
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Аутентификация модулем безопасности Поставщика Услуги.
IL_FUNC IL_RETCODE smAuthServiceProvider(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* Msg, IL_WORD wMsgLen, IL_BYTE* S, IL_WORD wS_len, IL_BYTE* PublicKeyCert, IL_WORD wPublicKeyCertLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      Выполнение модулем безопасности APDU-команды.
//  See Also:
//				
//  Arguments:
//      phCrypto	- Указатель на контекст модуля безопасности.
//      In			- Указатель на буфер с упакованными бинарными данными APDU-команды.
//		wInLen		- Длина данных APDU-команды.
//		Out			- Указатель на буфер для выходных данных APDU-команды.
//		pwOutLen	- Указатель на длину выходных данных.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Выполнение модулем безопасности APDU-команды.
IL_FUNC IL_RETCODE smSendAPDU(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* In, IL_WORD wInLen, IL_BYTE* Out, IL_WORD* pwOutLen);

//  Description:
//      Выполнение модулем безопасности APDU-команды пакета.
//  See Also:
//				
//  Arguments:
//      phCrypto	- Указатель на контекст модуля безопасности.
//      ilApduElem	- Указатель на элемент выполняемой APDU-команды пакета.
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Выполнение APDU-команды пакета.
IL_FUNC IL_RETCODE smRunApdu(IL_HANDLE_CRYPTO* phCrypto, IL_APDU_PACK_ELEM* ilApduElem);

//  Description:
//      Чтение наименования владельца модуля безопасности. 
//		Считывает наименования владельца модуля безопасности из файла 3.
//		Конвертирует считанную строку в кодировку Win-1251.
//  See Also:
//      
//  Arguments:
//      phCrypto		- Указатель на контекст модуля безопасности.
//		pOwnerNameOut	- Указатель на буфер для возвращаемой строки 
//  Return Value:
//      IL_RETCODE		- Код ошибки.
//  Summary:
//      Чтение наименования владельца модуля безопасности.
//---IL_FUNC IL_RETCODE smGetOwnerName(IL_HANDLE_CRYPTO* phCrypto, IL_CHAR *pOwnerNameOut);

#endif