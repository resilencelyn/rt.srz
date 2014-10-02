#ifndef _HAL_CRYPTOHANDLE_H_
#define _HAL_CRYPTOHANDLE_H_

#include "il_types.h"

#ifdef SM_SUPPORT
    #include "SmLib.h"
#endif

// ��������� ����������� ������ ���������
//#ifdef SM_SUPPORT
//    typedef struct 
//    {
//	    IL_BYTE SessionSmCounter[8];	// ������� ��������� ����������� ������
//    }SM_CONTEXT;
//#else
    typedef struct 
    {
	    IL_BYTE SessionSmCounter[8];	// ������� ��������� ����������� ������
	    IL_BYTE SKsmciddes[16];			// ���������� ���� ����������
	    IL_BYTE SKsmiiddes[16];			// ���������� ���� ��������������
	    IL_BYTE SKsmcidgost[32];
	    IL_BYTE SKsmiidgost[32];
    }SM_CONTEXT;
//#endif

// ��������� ������������ 
#ifdef SM_SUPPORT
    typedef struct 
    {
	    SM_CONTEXT SM;					// ��������� ����������� ������ ��������� 
	    IL_SM_HANDLE hSm;
		IL_BYTE SK_sm_id_smc_des[16];
    }HANDLE_CRYPTO;
#else
    typedef struct 
    {
	    SM_CONTEXT SM;					// ��������� ����������� ������ ��������� 
	    IL_BYTE Kifd[16];				// ���������� ������ ������� ���������
	    IL_BYTE tmpSifdgost[32];
	    IL_BYTE tmpPifdgost[64];
    }HANDLE_CRYPTO;
#endif

#endif//_HAL_CRYPTOHANDLE_H_