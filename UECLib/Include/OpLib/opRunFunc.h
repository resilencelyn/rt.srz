/** Объявление всех автоматов системы
  */
#ifndef __OP_RUN_H_
#define __OP_RUN_H_ 

#include "OpRunA0.h" //+++

#ifdef __cplusplus
extern "C" {
#endif

/**  Настройка и проверка контекста подсистемы при активации
  */
IL_FUNC IL_WORD opRunInitialize (s_opContext *p_opContext);

/**  Организовывает прерывание автомата для передачи управления внешней системе
  *  Инкапсулирует механизм предотвращения зацикливания автоматов, реализуемый следующим образом:
  * в случае многократного вызова функции dpRunInterrupt с передачей в качестве аргумента одного
  * и того же события ситуация детерминируется как зацикливание с последующим возникнованием исключительной 
  * ситуации. При возникновении исключительной ситуации функция возвращает соответствующее исключение, а не
  * переданное ей событие прервание.
  *  in_InterruptEvent - Событие-прерывание 
  * @return Возвращает событие-прерывание или событие-исключение "Зацикливание автомата".
  */
IL_FUNC IL_WORD opRunInterrupt(s_opContext *p_opContext, IL_WORD in_InterruptEvent);

/**  Проверяет условие выхода из автомата на основании его состояния
  *  in_OldState - Значение предыдущего состояния (перед первым Switch)
  *  inout_JustEntry - Флаг первичного вызова функции
  *  in_Event - внутреннее событие автомата (*inout_Event)
  *  При первом вызове функции внутри автомата возвращается 0 (выход не требуется).
  * @return 0 - выход не требуется
  * @return >0 - выход требуется
  */
IL_FUNC IL_BYTE opRunTestExitCondition (s_opContext *p_opContext, IL_WORD in_OldState, IL_WORD in_Event, IL_BYTE *inout_JustEntry);

/**  Захват исключительного события. 
  *  in_ExceptionEvent - Исключение
  *  Дальнешее определение возникшего (обрабатываемого) исключение 
  * происходит с помощью макроса IS_CATCHED_EXCEPTION.
  */
IL_FUNC IL_WORD opRunCatchException(s_opContext *p_opContext, IL_WORD in_ExceptionEvent);

/**  Организовывает исключительную ситуация в автомате
  *  in_ExceptionEvent - Исключение
  *  in_wResultCode - Код обоснования возникновения исключительной ситуации
  *  Исключение выбрасывается только в том случае, когда
  * когда логика автоматов не знает как обработать сложившуюся ситуацию.
  * При этом само исключительное событие сопровождается кодом
  * обоснования (ResultCode), которое сохраняется в контексте системы
  * для последующего анализа уже со стороны внешней системы.
  * Таким образом использование поля ResultCode контекста 
  * должно происходить только при возникновении исключительной
  * ситуации. in_wResultCode - может быть равен 0!
  * @return Возвращает Исключение
  */
IL_FUNC IL_WORD opRunThrowException(s_opContext *p_opContext, IL_WORD in_ExceptionEvent, IL_WORD in_wResultCode);

/**  Возвращает событие, помеченное RETURN_MARK
  *  Данное событие будет официально передано в вышестоящий автомат
  * для его обработки. См. макрос IS_RETURN_EVENT, IS_EVENT_RETURN_MARKED
  */
IL_FUNC IL_WORD opRunReturnEvent (IL_WORD in_Event);

/**  Увеличивает индекс текущего автомата в контексте
  * @return 0 - ошибка
  * @return >0 - успешно
  */
IL_FUNC IL_BYTE opRunIncreaseIndex (s_opContext *p_opContext);

/**  Уменьшает  индекс текущего автомата в контексте
  */
IL_FUNC void opRunDecreaseIndex (s_opContext *p_opContext);

// Вспомогательные автоматы
IL_FUNC IL_WORD opRunAuthOperationA1 (s_opContext *p_opContext, IL_WORD *inout_Event);
IL_FUNC IL_WORD opRunEstablishIssuerSessionA1 (s_opContext *p_opContext, IL_WORD *inout_Event);
IL_FUNC IL_WORD opRunAuthApplicationA1 (s_opContext *p_opContext, IL_WORD *inout_Event);



#ifdef __cplusplus
}
#endif

#endif//__OP_RUN_H_ 
