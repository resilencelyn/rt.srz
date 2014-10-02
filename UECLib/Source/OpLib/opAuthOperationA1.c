/**  УЭК - Вложенный автомат авторизации перед выполнением операции
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

IL_FUNC IL_WORD opRunAuthOperationA1 (s_opContext *p_opContext, IL_WORD *inout_Event)
{
    IL_WORD oldState;
    IL_WORD RC;
    IL_BYTE justEntry;

    IL_BYTE resp[UEC_CARD_RESP_MAX_LEN] = {0};
    IL_WORD resp_len = 0;

	justEntry = 1; 

    if (!opRunIncreaseIndex(p_opContext))
    {
        opRunInterrupt(p_opContext, E_EXCEPTION_DEPTH);
        return STATE;
    }

    do
    {
        oldState = STATE;

        switch (STATE)
        {
            // Активация 
        case S_IDLE:
			if(p_opContext->OperationCode == UEC_OP_EDIT_PRIVATE_DATA || p_opContext->OperationCode == UEC_OP_EDIT_OPERATOR_DATA)
				STATE = S_AUTH_TERMINAL;	// приложение уже селектировано!!!
			else
				STATE = S_APP_SELECTING; 
            break;

           // Выбор приложения 
		case S_APP_SELECTING:
			if (E_EMPTY == *inout_Event)
                STATE = S_AUTH_TERMINAL;
			break;

			// Аутентификация терминала
		case S_AUTH_TERMINAL:
			if (E_EMPTY == *inout_Event)
			{
				if(p_opContext->OperationCode == UEC_OP_UNLOCK_PUK)
					STATE = S_IDLE; // Верификации гражданина не требуется!!!
				else if(p_opContext->OperationCode == UEC_OP_EDIT_OPERATOR_DATA)
				{	
					STATE = S_PIN_WAITING; 
				}
				else if(p_opContext->OperationCode == UEC_OP_UNLOCK_PIN || p_opContext->OperationCode == UEC_OP_CHANGE_PUK)
					STATE = S_PUK_WAITING;
				else
					STATE = S_PIN_WAITING;
			}
			break;

           // Ввод ПИН 
		case S_PIN_WAITING:
            if (E_PIN_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_CITIZEN;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;
	
			// Повторный ввод ПИН
		case S_PIN_RETRY:
            if (E_PIN_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_CITIZEN;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

           // Ввод КРП 
		case S_PUK_WAITING:
            if (E_PUK_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_CITIZEN;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event  || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;
	
			// Повторный ввод КРП
		case S_PUK_RETRY:
            if (E_PUK_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_CITIZEN;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;
		
			// Верификация гражданина
		case S_VERIFY_CITIZEN:
			if (E_EMPTY == *inout_Event)
                STATE = S_IDLE;
			else if(E_PIN_IS_INCORRECT == *inout_Event && 
						(p_opContext->OperationCode == UEC_OP_UNLOCK_PIN
						|| p_opContext->OperationCode == UEC_OP_CHANGE_PUK))
                 STATE = S_PUK_RETRY;
			else if(E_PIN_IS_INCORRECT == *inout_Event)
               STATE = S_PIN_RETRY;
			break;
		}

       if (IS_EXCEPTION_EVENT_RANGE (*inout_Event) &&
            (!justEntry || IS_EMERGENCY_EXCEPTION (*inout_Event)))
        {
            ;//STATE = S_IDLE; /* Обработчика исключительной ситуации нет */
        }

        if (opRunTestExitCondition(p_opContext, oldState, *inout_Event, &justEntry))
            break;

        ///// РЕАЛИЗАЦИЯ СОСТОЯНИЙ
        switch (STATE)
        {
			// Выбор приложения
		case S_APP_SELECTING:
			// селектируем приложение
			PROT_WRITE_EX0(PROT_OPLIB1, "flAppSelect()") 
			RC = flAppSelect(p_opContext->phCrd, resp, &resp_len);
			PROT_WRITE_EX2(PROT_OPLIB2, "OUT: Resp[%u]=%s", resp_len, bin2hex(p_opContext->TmpBuf, resp, resp_len))
			PROT_WRITE_EX1(PROT_OPLIB1, "flAppSelect()=%u", RC) 
			if(!RC)
			{	// инициализируем системные данные ИД-приложения
				PROT_WRITE_EX0(PROT_OPLIB1, "_opCmnGetAppSystemInfo()") 
				RC = _opCmnGetAppSystemInfo(p_opContext, resp, resp_len);
				PROT_WRITE_EX1(PROT_OPLIB1, "_opCmnGetAppSystemInfo()=%u", RC) 
			}
			if (RC == 0)
				*inout_Event = E_EMPTY;
            else
                *inout_Event = opRunThrowException (p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Аутентификация терминала
		case S_AUTH_TERMINAL:
			if(p_opContext->SectorIdAuth == 0)
				p_opContext->SectorIdAuth = 1;
			OP_CMN_DISPLAY_STRING(p_opContext, "Используется криптоалгоритм ")
			OP_CMN_DISPLAY_LINE(p_opContext, (p_opContext->phCrd->ifGostCrypto ? "ГОСТ" : "RSA"))
			OP_CMN_DISPLAY_STRING(p_opContext, "Аутентификация терминала... ")
			PROT_WRITE_EX0(PROT_OPLIB1, "flAppTerminalAuth()") 
			RC = flAppTerminalAuth(p_opContext->phCrd);
			PROT_WRITE_EX1(PROT_OPLIB1, "flAppTerminalAuth()=%u", RC) 
			OP_CMN_DISPLAY_LINE(p_opContext, (!RC) ? "ОК" : "ОШИБКА")
			if(RC == 0)
			{
				PROT_WRITE_EX0(PROT_OPLIB1, "opCmnGetPAN()") 
				RC = opCmnGetPAN(p_opContext, p_opContext->PAN);
				if(!RC)
					PROT_WRITE_EX1(PROT_OPLIB1, "OUT: PAN=%s", p_opContext->PAN);
				PROT_WRITE_EX1(PROT_OPLIB1, "opCmnGetPAN()=%u", RC) 
				if(!RC)
				{
					PROT_WRITE_EX0(PROT_OPLIB1, "flGetPassPhrase()") 
					RC = flGetPassPhrase(p_opContext->phCrd, p_opContext->PassPhrase); 
					if(!RC)
						PROT_WRITE_EX1(PROT_OPLIB2, "OUT: PassPhrase=%s", p_opContext->PassPhrase); 
					PROT_WRITE_EX1(PROT_OPLIB1, "flGetPassPhrase()=%u", RC);
				}
				if(!RC)
					*inout_Event = E_EMPTY;
			}
			if(RC)
                *inout_Event = opRunThrowException (p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Ввод ПИН 
		case S_PIN_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_PIN_REQUESTED);
            break;

			// Повторный ввод ПИН
		case S_PIN_RETRY:
            *inout_Event = opRunInterrupt (p_opContext, E_PIN_RETRY_REQUESTED);
            break;

			// Ввод КРП 
		case S_PUK_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_PUK_REQUESTED);
            break;

			// Повторный ввод КРП
		case S_PUK_RETRY:
            *inout_Event = opRunInterrupt (p_opContext, E_PUK_RETRY_REQUESTED);
            break;

			// Верификация гражданина 
		case S_VERIFY_CITIZEN:
			PROT_WRITE_EX0(PROT_OPLIB1, "opCmnAppVerifyCitizen()")
			RC = opCmnAppVerifyCitizen(p_opContext);
			PROT_WRITE_EX1(PROT_OPLIB1, "opCmnAppVerifyCitizen()=%u", RC)
			if(RC == 0)
			{	
				*inout_Event = E_EMPTY;
				break;
			}
			else if(RC == ILRET_CRD_VERIFY_WRONG_PASSWORD_PRESENTED && p_opContext->PinTriesLeft > 0)
			{
				RC = 0;
				if(p_opContext->phCrd->AppVer == UECLIB_APP_VER_10)
				{	// ЗАПЛАТКА!!! ПОСКОЛЬКУ ПРИ ЛЮБОЙ ОШИБКЕ НА КАРТЕ ВЕРСИИ 1.0 НЕВЕРНО СБРАСЫВАЕТСЯ КРИПТОСЕССИЯ
					// перед повторным предъявлением ПИН необходимо заново селектировать приложение и аутентифицировать терминал
					PROT_WRITE_EX0(PROT_OPLIB1, "opCmnRestoreCryptoSession()")
					RC = opCmnRestoreCryptoSession(p_opContext);
					PROT_WRITE_EX1(PROT_OPLIB1, "opCmnRestoreCryptoSession()=%u", RC)
				}
				if(!RC)
				{
					*inout_Event = E_PIN_IS_INCORRECT;
					break;
				}
			}
			*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;
		}
	
	} while (STATE != S_IDLE);

    if (S_IDLE == STATE)
    {
        *inout_Event = E_EMPTY;
    }

    opRunDecreaseIndex(p_opContext);

    return STATE;
}


