#ifndef _SM_ERROR_H_
#define _SM_ERROR_H_


#define ILRET_OK							    0

// ������ ������ ������������
#define ILRET_SM_ERROR					        1000

// ��: ������ �������������� ����������/�����
#define ILRET_SM_SELECT_ERROR		  		    ILRET_SM_ERROR+0
// ��: ������������ ����� ������� ������ ������� ��������������
#define ILRET_SM_SELECT_WRONG_LENGTH  		    ILRET_SM_ERROR+1
// ��: ������������ �������� ���������� P1/P2 ������� ��������������
#define ILRET_SM_SELECT_WRONG_PARAMETERS	    ILRET_SM_ERROR+2
// ��: ������������� ������ �� ������
#define ILRET_SM_SELECT_FILE_NOT_FOUND		    ILRET_SM_ERROR+3

// ��: ������ �������������� �������� ��������
#define ILRET_SM_MUTAUTH_ERROR		  		    ILRET_SM_ERROR+10
// ��: ������������ ����� ������� ������ ������� �������������� �������� ��������
#define ILRET_SM_MUTAUTH_WRONG_LENGTH 		    ILRET_SM_ERROR+11
// ��: ������������ �������� ���������� P1/P2 ������� �������������� �������� ��������
#define ILRET_SM_MUTAUTH_WRONG_PARAMETERS	    ILRET_SM_ERROR+12
// ��: �������� �������� ������������ �������������� �������� ��������
#define ILRET_SM_MUTAUTH_WRONG_CRYPTO		    ILRET_SM_ERROR+13
// ��: ��������� � ������� �������������� �������� �������� ���� �� ������
#define ILRET_SM_MUTAUTH_KEY_NOT_FOUND		    ILRET_SM_ERROR+14
// ��: ���������� �� ������������ � ��������� ������� �������������� �������� ��������
#define ILRET_SM_MUTAUTH_CONDITIONS		        ILRET_SM_ERROR+15
// ��: ������ ������������ �� �����������
#define ILRET_SM_NOT_ACTIVATED					ILRET_SM_ERROR+16

// ��: ������ ��������� ���������� ����� �����
#define ILRET_SM_GETCHAL_ERROR		  		    ILRET_SM_ERROR+20
// ��: ������������ ����� ������� ������ ������� ��������� ���������� ����� �����
#define ILRET_SM_GETCHAL_WRONG_LENGTH 		    ILRET_SM_ERROR+21
// ��: ������������ �������� ���������� P1/P2 ������� ��������� ���������� ����� �����
#define ILRET_SM_GETCHAL_WRONG_PARAMETERS	    ILRET_SM_ERROR+22

// ��: ������ ���������/��������� �������� ����������� ������
#define ILRET_SM_CHDATA_ERROR		  		    ILRET_SM_ERROR+30
// ��: ������������ ����� ������� ������ ������� ���������/��������� �������� ����������� ������
#define ILRET_SM_CHDATA_WRONG_LENGTH 		    ILRET_SM_ERROR+31
// ��: �� ��������� ������� ������������ ��� ������� ���������/��������� �������� ����������� ������
#define ILRET_SM_CHDATA_WRONG_CRYPTO		    ILRET_SM_ERROR+32
// ��: ������������ �������� ���������� P1/P2 ������� ���������/��������� �������� ����������� ������
#define ILRET_SM_CHDATA_WRONG_PARAMETERS	    ILRET_SM_ERROR+33
// ��: ��������� � ������� ���������/��������� �������� ����������� ������ ���� �� ������
#define ILRET_SM_CHDATA_KEY_NOT_FOUND		    ILRET_SM_ERROR+34
// ��: ���� ����������
#define ILRET_SM_CHDATA_KEY_BLOCKED	            ILRET_SM_ERROR+35
// ��: �������� �� ���������� � �������� ���������� �����
#define ILRET_SM_CHDATA_OP_NOT_COMPATIBLE_KEY_STATE ILRET_SM_ERROR+36
// ��: ����� ������ ������� �� ���������� � ������� �������
#define ILRET_SM_CHDATA_LENGTH_NOT_COMPATIBLE_MODE ILRET_SM_ERROR+37
// ��: �������� ������ ������ �������
#define ILRET_SM_CHDATA_WRONG_FORMAT            ILRET_SM_ERROR+38
// ��: ����� ���������������� ������ ������ ���������� ����������� ��������
#define ILRET_SM_CHDATA_WRONG_PIN_LENGTH        ILRET_SM_ERROR+39

// ��: ������ ����������� ���������
#define ILRET_SM_VERIFY_ERROR		  		    ILRET_SM_ERROR+40
// ��: ������������ ����� �������� ������ ����������� ���������
#define ILRET_SM_VERIFY_WRONG_LENGTH  		    ILRET_SM_ERROR+41
// ��: ������������ �������� ���������� P1/P2 ������� ����������� ���������
#define ILRET_SM_VERIFY_WRONG_PARAMETERS	    ILRET_SM_ERROR+42
// ��: ������ ������������
#define ILRET_SM_VERIFY_PASSWORD_BLOCKED	    ILRET_SM_ERROR+43
// ��: ��������� � ������� ����������� ��������� ���� �� ������
#define ILRET_SM_VERIFY_PASSWORD_NOT_FOUND	    ILRET_SM_ERROR+44
// ��: �������� �������� ������
#define ILRET_SM_VERIFY_WRONG_PASSWORD_PRESENTED	ILRET_SM_ERROR+45
// ��: �� ��������� ������� ������������ ��� ����������� ���������
#define ILRET_SM_VERIFY_SECURITY_STATUS_NOT_SATISFIED	ILRET_SM_ERROR+46

// ��: ������ ���������� ����������������� ��������
#define ILRET_SM_PERFSECOP_ERROR			    ILRET_SM_ERROR+50
// ��: ������������ ����� ������� ������ ������� ���������� ����������������� ��������
#define ILRET_SM_PERFSECOP_WRONG_LENGTH	        ILRET_SM_ERROR+51
// ��: ������ �������� ��������� ��������� ������� ���������� ����������������� ��������
#define ILRET_SM_PERFSECOP_BINDING_CMD_MISSED	ILRET_SM_ERROR+52
// ��: ������� ����������������� �������� �� ������������ ����������
#define ILRET_SM_PERFSECOP_BINDING_NOT_SUPPORTED	ILRET_SM_ERROR+53
// ��: �������� ���������� ��������� ����� ������� ���������� ����������������� ��������
#define ILRET_SM_PERFSECOP_WRONG_CERT	        ILRET_SM_ERROR+54
// ��: �������� ��������� ������ ������� ���������� ����������������� ��������
#define ILRET_SM_PERFSECOP_WRONG_DATA_STRUCT	ILRET_SM_ERROR+55
// ��: ������������ �������� ���������� P1/P2 ������� ���������� ����������������� ��������
#define ILRET_SM_PERFSECOP_WRONG_PARAMETERS	    ILRET_SM_ERROR+56
// ��: ���� ����������� ������� �����������
#define ILRET_SM_PERFSECOP_SIGN_KEY_ABSENT	    ILRET_SM_ERROR+57

// ��: ������ ������ ������ �� ��������� �����
#define ILRET_SM_READBIN_ERROR				    ILRET_SM_ERROR+60
// ��: ������������ ����� ������� ������ ������� ������ ��������� �����
#define ILRET_SM_READBIN_WRONG_LENGTH		    ILRET_SM_ERROR+61
// ��: ������� ������ ������ �� �� ��������� �����
#define ILRET_SM_READBIN_WRONG_FILE_TYPE	    ILRET_SM_ERROR+62
// ��: �� ��������� ������� ������� ��� ������ ������ �� ��������� �����
#define ILRET_SM_READBIN_WRONG_SEC_CONDITIONS	ILRET_SM_ERROR+63
// ��: ������� ������ �������� ������ �� ������������������ �����
#define ILRET_SM_READBIN_EF_NOT_SELECTED	    ILRET_SM_ERROR+64
// ��: ������������ �������� ���������� P1/P2 ������� ������ ������ �� ��������� �����
#define ILRET_SM_READBIN_WRONG_PARAMETERS	    ILRET_SM_ERROR+65
// ��: ��������� ��� ������ ������ �� ��������� ����� �������� ��������� ������ �����
#define ILRET_SM_READBIN_WRONG_OFFSET		    ILRET_SM_ERROR+66

// ��: ������ ������ ������ � �������� ����
#define ILRET_SM_UPDBIN_ERROR				    ILRET_SM_ERROR+70
// ��: ������������ ����� ������� ������ ������� ������ � �������� ����
#define ILRET_SM_UPDBIN_WRONG_LENGTH		    ILRET_SM_ERROR+71
// ��: �� ��������� ������� ������� ��� ������ ������ � �������� ����
#define ILRET_SM_UPDBIN_WRONG_SEC_CONDITIONS	ILRET_SM_ERROR+72
// ��: ������� ������ �������� ������ � ����������������� ����
#define ILRET_SM_UPDBIN_WRONG_FILE			    ILRET_SM_ERROR+73
// ��: ������� ������ �� � �������� ����
#define ILRET_SM_UPDBIN_WRONG_FILE_TYPE			ILRET_SM_ERROR+74
// ��: ������������ �������� ���������� P1/P2 ������� ������ ������ � �������� ����
#define ILRET_SM_UPDBIN_WRONG_PARAMETERS	    ILRET_SM_ERROR+75

// ��: ������ ������ �������� ������ �� TLV-�����
#define ILRET_SM_GETDATA_ERROR				    ILRET_SM_ERROR+80
// ��: ������������ ����� �������/�������� ������ ��� ������ �� TLV-�����
#define ILRET_SM_GETDATA_WRONG_LENGTH		    ILRET_SM_ERROR+81
// ��: ������������ �������� ���������� P1/P2 ������� ������ �������� ������ �� TLV-�����
#define ILRET_SM_GETDATA_WRONG_PARAMETERS	    ILRET_SM_ERROR+82
// ��: �� ������ ������� ������ � ��������� ����� ��� ������ �� TLV-�����
#define ILRET_SM_GETDATA_TAG_NOT_FOUND		    ILRET_SM_ERROR+83
// ��: �� ��������� ������� ������� ��� ������ �������� ������ �� TLV-�����
#define ILRET_SM_GETDATA_WRONG_SEC_CONDITIONS	ILRET_SM_ERROR+84

// ��: ������ ��������� �������� ������ � TLV-����
#define ILRET_SM_PUTDATA_ERROR				    ILRET_SM_ERROR+90
// ��: ������������ ����� ������� ������ ������� ��������� �������� ������ � TLV-����
#define ILRET_SM_PUTDATA_WRONG_LENGTH		    ILRET_SM_ERROR+91
// ��: �� ��������� ������� ������� ��� ��������� �������� ������ � TLV-����
#define ILRET_SM_PUTDATA_WRONG_SEC_CONDITIONS	ILRET_SM_ERROR+92
// ��: ������������ �������� ���������� P1/P2 ������� ��������� �������� ������ � TLV-����
#define ILRET_SM_PUTDATA_WRONG_PARAMETERS	    ILRET_SM_ERROR+93
// ��: ����������� ��� ��� ��������� �������� ������ � TLV-����
#define ILRET_SM_PUTDATA_TAG_NOT_FOUND		    ILRET_SM_ERROR+94

// ��: ������ ���������� ������� MSE
#define ILRET_SM_MSE_ERROR	                    ILRET_SM_ERROR+100
// ��: ������������ ����� ������� ������ ������� MSE
#define ILRET_SM_MSE_WRONG_LENGTH  	            ILRET_SM_ERROR+101
// ��: ��������� ��������� ������������ ������� MSE �� ����� ���� ������������
#define ILRET_SM_MSE_CANT_SET_CONTEXT	        ILRET_SM_ERROR+102
// ��: ������������ �������� ���������� P1/P2 ������� MSE
#define ILRET_SM_MSE_WRONG_PARAMETERS	        ILRET_SM_ERROR+103

// ��: ������ ���������� ������� ������ ��������������
#define ILRET_SM_AUTH_BEGIN_ERROR	            ILRET_SM_ERROR+110
// ��: ������������ �������� ������ ������� ������ ��������������
#define ILRET_SM_AUTH_BEGIN_WRONG_DATA	        ILRET_SM_ERROR+111
// ��: ������ �������� ������ ������� ������ ��������������
#define ILRET_SM_AUTH_BEGIN_REF_DATA_ERROR	    ILRET_SM_ERROR+112

// ��: ������ ���������� ������� ���������� ��������������
#define ILRET_SM_AUTH_COMPLETE_ERROR    	    ILRET_SM_ERROR+120
// ��: ������������ �������� ������ ������� ���������� ��������������
#define ILRET_SM_AUTH_COMPLETE_WRONG_DATA	    ILRET_SM_ERROR+121
// ��: ������ �������� ������ ������� ���������� ��������������
#define ILRET_SM_AUTH_COMPLETE_REF_DATA_ERROR	ILRET_SM_ERROR+122

// ��: ������ ���������� ������� ��������� ������ � ����������� ������
#define ILRET_SM_SP_SESS_ERROR          	    ILRET_SM_ERROR+130
// ��: ������������ ����� ������� ������ ������� ��������� ������ � ����������� ������
#define ILRET_SM_SP_SESS_WRONG_LENGTH	        ILRET_SM_ERROR+131
// ��: ���������� �� ������������ ��� ���������� ������� ��������� ������ � ����������� ������
#define ILRET_SM_SP_SESS_CONDITIONS_NOT_SATISFIED   ILRET_SM_ERROR+132
// ��: ������������ �������� ���������� P1/P2 ������� ��������� ������ � ����������� ������
#define ILRET_SM_SP_SESS_WRONG_PARAMETERS       ILRET_SM_ERROR+133

// ��: ������ ���������
#define ILRET_SM_SE_ACTIVATION_ERROR    	    ILRET_SM_ERROR+140
// ��: ������������ �������� ������ ������� ���������
#define ILRET_SM_SE_ACTIVATION_WRONG_DATA	    ILRET_SM_ERROR+141

// ��: ������ ������������ ��� � �������������� ���������
#define ILRET_SM_SE_ALREADY_ACTIVATED			ILRET_SM_ERROR+150
// ��: ������ ������������ ��� � ���������������� ���������
#define ILRET_SM_SE_ALREADY_DEACTIVATED			ILRET_SM_ERROR+151
// ��: ����������� ����� ���������/����������� �����
#define ILRET_SM_SE_ILLEGAL_ACTIVATION_MODE		ILRET_SM_ERROR+152

// ��: �������� ������
#define ILRET_SM_WRONG_DATA						ILRET_SM_ERROR+160

#endif