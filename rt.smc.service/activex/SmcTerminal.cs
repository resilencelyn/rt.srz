// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmcTerminal.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Windows.Forms;
using Pc.Policy.Smartcard;
using Pc.Policy.Smartcard.Crypto;
using Pc.Policy.Smartcard.Data;
using Pc.Policy.Smartcard.Pinpad;
using Pc.Policy.Smartcard.Shared;
using Pc.Shared.BER.Converter;
using Pc.Shared.Security.Crypto;
using Pc.Shared.Utils.Extensions;
using ProtoBuf.ServiceModel;
using rt.smc.model;
using SmartCard.PCSC;
using SmartCard.PCSC.Native;
using rt.smc.service.serviceClient;
using rt.uec.model.Interfaces;

#endregion

namespace rt.smc.service.activex
{
  using rt.core.model.client;

  /// <summary>
  ///   The elektropolis.
  /// </summary>
  [Guid("32B3D0CF-3031-40B1-B074-53495BE5D158")]
  [ComVisible(true)]
  [ClassInterface(ClassInterfaceType.AutoDispatch)]
  public class SmcTerminal : ISmcTerminal
  {
    /// <summary>
    ///   Прокси
    /// </summary>
    private WcfProxy<IUecService> proxy;

    private string url  ;

    /// <summary>
    ///   Активный выбранный пользователем токен
    /// </summary>
    public string ActiveToken { get; set; }

    /// <summary>
    ///   Активный выбранный пользователем кард-ридер
    /// </summary>
    public string ActiveCardReader { get; set; }

    /// <summary>
    ///   Сервис
    /// </summary>
    protected IUecService Service
    {
      get
      {
        if (proxy == null || proxy.State != CommunicationState.Opened)
        {
          if (proxy != null)
          {
            proxy.Dispose();
          }

          var path = Assembly.GetExecutingAssembly().Location + ".config";
          var canalFactory = new CustomClientChannel<IUecService>(path);
          proxy = new WcfProxy<IUecService>(canalFactory.Endpoint.Binding, new EndpointAddress(url));
          proxy.Endpoint.Behaviors.Add(new TokenMessageInspector());
          proxy.Endpoint.Behaviors.Add(new ProtoEndpointBehavior());
          proxy.Open();
        }

        return proxy.Service;
      }
    }

    /// <summary>
    /// Открывает соединение к УЭК сервису
    /// </summary>
    /// <param name="token">
    ///   Авторизационный токен
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [ComVisible(true)]
    public bool OpenConnection(string token)
    {
      var split = token.Split('=');
      url = split[1];

      TokenMessageInspector.TokenFunc = () => new Token
      {
        Signature = string.Format("{0}=", split[0])
      };
      return true;
    }

    /// <summary>
    /// Закрывает соединение
    /// </summary>
    [ComVisible(true)]
    public void CloseConnection()
    {
      TokenMessageInspector.TokenFunc = () => null;
    }

    /// <summary>
    /// The set card reader.
    /// </summary>
    /// <param name="cardReader">
    /// The card reader.
    /// </param>
    [ComVisible(true)]
    public void SetCardReader()
    {
      ActiveCardReader = Service.GetCurrentSmcReaderName(SystemInformation.ComputerName);
      ActiveToken = Service.GetCurrentSmcTokenReaderName(SystemInformation.ComputerName);
    }

    /// <summary>
    ///   Получение списка доступных кард-ридеров в системе
    /// </summary>
    /// <returns> Список кард-ридеров </returns>
    [ComVisible(false)]
    public List<string> GetCardReaders()
    {
      // Вывод списка кард-ридеров, установленных в системе
      var manager = new PCSCReadersManager();
      manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);
      var cards = manager.OfType<ISCard>().ToList();
      var activeDevsName = new List<string>();
      foreach (var card in cards)
      {
        PolicySmartcardBase testConnect = null;
        try
        {
          testConnect = new PolicySmartcardBase(card);
        }
        catch (Exception)
        {
          testConnect = null;
        }
        finally
        {
          if (testConnect != null)
          {
            activeDevsName.Add(testConnect.BaseCard.ReaderName);
          }
        }
      }

      manager.ReleaseContext();
      return activeDevsName;
    }

    /// <summary>
    ///   Получение списка доступных токенов в системе
    /// </summary>
    /// <returns> Список токенов </returns>
    [ComVisible(false)]
    public List<string> GetRuTokens()
    {
      // Вывод списка токенов, установленных в системе
      var manager = new PCSCReadersManager();
      manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);
      var tokens = manager.OfType<ISCard>().Where(device => device.ReaderName.ToLower().Contains("tok")).ToList();
      var activeDevsName = new List<string>();
      foreach (var token in tokens)
      {
        PolicySmartcardBase testConnect = null;
        try
        {
          testConnect = new PolicySmartcardBase(token);
          testConnect.Connect();
        }
        catch
        {
          testConnect = null;
        }
        finally
        {
          if (testConnect != null)
          {
            activeDevsName.Add(testConnect.BaseCard.ReaderName);
            testConnect.Disconnect();
          }
        }
      }

      manager.ReleaseContext();
      return activeDevsName;
    }

    /// <summary>
    ///   Получение информации о карте (чип, номер карты, производитель и т.п.)
    /// </summary>
    /// <returns> The <see cref="CardInfoStrings" /> . </returns>
    [ComVisible(true)]
    public CardInfoStrings GetCardInfo()
    {
      var manager = new PCSCReadersManager();

      // Установление соединения с смарт-карт API
      manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);

      // Вывод списка ридеров в консоль
      if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveCardReader))
      {
        // Получение объекта ридера
        var card = manager[ActiveCardReader];

        // Создание объекта для работы с картой полиса ОМС
        var policy = new PolicySmartcardBase(card);

        // Подключение к карте полиса ОМС
        policy.Connect();

        // Чтение информации о карте полиса ОМС ( Идентификационные данные )
        var cid = policy.GetCardID();

        // Чтение информации о карте полиса ОМС ( Информация о микросхеме от производителя )
        var ccd = policy.GetICCD();

        CardInfoStrings cardInfo = null;
        if (cid != null)
        {
          cardInfo = new CardInfoStrings
                       {
                         CardHexNum = cid.SerialNumber.ToHexString(), 
                         CardType = cid.CardType, 
                         Vers = cid.CardVersion, 
                         InstitId = cid.InstitutionID.ToHexString(), 
                         AddInfo = cid.AdditionalInfo.ToHexString()
                       };
        }

        if (ccd != null)
        {
          cardInfo = cardInfo ?? new CardInfoStrings();
          cardInfo.IssuerCode = ccd.IssuerCode;
          cardInfo.IssuerData = ccd.IssuerData.ToHexString();
        }

        // Отключение от карты полиса ОМС
        policy.Disconnect();

        // Отключение от смарт-карт API
        manager.ReleaseContext();
        return cardInfo;
      }

      manager.ReleaseContext();
      return null;
    }

    /// <summary>
    ///   Получение информации о текущей СМО
    /// </summary>
    /// <returns> The <see cref="SmoInfoStrings" /> . </returns>
    [ComVisible(true)]
    public SmoInfoStrings GetCurrentSmo()
    {
      // Чтение информации о владельце карты полиса ОМС
      var manager = new PCSCReadersManager();

      // Установление соединения с смарт-карт API
      manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);

      // Вывод списка ридеров в консоль
      if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveCardReader))
      {
        // Получение объекта ридера
        var card = manager[ActiveCardReader];

        // Создание объекта для работы с картой полиса ОМС
        var policy = new PolicySmartcardBase(card);

        // Подключение к карте полиса ОМС
        policy.Connect();

        // Чтение информации о владельце полиса ОМС
        var smo = policy.GetCurrentSMOInformation();
        if (smo != null)
        {
          var smoInfo = new SmoInfoStrings
                          {
                            OgrnSmo = smo.OGRN, 
                            Okato = smo.OKATO, 
                            InsuranceStartDate =
                              FormatPolicyDate(smo.InsuranceStartDate, "Отсутствует"), 
                            InsuranceEndDate =
                              FormatPolicyDate(smo.InsuranceExpireDate, "Не ограничено")
                          };

          if (smo.EDS != null)
          {
            smoInfo.Eds = new Eds
                            {
                              Certificate = new X509Certificate2(smo.EDS.Certificate).Subject, 
                              Ecp = smo.EDS.Signature.ToHexString()
                            };

            try
            {
              smoInfo.Eds.StateEcp = smo.VerifyEDS() ? "Верна" : "Неверна";
            }
            catch
            {
            }
          }

          policy.Disconnect();
          manager.ReleaseContext();
          return smoInfo;
        }

        // Отключение от карты полиса ОМС
        policy.Disconnect();

        // Отключение от смарт-карт API
        manager.ReleaseContext();
        return null;
      }

      manager.ReleaseContext();
      return null;
    }

    /// <summary>
    ///   Чтение инфомрации о владельце карты (застрахованном)
    /// </summary>
    /// <returns> The <see cref="OwnerInfo" /> . </returns>
    [ComVisible(true)]
    public OwnerInfo GetOwnerInfo()
    {
      // Чтение информации о владельце карты полиса ОМС
      var manager = new PCSCReadersManager();
      try
      {
        // Установление соединения с смарт-карт API
        manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);

        // Вывод списка ридеров в консоль
        if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveCardReader))
        {
          // Получение объекта ридера
          var card = manager[ActiveCardReader];

          // Создание объекта для работы с картой полиса ОМС
          var policy = new PolicySmartcardBase(card);

          // Подключение к карте полиса ОМС
          policy.Connect();

          // Чтение информации о владельце полиса ОМС
          var ownerInfoCard = policy.GetOwnerInformation();
          if (ownerInfoCard != null)
          {
            var ownerInfo = new OwnerInfo
                              {
                                LastName = FormatPolicyText(ownerInfoCard.Identity_1, string.Empty),
                                FirstName = FormatPolicyText(ownerInfoCard.Identity_2, string.Empty),
                                MiddleName = FormatPolicyText(ownerInfoCard.Identity_3, string.Empty), 
                                Sex =
                                  ownerInfoCard.Sex == 1 ? "273" : ownerInfoCard.Sex == 2 ? "274" : string.Empty,
                                Birthday = FormatPolicyDate(ownerInfoCard.BirthDate, string.Empty),
                                BirthPlace = FormatPolicyText(ownerInfoCard.BirthPlace, string.Empty)
                              };

            if (ownerInfoCard.Citizenship != null)
            {
              ownerInfo.CitizenschipCode = FormatPolicyText(ownerInfoCard.Citizenship.CoutryCode, string.Empty);
              ownerInfo.CitizenschipName = FormatPolicyText(ownerInfoCard.Citizenship.CoutryCyrillicName, string.Empty);
            }

            ownerInfo.PolisNumber = ownerInfoCard.PolicyNumber;
            ownerInfo.PolisDateFrom = FormatPolicyDate(ownerInfoCard.IssueDate, string.Empty);
            ownerInfo.PolisDateTo = FormatPolicyDate(ownerInfoCard.ExpireDate, string.Empty);

            ownerInfo.Snils = FormatPolicyText(ownerInfoCard.SNILS, string.Empty);

            try
            {
              // Чтение атрибутов безопасности карты полиса ОМС
              var sod = policy.GetSecurityInformation();
              ownerInfo.StateEcp = ownerInfoCard.VerifyEDS(sod) ? "Верна" : "Неверна";
            }
            catch
            {
            }

            // Отключение от карты полиса ОМС
            policy.Disconnect();

            // Отключение от смарт-карт API
            manager.ReleaseContext();
            return ownerInfo;
          }

          // Отключение от карты полиса ОМС
          policy.Disconnect();

          // Отключение от смарт-карт API
          manager.ReleaseContext();
          return null;
        }

        manager.ReleaseContext();
        return null;
      }
      catch 
      {
        manager.ReleaseContext();
        throw;
      }
    }

    /// <summary>
    /// Смена СМО
    /// </summary>
    /// <param name="ogrnSmo">
    /// ОГРН СМО 
    /// </param>
    /// <param name="okatoSmo">
    /// ОКАТО СМО 
    /// </param>
    /// <returns>
    /// true - если всё выполнилось 
    /// </returns>
    [ComVisible(true)]
    public bool ChangeSmo(string ogrnSmo, string okatoSmo, string dateFrom, string dateTo, string securityModulePin, string cardPin)
    {
      // Чтение информации о владельце карты полиса ОМС
      var manager = new PCSCReadersManager();
      try
      {
        // Установление соединения с смарт-карт API
        manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);

        // Вывод списка ридеров в консоль
        if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveCardReader) == false)
        {
          manager.ReleaseContext();
          return false;
        }

        if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveToken) == false)
        {
          manager.ReleaseContext();
          return false;
        }

        // Получение объекта ридера
        var policyCard = manager[ActiveCardReader];
        var tokenCard = manager[ActiveToken];

        // Создание объекта для работы с картой полиса ОМС
        var policy = new PolicySmartcard(policyCard, tokenCard);

        // Подключение к карте полиса ОМС
        policy.Connect();

        // Аутентификация с картой модуля безопасности
        // Пин-код модуля безопасности 12345678
        policy.Authentificate_Mutual(securityModulePin, true);
        var verifyArgs = new VerifyPINArguments
        {
          // Данный параметр устанавливает признак использования пин-пада для 
          // предъявления пин-кода с помощью выносной клавиатуры. В случае установки данного параметра в значение false
          // требуется заполнение параметра 'PinValue'
          // Использование пин-пада возможно только в случае, если карта полиса ОМС установлена
          // в устройство с поддержкой функции выносной клавиатуры.
          // Проверить наличие функций выносной клавиатуры у устройства можно с помощью метода 
          // 'PolicyPinpad.IsDeviceSupported(policy_card, false
          // Не выполнять подключение к карте если подключение было выполнено внешними средствами
          UsePinpad = PolicyPinpad.IsDeviceSupported(policyCard, false), 

          // Значение пин-кода. Данный параметр используется только если UsePinpad = false
          PinValue = "1234", 

          // Ссылка на метод для отмены ввода пин-кода программным способом в случае использовании выносной клавиатуры
          // Инициализация происходит на стороне метода VerifyPIN
          CancelHandler = null, 

          // Ссылка на метод для обработки события нажатия кнопок на выносной клавиатуре
          // Можно не инициализировать - используется только в случае использовании выносной клавиатуры
          EnteredDigitHandler = key =>
                                  {
                                    // Тело метода, обработчика
                                  }
        };
        
        // Проверка пина
        policy.VerifyPIN(ref verifyArgs);

        // Парсим даты
        DateTime dtFrom = DateTime.Now;
        DateTime.TryParse(dateFrom, out dtFrom);

        // Парсим даты
        DateTime dtTo = DateTime.Now.AddYears(5);
        DateTime.TryParse(dateTo, out dtTo);
        
        // Создание объекта нового СМО
        var smo = new SMOInformation
                    {
                      OGRN = ogrnSmo, 
                      OKATO = okatoSmo,
                      InsuranceStartDate = dtFrom,
                      InsuranceExpireDate = dtTo, 
                      EDS = null /*ЭЦП будет инициализирована позже после ее создания*/
                    };

        // [Формирование ЭЦП нового СМО]
        // Открытие хранилища сертификатов текущего пользователя
        var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        store.Open(OpenFlags.ReadOnly);

        // Выбор сертификата с помощью строенного диалога
        var selected = X509Certificate2UI.SelectFromCollection(
          store.Certificates, 
          "Сертификата", 
          "Выберите сертификат для формирования ЭЦП СМО", 
          X509SelectionFlag.SingleSelection);
        if (selected.Count > 0)
        {
          // Создание объекта формирования подписи
          var signatureEncoder = new SignatureEncoder(selected[0]);

          // Формирование ЭЦП
          smo.EDS = new SMOInformationEDS
          {
            Signature = signatureEncoder.CreateSignHash(smo.EncodeBER()), // Преобразование объекта к формату BER-TLV
            Certificate = selected[0].Export(X509ContentType.Cert)
          };
        }

        // Запись информации о новом СМО на карту полиса ОМС
        policy.CreateNewSMO(smo);

        // Отключение от карты полиса ОМС
        policy.Disconnect();

        // Отключение от смарт-карт API
        manager.ReleaseContext();
        
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    ///   The get smo history.
    /// </summary>
    /// <returns> The List. </returns>
    [ComVisible(false)]
    public List<SmoInfoStrings> GetSmoHistory()
    {
      const string pinCard = "5473";

      // Чтение информации о владельце карты полиса ОМС
      var manager = new PCSCReadersManager();
      try
      {
        // Установление соединения с смарт-карт API
        manager.EstablishContext(READERSCONTEXTSCOPE.SCOPE_USER);

        // Вывод списка ридеров в консоль
        if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveCardReader) == false)
        {
          ////printf("Устройство чтения смарт-карт с именем [{0}] не найдено в системе.");
          manager.ReleaseContext();
          return null;
        }

        if (manager.OfType<ISCard>().Select(s => s.ReaderName).ToList().Contains(ActiveToken) == false)
        {
          ////printf("Устройство чтения смарт-карт с именем [{0}] не найдено в системе.");
          manager.ReleaseContext();
          return null;
        }

        // Получение объекта ридера
        var policyCard = manager[ActiveCardReader];
        var tokenCard = manager[ActiveToken];

        // Создание объекта для работы с картой полиса ОМС
        var policy = new PolicySmartcard(policyCard, tokenCard);

        // Подключение к карте полиса ОМС
        policy.Connect();


        // Аутентификация с картой модуля безопасности
        policy.Authentificate_External(pinCard);
        policy.Authentificate_External(pinCard, true);

        // Чтение истории СМО
        var historyList = policy.GetSMOHistory();
        var histSmos = new List<SmoInfoStrings>();
        historyList.ForEach(smo =>
                              {
                                if (smo != null)
                                {
                                  var smoInfo = new SmoInfoStrings
                                                  {
                                                    OgrnSmo = smo.OGRN, 
                                                    Okato = smo.OKATO, 
                                                    InsuranceStartDate =
                                                      FormatPolicyDate(
                                                        smo.InsuranceStartDate, "Отсутствует"), 
                                                    InsuranceEndDate =
                                                      FormatPolicyDate(
                                                        smo.InsuranceExpireDate, "Не ограничено"), 
                                                    Eds = smo.EDS == null
                                                            ? null
                                                            : new Eds
                                                                {
                                                                  Certificate =
                                                                    new X509Certificate2(smo.EDS.Certificate).Subject, 
                                                                  Ecp = smo.EDS.Signature.ToHexString()
                                                                }
                                                  };

                                  histSmos.Add(smoInfo);
                                }
                              });

        // Отключение от карты полиса ОМС
        policy.Disconnect();

        // Отключение от смарт-карт API
        manager.ReleaseContext();
        return histSmos;
      }
      catch
      {
        return null;
      }
    }

    /// <summary>
    /// The format policy date.
    /// </summary>
    /// <param name="date">
    /// The date. 
    /// </param>
    /// <param name="nullValue">
    /// The null_value. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    private string FormatPolicyDate(DateTime? date, string nullValue)
    {
      return date.HasValue == false ? nullValue : date.Value.ToString("dd.MM.yyyy");
    }

    /// <summary>
    /// The format policy text.
    /// </summary>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <param name="nullValue">
    /// The null value. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    private string FormatPolicyText(string value, string nullValue)
    {
      return string.IsNullOrEmpty(value) ? nullValue : value;
    }
  }
}