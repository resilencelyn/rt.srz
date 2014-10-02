#ifndef _TLV_H_
#define _TLV_H_

#include "il_types.h"

//  Description:
//      ������������ ��������� ����� TLV-�������� � ������� TLV-������ �� ��� ������� ����.
//  See Also:
//		TagFind
//  Arguments:
//      in_pData	  - ��������� �� ����� TLV-������, � ������� �������������� ����� TLV-��������. 
//		MaxLen		  - �����, ������������ ������������ �������� ������.
//		in_pTagPath	  - ��������� �� ������ ��������� ��������������� �����, ������������ ������ ���� �� �������� TLV-��������.
//		NumTags		  - ������ ������� ��������� ��������������� �����.
//		out_pDataLen  - ��������� �� ����������, ���������������� ������ ���������� TLV-�������� � ����������� �� ����� 'ifWithTag'.
//		out_ppTagData - ���������������� ��������� �� ��������� TLV-������� ��� NULL ��� ������������� ���������� ������.
//		ifWithTag	  - ������� ������������� ��������� �� ��������� ���������� TLV-�������� ��� ��� ��������.   	
//  Return Value:
//      IL_RETCODE - ��� ������.
//  Summary:
//      ��������� ����� TLV-�������� � ������� TLV-������ �� ��� ������� ����.
IL_FUNC IL_RETCODE TagFindByPath(IL_BYTE *in_pData, IL_DWORD MaxLen, const IL_TAG *in_pTagPath, IL_DWORD NumTags, IL_DWORD *out_pDataLen, IL_BYTE **out_ppTagData, IL_BYTE ifWithTag);

//  Description:
//      ��������� TLV-������� � ������� TLV-������.
//  See Also:
//  Arguments:
//      TagId	  - ������������� ���� ������������ TLV-��������. 
//		in_pData  - ��������� �� ����� � ������� �������� TLV-��������.
//		DataLen	  - ����� ������ �������� TLV-��������.
//		out_pData - ��������� �� ����� � �������� ����������� TLV-�������.
//  Return Value:
//      ����� ������������ TLV-��������.
//  Summary:
//      ���������� TLV-�������� � ������� TLV-������. 
IL_FUNC IL_DWORD AddTag(const IL_TAG TagId, IL_BYTE *in_pData, IL_DWORD DataLen, IL_BYTE *out_pData);

//  Description:
//      ������������ ����� TLV-�������� � ������� TLV-������ �� ��� ��������������.
//  See Also:
//		TagFindByPath
//  Arguments:
//      in_pData	  - ��������� �� ����� TLV-������, � ������� �������������� ����� TLV-��������. 
//		MaxLen		  - �����, ������������ ������������ �������� ������.
//		TagId		  - ������������� ���� �������� TLV-��������.
//		out_pDataLen  - ��������� �� ����������, ���������������� ������ ���������� TLV-�������� � ����������� �� ����� 'ifWithTag'.
//		out_ppTagData - ���������������� ��������� �� ��������� TLV-������� ��� NULL ��� ������������� ���������� ������.
//		ifWithTag	  - ������� ������������� ��������� �� ��������� ���������� TLV-�������� ��� ��� ��������.   	
//  Return Value:
//      IL_RETCODE - ��� ������.
//  Summary:
//      ����� TLV-�������� � ������� TLV-������ �� ��� ��������������.
IL_FUNC IL_RETCODE TagFind(IL_BYTE *in_pData, IL_DWORD MaxLen, const IL_TAG TagId, IL_DWORD *out_pDataLen, IL_BYTE **out_ppTagData, IL_BYTE ifWithTag);

//  Description:
//      ���������� ������ ���� �������������� TLV-��������.
//  See Also:
//		GetLenLen
//		GetDataLen
//  Arguments:
//		in_pTlvElem	- ��������� �� TLV-�������.
//  Return Value:
//      ������ ���� �������������� TLV-�������� � ������.
//  Summary:
//      ��������� ������� ���� �������������� TLV-��������.
IL_FUNC IL_DWORD GetTagLen(IL_BYTE *in_pTlvElem);

//  Description:
//      ���������� ������ ���� ����� �������� TLV-��������.
//  See Also:
//		GetTagLen
//		GetDataLen
//  Arguments:
//		in_pTlvElem	- ��������� �� TLV-�������.
//  Return Value:
//      ������ ���� ����� �������� TLV-��������.
//  Summary:
//      ��������� ������� ���� ����� �������� TLV-��������.
IL_FUNC IL_DWORD GetLenLen(IL_BYTE *in_pTlvElem);

//  Description:
//      ���������� ������ ���� ������ TLV-��������.
//  See Also:
//		GetTagLen
//		GetLenLen
//  Arguments:
//		in_pTlvElem	- ��������� �� TLV-�������.
//  Return Value:
//      ������ ���� ������ TLV-�������� � ������.
//  Summary:
//      ��������� ������� ���� ������ TLV-��������.
IL_FUNC IL_DWORD GetDataLen(IL_BYTE *in_pTlvElem);

//+++
IL_FUNC IL_RETCODE GetTagOffsetByPath(IL_BYTE *in_pDdata, IL_DWORD MaxLen, const IL_TAG *pTagPath, IL_DWORD NumTags, IL_WORD *out_Offset, IL_DWORD *out_pdwLen, IL_BYTE ifWithTag);



IL_FUNC IL_CHAR* DataToHexStr(IL_CHAR* str, IL_BYTE* data, IL_WORD data_len);
IL_FUNC IL_DWORD GetTag(IL_BYTE* data);
IL_FUNC IL_BYTE* TagParse(IL_BYTE* data, IL_BYTE level, IL_DWORD maxlen);

#endif