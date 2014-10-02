#pragma once

// Прикладные ошибки

//Ошибка карт-ридера
#define ILRET_CARD_READER_ERROR        1
//Ошибка карты
#define ILRET_CARD_ERROR               2
//Ошибка проверки формата сертификата
#define ILRET_CERTIFICATE_FORMAT_ERROR 3
//Ошибка криптопровайдера
#define ILRET_CRYPTO_PROVIDER_ERROR    4
//Неверный пароль!
#define ILRET_WRONG_PASSWORD_ERROR     5
//Карта заблокирована
#define ILRET_CARD_BLOCKED             6
//Карта изъята
#define ILRET_CARD_REMOVED_ERROR       7
// Ошибка открытия карты
#define ILRET_CARD_OPEN_ERROR          8
// Ошибка авторизации
#define ILRET_AUTHORISATION_ERROR      9
//Ошибка чтения данных
#define ILRET_READ_DATA_ERROR          10
//Ошибка записи данных
#define ILRET_WRITE_DATA_ERROR         11



// Ошибка OpLib
//Запись на карту не возможна
#define ILRET_OPLIB_PRIVATE_DATA_BELONGS_TO_OTHER_PERSON 11000
#define ILRET_OPLIB_OMS_HISTORY_FULL	11001