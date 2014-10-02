#ifndef _HAL_PROTOCOL_H_
#define _HAL_PROTOCOL_H_

#include <stdio.h>
#include "il_types.h"
#include "il_error.h"

// �������� ���������������� - ����������� ����������������
#define PROT_ALWAYS		0
// �������� ���������������� - ������� �����
//#define PROT_CARD		1
// �������� ���������������� - ������� 1  ������� ����-������
#define PROT_READER1	1
// �������� ���������������� - ������� 2  ������� ����-������
#define PROT_READER2	2
// �������� ���������������� - ������� 3  ������� ����-������
#define PROT_READER3	3
// �������� ���������������� - ������� 1  ������� CardLib
#define PROT_CARDLIB1	4
// �������� ���������������� - ������� 2  ������� CardLib
#define PROT_CARDLIB2	5
// �������� ���������������� - ������� 3  ������� CardLib
#define PROT_CARDLIB3	6
// �������� ���������������� - ������� 1  ������� FuncLib
#define PROT_FUNCLIB1	7
// �������� ���������������� - ������� 2  ������� FuncLib
#define PROT_FUNCLIB2	8
// �������� ���������������� - ������� 3  ������� FuncLib
#define PROT_FUNCLIB3	9
// �������� ���������������� - ������� 1  ������� OpLib
#define PROT_OPLIB1		10
// �������� ���������������� - ������� 2  ������� OpLib
#define PROT_OPLIB2		11
// �������� ���������������� - ������� 3  ������� OpLib
#define PROT_OPLIB3		12
// �������� ���������������� - ������� SmLib
#define PROT_SMLIB		13
// �������� ���������������� - ������� 1 ������ TellME 
#define PROT_TELLME1	14
// �������� ���������������� - ������� 2 ������ TellME
#define PROT_TELLME2	15
// �������� ���������������� - ������� 3 ������ TellME
#define PROT_TELLME3	16
// �������� ���������������� - ������� ���������������� TellME
#define PROT_EXTERN		17

// ��������� ������� ����������������
typedef struct
{
	FILE *hFile;					// ���������� ����� �������
	IL_CHAR FileDirName[256+1];		// ����
	IL_CHAR FileName[32+1];			// ��� �����
	IL_CHAR FullFileName[288+1];	// ������ ��� �����
	IL_BYTE DateStamp[3];			// ����� ����
	IL_BYTE ifOutput;				// ������� ������������� ���������������� � ���� Output
	IL_BYTE ifReader;				// ���� ���������������� ������� ����-������
	IL_BYTE ifCard;					// ���� ���������������� ������� �����
	IL_BYTE ifCardLib;				// ���� ���������������� ������� CardLib
	IL_BYTE ifFuncLib;				// ���� ���������������� ������� FuncLib
	IL_BYTE ifOpLib;				// ���� ���������������� ������� OpLib
	IL_BYTE ifSmLib;				// ���� ���������������� ������� SmLib
	IL_BYTE ifTellMe;				// ���� ���������������� ������� TellMe
	IL_BYTE ifExtern;				// ���� �������� ���������������� TellME
} PROTOCOL_DESCR;

//  Description:
//      �������������� ������ ���������������� ���������� �������� ��� ���.<p/>
//		���������� ��������� ������� ���������������� ���������������� ����������� ����������������� ����� 'terminal.ini'.
//  See Also:
//  Arguments:
//      �����������.
//  Return Value:
//      IL_RETCODE - ��� ������.
//  Summary:
//      ������������� ������ ������� ����������������.
IL_FUNC IL_RETCODE protInit(void);

//  Description:
//      ��������� ������ ���������������� ���������� �������� ��� ���.
//  See Also:
//  Arguments:
//      �����������.
//  Return Value:
//      IL_RETCODE - ��� ������.
//  Summary:
//      �������������� ������� ����������������.
IL_FUNC IL_RETCODE protDeinit(void);

//  Description:
//      ���������� ������ ���������������� ���������� ��������� ����������� � ������ � ���� ��������� �������������� ���������� ���������.<p/>
//		���� � ��������� �� ��������������� ���� ���������������� ��� ���������� ���������, ���������������� �� ������������. 
//  See Also:
//  Arguments:
//      ProtSource - ������������ ��������� ����������������.
//		Str		   - ��������� �� ��������������� ������.	
//  Return Value:
//      IL_RETCODE - ��� ������.
//  Summary:
//      ������ ������ ����������������.
IL_FUNC IL_RETCODE protWrite(IL_WORD ProtSource, IL_CHAR *Str);

//  Description:
//      ����������� � ���������� ������ ���������������� ���������� ��������� � ������ � ���� ��������� �������������� ���������� ���������.<p/>
//		���� � ��������� �� ��������������� ���� ���������������� ��� ���������� ���������, ���������������� �� ������������. 
//  See Also:
//  Arguments:
//      ProtSource - ������������ ��������� ����������������.
//		Format	   - ��������� �� ������-������.
//		...		   - ������ ������� ����������, ��������������� ������-�������. 
//  Return Value:
//      IL_RETCODE - ��� ������.
//  Summary:
//      ��������������� ������ ������ ����������������.
IL_FUNC IL_RETCODE protWriteEx(IL_WORD ProtSource, const char *Format, ...);

//
// ��� ������� ���������� ���� ���������� ���������������� ���������� ��������������� ��������� 'PROT_IGNORE'
//#define PROT_IGNORE

#ifndef PROT_IGNORE
#define PROT_INIT										protInit();
#define PROT_DEINIT										protDeinit();
#define PROT_WRITE(s, a)								protWrite(s,a);
#define PROT_WRITE_EX0(s,f)								protWriteEx((s),(f));
#define PROT_WRITE_EX1(s,f,a1)							protWriteEx((s),(f),(a1));
#define PROT_WRITE_EX2(s,f,a1,a2)						protWriteEx((s),(f),(a1),(a2));
#define PROT_WRITE_EX3(s,f,a1,a2,a3)					protWriteEx((s),(f),(a1),(a2),(a3));
#define PROT_WRITE_EX4(s,f,a1,a2,a3,a4)					protWriteEx((s),(f),(a1),(a2),(a3),(a4));
#define PROT_WRITE_EX5(s,f,a1,a2,a3,a4,a5)				protWriteEx((s),(f),(a1),(a2),(a3),(a4),(a5));
#define PROT_WRITE_EX6(s,f,a1,a2,a3,a4,a5,a6)			protWriteEx((s),(f),(a1),(a2),(a3),(a4),(a5),(a6));
#define PROT_WRITE_EX7(s,f,a1,a2,a3,a4,a5,a6,a7)		protWriteEx((s),(f),(a1),(a2),(a3),(a4),(a5),(a6),(a7));
#define PROT_WRITE_EX8(s,f,a1,a2,a3,a4,a5,a6,a7,a8)		protWriteEx((s),(f),(a1),(a2),(a3),(a4),(a5),(a6),(a7),(a8));
#define PROT_WRITE_EX9(s,f,a1,a2,a3,a4,a5,a6,a7,a8,a9)	protWriteEx((s),(f),(a1),(a2),(a3),(a4),(a5),(a6),(a7),(a8),(a9));


#else
#define PROT_INIT
#define PROT_DEINIT		
#define PROT_WRITE_EX0(s,f)
#define PROT_WRITE_EX1(s,f,a1)
#define PROT_WRITE_EX2(s,f,a1,a2)
#define PROT_WRITE_EX3(s,f,a1,a2,a3)
#define PROT_WRITE_EX4(s,f,a1,a2,a3,a4)
#define PROT_WRITE_EX5(s,f,a1,a2,a3,a4,a5)
#define PROT_WRITE_EX6(s,f,a1,a2,a3,a4,a5,a6)
#define PROT_WRITE_EX7(s,f,a1,a2,a3,a4,a5,a6,a7)
#define PROT_WRITE_EX8(s,f,a1,a2,a3,a4,a5,a6,a7,a8)
#define PROT_WRITE_EX9(s,f,a1,a2,a3,a4,a5,a6,a7,a8,a9)
#endif//PROT_IGNORE

#endif//_HAL_PROTOCOL_H_