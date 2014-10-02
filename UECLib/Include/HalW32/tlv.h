#ifndef _TLV_H_
#define _TLV_H_

#include "il_types.h"

//  Description:
//      Осуществляет вложенный поиск TLV-элемента в массиве TLV-данных по его полному пути.
//  See Also:
//		TagFind
//  Arguments:
//      in_pData	  - Указатель на буфер TLV-данных, в котором осуществляется поиск TLV-элемента. 
//		MaxLen		  - Длина, определяющая максимальный диапазон поиска.
//		in_pTagPath	  - Указатель на массив вложенных идентификаторов тегов, определяющих полный путь до искомого TLV-элемента.
//		NumTags		  - Размер массива вложенных идентификаторов тегов.
//		out_pDataLen  - Указатель на переменную, инициализируемую длиной найденного TLV-элемента в зависимости от флага 'ifWithTag'.
//		out_ppTagData - Инициализируемый указатель на найденный TLV-элемент или NULL при отрицательном результате поиска.
//		ifWithTag	  - Признак инициализации указателя на заголовок найденного TLV-элемента или его значение.   	
//  Return Value:
//      IL_RETCODE - Код ошибки.
//  Summary:
//      Вложенный поиск TLV-элемента в массиве TLV-данных по его полному пути.
IL_FUNC IL_RETCODE TagFindByPath(IL_BYTE *in_pData, IL_DWORD MaxLen, const IL_TAG *in_pTagPath, IL_DWORD NumTags, IL_DWORD *out_pDataLen, IL_BYTE **out_ppTagData, IL_BYTE ifWithTag);

//  Description:
//      Добавляет TLV-элемент к массиву TLV-данных.
//  See Also:
//  Arguments:
//      TagId	  - Идентификатор тега добавляемого TLV-элемента. 
//		in_pData  - Указатель на буфер с данными значения TLV-элемента.
//		DataLen	  - Длина данных значения TLV-элемента.
//		out_pData - Указатель на буфер к которому добавляется TLV-элемент.
//  Return Value:
//      Длина добавленного TLV-элемента.
//  Summary:
//      Добавление TLV-элемента к массиву TLV-данных. 
IL_FUNC IL_DWORD AddTag(const IL_TAG TagId, IL_BYTE *in_pData, IL_DWORD DataLen, IL_BYTE *out_pData);

//  Description:
//      Осуществляет поиск TLV-элемента в массиве TLV-данных по его идентификатору.
//  See Also:
//		TagFindByPath
//  Arguments:
//      in_pData	  - Указатель на буфер TLV-данных, в котором осуществляется поиск TLV-элемента. 
//		MaxLen		  - Длина, определяющая максимальный диапазон поиска.
//		TagId		  - Идентификатор тега искомого TLV-элемента.
//		out_pDataLen  - Указатель на переменную, инициализируемую длиной найденного TLV-элемента в зависимости от флага 'ifWithTag'.
//		out_ppTagData - Инициализируемый указатель на найденный TLV-элемент или NULL при отрицательном результате поиска.
//		ifWithTag	  - Признак инициализации указателя на заголовок найденного TLV-элемента или его значение.   	
//  Return Value:
//      IL_RETCODE - Код ошибки.
//  Summary:
//      Поиск TLV-элемента в массиве TLV-данных по его идентификатору.
IL_FUNC IL_RETCODE TagFind(IL_BYTE *in_pData, IL_DWORD MaxLen, const IL_TAG TagId, IL_DWORD *out_pDataLen, IL_BYTE **out_ppTagData, IL_BYTE ifWithTag);

//  Description:
//      Возвращает размер поля идентификатора TLV-элемента.
//  See Also:
//		GetLenLen
//		GetDataLen
//  Arguments:
//		in_pTlvElem	- Указатель на TLV-элемент.
//  Return Value:
//      Размер поля идентификатора TLV-элемента в байтах.
//  Summary:
//      Получение размера поля идентификатора TLV-элемента.
IL_FUNC IL_DWORD GetTagLen(IL_BYTE *in_pTlvElem);

//  Description:
//      Возвращает размер поля длины значения TLV-элемента.
//  See Also:
//		GetTagLen
//		GetDataLen
//  Arguments:
//		in_pTlvElem	- Указатель на TLV-элемент.
//  Return Value:
//      Размер поля длины значения TLV-элемента.
//  Summary:
//      Получение размера поля длины значения TLV-элемента.
IL_FUNC IL_DWORD GetLenLen(IL_BYTE *in_pTlvElem);

//  Description:
//      Возвращает размер поля данных TLV-элемента.
//  See Also:
//		GetTagLen
//		GetLenLen
//  Arguments:
//		in_pTlvElem	- Указатель на TLV-элемент.
//  Return Value:
//      Размер поля данных TLV-элемента в байтах.
//  Summary:
//      Получение размера поля данных TLV-элемента.
IL_FUNC IL_DWORD GetDataLen(IL_BYTE *in_pTlvElem);

//+++
IL_FUNC IL_RETCODE GetTagOffsetByPath(IL_BYTE *in_pDdata, IL_DWORD MaxLen, const IL_TAG *pTagPath, IL_DWORD NumTags, IL_WORD *out_Offset, IL_DWORD *out_pdwLen, IL_BYTE ifWithTag);



IL_FUNC IL_CHAR* DataToHexStr(IL_CHAR* str, IL_BYTE* data, IL_WORD data_len);
IL_FUNC IL_DWORD GetTag(IL_BYTE* data);
IL_FUNC IL_BYTE* TagParse(IL_BYTE* data, IL_BYTE level, IL_DWORD maxlen);

#endif