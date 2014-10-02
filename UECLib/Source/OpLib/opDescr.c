#include <stdio.h>
#include "opCmnFunc.h"


static SECTOR_DESCR SectorsDescr[] =
{
{ 0, "" }
};

static BLOCK_DESCR BlocksDescr[] =
{
{ 0,    0,  BLOCK_DATA_TLV, 0,		"Блок корневых данных" },
{ 0, 0, 0, 0, "" }
};

static DATA_DESCR DataDescr[] =
{
// SId BId	TagId   Type			MaxLen  TagPath isMust Name				   
{ 0,	0,	0x9F08,	DATA_NUMERIC,	4,		{0,0,0},  1,   "Версия" },
{ 0,	0,	0x5F25,	DATA_NUMERIC,	6,		{0,0,0},  1,   "Дата начала" },
{ 0,	0,	0x5F24,	DATA_NUMERIC,	6,		{0,0,0},  1,   "Дата завершенния" },
{ 0,	0,	0x5A,	DATA_NUMERIC,	19,		{0,0,0},  1,   "PAN" },
{ 0,	0,  0,		0,				0,		{0,0,0},  0, 	"" }
};

IL_WORD _GetExSectorDescr(s_opContext *p_opContext, IL_WORD sectorId, SECTOR_DESCR **ppSectorDescr)
{
	IL_WORD i;
	IL_BYTE sectorVer;

	// ищем описатель сектора во внешних описателях контекста
	for(i = 0; i < p_opContext->ExSectorsNum; i++)
	{
		if(p_opContext->ExSectorDescr[i].Id == sectorId)
		{
			*ppSectorDescr = &p_opContext->ExSectorDescr[i];
			return 0;
		}
	}

	// проверим переполнение списка внешних описателей блоков
	if(p_opContext->ExSectorsNum + 1 > MAX_EX_SECTORS)
		return ILRET_OPLIB_SECTORS_EX_DESCR_IS_OVER;

	// инициализируем описатель сектора из внешнего файла
	sectorVer = flGetSectorVersion(p_opContext->phCrd, (IL_BYTE)sectorId);
	if(!prmGetParameterSectorEx(sectorId, sectorVer, "Icon", p_opContext->ExSectorDescr[p_opContext->ExSectorsNum].Icon))
	{
		p_opContext->ExSectorDescr[p_opContext->ExSectorsNum++].Id = sectorId;
		*ppSectorDescr = &p_opContext->ExSectorDescr[p_opContext->ExSectorsNum-1];
		return 0;
	}

	return ILRET_OPLIB_SECTOR_DESCR_NOT_FOUND;
}

IL_FUNC IL_WORD opDescrGetSector(s_opContext *p_opContext, IL_WORD sectorId, SECTOR_DESCR **ppSectorDescr)
{
	IL_WORD RC;

	// проверим валидность входных аргументов
	if(!p_opContext || !ppSectorDescr)
		return ILRET_OPLIB_INVALID_ARGUMENT;
	
	// ищем описатель указанного сектора во внутренней таблице
	for(*ppSectorDescr =  &SectorsDescr[0]; (*ppSectorDescr)->Icon[0]; (*ppSectorDescr)++)
		if((*ppSectorDescr)->Id == sectorId)
			return 0;

	// получаем/инициализируем описатель сектора из внешнего файла в контекст 
	RC = _GetExSectorDescr(p_opContext, sectorId, ppSectorDescr); 
	return RC;
}

IL_WORD _GetExBlockDescr(s_opContext *p_opContext, IL_WORD sectorId, IL_WORD blockId, BLOCK_DESCR **ppBlockDescr)
{
	IL_WORD i;

	// ищем описатель блока во внешних описателях контекста
	for(*ppBlockDescr = NULL, i = 0 ; i < p_opContext->ExBlocksNum; i++)
	{
		if(p_opContext->ExBlockDescr[i].SectorId == sectorId && p_opContext->ExBlockDescr[i].Id == blockId)
		{
			*ppBlockDescr = &p_opContext->ExBlockDescr[i];
			return 0;
		}
	}

	// инициализируем описатели блоков сектора из внешнего файла
	{
		IL_CHAR ParamName[15];
		IL_CHAR ParamDescr[100];
		IL_INT  type, rootTag, tag, size, tagPath[3], isMust;
		IL_WORD b, d, i;
		IL_BYTE sectorVer = flGetSectorVersion(p_opContext->phCrd, (IL_BYTE)sectorId);

		for(b = 1; ; b++, p_opContext->ExBlocksNum++) 
		{
			// получим из внешнего файла описатель блока
			sprintf(ParamName, "BlockDescr%u", b);
			if(prmGetParameterSectorEx(sectorId, sectorVer, ParamName, ParamDescr))
				break;

			// проверим переполнение списка внешних описателей блоков
			if(p_opContext->ExBlocksNum + 1 > MAX_EX_BLOCKS)
				return ILRET_OPLIB_BLOCKS_EX_DESCR_IS_OVER;

			// инициализируем описатель блока в контекст
			p_opContext->ExBlockDescr[p_opContext->ExBlocksNum].SectorId = sectorId;
			p_opContext->ExBlockDescr[p_opContext->ExBlocksNum].Id = b;
			sscanf(ParamDescr, "%u|%X|%s", &type, &rootTag, &p_opContext->ExBlockDescr[p_opContext->ExBlocksNum].Icon);
			p_opContext->ExBlockDescr[p_opContext->ExBlocksNum].FileType = (IL_BYTE)type;	
			p_opContext->ExBlockDescr[p_opContext->ExBlocksNum].RootTag = (IL_DWORD)rootTag;
			if(b == blockId)
				*ppBlockDescr = &p_opContext->ExBlockDescr[p_opContext->ExBlocksNum]; 

			// инициализируем данные блока
			for(d = 1; ; d++, p_opContext->ExDatasNum++)
			{	// ищем описатель элемента данного во внешнем файле
				sprintf(ParamName, "DataDescr%u%u", b, d);
				if(prmGetParameterSectorEx(sectorId, sectorVer, ParamName, ParamDescr))
					break;

				// проверим переполнение списка внешних описателей элементов данных блока
				if(p_opContext->ExDatasNum + 1 > MAX_EX_DATAS)
					return ILRET_OPLIB_DATAS_EX_DESCR_IS_OVER;

				// инициализируем описатель данного в контекст
				p_opContext->ExDataDescr[p_opContext->ExDatasNum].SectorId = sectorId; 
				p_opContext->ExDataDescr[p_opContext->ExDatasNum].BlockId  = b; 
				if(p_opContext->ExBlockDescr[p_opContext->ExBlocksNum].FileType == BLOCK_DATA_BIN 
						|| p_opContext->ExBlockDescr[p_opContext->ExBlocksNum].FileType == BLOCK_DATA_RECORD) 
					sscanf(ParamDescr, "%u|%u|%u|%X,%X,%X|%u", &tag, &type, &size, &tagPath[0], &tagPath[1], &tagPath[2], &isMust);
				else
					sscanf(ParamDescr, "%X|%u|%u|%X,%X,%X|%u", &tag, &type, &size, &tagPath[0], &tagPath[1], &tagPath[2], &isMust);
				for(i = cmnStrLen(ParamDescr); i && ParamDescr[i] != '|'; i--) ;
				cmnStrCopy(p_opContext->ExDataDescr[p_opContext->ExDatasNum].Name, &ParamDescr[++i]);
				p_opContext->ExDataDescr[p_opContext->ExDatasNum].TagId = (IL_DWORD)tag;
				p_opContext->ExDataDescr[p_opContext->ExDatasNum].Type = (IL_WORD)type;
				p_opContext->ExDataDescr[p_opContext->ExDatasNum].MaxLen = (IL_WORD)size;
				p_opContext->ExDataDescr[p_opContext->ExDatasNum].TPath[0] = (IL_WORD)tagPath[0];
				p_opContext->ExDataDescr[p_opContext->ExDatasNum].TPath[1] = (IL_WORD)tagPath[1];
				p_opContext->ExDataDescr[p_opContext->ExDatasNum].TPath[2] = (IL_WORD)tagPath[2];
				p_opContext->ExDataDescr[p_opContext->ExDatasNum].isMust   =  isMust;
			}
		}
	}

	return (*ppBlockDescr == NULL ? ILRET_OPLIB_BLOCK_DESCR_NOT_FOUND : 0);
}

IL_FUNC IL_WORD opDescrGetBlock(s_opContext *p_opContext, IL_WORD sectorId, IL_WORD blockId, BLOCK_DESCR **ppBlockDescr)
{
	IL_WORD RC;

	// проверим валидность входных аргументов
	if(!p_opContext || !ppBlockDescr)
		return ILRET_OPLIB_INVALID_ARGUMENT;
	
	// ищем описатель блока во внутренней таблице
	for(*ppBlockDescr =  &BlocksDescr[0]; (*ppBlockDescr)->Icon[0]; (*ppBlockDescr)++)
	{
		if((*ppBlockDescr)->SectorId == sectorId && (*ppBlockDescr)->Id == blockId)
			return 0;
	}
	
	// инициализируем описатели блоков из внешнего файла в контекст 
	RC = _GetExBlockDescr(p_opContext, sectorId, blockId, ppBlockDescr); 
	return RC;
}

IL_FUNC IL_WORD opDescrGetDataByTagId(s_opContext *p_opContext, IL_WORD sectorId, IL_WORD blockId, IL_DWORD TagId, DATA_DESCR **ppDataDescr)
{
	IL_WORD RC;
	BLOCK_DESCR *pBlockDescr;
	IL_WORD i;

	// проверим валидность входных аргументов
	if(!p_opContext || !ppDataDescr)
		return ILRET_OPLIB_INVALID_ARGUMENT;

	// ищем описатель данного во внутренней таблице
	for(*ppDataDescr =  &DataDescr[0]; (*ppDataDescr)->Name[0]; (*ppDataDescr)++)
	{
		if((*ppDataDescr)->SectorId == sectorId 
				&& (*ppDataDescr)->BlockId == blockId 
				&& (*ppDataDescr)->TagId == TagId)
			return 0;
	}

	// инициализируем описатели блоков сектора из внешнего файла в контекст (если отсутствуют)
	if((RC = _GetExBlockDescr(p_opContext, sectorId, blockId, &pBlockDescr)) != 0)
		return RC;

	// ищем описатель данного во внешних описателях контекста
	for(i = 0; i < p_opContext->ExDatasNum; i++)
	{
		if(p_opContext->ExDataDescr[i].SectorId == sectorId 
			&& p_opContext->ExDataDescr[i].BlockId == blockId
			&& p_opContext->ExDataDescr[i].TagId == TagId)
		{
			*ppDataDescr = &p_opContext->ExDataDescr[i];
			return 0;
		}
	}

	return ILRET_OPLIB_DATA_DESCR_NOT_FOUND;
}

IL_FUNC IL_WORD opDescrGetFirstDataInBlock(s_opContext *p_opContext, IL_WORD sectorId, IL_WORD blockId, DATA_DESCR **ppDataDescr)
{
	IL_WORD RC;
	BLOCK_DESCR *pBlockDescr;
	IL_WORD i;

	// проверим валидность входных аргументов
	if(!p_opContext || !ppDataDescr)
		return ILRET_OPLIB_INVALID_ARGUMENT;

	// ищем описатель данного во внутренней таблице
	for(*ppDataDescr =  &DataDescr[0]; (*ppDataDescr)->Name[0]; (*ppDataDescr)++)
	{
		if((*ppDataDescr)->SectorId == sectorId && (*ppDataDescr)->BlockId == blockId) 
			return 0;
	}

	// инициализируем описатели блоков сектора из внешнего файла в контекст (если отсутствуют)
	if((RC = _GetExBlockDescr(p_opContext, sectorId, blockId, &pBlockDescr)) != 0)
		return RC;

	// ищем описатель данного во внешних описателях контекста
	for(i = 0; i < p_opContext->ExDatasNum; i++)
	{
		if(p_opContext->ExDataDescr[i].SectorId == sectorId && p_opContext->ExDataDescr[i].BlockId == blockId)
		{
			*ppDataDescr = &p_opContext->ExDataDescr[i];
			return 0;
		}
	}
	
	return ILRET_OPLIB_DATA_DESCR_NOT_FOUND;
}






