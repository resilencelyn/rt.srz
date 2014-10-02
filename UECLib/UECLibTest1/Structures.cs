using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UECLibTest1
{
  [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
  public struct PrivateData
  {
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string SNILS;                  //СНИЛС
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string PolicyNumberOMS;        //Номер полиса ОМС
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string IssuerAddress;          //Адрес эмитента
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string BirthDate;              //Дата рождения
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string BirthPlace;             //Место рождения
    [MarshalAs(UnmanagedType.LPWStr)]                     
    public string Gender;                 //Пол
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string LastName;               //Фамилия
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string FirstName;              //Имя
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string MiddleName;             //Отчество
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string Address;                //Адрес
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string TelNumber;              //Телефон
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string Email;                  //E-Mail
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string INN;                    //ИНН
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string DriveLicenseNumber;     //Номер водительского удостоверения
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string TransportNumber;        //Номер регистрации транспорта
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string DocumentType;           //Тип документа УДЛ
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string DocumentSeries;         //Серия документа
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string DocumentNumber;         //Номер документа
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string DocumentIssueDate;      //Дата выдачи документа
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string DocumentEndDate;        //Дата окончания действия документа
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string DocumentIssueAuthority; //Кем выдан
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string DocumentDepartmentCode; //Код подразделения
    [MarshalAs(UnmanagedType.LPWStr)] 
    public string DocumentAdditionalData; //Дополнительные сведения

    public void Init()
    {
      SNILS = new string('\0', 1024);                  //СНИЛС
      PolicyNumberOMS = new string('\0', 1024);        //Номер полиса ОМС
      IssuerAddress = new string('\0', 1024);          //Адрес эмитента
      BirthDate = new string('\0', 1024);              //Дата рождения
      BirthPlace = new string('\0', 1024);             //Место рождения
      Gender = new string('\0', 1024);                 //Пол
      LastName = new string('\0', 1024);               //Фамилия
      FirstName = new string('\0', 1024);              //Имя
      MiddleName = new string('\0', 1024);             //Отчество
      Address = new string('\0', 1024);                //Адрес
      TelNumber = new string('\0', 1024);              //Телефон
      Email = new string('\0', 1024);                  //E-Mail
      INN = new string('\0', 1024);                    //ИНН
      DriveLicenseNumber = new string('\0', 1024);     //Номер водительского удостоверения
      TransportNumber = new string('\0', 1024);        //Номер регистрации транспорта
      DocumentType = new string('\0', 1024);           //Тип документа УДЛ
      DocumentSeries = new string('\0', 1024);         //Серия документа
      DocumentNumber = new string('\0', 1024);         //Номер документа
      DocumentIssueDate = new string('\0', 1024);      //Дата выдачи документа
      DocumentEndDate = new string('\0', 1024);        //Дата окончания действия документа
      DocumentIssueAuthority = new string('\0', 1024); //Кем выдан
      DocumentDepartmentCode = new string('\0', 1024); //Код подразделения
      DocumentAdditionalData = new string('\0', 1024); //Дополнительные сведения
    }
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct OMSData
  {
    [MarshalAs(UnmanagedType.LPWStr)]
    public string LastName;            //ОГРН
    [MarshalAs(UnmanagedType.LPWStr)]
    public string FirstName;            //ОГРН
    [MarshalAs(UnmanagedType.LPWStr)]
    public string MiddleName;            //ОГРН
    [MarshalAs(UnmanagedType.LPWStr)]
    public string BirthDate;            //ОГРН
    [MarshalAs(UnmanagedType.LPWStr)]
    public string OGRN;                //ОГРН
    [MarshalAs(UnmanagedType.LPWStr)]
    public string OKATO;               //OKATO
    [MarshalAs(UnmanagedType.LPWStr)]
    public string InsuranceDate;       //Дата страхования
    [MarshalAs(UnmanagedType.LPWStr)]
    public string InsuranceEndDate;    //Дата окончания страхования
  
    public void Init()
    {
      OGRN = new string('\0', 100);                //ОГРН
      OKATO = new string('\0', 100);               //OKATO
      InsuranceDate = new string('\0', 100);       //Дата страхования
      InsuranceEndDate = new string('\0', 100);    //Дата окончания страхования
    }
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct CardReaderInfo
  {
    [MarshalAs(UnmanagedType.LPWStr)]
    public string readerName;  //Имя устройства
    
    public void Init()
    {
      readerName = new string('\0', 1000); //Имя устройства
    }
  }
}
