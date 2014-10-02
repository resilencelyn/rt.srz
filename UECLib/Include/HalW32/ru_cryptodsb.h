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

// используется только для 1.0
__deffunc __word GostR3410_2001_GenKeyPair (
								 __byte* priv_key32, 
								 __byte* pub_key64
								 );

// используется только для 1.0
__deffunc __word GostR3410_2001_Sign (
						   __byte* msg, 
						   __dword msg_len, 
						   __byte* priv_key32, 
						   __byte* sign64
						   );
						   
//  Description:
//		Вычисляет электронную цифровую подпись передаваемых данных в соответствии с ГОСТ Р 34.10-2001.<p/> 
//  See Also:
//		GostR3410_2001_Verify11
//  Arguments:
//      in_msg		  - Указатель на буфер, содержащий данные для подписи. 
//		msg_len		  - Длина подписываемых данных.
//		in_priv_key32 - Дескриптор закрытого ключа подписи.
//		out_sign64	  - Указатель на выходной буфер для значения подписи.
//  Return Value:
//      __word		- Код ошибки.
//  Summary:
//      Получение электронной цифровой подписи ГОСТ Р 34.10-2001.
__deffunc __word GostR3410_2001_Sign11 (
						   __byte* in_msg, 
						   __dword msg_len, 
						   __byte* in_priv_key32, 
						   __byte* out_sign64
						   );

// используется только для 1.0
__deffunc __word GostR3410_2001_Verify (
							 __byte* msg, 
							 __dword msg_len, 
							 __byte* pub_key64, 
							 __byte* sign64
							 );
							 
//  Description:
//		Проверяет электронную цифровую подпись передаваемых данных в соответствии с ГОСТ Р 34.10-2001.<p/> 
//  See Also:
//		GostR3410_2001_Sign11
//  Arguments:
//      in_msg		  - Указатель на буфер, содержащий данные для подписи. 
//		msg_len		  - Длина подписываемых данных.
//		in_pub_key64 -  Дескриптор открытого ключа проверяемой подписи.
//		out_sign64	  - Указатель на выходной буфер для значения проверяемой подписи.
//  Return Value:
//      __word		- Код ошибки.
//  Summary:
//      Проверка электронной цифровой подписи ГОСТ Р 34.10-2001.
__deffunc __word GostR3410_2001_Verify11 (
							 __byte* in_msg, 
							 __dword msg_len, 
							 __byte* in_pub_key64, 
							 __byte* sign64
							 );

// используется только для 1.0
__deffunc void GostR3410_2001_CompressPublicKey (
									  __byte* pub_key64, 
									  __byte* pub_key33_compressed
									  );

// используется только для 1.0
__deffunc __word GostR3410_2001_KeyMatching (
								  __byte* priv1key32, 
								  __byte* pub2key64, 
								  __byte* sk64
								  );

//  Description:
//		Осуществляет шифрование данных в соответствии с ГОСТ 28147-89. 
//  See Also:
//		Gost28147_Decrypt
//  Arguments:
//      in_key32			- Дескриптор ключа сессии, используемого для шифрования данных. 
//		in_data				- Указатель на буфер, содержащий данные для шифрования.
//		data_len			- длина шифруемых данных.
//		out_encrypted_data	- Указатель на выходной для зашифрованных данных.
//		in_iv8				- Указатель на буфер, содержащий начальное значение вектора инициализации.
//  Return Value:
//      Отсутствует.
//  Summary:
//      Шифрование данных в соответствии с ГОСТ 28147-89.
__deffunc void Gost28147_Encrypt (
					   __byte* in_key32, 
					   __byte* in_data, 
					   __dword data_len, 
					   __byte* out_encrypted_data, 
					   __byte* in_iv8
					   );

//  Description:
//		Осуществляет расшифрование данных в соответствии с ГОСТ 28147-89. 
//  See Also:
//		Gost28147_Encrypt
//  Arguments:
//      in_key32			- Дескриптор ключа сессии, используемого для расшифрования данных. 
//		in_data				- Указатель на буфер, содержащий данные для расшифрования.
//		data_len			- Длина данных для расшифрования.
//		out_decrypted_data	- Указатель на выходной для расшифрованных данных.
//		in_iv8				- Указатель на буфер, содержащий начальное значение вектора инициализации.
//  Return Value:
//      Отсутствует.
//  Summary:
//      Расшифрование данных в соответствии с ГОСТ 28147-89.
__deffunc void Gost28147_Decrypt (
					   __byte* key32, 
					   __byte* data, 
					   __dword data_len, 
					   __byte* decrypted_data, 
					   __byte* iv8
					   );

//  Description:
//		Осуществляет имитозащиту данных в соответствии с ГОСТ 28147-89. 
//  See Also:
//  Arguments:
//      in_key		- Дескриптор сессионного ключа, используемого для вычисления имитовставки. 
//		in_data		- Указатель на буфер, содержащий данные для вычисления имитовставки.
//		data_len	- Длина данных для вычисления имитовставки.
//		out_imit4	- Указатель на выходной буфер для имитоставки.
//  Return Value:
//      Отсутствует.
//  Summary:
//      Имитозащита данных в соответствии с ГОСТ 28147-89.
__deffunc void Gost28147_Imit (
					__byte* in_key, 
					__byte* in_data, 
					__dword data_len, 
					__byte* out_imit4
					);

//  Description:
//		Осуществляет Хэширование данных в соответствии с ГОСТ Р 34.11-94. 
//  See Also:
//  Arguments:
//      in_msg		- Указатель на буфер, содержащий данные для хэширования. 
//		msg_len		- Длина хэшируемых данных.
//		out_hash32	- Указатель на выходной буфер со значением функции хэширования.
//  Return Value:
//      Отсутствует.
//  Summary:
//      Хэширование данных в соответствии с ГОСТ Р 34.11-94.
__deffunc void GostR3411_94_Hash (
					   __byte* in_msg, 
					   __dword msg_len, 
					   __byte* out_hash32
					   );

//  Description:
//		Осуществляет шифрование данных в соответствии с ГОСТ 28147-89 в режиме ECB. 
//  See Also:
//  Arguments:
//      in_key32			- Дескриптор ключа сессии, используемого для шифрования данных. 
//		in_data				- Указатель на буфер, содержащий данные для шифрования.
//		data_len			- длина шифруемых данных.
//		out_encrypted_data	- Указатель на выходной для зашифрованных данных.
//  Return Value:
//      Отсутствует.
//  Summary:
//      Шифрование данных в соответствии с ГОСТ 28147-89 в режиме ECB.
__deffunc void Gost28147_EncryptECB (
						  __byte* in_key32, 
						  __byte* in_data, 
						  __dword data_len, 
						  __byte* out_encrypted_data
						  );
						  
//  Description:
//		Осуществляет вывод разделяемого секретного ключа сессионного обмена в сооветствии с ГОСТ Р 34.10-2001. 
//  See Also:
//  Arguments:
//      in_priv1key32	- Дескриптор закрытого ключа стороны 1. 
//		in_pub2key64	- Дескриптор открытого ключа стороны 2.
//		in_rand1		- Указатель на буфер, содержащий случайную величину стороны 1.
//		rand1_len		- Длина случайной величины стороны 1.
//		in_rand2		- Указатель на буфер, содержащий случайную величину стороны 2.
//		rand2_len		- Длина случайной величины стороны 2.
//		out_sk32		- Указатель на выходной буфер для значение дескриптора разделяемого секретного ключа.
//  Return Value:
//      __word	- Код ошибки.
//  Summary:
//      Вывод разделяемого секретного ключа сессионного обмена в сооветствии с ГОСТ Р 34.10-2001.
__deffunc __word GostR3410_2001_KeyMatching11 (
								 __byte* in_priv1key32, 
								 __byte* in_pub2key64, 
								 __byte* in_rand1, 
								 __dword rand1_len,
								 __byte* in_rand2, 
								 __dword rand2_len,
								 __byte* out_sk32);						  

//  Description:
//		Осуществляет шифрование данных в соответствии с ГОСТ 28147-89 в режиме CBC. 
//  See Also:
//		Gost28147_DecryptCBC
//  Arguments:
//      in_key32			- Дескриптор ключа сессии, используемого для шифрования данных. 
//		in_iv				- Указатель на буфер, содержащий начальное значение вектора инициализации.
//		in_data				- Указатель на буфер, содержащий данные для шифрования.
//		data_len			- длина шифруемых данных.
//		out_encrypted_data	- Указатель на выходной для зашифрованных данных.
//		out_encrypted_len   - Указатель на переменную, инициализируемую длиной зашифрованных данных.
//  Return Value:
//      Отсутствует.
//  Summary:
//      Шифрование данных в соответствии с ГОСТ 28147-89 в режиме CBC.
__deffunc void Gost28147_EncryptCBC(
								__byte* in_key32, 
								__byte* in_iv,
								__byte* in_data,
								__dword data_len,
								__byte* out_encrypted_data,
								__dword * out_encrypted_len);
								
//  Description:
//		Осуществляет расшифрование данных в соответствии с ГОСТ 28147-89 в режиме CBC. 
//  See Also:
//		Gost28147_Encrypt
//  Arguments:
//      in_key32			- Дескриптор ключа сессии, используемого для расшифрования данных. 
//		in_iv				- Указатель на буфер, содержащий начальное значение вектора инициализации.
//		in_data				- Указатель на буфер, содержащий данные для расшифрования.
//		data_len			- Длина данных для расшифрования.
//		out_decrypted_data	- Указатель на выходной для расшифрованных данных.
//		out_decrypted_len	- Указатель на переменную, инициализируемую длиной расшифрованных данных.
//  Return Value:
//      __word	- Код ошибки.
//  Summary:
//      Расшифрование данных в соответствии с ГОСТ 28147-89 в режиме CBC.
__deffunc __word Gost28147_DecryptCBC(
								__byte* in_key32, 
								__byte* in_iv,
								__byte* in_data,
								__dword in_data_len,
								__byte* out_decrypted_data,
								__dword * out_decrypted_len);

#endif