#ifndef _CONVERT_H_
#define _CONVERT_H_

#include "il_types.h"

//  Description:
//      ������������ ������ ������ HEX-������� � �������� ������.<p/>
//		������������ ��������� ������ �� ��������������. 
//  See Also:
//      bin2hex
//  Arguments:
//      in_strHex	- ��������� �� ������ ������ � HEX-�������.
//      out_pBin	- ��������� �� ����� ��� �������� �������� ������.
//      out_pBinLen	- ����� �������� ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ����������� Hex-������ � �������� ������.
IL_FUNC IL_RETCODE hex2bin(IL_CHAR *in_strHex, IL_BYTE *out_pBin, IL_DWORD *out_pBinLen);

//  Description:
//      ������������ ������ �������� ������ � Hex-������.<p/>
//		������������ ������ �������� ������ �� ��������������. 
//  See Also:
//      bin2hex
//  Arguments:
//      out_strHex	- ��������� �� �������� ������ ������ � HEX-�������.
//      in_pBin		- ��������� �� ������� ����� �������� ������.
//      out_pBinLen	- ����� ������� ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ����������� ������� �������� ������ � Hex-������.
IL_FUNC IL_CHAR* bin2hex(IL_CHAR *out_strHex, IL_BYTE *in_pBin, IL_DWORD in_BinLen);

//  Description:
//      ������������ ������ �������� �� ������� �������� ���������� ���������� ����� ��� ISO-8859 � ������ Win-1251.
//  See Also:
//      Ansi_2_Iso8859
//  Arguments:
//      in_str8859	- ��������� �� ������� ������ � ��������� ISO-8859.
//      out_str1251	- ��������� �� �������� ������ � ��������� Win-1251.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ����������� ������ ������� ISO-8859 � ������ Win-1251.
IL_FUNC IL_RETCODE Iso8859_2_Ansi(IL_CHAR *in_str8859, IL_CHAR *out_str1251);

//  Description:
//      ������������ ������ �������� Win-1251 � ������ ������� �������� ���������� ���������� �� ����� ��� ISO-8859.
//  See Also:
//      Iso8859_2_Ansi
//  Arguments:
//      in_str1251	- ��������� �� ������� ������ � ��������� Win-1251.
//      out_str8859	- ��������� �� �������� ������ � ��������� ISO-8859.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ����������� ������ Win-1251 � ������ ������� ISO-8859.
IL_FUNC IL_RETCODE Ansi_2_Iso8859(IL_CHAR *in_str1251, IL_CHAR *out_str8859);

//  Description:
//      ������������ �������� ������ ������ BCD-������� � ������ Win-1251.<p/>
//		������������ ������ �������� ������ �� ��������������. 
//  See Also:
//		str2bcdF
//		str2bcd0		
//      toBcd
//  Arguments:
//      in_pBcd		- ��������� �� ������� ������ ������ � BCD-�������.
//      in_BcdLen	- ����� �������� ������� ������.
//      out_Str		- ��������� �� �������� ������ Win-1251.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ����������� ������� BCD-������� � ������.
IL_FUNC void bcd2str(IL_BYTE *in_pBcd, IL_WORD in_BcdLen, IL_CHAR *out_Str);

//  Description:
//      ������������ ������ Win-1251 � �������� ������ BCD-������� � ��������� ������ ������������ 'F' �� ����������� �����.<p/>
//		������������ ������ ��������� ������� �� ��������������. 
//  See Also:
//		bcd2str
//		str2bcd0
//      toBcd
//  Arguments:
//      in_Str		- ��������� �� ������� ������ Win-1251.
//      out_pBcd	- ��������� �� �������� ������ ������ � BCD-�������.
//      maxLen		- ����������� ����� ������������ ��������� �������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ����������� ������ � ������ BCD-������� � ����������� ������ ������������ 'F'.
IL_FUNC void str2bcdF(IL_CHAR *in_Str, IL_BYTE *out_pBcd, IL_WORD maxLen);

//  Description:
//      ������������ ������ Win-1251 � �������� ������ BCD-������� � ��������� ����� ������������ '0' �� ����������� �����.<p/>
//		������������ ������ ��������� ������� �� ��������������. 
//  See Also:
//		bcd2str
//		str2bcdF
//      toBcd
//  Arguments:
//      in_Str		- ��������� �� ������� ������ Win-1251.
//      out_pBcd	- ��������� �� �������� ������ ������ � BCD-�������.
//      maxLen		- ����������� ����� ������������ ��������� �������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ����������� ������ � ������ BCD-������� � ����������� ����� ������������ '0'.
IL_FUNC void str2bcd0(IL_CHAR *str, IL_BYTE *bcd, IL_WORD max_len);

//  Description:
//      ������������ ���� � BCD-������.
//  See Also:
//      bcd2str
//		str2bcdF
//		str2bcd0		
//  Arguments:
//      b	- ��������� �� �������������� ����.
//  Return Value:
//      IL_BYTE	- BCD-�������.
//  Summary:
//      ����������� ������� BCD-������� � ������.
IL_FUNC IL_BYTE toBcd(IL_BYTE b);

//  Description:
//      ������������ ����� ������� �������� ���� ��������� ����� ��� ISO-5218 � ������ Win-1251.
//  See Also:
//      Ansi_2_Iso5218
//  Arguments:
//      in_p5218	- ��������� �� ���� � ��������� ISO-5218.
//      out_p1251	- ��������� �� �������� ������ � ��������� Win-1251.
//  Return Value:
//      �����������.
//  Summary:
//      ����������� ���� ������� ISO-5218 � ������ Win-1251.
IL_FUNC void Iso5218_2_Ansi(IL_BYTE *in_p5218, IL_CHAR *out_p1251);

//  Description:
//      ������������ ������ Win-1251 � ���� ������� �������� ���� ��������� ����� ��� ISO-5218.
//  See Also:
//      Iso5218_2_Ansi
//  Arguments:
//      in_p1251	- ��������� �� ������� ������ � ��������� Win-1251.
//      out_p5218	- ��������� �� �������� ���� � ��������� ISO-5218.
//  Return Value:
//      �����������.
//  Summary:
//      ����������� ������� Win-1251 � ���� ������� ISO-5218.
IL_FUNC void Ansi_2_Iso5218(IL_CHAR *in_p1251, IL_BYTE *out_p5218);

//  Description:
//      ������������ ������ ������ Win-1251 � ���-���� ISO 9564-3:2002 (Format 2).
//  See Also:
//  Arguments:
//      in_Pin1251		- ��������� �� ������� ������ ������ Win-1251.
//      out_pPin9564	- ��������� �� �������� ���-���� � ������� ISO 9564-3:2002 (Format 2).
//  Return Value:
//      �����������.
//  Summary:
//      ����������� ������ ������ Win-1251 � ���-���� ISO 9564.
IL_FUNC void str2pin(IL_CHAR *in_Pin1251, IL_BYTE *out_pPin9564);

//  Description:
//      ������������ 4 ����� ��������� ������� � DWORD-�������� ������� INTEL.
//  See Also:
//		dw2bin
//  Arguments:
//      in_pBin	- ��������� �� �������� ������.
//      out_pDw	- ��������� �� ���������������� DWORD-��������� ����������.
//  Return Value:
//      ��������� �� ��������� �� ����������������� ������� ���� �������.
//  Summary:
//      ����������� �������� ������ � DWORD-��������.
IL_FUNC IL_BYTE* bin2dw(IL_BYTE *in_pBin, IL_DWORD *out_pDw);

//  Description:
//      ������������ DWORD-�������� � 4 ����� ��������� ������� � ������� INTEL.
//  See Also:
//		bin2dw
//  Arguments:
//      in_Dw		- DWORD-����������.
//      out_pBin	- ��������� �� �������� ������.
//  Return Value:
//      ��������� �� ��������� �� ����������������� ������� ���� �������.
//  Summary:
//      ����������� �������� ������ � DWORD-��������.
IL_FUNC IL_BYTE* dw2bin(IL_DWORD in_Dw, IL_BYTE *out_pBin);

//  Description:
//      �������� ������� ���������� ���� � �������� �������.
//		� ���������� �������������� ������ ���� ������� �������������� �� ����� ����������, ��������� �� ����� ������� � �.�.
//  See Also:
//  Arguments:
//      in_pBin		- ��������� �� �������� ������.
//      out_pBin	- ����� �������������� ������.
//  Return Value:
//      �����������.
//  Summary:
//      ��������� ������� ���������� ���� ��������� �������.
IL_FUNC void swap(IL_BYTE *in_pBin, IL_DWORD DataLen);

//IL_FUNC IL_BYTE* bin2w(IL_BYTE *bin, IL_WORD *w);
//IL_FUNC IL_BYTE* w2bin(IL_WORD w, IL_BYTE *bin);

#endif//_CONVERT_H_