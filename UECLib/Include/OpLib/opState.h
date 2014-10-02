/**  ������ �������� ������ ������������ ��������� ��������� �������.
  */
#ifndef __OP_STATE_H_
#define __OP_STATE_H_


// ����� ��������
#define S_IDLE								0x0000
// ��������� �������������� ��������
#define S_EXCEPTION_CATCHING				0x0001
// �������� ������ ��������
#define S_SERVICE_SELECTING					0x0002
// �������� �� �������
#define S_CARD_WAITING						0x0003
// �������� �� �������
#define S_CARD_RELEASING					0x0004
// ������ �� ��������� �������������� �� ������
#define S_METAINFO_REQUESTED				0x0005
// �������������� ��-����������
#define S_APP_SELECTING						0x0006
// �������������� ���������
#define S_AUTH_TERMINAL						0x0007
// �������� ����� ���
#define S_PIN_WAITING						0x0008
// �������� ���������� ����� ���
#define S_PIN_RETRY							0x0009
// ����������� ����������
#define S_VERIFY_CITIZEN					0x000A
// ������ �� ��������� �������������� ������ �� �������� ������
#define S_EXTRA_DATA_REQUESTED				0x000B
// ������ �� ������ ������ � �����
#define S_CARD_DATA_READ					0x000C
// ������ � ����� �������
#define S_CARD_DATA_PREPARED				0x000D
// �������������� ��-����������
#define S_AUTH_APP_REQUESTED				0x000E
// ������ �� �������������� ��-���������� �����������
#define S_AUTH_APP_REQUEST_PREPARED			0x000F
// ��������� ����������� �������������� ��-����������
#define S_PROCESS_AUTH_APP_RESPONSE_DATA	0x0010
// �������� ����� ������ ��� 
#define S_NEW_PIN_WAITING					0x0011
// �������� ��������������� ���
#define S_CONFIRM_PIN_WAITING				0x0012
// ����� ���
#define S_CHANGE_PIN						0x0013
// ����� ����� ������ ��� ��������������
#define S_SELECT_EDITING_BLOCK				0x0014
// �������������� ���������� � ���������� ������ ��������
#define S_SECTORS_LIST_FILLING				0x0015
// �������������� ����� ����������� ��������
#define S_AUTH_OPERATION					0x0016
// ���������� ��������� ������ ��� ��������������
#define S_PREPARE_EDITING_CARD_DATA			0x0017
// �������������� ��������� ������
#define S_CARD_DATA_EDIT					0x0018
// ������ �� ����� ��������� ������
#define S_EDITED_CARD_DATA_WRITE			0x0019
// �������� ����� ��� 
#define S_PUK_WAITING						0x001A
// �������� ���������� ����� ���
#define S_PUK_RETRY							0x001B
// �������� ����� ������ ���
#define S_NEW_PUK_WAITING					0x001C
// ��������� ��������� ����������� ���������� � ���������
#define S_CRYPTO_ISSUER_SESSION_INIT		0x001D
// �������������� �������� (�������� ����������� ���������� � ���������)
#define S_AUTH_ISSUER						0x001E
// �������� ������ �������� ����������� ���������� � ���������
#define S_CHECK_ISSUER_SESSION				0x001F
// ������������� ��� � �������������� ���������� ��������
#define S_UNLOCK_TMP_PUK					0x0020
// ��������� ���������� ������ � ���������
#define S_ESTABLISH_ISSUER_SESSION			0x0021
// �������������� ��-����������
#define S_AUTH_APPLICATION					0x0022
// �������� ������ APDU-������
#define S_APDU_PACKET_WAITING				0x0023
// ���������� ������ APDU-������
#define S_EXECUTE_APDU_PACKET				0x0024
// ������ APDU-������� �����������
#define S_APDU_PACKET_ABSENT				0x0025
// ��������� ������ ������ APDU-������ ����� �� ���������� 
#define S_PROCESS_APDU_PACKET				0x0026
// ��������� ��� XML-������� ������
#define S_HASH_REQUESTED					0x0027
// ��������� ������-������� �� ������ ��������� ������ ��� ������
#define S_CARD_DATA_REQUESTED				0x0028
// ��������� ������� �����
#define S_CARD_CAPTURE_REQUESTED			0x0029
// ��������� �����
#define S_CARD_LOCK							0x002A
// ��������� �������� ��������� ������������ �������
#define S_SECTOR_EX_DESCR_REQUESTED			0x002B
// ���������� �������� ��������� �������
#define S_ADD_SECTOR_EX_DESCR				0x002C
// ��������� ������ ��� ���������� �������� �����
#define S_SET_SECTORS_DESCR_BUF				0x002D
// �������������� �������� ������ �������
#define S_CITIZEN_VERIFIED					0x002E
// ������������� ������������ ����������� ������� ��������� �����
#define S_DIGITAL_SIGN_CONFIRMATION			0x002F
// �������� ����� ��� ����������� ������� ��������� �����
#define S_DIGITAL_SIGN_PIN_WAITING			0x0030
// ������������ ����������� ������� ��������� �����
#define S_PREPARE_DIGITAL_SIGN				0x0031
// ������ �� �������������� ��-���������� �����������
#define S_APP_AUTH_REQUEST_PREPARED			0x0032
// ������������� ������������� ������������ ���������� ������ � ����������� ������ 
#define S_PROVIDER_SESSION_CONFIRMATION		0x0033
// ������������ ���������� ������ � ����������� ������
#define S_PROVIDER_SESSION_ESTABLISH		0x0034
// ���������� ������ � ����������� ������ �����������
#define S_PROVIDER_SESSION_ESTABLISHED	    0x0035
// ��������� ����� ������ ��� ����������
#define S_PROVIDER_DATA_ENCRYPT_REQUESTED	0x0036
// ���������� ����� ������
#define S_PROVIDER_DATA_ENCRYPT				0x0037
// ���� ������ ����������
#define S_PROVIDER_DATA_ENCRYPTED			0x0038
// ��������� ����� ������ ��� �������������
#define S_PROVIDER_DATA_DECRYPT_REQUESTED	0x0039
// ������������� ����� ������
#define S_PROVIDER_DATA_DECRYPT				0x003A
// ���� ������ �����������
#define S_PROVIDER_DATA_DECRYPTED			0x003B
// ������������� ������������� �������������� ���������� ������ 
#define S_PROVIDER_AUTH_CONFIRMATION		0x003C
// �������������� ���������� ������
#define S_AUTH_PROVIDER						0x003D
// �������� ����� ������ ������ �� ������ �������������� ��-����������
#define S_APP_AUTH_RESPONSE_DATA_REQUESTED	0x003E
// ������� ����� �� ������ �������������� ��-����������
#define S_APP_AUTH_RESPONSE_RECEIVED		0x003F
// ������������� ������ APDU-������ �� ���������� ����� ���������� ������
#define S_DECRYPT_APDU_PACKET				0x0040
// ���������� ������ ������������ ������ APDU-������ �� ���������� ����� ���������� ������
#define S_ENCRYPT_APDU_PACKET				0x0041
// ����������� ��������� ������ ������������
#define S_VERIFY_SE_OWNER					0x0042
// ������ ��������� ���������/����������� ������ ������������
#define S_START_SE_ACTIVATION				0x0043
// ���������� ��������� ���������/����������� ������ ������������
#define S_FINISH_SE_ACTIVATION				0x0044
// �������� ����� ��� ��������� ������ ������������
#define S_SE_ACTIVATION_PIN_WAITING			0x0045
// ��������� ����� ��������� �� 
#define S_SE_OWNER_NAME_REQUESTED			0x0046
// �������� ����� ������������ �����������
#define S_PASS_PHRASE_WAITING				0x0047
// ������ ������������ ����������� �� �����
#define S_PASS_PHRASE_WRITE					0x0048
// ����������� �� ������� ������� �� �������������� ��-����������\�������� ������
#define S_SEND_APP_AUTH_REQUEST				0x0049


#endif//__OP_STATE_H_