#ifndef _HAL_SCREADER_H_
#define _HAL_SCREADER_H_

#include "HAL_SCRHandle.h"
#include "HAL_SCRApdu.h"

//  Description:
//      Инициализирует смарт-карт ридер.
//  See Also:
//		crDeinit
//  Arguments:
//      pilRdrHandle	- Указатель на нетипизированный описатель карт-ридера. 
//						  В теле функции необходимо предусмотреть явное приведение этого указателя к используемому в конкретной реализацци типу описателя. 
//      ilRdrSettings	- Указатель на нетипизированные установочные параметры карт-ридера.
//						  В теле функции необходимо предусмотреть явное приведение этого указателя к используемому в конкретной реализацци типу. 	
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Инициализация смарт-карт ридера.
IL_FUNC IL_RETCODE crInit(IL_HANDLE_READER pilRdrHandle, IL_READER_SETTINGS ilRdrSettings);

//  Description:
//      Открывает сессию работы с картой.
//  See Also:
//		crCloseSession
//  Arguments:
//      pilRdrHandle	- Указатель на нетипизированный описатель карт-ридера. 
//						  В теле функции необходимо предусмотреть явное приведение этого указателя к используемому в конкретной реализацци типу описателя. 
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Открытие сессии работы с картой.
IL_FUNC IL_RETCODE crOpenSession(IL_HANDLE_READER pilRdrHandle);

//  Description:
//      Выполненяет APDU команду.
//  See Also:
//		crDeinit
//  Arguments:
//      pilRdrHandle - Указатель на нетипизированный описатель карт-ридера. 
//					   В теле функции необходимо предусмотреть явное приведение этого указателя к используемому в конкретной реализацци типу описателя. 
//      pilApdu		 - Указатель на описатель выполняемой APDU команды.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполнение APDU команды.
IL_FUNC IL_RETCODE crAPDU(IL_HANDLE_READER pilRdrHandle, IL_APDU *pilApdu);

//  Description:
//      Завершает сессию работы с картой.
//  See Also:
//		crOpenSession
//  Arguments:
//      pilRdrHandle	- Указатель на нетипизированный описатель карт-ридера. 
//						  В теле функции необходимо предусмотреть явное приведение этого указателя к используемому в конкретной реализацци типу описателя. 
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Завершение сессии работы с картой.
IL_FUNC IL_RETCODE crCloseSession(IL_HANDLE_READER pilRdrHandle);

//  Description:
//      Деинициализирует смарт-карт ридер.
//  See Also:
//		crInit
//  Arguments:
//      pilRdrHandle	- Указатель на нетипизированный описатель карт-ридера. 
//						  В теле функции необходимо предусмотреть явное приведение этого указателя к используемому в конкретной реализацци типу описателя. 
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Деинициализация смарт-карт ридера.
IL_FUNC IL_RETCODE crDeinit(IL_HANDLE_READER pilRdrHandle);

#endif