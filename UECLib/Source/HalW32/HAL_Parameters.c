#include <windows.h>
#include <stdio.h>
#include "HAL_Parameter.h"
#include "HAL_Common.h"
#include "il_error.h"
#include "CertType.h"

HMODULE GetThisDllHandle()
{
  MEMORY_BASIC_INFORMATION info;
  size_t len = VirtualQueryEx(GetCurrentProcess(), (void*)GetThisDllHandle, &info, sizeof(info));
  return len ? (HMODULE)info.AllocationBase : NULL;
}

IL_CHAR* GetParamFilename(IL_CHAR* pszFullParamFileName, IL_CHAR* pszParamFileName, IL_CHAR* pszParamFileExt)
{
#ifndef TELLME
  static char tmpFileName[_MAX_PATH];
  char  drive[_MAX_DRIVE], dir[_MAX_DIR], name[_MAX_FNAME], old_ext[_MAX_EXT];
  GetModuleFileName(GetThisDllHandle(),tmpFileName,sizeof(tmpFileName));

  _splitpath(tmpFileName, drive, dir, name, old_ext);

  _makepath(tmpFileName, drive, dir, pszParamFileName, pszParamFileExt);

  if(pszFullParamFileName)
    strcpy(pszFullParamFileName, tmpFileName);
#else
  sprintf(pszFullParamFileName, "c:\\UECParam\\%s.%s", pszParamFileName, pszParamFileExt);	
#endif//TELLME

  return pszFullParamFileName;
}

IL_PARAMETER_DESCR Params[] = 
{
  // наименование ридера
  {IL_PARAM_READERNAME,   IL_PARAM_FORMAT_STRING,     0,  "Reader",       "Name"          },

  // информация о терминале
  {IL_PARAM_TERMINAL_INFO,IL_PARAM_FORMAT_BYTEARRAY,  12, "TerminalInfo", "9F1C"          },

#ifndef SM_SUPPORT   
  // Закрытый ключ терминала Sifd.id.rsa
  {IL_PARAM_SIFDID_MOD,   IL_PARAM_FORMAT_BYTEARRAY,  0,  "Keys",         "SifdidMod_RSA"  },
  {IL_PARAM_SIFDID_EXP,   IL_PARAM_FORMAT_BYTEARRAY,  0,  "Keys",         "SifdidExp_RSA"  },
  // Закрытый ключ терминала Sifd.id.gost
  {IL_PARAM_SIFDID_GOST,  IL_PARAM_FORMAT_BYTEARRAY,  0,  "Keys",         "Sifdid_GOST"    },
#else
  // наименование ридера для ПМБ
  {IL_PARAM_SM_READERNAME,   IL_PARAM_FORMAT_STRING,  0,  "SmReader",     "Name"          },
  // ПИН владельца ПМБ
  {IL_PARAM_SM_PIN,		   IL_PARAM_FORMAT_STRING,  0,  "Sm",			"PIN"           },
#endif

  // мастер-ключ для вычисления криптограммы ИД-приложения
  {IL_PARAM_MK_AC_ID_RSA,	IL_PARAM_FORMAT_BYTEARRAY,				16,  "Keys",        "MkAcIdRsa"     },
  {IL_PARAM_MK_AC_ID_DIVERS_FLAG_RSA,IL_PARAM_FORMAT_BYTEARRAY,	1,   "Keys",        "MkAcIdRsaMaster"  },
  {IL_PARAM_MK_AC_ID_GOST,IL_PARAM_FORMAT_BYTEARRAY,				32,  "Keys",        "MkAcIdGost"    },
  {IL_PARAM_MK_AC_ID_DIVERS_FLAG_GOST,IL_PARAM_FORMAT_BYTEARRAY,  1,   "Keys",        "MkAcIdGostMaster"  },

  // мастер-ключ для обеспечения обмена через защищённый канал
  {IL_PARAM_MK_SM_ID_RSA,	IL_PARAM_FORMAT_BYTEARRAY,  16,  "Keys",        "MkSmIdRsa"     },
  {IL_PARAM_MK_SM_ID_GOST,IL_PARAM_FORMAT_BYTEARRAY,  32,  "Keys",        "MkSmIdGost"    },

  // приватные ключи провайдера услуг
  {IL_PARAM_S_SP_ID_RSA,	IL_PARAM_FORMAT_BYTEARRAY,  0,  "Keys",        "SSpIdRsa"     },
  {IL_PARAM_S_SP_ID_GOST, IL_PARAM_FORMAT_BYTEARRAY,  0,  "Keys",        "SSpIdGost"    },

  // сертификаты ОК провайдера услуг
  {IL_PARAM_CSPID_RSA,	IL_PARAM_FORMAT_BYTEARRAY,  0,  "Keys",        "CSpIdRsa"     }, 
  {IL_PARAM_CSPID_GOST,	IL_PARAM_FORMAT_BYTEARRAY,  0,  "Keys",        "CSpIdGost"    }, 

  // приватные ключи провайдера услуг
  {IL_PARAM_S_CA_ID_RSA,	IL_PARAM_FORMAT_BYTEARRAY,  0,  "Keys",        "SCaIdRsa"     },
  {IL_PARAM_S_CA_ID_GOST, IL_PARAM_FORMAT_BYTEARRAY,  0,  "Keys",        "SCaIdGost"    },

  // мастер-ключ для обеспечения обмена SE через защищённый канал
  {IL_PARAM_SE_SM_ID_RSA,	IL_PARAM_FORMAT_BYTEARRAY,  16,  "Keys",        "SeSmIdRsa"     },
  {IL_PARAM_SE_SM_ID_GOST,IL_PARAM_FORMAT_BYTEARRAY,  32,  "Keys",        "SeSmIdGost"    },

  //параметры для формирования ответа на запрос
  {IL_PARAM_MEMBER_ID,	IL_PARAM_FORMAT_BYTEARRAY,  0,  "Data",        "MemberId"     }, 
  {IL_PARAM_IDENT_OP_ID,	IL_PARAM_FORMAT_BYTEARRAY,  0,  "Data",        "IdentOpId"    }, 
  {IL_PARAM_PAYMENT_INFO,	IL_PARAM_FORMAT_BYTEARRAY,  0,  "Data",        "PaymentInfo"  }, 
  {IL_PARAM_AAC,	        IL_PARAM_FORMAT_BYTEARRAY,  0,  "Data",        "AAC"          }, 

  // протоколироваание
  {IL_PARAM_PROT_LOGNAME,		IL_PARAM_FORMAT_STRING,		0,  "Protocol",     "LogName"      },
  {IL_PARAM_PROT_LOGFILE,		IL_PARAM_FORMAT_BYTEARRAY,  1,  "Protocol",     "LogFile"       },
  {IL_PARAM_PROT_OUTPUT,		IL_PARAM_FORMAT_BYTEARRAY,  1,  "Protocol",     "Output"        },
  {IL_PARAM_PROT_READER,		IL_PARAM_FORMAT_BYTEARRAY,  1,  "Protocol",     "Reader"        },
  {IL_PARAM_PROT_CARD,		IL_PARAM_FORMAT_BYTEARRAY,  1,  "Protocol",     "Card"          },
  {IL_PARAM_PROT_CARDLIB,		IL_PARAM_FORMAT_BYTEARRAY,  1,  "Protocol",     "Cardlib"       },
  {IL_PARAM_PROT_FUNCLIB,		IL_PARAM_FORMAT_BYTEARRAY,  1,  "Protocol",     "Funclib"       },
  {IL_PARAM_PROT_OPLIB,		IL_PARAM_FORMAT_BYTEARRAY,  1,  "Protocol",     "Oplib"			},
  {IL_PARAM_PROT_SMLIB,		IL_PARAM_FORMAT_BYTEARRAY,  1,  "Protocol",     "Smlib"			},
  {IL_PARAM_PROT_TELLME,		IL_PARAM_FORMAT_BYTEARRAY,  1,  "Protocol",     "TellMe"		},
  {IL_PARAM_PROT_EXTERN,		IL_PARAM_FORMAT_BYTEARRAY,  1,  "Protocol",     "Extern"		},



  {IL_PARAM_USE_GOST,			IL_PARAM_FORMAT_BYTEARRAY,  1,  "TerminalInfo", "UseGOST"       },

  {0}
};

IL_FUNC IL_RETCODE prmGetParameterEx(IL_WORD ilParam, IL_BYTE* pParamBuf, IL_DWORD* pdwParamLen, IL_CHAR *FileName, IL_CHAR *Extention)
{
  IL_CHAR full_file_name[_MAX_PATH];
  IL_CHAR buf[8192];
  IL_DWORD dwRet = 0;
  IL_RETCODE ilRet = 0;
  IL_PARAMETER_DESCR* pParamDescr = NULL;
  IL_WORD i;

  GetParamFilename(full_file_name, FileName, Extention);

  for(i = 0; i<sizeof(Params)/sizeof(Params[0]); i++)
  {
    if(Params[i].ID == ilParam)
    {
      pParamDescr = &Params[i];
      break;
    }
  }

  if(pParamDescr==NULL)
    return ILRET_PARAM_DESCR_NOT_FOUND;

  dwRet = GetPrivateProfileString(pParamDescr->Section, pParamDescr->Name, "", buf, sizeof(buf), full_file_name);
  if(*buf == '\0')
    return ILRET_PARAM_NOT_FOUND;

  switch(pParamDescr->Format)
  {
  case IL_PARAM_FORMAT_BYTEARRAY:
    if(hex2bin(buf, pParamBuf, pdwParamLen))
      return ILRET_INVALID_HEX_STRING_FORMAT;
    break;
  case IL_PARAM_FORMAT_STRING:
    strcpy((IL_CHAR*)pParamBuf, buf);
    *pdwParamLen = strlen(buf)+1;
    break;
  default:
    return ILRET_PARAM_FORMAT_UNKNOWN;
  }

  if(pParamDescr->Length != 0)
  {
    if(pParamDescr->Length != *pdwParamLen)
      return ILRET_PARAM_WRONG_LENGTH;
  }

  return 0;
}

extern WCHAR g_szUecServiceToken[5*1024];
extern BOOL g_bUseGlobalConfigurationData;
IL_FUNC IL_RETCODE prmGetParameter(IL_WORD ilParam, IL_BYTE* pParamBuf, IL_DWORD* pdwParamLen)
{
  IL_RETCODE ilRet;
  if (wcslen((const WCHAR*)g_szUecServiceToken) == 0)
  {
    if (g_bUseGlobalConfigurationData)
      ilRet = prmGetParameterByGlobalConfigurationData(ilParam, pParamBuf, pdwParamLen); //работа через глобальные переменные
    else
      ilRet = prmGetParameterEx(ilParam, pParamBuf, pdwParamLen, "terminal", "ini"); //работа через terminal.ini
  }
  #ifdef _CLR_SUPPORT_
  else
    ilRet = prmGetParameterByService(ilParam, pParamBuf, pdwParamLen); //работа через сервис
  #endif

  return ilRet;
}

IL_FUNC IL_RETCODE prmGetParameterHost(IL_WORD ilParam, IL_BYTE* pParamBuf, IL_DWORD* pdwParamLen)
{
  IL_RETCODE ilRet;

  ilRet = prmGetParameterEx(ilParam, pParamBuf, pdwParamLen, "host", "ini");

  return ilRet;
}

IL_FUNC IL_RETCODE prmGetParameterHostCin2PanSnils(IL_BYTE *in_pCIN, IL_BYTE *out_pPan, IL_BYTE  *out_pSnils)
{
  IL_DWORD dwRet = 0;
  IL_CHAR full_file_name[_MAX_PATH];
  IL_CHAR ParamName[50];
  IL_CHAR CinPostfix[25];
  IL_CHAR buf[25];

  // получим путь до файла настроек эмулятора хоста
  GetParamFilename(full_file_name, "host", "ini");

  // конструируем постфикс для указанного CIN
  bin2hex(CinPostfix, in_pCIN, 10);

  // извлекаем значение PAN
  sprintf(ParamName, "Pan%s", CinPostfix);
  if(!(dwRet = GetPrivateProfileString("CIN", ParamName, "", buf, sizeof(buf), full_file_name)))
    return ILRET_PARAM_NOT_FOUND;
  if(strlen(buf) != 20)
    return ILRET_PARAM_WRONG_LENGTH;
  hex2bin(buf, out_pPan, &dwRet);

  // извлекаем значение SNILS
  sprintf(ParamName, "Snils%s", CinPostfix);
  if(!(dwRet = GetPrivateProfileString("CIN", ParamName, "", buf, sizeof(buf), full_file_name)))
    return ILRET_PARAM_NOT_FOUND;
  if(strlen(buf) != 12)
    return ILRET_PARAM_WRONG_LENGTH;
  hex2bin(buf, out_pSnils, &dwRet);

  return 0;
}

// Извлекает из файла настроек терминала версионный параметр сертификата
IL_FUNC IL_RETCODE prmGetParameterKeyVer(IL_WORD ilParam, IL_BYTE KeyVer, IL_BYTE ifGostCrypto, IL_BYTE* pCertBuf, IL_DWORD* pdwCertLen)
{
  //через сервис
  #ifdef _CLR_SUPPORT_
  if (wcslen((const WCHAR*)g_szUecServiceToken) != 0)
    return prmGetParameterKeyVerByService(ilParam, KeyVer, ifGostCrypto, pCertBuf, pdwCertLen);
  #endif

  if (g_bUseGlobalConfigurationData)
    return prmGetParameterKeyVerByByGlobalConfigurationData(ilParam, KeyVer, ifGostCrypto, pCertBuf, pdwCertLen);
  
  IL_RETCODE ilRet = 0;
  IL_DWORD dwRet = 0;
  IL_CHAR full_file_name[_MAX_PATH];
  IL_CHAR ParamName[25];
  IL_CHAR buf[8192];

  if(!(ilParam == IL_PARAM_CAID || ilParam == IL_PARAM_CIFDID || ilParam == IL_PARAM_CCAID))
    return ILRET_OPLIB_INVALID_ARGUMENT;

  // получим путь до файла настроек терминала
  GetParamFilename(full_file_name, "terminal", "ini");

  // конструируем имя параметра в зависимости от версии ключа и используемого криптоалгоритма
  {
    IL_CHAR* s; 
    switch(ilParam)
    {
    case IL_PARAM_CAID:
      s = "Caid";
      break;
    case IL_PARAM_CIFDID:
      s = "Cifdid";
      break;
    case IL_PARAM_CCAID:
      s = "Ccaid";
      break;
    default:
      return ILRET_OPLIB_INVALID_ARGUMENT;
    }
    sprintf(ParamName, "%s", s);
  }

  // добавим тип используемого криптоалгоритма
  strcat(ParamName, (ifGostCrypto ? "_GOST" : "_RSA"));

  // добавим постфикс используемой версии открытого ключа УЦ ЕПСС УЭК
  {	
    IL_CHAR _KeyVer[5];
    sprintf(_KeyVer, "_%u", KeyVer);
    strcat(ParamName, _KeyVer);
  }

  // извлечём значение сертификата
  dwRet = GetPrivateProfileString("Cert", ParamName, "", buf, sizeof(buf), full_file_name);
  if(!dwRet)
    return ILRET_PARAM_CERTIFICATE_NOT_FOUND;

  // форматируем в бинарный массив
  if(pCertBuf && hex2bin(buf, pCertBuf, pdwCertLen))
    return ILRET_INVALID_HEX_STRING_FORMAT;

  return 0;
}


IL_FUNC IL_RETCODE prmGetParameterSectorEx(IL_WORD SectorId, IL_BYTE SectorVer, IL_CHAR *ParamName, IL_CHAR *out_ParamBuf)
{
  IL_CHAR full_file_name[_MAX_PATH];
  IL_CHAR Section[25];
  IL_CHAR buf[256];

  if(!ParamName || !out_ParamBuf)
    return ILRET_OPLIB_INVALID_ARGUMENT;

  GetParamFilename(full_file_name, "sectors", "ini");
  sprintf(Section, "Sector%u_%02X", SectorId, SectorVer); 

  *buf = 0;
  GetPrivateProfileString(Section, ParamName, "", buf, sizeof(buf), full_file_name);
  if(*buf == 0)
    return ILRET_PARAM_NOT_FOUND;

  strcpy(out_ParamBuf, buf);

  return 0;
}

IL_FUNC IL_RETCODE prmWriteSectorExDescr(IL_WORD SectorId, IL_BYTE SectorVer, IL_CHAR *SectorExDescr)
{
  IL_CHAR full_file_name[_MAX_PATH];
  IL_CHAR Section[16];
  IL_CHAR *pKeyName, *pKeyValue, *pNext;
  IL_CHAR tmp[256];
  IL_DWORD len;

  // проверим валидность входных параметров
  if(!SectorId || !SectorExDescr)
    return ILRET_OPLIB_INVALID_ARGUMENT;

  // сформируем имя секции
  sprintf(Section, "Sector%u_%02X", SectorId, SectorVer);

  // получим путь до файла-описателя внешних секторов
  GetParamFilename(full_file_name, "sectors", "ini");

  // обнулим секцию
  if(!WritePrivateProfileString(Section, NULL, NULL, full_file_name))
    return ILRET_PARAM_WRITE_SECTOR_EX_ERROR;

  // добавим параметры
  for(pKeyName = SectorExDescr; pKeyName && pKeyName[0]; pKeyName = pNext)
  {
    if((pNext = strchr(pKeyName, ';')) == NULL)
      break; 

    if((len = ++pNext - pKeyName) > sizeof(tmp))
      return ILRET_PARAM_SECTOR_EX_WRONG_FORMAT;

    memcpy(tmp, pKeyName, len);
    tmp[len-1] = 0;
    if((pKeyValue = strchr(tmp, '=')) == NULL)
      return ILRET_PARAM_SECTOR_EX_WRONG_FORMAT;
    *pKeyValue++ = 0;

    if(!WritePrivateProfileString(Section, tmp, pKeyValue, full_file_name))
      return ILRET_PARAM_WRITE_SECTOR_EX_ERROR;	
  }

  return 0;
}

IL_FUNC IL_RETCODE prmGetParameterIqFront(IL_CHAR *SectionName, IL_CHAR *ParamName, IL_CHAR *out_ParamBuf)
{
  IL_CHAR full_file_name[_MAX_PATH];
  IL_CHAR buf[256] = { 0 };

  if(!ParamName || !out_ParamBuf)
    return ILRET_OPLIB_INVALID_ARGUMENT;

  GetParamFilename(full_file_name, "iqfront", "ini");
  GetPrivateProfileString(SectionName, ParamName, "", buf, sizeof(buf), full_file_name);
  if(*buf == 0)
    return ILRET_PARAM_NOT_FOUND;

  strcpy(out_ParamBuf, buf);

  return 0;
}

IL_FUNC IL_RETCODE prmGetParameterPattern(IL_CHAR *ParamName, IL_CHAR *out_ParamBuf, IL_WORD MaxBufLen)
{
  IL_CHAR full_file_name[_MAX_PATH];
  IL_CHAR buf[256] = { 0 };

  if(!ParamName || !out_ParamBuf)
    return ILRET_OPLIB_INVALID_ARGUMENT;

  GetParamFilename(full_file_name, "sectors", "ini");
  GetPrivateProfileString("PatternDescr", ParamName, "", buf, sizeof(buf), full_file_name);
  if(*buf == 0)
    return ILRET_PARAM_NOT_FOUND;

  if(strlen(buf) + 1 > MaxBufLen)
    return ILRET_OPLIB_BLOCKDATADESCR_BUF_IS_OVER;

  strcpy(out_ParamBuf, buf);
  return 0;
}




