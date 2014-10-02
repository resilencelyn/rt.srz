/**  ��� - �������� ������� ���������� ���������� �������� ������ ������������
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

#ifdef SM_SUPPORT
#include "SmLib.h"
#include "sm_error.h"
#endif//SM_SUPPORT


#define STATE   p_opContext->State[p_opContext->Index]

IL_FUNC IL_WORD opRunManageSeA0 (s_opContext *p_opContext, IL_WORD *inout_Event)
{
#ifdef SM_SUPPORT
    IL_WORD RC;
	IL_WORD oldState;
    IL_BYTE justEntry;
	
	IL_WORD wEvent;
	IL_BYTE KeyType;
	IL_BYTE ifStateActivated;

	justEntry = 1; 

	// ������� ������������ ������ ��� ��������������� ��������!!!
	if(!(p_opContext->OperationCode == UEC_OP_SE_CHANGE_PIN || 
		 p_opContext->OperationCode == UEC_OP_SE_UNLOCK_PIN ||
		 p_opContext->OperationCode == UEC_OP_SE_CHANGE_PUK ||
		 p_opContext->OperationCode == UEC_OP_SE_ACTIVATE ||
		 p_opContext->OperationCode == UEC_OP_SE_DEACTIVATE ||
		 p_opContext->OperationCode == UEC_OP_SE_REM_MANAGE)) 
	{
		p_opContext->ResultCode = ILRET_OPLIB_INVALID_OPERATION;
		return S_IDLE;
	}

   // ��������� � �������� ��������� ���������� ��� ��������� 
    if (E_ACTIVATE == *inout_Event)
    {
        if((RC = opRunInitialize (p_opContext)) != 0)
			return S_IDLE; // ������!!! 
    }

    do
    {
        oldState = STATE;
        
		switch (STATE)
        {
            // ��������� ��������
        case S_IDLE:
            if (E_ACTIVATE == *inout_Event)
			{
				if(p_opContext->OperationCode == UEC_OP_SE_ACTIVATE || p_opContext->OperationCode == UEC_OP_SE_DEACTIVATE)
					STATE = S_START_SE_ACTIVATION;
				else if(p_opContext->OperationCode == UEC_OP_SE_REM_MANAGE)
					STATE = S_CRYPTO_ISSUER_SESSION_INIT;
				else
					STATE = p_opContext->OperationCode == UEC_OP_SE_CHANGE_PIN ? S_PIN_WAITING : S_PUK_WAITING;
			}
            break;

			// ������ ��������� ���������/����������� ������ ������������
		case S_START_SE_ACTIVATION:
			if (E_EMPTY == *inout_Event)
			{
				if(p_opContext->SE_IfActivateOnline)
					STATE = S_CRYPTO_ISSUER_SESSION_INIT; 
				else
					STATE = S_SE_ACTIVATION_PIN_WAITING;
			}
            else if (E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// ����� ��� ��������� ������ ������������
		case S_SE_ACTIVATION_PIN_WAITING:
            if (E_PIN_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_SE_OWNER; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// ��������� ����� ��������� ��
		case S_SE_OWNER_NAME_REQUESTED:
            if (E_SE_OWNER_NAME_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE =  S_FINISH_SE_ACTIVATION; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// ���� ��� ��������� ��
		case S_PIN_WAITING:
            if (E_PIN_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_SE_OWNER;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// ��������� ���� ��� ��
		case S_PIN_RETRY:
            if (E_PIN_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_SE_OWNER;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// ���� �������� �������� ��� ��
		case S_PUK_WAITING:
            if (E_PUK_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_SE_OWNER;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event  || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// ��������� ���� ��� ��
		case S_PUK_RETRY:
            if (E_PUK_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_SE_OWNER;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// ����������� ��������� ������ ������������
		case S_VERIFY_SE_OWNER:
			if (E_EMPTY == *inout_Event)
			{
				if(p_opContext->OperationCode == UEC_OP_SE_CHANGE_PIN)
					STATE = S_NEW_PIN_WAITING;
				else if(p_opContext->OperationCode == UEC_OP_SE_UNLOCK_PIN)
					STATE = S_NEW_PIN_WAITING;
				else if(p_opContext->OperationCode == UEC_OP_SE_CHANGE_PUK)
					STATE = S_NEW_PUK_WAITING;
				else if(p_opContext->OperationCode == UEC_OP_SE_ACTIVATE)
					STATE = S_SE_OWNER_NAME_REQUESTED;
				else
					STATE = S_FINISH_SE_ACTIVATION;
			}
			else if(E_PIN_IS_INCORRECT == *inout_Event)
			{
				if(p_opContext->OperationCode == UEC_OP_SE_UNLOCK_PIN || p_opContext->OperationCode == UEC_OP_SE_CHANGE_PUK)
					STATE = S_PUK_RETRY;
				else
					STATE =  S_PIN_RETRY; 
			}
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// ���� ������ ���
		case S_NEW_PIN_WAITING:
            if (E_NEW_PIN_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_CONFIRM_PIN_WAITING;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
			break;

			// ���� ������ ���
		case S_NEW_PUK_WAITING:
            if (E_NEW_PUK_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_CONFIRM_PIN_WAITING;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
			break;

			// ���� ��������������� ���/���
		case S_CONFIRM_PIN_WAITING:
            if (E_CONFIRM_PIN_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_CHANGE_PIN;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
			break;

			// ����� ���/���
		case S_CHANGE_PIN:
			if (E_EMPTY == *inout_Event)
                STATE = S_IDLE;
			break;	

			// ���������� ��������� ����������/����������� ������ ������������
		case S_FINISH_SE_ACTIVATION:
			if (E_EMPTY == *inout_Event)
                STATE = S_IDLE;
			break;

			// �������� ������������ ��� ��������� ����������� ������ � ���������
		case S_CRYPTO_ISSUER_SESSION_INIT:
			if (E_ISSUER_AUTH_CRYPTOGRAMM_READY == *inout_Event)
				STATE = S_AUTH_ISSUER;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// �������������� �������� 
		case S_AUTH_ISSUER:
			if(E_EMPTY == *inout_Event)
				STATE = S_APDU_PACKET_WAITING;
            else if(E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// �������� ������ APDU-������� �� ���������\����������� ��
		case S_APDU_PACKET_WAITING:
			if (E_APDU_PACKET_ENTERED == *inout_Event)
				STATE = S_EXECUTE_APDU_PACKET;
			else if (E_APDU_PACKET_ABSENT == *inout_Event)
				STATE = S_APDU_PACKET_ABSENT;
			else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
			break;

			// ����� APDU-������ �� ���������\����������� �� �����������
		case S_APDU_PACKET_ABSENT:
			if (E_EMPTY == *inout_Event)
				STATE = S_IDLE;
			break;		

			// ���������� ������ APDU-������� �� ���������\����������� ��
		case S_EXECUTE_APDU_PACKET:
			if (E_EMPTY == *inout_Event)
				STATE = S_PROCESS_APDU_PACKET;	
			break;

			// ��������� ������ ������ APDU-������� ����� �� ����������
		case S_PROCESS_APDU_PACKET:
			if (E_APDU_PACKET_PROCESSED == *inout_Event)
				STATE = p_opContext->OperationCode == UEC_OP_SE_REM_MANAGE ? S_APDU_PACKET_WAITING : S_IDLE;
			else // ������
                justEntry = 0;					
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
			// ������ ��������� ���������/����������� ������ ������������
		case S_START_SE_ACTIVATION:
#ifdef GUIDE
			opCmnDisplayStringPrefix(p_opContext, "������������� ��������� ���������/����������� ��", ".");
#endif//GUIDE
			{
				IL_BYTE pAC0003[256];
				IL_DWORD dwAC0003;
				IL_WORD mode;

				// �������� �������� ���������/����������� ��
				RC = smActivationStart(p_opContext->phCrd->hCrypto, &ifStateActivated, pAC0003, &dwAC0003);
				
				// ��������� ����� ��������� ��
				mode = (pAC0003[0]<<8) + pAC0003[1];
				switch(mode)
				{
					case 0x0001: //������ ��������� �� ���
					case 0x0002: //������ ��������� �� ����
					case 0x0005: //������ ��������� �� ���
						// ���������� � �������� ��� ��������� ������
						p_opContext->SE_IfActivateOnline = 0;
						// ��������� � �������� ��� �������������� ��� ��������� ������
						p_opContext->PinNum = (IL_BYTE)mode;
						break;
					case 0x0035: //������ ���������
					case 0x0036: //������ ���������
						// ��������� � �������� ��� ��������� ������
						p_opContext->SE_IfActivateOnline = 1;
						break;
					default:
						// ����������� ����� ���������
						RC = ILRET_SM_SE_ILLEGAL_ACTIVATION_MODE;
				}
			}

			if(!RC)
			{	// �������� ������������ ���������� ��������
				if(ifStateActivated && p_opContext->OperationCode == UEC_OP_SE_ACTIVATE)
					RC = ILRET_SM_SE_ALREADY_ACTIVATED;
				if(!ifStateActivated && p_opContext->OperationCode == UEC_OP_SE_DEACTIVATE)
					RC = ILRET_SM_SE_ALREADY_DEACTIVATED;
			}
#ifdef GUIDE
			opCmnDisplayLine(p_opContext, RC ? "������" : "OK");
#endif GUIDE
			if(RC)
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			else 
				*inout_Event = E_EMPTY;
			break;

			// �������� ����� ��� ��������� ������ ������������
		case S_SE_ACTIVATION_PIN_WAITING:
			*inout_Event = opRunInterrupt (p_opContext, E_SE_ACTIVATION_PIN_REQUESTED);
			break;

			// ��������� ����� ��������� ��
		case S_SE_OWNER_NAME_REQUESTED:
			*inout_Event = opRunInterrupt (p_opContext, E_SE_OWNER_NAME_REQUESTED);
			break;

			// ���� ��� ��
		case S_PIN_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_PIN_REQUESTED);
			break;

			// ��������� ���� ��� 
		case S_PIN_RETRY:
			wEvent = (p_opContext->OperationCode == UEC_OP_SE_ACTIVATE || p_opContext->OperationCode == UEC_OP_SE_DEACTIVATE)
				? E_SE_ACTIVATION_PIN_RETRY_REQUESTED : E_PIN_RETRY_REQUESTED;
            *inout_Event = opRunInterrupt (p_opContext, wEvent);
            break;

			// ���� ��� ��
		case S_PUK_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_PUK_REQUESTED);
            break;

			// ��������� ���� ��� ��
		case S_PUK_RETRY:
            *inout_Event = opRunInterrupt (p_opContext, E_PUK_RETRY_REQUESTED);
            break;

			// ����������� ������ ������������
		case S_VERIFY_SE_OWNER:
			if(p_opContext->OperationCode == UEC_OP_SE_CHANGE_PIN)
				KeyType = IL_KEYTYPE_01_PIN;
			else if(p_opContext->OperationCode == UEC_OP_SE_UNLOCK_PIN || p_opContext->OperationCode == UEC_OP_SE_CHANGE_PUK)
				KeyType = IL_KEYTYPE_05_PUK;
			else
				KeyType = p_opContext->PinNum; 
			RC = smVerify(p_opContext->phCrd->hCrypto, KeyType, p_opContext->PinBlock, &p_opContext->PinTriesLeft);
			if(!RC)
				*inout_Event = E_EMPTY;
			else if(RC == ILRET_SM_VERIFY_WRONG_PASSWORD_PRESENTED && p_opContext->PinTriesLeft > 0)
				*inout_Event = E_PIN_IS_INCORRECT;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// �������� ����� ������ ��� ��
		case S_NEW_PIN_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_NEW_PIN_REQUESTED);
            break;

			// �������� ����� ������ ��� ��
		case S_NEW_PUK_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_NEW_PUK_REQUESTED);
            break;

			// �������� ����� ��������������� ������
		case S_CONFIRM_PIN_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_CONFIRM_PIN_REQUESTED);
            break;

			// ����� ���/���
		case S_CHANGE_PIN:
			if((p_opContext->pNewPinStr != p_opContext->pConfirmPinStr) && (cmnStrCmp(p_opContext->pNewPinStr, p_opContext->pConfirmPinStr)))
			{
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, ILRET_OPLIB_INVALID_CONFIRM_PIN);
				break; 
			}
			KeyType = p_opContext->OperationCode == UEC_OP_SE_CHANGE_PUK ? IL_KEYTYPE_05_PUK : IL_KEYTYPE_01_PIN;
			str2pin(p_opContext->pNewPinStr, p_opContext->PinBlock); 
			RC = smChangeRefData(p_opContext->phCrd->hCrypto, KeyType, p_opContext->PinBlock, 8);
			if(RC)
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			else
				*inout_Event = E_EMPTY;
			break;

			// ���������� ��������� ���������/����������� ������ ������������
		case S_FINISH_SE_ACTIVATION:
#ifdef GUIDE
			opCmnDisplayStringPrefix(p_opContext, "���������� ��������� ���������/����������� ��", ".");
#endif//GUIDE
			if(p_opContext->pSeOwnerName)
			{	// �������� �� ��������� ����� ��������� 
				cmnMemCopy(p_opContext->TmpBuf, p_opContext->pSeOwnerName, p_opContext->SeOwnerNameLen);
			}
			else
			{	// �������� ��� ��������� ��
				p_opContext->TmpBuf[0] = 0;
				p_opContext->SeOwnerNameLen = 1;
			}
			
			// ��������� ��������� ���������/�����������
			RC = smOfflineActivationFinish(p_opContext->phCrd->hCrypto, 
				 1, //ifGost
				 p_opContext->OperationCode == UEC_OP_SE_DEACTIVATE, 
				 p_opContext->TmpBuf,
				 p_opContext->SeOwnerNameLen);
#ifdef GUIDE
			opCmnDisplayLine(p_opContext, RC ? "������" : "OK");
#endif GUIDE
			if(RC)
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			else
				*inout_Event = E_EMPTY;
			break;

			// �������� ������������ ��� ��������� ����������� ������ � ���������
		case S_CRYPTO_ISSUER_SESSION_INIT:
            RC = smGetChallenge(p_opContext->phCrd->hCrypto, 16, p_opContext->SE_SessIn.IcChallenge);
			if(RC)
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			else
				*inout_Event = opRunInterrupt (p_opContext, E_ISSUER_SESSION_REQUESTED);
			break;

			// �������������� �������� 
		case S_AUTH_ISSUER:
			{
            IL_BYTE DataOut[256];
			IL_WORD wOutLen = 0;
#ifdef GUIDE
			opCmnDisplayStringPrefix(p_opContext, "��������� ������ � ��������� �� ������� ��", ".");
#endif//GUIDE
			RC = smMutualAuth(p_opContext->phCrd->hCrypto, 
							  0x00, 
							  p_opContext->SE_IfGostIssSession ? 0x35: 0x36, 
							  p_opContext->SE_SessOut.CardCryptogramm, 
							  p_opContext->SE_SessOut.CardCryptogrammLength, 
							  DataOut, 
							  &wOutLen);
#ifdef GUIDE
			opCmnDisplayLine(p_opContext, RC ? "������" : "OK");
#endif//GUIDE
            if(RC)
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			else
				*inout_Event = E_EMPTY;
			}
			break;

			// �������� ������ APDU-������ �� ���������\����������� ��
		case S_APDU_PACKET_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_APDU_PACKET_WAITING);
			break;

			// ���������� APDU-������ �� ���������\����������� ��
		case S_EXECUTE_APDU_PACKET:
			if((RC = opApiRunApduPacketSE(p_opContext)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// ��������� ������ ������ APDU-������� ����� �� ����������
		case S_PROCESS_APDU_PACKET:
            *inout_Event = opRunInterrupt (p_opContext, E_PROCESS_APDU_PACKET);
			break;

			// ����� APDU-������ APDU-������ �� ���������\����������� �� �����������
		case S_APDU_PACKET_ABSENT:
			*inout_Event = E_EMPTY;
			break;	

            // ��������� �������������� �������� 
        case S_EXCEPTION_CATCHING:
			STATE = S_IDLE;
            break;
		}

	} while (STATE != S_IDLE);

    if (S_IDLE == STATE)
    {
        *inout_Event = E_ACTIVATE;
    }

    return STATE;
#else
	return S_IDLE;
#endif//SM_SUPPORT
}