#ifndef _CONVERT_H_
#define _CONVERT_H_

#include "il_types.h"

//  Description:
//      Конвертирует строку данных HEX-формата в бинарный массив.<p/>
//		Переполнение выходного буфера не контролируется. 
//  See Also:
//      bin2hex
//  Arguments:
//      in_strHex	- Указатель на строку данных в HEX-формате.
//      out_pBin	- Указатель на буфер для выходных бинарных данных.
//      out_pBinLen	- Длина выходных данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Конвертация Hex-строки в бинарный массив.
IL_FUNC IL_RETCODE hex2bin(IL_CHAR *in_strHex, IL_BYTE *out_pBin, IL_DWORD *out_pBinLen);

//  Description:
//      Конвертирует массив бинарных данных в Hex-строку.<p/>
//		Переполнение буфера выходной строки не контролируется. 
//  See Also:
//      bin2hex
//  Arguments:
//      out_strHex	- Указатель на выходную строку данных в HEX-формате.
//      in_pBin		- Указатель на входной буфер бинарных данных.
//      out_pBinLen	- Длина входных данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Конвертация массива бинарных данных в Hex-строку.
IL_FUNC IL_CHAR* bin2hex(IL_CHAR *out_strHex, IL_BYTE *in_pBin, IL_DWORD in_BinLen);

//  Description:
//      Конвертирует строку символов из формата хранения символьной информации карты УЭК ISO-8859 в строку Win-1251.
//  See Also:
//      Ansi_2_Iso8859
//  Arguments:
//      in_str8859	- Указатель на входную строку в кодировке ISO-8859.
//      out_str1251	- Указатель на выходную строку в кодировке Win-1251.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Конвертация строки формата ISO-8859 в строку Win-1251.
IL_FUNC IL_RETCODE Iso8859_2_Ansi(IL_CHAR *in_str8859, IL_CHAR *out_str1251);

//  Description:
//      Конвертирует строку символов Win-1251 в строку формата хранения символьной информации на карте УЭК ISO-8859.
//  See Also:
//      Iso8859_2_Ansi
//  Arguments:
//      in_str1251	- Указатель на входную строку в кодировке Win-1251.
//      out_str8859	- Указатель на выходную строку в кодировке ISO-8859.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Конвертация строки Win-1251 в строку формата ISO-8859.
IL_FUNC IL_RETCODE Ansi_2_Iso8859(IL_CHAR *in_str1251, IL_CHAR *out_str8859);

//  Description:
//      Конвертирует бинарный массив данных BCD-формата в строку Win-1251.<p/>
//		Переполнение буфера выходной строки не контролируется. 
//  See Also:
//		str2bcdF
//		str2bcd0		
//      toBcd
//  Arguments:
//      in_pBcd		- Указатель на входной массив данных в BCD-формате.
//      in_BcdLen	- Длина входного массива данных.
//      out_Str		- Указатель на выходную строку Win-1251.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Конвертация массива BCD-формата в строку.
IL_FUNC void bcd2str(IL_BYTE *in_pBcd, IL_WORD in_BcdLen, IL_CHAR *out_Str);

//  Description:
//      Конвертирует строку Win-1251 в бинарный массив BCD-формата и дополняет справа заполнителем 'F' до необходимой длины.<p/>
//		Переполнение буфера выходного массива не контролируется. 
//  See Also:
//		bcd2str
//		str2bcd0
//      toBcd
//  Arguments:
//      in_Str		- Указатель на входную строку Win-1251.
//      out_pBcd	- Указатель на выходной массив данных в BCD-формате.
//      maxLen		- Необходимая длина формируемого выходного массива.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Конвертация строки в массив BCD-формата с дополнением справа заполнителем 'F'.
IL_FUNC void str2bcdF(IL_CHAR *in_Str, IL_BYTE *out_pBcd, IL_WORD maxLen);

//  Description:
//      Конвертирует строку Win-1251 в бинарный массив BCD-формата и дополняет слева заполнителем '0' до необходимой длины.<p/>
//		Переполнение буфера выходного массива не контролируется. 
//  See Also:
//		bcd2str
//		str2bcdF
//      toBcd
//  Arguments:
//      in_Str		- Указатель на входную строку Win-1251.
//      out_pBcd	- Указатель на выходной массив данных в BCD-формате.
//      maxLen		- Необходимая длина формируемого выходного массива.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Конвертация строки в массив BCD-формата с дополнением слева заполнителем '0'.
IL_FUNC void str2bcd0(IL_CHAR *str, IL_BYTE *bcd, IL_WORD max_len);

//  Description:
//      Конвертирует байт в BCD-формат.
//  See Also:
//      bcd2str
//		str2bcdF
//		str2bcd0		
//  Arguments:
//      b	- Указатель на конвертируемый байт.
//  Return Value:
//      IL_BYTE	- BCD-зачение.
//  Summary:
//      Конвертация массива BCD-формата в строку.
IL_FUNC IL_BYTE toBcd(IL_BYTE b);

//  Description:
//      Конвертирует байта формата хранения пола держателя карты УЭК ISO-5218 в символ Win-1251.
//  See Also:
//      Ansi_2_Iso5218
//  Arguments:
//      in_p5218	- Указатель на байт в кодировке ISO-5218.
//      out_p1251	- Указатель на выходной символ в кодировке Win-1251.
//  Return Value:
//      Отсутствует.
//  Summary:
//      Конвертация байт формата ISO-5218 в символ Win-1251.
IL_FUNC void Iso5218_2_Ansi(IL_BYTE *in_p5218, IL_CHAR *out_p1251);

//  Description:
//      Конвертирует символ Win-1251 в байт формата хранения пола держателя карты УЭК ISO-5218.
//  See Also:
//      Iso5218_2_Ansi
//  Arguments:
//      in_p1251	- Указатель на входной символ в кодировке Win-1251.
//      out_p5218	- Указатель на выходной байт в кодировке ISO-5218.
//  Return Value:
//      Отсутствует.
//  Summary:
//      Конвертация символа Win-1251 в байт формата ISO-5218.
IL_FUNC void Ansi_2_Iso5218(IL_CHAR *in_p1251, IL_BYTE *out_p5218);

//  Description:
//      Конвертирует строку пароля Win-1251 в ПИН-блок ISO 9564-3:2002 (Format 2).
//  See Also:
//  Arguments:
//      in_Pin1251		- Указатель на входную строку пароля Win-1251.
//      out_pPin9564	- Указатель на выходной ПИН-блок в формате ISO 9564-3:2002 (Format 2).
//  Return Value:
//      Отсутствует.
//  Summary:
//      Конвертация строку пароля Win-1251 в ПИН-блок ISO 9564.
IL_FUNC void str2pin(IL_CHAR *in_Pin1251, IL_BYTE *out_pPin9564);

//  Description:
//      Конвертирует 4 байта бинарного массива в DWORD-значение нотации INTEL.
//  See Also:
//		dw2bin
//  Arguments:
//      in_pBin	- Указатель на бинарный массив.
//      out_pDw	- Указатель на инициализируемую DWORD-значением переменную.
//  Return Value:
//      Указатель на следующий за конвертированными данными байт массива.
//  Summary:
//      Конвертация бинарных данных в DWORD-значение.
IL_FUNC IL_BYTE* bin2dw(IL_BYTE *in_pBin, IL_DWORD *out_pDw);

//  Description:
//      Конвертирует DWORD-значение в 4 байта бинарного массива в нотации INTEL.
//  See Also:
//		bin2dw
//  Arguments:
//      in_Dw		- DWORD-переменная.
//      out_pBin	- Указатель на бинарный массив.
//  Return Value:
//      Указатель на следующий за конвертированными данными байт массива.
//  Summary:
//      Конвертация бинарных данных в DWORD-значение.
IL_FUNC IL_BYTE* dw2bin(IL_DWORD in_Dw, IL_BYTE *out_pBin);

//  Description:
//      Изменяет порядок следования байт в бинарном массиве.
//		В результате преобразования первый байт массива переставляется на место последнего, последний на место первого и т.д.
//  See Also:
//  Arguments:
//      in_pBin		- Указатель на бинарный массив.
//      out_pBin	- Длина переставляемых данных.
//  Return Value:
//      Отсутствует.
//  Summary:
//      Изменение порядка следования байт бинарного массива.
IL_FUNC void swap(IL_BYTE *in_pBin, IL_DWORD DataLen);

//IL_FUNC IL_BYTE* bin2w(IL_BYTE *bin, IL_WORD *w);
//IL_FUNC IL_BYTE* w2bin(IL_WORD w, IL_BYTE *bin);

#endif//_CONVERT_H_