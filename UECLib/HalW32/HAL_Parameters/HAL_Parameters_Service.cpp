#include <windows.h>
#include <stdio.h>
#include "HAL_Parameter.h"
#include "HAL_Common.h"
#include "il_error.h"
#include "CertType.h"

#import "rt.uec.service.client.tlb" named_guids
WCHAR szUecServiceToken[5*1024];
WCHAR szPcName[1024];

IL_FUNC void prmSetUecServiceInfo(WCHAR* pwszUecServiceUri, WCHAR* pwszPcName)
{
  //wstrc  
}