#include <windows.h>
#include <time.h>
#include <stdio.h>

#include "HAL_Common.h"
#include "HAL_Rtc.h"

IL_FUNC IL_RETCODE rtcGetCurrentDate(IL_BYTE* date3)
{
    struct tm *cur_time;
    time_t long_time;

    time( &long_time );               
    cur_time = localtime( &long_time );

    date3[0] = toBcd((IL_BYTE)(cur_time->tm_year - 100));
    date3[1] = toBcd((IL_BYTE)cur_time->tm_mon+1);
    date3[2] = toBcd((IL_BYTE)cur_time->tm_mday);

    return 0;
}

IL_FUNC IL_RETCODE rtcGetCurrentTime(IL_BYTE* time3)
{
    struct tm *cur_time;
    time_t long_time;

    time( &long_time );               
    cur_time = localtime( &long_time );

    time3[0] = toBcd((IL_BYTE)(cur_time->tm_hour));
    time3[1] = toBcd((IL_BYTE)cur_time->tm_min);
    time3[2] = toBcd((IL_BYTE)cur_time->tm_sec);

    return 0;
}

IL_FUNC IL_RETCODE rtcGetTimeStamp(IL_BYTE* timestamp6)
{
    struct tm *cur_time;
    time_t long_time;

    time( &long_time );               
    cur_time = localtime( &long_time );

    timestamp6[0] = toBcd((IL_BYTE)(cur_time->tm_year - 100));
    timestamp6[1] = toBcd((IL_BYTE)cur_time->tm_mon+1);
    timestamp6[2] = toBcd((IL_BYTE)cur_time->tm_mday);
    timestamp6[3] = toBcd((IL_BYTE)(cur_time->tm_hour));
    timestamp6[4] = toBcd((IL_BYTE)cur_time->tm_min);
    timestamp6[5] = toBcd((IL_BYTE)cur_time->tm_sec);

    return 0;
}

IL_FUNC IL_INT rtcCompareDates(IL_BYTE* date1, IL_BYTE* date2)
{
    return memcmp(date1, date2, 3);
}

IL_FUNC void rtcStartTimer(IL_START_TIME* pStartTime) 
{
    QueryPerformanceCounter((LARGE_INTEGER*) pStartTime); 
}

IL_FUNC IL_DWORD rtcStopTimer(IL_START_TIME* pStartTime)
{
	LARGE_INTEGER ilEndTime, liFreq;

	QueryPerformanceCounter(&ilEndTime);

	ilEndTime.QuadPart -= ((LARGE_INTEGER*)pStartTime)->QuadPart;
	ilEndTime.QuadPart *= 1000;

	(void)QueryPerformanceFrequency(&liFreq);
	
	return ((IL_DWORD)(ilEndTime.QuadPart / liFreq.QuadPart));
}

IL_FUNC IL_CHAR *rtcGetCurrentDateTimeStr(IL_CHAR *out_str20)
{
	if(out_str20)
	{
		IL_BYTE timestamp[6];
		
		rtcGetTimeStamp(timestamp);
		sprintf(out_str20, "20%02X/%02X/%02X %02X:%02X:%02X", 
				timestamp[0], timestamp[1], timestamp[2], 
				timestamp[3], timestamp[4], timestamp[5]);

		return out_str20;
	}

	return NULL;
}

//+++
IL_FUNC IL_CHAR *rtcGetTimerIntervalStr(IL_CHAR *out_str, IL_START_TIME *pStartTime)
{
	IL_DWORD dwInterval;

	if(!pStartTime || !out_str)
		return "";

	dwInterval = rtcStopTimer(pStartTime);
	sprintf(out_str, "Интервал: %lu", dwInterval);

	return out_str;
}
