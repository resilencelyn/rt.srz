#pragma once

//��������� ����� ��� ������/������
// pwszUecServiceToken ����� ������� � ������� �������� (empty - ������ � terminal.ini)
UECLIB_API DWORD OpenCard(WCHAR* pwszUecServiceToken);

//��������� ����� ��� ������/������
// pwszUecServiceToken ����� ������� � ������� �������� (empty - ������ � terminal.ini)
// pwszCIN - �������������� ����� �����
UECLIB_API DWORD OpenCardWithHandle(WCHAR* pwszUecServiceToken, WCHAR* pwszCIN);

//��������� ����� ��� ������/������
//wszReaderName - ��� ������
//szOKO1OpenCert - ���������� ��������� ����� ���1
//szTerminalOpenCert - ���������� ��������� ����� ���������
//szUC1OpenCert - ���������� ��������� ��������������� ������
//szTerminalClosedCertGOST - ���������� ��������� ����� ���������
//pwszCIN - �������������� ����� �����
UECLIB_API DWORD OpenCardWithHandleByGlobalData(WCHAR* wszReaderName, BYTE* szOKO1OpenCert, BYTE* szTerminalOpenCert, 
  BYTE* szUC1OpenCert, BYTE* szTerminalClosedCert, WCHAR* pwszCIN);

//���������� �� PIN ����
//���������� ���������� ���������� �������
UECLIB_API DWORD Authorise(CHAR* pszPinCode, BYTE* pbPinRestTriesOut);

//������ ������ ������ ��������� �����
//pPrivateData - ��������� �� ���������, � ������� ����� �������� ������
UECLIB_API DWORD ReadPrivateData(PrivateData* pPrivateData);

//������ ������ ������ ��������� �����
//��� ���� ������������ � ������ � ������������������ ����������� �� ���������� PrivateData
UECLIB_API DWORD ReadPrivateDataInString(WCHAR* pDstString, DWORD pdwDstStringSize);

//������ ������ � ������� ��������� ������
//pOMSData - ��������� ���������, � ������� ����� �������� ������
UECLIB_API DWORD ReadMainOMSData(OMSData* pOMSData);

//������ ������ � ������� ��������� ������
//��� ���� ������������ � ������ � ������������������ ����������� �� ���������� OMSData
UECLIB_API DWORD ReadMainOMSDataInString(WCHAR* pDstString, DWORD pdwDstStringSize);

//������ ������ � ��������� �������
//pOMSData - ��������� �� ������ ��������, � ������� ����� �������� ������
//pdwOMSDataSize - ��������� �� ����������,  � ������� ������� ������� ������ �������,
//������������ ����� ��������� pOMSData � � ������� ������������ ������ ������������ �������
UECLIB_API DWORD ReadOMSData(OMSData* pOMSData, DWORD* pdwOMSDataSize);

//���������� ������ � ����� ��������� ������, � ����� ������� � ����������
UECLIB_API DWORD WriteOMSData(OMSData* pNewOMSData);

//��������� �����
UECLIB_API DWORD CloseCard(void);

//������������� ���� ���������� ����� �������� ��������� ����� ������
//systemTraceMode = TRUE -  ������������ ��������� ������
//systemTraceMode = FALSE - ������������ ���������� ������
UECLIB_API void SetSystemTraceMode(BOOL systemTraceMode);

//���������� �������� ������ �� �� ����
UECLIB_API void GetErrorDescription(DWORD dwErrorCode, CHAR* pszErrorDescription);

//���������� ������ ������������ ����-�������
//pReaderInfo - ��������� �� ������ ��������, � ������� ����� �������� ������
//pdwReaderInfoCount - ��������� �� ����������,  � ������� ������� ������� ������ �������,
//������������ ����� ��������� pReaderInfo � � ������� ������������ ������ ������������ �������
UECLIB_API void GetReaderList(CardReaderInfo* pReaderInfo, DWORD* pdwReaderInfoCount);

//���������� ������ ������������ ����-�������
//��� ������ ����� �������� � ������
UECLIB_API void GetReaderListInString(WCHAR* pDstString, DWORD pdwDstStringSize);

//���������� ��� ������� ������ � ������ ��������� ������
UECLIB_API void GetCurrentComputerName(WCHAR* pwszComputerName);

//���������� ��� ������� ������ � ������ ��������� ������
UECLIB_API void GetCurrentComputerNameWithTakingRemoteSession(WCHAR* pwszComputerName);




