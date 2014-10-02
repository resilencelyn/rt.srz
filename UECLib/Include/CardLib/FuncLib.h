#ifndef _FUNCLIB_H_
#define _FUNCLIB_H_

#include "FuncLibEx.h"
#include "il_version.h" 

#define PASS_PHRASE_MAX_LEN		40

//flFileSelect - �������� DF ��� EF � ����������� � ��������� ��������� ��������������� DF � EF
IL_FUNC IL_RETCODE flFileSelect(IL_CARD_HANDLE* phCrd, IL_BYTE ifDF, IL_BYTE* pId, IL_BYTE IdLen, IL_BYTE* pOut, IL_WORD* pOutLength);

//flAppTerminalAuth - �������������� ���������
IL_FUNC IL_RETCODE flAppTerminalAuthEx(IL_CARD_HANDLE* phCrd, IL_BYTE* pOpCert, IL_WORD wOpCertSize, IL_BYTE* pTermCert, IL_WORD wTermCertSize);

//flAppGetStatus - ��������� ��������� ����������
IL_FUNC IL_RETCODE flAppGetStatus(IL_CARD_HANDLE *phCrd, IL_BYTE *pStatusOut);

//  Description:
//		��������� �� �������� ������ ������������ ������� ����� ����� CIN.
//	See Also:
//  Arguments:
//		phCard		 - ��������� �� ��������� �����.
//		out_pCIN	 - ��������� �� ����� ��� ������������ ������ ������ 10 ����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//		��������� �������� ������ ����� CIN. 
IL_FUNC IL_RETCODE flGetCIN(IL_CARD_HANDLE* phCrd, IL_BYTE *out_pCIN);

//flCheckCertificate - �������� ����������� ��������� ����� ������
IL_FUNC IL_RETCODE flCheckCertificate(IL_CARD_HANDLE* phCrd, IL_BYTE* pKeyCertIn, IL_WORD wKeyCertInLen, IL_BYTE CertificateTypeToCheck);

//  Description:
//		��������� � ����� �������� ��������� ����� ����������� ������� ��������� ����� ��-����������.
//	See Also:
//  Arguments:
//		phCard		 - ��������� �� ��������� �����.
//		out_pData	 - ��������� �� ����� ��� ������������ ������.
//		out_pDataLen - ��������� �� ����� ������������ ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//		��������� ��������� ����� ����������� ������� ��������� ����� ��-����������. 
IL_FUNC IL_RETCODE flGetAuthPubKey(IL_CARD_HANDLE *phCrd, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

//  Description:
//		��������� � ����� �������� ��������� ����� �������� ��-����������.
//	See Also:
//  Arguments:
//		phCard		 - ��������� �� ��������� �����.
//		out_pData	 - ��������� �� ����� ��� ������������ ������.
//		out_pDataLen - ��������� �� ����� ������������ ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//		��������� ��������� ����� �������� ��-����������. 
IL_FUNC IL_RETCODE flGetIssPubKey(IL_CARD_HANDLE *phCrd, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

#endif