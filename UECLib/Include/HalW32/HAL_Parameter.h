#ifndef _HAL_PARAMETER_H_
#define _HAL_PARAMETER_H_

#include "il_types.h"

// Конфигурационный параметр 'Сертификат открытого ключа ОКО''
#define IL_PARAM_CAID			1
// Конфигурационный параметр 'Сертификат открытого ключа терминала'
#define IL_PARAM_CIFDID			2
// Конфигурационный параметр 'Сертификат открытого ключа УЦ'
#define IL_PARAM_CCAID			3

// Конфигурационный параметр 'Наименование ридера'
#define IL_PARAM_READERNAME		14
// Конфигурационный параметр 'Сведения о терминале'
#define IL_PARAM_TERMINAL_INFO  0x9F1C
// Конфигурационный параметр 'Модуль закрытого ключа терминала RSA' 
#define IL_PARAM_SIFDID_MOD     15
// Конфигурационный параметр 'Экспонента закрытого ключа терминала RSA'
#define IL_PARAM_SIFDID_EXP     16
// Конфигурационный параметр 'Закрытый ключ терминала ГОСТ'
#define IL_PARAM_SIFDID_GOST    17
// Конфигурационный параметр 'Наименование ридера Персонального Модуля Безопасности (ПМБ)'
#define IL_PARAM_SM_READERNAME  18
// Конфигурационный параметр 'Пароль инициализации Персонального Модуля Безопасности (ПМБ)'
#define IL_PARAM_SM_PIN			19

// Конфигурационный параметр 'Ключ для вычисления криптограммы ИД-приложения RSA'
#define IL_PARAM_MK_AC_ID_RSA	21
// Конфигурационный параметр 'Ключ для вычисления криптограммы ИД-приложения ГОСТ'
#define IL_PARAM_MK_AC_ID_GOST	22
// Конфигурационный параметр 'Ключ для обеспечения обмена через защищённый канал RSA'
#define IL_PARAM_MK_SM_ID_RSA	23
// Конфигурационный параметр 'Ключ для обеспечения обмена через защищённый канал ГОСТ'
#define IL_PARAM_MK_SM_ID_GOST  24
// Конфигурационный параметр 'Указаны мастер-ключи для вычисления криптограммы и обеспечения обмена через защищённый канал ГОСТ'
#define IL_PARAM_MK_AC_ID_DIVERS_FLAG_GOST  26
// Конфигурационный параметр 'Указаны мастер-ключи для вычисления криптограммы и обеспечения обмена через защищённый канал RSA'
#define IL_PARAM_MK_AC_ID_DIVERS_FLAG_RSA   27 
// Конфигурационный параметр 'Приватный ключ Поставщика услуг RSA'
#define IL_PARAM_S_SP_ID_RSA	28
// Конфигурационный параметр 'Приватный ключ Поставщика услуг ГОСТ'
#define IL_PARAM_S_SP_ID_GOST	29
// Конфигурационный параметр 'Сертификат открытого ключа Поставщика услуг ГОСТ'
#define IL_PARAM_CSPID_RSA		30
// Конфигурационный параметр 'Сертификат открытого ключа Поставщика услуг RSA'
#define IL_PARAM_CSPID_GOST		31
// Конфигурационный параметр 'Приватный ключ ФУО ГОСТ'
#define IL_PARAM_S_CA_ID_RSA	32
// Конфигурационный параметр 'Приватный ключ ФУО RSA'
#define IL_PARAM_S_CA_ID_GOST	33
// Конфигурационный параметр 'Ключ для обеспечения обмена через защищённый канал RSA Персонального Модуля Безопасности (ПМБ)'
#define IL_PARAM_SE_SM_ID_RSA	34
// Конфигурационный параметр 'Ключ для обеспечения обмена через защищённый канал ГОСТ Персонального Модуля Безопасности (ПМБ)'
#define IL_PARAM_SE_SM_ID_GOST  35

// Конфигурационный параметр 'Идентификатор Оператора Канала Обслуживания'
#define IL_PARAM_MEMBER_ID      40
// Конфигурационный параметр 'Уникальный порядковый номер идентификации'
#define IL_PARAM_IDENT_OP_ID    41
// Конфигурационный параметр 'Сведения, необходимые для осуществления оплаты'
#define IL_PARAM_PAYMENT_INFO   42
// Конфигурационный параметр 'Код аутентификации ИД-приложения'
#define IL_PARAM_AAC            43

// Конфигурационный параметр 'Имя файла-журнала для протоколирования'
#define IL_PARAM_PROT_LOGNAME		100	
// Конфигурационный параметр 'Признак необходимости протоколирования в файл-журнал'
#define IL_PARAM_PROT_LOGFILE		101	
// Конфигурационный параметр 'Признак необходимости протоколирования в окно Output'
#define IL_PARAM_PROT_OUTPUT		102	
// Конфигурационный параметр 'Флаг протоколирования функций карт-ридера'
#define IL_PARAM_PROT_READER		103	
// Конфигурационный параметр 'флаг протоколирования функций карты'
#define IL_PARAM_PROT_CARD			104	
// Конфигурационный параметр 'флаг протоколирования функций CardLib'
#define IL_PARAM_PROT_CARDLIB		105	
// Конфигурационный параметр 'Флаг протоколирования функций FuncLib'
#define IL_PARAM_PROT_FUNCLIB		106	
// Конфигурационный параметр 'Флаг протоколирования функций OpLib'
#define IL_PARAM_PROT_OPLIB			107	
// Конфигурационный параметр 'Флаг протоколирования функций SmLib'
#define IL_PARAM_PROT_SMLIB			108	
// Конфигурационный параметр 'Уровень протоколирования функций TellME'
#define	IL_PARAM_PROT_TELLME		109	
// Конфигурационный параметр 'Флаг внешнего протоколирования TellME'
#define IL_PARAM_PROT_EXTERN		110  

// Конфигурационный параметр 'Использовать криптоалгоритм ГОСТ'
#define IL_PARAM_USE_GOST			999

// Тип конфигурационного параметра 'Двоичный массив'
#define IL_PARAM_FORMAT_BYTEARRAY	1
// Тип конфигурационного параметра - 'Сторка'
#define IL_PARAM_FORMAT_STRING		2

// Описатель параметра конфигурационного файла
typedef struct 
{
	IL_WORD ID;			// Идентификатор параметра
	IL_WORD Format;		// Тип формата параметра
    IL_WORD Length;		// Длина значения параметра
	IL_CHAR *Section;	// Наименование секции конфигурационного файла
	IL_CHAR *Name;		// Наименование конфигурационного параметра.
}IL_PARAMETER_DESCR;

//  Description:
//      Конструирует строку с полным именем доступа к конфигурационному файлу по его имени и расширению.<p/>
//		В качестве каталога для размещения конфигурационных файлов используется каталог из которого запускается приложение.<p/>
//		В конфигурации TellME для этих целей используется предопределённый каталог 'c:\UECParam'. 
//  See Also:
//  Arguments:
//      out_FullParamFileName	- Указатель на возвращаемую строку с полным именем доступа к указанному конфигурационному файлу. 
//		in_ParamFileName		- Указатель на строку с именем конфигурационного файла.
//		in_ParamFileExt			- Указатель на строку с расширением конфигурационного файла
//  Return Value:
//      Указатель на возвращаемую строку с полным именем доступа к указанному конфигурационному файлу.
//  Summary:
//      Получение полного имени доступа к конфигурационному файлу.
IL_FUNC IL_CHAR* GetParamFilename(IL_CHAR *out_FullParamFileName, IL_CHAR *in_ParamFileName, IL_CHAR *in_ParamFileExt);

//  Description:
//      Извлекает значение конфигурационного параметра по указанному идентификатору.
//  See Also:
//  Arguments:
//      ParamId		  - Идентификатор параметра. 
//		out_pParamBuf - Указатель на выходной буфер для извлекаемого значения параметра.
//		out_pParamLen - Указатель на длину возвращённого значения.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Получение значения конфигурационного параметра по идентификатору.
IL_FUNC IL_RETCODE prmGetParameter(IL_WORD ParamId, IL_BYTE *out_pParamBuf, IL_DWORD *out_pParamLen);

//  Description:
//      Извлекает значение конфигурационного параметра эмулятора хоста по указанному идентификатору.<p/>
//		В качестве имени конфигурационного файла используется 'host.ini'.
//  See Also:
//  Arguments:
//      ParamId		  - Идентификатор параметра. 
//		out_pParamBuf - Указатель на выходной буфер для извлекаемого значения параметра.
//		out_pParamLen - Указатель на длину возвращённого значения.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Получение значения конфигурационного параметра по идентификатору.
IL_FUNC IL_RETCODE prmGetParameterHost(IL_WORD ParamId, IL_BYTE *out_pParamBuf, IL_DWORD *out_pParamLen);

//  Description:
//      Извлекает из секции 'CIN' файла настроек эмулятора хоста сопоставленные с указанным CIN значения PAN и SNILS.
//		В качестве имени конфигурационного файла используется 'host.ini'.
//  See Also:
//  Arguments:
//      in_pCIN		- Значение CIN. 
//		out_pPan	- Указатель на выходной буфер для значения PAN.
//		out_pSnils	- Указатель на выходной буфер для значения SNILS.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Получение значений PAN и SNILS по CIN.
IL_FUNC IL_RETCODE prmGetParameterHostCin2PanSnils(IL_BYTE *in_pCIN, IL_BYTE *out_pPan, IL_BYTE  *out_pSnils);

//  Description:
//      Извлекает значение конфигурационного параметра по указанному идентификатору, версии ключа удостоверяющего центра и типа криптоалгоритма ГОСТ/RSA.<p/>
//		Наименование извлекаемого параметра формируется динамически добавлением к имени параметра постфикса из подстроки криптоалгоритма и версии ключа.
//  See Also:
//  Arguments:
//      ParamId		  - Идентификатор параметра. 
//		KeyVer		  - Версия ключа УЦ.
//		ifGost		  - Тип криптоалгоритма ГОСТ/RSA.
//		out_pParamBuf - Указатель на выходной буфер для извлекаемого значения параметра.
//		out_pParamLen - Указатель на длину возвращённого значения.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Получение значения версионного конфигурационного параметра.
IL_FUNC IL_RETCODE prmGetParameterKeyVer(IL_WORD ParamId, IL_BYTE KeyVer, IL_BYTE ifGost, IL_BYTE *out_pParamBuf, IL_DWORD *out_pParamLen);

//  Description:
//      Извлекает значение конфигурационного параметра из файла 'sectors.ini' по идентификатору сектора и наименованию параметра.
//  See Also:
//  Arguments:
//      SectorId	  - Идентификатор сектора прикладных данных. 
//		SectorVer	  - Версия формата сектора.
//		in_pParamName - Указатель на строку с именем параметра.
//		out_pParamBuf - Указатель на выходной буфер с Win-1251-строкой извлекаемого значения параметра.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Получение значения конфигурационного параметра из файла 'sectors.ini'.
IL_FUNC IL_RETCODE prmGetParameterSectorEx(IL_WORD SectorId, IL_BYTE SectorVer, IL_CHAR *in_pParamName, IL_CHAR *out_pParamBuf);

//  Description:
//      Обновляет существующию или добавляет новую секцию внешнего описателя прикладных данных конфигурационного файла 'sectors.ini'.
//  See Also:
//  Arguments:
//      SectorId	  - Идентификатор сектора прикладных данных. 
//		SectorVer	  - Версия формата сектора.
//		SectorExDescr - Строка-описатель обновляемой секции сектора данных следующего формата:<p/><p/>
//								"Icon=S;
//								BlockDecr1=T|L|N;DataDecr11=d|t|l|n;DataDecr12=d|t|l|n;...;DataDecr1N=d|t|l|n;<p/>
//								BlockDecr2=T|L|N;DataDecr21=d|t|l|n;DataDecr22=d|t|l|n;...;DataDecr2N=d|t|l|n;<p/>
//								. . .<p/>
//								BlockDecrN=T|L|N;DataDecrN1=d|t|l|n;DataDecrN2=d|t|l|n;...;DataDecrNN=d|t|l|n;" , где:
//								- S - Наименование сектора данных.
//								- T - Тип доступа к данным юлока.
//								- L - Длина файла блока.
//								- N - Наимнование блока.
//								- d - Идентификатор элемента данных. Для бинарного файла смещение элемента данного от начала файла. Для TLV-файла шестнадцатиричное значение тега элемента данных. 
//								- t - Тип элемента данных.
//								- l - Максимальная длина элемента данных.
//								- n - Наименование элемента данных.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Обновление секции внешнего описателя прикладных данных конфигурационного файла 'sectors.ini'.
IL_FUNC IL_RETCODE prmWriteSectorExDescr(IL_WORD SectorId, IL_BYTE SectorVer, IL_CHAR *SectorExDescr);

//  Description:
//      Извлекает значение конфигурационного параметра из файла 'iqfront.ini' по наименованию параметра.<p/>
//		Используется в конфигурации TellME.
//  See Also:
//  Arguments:
//      SectionName	  - Наименовании секции конфигурационного файла. 
//		in_pParamName - Указатель на строку с именем параметра.
//		out_pParamBuf - Указатель на выходной буфер с Win-1251-строкой извлекаемого значения параметра.
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Получение значения конфигурационного параметра из файла 'iqfront.ini'.
IL_FUNC IL_RETCODE prmGetParameterIqFront(IL_CHAR *SectionName, IL_CHAR *in_pParamName, IL_CHAR *out_pParamBuf);

//  Description:
//      Извлекает значение конфигурационного параметра из секции 'PatternDescr' файла 'sectors.ini' по наименованию параметра.<p/>
//		Используется при получении строки описателя шаблонов документов бинарных блоков TLV-данных.
//  See Also:
//  Arguments:
//		in_pParamName - Указатель на строку с именем параметра.
//		out_pParamBuf - Указатель на выходной буфер с Win-1251-строкой извлекаемого значения параметра.
//      MaxBufLen     - Максимальный размер выходного буфера
//  Return Value:
//      IL_RETCODE	- Код ошибки.
//  Summary:
//      Получение значения конфигурационного параметра из секции 'PatternDescr' файла 'sectors.ini'.
IL_FUNC IL_RETCODE prmGetParameterPattern(IL_CHAR *ParamName, IL_CHAR *out_ParamBuf, IL_WORD MaxBufLen);

//
// Работа через сервис WCF
//
IL_FUNC void prmSetUecServiceToken(WCHAR* pwszUecServiceToken);
IL_FUNC IL_RETCODE prmGetParameterByService(IL_WORD ParamId, IL_BYTE *out_pParamBuf, IL_DWORD *out_pParamLen);
IL_FUNC IL_RETCODE prmGetParameterKeyVerByService(IL_WORD ilParam, IL_BYTE KeyVer, IL_BYTE ifGostCrypto, IL_BYTE* pCertBuf, IL_DWORD* pdwCertLen);

//
// Работа через глобальные переменные
//
IL_FUNC void prmSetGlobalConfigurationData(WCHAR* pwszReaderName, IL_BYTE* g_szOKO1OpenCert, IL_BYTE* g_szTerminalOpenCert, IL_BYTE* g_szUC1OpenCert, IL_BYTE* g_szTerminalClosedCertGOST);
IL_FUNC IL_RETCODE prmGetParameterByGlobalConfigurationData(IL_WORD ilParam, IL_BYTE* pParamBuf, IL_DWORD* pdwParamLen);
IL_FUNC IL_RETCODE prmGetParameterKeyVerByByGlobalConfigurationData(IL_WORD ilParam, IL_BYTE KeyVer, IL_BYTE ifGostCrypto, IL_BYTE* pCertBuf, IL_DWORD* pdwCertLen);

#endif //_HAL_PARAMETER_H_