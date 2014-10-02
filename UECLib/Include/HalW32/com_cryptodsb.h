#ifndef _com_cryptodsb_h_
#define _com_cryptodsb_h_

//////////////////////////////////////////////  
//	Internus Corp 
//
//	com.internus.cryptodsb
//	Product Version: 1.9
//
//	Prototypes and literals
//////////////////////////////////////////////

#define __byte	unsigned char 
#define __word	unsigned short 
#define __dword unsigned int 
#ifdef __cplusplus
    #define __deffunc  extern "C" 
#else
    #define __deffunc
#endif

#define MAX_RSA_MODULUS_LEN 256 

// �� ������������
__deffunc void DES_Encrypt (
					__byte* data_in_out8, 
					__byte* key16
				 );

// �� ������������
__deffunc void DES_Decrypt (
					__byte* data_in_out8, 
					__byte* key16
				 );

//  Description:
//		������������ ������������� ������ � ������������ � ISO/IEC 18033-3 � ������ CBC. 
//  See Also:
//		DES3_CBC_PAD_Encrypt
//  Arguments:
//      in_key16			- ���������� ����� ������, ������������� ��� ������������� ������. 
//		in_iv8				- ��������� �� �����, ���������� ��������� �������� ������� �������������.
//		in_data				- ��������� �� �����, ���������� ������ ��� �������������.
//		data_len			- ����� ������ ��� �������������.
//		out_decrypted_data	- ��������� �� �������� ��� �������������� ������.
//		out_decrypted_len	- ��������� �� ����������, ���������������� ������ �������������� ������.
//  Return Value:
//      ��� ������.
//  Summary:
//      ������������� ������ � ������������ � ISO/IEC 18033-3 � ������ CBC.
__deffunc __word DES3_CBC_PAD_Decrypt (
					__byte* in_key16, 
					__byte* in_iv8, 
					__byte* in_data, 
					__dword data_len, 
					__byte* out_decrypted_data, 
					__dword* out_decrypted_len
					);

//  Description:
//		������������ ���������� ������ � ������������ � ISO/IEC 18033-3 � ������ CBC. 
//  See Also:
//		DES3_CBC_PAD_Encrypt
//  Arguments:
//      in_key16			- ���������� ����� ������, ������������� ��� ������������� ������. 
//		in_iv8				- ��������� �� �����, ���������� ��������� �������� ������� �������������.
//		in_data				- ��������� �� �����, ���������� ������ ��� ����������.
//		data_len			- ����� ������ ��� ����������.
//		out_encrypted_data	- ��������� �� �������� ��� ������������� ������.
//		out_encrypted_len	- ��������� �� ����������, ���������������� ������ ������������� ������.
//  Return Value:
//      �����������.
//  Summary:
//      ���������� ������ � ������������ � ISO/IEC 18033-3 � ������ CBC.
__deffunc void DES3_CBC_PAD_Encrypt (
					__byte* in_key16, 
					__byte* in_iv8, 
					__byte* in_data, 
					__dword data_len, 
					__byte* out_encrypted_data, 
					__dword* out_encrypted_len
					);

//  Description:
//		������������ ����������� ������ � ������������ � ISO/IEC 9797-1. 
//  See Also:
//  Arguments:
//		in_data		- ��������� �� �����, ���������� ������ ��� ���������� ������������.
//		data_len	- ����� ������ ��� ���������� ������������.
//      in_key16	- ���������� ����������� �����, ������������� ��� ���������� ������������. 
//		out_mac4	- ��������� �� �������� ����� ��� �����������.
//  Return Value:
//      �����������.
//  Summary:
//      ����������� ������ � ������������ � ISO/IEC 9797-1.
__deffunc void DES_RetailMAC4 (
					__byte* in_data, 
					__word data_len, 
					__byte* in_key16, 
					__byte* out_mac4
					);
					
//  Description:
//		������������ ����������� ������ � ������������ � ISO/IEC 9797-1. 
//  See Also:
//  Arguments:
//		in_data		- ��������� �� �����, ���������� ������ ��� ���������� ������������.
//		data_len	- ����� ������ ��� ���������� ������������.
//      in_key16	- ���������� ����������� �����, ������������� ��� ���������� ������������. 
//		out_mac4	- ��������� �� �������� ����� ��� �����������.
//  Return Value:
//      �����������.
//  Summary:
//      ����������� ������ � ������������ � ISO/IEC 9797-1.
__deffunc void DES_MAC4 (
					__byte* in_data, 
					__word data_len, 
					__byte* in_key16, 
					__byte* out_mac4
					);

//  Description:
//		������������ �������������� ������ � ������������ � PKCS #1. 
//  See Also:
//  Arguments:
//		out_data	 - ��������� �� �������� �����, ���������� ��������������� ������.
//		out_data_len - ����� ��������������� ������.
//      in_data		 - ��������� �� �����, ���������� ������ ��� ��������������. 
//		in_data_len  - ����� ������ ��� ��������������.
//		in_key_mod   - ��������� �� �����, ���������� ������ ����������� ������ �����.
//		key_mod_len	 = ����� ������ ����������� ������ �����.
//		in_key_exp   - ��������� �� �����, ���������� ������ ����������� ���������� �����.
//		key_exp_len	 = ����� ������ ����������� ���������� �����.
//  Return Value:
//      0 ��� ������.
//  Summary:
//      �������������� ������ � ������������ � PKCS #1.
__deffunc int RSA_Block (
			__byte* out_data, 
			__word* out_data_len, 
            __byte* in_data, __word in_data_len, 
            __byte* in_key_mod, __word key_mod_len,
            __byte* in_key_exp, __word key_exp_len
			);

// ������� ��������� ����������� ������ � ������������ � ���������� SHA-1
// msg [in] ��������� �� �����, ���������� ������ ��� �����������
// msg_len [in]  ����� ������ ���������� ������
// hash32 [out] ��������� �� �����, ����� ������� ������������ �������� ������� �����������


//  Description:
//		������������ ����������� ������ � ������������ � ���������� SHA-1. 
//  See Also:
//  Arguments:
//		in_msg	- ��������� �� �����, ���������� ������ ��� �����������.
//		msg_len	- ����� ���������� ������.
//		out_msg_digest20 - ��������� �� �������� ����� � ������������� �������.
//  Return Value:
//      �����������.
//  Summary:
//      ����������� ������ � ������������ � ���������� SHA-1.
__deffunc void SHA1 (
		__byte* in_msg, 
		__dword msg_len, 
		__byte* out_msg_digest20
		);

#endif