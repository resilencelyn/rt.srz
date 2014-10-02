#include <windows.h>
#include <stdio.h>
#include "tlv.h"
#include "il_error.h"
#include "HAL_Common.h"

typedef struct TAG
{
    IL_TAG Tag;
    IL_CHAR* TagName;
} TAG;

TAG TAGS[] = 
{
    0x42,"Issuer Identification Number",
    0x51,"DF or EF File Reference",
    0x5A,"Application Primary Account Number (PAN)",
    0x65,"Certificate Issuer Data",
    0x6F,"File Control Information (FCI) Template",
    0x80,"Application Certificate Key Id",
    0x84,"Dedicated File (DF) Name",
    0x87,"Application Priority Indicator",
    0xA5,"File Control Information (FCI) Proprietary Template",
    0xC2,"Application Usage Control",
    0xE0,"Data Sector Directory",
    0xE1,"Data Sector Directory Record",
    0xE2,"Data Block Directory",
    0xE3,"Data Block Directory Record",
    0xEA,"Application Public Key GOST",
    0xEB,"Application Public Key RSA",

    0x5F20,"Cardholder Name",
    0x5F24,"Expiration Date",
    0x5F25,"Effective Date",
    0x5F29,"Certificate Profile Identifier",
    0x5F2B,"Date of Birth",
    0x5F35,"Sex",
    0x5F37,"Signature",
    0x5F40,"Cardholder Portrait Image",
    0x5F42,"Address",
    0x5F4C,"Certificate Holder Reference",
    0x5F74,"Application Cryptogramm",
    0x7F21,"Public Key Certificate",
    0x7F49,"Public Key",
    0x7F4C,"Certificate Holder Authorization Template",
    0x7F4E,"Certificate Body",
    0x9F03,"Extra Data",
    0x9F08,"Application Version",
    0x9F13,"Identification Operation Id",
    0x9F15,"Service Identifier",
    0x9F1C,"Terminal Info",
    0x9F21,"Operation Timestamp",
    0x9F27,"Application Authentication Code",
    0x9F35,"Operation Info",
    0x9F36,"Operation Cryptogramm Counter",
    0x9F37,"Unpredictable Number",
    0x9F4B,"Identification Digital Signature",
    0xDF01,"Current Date",
    0xDF02,"Request Hash",
    0xDF20,"Data Operator Identifier",
    0xDF21,"Data Sector Format Version",
    0xDF22,"Data Block Identifier",
    0xDF23,"Card Issuer Adress",
    0xDF24,"Place of Birth",
    0xDF25,"Phone Number",
    0xDF26,"E-mail",
    0xDF27,"Social Account Number",
    0xDF71,"IC Challenge",
    0xDF72,"Host Challenge",
    0xDF73,"Issuer Cryptogramm",
    0, ""
};

char* _GetTagName(IL_BYTE* data, IL_DWORD taglen)
{   
    DWORD tag, i;
    for(tag = 0, i = 0; i < taglen ;i++)
        tag  = tag<<8 | data[i];
    for(i = 0; TAGS[i].Tag;i++)
    {   if(TAGS[i].Tag == tag)
    return TAGS[i].TagName;
    }
    return "Unknown TAG";
}

char* GetTagName(IL_TAG tag)
{
    DWORD i;
    for(i = 0; TAGS[i].Tag;i++)
    {   if(TAGS[i].Tag == tag)
    return TAGS[i].TagName;
    }
    return "Unknown TAG";
}

IL_DWORD GetTagLen(IL_BYTE* data)
{
    IL_DWORD taglen = 1;
    if((data[taglen-1] & 0x1F) == 0x1F)     //if taglen more than 1 
    {   taglen++;
    for(;;)
    {   if(data[taglen-1] & 0x80)         
    taglen++;
    else
        break;
    }
    }
    return taglen;
}

IL_TAG GetTag(IL_BYTE* data)
{
    IL_TAG tag = 0;

	if(data && data[0])
	{
		IL_DWORD i;
		IL_DWORD taglen = GetTagLen(data);
		for(i=0; i<taglen; i++)
			tag = (tag<<8) + data[i];
	}

    return tag;
}

IL_DWORD GetLenLen(IL_BYTE* data)
{
    IL_DWORD taglen = GetTagLen(data);
    IL_DWORD lenlen;

    if(data[taglen] & 0x80)
        lenlen=(data[taglen] & 0x7F)+1;
    else
        lenlen = 1;

    return lenlen;
}

IL_DWORD GetDataLen(IL_BYTE* data)
{
    IL_DWORD taglen = GetTagLen(data);
    IL_DWORD lenlen = GetLenLen(data);
    IL_DWORD datalen = 0;
    DWORD i;

    if(lenlen==1)
        datalen = data[taglen];
    else
        for(i=0; i < lenlen-1; i++)
            datalen = (datalen<<8) + data[taglen+1+i];

    return datalen;
}

BYTE* TagParse(IL_BYTE* data, IL_BYTE level, IL_DWORD maxlen)
{   
    IL_DWORD i;
    DWORD j;
    IL_DWORD taglen = 1;
    IL_DWORD datalen = 0;
    IL_DWORD lenlen;
    IL_BYTE* nextdata = NULL;
    IL_BYTE* ptr = NULL;

    taglen = GetTagLen(data);
    lenlen = GetLenLen(data);
    datalen = GetDataLen(data);

    datalen = (datalen>maxlen)?(maxlen-taglen-lenlen):datalen;
    maxlen = datalen;

    for(i = 0; i < level; i++)
        printf("   ");
    for(i = 0; i < taglen; i++)
        printf("%02X ", data[i]);
    for(i = 0; i < lenlen; i++)
        printf("%02X ", data[taglen+i]);
    printf(" - %s", _GetTagName(data,taglen));
    printf("\n");

    if(data[0]&0x20)
    {//if constructed data object
        nextdata = &data[taglen+lenlen];
        for(;maxlen>0;)
        {   ptr = TagParse(nextdata, (IL_BYTE)(level+1), maxlen);
        if(nextdata >= &data[taglen+datalen+lenlen])
            break;

        maxlen -= (ptr-nextdata);
        nextdata = ptr;
        }
        return nextdata;
    }
    else
    {//if primitive data object
        for(i = 0; i < datalen; )
        {   IL_DWORD portion_len;
        for(j = 0; j < level; j++)
            printf("   ");
        for(j = 0; j < taglen; j++)
            printf("   ");
        for(j = 0; j < lenlen; j++)
            printf("   ");
        portion_len = (datalen-i)<16 ? datalen-i : 16;
        for(j=0; j<portion_len; j++, i++)
        {
            printf("%02X ", data[taglen+i+lenlen]);
        }
        printf("\n");
        }
        return &data[taglen+datalen+lenlen];
    }
}

//data - ptr to TLV-coded dataset
//maxlen - size of TLV-coded dataset
//pTagPath - pointer to array of TAGS (full path to TAG we want to find)  
//NumTags - size of array of TAGS
//pdwLen - returns len of TAG data or Tag+Len+Data if TAG found
//ppTagData - returns pointer to TAG data if TAG found 
IL_FUNC IL_BYTE* _TagFind(IL_BYTE* data, IL_DWORD maxlen, const IL_TAG* pTagPath, IL_DWORD NumTags, IL_DWORD* pdwLen, IL_BYTE** ppTagData, BYTE ifWithTag)
{   
    DWORD i;
    DWORD taglen = 0;
    DWORD datalen = 0;
    DWORD lenlen = 0;
    BYTE* nextdata = NULL;
    BYTE* ptr = NULL;
    DWORD CurrTagIndex;
    const IL_TAG* pCurrTagPath;
    BYTE TagFound;

    *ppTagData = NULL;
    *pdwLen = 0;
    CurrTagIndex = 0;
    pCurrTagPath = pTagPath;
    TagFound = 0;

    while(maxlen>0)
    {
        taglen = GetTagLen(data);
        lenlen = GetLenLen(data);
        datalen = GetDataLen(data);

        for(i = 0; i<taglen; i++)
            if((*pCurrTagPath >> (8*(taglen-1-i)) & 0xFF) != data[i])
                break;

        if(i==taglen)
        {
            TagFound = 1;
            if(NumTags==1)
            {
                *ppTagData = ifWithTag?data:&data[taglen+lenlen];
                *pdwLen = ifWithTag?(taglen+lenlen+datalen):datalen;
                return &data[taglen+lenlen];
            }
        }

        if((data[0]&0x20) && TagFound)
        {//if constructed data object
            nextdata = &data[taglen+lenlen];
            maxlen = datalen;
            for(;(maxlen>0) && (NumTags>1);)
            {   ptr = _TagFind(nextdata, maxlen, &pCurrTagPath[1], NumTags-1, pdwLen, ppTagData, ifWithTag);
            if(*ppTagData!= NULL)
                break;
            else
                TagFound = 0;
            if(nextdata >= &data[taglen+datalen+lenlen])
                break;

			if(maxlen > (IL_DWORD)(ptr-nextdata)) //ssm+++
				maxlen -= (ptr-nextdata);
			else
                maxlen = 0;

            nextdata = ptr;
            }
            return nextdata;
        }
        else
        {//if primitive data object
            data = &data[taglen+lenlen+datalen];
            if(maxlen > taglen+lenlen+datalen)
                maxlen -= (taglen+lenlen+datalen);
            else
                maxlen = 0;
        }
    }
    return &data[taglen+lenlen+datalen];
}

IL_DWORD AddTag(const IL_TAG tag, IL_BYTE* data_in, IL_DWORD in_len, IL_BYTE* data_out)
{
    IL_DWORD tag_len;
    IL_DWORD len_len;
    IL_DWORD out_len = 0;
    IL_DWORD i;
    IL_DWORD offset;
    IL_BYTE bTmp;

    if(tag > 0x00FFFFFF)
        tag_len = 4;
    else if(tag > 0x0000FFFF)
        tag_len = 3;
    else if(tag > 0x000000FF)
        tag_len = 2;
    else 
        tag_len = 1;

    if(in_len > 0x00FFFFFF)
        len_len = 5;
    else if(in_len > 0x0000FFFF)
        len_len = 4;
    else if(in_len > 0x000000FF)
        len_len = 3;
    else if(in_len > 0x0000007F)
        len_len = 2;
    else
        len_len = 1;

    out_len = tag_len + len_len + in_len;

	if(!data_in || !data_out)
		return out_len;

    cmnMemMove(&data_out[tag_len + len_len], data_in, (IL_WORD)in_len);

    offset = 0;
    for(i = 0; i < tag_len; i++)
    {
        bTmp = (IL_BYTE)(tag>>((tag_len - i - 1)*8));
        data_out[offset++] = bTmp;
    }

    if(len_len > 1)
    {
        data_out[offset] = (BYTE)(0x80 + len_len - 1);
        len_len--;
        offset++;
    }

    for(i = 0; i < len_len; i++)
        data_out[offset++] = (BYTE)(in_len>>((len_len - i - 1)*8));

    return out_len;
}

IL_FUNC IL_RETCODE TagFind(IL_BYTE* data, IL_DWORD maxlen, const IL_TAG dwTag, IL_DWORD* pdwLen, IL_BYTE** ppTagData, BYTE ifWithTag)
{
    _TagFind(data, maxlen, &dwTag, 1, pdwLen, ppTagData, ifWithTag);

    if(*ppTagData == NULL)
        return ILRET_DATA_TAG_NOT_FOUND;

    return 0;
}

IL_FUNC IL_RETCODE TagFindByPath(IL_BYTE* data, IL_DWORD maxlen, const IL_TAG* pTagPath, IL_DWORD NumTags, IL_DWORD* pdwLen, IL_BYTE** ppTagData, BYTE ifWithTag)
{
    _TagFind(data, maxlen, pTagPath, NumTags, pdwLen, ppTagData, ifWithTag);

    if(*ppTagData == NULL)
        return ILRET_DATA_TAG_NOT_FOUND;

    return 0;
}

//++++
IL_FUNC IL_RETCODE GetTagOffsetByPath(IL_BYTE* data, IL_DWORD maxlen, const IL_TAG* pTagPath, IL_DWORD NumTags, IL_WORD *Offset, IL_DWORD* pdwLen, BYTE ifWithTag)
{
	IL_BYTE *pTag;

	_TagFind(data, maxlen, pTagPath, NumTags, pdwLen, &pTag, ifWithTag);
	if(pTag == NULL)
		return ILRET_DATA_TAG_NOT_FOUND;

	*Offset = (IL_WORD)(pTag - data);

	return 0;
}


