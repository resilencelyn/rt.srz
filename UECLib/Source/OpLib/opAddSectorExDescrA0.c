/**  ��� - �������� ������� ���������� �������������� ������ �� �����
  */
#include "il_types.h"
#include "il_error.h"
#include "FuncLib.h"
#include "opCtxDef.h"
#include "opDescr.h"
#include "opEvent.h"
#include "opState.h"
#include "opRunFunc.h"
#include "opCmnFunc.h"
#include "opApi.h"

#define STATE   p_opContext->State[p_opContext->Index]

IL_FUNC IL_WORD opRunAddSectorExDescrA0 (s_opContext *p_opContext, IL_WORD *inout_Event)
{
    IL_WORD oldState;
    IL_WORD RC;
    IL_BYTE justEntry;

    justEntry = 1; 

	// ������� ������������ ������ ��� ��������������� ��������
	if(p_opContext->OperationCode != UEC_OP_ADD_SECTOR_EX_DESCR)
	{
		p_opContext->ResultCode = ILRET_OPLIB_INVALID_OPERATION;
		return S_IDLE;
	}

    // ��������� � �������� ��������� ���������� ��� ��������� 
    if (E_ACTIVATE == *inout_Event)
    {
        RC = opRunInitialize (p_opContext);
        if (RC != 0) return S_IDLE; // ������ 
    }

    do
    {
        oldState = STATE;
        
		switch (STATE)
        {
            // ��������� 
        case S_IDLE:
            if (E_ACTIVATE == *inout_Event)
				STATE = S_SECTOR_EX_DESCR_REQUESTED;
			break;

 			// ��������� �������� ��������� ������������ �������
		case S_SECTOR_EX_DESCR_REQUESTED:
            if (E_SECTOR_EX_DESCR_ENTERED == *inout_Event)
				STATE = S_ADD_SECTOR_EX_DESCR;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// ���������� �������� ��������� �������
		case S_ADD_SECTOR_EX_DESCR:
			if(E_EMPTY == *inout_Event)
				STATE = S_IDLE;
			 break;

			// ��������� �������������� ��������
        case S_EXCEPTION_CATCHING:
			STATE = S_IDLE;
			break;
		}

		///// �������� �������������� ��������
        if (IS_EXCEPTION_EVENT_RANGE (*inout_Event) &&   // ��� ���������� � 
            ((!justEntry) ||                             // ��� ������ �� �� ������� ����� ��� 
             (IS_EMERGENCY_EXCEPTION (*inout_Event))))   // �������� ���������� ��� ����������� �� ���������? 
        {
            STATE = S_EXCEPTION_CATCHING;
        }
        if (opRunTestExitCondition (p_opContext, oldState, *inout_Event, &justEntry))
        {
            break;
        }

        ///// ���������� ���������
        switch (STATE)
        {
			// ��������� �������� ��������� ������������ �������
		case S_SECTOR_EX_DESCR_REQUESTED:
			*inout_Event = opRunInterrupt (p_opContext, E_SECTOR_EX_DESCR_REQUESTED);
			break;

			// ���������� �������� ��������� �������		
		case S_ADD_SECTOR_EX_DESCR:
			//TODO: bTmp - SectorVer!!!!
			if((RC = opApiWriteSectorExDescr(p_opContext->wTmp, p_opContext->bTmp, p_opContext->pExSectorDesr)) == 0)
				*inout_Event = E_EMPTY;
			else
                *inout_Event = opRunThrowException (p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

            // ��������� �������������� �������� 
        case S_EXCEPTION_CATCHING:
            *inout_Event = opRunCatchException (p_opContext, *inout_Event);
            break;
		}
	
	} while (STATE != S_IDLE);

    if (S_IDLE == STATE)
    {
        *inout_Event = E_ACTIVATE;
    }

    return STATE;
}