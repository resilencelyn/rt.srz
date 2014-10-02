#ifndef _HAL_PARAMETER_H_
#define _HAL_PARAMETER_H_

#include "il_types.h"

#define IL_PARAM_CAID_RSA	        1
#define IL_PARAM_CIFDID_RSA         2
#define IL_PARAM_CAID_PFR_RSA	    3
#define IL_PARAM_CIFDID_PFR_RSA     4
#define IL_PARAM_CAID_FFOMS_RSA	    5
#define IL_PARAM_CIFDID_FFOMS_RSA   6
#define IL_PARAM_CAID_GOST	        7
#define IL_PARAM_CIFDID_GOST        8
#define IL_PARAM_CAID_PFR_GOST	    9
#define IL_PARAM_CIFDID_PFR_GOST   10
#define IL_PARAM_CAID_FFOMS_GOST   11
#define IL_PARAM_CIFDID_FFOMS_GOST 12

#define IL_PARAM_APP_VER_INFO   13
#define IL_PARAM_READERNAME		14
#define IL_PARAM_TERMINAL_INFO  0x9F1C
#define IL_PARAM_PROT_READER	15
#define IL_PARAM_PROT_CARD		16
#define IL_PARAM_PROT_CARDLIB	17
#define IL_PARAM_PROT_FUNCLIB	18

//TODO remove! DEMO mode only!
#define IL_PARAM_SIFDID_MOD     19
#define IL_PARAM_SIFDID_EXP     20
#define IL_PARAM_PICRSA_MOD     21
#define IL_PARAM_PICRSA_EXP     22

#define IL_PARAM_FORMAT_BYTEARRAY	1
#define IL_PARAM_FORMAT_STRING		2

typedef struct 
{
	IL_WORD ID;
	IL_WORD Format;
    IL_WORD Length;
	IL_CHAR* Section;
	IL_CHAR* Name;
}IL_PARAMETER_DESCR;

IL_FUNC IL_RETCODE prmGetParameter(IL_WORD ilParam, IL_BYTE* pParamBuf, IL_DWORD* pdwParamLen);

#endif