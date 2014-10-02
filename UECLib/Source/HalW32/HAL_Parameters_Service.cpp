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

//Авторизационный токен
WCHAR g_szUecServiceToken[5*1024] = {0};
IL_FUNC void prmSetUecServiceToken(WCHAR* pwszUecServiceToken)
{
  wcscpy(g_szUecServiceToken, pwszUecServiceToken);
}

#ifdef _CLR_SUPPORT_
  
#import "rt.uec.model.tlb" named_guids
#import "rt.uec.service.tlb" named_guids

//Ссылка на сервис
rt_uec_service::IUecClientInterop* g_pUecService = NULL;

enum SertificateType
{
  /// <summary> Сертификат  УЦ №1 RSA</summary>
  ST_UC1RSA = 484,

  /// <summary> Сертификат ОКО №1 RSA</summary>
  ST_OKO1RSA = 485,

  /// <summary> Сертификат терминала RSA</summary>
  ST_TerminalRSA = 486,

  /// <summary> Сертификат  УЦ №1 ГОСТ</summary>
  ST_UC1GOST = 489,

  /// <summary> Сертификат ОКО №1 ГОСТ</summary>
  ST_OKO1GOST = 490,

  /// <summary> Сертификат терминала ГОСТ</summary>
  ST_TerminalGOST = 491,

  /// <summary> Сертификат терминала ГОСТ</summary>
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
  
  //Загрузка .NET сборки 
  HRESULT hResult = CoCreateInstance(rt_uec_service::CLSID_UecClientInterop, NULL, CLSCTX_INPROC_SERVER, 
     rt_uec_service::IID_IUecClientInterop, reinterpret_cast<void**> (&g_pUecService));
  if (FAILED(hResult) || g_pUecService == NULL)
  {
    return FALSE;
  }

  //Открытие соединения
  return g_pUecService->OpenConnection(g_szUecServiceToken);
}

void CloseConnection2UecService()
{
  //Закрытие соединения
  if (g_pUecService != NULL)
    g_pUecService->CloseConnection();

  g_pUecService = NULL;

  CoUninitialize();
}

//Возвращает параметр
IL_FUNC IL_RETCODE prmGetParameterByService(IL_WORD ilParam, IL_BYTE* pParamBuf, IL_DWORD* pdwParamLen)
{
  //Открытие соединения к сервису
  if (!OpenConnection2UecService())
    return ILRET_PARAM_NOT_FOUND;

  //Получение имени текущего компьютера
  DWORD dwCurrentComputerName = 1024*5;
  WCHAR szCurrentComputerName[1024*5];
  GetCurrentComputerName(szCurrentComputerName, dwCurrentComputerName);

  try
  {
    switch (ilParam)
    {
    case IL_PARAM_READERNAME: //Имя ридера
      {
        BSTR strReaderName = NULL;
        g_pUecService->GetCurrentReaderName(szCurrentComputerName, &strReaderName);
        WideCharToMultiByte(CP_ACP, 0, strReaderName, -1, (LPSTR)pParamBuf, *pdwParamLen, NULL, NULL);
        SysFreeString(strReaderName);
      }
      break;
    case IL_PARAM_USE_GOST:   //Тип криптографии
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

        //Конвертация данных
        void* pArrayData;
        //Obtain safe pointer to the array
        SafeArrayAccessData(saKey, &pArrayData);
        //Copy the bitmap into our buffer
        IL_CHAR buf[20*1024] = {0};
        memcpy(buf, pArrayData, saKey->rgsabound[0].cElements); //Unlock the variant data
        // форматируем в бинарный массив
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
  
  //закрытие соединения
  CloseConnection2UecService();
  
  return ILRET_OK;  
}

// Возвращает версионый параметр сертификата
IL_FUNC IL_RETCODE prmGetParameterKeyVerByService(IL_WORD ilParam, IL_BYTE KeyVer, IL_BYTE ifGostCrypto, IL_BYTE* pCertBuf, IL_DWORD* pdwCertLen)
{
  //Открытие соединения к сервису
  if (!OpenConnection2UecService())
    return ILRET_PARAM_NOT_FOUND;

  //Получение имени текущего компьютера
  DWORD dwCurrentComputerName = 1024*5;
  WCHAR szCurrentComputerName[1024*5];
  GetCurrentComputerName(szCurrentComputerName, dwCurrentComputerName);

  SAFEARRAY* saKey = NULL;
  try
  {
    switch (ilParam)
    {
    case IL_PARAM_CAID:   //Сертификат открытого ключа ОКО
      {
        g_pUecService->GetCertificateKey(szCurrentComputerName, KeyVer, ifGostCrypto ? ST_OKO1GOST : ST_OKO1RSA, &saKey);
      }
      break;
    case IL_PARAM_CIFDID: //Сертификат открытого ключа терминала
      {
        g_pUecService->GetCertificateKey(szCurrentComputerName, KeyVer, ifGostCrypto ? ST_TerminalGOST : ST_TerminalRSA, &saKey);
      }
      break;
    case IL_PARAM_CCAID: //Сертификат открытого ключа УЦ
      {
        g_pUecService->GetCertificateKey(szCurrentComputerName, KeyVer, ifGostCrypto ? ST_UC1GOST : ST_UC1RSA, &saKey);
      }
      break;
    }
  }
  catch (...)
  {
  }
  
  //закрытие соединения
  CloseConnection2UecService();
  
  if (saKey == NULL)
    return ILRET_PARAM_NOT_FOUND;

  //Конвертация данных
  void* pArrayData;
  //Obtain safe pointer to the array
  SafeArrayAccessData(saKey, &pArrayData);
  //Copy the bitmap into our buffer
  IL_CHAR buf[20*1024] = {0};
  memcpy(buf, pArrayData, saKey->rgsabound[0].cElements); //Unlock the variant data
  // форматируем в бинарный массив
  if(pCertBuf && hex2bin(buf, pCertBuf, pdwCertLen))
    return ILRET_INVALID_HEX_STRING_FORMAT;
  SafeArrayUnaccessData(saKey);

  return ILRET_OK;
}

#endif