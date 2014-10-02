#include <windows.h>
#include <stdio.h>
#include "HAL_Parameter.h"
#include "HAL_Common.h"
#include "il_error.h"
#include "CertType.h"
#ifdef _CLR_SUPPORT_  
  #include <mscoree.h>  
  #include <metahost.h>
#endif

//��������������� �����
WCHAR g_szUecServiceToken[5*1024] = {0};
IL_FUNC void prmSetUecServiceToken(WCHAR* pwszUecServiceToken)
{
  wcscpy(g_szUecServiceToken, pwszUecServiceToken);
}

#ifdef _CLR_SUPPORT_
  
#import "rt.uec.model.tlb" named_guids
#import "rt.uec.service.tlb" named_guids

//������ �� ������
rt_uec_service::IUecClientInterop* g_pUecService = NULL;

enum SertificateType
{
  /// <summary> ����������  �� �1 RSA</summary>
  ST_UC1RSA = 484,

  /// <summary> ���������� ��� �1 RSA</summary>
  ST_OKO1RSA = 485,

  /// <summary> ���������� ��������� RSA</summary>
  ST_TerminalRSA = 486,

  /// <summary> ����������  �� �1 ����</summary>
  ST_UC1GOST = 489,

  /// <summary> ���������� ��� �1 ����</summary>
  ST_OKO1GOST = 490,

  /// <summary> ���������� ��������� ����</summary>
  ST_TerminalGOST = 491,

  /// <summary> ���������� ��������� ����</summary>
  ST_PrivateTerminalGOST = 528
};

void GetCurrentComputerName(WCHAR* pwszBuffer, DWORD dwBufferSize)
{
  GetComputerNameW(pwszBuffer, &dwBufferSize);
}

HMODULE GetCurrentDllHandle()
{
  MEMORY_BASIC_INFORMATION info;
  size_t len = VirtualQueryEx(GetCurrentProcess(), (void*)GetCurrentDllHandle, &info, sizeof(info));
  return len ? (HMODULE)info.AllocationBase : NULL;
}

BOOL OpenConnection2UecService()
{
  if (g_pUecService != NULL)
    return true;

  CoInitialize(NULL);
  
  //�������� .NET ������ 
  HRESULT hResult = CoCreateInstance(rt_uec_service::CLSID_UecClientInterop, NULL, CLSCTX_INPROC_SERVER, 
     rt_uec_service::IID_IUecClientInterop, reinterpret_cast<void**> (&g_pUecService));
  if (FAILED(hResult) || g_pUecService == NULL)
  {
    return FALSE;
  }

  //�������� ����������
  return g_pUecService->OpenConnection(g_szUecServiceToken);
}

void CloseConnection2UecService()
{
  //�������� ����������
  if (g_pUecService != NULL)
    g_pUecService->CloseConnection();

  g_pUecService = NULL;

  CoUninitialize();
}

//���������� ��������
IL_FUNC IL_RETCODE prmGetParameterByService(IL_WORD ilParam, IL_BYTE* pParamBuf, IL_DWORD* pdwParamLen)
{
  //�������� ���������� � �������
  if (!OpenConnection2UecService())
    return ILRET_PARAM_NOT_FOUND;

  //��������� ����� �������� ����������
  DWORD dwCurrentComputerName = 1024*5;
  WCHAR szCurrentComputerName[1024*5];
  GetCurrentComputerName(szCurrentComputerName, dwCurrentComputerName);

  try
  {
    switch (ilParam)
    {
    case IL_PARAM_READERNAME: //��� ������
      {
        BSTR strReaderName = NULL;
        g_pUecService->GetCurrentReaderName(szCurrentComputerName, &strReaderName);
        WideCharToMultiByte(CP_ACP, 0, strReaderName, -1, (LPSTR)pParamBuf, *pdwParamLen, NULL, NULL);
        SysFreeString(strReaderName);
      }
      break;
    case IL_PARAM_USE_GOST:   //��� ������������
      {
        rt_uec_model::CryptographyType type = rt_uec_model::CryptographyType_GOST;
        g_pUecService->GetCurrentCryptographyType(szCurrentComputerName, &type);
        if (type == rt_uec_model::CryptographyType_RSA)
        {
          *pParamBuf = 0;
        }
        else
        if (type == rt_uec_model::CryptographyType_GOST)
        {
          *pParamBuf = 1;
        }
      }
      break;
    case IL_PARAM_SIFDID_GOST:
      {
        SAFEARRAY* saKey = NULL;
        g_pUecService->GetCertificateKey(szCurrentComputerName, 1, ST_PrivateTerminalGOST, &saKey);
        if (saKey == NULL)
          return ILRET_PARAM_NOT_FOUND;

        //����������� ������
        void* pArrayData;
        //Obtain safe pointer to the array
        SafeArrayAccessData(saKey, &pArrayData);
        //Copy the bitmap into our buffer
        IL_CHAR buf[20*1024] = {0};
        memcpy(buf, pArrayData, saKey->rgsabound[0].cElements); //Unlock the variant data
        // ����������� � �������� ������
        if(pParamBuf && hex2bin(buf, pParamBuf, pdwParamLen))
          return ILRET_INVALID_HEX_STRING_FORMAT;
        SafeArrayUnaccessData(saKey);
      }
      break;
    case IL_PARAM_PROT_LOGNAME:
      {
        BSTR strValue = NULL;
        g_pUecService->GetProtocolSettings(rt_uec_model::ProtocolSettingsEnum_LogName, &strValue);
        WideCharToMultiByte(CP_ACP, 0, strValue, -1, (LPSTR)pParamBuf, *pdwParamLen, NULL, NULL);
        SysFreeString(strValue);
      }
      break;
    case IL_PARAM_PROT_LOGFILE:
      {
        BSTR strValue = NULL;
        g_pUecService->GetProtocolSettings(rt_uec_model::ProtocolSettingsEnum_LogFile, &strValue);
        WideCharToMultiByte(CP_ACP, 0, strValue, -1, (LPSTR)pParamBuf, *pdwParamLen, NULL, NULL);
        SysFreeString(strValue);
      }
      break;
    case IL_PARAM_PROT_OUTPUT:	
      {
        BSTR strValue = NULL;
        g_pUecService->GetProtocolSettings(rt_uec_model::ProtocolSettingsEnum_Output, &strValue);
        WideCharToMultiByte(CP_ACP, 0, strValue, -1, (LPSTR)pParamBuf, *pdwParamLen, NULL, NULL);
        SysFreeString(strValue);
      }
      break;
    case IL_PARAM_PROT_READER:	
      {
        BSTR strValue = NULL;
        g_pUecService->GetProtocolSettings(rt_uec_model::ProtocolSettingsEnum_Reader, &strValue);
        WideCharToMultiByte(CP_ACP, 0, strValue, -1, (LPSTR)pParamBuf, *pdwParamLen, NULL, NULL);
        SysFreeString(strValue);
      }
      break;
    case IL_PARAM_PROT_CARD:		
      {
        BSTR strValue = NULL;
        g_pUecService->GetProtocolSettings(rt_uec_model::ProtocolSettingsEnum_Card, &strValue);
        WideCharToMultiByte(CP_ACP, 0, strValue, -1, (LPSTR)pParamBuf, *pdwParamLen, NULL, NULL);
        SysFreeString(strValue);
      }
      break;
    case IL_PARAM_PROT_CARDLIB:	
      {
        BSTR strValue = NULL;
        g_pUecService->GetProtocolSettings(rt_uec_model::ProtocolSettingsEnum_Cardlib, &strValue);
        WideCharToMultiByte(CP_ACP, 0, strValue, -1, (LPSTR)pParamBuf, *pdwParamLen, NULL, NULL);
        SysFreeString(strValue);
      }
      break;
    case IL_PARAM_PROT_FUNCLIB:	
      {
        BSTR strValue = NULL;
        g_pUecService->GetProtocolSettings(rt_uec_model::ProtocolSettingsEnum_Funclib, &strValue);
        WideCharToMultiByte(CP_ACP, 0, strValue, -1, (LPSTR)pParamBuf, *pdwParamLen, NULL, NULL);
        SysFreeString(strValue);
      }
      break;
    case IL_PARAM_PROT_OPLIB:		
      {
        BSTR strValue = NULL;
        g_pUecService->GetProtocolSettings(rt_uec_model::ProtocolSettingsEnum_Oplib, &strValue);
        WideCharToMultiByte(CP_ACP, 0, strValue, -1, (LPSTR)pParamBuf, *pdwParamLen, NULL, NULL);
        SysFreeString(strValue);
      }
      break;
    case IL_PARAM_PROT_SMLIB:		
      {
        BSTR strValue = L"00";
        WideCharToMultiByte(CP_ACP, 0, strValue, -1, (LPSTR)pParamBuf, *pdwParamLen, NULL, NULL);
        SysFreeString(strValue);
      }
      break;
    case IL_PARAM_PROT_TELLME:	
      {
        BSTR strValue = NULL;
        g_pUecService->GetProtocolSettings(rt_uec_model::ProtocolSettingsEnum_TellMe, &strValue);
        WideCharToMultiByte(CP_ACP, 0, strValue, -1, (LPSTR)pParamBuf, *pdwParamLen, NULL, NULL);
        SysFreeString(strValue);

      }
      break;
    case IL_PARAM_PROT_EXTERN:
      {
        BSTR strValue = NULL;
        g_pUecService->GetProtocolSettings(rt_uec_model::ProtocolSettingsEnum_Extern, &strValue);
        WideCharToMultiByte(CP_ACP, 0, strValue, -1, (LPSTR)pParamBuf, *pdwParamLen, NULL, NULL);
        SysFreeString(strValue);
      }
      break;
    }
  }
  catch (...)
  {
  }
  
  //�������� ����������
  CloseConnection2UecService();
  
  return ILRET_OK;  
}

// ���������� ��������� �������� �����������
IL_FUNC IL_RETCODE prmGetParameterKeyVerByService(IL_WORD ilParam, IL_BYTE KeyVer, IL_BYTE ifGostCrypto, IL_BYTE* pCertBuf, IL_DWORD* pdwCertLen)
{
  //�������� ���������� � �������
  if (!OpenConnection2UecService())
    return ILRET_PARAM_NOT_FOUND;

  //��������� ����� �������� ����������
  DWORD dwCurrentComputerName = 1024*5;
  WCHAR szCurrentComputerName[1024*5];
  GetCurrentComputerName(szCurrentComputerName, dwCurrentComputerName);

  SAFEARRAY* saKey = NULL;
  try
  {
    switch (ilParam)
    {
    case IL_PARAM_CAID:   //���������� ��������� ����� ���
      {
        g_pUecService->GetCertificateKey(szCurrentComputerName, KeyVer, ifGostCrypto ? ST_OKO1GOST : ST_OKO1RSA, &saKey);
      }
      break;
    case IL_PARAM_CIFDID: //���������� ��������� ����� ���������
      {
        g_pUecService->GetCertificateKey(szCurrentComputerName, KeyVer, ifGostCrypto ? ST_TerminalGOST : ST_TerminalRSA, &saKey);
      }
      break;
    case IL_PARAM_CCAID: //���������� ��������� ����� ��
      {
        g_pUecService->GetCertificateKey(szCurrentComputerName, KeyVer, ifGostCrypto ? ST_UC1GOST : ST_UC1RSA, &saKey);
      }
      break;
    }
  }
  catch (...)
  {
  }
  
  //�������� ����������
  CloseConnection2UecService();
  
  if (saKey == NULL)
    return ILRET_PARAM_NOT_FOUND;

  //����������� ������
  void* pArrayData;
  //Obtain safe pointer to the array
  SafeArrayAccessData(saKey, &pArrayData);
  //Copy the bitmap into our buffer
  IL_CHAR buf[20*1024] = {0};
  memcpy(buf, pArrayData, saKey->rgsabound[0].cElements); //Unlock the variant data
  // ����������� � �������� ������
  if(pCertBuf && hex2bin(buf, pCertBuf, pdwCertLen))
    return ILRET_INVALID_HEX_STRING_FORMAT;
  SafeArrayUnaccessData(saKey);

  return ILRET_OK;
}

#endif