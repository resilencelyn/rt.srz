#ifndef _SMLIB_H_
#define _SMLIB_H_

#include "il_types.h"
#include "il_version.h"
#include "HAL_SCReader.h"
#include "CardLibEx.h"
#include "FuncLibEx.h"
#include "HAL_CryptoHandle.h"

#define UECLIB_SM_VER	  0x10

#define INS_AUTH_BEGIN    0x16
#define INS_AUTH_COMPLETE 0x18
#define INS_SP_SESS_INIT  0x1A
#define INS_SE_ACTIVATION 0x1C

// ��������� ������ ������������
typedef struct 
{
	IL_HANDLE_READER hRdr;			// ���������� �����-���� ������
	IL_BYTE AppVer;					// ������ �����
    IL_RECORD_LIST sectors;			// ������ ��������
    IL_RECORD_LIST blocks;			// ������ ������
	IL_BYTE Certificates[4][2048];	// ������ ������������ �������� ������
	IL_WORD wCertificatesLen[4];	// ������ ���� ������������
}IL_SM_HANDLE;

//  Description:
//      ������������� ������ ������������.
//  See Also:
//      smDeinit
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//      ilSmRdrSettings - ��������� �� ��������� ��������� ������ ������ ������������
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ������������� ������ ������������.
IL_FUNC IL_RETCODE smInit(IL_HANDLE_CRYPTO* phCrypto, IL_READER_SETTINGS ilSmRdrSettings);

//  Description:
//      ��������������� ������ ������������. 
//  See Also:
//      smInit
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      �������������� ������ ������������.
IL_FUNC IL_RETCODE smDeinit(IL_HANDLE_CRYPTO* phCrypto);

//  Description:
//      ������ ������ � ������� ������������.
//  See Also:
//      smClose
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ������ ������ � ������� ������������.
IL_FUNC IL_RETCODE smOpen(IL_HANDLE_CRYPTO* phCrypto);

//  Description:
//      ���������� ������ � ������� ������������.
//  See Also:
//      smOpen
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ���������� ������ � ������� ������������.
IL_FUNC IL_RETCODE smClose(IL_HANDLE_CRYPTO* phCrypto);

//  Description:
//      ����� ���������� ������ ������������.
//  See Also:
//      
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//      pOut			- ��������� �� ������������  ������.
//      pwOutLen		- ��������� �� ����� ������������ ������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ����� ���������� ������ ������������.
IL_FUNC IL_RETCODE smAppSelect(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* pOut, IL_WORD* pwOutLen);

//  Description:
//      ����� ���������� ��� ����� ������ ������ ������������.
//  See Also:
//      
//  Arguments:
//      phCrypto	- ��������� �� �������� ������ ������������.
//      P1			- ���������� ������:
//						- '00' � ���� ������ (EF).
//						- '04' � ���������� �� AID ����������.
//      P2			- ����� �������� ������:
//						- '00' � ������� ������ ������ ����������.
//						- '04' � ������� File Control Parameters (FCP), �������� ������ ��� EF.
//						- '08' � ������� File Management Data (FMD), �������� ������ ��� ADF.
//						- '0C' � �� ���������� ���������� � ��������� �������.
//		pId			- ��������� �� ������� ������ �������������� �������������� ����� ��� ����������.
//		IdLen		- ����� ������� ������.
//		pOut		- ��������� �� ������������  ������.
//		pOutLength	- ��������� �� ����� ������������ ������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ����� ���������� ��� ����� ������ ������ ������������.
IL_FUNC IL_RETCODE smSelect(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE P1, IL_BYTE P2, IL_BYTE* pId, IL_BYTE IdLen, IL_BYTE* pOut, IL_WORD* pOutLength);

//  Description:
//      ��������� �������� ��������� ������ ���������� �� �� ����.
//  See Also:
//      
//  Arguments:
//      phCrypto	- ��������� �� �������� ������ ������������.
//		wTag		- ������������� ���� ����������� �������� ������.
//		pOut		- ��������� �� ������������ ������ �� ��������� �������� �������.
//		pOutLength	- ��������� �� ����� ������������ ������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ��������� �������� ��������� ������ ���������� �� �� ����.
IL_FUNC IL_RETCODE smGetData(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wTag, IL_BYTE* pOut, IL_WORD* pwOutLength);

//  Description:
//      ������ ����������� ��������� ����� ��.
//  See Also:
//      
//  Arguments:
//      phCrypto	- ��������� �� �������� ������ ������������.
//		wFileId		- ������������� ������������ ����� ������.
//		pOut		- ��������� �� ������������ ������.
//		pOutLength	- ��������� �� ����� ������������ ������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ������ ����������� ��������� ����� ��.
IL_FUNC IL_RETCODE smReadFile(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wFileId, IL_BYTE* FileData, IL_WORD* pwFileDataLen);

//  Description:
//      ������ ����� ������ �� ������ ������������ ��������� GET DATA ��� READ BINARY.
//  See Also:
//      
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//      wFileId			- ������������� �����, ���� � ������ ���� ������ �������� �� �� �������� �������
//      wDataId			- ������������� (���) ������, ���� � ������ ���� ������ �������� �� ��������� �����
//      pDataOut		- ��������� �� ������������  ������.
//      pwDataOutLen	- ��������� �� ����� ������������ ������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ������ ����� ������ �� ������ ������������ ��������� GET DATA ��� READ BINARY.
IL_FUNC IL_RETCODE smReadBlock(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wFileId, IL_WORD wDataId, IL_BYTE* pDataOut, IL_WORD* pwDataOutLen);

//  Description:
//      ������������ ������ ������ ������������.
//  See Also:
//      
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//      P2				- �������� P2 APDU VERIFY.
//      pDataIn8		- ��������� �� ���-���� ������ 8 ����.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ������������ ������ ������ ������������.
IL_FUNC IL_RETCODE smVerify(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE P2, IL_BYTE* pDataIn8, IL_BYTE* pbTriesRemained);

//  Description:
//      ����� ������ ������ ������������.
//  See Also:
//      
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//      P2				- �������� P2 APDU CHANGE REF DATA.
//      pDataIn8		- ��������� �� ���-���� ������ 8 ����.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ����� ������ ������ ������������.
IL_FUNC IL_RETCODE smChangeRefData(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE P2, IL_BYTE* pDataIn8, IL_WORD wDataInLen);

//  Description:
//      ������ ������ ������������ ������ ������������. 
//		��������� ����������� ������ ������������ �� ������ 5,6,7,8 ������ ������������.
//		��������� ��������� ����������� � ��������� ������ ������������.
//  See Also:
//      
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ������ ������ ������������ ������ ������������.
IL_FUNC IL_RETCODE smReadCertificates(IL_HANDLE_CRYPTO* phCrypto);

//  Description:
//      ��������� ����������� ���������� ����� � ��������� �����������, ���������� �� ������ ������������. 
//  See Also:
//      smGetCertificate
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//      ilParamType		- ��������� �� �������� ������ ������������.
//      KeyVer			- ������ ��������� ����� �����������.
//      ifGost			- ��� ����� �����������: 0 - RSA, 1 - ����.
//      certGost		- ��������� �� ����� ��� ������������� �����������.
//      pdwCertGostLen	- ��������� �� ����� ������������� �����������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ��������� ����������� ���������� ����� � ��������� �����������.
IL_FUNC IL_RETCODE smGetCertificate(IL_HANDLE_CRYPTO* phCrypto, IL_WORD ilParamType, IL_BYTE KeyVer, IL_BYTE ifGost, IL_BYTE* certGost, IL_DWORD* pdwCertGostLen);

//  Description:
//      ������� ������������ ������ ��� ������������ ������ � ���������� (1� ����).
//  See Also:
//      smReadCertificates
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//      pIn				- ��������� �� ������� ������ ��� ������������ ������ � ����������.
//      InLen			- ����� ������� ������.
//      pOut			- ��������� �� �������� ������ ��� ������������ ������ � ����������.
//      pOutLen			- ��������� �� ����� �������� ������.
//      ifGost			- ��� ��������������� ������: 0 - RSA, 1 - ����.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ������� ������������ ������ ��� ������������ ������ � ���������� (1� ����).
IL_FUNC IL_RETCODE smAuthBegin(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* pIn, IL_WORD InLen, IL_BYTE* pOut, IL_WORD* pOutLen, IL_BYTE ifGost);

//  Description:
//      ������� ������������ ������ ��� ������������ ������ � ���������� (2� ����).
//  See Also:
//      smAuthComplete
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//      pIn				- ��������� �� ������� ������ ��� ������������ ������ � ����������.
//      InLen			- ����� ������� ������.
//      ifGost			- ��� ��������������� ������: 0 - RSA, 1 - ����.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ������ ������ ������������ ������ ������������.
IL_FUNC IL_RETCODE smAuthComplete(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* pIn, IL_WORD InLen, BYTE ifGost);

//  Description:
//		���������a APDU � ������ ���.
//      ������� ��������� �������� �� ���������� APDU � ������ ���.
//  See Also:
//      smProcessSM
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//      pilApdu			- ��������� �� ��������� ������ � ���������������� APDU ��������.
///  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ���������a APDU � ������ ���.
IL_FUNC IL_RETCODE smPrepareSM(IL_HANDLE_CRYPTO* phCrypto, IL_APDU* pilApdu);

//  Description:
//      ������� ��������� �������� �� �������� � ����������� APDU � ������ ���.
//  See Also:
//      smProcessSM
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//      pilApdu			- ��������� �� ��������� ������ � ����������� APDU ��������.
///  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ������ ������ ������������ ������ ������������.
IL_FUNC IL_RETCODE smProcessSM(IL_HANDLE_CRYPTO* phCrypto, IL_APDU* pilApdu);

//  Description:
//      ��������� �� ������ ������������ ���������� �����. 
//  See Also:
//      
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//		wOutLength		- ����� ���������� �����.
//		pOut			- ��������� �� �������� �����.  
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ��������� �� ������ ������������ ���������� �����.
IL_FUNC IL_RETCODE smGetChallenge(IL_HANDLE_CRYPTO* phCrypto, IL_WORD wOutLength, IL_BYTE* pOut);

//  Description:
//      ������� ������������� ��� �������������� �������� ������� �� ������� ������ ������������. 
//  See Also:
//      
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//		CLA				- ����� �������.
//		P2				- ������������ ������������� �����.
//		pDataIn			- ��������� �� ������ ��������������.
//		wDataInLen		- ����� ������ ��������������.
//		pDataOut		- ��������� �� ����� ��� ������ ���������-������ �������.
//		pDataOutLen		- ��������� �� ����� ������������ ������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      �������������� �������� ������� �� ������� ������ ������������.
IL_FUNC IL_RETCODE smMutualAuth(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE CLA, IL_BYTE P2, IL_BYTE* pDataIn, IL_WORD wDataInLen, IL_BYTE* pDataOut, IL_WORD* pDataOutLen);

//  Description:
//      ������� ��������� �������� �� ���������\����������� ������ ������������.
//  See Also:
//      smActivationStart
//		smOfflineActivationFinish		
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//      CLA				- ����� �������.
//		IfDeactivation	- ������� ���������� ����������� ������ ������������
///  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ���������\����������� ������ ������������.
IL_FUNC IL_RETCODE smActivation(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE CLA, IL_BYTE IfDeactivation);

//  Description:
//      ������� ��������� �������� �� ������������� ���������� ������ � ����������� ������.
//  See Also:
//				
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//      pDataIn14		- ������� ������ ��� ��������� ������ � ����������� ������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ������������� ������ � ����������� ������.
IL_FUNC IL_RETCODE smSpSessionInit(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* pDataIn14);

//  Description:
//      ������� ��������� �������������� �������� ��������� ���������\����������� ������ ������������.
//  See Also:
//				
//  Arguments:
//      phCrypto		 - ��������� �� �������� ������ ������������.
//      ifStateActivated - ��������� �� ������������ ������� ��������������� ��������� ������ ���������.
//		pAC0003			 - ��������� �� ������������ ������ � ������� ������� � ����� 3 ��� ����������� ������ ���������.
//		pdwAC0003		 - ��������� �� ����� ������������ ������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ������������� ��������� ���������\����������� ������ ������������.
IL_FUNC IL_RETCODE smActivationStart(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* ifStateActivated, IL_BYTE* pAC0003, IL_DWORD* pdwAC0003);

//  Description:
//      ������� ��������� ����������� �������� ��������� ���������\����������� ������ ������������.
//  See Also:
//				
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//      ifGost			- ������� ������������� ��������������� ����.
//		ifDeactivate	- ������� ���������� ��������� ����������� ������ ������������.
//		pSmOwnerName    - ��������� �� ������� ������ ������������ ��������� ������ ������������.
//		wSmOwnerNameLen - ����� ������������ ��������� ������ ������������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ���������� ��������� ���������\����������� ������ ������������.
IL_FUNC IL_RETCODE smOfflineActivationFinish(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE ifGost, BYTE ifDeactivate, IL_BYTE* pSmOwnerName, IL_WORD wSmOwnerNameLen);

//  Description:
//      ��������� �������������� ������� ������������ ���������� ������.
//  See Also:
//				
//  Arguments:
//      phCrypto			- ��������� �� �������� ������ ������������.
//      Msg					- ��������� �� ����������������� ������.
//		wMsgLen				- ����� ����������������� ������.
//		S					- ��������� �� �������������� ����������� ������ ��� ��� ����������������� ������.
//		wS_len				- ����� �������������� ���.
//		PublicKeyCert		- ��������� �� ���������� ��������� ����� ���������� ������.
//		wPublicKeyCertLen	- ����� �����������
//		ifGost				- ������� �������������� �� ��������������� ����.
//		AppVer				- ������ ��-���������� (�����).
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      �������������� ������� ������������ ���������� ������.
IL_FUNC IL_RETCODE smAuthServiceProvider(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* Msg, IL_WORD wMsgLen, IL_BYTE* S, IL_WORD wS_len, IL_BYTE* PublicKeyCert, IL_WORD wPublicKeyCertLen, IL_BYTE ifGost, IL_BYTE AppVer);

//  Description:
//      ���������� ������� ������������ APDU-�������.
//  See Also:
//				
//  Arguments:
//      phCrypto	- ��������� �� �������� ������ ������������.
//      In			- ��������� �� ����� � ������������ ��������� ������� APDU-�������.
//		wInLen		- ����� ������ APDU-�������.
//		Out			- ��������� �� ����� ��� �������� ������ APDU-�������.
//		pwOutLen	- ��������� �� ����� �������� ������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ���������� ������� ������������ APDU-�������.
IL_FUNC IL_RETCODE smSendAPDU(IL_HANDLE_CRYPTO* phCrypto, IL_BYTE* In, IL_WORD wInLen, IL_BYTE* Out, IL_WORD* pwOutLen);

//  Description:
//      ���������� ������� ������������ APDU-������� ������.
//  See Also:
//				
//  Arguments:
//      phCrypto	- ��������� �� �������� ������ ������������.
//      ilApduElem	- ��������� �� ������� ����������� APDU-������� ������.
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ���������� APDU-������� ������.
IL_FUNC IL_RETCODE smRunApdu(IL_HANDLE_CRYPTO* phCrypto, IL_APDU_PACK_ELEM* ilApduElem);

//  Description:
//      ������ ������������ ��������� ������ ������������. 
//		��������� ������������ ��������� ������ ������������ �� ����� 3.
//		������������ ��������� ������ � ��������� Win-1251.
//  See Also:
//      
//  Arguments:
//      phCrypto		- ��������� �� �������� ������ ������������.
//		pOwnerNameOut	- ��������� �� ����� ��� ������������ ������ 
//  Return Value:
//      IL_RETCODE		- ��� ������.
//  Summary:
//      ������ ������������ ��������� ������ ������������.
//---IL_FUNC IL_RETCODE smGetOwnerName(IL_HANDLE_CRYPTO* phCrypto, IL_CHAR *pOwnerNameOut);

#endif