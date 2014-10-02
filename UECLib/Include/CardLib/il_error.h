#ifndef _IL_ERROR_H_
#define _IL_ERROR_H_

// Успешное выполнение
#define ILRET_OK							0

// Системная ошибка
#define ILRET_SYS_ERROR						200
// Ошибка динамического выделения памяти
#define ILRET_SYS_MEM_ALLOC_ERROR			201
// Неверный аргумент при вызове функции
#define ILRET_SYS_INVALID_ARGUMENT			202

// Ошибка карт-ридера
#define ILRET_SCR_ERROR						300
// Отсутствует питание на карте
#define ILRET_SCR_UNPOWERED_CARD			ILRET_SCR_ERROR+1
// Карта отсутствует в карт-ридере
#define ILRET_SCR_REMOVED_CARD				ILRET_SCR_ERROR+2
// Повторный RESET карты
#define ILRET_SCR_RESET_CARD				ILRET_SCR_ERROR+3
// Карта не отвечает на RESET
#define ILRET_SCR_UNRESPONSIVE_CARD			ILRET_SCR_ERROR+4
// Ошибка протокола карты
#define ILRET_SCR_PROTOCOL_ERROR			ILRET_SCR_ERROR+5
// Ошибка разделяемого доступа к карт-ридеру
#define ILRET_SCR_SHARING_VIOLATION			ILRET_SCR_ERROR+6
// Неизвестный карт-ридер
#define ILRET_SCR_UNKNOWN_READER			ILRET_SCR_ERROR+7
// Карт-ридер не готов
#define ILRET_SCR_NOT_READY					ILRET_SCR_ERROR+8
// Данный протокол APDU обмена не поддерживается
#define ILRET_SCR_PROTO_MISMATCH			ILRET_SCR_ERROR+9
// Карт-ридер не поддерживает коммуникацию с картой
#define ILRET_SCR_UNSUPPORTED_CARD			ILRET_SCR_ERROR+10
// Неверный ATR карты
#define ILRET_SCR_INVALID_ATR				ILRET_SCR_ERROR+11
// Некорректный идентификатор соединения карт-ридера
#define ILRET_SCR_INVALID_HANDLE			ILRET_SCR_ERROR+12
// Неверный ридер
#define ILRET_SCR_INVALID_DEVICE			ILRET_SCR_ERROR+13
// Превышено время ожидания ответа карт-ридера
#define ILRET_SCR_TIMEOUT					ILRET_SCR_ERROR+14
// Карт-ридер не доступен
#define ILRET_SCR_READER_UNAVAILABLE		ILRET_SCR_ERROR+15

// Ошибка карты
#define ILRET_CRD_ERROR						ILRET_SCR_ERROR+50
// Неверная длина ответа карты
#define ILRET_CRD_LENGTH_NOT_MATCH			ILRET_SCR_ERROR+51
// Не найден тег в возвращаемых в ответе карты данных
#define ILRET_CRD_APDU_TAG_NOT_FOUND		ILRET_SCR_ERROR+52
// Неверная длина тега возвращаемых в ответе карты данных
#define ILRET_CRD_APDU_TAG_LEN_ERROR		ILRET_SCR_ERROR+53
// Неверный формат данных APDU-команды
#define ILRET_CRD_APDU_DATA_FORMAT_ERROR	ILRET_SCR_ERROR+54

// Ошибка верификации гражданина
#define ILRET_CRD_VERIFY_ERROR		  		ILRET_SCR_ERROR+60
// Неправильная длина выходных данных верификации гражданина
#define ILRET_CRD_VERIFY_WRONG_LENGTH  		ILRET_SCR_ERROR+61
// Неправильные значения параметров P1/P2 команды верификации гражданина
#define ILRET_CRD_VERIFY_WRONG_PARAMETERS	ILRET_SCR_ERROR+62
// Пароль заблокирован
#define ILRET_CRD_VERIFY_PASSWORD_BLOCKED	ILRET_SCR_ERROR+63
// Указанный в команде верификации гражданина ключ не найден
#define ILRET_CRD_VERIFY_PASSWORD_NOT_FOUND	ILRET_SCR_ERROR+64
// Неверное значение пароля
#define ILRET_CRD_VERIFY_WRONG_PASSWORD_PRESENTED	ILRET_SCR_ERROR+65

// Ошибка селектирования приложения/файла
#define ILRET_CRD_SELECT_ERROR		  		ILRET_SCR_ERROR+70
// Неправильная длина входных данных команды селектирования
#define ILRET_CRD_SELECT_WRONG_LENGTH  		ILRET_SCR_ERROR+71
// Неправильные значения параметров P1/P2 команды селектирования
#define ILRET_CRD_SELECT_WRONG_PARAMETERS	ILRET_SCR_ERROR+72
// Выбранный объект блокирован
#define ILRET_CRD_SELECT_OBJECT_BLOCKED		ILRET_SCR_ERROR+73
// Входные данные команды селектирования имеют недопустимые значения
#define ILRET_CRD_SELECT_WRONG_CMD_DATA		ILRET_SCR_ERROR+74
// Селектируемый объект не найден
#define ILRET_CRD_SELECT_FILE_NOT_FOUND		ILRET_SCR_ERROR+75
// Отсутствует информация о селектированном объекте
#define ILRET_CRD_SELECT_RESPONSE_ABSENT	ILRET_SCR_ERROR+76
// Сектор с указанным идентификатором отсутствует в списке секторов
#define ILRET_CRD_SELECT_SECTOR_NOT_FOUND	ILRET_SCR_ERROR+77
// Блок с указанным идентификатором отсутствует в списке блоков сектора
#define ILRET_CRD_SELECT_BLOCK_NOT_FOUND	ILRET_SCR_ERROR+78

// Ошибка аутентификации ИД-приложения
#define ILRET_CRD_INTAUTH_ERROR		  		ILRET_SCR_ERROR+80
// Неправильная длина входных данных команды аутентификации ИД-приложения
#define ILRET_CRD_INTAUTH_WRONG_LENGTH  	ILRET_SCR_ERROR+81
// Неправильные значения параметров P1/P2 команды аутентификации ИД-приложения
#define ILRET_CRD_INTAUTH_WRONG_PARAMETERS	ILRET_SCR_ERROR+82
// Входные данные команды аутентификации ИД-приложения имеют недопустимые значения
#define ILRET_CRD_INTAUTH_WRONG_CMD_DATA	ILRET_SCR_ERROR+84
// Указанный в команде аутентификации ИД-приложения ключ не найден
#define ILRET_CRD_INTAUTH_KEY_NOT_FOUND		ILRET_SCR_ERROR+85

// Ошибка аутентификации внешнего субъекта
#define ILRET_CRD_MUTAUTH_ERROR		  		ILRET_SCR_ERROR+90
// Неправильная длина входных данных команды аутентификации внешнего субъекта
#define ILRET_CRD_MUTAUTH_WRONG_LENGTH 		ILRET_SCR_ERROR+91
// Неправильные значения параметров P1/P2 команды аутентификации внешнего субъекта
#define ILRET_CRD_MUTAUTH_WRONG_PARAMETERS	ILRET_SCR_ERROR+92
// Неверное значение криптограммы аутентификации внешнего субъекта
#define ILRET_CRD_MUTAUTH_WRONG_CRYPTO		ILRET_SCR_ERROR+93
// Указанный в команде аутентификации внешнего субъекта ключ не найден
#define ILRET_CRD_MUTAUTH_KEY_NOT_FOUND		ILRET_SCR_ERROR+94
// Приложение не подготовлено к получению команды аутентификации внешнего субъекта
#define ILRET_CRD_MUTAUTH_CONDITIONS		ILRET_SCR_ERROR+95

// Ошибка получения случайного числа карты
#define ILRET_CRD_GETCHAL_ERROR		  		ILRET_SCR_ERROR+100
// Неправильная длина входных данных команды получения случайного числа карты
#define ILRET_CRD_GETCHAL_WRONG_LENGTH 		ILRET_SCR_ERROR+101
// Неправильные значения параметров P1/P2 команды получения случайного числа карты
#define ILRET_CRD_GETCHAL_WRONG_PARAMETERS	ILRET_SCR_ERROR+102

// Ошибка изменения/установки значения критических данных
#define ILRET_CRD_CHDATA_ERROR		  		ILRET_SCR_ERROR+110
// Неправильная длина входных данных команды изменения/установки значения критических данных
#define ILRET_CRD_CHDATA_WRONG_LENGTH 		ILRET_SCR_ERROR+111
// Не выполнены условия безопасности для команды изменения/установки значения критических данных
#define ILRET_CRD_CHDATA_WRONG_CRYPTO		ILRET_SCR_ERROR+112
// Неверная структура данных команды изменения/установки значения критических данных
#define ILRET_CRD_CHDATA_WRONG_DATA_STRUCT	ILRET_SCR_ERROR+113
// Неверное значение имитовставки команды изменения/установки значения критических данных
#define ILRET_CRD_CHDATA_WRONG_SM_TAG		ILRET_SCR_ERROR+114
// Неправильные значения параметров P1/P2 команды изменения/установки значения критических данных
#define ILRET_CRD_CHDATA_WRONG_PARAMETERS	ILRET_SCR_ERROR+115
// Указанный в команде изменения/установки значения критических данных ключ не найден
#define ILRET_CRD_CHDATA_KEY_NOT_FOUND		ILRET_SCR_ERROR+116
// Длина устанавливаемого пароля меньше допустимой
#define ILRET_CRD_CHDATA_DATA_LEN_TOO_SHORT	ILRET_SCR_ERROR+117

// Ошибка разблокировки пароля
#define ILRET_CRD_RSTCNTR_ERROR		  		ILRET_SCR_ERROR+120
// Неправильная длина входных данных команды разблокировки пароля
#define ILRET_CRD_RSTCNTR_WRONG_LENGTH 		ILRET_SCR_ERROR+121
// Неверная структура данных команды разблокировки пароля
#define ILRET_CRD_RSTCNTR_WRONG_DATA_STRUCT	ILRET_SCR_ERROR+123
// Неверное значение одного из тегов SM команды разблокировки пароля
#define ILRET_CRD_RSTCNTR_WRONG_SM_TAG		ILRET_SCR_ERROR+124
// Неправильные значения параметров P1/P2 команды разблокировки пароля
#define ILRET_CRD_RSTCNTR_WRONG_PARAMETERS	ILRET_SCR_ERROR+125
// Указанный в команде разблокировки пароля ключ не найден
#define ILRET_CRD_RSTCNTR_KEY_NOT_FOUND		ILRET_SCR_ERROR+126

// Ошибка выполнения криптографической операции
#define ILRET_CRD_PERFSECOP_ERROR			ILRET_SCR_ERROR+130
// Неправильная длина входных данных команды выполнения криптографической операции
#define ILRET_CRD_PERFSECOP_WRONG_LENGTH	ILRET_SCR_ERROR+131
// Ошибка ожидания последней связанной команды выполнения криптографической операции
#define ILRET_CRD_PERFSECOP_BINDING_CMD_MISSED	ILRET_SCR_ERROR+132
// Команда криптографической операции не поддерживает связывание
#define ILRET_CRD_PERFSECOP_BINDING_NOT_SUPPORTED	ILRET_SCR_ERROR+133
// Неверный сертификат открытого ключа команды выполнения криптографической операции
#define ILRET_CRD_PERFSECOP_WRONG_CERT	ILRET_SCR_ERROR+134
// Неверная структура данных команды выполнения криптографической операции
#define ILRET_CRD_PERFSECOP_WRONG_DATA_STRUCT	ILRET_SCR_ERROR+135
// Неправильные значения параметров P1/P2 команды выполнения криптографической операции
#define ILRET_CRD_PERFSECOP_WRONG_PARAMETERS	ILRET_SCR_ERROR+136

// Ошибка чтения данных из бинарного файла
#define ILRET_CRD_READBIN_ERROR				ILRET_SCR_ERROR+140
// Неправильная длина входных данных команды чтения бинарного файла
#define ILRET_CRD_READBIN_WRONG_LENGTH		ILRET_SCR_ERROR+141
// Попытка чтения данных не из бинарного файла
#define ILRET_CRD_READBIN_WRONG_FILE_TYPE	ILRET_SCR_ERROR+142
// Не выполнены условия доступа при чтении данных из бинарного файла
#define ILRET_CRD_READBIN_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+143
// Попытка чтения бинарных данных из неселектированного файла
#define ILRET_CRD_READBIN_EF_NOT_SELECTED	ILRET_SCR_ERROR+144
// Неправильные значения параметров P1/P2 команды чтения данных из бинарного файла
#define ILRET_CRD_READBIN_WRONG_PARAMETERS	ILRET_SCR_ERROR+145
// Указанное при чтении данных из бинарного файла смещение превышает размер файла
#define ILRET_CRD_READBIN_WRONG_OFFSET		ILRET_SCR_ERROR+146

// Ошибка записи данных в бинарный файл
#define ILRET_CRD_UPDBIN_ERROR				ILRET_SCR_ERROR+150
// Неправильная длина входных данных команды записи в бинарный файл
#define ILRET_CRD_UPDBIN_WRONG_LENGTH		ILRET_SCR_ERROR+151
// Не выполнены условия доступа при записи данных в бинарный файл
#define ILRET_CRD_UPDBIN_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+152
// Попытка записи бинарных данных в неселектированный файл
#define ILRET_CRD_UPDBIN_WRONG_FILE			ILRET_SCR_ERROR+153
// Неправильные значения параметров P1/P2 команды записи данных в бинарный файл
#define ILRET_CRD_UPDBIN_WRONG_PARAMETERS	ILRET_SCR_ERROR+154
// Неверное смещение при попытке записи данных в бинарный файл
#define ILRET_CRD_UPDBIN_WRONG_OFFSET		ILRET_SCR_ERROR+155

// Ошибка чтения элемента данных из TLV-файла
#define ILRET_CRD_GETDATA_ERROR				ILRET_SCR_ERROR+160
// Неправильная длина входных/выходных данных при чтении из TLV-файла
#define ILRET_CRD_GETDATA_WRONG_LENGTH		ILRET_SCR_ERROR+161
// Неправильные значения параметров P1/P2 команды чтения элемента данных из TLV-файла
#define ILRET_CRD_GETDATA_WRONG_PARAMETERS	ILRET_SCR_ERROR+162
// Не найден элемент данных с указанным тегом при чтении из TLV-файла
#define ILRET_CRD_GETDATA_TAG_NOT_FOUND		ILRET_SCR_ERROR+163
// Не выполнены условия доступа при чтении элемента данных из TLV-файла
#define ILRET_CRD_GETDATA_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+164

// Ошибка установки элемента данных в TLV-файл
#define ILRET_CRD_PUTDATA_ERROR				ILRET_SCR_ERROR+170
// Неправильная длина входных данных команды установки элемента данных в TLV-файл
#define ILRET_CRD_PUTDATA_WRONG_LENGTH		ILRET_SCR_ERROR+171
// Не выполнены условия доступа при установке элемента данных в TLV-файл
#define ILRET_CRD_PUTDATA_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+172
// Неправильные значения параметров P1/P2 команды установки элемента данных в TLV-файл
#define ILRET_CRD_PUTDATA_WRONG_PARAMETERS	ILRET_SCR_ERROR+173
// Неизвестный тэг при установке элемента данных в TLV-файл
#define ILRET_CRD_PUTDATA_TAG_NOT_FOUND		ILRET_SCR_ERROR+174

// Ошибка чтения данных из файла записей линейной структуры
#define ILRET_CRD_READREC_ERROR	            ILRET_SCR_ERROR+180
// Файл записей линейной структуры блокирован
#define ILRET_CRD_READREC_FILE_BLOCKED  	ILRET_SCR_ERROR+181
// Непрвильная длина выходных данных файла записей линейной структуры
#define ILRET_CRD_READREC_WRONG_LENGTH		ILRET_SCR_ERROR+182
// Файл не является файлом записей линейной структуры
#define ILRET_CRD_READREC_WRONG_FILE_TYPE	ILRET_SCR_ERROR+183
// Не выполнены условия доступа к файлу записей линейной структуры
#define ILRET_CRD_READREC_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+184
// Неправильные значения параметров команды чтения файла записей линейной структуры
#define ILRET_CRD_READREC_WRONG_PARAMETERS	ILRET_SCR_ERROR+185
// Не найдена запись при чтении файла записей линейной структуры
#define ILRET_CRD_READREC_RECORD_NOT_FOUND	ILRET_SCR_ERROR+186
// Неправильные значения параметров P1/P2 при чтении файла записей линейной структуры
#define ILRET_CRD_READREC_WRONG_PARAMETERS_P1P2	ILRET_SCR_ERROR+187

// Ошибка обновления данных файла записей линейной структуры
#define ILRET_CRD_UPDREC_ERROR	            ILRET_SCR_ERROR+190
// Файл записей линейной структуры блокирован
#define ILRET_CRD_UPDREC_FILE_BLOCKED  	    ILRET_SCR_ERROR+191
// Непрвильная длина данных при обновлении файла записей линейной структуры
#define ILRET_CRD_UPDREC_WRONG_LENGTH		ILRET_SCR_ERROR+192
// Файл не является файлом записей линейной структуры
#define ILRET_CRD_UPDREC_WRONG_FILE_TYPE	ILRET_SCR_ERROR+193
// Не выполнены условия доступа к файлу записей линейной структуры
#define ILRET_CRD_UPDREC_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+194
// Неправильные значения параметров команды обновления файла записей линейной структуры
#define ILRET_CRD_UPDREC_WRONG_PARAMETERS	ILRET_SCR_ERROR+195
// Не найдена запись при обновлении файла записей линейной структуры
#define ILRET_CRD_UPDREC_RECORD_NOT_FOUND	ILRET_SCR_ERROR+196
// Неправильные значения параметров P1/P2 при обновлении файла записей линейной структуры
#define ILRET_CRD_UPDREC_WRONG_PARAMETERS_P1P2	ILRET_SCR_ERROR+197

// Ошибка добавления данных в файл записей линейной структуры
#define ILRET_CRD_APPREC_ERROR	            ILRET_SCR_ERROR+200
// Файл записей линейной структуры блокирован
#define ILRET_CRD_APPREC_FILE_BLOCKED  	    ILRET_SCR_ERROR+201
// Непрвильная длина данных при добавлении в файла записей линейной структуры
#define ILRET_CRD_APPREC_WRONG_LENGTH		ILRET_SCR_ERROR+202
// Файл не является файлом записей линейной структуры
#define ILRET_CRD_APPREC_WRONG_FILE_TYPE	ILRET_SCR_ERROR+203
// Не выполнены условия доступа к файлу записей линейной структуры
#define ILRET_CRD_APPREC_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+204
// Неправильные значения параметров команды добавления в файл записей линейной структуры
#define ILRET_CRD_APPREC_WRONG_PARAMETERS	ILRET_SCR_ERROR+205
// Недостаточно места в файле записей линейной структуры при добавлении
#define ILRET_CRD_APPREC_NOT_ENOUGH_MEMORY	ILRET_SCR_ERROR+206
// Неправильные значения параметров P1/P2 при добавлении в файла записей линейной структуры
#define ILRET_CRD_APPREC_WRONG_PARAMETERS_P1P2	ILRET_SCR_ERROR+207


// Ошибка получения параметра из внешнего файла настроек
#define ILRET_PARAM_ERROR					700
// Не найден запрашиваемый параметр внешнего файла настроек
#define ILRET_PARAM_NOT_FOUND				ILRET_PARAM_ERROR+1
// Неверный формат параметра внешнего файла настроек
#define ILRET_PARAM_WRONG_FORMAT			ILRET_PARAM_ERROR+2
// Неверная длина считанного из внешнего файла настроек параметра
#define ILRET_PARAM_WRONG_LENGTH			ILRET_PARAM_ERROR+3
// Неизвестный формат параметра внешнего файла настроек
#define ILRET_PARAM_FORMAT_UNKNOWN			ILRET_PARAM_ERROR+4
// Не найден описатель параметра внешнего файла настроек
#define ILRET_PARAM_DESCR_NOT_FOUND		    ILRET_PARAM_ERROR+5
// Ошибка записи секции внешнего описателя сектора
#define ILRET_PARAM_WRITE_SECTOR_EX_ERROR	ILRET_PARAM_ERROR+6
// Неверный формат секции внешнего описателя сектора
#define ILRET_PARAM_SECTOR_EX_WRONG_FORMAT	ILRET_PARAM_ERROR+7	
// Не найден сертификат во внешнем файле настроек
#define ILRET_PARAM_CERTIFICATE_NOT_FOUND	ILRET_PARAM_ERROR+8

// Ошибка протоколирования
#define ILRET_PROT_ERROR					710
// Ошибка открытия файла протоколирования
#define ILRET_PROT_LOGFILE_OPEN_ERROR		ILRET_PROT_ERROR+1
// Ошибка записи в файл протоколирования
#define ILRET_PROT_LOGEILE_WRITE_ERROR		ILRET_PROT_ERROR+2

// Ошибка входных данных вызываемой функции
#define ILRET_DATA_ERROR					800
// Не найден элемент данных с указанным тегом
#define ILRET_DATA_TAG_NOT_FOUND			ILRET_DATA_ERROR+1
// Неверный формат элемента данных
#define ILRET_DATA_TAG_WRONG_FORMAT			ILRET_DATA_ERROR+2
// Неверная длина элемента данных
#define ILRET_DATA_TAG_WRONG_LENGTH			ILRET_DATA_ERROR+3
// Неверный формат считанного с карты элемента TLV-данных
#define ILRET_DATA_CARD_FORMAT_ERROR		ILRET_DATA_ERROR+4

// Терминал не поддерживает работу с картой данной версии
#define ILRET_APP_VER_NOT_SUPPORTED			ILRET_DATA_ERROR+5
// Терминал не поддерживает криптоалгоритмы данной карты
#define ILRET_NO_CRYPTOALG_SUPPORTED		ILRET_DATA_ERROR+6
// Срок начала действия карты не наступил
#define ILRET_APP_NOT_ACTIVE_YET		    ILRET_DATA_ERROR+7
// Срок действия карты истёк
#define ILRET_APP_EXPIRED		            ILRET_DATA_ERROR+8
// Неверный формат Hex-строки
#define ILRET_INVALID_HEX_STRING_FORMAT		ILRET_DATA_ERROR+9
// Недопустимый ответ APDU-команды пакета
#define ILRET_APDU_RES_NOT_ALLOWED			ILRET_DATA_ERROR+10
// Превышено количество допустимых ответов APDU-команды пакета
#define ILRET_APDU_ALLOWED_RES_IS_OVER		ILRET_DATA_ERROR+11
// Приложение находится в несогласованном состоянии
#define ILRET_APP_INCONSISTENT_STATE		ILRET_DATA_ERROR+12
// Приложение находится в неизвестном состоянии
#define ILRET_APP_UNKNOWN_STATE				ILRET_DATA_ERROR+13
// Недостаточный размер буфера
#define ILRET_BUFFER_TOO_SMALL				ILRET_DATA_ERROR+14

// Ошибка проверки формата сертификата
#define ILRET_CERT_ERROR					ILRET_DATA_ERROR+20
// Отсутствует обязательный элемент данных сертификата
#define ILRET_CERT_MISSING_ELEMENT			ILRET_CERT_ERROR+1
// Неверная длина данных элемента сертификата
#define ILRET_CERT_WRONG_LENGTH				ILRET_CERT_ERROR+2
// Срок начала действия сертификата не наступил
#define ILRET_CERT_NOT_ACTIVE_YET		    ILRET_CERT_ERROR+3
// Срок действия сертификата истёк
#define ILRET_CERT_EXPIRED		            ILRET_CERT_ERROR+4
// Неверная версия сертификата
#define ILRET_CERT_WRONG_VERSION			ILRET_CERT_ERROR+5
// Неверная экспонента открытого ключа в сертификате
#define ILRET_CERT_WRONG_RSA_EXP			ILRET_CERT_ERROR+6
// Неверный тип сертификата открытого ключа
#define ILRET_CERT_INVALID_TYPE				ILRET_CERT_ERROR+7
// Не совпадают сведения о терминале в сертификатах GOST и RSA
#define ILRET_CERT_TERMINFO_NOT_MATCH		ILRET_CERT_ERROR+8

// Ошибка криптопровайдера
#define ILRET_CRYPTO_ERROR					900
// Ошибка формата RSA
#define ILRET_CRYPTO_RSA_FORMAT				ILRET_CRYPTO_ERROR+1
// Ошибка при подготовке криптосессии
#define ILRET_CRYPTO_CRYPTO_PREPARE_SESSION	ILRET_CRYPTO_ERROR+2
// Неверное значение ключа MAC криптосессии
#define ILRET_CRYPTO_WRONG_SM_MAC			ILRET_CRYPTO_ERROR+3
// Неверная длина криптографических данных
#define ILRET_CRYPTO_WRONG_DATA_LENGTH		ILRET_CRYPTO_ERROR+4
// Неверный формат криптографических данных
#define ILRET_CRYPTO_WRONG_DATA_FORMAT		ILRET_CRYPTO_ERROR+5
// Неверный сертификат
#define ILRET_CRYPTO_WRONG_CERT				ILRET_CRYPTO_ERROR+6
// Неверное значение ключа MAC при проверке запроса на аутентификацию ИД-приложения
#define ILRET_CRYPTO_WRONG_MAC			    ILRET_CRYPTO_ERROR+7
// Неверное значение ключа MAC при проверке установленной защищённой сессии с эмитентом
#define ILRET_CRYPTO_WRONG_CHK_ISS_SESS_MAC ILRET_CRYPTO_ERROR+8
// Ошибка генерации ключевой пары
#define ILRET_CRYPTO_ERROR_GENKEYPAIR		ILRET_CRYPTO_ERROR+9
// Ошибка проверки заполнителя значения параметра
#define ILRET_CRYPTO_ERROR_FILLPARAM		ILRET_CRYPTO_ERROR+10
// Ошибки формирования подписи
#define ILRET_CRYPTO_ERROR_SIGN         	ILRET_CRYPTO_ERROR+11
// Ошибка проверки подписи
#define ILRET_CRYPTO_ERROR_CHECKSIGN		ILRET_CRYPTO_ERROR+12
// Несовпадение сессионных ключей
#define ILRET_CRYPTO_ERROR_KEYMATCHING		ILRET_CRYPTO_ERROR+13

// Ошибка OpLib
#define ILRET_OPLIB_ERROR							4000
// Отказ пользователя от выполнения операции
#define ILRET_OPLIB_ESCAPE							ILRET_OPLIB_ERROR + 0
// Неправильный аргумент функции
#define ILRET_OPLIB_INVALID_ARGUMENT				ILRET_OPLIB_ERROR + 1 
// Неверная длина метаинформации по услуге
#define ILRET_OPLIB_METAINFO_WRONG_LEN				ILRET_OPLIB_ERROR + 2 
// Превышена длина внешних данных услуги 
#define ILRET_OPLIB_EXTRA_DATA_IS_OVER				ILRET_OPLIB_ERROR + 3 
// Не найден описатель сектора в списке секторов 
#define ILRET_OPLIB_SECTOR_NOT_FOUND_IN_LIST		ILRET_OPLIB_ERROR + 4 
// Не найден описатель элемента данных
#define ILRET_OPLIB_DATA_DESCR_NOT_FOUND			ILRET_OPLIB_ERROR + 5
// Не найден описатель блока данных
#define ILRET_OPLIB_BLOCK_DESCR_NOT_FOUND			ILRET_OPLIB_ERROR + 6
// Не найден описатель сектора данных
#define ILRET_OPLIB_SECTOR_DESCR_NOT_FOUND			ILRET_OPLIB_ERROR + 7
// Не найден блок данных
#define ILRET_OPLIB_BLOCK_NOT_FOUND					ILRET_OPLIB_ERROR + 8
// Неверный тип данных
#define ILRET_OPLIB_ILLEGAL_DATA_TYPE				ILRET_OPLIB_ERROR + 9
// Ошибка формата данных TLV-элемента
#define ILRET_OPLIB_CORRUPTED_TLV_DATA				ILRET_OPLIB_ERROR + 10
// Неверный пароль подтверждения
#define ILRET_OPLIB_INVALID_CONFIRM_PIN				ILRET_OPLIB_ERROR + 11
#define PIN_TRIES_MAX								16
// Неверный пароль! Осталась попыток: 1
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_1		(ILRET_OPLIB_ERROR + 12)
// Неверный пароль! Осталась попыток: 2
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_2		ILRET_OPLIB_ERROR + 13
// Неверный пароль! Осталась попыток: 3
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_3		ILRET_OPLIB_ERROR + 14
// Неверный пароль! Осталась попыток: 4
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_4		ILRET_OPLIB_ERROR + 15
// Неверный пароль! Осталась попыток: 5
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_5		ILRET_OPLIB_ERROR + 16
// Неверный пароль! Осталась попыток: 6
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_6		ILRET_OPLIB_ERROR + 17
// Неверный пароль! Осталась попыток: 7
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_7		ILRET_OPLIB_ERROR + 18
// Неверный пароль! Осталась попыток: 8
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_8		ILRET_OPLIB_ERROR + 19
// Неверный пароль! Осталась попыток: 9
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_9		ILRET_OPLIB_ERROR + 20
// Неверный пароль! Осталась попыток: 10
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_10		ILRET_OPLIB_ERROR + 21
// Неверный пароль! Осталась попыток: 11
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_11		ILRET_OPLIB_ERROR + 22
// Неверный пароль! Осталась попыток: 12
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_12		ILRET_OPLIB_ERROR + 23
// Неверный пароль! Осталась попыток: 13
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_13		ILRET_OPLIB_ERROR + 24
// Неверный пароль! Осталась попыток: 14
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_14		ILRET_OPLIB_ERROR + 25
// Неверный пароль! Осталась попыток: 15
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_15		ILRET_OPLIB_ERROR + 26
// Неверный пароль! Осталась попыток: 16
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_16		ILRET_OPLIB_ERROR + 27
// Отсутствует пакет APDU-команд
#define ILRET_OPLIB_APDU_PACKET_ABSENT				ILRET_OPLIB_ERROR + 28
// Ошибка инициализации системы протоколирования
#define ILRET_OPLIB_INIT_PROTOCOL_ERROR				ILRET_OPLIB_ERROR + 29
// Отсутствуют данные для записи на карту
#define ILRET_OPLIB_DATA_FOR_WRITE_NOT_FOUND		ILRET_OPLIB_ERROR + 30
// Неверный тип данных
#define ILRET_OPLIB_INVALID_WRITE_DATA_TYPE			ILRET_OPLIB_ERROR + 31
// Неверный индекс редактируемых данных
#define ILRET_OPLIB_INVALID_EDIT_DATA_INDEX			ILRET_OPLIB_ERROR + 32
// На карте отсутствуют сектора данных
#define ILRET_OPLIB_SECTOR_LIST_IS_EMPTY			ILRET_OPLIB_ERROR + 33
// Ошибка проверки криптограммы аутентификации
#define ILRET_OPLIB_APP_AUTH_WRONG_CRYPTOGRAM		ILRET_OPLIB_ERROR + 34
// Неверный тип файла
#define ILRET_OPLIB_INVALID_FILE_TYPE				ILRET_OPLIB_ERROR + 35
// Длина данных для записи на карту превышает размер буфера
#define ILRET_OPLIB_WRITE_DATA_LEN_IS_OVER			ILRET_OPLIB_ERROR + 36
// Ошибка блокировки карты
#define ILRET_OPLIB_CARD_LOCK_ERROR					ILRET_OPLIB_ERROR + 37
// Карта заблокирована
#define ILRET_OPLIB_CARD_LOCKED						ILRET_OPLIB_ERROR + 38
// Карта изъята
#define ILRET_OPLIB_CARD_CAPTURED					ILRET_OPLIB_ERROR + 39	
// Превышен размер временного буфера контекста
#define ILRET_OPLIB_CTX_TMP_BUF_IS_OVER				ILRET_OPLIB_ERROR + 40	
// Автомат не предназначен для выполнения данной операции
#define ILRET_OPLIB_INVALID_OPERATION				ILRET_OPLIB_ERROR + 41
// Превышен размер буфера внешних описателей секторов
#define ILRET_OPLIB_SECTORS_EX_DESCR_IS_OVER		ILRET_OPLIB_ERROR + 42
// Превышен размер буфера внешних описателей блоков
#define ILRET_OPLIB_BLOCKS_EX_DESCR_IS_OVER			ILRET_OPLIB_ERROR + 43	
// Превышен размер буфера внешних описателей элементов данных
#define ILRET_OPLIB_DATAS_EX_DESCR_IS_OVER			ILRET_OPLIB_ERROR + 44
// Неверное значение контрольной суммы внешнего буфера
#define ILRET_OPLIB_INVALID_BUF_CRC_VALUE			ILRET_OPLIB_ERROR + 45
// Не установлен указатель на внешний буфер
#define ILRET_OPLIB_EXTERNAL_BUF_NOT_SET			ILRET_OPLIB_ERROR + 46	
// Превышен размер внешнего буфера для бинарных TLV-данных
#define ILRET_OPLIB_BINTLV_BUF_IS_OVER				ILRET_OPLIB_ERROR + 47
// Превышен размер внешнего буфера для считываемых с карты данных 
#define ILRET_OPLIB_CARDDATA_BUF_IS_OVER			ILRET_OPLIB_ERROR + 48	
// Превышен размер внешнего буфера для описателя секторов 
#define ILRET_OPLIB_SECTORSDESCR_BUF_IS_OVER		ILRET_OPLIB_ERROR + 49
// Превышен размер внешнего буфера для описателя данных блока
#define ILRET_OPLIB_BLOCKDATADESCR_BUF_IS_OVER		ILRET_OPLIB_ERROR + 50	
// Превышен размер внешнего буфера запроса на аутентификацию ИД-приложения 
#define ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER			ILRET_OPLIB_ERROR + 51	
// Превышен размер внешнего буфера пакета APDU-команд
#define ILRET_OPLIB_APDUPACKET_BUF_IS_OVER			ILRET_OPLIB_ERROR + 52	
// Шифрованые/расшифрованые данные не идентичны
#define ILRET_OPLIB_ENCRYPT_DECRYPT_ERROR			ILRET_OPLIB_ERROR + 53
// Не установлена сессия с Поставщиком Услуги
#define ILRET_OPLIB_PROVIDER_SESSION_NOT_SET		ILRET_OPLIB_ERROR + 54
// Неверная длина хэш-значения запроса на оказание услуги
#define ILRET_OPLIB_ILLEGAL_HASH_LEN				ILRET_OPLIB_ERROR + 55

// Ошибка подсистемы автоматического тестирования
#define ILRET_TEST_ERROR							5000
// Не найден параметр в файле тестового скрипта
#define ILRET_TEST_PARAM_NOT_FOUND					ILRET_TEST_ERROR + 1
// В тестовом скрипте не найден параметр наименование ридера
#define ILRET_TEST_PARAM_READER_NAME_NOT_FOUND		ILRET_TEST_ERROR + 2
// В тестовом скрипте не найден параметр последовательности исполняемых тестов
#define ILRET_TEST_PARAM_TESTS_NOT_FOUND			ILRET_TEST_ERROR + 3
// В тестовом скрипте не найден параметр идентификатора операции
#define ILRET_TEST_PARAM_OPER_ID_NOT_FOUND			ILRET_TEST_ERROR + 4
// Неверный параметр идентификатора операции в тестовом скрипте
#define ILRET_TEST_PARAM_ILLEGAL_OPER_ID			ILRET_TEST_ERROR + 5
// Неверный параметр идентификатора операции в тестовом скрипте
#define ILRET_TEST_PARAM_ILLEGAL_FORMAT				ILRET_TEST_ERROR + 6

#endif //_IL_ERROR_H_
