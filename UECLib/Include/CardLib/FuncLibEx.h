#ifndef _FUNCLIB_EX_H_
#define _FUNCLIB_EX_H_

#include "CardLibEx.h"
#include "HAL_SCRApdu.h"

//	Максимальное количество допустимых SW1-SW2 ответов карты
#define ALLOWED_RES_MAX			20

// Элемент пакета APDU-команды.
typedef struct
{
	IL_APDU	Apdu;							// Описатель APDU-команды
	IL_BYTE allowed_res_len;				// Количество допустимых SW1-SW2 ответов карты 
	IL_BYTE allowed_res[ALLOWED_RES_MAX*2];	// Массив допустимых SW1-SW2 ответов карты
} IL_APDU_PACK_ELEM;

//  Description:
//      Выполняет инициализацию карт-ридера.
//  See Also:
//		flDeinitReader
//  Arguments:
//      phCard			- Указатель на описатель карты.
//      ilRdrSettings	- Указатель на инициализирующие параметры карт-ридера.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Инициализация карт-ридера.
IL_FUNC IL_RETCODE flInitReader(IL_CARD_HANDLE* phCrd, IL_READER_SETTINGS ilRdrSettings);

//  Description:
//      Выполняет деинициализацию карт-ридера.
//  See Also:
//		flInitReader
//  Arguments:
//      phCard		- Указатель на описатель карты.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Деинициализация карт-ридера.
IL_FUNC IL_RETCODE flDeinitReader(IL_CARD_HANDLE* phCrd);

//	Description:
//		Выполняет выбор приложения карты УЭК.<p/>
//		Осуществляет первоначальные проверки и настройки:
//			* Инициализирует и проверяет версию приложения.
//			* Инициализирует и проверяет срок действия приложения.
//			* Инициализирует сведения о применении приложения и согласовывает тип используемого картой криптоалгоритма ГОСТ или RSA.
//			* Инициализирует флаги поддержки механизма электронной подписи, поддержки картой "длинных данных" и команды MSE.
//			* Проверяет наличие каталога секторов и инициализирует список размещённых на карте секторов прикладных данных.
//			* Проверяет наличие сертификата открытого ключа терминала и ОКО.
//			* Инициализирует статус приложения.
//  See Also:
//		flAppReselect
//	Arguments:
//		phCard		 - Указатель на описатель карты.
//		out_pData	 - Указатель на буфер для возвращаемых данных выбора приложения.
//		out_pDataLen - Указатель на длину возвращаемых данных.
//   Return Value:
//		IL_RETCODE - Код ошибки.
//   Summary:
//		Выбор приложения карты УЭК, первоначальные проврки и настройки.  
IL_FUNC IL_RETCODE flAppSelect(IL_CARD_HANDLE* phCrd, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

//	Description:
//		Выполняет повторный выбор приложения карты УЭК без проверок и возврата информации.<p/>
//		Все инициализированные при первоначальном выборе приложения, настройки остаются в силе.
//  See Also:
//		flAppSelect
//  Arguments:
//		phCard - Указатель на описатель карты.
//  Return Value:
//		IL_RETCODE - Код ошибки.
//  Summary:
//		Повторный выбор приложения карты. inalAuth
IL_FUNC IL_RETCODE flAppReselect(IL_CARD_HANDLE* phCrd);

//  Description:
//		Выполняет процедуру аутентификации терминала.<p/>
//		Данная процедура предназначена для того, чтобы идентификационное приложение убедилось, что терминал обладает
//		закрытым ключом, который соответствует открытому ключу, указанному в сертификате открытого ключа терминала.<p/>
//		В процессе аутентификации терминала:
//			* Производится проверка сертификатов терминала.
//			* Выполняется процедура динамической аутентификации с использованием согласованного ранее криптоалгоритма ГОСТ или RSA.
//			* Устанавливается защищённая сессия для обмена сообщениями между терминалом и картой.
//	See Also:
//		flAppSelect
//  Arguments:
//		phCard - Указатель на описатель карты.
//  Return Value:
//		IL_RETCODE - Код ошибки.
//	Summary:
//		Аутентификация терминала.
IL_FUNC IL_RETCODE flAppTerminalAuth(IL_CARD_HANDLE* phCrd);

//  Description:
//		Осуществляет процедуру верификации держателя карты УЭК.<p/> 
//		Указанная процедура предназначена для проверки ИД-приложением правомочности использования карты УЭК. 
//		Вызывается только после успешного выполнения аутентификации терминала. 
//  See Also:
//		opApiVerifyCitizen
//		flGetPassPhrase
//  Arguments:
//      phCard			   - Указатель на описатель карты.
//		PinNum			   - Номер используемого при верификации пароля.
//		in_pPinBlock8	   - Указатель на ПИН-блок со значением пароля в формате ISO/IEC 9564-3:2002 (Format 2).
//		out_pTriesRemained - Указатель на переменную, инициализируемую значением количества оставшихся попыток предъявления пароля.
//  Return Value:
//      IL_WORD	- Код ошибки.
//  Summary:
//      Верификация держателя карты.
IL_FUNC IL_RETCODE flAppCitizenVerification(IL_CARD_HANDLE *phCrd, IL_BYTE PinNum, IL_BYTE *in_pPinBlock8, IL_BYTE *out_pTriesRemained);

//  Description:
//		Изменяет значение ПИН-кода карты УЭК на новое.<p/>
//		Перед вызовом этой функции должна быть успешно проведена процедура верификации держателя карты.
//  See Also:
//		opApiManagePinPuk
//		opApiVerifyCitizen
//  Arguments:
//      phCard			 - Указатель на описатель карты.
//		PinNum			 - Номер изменяемого ПИН-кода.
//		in_pNewPinBlock8 - Указатель на ПИН-блок с новым значением пароля в формате ISO/IEC 9564-3:2002 (Format 2).
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Смена ПИН.
IL_FUNC IL_RETCODE flAppChangePIN(IL_CARD_HANDLE* phCrd, IL_BYTE PinNum, IL_BYTE *in_pNewPinBlock8);

//	Description:
//		Формирует запрос на аутентификацию ИД-приложения для оказания услуги.
//  See Also
//		opApiPrepareAppAuthRequest
//  Arguments:
//		phCard			 - Указатель на описатель карты.
//		ifOnline		 - Признак формирования запроса для аутентификации в режиме Online.
//      in_pTermAuthData - Указатель на буфер c входными TLV-данными терминала:
//							* Идентификатор услуги '9F15'.
//							* Случайное число терминала '9F37'.
//							* Штамп времени выполнения операции по часам терминала '9F21'.
//							* Дополнительные сведения об операции '9F03'.
//							* Хэш-значение запроса на оказание услуги 'DF02'.
//							* Сведения о терминале '9F1C'.
//							* Сжатое значение открытого ключа терминала '9F1D' (для карт версии 1.1).
//							* Сведения об идентификационном приложении 'C2' (для карт версии 1.1).
//							* Номер версии идентификационного приложения '9F08' (для карт версии 1.1).
//							* Открытый ключ терминала '9F1E' (для карт версии 1.1).
//   TermAuthDataLen	-  Длина входных TLV\-данных терминала.
//   out_pAuthData      -  Указатель на буфер для формируемого запроса.
//   inout_pAuthDataLen -  Указатель на максимальный размер буфера и фактическую длину сформированного запроса.
//   Return Value:
//		IL_WORD - Код ошибки.
//   Summary:
//		Формирование запроса на аутентификацию ИД-приложения для оказания услуги.                                                
IL_FUNC IL_RETCODE flAppAuthRequest(IL_CARD_HANDLE *phCrd, IL_BYTE ifOnline, IL_BYTE *in_pTermAuthData, IL_DWORD TermAuthDataLen, 
									IL_BYTE *out_pAuthData, IL_WORD *inout_pAuthDataLen);

//	Description:
//		Формирует запрос на аутентификацию ИД-приложения для установки защищённой сессии с эмитентом.
//	See Also:
//		opApiPrepareAppAuthRequestIssSession
//  Arguments:
//		phCard           - Указатель на описатель карты.
//		in_pTermAuthData - Указатель на буфер c входными TLV-данными терминала:
//							* Идентификатор услуги '9F15' инициализируется нулевыми значениями.
//                          * Случайное число терминала '9F37'.
//                          * Штамп времени выполнения операции по часам терминала '9F21'.
//                          * Дополнительные сведения об операции '9F03'. Для операции 'Разблокировка КРП'
//                            инициализируется ПИН&#45;блоком для временного значения КРП, формируемого терминалом. 
//							  Иначе инициализируется нулевыми значениями.
//                          * Хэш-значение запроса на оказание услуги 'DF02' инициализируется нулевыми значениями.
//							* Сведения о терминале '9F1C'.
//							* Сжатое значение открытого ключа терминала '9F1D' (для карт версии 1.1).
//							* Сведения об идентификационном приложении 'C2' (для карт версии 1.1).
//                          * Номер версии идентификационного приложения '9F08' (для карт версии 1.1).
//                          * Открытый ключ терминала '9F1E' (для карт версии 1.1).
//		TermAuthDataLen - Длина входных TLV\-данных терминала.
//		out_pAuthData - Указатель на буфер для формируемого запроса.
//		inout_pAuthDataLen - Указатель на максимальный размер буфера и фактическую длину сформированного запроса.
//   Return Value:
//		IL_WORD - Код ошибки.
//   Summary:
//		Формирование запроса на аутентификацию ИД-приложения для установки защищённой сессии с эмитентом. 
IL_FUNC IL_RETCODE flAppAuthRequestIssSession(IL_CARD_HANDLE *phCrd, IL_BYTE *in_pTermAuthData, IL_DWORD TermAuthDataLen, 
											  IL_BYTE *out_pAuthData, IL_WORD *inout_pAuthDataLen);

//  Description:
//		Проверяет достоверность данных результатов аутентификации ИД-приложения и возвращает код аутентификации.
//  See Also:
//		opApiPrepareAppAuthRequest
//		opApiCheckAppAuthResponse
//		flAppAuthRequest
//  Arguments:
//      phCard					- Указатель на описатель карты.
//		in_pAuthData			- Указатель на буфер сформированного ранее запроса на аутентификацию ИД-приложения для оказания услуги.
//		AuthDataLen				- Длина запроса на аутентификацию.
//		in_pAuthResponseData	- Указатель на буфер с данными результатов аутентификации ИД-приложения.
//		in_AuthResponseDataLen	- Длина данных результата аутентификации.
//		out_pAuthResult			- Указатель на переменную, инициализируемую значением возвращённого кода аутентификации ИД-приложения.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Проверка результатов аутентификации ИД-приложения.
IL_FUNC IL_RETCODE flAppAuthCheckResponse(IL_CARD_HANDLE *phCrd, IL_BYTE *in_pAuthData, IL_WORD AuthDataLen, IL_BYTE *in_pAuthRespData, IL_WORD AuthRespDataLen, IL_WORD *out_pAuthResult);

//  Description:
//		Изменяет значение КРП карты УЭК на новое.<p/>
//		Перед вызовом этой функции должна быть успешно проведена процедура верификации держателя карты по КРП.
//  See Also:
//		opApiManagePinPuk
//		opApiVerifyCitizen
//  Arguments:
//      phCard			 - Указатель на описатель карты.
//		in_pNewPinBlock8 - Указатель на ПИН-блок с новым значением КРП в формате ISO/IEC 9564-3:2002 (Format 2).
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Смена КРП.
IL_FUNC IL_RETCODE flAppChangePUK(IL_CARD_HANDLE *phCrd, IL_BYTE *in_pNewPukBlock8);

//  Description:
//		Разблокирует ПИН карты УЭК.<p/>
//		Перед вызовом этой функции должна быть успешно проведена процедура верификации держателя карты по КРП.
//  See Also:
//		opApiManagePinPuk
//		opApiVerifyCitizen
//  Arguments:
//      phCard			 - Указатель на описатель карты.
//		PinNum			 - Номер разблокируемого ПИН-кода.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Разблокировка ПИН.
IL_FUNC IL_RETCODE flAppUnlockPIN(IL_CARD_HANDLE *phCrd, IL_BYTE PinNum);

//  Description:
//		Селектирует указанный файл и считывает блок данных.
//  See Also:
//  Arguments:
//      phCard		 - Указатель на описатель карты.
//		FileId		 - Идентификатор файла.
//		DataId		 - Идентификатор типа данных:
//						- '0000' - Чтение содержимого бинарного файла с TLV-данными.
//						- 'FFFF' - Специальный случай чтения сертификата публичного ключа приложения.
//						- Иначе - Идентификатор тега считываемого из TLV-файла значения элемента данных. 
//		out_pData	 - Указатель на буфер для возвращаемых данных.
//		out_pDataLen - Указатель на длину возыращаемых данных.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Чтение блока данных из файла.
IL_FUNC IL_RETCODE flAppReadBlock(IL_CARD_HANDLE *phCrd, IL_WORD FileId, IL_WORD DataId, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

//  Description:
//		Осуществляет аутентификацию эмитента ИД-приложения.  
//  See Also:
//		opApiSetIssuerCryptoSession
//  Arguments:
//      phCard				    - Указатель на описатель карты.
//		in_pHostData20		    - Указатель на буфер с конкатенированными данными криптограммы аутентификации хоста (4 байта) и случайного числа хоста (16 байт).
//		HostDataLen			    - Длина данных хоста.
//		out_pCardCryptogramm4   - Указатель на буфер для возвращаемый криптограммы аутентификации карты (4 байта).
//		out_pCardCryptogrammLen - Указатель на длину возвращаемых картой данных.
//  Return Value:
//      IL_WORD		- Код ошибки.
//  Summary:
//      Аутентификация эмитента ИД-приложения.
IL_FUNC IL_RETCODE flIssuerAuth(IL_CARD_HANDLE *phCrd, IL_BYTE *in_pHostData20, IL_WORD HostDataLen, 
								IL_BYTE *out_pCardCryptogramm4, IL_WORD *out_pCardCryptogrammLen);

//  Description:
//      Выполняет чтение данных из бинарного файла по указанному смещению.
//  See Also:
//		flReadBinaryEx
//  Arguments:
//      phCard		- Указатель на описатель карты.
//      Offset		- Смещение в байтах от начала файла до считываемых данных.
//      DataLen		- Длина считываемых данных.
//		out_pData	- Указатель на выходной буфер для считываемых данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Чтение данных из бинарного файла.
IL_FUNC IL_RETCODE flReadBinary(IL_CARD_HANDLE *phCard, IL_WORD Offset, IL_WORD DataLen, IL_BYTE *out_pData);

//  Description:
//      Выполняет чтение данных из бинарного файла с указанного смещения до конца файла с контролем переполнения выходного буфера.
//  See Also:
//		flReadBinary
//  Arguments:
//      phCard		 - Указатель на описатель карты.
//      Offset		 - Смещение в байтах от начала файла до считываемых данных.
//		out_pData	 - Указатель на выходной буфер для считываемых данных.
//      BufLen		 - Максимальная длина выходного буфера.
//		out_pDataLen - Указатель на фактическую длину считанных данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Чтение данных из бинарного файла c контролем переполнения.
IL_FUNC IL_RETCODE flReadBinaryEx(IL_CARD_HANDLE *phCard, IL_WORD Offset, IL_BYTE *out_pData, IL_WORD BufLen, IL_WORD *out_pDataLen);

//  Description:
//      Выполняет запись данных в бинарный файл по указанному смещению.
//  See Also:
//		flReadBinary
//		flReadBinaryEx
//  Arguments:
//      phCard		- Указатель на описатель карты.
//      Offset		- Смещение в байтах от начала файла до записываемых данных.
//      DataLen		- Длина записываемых данных.
//		in_pData	- Указатель на буфер записываемых данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Запись данных в бинарный файл.
IL_FUNC IL_RETCODE flUpdateBinary(IL_CARD_HANDLE* phCard, IL_WORD Offset, IL_WORD DataLen, IL_BYTE *in_pData);

//  Description:
//      Выполняет APDU-команду пакета.
//  See Also:
//  Arguments:
//      phCard		    - Указатель на описатель карты.
//      SM_MODE		    - Режим передачи APDU-команды:
//						   * SM_MODE_NONE - по открытому каналу.
//						   * SM_MODE_IF_SESSION - по закрытому каналу при условии его установки.
//						   * SM_MODE_ALWAYS - по закрытому каналу.
//		inout_pApduElem - Указатель на элемент APDU-команды.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выполннение APDU-команд пакета.
IL_FUNC IL_RETCODE flRunApdu(IL_CARD_HANDLE *phCrd, IL_BYTE SM_MODE, IL_APDU_PACK_ELEM *inout_pApduElem);

//  Description:
//		Осуществляет чтение с карты фразы контрольного приветствия.<p />
//		Фраза контрольного приветствия отображается перед вводом держателем карты ПИН. 
//		По данной фразе держатель убеждается, что аутентификация терминала выполнена успешно, 
//		терминал является доверенным, работа с ПИН и персональными данными будет безопасной.
//  See Also:
//  Arguments:
//		phCard			- Указатель на описатель карты.
//      out_pPassPhrase - Указатель на выходной буфер для строки с фразой контрольного приветствия. 
//						  Считанная с карты строка автоматически конвертируется в Win\-1251. 
//						  При отсутствии на карте механизма контрольного приветствия возвращается пустая строка.
//  Return Value:
//		IL_RETCODE - Код ошибки.
//  Summary:
//		Получение фразы контрольного приветствия. 
IL_FUNC IL_RETCODE flGetPassPhrase(IL_CARD_HANDLE *phCrd, IL_CHAR *out_pPassPhrase);

//  Description:
//		Осуществляет запись на карту фразы контрольного приветствия.<p />
//		Фраза контрольного приветствия отображается перед вводом держателем карты ПИН. По данной фразе держатель убеждается,
//		что аутентификация терминала выполнена успешно, терминал является доверенным, работа с ПИН и персональными данными
//		будет безопасной.
//  See Also:
//		opCtxSetPassPhrase 
//		opCtxGetPassPhrase
//  Arguments:
//		phCard		   -  Указатель на описатель карты.
//		in_pPassPhrase - Указатель на входной буфер cо строкой фразы контрольного приветствия в кодировке Win-1251. 
//						 Записываемая на карту строка автоматически конвертируется в формат Iso-8859.
//  Return Value:
//		IL_RETCODE - Код ошибки.
//  Summary:
//		Запись на карту фразы контрольного приветствия. 
IL_FUNC IL_RETCODE flSetPassPhrase(IL_CARD_HANDLE *phCrd, IL_CHAR *in_pPassPhrase);

//  Description:
//      Осуществляет выбор сектора и (если указано) блока прикладных данных карты с учётом текущего контекста карты.
//  See Also:
//  Arguments:
//      phCard		     - Указатель на описатель карты.
//		sectorId		 - Идентификатор выбираемого сектора. Нулевое значение означает переход в системный контекст (уровень приложения).
//		blockId			 - Идентификатор выбираемого блока. При нулевом значении выбор блока не производится. 
//		ifForceSelection - Признак необходимости выбора без учёта текущего контекста.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Выбор сектора и блока данных карты с учётом текущего контекста.
IL_FUNC IL_RETCODE flSelectContext(IL_CARD_HANDLE *phCrd, IL_WORD sectorId, IL_WORD blockId, IL_BYTE ifForceSelection);

//  Description:
//      Считывает с карты значение открытого ключа ИД-приложения.
//  See Also:
//  Arguments:
//      phCard		 - Указатель на описатель карты.
//		out_pData	 - Указатель на буфер для возвращаемых данных.
//		out_pDataLen - Указатель на длину возыращаемых данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Получение открытого ключа ИД-приложения.
IL_FUNC IL_RETCODE flGetAppPubKey(IL_CARD_HANDLE *phCrd, IL_BYTE *out_pData, IL_WORD *out_pDataLen);

//  Description:
//		Получает сведения о терминале.<p/>
//		Для карт версии 1.0 сведения о терминале считываются из секции 'TerminalInfo' конфигурационного файла 'terminal.ini'.<p/>
//		Для карт версии 1.0 сведения о терминале извлекаются из сертификата открытого ключа терминала.
//  See Also:
//  Arguments:
//		phCard - Указатель на описатель карты.
//		out_pData - Указатель на буфер для возвращаемых данных.
//		out_pDataLen - Указатель на длину возвращаемых данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//		Получение сведений о терминале. 
IL_FUNC IL_RETCODE flGetTerminalInfo(IL_CARD_HANDLE *phCrd, IL_BYTE *out_pData, IL_DWORD *out_pDataLen);

//  Description:
//		Формирует электронную подпись держателя карты УЭК.
//  See Also:
//		opCtxSetDigitalSignatureBuf 
//		opApiMakeDigitalSignature
//  Arguments:
//		phCard - Указатель на описатель карты.
//		in_pAuthRequest - Указатель на сформированный ранее запрос на аутентификацию ИД\-приложения для оказания услуги.
//		AuthRequestLen - Длина запроса на аутентификацию ИД-приложения.
//		out_pSign - Указатель на буфер для возвращаемой электронной подписи.
//		inout_pSignLen - Указатель на размер буфера электронной подписи и длину возвращаемых данных.
//		out_pCertChain - Указатель на цепочку сертификатов ключа проверки электронной подписи.
//		inout_pCertChainLen - Указатель на максимальный размер буфера и длину возвращаемой цепочки сертификатов.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//		Формирование электронной подписи держателя карты УЭК. 
IL_FUNC IL_RETCODE flMakeDigitalSignature(IL_CARD_HANDLE *phCrd, 
										  IL_BYTE *in_pAuthRequest, IL_WORD AuthRequestLen, 
										  IL_BYTE *out_pSign, IL_WORD *inout_pSignLen,
										  IL_BYTE *out_pCertChain, IL_WORD *inout_pCertChainLen);

//  Description:
//      Осуществляет проверку сертификата Поставщика Услуги.
//  See Also:
//  Arguments:
//      phCard			- Указатель на описатель карты.
//		in_pKeyCert		- Указатель на буфер с данными проверяемого сертификата.
//		KeyCertLen		- Длина проверяемого сертификата.
//		ifGost			- Тип проверяемого сертификата ГОСТ/RSA.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Проверка сертификата Поставщика Услуги.
IL_FUNC IL_RETCODE flCheckCertificateSP(IL_CARD_HANDLE *phCrd, IL_BYTE *in_pKeyCert, IL_WORD KeyCertLen, IL_BYTE ifGost);

//  Description:
//      Получает сертификат открытого ключа.
//  See Also:
//  Arguments:
//      phCard		 - Указатель на описатель карты.
//		ilParam		 - Идентификатор получаемого сертификата (см. 'HAL_Parameter.h').
//		KeyVer		 - Версия ключа Удостоверяющего Центра.
//		ifGost		 - Тип сертификата ГОСТ/RSA.
//		in_pCertBuf  - Указатель на буфер с возвращаемым значением сертификата.
//		out_pCertLen - Указатель на длину возвращённого сертификата.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Получение сертификата открытого ключа.
IL_FUNC IL_RETCODE flGetCertificate(IL_CARD_HANDLE *phCrd, IL_WORD ilParam, IL_BYTE KeyVer, IL_BYTE ifGost, IL_BYTE *in_pCertBuf, IL_DWORD *out_pCertLen);

//  Description:
//      Получает идентификатор версии формата сектора прикладных данных карты УЭК.<p/>
//		Идентификатор версии используется в качестве критерия выбора соответствующей секции внешнего описателя секторов данных 
//		конфигурационного файла 'sectors.ini' при чтении данных с карты или записи данных на карту. 
//  See Also:
//		prmGetParameterSectorEx
//  Arguments:
//      phCard	 - Указатель на описатель карты.
//      SectorId - Идентификатор сектора.
//  Return Value:
//      SectorVer - Идентификатор версии формата указанного сектора прикладных данных.
//  Summary:
//      Получение идентификатора версии формата сектора данных.
IL_FUNC IL_BYTE flGetSectorVersion(IL_CARD_HANDLE* phCrd, IL_BYTE SectorId);

//  Description:
//      Считывает массив TLV-данных из бинарного блока прикладных данных карты УЭК.<p/>
//		Используется для кеширования бинарных TLV-данных в контекст ИБТ. 
//		Кешированные данные автоматически модифицируются при обновлении, добавлении или удалении элементов TLV-данных бинарного блока карты. 
//  See Also:
//  Arguments:
//      phCard			- Указатель на описатель карты.
//      SectorId		- Идентификатор сектора.
//		BlockId			- Идентификатор блока.
//		out_pData		- Указатель на выходной буфер для считываемых данных. 
//		inout_pDataLen	- Указатель на максимальный размер выходного буфера и фактический размер считанных данных.
//  Return Value:
//      SectorVer - Идентификатор версии формата указанного сектора прикладных данных.
//  Summary:
//      Чтение TLV-данных бинарного блока прикладных данных карты.
IL_FUNC IL_RETCODE flAppReadBinTlvBlock(IL_CARD_HANDLE* phCrd, IL_WORD SectorId, IL_WORD BlockId, IL_BYTE *out_pData, IL_WORD *inout_pDataLen);


#endif//_FUNCLIB_EX_H_