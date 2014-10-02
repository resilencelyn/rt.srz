#include <string.h>
#include "HAL_Common.h"


IL_FUNC	IL_INT	cmnMemCmp (const IL_BYTE *s1, const IL_BYTE *s2, IL_WORD n)
{
	return memcmp(s1, s2, n);
}

//IL_FUNC	IL_INT	cmnMemICmp(IL_BYTE *s1, IL_BYTE *s2, IL_WORD n)
//{
//	return memicmp(s1, s2, n);
//}

IL_FUNC	IL_CHAR* cmnStrCopy (IL_CHAR *dest, const IL_CHAR *src)
{
	return strcpy(dest, src);
}

IL_FUNC	IL_CHAR* cmnStrCat (IL_CHAR *dest, const IL_CHAR *src)
{
	return strcat(dest, src);
}

IL_FUNC	IL_WORD	cmnStrLen (const IL_CHAR *src)
{
	return (IL_WORD)strlen(src);
}

IL_FUNC	IL_INT cmnStrCmp (const IL_CHAR *str1, const IL_CHAR *str2)
{
	return strcmp(str1, str2);
}

IL_FUNC	IL_BYTE* cmnMemSet (IL_BYTE *s, IL_INT c, IL_WORD n)
{
	return memset(s, c, n);
}

IL_FUNC	IL_BYTE* cmnMemCopy (IL_BYTE *dest, const IL_BYTE *src, IL_WORD n)
{
	return memcpy(dest, src, n);
}

IL_FUNC	IL_BYTE* cmnMemMove(IL_BYTE *dest, const IL_BYTE *src, IL_WORD n)
{
	return memmove(dest, src, n);
}

IL_FUNC	IL_BYTE* cmnMemClr (IL_BYTE *dest, IL_WORD n)
{
	return memset(dest, 0, n);
}

//+++
IL_FUNC IL_BYTE* cmnMemAlloc(IL_WORD size)
{
	return malloc(size);
}

//+++
IL_FUNC void cmnMemFree(void *buf)
{
	if(buf)
		free(buf);
}




