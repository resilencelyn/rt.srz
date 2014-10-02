/**  УЭК - Головной автомат подсистемы управления паролями модуля безопасности
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

	// Автомат используется только для предопределённых операций!!!
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

   // Настройка и проверка контекста подсистемы при активации 
    if (E_ACTIVATE == *inout_Event)
    {
        if((RC = opRunInitialize (p_opContext)) != 0)
			return S_IDLE; // Ошибка!!! 
    }

    do
    {
        oldState = STATE;
        
		switch (STATE)
        {
            // Активация автомата
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

			// Начало процедуры активации/деактивации модуля безопасности
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

			// Ввода ПИН активации модуля безопасности
		case S_SE_ACTIVATION_PIN_WAITING:
            if (E_PIN_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_SE_OWNER; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// Получение имени владельца МБ
		case S_SE_OWNER_NAME_REQUESTED:
            if (E_SE_OWNER_NAME_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE =  S_FINISH_SE_ACTIVATION; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// Ввод ПИН владельца МБ
		case S_PIN_WAITING:
            if (E_PIN_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_SE_OWNER;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// Повторный ввод ПИН МБ
		case S_PIN_RETRY:
            if (E_PIN_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_SE_OWNER;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// Ввод текущего значения КРП МБ
		case S_PUK_WAITING:
            if (E_PUK_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_SE_OWNER;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event  || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// Повторный ввод КРП МБ
		case S_PUK_RETRY:
            if (E_PUK_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
                STATE = S_VERIFY_SE_OWNER;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// Верификация владельца модуля безопасности
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
                STATE = S_IDLE;
			break;	

			// Завершение процедуры актиивации/деактивации модуля безопасности
		case S_FINISH_SE_ACTIVATION:
			if (E_EMPTY == *inout_Event)
                STATE = S_IDLE;
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
			if(E_EMPTY == *inout_Event)
				STATE = S_APDU_PACKET_WAITING;
            else if(E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
                justEntry = 0;
			break;

			// Ожидание пакета APDU-комманд на активацию\деактивацию МБ
		case S_APDU_PACKET_WAITING:
			if (E_APDU_PACKET_ENTERED == *inout_Event)
				STATE = S_EXECUTE_APDU_PACKET;
			else if (E_APDU_PACKET_ABSENT == *inout_Event)
				STATE = S_APDU_PACKET_ABSENT;
			else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
			break;

			// Пакет APDU-команд на активацию\деактивацию МБ отсутствует
		case S_APDU_PACKET_ABSENT:
			if (E_EMPTY == *inout_Event)
				STATE = S_IDLE;
			break;		

			// Выполнение пакета APDU-комманд на активацию\деактивацию МБ
		case S_EXECUTE_APDU_PACKET:
			if (E_EMPTY == *inout_Event)
				STATE = S_PROCESS_APDU_PACKET;	
			break;

			// Обработка хостом пакета APDU-комманд после их исполнения
		case S_PROCESS_APDU_PACKET:
			if (E_APDU_PACKET_PROCESSED == *inout_Event)
				STATE = p_opContext->OperationCode == UEC_OP_SE_REM_MANAGE ? S_APDU_PACKET_WAITING : S_IDLE;
			else // ошибка
                justEntry = 0;					
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
			// Начало процедуры активации/деактивации модуля безопасности
		case S_START_SE_ACTIVATION:
#ifdef GUIDE
			opCmnDisplayStringPrefix(p_opContext, "Инициализация процедуры активации/деактивации МБ", ".");
#endif//GUIDE
			{
				IL_BYTE pAC0003[256];
				IL_DWORD dwAC0003;
				IL_WORD mode;

				// стартуем операцию активации/деактивации МБ
				RC = smActivationStart(p_opContext->phCrd->hCrypto, &ifStateActivated, pAC0003, &dwAC0003);
				
				// определим режим активации МБ
				mode = (pAC0003[0]<<8) + pAC0003[1];
				switch(mode)
				{
					case 0x0001: //офлайн активация по ПИН
					case 0x0002: //офлайн активация по АПИН
					case 0x0005: //офлайн активация по КРП
						// установитм в контекст тип активации офлайн
						p_opContext->SE_IfActivateOnline = 0;
						// установим в контекст тип предъявляемого для активации пароля
						p_opContext->PinNum = (IL_BYTE)mode;
						break;
					case 0x0035: //онлайн активация
					case 0x0036: //онлайн активация
						// установим в контекст тип активации онлайн
						p_opContext->SE_IfActivateOnline = 1;
						break;
					default:
						// неизвестный режим активации
						RC = ILRET_SM_SE_ILLEGAL_ACTIVATION_MODE;
				}
			}

			if(!RC)
			{	// проверим допустимость выполнения операции
				if(ifStateActivated && p_opContext->OperationCode == UEC_OP_SE_ACTIVATE)
					RC = ILRET_SM_SE_ALREADY_ACTIVATED;
				if(!ifStateActivated && p_opContext->OperationCode == UEC_OP_SE_DEACTIVATE)
					RC = ILRET_SM_SE_ALREADY_DEACTIVATED;
			}
#ifdef GUIDE
			opCmnDisplayLine(p_opContext, RC ? "ОШИБКА" : "OK");
#endif GUIDE
			if(RC)
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			else 
				*inout_Event = E_EMPTY;
			break;

			// Ожидание ввода ПИН активации модуля безопасности
		case S_SE_ACTIVATION_PIN_WAITING:
			*inout_Event = opRunInterrupt (p_opContext, E_SE_ACTIVATION_PIN_REQUESTED);
			break;

			// Получение имени владельца МБ
		case S_SE_OWNER_NAME_REQUESTED:
			*inout_Event = opRunInterrupt (p_opContext, E_SE_OWNER_NAME_REQUESTED);
			break;

			// Ввод ПИН МБ
		case S_PIN_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_PIN_REQUESTED);
			break;

			// Повторный ввод ПИН 
		case S_PIN_RETRY:
			wEvent = (p_opContext->OperationCode == UEC_OP_SE_ACTIVATE || p_opContext->OperationCode == UEC_OP_SE_DEACTIVATE)
				? E_SE_ACTIVATION_PIN_RETRY_REQUESTED : E_PIN_RETRY_REQUESTED;
            *inout_Event = opRunInterrupt (p_opContext, wEvent);
            break;

			// Ввод КРП МБ
		case S_PUK_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_PUK_REQUESTED);
            break;

			// Повторный ввод КРП МБ
		case S_PUK_RETRY:
            *inout_Event = opRunInterrupt (p_opContext, E_PUK_RETRY_REQUESTED);
            break;

			// Верификация модуля безопасности
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

			// Ожидание ввода нового ПИН МБ
		case S_NEW_PIN_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_NEW_PIN_REQUESTED);
            break;

			// Ожидание ввода нового КРП МБ
		case S_NEW_PUK_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_NEW_PUK_REQUESTED);
            break;

			// Ожидание ввода подтверждающего пароля
		case S_CONFIRM_PIN_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_CONFIRM_PIN_REQUESTED);
            break;

			// Смена ПИН/КРП
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

			// Завершение процедуры активации/деактивации модуля безопасности
		case S_FINISH_SE_ACTIVATION:
#ifdef GUIDE
			opCmnDisplayStringPrefix(p_opContext, "Завершение процедуры активации/деактивации МБ", ".");
#endif//GUIDE
			if(p_opContext->pSeOwnerName)
			{	// копируем во временный буфер контекста 
				cmnMemCopy(p_opContext->TmpBuf, p_opContext->pSeOwnerName, p_opContext->SeOwnerNameLen);
			}
			else
			{	// обнуляем имя владельца МБ
				p_opContext->TmpBuf[0] = 0;
				p_opContext->SeOwnerNameLen = 1;
			}
			
			// завершаем процедуру активации/деактивации
			RC = smOfflineActivationFinish(p_opContext->phCrd->hCrypto, 
				 1, //ifGost
				 p_opContext->OperationCode == UEC_OP_SE_DEACTIVATE, 
				 p_opContext->TmpBuf,
				 p_opContext->SeOwnerNameLen);
#ifdef GUIDE
			opCmnDisplayLine(p_opContext, RC ? "ОШИБКА" : "OK");
#endif GUIDE
			if(RC)
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			else
				*inout_Event = E_EMPTY;
			break;

			// Ожидпние криптограммы для установки защищённого канала с эмитентом
		case S_CRYPTO_ISSUER_SESSION_INIT:
            RC = smGetChallenge(p_opContext->phCrd->hCrypto, 16, p_opContext->SE_SessIn.IcChallenge);
			if(RC)
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			else
				*inout_Event = opRunInterrupt (p_opContext, E_ISSUER_SESSION_REQUESTED);
			break;

			// Аутентификация эмитента 
		case S_AUTH_ISSUER:
			{
            IL_BYTE DataOut[256];
			IL_WORD wOutLen = 0;
#ifdef GUIDE
			opCmnDisplayStringPrefix(p_opContext, "Установка сессии с эмитентом на стороне МБ", ".");
#endif//GUIDE
			RC = smMutualAuth(p_opContext->phCrd->hCrypto, 
							  0x00, 
							  p_opContext->SE_IfGostIssSession ? 0x35: 0x36, 
							  p_opContext->SE_SessOut.CardCryptogramm, 
							  p_opContext->SE_SessOut.CardCryptogrammLength, 
							  DataOut, 
							  &wOutLen);
#ifdef GUIDE
			opCmnDisplayLine(p_opContext, RC ? "ОШИБКА" : "OK");
#endif//GUIDE
            if(RC)
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			else
				*inout_Event = E_EMPTY;
			}
			break;

			// Ожидание пакета APDU-команд на активацию\деактивацию МБ
		case S_APDU_PACKET_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_APDU_PACKET_WAITING);
			break;

			// Выполнение APDU-команд на активацию\деактивацию МБ
		case S_EXECUTE_APDU_PACKET:
			if((RC = opApiRunApduPacketSE(p_opContext)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Обработка хостом пакета APDU-комманд после их исполнения
		case S_PROCESS_APDU_PACKET:
            *inout_Event = opRunInterrupt (p_opContext, E_PROCESS_APDU_PACKET);
			break;

			// Пакет APDU-команд APDU-команд на активацию\деактивацию МБ отсутствует
		case S_APDU_PACKET_ABSENT:
			*inout_Event = E_EMPTY;
			break;	

            // Обработка исключительных ситуаций 
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