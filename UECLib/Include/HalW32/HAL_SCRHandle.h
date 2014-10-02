#ifndef _HAL_SCRHANDLE_H_
#define _HAL_SCRHANDLE_H_

#include "il_types.h"
#include <windows.h>
#include <winscard.h>

typedef char* READER_SETTINGS;

typedef struct HANDLE_READER
{
	SCARDCONTEXT    hContext;
	SCARDHANDLE     hCard;

	READER_SETTINGS prdrSettings;
	IL_DWORD        dwShareMode;
	IL_DWORD        dwScope;
	IL_DWORD        dwProtocol;
	IL_DWORD        dwActiveProtocol;
	IL_DWORD        dwReaderState;
} HANDLE_READER;


#endif//_HAL_SCRHANDLE_H_