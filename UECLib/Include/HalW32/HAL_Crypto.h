#ifndef _HAL_CRYPTO_H_
#define _HAL_CRYPTO_H_

#include "il_types.h"
#include "HAL_CryptoHandle.h"
#include "HAL_SCReader.h"

// ������������ ����� ������ ����� RSA
#define MAX_RSA_MOD_LEN 2048/8

// ��������� ����� RSA
typedef struct
{
	IL_BYTE mod[MAX_RSA_MOD_LEN];	// ������ �����
	IL_WORD mod_len;				// ����� ������ �����
	IL_BYTE exp[MAX_RSA_MOD_LEN];	// ���������� �����
	IL_WORD exp_len;				// ����� ����������
} KEY_RSA;

// ��������� ��������� ����� ����
typedef struct
{
    IL_BYTE key[64];	// �������� ��������� ����� ����
} KEY_GOST_PUB;

// ��������� ��������� ����� ����
typedef struct
{
    IL_BYTE key[32];	// �������� ��������� ����� ����	
} KEY_GOST_PRIV;

//��������� ������� ������ ��� ������� cryptoPrepareMutualAuthDataGost � cryptoPrepareMutualAuthDataRsa 
typedef struct 
{
	KEY_RSA KeyPicidrsa;		// �������� ��������� ����� RSA
    KEY_GOST_PUB KeyPicidgost;	// �������� ��������� ����� ����
	IL_BYTE IcChallenge[16];	// ��������� ����� �����
	IL_WORD IcChallengeLength;	// ����� ���������� ����� �����
} MUTUAL_AUTH_DATA_IN;

//��������� �������� ������ ��� ������� cryptoPrepareMutualAuthDataGost � cryptoPrepareMutualAuthDataRsa 
typedef struct
{
	IL_BYTE S[256];	// �������� ������� ��� ��������������� ��������� � �������������� ��������� ����� ��������� ����
	IL_WORD S_len;	// ����� �������� �������
	IL_BYTE Y[256];	// ���� ������������� �� �������� ����� ��������� ������ RSA
	IL_WORD Y_len;	// ����� �������������� ����� ������ RSA
} MUTUAL_AUTH_DATA_OUT;

//��������� ������� ������ ��� ������� cryptoPrepareSession
typedef struct
{
	IL_BYTE Data[256];					// ���� ������ ��� ��������� ����������� ����� 
	IL_WORD Data_len;					// ����� ����� ������ ��� ��������� ����������� ����� 
	IL_BYTE InitSessionSmCounter[8];	// ��������� �������� �������� ���������
	IL_BYTE TermRandom[16];		// ��������� ����� ���������
	IL_WORD TermRandom_len;		// ����� ���������� ����� ���������
	IL_BYTE IcChallenge[16];	// ��������� ����� �����	
	IL_WORD IcChallenge_len;	// ����� ���������� ����� �����
} SESSION_DATA_IN;

// ��������� ������� ������ ��� ������� hostPrepareIssuerSession
typedef struct
{
	IL_BYTE IcChallenge[16];		// ��������� ����� �����
} ISSUER_SESSION_DATA_IN;

// ��������� �������� ������ ��� ������� hostPrepareIssuerSession
typedef struct
{
    IL_BYTE CardCryptogramm[20];	// ������������ ������ ������������ �������������� ����� (4 �����) � ���������� ����� ����� (16����)
	IL_BYTE CardCryptogrammLength;  // ����� ����������������� ������
} ISSUER_SESSION_DATA_OUT;

// ��������� ������� ������ ��� ������� hostCheckIssuerSession
typedef struct
{
    IL_BYTE HostChallenge[16];		// ��������� ����� �����
    IL_BYTE CardCryptogramm[4];		// ����������� ������������ ����� (Tic)
} CHECK_ISSUER_SESSION_DATA_IN;

// ��������� ���������� ������ � ����������� ������
typedef struct
{
    IL_BYTE Y[256];
    IL_WORD Y_len;
    
    IL_BYTE Random[256];
    IL_WORD Random_len;
    IL_BYTE Pidgost[64];
} PROVIDER_SESSION_DATA;

#ifndef SM_SUPPORT
// ��������� ��������� ������ � ����������� ������ 
typedef struct
{
    IL_BYTE SK_sm_id_smc_des[16];	// ���������� ����� DES
    IL_BYTE SK_sm_id_smc_gost[32];	// ���������� ����� ����
} PROVIDER_SM_CONTEXT;
#else
typedef	IL_HANDLE_CRYPTO PROVIDER_SM_CONTEXT;
#endif

// ��������� ������� ������ ��� ������� hostPrepareSmIssuerSession
typedef struct
{
	IL_BYTE IcChallenge[16];		// C�������� ����� ����� (SM)
} SM_SESSION_DATA_IN;

// ��������� �������� ������ ��� ������� hostPrepareSmIssuerSession
typedef struct
{
    IL_BYTE CardCryptogramm[20];	// ������������ ������ ������������ �������������� ����� (4 �����) � ���������� ����� ����� (16����)
	IL_BYTE CardCryptogrammLength;  // ����� ����������������� ������
} SM_SESSION_DATA_OUT;

typedef struct
{
    IL_BYTE HostChallenge[16];		// ��������� ����� �����
    IL_BYTE CardCryptogramm[4];		// ����������� ������������ SM (Tic)
} CHECK_SM_SESSION_DATA_IN;



//  Description:
//		�������������� ���������-������ � ������ ������������� ���������� ������ ������ �����������.
//  See Also:
//		cryptoProcessSM
//  Arguments:
//      hCrypto		 - ��������� �� ���������������� ��������� ������������� ���������� ������ ������ �����������. 
//		in_pilApdu	 - ��������� �� ���������������� ���������-������ (APDU-�������).
//		ifGOST		 - ��� �������������� ��������������� ����/RSA.
//		AppVer		 - ������ ��-���������� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ���������-������� � ������ ������������� ���������� ������ ������ ���������.
IL_FUNC IL_RETCODE cryptoPrepareSM(IL_HANDLE_CRYPTO hCrypto, IL_APDU *in_pilApdu, IL_BYTE ifGOST, IL_BYTE AppVer);

//  Description:
//		������������ ���������-������ (APDU-�������) � ������ ������������� ���������� ������ ������ �����������.
//  See Also:
//		cryptoPrepareSM
//  Arguments:
//      hCrypto		 - ��������� �� ���������������� ��������� ������������� ���������� ������ ������ �����������. 
//		in_pilApdu	 - ��������� �� ���������� ���������-������ (APDU-�������).
//		ifGOST		 - ��� �������������� ��������������� ����/RSA.
//		AppVer		 - ������ ��-���������� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� ���������-������� � ������ ������������� ���������� ������ ������ ���������.
IL_FUNC IL_RETCODE cryptoProcessSM(IL_HANDLE_CRYPTO hCrypto, IL_APDU* pilApdu, IL_BYTE ifGOST, IL_BYTE AppVer);

//  Description:
//		������������� ���������� ������ RSA ��� ������������ ������ ����������� ����� ������ � ������� ������� (���������� ��� �� ���).
//  See Also:
//		cryptoPrepareSM
//		cryptoProcessSM
//  Arguments:
//      hCrypto			  - ��������� �� ���������������� ��������� ���������� ������ ������ �����������. 
//		in_pSessionDataIn - ��������� �� ������� ��������� ��������������� ������.
//		KeyVer			  - ������ ����� ��������������� ������ (��).
//		AppVer			  - ������ ��-���������� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� ���������� ������ RSA ��� ������ �����������-��������� ����� ������ � ������� �������.
IL_FUNC IL_RETCODE cryptoPrepareSession(IL_HANDLE_CRYPTO hCrypto, SESSION_DATA_IN *in_pSessionDataIn, IL_BYTE KeyVer, IL_BYTE AppVer);

//  Description:
//		������������� ���������� ������ ���� ��� ������������ ������ ����������� ����� ������ � ������� ������� (���������� ��� �� ���).
//  See Also:
//		cryptoPrepareSM
//		cryptoProcessSM
//  Arguments:
//      hCrypto			  - ��������� �� ���������������� ��������� ���������� ������ ������ �����������. 
//		in_pSessionDataIn - ��������� �� ������� ��������� ��������������� ������.
//		KeyVer			  - ������ ����� ��������������� ������ (��).
//		AppVer			  - ������ ��-���������� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� ���������� ������ ���� ��� ������ �����������-��������� ����� ������ � ������� �������.
IL_FUNC IL_RETCODE cryptoPrepareSessionGOST(IL_HANDLE_CRYPTO hCrypto, SESSION_DATA_IN* pSessionDataIn, IL_BYTE KeyVer, IL_BYTE AppVer);

//  Description:
//		��������������� ������ ��� ����������� �������������� �������� ������� � �������������� ��������������� ����.
//  See Also:
//		cryptoPrepareMutualAuthDataRsa
//  Arguments:
//      hCrypto		 - ��������� �� ���������������� ��������� ���������� ������ ������ �����������. 
//		in_pDataIn	 - ��������� �� ������� ���������.
//		out_pDataOut - ��������� �� �������������� �������� ������ ��� ��������������.
//		KeyVer		 - ������ ����� ��������������� ������ (��).
//		AppVer		 - ������ ��-���������� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������ ��� �������������� ����.
IL_FUNC IL_RETCODE cryptoPrepareMutualAuthDataGost(IL_HANDLE_CRYPTO hCrypto, MUTUAL_AUTH_DATA_IN *in_pDataIn, MUTUAL_AUTH_DATA_OUT *out_pDataOut, IL_BYTE KeyVer, IL_BYTE AppVer);

//  Description:
//		��������������� ������ ��� ����������� �������������� �������� ������� � �������������� ��������������� RSA.
//  See Also:
//		cryptoPrepareMutualAuthDataRsa
//  Arguments:
//      hCrypto		 - ��������� �� ���������������� ��������� ���������� ������ ������ �����������. 
//		in_pDataIn	 - ��������� �� ������� ���������.
//		out_pDataOut - ��������� �� �������������� �������� ������ ��� ��������������.
//		KeyVer		 - ������ ����� ��������������� ������ (��).
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������ ��� �������������� RSA.
IL_FUNC IL_RETCODE cryptoPrepareMutualAuthDataRsa(IL_HANDLE_CRYPTO hCrypto, MUTUAL_AUTH_DATA_IN *in_pDataIn, MUTUAL_AUTH_DATA_OUT *out_pDataOut, IL_BYTE KeyVer);

//  Description:
//		������������� ���������� ������ � ����������� ������ ��� ����������� ������������������ ������ ������ ����� ���������� � ����������� ������ � ������������ �� ������������� ����� 1.1.
//  See Also:
//  Arguments:
//      in_pSM			- ��������� �� �������������� ������ � ����������� ������. 
//		KeyVer			- ������ ����� ��������������� ������ (��).
//		in_pCspaid		- ��������� �� ����� ����������� ��������� ����� ���������� ������.
//		CspaidLen		- ����� �����������.
//		in_pSPChallenge - ��������� �� ����� � ��������������� ������ ���������� ������ (Application PAN || ACC).	
//		SPChallengeLen  - ����� ���������������� �����.
//		out_pSessData	- ��������� �� �������� ����� � ����������� ������������� ������.
//		ifGost			- ��� ������������� ��������������� ������ ����/RSA.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� ���������� ������ � ����������� ������.
IL_FUNC IL_RETCODE cryptoSetTerminalToProviderSession11(PROVIDER_SM_CONTEXT *in_pSM, IL_BYTE KeyVer, IL_BYTE *in_pCspaid, IL_WORD CspaidLen, IL_BYTE *in_pSPChallenge, IL_DWORD SPChallengeLen, PROVIDER_SESSION_DATA *out_pSessData, IL_BYTE ifGost);

//  Description:
//		��������� �������������� ���������� ������.
//  See Also:
//		opApiAuthServiceProvider
//  Arguments:
//      in_pSM		- ��������� �� �������������� ������ � ����������� ������. 
//		in_pMsg		- ��������� �� ������ ������ �� ������ �������� �����.
//		MsgLen		- ����� ������ ������.
//		in_pMsgSign	- ��������� �� ����� c ���, �������������� ����������� ������ ��� ������ ������ �� ������ �������� �������.
//		MsgSignLen	- ����� �������������� ���.
//		in_pCSpId	- ��������� �� ����� ����������� ��������� ����� ���������� ������.
//		CSpIdLen	- ����� �����������.
//		ifGost		- ��� ������������� ��������������� ������ ����/RSA.
//		AppVer		- ������ ��-����������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      �������������� ���������� ������.
IL_FUNC IL_RETCODE cryptoAuthServiceProvider(PROVIDER_SM_CONTEXT *in_pSM, IL_BYTE *in_pMsg, IL_WORD MsgLen, IL_BYTE *in_pMsgSign, IL_WORD MsgSignLen, IL_BYTE *in_pCSpId, IL_WORD CSpIdLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//		������������� ����������� ������ ������� �������� ������ � ������ ������������� � ������������ �� ������������� ����� 1.1 ������ � ����������� ������.
//  See Also:
//		opApiEncryptProviderToTerminal
//  Arguments:
//      in_pSM		   - ��������� �� �������������� ������ � ����������� ������. 
//		in_pMsg		   - ��������� �� ����� � ������� ��� ����������.
//		MsgLen		   - ����� ������ ��� ����������.
//		out_pEncMsg	   - ��������� �� �������� ����� c �������������� �������.
//		out_pEncMsgLen - ��������� �� ����������, ���������������� ������ ������������� ����� ������������� ������.
//		ifGost		   - ��� ������������� ��������������� ������ ����/RSA.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������ � ������ ������������� ������ � ����������� ������.
IL_FUNC IL_RETCODE cryptoEncryptTerminalToProvider11(PROVIDER_SM_CONTEXT *in_pSM, IL_BYTE *in_pMsg, IL_DWORD MsgLen, IL_BYTE *out_pEncMsg, IL_DWORD *out_pEncMsgLen, IL_BYTE ifGost);

//  Description:
//		�������������� ������ � ������ ������������� � ������������ �� ������������� ����� 1.1 ������ � ����������� ������.
//  See Also:
//		opApiDecryptProviderToTerminal
//  Arguments:
//      in_pSM		    - ��������� �� �������������� ������ � ����������� ������. 
//		in_pMsg		    - ��������� �� ����� � ������� ��� �������������.
//		MsgLen		    - ����� ������ ��� �������������.
//		out_pDecrMsg    - ��������� �� �������� ����� c ��������������� �������.
//		out_pDecrMsgLen - ��������� �� ����������, ���������������� ������ ������������� ����� �������������� ������.
//		ifGost		    - ��� ������������� ��������������� ������ ����/RSA.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ������������� ������ � ������ ������������� ������ � ����������� ������.
IL_FUNC IL_RETCODE cryptoDecryptTerminalToProvider11(PROVIDER_SM_CONTEXT *in_pSM, IL_BYTE *in_pMsg, IL_DWORD MsgLen, IL_BYTE *out_pDecrMsg, IL_DWORD *out_pDecrMsgLen, IL_BYTE ifGost);

//  Description:
//		��������� �������� ������ � ���������� ��������� RSA-����� �� ������ �����������.
//  See Also:
//  Arguments:
//      in_pKeyCert	- ��������� �� RSA-���������� ��������� �����. 
//		KeyCertLen  - ����� �����������.
//		out_pKeyRSA - ��������� �� �������� ����� c� ��������� RSA-��������� ������������ �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� �������� ��������� ����� RSA �� �����������.
IL_FUNC IL_RETCODE cryptoRsaKeyFromCertificate(IL_BYTE *in_pKeyCert, IL_WORD KeyCertLen, KEY_RSA *out_pKeyRSA);

//  Description:
//		��������� �������� ��������� ����� ���� �� ������ �����������.
//  See Also:
//  Arguments:
//      in_pKeyCert		- ��������� �� ����-���������� ��������� �����. 
//		KeyCertLen		- ����� �����������.
//		out_pKeyGostPub - ��������� �� �������� ����� c� ��������� ������������ ����� ����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� �������� ��������� ����� ���� �� �����������.
IL_FUNC IL_RETCODE cryptoGostKeyFromCertificate(IL_BYTE *in_pKeyCert, IL_WORD KeyCertLen, KEY_GOST_PUB *out_pKeyGostPub);

//  Description:
//		��������� ������� �� ��������� RSA-�����.
//  See Also:
//  Arguments:
//		in_pMsg		- ��������� �� ������������� ���������.
//      MsgLen		- ����� �������������� ���������. 
//		in_pSign	- ��������� �� ����������� �������
//		SignLen		- ����� ����������� �������.
//		in_pKeyRSA	- ��������� �� ������������� RSA ����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      �������� RSA-�������.
IL_FUNC IL_RETCODE cryptoVerifyRsaSignature(IL_BYTE *in_pMsg, IL_WORD MsgLen, IL_BYTE *in_pSign, IL_WORD SignLen, KEY_RSA *in_pKeyRSA);

//  Description:
//		��������� �������� ��������� ����� �� ������ �����������.
//  See Also:
//  Arguments:
//      in_pCert	 - ��������� �� ���������� ��������� �����. 
//		CertLen		 - ����� �����������.
//		out_pKey	 - ��������� �� �������� ����� c� ��������� ������������ �����.
//		ifGost		 - ��� ������������� ��������������� ����/RSA.
//		ifCompressed - ������� �������� ������� �������� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� �������� ��������� ����� �� �����������.
IL_FUNC IL_RETCODE cryptoPublicKeyFromCertificate(IL_BYTE *in_pCert, IL_WORD CertLen, IL_BYTE *out_pKey, IL_WORD *out_pKeyLen, IL_BYTE ifGost, IL_BYTE ifCompressed);

//  Description:
//		��������� �������� ������� ��� ���������� ���������.
//  See Also:
//		cryptoCheckMessageSignature
//  Arguments:
//      in_pMsg		 - ��������� �� ������������� ���������. 
//		MsgLen		 - ����� ������������� ���������.
//		in_pPrivKey	 - ��������� �� �������� ����, ������������ ��� ���������� �������.
//		PrivKeyLen	 - ����� ��������� �����.
//		out_pSign	 - ��������� �� �������� ����� ��� ����������� �������.
//		out_pSignLen - ��������� �� ����������, ���������������� ��������� ����� �������.
//		ifGost		 - ��� ������������� ��������������� ����/RSA.
//		AppVer		 - ������ ��-����������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� �������� �������.
IL_FUNC IL_RETCODE cryptoCalcMessageSignature(IL_BYTE *in_pMsg, IL_WORD MsgLen, IL_BYTE *in_pPrivKey, IL_WORD PrivKeyLen, IL_BYTE *out_pSign, IL_WORD *out_pSignLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//		��������� ������������� �������� ������� ��� ���������� ���������.
//  See Also:
//		cryptoCalcMessageSignature
//  Arguments:
//      in_pMsg		 - ��������� �� ���������. 
//		MsgLen		 - ����� ���������.
//		in_pSign	 - ��������� �� ����������� �������� ������� ���������.
//		SignLen		 - ����� �������.
//		in_pPubKey	 - ��������� �� �������� ����, ������������ ��� �������� �������.
//		PubKeyLen	 - ����� ��������� �����.
//		ifGost		 - ��� ������������� ��������������� ����/RSA.
//		AppVer		 - ������ ��-����������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      �������� ������������� �������� �������.
IL_FUNC IL_RETCODE cryptoCheckMessageSignature(IL_BYTE *in_pMsg, IL_WORD MsgLen, IL_BYTE *in_pSign, IL_WORD SignLen, IL_BYTE *in_pPubKey, IL_WORD PubKeyLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//		���������� ��������� ����� ��������� ��������� �����.
//  See Also:
//  Arguments:
//      out_pRandom	 - ��������� �� �������� ����� ��� ���������� �����. 
//		RandomLen	 - ����� ������������� ���������� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� ���������� ����� ���������.
IL_FUNC IL_RETCODE GetRandom(IL_BYTE *out_pRandom, IL_DWORD RandomLen);

//  Description:
//		��������� ���-�������� �����.
//  See Also:
//  Arguments:
//      in_pSnils	 - ��������� �� ������� ����� �������� 6 ���� � "�����" ��������� �����. 
//		out_pHashBuf - ��������� �� �������� ����� � ����������� ���-��������� �����.
//		out_pHashLen - ��������� �� ������������ �������� ����� ������������ ���-�������� �����.
//		ifGost		 - ��� ������������� ��������������� ����/RSA.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ���-�������� �����.
IL_FUNC IL_RETCODE cryptoGetHashSnils(IL_BYTE *in_pSnils, IL_BYTE *out_pHashBuf, IL_WORD *out_pHashLen, IL_BYTE ifGost);



IL_FUNC IL_RETCODE RsaKeyFromCertificateEx(IL_TAG rootTag, IL_BYTE* pKeyCert, IL_WORD wKeyCertLen, KEY_RSA* pKeyRSA);
IL_FUNC IL_RETCODE GostKeyFromCertificateEx(IL_TAG rootTag, IL_BYTE* pKeyCert, IL_WORD wKeyCertLen, KEY_GOST_PUB* pKeyGostPub);
IL_FUNC void MakeRsaCertificate(IL_BYTE* msg, IL_WORD msg_len, KEY_RSA* pKeyRSA, IL_BYTE* cert, IL_WORD* cert_len);
IL_FUNC void EncryptRsa(IL_BYTE* data, IL_WORD data_len, KEY_RSA* pKeyRSA, IL_BYTE* enc_data, IL_WORD* enc_data_len);
IL_FUNC IL_RETCODE DecryptRsa(IL_BYTE* enc_data, IL_WORD enc_data_len, KEY_RSA* pKeyRSA, IL_BYTE* data, IL_WORD* data_len);
IL_FUNC void KDF(IL_BYTE* MSK16, IL_BYTE* R, IL_WORD R_len, IL_BYTE* SMK16);
IL_FUNC void KDF_GOST(IL_BYTE* MSK32, IL_BYTE* R, IL_WORD R_len, IL_BYTE* SMK32);
IL_FUNC void MKDF(IL_BYTE* IMK16, IL_BYTE* DD, IL_WORD DD_len, IL_BYTE* MK16);
IL_FUNC void MKDF_GOST(IL_BYTE* IMK32, IL_BYTE* DD, IL_WORD DD_len, IL_BYTE* MK32);
IL_FUNC IL_RETCODE cryptoSetTerminalToProviderSessionRsa(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Cspaid_buf, IL_WORD wCspaid_size, IL_BYTE* msg, IL_DWORD msg_len, PROVIDER_SESSION_DATA* pSessDataOut);
IL_FUNC IL_RETCODE cryptoSetTerminalToProviderSessionGost(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* bPubKey64, IL_BYTE* bPrivKey32, IL_BYTE* Msg, IL_DWORD Msg_len, IL_BYTE* bRand, IL_WORD wRandLen);
IL_FUNC IL_RETCODE cryptoEncryptTerminalToProvider(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* EncMsg, IL_DWORD* pdwMsgLenEncrypted, IL_BYTE ifGost);
IL_FUNC IL_RETCODE cryptoDecryptTerminalToProvider(PROVIDER_SM_CONTEXT* pSM, IL_BYTE* Msg, IL_DWORD MsgLen, IL_BYTE* DecryptedMsg, IL_DWORD* pdwDecryptedMsgLen, IL_BYTE ifGost);

#endif