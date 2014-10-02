/**  ��� - �������� ������� ���������� '�������� ���������� ��������� �����'
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

IL_FUNC IL_WORD opRunRemManageDataA0 (s_opContext *p_opContext, IL_WORD *inout_Event)
{
    IL_WORD RC;
	IL_WORD oldState;
    IL_BYTE justEntry;
	IL_WORD authResult;		// ��������� �������������� ��-����������
	IL_BYTE CanCaptureCard;	// ������� ������� ������������ �� ������� �����

    justEntry = 1; 

	// ������� ������������ ������ ��� ��������������� ��������!!!
	// ������� ������������ ������ ��� ��������������� ��������!!!
	if(!(p_opContext->OperationCode == UEC_OP_REM_MANAGE_IDAPP_DATA || 
		 p_opContext->OperationCode == UEC_OP_REM_MANAGE_CARD_DATA))
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
				STATE = p_opContext->OperationCode == UEC_OP_REM_MANAGE_IDAPP_DATA ? S_AUTH_OPERATION : S_AUTH_APPLICATION;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

 			// �������������� ����� ����������� ��������
		case S_AUTH_OPERATION:
			if (E_EMPTY == *inout_Event)
                STATE = S_CITIZEN_VERIFIED; 
			break;

			// �������������� ��������� � ����������� ���������� ������ ������� 
		case S_CITIZEN_VERIFIED:
			if(E_CONTINUE == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_AUTH_APPLICATION;
			break;

			// �������������� ��-����������
		case S_AUTH_APPLICATION:
			if(E_EMPTY == *inout_Event)
				STATE = S_APP_AUTH_REQUEST_PREPARED; 
            else if(E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// ������ �� �������������� ��-���������� �����������
		case S_APP_AUTH_REQUEST_PREPARED:
			if(E_PROCESS_APP_AUTH_RESPONSE_DATA == *inout_Event)
				STATE = S_PROCESS_AUTH_APP_RESPONSE_DATA;
			else if(E_CONTINUE == *inout_Event)	
				STATE = S_APDU_PACKET_WAITING;	
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// ��������� ����������� �������������� ��-����������
		case S_PROCESS_AUTH_APP_RESPONSE_DATA:
			if(E_EMPTY == *inout_Event) 
				STATE = S_CARD_RELEASING; 
			else if(E_ISSUER_SESSION_REQUEST == *inout_Event)
				STATE = S_ESTABLISH_ISSUER_SESSION;
			else if(E_CARD_CAPTURE_REQUEST == *inout_Event)
				STATE = S_CARD_CAPTURE_REQUESTED;
			break;

			// ��������� ���������� ������ � ���������
		case S_ESTABLISH_ISSUER_SESSION:
			if(E_EMPTY == *inout_Event)
				STATE = S_APDU_PACKET_WAITING; 
			break;

			// �������� ������ APDU-������� �� ������������� � ��������� ���������� �������� ���
		case S_APDU_PACKET_WAITING:
			if (E_APDU_PACKET_ENTERED == *inout_Event)
				STATE = S_EXECUTE_APDU_PACKET;
			else if (E_APDU_PACKET_ABSENT == *inout_Event)
				STATE = S_APDU_PACKET_ABSENT;
			else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
			break;

			// ���������� APDU-������� ���������� ��������� �����
		case S_EXECUTE_APDU_PACKET:
			if (E_EMPTY == *inout_Event)
				STATE = S_PROCESS_APDU_PACKET;	
			break;

			// ��������� ������ ������ APDU-������� ����� �� ����������
		case S_PROCESS_APDU_PACKET:
			if (E_APDU_PACKET_PROCESSED == *inout_Event)
				STATE = S_APDU_PACKET_WAITING;
			else // ������
                justEntry = 0;					
			break;

			// ����� APDU-������� �����������
		case S_APDU_PACKET_ABSENT:
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
			p_opContext->PinNum = IL_KEYTYPE_01_PIN;
			opRunAuthOperationA1(p_opContext, inout_Event);
            break;

			// �������������� ��������� � ����������� ���������� ������ ������� 
		case S_CITIZEN_VERIFIED:
            *inout_Event = opRunInterrupt (p_opContext, E_CITIZEN_VERIFIED);
			break;

			// �������������� ��-����������
		case S_AUTH_APPLICATION:
			opRunAuthApplicationA1(p_opContext, inout_Event);
			break;

			// ������ �� �������������� ��-���������� �����������
		case S_APP_AUTH_REQUEST_PREPARED:
            *inout_Event = opRunInterrupt(p_opContext, E_APP_AUTH_REQUEST_PREPARED);
			break;

			// ��������� ����������� �������������� ��-����������
		case S_PROCESS_AUTH_APP_RESPONSE_DATA:
			PROT_WRITE_EX0(PROT_OPLIB3, "opCmnCheckAppAuthResponse()")
			RC = opCmnCheckAppAuthResponse(p_opContext, &authResult);
			PROT_WRITE_EX1(PROT_OPLIB3, "opCmnCheckAppAuthResponse()=%u", RC)
			if(!RC)
			{
				if(authResult == 100)
				{	// ������ �������� ������������ ��������������
					*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, ILRET_OPLIB_APP_AUTH_WRONG_CRYPTOGRAM);
				}
				else if(authResult == 200)
				{	// ��������� ������� �����
					*inout_Event = E_CARD_CAPTURE_REQUEST;
				}
				else
				{	// �������������� ��������� �������
					// ��������� ��������� ����� ����� � ��������� 
					if((RC = _opCmnGetIcChallenge(p_opContext)) == 0)
						*inout_Event = E_ISSUER_SESSION_REQUEST;
					else
						*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
				}
			}
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// ��������� ���������� ������ � ���������
		case S_ESTABLISH_ISSUER_SESSION:
			opRunEstablishIssuerSessionA1(p_opContext, inout_Event);
			break;

			// �������� ������ APDU-������� �� ������������� � ��������� ���������� �������� ���
		case S_APDU_PACKET_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_APDU_PACKET_WAITING);
			break;

			// ���������� APDU-������� ���������� ��������� �����
		case S_EXECUTE_APDU_PACKET:
			if((RC = opApiRunApduPacket(p_opContext)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// ����� APDU-������� �����������
		case S_APDU_PACKET_ABSENT:
			*inout_Event = E_EMPTY;
			break;	

			// ��������� ������ ������ APDU-������� ����� �� ����������
		case S_PROCESS_APDU_PACKET:
            *inout_Event = opRunInterrupt (p_opContext, E_PROCESS_APDU_PACKET);
			break;

			// ������� �����
		case S_CARD_CAPTURE_REQUESTED:
			if((RC = opApiCanCaptureCard(p_opContext, &CanCaptureCard)) == 0)
			{
				if(CanCaptureCard)
				{
		            *inout_Event = opRunInterrupt (p_opContext, E_CARD_CAPTURE_REQUESTED);
					break;
				}
				RC = ILRET_OPLIB_CARD_LOCKED;
			}
			*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);			
			break;

		case S_CARD_LOCK:
			// �������������� ���������� �����
			if((RC = opApiCanCaptureCard(p_opContext, NULL)) == 0)
			{
				p_opContext->ResultCode = ILRET_OPLIB_CARD_LOCKED;
				*inout_Event = E_EMPTY;
			}
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