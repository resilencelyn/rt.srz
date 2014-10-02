#ifndef _HAL_COMMON_H_
#define _HAL_COMMON_H_

#include "il_types.h"
#include "Convert.h"
#include <stdlib.h> //+++

// ����������� �������� 'x' � ������ ���� ASCII 
#define DIG2ASC(x)  ( ((x)<10) ? (48+(x)) : (55+(x)) )
// ����������� 'x' ������ ���� ASCII � �������� �����  
#define ASC2DIG(x)  ( ((x)<65) ? ((x)-48) : ((x)-55) )
// ���������� �� ����, ���� 'x' ��� ����������������� �����, � ���� � ��������� ������
#define ISXDIGIT(x) ( ( ((x)>=0x30) && ((x)<=0x39) ) || ( ((x)>=0x41) && ((x)<=0x46) ) || ( ((x)>=0x61) && ((x)<=0x7A) ) )
// ����������� ��� ��������� ����� � �������� ��������, ��������� ���� �� ����������
#define TOUPPER(x)  ( ( ((x)>=0x61) && ((x)<=0x7A) ) ? ((x)-0x20) : (x) )

//  Description:
//      ���������� ������ n-�������� ��������, ���������� ����������� Buf1 � Buf2.
//  See Also:
//  Arguments:
//      Buf1 - ��������� �� ������ ������������ ������. 
//		Buf2 - ��������� �� ������ ������������ ������.
//		n	 - ���������� ������������ ����.
//  Return Value:
//      ��������� ���������:<p/>
//		  <0 - Buf1 ������ Buf2.<p/>
//		  0  - Buf1 ����� Buf2.<p/>
//		  >0 - Buf1 ������ Buf2. 
//  Summary:
//      ��������� �������� ������.
IL_FUNC	IL_INT		cmnMemCmp (const IL_BYTE *Buf1, const IL_BYTE *Buf2, IL_WORD n);

//  Description:
//      �������� ����� ������ Src � ������ Dest.
//  See Also:
//  Arguments:
//      Dest - ��������� �� �������������� ������. 
//		Src  - ��������� �� ���������� ������.
//  Return Value:
//      ��������� �� �������������� ������.
//  Summary:
//      ����������� ����� ������ � ������.
IL_FUNC	IL_CHAR*	cmnStrCopy (IL_CHAR *Dest, const IL_CHAR *Src);

//  Description:
//      ���������� ������ Str1 � Str2.
//  See Also:
//  Arguments:
//      Str1 - ��������� �� ������ ������������ ������. 
//		Str2 - ��������� �� ������ ������������ ������.
//  Return Value:
//      ��������� ���������:<p/>
//		  <0 - Str1 ������ Str2.<p/>
//		  0  - Str1 ����� Str2.<p/>
//		  >0 - Str1 ������ Str2. 
//  Summary:
//      ��������� �����.
IL_FUNC	IL_INT		cmnStrCmp (const IL_CHAR *Str1, const IL_CHAR *Str2);

//  Description:
//      ������������ ������ Src � ������ Dest.
//  See Also:
//  Arguments:
//      Dest - ��������� �� �������������� ������. 
//		Src  - ��������� �� ������������� ������.
//  Return Value:
//      ��������� �� �������������� ������.
//  Summary:
//      ������������ �����.
IL_FUNC	IL_CHAR*	cmnStrCat (IL_CHAR *Dest, const IL_CHAR *Src); 

//  Description:
//      ��������� ����� ������ Src.
//  See Also:
//  Arguments:
//		Src  - ��������� �� ������.
//  Return Value:
//      ����� ������.
//  Summary:
//      ����������� ����� ������.
IL_FUNC	IL_WORD		cmnStrLen (const IL_CHAR *Src);

//  Description:
//      �������������� ������ n-���� ������� Dest ��������� ���������.
//  See Also:
//  Arguments:
//      Dest - ��������� �� ���������������� ������. 
//		c	 - ���������������� ��������.
//		n	 - ���������� ���������������� ����.
//  Return Value:
//      ��������� �� ���������������� ������.
//  Summary:
//      ������������� ������� ������.
IL_FUNC	IL_BYTE*	cmnMemSet (IL_BYTE *Dest, IL_INT c, IL_WORD n);

//  Description:
//      �������� ������ n-���� ������� Src � ������ Dest.<p/>
//		���� ��������� ������� ������������, ������� ����������� �������� �������.
//  See Also:
//  Arguments:
//      Dest - ��������� �� �������������� ������. 
//		Src  - ��������� �� ���������� ������.
//		n	 - ���������� ���������� ����.
//  Return Value:
//      ��������� �� �������������� ������.
//  Summary:
//      ����������� ������� ������.
IL_FUNC	IL_BYTE*	cmnMemCopy (IL_BYTE *Dest, const IL_BYTE *Src, IL_WORD n);

//  Description:
//      ���������� ������ n-���� ������� Src � ������ Dest.<p/>
//		���� ��������� ������� ������������, ������� ����������� �������� �������.
//  See Also:
//  Arguments:
//      Dest - ��������� �� �������������� ������. 
//		Src  - ��������� �� ����������� ������.
//		n	 - ���������� ������������ ����.
//  Return Value:
//      ��������� �� �������������� ������.
//  Summary:
//      ����������� �������� ������.
IL_FUNC	IL_BYTE*	cmnMemMove(IL_BYTE *Dest, const IL_BYTE *Src, IL_WORD n);

//  Description:
//      �������������� ������ n-���� ������� Dest ������� ���������.
//  See Also:
//  Arguments:
//      Dest - ��������� �� ���������� ������. 
//		n	 - ���������� ���������� ����.
//  Return Value:
//      ��������� �� ���������� ������.
//  Summary:
//      ��������� ������� ������.
IL_FUNC	IL_BYTE*	cmnMemClr (IL_BYTE *Dest, IL_WORD n);

//  Description:
//      �������� ���� ����������� �������������� ������ ��������� �����.
//  See Also:
//		cmnMemFree
//  Arguments:
//      Size - ������ ���������� ������ � ������. 
//  Return Value:
//      ��������� �� ���������� ���� ������ ��� NULL ��� ��������� ����������.
//  Summary:
//      ��������� ����� ������������ ������.
IL_FUNC IL_BYTE*    cmnMemAlloc(IL_WORD Size);

//  Description:
//      ����������� ����� ���������� ���� ����������� �������������� ������.
//  See Also:
//		cmnMemAlloc
//  Arguments:
//      Buf - ��������� �� ������������� ���� ������������ ������. 
//  Return Value:
//      ��������� �� ���������� ���� ������ ��� NULL ��� ��������� ����������.
//  Summary:
//      ������������ ����� ������������ ������.
IL_FUNC void		cmnMemFree(void *Buf);



#endif//_HAL_COMMON_H_