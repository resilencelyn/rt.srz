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
#include "opCmnFunc.h"

#define STATE   p_opContext->State[p_opContext->Index]

IL_FUNC IL_WORD opRunEstablishIssuerSessionA1 (s_opContext *p_opContext, IL_WORD *inout_Event)
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
			STATE = S_CRYPTO_ISSUER_SESSION_INIT; 
            break;

			// Ожидпние криптограммы для установки защищённого канала с эмитентом
		case S_CRYPTO_ISSUER_SESSION_INIT:
			if (E_ISSUER_AUTH_CRYPTOGRAMM_READY == *inout_Event)
				STATE = S_AUTH_ISSUER;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// Аутентификация эмитента
		case S_AUTH_ISSUER:
			if (E_EMPTY == *inout_Event)
				STATE = S_CHECK_ISSUER_SESSION;
			break;

			// проверка установленной криптосессии
		case S_CHECK_ISSUER_SESSION:
			if (E_ISSUER_SESSION_CHECKED == *inout_Event)
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
		case S_CRYPTO_ISSUER_SESSION_INIT:
			*inout_Event = opRunInterrupt (p_opContext, E_ISSUER_SESSION_REQUESTED);
			break;

			// Аутентификация эмитента 
		case S_AUTH_ISSUER:
#ifdef GUIDE
			opCmnDisplayStringPrefix(p_opContext, "Установка защищённой сессии с эмитентом ИД-приложения", ".");
#endif//GUIDE
			// создаём защищённую сессию обмена между картой и имитентом ИД-приложения
			if((RC = flIssuerAuth(p_opContext->phCrd, 
									p_opContext->sess_out.CardCryptogramm, 
									p_opContext->sess_out.CardCryptogrammLength, 
									p_opContext->TmpBuf, 
									&p_opContext->wTmp)) == 0)
			{	// подготавливаем в контексте данные для проверки хостом созданной сессии 
#ifdef GUIDE
				opCmnDisplayLine(p_opContext, "OK");
#endif//GUIDE
				cmnMemCopy(p_opContext->chk_sess_in.CardCryptogramm, p_opContext->TmpBuf, 4);
                if(p_opContext->phCrd->ifNeedMSE)
	                cmnMemCopy(p_opContext->chk_sess_in.HostChallenge,  &p_opContext->sess_out.CardCryptogramm[0], 8);
                else				
				    cmnMemCopy(p_opContext->chk_sess_in.HostChallenge, &p_opContext->sess_out.CardCryptogramm[4], 16);
				*inout_Event = E_EMPTY;
			}
			else
			{
#ifdef GUIDE
				opCmnDisplayLine(p_opContext, "ОШИБКА");
#endif//GUIDE
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			}
			break;

		case S_CHECK_ISSUER_SESSION:
            *inout_Event = opRunInterrupt (p_opContext, E_CHECK_ISSUER_SESSION_REQUESTED);
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


