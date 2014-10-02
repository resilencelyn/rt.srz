#ifndef _HAL_COMMON_H_
#define _HAL_COMMON_H_

#include "il_types.h"
#include "Convert.h"
#include <stdlib.h> //+++

// Преобразует значение 'x' в символ кода ASCII 
#define DIG2ASC(x)  ( ((x)<10) ? (48+(x)) : (55+(x)) )
// Преобразует 'x' символ кода ASCII в значение байта  
#define ASC2DIG(x)  ( ((x)<65) ? ((x)-48) : ((x)-55) )
// Возвращает не нуль, если 'x' код шестнадцатиричной цифры, и нуль в противном случае
#define ISXDIGIT(x) ( ( ((x)>=0x30) && ((x)<=0x39) ) || ( ((x)>=0x41) && ((x)<=0x46) ) || ( ((x)>=0x61) && ((x)<=0x7A) ) )
// Преобразует код латинской буквы к верхнему регистру, остальные коды не изменяются
#define TOUPPER(x)  ( ( ((x)>=0x61) && ((x)<=0x7A) ) ? ((x)-0x20) : (x) )

//  Description:
//      Сравнивает первые n-символов массивов, адресуемых параметрами Buf1 и Buf2.
//  See Also:
//  Arguments:
//      Buf1 - Указатель на первый сравниваемый массив. 
//		Buf2 - Указатель на второй сравниваемый массив.
//		n	 - Количество сравниваемых байт.
//  Return Value:
//      Результат сравнения:<p/>
//		  <0 - Buf1 меньше Buf2.<p/>
//		  0  - Buf1 равен Buf2.<p/>
//		  >0 - Buf1 больше Buf2. 
//  Summary:
//      Сравнение массивов данных.
IL_FUNC	IL_INT		cmnMemCmp (const IL_BYTE *Buf1, const IL_BYTE *Buf2, IL_WORD n);

//  Description:
//      Копирует байты строки Src в строку Dest.
//  See Also:
//  Arguments:
//      Dest - Указатель на результирующую строку. 
//		Src  - Указатель на копируемую строку.
//  Return Value:
//      Указатель на результирующую строку.
//  Summary:
//      Копирование одной строки в другую.
IL_FUNC	IL_CHAR*	cmnStrCopy (IL_CHAR *Dest, const IL_CHAR *Src);

//  Description:
//      Сравнивает строки Str1 и Str2.
//  See Also:
//  Arguments:
//      Str1 - Указатель на первую сравниваемую строку. 
//		Str2 - Указатель на вторую сравниваемую строку.
//  Return Value:
//      Результат сравнения:<p/>
//		  <0 - Str1 меньше Str2.<p/>
//		  0  - Str1 равна Str2.<p/>
//		  >0 - Str1 больше Str2. 
//  Summary:
//      Сравнение строк.
IL_FUNC	IL_INT		cmnStrCmp (const IL_CHAR *Str1, const IL_CHAR *Str2);

//  Description:
//      Присоединяет строку Src к строке Dest.
//  See Also:
//  Arguments:
//      Dest - Указатель на результирующую строку. 
//		Src  - Указатель на присодиняемую строку.
//  Return Value:
//      Указатель на результирующую строку.
//  Summary:
//      Конкатенация строк.
IL_FUNC	IL_CHAR*	cmnStrCat (IL_CHAR *Dest, const IL_CHAR *Src); 

//  Description:
//      Вычисляет длину строки Src.
//  See Also:
//  Arguments:
//		Src  - Указатель на строку.
//  Return Value:
//      Длина строки.
//  Summary:
//      Определение длины строки.
IL_FUNC	IL_WORD		cmnStrLen (const IL_CHAR *Src);

//  Description:
//      Инициализирует первые n-байт массива Dest указанным значением.
//  See Also:
//  Arguments:
//      Dest - Указатель на инициализируемый массив. 
//		c	 - Инициализирующее значение.
//		n	 - Количество инициализируемых байт.
//  Return Value:
//      Указатель на инициализируемую строку.
//  Summary:
//      Инициализация массива данных.
IL_FUNC	IL_BYTE*	cmnMemSet (IL_BYTE *Dest, IL_INT c, IL_WORD n);

//  Description:
//      Копирует первые n-байт массива Src в массив Dest.<p/>
//		Если указанные массивы пересекаются, процесс копирования проходит успешно.
//  See Also:
//  Arguments:
//      Dest - Указатель на результирующий массив. 
//		Src  - Указатель на копируемый массив.
//		n	 - Количество копируемых байт.
//  Return Value:
//      Указатель на результирующий массив.
//  Summary:
//      Копирование массива данных.
IL_FUNC	IL_BYTE*	cmnMemCopy (IL_BYTE *Dest, const IL_BYTE *Src, IL_WORD n);

//  Description:
//      Перемещает первые n-байт массива Src в массив Dest.<p/>
//		Если указанные массивы пересекаются, процесс копирования проходит успешно.
//  See Also:
//  Arguments:
//      Dest - Указатель на результирующий массив. 
//		Src  - Указатель на перемещамый массив.
//		n	 - Количество перемещаемых байт.
//  Return Value:
//      Указатель на результирующий массив.
//  Summary:
//      Перемещение массивов данных.
IL_FUNC	IL_BYTE*	cmnMemMove(IL_BYTE *Dest, const IL_BYTE *Src, IL_WORD n);

//  Description:
//      Инициализирует первые n-байт массива Dest нулевым значением.
//  See Also:
//  Arguments:
//      Dest - Указатель на обнуляемый массив. 
//		n	 - Количество обнуляемых байт.
//  Return Value:
//      Указатель на обнуляемый массив.
//  Summary:
//      Обнуление массива данных.
IL_FUNC	IL_BYTE*	cmnMemClr (IL_BYTE *Dest, IL_WORD n);

//  Description:
//      Выделяет блок динамически распределяемой памяти указанной длины.
//  See Also:
//		cmnMemFree
//  Arguments:
//      Size - Размер выделяемой памяти в байтах. 
//  Return Value:
//      Указатель на выделенный блок памяти или NULL при неудачном завершении.
//  Summary:
//      Выделение блока динамической памяти.
IL_FUNC IL_BYTE*    cmnMemAlloc(IL_WORD Size);

//  Description:
//      Освобождает ранее выделенный блок динамически распределяемой памяти.
//  See Also:
//		cmnMemAlloc
//  Arguments:
//      Buf - Указатель на освобождаемый блок динамической памяти. 
//  Return Value:
//      Указатель на выделенный блок памяти или NULL при неудачном завершении.
//  Summary:
//      Освобождение блока динамической памяти.
IL_FUNC void		cmnMemFree(void *Buf);



#endif//_HAL_COMMON_H_