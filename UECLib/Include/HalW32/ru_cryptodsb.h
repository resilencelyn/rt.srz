#ifndef _ru_cryptodsb_h_
#define _ru_cryptodsb_h_

///////////////////////////////////////////////////////  
//	Internus Corp 
//
//	ru.internus.cryptodsb
//	Product Version: 1.9
//
//	Prototypes and literals
//////////////////////////////////////////////////////

#define __byte	unsigned char 
#define __word	unsigned short 
#define __dword unsigned int 
#ifdef __cplusplus
    #define __deffunc  extern "C" 
#else
    #define __deffunc
#endif

// ������������ ������ ��� 1.0
__deffunc __word GostR3410_2001_GenKeyPair (
								 __byte* priv_key32, 
								 __byte* pub_key64
								 );

// ������������ ������ ��� 1.0
__deffunc __word GostR3410_2001_Sign (
						   __byte* msg, 
						   __dword msg_len, 
						   __byte* priv_key32, 
						   __byte* sign64
						   );
						   
//  Description:
//		��������� ����������� �������� ������� ������������ ������ � ������������ � ���� � 34.10-2001.<p/> 
//  See Also:
//		GostR3410_2001_Verify11
//  Arguments:
//      in_msg		  - ��������� �� �����, ���������� ������ ��� �������. 
//		msg_len		  - ����� ������������� ������.
//		in_priv_key32 - ���������� ��������� ����� �������.
//		out_sign64	  - ��������� �� �������� ����� ��� �������� �������.
//  Return Value:
//      __word		- ��� ������.
//  Summary:
//      ��������� ����������� �������� ������� ���� � 34.10-2001.
__deffunc __word GostR3410_2001_Sign11 (
						   __byte* in_msg, 
						   __dword msg_len, 
						   __byte* in_priv_key32, 
						   __byte* out_sign64
						   );

// ������������ ������ ��� 1.0
__deffunc __word GostR3410_2001_Verify (
							 __byte* msg, 
							 __dword msg_len, 
							 __byte* pub_key64, 
							 __byte* sign64
							 );
							 
//  Description:
//		��������� ����������� �������� ������� ������������ ������ � ������������ � ���� � 34.10-2001.<p/> 
//  See Also:
//		GostR3410_2001_Sign11
//  Arguments:
//      in_msg		  - ��������� �� �����, ���������� ������ ��� �������. 
//		msg_len		  - ����� ������������� ������.
//		in_pub_key64 -  ���������� ��������� ����� ����������� �������.
//		out_sign64	  - ��������� �� �������� ����� ��� �������� ����������� �������.
//  Return Value:
//      __word		- ��� ������.
//  Summary:
//      �������� ����������� �������� ������� ���� � 34.10-2001.
__deffunc __word GostR3410_2001_Verify11 (
							 __byte* in_msg, 
							 __dword msg_len, 
							 __byte* in_pub_key64, 
							 __byte* sign64
							 );

// ������������ ������ ��� 1.0
__deffunc void GostR3410_2001_CompressPublicKey (
									  __byte* pub_key64, 
									  __byte* pub_key33_compressed
									  );

// ������������ ������ ��� 1.0
__deffunc __word GostR3410_2001_KeyMatching (
								  __byte* priv1key32, 
								  __byte* pub2key64, 
								  __byte* sk64
								  );

//  Description:
//		������������ ���������� ������ � ������������ � ���� 28147-89. 
//  See Also:
//		Gost28147_Decrypt
//  Arguments:
//      in_key32			- ���������� ����� ������, ������������� ��� ���������� ������. 
//		in_data				- ��������� �� �����, ���������� ������ ��� ����������.
//		data_len			- ����� ��������� ������.
//		out_encrypted_data	- ��������� �� �������� ��� ������������� ������.
//		in_iv8				- ��������� �� �����, ���������� ��������� �������� ������� �������������.
//  Return Value:
//      �����������.
//  Summary:
//      ���������� ������ � ������������ � ���� 28147-89.
__deffunc void Gost28147_Encrypt (
					   __byte* in_key32, 
					   __byte* in_data, 
					   __dword data_len, 
					   __byte* out_encrypted_data, 
					   __byte* in_iv8
					   );

//  Description:
//		������������ ������������� ������ � ������������ � ���� 28147-89. 
//  See Also:
//		Gost28147_Encrypt
//  Arguments:
//      in_key32			- ���������� ����� ������, ������������� ��� ������������� ������. 
//		in_data				- ��������� �� �����, ���������� ������ ��� �������������.
//		data_len			- ����� ������ ��� �������������.
//		out_decrypted_data	- ��������� �� �������� ��� �������������� ������.
//		in_iv8				- ��������� �� �����, ���������� ��������� �������� ������� �������������.
//  Return Value:
//      �����������.
//  Summary:
//      ������������� ������ � ������������ � ���� 28147-89.
__deffunc void Gost28147_Decrypt (
					   __byte* key32, 
					   __byte* data, 
					   __dword data_len, 
					   __byte* decrypted_data, 
					   __byte* iv8
					   );

//  Description:
//		������������ ����������� ������ � ������������ � ���� 28147-89. 
//  See Also:
//  Arguments:
//      in_key		- ���������� ����������� �����, ������������� ��� ���������� ������������. 
//		in_data		- ��������� �� �����, ���������� ������ ��� ���������� ������������.
//		data_len	- ����� ������ ��� ���������� ������������.
//		out_imit4	- ��������� �� �������� ����� ��� �����������.
//  Return Value:
//      �����������.
//  Summary:
//      ����������� ������ � ������������ � ���� 28147-89.
__deffunc void Gost28147_Imit (
					__byte* in_key, 
					__byte* in_data, 
					__dword data_len, 
					__byte* out_imit4
					);

//  Description:
//		������������ ����������� ������ � ������������ � ���� � 34.11-94. 
//  See Also:
//  Arguments:
//      in_msg		- ��������� �� �����, ���������� ������ ��� �����������. 
//		msg_len		- ����� ���������� ������.
//		out_hash32	- ��������� �� �������� ����� �� ��������� ������� �����������.
//  Return Value:
//      �����������.
//  Summary:
//      ����������� ������ � ������������ � ���� � 34.11-94.
__deffunc void GostR3411_94_Hash (
					   __byte* in_msg, 
					   __dword msg_len, 
					   __byte* out_hash32
					   );

//  Description:
//		������������ ���������� ������ � ������������ � ���� 28147-89 � ������ ECB. 
//  See Also:
//  Arguments:
//      in_key32			- ���������� ����� ������, ������������� ��� ���������� ������. 
//		in_data				- ��������� �� �����, ���������� ������ ��� ����������.
//		data_len			- ����� ��������� ������.
//		out_encrypted_data	- ��������� �� �������� ��� ������������� ������.
//  Return Value:
//      �����������.
//  Summary:
//      ���������� ������ � ������������ � ���� 28147-89 � ������ ECB.
__deffunc void Gost28147_EncryptECB (
						  __byte* in_key32, 
						  __byte* in_data, 
						  __dword data_len, 
						  __byte* out_encrypted_data
						  );
						  
//  Description:
//		������������ ����� ������������ ���������� ����� ����������� ������ � ����������� � ���� � 34.10-2001. 
//  See Also:
//  Arguments:
//      in_priv1key32	- ���������� ��������� ����� ������� 1. 
//		in_pub2key64	- ���������� ��������� ����� ������� 2.
//		in_rand1		- ��������� �� �����, ���������� ��������� �������� ������� 1.
//		rand1_len		- ����� ��������� �������� ������� 1.
//		in_rand2		- ��������� �� �����, ���������� ��������� �������� ������� 2.
//		rand2_len		- ����� ��������� �������� ������� 2.
//		out_sk32		- ��������� �� �������� ����� ��� �������� ����������� ������������ ���������� �����.
//  Return Value:
//      __word	- ��� ������.
//  Summary:
//      ����� ������������ ���������� ����� ����������� ������ � ����������� � ���� � 34.10-2001.
__deffunc __word GostR3410_2001_KeyMatching11 (
								 __byte* in_priv1key32, 
								 __byte* in_pub2key64, 
								 __byte* in_rand1, 
								 __dword rand1_len,
								 __byte* in_rand2, 
								 __dword rand2_len,
								 __byte* out_sk32);						  

//  Description:
//		������������ ���������� ������ � ������������ � ���� 28147-89 � ������ CBC. 
//  See Also:
//		Gost28147_DecryptCBC
//  Arguments:
//      in_key32			- ���������� ����� ������, ������������� ��� ���������� ������. 
//		in_iv				- ��������� �� �����, ���������� ��������� �������� ������� �������������.
//		in_data				- ��������� �� �����, ���������� ������ ��� ����������.
//		data_len			- ����� ��������� ������.
//		out_encrypted_data	- ��������� �� �������� ��� ������������� ������.
//		out_encrypted_len   - ��������� �� ����������, ���������������� ������ ������������� ������.
//  Return Value:
//      �����������.
//  Summary:
//      ���������� ������ � ������������ � ���� 28147-89 � ������ CBC.
__deffunc void Gost28147_EncryptCBC(
								__byte* in_key32, 
								__byte* in_iv,
								__byte* in_data,
								__dword data_len,
								__byte* out_encrypted_data,
								__dword * out_encrypted_len);
								
//  Description:
//		������������ ������������� ������ � ������������ � ���� 28147-89 � ������ CBC. 
//  See Also:
//		Gost28147_Encrypt
//  Arguments:
//      in_key32			- ���������� ����� ������, ������������� ��� ������������� ������. 
//		in_iv				- ��������� �� �����, ���������� ��������� �������� ������� �������������.
//		in_data				- ��������� �� �����, ���������� ������ ��� �������������.
//		data_len			- ����� ������ ��� �������������.
//		out_decrypted_data	- ��������� �� �������� ��� �������������� ������.
//		out_decrypted_len	- ��������� �� ����������, ���������������� ������ �������������� ������.
//  Return Value:
//      __word	- ��� ������.
//  Summary:
//      ������������� ������ � ������������ � ���� 28147-89 � ������ CBC.
__deffunc __word Gost28147_DecryptCBC(
								__byte* in_key32, 
								__byte* in_iv,
								__byte* in_data,
								__dword in_data_len,
								__byte* out_decrypted_data,
								__dword * out_decrypted_len);

#endif