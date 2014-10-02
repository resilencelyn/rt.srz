#ifndef _HAL_HOST_H_
#define _HAL_HOST_H_

#include "il_types.h"
#include "HAL_Common.h"
#include "HAL_Crypto.h"
#include "Tlv.h"
#include "TAG.h"
#include "il_error.h"
#include "FuncLib.h"

// �������������� ���������� ������ ����� ��-����������� � ��������� ��-���������� �� ������� ��������� �����
typedef struct 
{
	SM_CONTEXT SM;
} HANDLE_CRYPTO_HOST;

// �������������� ���������� ������ ����� ����������� ������ � ���������� �� ������� ��������� �����
typedef struct
{
    IL_BYTE SK_sm_id_smc_des[16];	// ���������� ���� ���������� RSA
    IL_BYTE SK_sm_id_smc_gost[32];	// ���������� ���� ���������� ����
} HOST_PROVIDER_SM_CONTEXT;

//  Description:
//      ��������� ��� ������ ���������� ������ �� ������ �������� ������ ��� ����������� �������������� �� ������� ���������.<p/>
//		�������� �������������� ���������� ����� ������������ �������� ���������� ������������� ������, ���������� � ������ �� ������ �������� ������. 
//		�������������� ���������� ����� �������������� ������ � ��� ������, ���� � �������������� �� ������ ������������ ��������������� �������.
//  See Also:
//  Arguments:
//		in_pMsg		 - ��������� �� ������������� ���������.
//		MsgLen		 - ����� �������������� ���������.
//      out_pSign    - ��������� �� ��������� ������ ��� ����������� �������� �������.
//		out_pSignLen - ����� �������������� �������� �������.
//		ifGost		 - ������� ������������ ������� � �������������� ��������������� ����. ����� ������������ RSA.
//		AppVer		 - ������ ��-���������� ������������� � ����� �����.
//  Return Value:
//      IL_RETCODE	 - ��� ������.
//  Summary:
//      ������������ ����������� ������ ��� ������ �� ������ �������� ������.
IL_FUNC IL_RETCODE hostAuthServiceProvider(IL_BYTE *in_pMsg, IL_WORD MsgLen, IL_BYTE *out_pSign, IL_WORD *out_pSignLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      ��������� �������������� ���������� ������ �� �������������� ��-���������� � ������ Online.<p/>
//		�������� � ��� ������������� ����� ��� ���������� ������������ ��-���������� �� ������ ����� ������� � ������ 'Keys' ����������������� ����� ��������� ����� 'host.ini'.  
//  See Also:
//  Arguments:
//		in_pAuthReqData	- ��������� �� ������� ����� � TLV-������� ������� �� �������������� ��-���������� � ������ Online.
//		AuthReqDataLen	- ����� ��������������� �������.
//		ifGost	- ������� ������������ ������������ �������������� � �������������� ��������������� ����. ����� ������������ RSA.
//		AppVer	- ������ ��-���������� ������������� � ����� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      �������� ������� �� �������������� ��-���������� � ������ Online.
IL_FUNC IL_RETCODE hostCheckAuthRequestOnline(IL_BYTE *in_pAuthReqData, IL_WORD AuthReqDataLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      ��������� �������������� ���������� ������ �� �������������� ��-���������� � ������ Offine.<p/>
//		�������� � ��� ����� 'MkAcId' ��� ���������� ������������ ��-���������� �� ������ ��������� ����� ������� � ������ 'Keys' ����������������� ����� 'host.ini'.  
//  See Also:
//  Arguments:
//		in_pAuthReqData	- ��������� �� ������� ����� � TLV-������� ������� �� �������������� ��-���������� � ������ Online.
//		AuthReqDataLen	- ����� ��������������� �������.
//		ifGost	- ������� ������������ ������������ �������������� � �������������� ��������������� ����. ����� ������������ RSA.
//		AppVer	- ������ ��-���������� ������������� � ����� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      �������� ������� �� �������������� ��-���������� � ������ Offline.
IL_FUNC IL_RETCODE hostCheckAuthRequestOffline(IL_BYTE *in_pAuthReqData, IL_WORD AuthReqDataLen, IL_BYTE ifGost, IL_BYTE AppVer); 

//  Description:
//      �������������� ���������� ������ ����� ��-����������� ����� ��� � ��������� ��-���������� �� ������� ��������� �����.<p/>
//		�������� � ��� ����� 'MkSmId' ��� ����������� ������ ����� ���������� ����� �� ������ ��������� ����� ������� � ������ 'Keys' ����������������� ����� 'host.ini'.<p/>
//		����� �������� ��������� ����������� ������ �������� ������������ ���� ������������� ������� APDU-������, ������� �� �� ����� ��� ���������� � ����������� ������ ��� �� ��������� �� ������� �����.
//  See Also:
//  Arguments:
//		hCryptoHost		 - ��������� �� ��������� ��� ��������������� ������ �� ������� �����.
//		in_pAuthReqData	 - ��������� �� ������� ����� � TLV-������� ������� �� �������������� ��-���������� � ������ Online.
//		AuthReqDataLen	 - ����� ��������������� �������.
//		in_pSessionData  - ��������� �� ��������� ������� ������ �� ��������� ������, ��������������� ������. 
//		out_pSessionData - ��������� �� ��������� �������� ������ � ������������������ ������� ������������ �������������� ����� � ���������� ����� �����.
//		ifGost	- ������� ��������� ���������� ������ � �������������� ��������������� ����. ����� ������������ RSA.
//		AppVer	- ������ ��-���������� ������������� � ����� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� ���������� ������ ����� ������ � ��������� ��-����������.
IL_FUNC IL_RETCODE hostPrepareIssuerSession(HANDLE_CRYPTO_HOST *hCryptoHost, IL_BYTE *in_pAuthReqData, IL_WORD AuthReqDataLen, ISSUER_SESSION_DATA_IN *in_pSessionData, ISSUER_SESSION_DATA_OUT *out_pSessionData, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      ��������� ������������� ���������� ������ ����� ��-����������� ����� ��� � ��������� ��-���������� �� ������� ��������� �����.<p/>
//		����� �������� ��������� ����������� ������ �������� ������������ ���� ������������� ������� APDU-������, ������� �� �� ����� ��� ���������� � ����������� ������ ��� �� ��������� �� ������� �����.
//  See Also:
//  Arguments:
//		hCryptoHost			 - ��������� �� ��������� ��� ��������������� ������ �� ������� �����.
//		in_pCheckSessionData - ��������� �� ��������� ������� ������ �� ��������� ������ ����� � ����������� ������������� �����.
//		ifGost				 - ������� ��������� ���������� ������ � �������������� ��������������� ����. ����� ������������ RSA.
//		AppVer				 - ������ ��-���������� ������������� � ����� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      �������� ������������� ���������� ������ ����� ������ � ��������� ��-����������.
IL_FUNC IL_RETCODE hostCheckIssuerSession(HANDLE_CRYPTO_HOST *hCryptoHost, CHECK_ISSUER_SESSION_DATA_IN *in_pCheckSessionData, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      ������� ���� ������ � ������ ������������� ����� ���������� ������ ����� ����������� ������ � ���������� ��� ����������� ������������������ ������ �������.<p/>
//  See Also:
//		opCtxSetProviderEncrDecrBuf
//		hostEncryptServiceProviderData
//		hostPrepareServiceProviderSession
//  Arguments:
//		pSM				- ��������� �� �������������� ��������� ����� ��� ������ ���������� ������.
//		in_pClearData	- ��������� �� ���� �������� ������ ��� ����������.
//		ClearDataLen	- ����� ��������� ������.
//		out_pEncData	- ��������� �� �������� ����� ��� ������������� ������.
//		out_pEncDataLen - ��������� �� ����� ������������� ������.
//		ifGost			- ������� ���������� ������ � �������������� ��������������� ����. ����� ������������ RSA.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ����� ������ ����������� ������.
IL_FUNC IL_RETCODE hostEncryptServiceProviderData(HOST_PROVIDER_SM_CONTEXT *pSM, IL_BYTE *in_pClearData, IL_DWORD ClearDataLen, IL_BYTE *in_pEncData, IL_DWORD *pEncDataLen, IL_BYTE ifGost);

//  Description:
//      �������������� ���� ������ � ������ ������������� ����� ���������� ������ ����� ����������� ������ � ���������� ��� ����������� ������������������ ������ �������.<p/>
//  See Also:
//		opCtxSetProviderEncrDecrBuf
//		hostEncryptServiceProviderData
//		hostPrepareServiceProviderSession
//  Arguments:
//		pSM					  - ��������� �� �������������� ��������� ����� ��� ������������� ������.
//		in_pEncryptedData	  - ��������� �� ���� ������������� ������ ��� �������������.
//		ClearDataLen		  - ����� ������������� ������.
//		out_pDecryptedData	  - ��������� �� �������� ����� ��� �������������� ������.
//		out_pDecryptedDataLen - ��������� �� ����� �������������� ������.
//		ifGost				  - ������� ���������� ������ � �������������� ��������������� ����. ����� ������������ RSA.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ������������� ����� ������ ����������� ������.
IL_FUNC IL_RETCODE hostDecryptServiceProviderData(HOST_PROVIDER_SM_CONTEXT *pSM, IL_BYTE *in_pEncryptedData, IL_DWORD EncryptedDataLen, IL_BYTE *out_pDecryptedData, IL_DWORD *out_pDecryptedDataLen, IL_BYTE ifGost);

//  Description:
//      �������������� ������������ ������ APDU-������ � ������ ������������� ����� ���������� ������ ����� ��-����������� � ��������� ��-����������.
//  See Also:
//		opApiRunApduPacket
//  Arguments:
//		pSM				  - ��������� �� �������������� ��������� ����� ��� ������������� ������.
//		in_pApduSequence  - ��������� �� �������������� ��������� ����� APDU-������ � �������� ����.
//		ApduNum			  - ���������� APDU-������ � ������.
//		ifGost			  - ������� ���������� ������ � �������������� ��������������� ����. ����� ������������ RSA.
//		AppVer			  - ������ ��-���������� ������������� � ����� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������������ ������ APDU-������.
IL_FUNC IL_RETCODE hostPrepareApdus(SM_CONTEXT *pSM, IL_APDU_PACK_ELEM *in_pApduSequence, IL_WORD ApduNum, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      �������������� ������ ������ APDU-������ ���������� � ������ ������������� ����� ���������� ������ ����� ��-����������� � ��������� ��-����������.
//  See Also:
//		opApiRunApduPacket
//  Arguments:
//		pSM				  - ��������� �� �������������� ��������� ����� ��� ������������� ������.
//		in_pApduSequence  - ��������� �� �������������� ���������� ����� APDU-������ � ������������ �������� � ��������� �������.
//		ApduNum			  - ���������� APDU-������ � ������.
//		ifGost			  - ������� ���������� ������ � �������������� ��������������� ����. ����� ������������ RSA.
//		AppVer			  - ������ ��-���������� ������������� � ����� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ����������� ������� ������������ ������ APDU-������.
IL_FUNC IL_RETCODE hostProcessApdus(SM_CONTEXT *pSM, IL_APDU_PACK_ELEM *in_pApduSequence, IL_WORD ApduNum, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      �������������� ������ ������ �� ������ �������������� ��-���������� ��� ����������� ��������� ������ ����������.<p/>
//		�������� ������ ������ 'MemberId', 'IdentOpId', 'PaymentInfo' � 'AAC' ����������� �������������� ��������� ������ 'Data' ����������������� ����� 'host.ini'.   
//  See Also:
//		opApiCheckAppAuthResponse	
//  Arguments:
//		in_pAuthReqData		 - ��������� �� ������� ����� � TLV-������� ������� �� �������������� ��-���������� � ������ Online.
//		AuthReqDataLen		 - ����� ��������������� �������.
//		out_pAuthRespData	 - ��������� �� �������� ����� ��� ������������ ������ �� ������ �������������� ��-����������.
//		out_pAuthRespDataLen - ��������� �� ����� ��������������� ������.
//		ifGost				 - ������� ������������� ��������������� ����. ����� ������������ RSA.
//		AppVer				 - ������ ��-���������� ������������� � ����� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������ �� ������ �������������� ��-����������.
IL_FUNC IL_RETCODE hostPrepareAuthResponse(IL_BYTE *in_pAuthReqData, IL_WORD AuthReqDataLen, IL_BYTE *out_pAuthRespData, IL_WORD *out_pAuthRespDataLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      �������������� �� ������� ��������� ����� ���������� ������ ����� ����������� ������ � ����������.<p/>
//		��� ���������� ������ ������������ �������� ��������� ����� ����������� ���������� ������ 'CSpId' ����������������� ����� 'host.ini', 
//		� ������ ������������� ������ �������������� ������������������ ������ ������� ������/����� ��� �������� ������.<p/>
//		���������� ���������� � ����������� ������ ��������������� ������ � ��� ������, ���� ����� ��������� ���������� � �������������� �� ������.
//		
//  See Also:
//		opCtxGetProviderSessionData
//		hostEncryptServiceProviderData
//		hostDecryptServiceProviderData
//  Arguments:
//		pSM					     - ��������� �� �������������� ��������� ����� ��� ������������� ������.
//		in_SPChallehge		     - ��������� �� ���� ������ c ��������������� ������, ����������� ����������� ������ (PAN||ACC).
//		ClearDataLen		     - ����� ����� ������ ���������������� �����.
//		in_pProviderSessionDara -  ��������� �� ������� ��������� � ������� ������������� ������.
//		ifGost				     - ������� �������������� ��������������� ���� ��� ���������������� ������. ����� ������������ RSA.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ���������� ������ ����� ����������� ������ � ����������.
IL_FUNC IL_RETCODE hostPrepareServiceProviderSession(HOST_PROVIDER_SM_CONTEXT *pSM, IL_BYTE *in_SPChallenge, IL_DWORD SPChallengeLen, PROVIDER_SESSION_DATA *in_pProviderSessionDara, IL_BYTE ifGost);

//  Description:
//      �������������� ���������� ������ ����� ������� ������������ (�� ���) � ��������� ��-���������� �� ������� ��������� �����.<p/>
//		����� �������� ��������� ����������� ������ �������� ������������ ���� ������������� ������� APDU-������, ������� �� �� �� ��� ���������� � ����������� ������ ��� �� ��������� �� ������� �����.
//  See Also:
//		opCtxGetSeIssuerSessionIcChallenge
//		opCtxSetSeIssuerSessionCryptogramm
//  Arguments:
//		hCryptoHost		 - ��������� �� ��������� ��� ��������������� ������ �� ������� �����.
//		in_pSessionData  - ��������� �� ��������� ������� ������ �� ��������� ������, ��������������� ��. 
//		out_pSessionData - ��������� �� ��������� �������� ������ � ������������������ ������� ������������ �������������� ����� � ���������� ����� �����.
//		ifGost			 - ������� ��������� ���������� ������ � �������������� ��������������� ����. ����� ������������ RSA.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� ���������� ������ ����� ������� ������������ � ��������� ��-����������.
IL_FUNC IL_RETCODE hostPrepareSmIssuerSession(HANDLE_CRYPTO_HOST *hCryptoHost, SM_SESSION_DATA_IN *in_pSessionData, SM_SESSION_DATA_OUT *out_pSessionData, IL_BYTE ifGost);

IL_FUNC IL_RETCODE hostSetTerminalToProviderSessionRsa(HOST_PROVIDER_SM_CONTEXT* pSM, IL_BYTE* pPrivateProviderKeyCert, IL_WORD wPrivateProviderKeyCertLen, IL_BYTE* msg, IL_DWORD msg_len, IL_BYTE* pY, IL_WORD wY_len);
IL_FUNC IL_RETCODE hostSetTerminalToProviderSessionGost(HOST_PROVIDER_SM_CONTEXT* pSM, IL_BYTE* bPubKey64, IL_BYTE* bPrivKey32, IL_BYTE* msg, IL_DWORD msg_len, IL_BYTE* bRand, IL_WORD wRand);
IL_FUNC IL_RETCODE hostPrepareUnlockPukApdus(HANDLE_CRYPTO_HOST* hCrypto, IL_BYTE *in_pAuthRequest, IL_WORD in_wAuthRequestLen, IL_BYTE ifGostCrypto, IL_BYTE AppVer, IL_BYTE *out_ApduIn, IL_WORD *out_ApduInLen);
IL_FUNC IL_RETCODE hostPrepareEditIdDataApdus(HANDLE_CRYPTO_HOST* hCrypto, IL_TAG TagId, IL_CHAR *Data, IL_BYTE ifGostCrypto, IL_BYTE *out_ApduIn, IL_WORD *out_ApduInLen);
IL_FUNC IL_RETCODE hostPrepareEditPrivateDataApdus(IL_TAG TagId, IL_CHAR *Data, IL_BYTE *out_ApduIn, IL_WORD *out_ApduInLen);


#endif//_HAL_HOST_H_