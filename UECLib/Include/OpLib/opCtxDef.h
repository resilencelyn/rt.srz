/**  Определение контекста системы UEC
  */
#ifndef __OP_CTXDEF_H_
#define __OP_CTXDEF_H_

#ifdef __cplusplus
extern "C" {
#endif

#include "CardLib.h"
#include "FuncLib.h"
#include "opDescr.h"

	
#define UEC_MAX_STATES					32                  // Максимальная глубина вложения автоматов 

#define UEC_TMPBUF_LEN					(1024*8)			// Размер буфера временного хранения данных
#define UEC_AUTHREQISSSESBUF_LEN		1024				// Размер буфера хранения параметров запроса на аутентификацию для установления защищённой сессии с эмитентом
#define BIN_TLV_DATA_LEN				(1024*5)			//+++ Размер буфера хранения TLV-данных из бинарного файла

#define	MAX_EX_SECTORS					5
#define	MAX_EX_BLOCKS					20
#define	MAX_EX_DATAS					50

typedef struct 
{
	IL_BYTE *pMsg;
	IL_WORD MsgLen;
	IL_BYTE *pS;
	IL_WORD SLen;
} PROVIDER_AUTH_DATA;

/**  Контекст системы
  *  Структура не должна быть упакована, иначе в некоторых ПУ может произойти нарушение доступа
  * к полям структуры из-за архитектурых особенностей организации памяти.
  * В контексте должна быть всегда актуальные данные!
  */
typedef struct
{
    IL_WORD	State[UEC_MAX_STATES];		// Массив внутренних состояний 
    IL_BYTE	Index;						// Индекс текущего автомата 
    IL_WORD	InterruptEvent;				// Код внешнего события-прерывания 
    IL_WORD	wCntCycles;					// Счетчик системы предотвращения зацикливания автоматов 

	IL_CHAR PAN[23];					// Внешний номер карты (счёта) 
	IL_CHAR	AppVersion[4];				// Спецификация версии карты 
	IL_CHAR	EffectiveDate[7];			// Дата начала срока действия карты 
	IL_CHAR	ExpirationDate[7];			// Дата окончания срока действия карты УЭК 

	IL_CARD_HANDLE* phCrd;				// Описатель карт-ридера  
	IL_WORD	OperationCode;				// Код выполняемой операции (см. opDescr.h) 		
    IL_WORD	ResultCode;					// Результат выполнения операции (см. il_error.h) 

	IL_BYTE PinNum;						// Номер ПИН 	
	IL_BYTE PinBlock[8];				// ПИН-блок для предъвления на карту 
	IL_BYTE PinTriesLeft;				// Количество оставшихся попыток ввода ПИН/КРП 
	IL_CHAR *pNewPinStr;				// Указатель на строку с новым паролем
	IL_CHAR *pConfirmPinStr;			// Указатель на строку с подтверждающим паролем
	IL_CHAR PassPhrase[PASS_PHRASE_MAX_LEN+1]; // Буфер для фразы контрольного приветствия
	IL_BYTE ifPassPhraseUsed;			// признак использования механизма контрольного приветствия

	IL_BYTE *pMetaInfo;					// Указатель на внешний буфер метаинформации по услуге
	IL_WORD MetaInfoLen;				// Длина метаинформации
	IL_BYTE MataInfoCrc;				// Контрольная сумма метаинформации

	IL_BYTE *pExtraData;				// Указатель на внешний буфер дополнительных параметров услуги
	IL_WORD ExtraDataLen;				// Длина дополнительных параметров услуги
	IL_BYTE ExtraDataCrc;				// Контрольная сумма дополнительных параметров услуги

	IL_BYTE *pRequestHash;				// Указатель на внешний буфер хэш XML-запроса оказания услуги
	IL_WORD RequestHashLen;				// Длина хэш XML-запроса оказания услуги
	IL_BYTE RequestHashCrc;				// Контрольная сумма хэш XML-запроса оказания услуги

	IL_CHAR *pSectorsDescrBuf;			// Указатель на внешний буфер для описателя имеющихся на карте секторв 
	IL_WORD *pSectorsDescrLen;			// Указатель на максимальный размер буфера описателя секторов

	IL_CHAR *pCardDataDescr;			// Указатель на строку-описатель считываемых с карты данных
	IL_CHAR *pCardDataBuf;				// Указатель на внешний буфер для считываемых с карты данных
	IL_WORD *pCardDataLen;				// Указатель на максимальный размер буфера и длину считанных с карты данных

	IL_CHAR *pBlockDescr;				// Указатель на строку-описатель сектора и блока считываемых с карты данных
	IL_CHAR *pBlockDataBuf;				// Указатель на внешний буфер для считываемых из блока данных 
	IL_WORD *pBlockDataLen;				// Указатель на максимальный размер буфера и длину считанных из блока данных

	IL_BYTE *pPhotoBuf;					// Указатель на буфер для считываемых с карты данных фотографии
	IL_WORD *pPhotoLen;					// Указатель на максимальный размер буфера фотографии и длину считанных в буфер данных

	IL_BYTE ifAuthOnline;				// Режим футентификации ИД-приложения (0-Offline, 1-Online)
	IL_BYTE *pAuthRequestBuf;			// Указатель на внешний буфер запроса на аутентификацию ИД-приложения
	IL_WORD *pAuthRequestLen;			// Указатель на максимальный размер буфера и длину сформированного запроса на аутентификацию ИД-приложения
	IL_BYTE AuthRequestCrc;				// Контрольная сумма запроса на аутентификацию ИД-приложения

	IL_BYTE *pAuthResponseData;			// Указатель на ответ с результатами аутентификации 
	IL_WORD AuthResponseLen;			// Длина ответа аутентификации				
	IL_BYTE *pKeyCert;					// Указатель на цепочку сертификатов для проверки результатов аутентификации
	IL_WORD KeyCertLen;					// Длина цепочки сертификатов
	IL_WORD AuthResult;					// Код аутентификации ИД-приложения 

	IL_BYTE *pDigitalSignBuf;			// Указатель на внешний буфер для ЭЦП держателя карты
	IL_WORD *pDigitalSignLen;			// Указатель на максимальный размер буфера и длину сформированной ЭЦП
	IL_BYTE *pDigitalSignCertChain;		// Указатель на внешний буфер для цепочки сертификатов ключа проверки ЭЦП держателя карты
	IL_WORD *pDigitalSignCertChainLen;	// Указатель на максимальный размер буфера цепочки сертификатов и длину цепочки

	IL_BYTE AuthRequestIssSessionBuf[1024];		// Буфер для хранения параметров запроса на аутентификацию ИД-приложения для установления сессии с эмитентом	
	IL_WORD AuthRequestIssSessionLen;			// Фактическая длина сформированного буфера запроса

	IL_APDU_PACK_ELEM *pApduPacket;				// Указатель на пакет APDU-комманд 
	IL_BYTE isApduEncryptedPS;					// Флаг необходимости шифрования/расшифрования пакета на сессионном ключе Поставщика Услуги
	IL_WORD *pApduPacketSize;					// Указатель на размер входного пакета/количество успешно выполненных APDU-команд пакета
	IL_WORD *pApduPacketResult;					// Указатель на код возврата выполнения пакета APDU-комманд
	IL_BYTE *pApduIn;							// Указатель на внешний входной буфер пакета APDU-команд
	IL_WORD ApduInLen;							// Длина буфера пвкета APDU-команд
	IL_BYTE *pApduOut;							// Указатель на выходной буфер с результатами выполнения APDU-команд пакета
	IL_WORD *ApduOutLen;						// Указатель на максимальный размер буфера и длину возвращаемых данных APDU-команд пакета

	IL_WORD SectorIdAuth;						// Идентификатор сектор для аутентификации терминала 

	SECTOR_DESCR ExSectorDescr[MAX_EX_SECTORS];	// Массив внешних описателей секторов
	IL_WORD ExSectorsNum;						// Количество описателей внешних секторов
	BLOCK_DESCR ExBlockDescr[MAX_EX_BLOCKS];	// Массив внешних описателей блоков
	IL_WORD ExBlocksNum;						// Количество описателей внешних блоков
	DATA_DESCR ExDataDescr[MAX_EX_DATAS];		// Массив внешних описателей данных
	IL_WORD ExDatasNum;							// Количество описателей внешних данных
	IL_CHAR *pExSectorDesr;						// Указатель на строку-описатель добавляемого внешнего сектора 

	DATA_DESCR *pFirstEditDataDescr;			// Указатель на описатель первого элемента редактируемых данных 
	DATA_DESCR *pFirstEditDataDescrCopy;		// Копия указателя на описатель первого элемента редактируемых данных 

	ISSUER_SESSION_DATA_IN  sess_in;			// Данные карты для установки защищённой сессии
	ISSUER_SESSION_DATA_OUT sess_out;			// Конкатенированные данные хоста для установки защищённой сессии
	CHECK_ISSUER_SESSION_DATA_IN chk_sess_in;	// Данные для проверки установленной защищённой сессии

	IL_BYTE isProviderSession;					// Флаг установки защищённой сессии с Поставщиком Услуги
	IL_BYTE ifGostPS;							// Тип используемого криптоалгоритма защищённой сессии с Поставщиком Услуг
	IL_BYTE *pCSpId;							// Указатель на сертификат ОК Поставщика Услуг
	IL_WORD CSpIdLen;							// Длина сертификата ОК Поставщика Услуг
	PROVIDER_SESSION_DATA ProviderSessionData;	// Параметры сессии с Поставщиком Услуг 
	PROVIDER_SM_CONTEXT PSMContext;				// Криптоконтекст защищённой сессии с Поставщиком Услуг 
	PROVIDER_AUTH_DATA ProviderAuthData;		// Данные для аутентификации Поставщика Услуг
	IL_BYTE *pClearData;						// Указатель на незашифрованные данные
	IL_DWORD *pClearDataLen;					// Указатель на длину незашифрованных данных
	IL_BYTE *pEncryptedData;					// Указатель на зашифрованные данные
	IL_DWORD *pEncryptedDataLen;				// Указатель на длину незашифрованных данных

#ifdef SM_SUPPORT
	IL_BYTE SE_IfActivateOnline;				// Признак активации\деактивации МБ в режиме Online
	IL_BYTE SE_IfGostIssSession;				// Признак установки криптосессии с эмитентом в режиме ГОСТ
	IL_BYTE *pSeOwnerName;						// Указатель на внешний буфер с данными владельца МБ
	IL_WORD SeOwnerNameLen;						// Длина данных владельца МБ	
	SM_SESSION_DATA_IN SE_SessIn;				// Данные МБ для установки защищённой сессии
	SM_SESSION_DATA_OUT SE_SessOut;				// Конкатенированные данные хоста для установки защищённой сессии c МБ
#endif//SM_SUPPORT

	IL_BYTE TmpBuf[UEC_TMPBUF_LEN];				// Буфер для временного хранения данных  	
	IL_WORD wTmp;								// Временная WORD-переменная
	IL_BYTE bTmp;								// Временная BYTE-переменная	

	BINTLV_DESCR BinTlvDescr;					// Описатель кешированных BinTlv-данных	

#ifdef GUIDE
	void (*pDisplayText)(IL_CHAR*);				// Указатель на функцию вывода текстовой строки на дисплей 
#endif//GUIDE
} s_opContext; 


#ifdef __cplusplus
}
#endif

#endif//__OP_CTXDEF_H_
