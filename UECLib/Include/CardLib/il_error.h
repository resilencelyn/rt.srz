#ifndef _IL_ERROR_H_
#define _IL_ERROR_H_

// �������� ����������
#define ILRET_OK							0

// ��������� ������
#define ILRET_SYS_ERROR						200
// ������ ������������� ��������� ������
#define ILRET_SYS_MEM_ALLOC_ERROR			201
// �������� �������� ��� ������ �������
#define ILRET_SYS_INVALID_ARGUMENT			202

// ������ ����-������
#define ILRET_SCR_ERROR						300
// ����������� ������� �� �����
#define ILRET_SCR_UNPOWERED_CARD			ILRET_SCR_ERROR+1
// ����� ����������� � ����-������
#define ILRET_SCR_REMOVED_CARD				ILRET_SCR_ERROR+2
// ��������� RESET �����
#define ILRET_SCR_RESET_CARD				ILRET_SCR_ERROR+3
// ����� �� �������� �� RESET
#define ILRET_SCR_UNRESPONSIVE_CARD			ILRET_SCR_ERROR+4
// ������ ��������� �����
#define ILRET_SCR_PROTOCOL_ERROR			ILRET_SCR_ERROR+5
// ������ ������������ ������� � ����-������
#define ILRET_SCR_SHARING_VIOLATION			ILRET_SCR_ERROR+6
// ����������� ����-�����
#define ILRET_SCR_UNKNOWN_READER			ILRET_SCR_ERROR+7
// ����-����� �� �����
#define ILRET_SCR_NOT_READY					ILRET_SCR_ERROR+8
// ������ �������� APDU ������ �� ��������������
#define ILRET_SCR_PROTO_MISMATCH			ILRET_SCR_ERROR+9
// ����-����� �� ������������ ������������ � ������
#define ILRET_SCR_UNSUPPORTED_CARD			ILRET_SCR_ERROR+10
// �������� ATR �����
#define ILRET_SCR_INVALID_ATR				ILRET_SCR_ERROR+11
// ������������ ������������� ���������� ����-������
#define ILRET_SCR_INVALID_HANDLE			ILRET_SCR_ERROR+12
// �������� �����
#define ILRET_SCR_INVALID_DEVICE			ILRET_SCR_ERROR+13
// ��������� ����� �������� ������ ����-������
#define ILRET_SCR_TIMEOUT					ILRET_SCR_ERROR+14
// ����-����� �� ��������
#define ILRET_SCR_READER_UNAVAILABLE		ILRET_SCR_ERROR+15

// ������ �����
#define ILRET_CRD_ERROR						ILRET_SCR_ERROR+50
// �������� ����� ������ �����
#define ILRET_CRD_LENGTH_NOT_MATCH			ILRET_SCR_ERROR+51
// �� ������ ��� � ������������ � ������ ����� ������
#define ILRET_CRD_APDU_TAG_NOT_FOUND		ILRET_SCR_ERROR+52
// �������� ����� ���� ������������ � ������ ����� ������
#define ILRET_CRD_APDU_TAG_LEN_ERROR		ILRET_SCR_ERROR+53
// �������� ������ ������ APDU-�������
#define ILRET_CRD_APDU_DATA_FORMAT_ERROR	ILRET_SCR_ERROR+54

// ������ ����������� ����������
#define ILRET_CRD_VERIFY_ERROR		  		ILRET_SCR_ERROR+60
// ������������ ����� �������� ������ ����������� ����������
#define ILRET_CRD_VERIFY_WRONG_LENGTH  		ILRET_SCR_ERROR+61
// ������������ �������� ���������� P1/P2 ������� ����������� ����������
#define ILRET_CRD_VERIFY_WRONG_PARAMETERS	ILRET_SCR_ERROR+62
// ������ ������������
#define ILRET_CRD_VERIFY_PASSWORD_BLOCKED	ILRET_SCR_ERROR+63
// ��������� � ������� ����������� ���������� ���� �� ������
#define ILRET_CRD_VERIFY_PASSWORD_NOT_FOUND	ILRET_SCR_ERROR+64
// �������� �������� ������
#define ILRET_CRD_VERIFY_WRONG_PASSWORD_PRESENTED	ILRET_SCR_ERROR+65

// ������ �������������� ����������/�����
#define ILRET_CRD_SELECT_ERROR		  		ILRET_SCR_ERROR+70
// ������������ ����� ������� ������ ������� ��������������
#define ILRET_CRD_SELECT_WRONG_LENGTH  		ILRET_SCR_ERROR+71
// ������������ �������� ���������� P1/P2 ������� ��������������
#define ILRET_CRD_SELECT_WRONG_PARAMETERS	ILRET_SCR_ERROR+72
// ��������� ������ ����������
#define ILRET_CRD_SELECT_OBJECT_BLOCKED		ILRET_SCR_ERROR+73
// ������� ������ ������� �������������� ����� ������������ ��������
#define ILRET_CRD_SELECT_WRONG_CMD_DATA		ILRET_SCR_ERROR+74
// ������������� ������ �� ������
#define ILRET_CRD_SELECT_FILE_NOT_FOUND		ILRET_SCR_ERROR+75
// ����������� ���������� � ��������������� �������
#define ILRET_CRD_SELECT_RESPONSE_ABSENT	ILRET_SCR_ERROR+76
// ������ � ��������� ��������������� ����������� � ������ ��������
#define ILRET_CRD_SELECT_SECTOR_NOT_FOUND	ILRET_SCR_ERROR+77
// ���� � ��������� ��������������� ����������� � ������ ������ �������
#define ILRET_CRD_SELECT_BLOCK_NOT_FOUND	ILRET_SCR_ERROR+78

// ������ �������������� ��-����������
#define ILRET_CRD_INTAUTH_ERROR		  		ILRET_SCR_ERROR+80
// ������������ ����� ������� ������ ������� �������������� ��-����������
#define ILRET_CRD_INTAUTH_WRONG_LENGTH  	ILRET_SCR_ERROR+81
// ������������ �������� ���������� P1/P2 ������� �������������� ��-����������
#define ILRET_CRD_INTAUTH_WRONG_PARAMETERS	ILRET_SCR_ERROR+82
// ������� ������ ������� �������������� ��-���������� ����� ������������ ��������
#define ILRET_CRD_INTAUTH_WRONG_CMD_DATA	ILRET_SCR_ERROR+84
// ��������� � ������� �������������� ��-���������� ���� �� ������
#define ILRET_CRD_INTAUTH_KEY_NOT_FOUND		ILRET_SCR_ERROR+85

// ������ �������������� �������� ��������
#define ILRET_CRD_MUTAUTH_ERROR		  		ILRET_SCR_ERROR+90
// ������������ ����� ������� ������ ������� �������������� �������� ��������
#define ILRET_CRD_MUTAUTH_WRONG_LENGTH 		ILRET_SCR_ERROR+91
// ������������ �������� ���������� P1/P2 ������� �������������� �������� ��������
#define ILRET_CRD_MUTAUTH_WRONG_PARAMETERS	ILRET_SCR_ERROR+92
// �������� �������� ������������ �������������� �������� ��������
#define ILRET_CRD_MUTAUTH_WRONG_CRYPTO		ILRET_SCR_ERROR+93
// ��������� � ������� �������������� �������� �������� ���� �� ������
#define ILRET_CRD_MUTAUTH_KEY_NOT_FOUND		ILRET_SCR_ERROR+94
// ���������� �� ������������ � ��������� ������� �������������� �������� ��������
#define ILRET_CRD_MUTAUTH_CONDITIONS		ILRET_SCR_ERROR+95

// ������ ��������� ���������� ����� �����
#define ILRET_CRD_GETCHAL_ERROR		  		ILRET_SCR_ERROR+100
// ������������ ����� ������� ������ ������� ��������� ���������� ����� �����
#define ILRET_CRD_GETCHAL_WRONG_LENGTH 		ILRET_SCR_ERROR+101
// ������������ �������� ���������� P1/P2 ������� ��������� ���������� ����� �����
#define ILRET_CRD_GETCHAL_WRONG_PARAMETERS	ILRET_SCR_ERROR+102

// ������ ���������/��������� �������� ����������� ������
#define ILRET_CRD_CHDATA_ERROR		  		ILRET_SCR_ERROR+110
// ������������ ����� ������� ������ ������� ���������/��������� �������� ����������� ������
#define ILRET_CRD_CHDATA_WRONG_LENGTH 		ILRET_SCR_ERROR+111
// �� ��������� ������� ������������ ��� ������� ���������/��������� �������� ����������� ������
#define ILRET_CRD_CHDATA_WRONG_CRYPTO		ILRET_SCR_ERROR+112
// �������� ��������� ������ ������� ���������/��������� �������� ����������� ������
#define ILRET_CRD_CHDATA_WRONG_DATA_STRUCT	ILRET_SCR_ERROR+113
// �������� �������� ������������ ������� ���������/��������� �������� ����������� ������
#define ILRET_CRD_CHDATA_WRONG_SM_TAG		ILRET_SCR_ERROR+114
// ������������ �������� ���������� P1/P2 ������� ���������/��������� �������� ����������� ������
#define ILRET_CRD_CHDATA_WRONG_PARAMETERS	ILRET_SCR_ERROR+115
// ��������� � ������� ���������/��������� �������� ����������� ������ ���� �� ������
#define ILRET_CRD_CHDATA_KEY_NOT_FOUND		ILRET_SCR_ERROR+116
// ����� ���������������� ������ ������ ����������
#define ILRET_CRD_CHDATA_DATA_LEN_TOO_SHORT	ILRET_SCR_ERROR+117

// ������ ������������� ������
#define ILRET_CRD_RSTCNTR_ERROR		  		ILRET_SCR_ERROR+120
// ������������ ����� ������� ������ ������� ������������� ������
#define ILRET_CRD_RSTCNTR_WRONG_LENGTH 		ILRET_SCR_ERROR+121
// �������� ��������� ������ ������� ������������� ������
#define ILRET_CRD_RSTCNTR_WRONG_DATA_STRUCT	ILRET_SCR_ERROR+123
// �������� �������� ������ �� ����� SM ������� ������������� ������
#define ILRET_CRD_RSTCNTR_WRONG_SM_TAG		ILRET_SCR_ERROR+124
// ������������ �������� ���������� P1/P2 ������� ������������� ������
#define ILRET_CRD_RSTCNTR_WRONG_PARAMETERS	ILRET_SCR_ERROR+125
// ��������� � ������� ������������� ������ ���� �� ������
#define ILRET_CRD_RSTCNTR_KEY_NOT_FOUND		ILRET_SCR_ERROR+126

// ������ ���������� ����������������� ��������
#define ILRET_CRD_PERFSECOP_ERROR			ILRET_SCR_ERROR+130
// ������������ ����� ������� ������ ������� ���������� ����������������� ��������
#define ILRET_CRD_PERFSECOP_WRONG_LENGTH	ILRET_SCR_ERROR+131
// ������ �������� ��������� ��������� ������� ���������� ����������������� ��������
#define ILRET_CRD_PERFSECOP_BINDING_CMD_MISSED	ILRET_SCR_ERROR+132
// ������� ����������������� �������� �� ������������ ����������
#define ILRET_CRD_PERFSECOP_BINDING_NOT_SUPPORTED	ILRET_SCR_ERROR+133
// �������� ���������� ��������� ����� ������� ���������� ����������������� ��������
#define ILRET_CRD_PERFSECOP_WRONG_CERT	ILRET_SCR_ERROR+134
// �������� ��������� ������ ������� ���������� ����������������� ��������
#define ILRET_CRD_PERFSECOP_WRONG_DATA_STRUCT	ILRET_SCR_ERROR+135
// ������������ �������� ���������� P1/P2 ������� ���������� ����������������� ��������
#define ILRET_CRD_PERFSECOP_WRONG_PARAMETERS	ILRET_SCR_ERROR+136

// ������ ������ ������ �� ��������� �����
#define ILRET_CRD_READBIN_ERROR				ILRET_SCR_ERROR+140
// ������������ ����� ������� ������ ������� ������ ��������� �����
#define ILRET_CRD_READBIN_WRONG_LENGTH		ILRET_SCR_ERROR+141
// ������� ������ ������ �� �� ��������� �����
#define ILRET_CRD_READBIN_WRONG_FILE_TYPE	ILRET_SCR_ERROR+142
// �� ��������� ������� ������� ��� ������ ������ �� ��������� �����
#define ILRET_CRD_READBIN_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+143
// ������� ������ �������� ������ �� ������������������ �����
#define ILRET_CRD_READBIN_EF_NOT_SELECTED	ILRET_SCR_ERROR+144
// ������������ �������� ���������� P1/P2 ������� ������ ������ �� ��������� �����
#define ILRET_CRD_READBIN_WRONG_PARAMETERS	ILRET_SCR_ERROR+145
// ��������� ��� ������ ������ �� ��������� ����� �������� ��������� ������ �����
#define ILRET_CRD_READBIN_WRONG_OFFSET		ILRET_SCR_ERROR+146

// ������ ������ ������ � �������� ����
#define ILRET_CRD_UPDBIN_ERROR				ILRET_SCR_ERROR+150
// ������������ ����� ������� ������ ������� ������ � �������� ����
#define ILRET_CRD_UPDBIN_WRONG_LENGTH		ILRET_SCR_ERROR+151
// �� ��������� ������� ������� ��� ������ ������ � �������� ����
#define ILRET_CRD_UPDBIN_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+152
// ������� ������ �������� ������ � ����������������� ����
#define ILRET_CRD_UPDBIN_WRONG_FILE			ILRET_SCR_ERROR+153
// ������������ �������� ���������� P1/P2 ������� ������ ������ � �������� ����
#define ILRET_CRD_UPDBIN_WRONG_PARAMETERS	ILRET_SCR_ERROR+154
// �������� �������� ��� ������� ������ ������ � �������� ����
#define ILRET_CRD_UPDBIN_WRONG_OFFSET		ILRET_SCR_ERROR+155

// ������ ������ �������� ������ �� TLV-�����
#define ILRET_CRD_GETDATA_ERROR				ILRET_SCR_ERROR+160
// ������������ ����� �������/�������� ������ ��� ������ �� TLV-�����
#define ILRET_CRD_GETDATA_WRONG_LENGTH		ILRET_SCR_ERROR+161
// ������������ �������� ���������� P1/P2 ������� ������ �������� ������ �� TLV-�����
#define ILRET_CRD_GETDATA_WRONG_PARAMETERS	ILRET_SCR_ERROR+162
// �� ������ ������� ������ � ��������� ����� ��� ������ �� TLV-�����
#define ILRET_CRD_GETDATA_TAG_NOT_FOUND		ILRET_SCR_ERROR+163
// �� ��������� ������� ������� ��� ������ �������� ������ �� TLV-�����
#define ILRET_CRD_GETDATA_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+164

// ������ ��������� �������� ������ � TLV-����
#define ILRET_CRD_PUTDATA_ERROR				ILRET_SCR_ERROR+170
// ������������ ����� ������� ������ ������� ��������� �������� ������ � TLV-����
#define ILRET_CRD_PUTDATA_WRONG_LENGTH		ILRET_SCR_ERROR+171
// �� ��������� ������� ������� ��� ��������� �������� ������ � TLV-����
#define ILRET_CRD_PUTDATA_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+172
// ������������ �������� ���������� P1/P2 ������� ��������� �������� ������ � TLV-����
#define ILRET_CRD_PUTDATA_WRONG_PARAMETERS	ILRET_SCR_ERROR+173
// ����������� ��� ��� ��������� �������� ������ � TLV-����
#define ILRET_CRD_PUTDATA_TAG_NOT_FOUND		ILRET_SCR_ERROR+174

// ������ ������ ������ �� ����� ������� �������� ���������
#define ILRET_CRD_READREC_ERROR	            ILRET_SCR_ERROR+180
// ���� ������� �������� ��������� ����������
#define ILRET_CRD_READREC_FILE_BLOCKED  	ILRET_SCR_ERROR+181
// ����������� ����� �������� ������ ����� ������� �������� ���������
#define ILRET_CRD_READREC_WRONG_LENGTH		ILRET_SCR_ERROR+182
// ���� �� �������� ������ ������� �������� ���������
#define ILRET_CRD_READREC_WRONG_FILE_TYPE	ILRET_SCR_ERROR+183
// �� ��������� ������� ������� � ����� ������� �������� ���������
#define ILRET_CRD_READREC_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+184
// ������������ �������� ���������� ������� ������ ����� ������� �������� ���������
#define ILRET_CRD_READREC_WRONG_PARAMETERS	ILRET_SCR_ERROR+185
// �� ������� ������ ��� ������ ����� ������� �������� ���������
#define ILRET_CRD_READREC_RECORD_NOT_FOUND	ILRET_SCR_ERROR+186
// ������������ �������� ���������� P1/P2 ��� ������ ����� ������� �������� ���������
#define ILRET_CRD_READREC_WRONG_PARAMETERS_P1P2	ILRET_SCR_ERROR+187

// ������ ���������� ������ ����� ������� �������� ���������
#define ILRET_CRD_UPDREC_ERROR	            ILRET_SCR_ERROR+190
// ���� ������� �������� ��������� ����������
#define ILRET_CRD_UPDREC_FILE_BLOCKED  	    ILRET_SCR_ERROR+191
// ����������� ����� ������ ��� ���������� ����� ������� �������� ���������
#define ILRET_CRD_UPDREC_WRONG_LENGTH		ILRET_SCR_ERROR+192
// ���� �� �������� ������ ������� �������� ���������
#define ILRET_CRD_UPDREC_WRONG_FILE_TYPE	ILRET_SCR_ERROR+193
// �� ��������� ������� ������� � ����� ������� �������� ���������
#define ILRET_CRD_UPDREC_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+194
// ������������ �������� ���������� ������� ���������� ����� ������� �������� ���������
#define ILRET_CRD_UPDREC_WRONG_PARAMETERS	ILRET_SCR_ERROR+195
// �� ������� ������ ��� ���������� ����� ������� �������� ���������
#define ILRET_CRD_UPDREC_RECORD_NOT_FOUND	ILRET_SCR_ERROR+196
// ������������ �������� ���������� P1/P2 ��� ���������� ����� ������� �������� ���������
#define ILRET_CRD_UPDREC_WRONG_PARAMETERS_P1P2	ILRET_SCR_ERROR+197

// ������ ���������� ������ � ���� ������� �������� ���������
#define ILRET_CRD_APPREC_ERROR	            ILRET_SCR_ERROR+200
// ���� ������� �������� ��������� ����������
#define ILRET_CRD_APPREC_FILE_BLOCKED  	    ILRET_SCR_ERROR+201
// ����������� ����� ������ ��� ���������� � ����� ������� �������� ���������
#define ILRET_CRD_APPREC_WRONG_LENGTH		ILRET_SCR_ERROR+202
// ���� �� �������� ������ ������� �������� ���������
#define ILRET_CRD_APPREC_WRONG_FILE_TYPE	ILRET_SCR_ERROR+203
// �� ��������� ������� ������� � ����� ������� �������� ���������
#define ILRET_CRD_APPREC_WRONG_SEC_CONDITIONS	ILRET_SCR_ERROR+204
// ������������ �������� ���������� ������� ���������� � ���� ������� �������� ���������
#define ILRET_CRD_APPREC_WRONG_PARAMETERS	ILRET_SCR_ERROR+205
// ������������ ����� � ����� ������� �������� ��������� ��� ����������
#define ILRET_CRD_APPREC_NOT_ENOUGH_MEMORY	ILRET_SCR_ERROR+206
// ������������ �������� ���������� P1/P2 ��� ���������� � ����� ������� �������� ���������
#define ILRET_CRD_APPREC_WRONG_PARAMETERS_P1P2	ILRET_SCR_ERROR+207


// ������ ��������� ��������� �� �������� ����� ��������
#define ILRET_PARAM_ERROR					700
// �� ������ ������������� �������� �������� ����� ��������
#define ILRET_PARAM_NOT_FOUND				ILRET_PARAM_ERROR+1
// �������� ������ ��������� �������� ����� ��������
#define ILRET_PARAM_WRONG_FORMAT			ILRET_PARAM_ERROR+2
// �������� ����� ���������� �� �������� ����� �������� ���������
#define ILRET_PARAM_WRONG_LENGTH			ILRET_PARAM_ERROR+3
// ����������� ������ ��������� �������� ����� ��������
#define ILRET_PARAM_FORMAT_UNKNOWN			ILRET_PARAM_ERROR+4
// �� ������ ��������� ��������� �������� ����� ��������
#define ILRET_PARAM_DESCR_NOT_FOUND		    ILRET_PARAM_ERROR+5
// ������ ������ ������ �������� ��������� �������
#define ILRET_PARAM_WRITE_SECTOR_EX_ERROR	ILRET_PARAM_ERROR+6
// �������� ������ ������ �������� ��������� �������
#define ILRET_PARAM_SECTOR_EX_WRONG_FORMAT	ILRET_PARAM_ERROR+7	
// �� ������ ���������� �� ������� ����� ��������
#define ILRET_PARAM_CERTIFICATE_NOT_FOUND	ILRET_PARAM_ERROR+8

// ������ ����������������
#define ILRET_PROT_ERROR					710
// ������ �������� ����� ����������������
#define ILRET_PROT_LOGFILE_OPEN_ERROR		ILRET_PROT_ERROR+1
// ������ ������ � ���� ����������������
#define ILRET_PROT_LOGEILE_WRITE_ERROR		ILRET_PROT_ERROR+2

// ������ ������� ������ ���������� �������
#define ILRET_DATA_ERROR					800
// �� ������ ������� ������ � ��������� �����
#define ILRET_DATA_TAG_NOT_FOUND			ILRET_DATA_ERROR+1
// �������� ������ �������� ������
#define ILRET_DATA_TAG_WRONG_FORMAT			ILRET_DATA_ERROR+2
// �������� ����� �������� ������
#define ILRET_DATA_TAG_WRONG_LENGTH			ILRET_DATA_ERROR+3
// �������� ������ ���������� � ����� �������� TLV-������
#define ILRET_DATA_CARD_FORMAT_ERROR		ILRET_DATA_ERROR+4

// �������� �� ������������ ������ � ������ ������ ������
#define ILRET_APP_VER_NOT_SUPPORTED			ILRET_DATA_ERROR+5
// �������� �� ������������ ��������������� ������ �����
#define ILRET_NO_CRYPTOALG_SUPPORTED		ILRET_DATA_ERROR+6
// ���� ������ �������� ����� �� ��������
#define ILRET_APP_NOT_ACTIVE_YET		    ILRET_DATA_ERROR+7
// ���� �������� ����� ����
#define ILRET_APP_EXPIRED		            ILRET_DATA_ERROR+8
// �������� ������ Hex-������
#define ILRET_INVALID_HEX_STRING_FORMAT		ILRET_DATA_ERROR+9
// ������������ ����� APDU-������� ������
#define ILRET_APDU_RES_NOT_ALLOWED			ILRET_DATA_ERROR+10
// ��������� ���������� ���������� ������� APDU-������� ������
#define ILRET_APDU_ALLOWED_RES_IS_OVER		ILRET_DATA_ERROR+11
// ���������� ��������� � ��������������� ���������
#define ILRET_APP_INCONSISTENT_STATE		ILRET_DATA_ERROR+12
// ���������� ��������� � ����������� ���������
#define ILRET_APP_UNKNOWN_STATE				ILRET_DATA_ERROR+13
// ������������� ������ ������
#define ILRET_BUFFER_TOO_SMALL				ILRET_DATA_ERROR+14

// ������ �������� ������� �����������
#define ILRET_CERT_ERROR					ILRET_DATA_ERROR+20
// ����������� ������������ ������� ������ �����������
#define ILRET_CERT_MISSING_ELEMENT			ILRET_CERT_ERROR+1
// �������� ����� ������ �������� �����������
#define ILRET_CERT_WRONG_LENGTH				ILRET_CERT_ERROR+2
// ���� ������ �������� ����������� �� ��������
#define ILRET_CERT_NOT_ACTIVE_YET		    ILRET_CERT_ERROR+3
// ���� �������� ����������� ����
#define ILRET_CERT_EXPIRED		            ILRET_CERT_ERROR+4
// �������� ������ �����������
#define ILRET_CERT_WRONG_VERSION			ILRET_CERT_ERROR+5
// �������� ���������� ��������� ����� � �����������
#define ILRET_CERT_WRONG_RSA_EXP			ILRET_CERT_ERROR+6
// �������� ��� ����������� ��������� �����
#define ILRET_CERT_INVALID_TYPE				ILRET_CERT_ERROR+7
// �� ��������� �������� � ��������� � ������������ GOST � RSA
#define ILRET_CERT_TERMINFO_NOT_MATCH		ILRET_CERT_ERROR+8

// ������ ����������������
#define ILRET_CRYPTO_ERROR					900
// ������ ������� RSA
#define ILRET_CRYPTO_RSA_FORMAT				ILRET_CRYPTO_ERROR+1
// ������ ��� ���������� ������������
#define ILRET_CRYPTO_CRYPTO_PREPARE_SESSION	ILRET_CRYPTO_ERROR+2
// �������� �������� ����� MAC ������������
#define ILRET_CRYPTO_WRONG_SM_MAC			ILRET_CRYPTO_ERROR+3
// �������� ����� ����������������� ������
#define ILRET_CRYPTO_WRONG_DATA_LENGTH		ILRET_CRYPTO_ERROR+4
// �������� ������ ����������������� ������
#define ILRET_CRYPTO_WRONG_DATA_FORMAT		ILRET_CRYPTO_ERROR+5
// �������� ����������
#define ILRET_CRYPTO_WRONG_CERT				ILRET_CRYPTO_ERROR+6
// �������� �������� ����� MAC ��� �������� ������� �� �������������� ��-����������
#define ILRET_CRYPTO_WRONG_MAC			    ILRET_CRYPTO_ERROR+7
// �������� �������� ����� MAC ��� �������� ������������� ���������� ������ � ���������
#define ILRET_CRYPTO_WRONG_CHK_ISS_SESS_MAC ILRET_CRYPTO_ERROR+8
// ������ ��������� �������� ����
#define ILRET_CRYPTO_ERROR_GENKEYPAIR		ILRET_CRYPTO_ERROR+9
// ������ �������� ����������� �������� ���������
#define ILRET_CRYPTO_ERROR_FILLPARAM		ILRET_CRYPTO_ERROR+10
// ������ ������������ �������
#define ILRET_CRYPTO_ERROR_SIGN         	ILRET_CRYPTO_ERROR+11
// ������ �������� �������
#define ILRET_CRYPTO_ERROR_CHECKSIGN		ILRET_CRYPTO_ERROR+12
// ������������ ���������� ������
#define ILRET_CRYPTO_ERROR_KEYMATCHING		ILRET_CRYPTO_ERROR+13

// ������ OpLib
#define ILRET_OPLIB_ERROR							4000
// ����� ������������ �� ���������� ��������
#define ILRET_OPLIB_ESCAPE							ILRET_OPLIB_ERROR + 0
// ������������ �������� �������
#define ILRET_OPLIB_INVALID_ARGUMENT				ILRET_OPLIB_ERROR + 1 
// �������� ����� �������������� �� ������
#define ILRET_OPLIB_METAINFO_WRONG_LEN				ILRET_OPLIB_ERROR + 2 
// ��������� ����� ������� ������ ������ 
#define ILRET_OPLIB_EXTRA_DATA_IS_OVER				ILRET_OPLIB_ERROR + 3 
// �� ������ ��������� ������� � ������ �������� 
#define ILRET_OPLIB_SECTOR_NOT_FOUND_IN_LIST		ILRET_OPLIB_ERROR + 4 
// �� ������ ��������� �������� ������
#define ILRET_OPLIB_DATA_DESCR_NOT_FOUND			ILRET_OPLIB_ERROR + 5
// �� ������ ��������� ����� ������
#define ILRET_OPLIB_BLOCK_DESCR_NOT_FOUND			ILRET_OPLIB_ERROR + 6
// �� ������ ��������� ������� ������
#define ILRET_OPLIB_SECTOR_DESCR_NOT_FOUND			ILRET_OPLIB_ERROR + 7
// �� ������ ���� ������
#define ILRET_OPLIB_BLOCK_NOT_FOUND					ILRET_OPLIB_ERROR + 8
// �������� ��� ������
#define ILRET_OPLIB_ILLEGAL_DATA_TYPE				ILRET_OPLIB_ERROR + 9
// ������ ������� ������ TLV-��������
#define ILRET_OPLIB_CORRUPTED_TLV_DATA				ILRET_OPLIB_ERROR + 10
// �������� ������ �������������
#define ILRET_OPLIB_INVALID_CONFIRM_PIN				ILRET_OPLIB_ERROR + 11
#define PIN_TRIES_MAX								16
// �������� ������! �������� �������: 1
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_1		(ILRET_OPLIB_ERROR + 12)
// �������� ������! �������� �������: 2
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_2		ILRET_OPLIB_ERROR + 13
// �������� ������! �������� �������: 3
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_3		ILRET_OPLIB_ERROR + 14
// �������� ������! �������� �������: 4
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_4		ILRET_OPLIB_ERROR + 15
// �������� ������! �������� �������: 5
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_5		ILRET_OPLIB_ERROR + 16
// �������� ������! �������� �������: 6
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_6		ILRET_OPLIB_ERROR + 17
// �������� ������! �������� �������: 7
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_7		ILRET_OPLIB_ERROR + 18
// �������� ������! �������� �������: 8
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_8		ILRET_OPLIB_ERROR + 19
// �������� ������! �������� �������: 9
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_9		ILRET_OPLIB_ERROR + 20
// �������� ������! �������� �������: 10
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_10		ILRET_OPLIB_ERROR + 21
// �������� ������! �������� �������: 11
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_11		ILRET_OPLIB_ERROR + 22
// �������� ������! �������� �������: 12
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_12		ILRET_OPLIB_ERROR + 23
// �������� ������! �������� �������: 13
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_13		ILRET_OPLIB_ERROR + 24
// �������� ������! �������� �������: 14
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_14		ILRET_OPLIB_ERROR + 25
// �������� ������! �������� �������: 15
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_15		ILRET_OPLIB_ERROR + 26
// �������� ������! �������� �������: 16
#define ILRET_OPLIB_ILLEGAL_PIN_TRIES_LEFT_16		ILRET_OPLIB_ERROR + 27
// ����������� ����� APDU-������
#define ILRET_OPLIB_APDU_PACKET_ABSENT				ILRET_OPLIB_ERROR + 28
// ������ ������������� ������� ����������������
#define ILRET_OPLIB_INIT_PROTOCOL_ERROR				ILRET_OPLIB_ERROR + 29
// ����������� ������ ��� ������ �� �����
#define ILRET_OPLIB_DATA_FOR_WRITE_NOT_FOUND		ILRET_OPLIB_ERROR + 30
// �������� ��� ������
#define ILRET_OPLIB_INVALID_WRITE_DATA_TYPE			ILRET_OPLIB_ERROR + 31
// �������� ������ ������������� ������
#define ILRET_OPLIB_INVALID_EDIT_DATA_INDEX			ILRET_OPLIB_ERROR + 32
// �� ����� ����������� ������� ������
#define ILRET_OPLIB_SECTOR_LIST_IS_EMPTY			ILRET_OPLIB_ERROR + 33
// ������ �������� ������������ ��������������
#define ILRET_OPLIB_APP_AUTH_WRONG_CRYPTOGRAM		ILRET_OPLIB_ERROR + 34
// �������� ��� �����
#define ILRET_OPLIB_INVALID_FILE_TYPE				ILRET_OPLIB_ERROR + 35
// ����� ������ ��� ������ �� ����� ��������� ������ ������
#define ILRET_OPLIB_WRITE_DATA_LEN_IS_OVER			ILRET_OPLIB_ERROR + 36
// ������ ���������� �����
#define ILRET_OPLIB_CARD_LOCK_ERROR					ILRET_OPLIB_ERROR + 37
// ����� �������������
#define ILRET_OPLIB_CARD_LOCKED						ILRET_OPLIB_ERROR + 38
// ����� ������
#define ILRET_OPLIB_CARD_CAPTURED					ILRET_OPLIB_ERROR + 39	
// �������� ������ ���������� ������ ���������
#define ILRET_OPLIB_CTX_TMP_BUF_IS_OVER				ILRET_OPLIB_ERROR + 40	
// ������� �� ������������ ��� ���������� ������ ��������
#define ILRET_OPLIB_INVALID_OPERATION				ILRET_OPLIB_ERROR + 41
// �������� ������ ������ ������� ���������� ��������
#define ILRET_OPLIB_SECTORS_EX_DESCR_IS_OVER		ILRET_OPLIB_ERROR + 42
// �������� ������ ������ ������� ���������� ������
#define ILRET_OPLIB_BLOCKS_EX_DESCR_IS_OVER			ILRET_OPLIB_ERROR + 43	
// �������� ������ ������ ������� ���������� ��������� ������
#define ILRET_OPLIB_DATAS_EX_DESCR_IS_OVER			ILRET_OPLIB_ERROR + 44
// �������� �������� ����������� ����� �������� ������
#define ILRET_OPLIB_INVALID_BUF_CRC_VALUE			ILRET_OPLIB_ERROR + 45
// �� ���������� ��������� �� ������� �����
#define ILRET_OPLIB_EXTERNAL_BUF_NOT_SET			ILRET_OPLIB_ERROR + 46	
// �������� ������ �������� ������ ��� �������� TLV-������
#define ILRET_OPLIB_BINTLV_BUF_IS_OVER				ILRET_OPLIB_ERROR + 47
// �������� ������ �������� ������ ��� ����������� � ����� ������ 
#define ILRET_OPLIB_CARDDATA_BUF_IS_OVER			ILRET_OPLIB_ERROR + 48	
// �������� ������ �������� ������ ��� ��������� �������� 
#define ILRET_OPLIB_SECTORSDESCR_BUF_IS_OVER		ILRET_OPLIB_ERROR + 49
// �������� ������ �������� ������ ��� ��������� ������ �����
#define ILRET_OPLIB_BLOCKDATADESCR_BUF_IS_OVER		ILRET_OPLIB_ERROR + 50	
// �������� ������ �������� ������ ������� �� �������������� ��-���������� 
#define ILRET_OPLIB_AUTHREQUEST_BUF_IS_OVER			ILRET_OPLIB_ERROR + 51	
// �������� ������ �������� ������ ������ APDU-������
#define ILRET_OPLIB_APDUPACKET_BUF_IS_OVER			ILRET_OPLIB_ERROR + 52	
// ����������/������������� ������ �� ���������
#define ILRET_OPLIB_ENCRYPT_DECRYPT_ERROR			ILRET_OPLIB_ERROR + 53
// �� ����������� ������ � ����������� ������
#define ILRET_OPLIB_PROVIDER_SESSION_NOT_SET		ILRET_OPLIB_ERROR + 54
// �������� ����� ���-�������� ������� �� �������� ������
#define ILRET_OPLIB_ILLEGAL_HASH_LEN				ILRET_OPLIB_ERROR + 55

// ������ ���������� ��������������� ������������
#define ILRET_TEST_ERROR							5000
// �� ������ �������� � ����� ��������� �������
#define ILRET_TEST_PARAM_NOT_FOUND					ILRET_TEST_ERROR + 1
// � �������� ������� �� ������ �������� ������������ ������
#define ILRET_TEST_PARAM_READER_NAME_NOT_FOUND		ILRET_TEST_ERROR + 2
// � �������� ������� �� ������ �������� ������������������ ����������� ������
#define ILRET_TEST_PARAM_TESTS_NOT_FOUND			ILRET_TEST_ERROR + 3
// � �������� ������� �� ������ �������� �������������� ��������
#define ILRET_TEST_PARAM_OPER_ID_NOT_FOUND			ILRET_TEST_ERROR + 4
// �������� �������� �������������� �������� � �������� �������
#define ILRET_TEST_PARAM_ILLEGAL_OPER_ID			ILRET_TEST_ERROR + 5
// �������� �������� �������������� �������� � �������� �������
#define ILRET_TEST_PARAM_ILLEGAL_FORMAT				ILRET_TEST_ERROR + 6

#endif //_IL_ERROR_H_
