/**  УЭК - Головной автомат подсистемы смены ПИН
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

IL_FUNC IL_WORD opRunManagePinPukA0 (s_opContext *p_opContext, IL_WORD *inout_Event)
{
    IL_WORD RC;
	IL_WORD oldState;
    IL_BYTE justEntry;
	IL_WORD authResult;		// результат аутентификации ИД-приложения
	IL_BYTE CanCaptureCard;	// признак наличия оборудования по изъятию карты

    justEntry = 1; 

	// Автомат используется только для предопределённых операций!!!
	if(!(p_opContext->OperationCode == UEC_OP_CHANGE_PIN || 
		 p_opContext->OperationCode == UEC_OP_UNLOCK_PIN ||
		 p_opContext->OperationCode == UEC_OP_CHANGE_PUK ||
		 p_opContext->OperationCode == UEC_OP_UNLOCK_PUK))
	{
		p_opContext->ResultCode = ILRET_OPLIB_INVALID_OPERATION;
		return S_IDLE;
	}

    // Настройка и проверка контекста подсистемы при активации 
    if (E_ACTIVATE == *inout_Event)
    {
        if((RC = opRunInitialize (p_opContext)) != 0)
			return S_IDLE; // Ошибка 
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
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
            break;

 			// Аутентификация операции
		case S_AUTH_OPERATION: 
			if (E_EMPTY == *inout_Event)
			{
				if(p_opContext->OperationCode == UEC_OP_CHANGE_PIN || p_opContext->OperationCode == UEC_OP_UNLOCK_PIN)
					STATE = S_NEW_PIN_WAITING;		 
				else if(p_opContext->OperationCode == UEC_OP_UNLOCK_PUK)
					STATE = S_AUTH_APPLICATION; 
				else
					STATE = S_NEW_PUK_WAITING;
			}
			break;

			// Аутентификация ИД-приложения
		case S_AUTH_APPLICATION:
			if(E_EMPTY == *inout_Event)
				STATE = S_APP_AUTH_REQUEST_PREPARED; 
            else if(E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// Запрос на аутентификацию ИД-приложения подготовлен
		case S_APP_AUTH_REQUEST_PREPARED:
			if(E_PROCESS_APP_AUTH_RESPONSE_DATA == *inout_Event)
				STATE = S_PROCESS_AUTH_APP_RESPONSE_DATA;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// Обработка результатов аутентификации ИД-приложения
		case S_PROCESS_AUTH_APP_RESPONSE_DATA:
			if(E_EMPTY == *inout_Event) 
				STATE = S_CARD_RELEASING; 
			else if(E_ISSUER_SESSION_REQUEST == *inout_Event)
				STATE = S_ESTABLISH_ISSUER_SESSION;
			else if(E_CARD_CAPTURE_REQUEST == *inout_Event)
				STATE = S_CARD_CAPTURE_REQUESTED;
			break;

			// Установка защищённой сессии с эмитентом
		case S_ESTABLISH_ISSUER_SESSION:
			if(E_EMPTY == *inout_Event)
				STATE = S_APDU_PACKET_WAITING; 
			break;

			// Ожидание пакета APDU-комманд на разблокировку и установку временного значения КРП
		case S_APDU_PACKET_WAITING:
			if (E_APDU_PACKET_ENTERED == *inout_Event)
				STATE = S_EXECUTE_APDU_PACKET;
			else if (E_APDU_PACKET_ABSENT == *inout_Event)
				STATE = S_APDU_PACKET_ABSENT;
			else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
			break;

			// Выполнение APDU-комманд на разблокировку и установку временного значения КРП
		case S_EXECUTE_APDU_PACKET:
			if (E_EMPTY == *inout_Event)
				STATE = S_PROCESS_APDU_PACKET;	
			break;

			// Обработка хостом пакета APDU-комманд после их исполнения
		case S_PROCESS_APDU_PACKET:
			if (E_APDU_PACKET_PROCESSED == *inout_Event)
				STATE = S_NEW_PUK_WAITING;
			else // ошибка
                justEntry = 0;					
			break;

			// Пакет APDU-комманд на разблокировку и установку временного значения КРП отсутствует
		case S_APDU_PACKET_ABSENT:
			break;		

			// Ввод нового ПИН
		case S_NEW_PIN_WAITING:
            if (E_NEW_PIN_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_CONFIRM_PIN_WAITING;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
			break;

			// Ввод нового КРП
		case S_NEW_PUK_WAITING:
            if (E_NEW_PUK_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_CONFIRM_PIN_WAITING;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
			break;

			// Ввод подтверждающего ПИН/КРП
		case S_CONFIRM_PIN_WAITING:
            if (E_CONFIRM_PIN_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_CHANGE_PIN;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
			break;

			// Смена ПИН/КРП
		case S_CHANGE_PIN:
			if (E_EMPTY == *inout_Event)
                STATE = S_CARD_RELEASING;
			break;

			// Изъятие карты
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
			// Блокировка карты
			if (E_EMPTY == *inout_Event)
				STATE = S_CARD_RELEASING;
			break;		

			// Извлечение карты
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

			// Аутентификация операции 
		case S_AUTH_OPERATION: 
			{
				IL_BYTE PinNum = p_opContext->PinNum; 
				if(p_opContext->OperationCode != UEC_OP_CHANGE_PIN) 
					opCtxSetPinNum(p_opContext, IL_KEYTYPE_05_PUK); 
				opRunAuthOperationA1(p_opContext, inout_Event);
				if(p_opContext->OperationCode != UEC_OP_CHANGE_PIN) 
					opCtxSetPinNum(p_opContext, PinNum); 
			}
            break;

			// Аутентификация ИД-приложения
		case S_AUTH_APPLICATION:
			opRunAuthApplicationA1(p_opContext, inout_Event);
			break;
			
			// Запрос на аутентификацию ИД-приложения подготовлен
		case S_APP_AUTH_REQUEST_PREPARED:
            *inout_Event = opRunInterrupt(p_opContext, E_APP_AUTH_REQUEST_PREPARED);
			break;

			// Обработка результатов аутентификации ИД-приложения
		case S_PROCESS_AUTH_APP_RESPONSE_DATA:
			PROT_WRITE_EX0(PROT_OPLIB1, "opCmnCheckAppAuthResponse()")
			RC = opCmnCheckAppAuthResponse(p_opContext, &authResult);
			PROT_WRITE_EX1(PROT_OPLIB1, "opCmnCheckAppAuthResponse()=%u", RC)
			if(!RC)
			{
				if(authResult == 100)
				{	// ошибка проверки криптограммы аутентификации
					*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, ILRET_OPLIB_APP_AUTH_WRONG_CRYPTOGRAM);
				}
				else if(authResult == 200)
				{	// требуется изъятие карты
					*inout_Event = E_CARD_CAPTURE_REQUEST;
				}
				else
				{	// аутентификация выполнена успешно
					// сохраняем случайное число карты в контексте 
					PROT_WRITE_EX0(PROT_OPLIB1, "_opCmnGetIcChallenge()")
					RC = _opCmnGetIcChallenge(p_opContext);
					PROT_WRITE_EX1(PROT_OPLIB1, "_opCmnGetIcChallenge()=%u", RC)
					if(!RC)
						*inout_Event = E_ISSUER_SESSION_REQUEST;
					else
						*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
				}
			}
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Установка защищённой сессии с эмитентом
		case S_ESTABLISH_ISSUER_SESSION:
			opRunEstablishIssuerSessionA1(p_opContext, inout_Event);
			break;

			// ожидание пакета APDU-комманд на разблокировку и установку временного значения КРП
		case S_APDU_PACKET_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_APDU_PACKET_WAITING);
			break;

			// выполнение APDU-комманд на разблокировку и установку временного значения КРП
		case S_EXECUTE_APDU_PACKET:
			PROT_WRITE_EX0(PROT_OPLIB1, "opCmnUnlockTmpPUK()")
			RC = opCmnUnlockTmpPUK(p_opContext);
			PROT_WRITE_EX1(PROT_OPLIB1, "opCmnUnlockTmpPUK()=%u", RC)
			if(!RC)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Пакет APDU-комманд на разблокировку и установку временного значения КРП отсутствует
		case S_APDU_PACKET_ABSENT:
			*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, ILRET_OPLIB_APDU_PACKET_ABSENT);
			break;	

			// Обработка хостом пакета APDU-комманд после их исполнения
		case S_PROCESS_APDU_PACKET:
            *inout_Event = opRunInterrupt (p_opContext, E_PROCESS_APDU_PACKET);
			break;

			// Ожидание ввода нового ПИН
		case S_NEW_PIN_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_NEW_PIN_REQUESTED);
            break;

			// Ожидание ввода нового КРП
		case S_NEW_PUK_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_NEW_PUK_REQUESTED);
            break;

			// Ожидание ввода подтверждающего пароля
		case S_CONFIRM_PIN_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_CONFIRM_PIN_REQUESTED);
            break;

			// Смена ПИН/КРП
		case S_CHANGE_PIN:
			//???p_opContext->PinNum = PinNum;
			RC = opCmnChangePinPuk(p_opContext);
			if(RC == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Изъятие карты
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
			// Принудительная блокировка карты
			if((RC = opApiCanCaptureCard(p_opContext, NULL)) == 0)
			{
				p_opContext->ResultCode = ILRET_OPLIB_CARD_LOCKED;
				*inout_Event = E_EMPTY;
			}
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