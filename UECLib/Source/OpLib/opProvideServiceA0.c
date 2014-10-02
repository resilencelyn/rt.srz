/**  УЭК - Головной автомат подсистемы оказания услуг
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

	// Автомат используется только для предопределённых операций!!!
	if(!(p_opContext->OperationCode == UEC_OP_PROVIDE_SERVICE))
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

		/////
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
				STATE = S_SERVICE_SELECTING;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

            // Ожидание выбора услуги
        case S_SERVICE_SELECTING:
            if (E_SERVICE_SELECTED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_METAINFO_REQUESTED;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

          // Запрос на получение метаинформации по услуге 
        case S_METAINFO_REQUESTED:
            if (E_METAINFO_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_AUTH_OPERATION;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

			// Аутентификация операции
		case S_AUTH_OPERATION:
			if (E_EMPTY == *inout_Event)
                STATE = S_CARD_DATA_REQUESTED; 
            break;

			// Получение строки запроса на чтение карточных данных
		case S_CARD_DATA_REQUESTED:
           if (E_CARD_DATA_DESCR_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_CARD_DATA_READ;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
			break;

			// Чтение запрошенных карточных данных
		case S_CARD_DATA_READ:
			if (E_EMPTY == *inout_Event)
                STATE = S_CARD_DATA_PREPARED; 
			break;
	
			// Карточные данные подготовлены
		case S_CARD_DATA_PREPARED:
            if (E_SERVICE_REQUEST_DATA_PREPARED == *inout_Event)
				STATE = S_PROVIDER_SESSION_CONFIRMATION;  
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

		//	Подтверждение установления защищённой сессии с Поставщиком Услуги
		case S_PROVIDER_SESSION_CONFIRMATION:
			if(E_PROVIDER_SESSION_CONFIRMED == *inout_Event)
				STATE = S_PROVIDER_SESSION_ESTABLISH; 
			else if(E_PROVIDER_SESSION_NOT_CONFIRMED == *inout_Event)
				STATE = S_EXTRA_DATA_REQUESTED; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;	

			// Установление защищённой сессии с Поставщиком Услуги
		case S_PROVIDER_SESSION_ESTABLISH:
			if(E_EMPTY == *inout_Event)
				STATE = S_EXTRA_DATA_REQUESTED; 
			break;

			// Получение дополнительных сведений об услуге
		case S_EXTRA_DATA_REQUESTED:
            if (E_EXTRA_DATA_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_HASH_REQUESTED; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

			// Получение хэш XML-запроса на оказание услуги
		case S_HASH_REQUESTED:
            if (E_HASH_ENTERED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_AUTH_APPLICATION; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

			// Аутентификация ИД-приложения (подготовка запроса)
		case S_AUTH_APPLICATION:
			if (E_EMPTY == *inout_Event)
				STATE = S_APP_AUTH_REQUEST_PREPARED;  
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// Запрос на аутентификацию ИД-приложения подготовлен
		case S_APP_AUTH_REQUEST_PREPARED:
			if(E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			else if(*inout_Event == E_CONTINUE || IS_DEFAULT_EVENT)
				STATE = S_DIGITAL_SIGN_CONFIRMATION;			
			break;

			// Подтверждение формирования электронной подписи держателя карты
		case S_DIGITAL_SIGN_CONFIRMATION:
			if(E_DIGITAL_SIGN_CONFIRMED == *inout_Event)
				STATE = S_DIGITAL_SIGN_PIN_WAITING; 
			else if(E_DIGITAL_SIGN_NOT_CONFIRMED == *inout_Event)
				STATE = p_opContext->isProviderSession ? S_PROVIDER_DATA_ENCRYPT_REQUESTED : S_SEND_APP_AUTH_REQUEST; //S_PROVIDER_SESSION_CONFIRMATION; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// Ввод пароля держателя электронной подписи
		case S_DIGITAL_SIGN_PIN_WAITING:
			if(E_DIGITAL_SIGN_PIN_ENTERED == *inout_Event)
				STATE = S_PREPARE_DIGITAL_SIGN;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// Формирование электронной подписи держателя карты
		case S_PREPARE_DIGITAL_SIGN:
			if(E_EMPTY == *inout_Event)
				STATE = p_opContext->isProviderSession ? S_PROVIDER_DATA_ENCRYPT_REQUESTED : S_SEND_APP_AUTH_REQUEST;
			break;

			// Получение блока данных для шифрования
		case S_PROVIDER_DATA_ENCRYPT_REQUESTED:
			if(E_PROVIDER_DATA_ENCRYPT_ENTERED  == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_PROVIDER_DATA_ENCRYPT;
			else if(E_PROVIDER_DATA_EMPTY == *inout_Event)
				STATE = S_SEND_APP_AUTH_REQUEST; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// Шифрование блока данных
		case S_PROVIDER_DATA_ENCRYPT:
			if(E_EMPTY == *inout_Event)
				STATE = S_PROVIDER_DATA_ENCRYPTED;
			break;

			// Блок данных зашифрован
		case S_PROVIDER_DATA_ENCRYPTED:
            if(*inout_Event == E_PROVIDER_DATA_PROCESSED || IS_DEFAULT_EVENT)
				STATE = S_PROVIDER_DATA_ENCRYPT_REQUESTED;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// Уведомление о посылке запроса на аутентификацию ИД-приложения
		case S_SEND_APP_AUTH_REQUEST:
			if(E_APP_AUTH_RESPONSE_RECEIVED == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_APP_AUTH_RESPONSE_RECEIVED; 
            else if(E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// Получен ответ на аутентификацию ИД-приложения
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

			// Получение блока данных для расшифрования
		case S_PROVIDER_DATA_DECRYPT_REQUESTED:
			if(E_PROVIDER_DATA_DECRYPT_ENTERED  == *inout_Event || IS_DEFAULT_EVENT)
				STATE = S_PROVIDER_DATA_DECRYPT;
			else if(E_PROVIDER_DATA_EMPTY == *inout_Event)
				STATE = S_PROVIDER_AUTH_CONFIRMATION; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// Расшифрование блока данных
		case S_PROVIDER_DATA_DECRYPT:
			if(E_EMPTY == *inout_Event)
				STATE = S_PROVIDER_DATA_DECRYPTED;
			else if(E_PROVIDER_DATA_EMPTY == *inout_Event)
				STATE = S_PROVIDER_AUTH_CONFIRMATION; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// Блок данных расшифрован
		case S_PROVIDER_DATA_DECRYPTED:
            if(*inout_Event == E_PROVIDER_DATA_PROCESSED || IS_DEFAULT_EVENT)
				STATE = S_PROVIDER_DATA_DECRYPT_REQUESTED;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;	

			// Подтверждение необходимости аутентификация Поставщика Услуги
		case S_PROVIDER_AUTH_CONFIRMATION:
			if(E_PROVIDER_AUTH_CONFIRMED == *inout_Event)
				STATE = S_AUTH_PROVIDER;
			else if(E_PROVIDER_AUTH_NOT_CONFIRMED == *inout_Event)
				STATE = S_APP_AUTH_RESPONSE_DATA_REQUESTED; 
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// Аутентификация Поставщика Услуги
		case S_AUTH_PROVIDER:
			if(E_EMPTY == *inout_Event)
				STATE = S_APP_AUTH_RESPONSE_DATA_REQUESTED;
			break;

			// Ввод результатов аутентификации ИД-приложения
		case S_APP_AUTH_RESPONSE_DATA_REQUESTED:
			if(E_PROCESS_APP_AUTH_RESPONSE_DATA == *inout_Event)
				STATE = S_PROCESS_AUTH_APP_RESPONSE_DATA;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event || E_EXCEPTION_RUNTIME_ERROR == *inout_Event) 
                justEntry = 0;
			break;

			// Обработка результатов аутентификации ИД-приложения
		case S_PROCESS_AUTH_APP_RESPONSE_DATA:
			if(E_EMPTY == *inout_Event) 
				STATE = p_opContext->AuthResult == 400 ? S_APDU_PACKET_WAITING : S_CARD_RELEASING;
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

			// Ожидание пакета APDU-команд 
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

			// Расшифрование пакета APDU-команд на сессионном ключе Поставщика Услуги
		case S_DECRYPT_APDU_PACKET:
			if(E_EXECUTE_APDU_PACKET == *inout_Event)
				STATE = S_EXECUTE_APDU_PACKET;
			break;

			// Выполнение APDU-комманд на разблокировку и установку временного значения КРП
		case S_EXECUTE_APDU_PACKET:
			if(E_EMPTY == *inout_Event)
				STATE = p_opContext->isApduEncryptedPS ? S_ENCRYPT_APDU_PACKET : S_PROCESS_APDU_PACKET;	
			break;

			// Шифрование ответа выполненного пакета APDU-команд на сессионном ключе Поставщика Услуги
		case S_ENCRYPT_APDU_PACKET:
			if (E_EMPTY == *inout_Event)
				STATE = S_PROCESS_APDU_PACKET;
			break;

			// Обработка хостом пакета APDU-комманд после их исполнения
		case S_PROCESS_APDU_PACKET:
			if (E_APDU_PACKET_PROCESSED == *inout_Event)
				STATE = S_APDU_PACKET_WAITING;	
			else if(E_APDU_PACKET_ABSENT == *inout_Event)
				STATE = S_CARD_RELEASING;		
			else // ошибка
                justEntry = 0;					
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

			// Выбор услуги
		case S_SERVICE_SELECTING:
            *inout_Event = opRunInterrupt (p_opContext, E_SERVICE_SELECTING);
            break;

           // Запрос на получение метаинформации по услуге 
        case S_METAINFO_REQUESTED:
            *inout_Event = opRunInterrupt (p_opContext, E_METAINFO_REQUESTED);
            break;

			// Аутентификация операции 
		case S_AUTH_OPERATION: 
			p_opContext->PinNum = IL_KEYTYPE_01_PIN;
			opRunAuthOperationA1(p_opContext, inout_Event);
            break;

			// Получение строки-запроса на чтение карточных данных 
		case S_CARD_DATA_REQUESTED:
            *inout_Event = opRunInterrupt (p_opContext, E_CARD_DATA_REQUESTED);
			break;

			// Чтение запрошенных карточных данных
		case S_CARD_DATA_READ:
			if((RC = opApiReadCardData(p_opContext, p_opContext->pCardDataDescr, p_opContext->pCardDataBuf, p_opContext->pCardDataLen)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Данные с карты подготовлены
		case S_CARD_DATA_PREPARED:
            *inout_Event = opRunInterrupt (p_opContext, E_CARD_DATA_PREPARED);
            break;

			// Получение дополнительных сведений операции
		case S_EXTRA_DATA_REQUESTED:
            *inout_Event = opRunInterrupt (p_opContext, E_EXTRA_DATA_REQUESTED);
            break;

			// Получение хэш XML-запроса на оказание услуги
		case S_HASH_REQUESTED:
            *inout_Event = opRunInterrupt(p_opContext, E_HASH_REQUESTED);
            break;

			// Подтверждение установления защищённой сессии с Поставщиком Услуги
		case S_PROVIDER_SESSION_CONFIRMATION:
            *inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_SESSION_CONFIRMATION);
			break;

			// Установление защищённой сессии с Поставщиком Услуги
		case S_PROVIDER_SESSION_ESTABLISH:
			if((RC = opApiSetProviderCryptoSession(p_opContext)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Аутентификация ИД-приложения
		case S_AUTH_APPLICATION:
			opRunAuthApplicationA1(p_opContext, inout_Event);
			break;

			// Запрос на аутентификацию ИД-приложения подготовлен
		case S_APP_AUTH_REQUEST_PREPARED:
            *inout_Event = opRunInterrupt(p_opContext, E_APP_AUTH_REQUEST_PREPARED);
			break;

			// Подтверждение необходимости формирования электронной подписи держателя карты
		case S_DIGITAL_SIGN_CONFIRMATION:
			if(p_opContext->phCrd->ifSign)
				*inout_Event = opRunInterrupt(p_opContext, E_DIGITAL_SIGN_CONFIRMATION);
			else
				*inout_Event = E_DIGITAL_SIGN_NOT_CONFIRMED;
			break;

			// Ввод пароля держателя электронной подписи
		case S_DIGITAL_SIGN_PIN_WAITING:
			p_opContext->PinNum = IL_KEYTYPE_02_PIN;
            *inout_Event = opRunInterrupt (p_opContext, E_DIGITAL_SIGN_PIN_REQUESTED);
			break;

			// Формирование электронной подписи держателя карты
		case S_PREPARE_DIGITAL_SIGN:
			// верифицируем гражданина по ПИН2
			if((RC = opCmnAppVerifyCitizen(p_opContext)) == 0)
			{	// формируем электронную подпись деержателя карты
				RC = opApiMakeDigitalSignature(p_opContext);
			}
			else if(RC == ILRET_CRD_VERIFY_WRONG_PASSWORD_PRESENTED && p_opContext->PinTriesLeft > 0)
			{	// повторная верификация гражданина по ПИН2
				*inout_Event = E_DIGITAL_SIGN_PIN_RETRY_REQUESTED;
				break;
			}
			if(!RC)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Получение блока данных для шифрования
		case S_PROVIDER_DATA_ENCRYPT_REQUESTED:
            *inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_DATA_ENCRYPT_REQUESTED);
			break;

			// Шифрование блока данных
		case S_PROVIDER_DATA_ENCRYPT:
			if((RC = opApiEncryptProviderToTerminal(p_opContext)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Блок данных зашифрован
		case S_PROVIDER_DATA_ENCRYPTED:
            *inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_DATA_ENCRYPTED);
			break;

			// Уведомление о посылка запроса на аутентификацию ИД-приложения
		case S_SEND_APP_AUTH_REQUEST:
            *inout_Event = opRunInterrupt(p_opContext, E_SEND_APP_AUTH_REQUESTED);
			break;

			// Получен ответ на аутентификацию ИД-приложения
		case S_APP_AUTH_RESPONSE_RECEIVED:
			if(p_opContext->isProviderSession)
				*inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_DATA_DECRYPT_REQUESTED);
			else
				*inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_AUTH_CONFIRMATION);
			break;

			// Получение блока данных для расшифрования
		case S_PROVIDER_DATA_DECRYPT_REQUESTED:
            *inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_DATA_DECRYPT_REQUESTED);
			break;

			// Расшифрование блока данных
		case S_PROVIDER_DATA_DECRYPT:
			if((RC = opApiDecryptProviderToTerminal(p_opContext)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Блок данных расшифрован
		case S_PROVIDER_DATA_DECRYPTED:
            *inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_DATA_DECRYPTED);
			break;

			// Подтверждение необходимости аутентификация Поставщика Услуги
		case S_PROVIDER_AUTH_CONFIRMATION:
            *inout_Event = opRunInterrupt(p_opContext, E_PROVIDER_AUTH_CONFIRMATION);
			break;

			// Аутентификация Поставщика Услуги
		case S_AUTH_PROVIDER:
			if((RC = opApiAuthServiceProvider(p_opContext)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Ввод результатов аутентификации ИД-приложения
		case S_APP_AUTH_RESPONSE_DATA_REQUESTED:
			*inout_Event = opRunInterrupt(p_opContext, E_APP_AUTH_RESPONSE_DATA_REQUESTED);
			break;

			// Обработка результатов аутентификации ИД-приложения
		case S_PROCESS_AUTH_APP_RESPONSE_DATA:
			PROT_WRITE_EX0(PROT_OPLIB1, "opCmnCheckAppAuthResponse()")
			RC = opCmnCheckAppAuthResponse(p_opContext, &authResult);
			PROT_WRITE_EX1(PROT_OPLIB1, "opCmnCheckAppAuthResponse()=%u", RC)
			if(!RC)
			{
				if(authResult == 0)
				{	// аутентификация выполнена успешно
					*inout_Event = E_EMPTY;
				}
				else if(authResult == 100)
				{	// ошибка проверки криптограммы аутентификации
					*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, ILRET_OPLIB_APP_AUTH_WRONG_CRYPTOGRAM);
				}
				else if(authResult == 200)
				{	// требуется изъятие карты
					*inout_Event = E_CARD_CAPTURE_REQUEST;
				}
				else if(authResult == 300)
				{	// требуется установление защищённого обмена между картой и эмитентом!!! 
					// формируем запрос на аутентификацию ИД-приложения для установления защищённой сессии с эмитентом
					if((RC = opApiPrepareAppAuthRequestIssSession(p_opContext)) == 0) 
					{	// пересчитываем контрольную сумму для аутентификации вновь сформированного запроса на установку защищённой сесси с эмитентом
						p_opContext->AuthRequestCrc = _calculate_crc(p_opContext->pAuthRequestBuf, *p_opContext->pAuthRequestLen);
						// сохраняем случайное число карты в контексте
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
				{	// аутентификация выполнена успешно
					*inout_Event = E_EMPTY;
				}
			}
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Установка защищённой сессии с эмитентом
		case S_ESTABLISH_ISSUER_SESSION:
			opRunEstablishIssuerSessionA1(p_opContext, inout_Event);
			break;

			// Ожидание пакета APDU-комманд 
		case S_APDU_PACKET_WAITING:
            *inout_Event = opRunInterrupt (p_opContext, E_APDU_PACKET_WAITING);
			break;

			// Расшифрование пакета APDU-команд на сессионном ключе Поставщика Услуги
		case S_DECRYPT_APDU_PACKET:
			if((RC = opApiDecryptProviderToTerminal(p_opContext)) == 0)
			{	// копируем расшифрованный пакет во внешний буфер
				cmnMemCopy(p_opContext->pApduIn, p_opContext->pClearData, (IL_WORD)(*p_opContext->pClearDataLen));
				p_opContext->ApduInLen = (IL_WORD)(*p_opContext->pClearDataLen);
				*inout_Event = E_EXECUTE_APDU_PACKET;
			}
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;			

			// Выполнение пакета APDU-комманд 
		case S_EXECUTE_APDU_PACKET:
			if((RC = opApiRunApduPacket(p_opContext)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Шифрование ответа выполненного пакета APDU-команд на сессионном ключе Поставщика Услуги
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

			// Обработка хостом пакета APDU-комманд после их исполнения
		case S_PROCESS_APDU_PACKET:
            *inout_Event = opRunInterrupt (p_opContext, E_PROCESS_APDU_PACKET);
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

			// Извлечение карты из ридера
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