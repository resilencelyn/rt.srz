#include <string.h>
#include "HAL_Common.h"
#include "il_error.h"

IL_FUNC IL_RETCODE hex2bin(IL_CHAR* str, IL_BYTE* buff, IL_DWORD* pdwLen)
{   
    IL_WORD i;
    IL_WORD k;

	if(strlen(str)%2)
		return ILRET_INVALID_HEX_STRING_FORMAT;

	for(i = k = 0; k < strlen(str); k++)
	{
		if(!ISXDIGIT(str[k]))
            return ILRET_INVALID_HEX_STRING_FORMAT;

		if(k%2)
			buff[i-1] |= ASC2DIG(TOUPPER(str[k]));
		else
			buff[i++] = (IL_BYTE)((ASC2DIG(TOUPPER(str[k]))) << 4);
	}
	
	*pdwLen = i;
	return 0;
}

IL_FUNC IL_CHAR* bin2hex( IL_CHAR* str, IL_BYTE* buff, IL_DWORD data_len)
{
    IL_DWORD k;
    for(k=0; k < data_len; k++)
    {
        str[k*2]   = DIG2ASC(buff[k] >> 4);
        str[k*2+1] = DIG2ASC(buff[k] & 0x0F);
    }
    str[k*2] = 0;

    return str;
}

IL_FUNC IL_RETCODE Iso8859_2_Ansi(IL_CHAR* str, IL_CHAR* str_out)
{
	int i;

	for(i = 0; str[i]; i++)
	{
		str_out[i] = str[i];
		if((IL_BYTE)str[i] >= 0x80)
		{
			if((IL_BYTE)str[i] == 0xA1)
				str_out[i] += 0x07;	// '¨'
			else if((IL_BYTE)str[i] == 0xF1)
				str_out[i] -= 0x39;	// '¸'
			else if((IL_BYTE)str[i] >= 0xB0 && (IL_BYTE)str[i] <= 0xEF)
				str_out[i] += 0x10;	// 'A'...'ÿ'
			else
				str_out[i] = 0x3F;	// '?'
		}
	}
	
	str_out[i] = 0x00;

    return 0;
}

IL_FUNC IL_RETCODE Ansi_2_Iso8859(IL_CHAR* str, IL_CHAR* str_out)
{
	int i;

	for(i = 0; str[i]; i++)
	{
		str_out[i] = str[i];
		if((IL_BYTE)str[i] >= 0x80)
		{
			if((IL_BYTE)str[i] == 0xA8)
				str_out[i] -= 0x07;	// '¨'
			else if((IL_BYTE)str[i] == 0xB8)
				str_out[i] += 0x39;	// '¸'
			else if((IL_BYTE)str[i] >= 0xC0)
				str_out[i] -= 0x10;	// 'A'...'ÿ'
			else
				str_out[i] = 0x3F;	// '?'
		}
	}
	
	str_out[i] = 0x00;

    return 0;
}

IL_BYTE toBcd(IL_BYTE b)
{
    return ((b/10)<<4) + b%10;
}

IL_FUNC void bcd2str(IL_BYTE *bcd, IL_WORD bcd_len, IL_CHAR *out_str)
{
    IL_CHAR *s;
    IL_BYTE b;
    IL_BYTE *bcd_max;

    if(!bcd || !out_str) return;

    for(s = out_str, bcd_max = bcd+bcd_len; bcd < bcd_max; bcd++)
    {
        b = *bcd >> 4;
        *s = (b > 0x09) ? 0 : b + 0x30;
        if(*s++ == 0) return;
        b = *bcd & 0x0F;
        *s = (b > 0x09) ? 0 : b + 0x30;
        if(*s++ == 0) return;
    }

    *s = 0;
}

IL_FUNC void Iso5218_2_Ansi(IL_BYTE *pData, IL_CHAR *out_str)
{
    if(!pData || !out_str) return;

    if(*pData == 0x01)
        *out_str++ = 'Ì';
    else if(*pData == 0x02)
        *out_str++ = 'Æ';
    else
        *out_str++ = '?';

    *out_str = 0;
}

IL_FUNC void Ansi_2_Iso5218(IL_CHAR *str, IL_BYTE *out_pData)
{
    if(!str || !out_pData) return;

	if(*str == 'Ì')
		*out_pData = 0x01;
	else if(*str == 'Æ')
		*out_pData = 0x02;
	else 
		*out_pData = 0x00;
}

IL_FUNC void str2bcdF(IL_CHAR *str, IL_BYTE *bcd, IL_WORD max_len)
{
    IL_BYTE i, j, n;

    n = (IL_BYTE)cmnStrLen(str);
    cmnMemSet(bcd, 0xFF, max_len);
    for(j = 0, i = 0; i < n; j++)
    {
        bcd[j]  = ((str[i++] - 0x30) << 4);
        bcd[j] |= (i < n ? (str[i++] - 0x30) : 0x0F);
    }
}

IL_FUNC void str2bcd0(IL_CHAR *str, IL_BYTE *bcd, IL_WORD max_len)
{
    IL_INT i, j, n;

    n = (IL_BYTE)cmnStrLen(str);
    cmnMemSet(bcd, 0x00, max_len);
    
	for(j = max_len - 1, i = n-1; i >= 0; j--)
    {
		bcd[j]  = (str[i--] - 0x30);
		if(i >= 0)
			bcd[j] |= (i >= 0 ? ((str[i--] - 0x30) << 4) : 0x00);
    }
}

IL_FUNC void str2pin(IL_CHAR *str, IL_BYTE *pin)
{
    IL_BYTE i, j, n;

    n = (IL_BYTE)cmnStrLen(str);
    cmnMemSet(pin, 0xFF, 8);
    pin[0] = ((0x02 << 4) | n);
    for(j = 1, i = 0; i < n; j++)
    {
        pin[j]  = ((str[i++] - 0x30) << 4);
        pin[j] |= (i < n ? (str[i++] - 0x30) : 0x0F);
    }
}

IL_FUNC IL_BYTE* bin2dw(IL_BYTE *bin, IL_DWORD *dw)
{
	if(!bin || !dw) return NULL;
	*dw = ((bin[3] << 24) + (bin[2] << 16) + (bin[1] << 8) + bin[0]);
	
	return bin+4;
}

IL_FUNC IL_BYTE* dw2bin(IL_DWORD dw, IL_BYTE *bin)
{
	if(!bin) return NULL;
	bin[0] = (IL_BYTE)(dw & 0x000000FF);
	bin[1] = (IL_BYTE)((dw >> 8)  & 0x000000FF);
	bin[2] = (IL_BYTE)((dw >> 16) & 0x000000FF);
	bin[3] = (IL_BYTE)((dw >> 24) & 0x000000FF);

	return bin+4;
}

IL_FUNC void swap(IL_BYTE *bin, IL_DWORD len)
{
    IL_DWORD i;
    IL_BYTE tmp;
    
	for(i = 0; i < len/2; i++)
	{
	    tmp = bin[i];
	    bin[i] = bin[len - i - 1];
	    bin[len - i - 1] = tmp;
	}
}

/***
IL_FUNC IL_BYTE* bin2w(IL_BYTE *bin, IL_WORD *w)
{
	if(!bin || !w) return NULL;
	*w = ((bin[1] << 8) + bin[0]); 
	
	return bin+2;
}

IL_FUNC IL_BYTE* w2bin(IL_WORD w, IL_BYTE *bin)
{
	if(!bin) return NULL;
	bin[0] = (IL_BYTE)(w & 0x00FF);
	bin[1] = (IL_BYTE)((w >> 8) & 0x00FF);

	return bin+2;
}
***/
 

