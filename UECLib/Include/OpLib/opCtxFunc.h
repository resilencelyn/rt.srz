/**  ������ �������� ������� ������� � ��������� ��������� ��������� �������.
  */
#ifndef __OP_CTXFUNC_H_
#define __OP_CTXFUNC_H_ 

#ifdef __cplusplus
extern "C" {
#endif

//  Description:
//      ������������� � �������� ��� ��������� �� ������� ����������� ��������� ������ �� ����� �������. 
//		����������� ��� ������ ��������������� ��������� � ����������� �������� ���.
//  See Also:
//      
//  Arguments:
//      p_opContext		- ��������� �� �������� ���.
//		(*pf)(IL_CHAR*) - ��������� �� ������� ����������� ��������� ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ��������� �� ������� ����������� ������ �� ����� �������.
IL_FUNC IL_WORD opCtxSetDisplayTextFunc(s_opContext *p_opContext, void (*pf)(IL_CHAR*));         

//  Description:
//      ������� �������� ���. 
//		����������� ��������������� ����� �������������� �������� ���.
//		�������� � ������� ������������� �������� ��������� ���������� 'opApiInitOperation'
//  See Also:
//      opApiInitOperation
//  Arguments:
//      p_opContext		- ��������� �� �������� ���.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ������� ��������� ���.
IL_FUNC IL_WORD opCtxSetClean (s_opContext *p_opContext);

//  Description:
//      ������������� � �������� ��� ��������� �� ���������� ����� ���. 
//		������ ��������� ������������ ��� ��� ���������� � ����� ���. 
//  See Also:
//		IL_CARD_HANDLE    
//  Arguments:
//      p_opContext		- ��������� �� �������� ���.
//		phCrd			- ��������� �� ���������� ����� ���.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ��������� �� ���������� ����� ���.
IL_FUNC IL_WORD opCtxSetCardReaderHandler(s_opContext *p_opContext, IL_CARD_HANDLE *phCrd);

//  Description:
//      ������������� � �������� ��� ��������� �� TLV-������ �������������� ���� ����������� �������/������ ('9F15').<p/> 
//		��� ������ ������������ ��� ��� ������������ ������� �� �������������� ��-����������.
//  See Also:
//      opApiPrepareAppAuthRequest
//  Arguments:
//      p_opContext		- ��������� �� �������� ���.
//		in_pMetaInfo	- ��������� �� ������ �������������� ���� ����������� �������/������.
//		in_MetaInfoLen	- ����� ��������������� ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ��������� �� ������ �������������� �� ������.
IL_FUNC IL_WORD opCtxSetMetaInfo(s_opContext *p_opContext, IL_BYTE *in_pMetaInfo, IL_WORD in_MetaInfoLen);

//  Description:
//      ������������� � �������� ��� ���-���� �������������� �� ����� ��� ������.<p/> 
//		������������� ����������� ������� ������ �� ��������� ������ �� WIN-1251 � ���-���� � ��������� ISO 9564-3:2002 (Format 2).<p/>
//		�����/��� �������������� ������ ������� �� ��������� ����������� ��������.
//  See Also:
//      
//  Arguments:
//      p_opContext		- ��������� �� �������� ���.
//		in_strPin		- ��������� �� ������ �� ��������� ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ���-����� �������������� �� ����� ��� ������.
IL_FUNC IL_WORD opCtxSetPinBlock(s_opContext *p_opContext, IL_CHAR *in_strPin);

//  Description:
//		��������� �� ��������� ��� ���������� ���������� ������� ������������ �� ����� ��� ������.<p/>
//		����� ����� ������ ��� ��������� ������ ����� 'ILRET_CRD_VERIFY_WRONG_PASSWORD_PRESENTED'.
//  See Also:
//      
//  Arguments:
//      p_opContext		 - ��������� �� �������� ���.
//		out_PinTriesLeft - ��������� �� ���������� c ����������� ��������� ���������� ������� ������������ ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ���������� �� ��������� ��� ���������� ���������� ������� ������������ �� ����� ��� ������.
IL_FUNC IL_WORD opCtxGetPinTriesLeft(s_opContext *p_opContext, IL_BYTE *out_PinTriesLeft);

//  Description:
//      ������������� � �������� ��� �����/��� �������������� �� ����� ��� ������. 
//  See Also:
//      KeyType.h
//  Arguments:
//      p_opContext		- ��������� �� �������� ���.
//		in_PinNum		- �����/��� �������������� ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ������/���� �������������� ������.
IL_FUNC IL_WORD opCtxSetPinNum(s_opContext *p_opContext, IL_BYTE in_PinNum);

//  Description:
//      ��������� �� ��������� ��� �����/��� �������������� ������. 
//  See Also:
//      
//  Arguments:
//      p_opContext		- ��������� �� �������� ���.
//		out_PinNum		- ��������� �� ����������� �������� ������/���� ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ���������� �� ��������� ��� ������/���� �������������� ������.
IL_FUNC IL_WORD opCtxGetPinNum(s_opContext *p_opContext, IL_BYTE *out_PinNum);

//  Description:
//      ������������� � �������� ��� �������������� �������� �� ��������. 
//		��� ������ ������������ ��� ��� ������������ ������� �� �������������� ��-����������.
//  See Also:
//      opApiPrepareAppAuthRequest
//  Arguments:
//      p_opContext		- ��������� �� �������� ���.
//		in_pExtraData	- ��������� �� ����� � ��������������� ���������� �� ��������.
//		in_ExtraDataLen - ����� ��������������� �������� �� ��������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� �������������� �������� �� ��������.
IL_FUNC IL_WORD opCtxSetExtraData(s_opContext *p_opContext, IL_BYTE *in_pExtraData, IL_WORD in_ExtraDataLen);

//  Description:
//      ������������� � �������� ��� ��������� ������� �� ��������� � ����� ������ �� ����������� ������.<p/>
//		������������ � �������������� ���������� ��� ��������� ������� 'E_CARD_DATA_REQUESTED' ��� 'E_CARD_DATA_EDIT_REQUSTED'.<p/> 
//		������ ����� ����������� � ����� ���� � "�����" �������, ���� ������������� ���������������� � ������������ � ����������� �� ������� ����� ���������� ������ 'sectors.ini'. 
//  See Also:
//      opApiReadCardData
//  Arguments:
//      p_opContext		 - ��������� �� �������� ���.
//		in_CardDataDescr - ��������� �� ������-��������� ����������� � ����� ������ � �������:<p/>
//		<p/>"[�������1];[�������2];...[�������N];"<p/>
//		<p/>�������� ����������� ������ �������� � �������:<p/>
//		<p/>"[~|x]S-B-D[-L]" , ���:
//		- 'x' - �������������� ������������ ��� ������ "�����" ������ �� ��������� �����. ��������� ������ � ���� ������ ������������ � ���� HEX-������.
//		- '~' - �������������� ������������ ��� ������ "�����" ������ �� TLV-�����. ��������� ������ � ���� ������ ������������ � ���� HEX-������.
//		- 'S' - ������������� ������� ����������� ������.
//		- 'B' - ������������� ����� ����������� ������.
//		- 'D' - ������������� ������������ �������� ������. ��� ������ �� ��������� ����� ����������� ���������� �������� �������� ������������ �������� ������ �� ������ �����. ��� ������ �� TLV-����� ����������� ����������������� �������� ���� ������������ �������� ������.
//		- 'L' - �������������� ������������, ������������ ������ ��� ����������� "�����" ������ �� ��������� ����� � ����������� ����� ����������� ������.
//		out_pCardDataBuf		 - ��������� �� ����� ��� ��������� � ����� ������.<p/>
//		��������� � ����� ������ ������������ � ���� ������ ���������� �������:<p/>
//		<p/>"[�������1]=[��������1]\\n[�������2=[��������2]\\n....[�������N=[��������N]\\n".
//		inout_pCardDataLen		 - ��������� �� ������������ ������ ������ ��� ����������� ������ � ����� ������������ ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ���������� ������� �� ������ ������ � �����. 
IL_FUNC IL_WORD opCtxSetCardDataBuf(s_opContext *p_opContext, IL_CHAR *in_CardDataDescr, IL_CHAR *out_pCardDataBuf, IL_WORD *inout_pCardDataLen);

//  Description:
//      ������������� � �������� ��� ��������� ��� ������������ ������������ ������� �� �������������� ��-����������. 
//  See Also:
//		opCtxSetMetaInfo
//		opCtxSetRequestHash
//		opCtxSetExtraData
//		opApiPrepareAppAuthRequest
//  Arguments:
//      p_opContext			  - ��������� �� �������� ���.
//		ifAuthOnline		  - ������� ������������ ������� ��� �������������� � ������ Online.
//		out_pAuthRequestBuf	  - ��������� �� ����� ��� ������������ �������.
//		inout_pAuthRequestLen - ��������� �� ������������ ������ ������ � ����������� ����� ��������������� �������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ���������� ������� �� �������������� ��-����������.
IL_FUNC IL_WORD opCtxSetAuthRequestBuf(s_opContext *p_opContext, IL_BYTE ifAuthOnline, IL_BYTE *out_pAuthRequestBuf, IL_WORD *inout_pAuthRequestLen);

//  Description:
//      ������������� � �������� ��� ��������� ��� ����������� ��������� ����������� �������������� ��-����������. 
//  See Also:
//		opApiCheckAppAuthResponse
//  Arguments:
//      p_opContext				- ��������� �� �������� ���.
//		in_pAppAuthResponseData	- ��������� �� ����� � ������� ����������� �������������� ��-����������.
//		AuthResponseDataLen		- ����� ������ ������ ����������� ������������� ��-����������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ���������� ��������� ����������� �������������� ��-����������.
IL_FUNC IL_WORD opCtxSetAppAuthResponseData(s_opContext *p_opContext, IL_BYTE *in_pAppAuthResponseData, IL_WORD AuthResponseDataLen);

//  Description:
//      ������������� � �������� ��� ��� ����������� ��������.<p/>
//		���� �������� ���������� � ������������ ����� 'opDescr.h'.<p/>
//		��� ������������ ��������� ���������� ��� ����������� �������� ��������� � ��� � �������� ��������� ������� 'opApiInitOperation'. 
//  See Also:
//		opApiInitOperation
//  Arguments:
//      p_opContext		- ��������� �� �������� ���.
//		in_OpCode		- ��� ����������� ��������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ���� ����������� ��������.
IL_FUNC IL_WORD opCtxSetOperationCode(s_opContext *p_opContext, IL_WORD in_OpCode);

//  Description:
//      ������������� � �������� ��� ������ � ����� ��������� ������.
//  See Also:
//		opCtxSetConfirmPinStr	
//  Arguments:
//      p_opContext		- ��������� �� �������� ���.
//		in_pNewPinStr	- ��������� �� ������ Win-1251 � ����� ��������� ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ������ � ����� ��������� ������.
IL_FUNC IL_WORD opCtxSetNewPinStr(s_opContext *p_opContext, IL_CHAR *in_pNewPinStr);

//  Description:
//      ������������� � �������� ��� ������ � �������������� ��������� ������ ������.
//  See Also:
//		opCtxSetNewPinStr	
//  Arguments:
//      p_opContext			- ��������� �� �������� ���.
//		in_pConfirmPinStr	- ��������� �� ������ Win-1251 � �������������� ��������� ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ������ � �������������� ��������� ������ ������.
IL_FUNC IL_WORD opCtxSetConfirmPinStr(s_opContext *p_opContext, IL_CHAR *in_pConfirmPinStr);

/* Description
   ��������� �� ��������� ��� ��������� ����� ����� ���
   ����������� ��������� ����������� ������ ����������� �����
   ��-����������� � ���������.
   See Also
   opCtxSetIssuerSessionCryptogramm
   Parameters
   p_opContext :         ��������� �� �������� ���.
   out_pIcChallenge16 :  ��������� �� ����� ��� ������������
                         ���������� ����� �����.
   Returns
   IL_WORD - ��� ������.
   Summary
   ���������� �� ��������� ��� ���������� ����� �����.        */
IL_FUNC IL_WORD opCtxGetIssuerSessionIcChallenge(s_opContext *p_opContext, IL_BYTE *out_pIcChallenge16);

//  Description:
//      ������������� � �������� ��� ����������������� ������ ������������ �������������� ����� (4 �����) � ���������� ����� ����� (16 ����) ��� ����������� ��������� ����������� ������ ��������� ����� ��-����������� � ���������.
//  See Also:
//		opCtxGetIssuerSessionIcChallenge	
//  Arguments:
//      p_opContext	   - ��������� �� �������� ���.
//		in_pHostData20 - ��������� �� ����� � ���������������� �������.
//		HostDataLength - ����� ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ������������ �������������� �����.
IL_FUNC IL_WORD opCtxSetIssuerSessionCryptogramm(s_opContext *p_opContext, IL_BYTE *in_pHostData20, IL_BYTE HostDataLength);

//  Description:
//      ��������� �� ��������� ��� ������ ��� �������� ������������� ���������� ������� � ��������� ��-����������.
//  See Also:
//		opCtxGetIssuerSessionIcChallenge
//		opCtxSetIssuerSessionCryptogramm
//  Arguments:
//      p_opContext			  - ��������� �� �������� ���.
//		out_pHostChallenge16  - ��������� �� ����� ��� ������������ �������� ���������� ����� �����.
//		out_pCardCryptogramm4 - ��������� �� ����� ��� ������������ �������� ������������ �������������� �����.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ���������� �� ��������� ��� ������ ��� �������� ������������� ������� � ��������� ��-����������.
IL_FUNC IL_WORD opCtxGetCheckIssuerSessionData(s_opContext *p_opContext, IL_BYTE *out_pHostChallenge16, IL_BYTE *out_pCardCryptogramm4);

//  Description:
//      ������������� � �������� ��� ��������� �� ����� � ���-��������� ������� �� �������� ������.<p/> 
//		��� ������ ������������ ��� ��� ������������ ��� ������� �� �������������� ��-����������.
//  See Also:
//		opApiPrepareAppAuthRequest   
//  Arguments:
//      p_opContext			- ��������� �� �������� ���.
//		in_pRequestHash		- ��������� �� ����� � ���-���������.
//		in_RequestHashLen	- ����� ���-���������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ��������� �� ���-�������� ������� �� �������� ������.
IL_FUNC IL_WORD opCtxSetRequestHash(s_opContext *p_opContext, IL_BYTE *in_pRequestHash, IL_WORD in_RequestHashLen);

//  Description:
//      ������������� � �������� ��� ��������� �� ����� ��� ����������� �� ����� ���������� ��������� ����� ������. 
//  See Also:
//		opApiReadPhoto   
//  Arguments:
//      p_opContext			- ��������� �� �������� ���.
//		out_pPhotoBuf		- ��������� �� ����� ��� ����������� ������ ����������.
//		inout_pPhotoLen		- ��������� �� ������������ ������ ������ � ����������� ����� ��������� ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ��������� �� ����� ��� ���������� ��������� �����.
IL_FUNC IL_WORD opCtxSetPhotoBuf(s_opContext *p_opContext, IL_BYTE *out_pPhotoBuf, IL_WORD *inout_pPhotoLen);

//  Description:
//      ������������� � �������� ��� ��������� �������� ��������� ������� ������ ����� ���.<p/> 
//		������������ ��� ������������� ����������� ��� ���������� ������� ���������� �������� ������ ����� ���. 
//  See Also:
//		opApiWriteSectorExDescr   
//  Arguments:
//      p_opContext		  - ��������� �� �������� ���.
//		SectorId		  - ������������� ������� ���������� ������.
//		SectorVer		  - ������ ������� ������� ���������� ������.
//		in_pExSectorDescr - ��������� �� ������-��������� ���������� ������. 
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ���������� �������� ��������� ������ �����.
IL_FUNC IL_WORD opCtxSetSectorExDescr(s_opContext *p_opContext, IL_WORD SectorId, IL_BYTE SectorVer, IL_CHAR *in_pExSectorDescr);

//  Description:
//      ���������� �� ��������� ��� ����� ������������ ����������� ��� ������ ������ ������� � ������ ������ ��� ���� ����������� ������� � ������ ���������� ������� ��������.
//		<p/>����� ������������ ����������� ����������� � ����� � �������� � ��������� ��� � ���� ������ Win-1251.   
//		<p/>����� ������������ ����������� ������ ������������ ������ ����� ������ ������������� ������.
//  See Also:
//		opCtxSetPassPhrase	
//  Arguments:
//      p_opContext		 - ��������� �� �������� ���.
//		out_pPassPhrase  - ��������� �� �������� ����� ��� ����� ������������ �����������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� ������ � ������ ������������ �����������.
IL_FUNC IL_WORD opCtxGetPassPhrase(s_opContext *p_opContext, IL_CHAR *out_pPassPhrase);

//  Description:
//      ������������� � �������� ��� ����� ������������ �����������.
//		<p/>����� ������������ ����������� ������ ������������ ������ ����� ������ ������������� ������.
//  See Also:
//		opCtxGetPassPhrase
//      flSetPassPhrase
//  Arguments:
//      p_opContext		 - ��������� �� �������� ���.
//		in_pPassPhrase  - ��������� �� ������� ����� c ������ ������������ ����������� � ��������� Win-1251.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ������������� � �������� ��� ����� ������������ �����������.
IL_FUNC IL_WORD opCtxSetPassPhrase(s_opContext *p_opContext, IL_CHAR *in_PassPhrase);

//  Description:
//      ������������� � �������� ��� ��������� ��� ��������� ������-��������� ����������� �� ����� ��� ��������� ������.
//  See Also:
//		opApiGetCardSectorsDescr   
//  Arguments:
//      p_opContext				- ��������� �� �������� ���.
//		out_pSectorsDescr		- ��������� �� ����� ��� ������������ ������-���������.
//		inout_pSectorsDescrLen	- ��������� �� ������������ ������ ������ � ����� ������������ ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ���������� �������� ��������� ������ �����.
IL_FUNC IL_WORD opCtxSetSectorsDescrBuf(s_opContext *p_opContext, 
										IL_CHAR *out_pSectorsDescr, IL_WORD *inout_pSectorsDescrLen); 

//  Description:
//      ������������� � �������� ��� ��������� ��� ��������� ������-��������� ����� ������������� ������.
//  See Also:
//		opApiGetCardBlockDataDescr   
//  Arguments:
//      p_opContext			- ��������� �� �������� ���.
//		in_pBlockId		- ��������� �� ������-������������� �������������� ����� � ������� "S-B", ���:
//								- S - ������������� �������,
//								- B - ������������� ����� ������������� ������.
//		out_pBlockDataBuf	- ��������� �� ����� ��� ������������ ������-��������� ����� ������������� ������.
//		inout_pBlockDataLen - ��������� �� ������������ ������ ������ � ����� ������������ ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ���������� ��������� ������-��������� ����� ������������� ������.
IL_FUNC IL_WORD opCtxSetBlockDataBuf(s_opContext *p_opContext, 
									 IL_CHAR *in_pBlockId, IL_CHAR *out_pBlockDataBuf, 
									 IL_WORD *inout_pBlockDataLen);

//  Description:
//      ������������� � �������� ��� ��������� ������ APDU-������ ��� ������������ ����������.
//  See Also:
//		opApiRunApduPacket   
//  Arguments:
//      p_opContext				- ��������� �� �������� ���.
//		isApduEncryptedPS		- ������� ���������� ������ � ������ ������������� ������ � ����������� ������.
//		out_pApduPacketSize		- ��������� �� ���������� � ����������� ��������� ���������� ������� ����������� ������ ������.
//		pApduInBuf				- ��������� �� ������� ����� ������, �������������� ������������������ APDU-������ ���������� �������:<p/> 
//								  <p/>"[Cmd][LenIn][LenExp][DataIn][AllowResLen][AllowRes]...", ���: 
//									- Cmd - ��������� APDU-������� (IL_BYTE*4: Class,Ins,P1,P2), 
//									- LenIn - ����� ������� ������ (IL_DWORD � ������� INTEL), 
//									- LenExp - ��������� ����� �������� ������ (IL_DWORD � ������� INTEL), 
//									- DataIn - ������� ������ IL_BYTE(LenIn), 
//									- AllowResLen - ����� ������ ���������� �������� ������ ����� SW12 (IL_BYTE), 
//									- AllowRes - ������ ���������� �������� ������ ����� SW1,SW2 (IL_BYTE: AllowResLen*2). 
//		ApduInLen				- ����� �������� ������ ������.
//		pApduOutBuf				- ��������� �� �������� ����� ��� ������� ������ � ������������ ���������� ������ APDU-������ ���������� �������:<p/>
//									<p/>"[Cmd][Sw1][Sw2][LenOut][DataOut]...", ���: 
//									- Cmd - ��������� APDU-������� (IL_BYTE*4: Class,Ins,P1,P2),
//									- SW1 - ������ SW1 ������ �����,
//									- SW2 - ������ SW2 ������ �����,
//									- LenOut - ����� �������� ������ (IL_DWORD � ������� INTEL),
//									- DataOut - �������� ������ IL_BYTE(LenOut).
//		pApduOutLen				- ����� ������� ������ � ������������ ���������� ������. 
//		out_pApduPacketResult	- ��������� �� ���������� � ����� ���������� ��������� ������� ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ���������� ������ APDU-������ ��� ������������ ����������. 
IL_FUNC IL_WORD opCtxSetApduPacketBuf(s_opContext *p_opContext, 
									  IL_BYTE isApduEncryptedPS, 
									  IL_WORD *out_pApduPacketSize, IL_BYTE *pApduInBuf, 
									  IL_WORD ApduInLen, IL_BYTE *pApduOutBuf, IL_WORD *pApduOutLen, 
									  IL_WORD *out_pApduPacketResult);

//  Description:
//      ������������� � �������� ��� ��������� ��� ��������� ����������� ������� ��������� ����� ���.
//  See Also:
//		opApiMakeDigitalSignature   
//  Arguments:
//      p_opContext						- ��������� �� �������� ���.
//		out_pDigitalSignBuf				- ��������� �� ����� ��� ������������ ����������� �������.
//		inout_pDigitalSignLen			- ��������� �� ������������ ������ ������ � ����� ������������ ����������� �������.
//		out_pDigitalSignCertChain		- ��������� �� ����� ��� ������������ ������� ������������ ����� �������� ����������� �������.
//		inout_pDigitalSignCertChainLen	- ��������� �� ������������ ������ ������ � ����� ������������ ������� ������������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ���������� ��������� ����������� ������� ��������� ����� ���.
IL_FUNC IL_WORD opCtxSetDigitalSignatureBuf(s_opContext *p_opContext, 
											IL_BYTE *out_pDigitalSignBuf, IL_WORD *inout_pDigitalSignLen,
											IL_BYTE *out_pDigitalSignCertChain, IL_WORD *inout_pDigitalSignCertChainLen);

//  Description:
//      ������������� � �������� ��� ��������� ��� ��������� ����������� ������ � ����������� ������.
//  See Also:
//		opCtxGetProviderSessionData
//		opCtxSetProviderAuthData
//		opCtxSetProviderEncrDecrBuf
//		opApiSetProviderCryptoSession
//		opApiAuthServiceProvider
//		opApiEncryptProviderToTerminal
//		opApiDecryptProviderToTerminal
//  Arguments:
//      p_opContext		- ��������� �� �������� ���.
//		ifGostPS		- ������� ��������� ������ �� ������ ��������������� ����.
//		in_pCSpId		- ��������� �� ����� ����������� ��������� ����� ���������� ������.
//		in_CSpIdLen		- ����� �����������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ���������� ��������� ����������� ������ � ����������� ������.
IL_FUNC IL_WORD opCtxSetProviderSessionParams(s_opContext *p_opContext, IL_BYTE ifGostPS, IL_BYTE *in_pCSpId, IL_WORD in_CSpIdLen);

//  Description:
//      ��������� �� ��������� ��� �������� ������ ������������� ���������� ������ � ����������� ������.
//  See Also:
//		opCtxSetProviderSessionParams
//		opCtxSetProviderAuthData
//		opCtxSetProviderEncrDecrBuf
//		opApiSetProviderCryptoSession
//		opApiAuthServiceProvider
//		opApiEncryptProviderToTerminal
//		opApiDecryptProviderToTerminal
//  Arguments:
//      p_opContext					- ��������� �� �������� ���.
//		out_pProviderSessionDataOut	- ��������� �� ����� ��� ������������ ���������� ������������� ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ���������� �� ��������� ��� �������� ������ ������������� ���������� ������ � ����������� ������.
IL_FUNC IL_WORD opCtxGetProviderSessionData(s_opContext *p_opContext, PROVIDER_SESSION_DATA *out_pProviderSessionDataOut);

//  Description:
//      ������������� � �������� ��� ��������� ��� �������������� ���������� ������.<p/>
//		��������  ��������������  ����������  ������  ������������  ��������  �������������  ������, ���������� � ������ �� ������ �������� ������. 
//  See Also:
//		opCtxSetProviderSessionParams
//		opCtxGetProviderSessionData
//		opCtxSetProviderEncrDecrBuf
//		opApiSetProviderCryptoSession
//		opApiAuthServiceProvider
//		opApiEncryptProviderToTerminal
//		opApiDecryptProviderToTerminal
//  Arguments:
//      p_opContext		- ��������� �� �������� ���.
//		in_pMsg			- ��������� �� ������ ������ �� ������ �������� �����.
//		MsgLen			- ����� ������ ������.
//		in_pMsgSign		- ��������� �� ����� c ���, �������������� ����������� ������ ��� ������ ������ �� ������ �������� �������.
//		MsgSignLen		- ����� �������������� ���.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ���������� ��� �������������� ���������� ������.
IL_FUNC IL_WORD opCtxSetProviderAuthData(s_opContext *p_opContext, IL_BYTE *in_pMsg, IL_WORD MsgLen, IL_BYTE *in_pMsgSign, IL_WORD MsgSignLen);

//  Description:
//      ������������� � �������� ��� ��������� �� ������, ��� ���������� � ������������� ������ ������� �� �������� ������ � ������ �������������� ����������� ������ � ����������� ������.
//  See Also:
//		opCtxSetProviderSessionParams
//		opCtxGetProviderSessionData
//		opCtxSetProviderAuthData
//		opApiSetProviderCryptoSession
//		opApiAuthServiceProvider
//		opApiEncryptProviderToTerminal
//		opApiDecryptProviderToTerminal
//  Arguments:
//      p_opContext				- ��������� �� �������� ���.
//		in_pClearData			- ��������� �� ������� ����� � ���������������� ������� ��� ���������� ��� �������� ����� � ��������������� ������� ��� �������������.
//		inout_pClearDataLen	    - ��������� �� ����� ��������� ������ ��� ������������ ������ ������ � ����� ������������ �������������� ������.
//		in_pEncryptedData	    - ��������� �� ������� ����� � �������������� ������� ��� ������������� ��� �������� ����� � ��������������� ������� ��� ����������.
//		inout_pEncryptedDataLen - ��������� �� ����� ������������� ������ ��� ������������ ������ ������ � ����� ������������ ������������� ������.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ���������� �� ������, ��� ���������� � ������������� ������ ������� �� �������� ������ � ������ �������������� ����������� ������ � ����������� ������.
IL_FUNC IL_WORD opCtxSetProviderEncrDecrBuf(s_opContext *p_opContext, 
											IL_BYTE *in_pClearData, IL_DWORD *inout_pClearDataLen,
											IL_BYTE *in_pEncryptedData, IL_DWORD *inout_pEncryptedDataLen);

//  Description:
//      ������������� � �������� ��� ������ ��������� ������������� ������ ������������ (���).<p/>
//		������������ ������ � ������� ��� � ���������� ��� ��� ��� ���������. 
//  See Also:
//		smOfflineActivationFinish
//  Arguments:
//      p_opContext			- ��������� �� �������� ���.
//		in_pSeOwnerName		- ��������� �� ������ ��������� ���.
//		SeOwnerNameLen		- ����� ������ ��������� ���.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ������ � ��������� ���.
IL_FUNC IL_WORD opCtxSetSeOwnerName(s_opContext *p_opContext, IL_BYTE *in_pSeOwnerName, IL_WORD SeOwnerNameLen);

//  Description:
//      ��������� �� ��������� ��� ������������ �������������� ����� ��� ��������� ����������� ���������� � ���������.<p/>
//		������������ ������ � ������� ��� � ���������� ���. 
//  See Also:
//		opCtxSetSeIssuerSessionCryptogramm
//  Arguments:
//      p_opContext			- ��������� �� �������� ���.
//		out_pIcChallenge16	- ��������� �� �������� ����� ��� �������������� ������������ �������������� �����.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ���������� �� ��������� ��� ������������ �������������� ����� ��� ��������� ����������� ���������� ��� � ��������� � ������������� ���.
IL_FUNC IL_WORD opCtxGetSeIssuerSessionIcChallenge(s_opContext *p_opContext, IL_BYTE *out_pIcChallenge16);

//  Description:
//      ������������� � �������� ��� ����������������� ������ ������������ �������������� ����� (4 �����) � ���������� ����� ����� (16 ����) ��� ����������� ��������� ����������� ������ ��������� ����� ��-����������� � ���������.
//		������������ ������ � ������� ��� � ���������� ���. 
//  See Also:
//		opCtxGetSeIssuerSessionIcChallenge	
//  Arguments:
//      p_opContext				- ��������� �� �������� ���.
//		in_pCardCryptogramm		- ��������� �� ����� � ���������������� �������.
//		CardCryptogrammLength	- ����� ������.
//		ifGostSession			- ������� ��������� ������ ����.
//  Return Value:
//      IL_WORD		- ��� ������.
//  Summary:
//      ��������� � �������� ��� ������������ �������������� ����� ��� ��������� ���������� ������ � ��������� � ������������� ���.
IL_FUNC IL_WORD opCtxSetSeIssuerSessionCryptogramm(s_opContext *p_opContext, IL_BYTE *in_pCardCryptogramm, IL_BYTE CardCryptogrammLength, IL_BYTE ifGostSession);

#ifdef __cplusplus
}
#endif

#endif//__OP_CTXFUNC_H_
