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


//������ ����
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
  
  //�����
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

  //�������� �����
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
  // �� ������ ��� � ����� ������ ������ ��������� ������
  if (g_bSystemTraceMode || dwSystemErrorCode == ILRET_OK)
    return dwSystemErrorCode;
  
  // ������ ����-������
  if (dwSystemErrorCode >= ILRET_SCR_ERROR && dwSystemErrorCode <= ILRET_SCR_READER_UNAVAILABLE)
    return ILRET_CARD_READER_ERROR;

  //������ �����
  if (dwSystemErrorCode >= ILRET_CRD_ERROR && dwSystemErrorCode <= ILRET_CRD_APPREC_WRONG_PARAMETERS_P1P2)
    return ILRET_CARD_ERROR;

   //������ �������� ������� �����������
  if (dwSystemErrorCode >= ILRET_CERT_ERROR && dwSystemErrorCode <= ILRET_CERT_TERMINFO_NOT_MATCH)
    return ILRET_CERTIFICATE_FORMAT_ERROR;

  //������ ����������������
  if (dwSystemErrorCode >= ILRET_CRYPTO_ERROR && dwSystemErrorCode <= ILRET_CRYPTO_ERROR_KEYMATCHING)
    return ILRET_CRYPTO_PROVIDER_ERROR;

  //�� ������ ������
  if (dwSystemErrorCode >= ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_1 && dwSystemErrorCode <= ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_16)
    return ILRET_WRONG_PASSWORD_ERROR;

  //����� �������������
  if (dwSystemErrorCode == ILRET_OPLIB_CARD_LOCKED)
    return ILRET_CARD_BLOCKED;

  //����� ������
  if (dwSystemErrorCode == ILRET_OPLIB_CARD_CAPTURED)
    return ILRET_CARD_BLOCKED;

  // ���������� ��� ���������� ��������
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

//��������� ����� ��� ������/������
UECLIB_API DWORD OpenCard(WCHAR* pwszUecServiceToken)
{
  //��������� security token ��� ������� � ��� �������
  prmSetUecServiceToken(pwszUecServiceToken);
  
  // �������������� ��������� �� ����������� ������ � ������������
  g_hCrd.hRdr = (IL_HANDLE_READER)&g_hRdr;
  g_hCrd.hCrypto = (IL_HANDLE_CRYPTO)&g_hCrypto;

  //��������������� ������
  IL_RETCODE dwRetCode = flDeinitReader(&g_hCrd);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_OPEN_CARD);

  //������ ������������
  IL_DWORD dwLen = 200;
  char chReaderSettings[200];
  dwRetCode = prmGetParameter(IL_PARAM_READERNAME, (IL_BYTE*)chReaderSettings, &dwLen);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_OPEN_CARD);

  //������������� ������
  dwRetCode = flInitReader(&g_hCrd, chReaderSettings);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_OPEN_CARD); //����� �� �����������

  //�������� �����
  dwRetCode = clCardOpen(&g_hCrd);
  return ConvertErrorCode(dwRetCode, CF_OPEN_CARD);
}

//��������� ����� ��� ������/������
//pszPinCode - ��� ���
UECLIB_API DWORD OpenCardWithHandle(WCHAR* pwszUecServiceToken, WCHAR* pwszCIN)
{
  //��������� security token ��� ������� � ��� �������
  prmSetUecServiceToken(pwszUecServiceToken);
  
  // �������������� ��������� �� ����������� ������ � ������������
  g_hCrd.hRdr = (IL_HANDLE_READER)&g_hRdr;
  g_hCrd.hCrypto = (IL_HANDLE_CRYPTO)&g_hCrypto;

  //��������������� ������
  IL_RETCODE dwRetCode = flDeinitReader(&g_hCrd);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_OPEN_CARD);

  //������ ������������
  IL_DWORD dwLen = 200;
  char chReaderSettings[200];
  dwRetCode = prmGetParameter(IL_PARAM_READERNAME, (IL_BYTE*)chReaderSettings, &dwLen);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_OPEN_CARD);

  //������������� ������
  dwRetCode = flInitReader(&g_hCrd, chReaderSettings);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_OPEN_CARD); //����� �� �����������

  //�������� �����
  dwRetCode = clCardOpen(&g_hCrd);
  if (dwRetCode == ILRET_OK)
  {
    //��������� CIN ������
    memset(g_hCrd.CIN, 0, sizeof(g_hCrd.CIN));
    if (flGetCIN(&g_hCrd, g_hCrd.CIN) == ILRET_OK)
    {
      //����������� CIN ������ � ������
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
  //�������� ������ ��� ������ ������
  prmSetGlobalConfigurationData(wszReaderName, szOKO1OpenCert, szTerminalOpenCert, szUC1OpenCert, szTerminalClosedCert);

  //�������� �����
  return OpenCardWithHandle(L"", pwszCIN);
}

//���������� �� PIN ����
UECLIB_API DWORD Authorise(CHAR* pszPinCode, BYTE* pbPinRestTriesOut)
{
  if (pszPinCode == NULL || pbPinRestTriesOut == NULL)
    return ILRET_SYS_INVALID_ARGUMENT;

  //������� �������� ��������
  DWORD dwRetCode = opCtxSetClean(&g_opContext);
  if(dwRetCode != 0) 
    return ConvertErrorCode(dwRetCode, CF_AUTHORISE);

  //�������������� ��������
  IL_BYTE MetaInfo[] = { 0x00 };
  IL_CHAR passPhrase[PASS_PHRASE_MAX_LEN+1];
  dwRetCode = opApiInitOperation(&g_opContext, &g_hCrd, UEC_OP_EDIT_PRIVATE_DATA, MetaInfo, sizeof(MetaInfo),
    NULL, NULL, NULL, NULL, passPhrase, NULL);
  if (dwRetCode != 0)
    return ConvertErrorCode(dwRetCode, CF_AUTHORISE);

  // ������������ ����������
  dwRetCode = opApiVerifyCitizen(&g_opContext, IL_KEYTYPE_01_PIN, pszPinCode);
  if(dwRetCode != 0)
  {
    // ����������� ������
		if((dwRetCode < ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_1) || (dwRetCode >= ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_1 + PIN_TRIES_MAX))
      CloseCard(); //��������� ���������� ������� ����� ���, ����� �������������
    
    //������ �-�� ���������� ������� ����� ���
    *pbPinRestTriesOut = (BYTE)(dwRetCode - ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_1 + 1);
    
    return ConvertErrorCode(dwRetCode, CF_AUTHORISE);
  }

  return ILRET_OK;
}

//������ ������ ������ ��������� �����
UECLIB_API DWORD ReadPrivateData(PrivateData* pPrivateData)
{
  if (pPrivateData == NULL)
    return ConvertErrorCode(ILRET_SYS_INVALID_ARGUMENT, CF_READ_DATA);

  // ������� ������-��������� ������ c������-����� [1-2](���, ���, ����� ������ � �.�.)
  IL_CHAR szBlockDataDescr[1024*2+1];
  IL_WORD wBlockDataDescrLen = sizeof(szBlockDataDescr);
  memset(szBlockDataDescr, 0, wBlockDataDescrLen);
  DWORD dwRetCode = opApiGetCardBlockDataDescr(&g_opContext, "1-2", szBlockDataDescr, &wBlockDataDescrLen);
  if (dwRetCode != ILRET_OK)
  {
    // ��������� ��������
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_READ_DATA);
  }

  // ������� �������� ����� [1-2]
  ParsePrivateData(pPrivateData, szBlockDataDescr);

  // ������� ������-��������� ������ c������-����� [1-4](�����, �������, �������� � �.�.)
  wBlockDataDescrLen = sizeof(szBlockDataDescr);
  memset(szBlockDataDescr, 0, wBlockDataDescrLen);
  dwRetCode = opApiGetCardBlockDataDescr(&g_opContext, "1-4", szBlockDataDescr, &wBlockDataDescrLen);
  if (dwRetCode != ILRET_OK)
  {
    // ��������� ��������
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_READ_DATA);;
  }

  // ������� �������� ����� [1-4]
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
  //�����
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szSNILS, &dwOffset);
  //����� ������ ���
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szPolicyNumberOMS, &dwOffset);
  //����� ��������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szIssuerAddress, &dwOffset);
  //���� ��������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szBirthDate, &dwOffset);
  //����� ��������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szBirthPlace, &dwOffset);
  //���
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szGender, &dwOffset);
  //�������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szLastName, &dwOffset);
  //���
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szFirstName, &dwOffset);
  //��������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szMiddleName, &dwOffset);
  //�����
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szAddress, &dwOffset);
  //�������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szTelNumber, &dwOffset);
  //E-Mail
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szEmail, &dwOffset);
  //���
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szINN, &dwOffset);
  //����� ������������� �����
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDriveLicenseNumber, &dwOffset);
  //����� ����������� �������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szTransportNumber, &dwOffset);
  //��� ��������� ���
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentType, &dwOffset);
  //����� ���������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentSeries, &dwOffset);
  //����� ���������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentNumber, &dwOffset);
  //���� ������ ���������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentIssueDate, &dwOffset);
  //���� ��������� �������� �
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentEndDate, &dwOffset);
  //��� �����
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentIssueAuthority, &dwOffset);
  //��� �������������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentDepartmentCode, &dwOffset);
  //�������������� ��������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, privateData.szDocumentAdditionalData, &dwOffset);

  return ILRET_OK;
}

UECLIB_API DWORD ReadMainOMSData(OMSData* pOMSData)
{
  if (pOMSData == NULL)
    return ConvertErrorCode(ILRET_SYS_INVALID_ARGUMENT, CF_READ_DATA);

  // ������� ������-��������� ������ c������-����� [3-1](�������� � ������� ������)
  IL_CHAR szBlockDataDescr[1024*2+1];
  IL_WORD wBlockDataDescrLen = sizeof(szBlockDataDescr);
  memset(szBlockDataDescr, 0, wBlockDataDescrLen);
  DWORD dwRetCode = opApiGetCardBlockDataDescr(&g_opContext, "3-1", szBlockDataDescr, &wBlockDataDescrLen);
  if (dwRetCode != ILRET_OK)
  {
    // ��������� ��������
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_READ_DATA);
  }

  // ������� �������� ����� [3-1]
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
  //���� �����������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, omsData.szOGRN, &dwOffset);
  //����� �����������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, omsData.szOKATO, &dwOffset);
  //���� �����������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, omsData.szInsuranceDate, &dwOffset);
  //���� ��������� �����������
  WriteFieldToBuffer(pDstString, pdwDstStringSize, omsData.szInsuranceEndDate, &dwOffset);

  return ILRET_OK;
}

//������ ������ � ��������� �������
UECLIB_API DWORD ReadOMSData(OMSData* pOMSData, DWORD* pdwOMSDataSize)
{
  if (pOMSData == NULL || pdwOMSDataSize == NULL || *pdwOMSDataSize < 11)
    return ConvertErrorCode(ILRET_SYS_INVALID_ARGUMENT, CF_READ_DATA);

  //������ ������ � ������� ��������� ������
  DWORD dwRetCode = ReadMainOMSData(pOMSData);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_READ_DATA);
  
  //��� ������� ������ � ������� ��������� ������->�������
  if (*pdwOMSDataSize == 1)
    return ILRET_OK;
  
  *pdwOMSDataSize = 1; //��������� ������ � ������� ��������� ������

  // ������� ������-��������� ������ c������-����� [3-2](�������� � ���������� �������)
  IL_CHAR szBlockDataDescr[1024*2+1];
  IL_WORD wBlockDataDescrLen = sizeof(szBlockDataDescr);
  wBlockDataDescrLen = sizeof(szBlockDataDescr);
  memset(szBlockDataDescr, 0, wBlockDataDescrLen);
  dwRetCode = opApiGetCardBlockDataDescr(&g_opContext, "3-2", szBlockDataDescr, &wBlockDataDescrLen);
  if (dwRetCode != ILRET_OK)
  {
    // ��������� ��������
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_READ_DATA);
  }
  
  // ������� �������� ����� [3-2]
  ParseOldOMSData(pOMSData, szBlockDataDescr, pdwOMSDataSize);

  return ILRET_OK;
}

//���������� ������ � ����� ��������� ������, � ����� ������� � ����������
UECLIB_API DWORD WriteOMSData(OMSData* pNewOMSData)
{
  if (pNewOMSData == NULL || pNewOMSData->szOGRN == NULL || pNewOMSData->szOKATO == NULL || 
      pNewOMSData->szInsuranceDate == NULL || pNewOMSData->szInsuranceEndDate == NULL ||
      pNewOMSData->szLastName == NULL || pNewOMSData->szFirstName == NULL || pNewOMSData->szMiddleName == NULL || pNewOMSData->szBirthDate == NULL)
    return ConvertErrorCode(ILRET_SYS_INVALID_ARGUMENT, CF_WRITE_DATA);

  //������ ��������� ������, ��� ����������� ����������� ������ �� �����
  PrivateData privateData;
  DWORD dwRetCode = ReadPrivateData(&privateData);
  if (dwRetCode != ILRET_OK)
    return dwRetCode;

  //��������� �������
  if (_wcsicmp(privateData.szLastName, pNewOMSData->szLastName) != 0) 
    return ConvertErrorCode(ILRET_OPLIB_PRIVATE_DATA_BELONGS_TO_OTHER_PERSON, CF_WRITE_DATA);
  
  //��������� �����
  if (_wcsicmp(privateData.szFirstName, pNewOMSData->szFirstName) != 0) 
    return ConvertErrorCode(ILRET_OPLIB_PRIVATE_DATA_BELONGS_TO_OTHER_PERSON, CF_WRITE_DATA);
  
  //��������� ��������
  if (_wcsicmp(privateData.szMiddleName, pNewOMSData->szMiddleName) != 0) 
    return ConvertErrorCode(ILRET_OPLIB_PRIVATE_DATA_BELONGS_TO_OTHER_PERSON, CF_WRITE_DATA);

  //��������� ���� ��������
  if (_wcsicmp(privateData.szBirthDate, pNewOMSData->szBirthDate) != 0) 
    return ConvertErrorCode(ILRET_OPLIB_PRIVATE_DATA_BELONGS_TO_OTHER_PERSON, CF_WRITE_DATA);

  //������ ������ � ����������, ���������� �� �����
  OMSData* pOldOMSData = new OMSData[11];
  DWORD dwSize = 11;
  dwRetCode = ReadOMSData(pOldOMSData, &dwSize);
  if (dwRetCode != ILRET_OK)
    return ConvertErrorCode(dwRetCode, CF_WRITE_DATA);

  //����� ���������, ������ �� ��������
  if (dwSize == 11)
  {
    //��������� ��������
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(ILRET_OPLIB_OMS_HISTORY_FULL, CF_WRITE_DATA);
  }

  //������� ������ ��� ������ �������
  WCHAR wszHistoryData[1024];
  swprintf(wszHistoryData, L"0%s0%s%s", pOldOMSData[0].szOGRN, 
    pOldOMSData[0].szOKATO, pOldOMSData[0].szInsuranceDate);

  //������ �������
  CHAR szHistoryData[1024];
  WideCharToMultiByte(CP_ACP, 0, wszHistoryData, -1, szHistoryData, 1024, NULL, NULL);
  CHAR szHistoryCardData[1024];
  sprintf(szHistoryCardData, "%s=%s", "3-2-0|14", szHistoryData);
  dwRetCode = opApiWriteCardData(&g_opContext, szHistoryCardData);
  if (dwRetCode != ILRET_OK)
  {
    // ��������� ��������
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_WRITE_DATA);
  }

  //������ ���� ��� ������ ������
  CHAR szData[100];
  CHAR szCardData[200];
  WideCharToMultiByte(CP_ACP, 0, pNewOMSData->szOGRN, -1, szData, 100, NULL, NULL);
  sprintf(szCardData, "%s=0%s", "3-1-0|7", szData);
  dwRetCode = opApiWriteCardData(&g_opContext, szCardData);
  if (dwRetCode != ILRET_OK)
  {
    // ��������� ��������
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_WRITE_DATA);
  }
  
  //������ ����� ��� ������ ������
  WideCharToMultiByte(CP_ACP, 0, pNewOMSData->szOKATO, -1, szData, 100, NULL, NULL);
  sprintf(szCardData, "%s=0%s", "3-1-7|3", szData);
  dwRetCode = opApiWriteCardData(&g_opContext, szCardData);
  if (dwRetCode != ILRET_OK)
  {
    // ��������� ��������
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_WRITE_DATA);
  }

  //������ ���� ����������� ��� ������ ������
  WideCharToMultiByte(CP_ACP, 0, pNewOMSData->szInsuranceDate, -1, szData, 100, NULL, NULL);
  sprintf(szCardData, "%s=%s", "3-1-10|4", szData);
  dwRetCode = opApiWriteCardData(&g_opContext, szCardData);
  if (dwRetCode != ILRET_OK)
  {
    // ��������� ��������
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_WRITE_DATA);
  }

  //������ ���� ��������� ����������� ��� ������ ������
  WideCharToMultiByte(CP_ACP, 0, pNewOMSData->szInsuranceEndDate, -1, szData, 100, NULL, NULL);
  sprintf(szCardData, "%s=%s", "3-1-14|4", szData);
  dwRetCode = opApiWriteCardData(&g_opContext, szCardData);
  if (dwRetCode != ILRET_OK)
  {
    // ��������� ��������
    opApiDeinitOperation(&g_opContext);
    return ConvertErrorCode(dwRetCode, CF_WRITE_DATA);
  }

  return ILRET_OK;
}

//��������� �����
UECLIB_API DWORD CloseCard(void)
{
  // ��������� ��������
  opApiDeinitOperation(&g_opContext);
  
  //������� �������� ��������
  opCtxSetClean(&g_opContext);

  //�������� �����
  if (g_hCrd.hRdr != NULL)
  {
    clCardClose(&g_hCrd);
    //��������������� ������
    flDeinitReader(&g_hCrd);
  }

  memset(&g_hCrd, 0, sizeof(g_hCrd));

  return ILRET_OK;
}

UECLIB_API void SetSystemTraceMode(BOOL systemTraceMode)
{
  g_bSystemTraceMode = systemTraceMode;
}

//���������� �������� ������ �� �� ����
UECLIB_API void GetErrorDescription(DWORD dwRetCode, CHAR* pszErrorDescription)
{
  if (pszErrorDescription == NULL)
    return;

  if (g_bSystemTraceMode)
  {
    //��������� ������, ������ �� ������������� �����
    if (dwRetCode == ILRET_OPLIB_OMS_HISTORY_FULL)
    {
      strcpy(pszErrorDescription, "������ �� ���������, ��� ��� ����� ��������� ���������");
      return;
    }
    else
    if (dwRetCode == ILRET_OPLIB_PRIVATE_DATA_BELONGS_TO_OTHER_PERSON)
    {
      strcpy(pszErrorDescription, "����� � ���������� ����������� ����� ����");
      return;
    }

    strcpy(pszErrorDescription, opApiGetErrorDescr(dwRetCode));
  }
  else
  {
    if (dwRetCode == ILRET_CARD_OPEN_ERROR)
      strcpy(pszErrorDescription, "������ �������� �����");
    if (dwRetCode == ILRET_AUTHORISATION_ERROR)
      strcpy(pszErrorDescription, "������ �����������");
    if (dwRetCode == ILRET_READ_DATA_ERROR)
      strcpy(pszErrorDescription, "������ ������ ������");
    if (dwRetCode == ILRET_WRITE_DATA_ERROR)
      strcpy(pszErrorDescription, "������ ������ ������");
    if (dwRetCode == ILRET_CARD_READER_ERROR)
      strcpy(pszErrorDescription, "������ ����-������");
    if (dwRetCode == ILRET_CARD_ERROR)
      strcpy(pszErrorDescription, "������ �����");
    if (dwRetCode == ILRET_CERTIFICATE_FORMAT_ERROR)
      strcpy(pszErrorDescription, "������ �������� ������� �����������");
    if (dwRetCode == ILRET_CRYPTO_PROVIDER_ERROR)
      strcpy(pszErrorDescription, "������ ����������������");
    if (dwRetCode == ILRET_WRONG_PASSWORD_ERROR)
      strcpy(pszErrorDescription, "�������� ������!");
    if (dwRetCode == ILRET_CARD_BLOCKED)
        strcpy(pszErrorDescription, "����� �������������");
    if (dwRetCode == ILRET_CARD_REMOVED_ERROR)
      strcpy(pszErrorDescription, "����� ������");
  }
}

//���������� ������ ������������ ����-�������
//pReaderInfo - ��������� �� ������ ��������, � ������� ����� �������� ������
//pdwReaderInfoCount - ��������� �� ����������,  � ������� ������� ������� ������ �������,
//������������ ����� ��������� pReaderInfo � � ������� ������������ ������ ������������ �������
UECLIB_API void GetReaderList(CardReaderInfo* pReaderInfo, DWORD* pdwReaderInfoCount)
{
  if (pdwReaderInfoCount == NULL)
    return;

  //����������� ������ ���������
  SCARDCONTEXT hContext = NULL;
  LPTSTR pszReadersNames = NULL;
  DWORD cch = SCARD_AUTOALLOCATE;
  if (SCardListReaders(hContext, NULL, (LPTSTR)&pszReadersNames, &cch) != SCARD_S_SUCCESS)
  {
    *pdwReaderInfoCount = 0;
    return;
  }
  
  //������ ����� ���������
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
        
    //������� � ���������� ��������
    pszReaderName = pszReaderName + wcslen((WCHAR*)pszReaderName) + 1;

    dwReaderIndex++;
  }

  *pdwReaderInfoCount = dwReaderIndex;
    
  //����������� ������
  SCardFreeMemory( hContext, pszReadersNames );
}

//���������� ������ ������������ ����-�������
//��� ������ ����� �������� � ������
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
  
  //��������� ����� �������� ����������
  DWORD dwCurrentComputerName = 1024*5;
  WCHAR wszCurrentComputerName[1024*5];
  GetComputerName(wszCurrentComputerName, &dwCurrentComputerName);
  wcscpy(pwszComputerName, wszCurrentComputerName);
}

//���������� ��� ������� ������ � ������ ��������� ������
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



