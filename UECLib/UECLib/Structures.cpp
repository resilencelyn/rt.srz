#include "stdafx.h"
#include "Structures.h"

PrivateData::PrivateData()
{
  szSNILS = new WCHAR[1024];
  memset(szSNILS, 0, 1024*2);
  szPolicyNumberOMS  = new WCHAR[1024];
  memset(szPolicyNumberOMS, 0, 1024*2);
  szIssuerAddress = new WCHAR[1024];
  memset(szIssuerAddress, 0, 1024*2);
  szBirthDate = new WCHAR[1024];
  memset(szBirthDate, 0, 1024*2);
  szBirthPlace = new WCHAR[1024];
  memset(szBirthPlace, 0, 1024*2);
  szGender = new WCHAR[1024];
  memset(szGender, 0, 1024*2);
  szLastName = new WCHAR[1024];
  memset(szLastName, 0, 1024*2);
  szFirstName = new WCHAR[1024];
  memset(szFirstName, 0, 1024*2);
  szMiddleName = new WCHAR[1024];
  memset(szMiddleName, 0, 1024*2);
  szAddress = new WCHAR[1024];  
  memset(szAddress, 0, 1024*2);
  szTelNumber= new WCHAR[1024];
  memset(szTelNumber, 0, 1024*2);
  szEmail = new WCHAR[1024];
  memset(szEmail, 0, 1024*2);
  szINN = new WCHAR[1024];
  memset(szINN, 0, 1024*2);
  szDriveLicenseNumber = new WCHAR[1024];
  memset(szDriveLicenseNumber, 0, 1024*2);
  szTransportNumber = new WCHAR[1024];
  memset(szTransportNumber, 0, 1024*2);
  szDocumentType = new WCHAR[1024];
  memset(szDocumentType, 0, 1024*2);
  szDocumentSeries = new WCHAR[1024];
  memset(szDocumentSeries, 0, 1024*2);
  szDocumentNumber = new WCHAR[1024];
  memset(szDocumentNumber, 0, 1024*2);
  szDocumentIssueDate = new WCHAR[1024];
  memset(szDocumentIssueDate, 0, 1024*2);
  szDocumentEndDate = new WCHAR[1024];
  memset(szDocumentEndDate, 0, 1024*2);
  szDocumentIssueAuthority = new WCHAR[1024];
  memset(szDocumentIssueAuthority, 0, 1024*2);
  szDocumentDepartmentCode = new WCHAR[1024];
  memset(szDocumentDepartmentCode, 0, 1024*2);
  szDocumentAdditionalData = new WCHAR[1024];
  memset(szDocumentAdditionalData, 0, 1024*2);
}

PrivateData::~PrivateData()
{
  delete[] szSNILS;
  delete[] szPolicyNumberOMS;
  delete[] szIssuerAddress;
  delete[] szBirthDate;
  delete[] szBirthPlace;
  delete[] szGender;
  delete[] szLastName;
  delete[] szFirstName;
  delete[] szMiddleName;
  delete[] szAddress;
  delete[] szTelNumber;
  delete[] szEmail;
  delete[] szINN;
  delete[] szDriveLicenseNumber;
  delete[] szTransportNumber;
  delete[] szDocumentType;
  delete[] szDocumentSeries;
  delete[] szDocumentNumber;
  delete[] szDocumentIssueDate;
  delete[] szDocumentEndDate;
  delete[] szDocumentIssueAuthority;
  delete[] szDocumentDepartmentCode;
  delete[] szDocumentAdditionalData;
}

OMSData::OMSData()
{
  szLastName = new WCHAR[1024];
  memset(szLastName, 0, 1024*2);
  szFirstName = new WCHAR[1024];             
  memset(szFirstName, 0, 1024*2);
  szMiddleName = new WCHAR[1024];
  memset(szMiddleName, 0, 1024*2);
  szBirthDate = new WCHAR[1024];
  memset(szBirthDate, 0, 1024*2);
  szOGRN = new WCHAR[1024];
  memset(szOGRN, 0, 1024*2);
  szOKATO = new WCHAR[1024];
  memset(szOKATO, 0, 1024*2);
  szInsuranceDate = new WCHAR[1024];
  memset(szInsuranceDate, 0, 1024*2);
  szInsuranceEndDate = new WCHAR[1024];
  memset(szInsuranceEndDate, 0, 1024*2);
}

OMSData::OMSData(const OMSData& o)
{
  OMSData();
  wcscpy(szLastName, o.szLastName);
  wcscpy(szFirstName, o.szFirstName);
  wcscpy(szMiddleName, o.szMiddleName);
  wcscpy(szBirthDate, o.szBirthDate);
  wcscpy(szOGRN, o.szOGRN);
  wcscpy(szOKATO, o.szOKATO);
  wcscpy(szInsuranceDate, o.szInsuranceDate);
  wcscpy(szInsuranceEndDate, o.szInsuranceEndDate);
}

OMSData& OMSData::operator=(OMSData& o)
{
  wcscpy(szLastName, o.szLastName);
  wcscpy(szFirstName, o.szFirstName);
  wcscpy(szMiddleName, o.szMiddleName);
  wcscpy(szBirthDate, o.szBirthDate);
  wcscpy(szOGRN, o.szOGRN);
  wcscpy(szOKATO, o.szOKATO);
  wcscpy(szInsuranceDate, o.szInsuranceDate);
  wcscpy(szInsuranceEndDate, o.szInsuranceEndDate);
  return *this;
}

OMSData::~OMSData()
{
  delete[] szLastName;
  delete[] szFirstName;
  delete[] szMiddleName;
  delete[] szBirthDate;
  delete[] szOGRN;
  delete[] szOKATO;
  delete[] szInsuranceDate;
  delete[] szInsuranceEndDate;
}

CardReaderInfo::CardReaderInfo()
{
  szReaderName = new WCHAR[1024];
  memset(szReaderName, 0, 1024*2);
}

CardReaderInfo::~CardReaderInfo()
{
  delete[] szReaderName;
}