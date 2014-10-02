#include "il_types.h"
#include "il_error.h"
#include "tag.h"
#include "tlv.h"
#include "ru_cryptodsb.h"
#include "HAL_Crypto.h"
#include "HAL_Common.h"

//Key Certificate tags
const IL_TAG TAG_PATH_GOST_EA_81[] = {IL_TAG_EA, IL_TAG_81};
const IL_TAG TAG_PATH_GOST_7F4E_7F49_81[] = {IL_TAG_7F4E, IL_TAG_7F49, IL_TAG_81};

IL_FUNC IL_RETCODE GostKeyFromCertificateEx(IL_TAG rootTag, IL_BYTE* pKeyCert, IL_WORD wKeyCertLen, KEY_GOST_PUB* pKeyGostPub)
{
    IL_RETCODE ilRet = 0;
    IL_BYTE* pTmpPtr;
    IL_DWORD dwTmpLen;
    const IL_TAG* tag_path_81;
    IL_WORD tag_path_81_len;
    
    if(rootTag == IL_TAG_EA)
    {
        tag_path_81 = TAG_PATH_GOST_EA_81;
        tag_path_81_len = sizeof(TAG_PATH_GOST_EA_81)/sizeof(TAG_PATH_GOST_EA_81[0]);
    }
    else
    {
        IL_DWORD dwLen7F21 = 0;
        IL_BYTE* p7F21 = NULL;
        
		ilRet = TagFind(pKeyCert, wKeyCertLen, IL_TAG_7F21, &dwLen7F21, &p7F21, 0);
		if(ilRet == 0)
		{
		    pKeyCert = p7F21;
		    wKeyCertLen = (IL_WORD)dwLen7F21;
		}

        tag_path_81 = TAG_PATH_GOST_7F4E_7F49_81;
        tag_path_81_len = sizeof(TAG_PATH_GOST_7F4E_7F49_81)/sizeof(TAG_PATH_GOST_7F4E_7F49_81[0]);
    } 

    ilRet = TagFindByPath(pKeyCert, wKeyCertLen, tag_path_81, tag_path_81_len, &dwTmpLen, &pTmpPtr, 0);
    if(ilRet)
        return ilRet;

	cmnMemCopy(pKeyGostPub->key, pTmpPtr, (IL_WORD)dwTmpLen);

    return ilRet;
}

IL_FUNC IL_RETCODE cryptoGostKeyFromCertificate(IL_BYTE* pKeyCert, IL_WORD wKeyCertLen, KEY_GOST_PUB* pKeyGostPub)
{
    IL_RETCODE ilRet = 0;

    ilRet = GostKeyFromCertificateEx(IL_TAG_EA, pKeyCert, wKeyCertLen, pKeyGostPub);

    return ilRet;
}

#ifndef SM_SUPPORT 
IL_FUNC void KDF_GOST(IL_BYTE* MSK32, IL_BYTE* R, IL_WORD R_len, IL_BYTE* SMK32)
{
    IL_BYTE tmp[256];
    IL_BYTE Message_Digest[32];
    IL_DWORD offset;

    cmnMemSet(tmp, 0, sizeof(tmp));
    offset = 0;
    cmnMemCopy(tmp, MSK32, 32);
    offset += 32;
    if(R)
    {
        cmnMemCopy(&tmp[32], R, R_len);
        offset += R_len;
    }

    GostR3411_94_Hash(tmp, offset, Message_Digest);

    cmnMemCopy(SMK32, Message_Digest, 32);
}

IL_FUNC void MKDF_GOST(IL_BYTE* IMK32, IL_BYTE* DD, IL_WORD DD_len, IL_BYTE* MK32)
{
	IL_BYTE i, j; 
    IL_BYTE ICV[8] = {0};

    IL_BYTE Message_Digest[32] = {0};

    GostR3411_94_Hash(DD, DD_len, Message_Digest);
    
	for(i = 0; i < 4; i++)
	{
		for(j = 0; j < 8; j++)
			Message_Digest[i*8 + j] ^= ICV[j];

		Gost28147_EncryptECB(IMK32, &Message_Digest[i*8], 8, &Message_Digest[i*8]);

		cmnMemCopy(ICV, &Message_Digest[i*8], 8);
	}

	cmnMemCopy(MK32, Message_Digest, 32);
}
#endif//SM_SUPPORT
