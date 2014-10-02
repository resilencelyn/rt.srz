#ifndef __OP_DESCR_H_
#define __OP_DESCR_H_

// максимальная длина ответа карты
#define UEC_CARD_RESP_MAX_LEN			256	
// максимальная длина возвращаемых данных
#define UEC_DATA_OUT_MAX_LEN			256		

// инициализировать список описателей секторов 
#define UEC_SECTOR_TYPE					1
// инициализировать список описателей блоков текущего сектора
#define UEC_BLOCK_TYPE					0		

// тип блока данных - TLV
#define BLOCK_DATA_TLV					0
// тип блока данных - BIN
#define BLOCK_DATA_BIN					1
// тип блока данных - RECORD
#define BLOCK_DATA_RECORD				2
// тип блока данных - BINTLV
#define BLOCK_DATA_BINTLV				3

#define BLOCK_DATA_TYPE_MAX				BLOCK_DATA_BINTLV

#define MAX_TAG_IDS_DESCR				10 // максимальное количество описателей индивидуальных тегов

// режим компиляции блока BinTlv-данных - обновить значение элемета
#define BINTLV_DATA_UPDATE				0
// режим компиляции блока BinTlv-данных - добавить элемент 
#define BINTLV_DATA_ADD					1
// режим компиляции блока BinTlv-данных - удалить элемент 
#define BINTLV_DATA_DELETE				2
// режим компиляции блока BinTlv-данных - удалить элементы шаблона 7F7F перед обновлением типа документа '9F7F' 
#define BINTLV_DATA_UPDATE_EX			3 


// тип данных: 'a' - символьная информация 
#define DATA_ASCII						0
// тип данных: 'cn' - целое чисдо без знака, выравненное по левому краю и дополненое спрапва 'F'
#define DATA_NUMERIC					1	
// тип данных: 'b' - бинарные данные
#define DATA_BYTE						2	
// тип данных: 'b' - пол (1-муж 2-жен)
#define DATA_ISO5218					3
// тип данных: 'n' - целое чисдо без знака, выравненное по правому краю и дополненое слева '0'
#define DATA_NUMERIC2					4			

// Операция: Предоставление услуги 
#define UEC_OP_PROVIDE_SERVICE			1
// Операция: Смена ПИН 
#define UEC_OP_CHANGE_PIN				2	
// Операция: Разблокировка ПИН 
#define UEC_OP_UNLOCK_PIN				3	
// Операция: Смена КРП 	
#define UEC_OP_CHANGE_PUK				4
// Операция: Разблокировка КРП
#define UEC_OP_UNLOCK_PUK				5
// Операция: Смена контрольного приветствия
#define UEC_OP_CHANGE_PASS_PHRASE		6
// Операция: Изменение частных данных гражданина
#define UEC_OP_EDIT_PRIVATE_DATA		7	
// Операция: Изменение данных оператора
#define UEC_OP_EDIT_OPERATOR_DATA		8
// Операция: Удалённое управление контентом ИД-Приложения
#define UEC_OP_REM_MANAGE_IDAPP_DATA	9
// Операция: Удалённое управление контентом карты
#define UEC_OP_REM_MANAGE_CARD_DATA	    10
// Операция: Добавление описателя внешнего сектора приклодных данных
#define UEC_OP_ADD_SECTOR_EX_DESCR		11	
// Операция: Активация модуля безопасности (МБ)
#define UEC_OP_SE_ACTIVATE				12
// Операция: Деактивация модуля безопасности (МБ)
#define UEC_OP_SE_DEACTIVATE			13
// Операция: Смена ПИН модуля безопасности (МБ)
#define UEC_OP_SE_CHANGE_PIN			14
// Операция: Разблокировка ПИН модуля безопасности (МБ)
#define UEC_OP_SE_UNLOCK_PIN			15
// Операция: Смена КРП модуля безопасности (МБ)
#define UEC_OP_SE_CHANGE_PUK			16	
// Операция: Удалённое управление контентом модуля безопасности (МБ)
#define UEC_OP_SE_REM_MANAGE			17

#define UEC_OP_END						18

#define ICON_MAX_LEN					51
// Описатель сектора
typedef struct
{
	IL_WORD	Id;					// идентификатор сектора, которому принадлежит блок
	IL_CHAR Icon[ICON_MAX_LEN];	// наименование сектора
} SECTOR_DESCR;

// Описатель блока
typedef struct
{
	IL_WORD		SectorId;			// идентификатор сектора, которому принадлежит блок
	IL_WORD		Id;					// идентификатор блока
	IL_BYTE		FileType;			// идентификатор типа блока данных 
	IL_DWORD	RootTag;			// корневой тег (для блока данных BLOCK_DATA_BINTLV)	
	IL_CHAR		Icon[ICON_MAX_LEN];	// наименование блока
} BLOCK_DESCR;

// Описатель элемента данных
#define TPATH_MAX_LEN					3
typedef struct
{
	IL_WORD	 SectorId;				// идентификатор сектора
	IL_WORD	 BlockId;				// идентификатор блока данных
	IL_WORD	 TagId;					// идентификатор тега/данного 
	IL_WORD	 Type;					// тип (DATA_ASCII|DATA_NUMERIC|DATA_BYTE|DATA_NUMERIC2)
	IL_WORD	 MaxLen;				// максимальная длина на карте
	IL_WORD	 TPath[TPATH_MAX_LEN];	// путь от корневого тега до тега данного
	IL_BYTE  isMust;				// флаг обязательности наличия элемента
	IL_CHAR  Name[ICON_MAX_LEN];	// наименование
} DATA_DESCR;

// Описатель кешированных BinTlv-данных 
typedef struct
{
	IL_WORD	 SectorId;				// идентификатор сектора
	IL_WORD	 BlockId;				// идентификатор блока данных
	IL_BYTE  *pData;				// указатель на буфер для хранения считанных с карты бинарных TLV-данных
	IL_WORD  DataLen;				// Фактическая длина считанных в буфер бинарных TLV-данных
} BINTLV_DESCR;

#endif//__OP_DESCR_H_

