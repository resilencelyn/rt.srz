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

// не используется
__deffunc void DES_Encrypt (
					__byte* data_in_out8, 
					__byte* key16
				 );

// не используется
__deffunc void DES_Decrypt (
					__byte* data_in_out8, 
					__byte* key16
				 );

//  Description:
//		Осуществляет расшифрование данных в соответствии с ISO/IEC 18033-3 в режиме CBC. 
//  See Also:
//		DES3_CBC_PAD_Encrypt
//  Arguments:
//      in_key16			- Дескриптор ключа сессии, используемого для расшифрования данных. 
//		in_iv8				- Указатель на буфер, содержащий начальное значение вектора инициализации.
//		in_data				- Указатель на буфер, содержащий данные для расшифрования.
//		data_len			- Длина данных для расшифрования.
//		out_decrypted_data	- Указатель на выходной для расшифрованных данных.
//		out_decrypted_len	- Указатель на переменную, инициализируемую длиной расшифрованных данных.
//  Return Value:
//      Код ошибки.
//  Summary:
//      Расшифрование данных в соответствии с ISO/IEC 18033-3 в режиме CBC.
__deffunc __word DES3_CBC_PAD_Decrypt (
					__byte* in_key16, 
					__byte* in_iv8, 
					__byte* in_data, 
					__dword data_len, 
					__byte* out_decrypted_data, 
					__dword* out_decrypted_len
					);

//  Description:
//		Осуществляет шифрование данных в соответствии с ISO/IEC 18033-3 в режиме CBC. 
//  See Also:
//		DES3_CBC_PAD_Encrypt
//  Arguments:
//      in_key16			- Дескриптор ключа сессии, используемого для расшифрования данных. 
//		in_iv8				- Указатель на буфер, содержащий начальное значение вектора инициализации.
//		in_data				- Указатель на буфер, содержащий данные для шифрования.
//		data_len			- Длина данных для шифрования.
//		out_encrypted_data	- Указатель на выходной для зашифрованных данных.
//		out_encrypted_len	- Указатель на переменную, инициализируемую длиной зашифрованных данных.
//  Return Value:
//      Отсутствует.
//  Summary:
//      Шифрование данных в соответствии с ISO/IEC 18033-3 в режиме CBC.
__deffunc void DES3_CBC_PAD_Encrypt (
					__byte* in_key16, 
					__byte* in_iv8, 
					__byte* in_data, 
					__dword data_len, 
					__byte* out_encrypted_data, 
					__dword* out_encrypted_len
					);

//  Description:
//		Осуществляет имитозащиту данных в соответствии с ISO/IEC 9797-1. 
//  See Also:
//  Arguments:
//		in_data		- Указатель на буфер, содержащий данные для вычисления имитовставки.
//		data_len	- Длина данных для вычисления имитовставки.
//      in_key16	- Дескриптор сессионного ключа, используемого для вычисления имитовставки. 
//		out_mac4	- Указатель на выходной буфер для имитоставки.
//  Return Value:
//      Отсутствует.
//  Summary:
//      Имитозащита данных в соответствии с ISO/IEC 9797-1.
__deffunc void DES_RetailMAC4 (
					__byte* in_data, 
					__word data_len, 
					__byte* in_key16, 
					__byte* out_mac4
					);
					
//  Description:
//		Осуществляет имитозащиту данных в соответствии с ISO/IEC 9797-1. 
//  See Also:
//  Arguments:
//		in_data		- Указатель на буфер, содержащий данные для вычисления имитовставки.
//		data_len	- Длина данных для вычисления имитовставки.
//      in_key16	- Дескриптор сессионного ключа, используемого для вычисления имитовставки. 
//		out_mac4	- Указатель на выходной буфер для имитоставки.
//  Return Value:
//      Отсутствует.
//  Summary:
//      Имитозащита данных в соответствии с ISO/IEC 9797-1.
__deffunc void DES_MAC4 (
					__byte* in_data, 
					__word data_len, 
					__byte* in_key16, 
					__byte* out_mac4
					);

//  Description:
//		Осуществляет преобразование данных в соответствии с PKCS #1. 
//  See Also:
//  Arguments:
//		out_data	 - Указатель на выходной буфер, содержащий преобразованные данные.
//		out_data_len - Длина преобразованных данных.
//      in_data		 - Указатель на буфер, содержащий данные для преобразования. 
//		in_data_len  - Длина данные для преобразования.
//		in_key_mod   - Указатель на буфер, содержащий данные дескриптора модуля ключа.
//		key_mod_len	 = Длина данных дескриптора модуля ключа.
//		in_key_exp   - Указатель на буфер, содержащий данные дескриптора экспоненты ключа.
//		key_exp_len	 = Длина данных дескриптора экспоненты ключа.
//  Return Value:
//      0 или ошибка.
//  Summary:
//      Преобразование данных в соответствии с PKCS #1.
__deffunc int RSA_Block (
			__byte* out_data, 
			__word* out_data_len, 
            __byte* in_data, __word in_data_len, 
            __byte* in_key_mod, __word key_mod_len,
            __byte* in_key_exp, __word key_exp_len
			);

// Функция выполняет хэширование данных в соответствии с алгоритмом SHA-1
// msg [in] указатель на буфер, содержащий данные для хэширования
// msg_len [in]  число байтов хэшируемых данных
// hash32 [out] Указатель на буфер, через который возвращается значение функции хэширования


//  Description:
//		Осуществляет хэширование данных в соответствии с алгоритмом SHA-1. 
//  See Also:
//  Arguments:
//		in_msg	- Указатель на буфер, содержащий данные для хэширования.
//		msg_len	- Длина хэшируемых данных.
//		out_msg_digest20 - Указатель на выходной буфер с хэшированными данными.
//  Return Value:
//      Отсутствует.
//  Summary:
//      Хэширование данных в соответствии с алгоритмом SHA-1.
__deffunc void SHA1 (
		__byte* in_msg, 
		__dword msg_len, 
		__byte* out_msg_digest20
		);

#endif