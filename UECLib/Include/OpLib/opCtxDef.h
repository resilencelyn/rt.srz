/**  ����������� ��������� ������� UEC
  */
#ifndef __OP_CTXDEF_H_
#define __OP_CTXDEF_H_

#ifdef __cplusplus
extern "C" {
#endif

#include "CardLib.h"
#include "FuncLib.h"
#include "opDescr.h"

	
#define UEC_MAX_STATES					32                  // ������������ ������� �������� ��������� 

#define UEC_TMPBUF_LEN					(1024*8)			// ������ ������ ���������� �������� ������
#define UEC_AUTHREQISSSESBUF_LEN		1024				// ������ ������ �������� ���������� ������� �� �������������� ��� ������������ ���������� ������ � ���������
#define BIN_TLV_DATA_LEN				(1024*5)			//+++ ������ ������ �������� TLV-������ �� ��������� �����

#define	MAX_EX_SECTORS					5
#define	MAX_EX_BLOCKS					20
#define	MAX_EX_DATAS					50

typedef struct 
{
	IL_BYTE *pMsg;
	IL_WORD MsgLen;
	IL_BYTE *pS;
	IL_WORD SLen;
} PROVIDER_AUTH_DATA;

/**  �������� �������
  *  ��������� �� ������ ���� ���������, ����� � ��������� �� ����� ��������� ��������� �������
  * � ����� ��������� ��-�� ������������ ������������ ����������� ������.
  * � ��������� ������ ���� ������ ���������� ������!
  */
typedef struct
{
    IL_WORD	State[UEC_MAX_STATES];		// ������ ���������� ��������� 
    IL_BYTE	Index;						// ������ �������� �������� 
    IL_WORD	InterruptEvent;				// ��� �������� �������-���������� 
    IL_WORD	wCntCycles;					// ������� ������� �������������� ������������ ��������� 

	IL_CHAR PAN[23];					// ������� ����� ����� (�����) 
	IL_CHAR	AppVersion[4];				// ������������ ������ ����� 
	IL_CHAR	EffectiveDate[7];			// ���� ������ ����� �������� ����� 
	IL_CHAR	ExpirationDate[7];			// ���� ��������� ����� �������� ����� ��� 

	IL_CARD_HANDLE* phCrd;				// ��������� ����-������  
	IL_WORD	OperationCode;				// ��� ����������� �������� (��. opDescr.h) 		
    IL_WORD	ResultCode;					// ��������� ���������� �������� (��. il_error.h) 

	IL_BYTE PinNum;						// ����� ��� 	
	IL_BYTE PinBlock[8];				// ���-���� ��� ����������� �� ����� 
	IL_BYTE PinTriesLeft;				// ���������� ���������� ������� ����� ���/��� 
	IL_CHAR *pNewPinStr;				// ��������� �� ������ � ����� �������
	IL_CHAR *pConfirmPinStr;			// ��������� �� ������ � �������������� �������
	IL_CHAR PassPhrase[PASS_PHRASE_MAX_LEN+1]; // ����� ��� ����� ������������ �����������
	IL_BYTE ifPassPhraseUsed;			// ������� ������������� ��������� ������������ �����������

	IL_BYTE *pMetaInfo;					// ��������� �� ������� ����� �������������� �� ������
	IL_WORD MetaInfoLen;				// ����� ��������������
	IL_BYTE MataInfoCrc;				// ����������� ����� ��������������

	IL_BYTE *pExtraData;				// ��������� �� ������� ����� �������������� ���������� ������
	IL_WORD ExtraDataLen;				// ����� �������������� ���������� ������
	IL_BYTE ExtraDataCrc;				// ����������� ����� �������������� ���������� ������

	IL_BYTE *pRequestHash;				// ��������� �� ������� ����� ��� XML-������� �������� ������
	IL_WORD RequestHashLen;				// ����� ��� XML-������� �������� ������
	IL_BYTE RequestHashCrc;				// ����������� ����� ��� XML-������� �������� ������

	IL_CHAR *pSectorsDescrBuf;			// ��������� �� ������� ����� ��� ��������� ��������� �� ����� ������� 
	IL_WORD *pSectorsDescrLen;			// ��������� �� ������������ ������ ������ ��������� ��������

	IL_CHAR *pCardDataDescr;			// ��������� �� ������-��������� ����������� � ����� ������
	IL_CHAR *pCardDataBuf;				// ��������� �� ������� ����� ��� ����������� � ����� ������
	IL_WORD *pCardDataLen;				// ��������� �� ������������ ������ ������ � ����� ��������� � ����� ������

	IL_CHAR *pBlockDescr;				// ��������� �� ������-��������� ������� � ����� ����������� � ����� ������
	IL_CHAR *pBlockDataBuf;				// ��������� �� ������� ����� ��� ����������� �� ����� ������ 
	IL_WORD *pBlockDataLen;				// ��������� �� ������������ ������ ������ � ����� ��������� �� ����� ������

	IL_BYTE *pPhotoBuf;					// ��������� �� ����� ��� ����������� � ����� ������ ����������
	IL_WORD *pPhotoLen;					// ��������� �� ������������ ������ ������ ���������� � ����� ��������� � ����� ������

	IL_BYTE ifAuthOnline;				// ����� �������������� ��-���������� (0-Offline, 1-Online)
	IL_BYTE *pAuthRequestBuf;			// ��������� �� ������� ����� ������� �� �������������� ��-����������
	IL_WORD *pAuthRequestLen;			// ��������� �� ������������ ������ ������ � ����� ��������������� ������� �� �������������� ��-����������
	IL_BYTE AuthRequestCrc;				// ����������� ����� ������� �� �������������� ��-����������

	IL_BYTE *pAuthResponseData;			// ��������� �� ����� � ������������ �������������� 
	IL_WORD AuthResponseLen;			// ����� ������ ��������������				
	IL_BYTE *pKeyCert;					// ��������� �� ������� ������������ ��� �������� ����������� ��������������
	IL_WORD KeyCertLen;					// ����� ������� ������������
	IL_WORD AuthResult;					// ��� �������������� ��-���������� 

	IL_BYTE *pDigitalSignBuf;			// ��������� �� ������� ����� ��� ��� ��������� �����
	IL_WORD *pDigitalSignLen;			// ��������� �� ������������ ������ ������ � ����� �������������� ���
	IL_BYTE *pDigitalSignCertChain;		// ��������� �� ������� ����� ��� ������� ������������ ����� �������� ��� ��������� �����
	IL_WORD *pDigitalSignCertChainLen;	// ��������� �� ������������ ������ ������ ������� ������������ � ����� �������

	IL_BYTE AuthRequestIssSessionBuf[1024];		// ����� ��� �������� ���������� ������� �� �������������� ��-���������� ��� ������������ ������ � ���������	
	IL_WORD AuthRequestIssSessionLen;			// ����������� ����� ��������������� ������ �������

	IL_APDU_PACK_ELEM *pApduPacket;				// ��������� �� ����� APDU-������� 
	IL_BYTE isApduEncryptedPS;					// ���� ������������� ����������/������������� ������ �� ���������� ����� ���������� ������
	IL_WORD *pApduPacketSize;					// ��������� �� ������ �������� ������/���������� ������� ����������� APDU-������ ������
	IL_WORD *pApduPacketResult;					// ��������� �� ��� �������� ���������� ������ APDU-�������
	IL_BYTE *pApduIn;							// ��������� �� ������� ������� ����� ������ APDU-������
	IL_WORD ApduInLen;							// ����� ������ ������ APDU-������
	IL_BYTE *pApduOut;							// ��������� �� �������� ����� � ������������ ���������� APDU-������ ������
	IL_WORD *ApduOutLen;						// ��������� �� ������������ ������ ������ � ����� ������������ ������ APDU-������ ������

	IL_WORD SectorIdAuth;						// ������������� ������ ��� �������������� ��������� 

	SECTOR_DESCR ExSectorDescr[MAX_EX_SECTORS];	// ������ ������� ���������� ��������
	IL_WORD ExSectorsNum;						// ���������� ���������� ������� ��������
	BLOCK_DESCR ExBlockDescr[MAX_EX_BLOCKS];	// ������ ������� ���������� ������
	IL_WORD ExBlocksNum;						// ���������� ���������� ������� ������
	DATA_DESCR ExDataDescr[MAX_EX_DATAS];		// ������ ������� ���������� ������
	IL_WORD ExDatasNum;							// ���������� ���������� ������� ������
	IL_CHAR *pExSectorDesr;						// ��������� �� ������-��������� ������������ �������� ������� 

	DATA_DESCR *pFirstEditDataDescr;			// ��������� �� ��������� ������� �������� ������������� ������ 
	DATA_DESCR *pFirstEditDataDescrCopy;		// ����� ��������� �� ��������� ������� �������� ������������� ������ 

	ISSUER_SESSION_DATA_IN  sess_in;			// ������ ����� ��� ��������� ���������� ������
	ISSUER_SESSION_DATA_OUT sess_out;			// ����������������� ������ ����� ��� ��������� ���������� ������
	CHECK_ISSUER_SESSION_DATA_IN chk_sess_in;	// ������ ��� �������� ������������� ���������� ������

	IL_BYTE isProviderSession;					// ���� ��������� ���������� ������ � ����������� ������
	IL_BYTE ifGostPS;							// ��� ������������� ��������������� ���������� ������ � ����������� �����
	IL_BYTE *pCSpId;							// ��������� �� ���������� �� ���������� �����
	IL_WORD CSpIdLen;							// ����� ����������� �� ���������� �����
	PROVIDER_SESSION_DATA ProviderSessionData;	// ��������� ������ � ����������� ����� 
	PROVIDER_SM_CONTEXT PSMContext;				// �������������� ���������� ������ � ����������� ����� 
	PROVIDER_AUTH_DATA ProviderAuthData;		// ������ ��� �������������� ���������� �����
	IL_BYTE *pClearData;						// ��������� �� ��������������� ������
	IL_DWORD *pClearDataLen;					// ��������� �� ����� ��������������� ������
	IL_BYTE *pEncryptedData;					// ��������� �� ������������� ������
	IL_DWORD *pEncryptedDataLen;				// ��������� �� ����� ��������������� ������

#ifdef SM_SUPPORT
	IL_BYTE SE_IfActivateOnline;				// ������� ���������\����������� �� � ������ Online
	IL_BYTE SE_IfGostIssSession;				// ������� ��������� ������������ � ��������� � ������ ����
	IL_BYTE *pSeOwnerName;						// ��������� �� ������� ����� � ������� ��������� ��
	IL_WORD SeOwnerNameLen;						// ����� ������ ��������� ��	
	SM_SESSION_DATA_IN SE_SessIn;				// ������ �� ��� ��������� ���������� ������
	SM_SESSION_DATA_OUT SE_SessOut;				// ����������������� ������ ����� ��� ��������� ���������� ������ c ��
#endif//SM_SUPPORT

	IL_BYTE TmpBuf[UEC_TMPBUF_LEN];				// ����� ��� ���������� �������� ������  	
	IL_WORD wTmp;								// ��������� WORD-����������
	IL_BYTE bTmp;								// ��������� BYTE-����������	

	BINTLV_DESCR BinTlvDescr;					// ��������� ������������ BinTlv-������	

#ifdef GUIDE
	void (*pDisplayText)(IL_CHAR*);				// ��������� �� ������� ������ ��������� ������ �� ������� 
#endif//GUIDE
} s_opContext; 


#ifdef __cplusplus
}
#endif

#endif//__OP_CTXDEF_H_
