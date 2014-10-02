#ifndef _HAL_PARAMETER_H_
#define _HAL_PARAMETER_H_

#include "il_types.h"

// ���������������� �������� '���������� ��������� ����� ���''
#define IL_PARAM_CAID			1
// ���������������� �������� '���������� ��������� ����� ���������'
#define IL_PARAM_CIFDID			2
// ���������������� �������� '���������� ��������� ����� ��'
#define IL_PARAM_CCAID			3

// ���������������� �������� '������������ ������'
#define IL_PARAM_READERNAME		14
// ���������������� �������� '�������� � ���������'
#define IL_PARAM_TERMINAL_INFO  0x9F1C
// ���������������� �������� '������ ��������� ����� ��������� RSA' 
#define IL_PARAM_SIFDID_MOD     15
// ���������������� �������� '���������� ��������� ����� ��������� RSA'
#define IL_PARAM_SIFDID_EXP     16
// ���������������� �������� '�������� ���� ��������� ����'
#define IL_PARAM_SIFDID_GOST    17
// ���������������� �������� '������������ ������ ������������� ������ ������������ (���)'
#define IL_PARAM_SM_READERNAME  18
// ���������������� �������� '������ ������������� ������������� ������ ������������ (���)'
#define IL_PARAM_SM_PIN			19

// ���������������� �������� '���� ��� ���������� ������������ ��-���������� RSA'
#define IL_PARAM_MK_AC_ID_RSA	21
// ���������������� �������� '���� ��� ���������� ������������ ��-���������� ����'
#define IL_PARAM_MK_AC_ID_GOST	22
// ���������������� �������� '���� ��� ����������� ������ ����� ���������� ����� RSA'
#define IL_PARAM_MK_SM_ID_RSA	23
// ���������������� �������� '���� ��� ����������� ������ ����� ���������� ����� ����'
#define IL_PARAM_MK_SM_ID_GOST  24
// ���������������� �������� '������� ������-����� ��� ���������� ������������ � ����������� ������ ����� ���������� ����� ����'
#define IL_PARAM_MK_AC_ID_DIVERS_FLAG_GOST  26
// ���������������� �������� '������� ������-����� ��� ���������� ������������ � ����������� ������ ����� ���������� ����� RSA'
#define IL_PARAM_MK_AC_ID_DIVERS_FLAG_RSA   27 
// ���������������� �������� '��������� ���� ���������� ����� RSA'
#define IL_PARAM_S_SP_ID_RSA	28
// ���������������� �������� '��������� ���� ���������� ����� ����'
#define IL_PARAM_S_SP_ID_GOST	29
// ���������������� �������� '���������� ��������� ����� ���������� ����� ����'
#define IL_PARAM_CSPID_RSA		30
// ���������������� �������� '���������� ��������� ����� ���������� ����� RSA'
#define IL_PARAM_CSPID_GOST		31
// ���������������� �������� '��������� ���� ��� ����'
#define IL_PARAM_S_CA_ID_RSA	32
// ���������������� �������� '��������� ���� ��� RSA'
#define IL_PARAM_S_CA_ID_GOST	33
// ���������������� �������� '���� ��� ����������� ������ ����� ���������� ����� RSA ������������� ������ ������������ (���)'
#define IL_PARAM_SE_SM_ID_RSA	34
// ���������������� �������� '���� ��� ����������� ������ ����� ���������� ����� ���� ������������� ������ ������������ (���)'
#define IL_PARAM_SE_SM_ID_GOST  35

// ���������������� �������� '������������� ��������� ������ ������������'
#define IL_PARAM_MEMBER_ID      40
// ���������������� �������� '���������� ���������� ����� �������������'
#define IL_PARAM_IDENT_OP_ID    41
// ���������������� �������� '��������, ����������� ��� ������������� ������'
#define IL_PARAM_PAYMENT_INFO   42
// ���������������� �������� '��� �������������� ��-����������'
#define IL_PARAM_AAC            43

// ���������������� �������� '��� �����-������� ��� ����������������'
#define IL_PARAM_PROT_LOGNAME		100	
// ���������������� �������� '������� ������������� ���������������� � ����-������'
#define IL_PARAM_PROT_LOGFILE		101	
// ���������������� �������� '������� ������������� ���������������� � ���� Output'
#define IL_PARAM_PROT_OUTPUT		102	
// ���������������� �������� '���� ���������������� ������� ����-������'
#define IL_PARAM_PROT_READER		103	
// ���������������� �������� '���� ���������������� ������� �����'
#define IL_PARAM_PROT_CARD			104	
// ���������������� �������� '���� ���������������� ������� CardLib'
#define IL_PARAM_PROT_CARDLIB		105	
// ���������������� �������� '���� ���������������� ������� FuncLib'
#define IL_PARAM_PROT_FUNCLIB		106	
// ���������������� �������� '���� ���������������� ������� OpLib'
#define IL_PARAM_PROT_OPLIB			107	
// ���������������� �������� '���� ���������������� ������� SmLib'
#define IL_PARAM_PROT_SMLIB			108	
// ���������������� �������� '������� ���������������� ������� TellME'
#define	IL_PARAM_PROT_TELLME		109	
// ���������������� �������� '���� �������� ���������������� TellME'
#define IL_PARAM_PROT_EXTERN		110  

// ���������������� �������� '������������ �������������� ����'
#define IL_PARAM_USE_GOST			999

// ��� ����������������� ��������� '�������� ������'
#define IL_PARAM_FORMAT_BYTEARRAY	1
// ��� ����������������� ��������� - '������'
#define IL_PARAM_FORMAT_STRING		2

// ��������� ��������� ����������������� �����
typedef struct 
{
	IL_WORD ID;			// ������������� ���������
	IL_WORD Format;		// ��� ������� ���������
    IL_WORD Length;		// ����� �������� ���������
	IL_CHAR *Section;	// ������������ ������ ����������������� �����
	IL_CHAR *Name;		// ������������ ����������������� ���������.
}IL_PARAMETER_DESCR;

//  Description:
//      ������������ ������ � ������ ������ ������� � ����������������� ����� �� ��� ����� � ����������.<p/>
//		� �������� �������� ��� ���������� ���������������� ������ ������������ ������� �� �������� ����������� ����������.<p/>
//		� ������������ TellME ��� ���� ����� ������������ ��������������� ������� 'c:\UECParam'. 
//  See Also:
//  Arguments:
//      out_FullParamFileName	- ��������� �� ������������ ������ � ������ ������ ������� � ���������� ����������������� �����. 
//		in_ParamFileName		- ��������� �� ������ � ������ ����������������� �����.
//		in_ParamFileExt			- ��������� �� ������ � ����������� ����������������� �����
//  Return Value:
//      ��������� �� ������������ ������ � ������ ������ ������� � ���������� ����������������� �����.
//  Summary:
//      ��������� ������� ����� ������� � ����������������� �����.
IL_FUNC IL_CHAR* GetParamFilename(IL_CHAR *out_FullParamFileName, IL_CHAR *in_ParamFileName, IL_CHAR *in_ParamFileExt);

//  Description:
//      ��������� �������� ����������������� ��������� �� ���������� ��������������.
//  See Also:
//  Arguments:
//      ParamId		  - ������������� ���������. 
//		out_pParamBuf - ��������� �� �������� ����� ��� ������������ �������� ���������.
//		out_pParamLen - ��������� �� ����� ������������� ��������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� �������� ����������������� ��������� �� ��������������.
IL_FUNC IL_RETCODE prmGetParameter(IL_WORD ParamId, IL_BYTE *out_pParamBuf, IL_DWORD *out_pParamLen);

//  Description:
//      ��������� �������� ����������������� ��������� ��������� ����� �� ���������� ��������������.<p/>
//		� �������� ����� ����������������� ����� ������������ 'host.ini'.
//  See Also:
//  Arguments:
//      ParamId		  - ������������� ���������. 
//		out_pParamBuf - ��������� �� �������� ����� ��� ������������ �������� ���������.
//		out_pParamLen - ��������� �� ����� ������������� ��������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� �������� ����������������� ��������� �� ��������������.
IL_FUNC IL_RETCODE prmGetParameterHost(IL_WORD ParamId, IL_BYTE *out_pParamBuf, IL_DWORD *out_pParamLen);

//  Description:
//      ��������� �� ������ 'CIN' ����� �������� ��������� ����� �������������� � ��������� CIN �������� PAN � SNILS.
//		� �������� ����� ����������������� ����� ������������ 'host.ini'.
//  See Also:
//  Arguments:
//      in_pCIN		- �������� CIN. 
//		out_pPan	- ��������� �� �������� ����� ��� �������� PAN.
//		out_pSnils	- ��������� �� �������� ����� ��� �������� SNILS.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� �������� PAN � SNILS �� CIN.
IL_FUNC IL_RETCODE prmGetParameterHostCin2PanSnils(IL_BYTE *in_pCIN, IL_BYTE *out_pPan, IL_BYTE  *out_pSnils);

//  Description:
//      ��������� �������� ����������������� ��������� �� ���������� ��������������, ������ ����� ��������������� ������ � ���� ��������������� ����/RSA.<p/>
//		������������ ������������ ��������� ����������� ����������� ����������� � ����� ��������� ��������� �� ��������� ��������������� � ������ �����.
//  See Also:
//  Arguments:
//      ParamId		  - ������������� ���������. 
//		KeyVer		  - ������ ����� ��.
//		ifGost		  - ��� ��������������� ����/RSA.
//		out_pParamBuf - ��������� �� �������� ����� ��� ������������ �������� ���������.
//		out_pParamLen - ��������� �� ����� ������������� ��������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� �������� ����������� ����������������� ���������.
IL_FUNC IL_RETCODE prmGetParameterKeyVer(IL_WORD ParamId, IL_BYTE KeyVer, IL_BYTE ifGost, IL_BYTE *out_pParamBuf, IL_DWORD *out_pParamLen);

//  Description:
//      ��������� �������� ����������������� ��������� �� ����� 'sectors.ini' �� �������������� ������� � ������������ ���������.
//  See Also:
//  Arguments:
//      SectorId	  - ������������� ������� ���������� ������. 
//		SectorVer	  - ������ ������� �������.
//		in_pParamName - ��������� �� ������ � ������ ���������.
//		out_pParamBuf - ��������� �� �������� ����� � Win-1251-������� ������������ �������� ���������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� �������� ����������������� ��������� �� ����� 'sectors.ini'.
IL_FUNC IL_RETCODE prmGetParameterSectorEx(IL_WORD SectorId, IL_BYTE SectorVer, IL_CHAR *in_pParamName, IL_CHAR *out_pParamBuf);

//  Description:
//      ��������� ������������ ��� ��������� ����� ������ �������� ��������� ���������� ������ ����������������� ����� 'sectors.ini'.
//  See Also:
//  Arguments:
//      SectorId	  - ������������� ������� ���������� ������. 
//		SectorVer	  - ������ ������� �������.
//		SectorExDescr - ������-��������� ����������� ������ ������� ������ ���������� �������:<p/><p/>
//								"Icon=S;
//								BlockDecr1=T|L|N;DataDecr11=d|t|l|n;DataDecr12=d|t|l|n;...;DataDecr1N=d|t|l|n;<p/>
//								BlockDecr2=T|L|N;DataDecr21=d|t|l|n;DataDecr22=d|t|l|n;...;DataDecr2N=d|t|l|n;<p/>
//								. . .<p/>
//								BlockDecrN=T|L|N;DataDecrN1=d|t|l|n;DataDecrN2=d|t|l|n;...;DataDecrNN=d|t|l|n;" , ���:
//								- S - ������������ ������� ������.
//								- T - ��� ������� � ������ �����.
//								- L - ����� ����� �����.
//								- N - ����������� �����.
//								- d - ������������� �������� ������. ��� ��������� ����� �������� �������� ������� �� ������ �����. ��� TLV-����� ����������������� �������� ���� �������� ������. 
//								- t - ��� �������� ������.
//								- l - ������������ ����� �������� ������.
//								- n - ������������ �������� ������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������ �������� ��������� ���������� ������ ����������������� ����� 'sectors.ini'.
IL_FUNC IL_RETCODE prmWriteSectorExDescr(IL_WORD SectorId, IL_BYTE SectorVer, IL_CHAR *SectorExDescr);

//  Description:
//      ��������� �������� ����������������� ��������� �� ����� 'iqfront.ini' �� ������������ ���������.<p/>
//		������������ � ������������ TellME.
//  See Also:
//  Arguments:
//      SectionName	  - ������������ ������ ����������������� �����. 
//		in_pParamName - ��������� �� ������ � ������ ���������.
//		out_pParamBuf - ��������� �� �������� ����� � Win-1251-������� ������������ �������� ���������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� �������� ����������������� ��������� �� ����� 'iqfront.ini'.
IL_FUNC IL_RETCODE prmGetParameterIqFront(IL_CHAR *SectionName, IL_CHAR *in_pParamName, IL_CHAR *out_pParamBuf);

//  Description:
//      ��������� �������� ����������������� ��������� �� ������ 'PatternDescr' ����� 'sectors.ini' �� ������������ ���������.<p/>
//		������������ ��� ��������� ������ ��������� �������� ���������� �������� ������ TLV-������.
//  See Also:
//  Arguments:
//		in_pParamName - ��������� �� ������ � ������ ���������.
//		out_pParamBuf - ��������� �� �������� ����� � Win-1251-������� ������������ �������� ���������.
//      MaxBufLen     - ������������ ������ ��������� ������
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������� �������� ����������������� ��������� �� ������ 'PatternDescr' ����� 'sectors.ini'.
IL_FUNC IL_RETCODE prmGetParameterPattern(IL_CHAR *ParamName, IL_CHAR *out_ParamBuf, IL_WORD MaxBufLen);

//
// ������ ����� ������ WCF
//
IL_FUNC void prmSetUecServiceToken(WCHAR* pwszUecServiceToken);
IL_FUNC IL_RETCODE prmGetParameterByService(IL_WORD ParamId, IL_BYTE *out_pParamBuf, IL_DWORD *out_pParamLen);
IL_FUNC IL_RETCODE prmGetParameterKeyVerByService(IL_WORD ilParam, IL_BYTE KeyVer, IL_BYTE ifGostCrypto, IL_BYTE* pCertBuf, IL_DWORD* pdwCertLen);

//
// ������ ����� ���������� ����������
//
IL_FUNC void prmSetGlobalConfigurationData(WCHAR* pwszReaderName, IL_BYTE* g_szOKO1OpenCert, IL_BYTE* g_szTerminalOpenCert, IL_BYTE* g_szUC1OpenCert, IL_BYTE* g_szTerminalClosedCertGOST);
IL_FUNC IL_RETCODE prmGetParameterByGlobalConfigurationData(IL_WORD ilParam, IL_BYTE* pParamBuf, IL_DWORD* pdwParamLen);
IL_FUNC IL_RETCODE prmGetParameterKeyVerByByGlobalConfigurationData(IL_WORD ilParam, IL_BYTE KeyVer, IL_BYTE ifGostCrypto, IL_BYTE* pCertBuf, IL_DWORD* pdwCertLen);

#endif //_HAL_PARAMETER_H_