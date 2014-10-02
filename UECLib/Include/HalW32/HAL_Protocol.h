#ifndef _HAL_PROTOCOL_H_
#define _HAL_PROTOCOL_H_

#include <stdio.h>
#include "il_types.h"
#include "il_error.h"

// Источник протоколирования - безусловное протоколирование
#define PROT_ALWAYS		0
// Источник протоколирования - функция карты
//#define PROT_CARD		1
// Источник протоколирования - уровень 1  функции карт-ридера
#define PROT_READER1	1
// Источник протоколирования - уровень 2  функции карт-ридера
#define PROT_READER2	2
// Источник протоколирования - уровень 3  функции карт-ридера
#define PROT_READER3	3
// Источник протоколирования - уровень 1  функции CardLib
#define PROT_CARDLIB1	4
// Источник протоколирования - уровень 2  функции CardLib
#define PROT_CARDLIB2	5
// Источник протоколирования - уровень 3  функции CardLib
#define PROT_CARDLIB3	6
// Источник протоколирования - уровень 1  функции FuncLib
#define PROT_FUNCLIB1	7
// Источник протоколирования - уровень 2  функции FuncLib
#define PROT_FUNCLIB2	8
// Источник протоколирования - уровень 3  функции FuncLib
#define PROT_FUNCLIB3	9
// Источник протоколирования - уровень 1  функции OpLib
#define PROT_OPLIB1		10
// Источник протоколирования - уровень 2  функции OpLib
#define PROT_OPLIB2		11
// Источник протоколирования - уровень 3  функции OpLib
#define PROT_OPLIB3		12
// Источник протоколирования - функция SmLib
#define PROT_SMLIB		13
// Источник протоколирования - уровень 1 метода TellME 
#define PROT_TELLME1	14
// Источник протоколирования - уровень 2 метода TellME
#define PROT_TELLME2	15
// Источник протоколирования - уровень 3 метода TellME
#define PROT_TELLME3	16
// Источник протоколирования - внешнее протоколирование TellME
#define PROT_EXTERN		17

// Описатель системы протоколирования
typedef struct
{
	FILE *hFile;					// дескриптор файла журнала
	IL_CHAR FileDirName[256+1];		// путь
	IL_CHAR FileName[32+1];			// имя файла
	IL_CHAR FullFileName[288+1];	// полное имя файла
	IL_BYTE DateStamp[3];			// штамп даты
	IL_BYTE ifOutput;				// признак необходимости протоколирования в окно Output
	IL_BYTE ifReader;				// флаг протоколирования функций карт-ридера
	IL_BYTE ifCard;					// флаг протоколирования функций карты
	IL_BYTE ifCardLib;				// флаг протоколирования функций CardLib
	IL_BYTE ifFuncLib;				// флаг протоколирования функций FuncLib
	IL_BYTE ifOpLib;				// флаг протоколирования функций OpLib
	IL_BYTE ifSmLib;				// флаг протоколирования функций SmLib
	IL_BYTE ifTellMe;				// флаг протоколирования уровней TellMe
	IL_BYTE ifExtern;				// флаг внешнего протоколирования TellME
} PROTOCOL_DESCR;

//  Description:
//      Инициализирует сессию протоколирования выполнения операций ИБТ УЭК.<p/>
//		Внутренний описатель системы протоколирования инициализируется параметрами конфигурационного файла 'terminal.ini'.
//  See Also:
//  Arguments:
//      Отсутствуют.
//  Return Value:
//      IL_RETCODE - Код ошибки.
//  Summary:
//      Инициализация сессии системы протоколирования.
IL_FUNC IL_RETCODE protInit(void);

//  Description:
//      Завершает сессию протоколирования выполнения операций ИБТ УЭК.
//  See Also:
//  Arguments:
//      Отсутствуют.
//  Return Value:
//      IL_RETCODE - Код ошибки.
//  Summary:
//      Денициализация системы протоколирования.
IL_FUNC IL_RETCODE protDeinit(void);

//  Description:
//      Записывает строку протоколирования указанного источника опционально в журнал и окно отладчика соответственно настройкам описателя.<p/>
//		Если в описателе не инициализирован флаг протоколирования для указанного источника, протоколирование не производится. 
//  See Also:
//  Arguments:
//      ProtSource - Спецификатор источника протоколирования.
//		Str		   - Указатель на протоколируемую строку.	
//  Return Value:
//      IL_RETCODE - Код ошибки.
//  Summary:
//      Запись строки протоколирования.
IL_FUNC IL_RETCODE protWrite(IL_WORD ProtSource, IL_CHAR *Str);

//  Description:
//      Форматирует и записывает строку протоколирования указанного источника в журнал и окно отладчика соответственно настройкам описателя.<p/>
//		Если в описателе не инициализирован флаг протоколирования для указанного источника, протоколирование не производится. 
//  See Also:
//  Arguments:
//      ProtSource - Спецификатор источника протоколирования.
//		Format	   - Указатель на строку-формат.
//		...		   - Список неявных параметров, соответствующих строке-формату. 
//  Return Value:
//      IL_RETCODE - Код ошибки.
//  Summary:
//      Форматированная запись строки протоколирования.
IL_FUNC IL_RETCODE protWriteEx(IL_WORD ProtSource, const char *Format, ...);

//
// ДЛЯ ПОЛНОГО ОТКЛЮЧЕНИЯ КОДА ПОДСИСТЕМЫ ПРОТОКОЛИРОВАНИЯ НЕОБХОДИМО РАСКОММЕНТАРИТЬ ДЕФЕНИЦИЮ 'PROT_IGNORE'
//#define PROT_IGNORE

#ifndef PROT_IGNORE
#define PROT_INIT										protInit();
#define PROT_DEINIT										protDeinit();
#define PROT_WRITE(s, a)								protWrite(s,a);
#define PROT_WRITE_EX0(s,f)								protWriteEx((s),(f));
#define PROT_WRITE_EX1(s,f,a1)							protWriteEx((s),(f),(a1));
#define PROT_WRITE_EX2(s,f,a1,a2)						protWriteEx((s),(f),(a1),(a2));
#define PROT_WRITE_EX3(s,f,a1,a2,a3)					protWriteEx((s),(f),(a1),(a2),(a3));
#define PROT_WRITE_EX4(s,f,a1,a2,a3,a4)					protWriteEx((s),(f),(a1),(a2),(a3),(a4));
#define PROT_WRITE_EX5(s,f,a1,a2,a3,a4,a5)				protWriteEx((s),(f),(a1),(a2),(a3),(a4),(a5));
#define PROT_WRITE_EX6(s,f,a1,a2,a3,a4,a5,a6)			protWriteEx((s),(f),(a1),(a2),(a3),(a4),(a5),(a6));
#define PROT_WRITE_EX7(s,f,a1,a2,a3,a4,a5,a6,a7)		protWriteEx((s),(f),(a1),(a2),(a3),(a4),(a5),(a6),(a7));
#define PROT_WRITE_EX8(s,f,a1,a2,a3,a4,a5,a6,a7,a8)		protWriteEx((s),(f),(a1),(a2),(a3),(a4),(a5),(a6),(a7),(a8));
#define PROT_WRITE_EX9(s,f,a1,a2,a3,a4,a5,a6,a7,a8,a9)	protWriteEx((s),(f),(a1),(a2),(a3),(a4),(a5),(a6),(a7),(a8),(a9));


#else
#define PROT_INIT
#define PROT_DEINIT		
#define PROT_WRITE_EX0(s,f)
#define PROT_WRITE_EX1(s,f,a1)
#define PROT_WRITE_EX2(s,f,a1,a2)
#define PROT_WRITE_EX3(s,f,a1,a2,a3)
#define PROT_WRITE_EX4(s,f,a1,a2,a3,a4)
#define PROT_WRITE_EX5(s,f,a1,a2,a3,a4,a5)
#define PROT_WRITE_EX6(s,f,a1,a2,a3,a4,a5,a6)
#define PROT_WRITE_EX7(s,f,a1,a2,a3,a4,a5,a6,a7)
#define PROT_WRITE_EX8(s,f,a1,a2,a3,a4,a5,a6,a7,a8)
#define PROT_WRITE_EX9(s,f,a1,a2,a3,a4,a5,a6,a7,a8,a9)
#endif//PROT_IGNORE

#endif//_HAL_PROTOCOL_H_