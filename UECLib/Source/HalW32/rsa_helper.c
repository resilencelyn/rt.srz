#include "il_types.h"
#include "il_error.h"
#include "tag.h"
#include "tlv.h"
#include "com_cryptodsb.h"
#include "rsa_helper.h"
#include "HAL_Crypto.h"
#include "HAL_Common.h"

const IL_BYTE OID[] = {0x30,0x21,0x30,0x09,0x06,0x05,0x2B,0x0E,0x03,0x02,0x1A,0x05,0x00,0x04,0x14};

//Key Certificate tags
const IL_TAG TAG_PATH_RSA_EB_81[] = {IL_TAG_EB, IL_TAG_81};
const IL_TAG TAG_PATH_RSA_EB_82[] = {IL_TAG_EB, IL_TAG_82};
const IL_TAG TAG_PATH_RSA_7F4E_7F49_81[] = {IL_TAG_7F4E, IL_TAG_7F49, IL_TAG_81};
const IL_TAG TAG_PATH_RSA_7F4E_7F49_82[] = {IL_TAG_7F4E, IL_TAG_7F49, IL_TAG_82};

IL_FUNC void KDF(IL_BYTE* MSK16, IL_BYTE* R, IL_WORD R_len, IL_BYTE* SMK16)
{
    IL_BYTE tmp[256];
    IL_BYTE Message_Digest[20];
    IL_DWORD offset;

    cmnMemSet(tmp, 0, sizeof(tmp));
    offset = 0;
    cmnMemCopy(tmp, MSK16, 16);
    offset += 16;
    if(R)
    {
        cmnMemCopy(&tmp[16], R, R_len);
        offset += R_len;
    }

    SHA1(tmp, offset, Message_Digest);

    cmnMemCopy(SMK16, Message_Digest, 16);
}

IL_FUNC void MKDF(IL_BYTE* IMK16, IL_BYTE* DD, IL_WORD DD_len, IL_BYTE* MK16)
{
    IL_BYTE tmp[256];
    IL_BYTE Message_Digest[20];
    IL_WORD i;

    SHA1(DD, DD_len, Message_Digest);
    
    cmnMemCopy(tmp, Message_Digest, 16);

    //тип шифрования задан письмом From: Докучаев Дмитрий Юрьевич Sent: Wednesday, January 25, 2012 10:35 AM
    DES3_Encrypt(&tmp[0], IMK16);

    for(i = 0; i < 8; i++)  
        tmp[i+8] ^= tmp[i];

    DES3_Encrypt(&tmp[8], IMK16);

    cmnMemCopy(MK16, tmp, 16);
}

IL_FUNC void MakeRsaCertificate(IL_BYTE* msg, IL_WORD msg_len, KEY_RSA* pKeyRSA, IL_BYTE* cert, IL_WORD* cert_len)
{
    IL_BYTE Message_Digest[20];
    IL_BYTE tmp[MAX_RSA_MODULUS_LEN] = {0};

    SHA1(msg, msg_len, Message_Digest);

    tmp[0] = 0x00;
    tmp[1] = 0x01;//for private keys
    cmnMemSet(&tmp[2], 0xFF, pKeyRSA->mod_len-sizeof(OID)-sizeof(Message_Digest)-3);
    tmp[pKeyRSA->mod_len-sizeof(OID)-sizeof(Message_Digest)-1] = 0x00;
    cmnMemCopy(&tmp[pKeyRSA->mod_len-sizeof(OID)-sizeof(Message_Digest)], OID, sizeof(OID));
    cmnMemCopy(&tmp[pKeyRSA->mod_len-sizeof(Message_Digest)], Message_Digest, sizeof(Message_Digest));

    RSA_Block(cert, cert_len, tmp, pKeyRSA->mod_len, pKeyRSA->mod, pKeyRSA->mod_len, pKeyRSA->exp, pKeyRSA->exp_len);
}

// шифрование блока данных при помощи ключа RSA
IL_FUNC void EncryptRsa(IL_BYTE* data, IL_WORD data_len, KEY_RSA* pKeyRSA, IL_BYTE* enc_data, IL_WORD* enc_data_len)
{
    IL_BYTE tmp[MAX_RSA_MODULUS_LEN] = {0};

    tmp[0] = 0x00;
    tmp[1] = 0x02;//for public keys
    cmnMemSet(&tmp[2], 0xFF, pKeyRSA->mod_len - data_len - 3);
    tmp[pKeyRSA->mod_len - data_len - 1] = 0x00;
    cmnMemCopy(&tmp[pKeyRSA->mod_len - data_len], data, data_len);

    RSA_Block(enc_data, enc_data_len, tmp, pKeyRSA->mod_len, pKeyRSA->mod, pKeyRSA->mod_len, pKeyRSA->exp, pKeyRSA->exp_len);
}


IL_FUNC IL_RETCODE DecryptRsa(IL_BYTE* enc_data, IL_WORD enc_data_len, KEY_RSA* pKeyRSA, IL_BYTE* data, IL_WORD* data_len)
{
    IL_BYTE tmp[MAX_RSA_MODULUS_LEN] = {0};
    IL_WORD tmp_len;

    RSA_Block(tmp, &tmp_len, enc_data, enc_data_len, pKeyRSA->mod, pKeyRSA->mod_len, pKeyRSA->exp, pKeyRSA->exp_len);

    if(tmp[0] != 0)
        return ILRET_CRYPTO_RSA_FORMAT;

    if(tmp[1] != 2)
        return ILRET_CRYPTO_RSA_FORMAT;

    cmnMemCopy(data, tmp, tmp_len);
    *data_len = tmp_len;

    return 0;
}

IL_FUNC IL_RETCODE cryptoVerifyRsaSignature(IL_BYTE* msg, IL_WORD msg_len, IL_BYTE* cert, IL_WORD cert_len, KEY_RSA* pKeyRSA)
{
    IL_BYTE Message_Digest[20];
    IL_BYTE tmp[MAX_RSA_MODULUS_LEN] = {0};
    IL_BYTE tmp1[MAX_RSA_MODULUS_LEN] = {0};
    IL_WORD tmp1_len;
	IL_RETCODE ilRet;
	
	if(cert_len != pKeyRSA->mod_len)
		return ILRET_CRYPTO_WRONG_DATA_LENGTH;

	ilRet = (IL_RETCODE)RSA_Block(tmp1, &tmp1_len, cert, cert_len, pKeyRSA->mod, pKeyRSA->mod_len, pKeyRSA->exp, pKeyRSA->exp_len);
	if(ilRet)
		return ilRet;

    SHA1(msg, msg_len, Message_Digest);

    tmp[0] = 0x00;
    tmp[1] = 0x01;//for private keys
    cmnMemSet(&tmp[2], 0xFF, pKeyRSA->mod_len-sizeof(OID)-sizeof(Message_Digest)-3);
    tmp[pKeyRSA->mod_len-sizeof(OID)-sizeof(Message_Digest)-1] = 0x00;
    cmnMemCopy(&tmp[pKeyRSA->mod_len-sizeof(OID)-sizeof(Message_Digest)], OID, sizeof(OID));
    cmnMemCopy(&tmp[pKeyRSA->mod_len-sizeof(Message_Digest)], Message_Digest, sizeof(Message_Digest));

	if(cmnMemCmp(tmp, tmp1, tmp1_len))
		return ILRET_CRYPTO_WRONG_CERT;

	return ilRet;
}

IL_FUNC IL_RETCODE RsaKeyFromCertificateEx(IL_TAG rootTag, IL_BYTE* pKeyCert, IL_WORD wKeyCertLen, KEY_RSA* pKeyRSA)
{
    IL_RETCODE ilRet = 0;
    IL_BYTE* pTmpPtr;
    IL_DWORD dwTmpLen;
    const IL_TAG* tag_path_81;
    IL_WORD tag_path_81_len;
    const IL_TAG* tag_path_82;
    IL_WORD tag_path_82_len;
    
    if(rootTag == IL_TAG_EB)
    {
        tag_path_81 = TAG_PATH_RSA_EB_81;
        tag_path_81_len = sizeof(TAG_PATH_RSA_EB_81)/sizeof(TAG_PATH_RSA_EB_81[0]);
        tag_path_82 = TAG_PATH_RSA_EB_82;
        tag_path_82_len = sizeof(TAG_PATH_RSA_EB_82)/sizeof(TAG_PATH_RSA_EB_82[0]);
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

        tag_path_81 = TAG_PATH_RSA_7F4E_7F49_81;
        tag_path_81_len = sizeof(TAG_PATH_RSA_7F4E_7F49_81)/sizeof(TAG_PATH_RSA_7F4E_7F49_81[0]);
        tag_path_82 = TAG_PATH_RSA_7F4E_7F49_82;
        tag_path_82_len = sizeof(TAG_PATH_RSA_7F4E_7F49_82)/sizeof(TAG_PATH_RSA_7F4E_7F49_82[0]);
    } 

    ilRet = TagFindByPath(pKeyCert, wKeyCertLen, tag_path_81, tag_path_81_len, &dwTmpLen, &pTmpPtr, 0);
    if(ilRet)
        return ilRet;

	cmnMemCopy(pKeyRSA->mod, pTmpPtr, (IL_WORD)dwTmpLen);
    pKeyRSA->mod_len = (IL_WORD)dwTmpLen;
    ilRet = TagFindByPath(pKeyCert, wKeyCertLen, tag_path_82, tag_path_82_len, &dwTmpLen, &pTmpPtr, 0);
    if(ilRet)
        return ilRet;

	cmnMemCopy(pKeyRSA->exp, pTmpPtr, (IL_WORD)dwTmpLen);
    pKeyRSA->exp_len = (IL_WORD)dwTmpLen;

    return ilRet;
}

// извлечение из сертификата открытого ключа ИД-приложения (RSA)
IL_FUNC IL_RETCODE cryptoRsaKeyFromCertificate(IL_BYTE* pKeyCert, IL_WORD wKeyCertLen, KEY_RSA* pKeyRSA)
{
    return RsaKeyFromCertificateEx(IL_TAG_EB, pKeyCert, wKeyCertLen, pKeyRSA);
}
