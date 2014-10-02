/**  ��� - �������� ������� �������� '����� ������������ �����������'
  */
#include "il_types.h"
#include "il_error.h"
#include "FuncLib.h"
#include "opCtxDef.h"
#include "opDescr.h"
#include "opEvent.h"
#include "opState.h"
#include "opRunFunc.h"
#include "opApi.h"

#define STATE   p_opContext->State[p_opContext->Index]

IL_FUNC IL_WORD opRunChangePassPhraseA0 (s_opContext *p_opContext, IL_WORD *inout_Event)
{
    IL_WORD oldState;
    IL_WORD RC;
    IL_BYTE justEntry;

    justEntry = 1; 

	// ������� ������������ ������ ��� ��������������� ��������!!!
	if(p_opContext->OperationCode != UEC_OP_CHANGE_PASS_PHRASE)
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
				STATE = S_CARD_WAITING;
            break;

            // �������� �� ������� 
        case S_CARD_WAITING:
            if (E_CARD_INSERTED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_AUTH_OPERATION;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

 			// �������������� ����� ����������� ��������
		case S_AUTH_OPERATION:
			if (E_EMPTY == *inout_Event)
                STATE = S_PASS_PHRASE_WAITING; //---S_CARD_RELEASING; 
			break;

			//+++ �������� ����� ������������ �����������
		case S_PASS_PHRASE_WAITING:
			if (E_PASS_PHRASE_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_PASS_PHRASE_WRITE; //---STATE = S_CARD_RELEASING; 
			break;
			
			//+++ ������ ������������ ����������� �� �����
		case S_PASS_PHRASE_WRITE:
			if (E_EMPTY == *inout_Event)
				STATE = S_CARD_RELEASING;
			break;

			// ������������ �����
		case S_CARD_RELEASING:
			if(E_CARD_RELEASED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_IDLE;
			break;

			// ��������� �������������� ��������
        case S_EXCEPTION_CATCHING:
			// ������������� ��������� �� ������������ �����!!!
			*inout_Event = E_EMPTY;
			STATE = S_CARD_RELEASING;
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
            // �������� �� ������� 
        case S_CARD_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_CARD_WAITING);
            break;

 			// �������������� ����� ����������� ��������
		case S_AUTH_OPERATION:
			opCtxSetPinNum(p_opContext, IL_KEYTYPE_01_PIN);
			opRunAuthOperationA1(p_opContext, inout_Event);
            break;

			//+++ �������� ����� ������������ �����������
		case S_PASS_PHRASE_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_PASS_PHRASE_REQUESTED);
			break;
			
			//+++ ������ ������������ ����������� �� �����
		case S_PASS_PHRASE_WRITE:
			if(!(RC = opApiWritePassPhrase(p_opContext)))
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// ���������� �����
        case S_CARD_RELEASING:
            *inout_Event = opRunInterrupt (p_opContext, E_CARD_RELEASING);
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