// UEKLib.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include <windows.h>
#include "CardLib.h"
#include "FuncLib.h"
#include "il_error.h"
#include "opDescr.h"
#include "opApi.h"
#include "opRunFunc.h"
#include "opEvent.h"
#include "opState.h"
#include "Structures.h"
#include "Parsing.h"
#include "ErrorCodes.h"
#include <time.h>
#include <ios>
#include <fstream>
#include <iostream>
#include <Wtsapi32.h>
#include "UECLib.h"

IL_CARD_HANDLE g_hCrd;
HANDLE_READER g_hRdr; 
HANDLE_CRYPTO g_hCrypto;
s_opContext g_opContext;
BOOL g_bSystemTraceMode = FALSE;


//Запись лога
void WriteLog(CHAR* szMethodName, DWORD dwTicks)
{
  CHAR szModuleFileName[_MAX_PATH];
  GetModuleFileNameA(NULL, szModuleFileName, 1000);
  CHAR szDrive[_MAX_PATH];
  CHAR szDir[_MAX_PATH];
  CHAR szFileName[_MAX_PATH];
  CHAR szExt[_MAX_PATH];
  _splitpath(szModuleFileName, szDrive, szDir, szFileName, szExt);

  CHAR szPathToLog[_MAX_PATH];
  sprintf(szPathToLog, "%s%s\\log\\console.Info.log", szDrive, szDir);
  
  //Время
  time_t rawtime;
  struct tm * timeinfo;
  time ( &rawtime );
  timeinfo = localtime ( &rawtime );
  char szDateTime[200];
  sprintf(szDateTime, "%d-%d-%d %d:%d:%d.0000: ", timeinfo->tm_year + 1900, timeinfo->tm_mon + 1, timeinfo->tm_mday, 
    timeinfo->tm_hour, timeinfo->tm_min, timeinfo->tm_sec);

  float fTicks = (float)dwTicks/(float)1000;
  
  char szLogRecord[2000];
  sprintf(szLogRecord, "%s                                   Call: %s = %.3f seconds\n", szDateTime, szMethodName, fTicks);

  //Открытие файла
  std::ofstream log(szPathToLog, std::ios::app);
  log << szLogRecord;
}

enum CallFunction
{
  CF_OPEN_CARD,
  CF_AUTHORISE,
  CF_READ_DATA,
  CF_WRITE_DATA
};

DWORD ConvertErrorCode(DWORD dwSystemErrorCode, CallFunction callFunction)
{
  // Не меняем код в случе режима вывода системынх ошибок
  if (g_bSystemTraceMode || dwSystemErrorCode == ILRET_OK)
    return dwSystemErrorCode;
  
  // Ошибка карт-ридера
  if (dwSystemErrorCode >= ILRET_SCR_ERROR && dwSystemErrorCode <= ILRET_SCR_READER_UNAVAILABLE)
    return ILRET_CARD_READER_ERROR;

  //Ошибка карты
  if (dwSystemErrorCode >= ILRET_CRD_ERROR && dwSystemErrorCode <= ILRET_CRD_APPREC_WRONG_PARAMETERS_P1P2)
    return ILRET_CARD_ERROR;

   //Ошибка проверки формата сертификата
  if (dwSystemErrorCode >= ILRET_CERT_ERROR && dwSystemErrorCode <= ILRET_CERT_TERMINFO_NOT_MATCH)
    return ILRET_CERTIFICATE_FORMAT_ERROR;

  //Ошибка криптопровайдера
  if (dwSystemErrorCode >= ILRET_CRYPTO_ERROR && dwSystemErrorCode <= ILRET_CRYPTO_ERROR_KEYMATCHING)
    return ILRET_CRYPTO_PROVIDER_ERROR;

  //Не верный пароль
  if (dwSystemErrorCode >= ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_1 && dwSystemErrorCode <= ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_16)
    return ILRET_WRONG_PASSWORD_ERROR;

  //Карта заблокирована
  if (dwSystemErrorCode == ILRET_OPLIB_CARD_LOCKED)
    return ILRET_CARD_BLOCKED;

  //Карта изъята
  if (dwSystemErrorCode == ILRET_OPLIB_CARD_CAPTURED)
    return ILRET_CARD_BLOCKED;

  // Возвращаем все оставшиеся значения
  switch (callFunction)
  {
    case CF_OPEN_CARD:
      return ILRET_CARD_OPEN_ERROR;
    case CF_AUTHORISE:
      return ILRET_AUTHORISATION_ERROR;
    case CF_READ_DATA:
      return ILRET_READ_DATA_ERROR;
    case CF_WRITE_DATA:
      return ILRET_WRITE_DATA_ERROR;
  }

  return dwSystemErrorCode;
}

void WriteFieldToBuffer(WCHAR* pwszBuffer, DWORD dwBuferSize, WCHAR* pwszField, DWORD* pdwOffset)
{
  if (*pdwOffset + wcslen(pwszField)*2 + wcslen(L"<|>")*2 > dwBuferSize)
    return;
  
  if (pwszField)
  {
    wcscpy(pwszBuffer + *pdwOffset, pwszField);
    (*pdwOffset) += wcslen(pwszField);
  }
  wcscpy(pwszBuffer + *pdwOffset, L"<|>");
  (*pdwOffset) += wcslen(L"<|>");
}

//Открывает карту для чтения/записи
UECLIB_API DWORD OpenCard(WCHAR* pwszUecServiceToken)
{
  //Сохраняем security token для доступа к УЭК сервису
  prmSetUecServiceToken(pwszUecServiceToken);
  
  // инициализируем указатели на дескрипторы ридера и криптосессии
  g_hCrd.hRdr = (IL_HANDLE_READER)&g_hRdr;
  g_hCrd.hCrypto = (IL_HANDLE_CRYPTO)&g_hCrypto;

  //деинициализация ридера
  IL_RETCODE dwRetCode = flDeinitReader(&g_hCrd);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_OPEN_CARD);

  //чтение конфигурации
  IL_DWORD dwLen = 200;
  char chReaderSettings[200];
  dwRetCode = prmGetParameter(IL_PARAM_READERNAME, (IL_BYTE*)chReaderSettings, &dwLen);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_OPEN_CARD);

  //инициализация ридера
  dwRetCode = flInitReader(&g_hCrd, chReaderSettings);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_OPEN_CARD); //карта не установлена

  //Открытие карты
  dwRetCode = clCardOpen(&g_hCrd);
  return ConvertErrorCode(dwRetCode, CF_OPEN_CARD);
}

//Открывает карту для чтения/записи
//pszPinCode - пин код
UECLIB_API DWORD OpenCardWithHandle(WCHAR* pwszUecServiceToken, WCHAR* pwszCIN)
{
  //Сохраняем security token для доступа к УЭК сервису
  prmSetUecServiceToken(pwszUecServiceToken);
  
  // инициализируем указатели на дескрипторы ридера и криптосессии
  g_hCrd.hRdr = (IL_HANDLE_READER)&g_hRdr;
  g_hCrd.hCrypto = (IL_HANDLE_CRYPTO)&g_hCrypto;

  //деинициализация ридера
  IL_RETCODE dwRetCode = flDeinitReader(&g_hCrd);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_OPEN_CARD);

  //чтение конфигурации
  IL_DWORD dwLen = 200;
  char chReaderSettings[200];
  dwRetCode = prmGetParameter(IL_PARAM_READERNAME, (IL_BYTE*)chReaderSettings, &dwLen);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_OPEN_CARD);

  //инициализация ридера
  dwRetCode = flInitReader(&g_hCrd, chReaderSettings);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_OPEN_CARD); //карта не установлена

  //открытие карты
  dwRetCode = clCardOpen(&g_hCrd);
  if (dwRetCode == ILRET_OK)
  {
    //Получение CIN номера
    memset(g_hCrd.CIN, 0, sizeof(g_hCrd.CIN));
    if (flGetCIN(&g_hCrd, g_hCrd.CIN) == ILRET_OK)
    {
      //Конвертация CIN номера в строку
      IL_CHAR szCIN[200] = {0};
      bin2hex(szCIN, g_hCrd.CIN, 10);
      MultiByteToWideChar(CP_ACP, 0, szCIN, -1, pwszCIN, strlen(szCIN)*2);
    }
  }
  
  return ConvertErrorCode(dwRetCode, CF_OPEN_CARD);
}

UECLIB_API DWORD OpenCardWithHandleByGlobalData(WCHAR* wszReaderName, BYTE* szOKO1OpenCert, BYTE* szTerminalOpenCert, 
  BYTE* szUC1OpenCert, BYTE* szTerminalClosedCert, WCHAR* pwszCIN)
{
  //Передача данных для работы ридера
  prmSetGlobalConfigurationData(wszReaderName, szOKO1OpenCert, szTerminalOpenCert, szUC1OpenCert, szTerminalClosedCert);

  //открытие карты
  return OpenCardWithHandle(L"", pwszCIN);
}

//Авторизует по PIN коду
UECLIB_API DWORD Authorise(CHAR* pszPinCode, BYTE* pbPinRestTriesOut)
{
  if (pszPinCode == NULL || pbPinRestTriesOut == NULL)
    return ILRET_SYS_INVALID_ARGUMENT;

  //очистим контекст операции
  DWORD dwRetCode = opCtxSetClean(&g_opContext);
  if(dwRetCode != 0) 
    return ConvertErrorCode(dwRetCode, CF_AUTHORISE);

  //инициализируем операцию
  IL_BYTE MetaInfo[] = { 0x00 };
  IL_CHAR passPhrase[PASS_PHRASE_MAX_LEN+1];
  dwRetCode = opApiInitOperation(&g_opContext, &g_hCrd, UEC_OP_EDIT_PRIVATE_DATA, MetaInfo, sizeof(MetaInfo),
    NULL, NULL, NULL, NULL, passPhrase, NULL);
  if (dwRetCode != 0)
    return ConvertErrorCode(dwRetCode, CF_AUTHORISE);

  // верифицируем гражданина
  dwRetCode = opApiVerifyCitizen(&g_opContext, IL_KEYTYPE_01_PIN, pszPinCode);
  if(dwRetCode != 0)
  {
    // анализируем ошибку
		if((dwRetCode < ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_1) || (dwRetCode >= ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_1 + PIN_TRIES_MAX))
      CloseCard(); //Превышено количество попыток ввода ПИН, карта заблокирована
    
    //Расчет к-ва оставшихся попыток ввода ПИН
    *pbPinRestTriesOut = (BYTE)(dwRetCode - ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_1 + 1);
    
    return ConvertErrorCode(dwRetCode, CF_AUTHORISE);
  }

  return ILRET_OK;
}

//Читает личные данные владельца карты
UECLIB_API DWORD ReadPrivateData(PrivateData* pPrivateData)
{
  if (pPrivateData == NULL)
    return ConvertErrorCode(ILRET_SYS_INVALID_ARGUMENT, CF_READ_DATA);

  // получим строку-описатель данных cектора-блока [1-2](ФИО, пол, номер полиса и т.д.)
  IL_CHAR szBlockDataDescr[1024*2+1];
  IL_WORD wBlockDataDescrLen = sizeof(szBlockDataDescr);
  memset(szBlockDataDescr, 0, wBlockDataDescrLen);
  DWORD dwRetCode = opApiGetCardBlockDataDescr(&g_opContext, "1-2", szBlockDataDescr, &wBlockDataDescrLen);
  if (dwRetCode != ILRET_OK)
  {
    // завершаем операцию
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_READ_DATA);
  }

  // парсинг значений блока [1-2]
  ParsePrivateData(pPrivateData, szBlockDataDescr);

  // получим строку-описатель данных cектора-блока [1-4](Адрес, телефон, документ и т.д.)
  wBlockDataDescrLen = sizeof(szBlockDataDescr);
  memset(szBlockDataDescr, 0, wBlockDataDescrLen);
  dwRetCode = opApiGetCardBlockDataDescr(&g_opContext, "1-4", szBlockDataDescr, &wBlockDataDescrLen);
  if (dwRetCode != ILRET_OK)
  {
    // завершаем операцию
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_READ_DATA);;
  }

  // парсинг значений блока [1-4]
  ParsePrivateData(pPrivateData, szBlockDataDescr);

  return ILRET_OK;
}

UECLIB_API DWORD ReadPrivateDataInString(WCHAR* pDstString, DWORD pdwDstStringSize)
{
  if (pDstString == NULL)
    return ConvertErrorCode(ILRET_SYS_INVALID_ARGUMENT, CF_READ_DATA);
  
  PrivateData privateData;
  DWORD dwRetCode = ReadPrivateData(&privateData);
  if (dwRetCode != ILRET_OK)
    return dwRetCode;

  DWORD dwOffset = 0;
  //СНИЛС
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szSNILS, &dwOffset);
  //Номер полиса ОМС
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szPolicyNumberOMS, &dwOffset);
  //Адрес эмитента
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szIssuerAddress, &dwOffset);
  //Дата рождения
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szBirthDate, &dwOffset);
  //Место рождения
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szBirthPlace, &dwOffset);
  //Пол
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szGender, &dwOffset);
  //Фамилия
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szLastName, &dwOffset);
  //Имя
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szFirstName, &dwOffset);
  //Отчество
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szMiddleName, &dwOffset);
  //Адрес
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szAddress, &dwOffset);
  //Телефон
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szTelNumber, &dwOffset);
  //E-Mail
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szEmail, &dwOffset);
  //ИНН
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szINN, &dwOffset);
  //Номер водительского удост
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDriveLicenseNumber, &dwOffset);
  //Номер регистрации транспо
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szTransportNumber, &dwOffset);
  //Тип документа УДЛ
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentType, &dwOffset);
  //Серия документа
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentSeries, &dwOffset);
  //Номер документа
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentNumber, &dwOffset);
  //Дата выдачи документа
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentIssueDate, &dwOffset);
  //Дата окончания действия д
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentEndDate, &dwOffset);
  //Кем выдан
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentIssueAuthority, &dwOffset);
  //Код подразделения
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentDepartmentCode, &dwOffset);
  //Дополнительные сведения
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentAdditionalData, &dwOffset);

  return ILRET_OK;
}

UECLIB_API DWORD ReadMainOMSData(OMSData* pOMSData)
{
  if (pOMSData == NULL)
    return ConvertErrorCode(ILRET_SYS_INVALID_ARGUMENT, CF_READ_DATA);

  // получим строку-описатель данных cектора-блока [3-1](Сведения о текущем полисе)
  IL_CHAR szBlockDataDescr[1024*2+1];
  IL_WORD wBlockDataDescrLen = sizeof(szBlockDataDescr);
  memset(szBlockDataDescr, 0, wBlockDataDescrLen);
  DWORD dwRetCode = opApiGetCardBlockDataDescr(&g_opContext, "3-1", szBlockDataDescr, &wBlockDataDescrLen);
  if (dwRetCode != ILRET_OK)
  {
    // завершаем операцию
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_READ_DATA);
  }

  // парсинг значений блока [3-1]
  ParseMainOMSData(pOMSData, szBlockDataDescr);

  return ILRET_OK;
}

UECLIB_API DWORD ReadMainOMSDataInString(WCHAR* pDstString, DWORD pdwDstStringSize)
{
  if (pDstString == NULL)
    return ConvertErrorCode(ILRET_SYS_INVALID_ARGUMENT, CF_READ_DATA);
  
  OMSData omsData;
  DWORD dwRetCode = ReadMainOMSData(&omsData);
  if (dwRetCode != ILRET_OK)
    return dwRetCode;

  DWORD dwOffset = 0;
  //ОГРН страховщика
  WriteFieldToBuffer(pDstString, pdwDstStringSize, omsData.szOGRN, &dwOffset);
  //ОКАТО страховщика
  WriteFieldToBuffer(pDstString, pdwDstStringSize, omsData.szOKATO, &dwOffset);
  //Дата страхования
  WriteFieldToBuffer(pDstString, pdwDstStringSize, omsData.szInsuranceDate, &dwOffset);
  //Дата окончания страхования
  WriteFieldToBuffer(pDstString, pdwDstStringSize, omsData.szInsuranceEndDate, &dwOffset);

  return ILRET_OK;
}

//Читает данные о страховых полисах
UECLIB_API DWORD ReadOMSData(OMSData* pOMSData, DWORD* pdwOMSDataSize)
{
  if (pOMSData == NULL || pdwOMSDataSize == NULL || *pdwOMSDataSize < 11)
    return ConvertErrorCode(ILRET_SYS_INVALID_ARGUMENT, CF_READ_DATA);

  //Чтение данных о текущем страховом полисе
  DWORD dwRetCode = ReadMainOMSData(pOMSData);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_READ_DATA);
  
  //При запросе данных о текущем страховом полисе->возврат
  if (*pdwOMSDataSize == 1)
    return ILRET_OK;
  
  *pdwOMSDataSize = 1; //заполнены данные о текущем страховом полисе

  // получим строку-описатель данных cектора-блока [3-2](Сведения о предыдущих полисах)
  IL_CHAR szBlockDataDescr[1024*2+1];
  IL_WORD wBlockDataDescrLen = sizeof(szBlockDataDescr);
  wBlockDataDescrLen = sizeof(szBlockDataDescr);
  memset(szBlockDataDescr, 0, wBlockDataDescrLen);
  dwRetCode = opApiGetCardBlockDataDescr(&g_opContext, "3-2", szBlockDataDescr, &wBlockDataDescrLen);
  if (dwRetCode != ILRET_OK)
  {
    // завершаем операцию
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_READ_DATA);
  }
  
  // парсинг значений блока [3-2]
  ParseOldOMSData(pOMSData, szBlockDataDescr, pdwOMSDataSize);

  return ILRET_OK;
}

//Записывает данные о новом страховом полисе, а также историю о предыдущем
UECLIB_API DWORD WriteOMSData(OMSData* pNewOMSData)
{
  if (pNewOMSData == NULL || pNewOMSData->szOGRN == NULL || pNewOMSData->szOKATO == NULL || 
      pNewOMSData->szInsuranceDate == NULL || pNewOMSData->szInsuranceEndDate == NULL ||
      pNewOMSData->szLastName == NULL || pNewOMSData->szFirstName == NULL || pNewOMSData->szMiddleName == NULL || pNewOMSData->szBirthDate == NULL)
    return ConvertErrorCode(ILRET_SYS_INVALID_ARGUMENT, CF_WRITE_DATA);

  //Чтение приватных данных, для определения возможности записи на карту
  PrivateData privateData;
  DWORD dwRetCode = ReadPrivateData(&privateData);
  if (dwRetCode != ILRET_OK)
    return dwRetCode;

  //Сравнение фамилии
  if (_wcsicmp(privateData.szLastName, pNewOMSData->szLastName) != 0) 
    return ConvertErrorCode(ILRET_OPLIB_PRIVATE_DATA_BELONGS_TO_OTHER_PERSON, CF_WRITE_DATA);
  
  //Сравнение имени
  if (_wcsicmp(privateData.szFirstName, pNewOMSData->szFirstName) != 0) 
    return ConvertErrorCode(ILRET_OPLIB_PRIVATE_DATA_BELONGS_TO_OTHER_PERSON, CF_WRITE_DATA);
  
  //Сравнение отчества
  if (_wcsicmp(privateData.szMiddleName, pNewOMSData->szMiddleName) != 0) 
    return ConvertErrorCode(ILRET_OPLIB_PRIVATE_DATA_BELONGS_TO_OTHER_PERSON, CF_WRITE_DATA);

  //Сравнение даты рождения
  if (_wcsicmp(privateData.szBirthDate, pNewOMSData->szBirthDate) != 0) 
    return ConvertErrorCode(ILRET_OPLIB_PRIVATE_DATA_BELONGS_TO_OTHER_PERSON, CF_WRITE_DATA);

  //Чтение данных о страховках, записанных на карту
  OMSData* pOldOMSData = new OMSData[11];
  DWORD dwSize = 11;
  dwRetCode = ReadOMSData(pOldOMSData, &dwSize);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_WRITE_DATA);

  //Карта заполнена, запись не возможна
  if (dwSize == 11)
  {
    //завершаем операцию
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(ILRET_OPLIB_OMS_HISTORY_FULL, CF_WRITE_DATA);
  }

  //Склейка строки для записи истории
  WCHAR wszHistoryData[1024];
  swprintf(wszHistoryData, L"0%s0%s%s", pOldOMSData[0].szOGRN, 
    pOldOMSData[0].szOKATO, pOldOMSData[0].szInsuranceDate);

  //Запись истории
  CHAR szHistoryData[1024];
  WideCharToMultiByte(CP_ACP, 0, wszHistoryData, -1, szHistoryData, 1024, NULL, NULL);
  CHAR szHistoryCardData[1024];
  sprintf(szHistoryCardData, "%s=%s", "3-2-0|14", szHistoryData);
  dwRetCode = opApiWriteCardData(&g_opContext, szHistoryCardData);
  if (dwRetCode != ILRET_OK)
  {
    // завершаем операцию
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_WRITE_DATA);
  }

  //Запись ОГРН для нового полиса
  CHAR szData[100];
  CHAR szCardData[200];
  WideCharToMultiByte(CP_ACP, 0, pNewOMSData->szOGRN, -1, szData, 100, NULL, NULL);
  sprintf(szCardData, "%s=0%s", "3-1-0|7", szData);
  dwRetCode = opApiWriteCardData(&g_opContext, szCardData);
  if (dwRetCode != ILRET_OK)
  {
    // завершаем операцию
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_WRITE_DATA);
  }
  
  //Запись ОКАТО для нового полиса
  WideCharToMultiByte(CP_ACP, 0, pNewOMSData->szOKATO, -1, szData, 100, NULL, NULL);
  sprintf(szCardData, "%s=0%s", "3-1-7|3", szData);
  dwRetCode = opApiWriteCardData(&g_opContext, szCardData);
  if (dwRetCode != ILRET_OK)
  {
    // завершаем операцию
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_WRITE_DATA);
  }

  //Запись даты страхования для нового полиса
  WideCharToMultiByte(CP_ACP, 0, pNewOMSData->szInsuranceDate, -1, szData, 100, NULL, NULL);
  sprintf(szCardData, "%s=%s", "3-1-10|4", szData);
  dwRetCode = opApiWriteCardData(&g_opContext, szCardData);
  if (dwRetCode != ILRET_OK)
  {
    // завершаем операцию
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_WRITE_DATA);
  }

  //Запись даты окончания страхования для нового полиса
  WideCharToMultiByte(CP_ACP, 0, pNewOMSData->szInsuranceEndDate, -1, szData, 100, NULL, NULL);
  sprintf(szCardData, "%s=%s", "3-1-14|4", szData);
  dwRetCode = opApiWriteCardData(&g_opContext, szCardData);
  if (dwRetCode != ILRET_OK)
  {
    // завершаем операцию
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_WRITE_DATA);
  }

  return ILRET_OK;
}

//Закрывает карту
UECLIB_API DWORD CloseCard(void)
{
  // завершаем операцию
  opApiDeinitOperation(&g_opContext);
  
  //очистим контекст операции
  opCtxSetClean(&g_opContext);

  //закрытие карты
  if (g_hCrd.hRdr != NULL)
  {
    clCardClose(&g_hCrd);
    //деинициализация ридера
    flDeinitReader(&g_hCrd);
  }

  memset(&g_hCrd, 0, sizeof(g_hCrd));

  return ILRET_OK;
}

UECLIB_API void SetSystemTraceMode(BOOL systemTraceMode)
{
  g_bSystemTraceMode = systemTraceMode;
}

//Возвращает описание ошибки по ее коду
UECLIB_API void GetErrorDescription(DWORD dwRetCode, CHAR* pszErrorDescription)
{
  if (pszErrorDescription == NULL)
    return;

  if (g_bSystemTraceMode)
  {
    //кастомная ошибка, запись на переполненную карту
    if (dwRetCode == ILRET_OPLIB_OMS_HISTORY_FULL)
    {
      strcpy(pszErrorDescription, "Запись не разрешена, так как карта заполнена полностью");
      return;
    }
    else
    if (dwRetCode == ILRET_OPLIB_PRIVATE_DATA_BELONGS_TO_OTHER_PERSON)
    {
      strcpy(pszErrorDescription, "Карта в устройстве принадлежит иному лицу");
      return;
    }

    strcpy(pszErrorDescription, opApiGetErrorDescr(dwRetCode));
  }
  else
  {
    if (dwRetCode == ILRET_CARD_OPEN_ERROR)
      strcpy(pszErrorDescription, "Ошибка открытия карты");
    if (dwRetCode == ILRET_AUTHORISATION_ERROR)
      strcpy(pszErrorDescription, "Ошибка авторизации");
    if (dwRetCode == ILRET_READ_DATA_ERROR)
      strcpy(pszErrorDescription, "Ошибка чтения данных");
    if (dwRetCode == ILRET_WRITE_DATA_ERROR)
      strcpy(pszErrorDescription, "Ошибка записи данных");
    if (dwRetCode == ILRET_CARD_READER_ERROR)
      strcpy(pszErrorDescription, "Ошибка карт-ридера");
    if (dwRetCode == ILRET_CARD_ERROR)
      strcpy(pszErrorDescription, "Ошибка карты");
    if (dwRetCode == ILRET_CERTIFICATE_FORMAT_ERROR)
      strcpy(pszErrorDescription, "Ошибка проверки формата сертификата");
    if (dwRetCode == ILRET_CRYPTO_PROVIDER_ERROR)
      strcpy(pszErrorDescription, "Ошибка криптопровайдера");
    if (dwRetCode == ILRET_WRONG_PASSWORD_ERROR)
      strcpy(pszErrorDescription, "Неверный пароль!");
    if (dwRetCode == ILRET_CARD_BLOCKED)
        strcpy(pszErrorDescription, "Карта заблокирована");
    if (dwRetCode == ILRET_CARD_REMOVED_ERROR)
      strcpy(pszErrorDescription, "Карта изъята");
  }
}

//Возвращает список подключенных карт-ридеров
//pReaderInfo - Указатель на массив структур, в которые будут записаны данные
//pdwReaderInfoCount - указатель на переменную,  в которой функция ожидает размер массива,
//передваемого через указатель pReaderInfo и в который записывается размер заполненного массива
UECLIB_API void GetReaderList(CardReaderInfo* pReaderInfo, DWORD* pdwReaderInfoCount)
{
  if (pdwReaderInfoCount == NULL)
    return;

  //Запрашиваем список устройств
  SCARDCONTEXT hContext = NULL;
  LPTSTR pszReadersNames = NULL;
  DWORD cch = SCARD_AUTOALLOCATE;
  if (SCardListReaders(hContext, NULL, (LPTSTR)&pszReadersNames, &cch) != SCARD_S_SUCCESS)
  {
    *pdwReaderInfoCount = 0;
    return;
  }
  
  //Парсим имена устройств
  LPTSTR pszReaderName = pszReadersNames;
  DWORD dwReaderIndex = 0;
  while (*pszReaderName !='\0')
  {
    if (pReaderInfo != NULL && *pdwReaderInfoCount > dwReaderIndex)
    {
      wcscpy(pReaderInfo[dwReaderIndex].szReaderName, pszReaderName);
    }
    else
    {
      break;
    }
        
    //Переход к следующему значению
    pszReaderName = pszReaderName + wcslen((WCHAR*)pszReaderName) + 1;

    dwReaderIndex++;
  }

  *pdwReaderInfoCount = dwReaderIndex;
    
  //Освобождаем память
  SCardFreeMemory( hContext, pszReadersNames );
}

//Возвращает список подключенных карт-ридеров
//Все ридеры будут записаны в строку
UECLIB_API void GetReaderListInString(WCHAR* pDstString, DWORD pdwDstStringSize)
{
  DWORD readerCount = 100;
  CardReaderInfo info[100];
  GetReaderList(info, &readerCount);
  
  DWORD dwOffset = 0;
  for (int readerIndex=0; readerIndex < readerCount; readerIndex++)
    WriteFieldToBuffer(pDstString, pdwDstStringSize, info[readerIndex].szReaderName, &dwOffset);
}

UECLIB_API void GetCurrentComputerName(WCHAR* pwszComputerName)
{
  if (pwszComputerName == NULL)
    return; 
  
  //Получение имени текущего компьютера
  DWORD dwCurrentComputerName = 1024*5;
  WCHAR wszCurrentComputerName[1024*5];
  GetComputerName(wszCurrentComputerName, &dwCurrentComputerName);
  wcscpy(pwszComputerName, wszCurrentComputerName);
}

//Возвращает имя текущей машины с учетом удаленной сессии
UECLIB_API void GetCurrentComputerNameWithTakingRemoteSession(WCHAR* pwszComputerName)
{
  if (pwszComputerName == NULL)
    return;
  
  WCHAR* pwszClientName = NULL;
  DWORD dwClientSizeName = 0;
  if (!WTSQuerySessionInformation(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, WTSClientName, &pwszClientName, &dwClientSizeName))
    return;
    
  wcscpy(pwszComputerName, pwszClientName);
  WTSFreeMemory(pwszClientName);
}



