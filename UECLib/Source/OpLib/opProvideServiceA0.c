/**  ��� - �������� ������� ���������� �������� �����
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

IL_FUNC IL_WORD opRunServiceProviderA0 (s_opContext *p_opContext, IL_WORD *inout_Event)
{
    IL_WORD oldState;
    IL_WORD RC;
    IL_BYTE justEntry;

	IL_WORD authResult;
	IL_BYTE CanCaptureCard;

    justEntry = 1; 

	// ������� ������������ ������ ��� ��������������� ��������!!!
	if(!(p_opContext->OperationCode == UEC_OP_PROVIDE_SERVICE))
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

		/////
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
				STATE = S_SERVICE_SELECTING;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

            // �������� ������ ������
        case S_SERVICE_SELECTING:
            if (E_SERVICE_SELECTED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_METAINFO_REQUESTED;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

          // ������ �� ��������� �������������� �� ������ 
        case S_METAINFO_REQUESTED:
            if (E_METAINFO_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_AUTH_OPERATION;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

			// �������������� ��������
		case S_AUTH_OPERATION:
			if (E_EMPTY == *inout_Event)
                STATE = S_CARD_DATA_REQUESTED; 
            break;

			// ��������� ������ ������� �� ������ ��������� ������
		case S_CARD_DATA_REQUESTED:
           if (E_CARD_DATA_DESCR_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_CARD_DATA_READ;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
			break;

			// ������ ����������� ��������� ������
		case S_CARD_DATA_READ:
			if (E_EMPTY == *inout_Event)
                STATE = S_CARD_DATA_PREPARED; 
			break;
	
			// ��������� ������ ������������
		case S_CARD_DATA_PREPARED:
            if (E_SERVICE_REQUEST_DATA_PREPARED == *inout_Event)
				STATE = S_PROVIDER_SESSION_CONFIRMATION;  
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

		//	������������� ������������ ���������� ������ � ����������� ������
		case S_PROVIDER_SESSION_CONFIRMATION:
			if(E_PROVIDER_SESSION_CONFIRMED == *inout_Event)
				STATE = S_PROVIDER_SESSION_ESTABLISH; 
			else if(E_PROVIDER_SESSION_NOT_CONFIRMED == *inout_Event)
				STATE = S_EXTRA_DATA_REQUESTED; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;	

			// ������������ ���������� ������ � ����������� ������
		case S_PROVIDER_SESSION_ESTABLISH:
			if(E_EMPTY == *inout_Event)
				STATE = S_EXTRA_DATA_REQUESTED; 
			break;

			// ��������� �������������� �������� �� ������
		case S_EXTRA_DATA_REQUESTED:
            if (E_EXTRA_DATA_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_HASH_REQUESTED; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

			// ��������� ��� XML-������� �� �������� ������
		case S_HASH_REQUESTED:
            if (E_HASH_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_AUTH_APPLICATION; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

			// �������������� ��-���������� (���������� �������)
		case S_AUTH_APPLICATION:
			if (E_EMPTY == *inout_Event)
				STATE = S_APP_AUTH_REQUEST_PREPARED;  
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// ������ �� �������������� ��-���������� �����������
		case S_APP_AUTH_REQUEST_PREPARED:
			if(E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			else if(*inout_Event == E_CONTINUE || IS_DEFAULT_EVENT)
				STATE = S_DIGITAL_SIGN_CONFIRMATION;			
			break;

			// ������������� ������������ ����������� ������� ��������� �����
		case S_DIGITAL_SIGN_CONFIRMATION:
			if(E_DIGITAL_SIGN_CONFIRMED == *inout_Event)
				STATE = S_DIGITAL_SIGN_PIN_WAITING; 
			else if(E_DIGITAL_SIGN_NOT_CONFIRMED == *inout_Event)
				STATE = p_opContext->isProviderSession ? S_PROVIDER_DATA_ENCRYPT_REQUESTED : S_SEND_APP_AUTH_REQUEST; //S_PROVIDER_SESSION_CONFIRMATION; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// ���� ������ ��������� ����������� �������
		case S_DIGITAL_SIGN_PIN_WAITING:
			if(E_DIGITAL_SIGN_PIN_ENTERED == *inout_Event)
				STATE = S_PREPARE_DIGITAL_SIGN;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// ������������ ����������� ������� ��������� �����
		case S_PREPARE_DIGITAL_SIGN:
			if(E_EMPTY == *inout_Event)
				STATE = p_opContext->isProviderSession ? S_PROVIDER_DATA_ENCRYPT_REQUESTED : S_SEND_APP_AUTH_REQUEST;
			break;

			// ��������� ����� ������ ��� ����������
		case S_PROVIDER_DATA_ENCRYPT_REQUESTED:
			if(E_PROVIDER_DATA_ENCRYPT_ENTERED  == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_PROVIDER_DATA_ENCRYPT;
			else if(E_PROVIDER_DATA_EMPTY == *inout_Event)
				STATE = S_SEND_APP_AUTH_REQUEST; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// ���������� ����� ������
		case S_PROVIDER_DATA_ENCRYPT:
			if(E_EMPTY == *inout_Event)
				STATE = S_PROVIDER_DATA_ENCRYPTED;
			break;

			// ���� ������ ����������
		case S_PROVIDER_DATA_ENCRYPTED:
            if(*inout_Event == E_PROVIDER_DATA_PROCESSED || IS_DEFAULT_EVENT)
				STATE = S_PROVIDER_DATA_ENCRYPT_REQUESTED;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// ����������� � ������� ������� �� �������������� ��-����������
		case S_SEND_APP_AUTH_REQUEST:
			if(E_APP_AUTH_RESPONSE_RECEIVED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_APP_AUTH_RESPONSE_RECEIVED; 
            else if(E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// ������� ����� �� �������������� ��-����������
		case S_APP_AUTH_RESPONSE_RECEIVED:
			if(E_PROVIDER_DATA_DECRYPT_ENTERED == *inout_Event)
				STATE = S_PROVIDER_DATA_DECRYPT;
			else if(E_PROVIDER_DATA_EMPTY == *inout_Event)
				STATE = S_PROVIDER_AUTH_CONFIRMATION; 
			else if(E_PROVIDER_AUTH_CONFIRMED == *inout_Event)
				STATE = S_AUTH_PROVIDER;
			else if(E_PROVIDER_AUTH_NOT_CONFIRMED == *inout_Event)
				STATE = S_APP_AUTH_RESPONSE_DATA_REQUESTED;
            else if(E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// ��������� ����� ������ ��� �������������
		case S_PROVIDER_DATA_DECRYPT_REQUESTED:
			if(E_PROVIDER_DATA_DECRYPT_ENTERED  == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_PROVIDER_DATA_DECRYPT;
			else if(E_PROVIDER_DATA_EMPTY == *inout_Event)
				STATE = S_PROVIDER_AUTH_CONFIRMATION; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// ������������� ����� ������
		case S_PROVIDER_DATA_DECRYPT:
			if(E_EMPTY == *inout_Event)
				STATE = S_PROVIDER_DATA_DECRYPTED;
			else if(E_PROVIDER_DATA_EMPTY == *inout_Event)
				STATE = S_PROVIDER_AUTH_CONFIRMATION; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// ���� ������ �����������
		case S_PROVIDER_DATA_DECRYPTED:
            if(*inout_Event == E_PROVIDER_DATA_PROCESSED || IS_DEFAULT_EVENT)
				STATE = S_PROVIDER_DATA_DECRYPT_REQUESTED;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;	

			// ������������� ������������� �������������� ���������� ������
		case S_PROVIDER_AUTH_CONFIRMATION:
			if(E_PROVIDER_AUTH_CONFIRMED == *inout_Event)
				STATE = S_AUTH_PROVIDER;
			else if(E_PROVIDER_AUTH_NOT_CONFIRMED == *inout_Event)
				STATE = S_APP_AUTH_RESPONSE_DATA_REQUESTED; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// �������������� ���������� ������
		case S_AUTH_PROVIDER:
			if(E_EMPTY == *inout_Event)
				STATE = S_APP_AUTH_RESPONSE_DATA_REQUESTED;
			break;

			// ���� ����������� �������������� ��-����������
		case S_APP_AUTH_RESPONSE_DATA_REQUESTED:
			if(E_PROCESS_APP_AUTH_RESPONSE_DATA == *inout_Event)
				STATE = S_PROCESS_AUTH_APP_RESPONSE_DATA;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// ��������� ����������� �������������� ��-����������
		case S_PROCESS_AUTH_APP_RESPONSE_DATA:
			if(E_EMPTY == *inout_Event) 
				STATE = p_opContext->AuthResult == 400 ? S_APDU_PACKET_WAITING : S_CARD_RELEASING;
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

			// �������� ������ APDU-������ 
		case S_APDU_PACKET_WAITING:
			if (E_APDU_PACKET_ENTERED == *inout_Event)
				STATE = S_EXECUTE_APDU_PACKET;
			else if(E_APDU_ENCRYPTED_PACKET_ENTERED == *inout_Event)
				STATE = S_DECRYPT_APDU_PACKET;
			else if (E_APDU_PACKET_ABSENT == *inout_Event)
				STATE = S_CARD_RELEASING;
			else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
			break;

			// ������������� ������ APDU-������ �� ���������� ����� ���������� ������
		case S_DECRYPT_APDU_PACKET:
			if(E_EXECUTE_APDU_PACKET == *inout_Event)
				STATE = S_EXECUTE_APDU_PACKET;
			break;

			// ���������� APDU-������� �� ������������� � ��������� ���������� �������� ���
		case S_EXECUTE_APDU_PACKET:
			if(E_EMPTY == *inout_Event)
				STATE = p_opContext->isApduEncryptedPS ? S_ENCRYPT_APDU_PACKET : S_PROCESS_APDU_PACKET;	
			break;

			// ���������� ������ ������������ ������ APDU-������ �� ���������� ����� ���������� ������
		case S_ENCRYPT_APDU_PACKET:
			if (E_EMPTY == *inout_Event)
				STATE = S_PROCESS_APDU_PACKET;
			break;

			// ��������� ������ ������ APDU-������� ����� �� ����������
		case S_PROCESS_APDU_PACKET:
			if (E_APDU_PACKET_PROCESSED == *inout_Event)
				STATE = S_APDU_PACKET_WAITING;	
			else if(E_APDU_PACKET_ABSENT == *inout_Event)
				STATE = S_CARD_RELEASING;		
			else // ������
                justEntry = 0;					
			break;

			// ������� �����
		case S_CARD_CAPTURE_REQUESTED:
			if(E_CARD_CAPTURED == *inout_Event  || IS_DEFAULT_EVENT)
			{
				p_opContext->ResultCode = ILRET_OPLIB_CARD_CAPTURED;
				STATE = S_CARD_RELEASING;
			}
			else if(E_CARD_LOCK_REQUESTED == *inout_Event)
				STATE = S_CARD_LOCK;
			break;

		case S_CARD_LOCK:
			// ���������� �����
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

			// ����� ������
		case S_SERVICE_SELECTING:
            *inout_Event = opRunInterrupt (p_opContext, E_SERVICE_SELECTING);
            break;

           // ������ �� ��������� �������������� �� ������ 
        case S_METAINFO_REQUESTED:
            *inout_Event = opRunInterrupt (p_opContext, E_METAINFO_REQUESTED);
            break;

			// �������������� �������� 
		case S_AUTH_OPERATION: 
			p_opContext->PinNum = IL_KEYTYPE_01_PIN;
			opRunAuthOperationA1(p_opContext, inout_Event);
            break;

			// ��������� ������-������� �� ������ ��������� ������ 
		case S_CARD_DATA_REQUESTED:
            *inout_Event = opRunInterrupt (p_opContext, E_CARD_DATA_REQUESTED);
			break;

			// ������ ����������� ��������� ������
		case S_CARD_DATA_READ:
			if((RC = opApiReadCardData(p_opContext, p_opContext->pCardDataDescr, p_opContext->pCardDataBuf, p_opContext->pCardDataLen)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// ������ � ����� ������������
		case S_CARD_DATA_PREPARED:
            *inout_Event = opRunInterrupt (p_opContext, E_CARD_DATA_PREPARED);
            break;

			// ��������� �������������� �������� ��������
		case S_EXTRA_DATA_REQUESTED:
            *inout_Event = opRunInterrupt (p_opContext, E_EXTRA_DATA_REQUESTED);
            break;

			// ��������� ��� XML-������� �� �������� ������
		case S_HASH_REQUESTED:
            *inout_Event = opRunInterrupt(p_opContext, E_HASH_REQUESTED);
            break;

			// ������������� ������������ ���������� ������ � ����������� ������
		case S_PROVIDER_SESSION_CONFIRMATION:
            *inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_SESSION_CONFIRMATION);
			break;

			// ������������ ���������� ������ � ����������� ������
		case S_PROVIDER_SESSION_ESTABLISH:
			if((RC = opApiSetProviderCryptoSession(p_opContext)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// �������������� ��-����������
		case S_AUTH_APPLICATION:
			opRunAuthApplicationA1(p_opContext, inout_Event);
			break;

			// ������ �� �������������� ��-���������� �����������
		case S_APP_AUTH_REQUEST_PREPARED:
            *inout_Event = opRunInterrupt(p_opContext, E_APP_AUTH_REQUEST_PREPARED);
			break;

			// ������������� ������������� ������������ ����������� ������� ��������� �����
		case S_DIGITAL_SIGN_CONFIRMATION:
			if(p_opContext->phCrd->ifSign)
				*inout_Event = opRunInterrupt(p_opContext, E_DIGITAL_SIGN_CONFIRMATION);
			else
				*inout_Event = E_DIGITAL_SIGN_NOT_CONFIRMED;
			break;

			// ���� ������ ��������� ����������� �������
		case S_DIGITAL_SIGN_PIN_WAITING:
			p_opContext->PinNum = IL_KEYTYPE_02_PIN;
            *inout_Event = opRunInterrupt (p_opContext, E_DIGITAL_SIGN_PIN_REQUESTED);
			break;

			// ������������ ����������� ������� ��������� �����
		case S_PREPARE_DIGITAL_SIGN:
			// ������������ ���������� �� ���2
			if((RC = opCmnAppVerifyCitizen(p_opContext)) == 0)
			{	// ��������� ����������� ������� ���������� �����
				RC = opApiMakeDigitalSignature(p_opContext);
			}
			else if(RC == ILRET_CRD_VERIFY_WRONG_PASSWORD_PRESENTED && p_opContext->PinTriesLeft > 0)
			{	// ��������� ����������� ���������� �� ���2
				*inout_Event = E_DIGITAL_SIGN_PIN_RETRY_REQUESTED;
				break;
			}
			if(!RC)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// ��������� ����� ������ ��� ����������
		case S_PROVIDER_DATA_ENCRYPT_REQUESTED:
            *inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_DATA_ENCRYPT_REQUESTED);
			break;

			// ���������� ����� ������
		case S_PROVIDER_DATA_ENCRYPT:
			if((RC = opApiEncryptProviderToTerminal(p_opContext)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// ���� ������ ����������
		case S_PROVIDER_DATA_ENCRYPTED:
            *inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_DATA_ENCRYPTED);
			break;

			// ����������� � ������� ������� �� �������������� ��-����������
		case S_SEND_APP_AUTH_REQUEST:
            *inout_Event = opRunInterrupt(p_opContext, E_SEND_APP_AUTH_REQUESTED);
			break;

			// ������� ����� �� �������������� ��-����������
		case S_APP_AUTH_RESPONSE_RECEIVED:
			if(p_opContext->isProviderSession)
				*inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_DATA_DECRYPT_REQUESTED);
			else
				*inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_AUTH_CONFIRMATION);
			break;

			// ��������� ����� ������ ��� �������������
		case S_PROVIDER_DATA_DECRYPT_REQUESTED:
            *inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_DATA_DECRYPT_REQUESTED);
			break;

			// ������������� ����� ������
		case S_PROVIDER_DATA_DECRYPT:
			if((RC = opApiDecryptProviderToTerminal(p_opContext)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// ���� ������ �����������
		case S_PROVIDER_DATA_DECRYPTED:
            *inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_DATA_DECRYPTED);
			break;

			// ������������� ������������� �������������� ���������� ������
		case S_PROVIDER_AUTH_CONFIRMATION:
            *inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_AUTH_CONFIRMATION);
			break;

			// �������������� ���������� ������
		case S_AUTH_PROVIDER:
			if((RC = opApiAuthServiceProvider(p_opContext)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// ���� ����������� �������������� ��-����������
		case S_APP_AUTH_RESPONSE_DATA_REQUESTED:
			*inout_Event = opRunInterrupt(p_opContext, E_APP_AUTH_RESPONSE_DATA_REQUESTED);
			break;

			// ��������� ����������� �������������� ��-����������
		case S_PROCESS_AUTH_APP_RESPONSE_DATA:
			PROT_WRITE_EX0(PROT_OPLIB1, "opCmnCheckAppAuthResponse()")
			RC = opCmnCheckAppAuthResponse(p_opContext, &authResult);
			PROT_WRITE_EX1(PROT_OPLIB1, "opCmnCheckAppAuthResponse()=%u", RC)
			if(!RC)
			{
				if(authResult == 0)
				{	// �������������� ��������� �������
					*inout_Event = E_EMPTY;
				}
				else if(authResult == 100)
				{	// ������ �������� ������������ ��������������
					*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, ILRET_OPLIB_APP_AUTH_WRONG_CRYPTOGRAM);
				}
				else if(authResult == 200)
				{	// ��������� ������� �����
					*inout_Event = E_CARD_CAPTURE_REQUEST;
				}
				else if(authResult == 300)
				{	// ��������� ������������ ����������� ������ ����� ������ � ���������!!! 
					// ��������� ������ �� �������������� ��-���������� ��� ������������ ���������� ������ � ���������
					if((RC = opApiPrepareAppAuthRequestIssSession(p_opContext)) == 0) 
					{	// ������������� ����������� ����� ��� �������������� ����� ��������������� ������� �� ��������� ���������� ����� � ���������
						p_opContext->AuthRequestCrc = _calculate_crc(p_opContext->pAuthRequestBuf, *p_opContext->pAuthRequestLen);
						// ��������� ��������� ����� ����� � ���������
						PROT_WRITE_EX0(PROT_OPLIB1, "_opCmnGetIcChallenge()");
						RC = _opCmnGetIcChallenge(p_opContext);
						PROT_WRITE_EX1(PROT_OPLIB1, "_opCmnGetIcChallenge()=%u", RC);
						if(!RC)
							*inout_Event = E_ISSUER_SESSION_REQUEST;
					}
					if(RC)
						*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
				}
				else
				{	// �������������� ��������� �������
					*inout_Event = E_EMPTY;
				}
			}
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// ��������� ���������� ������ � ���������
		case S_ESTABLISH_ISSUER_SESSION:
			opRunEstablishIssuerSessionA1(p_opContext, inout_Event);
			break;

			// �������� ������ APDU-������� 
		case S_APDU_PACKET_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_APDU_PACKET_WAITING);
			break;

			// ������������� ������ APDU-������ �� ���������� ����� ���������� ������
		case S_DECRYPT_APDU_PACKET:
			if((RC = opApiDecryptProviderToTerminal(p_opContext)) == 0)
			{	// �������� �������������� ����� �� ������� �����
				cmnMemCopy(p_opContext->pApduIn, p_opContext->pClearData, (IL_WORD)(*p_opContext->pClearDataLen));
				p_opContext->ApduInLen = (IL_WORD)(*p_opContext->pClearDataLen);
				*inout_Event = E_EXECUTE_APDU_PACKET;
			}
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;			

			// ���������� ������ APDU-������� 
		case S_EXECUTE_APDU_PACKET:
			if((RC = opApiRunApduPacket(p_opContext)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// ���������� ������ ������������ ������ APDU-������ �� ���������� ����� ���������� ������
		case S_ENCRYPT_APDU_PACKET:
			p_opContext->pClearData		= p_opContext->pApduOut;
			*p_opContext->pClearDataLen = *p_opContext->ApduOutLen;
			if((RC = opApiEncryptProviderToTerminal(p_opContext)) == 0)
			{
				cmnMemCopy(p_opContext->pApduOut, p_opContext->pEncryptedData, (IL_WORD)(*p_opContext->pEncryptedDataLen)); 
				*p_opContext->ApduOutLen = (IL_WORD)(*p_opContext->pEncryptedDataLen);
				*inout_Event = E_EMPTY;
			}
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
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

			// ���������� ����� �� ������
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