#ifndef _CARDLIB_H_
#define _CARDLIB_H_

#include "CardLibEx.h"
#include "il_version.h"
#include "HAL_SCReader.h"
#include "HAL_Crypto.h"

#define PATCH_9F36	//+++ �������� ��� ���� '9F36' �������� ������ ��� �� 01.01.2013

// ������� 'VERIFY' - ����������� ��������� �����
#define INS_VERIFY		0x20
// ������� 'MANAGE SECURE ENVIRONMENT' - ������������ ���������� ������������ ���������� �����
#define INS_MSE 		0x22
// ������� 'CHANGE REFERENCE DATA' - ��������� (���������) �������� ����������� ������ ������
#define INS_CHANGE_DATA	0x24
// ������� 'PERFORM SECURITY OPERATION' - ���������� ����������������� ��������
#define INS_PERF_SEC_OP	0x2A
// ������� 'RESET RETRY COUNTER' - ������������� ������� � ��������� �������� ���������� ������� ��� ������������ � ������������ ��������
#define INS_RST_CNTR	0x2C
// ������� 'MUTUAL (EXTERNAL) AUTHENTICATE' - �������������� �������� �������� �� ������� ��-����������. 
#define INS_MUT_AUTH	0x82
// ������� 'GET CHALLENGE' - ��������� �� ��-���������� ����������  �����
#define INS_GET_CHAL	0x84
// ������� 'INTERNAL AUTHENTICATE' - �������������� ��-���������� �� ������� ���
#define INS_INT_AUTH	0x88
// ������� 'SELECT' - ����� ���������� ��� �����
#define INS_SELECT		0xA4
// ������� 'READ BINARY' - ������ ������ �� ���������  �����
#define INS_READ_BIN	0xB0
// ������� 'READ RECORD' - ���������� ������ �� ����� ������� �������� ���������
#define INS_READ_REC	0xB2
// ������� 'GET RESPONSE' - ��������� ������ �� �����
#define INS_GET_RESP	0xC0
// ������� 'GET DATA' - ��������� �������� ��������� ������ ������������������ ���������� �� ���� 
#define INS_GET_DATA	0xCA
// ������� 'UPDATE BINARY' - ������ ������ � �������� ����
#define INS_UPDATE_BIN	0xD6
// ������� 'UPDATE RECORD' - ���������� ����������� ������ ����� ������� ��������  ���������
#define INS_UPDATE_REC	0xDC
// ������� 'PUT DATA' - ��������� �������� ��������� ������ ��-���������� �� ����
#define INS_PUT_DATA	0xDA
// ������� 'APPEND RECORD' - ���������� ������ � ����� ����� ������� �������� ��������� 
#define INS_APPEND_REC	0xE2

// ����� �������� APDU-������� �� ��������� ������
#define SM_MODE_NONE        0
// ����� �������� APDU-������� �� ��������� ������ ��� ������� ��� ���������
#define SM_MODE_IF_SESSION  1
// ����� �������� APDU-������� �� ��������� ������
#define SM_MODE_ALWAYS      2

//  Description:
//      ��������� APDU-������� ����� ���.
//  See Also:
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//      in_pilApdu	- ��������� �� ��������� APDU-�������.
//      SM_MODE		- ����� �������� APDU-�������:
//						- SM_MODE_NONE - �� ��������� ������.
//						- SM_MODE_IF_SESSION - �� ��������� ������ ��� ������� ��� ���������.
//						- SM_MODE_ALWAYS - �� ��������� ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� APDU-�������.
IL_FUNC IL_RETCODE clAPDU(IL_CARD_HANDLE *phCard, IL_APDU *in_pilApdu, IL_BYTE SM_MODE);

/* Description
   ��������� APDU-������� 'PERFORM SECURITY OPERATION'
   ����������������� ��������.<p />
   ������������ ��� ����������� ������� ������������ ���
   �������� ��������� ����� � ����������� ������� ���������
   �����.
   See Also
   Parameters
   phCard :      ��������� �� ��������� �����.
   CLA :         ����� �������\:
                 * '00' � ��� �������� ������� �� ���������
                   ������.
                 * '0C' � ��� �������� ������� �� �����������
                   ������ (Secure messaging). 
   in_pDataIn :  ������� ������. � ����������� �� ���������
                 ���������� ��� ���������� ������������ ���������
                 �����, ��� ���\-�������� ��������� ���������.
   DataLen :     ����� ������� ������.
   Returns
   IL_RETCODE - ��� ������.
   Summary
   ���������� ������� ����������������� ��������.                 */
IL_FUNC IL_RETCODE clAppPerformSecOperation(IL_CARD_HANDLE *phCrd, IL_BYTE CLA, IL_BYTE *in_pDataIn, IL_WORD DataLen);

//  Description:
//      ��������� APDU-������� 'READ BINARY' ��� ������ ������ �� ��������� ����� �� ���������� ��������.
//  See Also:
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//      Offset		- �������� � ������ �� ������ ����� �� ����������� ������.
//      DataLen		- ����� ����������� ������.
//		out_pData	- ��������� �� ����� ��� ����������� ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������� ������ ������ �� ��������� �����.
IL_FUNC IL_RETCODE clAppReadBinary(IL_CARD_HANDLE *phCrd, IL_WORD Offset, IL_WORD Length, IL_BYTE *out_pData);

//  Description:
//      ��������� APDU-������� 'GET CHALLENGE' ��� ��������� �� ��-���������� ���������� �����.
//  See Also:
//  Arguments:
//      phCard			- ��������� �� ��������� �����.
//      Length			- ��������� ����� ���������� �����.
//		out_pChallenge	- ��������� �� ����� ��� ������������� ���������� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������� ��������� ���������� ����� ��-����������.
IL_FUNC IL_RETCODE clAppGetChallenge(IL_CARD_HANDLE *phCrd, IL_WORD Length, IL_BYTE *out_pChallenge);

/* Description
   ��������� APDU-������� 'MUTUAL (EXTERNAL) AUTHENTICATE' ���
   �������������� �������� �������� �� ������� ��-����������.
   See Also
   Parameters
   phCard :        ��������� �� ��������� �����.
   CLA :           ����� �������. '00' ��� �������� ������� ��
                   ��������� ������.
   P2 :            ������������� ������������� �����.
   in_pDataIn :    ������� ������ ��� ��������������.
   DataInLen :     ����� ������� ������.
   out_pData :     ��������� �� ����� ��� ������������ ������
                   ������������ ��� ����������� ������ �������.
   out_pDataLen :  ��������� �� ���������� ��� ����� ������������
                   ������.
   Returns
   IL_RETCODE - ��� ������.
   Summary
   ���������� ������� �������������� ��-����������� ��������
   ��������.                                                      */
IL_FUNC IL_RETCODE clAppMutualAuth(IL_CARD_HANDLE *phCrd, IL_BYTE CLA, IL_BYTE P2, IL_BYTE *in_pDataIn, IL_WORD DataInLen, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

//  Description:
//      ��������� APDU-������� 'INTERNAL AUTHENTICATE' ��� �������������� ��-���������� �� ������� ���.
//  See Also:
//  Arguments:
//      phCard		 - ��������� �� ��������� �����.
//		P2			 - ������������� ������������� �����. 
//      in_pDataIn	 - ������� ������ ��� ��������������. 
//		DataInLen	 - ����� ������� ������.
//		out_pData	 - ��������� �� ����� ��� ������������ ������ ������������, �������������� ������������� ��-���������� � ���������� ����������� ������.
//		out_pDataLen - ��������� �� ���������� ��� ����� ������������ ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������� �������������� ��-���������� �� ������� ���.
IL_FUNC IL_RETCODE clAppInternalAuth(IL_CARD_HANDLE *phCrd, IL_BYTE P2, IL_BYTE *in_pDataIn, IL_WORD DataInLen, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

/* Description
   ��������� APDU-������� 'VERIFY' ��� ����������� ���������
   �����.
   See Also
   Parameters
   phCard :              ��������� �� ��������� �����.
   P2 :                  ������������� ������������� ������.
   in_pData8 :           ��������� �� ���\-���� �� ���������
                         ������ � ������� ISO 9564\-3\:2002
                         (Format 2).
   out_pTriesRemained :  ��������� �� ���������� ���
                         ������������� �������� ����������
                         ���������� ����������� ������.
   Returns
   IL_RETCODE - ��� ������.
   Summary
   ���������� ������� ����������� ��������� �����.           */
IL_FUNC IL_RETCODE clAppVerify(IL_CARD_HANDLE *phCrd, IL_BYTE P2, IL_BYTE *in_pData8, IL_BYTE *out_pTriesRemained);

//  Description:
//      ��������� APDU-������� 'CHANGE REFERENCE DATA' ��� ��������� ��� ��������� �������� ����������� ������ ������.
//  See Also:
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//		P2			- ������������� ���������� ����������� ������. 
//      in_pData	- ��������� �� ����� � ����� ��������� ����������� ������. 
//		DataLen		- ����� ��������������� ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������� ��������� ��� ��������� ������.
IL_FUNC IL_RETCODE clAppChangeRefData(IL_CARD_HANDLE *phCrd, IL_BYTE P2, IL_BYTE *in_pData, IL_WORD DataLen);

//  Description:
//      ��������� APDU-������� 'RESET RETRY COUNTER' ��� ������������� ������� � ��������� �������� ���������� ������� ��� ������������ � ������������ ��������.
//  See Also:
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//		P2			- ������������� ���������� ����������� ������. 
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������� ������������� ������ � ��������� �������� ���������� ������� ��� ������������.
IL_FUNC IL_RETCODE clAppResetRetryCounter(IL_CARD_HANDLE *phCrd, IL_BYTE P2);

//  Description:
//      ��������� APDU-������� 'PERFORM SECURITY OPERATION' ����������������� �������� ��� �������� ����������� ������� ��������� �����.
//  See Also:
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//      in_pData	- ��������� �� ����� � ���-��������� ��������� ���������. 
//		DataLen		- ����� ��������� ���������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������� �������� ����������� ������� ��������� �����.
IL_FUNC IL_RETCODE clAppComputeDigitalSignature(IL_CARD_HANDLE *phCard, IL_BYTE *in_pData, IL_WORD DataLen);

IL_FUNC IL_RETCODE clAppReadBinaryEx(IL_CARD_HANDLE* phCard, IL_WORD wOffset, IL_WORD wBufLength, IL_BYTE* pOut, IL_WORD* pwOutLen);


#endif