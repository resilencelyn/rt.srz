#ifndef __OP_DESCR_H_
#define __OP_DESCR_H_

// ������������ ����� ������ �����
#define UEC_CARD_RESP_MAX_LEN			256	
// ������������ ����� ������������ ������
#define UEC_DATA_OUT_MAX_LEN			256		

// ���������������� ������ ���������� �������� 
#define UEC_SECTOR_TYPE					1
// ���������������� ������ ���������� ������ �������� �������
#define UEC_BLOCK_TYPE					0		

// ��� ����� ������ - TLV
#define BLOCK_DATA_TLV					0
// ��� ����� ������ - BIN
#define BLOCK_DATA_BIN					1
// ��� ����� ������ - RECORD
#define BLOCK_DATA_RECORD				2
// ��� ����� ������ - BINTLV
#define BLOCK_DATA_BINTLV				3

#define BLOCK_DATA_TYPE_MAX				BLOCK_DATA_BINTLV

#define MAX_TAG_IDS_DESCR				10 // ������������ ���������� ���������� �������������� �����

// ����� ���������� ����� BinTlv-������ - �������� �������� �������
#define BINTLV_DATA_UPDATE				0
// ����� ���������� ����� BinTlv-������ - �������� ������� 
#define BINTLV_DATA_ADD					1
// ����� ���������� ����� BinTlv-������ - ������� ������� 
#define BINTLV_DATA_DELETE				2
// ����� ���������� ����� BinTlv-������ - ������� �������� ������� 7F7F ����� ����������� ���� ��������� '9F7F' 
#define BINTLV_DATA_UPDATE_EX			3 


// ��� ������: 'a' - ���������� ���������� 
#define DATA_ASCII						0
// ��� ������: 'cn' - ����� ����� ��� �����, ����������� �� ������ ���� � ���������� ������� 'F'
#define DATA_NUMERIC					1	
// ��� ������: 'b' - �������� ������
#define DATA_BYTE						2	
// ��� ������: 'b' - ��� (1-��� 2-���)
#define DATA_ISO5218					3
// ��� ������: 'n' - ����� ����� ��� �����, ����������� �� ������� ���� � ���������� ����� '0'
#define DATA_NUMERIC2					4			

// ��������: �������������� ������ 
#define UEC_OP_PROVIDE_SERVICE			1
// ��������: ����� ��� 
#define UEC_OP_CHANGE_PIN				2	
// ��������: ������������� ��� 
#define UEC_OP_UNLOCK_PIN				3	
// ��������: ����� ��� 	
#define UEC_OP_CHANGE_PUK				4
// ��������: ������������� ���
#define UEC_OP_UNLOCK_PUK				5
// ��������: ����� ������������ �����������
#define UEC_OP_CHANGE_PASS_PHRASE		6
// ��������: ��������� ������� ������ ����������
#define UEC_OP_EDIT_PRIVATE_DATA		7	
// ��������: ��������� ������ ���������
#define UEC_OP_EDIT_OPERATOR_DATA		8
// ��������: �������� ���������� ��������� ��-����������
#define UEC_OP_REM_MANAGE_IDAPP_DATA	9
// ��������: �������� ���������� ��������� �����
#define UEC_OP_REM_MANAGE_CARD_DATA	    10
// ��������: ���������� ��������� �������� ������� ���������� ������
#define UEC_OP_ADD_SECTOR_EX_DESCR		11	
// ��������: ��������� ������ ������������ (��)
#define UEC_OP_SE_ACTIVATE				12
// ��������: ����������� ������ ������������ (��)
#define UEC_OP_SE_DEACTIVATE			13
// ��������: ����� ��� ������ ������������ (��)
#define UEC_OP_SE_CHANGE_PIN			14
// ��������: ������������� ��� ������ ������������ (��)
#define UEC_OP_SE_UNLOCK_PIN			15
// ��������: ����� ��� ������ ������������ (��)
#define UEC_OP_SE_CHANGE_PUK			16	
// ��������: �������� ���������� ��������� ������ ������������ (��)
#define UEC_OP_SE_REM_MANAGE			17

#define UEC_OP_END						18

#define ICON_MAX_LEN					51
// ��������� �������
typedef struct
{
	IL_WORD	Id;					// ������������� �������, �������� ����������� ����
	IL_CHAR Icon[ICON_MAX_LEN];	// ������������ �������
} SECTOR_DESCR;

// ��������� �����
typedef struct
{
	IL_WORD		SectorId;			// ������������� �������, �������� ����������� ����
	IL_WORD		Id;					// ������������� �����
	IL_BYTE		FileType;			// ������������� ���� ����� ������ 
	IL_DWORD	RootTag;			// �������� ��� (��� ����� ������ BLOCK_DATA_BINTLV)	
	IL_CHAR		Icon[ICON_MAX_LEN];	// ������������ �����
} BLOCK_DESCR;

// ��������� �������� ������
#define TPATH_MAX_LEN					3
typedef struct
{
	IL_WORD	 SectorId;				// ������������� �������
	IL_WORD	 BlockId;				// ������������� ����� ������
	IL_WORD	 TagId;					// ������������� ����/������� 
	IL_WORD	 Type;					// ��� (DATA_ASCII|DATA_NUMERIC|DATA_BYTE|DATA_NUMERIC2)
	IL_WORD	 MaxLen;				// ������������ ����� �� �����
	IL_WORD	 TPath[TPATH_MAX_LEN];	// ���� �� ��������� ���� �� ���� �������
	IL_BYTE  isMust;				// ���� �������������� ������� ��������
	IL_CHAR  Name[ICON_MAX_LEN];	// ������������
} DATA_DESCR;

// ��������� ������������ BinTlv-������ 
typedef struct
{
	IL_WORD	 SectorId;				// ������������� �������
	IL_WORD	 BlockId;				// ������������� ����� ������
	IL_BYTE  *pData;				// ��������� �� ����� ��� �������� ��������� � ����� �������� TLV-������
	IL_WORD  DataLen;				// ����������� ����� ��������� � ����� �������� TLV-������
} BINTLV_DESCR;

#endif//__OP_DESCR_H_

