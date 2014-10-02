// UECLibTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Structures.h"
#include "time.h"
#include "UECLib.h"


int _tmain(int argc, _TCHAR* argv[])
{
	//// //OpenCard(NULL);
 //// 
 //// WCHAR szToken[1204] = L"asdasdasd";
 WCHAR szCIN[200] = {0};

 ////// 
 ////// //GetCurrentComputerNameWithTakingRemoteSession(szToken);
 ////// 
 ////// ////Открытие
 ////// //DWORD tOpenCardBegin = GetTickCount();
 ////DWORD dwErr = OpenCard(szToken);
 ////// //DWORD tOpenCardEnd = GetTickCount();
 ////// //double dOpenCardInSeconds = tOpenCardEnd - tOpenCardBegin;

 BYTE szUC1Cert[] = "7F2181BC7F4E765F29010242040000099965068001158101015F2004000009995F24032207315F25031207267F494281406C4111B867591FA3BCA1E9D08E0603791FC958400C10DFBC2A53B1B70F54BC7A57376CE2C792B97F23A9F5FCA7CDDCB4C7AEC90EE44E1A044B33A1881E685EBD7F4C098001018201018301085F37400C97E160F7A59AADD4B27D791D63A0B06BE16BA16E9C681000BB1DF8C7F6D053BD7878E4BA89395ACCF1F47D5A33FDB8162CD6224A678A823CFAB5AA6CE4C793";
 BYTE szOKOCert[] = "7F2181C97F4E81825F29010242040000099965068001158101015F2004010000065F24031404305F25031403117F49428140C09BD0ECB8123A92E97FC7589AA76617AB8DC6D50D322258DF63913B18BCE195244CFE0BF29D1D3453CCD0EC509CC8AAD1F851638CAC82E3CC6176CE1B0DF57F7F4C15800104810A007100CB00CD0069006B8201018301085F374029FE130352A05285F8047DF6492E544000D9B69C6C98EB177576894DF7A98B8132AC96C3F809AF0DD3EA258835C6816D30E945D99CB098DEC2D8F5447BE4FF2B";
 BYTE szTerminalOpenCert[] = "7F2181D57F4E818E5F29010242040100000665068001158101015F201000000001010000065502051B1D05C3005F24031404305F25031403117F4942814040C1997998D495E1926C24E68864115A1AED4295EB289B5EFFCB19C02D2104525EFE12355F20993FA00D71871224C4EC90CADE58636C0784F21CE07F89BF62807F4C15800107810A007100CB00CD0069006B8201018301015F37405DDF03594F6BF5C227661095B28149061D76125E11FED86C40D33700C935FE8F69E4AB52AE7CE9039CBD119BB5EDC3887FF610789C81FD7E0A46CA63547282F4";
 BYTE szTerminalClosedCert[] = "763BFB4FFA7D16471FB331F23C8FA73AD3247DC6AC915D36A218B724E85F7FCA";
 //// 
 //// SetSystemTraceMode(TRUE);
 //// 
 DWORD dwErr = OpenCardWithHandleByGlobalData(L"ACS ACR1281 1S Dual Reader ICC 0", szOKOCert, szTerminalOpenCert, szUC1Cert, szTerminalClosedCert, szCIN);


 ////// //Авторизация
 //// DWORD tAuthoCardBegin = GetTickCount();
 BYTE nRestTryOut = 0;
 dwErr = Authorise("3296", &nRestTryOut);
 ////// DWORD tAuthoCardEnd = GetTickCount();
 ////// double dAuthoCardInSeconds = tAuthoCardEnd - tAuthoCardBegin;

 DWORD dwBufferSize = 10000;
 WCHAR szBuffer[10000];
 ReadPrivateDataInString((WCHAR*)szBuffer, dwBufferSize);
 MessageBox(NULL, szBuffer, NULL, 0);
 ReadMainOMSDataInString((WCHAR*)szBuffer, dwBufferSize);
 MessageBox(NULL, szBuffer, NULL, 0);
 

 ////// //Чтение приватных данных
 ////// DWORD tReadPrivateDataBegin = GetTickCount();
 //PrivateData data;
 //dwErr = ReadPrivateData(&data);
 ////// DWORD tReadPrivateDataEnd = GetTickCount();
 ////// double dReadPrivateDataInSeconds = tReadPrivateDataEnd - tReadPrivateDataBegin;

 ////// //Чтение данных страхования
 ////// DWORD tReadOmsDataBegin = GetTickCount();
 ////   OMSData omsData[11];
 ////   DWORD dwSize = 11;
 ////   ReadOMSData(omsData, &dwSize);
 ////// DWORD tReadOmsDataEnd = GetTickCount();
 ////// DWORD dReadOmsDataInSeconds = tReadOmsDataEnd - tReadOmsDataBegin;
 ////// 
 ////// OMSData oData;
 ////// wcscpy(oData.szOGRN, L"1027739099772");
 ////// wcscpy(oData.szOKATO, L"45000");
 ////// wcscpy(oData.szInsuranceDate, L"20140307");
 ////// wcscpy(oData.szInsuranceEndDate, L"20300101");
 //////// 
 ////// WriteOMSData(&oData);

 //// //WCHAR szBuffer[300];
 //// //GetCurrentComputerNameWithTakingRemoteSession(szBuffer);

   /* CardReaderInfo info[10];
    DWORD dwReaderInfoCount;
    GetReaderList(NULL, &dwReaderInfoCount);
    CardReaderInfo* pInfo = new CardReaderInfo[dwReaderInfoCount];
    GetReaderList(pInfo, &dwReaderInfoCount);
    delete[] pInfo;*/

  CardReaderInfo info[10];
  DWORD dwReaderInfoCount = 10;
  GetReaderList(info, &dwReaderInfoCount);
 
}

