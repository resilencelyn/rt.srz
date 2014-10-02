#ifndef __OP_CMNFUNC_H_
#define __OP_CMNFUNC_H_

#ifdef __cplusplus
extern "C" {
#endif

#include "il_types.h"
#include "il_error.h"
#include "opDescr.h"
#include "opCtxDef.h"
#include "HAL_Common.h"
#include "FuncLib.h"
#include "tlv.h"
#include "TAG.h"
#include "HAL_Crypto.h"
#include "HAL_Rtc.h"
#include "HAL_Parameter.h"
#include "KeyType.h"

#ifdef GUIDE
	IL_FUNC void opCmnDisplayString(s_opContext *p_opContext, IL_CHAR *str);
	IL_FUNC void opCmnDisplayStringPrefix(s_opContext *p_opContext, IL_CHAR *prefix, IL_CHAR *filler);
	IL_FUNC void opCmnDisplayLine(s_opContext *p_opContext, IL_CHAR *str);
	#define OP_CMN_DISPLAY_STRING(c, s)				opCmnDisplayString((c), (s));
	#define OP_CMN_DISPLAY_STRING_PREFIX(c, s, p)	opCmnDisplayStringPrefix((c), (s), (p));
	#define OP_CMN_DISPLAY_LINE(c, s)				opCmnDisplayLine((c), (s));
#else
	#define OP_CMN_DISPLAY_STRING(c, s)	
	#define OP_CMN_DISPLAY_STRING_PREFIX(c, s, p)
	#define OP_CMN_DISPLAY_LINE(c, s)
#endif//GUIDE

///////////////////////////////////////////////////////////////////////////////////////////////////////
//	UEC_FUNC.C
///////////////////////////////////////////////////////////////////////////////////////////////////////
IL_FUNC IL_WORD opCmnRestoreCryptoSession(s_opContext *p_opContext);
IL_FUNC IL_WORD opCmnAppVerifyCitizen(s_opContext *p_opContext);
IL_FUNC IL_WORD opCmnGetDataByDataDescr(s_opContext *p_opContext, DATA_DESCR *in_DataDescr, IL_CHAR *out_str, IL_WORD *MaxLen);
IL_FUNC IL_WORD opCmnReadCardDataRaw(s_opContext *p_opContext, IL_WORD sectorId, IL_WORD blockId, IL_BYTE BlockType, IL_WORD TagId, IL_WORD *inout_pDataLen, IL_BYTE *out_pData); 
IL_FUNC IL_WORD opGetDataByTagId(s_opContext *p_opContext, IL_WORD sectorId, IL_WORD blockId, IL_DWORD TagId, IL_CHAR *out_str);
IL_FUNC IL_WORD opCmnPrepareAppAuthRequest(s_opContext *p_opContext);
IL_FUNC IL_WORD opCmnCheckAppAuthResponse(s_opContext *p_opContext, IL_WORD *out_authResult);
IL_FUNC IL_WORD opCmnChangePinPuk(s_opContext *p_opContext);
IL_FUNC IL_WORD opCmnGetPAN(s_opContext *p_opContext, IL_CHAR *out_str22);
IL_FUNC IL_WORD _opCmnGetAppPubKey(s_opContext *p_opContext, IL_BYTE *pDataOut, IL_WORD *pwDataLenOut);
IL_FUNC IL_WORD _opCmnGetIssPubKey(s_opContext *p_opContext, IL_BYTE *pDataOut, IL_WORD *pwDataLenOut);
IL_FUNC IL_WORD _opCmnGetAppSystemInfoTagId(s_opContext *p_opContext, IL_BYTE *resp, IL_WORD resp_len, IL_WORD TagId, IL_CHAR *out_str);
IL_FUNC IL_WORD _opCmnGetAppSystemInfo(s_opContext *p_opContext, IL_BYTE *resp, IL_WORD resp_len);
IL_FUNC IL_WORD _opCmnGetIcChallenge(s_opContext *p_opContext);
IL_FUNC IL_WORD opCmnUnlockTmpPUK(s_opContext *p_opContext);
IL_FUNC IL_WORD _opCmnCreateTmpPukBlock(IL_BYTE *out_PukBlock8);
IL_FUNC IL_WORD opCmnReadCardData(s_opContext *p_opContext, IL_CHAR *in_Str, IL_CHAR *out_Str, IL_WORD *out_StrLen);
IL_FUNC IL_BYTE _calculate_crc(IL_BYTE *buf, IL_WORD buf_len);
IL_FUNC IL_BYTE* _bin2apduin(IL_BYTE* bin, IL_APDU_PACK_ELEM *pApduElem);
IL_FUNC IL_WORD _apduout2bin(IL_APDU_PACK_ELEM *apdu, IL_BYTE *bin, IL_WORD *bin_len, IL_WORD max_len);

IL_FUNC IL_WORD Apdus2Bin(IL_APDU_PACK_ELEM *apdus, IL_WORD pack_size, IL_BYTE *bin, IL_WORD *bin_size);
IL_FUNC IL_WORD Bin2Apdus(IL_BYTE *bin, IL_APDU_PACK_ELEM *apdus, IL_WORD pack_size);
IL_FUNC IL_WORD _apdu2bin(IL_APDU_PACK_ELEM *apdu, IL_BYTE *bin, IL_WORD *bin_size, IL_WORD max_size);


/////////////////////////////////////////////////////////////////////////////////////////////////////////
/// UEC_DESCR.C
/////////////////////////////////////////////////////////////////////////////////////////////////////////
IL_FUNC IL_WORD opDescrGetSector(s_opContext *p_opContext, IL_WORD sectorId, SECTOR_DESCR **ppSectorDescr);
IL_FUNC IL_WORD opDescrGetBlock(s_opContext *p_opContext, IL_WORD sectorId, IL_WORD blockId, BLOCK_DESCR **ppBlockDescr);
IL_FUNC IL_WORD opDescrGetDataByTagId(s_opContext *p_opContext, IL_WORD in_sectorId, IL_WORD in_blockId, IL_DWORD in_TagId, DATA_DESCR **ppDataDescr);
IL_FUNC IL_WORD opDescrGetFirstDataInBlock(s_opContext *p_opContext, IL_WORD sectorId, IL_WORD blockId, DATA_DESCR **ppDataDescr);


#ifdef __cplusplus
}
#endif

#endif//__OP_CMNFUNC_H_ 