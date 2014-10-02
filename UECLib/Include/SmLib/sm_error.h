#ifndef _SM_ERROR_H_
#define _SM_ERROR_H_


#define ILRET_OK							    0

// Ошибка Модуля Безопасности
#define ILRET_SM_ERROR					        1000

// МБ: Ошибка селектирования приложения/файла
#define ILRET_SM_SELECT_ERROR		  		    ILRET_SM_ERROR+0
// МБ: Неправильная длина входных данных команды селектирования
#define ILRET_SM_SELECT_WRONG_LENGTH  		    ILRET_SM_ERROR+1
// МБ: Неправильные значения параметров P1/P2 команды селектирования
#define ILRET_SM_SELECT_WRONG_PARAMETERS	    ILRET_SM_ERROR+2
// МБ: Селектируемый объект не найден
#define ILRET_SM_SELECT_FILE_NOT_FOUND		    ILRET_SM_ERROR+3

// МБ: Ошибка аутентификации внешнего субъекта
#define ILRET_SM_MUTAUTH_ERROR		  		    ILRET_SM_ERROR+10
// МБ: Неправильная длина входных данных команды аутентификации внешнего субъекта
#define ILRET_SM_MUTAUTH_WRONG_LENGTH 		    ILRET_SM_ERROR+11
// МБ: Неправильные значения параметров P1/P2 команды аутентификации внешнего субъекта
#define ILRET_SM_MUTAUTH_WRONG_PARAMETERS	    ILRET_SM_ERROR+12
// МБ: Неверное значение криптограммы аутентификации внешнего субъекта
#define ILRET_SM_MUTAUTH_WRONG_CRYPTO		    ILRET_SM_ERROR+13
// МБ: Указанный в команде аутентификации внешнего субъекта ключ не найден
#define ILRET_SM_MUTAUTH_KEY_NOT_FOUND		    ILRET_SM_ERROR+14
// МБ: Приложение не подготовлено к получению команды аутентификации внешнего субъекта
#define ILRET_SM_MUTAUTH_CONDITIONS		        ILRET_SM_ERROR+15
// МБ: Модуль безопасности не активирован
#define ILRET_SM_NOT_ACTIVATED					ILRET_SM_ERROR+16

// МБ: Ошибка получения случайного числа карты
#define ILRET_SM_GETCHAL_ERROR		  		    ILRET_SM_ERROR+20
// МБ: Неправильная длина входных данных команды получения случайного числа карты
#define ILRET_SM_GETCHAL_WRONG_LENGTH 		    ILRET_SM_ERROR+21
// МБ: Неправильные значения параметров P1/P2 команды получения случайного числа карты
#define ILRET_SM_GETCHAL_WRONG_PARAMETERS	    ILRET_SM_ERROR+22

// МБ: Ошибка изменения/установки значения критических данных
#define ILRET_SM_CHDATA_ERROR		  		    ILRET_SM_ERROR+30
// МБ: Неправильная длина входных данных команды изменения/установки значения критических данных
#define ILRET_SM_CHDATA_WRONG_LENGTH 		    ILRET_SM_ERROR+31
// МБ: Не выполнены условия безопасности для команды изменения/установки значения критических данных
#define ILRET_SM_CHDATA_WRONG_CRYPTO		    ILRET_SM_ERROR+32
// МБ: Неправильные значения параметров P1/P2 команды изменения/установки значения критических данных
#define ILRET_SM_CHDATA_WRONG_PARAMETERS	    ILRET_SM_ERROR+33
// МБ: Указанный в команде изменения/установки значения критических данных ключ не найден
#define ILRET_SM_CHDATA_KEY_NOT_FOUND		    ILRET_SM_ERROR+34
// МБ: Ключ блокирован
#define ILRET_SM_CHDATA_KEY_BLOCKED	            ILRET_SM_ERROR+35
// МБ: Операция не совместима с текущими свойствами ключа
#define ILRET_SM_CHDATA_OP_NOT_COMPATIBLE_KEY_STATE ILRET_SM_ERROR+36
// МБ: Длина данных команды не совместима с режимом команды
#define ILRET_SM_CHDATA_LENGTH_NOT_COMPATIBLE_MODE ILRET_SM_ERROR+37
// МБ: Неверный формат данных команды
#define ILRET_SM_CHDATA_WRONG_FORMAT            ILRET_SM_ERROR+38
// МБ: Длина устанавливаемого пароля меньше минимально допустимого значения
#define ILRET_SM_CHDATA_WRONG_PIN_LENGTH        ILRET_SM_ERROR+39

// МБ: Ошибка верификации владельца
#define ILRET_SM_VERIFY_ERROR		  		    ILRET_SM_ERROR+40
// МБ: Неправильная длина выходных данных верификации владельца
#define ILRET_SM_VERIFY_WRONG_LENGTH  		    ILRET_SM_ERROR+41
// МБ: Неправильные значения параметров P1/P2 команды верификации владельца
#define ILRET_SM_VERIFY_WRONG_PARAMETERS	    ILRET_SM_ERROR+42
// МБ: Пароль заблокирован
#define ILRET_SM_VERIFY_PASSWORD_BLOCKED	    ILRET_SM_ERROR+43
// МБ: Указанный в команде верификации владельца ключ не найден
#define ILRET_SM_VERIFY_PASSWORD_NOT_FOUND	    ILRET_SM_ERROR+44
// МБ: Неверное значение пароля
#define ILRET_SM_VERIFY_WRONG_PASSWORD_PRESENTED	ILRET_SM_ERROR+45
// МБ: Не выполнены условия безопасности при верификации владельца
#define ILRET_SM_VERIFY_SECURITY_STATUS_NOT_SATISFIED	ILRET_SM_ERROR+46

// МБ: Ошибка выполнения криптографической операции
#define ILRET_SM_PERFSECOP_ERROR			    ILRET_SM_ERROR+50
// МБ: Неправильная длина входных данных команды выполнения криптографической операции
#define ILRET_SM_PERFSECOP_WRONG_LENGTH	        ILRET_SM_ERROR+51
// МБ: Ошибка ожидания последней связанной команды выполнения криптографической операции
#define ILRET_SM_PERFSECOP_BINDING_CMD_MISSED	ILRET_SM_ERROR+52
// МБ: Команда криптографической операции не поддерживает связывание
#define ILRET_SM_PERFSECOP_BINDING_NOT_SUPPORTED	ILRET_SM_ERROR+53
// МБ: Неверный сертификат открытого ключа команды выполнения криптографической операции
#define ILRET_SM_PERFSECOP_WRONG_CERT	        ILRET_SM_ERROR+54
// МБ: Неверная структура данных команды выполнения криптографической операции
#define ILRET_SM_PERFSECOP_WRONG_DATA_STRUCT	ILRET_SM_ERROR+55
// МБ: Неправильные значения параметров P1/P2 команды выполнения криптографической операции
#define ILRET_SM_PERFSECOP_WRONG_PARAMETERS	    ILRET_SM_ERROR+56
// МБ: Ключ электронной подписи отсутствует
#define ILRET_SM_PERFSECOP_SIGN_KEY_ABSENT	    ILRET_SM_ERROR+57

// МБ: Ошибка чтения данных из бинарного файла
#define ILRET_SM_READBIN_ERROR				    ILRET_SM_ERROR+60
// МБ: Неправильная длина входных данных команды чтения бинарного файла
#define ILRET_SM_READBIN_WRONG_LENGTH		    ILRET_SM_ERROR+61
// МБ: Попытка чтения данных не из бинарного файла
#define ILRET_SM_READBIN_WRONG_FILE_TYPE	    ILRET_SM_ERROR+62
// МБ: Не выполнены условия доступа при чтении данных из бинарного файла
#define ILRET_SM_READBIN_WRONG_SEC_CONDITIONS	ILRET_SM_ERROR+63
// МБ: Попытка чтения бинарных данных из неселектированного файла
#define ILRET_SM_READBIN_EF_NOT_SELECTED	    ILRET_SM_ERROR+64
// МБ: Неправильные значения параметров P1/P2 команды чтения данных из бинарного файла
#define ILRET_SM_READBIN_WRONG_PARAMETERS	    ILRET_SM_ERROR+65
// МБ: Указанное при чтении данных из бинарного файла смещение превышает размер файла
#define ILRET_SM_READBIN_WRONG_OFFSET		    ILRET_SM_ERROR+66

// МБ: Ошибка записи данных в бинарный файл
#define ILRET_SM_UPDBIN_ERROR				    ILRET_SM_ERROR+70
// МБ: Неправильная длина входных данных команды записи в бинарный файл
#define ILRET_SM_UPDBIN_WRONG_LENGTH		    ILRET_SM_ERROR+71
// МБ: Не выполнены условия доступа при записи данных в бинарный файл
#define ILRET_SM_UPDBIN_WRONG_SEC_CONDITIONS	ILRET_SM_ERROR+72
// МБ: Попытка записи бинарных данных в неселектированный файл
#define ILRET_SM_UPDBIN_WRONG_FILE			    ILRET_SM_ERROR+73
// МБ: Попытка записи не в бинарный файл
#define ILRET_SM_UPDBIN_WRONG_FILE_TYPE			ILRET_SM_ERROR+74
// МБ: Неправильные значения параметров P1/P2 команды записи данных в бинарный файл
#define ILRET_SM_UPDBIN_WRONG_PARAMETERS	    ILRET_SM_ERROR+75

// МБ: Ошибка чтения элемента данных из TLV-файла
#define ILRET_SM_GETDATA_ERROR				    ILRET_SM_ERROR+80
// МБ: Неправильная длина входных/выходных данных при чтении из TLV-файла
#define ILRET_SM_GETDATA_WRONG_LENGTH		    ILRET_SM_ERROR+81
// МБ: Неправильные значения параметров P1/P2 команды чтения элемента данных из TLV-файла
#define ILRET_SM_GETDATA_WRONG_PARAMETERS	    ILRET_SM_ERROR+82
// МБ: Не найден элемент данных с указанным тегом при чтении из TLV-файла
#define ILRET_SM_GETDATA_TAG_NOT_FOUND		    ILRET_SM_ERROR+83
// МБ: Не выполнены условия доступа при чтении элемента данных из TLV-файла
#define ILRET_SM_GETDATA_WRONG_SEC_CONDITIONS	ILRET_SM_ERROR+84

// МБ: Ошибка установки элемента данных в TLV-файл
#define ILRET_SM_PUTDATA_ERROR				    ILRET_SM_ERROR+90
// МБ: Неправильная длина входных данных команды установки элемента данных в TLV-файл
#define ILRET_SM_PUTDATA_WRONG_LENGTH		    ILRET_SM_ERROR+91
// МБ: Не выполнены условия доступа при установке элемента данных в TLV-файл
#define ILRET_SM_PUTDATA_WRONG_SEC_CONDITIONS	ILRET_SM_ERROR+92
// МБ: Неправильные значения параметров P1/P2 команды установки элемента данных в TLV-файл
#define ILRET_SM_PUTDATA_WRONG_PARAMETERS	    ILRET_SM_ERROR+93
// МБ: Неизвестный тэг при установке элемента данных в TLV-файл
#define ILRET_SM_PUTDATA_TAG_NOT_FOUND		    ILRET_SM_ERROR+94

// МБ: Ощибка выполнения команды MSE
#define ILRET_SM_MSE_ERROR	                    ILRET_SM_ERROR+100
// МБ: Неправильная длина входных данных команды MSE
#define ILRET_SM_MSE_WRONG_LENGTH  	            ILRET_SM_ERROR+101
// МБ: Установка контекста безопасности команды MSE не может быть осуществлена
#define ILRET_SM_MSE_CANT_SET_CONTEXT	        ILRET_SM_ERROR+102
// МБ: Неправильные значения параметров P1/P2 команды MSE
#define ILRET_SM_MSE_WRONG_PARAMETERS	        ILRET_SM_ERROR+103

// МБ: Ошибка выполнения команды начала аутентификации
#define ILRET_SM_AUTH_BEGIN_ERROR	            ILRET_SM_ERROR+110
// МБ: Некорректное значение данных команды начала аутентификации
#define ILRET_SM_AUTH_BEGIN_WRONG_DATA	        ILRET_SM_ERROR+111
// МБ: Ошибка указания данных команды начала аутентификации
#define ILRET_SM_AUTH_BEGIN_REF_DATA_ERROR	    ILRET_SM_ERROR+112

// МБ: Ошибка выполнения команды завершения аутентификации
#define ILRET_SM_AUTH_COMPLETE_ERROR    	    ILRET_SM_ERROR+120
// МБ: Некорректное значение данных команды завершения аутентификации
#define ILRET_SM_AUTH_COMPLETE_WRONG_DATA	    ILRET_SM_ERROR+121
// МБ: Ошибка указания данных команды завершения аутентификации
#define ILRET_SM_AUTH_COMPLETE_REF_DATA_ERROR	ILRET_SM_ERROR+122

// МБ: Ошибка выполнения команды установки сессии с Поставщиком Услуги
#define ILRET_SM_SP_SESS_ERROR          	    ILRET_SM_ERROR+130
// МБ: Неправильная длина входных данных команды установки сессии с Поставщиком Услуги
#define ILRET_SM_SP_SESS_WRONG_LENGTH	        ILRET_SM_ERROR+131
// МБ: Приложение не подготовлено для выполнения команды установки сессии с Поставщиком Услуги
#define ILRET_SM_SP_SESS_CONDITIONS_NOT_SATISFIED   ILRET_SM_ERROR+132
// МБ: Неправильные значения параметров P1/P2 команды установки сессии с Поставщиком Услуги
#define ILRET_SM_SP_SESS_WRONG_PARAMETERS       ILRET_SM_ERROR+133

// МБ: Ошибка активации
#define ILRET_SM_SE_ACTIVATION_ERROR    	    ILRET_SM_ERROR+140
// МБ: Некорректное значение данных команды активации
#define ILRET_SM_SE_ACTIVATION_WRONG_DATA	    ILRET_SM_ERROR+141

// МБ: Модуль безапасности уже в активированном состоянии
#define ILRET_SM_SE_ALREADY_ACTIVATED			ILRET_SM_ERROR+150
// МБ: Модуль безапасности уже в неактивированном состоянии
#define ILRET_SM_SE_ALREADY_DEACTIVATED			ILRET_SM_ERROR+151
// МБ: Неизвестный режим активации/деактивации карты
#define ILRET_SM_SE_ILLEGAL_ACTIVATION_MODE		ILRET_SM_ERROR+152

// МБ: Неверные данные
#define ILRET_SM_WRONG_DATA						ILRET_SM_ERROR+160

#endif