#ifndef _CARDLIB_EX_H_
#define _CARDLIB_EX_H_

#include "il_types.h"

// ��������� ��������/������ ������
typedef struct
{
    IL_BYTE id;   // ������������� �������/�����
    IL_BYTE fid[2];  // ������������� ������� � ��������/�����
    IL_BYTE version; // ������ �����
} IL_RECORD_DEF;

// ������ ��������/������ ������
#define IL_MAX_RECORDS 32  // ������������ ���������� ������� � ���������

typedef struct
{
    IL_RECORD_DEF rec[IL_MAX_RECORDS];  // ������ ���������� ��������/������ ������ 
    IL_WORD num_records;     // ���������� ������� � �������
} IL_RECORD_LIST;

typedef struct 
{
	IL_HANDLE_READER hRdr;		// ���������� �����-���� ������
	IL_HANDLE_CRYPTO hCrypto;	// ���������� ������������
	IL_BYTE AppVer;				// ������ �����
	IL_BYTE KeyVer;				// ������ ��������� ����� �� ���� ���
	IL_BYTE AUC[2];				// �������� � ���������� ��-����������
	IL_BYTE AppStatus;			// ������ ��-����������
	IL_BYTE ifLongAPDU;			// ������� ��������� "�������" ������
	IL_BYTE ifGostCrypto;		// ������� ������������� ��������������� ���� 
	IL_BYTE ifSM;				// ������� ��������� ���������� ������  
	IL_BYTE ifNeedMSE;			// ������� ������������� ����������� ������ MSE
	IL_BYTE ifSign;             // ������� ������� ��������� ����������� ������� ��������� �����
	IL_WORD currDF;             // ��������������� DF
	IL_WORD currEF;             // ��������������� EF
    IL_RECORD_LIST sectors;     // ������ ��������
    IL_RECORD_LIST blocks;      // ������ ������
	IL_BYTE CIN[10];			// ������� ����� ����� �� �������� ������ ������������ 
} IL_CARD_HANDLE;

//  Description:
//      ��������� ������ � ������ ���.
//  See Also:
//		clCardClose
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      �������� ������ � ������ ���.
IL_FUNC IL_RETCODE clCardOpen(IL_CARD_HANDLE* phCrd);

//  Description:
//      ��������� ������ � ������ ���.
//  See Also:
//		clCardOpen
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      �������� ������ � ������ ���.
IL_FUNC IL_RETCODE clCardClose(IL_CARD_HANDLE* phCrd);

//  Description:
//      ��������� APDU-������� 'SELECT' ��� ��� ������ ����������, ����� �������� ��� ������������� �����.
//  See Also:
//  Arguments:
//      phCard		 - ��������� �� ��������� �����.
//		P1			 - ��� ����������� �������:
//						- '00' � ����� �����-�������� (DF), ����� ������ (EF) ��� �������� ����� (ADF). 
//						- '04' � ����� ���������� �� �������������� (�� AID ����������).
//		P2			 - ��� ������������ ������:
//						- '00' � ������� ������ ������ ����������. 
//						- '04' � ������� File Control Parameters (FCP), �������� ������ ��� EF. 
//						- '08' � ������� File Management Data (FMD), �������� ������ ��� ADF � DF. 
//						- '0C' � �� ���������� ���������� � ��������� �������. 
//		in_pId		 - ��������� �� ������������� ����������� �������.
//		IdLen		 - ����� ��������������.
//      out_pData	 - ��������� �� ����� ��� ������������ ������.
//		out_pDataLen - ��������� �� ����������, ���������������� ������ ������������ ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������� ������ ���������� ��� �����.
IL_FUNC IL_RETCODE clAppSelect(IL_CARD_HANDLE* phCrd, IL_BYTE P1, IL_BYTE P2, IL_BYTE  *in_pId, IL_BYTE IdLen, IL_BYTE *out_pData, IL_WORD * out_pDataLen);

//  Description:
//      ��������� APDU-������� 'UPDATE BINARY' ��� ������ ������ � �������� ���� �� ���������� ��������.
//  See Also:
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//      Offset		- �������� � ������ �� ������ ����� �� ������������ ������.
//		in_pData	- ��������� �� ����� � ������������� �������.
//      DataLen		- ����� ������������ ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������� ������ ������ � ��������  ����.
IL_FUNC IL_RETCODE clAppUpdateBinary(IL_CARD_HANDLE *phCrd, IL_WORD Offset, IL_BYTE *in_pData, IL_WORD DataLen);

//  Description:
//      ��������� APDU-������� 'GET DATA' ��� ��������� �������� �������� ������ �� TLV-����� �� ���������� ����.
//  See Also:
//		clAppPutData
//  Arguments:
//      phCard		 - ��������� �� ��������� �����.
//      Tag			 - ��� �������� ������.
//		out_pData	 - ��������� �� ����� ��� ����������� ������.
//      out_pDataLen - ��������� �� ���������� ��� ����� ��������� ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������� ��������� �������� �������� ������ �� TLV-�����.
IL_FUNC IL_RETCODE clAppGetData(IL_CARD_HANDLE *phCrd, IL_WORD Tag, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

/* Description
   ��������� APDU-������� 'PUT DATA' ��� ��������� ��������
   �������� ������ �� TLV-����� �� ���������� ����.
   See Also
   clAppGetData
   Parameters
   phCard :    ��������� �� ��������� �����.
   Tag :       ��� �������� ������.
   in_pData :  ��������� �� ����� ��� ��������������� ������.
   DataLen :   ����� ��������������� ������.
   Returns
   IL_RETCODE - ��� ������.
   Summary
   ���������� ������� ��������� �������� �������� ������ �
   TLV-����.                                                  */
IL_FUNC IL_RETCODE clAppPutData(IL_CARD_HANDLE *phCrd, IL_WORD Tag, IL_BYTE *in_pData, IL_WORD DataLen);

/* Description
   ��������� APDU-������� 'MANAGE SECURE ENVIRONMENT (MSE)' ���
   ������ ������������ ���������� ������������ ���������� �����.
   � �������� ������� ������ ������������ ���� ������ ������ 3
   �����.
   See Also
   clAppGetData
   Parameters
   phCard :  ��������� �� ��������� �����.
   P1 :      'F3' (RESTORE).
   P2 :      �������� ������������.
             * '01' � �������� ������������ �1 (����������
               ���������������)
             * '02' � �������� ������������ �2 (����������
               ���������������) 
   P3 :      1\-� ���� ������� ������.
   P4 :      2\-� ���� ������� ������.
   P5 :      3\-� ���� ������� ������.
   Returns
   IL_RETCODE - ��� ������.
   Summary
   ���������� ������� ������������ ���������� ������������
   ���������� �����.                                             */
IL_FUNC IL_RETCODE clMSE(IL_CARD_HANDLE* phCard, IL_BYTE P1, IL_BYTE P2, IL_BYTE P3, IL_BYTE P4, IL_BYTE P5);

//  Description:
//      ��������� APDU-������� 'READ RECORD' ��� ��� ���������� ������ �� RECORD-����� ������� �������� ���������.
//  See Also:
//		clAppUpdateRecord
//		clAppAppendRecord
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//      RecNumber	- �������� � ������ �� ������ ����� �� ����������� ������.
//		ExpLen		- ��������� ����� ����������� ������.
//		out_pData	- ��������� �� ����� ��� ����������� ������.
//      out_DataLen	- ��������� �� ����������, ���������������� ������ ��������� ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������� ���������� ������ �� RECORD-�����.
IL_FUNC IL_RETCODE clAppReadRecord(IL_CARD_HANDLE *phCard, IL_BYTE RecNumber, IL_BYTE ExpLen, IL_BYTE *out_pData, IL_WORD *out_DataLen);

//  Description:
//      ��������� APDU-������� 'UPDATE RECORD' ��� ���������� ����������� ������ � RECORD-����� ������� �������� ���������.
//  See Also:
//		clAppReadRecord
//		clAppAppendRecord
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//      RecNumber	- �������� � ������ �� ������ ����� �� ����������� ������.
//		in_pData	- ��������� �� ����� � ������������ �������.
//      DataLen		- ����� ����������� ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������� ���������� ������ � RECORD-�����.
IL_FUNC IL_RETCODE clAppUpdateRecord(IL_CARD_HANDLE *phCard, IL_BYTE RecNumber, IL_BYTE *in_pData, IL_WORD DataLen);

//  Description:
//      ��������� APDU-������� 'APPEND RECORD' ��� ���������� ������ � ����� RECORD-����� ������� �������� ���������.
//  See Also:
//		clAppReadRecord
//		clAppUpdateRecord
//  Arguments:
//      phCard		- ��������� �� ��������� �����.
//		in_pData	- ��������� �� ����� � ������������ �������.
//      DataLen		- ����� ����������� ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������� ���������� ������ � RECORD-����.
IL_FUNC IL_RETCODE clAppAppendRecord(IL_CARD_HANDLE *phCard, IL_BYTE *in_pData, IL_WORD DataLen);

#endif//_CARDLIB_EX_H_