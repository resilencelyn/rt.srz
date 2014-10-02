//
//���������
//
#pragma once

//������ ������ ��������� ���
struct UECLIB_API PrivateData
{
  //�����������
public:    
  PrivateData();
  ~PrivateData();
  
//����
public:    
  WCHAR* szSNILS;                 //�����
  WCHAR* szPolicyNumberOMS;       //����� ������ ���
  WCHAR* szIssuerAddress;         //����� ��������
  WCHAR* szBirthDate;             //���� ��������
  WCHAR* szBirthPlace;            //����� ��������
  WCHAR* szGender;                //���
  WCHAR* szLastName;              //�������
  WCHAR* szFirstName;             //���
  WCHAR* szMiddleName;            //��������
  WCHAR* szAddress;               //�����
  WCHAR* szTelNumber;             //�������
  WCHAR* szEmail;                 //E-Mail
  WCHAR* szINN;                   //���
  WCHAR* szDriveLicenseNumber;    //����� ������������� �������������
  WCHAR* szTransportNumber;       //����� ����������� ����������
  WCHAR* szDocumentType;          //��� ��������� ���
  WCHAR* szDocumentSeries;        //����� ���������
  WCHAR* szDocumentNumber;        //����� ���������
  WCHAR* szDocumentIssueDate;     //���� ������ ���������
  WCHAR* szDocumentEndDate;       //���� ��������� �������� ���������
  WCHAR* szDocumentIssueAuthority;//��� �����
  WCHAR* szDocumentDepartmentCode;//��� �������������
  WCHAR* szDocumentAdditionalData;//�������������� ��������
};

//������ � ���������
struct UECLIB_API OMSData
{
  //�����������
public:    
  OMSData();
  OMSData(const OMSData& o);
  ~OMSData();
  OMSData& operator = (OMSData& o); 

public:
  WCHAR* szLastName;         //�������
  WCHAR* szFirstName;        //���
  WCHAR* szMiddleName;       //��������
  WCHAR* szBirthDate;        //���� ��������
  
  WCHAR* szOGRN;             //���� �����������
  WCHAR* szOKATO;            //����� �����������
  WCHAR* szInsuranceDate;    //���� �����������
  WCHAR* szInsuranceEndDate; //���� ��������� �����������

};

//������ � ����-������
struct UECLIB_API CardReaderInfo
{
public:
  CardReaderInfo();
  ~CardReaderInfo();

public:
  WCHAR* szReaderName;
};


