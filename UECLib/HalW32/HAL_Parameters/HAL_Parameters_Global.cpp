#include <windows.h>
#include <stdio.h>
#include <mscoree.h>
#include "HAL_Parameter.h"
#include "HAL_Common.h"
#include "il_error.h"
#include "CertType.h"


//Имя ридера
IL_CHAR g_szReaderName[8*1024];             
IL_DWORD g_dwReaderName = -1;

//Сертификат открытого ключа ОКО(ГОСТ)
IL_BYTE  g_szOKO1OpenCertGOST[20*1024];       
IL_DWORD g_dwOKO1OpenCertGOSTLength = -1;

//Сертификат открытого ключа терминала(ГОСТ)
IL_BYTE g_szTerminalOpenCertGOST[20*1024];  
IL_DWORD g_dwTerminalOpenCertGOSTLength = -1; 

//Сертификат открытого ключа УЦ(ГОСТ)
IL_BYTE g_szUC1OpenCertGOST[20*1024];
IL_DWORD g_dwUC1OpenCertGOSTLength = -1;

//Сертификат открытого ключа ОКО(RSA)
IL_BYTE g_szOKO1OpenCertRSA[20*1024];      
IL_DWORD g_dwOKO1OpenCertRSALength = -1;      

//Сертификат открытого ключа терминала(RSA)
IL_BYTE g_szTerminalOpenCertRSA[20*1024];
IL_DWORD g_dwTerminalOpenCertRSALength = -1;

//Сертификат открытого ключа УЦ(RSA)
IL_BYTE g_szUC1OpenCertRSA[20*1024];
IL_DWORD g_dwUC1OpenCertRSALength = -1;

//Сертификат закрытого ключа терминала(RSA)
IL_BYTE g_szTerminalClosedCertGOST[20*1024];
IL_DWORD g_dwTerminalClosedCertGOSTLength = -1;


IL_FUNC void prmSetGlobalConfigurationData(WCHAR* pwszReaderName, IL_BYTE* szOKO1OpenCertGOST, IL_BYTE* szTerminalOpenCertGOST, 
  IL_BYTE* szUC1OpenCertGOST, IL_BYTE* szOKO1OpenCertRSA, IL_BYTE* szTerminalOpenCertRSA, IL_BYTE* szUC1OpenCertRSA, IL_BYTE* szTerminalClosedCertGOST)
{
  //Имя ридера
  WideCharToMultiByte(CP_ACP, 0, pwszReaderName, -1, 
    (LPSTR)g_szReaderName, g_dwReaderName, NULL, NULL); 
  
  //Сертификат открытого ключа ОКО(ГОСТ)
  if(szOKO1OpenCertGOST && hex2bin((IL_CHAR*)szOKO1OpenCertGOST, g_szOKO1OpenCertGOST, &g_dwOKO1OpenCertGOSTLength))
    g_dwOKO1OpenCertGOSTLength = -1;

  //Сертификат открытого ключа терминала(ГОСТ)
  if(szTerminalOpenCertGOST && hex2bin((IL_CHAR*)szTerminalOpenCertGOST, g_szTerminalOpenCertGOST, &g_dwTerminalOpenCertGOSTLength))
    g_dwTerminalOpenCertGOSTLength = -1;

  //Сертификат открытого ключа УЦ(ГОСТ)
  if(szUC1OpenCertGOST && hex2bin((IL_CHAR*)szUC1OpenCertGOST, g_szUC1OpenCertGOST, &g_dwUC1OpenCertGOSTLength))
    g_dwUC1OpenCertGOSTLength = -1;

  //Сертификат открытого ключа ОКО(RSA)
  if(szOKO1OpenCertRSA && hex2bin((IL_CHAR*)szOKO1OpenCertRSA, g_szOKO1OpenCertRSA, &g_dwOKO1OpenCertRSALength))
    g_dwOKO1OpenCertRSALength = -1;

  //Сертификат открытого ключа ОКО(RSA)
  if(szTerminalOpenCertRSA && hex2bin((IL_CHAR*)szTerminalOpenCertRSA, g_szTerminalOpenCertRSA, &g_dwTerminalOpenCertRSALength))
    g_dwTerminalOpenCertRSALength = -1;

  //Сертификат открытого ключа УЦ(ГОСТ)
  if(szUC1OpenCertRSA && hex2bin((IL_CHAR*)szUC1OpenCertRSA, g_szUC1OpenCertRSA, &g_dwUC1OpenCertRSALength))
    g_dwUC1OpenCertRSALength = -1;

  //Сертификат закрытого ключа терминала(RSA)
  if(szTerminalClosedCertGOST && hex2bin((IL_CHAR*)szTerminalClosedCertGOST, g_szTerminalClosedCertGOST, &g_dwTerminalClosedCertGOSTLength))
    g_dwTerminalClosedCertGOSTLength = -1;
}

//Возвращает параметр
IL_FUNC IL_RETCODE prmGetParameterByGlobalConfiguratioNData(IL_WORD ilParam, IL_BYTE* pParamBuf, IL_DWORD* pdwParamLen)
{
  switch (ilParam)
  {
  case IL_PARAM_READERNAME: //Имя ридера
    {
      if (g_dwReaderName == -1)
        return ILRET_PARAM_NOT_FOUND;
      strcpy((IL_CHAR*)pParamBuf, g_szReaderName);
    }
    break;
  case IL_PARAM_USE_GOST:   //Тип криптографии
    {
      /* rt_uec_model::CryptographyType type = rt_uec_model::CryptographyType_GOST;
      g_pUecService->GetCurrentCryptographyType(szCurrentComputerName, &type);
      if (type == rt_uec_model::CryptographyType_RSA)
      {
      *pParamBuf = 0;
      }
      else
      if (type == rt_uec_model::CryptographyType_GOST)
      {
      *pParamBuf = 1;
      }*/
    }
    break;
  case IL_PARAM_SIFDID_GOST:
    {
      if (g_dwTerminalClosedCertGOSTLength == -1)
        return ILRET_INVALID_HEX_STRING_FORMAT;

      memcpy(pParamBuf, g_szTerminalClosedCertGOST, g_dwTerminalClosedCertGOSTLength);
      *pdwParamLen = g_dwTerminalClosedCertGOSTLength;
    }
    break;
  }

  return ILRET_OK;  
}

// Возвращает версионый параметр сертификата
IL_FUNC IL_RETCODE prmGetParameterKeyVerByService(IL_WORD ilParam, IL_BYTE KeyVer, IL_BYTE ifGostCrypto, IL_BYTE* pCertBuf, IL_DWORD* pdwCertLen)
{
  switch (ilParam)
  {
  case IL_PARAM_CAID:   //Сертификат открытого ключа ОКО
    {
      if (ifGostCrypto)
      {
        if (g_dwOKO1OpenCertGOSTLength == -1)
          return ILRET_INVALID_HEX_STRING_FORMAT;

        memcpy(pCertBuf, g_szOKO1OpenCertGOST, g_dwOKO1OpenCertGOSTLength);
       *pdwCertLen = g_dwOKO1OpenCertGOSTLength;
      }
      else
      {
        if (g_dwOKO1OpenCertRSALength == -1)
          return ILRET_INVALID_HEX_STRING_FORMAT;

        memcpy(pCertBuf, g_szOKO1OpenCertRSA, g_dwOKO1OpenCertRSALength);
       *pdwCertLen = g_dwOKO1OpenCertRSALength;
      }
    }
    break;
  case IL_PARAM_CIFDID: //Сертификат открытого ключа терминала
    {
      if (ifGostCrypto)
      {
        if (g_dwTerminalOpenCertGOSTLength== -1)
          return ILRET_INVALID_HEX_STRING_FORMAT;

        memcpy(pCertBuf, g_szTerminalOpenCertGOST, g_dwTerminalOpenCertGOSTLength);
       *pdwCertLen = g_dwTerminalOpenCertGOSTLength;
      }
      else
      {
        if (g_dwTerminalOpenCertRSALength == -1)
          return ILRET_INVALID_HEX_STRING_FORMAT;

        memcpy(pCertBuf, g_szTerminalOpenCertRSA, g_dwTerminalOpenCertRSALength);
       *pdwCertLen = g_dwTerminalOpenCertRSALength;
      }
    }
    break;
  case IL_PARAM_CCAID: //Сертификат открытого ключа УЦ
    {
      if (ifGostCrypto)
      {
        if (g_dwUC1OpenCertGOSTLength == -1)
          return ILRET_INVALID_HEX_STRING_FORMAT;

        memcpy(pCertBuf, g_szUC1OpenCertGOST, g_dwUC1OpenCertGOSTLength);
       *pdwCertLen = g_dwUC1OpenCertGOSTLength;
      }
      else
      {
        if (g_dwUC1OpenCertRSALength == -1)
          return ILRET_INVALID_HEX_STRING_FORMAT;

        memcpy(pCertBuf, g_szUC1OpenCertRSA, g_dwUC1OpenCertRSALength);
       *pdwCertLen = g_dwUC1OpenCertRSALength;
      }
    }
    break;
  }
  return ILRET_OK;
}