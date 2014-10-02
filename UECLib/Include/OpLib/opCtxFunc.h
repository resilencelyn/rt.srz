/**  Модуль содержит функции доступа к публичным свойствам контекста системы.
  */
#ifndef __OP_CTXFUNC_H_
#define __OP_CTXFUNC_H_ 

#ifdef __cplusplus
extern "C" {
#endif

//  Description:
//      Устанавливает в контекст ИБТ указатель на функцию отображения текстовой строки на экран дисплея. 
//		Применяется для вывода диагностических сообщений в контрольных примерах ИБТ.
//  See Also:
//      
//  Arguments:
//      p_opContext		- Указатель на контекст ИБТ.
//		(*pf)(IL_CHAR*) - Указатель на функцию отображения текстовой строки.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ указателя на функцию отображения строки на экран дисплея.
IL_FUNC IL_WORD opCtxSetDisplayTextFunc(s_opContext *p_opContext, void (*pf)(IL_CHAR*));         

//  Description:
//      Очищает контекст ИБТ. 
//		Применяется непосредственно перед инициализацией операции ИБТ.
//		Встроена в функцию инициализации операции линейного интерфейса 'opApiInitOperation'
//  See Also:
//      opApiInitOperation
//  Arguments:
//      p_opContext		- Указатель на контекст ИБТ.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Очистка контекста ИБТ.
IL_FUNC IL_WORD opCtxSetClean (s_opContext *p_opContext);

//  Description:
//      Устанавливает в контекст ИБТ указатель на дескриптор карты УЭК. 
//		Данный указатель используется ИБТ при обращениях к карте УЭК. 
//  See Also:
//		IL_CARD_HANDLE    
//  Arguments:
//      p_opContext		- Указатель на контекст ИБТ.
//		phCrd			- Указатель на дескриптор карты УЭК.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ указателя на дескриптор карты УЭК.
IL_FUNC IL_WORD opCtxSetCardReaderHandler(s_opContext *p_opContext, IL_CARD_HANDLE *phCrd);

//  Description:
//      Устанавливает в контекст ИБТ указатель на TLV-данные идентификатора типа прикладного сервиса/услуги ('9F15').<p/> 
//		Эти данные используются ИБТ при формировании запроса на аутентификацию ИД-Приложения.
//  See Also:
//      opApiPrepareAppAuthRequest
//  Arguments:
//      p_opContext		- Указатель на контекст ИБТ.
//		in_pMetaInfo	- Указатель на данные идентификатора типа прикладного сервиса/услуги.
//		in_MetaInfoLen	- Длина устанавливаемых данных.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ указателя на данные метаинформации об услуге.
IL_FUNC IL_WORD opCtxSetMetaInfo(s_opContext *p_opContext, IL_BYTE *in_pMetaInfo, IL_WORD in_MetaInfoLen);

//  Description:
//      Устанавливает в контекст ИБТ ПИН-блок предъявляемого на карту УЭК пароля.<p/> 
//		Автоматически преобразует входную строку со значением пароля из WIN-1251 в ПИН-блок в стандарте ISO 9564-3:2002 (Format 2).<p/>
//		Номер/тип предъявляемого пароля зависит от контекста выполняемой операции.
//  See Also:
//      
//  Arguments:
//      p_opContext		- Указатель на контекст ИБТ.
//		in_strPin		- Указатель на строку со значением пароля.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ ПИН-блока предъявляемого на карту УЭК пароля.
IL_FUNC IL_WORD opCtxSetPinBlock(s_opContext *p_opContext, IL_CHAR *in_strPin);

//  Description:
//		Извлекает из контекста ИБТ количество оставшихся попыток предъявления на карту УЭК пароля.<p/>
//		Имеет смысл только при получении ошибки карты 'ILRET_CRD_VERIFY_WRONG_PASSWORD_PRESENTED'.
//  See Also:
//      
//  Arguments:
//      p_opContext		 - Указатель на контекст ИБТ.
//		out_PinTriesLeft - Указатель на переменную c извлекаемым значением оставшихся попыток предъявления пароля.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Извлечение из контекста ИБТ количества оставшихся попыток предъявления на карту УЭК пароля.
IL_FUNC IL_WORD opCtxGetPinTriesLeft(s_opContext *p_opContext, IL_BYTE *out_PinTriesLeft);

//  Description:
//      Устанавливает в контекст ИБТ номер/тип предъявляемого на карту УЭК пароля. 
//  See Also:
//      KeyType.h
//  Arguments:
//      p_opContext		- Указатель на контекст ИБТ.
//		in_PinNum		- Номер/Тип предъявляемого пароля.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ номера/типа предъявляемого пароля.
IL_FUNC IL_WORD opCtxSetPinNum(s_opContext *p_opContext, IL_BYTE in_PinNum);

//  Description:
//      Извлекает из контекста ИБТ номер/тип предъявляемого пароля. 
//  See Also:
//      
//  Arguments:
//      p_opContext		- Указатель на контекст ИБТ.
//		out_PinNum		- Указатель на извлекаемое значение номера/типа пароля.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Извлечение из контекста ИБТ номера/типа предъявляемого пароля.
IL_FUNC IL_WORD opCtxGetPinNum(s_opContext *p_opContext, IL_BYTE *out_PinNum);

//  Description:
//      Устанавливает в контекст ИБТ дополнительные сведения об операции. 
//		Эти данные используются ИБТ при формировании запроса на аутентификацию ИД-Приложения.
//  See Also:
//      opApiPrepareAppAuthRequest
//  Arguments:
//      p_opContext		- Указатель на контекст ИБТ.
//		in_pExtraData	- Указатель на буфер с дополнительными сведениями об операции.
//		in_ExtraDataLen - Длина дополнительными сведений об операции.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ дополнительных сведений об операции.
IL_FUNC IL_WORD opCtxSetExtraData(s_opContext *p_opContext, IL_BYTE *in_pExtraData, IL_WORD in_ExtraDataLen);

//  Description:
//      Устанавливает в контекст ИБТ параметры запроса на получение с карты данных по оказываемой услуге.<p/>
//		Используется в автоматическом интерфейсе при получении событий 'E_CARD_DATA_REQUESTED' или 'E_CARD_DATA_EDIT_REQUSTED'.<p/> 
//		Данные могут считываться с карты либо в "сыром" формате, либо автоматически конвертироваться в соответствии с параметрами во внешнем файле описателем данных 'sectors.ini'. 
//  See Also:
//      opApiReadCardData
//  Arguments:
//      p_opContext		 - Указатель на контекст ИБТ.
//		in_CardDataDescr - Указатель на строку-описатель считываемых с карты данных в формате:<p/>
//		<p/>"[элемент1];[элемент2];...[элементN];"<p/>
//		<p/>Элементы считываемых данных задаются в формате:<p/>
//		<p/>"[~|x]S-B-D[-L]" , где:
//		- 'x' - Необязательный спецификатор для чтения "сырых" данных из бинарного файла. Считанные данные в этом случае возвращаются в виде HEX-строки.
//		- '~' - Необязательный спецификатор для чтения "сырых" данных из TLV-файла. Считанные данные в этом случае возвращаются в виде HEX-строки.
//		- 'S' - Идентификатор сектора считываемых данных.
//		- 'B' - Идентификатор блока считываемых данных.
//		- 'D' - Идентификатор считываемого элемента данных. При чтении из бинарного файла указывается десятичное значение смещения считываемого элемента данных от начала файла. При чтении из TLV-файла указывается шестнадцатиричное значение тега считываемого элемента данных.
//		- 'L' - Необязательный спецификатор, используемый только при считываамии "сырых" данных из бинарного файла и указывающий длину считываемых данных.
//		out_pCardDataBuf		 - Указатель на буфер для считанных с карты данных.<p/>
//		Считанные с карты данные возвращаются в виде строки следующего формата:<p/>
//		<p/>"[элемент1]=[значение1]\\n[элемент2=[значение2]\\n....[элементN=[значениеN]\\n".
//		inout_pCardDataLen		 - Указатель на максимальный размер буфера для считываемых данных и длину возвращаемых данных.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ параметров запроса на чтение данных с карты. 
IL_FUNC IL_WORD opCtxSetCardDataBuf(s_opContext *p_opContext, IL_CHAR *in_CardDataDescr, IL_CHAR *out_pCardDataBuf, IL_WORD *inout_pCardDataLen);

//  Description:
//      Устанавливает в контекст ИБТ параметры для последующего формирования запроса на аутентификацию ИД-приложения. 
//  See Also:
//		opCtxSetMetaInfo
//		opCtxSetRequestHash
//		opCtxSetExtraData
//		opApiPrepareAppAuthRequest
//  Arguments:
//      p_opContext			  - Указатель на контекст ИБТ.
//		ifAuthOnline		  - Признак формирования запроса для аутентификации в режиме Online.
//		out_pAuthRequestBuf	  - Указатель на буфер для формируемого запроса.
//		inout_pAuthRequestLen - Указатель на максимальный размер буфера и фактическую длину сформированного запроса.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ параметров запроса на аутентификацию ИД-приложения.
IL_FUNC IL_WORD opCtxSetAuthRequestBuf(s_opContext *p_opContext, IL_BYTE ifAuthOnline, IL_BYTE *out_pAuthRequestBuf, IL_WORD *inout_pAuthRequestLen);

//  Description:
//      Устанавливает в контекст ИБТ параметры для последующей обработки результатов аутентификацию ИД-приложения. 
//  See Also:
//		opApiCheckAppAuthResponse
//  Arguments:
//      p_opContext				- Указатель на контекст ИБТ.
//		in_pAppAuthResponseData	- Указатель на буфер с данными результатов аутентификацию ИД-приложения.
//		AuthResponseDataLen		- Длина буфера данных результатов аутентификаци ИД-приложения.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ параметров обработки результатов аутентификацию ИД-приложения.
IL_FUNC IL_WORD opCtxSetAppAuthResponseData(s_opContext *p_opContext, IL_BYTE *in_pAppAuthResponseData, IL_WORD AuthResponseDataLen);

//  Description:
//      Устанавливает в контекст ИБТ код выполняемой операции.<p/>
//		Коды операций определены в заголовочном файле 'opDescr.h'.<p/>
//		При использовани линейного интерфейса код выполняемой операции передаётся в ИБТ в качестве аргумента функции 'opApiInitOperation'. 
//  See Also:
//		opApiInitOperation
//  Arguments:
//      p_opContext		- Указатель на контекст ИБТ.
//		in_OpCode		- Код выполняемой операции.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ кода выполняемой операции.
IL_FUNC IL_WORD opCtxSetOperationCode(s_opContext *p_opContext, IL_WORD in_OpCode);

//  Description:
//      Устанавливает в контекст ИБТ строку с новым значением пароля.
//  See Also:
//		opCtxSetConfirmPinStr	
//  Arguments:
//      p_opContext		- Указатель на контекст ИБТ.
//		in_pNewPinStr	- Указатель на строку Win-1251 с новым значением пароля.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ строки с новым значением пароля.
IL_FUNC IL_WORD opCtxSetNewPinStr(s_opContext *p_opContext, IL_CHAR *in_pNewPinStr);

//  Description:
//      Устанавливает в контекст ИБТ строку с подтверждающим значением нового пароля.
//  See Also:
//		opCtxSetNewPinStr	
//  Arguments:
//      p_opContext			- Указатель на контекст ИБТ.
//		in_pConfirmPinStr	- Указатель на строку Win-1251 с подтверждающим значением пароля.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ строки с подтверждающим значением нового пароля.
IL_FUNC IL_WORD opCtxSetConfirmPinStr(s_opContext *p_opContext, IL_CHAR *in_pConfirmPinStr);

/* Description
   Извлекает из контекста ИБТ случайное число карты для
   последующей установки защищённого обмена сообщениями между
   ИД-приложением и эмитентом.
   See Also
   opCtxSetIssuerSessionCryptogramm
   Parameters
   p_opContext :         Указатель на контекст ИБТ.
   out_pIcChallenge16 :  Указатель на буфер для извлекаемого
                         случайного числа карты.
   Returns
   IL_WORD - Код ошибки.
   Summary
   Извлечение из контекста ИБТ случайного числа карты.        */
IL_FUNC IL_WORD opCtxGetIssuerSessionIcChallenge(s_opContext *p_opContext, IL_BYTE *out_pIcChallenge16);

//  Description:
//      Устанавливает в контекст ИБТ конкатенированные данные криптограммы аутентификации хоста (4 байта) и случайного числа хоста (16 байт) для последующей установки защищённого обмена сообщений между ИД-приложением и эмитентом.
//  See Also:
//		opCtxGetIssuerSessionIcChallenge	
//  Arguments:
//      p_opContext	   - Указатель на контекст ИБТ.
//		in_pHostData20 - Указатель на буфер с устанавливаемыми данными.
//		HostDataLength - Длина данных.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ криптограммы аутентификации карты.
IL_FUNC IL_WORD opCtxSetIssuerSessionCryptogramm(s_opContext *p_opContext, IL_BYTE *in_pHostData20, IL_BYTE HostDataLength);

//  Description:
//      Извлекает из контекста ИБТ данные для проверки установленной защищённой сесссии с эмитентом ИД-приложения.
//  See Also:
//		opCtxGetIssuerSessionIcChallenge
//		opCtxSetIssuerSessionCryptogramm
//  Arguments:
//      p_opContext			  - Указатель на контекст ИБТ.
//		out_pHostChallenge16  - Указатель на буфер для извлекаемого значения случайного числа хоста.
//		out_pCardCryptogramm4 - Указатель на буфер для извлекаемого значения криптограммы аутентификации карты.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Извлечение из контекста ИБТ данных для проверки установленной сесссии с эмитентом ИД-приложения.
IL_FUNC IL_WORD opCtxGetCheckIssuerSessionData(s_opContext *p_opContext, IL_BYTE *out_pHostChallenge16, IL_BYTE *out_pCardCryptogramm4);

//  Description:
//      Устанавливает в контекст ИБТ указатель на буфер с хэш-значением запроса на оказание услуги.<p/> 
//		Эти данные используются ИБТ при формировании ИБТ запроса на аутентификацию ИД-Приложения.
//  See Also:
//		opApiPrepareAppAuthRequest   
//  Arguments:
//      p_opContext			- Указатель на контекст ИБТ.
//		in_pRequestHash		- Указатель на буфер с хэш-значением.
//		in_RequestHashLen	- Длина хэш-значениея.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ указателя на хэш-значение запроса на оказание услуги.
IL_FUNC IL_WORD opCtxSetRequestHash(s_opContext *p_opContext, IL_BYTE *in_pRequestHash, IL_WORD in_RequestHashLen);

//  Description:
//      Устанавливает в контекст ИБТ указатель на буфер для считываемых из файла фотографии владельца карты данных. 
//  See Also:
//		opApiReadPhoto   
//  Arguments:
//      p_opContext			- Указатель на контекст ИБТ.
//		out_pPhotoBuf		- Указатель на буфер для считываемых данных фотографии.
//		inout_pPhotoLen		- Указатель на максимальный размер буфера и фактическую длину считанных данных.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ указателя на буфер для фотографии владельца карты.
IL_FUNC IL_WORD opCtxSetPhotoBuf(s_opContext *p_opContext, IL_BYTE *out_pPhotoBuf, IL_WORD *inout_pPhotoLen);

//  Description:
//      Устанавливает в контекст ИБТ параметры внешнего описателя сектора данных карты УЭК.<p/> 
//		Используется при необходимости модификации или добавления внешних описателей секторов данных карты УЭК. 
//  See Also:
//		opApiWriteSectorExDescr   
//  Arguments:
//      p_opContext		  - Указатель на контекст ИБТ.
//		SectorId		  - Идентификатор сектора прикладных данных.
//		SectorVer		  - Версия формата сектора прикладных данных.
//		in_pExSectorDescr - Указатель на строку-описатель прикладных данных. 
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ параметров внешнего описателя данных карты.
IL_FUNC IL_WORD opCtxSetSectorExDescr(s_opContext *p_opContext, IL_WORD SectorId, IL_BYTE SectorVer, IL_CHAR *in_pExSectorDescr);

//  Description:
//      Возвращает из контекста ИБТ фразу контрольного приветствия при первом вызове функции и пустую строку при всех последующих вызовах в рамках проведения текущей операции.
//		<p/>Фраза контрольного приветствия считывается с карты и хранится в контексте ИБТ в виде строки Win-1251.   
//		<p/>Фраза контрольного приветствия должна отображаться только перед первым предъявлением пароля.
//  See Also:
//		opCtxSetPassPhrase	
//  Arguments:
//      p_opContext		 - Указатель на контекст ИБТ.
//		out_pPassPhrase  - Указатель на выходной буфер для фразы контрольного приветствия.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Получение строки с фразой контрольного приветствия.
IL_FUNC IL_WORD opCtxGetPassPhrase(s_opContext *p_opContext, IL_CHAR *out_pPassPhrase);

//  Description:
//      Устанавливает в контекст ИБТ фразу контрольного приветствия.
//		<p/>Фраза контрольного приветствия должна отображаться только перед первым предъявлением пароля.
//  See Also:
//		opCtxGetPassPhrase
//      flSetPassPhrase
//  Arguments:
//      p_opContext		 - Указатель на контекст ИБТ.
//		in_pPassPhrase  - Указатель на входной буфер c фразой контрольного приветствия в кодировке Win-1251.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Устанавливает в контекст ИБТ фразу контрольного приветствия.
IL_FUNC IL_WORD opCtxSetPassPhrase(s_opContext *p_opContext, IL_CHAR *in_PassPhrase);

//  Description:
//      Устанавливает в контекст ИБТ параметры для получения строки-описателя размещённых на карте УЭК секторами данных.
//  See Also:
//		opApiGetCardSectorsDescr   
//  Arguments:
//      p_opContext				- Указатель на контекст ИБТ.
//		out_pSectorsDescr		- Указатель на буфер для возвращаемой строки-описателя.
//		inout_pSectorsDescrLen	- Указатель на максимальный размер буфера и длину возвращаемых данных.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ параметров внешнего описателя данных карты.
IL_FUNC IL_WORD opCtxSetSectorsDescrBuf(s_opContext *p_opContext, 
										IL_CHAR *out_pSectorsDescr, IL_WORD *inout_pSectorsDescrLen); 

//  Description:
//      Устанавливает в контекст ИБТ параметры для получения строки-описателя блока редактируемых данных.
//  See Also:
//		opApiGetCardBlockDataDescr   
//  Arguments:
//      p_opContext			- Указатель на контекст ИБТ.
//		in_pBlockId		- Указатель на строку-идентификатор редактируемого блока в формате "S-B", где:
//								- S - идентификатор сектора,
//								- B - идентификатор блока редактируемых данных.
//		out_pBlockDataBuf	- Указатель на буфер для возвращаемой строки-описателя блока редактируемых данных.
//		inout_pBlockDataLen - Указатель на максимальный размер буфера и длину возвращаемых данных.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ параметров получения строки-описателя блока редактируемых данных.
IL_FUNC IL_WORD opCtxSetBlockDataBuf(s_opContext *p_opContext, 
									 IL_CHAR *in_pBlockId, IL_CHAR *out_pBlockDataBuf, 
									 IL_WORD *inout_pBlockDataLen);

//  Description:
//      Устанавливает в контекст ИБТ параметры пакета APDU-команд для последующего исполнения.
//  See Also:
//		opApiRunApduPacket   
//  Arguments:
//      p_opContext				- Указатель на контекст ИБТ.
//		isApduEncryptedPS		- Признак шифрования пакета в рамках установленной сессии с Поставщиком Услуги.
//		out_pApduPacketSize		- Указатель на переменную с возращаемым значением количества успешно выполненных команд пакета.
//		pApduInBuf				- Указатель на входной буфер пакета, представляющий последовательность APDU-команд следующего формата:<p/> 
//								  <p/>"[Cmd][LenIn][LenExp][DataIn][AllowResLen][AllowRes]...", где: 
//									- Cmd - Заголовок APDU-команды (IL_BYTE*4: Class,Ins,P1,P2), 
//									- LenIn - длина входных данных (IL_DWORD в формате INTEL), 
//									- LenExp - ожидаемая длина выходных данных (IL_DWORD в формате INTEL), 
//									- DataIn - входные данные IL_BYTE(LenIn), 
//									- AllowResLen - длина списка допустимых статусов ответа карты SW12 (IL_BYTE), 
//									- AllowRes - Список допустимых статусов ответа карты SW1,SW2 (IL_BYTE: AllowResLen*2). 
//		ApduInLen				- Длина входного буфера пакета.
//		pApduOutBuf				- Указатель на выходной буфер для массива данных с результатами выполнения пакета APDU-команд следующего формата:<p/>
//									<p/>"[Cmd][Sw1][Sw2][LenOut][DataOut]...", где: 
//									- Cmd - Заголовок APDU-команды (IL_BYTE*4: Class,Ins,P1,P2),
//									- SW1 - статус SW1 ответа карты,
//									- SW2 - статус SW2 ответа карты,
//									- LenOut - длина выходных данных (IL_DWORD в формате INTEL),
//									- DataOut - выходные данные IL_BYTE(LenOut).
//		pApduOutLen				- Длина массива данных с результатами выполнения пакета. 
//		out_pApduPacketResult	- Указатель на переменную с кодом выполнения последней команды пакета.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ параметров пакета APDU-команд для последующего исполнения. 
IL_FUNC IL_WORD opCtxSetApduPacketBuf(s_opContext *p_opContext, 
									  IL_BYTE isApduEncryptedPS, 
									  IL_WORD *out_pApduPacketSize, IL_BYTE *pApduInBuf, 
									  IL_WORD ApduInLen, IL_BYTE *pApduOutBuf, IL_WORD *pApduOutLen, 
									  IL_WORD *out_pApduPacketResult);

//  Description:
//      Устанавливает в контекст ИБТ параметры для получения электронной подписи держателя карты УЭК.
//  See Also:
//		opApiMakeDigitalSignature   
//  Arguments:
//      p_opContext						- Указатель на контекст ИБТ.
//		out_pDigitalSignBuf				- Указатель на буфер для возвращаемой электронной подписи.
//		inout_pDigitalSignLen			- Указатель на максимальный размер буфера и длину возвращаемой электронной подписи.
//		out_pDigitalSignCertChain		- Указатель на буфер для возвращаемой цепочки сертификатов ключа проверки электронной подписи.
//		inout_pDigitalSignCertChainLen	- Указатель на максимальный размер буфера и длину возвращаемой цепочки сертификатов.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ параметров получения электронной подписи держателя карты УЭК.
IL_FUNC IL_WORD opCtxSetDigitalSignatureBuf(s_opContext *p_opContext, 
											IL_BYTE *out_pDigitalSignBuf, IL_WORD *inout_pDigitalSignLen,
											IL_BYTE *out_pDigitalSignCertChain, IL_WORD *inout_pDigitalSignCertChainLen);

//  Description:
//      Устанавливает в контекст ИБТ параметры для настройки защищённого канала с Поставщиком услуги.
//  See Also:
//		opCtxGetProviderSessionData
//		opCtxSetProviderAuthData
//		opCtxSetProviderEncrDecrBuf
//		opApiSetProviderCryptoSession
//		opApiAuthServiceProvider
//		opApiEncryptProviderToTerminal
//		opApiDecryptProviderToTerminal
//  Arguments:
//      p_opContext		- Указатель на контекст ИБТ.
//		ifGostPS		- Признак настройки канала на основе криптоалгоритма ГОСТ.
//		in_pCSpId		- Указатель на буфер сертификата открытого ключа Поставщика услуги.
//		in_CSpIdLen		- Длина сертификата.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ параметров настройки защищённого канала с Поставщиком услуги.
IL_FUNC IL_WORD opCtxSetProviderSessionParams(s_opContext *p_opContext, IL_BYTE ifGostPS, IL_BYTE *in_pCSpId, IL_WORD in_CSpIdLen);

//  Description:
//      Извлекает из контекста ИБТ ключевые данные установленной терминалом сессии с Поставщиком услуги.
//  See Also:
//		opCtxSetProviderSessionParams
//		opCtxSetProviderAuthData
//		opCtxSetProviderEncrDecrBuf
//		opApiSetProviderCryptoSession
//		opApiAuthServiceProvider
//		opApiEncryptProviderToTerminal
//		opApiDecryptProviderToTerminal
//  Arguments:
//      p_opContext					- Указатель на контекст ИБТ.
//		out_pProviderSessionDataOut	- Указатель на буфер для возвращаемых параметров установленной сессии.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Извлечение из контекста ИБТ ключевых данных установленной терминалом сессии с Поставщиком услуги.
IL_FUNC IL_WORD opCtxGetProviderSessionData(s_opContext *p_opContext, PROVIDER_SESSION_DATA *out_pProviderSessionDataOut);

//  Description:
//      Устанавливает в контекст ИБТ параметры для аутентификации Поставщика услуги.<p/>
//		Механизм  аутентификации  поставщика  услуги  обеспечивает  контроль  достоверности  данных, полученных в ответе на запрос оказания услуги. 
//  See Also:
//		opCtxSetProviderSessionParams
//		opCtxGetProviderSessionData
//		opCtxSetProviderEncrDecrBuf
//		opApiSetProviderCryptoSession
//		opApiAuthServiceProvider
//		opApiEncryptProviderToTerminal
//		opApiDecryptProviderToTerminal
//  Arguments:
//      p_opContext		- Указатель на контекст ИБТ.
//		in_pMsg			- Указатель на данные ответа на запрос оказания услуг.
//		MsgLen			- Длина данных ответа.
//		in_pMsgSign		- Указатель на буфер c ЭЦП, сформированной Поставщиком услуги для данных ответа на запрос оказания услугии.
//		MsgSignLen		- Длина сформированной ЭЦП.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ параметров для аутентификации Поставщика услуги.
IL_FUNC IL_WORD opCtxSetProviderAuthData(s_opContext *p_opContext, IL_BYTE *in_pMsg, IL_WORD MsgLen, IL_BYTE *in_pMsgSign, IL_WORD MsgSignLen);

//  Description:
//      Устанавливает в контекст ИБТ указатели на буфера, для шифрования и расшифрования данных запроса на оказание услуги в рамках установленного защищённого канала с Поставщиком услуги.
//  See Also:
//		opCtxSetProviderSessionParams
//		opCtxGetProviderSessionData
//		opCtxSetProviderAuthData
//		opApiSetProviderCryptoSession
//		opApiAuthServiceProvider
//		opApiEncryptProviderToTerminal
//		opApiDecryptProviderToTerminal
//  Arguments:
//      p_opContext				- Указатель на контекст ИБТ.
//		in_pClearData			- Указатель на входной буфер с незашифрованными данными при шифрования или выходной буфер с расщифрованными данными при расшифровании.
//		inout_pClearDataLen	    - Указатель на длину шифруемых данных или максимальный размер буфера и длину возвращаемых расщифрованных данных.
//		in_pEncryptedData	    - Указатель на входной буфер с зашифрованными данными при расшифрования или выходной буфер с засщифрованными данными при шифровании.
//		inout_pEncryptedDataLen - Указатель на длину зашифрованных данных или максимальный размер буфера и длину возвращаемых зашиврованных данных.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ указателей на буфера, для шифрования и расшифрования данных запроса на оказание услуги в рамках установленного защищённого канала с Поставщиком услуги.
IL_FUNC IL_WORD opCtxSetProviderEncrDecrBuf(s_opContext *p_opContext, 
											IL_BYTE *in_pClearData, IL_DWORD *inout_pClearDataLen,
											IL_BYTE *in_pEncryptedData, IL_DWORD *inout_pEncryptedDataLen);

//  Description:
//      Устанавливает в контекст ИБТ данные владельца персонального модуля безопасности (ПМБ).<p/>
//		Используется только в версиях ИБТ с поддержкой ПМБ при его активации. 
//  See Also:
//		smOfflineActivationFinish
//  Arguments:
//      p_opContext			- Указатель на контекст ИБТ.
//		in_pSeOwnerName		- Указатель на данные владельца ПМБ.
//		SeOwnerNameLen		- Длина данных владельца ПМБ.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ данных о владельце ПМБ.
IL_FUNC IL_WORD opCtxSetSeOwnerName(s_opContext *p_opContext, IL_BYTE *in_pSeOwnerName, IL_WORD SeOwnerNameLen);

//  Description:
//      Извлекает из контекста ИБТ криптограмму аутентификации хоста для установки защищённого соединения с эмитентом.<p/>
//		Используется только в версиях ИБТ с поддержкой ПМБ. 
//  See Also:
//		opCtxSetSeIssuerSessionCryptogramm
//  Arguments:
//      p_opContext			- Указатель на контекст ИБТ.
//		out_pIcChallenge16	- Указатель на выходной буфер для сформированной криптограммы аутентификации хоста.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Извлечение из контекста ИБТ криптограммы аутентификации хоста для установки защищённого соединения ПМБ с эмитентом с исползованием ПМБ.
IL_FUNC IL_WORD opCtxGetSeIssuerSessionIcChallenge(s_opContext *p_opContext, IL_BYTE *out_pIcChallenge16);

//  Description:
//      Устанавливает в контекст ИБТ конкатенированные данные криптограммы аутентификации хоста (4 байта) и случайного числа хоста (16 байт) для последующей установки защищённого обмена сообщений между ИД-приложением и эмитентом.
//		Используется только в версиях ИБТ с поддержкой ПМБ. 
//  See Also:
//		opCtxGetSeIssuerSessionIcChallenge	
//  Arguments:
//      p_opContext				- Указатель на контекст ИБТ.
//		in_pCardCryptogramm		- Указатель на буфер с устанавливаемыми данными.
//		CardCryptogrammLength	- Длина данных.
//		ifGostSession			- Признак установки сессии ГОСТ.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Установка в контекст ИБТ криптограммы аутентификации карты для установки защищённой сессии с эмитентом с исползованием ПМБ.
IL_FUNC IL_WORD opCtxSetSeIssuerSessionCryptogramm(s_opContext *p_opContext, IL_BYTE *in_pCardCryptogramm, IL_BYTE CardCryptogrammLength, IL_BYTE ifGostSession);

#ifdef __cplusplus
}
#endif

#endif//__OP_CTXFUNC_H_
