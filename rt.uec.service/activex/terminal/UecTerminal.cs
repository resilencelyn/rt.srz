// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UecTerminal.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The uek terminal.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using rt.uec.service.dto;

namespace rt.uec.service.activex.terminal
{
  #region references

  

  #endregion

  /// <summary>
  /// The uek terminal.
  /// </summary>
  [Guid("6C20493B-E51F-4CCC-A3FA-64CD7CEED2C7")]
  [ComVisible(true)]
  [ClassInterface(ClassInterfaceType.AutoDispatch)]
  public class UecTerminal : IUecTerminal
  {
    /// <summary>
    /// The settings file name.
    /// </summary>
    private const string settingsFileName = "terminal.ini";

    /// <summary>
    /// The uec max string length.
    /// </summary>
    private const int UECMaxStringLength = 1024 + 1;

    #region UECLib Imports

    /// <summary>
    /// The open card.
    /// </summary>
    /// <param name="uekServiceToken">
    /// The uek service token.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    [DllImport("UECLib.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "OpenCard")]
    public static extern uint OpenCardExternal([MarshalAs(UnmanagedType.LPWStr)] string uekServiceToken);

    /// <summary>
    /// The authorise.
    /// </summary>
    /// <param name="pinCode">
    /// The pin code.
    /// </param>
    /// <param name="pinRestTriesOut">
    /// The pin rest tries out.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    [DllImport("UECLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint Authorise(
      [MarshalAs(UnmanagedType.LPStr)] string pinCode,
      [In] [Out] ref byte pinRestTriesOut);

    /// <summary>
    /// The close card.
    /// </summary>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    [DllImport("UECLib.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CloseCard")]
    public static extern uint CloseCardExternal();

    /// <summary>
    /// The get error description.
    /// </summary>
    /// <param name="errorCode">
    /// The error code.
    /// </param>
    /// <param name="errorDescription">
    /// The error description.
    /// </param>
    [DllImport("UECLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetErrorDescription(uint errorCode, StringBuilder errorDescription);

    /// <summary>
    /// The get reader list.
    /// </summary>
    /// <param name="cardReaderInfo">
    /// The card reader info.
    /// </param>
    /// <param name="dataSize">
    /// The data size.
    /// </param>
    [DllImport("UECLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetReaderList([MarshalAs(UnmanagedType.LPArray)] [In] [Out] CardReaderInfo[] cardReaderInfo, [In] [Out] ref ulong dataSize);

    /// <summary>
    /// The read private data.
    /// </summary>
    /// <param name="privateData">
    /// The private data.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    [DllImport("UECLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint ReadPrivateData([In] [Out] ref PrivateData privateData);

    /// <summary>
    /// The write oms data.
    /// </summary>
    /// <param name="privateData">
    /// The oms data.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    [DllImport("UECLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint WriteOMSData([In] ref OMSData omsData);

    /// <summary>
    /// The read oms data
    /// </summary>
    /// <param name="omsData">The oms data.</param>
    /// <param name="dataSize"></param>
    [DllImport("UECLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint ReadMainOMSData([In] [Out] ref OMSData omsData);

    #endregion

    #region Kernel32 Imports

    /// <summary>
    /// The get private profile string.
    /// </summary>
    /// <param name="lpAppName">
    /// The lp app name.
    /// </param>
    /// <param name="lpKeyName">
    /// The lp key name.
    /// </param>
    /// <param name="lpDefault">
    /// The lp default.
    /// </param>
    /// <param name="lpReturnedString">
    /// The lp returned string.
    /// </param>
    /// <param name="nSize">
    /// The n size.
    /// </param>
    /// <param name="lpFileName">
    /// The lp file name.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern uint GetPrivateProfileString(
      string lpAppName,
      string lpKeyName,
      string lpDefault,
      StringBuilder lpReturnedString,
      uint nSize,
      string lpFileName);

    /// <summary>
    /// The write private profile string.
    /// </summary>
    /// <param name="lpAppName">
    /// The lp app name.
    /// </param>
    /// <param name="lpKeyName">
    /// The lp key name.
    /// </param>
    /// <param name="lpValue">
    /// The lp value.
    /// </param>
    /// <param name="lpFileName">
    /// The lp file name.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern uint WritePrivateProfileString(
      string lpAppName,
      string lpKeyName,
      string lpValue,
      string lpFileName);

    #endregion

    #region Public Methods

    /// <summary>
    /// Возращает описание ошибки
    /// </summary>
    /// <param name="errorCode">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string GetErrorString(uint errorCode)
    {
      var errorString = new StringBuilder(UECMaxStringLength);
      GetErrorDescription(errorCode, errorString);
      return errorString.ToString();
    }

    /// <summary>
    /// Открытие карты
    /// </summary>
    /// <param name="uekServiceToken">
    /// The uek Service Token.
    /// </param>
    /// <returns>
    /// Результат открытия карты, или описание ошибки прикладного уровня
    /// </returns>
    [ComVisible(true)]
    public OperationResult OpenCard(string uekServiceToken)
    {
      var res = new OperationResult();
      try
      {
        res.Result = OpenCardExternal(uekServiceToken);
        res.ErrorString = GetErrorString(res.Result);
      }
      catch (Exception ex)
      {
        res.ErrorString = ex.Message;
      }

      return res;
    }

    /// <summary>
    /// Авторизация держателя карты
    /// </summary>
    /// <param name="pszPinIn">
    /// Пин код 1
    /// </param>
    /// <returns>
    /// Результат авторизации
    /// </returns>
    [ComVisible(true)]
    public AuthorizeResult Authorize(string pszPinIn)
    {
      var res = new AuthorizeResult();
      try
      {
        byte pinRestTriesOut = 0;
        res.Result = Authorise(pszPinIn, ref pinRestTriesOut);
        res.PinRestTriesOut = pinRestTriesOut;
        res.ErrorString = GetErrorString(res.Result);
      }
      catch (Exception ex)
      {
        res.ErrorString = ex.Message;
      }

      return res;
    }

    /// <summary>
    ///   Закрытие карты
    /// </summary>
    /// <returns>Результат закрытия карты</returns>
    [ComVisible(true)]
    public CloseResult CloseCard()
    {
      var res = new CloseResult();

      try
      {
        res.Result = CloseCardExternal();
        res.ErrorString = GetErrorString(res.Result);
      }
      catch (Exception ex)
      {
        res.ErrorString = ex.Message;
      }

      return res;
    }

    /// <summary>
    ///   Считывание персональных данных держателя карты
    /// </summary>
    /// <returns>Персональные данные держателя карты</returns>
    [ComVisible(true)]
    public GetCardInfoResult GetCardInfo()
    {
      var res = new GetCardInfoResult();
      try
      {
        var privateData = new PrivateData();
        privateData.Init();
        res.Result = ReadPrivateData(ref privateData);
        if (res.Result == 0)
        {
          res.FirstName = privateData.FirstName;
          res.LastName = privateData.LastName;
          res.MiddleName = privateData.MiddleName;
          if (privateData.BirthDate != null)
          {
            res.Birthday = DateTime.ParseExact(privateData.BirthDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
          }

          res.Birthplace = privateData.BirthPlace;
          res.Gender = (privateData.Gender == "М" ? 273 : 274).GetHashCode().ToString(CultureInfo.InvariantCulture);
          var ss0 = privateData.SNILS.Substring(0, 3);
          var ss1 = privateData.SNILS.Substring(3, 3);
          var ss2 = privateData.SNILS.Substring(6, 3);
          var ss3 = privateData.SNILS.Substring(9, 2);
          res.Snils = string.Format("{0}-{1}-{2} {3}", ss0, ss1, ss2, ss3);

          // todo сменить фейк после показа
          ////res.PolisNumber = privateData.PolicyNumberOMS;
          res.PolisNumber = "7855310843003223";

          res.DocumentType = privateData.DocumentType;
          res.DocumentSeries = privateData.DocumentSeries;
          res.DocumentNumber = privateData.DocumentNumber;
          res.DocumentIssueAuthority = privateData.DocumentIssueAuthority;

          if (privateData.DocumentIssueDate != null)
          {
            res.DocumentIssueDate = DateTime.ParseExact(privateData.DocumentIssueDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
          }
          else
          {
            res.DocumentIssueDate = string.Empty;
          }
        }

        res.ErrorString = GetErrorString(res.Result);
      }
      catch (Exception ex)
      {
        res.ErrorString = ex.Message;
      }

      return res;
    }

    /// <summary>
    /// Запись текущей страховки на карту УЭК
    /// </summary>
    /// <param name="omsData">
    /// The oms Data.
    /// </param>
    /// <returns>
    /// The <see cref="OperationResult"/>.
    /// </returns>
    [ComVisible(true)]
    public OperationResult WriteOmsData(string lastName, string firstName, string middleName, string birthDate, string ogrn, string okato, string dateFrom, string dateTo)
    {
      var res = new OperationResult();
      try
      {
        OMSData omsData = new OMSData
        {
          LastName = lastName,
          FirstName = firstName,
          MiddleName = middleName,
          BirthDate = string.IsNullOrEmpty(birthDate) ? string.Empty : DateTime.ParseExact(birthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"),
          OGRN = ogrn,
          OKATO = okato,
          InsuranceDate = string.IsNullOrEmpty(dateFrom) ? string.Empty : DateTime.ParseExact(dateFrom, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"),
          InsuranceEndDate = string.IsNullOrEmpty(dateTo) ? string.Empty : DateTime.ParseExact(dateTo, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"),
        };

        res.Result = WriteOMSData(ref omsData);
        res.ErrorString = GetErrorString(res.Result);
      }
      catch (Exception ex)
      {
        res.ErrorString = ex.Message;
      }

      return res;
    }

    /// <summary>
    /// Чтение списка подключенных карт ридеров
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    [ComVisible(true)]
    public string GetReaderList()
    {
      var res = new GetReaderListResult();
      res.ReaderNameList = new List<string>();

      try
      {
        CardReaderInfo[] readerInfo =
        {
          new CardReaderInfo { ReaderName = new string('\0', UECMaxStringLength) }, 
          new CardReaderInfo { ReaderName = new string('\0', UECMaxStringLength) }, 
          new CardReaderInfo { ReaderName = new string('\0', UECMaxStringLength) }, 
          new CardReaderInfo { ReaderName = new string('\0', UECMaxStringLength) }, 
          new CardReaderInfo { ReaderName = new string('\0', UECMaxStringLength) }, 
          new CardReaderInfo { ReaderName = new string('\0', UECMaxStringLength) }, 
          new CardReaderInfo { ReaderName = new string('\0', UECMaxStringLength) }, 
          new CardReaderInfo { ReaderName = new string('\0', UECMaxStringLength) }, 
          new CardReaderInfo { ReaderName = new string('\0', UECMaxStringLength) }, 
          new CardReaderInfo { ReaderName = new string('\0', UECMaxStringLength) }
        };

        ulong size = 10;
        GetReaderList(readerInfo, ref size);
        if (size > 0)
        {
          for (uint i = 0; i < size; i++)
          {
            res.ReaderNameList.Add(readerInfo[i].ReaderName);
          }
        }
      }
      catch (Exception ex)
      {
        res.ErrorString = ex.Message;
      }

      var resultBuilder = new StringBuilder();
      foreach (var reader in res.ReaderNameList)
      {
        resultBuilder.Append(reader);
        resultBuilder.Append(";");
      }
      return resultBuilder.ToString();
    }

    /// <summary>
    /// Считывает значение из конфигурационного файла - terminal.ini
    /// </summary>
    /// <param name="key">
    /// </param>
    /// <param name="value">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    [ComVisible(true)]
    public string GetCurrentReaderSettings()
    {
      var pathToIni = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), settingsFileName);
      var value = new StringBuilder(1000);
      GetPrivateProfileString("Reader", "Name", string.Empty, value, 1000, pathToIni);
      return value.ToString();
    }

    /// <summary>
    /// Записывает значение в конфигурационный файл - terminal.ini
    /// </summary>
    /// <param name="value">
    /// </param>
    [ComVisible(true)]
    public void SaveCurrentReaderSetting(string value)
    {
      var pathToIni = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), settingsFileName);
      WritePrivateProfileString("Reader", "Name", value, pathToIni);
    }

    /// <summary>
    /// Имя компьютера
    /// </summary>
    /// <returns></returns>
    [ComVisible(true)]
    public string GetWorkstationName()
    {
      return SystemInformation.ComputerName;
    }

    /// <summary>
    /// Чтение текущих данных о страховке
    /// </summary>
    /// <returns></returns>
    [ComVisible(true)]
    public OMSDataResult ReadMainOmsData()
    {
      var res = new OMSDataResult();
      try
      {
        var omsData = new OMSData();
        omsData.Init();
        res.Result = ReadMainOMSData(ref omsData);
        if (res.Result == 0)
        {
          res.Ogrn = omsData.OGRN;
          res.Okato = omsData.OKATO;
          res.DateFrom = DateTime.ParseExact(omsData.InsuranceDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
          res.DateTo = DateTime.ParseExact(omsData.InsuranceEndDate, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
        }
        res.ErrorString = GetErrorString(res.Result);
      }
      catch (Exception ex)
      {
        res.ErrorString = ex.Message;
      }

      return res;
    }

    #endregion
  }
}