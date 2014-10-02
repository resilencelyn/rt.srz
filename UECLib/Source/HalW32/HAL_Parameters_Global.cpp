#include <windows.h>
#include <stdio.h>
#include <mscoree.h>
#include "HAL_Parameter.h"
#include "HAL_Common.h"
#include "il_error.h"
#include "CertType.h"


BOOL g_bUseGlobalConfigurationData = FALSE;

//��� ������
IL_CHAR g_szReaderName[8*1024];             
IL_DWORD g_dwReaderNameLength = -1;

//���������� ��������� ����� ���
IL_BYTE  g_szOKO1OpenCert[20*1024];       
IL_DWORD g_dwOKO1OpenCertLength = -1;

//���������� ��������� ����� ���������
IL_BYTE g_szTerminalOpenCert[20*1024];  
IL_DWORD g_dwTerminalOpenCertLength = -1; 

//���������� ��������� ����� ��
IL_BYTE g_szUC1OpenCert[20*1024];
IL_DWORD g_dwUC1OpenCertLength = -1;

//���������� ��������� ����� ���������
IL_BYTE g_szTerminalClosedCert[20*1024];
IL_DWORD g_dwTerminalClosedCertLength = -1;

IL_FUNC void prmSetGlobalConfigurationData(WCHAR* pwszReaderName, IL_BYTE* szOKO1OpenCert, IL_BYTE* szTerminalOpenCert, 
  IL_BYTE* szUC1OpenCert, IL_BYTE* szTerminalClosedCert)
{
  if (pwszReaderName == NULL || szOKO1OpenCert == NULL || szTerminalOpenCert == NULL || szUC1OpenCert == NULL || szTerminalClosedCert == NULL)
  {
    g_bUseGlobalConfigurationData = FALSE;
    return;
  }
  
  //��� ������
  WideCharToMultiByte(CP_ACP, 0, pwszReaderName, -1, (LPSTR)g_szReaderName, 8192, NULL, NULL); 
  g_dwReaderNameLength = strlen(g_szReaderName);
  
  //���������� ��������� ����� ���
  if(szOKO1OpenCert && hex2bin((IL_CHAR*)szOKO1OpenCert, g_szOKO1OpenCert, &g_dwOKO1OpenCertLength))
    g_dwOKO1OpenCertLength = -1;
  
  //���������� ��������� ����� ���������
  if(szTerminalOpenCert && hex2bin((IL_CHAR*)szTerminalOpenCert, g_szTerminalOpenCert, &g_dwTerminalOpenCertLength))
    g_dwTerminalOpenCertLength = -1;
  
  //���������� ��������� ����� ��
  if(szUC1OpenCert && hex2bin((IL_CHAR*)szUC1OpenCert, g_szUC1OpenCert, &g_dwUC1OpenCertLength))
    g_dwUC1OpenCertLength = -1;
  
  //���������� ��������� ����� ���������
  if(szTerminalClosedCert && hex2bin((IL_CHAR*)szTerminalClosedCert, g_szTerminalClosedCert, &g_dwTerminalClosedCertLength))
    g_dwTerminalClosedCertLength = -1;
  
  g_bUseGlobalConfigurationData = TRUE;
}

//���������� ��������
IL_FUNC IL_RETCODE prmGetParameterByGlobalConfigurationData(IL_WORD ilParam, IL_BYTE* pParamBuf, IL_DWORD* pdwParamLen)
{
  switch (ilParam)
  {
  case IL_PARAM_READERNAME: //��� ������
    {
      if (g_dwReaderNameLength == -1)
        return ILRET_PARAM_NOT_FOUND;
      if (pParamBuf)
        strcpy((IL_CHAR*)pParamBuf, g_szReaderName);
    }
    break;
  case IL_PARAM_USE_GOST:   //��� ������������
    {
      if (pParamBuf)
        *pParamBuf = 1; //���������� ������ ����
    }
    break;
  case IL_PARAM_SIFDID_GOST: //���������� ��������� ����� ���������
    {
      if (g_dwTerminalClosedCertLength == -1)
        return ILRET_INVALID_HEX_STRING_FORMAT;

      if (pParamBuf)
        memcpy(pParamBuf, g_szTerminalClosedCert, g_dwTerminalClosedCertLength);
      if (pdwParamLen)
        *pdwParamLen = g_dwTerminalClosedCertLength;
    }
    break;
  }

  return ILRET_OK;  
}

// ���������� ��������� �������� �����������
IL_FUNC IL_RETCODE prmGetParameterKeyVerByByGlobalConfigurationData(IL_WORD ilParam, IL_BYTE KeyVer, IL_BYTE ifGostCrypto, IL_BYTE* pCertBuf, IL_DWORD* pdwCertLen)
{
  switch (ilParam)
  {
  case IL_PARAM_CAID:   //���������� ��������� ����� ���
    {
      if (g_dwOKO1OpenCertLength == -1)
        return ILRET_INVALID_HEX_STRING_FORMAT;

      if (pCertBuf)
        memcpy(pCertBuf, g_szOKO1OpenCert, g_dwOKO1OpenCertLength);
      if (pdwCertLen)
        *pdwCertLen = g_dwOKO1OpenCertLength;
    }
    break;
  case IL_PARAM_CIFDID: //���������� ��������� ����� ���������
    {
      if (g_dwTerminalOpenCertLength == -1)
        return ILRET_INVALID_HEX_STRING_FORMAT;

      if (pCertBuf)
        memcpy(pCertBuf, g_szTerminalOpenCert, g_dwTerminalOpenCertLength);
      if (pdwCertLen)
        *pdwCertLen = g_dwTerminalOpenCertLength;
    }
    break;
  case IL_PARAM_CCAID: //���������� ��������� ����� ��
    {
      if (g_dwUC1OpenCertLength == -1)
        return ILRET_INVALID_HEX_STRING_FORMAT;

      if (pCertBuf)
        memcpy(pCertBuf, g_szUC1OpenCert, g_dwUC1OpenCertLength);
      if (pdwCertLen)
        *pdwCertLen = g_dwUC1OpenCertLength;
    }
    break;
  }
  return ILRET_OK;
}