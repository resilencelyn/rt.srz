#include "il_types.h"
#include "HAL_SCReader.h"
#include "il_error.h"
#include "HAL_Protocol.h"
#include "HAL_Common.h"
#include <winscard.h>

#define DEFAULT_PROTOCOL_SET (SCARD_PROTOCOL_T0 | SCARD_PROTOCOL_T1)

#ifndef PROT_IGNORE
	//IL_CHAR protBuf[IL_APDU_BUF_SIZE*4+];
#endif//PROT_IGNORE

IL_RETCODE crConvertPCSCError(DWORD dwPCSC_Error)
{
    char *pMes;
    IL_RETCODE ilRet = 0;

#ifdef _DEBUG
    char MessageBuffer[256];
    if(dwPCSC_Error)
    {
        wsprintf( MessageBuffer,"PC/SC Error Code : 0x%X error code.", dwPCSC_Error);
        OutputDebugString(MessageBuffer);
    }
#endif

    pMes = NULL;

    switch( dwPCSC_Error )
    {
    case SCARD_S_SUCCESS: // SCARD_S_SUCCESS:
        //				pMes = "OK\n";
        pMes = NULL;
        ilRet = 0; break;

        //	case SCARD_E_BAD_SEEK:
        //				pMes = "Error occurred in setting the smart card file object pointer.\n";

    case SCARD_E_CANCELLED:
        pMes = "The action was cancelled by an SCardCancel request.\n";
        ilRet = ILRET_SCR_ERROR; break;

    case SCARD_E_CANT_DISPOSE:
        pMes = "The system could not dispose of the media in the requested manner.\n";
        ilRet = ILRET_SCR_ERROR;

    case SCARD_E_CARD_UNSUPPORTED:
        pMes = "The smart card does not meet minimal requirements for support.\n";
        ilRet = ILRET_SCR_UNSUPPORTED_CARD; break;

        //	case SCARD_E_SERTIFICATE_UNAVALIABLE:
        //				pMes = "The requested certificate could not be obtained.\n";

        //	case SCARD_E_COMM_DATA_LOST:
        //				pMes = "A communications error with the smart card has been detected.\n";

        //	case SCARD_E_DIR_NOT_FOUND:
        //				pMes = "The specified directory does not exist in the smart card.\n";

    case SCARD_E_DUPLICATE_READER:
        pMes = "The reader driver didn't produce a unique reader name.\n";
        ilRet = ILRET_SCR_INVALID_DEVICE; break;

        //	case SCARD_E_FILE_NOT_FOUND:
        //				pMes = "The specified file does not exist in the smart card.\n";

        //	case SCARD_E_ICC_CREATEORDER:
        //				pMes = "The requested order of object creation is not supported.\n";

        //	case SCARD_E_ICC_INSTALLATION:
        //				pMes = "No primary provider can be found for the smart card.\n";

    case SCARD_E_INSUFFICIENT_BUFFER:
        pMes = "The data buffer to receive returned data is too small for the returned data.\n";
        ilRet = ILRET_SCR_PROTOCOL_ERROR; break;

    case SCARD_E_INVALID_ATR:
        pMes = "An ATR obtained from the registry is not a valid ATR string.\n";
        ilRet = ILRET_SCR_INVALID_ATR; break;

        //	case SCARD_E_INVALID_CHV:
        //				pMes = "The supplied PIN is incorrect.\n";

    case SCARD_E_INVALID_HANDLE:
        pMes = "The supplied handle was invalid.\n";
        ilRet = ILRET_SCR_INVALID_HANDLE; break;

    case SCARD_E_INVALID_PARAMETER:
        pMes = "One or more of the supplied parameters could not be properly interpreted.\n";
        ilRet = ILRET_SCR_INVALID_DEVICE; break;

    case SCARD_E_INVALID_TARGET:
        pMes = "Registry startup information is missing or invalid.\n";
        ilRet = ILRET_SCR_INVALID_DEVICE; break;

    case SCARD_E_INVALID_VALUE:
        pMes = "One or more of the supplied parameters’ values could not be properly interpreted.\n";
        ilRet = ILRET_SCR_INVALID_DEVICE; break;

        //	case SCARD_E_NO_ACCESS:
        //				pMes = "Access is denied to the file.\n";

        //	case SCARD_E_NO_DIR:
        //				pMes = "The supplied path does not represent a smart card directory.\n";

        //	case SCARD_E_NO_FILE:
        //				pMes = "The supplied path does not represent a smart card file.\n";

    case SCARD_E_NO_MEMORY:
        pMes = "Not enough memory available to complete this command.\n";
        ilRet = ILRET_SCR_INVALID_DEVICE; break;

        //	case SCARD_E_NO_READERS_AVALIABLE:
        //				pMes = "No smart card reader is available.\n";

    case SCARD_E_NO_SERVICE:
        pMes = "The smart card resource manager is not running.\n";
        ilRet = ILRET_SCR_NOT_READY; break;

    case SCARD_E_NO_SMARTCARD:
        pMes = "The operation requires a smart card, but no smart card is currently in the device.\n";
        ilRet = ILRET_SCR_REMOVED_CARD; break;

        //	case SCARD_E_NO_SUCH_CERTIFICATE:
        //				pMes = "The requested certificate does not exist.\n";


    case SCARD_E_NOT_READY:
        pMes = "The reader or card is not ready to accept commands.\n";
        ilRet = ILRET_SCR_NOT_READY; break;

    case SCARD_E_NOT_TRANSACTED:
        pMes = "An attempt was made to end a non-existent transaction.\n";
        ilRet = ILRET_SCR_ERROR; break;

    case SCARD_E_PCI_TOO_SMALL:
        pMes = "The PCI Receive buffer was too small.\n";
        ilRet = ILRET_SCR_PROTOCOL_ERROR; break;

    case SCARD_E_PROTO_MISMATCH:
        pMes = "The requested protocols are incompatible with the protocol currently in use with the card.\n";
        ilRet = ILRET_SCR_PROTO_MISMATCH; break;

    case SCARD_E_READER_UNAVAILABLE:
        pMes = "The specified reader is not currently available for use.\n";
        ilRet = ILRET_SCR_READER_UNAVAILABLE; break;

    case SCARD_E_READER_UNSUPPORTED:
        pMes = "The reader driver does not meet minimal requirements for support.\n";
        ilRet = ILRET_SCR_INVALID_DEVICE; break;

    case SCARD_E_SERVICE_STOPPED:
        pMes = "The Smart card resource manager has shut down.\n";
        ilRet = ILRET_SCR_NOT_READY; break;

    case SCARD_E_SHARING_VIOLATION:
        pMes = "The card cannot be accessed because of other connections outstanding.\n";
        ilRet = ILRET_SCR_SHARING_VIOLATION; break;

    case SCARD_E_SYSTEM_CANCELLED:
        pMes = "The action was cancelled by the system presumably to log off or shut down.\n";
        ilRet = ILRET_SCR_INVALID_DEVICE; break;

    case SCARD_E_TIMEOUT:
        pMes = "The user-specified timeout value has expired.\n";
        ilRet = ILRET_SCR_TIMEOUT; break;

        //	case SCARD_E_UNEXPECTED:
        //				pMes = "An unexpected card error has occurred.\n";

    case SCARD_E_UNKNOWN_CARD:
        pMes = "The specified card name is not recognized.\n";
        ilRet = ILRET_SCR_UNSUPPORTED_CARD; break;

    case SCARD_E_UNKNOWN_READER:
        pMes = "The specified reader name is not recognized.\n";
        ilRet = ILRET_SCR_UNKNOWN_READER; break;

        //	case SCARD_E_UNKNOWN_MES_MNG:
        //				pMes = "An unrecognized error code was returned from a layered component.\n";

        //	case SCARD_E_UNSUPPORTED_FEATURE:
        //				pMes = "This smart card does not support the requested feature.\n";

        //	case SCARD_E_WRITE_TOO_MANY:
        //				pMes = "An attempt was made to write more data than would fit in the target object.\n";

    case SCARD_F_COMM_ERROR:
        pMes = "An internal communications error has been detected.\n";
        ilRet = ILRET_SCR_PROTOCOL_ERROR; break;

    case SCARD_F_INTERNAL_ERROR:
        pMes = "An internal consistency check failed.\n";
        ilRet = ILRET_SCR_ERROR; break;

    case SCARD_F_UNKNOWN_ERROR:
        pMes = "An internal error has been detected but the source is unknown.\n";
        ilRet = ILRET_SCR_ERROR; break;

    case SCARD_F_WAITED_TOO_LONG:
        pMes = "An internal consistency timer has expired.\n";
        ilRet = ILRET_SCR_TIMEOUT; break;

    case SCARD_P_SHUTDOWN:
        pMes = "The operation has been aborted to allow the server application to exit.\n";
        ilRet = ILRET_SCR_NOT_READY; break;

        //	case SCARD_W_CANCELED_BY_USER:
        //				pMes = "The action was cancelled by the user.\n";

        //	case SCARD_W_CHV_BLOCKED:
        //				pMes = "The card cannot be accessed because the maximum number of PIN entry attempts has been reached.\n";

        //	case SCARD_W_EOF:
        //				pMes = "The end of the smart card file has been reached.\n";

    case SCARD_W_REMOVED_CARD:
        pMes = "The card has been removed so that further communication is not possible.\n";
        ilRet = ILRET_SCR_REMOVED_CARD; break;

    case SCARD_W_RESET_CARD:
        pMes = "The card has been reset so any shared state information is invalid.\n";
        ilRet = ILRET_SCR_RESET_CARD; break;

        //	case SCARD_W_SECURITY_VIOLATION:
        //			pMes = "Access was denied because of a security violation.\n";

    case SCARD_W_UNPOWERED_CARD:
        pMes = "Power has been removed from the card so that further communication is not possible.\n";
        ilRet = ILRET_SCR_UNPOWERED_CARD; break;

    case SCARD_W_UNRESPONSIVE_CARD:
        pMes = "The card is not responding to a reset.\n";
        ilRet = ILRET_SCR_UNRESPONSIVE_CARD; break;

    case SCARD_W_UNSUPPORTED_CARD:
        pMes = "The reader cannot communicate with the card due to ATR configuration conflicts.\n";
        ilRet = ILRET_SCR_UNSUPPORTED_CARD; break;

        //	case SCARD_W_WRONG_CHV:
        //				pMes = "The card cannot be accessed because the wrong PIN was presented.\n";

    default:
        pMes = "NOT SPECIFIED.\n";
        ilRet = ILRET_SCR_ERROR; break;
    }

#ifdef _DEBUG
    if( pMes )
        OutputDebugString( pMes );
#endif

    return ilRet;
}

IL_FUNC IL_RETCODE crInit(IL_HANDLE_READER pilRdrHandle, IL_READER_SETTINGS ilRdrSettings)
{
    IL_RETCODE ilRet = 0;
    DWORD dwPCSC_Error = 0;
    BYTE needUnpower = 0;
    DWORD Disposition = 0;
	HANDLE_READER* pmyRdrHandle = (HANDLE_READER*)pilRdrHandle;

    cmnMemSet((IL_BYTE*)pilRdrHandle, 0, sizeof(HANDLE_READER));
    pmyRdrHandle->dwScope = SCARD_SCOPE_USER;
    pmyRdrHandle->dwShareMode = SCARD_SHARE_EXCLUSIVE;
	pmyRdrHandle->dwProtocol = DEFAULT_PROTOCOL_SET;    
    pmyRdrHandle->prdrSettings = ((READER_SETTINGS)ilRdrSettings);

    dwPCSC_Error = SCardEstablishContext(pmyRdrHandle->dwScope, NULL, NULL, &pmyRdrHandle->hContext);
    if(dwPCSC_Error ) goto end;

    if(!pmyRdrHandle->hContext)
    {
        ilRet = ILRET_SCR_INVALID_HANDLE;
        goto end;
    }

    dwPCSC_Error = SCardConnect(pmyRdrHandle->hContext,
        pmyRdrHandle->prdrSettings,
        pmyRdrHandle->dwShareMode,
        pmyRdrHandle->dwProtocol,
        &pmyRdrHandle->hCard,
        &pmyRdrHandle->dwActiveProtocol);

    if( dwPCSC_Error == SCARD_W_REMOVED_CARD )
    {
        needUnpower = 0;
        dwPCSC_Error = SCARD_S_SUCCESS;
    }
    else
        needUnpower = 1;

    if( dwPCSC_Error ) goto end;

    if( needUnpower )
        Disposition = SCARD_UNPOWER_CARD;
    else
        Disposition = SCARD_LEAVE_CARD;

    if( pmyRdrHandle->hCard ) 
        dwPCSC_Error = SCardDisconnect( pmyRdrHandle->hCard, Disposition );
    else
        dwPCSC_Error = SCARD_S_SUCCESS;

    dwPCSC_Error = SCARD_S_SUCCESS;
    pmyRdrHandle->hCard = 0;

end: 
    if(dwPCSC_Error)
        ilRet = crConvertPCSCError(dwPCSC_Error);
    if(ilRet)
        crDeinit(pmyRdrHandle);

	PROT_WRITE_EX2(PROT_READER1, "INIT   Error=%08X Ret=%u", dwPCSC_Error, ilRet); 
    return ilRet;
}

IL_FUNC IL_RETCODE crOpenSession(IL_HANDLE_READER pilRdrHandle)
{
    IL_RETCODE ilRet = 0;
    IL_DWORD dwPCSC_Error = 0;
    IL_BYTE  rdrName[256], Atr[36];
    IL_DWORD rdrNameLength = sizeof(rdrName);
    IL_DWORD AtrLength = sizeof(Atr);
	IL_DWORD dwProtocol = DEFAULT_PROTOCOL_SET;
	HANDLE_READER* pmyRdrHandle = (HANDLE_READER*)pilRdrHandle;
	char tmp[256] = {0};

    if( !pmyRdrHandle->hContext )
    {
        dwPCSC_Error = SCARD_F_UNKNOWN_ERROR;
        goto end;
    }

    if( pmyRdrHandle->hCard )
    {
        dwPCSC_Error= SCardReconnect(pmyRdrHandle->hCard,
            pmyRdrHandle->dwShareMode, 
            dwProtocol,
            SCARD_RESET_CARD ,
            &pmyRdrHandle->dwActiveProtocol );
    }
    else
    {
        dwPCSC_Error = SCardConnect(pmyRdrHandle->hContext, 
            pmyRdrHandle->prdrSettings, 
            pmyRdrHandle->dwShareMode, 
            dwProtocol,
            &pmyRdrHandle->hCard, 
            &pmyRdrHandle->dwActiveProtocol );
    }
    if( dwPCSC_Error ) goto end;

    if( !pmyRdrHandle->hCard )
    {
        dwPCSC_Error = SCARD_F_UNKNOWN_ERROR;
        goto end;
    }

    rdrNameLength = 256;
    AtrLength = 36;

    dwPCSC_Error = SCardStatus( pmyRdrHandle->hCard,
        rdrName,
        &rdrNameLength,
        &pmyRdrHandle->dwReaderState,
        &pmyRdrHandle->dwActiveProtocol,
        Atr,
        &AtrLength );

end:
    ilRet = crConvertPCSCError( dwPCSC_Error );
	PROT_WRITE_EX4(PROT_READER1, "OPEN   Error=%08X Ret=%u ATR[%lu]=%s", dwPCSC_Error, ilRet, AtrLength, bin2hex(tmp, Atr, AtrLength)); 
    return ilRet;
}

//crAPDU - выполнение APDU команды
IL_FUNC IL_RETCODE _crAPDU(HANDLE_READER* pilRdrHandle, IL_APDU* pilApdu)
{
    IL_RETCODE ilRet = 0;

    SCARD_IO_REQUEST	pioSendPci, pioRecvPci;

    DWORD dwPCSC_Error;
    BYTE	bIn [IL_APDU_BUF_SIZE];
    BYTE	bOut[IL_APDU_BUF_SIZE];
    DWORD bInLength = sizeof(bIn);
    DWORD bOutLength = sizeof(bOut);
#ifndef PROT_IGNORE
	IL_CHAR protBuf1[IL_APDU_BUF_SIZE*2+1];
	IL_CHAR protBuf2[IL_APDU_BUF_SIZE*2+1];
#endif//PROT_IGNORE

    pioSendPci.dwProtocol  = pilRdrHandle->dwActiveProtocol;
    pioSendPci.cbPciLength = sizeof(SCARD_IO_REQUEST);

    pioRecvPci.dwProtocol  = pilRdrHandle->dwActiveProtocol;
    pioRecvPci.cbPciLength = sizeof(SCARD_IO_REQUEST);

    bIn[0] = pilApdu->Cmd[0];
    bIn[1] = pilApdu->Cmd[1];
    bIn[2] = pilApdu->Cmd[2];
    bIn[3] = pilApdu->Cmd[3];

    if((pilApdu->LengthIn == 0) && (pilApdu->LengthExpected == 0))
        bInLength = 4;
    else if( pilApdu->LengthIn )
    {
        if(pilApdu->LengthIn < 256)
        {
            bIn[4] = pilApdu->LengthIn;
            cmnMemCopy( &bIn[5], pilApdu->DataIn, pilApdu->LengthIn );
            bInLength = 5 + pilApdu->LengthIn;
        }
        else
        {
            bIn[4] = 0;
            bIn[5] = (IL_BYTE)(pilApdu->LengthIn>>8);
            bIn[6] = (IL_BYTE)(pilApdu->LengthIn);
            cmnMemCopy( &bIn[7], pilApdu->DataIn, pilApdu->LengthIn );
            bInLength = 7 + pilApdu->LengthIn;
        }
    }
    else
    {
        bIn[4] = pilApdu->LengthExpected;
        bInLength = 5;
    }

    pilApdu->SW1 = pilApdu->SW2 = 0;

    dwPCSC_Error=SCardTransmit(	pilRdrHandle->hCard,
        &pioSendPci,
        bIn,
        bInLength,
        &pioRecvPci,
        bOut,
        &bOutLength );

    if( dwPCSC_Error )
    {
        goto end;
    }

    if( bOutLength < 2 )
    {
        dwPCSC_Error = SCARD_F_COMM_ERROR;
        goto end;
    }

    pilApdu->SW1 = bOut[bOutLength-2]; 
    pilApdu->SW2 = bOut[bOutLength-1];

    pilApdu->LengthOut = ( bOutLength > (0xFF + 2) ) ? 0xFF : (bOutLength - 2);

    if( pilApdu->LengthOut && pilApdu->DataOut )
    {
        cmnMemCopy( pilApdu->DataOut, bOut, pilApdu->LengthOut );
    }

end:
    ilRet = crConvertPCSCError( dwPCSC_Error );
	PROT_WRITE_EX6(PROT_READER1, "APDU   Error=%08X Ret=%u IN[%lu]=%s OUT[%lu]=%s", dwPCSC_Error, ilRet, bInLength, bin2hex(protBuf1, bIn, bInLength), bOutLength, bin2hex(protBuf2, bOut, bOutLength)); 
	return ilRet;
}

IL_FUNC IL_RETCODE crAPDU(IL_HANDLE_READER pilRdrHandle, IL_APDU* pilApdu)
{
    IL_RETCODE ilRet = 0;
    IL_APDU tmp_apdu = {0};

	IL_WORD ENVELOP_CMD_MAX_DATA_LEN = 248;
	IL_WORD len_to_send;
	IL_WORD cur_len;
	IL_WORD cur_ofs;

	HANDLE_READER* pmyRdrHandle = (HANDLE_READER*)pilRdrHandle;

	if((pilApdu->LengthIn < 256) || (pmyRdrHandle->dwActiveProtocol > 1) )
	{
		ilRet = _crAPDU(pmyRdrHandle, pilApdu);
		if(ilRet)
			return ilRet;
	}
	else
	{
		len_to_send = 4 + 3 + pilApdu->LengthIn;

		memset(&tmp_apdu, 0, sizeof(tmp_apdu));
		tmp_apdu.Cmd[1] = 0xC2;
		cur_len = (len_to_send > ENVELOP_CMD_MAX_DATA_LEN) ? ENVELOP_CMD_MAX_DATA_LEN : len_to_send;
		tmp_apdu.LengthIn = cur_len;
		memcpy(tmp_apdu.DataIn, pilApdu->Cmd, 4);
		tmp_apdu.DataIn[4] = 0;		
		tmp_apdu.DataIn[5] = pilApdu->LengthIn >> 8;
		tmp_apdu.DataIn[6] = (BYTE)pilApdu->LengthIn;
		cur_ofs = 0;
		memcpy(&tmp_apdu.DataIn[7], &pilApdu->DataIn[cur_ofs], cur_len - 7);
		ilRet = _crAPDU(pmyRdrHandle, &tmp_apdu);
		if(ilRet)
			return ilRet;
		if(SW(&tmp_apdu) != 0x9000)
			return 0;

		cur_ofs += (cur_len - 7);
		len_to_send -= cur_len;

		while(len_to_send > 0)
		{
			memset(&tmp_apdu, 0, sizeof(tmp_apdu));
			tmp_apdu.Cmd[1] = 0xC2;
			cur_len = (len_to_send > ENVELOP_CMD_MAX_DATA_LEN) ? ENVELOP_CMD_MAX_DATA_LEN : len_to_send;
			tmp_apdu.LengthIn = cur_len;
			memcpy(tmp_apdu.DataIn, &pilApdu->DataIn[cur_ofs], cur_len);
			ilRet = _crAPDU(pmyRdrHandle, &tmp_apdu);
			if(ilRet)
				return ilRet;
			if(SW(&tmp_apdu) != 0x9000)
				return 0;
			
			cur_ofs += cur_len;
			len_to_send -= cur_len;
		}
		pilApdu->SW1 = tmp_apdu.SW1;
		pilApdu->SW2 = tmp_apdu.SW2;
	}

    if(pmyRdrHandle->dwActiveProtocol == SCARD_PROTOCOL_T0)
    {
        if(pilApdu->SW1 == 0x61)
        {
            tmp_apdu.Cmd[1] = 0xC0;//INS_GET_RESP
            tmp_apdu.LengthExpected = pilApdu->SW2;
            ilRet = _crAPDU(pmyRdrHandle, &tmp_apdu);
            if(ilRet)
                return ilRet;

            pilApdu->LengthOut = tmp_apdu.LengthOut;
            cmnMemCopy(pilApdu->DataOut, tmp_apdu.DataOut, tmp_apdu.LengthOut);
            pilApdu->SW1 = tmp_apdu.SW1;
            pilApdu->SW2 = tmp_apdu.SW2;
        }
        else if(pilApdu->SW1 == 0x6C)
        {
            cmnMemCopy((IL_BYTE*)&tmp_apdu, (IL_BYTE*)pilApdu, sizeof(tmp_apdu));
            tmp_apdu.LengthExpected = pilApdu->SW2;
            ilRet = _crAPDU(pmyRdrHandle, &tmp_apdu);
            if(ilRet)
                return ilRet;

            pilApdu->LengthOut = tmp_apdu.LengthOut;
            cmnMemCopy(pilApdu->DataOut, tmp_apdu.DataOut, tmp_apdu.LengthOut);
            pilApdu->SW1 = tmp_apdu.SW1;
            pilApdu->SW2 = tmp_apdu.SW2;
        }
    }

    return ilRet;
}

IL_FUNC IL_RETCODE crCloseSession(IL_HANDLE_READER pilRdrHandle)
{
    IL_RETCODE ilRet = 0;
    DWORD dwPCSC_Error = 0;
	HANDLE_READER* pmyRdrHandle = (HANDLE_READER*)pilRdrHandle;

    if( !pmyRdrHandle->hCard )
        goto End;

    dwPCSC_Error= SCardDisconnect( pmyRdrHandle->hCard, SCARD_UNPOWER_CARD );

    pmyRdrHandle->hCard = 0;

    ilRet = crConvertPCSCError( dwPCSC_Error );

End:
	PROT_WRITE_EX2(PROT_READER1, "CLOSE  Error=%08X Ret=%u", dwPCSC_Error, ilRet); 
    return ilRet;
}

IL_FUNC IL_RETCODE crDeinit(IL_HANDLE_READER pilRdrHandle)
{
    IL_RETCODE ilRet = 0;
    DWORD dwPCSC_Error = 0;
	HANDLE_READER* pmyRdrHandle = (HANDLE_READER*)pilRdrHandle;

    dwPCSC_Error = crCloseSession(pilRdrHandle);
    dwPCSC_Error = 0;

    if( pmyRdrHandle->hContext )
    {
        (void)SCardCancel( pmyRdrHandle->hContext );
        dwPCSC_Error = SCardReleaseContext( pmyRdrHandle->hContext );
    }

    cmnMemSet( (IL_BYTE*)pmyRdrHandle, 0, sizeof(HANDLE_READER) );

    ilRet = crConvertPCSCError( dwPCSC_Error );

	PROT_WRITE_EX2(PROT_READER1, "DEINIT Error=%08X Ret=%u", dwPCSC_Error, ilRet); 
    return ilRet;
}

//crGetParam - получение параметров смарт-карт ридера
//crSetParam - установка параметров смарт-карт ридера
//crIOControl - для управления устройством и передачи инструкций в ПКР.
