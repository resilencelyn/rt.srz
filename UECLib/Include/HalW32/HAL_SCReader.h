#ifndef _HAL_SCREADER_H_
#define _HAL_SCREADER_H_

#include "HAL_SCRHandle.h"
#include "HAL_SCRApdu.h"

//  Description:
//      �������������� �����-���� �����.
//  See Also:
//		crDeinit
//  Arguments:
//      pilRdrHandle	- ��������� �� ���������������� ��������� ����-������. 
//						  � ���� ������� ���������� ������������� ����� ���������� ����� ��������� � ������������� � ���������� ���������� ���� ���������. 
//      ilRdrSettings	- ��������� �� ���������������� ������������ ��������� ����-������.
//						  � ���� ������� ���������� ������������� ����� ���������� ����� ��������� � ������������� � ���������� ���������� ����. 	
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ������������� �����-���� ������.
IL_FUNC IL_RETCODE crInit(IL_HANDLE_READER pilRdrHandle, IL_READER_SETTINGS ilRdrSettings);

//  Description:
//      ��������� ������ ������ � ������.
//  See Also:
//		crCloseSession
//  Arguments:
//      pilRdrHandle	- ��������� �� ���������������� ��������� ����-������. 
//						  � ���� ������� ���������� ������������� ����� ���������� ����� ��������� � ������������� � ���������� ���������� ���� ���������. 
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      �������� ������ ������ � ������.
IL_FUNC IL_RETCODE crOpenSession(IL_HANDLE_READER pilRdrHandle);

//  Description:
//      ����������� APDU �������.
//  See Also:
//		crDeinit
//  Arguments:
//      pilRdrHandle - ��������� �� ���������������� ��������� ����-������. 
//					   � ���� ������� ���������� ������������� ����� ���������� ����� ��������� � ������������� � ���������� ���������� ���� ���������. 
//      pilApdu		 - ��������� �� ��������� ����������� APDU �������.
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� APDU �������.
IL_FUNC IL_RETCODE crAPDU(IL_HANDLE_READER pilRdrHandle, IL_APDU *pilApdu);

//  Description:
//      ��������� ������ ������ � ������.
//  See Also:
//		crOpenSession
//  Arguments:
//      pilRdrHandle	- ��������� �� ���������������� ��������� ����-������. 
//						  � ���� ������� ���������� ������������� ����� ���������� ����� ��������� � ������������� � ���������� ���������� ���� ���������. 
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ���������� ������ ������ � ������.
IL_FUNC IL_RETCODE crCloseSession(IL_HANDLE_READER pilRdrHandle);

//  Description:
//      ���������������� �����-���� �����.
//  See Also:
//		crInit
//  Arguments:
//      pilRdrHandle	- ��������� �� ���������������� ��������� ����-������. 
//						  � ���� ������� ���������� ������������� ����� ���������� ����� ��������� � ������������� � ���������� ���������� ���� ���������. 
//  Return Value:
//      IL_RETCODE	- ��� ������.
//  Summary:
//      ��������������� �����-���� ������.
IL_FUNC IL_RETCODE crDeinit(IL_HANDLE_READER pilRdrHandle);

#endif