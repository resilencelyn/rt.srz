#pragma once

//Открывает карту для чтения/записи
// pwszUecServiceToken токен доступа к сервису настроек (empty - работа с terminal.ini)
UECLIB_API DWORD OpenCard(WCHAR* pwszUecServiceToken);

//Открывает карту для чтения/записи
// pwszUecServiceToken токен доступа к сервису настроек (empty - работа с terminal.ini)
// pwszCIN - результирующий номер карты
UECLIB_API DWORD OpenCardWithHandle(WCHAR* pwszUecServiceToken, WCHAR* pwszCIN);

//Открывает карту для чтения/записи
//wszReaderName - имя ридера
//szOKO1OpenCert - сертификат открытого ключа ОКО1
//szTerminalOpenCert - сертификат открытого ключа терминала
//szUC1OpenCert - сертификат открытого удостоверяющего центра
//szTerminalClosedCertGOST - сертификат закрытого ключа терминала
//pwszCIN - результирующий номер карты
UECLIB_API DWORD OpenCardWithHandleByGlobalData(WCHAR* wszReaderName, BYTE* szOKO1OpenCert, BYTE* szTerminalOpenCert, 
  BYTE* szUC1OpenCert, BYTE* szTerminalClosedCert, WCHAR* pwszCIN);

//Авторизует по PIN коду
//Возвращает количество оставшихся попыток
UECLIB_API DWORD Authorise(CHAR* pszPinCode, BYTE* pbPinRestTriesOut);

//Читает личные данные владельца карты
//pPrivateData - Указатель на структуру, в которую будут записаны данные
UECLIB_API DWORD ReadPrivateData(PrivateData* pPrivateData);

//Читает личные данные владельца карты
//Все поля записываются в строку в последовательности совпадающей со структурой PrivateData
UECLIB_API DWORD ReadPrivateDataInString(WCHAR* pDstString, DWORD pdwDstStringSize);

//Читает данные о текущем страховом полисе
//pOMSData - Указатель структуру, в которыу будут записаны данные
UECLIB_API DWORD ReadMainOMSData(OMSData* pOMSData);

//Читает данные о текущем страховом полисе
//Все поля записываются в строку в последовательности совпадающей со структурой OMSData
UECLIB_API DWORD ReadMainOMSDataInString(WCHAR* pDstString, DWORD pdwDstStringSize);

//Читает данные о страховых полисах
//pOMSData - Указатель на массив структур, в которые будут записаны данные
//pdwOMSDataSize - указатель на переменную,  в которой функция ожидает размер массива,
//передваемого через указатель pOMSData и в который записывается размер заполненного массива
UECLIB_API DWORD ReadOMSData(OMSData* pOMSData, DWORD* pdwOMSDataSize);

//Записывает данные о новом страховом полисе, а также историю о предыдущем
UECLIB_API DWORD WriteOMSData(OMSData* pNewOMSData);

//Закрывает карту
UECLIB_API DWORD CloseCard(void);

//Устанавливает либо сбрасывает режим возврата сисетмных кодов ошибок
//systemTraceMode = TRUE -  Возвращаются системные ошибки
//systemTraceMode = FALSE - Возвращаются прикладные ошибки
UECLIB_API void SetSystemTraceMode(BOOL systemTraceMode);

//Возвращает описание ошибки по ее коду
UECLIB_API void GetErrorDescription(DWORD dwErrorCode, CHAR* pszErrorDescription);

//Возвращает список подключенных карт-ридеров
//pReaderInfo - Указатель на массив структур, в которые будут записаны данные
//pdwReaderInfoCount - указатель на переменную,  в которой функция ожидает размер массива,
//передваемого через указатель pReaderInfo и в который записывается размер заполненного массива
UECLIB_API void GetReaderList(CardReaderInfo* pReaderInfo, DWORD* pdwReaderInfoCount);

//Возвращает список подключенных карт-ридеров
//Все ридеры будут записаны в строку
UECLIB_API void GetReaderListInString(WCHAR* pDstString, DWORD pdwDstStringSize);

//Возвращает имя текущей машины с учетом удаленной сессии
UECLIB_API void GetCurrentComputerName(WCHAR* pwszComputerName);

//Возвращает имя текущей машины с учетом удаленной сессии
UECLIB_API void GetCurrentComputerNameWithTakingRemoteSession(WCHAR* pwszComputerName);




