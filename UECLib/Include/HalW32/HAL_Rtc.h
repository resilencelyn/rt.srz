#ifndef _HAL_RTC_H_
#define _HAL_RTC_H_

#include "il_types.h"

typedef struct 
{
    IL_DWORD LowPart;
    IL_DWORD HighPart;
} IL_START_TIME;

//  Description:
//      Инициализирует значение текущей даты в BCD-формате по часам терминала.
//  See Also:
//  Arguments:
//		out_pDate3	- Указатель на выходной буфер, инициализируемый значением текущей даты в BCD-формате 'ГГММДД'.
//  Return Value:
//      IL_RETCODE - Код ошибки.
//  Summary:
//      Получение текущей даты.
IL_FUNC IL_RETCODE rtcGetCurrentDate(IL_BYTE *out_pDate3);

//  Description:
//      Инициализирует штамп времени выполнения операции по часам терминала.
//  See Also:
//  Arguments:
//		out_pTimeStamp6	- Указатель на выходной буфер, инициализируемый значением текущей даты в BCD-формате 'ГГММДДЧЧММСС'.
//  Return Value:
//      IL_RETCODE - Код ошибки.
//  Summary:
//      Получение штампа времени выполнения операции.
IL_FUNC IL_RETCODE rtcGetTimeStamp(IL_BYTE *out_pTimeStamp6);

//  Description:
//      Сравнивает две представленных в BCD-формате даты.
//  See Also:
//  Arguments:
//		in_pDate1 - Указатель на первую сравниваемую дату в BCD-формате 'ГГММДД'.
//		in_pDate2 - Указатель на вторую сравниваемую дату в BCD-формате 'ГГММДД'.
//  Return Value:
//		Результат сравнения:<p/>
//      '0' - Date1 == Date2.<p/>
//		'<0' - Date1 < Date2.<p/>
//		'>0' - Date1 > Date2.
//  Summary:
//      Сравнение двух дат.
IL_FUNC IL_INT rtcCompareDates(IL_BYTE *in_pDate1, IL_BYTE *in_pDate2);

//  Description:
//      Инициализирует строку со значением текущей даты и времени по часам терминала.
//  See Also:
//  Arguments:
//		out_pStr20	- Указатель на выходную строку, инициализируемую значением текущей даты и времени в формате 'ГГГГ/ММ/ДД ЧЧ:ММ:СС'.
//  Return Value:
//      Указатель на инициализируемую строку.
//  Summary:
//      Получение строки с текущей датой и временем.
IL_FUNC IL_CHAR *rtcGetCurrentDateTimeStr(IL_CHAR *out_pStr20);


IL_FUNC IL_RETCODE rtcGetCurrentTime(IL_BYTE* time3);
IL_FUNC void rtcStartTimer(IL_START_TIME* pStartTime);
IL_FUNC IL_DWORD rtcStopTimer(IL_START_TIME* pStartTime);
IL_FUNC IL_CHAR *rtcGetTimerIntervalStr(IL_CHAR *out_str, IL_START_TIME *pStartTime);

#endif//_HAL_RTC_H_