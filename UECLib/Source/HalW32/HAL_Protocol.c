#include <windows.h>
#include "HAL_Protocol.h"
#include "HAL_Parameter.h"
#include "HAL_Rtc.h"

static PROTOCOL_DESCR protDescr;

IL_CHAR* _getExecDirName(IL_CHAR *pExecDirName)
{
 #ifndef TELLME
   static IL_CHAR tmpFileName[_MAX_PATH];
    IL_CHAR  drive[_MAX_DRIVE], dir[_MAX_DIR], name[_MAX_FNAME], old_ext[_MAX_EXT];
    GetModuleFileName(0,tmpFileName,sizeof(tmpFileName));

    _splitpath(tmpFileName, drive, dir, name, old_ext);

    _makepath(tmpFileName, drive, dir, "", "");

    if(pExecDirName)
        strcpy(pExecDirName, tmpFileName);
#else
	sprintf(pExecDirName, "c:\\UECParam\\");
#endif//TELLME

    return pExecDirName;
}

static void _CreateFullFileName(void)
{
	sprintf(protDescr.FullFileName, "%s20%02X%02X%02X_%s", 
		protDescr.FileDirName, 
		protDescr.DateStamp[0], protDescr.DateStamp[1], protDescr.DateStamp[2], 
		protDescr.FileName);	
}

IL_FUNC FILE* _protFileOpen(void)
{
	IL_CHAR DateStamp[3];

	rtcGetCurrentDate(DateStamp);
	if(memcmp(DateStamp, protDescr.DateStamp, 3))
	{
		memcpy(protDescr.DateStamp, DateStamp, 3);
		_CreateFullFileName();
	}

	if(!protDescr.hFile && protDescr.FullFileName[0])
		protDescr.hFile = fopen(protDescr.FullFileName, "a+");	

	return protDescr.hFile;
}

IL_FUNC void _protFileClose(void)
{
	if(protDescr.hFile)
		fclose(protDescr.hFile);
	protDescr.hFile = NULL;
}

// записать строку в файл логирования
IL_FUNC IL_RETCODE _ToLogFile(IL_CHAR *str)
{
	if(str && _protFileOpen())
	{
		IL_DWORD size = strlen(str);
		if(fwrite(str,  1,  size, protDescr.hFile) != size)
			return ILRET_PROT_LOGEILE_WRITE_ERROR;
		if(fwrite("\n", 1, 1, protDescr.hFile) != 1)
			return ILRET_PROT_LOGEILE_WRITE_ERROR;

		_protFileClose();
	}

	return 0;
}

IL_FUNC IL_RETCODE protInit(void)
{
	IL_RETCODE RC;
	IL_DWORD dwLen = 0;
	IL_BYTE buf[256] = {0};

	// обнулим структуру протоколирования
	memset((IL_BYTE*)&protDescr, 0, sizeof(PROTOCOL_DESCR));

	// считаем признак необходимости протоколировать в файл-журнал
	if(!(RC = prmGetParameter(IL_PARAM_PROT_LOGFILE, buf, &dwLen)) && buf[0]) 
	{	
		IL_CHAR DateTimeStamp[50];

		// и откроем файл-журнал для протоколирования
		if((RC = prmGetParameter(IL_PARAM_PROT_LOGNAME, protDescr.FileName, &dwLen)))
			return RC;
		if(!dwLen)
			return ILRET_PARAM_WRONG_LENGTH;

		_getExecDirName(protDescr.FileDirName); 
		rtcGetCurrentDate(protDescr.DateStamp);
		_CreateFullFileName();

		if(_protFileOpen() == NULL)
			return ILRET_PROT_LOGFILE_OPEN_ERROR;
		sprintf(buf, "\n\n==== ОТКРЫТИЕ СЕССИИ ПРОТОКОЛИРОВАНИЯ %s ======", rtcGetCurrentDateTimeStr(DateTimeStamp));
		_ToLogFile(buf);
	}

	// установим признак необходимости протоколировать в окно Output
	RC = prmGetParameter(IL_PARAM_PROT_OUTPUT, buf, &dwLen);
	if(!RC && buf[0])
		protDescr.ifOutput = buf[0];

	// установим признак необходимости протоколировать функции ридера
	if(!(RC = prmGetParameter(IL_PARAM_PROT_READER, buf, &dwLen)) && buf[0]) 
		protDescr.ifReader = buf[0];

	// установим признак необходимости протоколировать функции ридера
	if(!(RC = prmGetParameter(IL_PARAM_PROT_CARD, buf, &dwLen)) && buf[0]) 
		protDescr.ifCard = buf[0];
	
	// установим признак необходимости протоколировать функции CardLib
	if(!(RC = prmGetParameter(IL_PARAM_PROT_CARDLIB, buf, &dwLen)) && buf[0]) 
		protDescr.ifCardLib = buf[0];

	// установим признак необходимости протоколировать функции FuncLib
	if(!(RC = prmGetParameter(IL_PARAM_PROT_FUNCLIB, buf, &dwLen)) && buf[0])
		protDescr.ifFuncLib = buf[0];

	// установим признак необходимости протоколировать функции OpLib
	if(!(RC = prmGetParameter(IL_PARAM_PROT_OPLIB, buf, &dwLen)) && buf[0])
		protDescr.ifOpLib = buf[0];

	// установим признак необходимости протоколировать функции SmLib
	if(!(RC = prmGetParameter(IL_PARAM_PROT_SMLIB, buf, &dwLen)) && buf[0]) 
		protDescr.ifSmLib = buf[0];

	// установим признак необходимости протоколировать уровни TellMe
	if(!(RC = prmGetParameter(IL_PARAM_PROT_TELLME, buf, &dwLen)) && buf[0]) 
		protDescr.ifTellMe = buf[0];

	// установим признак необходимости внешнего протоколирования TellMe
	if(!(RC = prmGetParameter(IL_PARAM_PROT_EXTERN, buf, &dwLen)) && buf[0]) 
		protDescr.ifExtern = buf[0];

	return 0;
}

IL_FUNC IL_RETCODE protDeinit(void)
{
	if(_protFileOpen())
	{
		IL_CHAR str[256];
		IL_CHAR TimeStamp[21];
		sprintf(str, "==== ЗАКРЫТИЕ СЕССИИ ПРОТОКОЛИРОВАНИЯ %s ======", rtcGetCurrentDateTimeStr(TimeStamp));
		_ToLogFile(str);
		_protFileClose();
	}

	return 0;
}

IL_FUNC IL_BYTE _protWritePrefix(IL_WORD prot_source)
{
	char* prefix = "";

    switch(prot_source)
    {
		case PROT_ALWAYS:
			return 0;
		//case PROT_READER:
		//	if(!protDescr.ifReader)
		//		return 1;
		//	prefix = "READER:  ";
		//	break;
		//case PROT_CARD:
		//	if(!protDescr.ifCard)
		//		return 1;
		//	prefix = "CARD:    ";
		//	break;
		case PROT_READER1:
		case PROT_READER2:
		case PROT_READER3:
			if(protDescr.ifReader < (prot_source - PROT_READER1 + 1))
				return 1;
			prefix = "READER:  ";
			break;
		case PROT_CARDLIB1:
		case PROT_CARDLIB2:
		case PROT_CARDLIB3:
			if(protDescr.ifCardLib < (prot_source - PROT_CARDLIB1 + 1))
				return 1;
			prefix = "CARDLIB: ";
			break;
		case PROT_FUNCLIB1:
		case PROT_FUNCLIB2:
		case PROT_FUNCLIB3:
			if(protDescr.ifFuncLib < (prot_source - PROT_FUNCLIB1 + 1))
				return 1;
			prefix = "FUNCLIB: ";
			break;
		case PROT_OPLIB1:
		case PROT_OPLIB2:
		case PROT_OPLIB3:
			if(protDescr.ifOpLib < (prot_source - PROT_OPLIB1 + 1))
				return 1;
			prefix = "OPLIB:   ";
			break;
		case PROT_SMLIB:
			if(!protDescr.ifSmLib)
				return 1;
			prefix = "SMLIB:   ";
			break;
		case PROT_TELLME1:
		case PROT_TELLME2:
		case PROT_TELLME3:
			if(protDescr.ifTellMe < (prot_source - PROT_TELLME1 + 1))
				return 1;
			prefix = "TELLME:  ";
			break;
		case PROT_EXTERN:
			if(!protDescr.ifExtern)
				return 1;
			prefix = "EXTERN:  ";
			break;
		default:
			return 1;
    }

	if(protDescr.ifOutput)
	{	// вывод префикса в окно Output
		OutputDebugString("\r\n");
		OutputDebugString(prefix);
	}

	if(protDescr.FileName[0])
	{	// вывод префикса в файл журнала
		IL_DWORD size = strlen(prefix);
		_protFileOpen();
		if(fwrite(prefix,  1,  size, protDescr.hFile) != size)
			return 1;
		//_protFileClose();
	}

	return 0;

}

IL_FUNC IL_RETCODE protWriteEx(IL_WORD prot_source, const char *Format, ...)
{
	// проверим необходимость ведения системы протоколирования
	if(!(protDescr.hFile || protDescr.ifOutput))
		return 0;	// протоколирование не ведётся!!!

	// проверим нулевой указатель или строку
	if(Format == NULL || *Format == 0x00)
		return 0;	// нечего протоколировать!!!

	// проверим источник протоколируемый строки и отобразим соответствующий префикс
	if(_protWritePrefix(prot_source) != 0)
		return 0;	// не протоколируется

	// сформатируем и выведем строку протоколирования
	{
		va_list  arg;
		IL_CHAR str[1024*5]; // Переполнение буфера сообщения логирования не контролируется!!!

		va_start( arg, Format );
		vsprintf( str, (char*)Format, arg );
		va_end( arg );

		// вывод в окно Output
		if(protDescr.ifOutput)
			OutputDebugString(str);

		// вывод в LogFile
		if(protDescr.FileName[0])
			_ToLogFile(str);
	}
	
	return 0;
}

IL_FUNC IL_RETCODE protWrite(IL_WORD prot_source, IL_CHAR* str)
{
	char* prefix = "";

	if(!(protDescr.hFile || protDescr.ifOutput))
		return 0;	// Протоколирование не ведётся!!!

    switch(prot_source)
    {
		//case PROT_READER:
		//	if(!protDescr.ifReader)
		//		return 0;
		//	prefix = "RDR: ";
		//	break;
		//case PROT_CARD:
		//	if(!protDescr.ifCard)
		//		return 0;
		//	prefix = "CRD: ";
		//	break;
		case PROT_READER1:
		case PROT_READER2:
		case PROT_READER3:
			if(protDescr.ifOpLib < (prot_source - PROT_READER1 + 1))
				return 0;
			prefix = "READER: ";
			break;
		case PROT_CARDLIB1:
		case PROT_CARDLIB2:
		case PROT_CARDLIB3:
			if(protDescr.ifCardLib < (prot_source - PROT_CARDLIB1 + 1))
				return 0;
			prefix = "CARDLIB: ";
			break;
		case PROT_FUNCLIB1:
		case PROT_FUNCLIB2:
		case PROT_FUNCLIB3:
			if(protDescr.ifOpLib < (prot_source - PROT_FUNCLIB1 + 1))
				return 0;
			prefix = "FUNCLIB: ";
			break;
		case PROT_OPLIB1:
		case PROT_OPLIB2:
		case PROT_OPLIB3:
			if(protDescr.ifOpLib < (prot_source - PROT_OPLIB1 + 1))
				return 0;
			prefix = "OPLIB : ";
			break;
		case PROT_SMLIB:
			if(!protDescr.ifSmLib)
				return 0;
			prefix = "SMLIB: ";
			break;
		//default:
		//	return 0;
    }

	if(protDescr.ifOutput)
	{	// Вывод в окно Output
		OutputDebugString("\r\n");
		OutputDebugString(prefix);
		OutputDebugString(str);
	}

	if(protDescr.FileName[0])
	{	// Вывод в файл журнал
		_ToLogFile(prefix);
		_ToLogFile(str);
	}

    return 0;
}
