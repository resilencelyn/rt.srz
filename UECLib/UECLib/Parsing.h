#pragma once

// Структура элемента данных
typedef struct
{
	CHAR szId[10];		  // 1-4-DF25
	INT nType;			    // 0
  INT  nMaxLen;			  // 14
	CHAR szHeader[100];	// Телефон
	CHAR szData[1024];	// (910)4826421
} API_DATA_ELEM;

BOOL ParsePrivateData(PrivateData* pPrivateData, CHAR* szBlockDataDescr);

BOOL ParseMainOMSData(OMSData* pOMSData, CHAR* szBlockDataDescr);

BOOL ParseOldOMSData(OMSData* pOMSData, CHAR* pszBlockDataDescr, DWORD* pdwOMSDataSize);

BOOL ParseBlockDataDescription(CHAR* szBlockDataDescr, INT nElemIndex, API_DATA_ELEM* pDataElem);

void SortOMSData(OMSData* pOMSData, DWORD dwOMSDataSize);


