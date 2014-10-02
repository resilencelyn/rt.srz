#ifndef _RSA_HELPER_H_
#define _RSA_HELPER_H_

#include "il_types.h"
#include "HAL_Crypto.h"

IL_FUNC IL_RETCODE _jCalcMAC8p(IL_BYTE* data, IL_WORD len, IL_BYTE* key16, IL_BYTE* icv, IL_BYTE* mac8, IL_BYTE isRetailMAC, IL_BYTE ifIcvEncryption);
void DES3_Encrypt(IL_BYTE* DataInOut8, IL_BYTE* Key16);

#endif