/**  УЭК - Головной автомат подсистемы редактирования данных на карте
  */
#include "il_types.h"
#include "il_error.h"
#include "FuncLib.h"
#include "opCtxDef.h"
#include "opDescr.h"
#include "opEvent.h"
#include "opState.h"
#include "opRunFunc.h"
//#include "opCmnFunc.h"
#include "opApi.h"

#define STATE   p_opContext->State[p_opContext->Index]

IL_WORD maxBlockDataLen;

IL_FUNC IL_WORD opRunEditCardDataA0 (s_opContext *p_opContext, IL_WORD *inout_Event)
{
    IL_WORD oldState;
    IL_WORD RC;
    IL_BYTE justEntry;

    justEntry = 1; 

	// Автомат используется только для предопределённых операций!!!
	if(!(p_opContext->OperationCode == UEC_OP_EDIT_PRIVATE_DATA || p_opContext->OperationCode == UEC_OP_EDIT_OPERATOR_DATA))
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
				STATE = S_SECTORS_LIST_FILLING;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

            // Заполнение списка секторов открытой карты 
		case S_SECTORS_LIST_FILLING:
			if(E_EMPTY == *inout_Event)
				STATE = S_AUTH_OPERATION; 
			else
				justEntry = 0;
            break;

 			// Аутентификация перед выполнением операции
		case S_AUTH_OPERATION:
			if (E_EMPTY == *inout_Event)
                STATE = S_SET_SECTORS_DESCR_BUF; 
			break;

			// Установка буфера описателя секторов
		case S_SET_SECTORS_DESCR_BUF:
			if(*inout_Event == E_SECTORS_DESCR_BUF_SET || *inout_Event == E_SECTORS_DESCR_BUF_NOT_USED)
				STATE = S_SELECT_EDITING_BLOCK;
			if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;	
			break;

            // Выбор блока данных для редактирования 
		case S_SELECT_EDITING_BLOCK:
            if (E_EDITING_CARD_DATA_SELECTED == *inout_Event)
			{
				maxBlockDataLen = *p_opContext->pBlockDataLen;
				STATE = S_PREPARE_EDITING_CARD_DATA; 
			}
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;
            break;

			// Чтение с карты редактируемых данных в контекст
		case S_PREPARE_EDITING_CARD_DATA:
			if (E_EMPTY == *inout_Event)
                STATE = S_CARD_DATA_EDIT;
            else if(E_EXCEPTION_USER_BREAK == *inout_Event)
                justEntry = 0;			
			break;

			// Редактирование данных
		case S_CARD_DATA_EDIT:
            if (E_CARD_DATA_MODIFIED == *inout_Event)
				STATE = S_EDITED_CARD_DATA_WRITE;
			else if(E_CARD_DATA_NOT_MODIFIED == *inout_Event)
				STATE = S_CARD_RELEASING;
            else if (E_EXCEPTION_USER_BREAK == *inout_Event)
                STATE = S_CARD_RELEASING;
			else if (E_EXCEPTION_RUNTIME_ERROR == *inout_Event)
				justEntry = 0;
			break;

			// Запись отредактированных данных на карту
		case S_EDITED_CARD_DATA_WRITE:
			if (E_EMPTY == *inout_Event)
                STATE = S_PREPARE_EDITING_CARD_DATA; 
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

		case S_SECTORS_LIST_FILLING:
			// селектируем приложение
			PROT_WRITE_EX0(PROT_OPLIB1, "flAppSelect()")
			RC = flAppSelect(p_opContext->phCrd, NULL, NULL);
			PROT_WRITE_EX1(PROT_OPLIB1, "flAppSelect()=%u", RC)
			if(!RC)
				*inout_Event = E_EMPTY;
			else
                *inout_Event = opRunThrowException (p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

 			// Аутентификация перед выполнением операции
		case S_AUTH_OPERATION:
			opCtxSetPinNum(p_opContext, IL_KEYTYPE_01_PIN);
			opRunAuthOperationA1(p_opContext, inout_Event);
            break;

			// Установка буфера описателя секторов
		case S_SET_SECTORS_DESCR_BUF:
			*inout_Event = opRunInterrupt(p_opContext, E_SECTORS_DESCR_BUF_REQUESTED);
			break;

            // Выбор сектора и блока данных для редактирования 
		case S_SELECT_EDITING_BLOCK:
            if(p_opContext->pSectorsDescrBuf)
			{
				if((RC = opApiGetCardSectorsDescr(p_opContext, p_opContext->pSectorsDescrBuf, p_opContext->pSectorsDescrLen)) != 0)
					*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			}
			*inout_Event = opRunInterrupt(p_opContext, E_SELECT_EDITING_CARD_DATA);
            break;

			// Чтение из указанного сектора-блока данных для их последующего редактирования
		case S_PREPARE_EDITING_CARD_DATA:
			*p_opContext->pBlockDataLen = maxBlockDataLen;
			if((RC = opApiGetCardBlockDataDescr(p_opContext, p_opContext->pBlockDescr, p_opContext->pBlockDataBuf, p_opContext->pBlockDataLen)) == 0)
				*inout_Event = E_EMPTY;
			else
				*inout_Event = opRunThrowException(p_opContext, E_EXCEPTION_RUNTIME_ERROR, RC);
			break;

			// Редактирование карточных данных 
		case S_CARD_DATA_EDIT:
            *inout_Event = opRunInterrupt (p_opContext, E_CARD_DATA_EDIT_REQUSTED);
			break;

			// Запись отредактированных частных данных гражданина на карту
		case S_EDITED_CARD_DATA_WRITE:
			if((RC = opApiWriteCardData(p_opContext, p_opContext->pCardDataDescr)) == 0)
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