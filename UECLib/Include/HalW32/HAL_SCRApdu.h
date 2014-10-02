#ifndef _HAL_SCRAPDU_H_
#define _HAL_SCRAPDU_H_

#include "il_types.h"

#define IL_APDU_BUF_SIZE 512 

#define SW(x) (((x)->SW1)<<8) + (x)->SW2

typedef struct IL_APDU
{
    IL_BYTE Cmd[4];
    IL_BYTE DataIn[IL_APDU_BUF_SIZE];
    IL_BYTE DataOut[IL_APDU_BUF_SIZE];
    IL_DWORD LengthIn;
    IL_DWORD LengthExpected;
    IL_DWORD LengthOut;
    IL_BYTE  SW1;
    IL_BYTE  SW2;
}IL_APDU;


#endif//_HAL_SCRAPDU_H_