/**  УЭК - Головной автомат операции 'Смена контрольного приветствия'
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

	// Автомат используется только для предопределённых операций!!!
	if(p_opContext->OperationCode != UEC_OP_CHANGE_PASS_PHRASE)
	{
		p_opContext->ResultCode = ILRET_OPLIB_INVALID_OPERATION;
		return S_IDLE;
	}

    // Настройка и проверка контекста подсистемы при активации 
    if (E_ACTIVATE == *inout_Event)
    {
        RC = opRunInitialize (p_opContext);
        if (RC != 0) return S_IDLE; // Ошибка 
    }

    do
    {
        oldState = STATE;
        
		switch (STATE)
        {
            // Активация 
        case S_IDLE:
            if (E_ACTIVATE == *inout_Event)
				STATE = S_CARD_WAITING;
            break;

            // Ожидание МК клиента 
        case S_CARD_WAITING:
            if (E_CARD_INSERTED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_AUTH_OPERATION;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

 			// Аутентификация перед выполнением операции
		case S_AUTH_OPERATION:
			if (E_EMPTY == *inout_Event)
                STATE = S_PASS_PHRASE_WAITING; //---S_CARD_RELEASING; 
			break;

			//+++ Ожидание ввода контрольного привктствия
		case S_PASS_PHRASE_WAITING:
			if (E_PASS_PHRASE_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_PASS_PHRASE_WRITE; //---STATE = S_CARD_RELEASING; 
			break;
			
			//+++ Запись контрольного приветствия на карту
		case S_PASS_PHRASE_WRITE:
			if (E_EMPTY == *inout_Event)
				STATE = S_CARD_RELEASING;
			break;

			// Освобождение карты
		case S_CARD_RELEASING:
			if(E_CARD_RELEASED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_IDLE;
			break;

			// Обработка исключительной ситуации
        case S_EXCEPTION_CATCHING:
			// принудительно переходим на освобождение карты!!!
			*inout_Event = E_EMPTY;
			STATE = S_CARD_RELEASING;
			break;
		}

        ///// ПЕРЕХВАТ ИСКЛЮЧИТЕЛЬНЫХ СИТУАЦИЙ
        if (IS_EXCEPTION_EVENT_RANGE (*inout_Event) &&   // Это исключение И 
            ((!justEntry) ||                             // оно пришло НЕ из внешней среды ИЛИ 
             (IS_EMERGENCY_EXCEPTION (*inout_Event))))   // является экстренным вне зависимости от источника? 
        {
            STATE = S_EXCEPTION_CATCHING;
        }
        if (opRunTestExitCondition (p_opContext, oldState, *inout_Event, &justEntry))
        {
            break;
        }

        ///// РЕАЛИЗАЦИЯ СОСТОЯНИЙ
        switch (STATE)
        {
            // Ожидание МК Клиента 
        case S_CARD_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_CARD_WAITING);
            break;

 			// Аутентификация перед выполнением операции
		case S_AUTH_OPERATION:
			opCtxSetPinNum(p_opContext, IL_KEYTYPE_01_PIN);
			opRunAuthOperationA1(p_opContext, inout_Event);
            break;

			//+++ Ожидание ввода контрольного приветствия
		case S_PASS_PHRASE_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_PASS_PHRASE_REQUESTED);
			break;
			
			//+++ Запись контрольного приветствия на карту
		case S_PASS_PHRASE_WRITE:
			if(!(RC = opApiWritePassPhrase(p_opContext)))
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Извлечение карты
        case S_CARD_RELEASING:
            *inout_Event = opRunInterrupt (p_opContext, E_CARD_RELEASING);
            break;

            // Обработка исключительных ситуаций 
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