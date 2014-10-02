/**  Идентификаторы событий автоматов подсистем
  */
#ifndef __OP_EVENT_H_
#define __OP_EVENT_H_ 

// Макрос определения события по умолчанию
#define IS_DEFAULT_EVENT    (*inout_Event == p_opContext->InterruptEvent && justEntry == 1)

/**  Внешние события
  */

//  Активация автомата
#define E_ACTIVATE							0x0000
// Ожидание выбора услуги
#define E_SERVICE_SELECTING					0x0001
// Услуга выбрана
#define E_SERVICE_SELECTED					0x0002
// Ожидание установки карты в ридер
#define E_CARD_WAITING						0x0003
// Карта установлена
#define E_CARD_INSERTED						0x0004
// Ожидание извлечения карты из ридера 			
#define E_CARD_RELEASING					0x0005
// Карта извлечена
#define E_CARD_RELEASED						0x0006
// Запрос на получение метаинформации по услуге
#define E_METAINFO_REQUESTED				0x0007
// Метаинформация по услуге получена и сохранена в контексте
#define E_METAINFO_ENTERED					0x0008
// Запрос на ввод ПИН
#define E_PIN_REQUESTED						0x0009
// Введен ПИН
#define E_PIN_ENTERED						0x000A
// Повторный ввод ПИН
#define E_PIN_RETRY_REQUESTED				0x000B
// Запрос на получение внешних данных для оказания услуги
#define E_EXTRA_DATA_REQUESTED				0x000C
// Внешние данные получены
#define E_EXTRA_DATA_ENTERED				0x000D
// Данные с карты подготовлены
#define E_CARD_DATA_PREPARED				0x000E
// Данные запроса на оказание услуги подготовлены
#define E_SERVICE_REQUEST_DATA_PREPARED		0x000F
// Запрос на аутентификацию ИД-приложения сформирован
#define E_APP_AUTH_REQUEST_PREPARED			0x0010
// Обработка результатов аутентификации ИД-приложения
#define E_PROCESS_APP_AUTH_RESPONSE_DATA	0x0011
// Запрос на ввод нового ПИН
#define E_NEW_PIN_REQUESTED					0x0012
// Новый ПИН введён
#define E_NEW_PIN_ENTERED					0x0013
// Запрос на ввод подтверждающнго ПИН
#define E_CONFIRM_PIN_REQUESTED				0x0014
// Подтверждающий ПИН введён
#define E_CONFIRM_PIN_ENTERED				0x0015
// Выбор данных для изменения
#define E_SELECT_EDITING_CARD_DATA			0x0016
// Данных для редактирования выбраны
#define E_EDITING_CARD_DATA_SELECTED		0x0017
// Запрос на редактирование считанных с карты данных
#define E_CARD_DATA_EDIT_REQUSTED			0x0018
// Редактируемые карточные данные изменены
#define E_CARD_DATA_MODIFIED				0x0019
// Редактируемые карточные данные не изменены
#define E_CARD_DATA_NOT_MODIFIED			0x001A
// Запрос на ввод КРП
#define E_PUK_REQUESTED						0x001B
// Введен КРП
#define E_PUK_ENTERED						0x001C
// Запрос на повторный ввод КРП
#define E_PUK_RETRY_REQUESTED				0x001D
// Запрос на ввод нового КРП
#define E_NEW_PUK_REQUESTED					0x001E
// Новый КРП введён
#define E_NEW_PUK_ENTERED					0x001F
// Требуется установка защищённой сессии с эмитентом
#define E_ISSUER_SESSION_REQUESTED			0x0020
// Криптограмма аутентификации хоста для защищённой сессии с эмитентом подготовлена
#define E_ISSUER_AUTH_CRYPTOGRAMM_READY		0x0021
// Требуется проверка установленной защищённой сессии с эмитентом
#define E_CHECK_ISSUER_SESSION_REQUESTED	0x0022
// Установленная защищённая сессии с эмитентом проверена
#define E_ISSUER_SESSION_CHECKED			0x0023
// Ожидание пакета APDU-комманд
#define E_APDU_PACKET_WAITING				0x0024
// Пакет APDU-комманд введён
#define E_APDU_PACKET_ENTERED				0x0025
// Пакет APDU-комманд оттствует
#define E_APDU_PACKET_ABSENT				0x0026
// Обработка хостом пакета APDU-комманд после их исполнения
#define E_PROCESS_APDU_PACKET				0x0027
// Обработка хостом пакета APDU-комманд успешно завершена
#define E_APDU_PACKET_PROCESSED				0x0028
// Запрос на получение хэш XML-запроса услуги
#define E_HASH_REQUESTED					0x0029
// Хэш XML-запроса услуги введён
#define E_HASH_ENTERED						0x002A
// Ввод строки-запроса на чтение карточных данных для оказания услуги
#define E_CARD_DATA_REQUESTED				0x002B
// Строка-запрос на чтение карточных данных введёна
#define E_CARD_DATA_DESCR_ENTERED			0x002C
// Требуется изъятие карты
#define E_CARD_CAPTURE_REQUESTED			0x002D
// Карта изъята
#define E_CARD_CAPTURED						0x002E
// Требуется блокировка карты
#define E_CARD_LOCK_REQUESTED				0x002F
// Запрос на получение внешнего описателя добавляемого сектора
#define E_SECTOR_EX_DESCR_REQUESTED			0x0030
// Параметры внешнего описателя добавляемого сектора введены
#define E_SECTOR_EX_DESCR_ENTERED			0x0031
// Требуется установка буфера для описателя секторов
#define E_SECTORS_DESCR_BUF_REQUESTED		0x0032
// Буфера для описателя секторов установлен
#define E_SECTORS_DESCR_BUF_SET				0x0033
// Буфера для описателя секторов не используется
#define E_SECTORS_DESCR_BUF_NOT_USED		0x0034
// Верификация гражданина прошла успешно
#define E_CITIZEN_VERIFIED					0x0035
// Подтверждение формирования электронной подписи держателя карты
#define E_DIGITAL_SIGN_CONFIRMATION			0x0036
// Формирование электронной подписи держателя карты подтверждено
#define E_DIGITAL_SIGN_CONFIRMED			0x0037
// Формирование электронной подписи держателя карты не требуется
#define E_DIGITAL_SIGN_NOT_CONFIRMED		0x0038
// Запрос на ввод пароля держателя электронной подписи
#define E_DIGITAL_SIGN_PIN_REQUESTED		0x0039
// Пароль держателя электронной подписи введён
#define E_DIGITAL_SIGN_PIN_ENTERED			0x003A
// Повторный ввод пароля держателя электронной подписи
#define E_DIGITAL_SIGN_PIN_RETRY_REQUESTED	0x003B
// Электронная подпись держателя карты сформирована
#define E_DIGITAL_SIGN_PREPARED				0x003C
// Подтверждение установления защищённой сессии с Поставщиком Услуги 
#define E_PROVIDER_SESSION_CONFIRMATION		0x003D
// Установка защищённой сессии с Поставщиком Услуги подтверждена 
#define E_PROVIDER_SESSION_CONFIRMED		0x003E
// Установка защищённой сессии с Поставщиком Услуги не требуется 
#define E_PROVIDER_SESSION_NOT_CONFIRMED	0x003F
// Защищённая сессии с Поставщиком Услуги установлена
#define E_PROVIDER_SESSION_ESTABLISHED		0x0040
// Защищённая сессии с Поставщиком Услуги установлена на стороне хоста
#define E_PROVIDER_SESSION_HOST_ESTABLISHED 0x0041
// Требуется передать блок данных для шифрования в рамках сессии с Поставщиком Услуги
#define E_PROVIDER_DATA_ENCRYPT_REQUESTED	0x0042
// Блок данных для шифрования передан
#define E_PROVIDER_DATA_ENCRYPT_ENTERED		0x0043
// Блок данных для шифрования/расшифрования отсутствует
#define E_PROVIDER_DATA_EMPTY				0x0044
// Блок данных зашифрован
#define E_PROVIDER_DATA_ENCRYPTED			0x0045
// Зашифрованный/Расшифрованный блок данных использован
#define E_PROVIDER_DATA_PROCESSED			0x0046
// Требуется передать блок данных для расшифрования в рамках сессии с Поставщиком Услуги
#define E_PROVIDER_DATA_DECRYPT_REQUESTED	0x0047
// Блок данных для расшифрования передан
#define E_PROVIDER_DATA_DECRYPT_ENTERED		0x0048
// Блок переданных данных расшифрован
#define E_PROVIDER_DATA_DECRYPTED			0x0049
// Подтверждение необходимости аутентификации Поставщика Услуги
#define E_PROVIDER_AUTH_CONFIRMATION		0x004A
// Аутентификация Поставщика Услуги подтверждена 
#define E_PROVIDER_AUTH_CONFIRMED			0x004B
// Аутентификация Поставщика Услуги не требуется 
#define E_PROVIDER_AUTH_NOT_CONFIRMED		0x004C
// Получен ответ на аутентификацию ИД-приложения 
#define E_APP_AUTH_RESPONSE_RECEIVED		0x004D
// Необходимы данные ответа на аутентификацию ИД-приложения 
#define E_APP_AUTH_RESPONSE_DATA_REQUESTED  0x004E
// Введён пакет APDU-команд, зашифрованный на ключе Провайдера Услуги
#define E_APDU_ENCRYPTED_PACKET_ENTERED		0x004F
// Запрос на ввод пароля активации модуля безопасности 
#define E_SE_ACTIVATION_PIN_REQUESTED		0x0050
// Пароль активации модуля безопасности введён
#define E_SE_ACTIVATION_PIN_ENTERED			0x0051
// Повторный ввод пароля активации модуля безопасности
#define E_SE_ACTIVATION_PIN_RETRY_REQUESTED	0x0052
// Требуется предать имя владельца МБ 
#define E_SE_OWNER_NAME_REQUESTED			0x0053
// Имя владельца МБ введён
#define E_SE_OWNER_NAME_ENTERED				0x0054
// Ввод фразы контрольного приветствия
#define E_PASS_PHRASE_REQUESTED				0x0055
// Фраза контрольного приветствия введена
#define E_PASS_PHRASE_ENTERED				0x0056
// Требуется посылка зароса на аутентификацию ИД-приложения
#define E_SEND_APP_AUTH_REQUESTED			0x0057
// Продолжение выполнения 
#define E_CONTINUE							0x0058


/*  Исключения
 * Диапазон исключений резервируется от 0x1000 до 0x1200.
 * Актуально до 0x1100, оставльное пространство для перехвата исключений.
 */

// Базовый идентификатор исключений 
#define E_EXCEPTION_EVENTS                  0x1000 
//  Недопустимая глубина вложенного автомата 
#define E_EXCEPTION_DEPTH                   E_EXCEPTION_EVENTS + 0x01
//  Недопустимое использование подсистемы 
#define E_EXCEPTION_USE                     E_EXCEPTION_EVENTS + 0x02
//  Ошибка выполнения 
#define E_EXCEPTION_RUNTIME_ERROR           E_EXCEPTION_EVENTS + 0x03
//  Отказ пользователя 
#define E_EXCEPTION_USER_BREAK              E_EXCEPTION_EVENTS + 0x04
// Экстренное прерывание 
#define E_EXCEPTION_INTERRUPT               E_EXCEPTION_EVENTS + 0x05
//  Зацикливание автомата
#define E_EXCEPTION_CYCLING_DETECTED        E_EXCEPTION_EVENTS + 0xFF
// Смещение к базе Исключительного события при его обработке 
#define E_CATCHING_OFFSET_EVENTS            0x100                   
//  Макрос определяет ялвляется ли исключение экстренным
#define IS_EMERGENCY_EXCEPTION(EVENT)       (EVENT == E_EXCEPTION_INTERRUPT)
//  Возвращает реальное значение исключения после его перехвата
#define IS_CATCHED_EXCEPTION(EVENT)         ((EVENT) - E_CATCHING_OFFSET_EVENTS)
// Макрос определения вхождения события в спектр исключений
#define IS_EXCEPTION_EVENT_RANGE(EVENT)     ((EVENT >= E_EXCEPTION_EVENTS) && (EVENT <= (E_EXCEPTION_EVENTS + 0x100)))

// Базовый идентификатор внутренних событий 
#define E_INTERNAL_EVENTS                   0x0400
// Успешное завершение операции 
#define E_EMPTY                             E_INTERNAL_EVENTS + 0x00
//  ПИН неверный 
#define E_PIN_IS_INCORRECT					E_INTERNAL_EVENTS + 0x01
// Требуется установка защищённого обмена между картой и эмитентом ИД-приложения 
#define E_ISSUER_SESSION_REQUEST			E_INTERNAL_EVENTS + 0x02
// Требуется изъятие карты 
#define E_CARD_CAPTURE_REQUEST				E_INTERNAL_EVENTS + 0x03
// Требуется выполнение подготовленного пакета 
#define E_EXECUTE_APDU_PACKET				E_INTERNAL_EVENTS + 0x04	


//  Бит события, обозначающий, что событие официально возвращено из вложенного автомата
#define RETURN_MARK 0x2000
//  Проверяет у события признак события, официально возвращенного из вложенного автомата для обработки вышестоящим автоматом
#define IS_EVENT_RETURN_MARKED(EVENT) ((EVENT & RETURN_MARK) >> 13)
// Очищает бит RETURN_MARK, оставляя лишь чистое событие.
// Если событие не помечено RETURN_MARK, то будет возвращено событие с данной пометкой.
#define IS_RETURN_EVENT(EVENT) (EVENT ^ RETURN_MARK)


#endif//__OP_EVENT_H_