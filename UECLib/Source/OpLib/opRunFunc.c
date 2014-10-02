/**  Общие функции системы автоматов
  */
#include "il_types.h"
#include "opCtxDef.h"
#include "opEvent.h"
#include "HAL_Common.h"

IL_FUNC IL_WORD opRunInitialize (s_opContext *p_opContext)
{	

	p_opContext->ifAuthOnline = 1; // по умолчанию аутентификация ИД-приложения ONLINE!!!
	return 0;
}

IL_FUNC IL_WORD opRunInterrupt(s_opContext *p_opContext, IL_WORD in_InterruptEvent)
{
    // Система предотвращения зацикливания
    p_opContext->wCntCycles = (p_opContext->InterruptEvent == in_InterruptEvent)
		? p_opContext->wCntCycles+1 : 0;

    if (p_opContext->wCntCycles > 10) return E_EXCEPTION_CYCLING_DETECTED;

    // Регистрация события-прерывания
    p_opContext->InterruptEvent = in_InterruptEvent;
    
    return p_opContext->InterruptEvent;
}

IL_FUNC IL_BYTE opRunTestExitCondition (s_opContext *p_opContext, IL_WORD in_OldState, IL_WORD in_Event, IL_BYTE *inout_JustEntry)
{
    IL_BYTE    RC;
    
    in_Event = in_Event; 

    if (in_OldState == p_opContext->State[p_opContext->Index] && !(*inout_JustEntry))
    {
        RC = 1;
    }
    else
    {
        // Если "только что вошли" в автомат с тем же самым событием, с которым и 
        // вышли и при этом не изменилось состояние в первом Switch, необходимо 
        // пропустить на обработку события "по умолчанию" во второй Switch. 
        RC = 0;
        *inout_JustEntry = 0;
    }

	return RC;
}


IL_FUNC IL_WORD opRunCatchException(s_opContext *p_opContext, IL_WORD in_ExceptionEvent)
{

    if (IS_EXCEPTION_EVENT_RANGE(in_ExceptionEvent))
    {
        in_ExceptionEvent += E_CATCHING_OFFSET_EVENTS;
    
        // Обнуление состояний вышестоящих автоматов 
        if (p_opContext->Index < UEC_MAX_STATES-1)
        {
            cmnMemSet ((IL_BYTE*)&p_opContext->State[p_opContext->Index+1], 
                        0, 
                        (IL_WORD)(sizeof(p_opContext->State[p_opContext->Index])*(UEC_MAX_STATES-p_opContext->Index-1)));
        }
    }

    return in_ExceptionEvent;
}

IL_FUNC IL_WORD opRunThrowException(s_opContext *p_opContext, IL_WORD in_ExceptionEvent, IL_WORD in_wResultCode)
{
    if (in_ExceptionEvent == E_EXCEPTION_RUNTIME_ERROR)
    {
        p_opContext->ResultCode = in_wResultCode;
    }

    return in_ExceptionEvent;
}

IL_FUNC IL_WORD opRunReturnEvent (IL_WORD in_Event)
{
    IL_WORD ret_Event;

    ret_Event = (in_Event ^ RETURN_MARK);

    return ret_Event;
}

IL_FUNC IL_BYTE opRunIncreaseIndex (s_opContext *p_opContext)
{
    if (p_opContext->Index + 1 >= UEC_MAX_STATES) return 0;

    p_opContext->Index++;

    return 1;
}

IL_FUNC void opRunDecreaseIndex (s_opContext *p_opContext)
{
    if (p_opContext->Index > 0) p_opContext->Index--;

    return;
}

IL_FUNC IL_WORD opRunPassThrowCatchedException (IL_WORD in_CatchedExceptionEvent)
{
    if (IS_EXCEPTION_EVENT_RANGE(IS_CATCHED_EXCEPTION(in_CatchedExceptionEvent)))
    {
        in_CatchedExceptionEvent -= E_CATCHING_OFFSET_EVENTS;
    }

    return in_CatchedExceptionEvent;
}


