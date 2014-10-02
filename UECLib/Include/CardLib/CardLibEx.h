#ifndef _CARDLIB_EX_H_
#define _CARDLIB_EX_H_

#include "il_types.h"

// описатель секторов/блоков данных
typedef struct
{
    IL_BYTE id;   // идентификатор сектора/блока
    IL_BYTE fid[2];  // идентификатор доступа к каталогу/файлу
    IL_BYTE version; // версия файла
} IL_RECORD_DEF;

// список секторов/блоков данных
#define IL_MAX_RECORDS 32  // максимальное количество записей в описателе

typedef struct
{
    IL_RECORD_DEF rec[IL_MAX_RECORDS];  // массив описателей секторов/блоков данных 
    IL_WORD num_records;     // количество записей в массиве
} IL_RECORD_LIST;

typedef struct 
{
	IL_HANDLE_READER hRdr;		// дескриптор смарт-карт ридера
	IL_HANDLE_CRYPTO hCrypto;	// дескриптор криптосессии
	IL_BYTE AppVer;				// версия карты
	IL_BYTE KeyVer;				// версия открытого ключа УЦ ЕПСС УЭК
	IL_BYTE AUC[2];				// сведения о применении ИД-приложения
	IL_BYTE AppStatus;			// статус ИД-приложения
	IL_BYTE ifLongAPDU;			// признак поддержки "длинных" команд
	IL_BYTE ifGostCrypto;		// признак использования криптоалгоритма ГОСТ 
	IL_BYTE ifSM;				// признак установки защищённой сессии  
	IL_BYTE ifNeedMSE;			// признак использования специальных команд MSE
	IL_BYTE ifSign;             // признак наличия поддержки электронной подписи держателя карты
	IL_WORD currDF;             // селектированный DF
	IL_WORD currEF;             // селектированный EF
    IL_RECORD_LIST sectors;     // список секторов
    IL_RECORD_LIST blocks;      // список блоков
	IL_BYTE CIN[10];			// учётный номер карты из главного домена безопасности 
} IL_CARD_HANDLE;

//  Description:
//      Открывает сессию с картой УЭК.
//  See Also:
//		clCardClose
//  Arguments:
//      phCard		- Указатель на описатель карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Открытие сессии с картой УЭК.
IL_FUNC IL_RETCODE clCardOpen(IL_CARD_HANDLE* phCrd);

//  Description:
//      Закрывает сессию с картой УЭК.
//  See Also:
//		clCardOpen
//  Arguments:
//      phCard		- Указатель на описатель карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Зыкрытие сессии с картой УЭК.
IL_FUNC IL_RETCODE clCardClose(IL_CARD_HANDLE* phCrd);

//  Description:
//      Выполняет APDU-команду 'SELECT' для для выбора приложения, файла каталога или элементарного файла.
//  See Also:
//  Arguments:
//      phCard		 - Указатель на описатель карты.
//		P1			 - Тип выбираемого объекта:
//						- '00' – выбор файла-каталога (DF), файла данных (EF) или главного файла (ADF). 
//						- '04' – выбор приложения по идентификатору (по AID приложения).
//		P2			 - Тип возвращаемых данных:
//						- '00' – вернуть данные выбора приложения. 
//						- '04' – вернуть File Control Parameters (FCP), доступно только для EF. 
//						- '08' – вернуть File Management Data (FMD), доступно только для ADF и DF. 
//						- '0C' – не возвращать информацию о выбранном объекте. 
//		in_pId		 - Указатель на идентификатор выбираемого объекта.
//		IdLen		 - Длина идентификатора.
//      out_pData	 - Указатель на буфер для возвращаемых данных.
//		out_pDataLen - Указатель на переменную, инициализируемую длиной возвращаемых данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение команды выбора приложения или файла.
IL_FUNC IL_RETCODE clAppSelect(IL_CARD_HANDLE* phCrd, IL_BYTE P1, IL_BYTE P2, IL_BYTE  *in_pId, IL_BYTE IdLen, IL_BYTE *out_pData, IL_WORD * out_pDataLen);

//  Description:
//      Выполняет APDU-команду 'UPDATE BINARY' для записи данных в бинарный файл по указанному смещению.
//  See Also:
//  Arguments:
//      phCard		- Указатель на описатель карты.
//      Offset		- Смещение в байтах от начала файла до записываемых данных.
//		in_pData	- Указатель на буфер с записываемыми данными.
//      DataLen		- Длина записываемых данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение команды записи данных в бинарный  файл.
IL_FUNC IL_RETCODE clAppUpdateBinary(IL_CARD_HANDLE *phCrd, IL_WORD Offset, IL_BYTE *in_pData, IL_WORD DataLen);

//  Description:
//      Выполняет APDU-команду 'GET DATA' для получения значения элемента данных из TLV-файла по указанному тегу.
//  See Also:
//		clAppPutData
//  Arguments:
//      phCard		 - Указатель на описатель карты.
//      Tag			 - Тег элемента данных.
//		out_pData	 - Указатель на буфер для считываемых данных.
//      out_pDataLen - Указатель на переменную для длины считанных данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение команды получения значения элемента данных из TLV-файла.
IL_FUNC IL_RETCODE clAppGetData(IL_CARD_HANDLE *phCrd, IL_WORD Tag, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

/* Description
   Выполняет APDU-команду 'PUT DATA' для установки значения
   элемента данных из TLV-файла по указанному тегу.
   See Also
   clAppGetData
   Parameters
   phCard :    Указатель на описатель карты.
   Tag :       Тег элемента данных.
   in_pData :  Указатель на буфер для устанавливаемых данных.
   DataLen :   Длина устанавливаемых данных.
   Returns
   IL_RETCODE - Код ошибки.
   Summary
   Выполнение команды установки значения элемента данных в
   TLV-файл.                                                  */
IL_FUNC IL_RETCODE clAppPutData(IL_CARD_HANDLE *phCrd, IL_WORD Tag, IL_BYTE *in_pData, IL_WORD DataLen);

/* Description
   Выполняет APDU-команду 'MANAGE SECURE ENVIRONMENT (MSE)' для
   явного переключения контекстов безопасности приложения карты.
   В качестве входных данных используется блок данных длиной 3
   байта.
   See Also
   clAppGetData
   Parameters
   phCard :  Указатель на описатель карты.
   P1 :      'F3' (RESTORE).
   P2 :      Контекст безопасности.
             * '01' – контекст безопасности №1 (российские
               криптоалгоритмы)
             * '02' – контекст безопасности №2 (зарубежные
               криптоалгоритмы) 
   P3 :      1\-й байт входных данных.
   P4 :      2\-й байт входных данных.
   P5 :      3\-й байт входных данных.
   Returns
   IL_RETCODE - Код ошибки.
   Summary
   Выполнение команды переключения контекстов безопасности
   приложения карты.                                             */
IL_FUNC IL_RETCODE clMSE(IL_CARD_HANDLE* phCard, IL_BYTE P1, IL_BYTE P2, IL_BYTE P3, IL_BYTE P4, IL_BYTE P5);

//  Description:
//      Выполняет APDU-команду 'READ RECORD' для для считывания записи из RECORD-файла записей линейной структуры.
//  See Also:
//		clAppUpdateRecord
//		clAppAppendRecord
//  Arguments:
//      phCard		- Указатель на описатель карты.
//      RecNumber	- Смещение в байтах от начала файла до считываемых данных.
//		ExpLen		- Ожидаемая длина считываемых данных.
//		out_pData	- Указатель на буфер для считываемых данных.
//      out_DataLen	- Указатель на переменную, инициализируемую длиной считанных данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение команды считывания записи из RECORD-файла.
IL_FUNC IL_RETCODE clAppReadRecord(IL_CARD_HANDLE *phCard, IL_BYTE RecNumber, IL_BYTE ExpLen, IL_BYTE *out_pData, IL_WORD *out_DataLen);

//  Description:
//      Выполняет APDU-команду 'UPDATE RECORD' для обновления содержимого записи в RECORD-файле записей линейной структуры.
//  See Also:
//		clAppReadRecord
//		clAppAppendRecord
//  Arguments:
//      phCard		- Указатель на описатель карты.
//      RecNumber	- Смещение в байтах от начала файла до считываемых данных.
//		in_pData	- Указатель на буфер с обновляемыми данными.
//      DataLen		- Длина обновляемых данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение команды обновления записи в RECORD-файле.
IL_FUNC IL_RETCODE clAppUpdateRecord(IL_CARD_HANDLE *phCard, IL_BYTE RecNumber, IL_BYTE *in_pData, IL_WORD DataLen);

//  Description:
//      Выполняет APDU-команду 'APPEND RECORD' для добавления записи в конец RECORD-файла записей линейной структуры.
//  See Also:
//		clAppReadRecord
//		clAppUpdateRecord
//  Arguments:
//      phCard		- Указатель на описатель карты.
//		in_pData	- Указатель на буфер с добавляемыми данными.
//      DataLen		- Длина добавляемых данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение команды добавления записи в RECORD-файл.
IL_FUNC IL_RETCODE clAppAppendRecord(IL_CARD_HANDLE *phCard, IL_BYTE *in_pData, IL_WORD DataLen);

#endif//_CARDLIB_EX_H_