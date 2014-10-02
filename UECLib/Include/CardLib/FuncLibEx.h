#ifndef _FUNCLIB_EX_H_
#define _FUNCLIB_EX_H_

#include "CardLibEx.h"
#include "HAL_SCRApdu.h"

//	������������ ���������� ���������� SW1-SW2 ������� �����
#define ALLOWED_RES_MAX			20

// ������� ������ APDU-�������.
typedef struct
{
	IL_APDU	Apdu;							// ��������� APDU-�������
	IL_BYTE allowed_res_len;				// ���������� ���������� SW1-SW2 ������� ����� 
	IL_BYTE allowed_res[ALLOWED_RES_MAX*2];	// ������ ���������� SW1-SW2 ������� �����
} IL_APDU_PACK_ELEM;

//  Description:
//      ��������� ������������� ����-������.
//  See Also:
//		flDeinitReader
//  Arguments:
//      phCard			- ��������� �� ��������� �����.
//      ilRdrSettings	- ��������� �� ���������������� ��������� ����-������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ������������� ����-������.
IL_FUNC IL_RETCODE flInitReader(IL_CARD_HANDLE* phCrd, IL_READER_SETTINGS ilRdrSettings);

//  Description:
//      ��������� ��������������� ����-������.
//  See Also:
//		flInitReader
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������������� ����-������.
IL_FUNC IL_RETCODE flDeinitReader(IL_CARD_HANDLE* phCrd);

//	Description:
//		��������� ����� ���������� ����� ���.<p/>
//		������������ �������������� �������� � ���������:
//			* �������������� � ��������� ������ ����������.
//			* �������������� � ��������� ���� �������� ����������.
//			* �������������� �������� � ���������� ���������� � ������������� ��� ������������� ������ ��������������� ���� ��� RSA.
//			* �������������� ����� ��������� ��������� ����������� �������, ��������� ������ "������� ������" � ������� MSE.
//			* ��������� ������� �������� �������� � �������������� ������ ����������� �� ����� �������� ���������� ������.
//			* ��������� ������� ����������� ��������� ����� ��������� � ���.
//			* �������������� ������ ����������.
//  See Also:
//		flAppReselect
//	Arguments:
//		phCard		 - ��������� �� ��������� �����.
//		out_pData	 - ��������� �� ����� ��� ������������ ������ ������ ����������.
//		out_pDataLen - ��������� �� ����� ������������ ������.
//   Return Value:
//		IL_RETCODE - ��� ������.
//   Summary:
//		����� ���������� ����� ���, �������������� ������� � ���������.  
IL_FUNC IL_RETCODE flAppSelect(IL_CARD_HANDLE* phCrd, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

//	Description:
//		��������� ��������� ����� ���������� ����� ��� ��� �������� � �������� ����������.<p/>
//		��� ������������������ ��� �������������� ������ ����������, ��������� �������� � ����.
//  See Also:
//		flAppSelect
//  Arguments:
//		phCard - ��������� �� ��������� �����.
//  Return Value:
//		IL_RETCODE - ��� ������.
//  Summary:
//		��������� ����� ���������� �����. inalAuth
IL_FUNC IL_RETCODE flAppReselect(IL_CARD_HANDLE* phCrd);

//  Description:
//		��������� ��������� �������������� ���������.<p/>
//		������ ��������� ������������� ��� ����, ����� ����������������� ���������� ���������, ��� �������� ��������
//		�������� ������, ������� ������������� ��������� �����, ���������� � ����������� ��������� ����� ���������.<p/>
//		� �������� �������������� ���������:
//			* ������������ �������� ������������ ���������.
//			* ����������� ��������� ������������ �������������� � �������������� �������������� ����� ��������������� ���� ��� RSA.
//			* ��������������� ���������� ������ ��� ������ ����������� ����� ���������� � ������.
//	See Also:
//		flAppSelect
//  Arguments:
//		phCard - ��������� �� ��������� �����.
//  Return Value:
//		IL_RETCODE - ��� ������.
//	Summary:
//		�������������� ���������.
IL_FUNC IL_RETCODE flAppTerminalAuth(IL_CARD_HANDLE* phCrd);

//  Description:
//		������������ ��������� ����������� ��������� ����� ���.<p/> 
//		��������� ��������� ������������� ��� �������� ��-����������� ������������� ������������� ����� ���. 
//		���������� ������ ����� ��������� ���������� �������������� ���������. 
//  See Also:
//		opApiVerifyCitizen
//		flGetPassPhrase
//  Arguments:
//      phCard			   - ��������� �� ��������� �����.
//		PinNum			   - ����� ������������� ��� ����������� ������.
//		in_pPinBlock8	   - ��������� �� ���-���� �� ��������� ������ � ������� ISO/IEC 9564-3:2002 (Format 2).
//		out_pTriesRemained - ��������� �� ����������, ���������������� ��������� ���������� ���������� ������� ������������ ������.
//  Return Value:
//      IL_WORD	- ��� ������.
//  Summary:
//      ����������� ��������� �����.
IL_FUNC IL_RETCODE flAppCitizenVerification(IL_CARD_HANDLE *phCrd, IL_BYTE PinNum, IL_BYTE *in_pPinBlock8, IL_BYTE *out_pTriesRemained);

//  Description:
//		�������� �������� ���-���� ����� ��� �� �����.<p/>
//		����� ������� ���� ������� ������ ���� ������� ��������� ��������� ����������� ��������� �����.
//  See Also:
//		opApiManagePinPuk
//		opApiVerifyCitizen
//  Arguments:
//      phCard			 - ��������� �� ��������� �����.
//		PinNum			 - ����� ����������� ���-����.
//		in_pNewPinBlock8 - ��������� �� ���-���� � ����� ��������� ������ � ������� ISO/IEC 9564-3:2002 (Format 2).
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ����� ���.
IL_FUNC IL_RETCODE flAppChangePIN(IL_CARD_HANDLE* phCrd, IL_BYTE PinNum, IL_BYTE *in_pNewPinBlock8);

//	Description:
//		��������� ������ �� �������������� ��-���������� ��� �������� ������.
//  See Also
//		opApiPrepareAppAuthRequest
//  Arguments:
//		phCard			 - ��������� �� ��������� �����.
//		ifOnline		 - ������� ������������ ������� ��� �������������� � ������ Online.
//      in_pTermAuthData - ��������� �� ����� c �������� TLV-������� ���������:
//							* ������������� ������ '9F15'.
//							* ��������� ����� ��������� '9F37'.
//							* ����� ������� ���������� �������� �� ����� ��������� '9F21'.
//							* �������������� �������� �� �������� '9F03'.
//							* ���-�������� ������� �� �������� ������ 'DF02'.
//							* �������� � ��������� '9F1C'.
//							* ������ �������� ��������� ����� ��������� '9F1D' (��� ���� ������ 1.1).
//							* �������� �� ����������������� ���������� 'C2' (��� ���� ������ 1.1).
//							* ����� ������ ������������������ ���������� '9F08' (��� ���� ������ 1.1).
//							* �������� ���� ��������� '9F1E' (��� ���� ������ 1.1).
//   TermAuthDataLen	-  ����� ������� TLV\-������ ���������.
//   out_pAuthData      -  ��������� �� ����� ��� ������������ �������.
//   inout_pAuthDataLen -  ��������� �� ������������ ������ ������ � ����������� ����� ��������������� �������.
//   Return Value:
//		IL_WORD - ��� ������.
//   Summary:
//		������������ ������� �� �������������� ��-���������� ��� �������� ������.                                                
IL_FUNC IL_RETCODE flAppAuthRequest(IL_CARD_HANDLE *phCrd, IL_BYTE ifOnline, IL_BYTE *in_pTermAuthData, IL_DWORD TermAuthDataLen, 
									IL_BYTE *out_pAuthData, IL_WORD *inout_pAuthDataLen);

//	Description:
//		��������� ������ �� �������������� ��-���������� ��� ��������� ���������� ������ � ���������.
//	See Also:
//		opApiPrepareAppAuthRequestIssSession
//  Arguments:
//		phCard           - ��������� �� ��������� �����.
//		in_pTermAuthData - ��������� �� ����� c �������� TLV-������� ���������:
//							* ������������� ������ '9F15' ���������������� �������� ����������.
//                          * ��������� ����� ��������� '9F37'.
//                          * ����� ������� ���������� �������� �� ����� ��������� '9F21'.
//                          * �������������� �������� �� �������� '9F03'. ��� �������� '������������� ���'
//                            ���������������� ���&#45;������ ��� ���������� �������� ���, ������������ ����������. 
//							  ����� ���������������� �������� ����������.
//                          * ���-�������� ������� �� �������� ������ 'DF02' ���������������� �������� ����������.
//							* �������� � ��������� '9F1C'.
//							* ������ �������� ��������� ����� ��������� '9F1D' (��� ���� ������ 1.1).
//							* �������� �� ����������������� ���������� 'C2' (��� ���� ������ 1.1).
//                          * ����� ������ ������������������ ���������� '9F08' (��� ���� ������ 1.1).
//                          * �������� ���� ��������� '9F1E' (��� ���� ������ 1.1).
//		TermAuthDataLen - ����� ������� TLV\-������ ���������.
//		out_pAuthData - ��������� �� ����� ��� ������������ �������.
//		inout_pAuthDataLen - ��������� �� ������������ ������ ������ � ����������� ����� ��������������� �������.
//   Return Value:
//		IL_WORD - ��� ������.
//   Summary:
//		������������ ������� �� �������������� ��-���������� ��� ��������� ���������� ������ � ���������. 
IL_FUNC IL_RETCODE flAppAuthRequestIssSession(IL_CARD_HANDLE *phCrd, IL_BYTE *in_pTermAuthData, IL_DWORD TermAuthDataLen, 
											  IL_BYTE *out_pAuthData, IL_WORD *inout_pAuthDataLen);

//  Description:
//		��������� ������������� ������ ����������� �������������� ��-���������� � ���������� ��� ��������������.
//  See Also:
//		opApiPrepareAppAuthRequest
//		opApiCheckAppAuthResponse
//		flAppAuthRequest
//  Arguments:
//      phCard					- ��������� �� ��������� �����.
//		in_pAuthData			- ��������� �� ����� ��������������� ����� ������� �� �������������� ��-���������� ��� �������� ������.
//		AuthDataLen				- ����� ������� �� ��������������.
//		in_pAuthResponseData	- ��������� �� ����� � ������� ����������� �������������� ��-����������.
//		in_AuthResponseDataLen	- ����� ������ ���������� ��������������.
//		out_pAuthResult			- ��������� �� ����������, ���������������� ��������� ������������� ���� �������������� ��-����������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      �������� ����������� �������������� ��-����������.
IL_FUNC IL_RETCODE flAppAuthCheckResponse(IL_CARD_HANDLE *phCrd, IL_BYTE *in_pAuthData, IL_WORD AuthDataLen, IL_BYTE *in_pAuthRespData, IL_WORD AuthRespDataLen, IL_WORD *out_pAuthResult);

//  Description:
//		�������� �������� ��� ����� ��� �� �����.<p/>
//		����� ������� ���� ������� ������ ���� ������� ��������� ��������� ����������� ��������� ����� �� ���.
//  See Also:
//		opApiManagePinPuk
//		opApiVerifyCitizen
//  Arguments:
//      phCard			 - ��������� �� ��������� �����.
//		in_pNewPinBlock8 - ��������� �� ���-���� � ����� ��������� ��� � ������� ISO/IEC 9564-3:2002 (Format 2).
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ����� ���.
IL_FUNC IL_RETCODE flAppChangePUK(IL_CARD_HANDLE *phCrd, IL_BYTE *in_pNewPukBlock8);

//  Description:
//		������������ ��� ����� ���.<p/>
//		����� ������� ���� ������� ������ ���� ������� ��������� ��������� ����������� ��������� ����� �� ���.
//  See Also:
//		opApiManagePinPuk
//		opApiVerifyCitizen
//  Arguments:
//      phCard			 - ��������� �� ��������� �����.
//		PinNum			 - ����� ��������������� ���-����.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ������������� ���.
IL_FUNC IL_RETCODE flAppUnlockPIN(IL_CARD_HANDLE *phCrd, IL_BYTE PinNum);

//  Description:
//		����������� ��������� ���� � ��������� ���� ������.
//  See Also:
//  Arguments:
//      phCard		 - ��������� �� ��������� �����.
//		FileId		 - ������������� �����.
//		DataId		 - ������������� ���� ������:
//						- '0000' - ������ ����������� ��������� ����� � TLV-�������.
//						- 'FFFF' - ����������� ������ ������ ����������� ���������� ����� ����������.
//						- ����� - ������������� ���� ������������ �� TLV-����� �������� �������� ������. 
//		out_pData	 - ��������� �� ����� ��� ������������ ������.
//		out_pDataLen - ��������� �� ����� ������������ ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ������ ����� ������ �� �����.
IL_FUNC IL_RETCODE flAppReadBlock(IL_CARD_HANDLE *phCrd, IL_WORD FileId, IL_WORD DataId, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

//  Description:
//		������������ �������������� �������� ��-����������.  
//  See Also:
//		opApiSetIssuerCryptoSession
//  Arguments:
//      phCard				    - ��������� �� ��������� �����.
//		in_pHostData20		    - ��������� �� ����� � ������������������ ������� ������������ �������������� ����� (4 �����) � ���������� ����� ����� (16 ����).
//		HostDataLen			    - ����� ������ �����.
//		out_pCardCryptogramm4   - ��������� �� ����� ��� ������������ ������������ �������������� ����� (4 �����).
//		out_pCardCryptogrammLen - ��������� �� ����� ������������ ������ ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      �������������� �������� ��-����������.
IL_FUNC IL_RETCODE flIssuerAuth(IL_CARD_HANDLE *phCrd, IL_BYTE *in_pHostData20, IL_WORD HostDataLen, 
								IL_BYTE *out_pCardCryptogramm4, IL_WORD *out_pCardCryptogrammLen);

//  Description:
//      ��������� ������ ������ �� ��������� ����� �� ���������� ��������.
//  See Also:
//		flReadBinaryEx
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//      Offset		- �������� � ������ �� ������ ����� �� ����������� ������.
//      DataLen		- ����� ����������� ������.
//		out_pData	- ��������� �� �������� ����� ��� ����������� ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ������ ������ �� ��������� �����.
IL_FUNC IL_RETCODE flReadBinary(IL_CARD_HANDLE *phCard, IL_WORD Offset, IL_WORD DataLen, IL_BYTE *out_pData);

//  Description:
//      ��������� ������ ������ �� ��������� ����� � ���������� �������� �� ����� ����� � ��������� ������������ ��������� ������.
//  See Also:
//		flReadBinary
//  Arguments:
//      phCard		 - ��������� �� ��������� �����.
//      Offset		 - �������� � ������ �� ������ ����� �� ����������� ������.
//		out_pData	 - ��������� �� �������� ����� ��� ����������� ������.
//      BufLen		 - ������������ ����� ��������� ������.
//		out_pDataLen - ��������� �� ����������� ����� ��������� ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ������ ������ �� ��������� ����� c ��������� ������������.
IL_FUNC IL_RETCODE flReadBinaryEx(IL_CARD_HANDLE *phCard, IL_WORD Offset, IL_BYTE *out_pData, IL_WORD BufLen, IL_WORD *out_pDataLen);

//  Description:
//      ��������� ������ ������ � �������� ���� �� ���������� ��������.
//  See Also:
//		flReadBinary
//		flReadBinaryEx
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//      Offset		- �������� � ������ �� ������ ����� �� ������������ ������.
//      DataLen		- ����� ������������ ������.
//		in_pData	- ��������� �� ����� ������������ ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ������ ������ � �������� ����.
IL_FUNC IL_RETCODE flUpdateBinary(IL_CARD_HANDLE* phCard, IL_WORD Offset, IL_WORD DataLen, IL_BYTE *in_pData);

//  Description:
//      ��������� APDU-������� ������.
//  See Also:
//  Arguments:
//      phCard		    - ��������� �� ��������� �����.
//      SM_MODE		    - ����� �������� APDU-�������:
//						   * SM_MODE_NONE - �� ��������� ������.
//						   * SM_MODE_IF_SESSION - �� ��������� ������ ��� ������� ��� ���������.
//						   * SM_MODE_ALWAYS - �� ��������� ������.
//		inout_pApduElem - ��������� �� ������� APDU-�������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ����������� APDU-������ ������.
IL_FUNC IL_RETCODE flRunApdu(IL_CARD_HANDLE *phCrd, IL_BYTE SM_MODE, IL_APDU_PACK_ELEM *inout_pApduElem);

//  Description:
//		������������ ������ � ����� ����� ������������ �����������.<p />
//		����� ������������ ����������� ������������ ����� ������ ���������� ����� ���. 
//		�� ������ ����� ��������� ����������, ��� �������������� ��������� ��������� �������, 
//		�������� �������� ����������, ������ � ��� � ������������� ������� ����� ����������.
//  See Also:
//  Arguments:
//		phCard			- ��������� �� ��������� �����.
//      out_pPassPhrase - ��������� �� �������� ����� ��� ������ � ������ ������������ �����������. 
//						  ��������� � ����� ������ ������������� �������������� � Win\-1251. 
//						  ��� ���������� �� ����� ��������� ������������ ����������� ������������ ������ ������.
//  Return Value:
//		IL_RETCODE - ��� ������.
//  Summary:
//		��������� ����� ������������ �����������. 
IL_FUNC IL_RETCODE flGetPassPhrase(IL_CARD_HANDLE *phCrd, IL_CHAR *out_pPassPhrase);

//  Description:
//		������������ ������ �� ����� ����� ������������ �����������.<p />
//		����� ������������ ����������� ������������ ����� ������ ���������� ����� ���. �� ������ ����� ��������� ����������,
//		��� �������������� ��������� ��������� �������, �������� �������� ����������, ������ � ��� � ������������� �������
//		����� ����������.
//  See Also:
//		opCtxSetPassPhrase 
//		opCtxGetPassPhrase
//  Arguments:
//		phCard		   -  ��������� �� ��������� �����.
//		in_pPassPhrase - ��������� �� ������� ����� c� ������� ����� ������������ ����������� � ��������� Win-1251. 
//						 ������������ �� ����� ������ ������������� �������������� � ������ Iso-8859.
//  Return Value:
//		IL_RETCODE - ��� ������.
//  Summary:
//		������ �� ����� ����� ������������ �����������. 
IL_FUNC IL_RETCODE flSetPassPhrase(IL_CARD_HANDLE *phCrd, IL_CHAR *in_pPassPhrase);

//  Description:
//      ������������ ����� ������� � (���� �������) ����� ���������� ������ ����� � ������ �������� ��������� �����.
//  See Also:
//  Arguments:
//      phCard		     - ��������� �� ��������� �����.
//		sectorId		 - ������������� ����������� �������. ������� �������� �������� ������� � ��������� �������� (������� ����������).
//		blockId			 - ������������� ����������� �����. ��� ������� �������� ����� ����� �� ������������. 
//		ifForceSelection - ������� ������������� ������ ��� ����� �������� ���������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ����� ������� � ����� ������ ����� � ������ �������� ���������.
IL_FUNC IL_RETCODE flSelectContext(IL_CARD_HANDLE *phCrd, IL_WORD sectorId, IL_WORD blockId, IL_BYTE ifForceSelection);

//  Description:
//      ��������� � ����� �������� ��������� ����� ��-����������.
//  See Also:
//  Arguments:
//      phCard		 - ��������� �� ��������� �����.
//		out_pData	 - ��������� �� ����� ��� ������������ ������.
//		out_pDataLen - ��������� �� ����� ������������ ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� ��������� ����� ��-����������.
IL_FUNC IL_RETCODE flGetAppPubKey(IL_CARD_HANDLE *phCrd, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

//  Description:
//		�������� �������� � ���������.<p/>
//		��� ���� ������ 1.0 �������� � ��������� ����������� �� ������ 'TerminalInfo' ����������������� ����� 'terminal.ini'.<p/>
//		��� ���� ������ 1.0 �������� � ��������� ����������� �� ����������� ��������� ����� ���������.
//  See Also:
//  Arguments:
//		phCard - ��������� �� ��������� �����.
//		out_pData - ��������� �� ����� ��� ������������ ������.
//		out_pDataLen - ��������� �� ����� ������������ ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//		��������� �������� � ���������. 
IL_FUNC IL_RETCODE flGetTerminalInfo(IL_CARD_HANDLE *phCrd, IL_BYTE *out_pData, IL_DWORD *out_pDataLen);

//  Description:
//		��������� ����������� ������� ��������� ����� ���.
//  See Also:
//		opCtxSetDigitalSignatureBuf 
//		opApiMakeDigitalSignature
//  Arguments:
//		phCard - ��������� �� ��������� �����.
//		in_pAuthRequest - ��������� �� �������������� ����� ������ �� �������������� ��\-���������� ��� �������� ������.
//		AuthRequestLen - ����� ������� �� �������������� ��-����������.
//		out_pSign - ��������� �� ����� ��� ������������ ����������� �������.
//		inout_pSignLen - ��������� �� ������ ������ ����������� ������� � ����� ������������ ������.
//		out_pCertChain - ��������� �� ������� ������������ ����� �������� ����������� �������.
//		inout_pCertChainLen - ��������� �� ������������ ������ ������ � ����� ������������ ������� ������������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//		������������ ����������� ������� ��������� ����� ���. 
IL_FUNC IL_RETCODE flMakeDigitalSignature(IL_CARD_HANDLE *phCrd, 
										  IL_BYTE *in_pAuthRequest, IL_WORD AuthRequestLen, 
										  IL_BYTE *out_pSign, IL_WORD *inout_pSignLen,
										  IL_BYTE *out_pCertChain, IL_WORD *inout_pCertChainLen);

//  Description:
//      ������������ �������� ����������� ���������� ������.
//  See Also:
//  Arguments:
//      phCard			- ��������� �� ��������� �����.
//		in_pKeyCert		- ��������� �� ����� � ������� ������������ �����������.
//		KeyCertLen		- ����� ������������ �����������.
//		ifGost			- ��� ������������ ����������� ����/RSA.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      �������� ����������� ���������� ������.
IL_FUNC IL_RETCODE flCheckCertificateSP(IL_CARD_HANDLE *phCrd, IL_BYTE *in_pKeyCert, IL_WORD KeyCertLen, IL_BYTE ifGost);

//  Description:
//      �������� ���������� ��������� �����.
//  See Also:
//  Arguments:
//      phCard		 - ��������� �� ��������� �����.
//		ilParam		 - ������������� ����������� ����������� (��. 'HAL_Parameter.h').
//		KeyVer		 - ������ ����� ��������������� ������.
//		ifGost		 - ��� ����������� ����/RSA.
//		in_pCertBuf  - ��������� �� ����� � ������������ ��������� �����������.
//		out_pCertLen - ��������� �� ����� ������������� �����������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� ����������� ��������� �����.
IL_FUNC IL_RETCODE flGetCertificate(IL_CARD_HANDLE *phCrd, IL_WORD ilParam, IL_BYTE KeyVer, IL_BYTE ifGost, IL_BYTE *in_pCertBuf, IL_DWORD *out_pCertLen);

//  Description:
//      �������� ������������� ������ ������� ������� ���������� ������ ����� ���.<p/>
//		������������� ������ ������������ � �������� �������� ������ ��������������� ������ �������� ��������� �������� ������ 
//		����������������� ����� 'sectors.ini' ��� ������ ������ � ����� ��� ������ ������ �� �����. 
//  See Also:
//		prmGetParameterSectorEx
//  Arguments:
//      phCard	 - ��������� �� ��������� �����.
//      SectorId - ������������� �������.
//  Return Value:
//      SectorVer - ������������� ������ ������� ���������� ������� ���������� ������.
//  Summary:
//      ��������� �������������� ������ ������� ������� ������.
IL_FUNC IL_BYTE flGetSectorVersion(IL_CARD_HANDLE* phCrd, IL_BYTE SectorId);

//  Description:
//      ��������� ������ TLV-������ �� ��������� ����� ���������� ������ ����� ���.<p/>
//		������������ ��� ����������� �������� TLV-������ � �������� ���. 
//		������������ ������ ������������� �������������� ��� ����������, ���������� ��� �������� ��������� TLV-������ ��������� ����� �����. 
//  See Also:
//  Arguments:
//      phCard			- ��������� �� ��������� �����.
//      SectorId		- ������������� �������.
//		BlockId			- ������������� �����.
//		out_pData		- ��������� �� �������� ����� ��� ����������� ������. 
//		inout_pDataLen	- ��������� �� ������������ ������ ��������� ������ � ����������� ������ ��������� ������.
//  Return Value:
//      SectorVer - ������������� ������ ������� ���������� ������� ���������� ������.
//  Summary:
//      ������ TLV-������ ��������� ����� ���������� ������ �����.
IL_FUNC IL_RETCODE flAppReadBinTlvBlock(IL_CARD_HANDLE* phCrd, IL_WORD SectorId, IL_WORD BlockId, IL_BYTE *out_pData, IL_WORD *inout_pDataLen);


#endif//_FUNCLIB_EX_H_