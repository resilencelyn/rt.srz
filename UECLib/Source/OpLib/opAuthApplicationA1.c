/**  УЭК - Вложенный автомат аутентификации ИД-приложения
  */
#include "il_types.h"
#include "il_error.h"
#include "FuncLib.h"
#include "opCtxDef.h"
#include "opDescr.h"
#include "opEvent.h"
#include "opState.h"
#include "opRunFunc.h"
#include "OpApi.h" 

#define STATE   p_opContext->State[p_opContext->Index]

IL_FUNC IL_WORD opRunAuthApplicationA1 (s_opContext *p_opContext, IL_WORD *inout_Event)
{
    IL_WORD oldState;
    IL_WORD RC;
    IL_BYTE justEntry;

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
			STATE = S_AUTH_APP_REQUESTED; 
            break;

			// Формирование запроса на аутентификацию ИД-приложения 
		case S_AUTH_APP_REQUESTED:
			if(E_EMPTY == *inout_Event)
				STATE = S_IDLE; 
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
			// Формирование запроса на аутентификацию ИД-Приложения
		case S_AUTH_APP_REQUESTED:
			// подготовим основной запрс на аутентификацию ИД-приложения для оказания услуги
			PROT_WRITE_EX0(PROT_OPLIB1, "opCmnPrepareAppAuthRequest()")
			RC = opCmnPrepareAppAuthRequest(p_opContext);
			PROT_WRITE_EX1(PROT_OPLIB1, "opCmnPrepareAppAuthRequest()=%u", RC)
			if(!RC)
			{
				if(p_opContext->OperationCode != UEC_OP_PROVIDE_SERVICE
					&& p_opContext->OperationCode != UEC_OP_REM_MANAGE_CARD_DATA) 
				{	// подготовим запрос на на аутентификацию ИД-приложения для установления защищённой сессии с эмитентом
					RC = opApiPrepareAppAuthRequestIssSession(p_opContext);
					// подсчитаем контрольную сумму для подготовленного запроса
					if(!RC)
						p_opContext->AuthRequestCrc = _calculate_crc(p_opContext->pAuthRequestBuf, *p_opContext->pAuthRequestLen);
				}
			}
			if(!RC)
				*inout_Event = E_EMPTY;
			else
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


