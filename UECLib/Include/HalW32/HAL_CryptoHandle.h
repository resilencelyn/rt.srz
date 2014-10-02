#ifndef _HAL_CRYPTOHANDLE_H_
#define _HAL_CRYPTOHANDLE_H_

#include "il_types.h"

#ifdef SM_SUPPORT
    #include "SmLib.h"
#endif

// описатель защищённого канала терминала
//#ifdef SM_SUPPORT
//    typedef struct 
//    {
//	    IL_BYTE SessionSmCounter[8];	// счётчик сообщений защищённого обмена
//    }SM_CONTEXT;
//#else
    typedef struct 
    {
	    IL_BYTE SessionSmCounter[8];	// счётчик сообщений защищённого обмена
	    IL_BYTE SKsmciddes[16];			// сессионный ключ шифрования
	    IL_BYTE SKsmiiddes[16];			// сессионный ключ аутентификации
	    IL_BYTE SKsmcidgost[32];
	    IL_BYTE SKsmiidgost[32];
    }SM_CONTEXT;
//#endif

// описатель криптосессии 
#ifdef SM_SUPPORT
    typedef struct 
    {
	    SM_CONTEXT SM;					// описатель защищённого канала терминала 
	    IL_SM_HANDLE hSm;
		IL_BYTE SK_sm_id_smc_des[16];
    }HANDLE_CRYPTO;
#else
    typedef struct 
    {
	    SM_CONTEXT SM;					// описатель защищённого канала терминала 
	    IL_BYTE Kifd[16];				// компонента общего секрета терминала
	    IL_BYTE tmpSifdgost[32];
	    IL_BYTE tmpPifdgost[64];
    }HANDLE_CRYPTO;
#endif

#endif//_HAL_CRYPTOHANDLE_H_