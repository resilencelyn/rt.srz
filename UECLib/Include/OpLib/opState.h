/**  Модуль содержит список всевозможных состояний автоматов системы.
  */
#ifndef __OP_STATE_H_
#define __OP_STATE_H_


// Режим ожидания
#define S_IDLE								0x0000
// Обработка исключительных ситуаций
#define S_EXCEPTION_CATCHING				0x0001
// Ожидание выбора операции
#define S_SERVICE_SELECTING					0x0002
// Ожидание МК Клиента
#define S_CARD_WAITING						0x0003
// Ожидание МК Клиента
#define S_CARD_RELEASING					0x0004
// Запрос на получение метаинформации по услуге
#define S_METAINFO_REQUESTED				0x0005
// Селектирование ИД-приложения
#define S_APP_SELECTING						0x0006
// Аутентификация терминала
#define S_AUTH_TERMINAL						0x0007
// Ожидание ввода ПИН
#define S_PIN_WAITING						0x0008
// Ожидание повторного ввода ПИН
#define S_PIN_RETRY							0x0009
// Верификация гражданина
#define S_VERIFY_CITIZEN					0x000A
// Запрос на получение дополнительных данных на оказание услуги
#define S_EXTRA_DATA_REQUESTED				0x000B
// Запрос на чтение данных с карты
#define S_CARD_DATA_READ					0x000C
// Данные с карты считаны
#define S_CARD_DATA_PREPARED				0x000D
// Аутентификация ИД-приложения
#define S_AUTH_APP_REQUESTED				0x000E
// Запрос на аутентификацию ИД-приложения сформирован
#define S_AUTH_APP_REQUEST_PREPARED			0x000F
// Обработка результатов аутентификации ИД-приложения
#define S_PROCESS_AUTH_APP_RESPONSE_DATA	0x0010
// Ожидание ввода нового ПИН 
#define S_NEW_PIN_WAITING					0x0011
// Ожидание подтверждающего ПИН
#define S_CONFIRM_PIN_WAITING				0x0012
// Смена ПИН
#define S_CHANGE_PIN						0x0013
// Выбор блока данных для редактирования
#define S_SELECT_EDITING_BLOCK				0x0014
// Селектирование приложения и заполнение списка секторов
#define S_SECTORS_LIST_FILLING				0x0015
// Аутентификация перед выполнением операции
#define S_AUTH_OPERATION					0x0016
// Подготовка карточных данных для редактирования
#define S_PREPARE_EDITING_CARD_DATA			0x0017
// Редактирование карточных данных
#define S_CARD_DATA_EDIT					0x0018
// Запись на карту изменённых данных
#define S_EDITED_CARD_DATA_WRITE			0x0019
// Ожидание ввода КРП 
#define S_PUK_WAITING						0x001A
// Ожидание повторного ввода КРП
#define S_PUK_RETRY							0x001B
// Ожидание ввода нового КРП
#define S_NEW_PUK_WAITING					0x001C
// Инициация установки защищённого соединения с эмитентом
#define S_CRYPTO_ISSUER_SESSION_INIT		0x001D
// Аутентификация эмитента (утановка защищённого соединения с эмитентом)
#define S_AUTH_ISSUER						0x001E
// Проверка хостом утановки защищённого соединения с эмитентом
#define S_CHECK_ISSUER_SESSION				0x001F
// Разблокировка КРП с использованием временного значения
#define S_UNLOCK_TMP_PUK					0x0020
// Установка защищённой сессии с эмитентом
#define S_ESTABLISH_ISSUER_SESSION			0x0021
// Аутентификация ИД-приложения
#define S_AUTH_APPLICATION					0x0022
// Ожидание пакета APDU-команд
#define S_APDU_PACKET_WAITING				0x0023
// Выполнение пакета APDU-команд
#define S_EXECUTE_APDU_PACKET				0x0024
// Пакета APDU-комманд отсутствует
#define S_APDU_PACKET_ABSENT				0x0025
// Обработка хостом пакета APDU-команд после их исполнения 
#define S_PROCESS_APDU_PACKET				0x0026
// Получение хэш XML-запроса услуги
#define S_HASH_REQUESTED					0x0027
// Получение строки-запроса на чтение карточных данных для услуги
#define S_CARD_DATA_REQUESTED				0x0028
// Требуется изъятие карты
#define S_CARD_CAPTURE_REQUESTED			0x0029
// Бокировка карты
#define S_CARD_LOCK							0x002A
// Получение внешнего описателя добавляемого сектора
#define S_SECTOR_EX_DESCR_REQUESTED			0x002B
// Добавление внешнего описателя сектора
#define S_ADD_SECTOR_EX_DESCR				0x002C
// Установка буфера для описателей секторов карты
#define S_SET_SECTORS_DESCR_BUF				0x002D
// Аутентификация операции прошла успешно
#define S_CITIZEN_VERIFIED					0x002E
// Подтверждение формирования электронной подписи держателя карты
#define S_DIGITAL_SIGN_CONFIRMATION			0x002F
// Ожидание ввода ПИН электронной подписи держателя карты
#define S_DIGITAL_SIGN_PIN_WAITING			0x0030
// Формирование электронной подписи держателя карты
#define S_PREPARE_DIGITAL_SIGN				0x0031
// Запрос на аутентификацию ИД-приложения подготовлен
#define S_APP_AUTH_REQUEST_PREPARED			0x0032
// Подтверждение необходимости установления защищённой сессии с Поставщиком Услуги 
#define S_PROVIDER_SESSION_CONFIRMATION		0x0033
// Установление защищённой сессии с Поставщиком Услуги
#define S_PROVIDER_SESSION_ESTABLISH		0x0034
// Защищённая сессии с Поставщиком Услуги установлена
#define S_PROVIDER_SESSION_ESTABLISHED	    0x0035
// Получение блока данных для шифрования
#define S_PROVIDER_DATA_ENCRYPT_REQUESTED	0x0036
// Шифрование блока данных
#define S_PROVIDER_DATA_ENCRYPT				0x0037
// Блок данных зашифрован
#define S_PROVIDER_DATA_ENCRYPTED			0x0038
// Получение блока данных для расшифрования
#define S_PROVIDER_DATA_DECRYPT_REQUESTED	0x0039
// Расшифрование блока данных
#define S_PROVIDER_DATA_DECRYPT				0x003A
// Блок данных расшифрован
#define S_PROVIDER_DATA_DECRYPTED			0x003B
// Подтверждение необходимости аутентификация Поставщика Услуги 
#define S_PROVIDER_AUTH_CONFIRMATION		0x003C
// Аутентификация Поставщика Услуги
#define S_AUTH_PROVIDER						0x003D
// Ожидание ввода данных ответа на запрос аутентификации ИД-приложения
#define S_APP_AUTH_RESPONSE_DATA_REQUESTED	0x003E
// Получен ответ на запрос аутентификации ИД-приложения
#define S_APP_AUTH_RESPONSE_RECEIVED		0x003F
// Расшифрование пакета APDU-команд на сессионном ключе Поставщика Услуги
#define S_DECRYPT_APDU_PACKET				0x0040
// Шифрование ответа выполненного пакета APDU-команд на сессионном ключе Поставщика Услуги
#define S_ENCRYPT_APDU_PACKET				0x0041
// Верификация владельца модуля безопасности
#define S_VERIFY_SE_OWNER					0x0042
// Начало процедуры активации/деактивации модуля безопасности
#define S_START_SE_ACTIVATION				0x0043
// Завершение процедуры активации/деактивации модуля безопасности
#define S_FINISH_SE_ACTIVATION				0x0044
// Ожидание ввода ПИН активации модуля безопасности
#define S_SE_ACTIVATION_PIN_WAITING			0x0045
// Получение имени владельца МБ 
#define S_SE_OWNER_NAME_REQUESTED			0x0046
// Ожидание ввода контрольного привктствия
#define S_PASS_PHRASE_WAITING				0x0047
// Запись контрольного приветствия на карту
#define S_PASS_PHRASE_WRITE					0x0048
// Уведомление об отправе запроса на аутентификацию ИД-приложения\оказание услуги
#define S_SEND_APP_AUTH_REQUEST				0x0049


#endif//__OP_STATE_H_