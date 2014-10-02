#include "stdafx.h"
#include <wchar.h>
#include <stdlib.h>
#include <time.h>
#include "Structures.h"
#include "Parsing.h"


BOOL ParseBlockDataDescription(CHAR* szBlockDataDescr, INT nElemIndex, API_DATA_ELEM* pDataElem)
{
	int  i;
	char *pStr;

	memset(pDataElem, 0, sizeof(API_DATA_ELEM));

	// �������������� ��������� �� ��������� �������� ������ ���������� �������
	for(i = 0, pStr = szBlockDataDescr; ; i++)
	{
		if(i > 0) 
			pStr = strchr(pStr, '\n');
		
		if(!pStr++ || *pStr == 0)
			return false;	// �������������� ������!!!

		if(i == nElemIndex)
		{
			if(!i)
        --pStr;
			break;	// ��������� ��������������� �� ���������!!!
		}
	}

	// �������������� ������� ����������
	// 1) Id: "1-4-DF25"
	for(i = 0; *pStr != '|'; i++)
		pDataElem->szId[i] = *pStr++;

	//++++ 2) TPath: "0,0,0"
	pStr = strchr(++pStr, '|'); 

	// 3) Type: 0-ASCII
	sscanf(++pStr, "%u", &pDataElem->nType);
	pStr = strchr(pStr, '|');

	// 4) MaxLen: 14
	sscanf(++pStr, "%u", &pDataElem->nMaxLen);
	pStr = strchr(pStr, '|');

	// 5) Header: "�������"
	for(i = 0, ++pStr; *pStr != '|'; i++)
		pDataElem->szHeader[i] = *pStr++;

	// 6) Data: "(910)4826421"
	for(i = 0, ++pStr; *pStr != '|'; i++)
		pDataElem->szData[i] = *pStr++;

	return TRUE;
}

BOOL ParsePrivateData(PrivateData* pPrivateData, CHAR* pszBlockDataDescr)
{
  if (pPrivateData == NULL || pszBlockDataDescr == NULL)
    return FALSE;
  
  // ������� ��������
  API_DATA_ELEM dataElem;
  for(INT nElemIndex = 0; ; nElemIndex++)
	{
		if(!ParseBlockDataDescription(pszBlockDataDescr, nElemIndex, &dataElem))
			break;

    WCHAR* pszFieldForWrite = NULL;
    if (!strcmp(dataElem.szId, "1-2-DF27")) //�����
      pszFieldForWrite = pPrivateData->szSNILS;
    else
    if (!strcmp(dataElem.szId, "1-2-DF2B")) //����� ������ ���
      pszFieldForWrite = pPrivateData->szPolicyNumberOMS;
    else
    if (!strcmp(dataElem.szId, "1-2-DF23")) //����� ��������
      pszFieldForWrite = pPrivateData->szIssuerAddress;
    else
    if (!strcmp(dataElem.szId, "1-2-5F2B")) //���� ��������
      pszFieldForWrite = pPrivateData->szBirthDate;
    else
    if (!strcmp(dataElem.szId, "1-2-DF24")) //����� ��������
      pszFieldForWrite = pPrivateData->szBirthPlace;
    else
    if (!strcmp(dataElem.szId, "1-2-5F35")) //���
      pszFieldForWrite = pPrivateData->szGender;
    if (!strcmp(dataElem.szId, "1-2-DF2D")) //�������
      pszFieldForWrite = pPrivateData->szLastName;
    else
    if (!strcmp(dataElem.szId, "1-2-DF2E")) //���
      pszFieldForWrite = pPrivateData->szFirstName;
    else
    if (!strcmp(dataElem.szId, "1-2-DF2F")) //��������
      pszFieldForWrite = pPrivateData->szMiddleName;
    else
    if (!strcmp(dataElem.szId, "1-4-5F42")) //�����
      pszFieldForWrite = pPrivateData->szAddress;
    else
    if (!strcmp(dataElem.szId, "1-4-DF25")) //�������
      pszFieldForWrite = pPrivateData->szTelNumber;
    else
    if (!strcmp(dataElem.szId, "1-4-DF26")) //E-Mail
      pszFieldForWrite = pPrivateData->szEmail;
    else
    if (!strcmp(dataElem.szId, "1-4-DF5C")) //���
      pszFieldForWrite = pPrivateData->szINN;
    else
    if (!strcmp(dataElem.szId, "1-4-DF5D")) //����� ������������� �������������
      pszFieldForWrite = pPrivateData->szDriveLicenseNumber;
    else
    if (!strcmp(dataElem.szId, "1-4-DF5D")) //����� ����������� ����������
      pszFieldForWrite = pPrivateData->szTransportNumber;
    else
    if (!strcmp(dataElem.szId, "1-4-9F7F")) //��� ���������
      pszFieldForWrite = pPrivateData->szDocumentType;
    else
    if (!strcmp(dataElem.szId, "1-4-DF4A")) //����� ���������
      pszFieldForWrite = pPrivateData->szDocumentSeries;
    else
    if (!strcmp(dataElem.szId, "1-4-DF4B")) //����� ���������
      pszFieldForWrite = pPrivateData->szDocumentNumber;
    else
    if (!strcmp(dataElem.szId, "1-4-5F25")) //���� ������
      pszFieldForWrite = pPrivateData->szDocumentIssueDate;
    else
    if (!strcmp(dataElem.szId, "1-4-5F24")) //���� ��������� ��������
      pszFieldForWrite = pPrivateData->szDocumentEndDate;
    else
    if (!strcmp(dataElem.szId, "1-4-DF4C")) //��� �����
      pszFieldForWrite = pPrivateData->szDocumentIssueAuthority;
    else
    if (!strcmp(dataElem.szId, "1-4-DF4D")) //��� �������������
      pszFieldForWrite = pPrivateData->szDocumentDepartmentCode;
    else
    if (!strcmp(dataElem.szId, "1-4-DF4F")) //�������������� ��������
      pszFieldForWrite = pPrivateData->szDocumentAdditionalData;

    //������ � ���������
    if (pszFieldForWrite != NULL)
      MultiByteToWideChar(CP_ACP, 0, dataElem.szData, -1, (LPWSTR)pszFieldForWrite, strlen(dataElem.szData)*2);
  }

  return TRUE;
}

BOOL ParseMainOMSData(OMSData* pOMSData, CHAR* pszBlockDataDescr)
{
  if (pOMSData == NULL || pszBlockDataDescr == NULL)
    return FALSE;
  
  // ������� ��������
  API_DATA_ELEM dataElem;
  for(INT nElemIndex = 0; ; nElemIndex++)
	{
		if(!ParseBlockDataDescription(pszBlockDataDescr, nElemIndex, &dataElem))
			break;

    WCHAR* pszFieldForWrite = NULL;
    if (!strcmp(dataElem.szId, "3-1-0")) //����
    {
      MultiByteToWideChar(CP_ACP, 0, dataElem.szData+1, -1, (LPWSTR)pOMSData->szOGRN, strlen(dataElem.szData)*2-2);
    }
    else
    if (!strcmp(dataElem.szId, "3-1-7")) //�����
    {
      MultiByteToWideChar(CP_ACP, 0, dataElem.szData+1, -1, (LPWSTR)pOMSData->szOKATO, strlen(dataElem.szData)*2-2);
    }
    else
    if (!strcmp(dataElem.szId, "3-1-10")) //���� �����������
    {
      MultiByteToWideChar(CP_ACP, 0, dataElem.szData, -1, (LPWSTR)pOMSData->szInsuranceDate, strlen(dataElem.szData)*2);
    }
    else
    if (!strcmp(dataElem.szId, "3-1-14")) //���� ��������� �����������
    {
      MultiByteToWideChar(CP_ACP, 0, dataElem.szData, -1, (LPWSTR)pOMSData->szInsuranceEndDate, strlen(dataElem.szData)*2);
    }
  }
  
  return TRUE;
}

BOOL ParseOldOMSData(OMSData* pOMSData, CHAR* pszBlockDataDescr, DWORD* pdwOMSDataSize)
{
  if (pOMSData == NULL || pszBlockDataDescr == NULL || pdwOMSDataSize == NULL)
    return FALSE;
  
  // ������� ��������
  INT nOMSDataIndex = 1;
  API_DATA_ELEM dataElem;
  for(INT nElemIndex = 0; ; nElemIndex++)
  {
    if(!ParseBlockDataDescription(pszBlockDataDescr, nElemIndex, &dataElem))
			break;
    
    //28 - ����� ������, ���������� ����, ����� � ���� ������ �����������
    if (strlen(dataElem.szData) != 28)
      continue;
    
    //������ �� ���� ��������� ������ �������
    for (INT nIdValue = 1; nIdValue <= 10; nIdValue++)
    {
      CHAR szId[10];
      sprintf_s(szId, 10, "3-2-%d", nIdValue); 
      if (strcmp(dataElem.szId, szId))
        continue;
      
      CHAR* pszData = dataElem.szData; 
        
      //���������� ����, 14 �������� + 1 ����� ������
      CHAR szOGRN[14+1];
      strncpy(szOGRN, pszData, 14);
      szOGRN[14] = '\0';
      MultiByteToWideChar(CP_ACP, 0, szOGRN+1, -1, (LPWSTR)pOMSData[nOMSDataIndex].szOGRN, strlen(szOGRN)*2-2);

      //���������� �����, 6 �������� + 1 ����� ������
      CHAR szOKATO[6+1];
      strncpy(szOKATO, pszData + 14, 6);
      szOKATO[6] = '\0';
      MultiByteToWideChar(CP_ACP, 0, szOKATO+1, -1, (LPWSTR)pOMSData[nOMSDataIndex].szOKATO, strlen(szOKATO)*2-2);
        
      //���������� ���� ������ �����������, 8 �������� + 1 ����� ������
      CHAR szInsuranceData[8+1];
      strncpy(szInsuranceData, pszData + 14 + 6, 8);
      szInsuranceData[8] = '\0';
      MultiByteToWideChar(CP_ACP, 0, szInsuranceData, -1, 
        (LPWSTR)pOMSData[nOMSDataIndex].szInsuranceDate, strlen(szInsuranceData)*2);

      nOMSDataIndex++;
    }
  }

  //���������� ����� �������� � �������
  if (nOMSDataIndex > 1)
    *pdwOMSDataSize = nOMSDataIndex;

  //����������
  SortOMSData(pOMSData, *pdwOMSDataSize);

  return TRUE;
}

time_t ParseDate(WCHAR* wszDate)
{
  if (wszDate == NULL)
    return 0;
  
  tm date;
  memset(&date, 0, sizeof(date));

  CHAR szDate[100];
  WideCharToMultiByte(CP_ACP, 0, wszDate, -1, szDate, 100, NULL, NULL);
  
  //���
  CHAR szYear[4+1];
  strncpy(szYear, szDate, 4);
  szYear[4] = '\0';
  INT nYear = atoi(szYear);
  date.tm_year = nYear - 1900;

  //�����
  CHAR szMonth[2+1];
  strncpy(szMonth, szDate + 4, 2);
  szMonth[2] = '\0';
  INT nMonth = atoi(szMonth);
  date.tm_mon = nMonth - 1;

  //����
  CHAR szDay[2+1];
  strncpy(szDay, szDate + 4 + 2, 2);
  szDay[2] = '\0';
  INT nDay = atoi(szDay);
  date.tm_mday = nDay;
  
  return _mktime64(&date);
}

void SortOMSData(OMSData* pOMSData, DWORD dwOMSDataSize)
{
  for (DWORD i = 0; i < dwOMSDataSize - 1; i++)
  {
    for (DWORD k = 0; k < dwOMSDataSize - 1; k++)
      
      if (ParseDate(pOMSData[k].szInsuranceDate) < ParseDate(pOMSData[k+1].szInsuranceDate))
      {
          OMSData tempOMSData;
          tempOMSData = pOMSData[k];
          pOMSData[k] = pOMSData[k + 1];
          pOMSData[k + 1] = tempOMSData;
      }
  }
}