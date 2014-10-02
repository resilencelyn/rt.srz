#ifndef _HAL_RTC_H_
#define _HAL_RTC_H_

#include "il_types.h"

typedef struct 
{
    IL_DWORD LowPart;
    IL_DWORD HighPart;
} IL_START_TIME;

//  Description:
//      �������������� �������� ������� ���� � BCD-������� �� ����� ���������.
//  See Also:
//  Arguments:
//		out_pDate3	- ��������� �� �������� �����, ���������������� ��������� ������� ���� � BCD-������� '������'.
//  Return Value:
//      IL_RETCODE - ��� ������.
//  Summary:
//      ��������� ������� ����.
IL_FUNC IL_RETCODE rtcGetCurrentDate(IL_BYTE *out_pDate3);

//  Description:
//      �������������� ����� ������� ���������� �������� �� ����� ���������.
//  See Also:
//  Arguments:
//		out_pTimeStamp6	- ��������� �� �������� �����, ���������������� ��������� ������� ���� � BCD-������� '������������'.
//  Return Value:
//      IL_RETCODE - ��� ������.
//  Summary:
//      ��������� ������ ������� ���������� ��������.
IL_FUNC IL_RETCODE rtcGetTimeStamp(IL_BYTE *out_pTimeStamp6);

//  Description:
//      ���������� ��� �������������� � BCD-������� ����.
//  See Also:
//  Arguments:
//		in_pDate1 - ��������� �� ������ ������������ ���� � BCD-������� '������'.
//		in_pDate2 - ��������� �� ������ ������������ ���� � BCD-������� '������'.
//  Return Value:
//		��������� ���������:<p/>
//      '0' - Date1 == Date2.<p/>
//		'<0' - Date1 < Date2.<p/>
//		'>0' - Date1 > Date2.
//  Summary:
//      ��������� ���� ���.
IL_FUNC IL_INT rtcCompareDates(IL_BYTE *in_pDate1, IL_BYTE *in_pDate2);

//  Description:
//      �������������� ������ �� ��������� ������� ���� � ������� �� ����� ���������.
//  See Also:
//  Arguments:
//		out_pStr20	- ��������� �� �������� ������, ���������������� ��������� ������� ���� � ������� � ������� '����/��/�� ��:��:��'.
//  Return Value:
//      ��������� �� ���������������� ������.
//  Summary:
//      ��������� ������ � ������� ����� � ��������.
IL_FUNC IL_CHAR *rtcGetCurrentDateTimeStr(IL_CHAR *out_pStr20);


IL_FUNC IL_RETCODE rtcGetCurrentTime(IL_BYTE* time3);
IL_FUNC void rtcStartTimer(IL_START_TIME* pStartTime);
IL_FUNC IL_DWORD rtcStopTimer(IL_START_TIME* pStartTime);
IL_FUNC IL_CHAR *rtcGetTimerIntervalStr(IL_CHAR *out_str, IL_START_TIME *pStartTime);

#endif//_HAL_RTC_H_