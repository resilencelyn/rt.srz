/**  �������������� ������� ��������� ���������
  */
#ifndef __OP_EVENT_H_
#define __OP_EVENT_H_ 

// ������ ����������� ������� �� ���������
#define IS_DEFAULT_EVENT    (*inout_Event == p_opContext->InterruptEvent && justEntry == 1)

/**  ������� �������
  */

//  ��������� ��������
#define E_ACTIVATE							0x0000
// �������� ������ ������
#define E_SERVICE_SELECTING					0x0001
// ������ �������
#define E_SERVICE_SELECTED					0x0002
// �������� ��������� ����� � �����
#define E_CARD_WAITING						0x0003
// ����� �����������
#define E_CARD_INSERTED						0x0004
// �������� ���������� ����� �� ������ 			
#define E_CARD_RELEASING					0x0005
// ����� ���������
#define E_CARD_RELEASED						0x0006
// ������ �� ��������� �������������� �� ������
#define E_METAINFO_REQUESTED				0x0007
// �������������� �� ������ �������� � ��������� � ���������
#define E_METAINFO_ENTERED					0x0008
// ������ �� ���� ���
#define E_PIN_REQUESTED						0x0009
// ������ ���
#define E_PIN_ENTERED						0x000A
// ��������� ���� ���
#define E_PIN_RETRY_REQUESTED				0x000B
// ������ �� ��������� ������� ������ ��� �������� ������
#define E_EXTRA_DATA_REQUESTED				0x000C
// ������� ������ ��������
#define E_EXTRA_DATA_ENTERED				0x000D
// ������ � ����� ������������
#define E_CARD_DATA_PREPARED				0x000E
// ������ ������� �� �������� ������ ������������
#define E_SERVICE_REQUEST_DATA_PREPARED		0x000F
// ������ �� �������������� ��-���������� �����������
#define E_APP_AUTH_REQUEST_PREPARED			0x0010
// ��������� ����������� �������������� ��-����������
#define E_PROCESS_APP_AUTH_RESPONSE_DATA	0x0011
// ������ �� ���� ������ ���
#define E_NEW_PIN_REQUESTED					0x0012
// ����� ��� �����
#define E_NEW_PIN_ENTERED					0x0013
// ������ �� ���� ��������������� ���
#define E_CONFIRM_PIN_REQUESTED				0x0014
// �������������� ��� �����
#define E_CONFIRM_PIN_ENTERED				0x0015
// ����� ������ ��� ���������
#define E_SELECT_EDITING_CARD_DATA			0x0016
// ������ ��� �������������� �������
#define E_EDITING_CARD_DATA_SELECTED		0x0017
// ������ �� �������������� ��������� � ����� ������
#define E_CARD_DATA_EDIT_REQUSTED			0x0018
// ������������� ��������� ������ ��������
#define E_CARD_DATA_MODIFIED				0x0019
// ������������� ��������� ������ �� ��������
#define E_CARD_DATA_NOT_MODIFIED			0x001A
// ������ �� ���� ���
#define E_PUK_REQUESTED						0x001B
// ������ ���
#define E_PUK_ENTERED						0x001C
// ������ �� ��������� ���� ���
#define E_PUK_RETRY_REQUESTED				0x001D
// ������ �� ���� ������ ���
#define E_NEW_PUK_REQUESTED					0x001E
// ����� ��� �����
#define E_NEW_PUK_ENTERED					0x001F
// ��������� ��������� ���������� ������ � ���������
#define E_ISSUER_SESSION_REQUESTED			0x0020
// ������������ �������������� ����� ��� ���������� ������ � ��������� ������������
#define E_ISSUER_AUTH_CRYPTOGRAMM_READY		0x0021
// ��������� �������� ������������� ���������� ������ � ���������
#define E_CHECK_ISSUER_SESSION_REQUESTED	0x0022
// ������������� ���������� ������ � ��������� ���������
#define E_ISSUER_SESSION_CHECKED			0x0023
// �������� ������ APDU-�������
#define E_APDU_PACKET_WAITING				0x0024
// ����� APDU-������� �����
#define E_APDU_PACKET_ENTERED				0x0025
// ����� APDU-������� ���������
#define E_APDU_PACKET_ABSENT				0x0026
// ��������� ������ ������ APDU-������� ����� �� ����������
#define E_PROCESS_APDU_PACKET				0x0027
// ��������� ������ ������ APDU-������� ������� ���������
#define E_APDU_PACKET_PROCESSED				0x0028
// ������ �� ��������� ��� XML-������� ������
#define E_HASH_REQUESTED					0x0029
// ��� XML-������� ������ �����
#define E_HASH_ENTERED						0x002A
// ���� ������-������� �� ������ ��������� ������ ��� �������� ������
#define E_CARD_DATA_REQUESTED				0x002B
// ������-������ �� ������ ��������� ������ ������
#define E_CARD_DATA_DESCR_ENTERED			0x002C
// ��������� ������� �����
#define E_CARD_CAPTURE_REQUESTED			0x002D
// ����� ������
#define E_CARD_CAPTURED						0x002E
// ��������� ���������� �����
#define E_CARD_LOCK_REQUESTED				0x002F
// ������ �� ��������� �������� ��������� ������������ �������
#define E_SECTOR_EX_DESCR_REQUESTED			0x0030
// ��������� �������� ��������� ������������ ������� �������
#define E_SECTOR_EX_DESCR_ENTERED			0x0031
// ��������� ��������� ������ ��� ��������� ��������
#define E_SECTORS_DESCR_BUF_REQUESTED		0x0032
// ������ ��� ��������� �������� ����������
#define E_SECTORS_DESCR_BUF_SET				0x0033
// ������ ��� ��������� �������� �� ������������
#define E_SECTORS_DESCR_BUF_NOT_USED		0x0034
// ����������� ���������� ������ �������
#define E_CITIZEN_VERIFIED					0x0035
// ������������� ������������ ����������� ������� ��������� �����
#define E_DIGITAL_SIGN_CONFIRMATION			0x0036
// ������������ ����������� ������� ��������� ����� ������������
#define E_DIGITAL_SIGN_CONFIRMED			0x0037
// ������������ ����������� ������� ��������� ����� �� ���������
#define E_DIGITAL_SIGN_NOT_CONFIRMED		0x0038
// ������ �� ���� ������ ��������� ����������� �������
#define E_DIGITAL_SIGN_PIN_REQUESTED		0x0039
// ������ ��������� ����������� ������� �����
#define E_DIGITAL_SIGN_PIN_ENTERED			0x003A
// ��������� ���� ������ ��������� ����������� �������
#define E_DIGITAL_SIGN_PIN_RETRY_REQUESTED	0x003B
// ����������� ������� ��������� ����� ������������
#define E_DIGITAL_SIGN_PREPARED				0x003C
// ������������� ������������ ���������� ������ � ����������� ������ 
#define E_PROVIDER_SESSION_CONFIRMATION		0x003D
// ��������� ���������� ������ � ����������� ������ ������������ 
#define E_PROVIDER_SESSION_CONFIRMED		0x003E
// ��������� ���������� ������ � ����������� ������ �� ��������� 
#define E_PROVIDER_SESSION_NOT_CONFIRMED	0x003F
// ���������� ������ � ����������� ������ �����������
#define E_PROVIDER_SESSION_ESTABLISHED		0x0040
// ���������� ������ � ����������� ������ ����������� �� ������� �����
#define E_PROVIDER_SESSION_HOST_ESTABLISHED 0x0041
// ��������� �������� ���� ������ ��� ���������� � ������ ������ � ����������� ������
#define E_PROVIDER_DATA_ENCRYPT_REQUESTED	0x0042
// ���� ������ ��� ���������� �������
#define E_PROVIDER_DATA_ENCRYPT_ENTERED		0x0043
// ���� ������ ��� ����������/������������� �����������
#define E_PROVIDER_DATA_EMPTY				0x0044
// ���� ������ ����������
#define E_PROVIDER_DATA_ENCRYPTED			0x0045
// �������������/�������������� ���� ������ �����������
#define E_PROVIDER_DATA_PROCESSED			0x0046
// ��������� �������� ���� ������ ��� ������������� � ������ ������ � ����������� ������
#define E_PROVIDER_DATA_DECRYPT_REQUESTED	0x0047
// ���� ������ ��� ������������� �������
#define E_PROVIDER_DATA_DECRYPT_ENTERED		0x0048
// ���� ���������� ������ �����������
#define E_PROVIDER_DATA_DECRYPTED			0x0049
// ������������� ������������� �������������� ���������� ������
#define E_PROVIDER_AUTH_CONFIRMATION		0x004A
// �������������� ���������� ������ ������������ 
#define E_PROVIDER_AUTH_CONFIRMED			0x004B
// �������������� ���������� ������ �� ��������� 
#define E_PROVIDER_AUTH_NOT_CONFIRMED		0x004C
// ������� ����� �� �������������� ��-���������� 
#define E_APP_AUTH_RESPONSE_RECEIVED		0x004D
// ���������� ������ ������ �� �������������� ��-���������� 
#define E_APP_AUTH_RESPONSE_DATA_REQUESTED  0x004E
// ����� ����� APDU-������, ������������� �� ����� ���������� ������
#define E_APDU_ENCRYPTED_PACKET_ENTERED		0x004F
// ������ �� ���� ������ ��������� ������ ������������ 
#define E_SE_ACTIVATION_PIN_REQUESTED		0x0050
// ������ ��������� ������ ������������ �����
#define E_SE_ACTIVATION_PIN_ENTERED			0x0051
// ��������� ���� ������ ��������� ������ ������������
#define E_SE_ACTIVATION_PIN_RETRY_REQUESTED	0x0052
// ��������� ������� ��� ��������� �� 
#define E_SE_OWNER_NAME_REQUESTED			0x0053
// ��� ��������� �� �����
#define E_SE_OWNER_NAME_ENTERED				0x0054
// ���� ����� ������������ �����������
#define E_PASS_PHRASE_REQUESTED				0x0055
// ����� ������������ ����������� �������
#define E_PASS_PHRASE_ENTERED				0x0056
// ��������� ������� ������ �� �������������� ��-����������
#define E_SEND_APP_AUTH_REQUESTED			0x0057
// ����������� ���������� 
#define E_CONTINUE							0x0058


/*  ����������
 * �������� ���������� ������������� �� 0x1000 �� 0x1200.
 * ��������� �� 0x1100, ���������� ������������ ��� ��������� ����������.
 */

// ������� ������������� ���������� 
#define E_EXCEPTION_EVENTS                  0x1000 
//  ������������ ������� ���������� �������� 
#define E_EXCEPTION_DEPTH                   E_EXCEPTION_EVENTS + 0x01
//  ������������ ������������� ���������� 
#define E_EXCEPTION_USE                     E_EXCEPTION_EVENTS + 0x02
//  ������ ���������� 
#define E_EXCEPTION_RUNTIME_ERROR           E_EXCEPTION_EVENTS + 0x03
//  ����� ������������ 
#define E_EXCEPTION_USER_BREAK              E_EXCEPTION_EVENTS + 0x04
// ���������� ���������� 
#define E_EXCEPTION_INTERRUPT               E_EXCEPTION_EVENTS + 0x05
//  ������������ ��������
#define E_EXCEPTION_CYCLING_DETECTED        E_EXCEPTION_EVENTS + 0xFF
// �������� � ���� ��������������� ������� ��� ��� ��������� 
#define E_CATCHING_OFFSET_EVENTS            0x100                   
//  ������ ���������� ��������� �� ���������� ����������
#define IS_EMERGENCY_EXCEPTION(EVENT)       (EVENT == E_EXCEPTION_INTERRUPT)
//  ���������� �������� �������� ���������� ����� ��� ���������
#define IS_CATCHED_EXCEPTION(EVENT)         ((EVENT) - E_CATCHING_OFFSET_EVENTS)
// ������ ����������� ��������� ������� � ������ ����������
#define IS_EXCEPTION_EVENT_RANGE(EVENT)     ((EVENT >= E_EXCEPTION_EVENTS) && (EVENT <= (E_EXCEPTION_EVENTS + 0x100)))

// ������� ������������� ���������� ������� 
#define E_INTERNAL_EVENTS                   0x0400
// �������� ���������� �������� 
#define E_EMPTY                             E_INTERNAL_EVENTS + 0x00
//  ��� �������� 
#define E_PIN_IS_INCORRECT					E_INTERNAL_EVENTS + 0x01
// ��������� ��������� ����������� ������ ����� ������ � ��������� ��-���������� 
#define E_ISSUER_SESSION_REQUEST			E_INTERNAL_EVENTS + 0x02
// ��������� ������� ����� 
#define E_CARD_CAPTURE_REQUEST				E_INTERNAL_EVENTS + 0x03
// ��������� ���������� ��������������� ������ 
#define E_EXECUTE_APDU_PACKET				E_INTERNAL_EVENTS + 0x04	


//  ��� �������, ������������, ��� ������� ���������� ���������� �� ���������� ��������
#define RETURN_MARK 0x2000
//  ��������� � ������� ������� �������, ���������� ������������� �� ���������� �������� ��� ��������� ����������� ���������
#define IS_EVENT_RETURN_MARKED(EVENT) ((EVENT & RETURN_MARK) >> 13)
// ������� ��� RETURN_MARK, �������� ���� ������ �������.
// ���� ������� �� �������� RETURN_MARK, �� ����� ���������� ������� � ������ ��������.
#define IS_RETURN_EVENT(EVENT) (EVENT ^ RETURN_MARK)


#endif//__OP_EVENT_H_