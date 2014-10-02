//
//Структуры
//
#pragma once

//Личные данные владельца УЭК
struct UECLIB_API PrivateData
{
  //конструктор
public:    
  PrivateData();
  ~PrivateData();
  
//Поля
public:    
  WCHAR* szSNILS;                 //СНИЛС
  WCHAR* szPolicyNumberOMS;       //Номер полиса ОМС
  WCHAR* szIssuerAddress;         //Адрес эмитента
  WCHAR* szBirthDate;             //Дата рождения
  WCHAR* szBirthPlace;            //Место рождения
  WCHAR* szGender;                //Пол
  WCHAR* szLastName;              //Фамилия
  WCHAR* szFirstName;             //Имя
  WCHAR* szMiddleName;            //Отчество
  WCHAR* szAddress;               //Адрес
  WCHAR* szTelNumber;             //Телефон
  WCHAR* szEmail;                 //E-Mail
  WCHAR* szINN;                   //ИНН
  WCHAR* szDriveLicenseNumber;    //Номер водительского удостоверения
  WCHAR* szTransportNumber;       //Номер регистрации транспорта
  WCHAR* szDocumentType;          //Тип документа УДЛ
  WCHAR* szDocumentSeries;        //Серия документа
  WCHAR* szDocumentNumber;        //Номер документа
  WCHAR* szDocumentIssueDate;     //Дата выдачи документа
  WCHAR* szDocumentEndDate;       //Дата окончания действия документа
  WCHAR* szDocumentIssueAuthority;//Кем выдан
  WCHAR* szDocumentDepartmentCode;//Код подразделения
  WCHAR* szDocumentAdditionalData;//Дополнительные сведения
};

//Данные о страховке
struct UECLIB_API OMSData
{
  //конструктор
public:    
  OMSData();
  OMSData(const OMSData& o);
  ~OMSData();
  OMSData& operator = (OMSData& o); 

public:
  WCHAR* szLastName;         //Фамилия
  WCHAR* szFirstName;        //Имя
  WCHAR* szMiddleName;       //Отчество
  WCHAR* szBirthDate;        //Дата рождения
  
  WCHAR* szOGRN;             //ОГРН страховщика
  WCHAR* szOKATO;            //ОКАТО страховщика
  WCHAR* szInsuranceDate;    //Дата страхования
  WCHAR* szInsuranceEndDate; //Дата окончания страхования

};

//Данные о карт-ридере
struct UECLIB_API CardReaderInfo
{
public:
  CardReaderInfo();
  ~CardReaderInfo();

public:
  WCHAR* szReaderName;
};


